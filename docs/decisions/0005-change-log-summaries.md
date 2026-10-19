# 0005. Change log as plain-words summaries; one change file per story and module

- **Date:** 2026-10-19
- **Status:** Accepted
- **Story:** none (repository refactor)

## Context

`changes/` held every published `applyModelApiCode` lambda as its own `.cs` file under dated folders. Nobody read them back: the story's implementation notes and the snapshot diffs answered every question about what changed, and finding a story's saves meant scanning file names. Blocked MCP publishes added a row to `PUBLISHES.md` on top of the manual publish row that followed. The investigation report lived in the repository although its approved stories already became story files.

A first version of this refactor turned everything into Markdown, code included. It was reverted for the snapshots and the checks: their code is used (the checks have to run, and snapshot diffs and recreating elements need the exact code), so they stay `.cs`.

## Decision

- `changes/<story>-<Module>.md`: one file per story and module, one `## Save N` section per save with when it was saved and published, the outcome, the elements it touched and a short summary in plain words. **No code.** The `outsystems-mcp-publish` hook writes the section and leaves a `SUMMARY:` line that Claude replaces.
- `snapshots/<Module>/` stays Model API code (`.cs`, name lists `.json`), as the read tools return it.
- `queries/` became `checks/`; the checks stay `.cs` lambdas with `//` header comments.
- `PUBLISHES.md` has no rows for blocked calls.
- `investigations/` is gone. Investigation reports are working files kept outside the repository (`~/.claude/outsystems-investigations/`); stories cite the finding IDs.
- `README.md` keeps prerequisites and getting started and points to `CLAUDE.md` for the repository map.

## Consequences

- A story's changes are one short, readable file away from the story (84 KB in all instead of 532 KB).
- The exact lambdas are no longer kept for new saves; they are in the session transcript only. The resulting model is still exact in `snapshots/`. The lambdas logged up to TT8 are in git history (commit 21f9b01, `changes/`).
- The `outsystems-mcp-publish` hook writes summary sections and skips blocked calls. `extract-snapshot.mjs` now reads a large read from the file Claude Code saved it to, so large screens and blocks need no manual rebuild. The `implementation`, `code-review`, `investigate` and `repo-setup` skills point to `checks/`.
- The 2026-10-16 gap analysis is in git history (commit 5bb59c3).
