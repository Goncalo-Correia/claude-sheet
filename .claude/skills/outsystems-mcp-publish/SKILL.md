---
name: outsystems-mcp-publish
description: Closes a story in the project repository once outsystems-mcp-code-review has approved it - the module is already published to Development by then. It checks the review approved everything and that the live module has nothing unpublished (it publishes first if it has), makes sure every publish of the story is in PUBLISHES.md and every applyModelApiCode save has a plain-words section in changes/<story>-<Module>.md (the project's omlPublish hook writes them on each publish), refreshes snapshots/<Module>/ from the live module, marks the story Done and offers to commit. Use when the review approves everything - "publish", "update the repository", "record the story", "close the story", "publish skill", "ship it" - and in repositories set up with outsystems-mcp-repo-setup. Also use it on its own to refresh or take module snapshots - "refresh the snapshot", "update snapshots", "snapshot the module", "full snapshot" - or when a snapshot is missing or old.
---

# Update the repository after an approved story

Files in this skill:

- `scripts/log-publish.mjs`: the publish hook described below.
- `references/snapshot.md`: how to refresh `snapshots/<Module>/`, after a story or on its own.
- `scripts/extract-snapshot.mjs`: copies the read-tool results from the session transcript into `snapshots/`, used by that procedure.

## Where this skill sits

```
outsystems-mcp-implementation   define → implement → plan check + merge → publish to Development
outsystems-mcp-code-review      the user tests in the browser and checks Service Studio; fixes are republished
outsystems-mcp-publish          (this skill) review approved → record everything in the repository → Done
```

In OutSystems an app can only be tested in the browser once it is published, so the module is published **before** the review, by `outsystems-mcp-implementation`, and again after each review fix. By the time this skill runs, the module is normally already published with the approved version. This skill's job is the repository.

## The publish hook

The project's `.claude/settings.json` registers `scripts/log-publish.mjs` as a `PostToolUse` hook on `mcp__servicestudio__omlPublish`. It runs on **every** MCP publish, whichever skill makes it. After each publish it:

- appends a row to `PUBLISHES.md`: when, module, status, story, notes. A `blocked` call publishes nothing and adds no row;
- **only if the publish succeeded**, reads that session's transcript and, for every successful `applyModelApiCode` call for that module since its previous successful publish or `omlReset`, appends a section to `changes/<story>-<Module>.md` (`changes/<yyyy-mm-dd>-<Module>.md` without a story): `## Save N · <when it was saved>`, when it was published, the outcome, the elements the lambda touched, and a `SUMMARY:` placeholder line. **The code is not written.** Session tokens are never written;
- tells you in its output how many saves it logged.

Whoever publishes (implementation, review fix round, or this skill) replaces each new `SUMMARY:` line right after the publish with one or two plain-words sentences on what that save changed (element names in backticks, no code). A failed or blocked publish logs nothing to `changes/`; those saves are picked up by the next successful publish in the same session.

**Manual publish.** When the MCP publish is blocked and the user publishes from Service Studio (1-Click Publish), the hook doesn't fire. Add the `PUBLISHES.md` row yourself (status `success (manual)`) and the `changes/` sections in the hook's format, with summaries.

## 1. Check the story is ready

Stop and tell the user if any of these fail.

1. **Repository.** `PUBLISHES.md` and `changes/` exist. If they don't, offer `outsystems-mcp-repo-setup` first.
2. **Story.** `.claude/state/current-story` holds the story ID (or the user names it). Open `stories/<ID>-*.md`.
3. **Review approved.** `reviews/<Module>/<story-id>.md` exists for every module the story touched, and its last round ends with `Review: approved` (see `outsystems-mcp-code-review`). If it doesn't, say so and ask whether to continue anyway.
4. **Session token.** Reuse the one from this conversation. Call `createSessionToken` only if there is none.
5. **Module.** `listApps` shows each module open (`isOpen: true`).

## 2. Make sure the approved version is published

For each module of the story, producer first:

- **Unmerged chain** (`applyModelApiCode` calls since the last `omlMerge` in this session): stop. Something changed after the review; it has to go through the review before it's recorded.
- **Merged but not published** (an `omlMerge` in this session after the module's last successful `omlPublish`, or the user says they changed it in Service Studio): check `getValidationMessages` (errors block), then call `mcp__servicestudio__omlPublish` with `{ "eSpaceName": "<Module>" }` and write the `SUMMARY:` lines it leaves. If the change wasn't reviewed, say so before publishing.
- **Otherwise** the module is already published with the approved version: don't publish again.

`omlPublish` asks for confirmation (project settings), and the user may have to grant the write permission in the Service Studio MCP dialog. If the call returns `status: "blocked"`, say so and stop. Make one publish call at a time.

## 3. Check the record

1. **`PUBLISHES.md`** has a row for every publish of this story (implementation and each review fix round), tagged with the story. Publishes made in another session were logged by that session's hook; manual publishes need the row described above.
2. **`changes/<story>-<Module>.md`** has a section for every save, and **no `SUMMARY:` placeholder is left**. Write any that are, from the touched elements, the story's implementation notes and the review log.
3. If the hook logged 0 saves although changes were made (another session's transcript is gone, a subagent, or hand edits in Service Studio), say so in the story trail. The snapshot diff still shows them.

## 4. Refresh the snapshots

Follow [references/snapshot.md](references/snapshot.md) for each module the story touched: targeted mode for the story's elements, otherwise light.

## 5. Close the story

1. Update the story file: Status `Done`, the publish dates, and the Trail (`changes/`, reviews, `PUBLISHES.md`). Tick the acceptance criteria the review approved.
2. Clear `.claude/state/current-story`.
3. If a design choice was made, offer to add a decision in `docs/decisions/`.
4. Offer to commit `PUBLISHES.md`, `changes/`, `snapshots/`, `reviews/` and the story together. Don't commit without asking.

## Refreshing snapshots on their own

When the user only wants a snapshot, for example after hand edits in Service Studio, a first snapshot, or a full snapshot, follow [references/snapshot.md](references/snapshot.md) directly. Don't publish and don't close a story.

## Troubleshooting

- **The hook writes nothing.** Check that `.claude/settings.json` still has the `PostToolUse` hook on `mcp__servicestudio__omlPublish` and that `node --version` works. Run Claude Code with `OS_REPO_HOOK_DEBUG=1` set: the hook then saves its input, without the session token, to `.claude/state/last-log-publish.json`, and errors go to `.claude/state/error-log-publish.log`.
- **The hook command can't find the script.** It uses `$CLAUDE_PROJECT_DIR/.claude/skills/outsystems-mcp-publish/scripts/log-publish.mjs`. If the skill lives somewhere else, change the path in `.claude/settings.json`.
- **Saves are missing.** The transcript format is internal to Claude Code and may change between versions. Check `.claude/state/error-log-publish.log`, and compare a line of `~/.claude/projects/<project>/<session>.jsonl` with `collectChanges` in `scripts/log-publish.mjs`.
- **Testing the hook.** Pipe a fake `PostToolUse` event into the script with `CLAUDE_PROJECT_DIR` pointing to a scratch copy of the repo, never the real one. The event needs `tool_input.input.eSpaceName`, `tool_response` text `{"published":true}`, a `tool_use_id`, and a `transcript_path` to a JSONL file holding an `applyModelApiCode` `tool_use` block and its `tool_result`.
