# 0005. Markdown-only repository, one change file per story and module

- **Date:** 2026-10-19
- **Status:** Accepted
- **Story:** none (repository refactor)

## Context

The record of the modules (ADR 0001) was a mix of `.cs` files (change lambdas, snapshots, checks), `.json` name lists and Markdown. The `.cs` files are never compiled; they are read by people and sent to `applyModelApiCode` by Claude. Change files were one per save under dated folders, so finding a story's code meant scanning file names. Snapshots repeated every default property, which doubled their size and diffs. Blocked MCP publishes added a row to `PUBLISHES.md` on top of the manual publish row that followed. The investigation report lived in the repository although its approved stories already became story files.

## Decision

- Every file in the repository is Markdown; code sits in a `csharp` block and name lists in a `json` block.
- `changes/<story>-<Module>.md`: one file per story and module, one `## Save N` section per lambda, oldest first.
- `snapshots/<Module>/<element>.md`: a short header plus the read result. Lines that only set a property to its default value are left out.
- `queries/` became `checks/`: each check says what it does, how to run it and what passing means, then gives the code.
- `PUBLISHES.md` has no rows for blocked calls.
- `investigations/` is gone. Investigation reports are working files kept outside the repository (`~/.claude/outsystems-investigations/`); stories cite the finding IDs.
- `README.md` keeps prerequisites and getting started and points to `CLAUDE.md` for the repository map.

## Consequences

- Everything renders on GitHub, and a story's code is one file away from the story.
- Snapshots are about 40% smaller (1.6 MB to 0.9 MB) and diffs show only real changes. A snapshot no longer lists default values explicitly; a missing property means its default.
- The `outsystems-mcp-publish` scripts were changed to match: the hook appends to `changes/<story>-<Module>.md` and skips blocked calls; `extract-snapshot.mjs` writes Markdown (`snapshot-format.mjs`) and reads large reads from the file Claude Code saved them to, so they no longer need a manual rebuild. The `implementation`, `code-review`, `investigate` and `repo-setup` skills point to `checks/` and the new formats.
- The 2026-10-16 gap analysis is in git history (commit 5bb59c3).
