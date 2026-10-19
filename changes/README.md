# Change log

What each `applyModelApiCode` save changed, recorded **once the module is published**. There is one file per story and module, `<story>-<Module>.md` (or `<yyyy-mm-dd>-<Module>.md` for work without a story), with one section per save, oldest first:

- **When** it was saved (the section heading) and **when it was published**.
- The **outcome**: `saved`, or `saved with N validation error(s)` plus the errors. A later save in the same file usually fixed those.
- **What it touched**: the elements the lambda creates, renames or deletes, listed by the `outsystems-mcp-publish` hook.
- A short **summary** in plain words, written by Claude after the publish.

There is no code here. The exact result of every publish is in `snapshots/<Module>/` (`git log -p` shows what changed), and the lambdas logged up to TT8 are in git history (commit 21f9b01).

Notes:

- The hook reads the lambdas from the Claude Code session that publishes: every `applyModelApiCode` call for the module since that session's previous publish or `omlReset`. Changes made in an earlier session, or by a subagent, are not logged by the hook. They still show up in the next snapshot diff.
- When the MCP publish is blocked and the user publishes by hand, Claude adds the sections in the same format.
- Compile errors and runs that saved nothing are not recorded.
- Changes made by hand in Service Studio don't appear here. They show up in the next snapshot diff.
