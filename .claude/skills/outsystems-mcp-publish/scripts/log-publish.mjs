#!/usr/bin/env node
// PostToolUse hook for mcp__servicestudio__omlPublish, registered by the outsystems-mcp-publish skill.
//
// Every publish that ran:  appends a row to PUBLISHES.md (a blocked call, which publishes nothing, adds no row).
// A successful publish:    appends one section per applyModelApiCode save to changes/<story>-<Module>.md: when, outcome,
//                          what it touched and a summary placeholder for Claude. The code itself is not written.
//
// The lambdas come from this session's transcript: every successful applyModelApiCode call for the
// module after the previous successful publish or omlReset of that module. The transcript format is
// internal to Claude Code, so parsing is defensive. If it finds nothing it says so; it never fails the tool.
// Session tokens are never written.

import fs from 'node:fs';
import path from 'node:path';

const SS = 'mcp__servicestudio__';
const HEADER = '# Publish history\n\n| When | Module | Status | Story | Notes |\n|---|---|---|---|---|\n';
const SUMMARY_PLACEHOLDER = 'SUMMARY: to be written by Claude after the publish.';

let REPO = '';
let STATE = '';

try {
  const event = JSON.parse(await readStdin() || '{}');
  REPO = process.env.CLAUDE_PROJECT_DIR || event.cwd || process.cwd();
  STATE = path.join(REPO, '.claude', 'state');
  if (event.tool_input) delete event.tool_input.sessionToken;
  if (process.env.OS_REPO_HOOK_DEBUG) write(path.join(STATE, 'last-log-publish.json'), JSON.stringify({ ...event, tool_input: event.tool_input }, null, 2));
  const out = handle(event);
  if (out) process.stdout.write(JSON.stringify({ hookSpecificOutput: { hookEventName: 'PostToolUse', additionalContext: out } }));
} catch (err) {
  try { if (STATE) write(path.join(STATE, 'error-log-publish.log'), `${new Date().toISOString()} ${err?.stack || err}\n`, true); } catch {}
}
process.exit(0);

function handle(event) {
  if (!fs.existsSync(path.join(REPO, 'changes')) && !fs.existsSync(path.join(REPO, 'PUBLISHES.md'))) {
    return `outsystems-mcp-publish: ${REPO} is not set up with outsystems-mcp-repo-setup (no changes/ or PUBLISHES.md), so this publish was not logged.`;
  }

  // The skill can be invoked more than once per session; log each publish call only once.
  const seenFile = path.join(STATE, 'logged-publishes');
  const seen = read(seenFile).split('\n');
  if (event.tool_use_id && seen.includes(event.tool_use_id)) return '';
  if (event.tool_use_id) write(seenFile, event.tool_use_id + '\n', true);

  const module = input(event.tool_input).eSpaceName || 'UnknownModule';
  const result = parseResult(event.tool_response) ?? {};
  const published = result.published === true;
  const status = published ? 'published' : (result.status || 'unknown');
  const story = read(path.join(STATE, 'current-story')).trim();
  const now = new Date();

  let changes = [];
  let note = '';
  if (published) {
    try {
      changes = collectChanges(event.transcript_path, module, event.tool_use_id);
      changes.forEach(c => writeChange(c, module, story, now));
    } catch (err) {
      note = `transcript not readable (${err.message})`;
    }
  }

  const notes = [String(result.reason || ''), published ? `${changes.length} change(s) logged` : '', note]
    .filter(Boolean).join('; ').replace(/\|/g, '/').replace(/\r?\n/g, ' ');
  const file = path.join(REPO, 'PUBLISHES.md');
  if (!fs.existsSync(file)) write(file, HEADER);
  const { date, time } = local(now);
  if (status !== 'blocked') write(file, `| ${date} ${time.slice(0, 2)}:${time.slice(2, 4)} | ${module} | ${status} | ${story || '-'} | ${notes} |\n`, true);

  if (!published) {
    return `${module} was NOT published (status: ${status}).${status === 'blocked' ? ' Not logged in PUBLISHES.md (blocked calls are not recorded); if the user publishes by hand, add the row yourself.' : ' Logged in PUBLISHES.md.'} No lambdas were written to changes/; they will be logged by the next successful publish in this session.`;
  }
  return [
    `${module} was published and logged in PUBLISHES.md. ${changes.length} save(s) logged in changes/${safeName(story || local(now).date)}-${safeName(module)}.md. Replace each "${SUMMARY_PLACEHOLDER}" line there with a short plain-words summary of what that save changed (no code).`,
    note ? `Warning: ${note}. Tell the user the change log for this publish is incomplete.` : '',
    !note && changes.length === 0 ? 'No lambdas were found in this session for the module (changes made in another session, by a subagent, or by hand in Service Studio are not logged). Mention this to the user.' : '',
    'Next: refresh the snapshots following references/snapshot.md of the outsystems-mcp-publish skill, then update the story file.',
  ].filter(Boolean).join(' ');
}

