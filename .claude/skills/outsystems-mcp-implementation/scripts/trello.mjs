// Trello helper for outsystems-mcp-implementation.
//
//   node trello.mjs status
//   node trello.mjs setup <board URL or id> [--backlog "Backlog"] [--in-progress "In Progress"] [--done "Done"]
//   node trello.mjs move <story ID> [--dry-run]
//
// The board connection lives in .claude/trello.json (board id and list ids, no secrets) in the
// current project. Credentials come only from the TRELLO_KEY and TRELLO_TOKEN environment
// variables and are sent in the Authorization header, never in a URL, never printed or written.
import { existsSync, readFileSync, writeFileSync, mkdirSync } from 'node:fs';
import { join, dirname } from 'node:path';

const CONFIG = join(process.cwd(), '.claude', 'trello.json');
const API = 'https://api.trello.com/1';

const [cmd, arg, ...rest] = process.argv.slice(2);
const opt = (name, def) => { const i = rest.indexOf(name); return i >= 0 ? rest[i + 1] : def; };
const flag = name => rest.includes(name);
// process.exit() right after fetch() crashes Node on Windows (libuv assertion), so end by throwing
// and let the top-level handler set process.exitCode.
class Exit extends Error { constructor(code) { super(); this.code = code; } }
const fail = (msg, code = 1) => { console.log(msg); throw new Exit(code); };
const done = msg => { console.log(msg); throw new Exit(0); };

function credentials() {
  const key = process.env.TRELLO_KEY, token = process.env.TRELLO_TOKEN;
  return key && token ? { Authorization: `OAuth oauth_consumer_key="${key}", oauth_token="${token}"` } : null;
}

async function api(path, init = {}) {
  const auth = credentials();
  if (!auth) fail('NO_CREDENTIALS: set the TRELLO_KEY and TRELLO_TOKEN environment variables.', 3);
  const r = await fetch(API + path, { ...init, headers: { ...auth, 'Content-Type': 'application/json', ...(init.headers || {}) } });
  if (!r.ok) fail(`TRELLO_ERROR: ${init.method || 'GET'} ${path.split('?')[0]} returned ${r.status} ${await r.text()}`);
  return r.json();
}

const loadConfig = () => existsSync(CONFIG) ? JSON.parse(readFileSync(CONFIG, 'utf8')) : null;

try {
if (cmd === 'status') {
  const cfg = loadConfig();
  if (!cfg) fail(`NOT_CONFIGURED: no ${CONFIG}.`, 2);
  if (!credentials()) fail('NO_CREDENTIALS: .claude/trello.json exists but TRELLO_KEY / TRELLO_TOKEN are not set.', 3);
  const board = await api(`/boards/${cfg.boardId}?fields=name,url`);
  console.log(`CONNECTED: board "${board.name}" ${board.url}; lists ${Object.entries(cfg.lists).map(([k, v]) => `${k}=${v.name}`).join(', ')}`);
} else if (cmd === 'setup') {
  if (!arg) fail('Usage: node trello.mjs setup <board URL or id>');
  const shortId = (arg.match(/trello\.com\/b\/([^/]+)/) || [, arg])[1];
  const board = await api(`/boards/${shortId}?fields=name,url`);
  const lists = await api(`/boards/${board.id}/lists?fields=name`);
  const wanted = { backlog: opt('--backlog', 'Backlog'), inProgress: opt('--in-progress', 'In Progress'), done: opt('--done', 'Done') };
  const found = {}, missing = [];
  for (const [k, name] of Object.entries(wanted)) {
    const l = lists.find(x => x.name.trim().toLowerCase() === name.toLowerCase());
    if (l) found[k] = { id: l.id, name: l.name }; else missing.push(`${k} ("${name}")`);
  }
  if (missing.length) fail(`LISTS_NOT_FOUND: ${missing.join(', ')}. Lists on "${board.name}": ${lists.map(l => `"${l.name}"`).join(', ')}. Re-run with --backlog / --in-progress / --done.`, 4);
  mkdirSync(dirname(CONFIG), { recursive: true });
  writeFileSync(CONFIG, JSON.stringify({ board: board.url, boardId: board.id, boardName: board.name, lists: found }, null, 2) + '\n');
  console.log(`CONFIGURED: ${CONFIG} -> board "${board.name}", lists ${Object.values(found).map(l => l.name).join(', ')}`);
} else if (cmd === 'move') {
  if (!arg) fail('Usage: node trello.mjs move <story ID> [--dry-run]');
  const cfg = loadConfig();
  if (!cfg) fail(`NOT_CONFIGURED: no ${CONFIG}.`, 2);
  const id = arg.replace(/\s+/g, '').toLowerCase();
  const cards = await api(`/boards/${cfg.boardId}/cards?fields=name,idList,shortUrl`);
  // Card names start with the story ID: "US10 · [Block] …"
  const matches = cards.filter(c => { const n = c.name.replace(/\s+/g, ' ').toLowerCase(); return n.startsWith(id) && !/[a-z0-9]/.test(n.charAt(id.length)); });
  if (matches.length === 0) fail(`CARD_NOT_FOUND: no card on "${cfg.boardName}" whose name starts with ${arg}.`, 5);
  if (matches.length > 1) fail(`AMBIGUOUS: ${matches.map(c => `"${c.name}" ${c.shortUrl}`).join('; ')}`, 6);
  const card = matches[0];
  const listName = Object.values(cfg.lists).find(l => l.id === card.idList)?.name || 'another list';
  if (card.idList === cfg.lists.inProgress.id) { done(`ALREADY: "${card.name}" is in ${listName} ${card.shortUrl}`); }
  if (card.idList !== cfg.lists.backlog.id) fail(`NOT_IN_BACKLOG: "${card.name}" is in ${listName}, not moved ${card.shortUrl}`, 7);
  if (flag('--dry-run')) { done(`DRY_RUN: would move "${card.name}" from ${cfg.lists.backlog.name} to ${cfg.lists.inProgress.name} ${card.shortUrl}`); }
  await api(`/cards/${card.id}`, { method: 'PUT', body: JSON.stringify({ idList: cfg.lists.inProgress.id, pos: 'top' }) });
  console.log(`MOVED: "${card.name}" from ${cfg.lists.backlog.name} to ${cfg.lists.inProgress.name} ${card.shortUrl}`);
} else {
  fail('Usage: node trello.mjs status | setup <board URL or id> | move <story ID> [--dry-run]');
}
} catch (e) {
  if (e instanceof Exit) process.exitCode = e.code; else throw e;
}
