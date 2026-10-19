# TT8: [Tech] Action folders and node names in both modules

- **Status:** To do
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-19
- **Source:** new conventions `docs/conventions.md` › Naming › Action folders and Node names (added 2026-10-19)
- **Trello:** https://trello.com/c/A6eqqy0K (Backlog)

## Story

The conventions now say that every reusable server and client action lives in a folder named after its `<Entity>` prefix, and that every node that can carry a name or label in an action flow has one (server, client, screen/block and data actions). Today no action is in a folder (except DoLogin/DoLogout in Authentication), and many nodes have no label. This task brings both modules in line, so flows read without opening each node and the Logic tree is organised by entity.

Measured on 2026-10-19 (live modules, after US8):

| Module | Actions without folder | Flows | Nodes that can be named | Unlabelled | Execute nodes with a numeric suffix |
|---|---|---|---|---|---|
| ClaudeSheet_CS | 29 of 29 server actions | 29 | 229 | 84 | 16 (Bootstrap 13, ProjectHours_GetWeekly 3) |
| ClaudeSheet | 1 client action (Minutes_Format) | 102 (99 screen/block/data) | 230 | 121 | 1 (HoursCalendar.OnInitialize) |

## Acceptance criteria

- [ ] A01 · Every server action in ClaudeSheet_CS and every client action in ClaudeSheet is in a folder named after its `<Entity>` prefix (Bootstrap in **Setup**; DoLogin/DoLogout stay in **Authentication**).
- [ ] A02 · Every Assign, If, Message, For Each and Switch node in every flow of both modules has a label that says what it does (the report in `queries/node-names.cs` shows 0 unlabelled).
- [ ] A03 · No Execute Action node name ends in a numeric suffix. Every expression that referenced a renamed node still validates.
- [ ] A04 · Behaviour is unchanged: MySheet and the admin screens work as before (verify at runtime).
- [ ] A05 · Both modules: 0 errors and 0 warnings, and the flow layout report is no worse than before.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Create the server-action folders Calendar, Client, Date, DaySheet, Holiday, Project, ProjectAssignment, ProjectHours, Settings, User and Setup, and move each of the 29 server actions into its folder.
2. P02 · Label the 84 unlabelled nodes and rename the 16 numerically suffixed Execute nodes (e.g. Bootstrap's `Holiday_CreateIfMissing2…13` → `CreateHoliday_<name>`), updating every expression that referenced a renamed node.

### ClaudeSheet

Dependencies: none (folders and node names don't change the public interface of ClaudeSheet_CS). Refresh references after ClaudeSheet_CS is published.

3. P03 · Create the client-action folder Minutes and move Minutes_Format into it.
4. P04 · Label the 121 unlabelled nodes across the screen, block and data actions, and rename the 1 suffixed Execute node, updating references.

### Repository

5. P05 · Add `queries/node-names.cs`, a throw-probe report: per module, actions without a folder, unlabelled nodes per flow and suffixed Execute nodes. Document it in `queries/README.md` and in `docs/conventions.md` › Node names.

## Implementation notes

## Trail
