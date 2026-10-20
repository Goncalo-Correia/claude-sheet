# {{PROJECT_NAME}}

{{DESCRIPTION}}

Platform: {{PLATFORM}}. The app is built through the **Service Studio MCP** (`mcp__servicestudio__*`, at `{{MCP_URL}}`). The modules themselves live in the OutSystems platform. This repository is where the work is planned, recorded and reviewed.

## Environments

{{ENVIRONMENTS_TABLE}}

## Modules

{{MODULES_TABLE}}

Architecture, references and layer rules: [docs/architecture.md](docs/architecture.md).

## Repository map

| Path | What it holds | Who writes it |
|---|---|---|
| `stories/` | One file per user story or technical task: what to build, acceptance criteria, status | Claude + team |
| `changes/<story>-<Module>.md` | What each published `applyModelApiCode` save changed, in plain words: one section per save with its date, outcome, touched elements and summary. No code | `outsystems-mcp-publish` hook |
| `snapshots/<Module>/` | Model API code of each module as of the last closed story | `outsystems-mcp-publish` skill |
| `reviews/<Module>/` | Review rounds from `outsystems-mcp-code-review` | Claude |
| `PUBLISHES.md` | Every publish and its result (blocked MCP calls are not recorded) | `outsystems-mcp-publish` hook |
| `docs/` | Architecture, conventions, decisions (ADRs) | Claude + team |
| `checks/` | Model API checks and fixes for the conventions as `.cs` lambdas; the header comments say how to run each one | Claude + team |
| `.claude/` | Settings (permissions, the publish hook), project skills, and `state/current-story` | Setup |

## Workflow for every story or task

1. **Story file.** Find or create `stories/<ID>-<slug>.md` from `stories/TEMPLATE.md`. Write the ID (e.g. `US-012`) to `.claude/state/current-story` so changes and publishes are tagged with it. Clear that file when the story is done.
2. **Context.** Read `docs/conventions.md` and the relevant files in `snapshots/<Module>/` before touching the module. Snapshots can be out of date: confirm what you're about to change with the live read tools.
3. **Implement, plan check, publish** with the `outsystems-mcp-implementation` skill: it asks for the description, the implementation plan and the acceptance criteria, implements with `applyModelApiCode` (following `servicestudio-mcp-oml`), shows the plan check form together with `omlMerge`, and once every plan step is Yes **publishes to Development**. An OutSystems app can only be tested in the browser once it is published, so publishing comes before the review. Story status: `In review`.
4. **Review** with the `outsystems-mcp-code-review` skill: the user tests the acceptance criteria in the browser on Development and checks the changes in Service Studio. Each fix is merged and republished so it can be retested. Save review logs to `reviews/<Module>/<story-id>.md` in this repo, not in `~/.claude/outsystems-reviews/`. The approving round ends with `Review: approved`.
5. **Update the repository** with the `outsystems-mcp-publish` skill once the review approves everything: it checks nothing is left unpublished, completes the change log, refreshes `snapshots/<Module>/`, marks the story `Done`, clears `.claude/state/current-story` and offers to commit. To refresh snapshots on their own, ask for it ("refresh the snapshot"); the same skill handles it.

**Change log.** `.claude/settings.json` runs the publish hook (`outsystems-mcp-publish/scripts/log-publish.mjs`) after every MCP `omlPublish`, whichever skill makes it: it adds a `PUBLISHES.md` row and a section per save to `changes/<story>-<Module>.md`. Don't write to `changes/` yourself; replace each `SUMMARY:` line it leaves with a short plain-words summary of that save (no code), right after the publish. If the MCP publish is blocked and the user publishes by hand, add the `PUBLISHES.md` row and the `changes/` sections (with summaries) yourself, in the same format. `omlPublish` always asks for confirmation.

## Rules

- Never write session tokens, passwords or LifeTime credentials to any file in this repo.
- Call `createSessionToken` once per session and reuse the token.
- Make Service Studio read calls one at a time. Parallel reads fail with "OML not found".
- A module must be open in Service Studio to be read or changed. `listApps` shows only the open module and its references.
- `omlPublish` publishes the live module in Service Studio, not an unmerged chain, so merge first.
- `docs/conventions.md` is the naming and design standard. If the user asks for something that contradicts it, point that out before doing it.
