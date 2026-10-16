# ClaudeSheet

TODO: describe the project

Platform: OutSystems 11. The app is built through the **Service Studio MCP** (`mcp__servicestudio__*`, at `http://127.0.0.1:41820/mcp`). The modules themselves live in the OutSystems platform. This repository is where the work is planned, recorded and reviewed.

## Environments

| Environment | URL |
|---|---|
| Development | TODO |

## Modules

| Module | App | Type | Layer | Version | Holds |
|---|---|---|---|---|---|
| ClaudeSheet_CS | ClaudeSheet | Reactive | Core | 0 | Backend: database, timers, processes, server actions, structures, roles, user exceptions, site properties |
| ClaudeSheet | ClaudeSheet | Reactive | End-user | 0 | Front end: screens, blocks, data actions, screen/client actions, theme |

ClaudeSheet references ClaudeSheet_CS, never the other way round. Put every new element in its module from the start (`docs/conventions.md` › Module placement). TT4 moved the backend to ClaudeSheet_CS (2026-10-15).

Architecture, references, layer rules and what goes where: [docs/architecture.md](docs/architecture.md), [ADR 0004](docs/decisions/0004-split-end-user-and-core.md).

## Repository map

| Path | What it holds | Who writes it |
|---|---|---|
| `stories/` | One file per user story or technical task: what to build, acceptance criteria, status | Claude + team |
| `changes/<date>/` | Every `applyModelApiCode` lambda that saved an OML and was then published, with module, story and outcome | `outsystems-mcp-publish` skill |
| `snapshots/<Module>/` | Model API code of each module as of the last publish | `outsystems-mcp-publish` skill |
| `reviews/<Module>/` | Review rounds from `outsystems-mcp-code-review` | Claude |
| `PUBLISHES.md` | Every `omlPublish` call and its result | `outsystems-mcp-publish` skill |
| `docs/` | Architecture, conventions, decisions (ADRs) | Claude + team |
| `queries/` | Reusable ModelQL queries for `runQuery` | Claude + team |
| `.claude/` | Settings and project skills | Setup |

## Workflow for every story or task

1. **Story file.** Find or create `stories/<ID>-<slug>.md` from `stories/TEMPLATE.md`. Write the ID (e.g. `US-012`) to `.claude/state/current-story` so changes and publishes are tagged with it. Clear that file when the story is done.
2. **Context.** Read `docs/conventions.md` and the relevant files in `snapshots/<Module>/` before touching the module. Snapshots can be out of date: confirm what you're about to change with the live read tools.
3. **Implement** with `applyModelApiCode`, following the `servicestudio-mcp-oml` skill. Don't write to `changes/` yourself: the lambdas are logged when the module is published (step 6).
4. **Merge** with `omlMerge` once the last mutation is clean.
5. **Review** with the `outsystems-mcp-code-review` skill. Save review logs to `reviews/<Module>/<story-id>.md` in this repo, not in `~/.claude/outsystems-reviews/`.
6. **Publish** with the `outsystems-mcp-publish` skill, and only after the review approves everything. `omlPublish` always asks for confirmation. After a successful publish, the skill's hook records the publish in `PUBLISHES.md` and writes this session's lambdas for the module to `changes/<date>/`.
7. **Snapshot.** After a successful publish, the `outsystems-mcp-publish` skill refreshes the published module's snapshots. To refresh them without publishing, ask for it ("refresh the snapshot"); the same skill handles it.
8. **Close out.** Update the story's status and implementation notes. If a design choice was made, add a decision in `docs/decisions/`. Offer to commit; don't commit without asking.

## Multi-module stories

When a story's plan has steps in more than one module (`## Implementation plan` grouped per module):

1. **Producer first.** ClaudeSheet_CS before ClaudeSheet. ClaudeSheet can only use what ClaudeSheet_CS has **published**.
2. **One module at a time, in this session.** No subagents. For each module, in order: implement and check it with `applyModelApiCode` (read calls one at a time, its `eSpaceName` on every call), run the conventions gate, then the plan check form + `omlMerge`, the review (`reviews/<Module>/<story>.md`), the publish (the hook logs the lambdas to `changes/`) and the snapshots. Finish one module's chain before starting the next.
3. **Hand-off between modules** (the Model API can't add dependencies, so the user does that one step):
   1. Publish ClaudeSheet_CS.
   2. **Dependency checklist.** From the consumer's plan steps, list every producer element the consumer will use, grouped by type (Entities, Static Entities, Structures, Server Actions, Roles, Exceptions). Show it as a blocking question: *"In **ClaudeSheet** → Manage Dependencies → **ClaudeSheet_CS**, tick: …"* with the answers **Done** / **Not yet**. Also write the list into the story's plan (`Dependencies to add`).
   3. On **Done**: run `omlRefreshReferences` on ClaudeSheet (before the ClaudeSheet chain starts: it drops the session pointer), then a throw-probe on ClaudeSheet that looks up each listed element in `eSpace.References.Named("ClaudeSheet_CS")`. Any missing → show the question again with only the missing ones. All found → start the ClaudeSheet chain.
   4. On **Not yet**: wait; don't start the consumer.
   5. Known behaviour (TT4): a reference lists the ticked elements **plus what they depend on**, not every public element. A producer element whose name clashes with an element still in the consumer comes in with a **"2" suffix** (e.g. `DaySheet_Save2`), and it keeps the suffix when the local copy is deleted in the same chain. Once the local copy is gone, drop the suffix with the Model API: renaming the reference element (`eSpace.References.Named("<Producer>").ServerActions/Structures/Entities` → `.Name = "<name>"`) renames the consumer's local name, and Execute nodes and expressions follow.
4. Each module has its own session pointer: never interleave `applyModelApiCode` calls on two modules in one chain.

## Rules

- Never write session tokens, passwords or LifeTime credentials to any file in this repo.
- Call `createSessionToken` once per session and reuse the token.
- Make Service Studio read calls one at a time. Parallel reads fail with "OML not found".
- Every module a story touches must be open in Service Studio to be read or changed; `listApps` shows the open modules and their references. Pass the right `eSpaceName` on every call: each module has its own session pointer.
- `omlPublish` publishes the live module in Service Studio, not an unmerged chain, so merge first.
- `docs/conventions.md` is the naming and design standard. If the user asks for something that contradicts it, point that out before doing it.