// Walks the transcript in order. Changes since the last successful publish/reset of the module are kept.
function collectChanges(transcriptPath, module, currentPublishId) {
  if (!transcriptPath) throw new Error('no transcript_path in hook input');
  const uses = new Map();
  let pending = [];
  for (const line of fs.readFileSync(transcriptPath, 'utf8').split('\n')) {
    if (!line.trim()) continue;
    let entry;
    try { entry = JSON.parse(line); } catch { continue; }
    if (entry.isSidechain) continue;
    const blocks = Array.isArray(entry.message?.content) ? entry.message.content : [];
    for (const b of blocks) {
      if (b?.type === 'tool_use' && String(b.name).startsWith(SS)) {
        if (b.id === currentPublishId) return pending;
        uses.set(b.id, { name: b.name.slice(SS.length), input: input(b.input), when: entry.timestamp });
        continue;
      }
      if (b?.type !== 'tool_result' || b.is_error) continue;
      const use = uses.get(b.tool_use_id);
      if (!use || use.input.eSpaceName !== module) continue;
      const res = parseResult(entry.toolUseResult ?? b.content) ?? {};
      if (use.name === 'applyModelApiCode' && use.input.code && res.mutatedOmlPath) pending.push({ ...use, result: res });
      else if (use.name === 'omlReset') pending = [];
      else if (use.name === 'omlPublish' && res.published === true) pending = [];
    }
  }
  return pending;
}

// One Markdown file per story and module (changes/<story>-<Module>.md, or changes/<date>-<Module>.md
// without a story); each save is appended as a "## Save N" section: when, outcome, what the lambda touched,
// and a SUMMARY placeholder that Claude replaces with a plain-words summary. No code is written.
function writeChange(change, module, story, publishedAt) {
  const at = change.when ? new Date(change.when) : publishedAt;
  const errors = (change.result.validationMessages || []).filter(m => String(m.type).toLowerCase() === 'error');
  const label = story || local(at).date;
  const file = path.join(REPO, 'changes', `${safeName(label)}-${safeName(module)}.md`);
  const existing = read(file);
  const n = (existing.match(/^## Save \d+/gm) || []).length + 1;
  const touched = touchedBy(String(change.input.code));
  const section = [
    ...(existing ? [] : [`# ${label} · ${module} changes`, '', `What each \`applyModelApiCode\` save for ${label} changed in ${module}, oldest first. Logged when the module was published. The resulting module is in \`snapshots/${module}/\`.`, '']),
    `## Save ${n} · ${utc(at)}`,
    '',
    `- **Published:** ${utc(publishedAt)}`,
    `- **Outcome:** ${errors.length ? `saved with ${errors.length} validation error(s)` : 'saved'}`,
    ...errors.map(e => `- **Error:** ${e.ownerPath || ''} - ${e.message || ''}`.replace(/\r?\n/g, ' ')),
    `- **Touched:** ${touched || 'existing elements only (see the snapshot diff)'}`,
    '',
    SUMMARY_PLACEHOLDER,
    '',
  ];
  write(file, section.join('\n'), true);
}

// Elements a lambda creates, renames or deletes, read from its Create…("Name") calls and its edits; best effort.
function touchedBy(code) {
  const kinds = 'ServerAction|ClientAction|ServiceAction|Screen|Block|DataAction|ScreenAction|ServerEntity|StaticEntity|Structure|Role|UserException|SiteProperty|Timer|Folder|Theme|UIFlow|MobileFlow';
  const created = [...new Set([...code.matchAll(new RegExp(`\\.Create(${kinds})\\(\\s*(?:[^,()"]*,\\s*)?"([^"]+)"`, 'g'))].map(m => `${m[1].replace(/([a-z])([A-Z])/g, '$1 $2').toLowerCase()} \`${m[2]}\``))];
  const edits = [];
  if (/\.Delete\(\)/.test(code)) edits.push('deletes elements');
  if (/\.Name\s*=/.test(code)) edits.push('renames elements');
  if (/\.Label\s*=/.test(code)) edits.push('sets node labels');
  if (/HorizontalPosition|VerticalPosition|ConnectedBelow|ToTheRightOf/.test(code) && !created.length) edits.push('moves nodes');
  return [created.length ? 'creates ' + created.slice(0, 20).join(', ') + (created.length > 20 ? `, and ${created.length - 20} more` : '') : '', ...edits].filter(Boolean).join('; ');
}

function utc(d) { return d.toISOString().slice(0, 19).replace('T', ' ') + ' UTC'; }

// ---------- helpers ----------
function input(toolInput) { return toolInput?.input ?? toolInput ?? {}; }

// MCP results arrive as a string, an array of content blocks, { content: [...] } or { structuredContent }.
function parseResult(resp) {
  if (resp == null) return null;
  if (typeof resp === 'object' && !Array.isArray(resp)) {
    if (resp.structuredContent) return resp.structuredContent;
    if (Array.isArray(resp.content)) return parseResult(resp.content);
    return resp;
  }
  const text = Array.isArray(resp) ? resp.map(b => (typeof b === 'string' ? b : b?.text ?? '')).join('') : String(resp);
  try { return JSON.parse(text); } catch { return null; }
}

function readStdin() {
  return new Promise(resolve => {
    let data = '';
    process.stdin.setEncoding('utf8');
    process.stdin.on('data', c => (data += c));
    process.stdin.on('end', () => resolve(data));
  });
}

function read(file) { try { return fs.readFileSync(file, 'utf8'); } catch { return ''; } }

function write(file, content, append = false) {
  fs.mkdirSync(path.dirname(file), { recursive: true });
  (append ? fs.appendFileSync : fs.writeFileSync)(file, content);
}

function local(d) {
  const p = n => String(n).padStart(2, '0');
  return { date: `${d.getFullYear()}-${p(d.getMonth() + 1)}-${p(d.getDate())}`, time: `${p(d.getHours())}${p(d.getMinutes())}${p(d.getSeconds())}` };
}

function safeName(s) { return String(s).replace(/[^\w.-]+/g, '_'); }
