# Change log

Every `applyModelApiCode` call that saved an OML is recorded here **once the module is published**, by the `outsystems-mcp-publish` skill. There is one file per story and module, `<story>-<Module>.md` (or `<yyyy-mm-dd>-<Module>.md` for work without a story). Each save is a section, oldest first, with the exact lambda that was run:

````markdown
## Save 1 · 2026-10-19 16:22:58 UTC

- **Published:** 2026-10-19 16:28:23 UTC
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => { … }
```
````

Notes:

- A section here means the change was **published**. `PUBLISHES.md` has the publish, `reviews/` the approval.
- The lambdas are read from the Claude Code session that publishes: every `applyModelApiCode` call for the module since that session's previous publish or `omlReset`. Changes made in an earlier session, or by a subagent, are not logged. They still show up in the next snapshot diff.
- When the MCP publish is blocked and the user publishes by hand, Claude adds the sections from the session transcript in the same format.
- Compile errors and runs that saved nothing are not recorded.
- `Outcome: saved with N validation error(s)` usually means a later save in the same file fixed it.
- Changes made by hand in Service Studio don't appear here. They show up in the next snapshot diff.
