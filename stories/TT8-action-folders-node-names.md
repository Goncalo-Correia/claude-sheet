# TT8: [Tech] Action folders and node names in both modules

- **Status:** Published
- **Published:** ClaudeSheet_CS 2026-10-19 16:28, ClaudeSheet 2026-10-19 16:47 (both manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-19
- **Source:** new conventions `docs/conventions.md` › Naming › Action folders and Node names (added 2026-10-19)
- **Trello:** https://trello.com/c/A6eqqy0K (moved to Done 2026-10-19)

## Story

The conventions now say that every reusable server and client action lives in a folder named after its `<Entity>` prefix, and that every node that can carry a name or label in an action flow has one (server, client, screen/block and data actions). Today no action is in a folder (except DoLogin/DoLogout in Authentication), and many nodes have no label. This task brings both modules in line, so flows read without opening each node and the Logic tree is organised by entity.

Measured on 2026-10-19 (live modules, after US8):

| Module | Actions without folder | Flows | Nodes that can be named | Unlabelled | Execute nodes with a numeric suffix |
|---|---|---|---|---|---|
| ClaudeSheet_CS | 29 of 29 server actions | 29 | 229 | 84 | 16 (Bootstrap 13, ProjectHours_GetWeekly 3) |
| ClaudeSheet | 1 client action (Minutes_Format) | 102 (99 screen/block/data) | 230 | 121 | 1 (HoursCalendar.OnInitialize) |

## Acceptance criteria

- [x] A01 · Every server action in ClaudeSheet_CS and every client action in ClaudeSheet is in a folder named after its `<Entity>` prefix (Bootstrap in **Setup**; DoLogin/DoLogout stay in **Authentication**).
- [x] A02 · Every Assign, If, Message, For Each and Switch node in every flow of both modules has a label that says what it does (the report in `checks/node-names.md` shows 0 unlabelled).
- [x] A03 · No Execute Action node name ends in a numeric suffix. Every expression that referenced a renamed node still validates.
- [ ] A04 · Behaviour is unchanged: MySheet and the admin screens work as before (verify at runtime).
- [x] A05 · Both modules: 0 errors and 0 warnings, and the flow layout report is no worse than before.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Create the server-action folders Calendar, Client, Date, DaySheet, Holiday, Project, ProjectAssignment, ProjectHours, Settings, User and Setup, and move each of the 29 server actions into its folder.
2. P02 · Label the 84 unlabelled nodes and rename the 16 numerically suffixed Execute nodes (e.g. Bootstrap's `Holiday_CreateIfMissing2…13` → `CreateHoliday_<name>`), updating every expression that referenced a renamed node.

### ClaudeSheet

Dependencies: none (folders and node names don't change the public interface of ClaudeSheet_CS). Refresh references after ClaudeSheet_CS is published.

3. P03 · Create the client-action folder Minutes and move Minutes_Format into it.
4. P04 · Label the 121 unlabelled nodes across the screen, block and data actions, and rename the 1 suffixed Execute node, updating references.

### Repository

5. P05 · Add `checks/node-names.md`, a throw-probe report: per module, actions without a folder, unlabelled nodes per flow and suffixed Execute nodes. Document it in `checks/README.md` and in `docs/conventions.md` › Node names.

## Implementation notes

### ClaudeSheet_CS

- Implemented 2026-10-19 in **1 `applyModelApiCode` save** (one dry run before it), 0 validation messages.
- **P01:** 11 server-action folders created (Calendar, Client, Date, DaySheet, Holiday, Project, ProjectAssignment, ProjectHours, Settings, User, Setup), and all 29 server actions moved into their folders.
- **P02:**
  - 84 nodes labelled (Assign and For Each nodes). Each label was written from what the node does, e.g. `Trim name`, `Return Id`, `Each fixed holiday`, `Count copied`, `Classify day`. The lambda matches every label to its node by position and checks it, and the dry run confirmed all 84.
  - 17 Execute nodes renamed. In Bootstrap, `Holiday_CreateIfMissing` … `Holiday_CreateIfMissing13` became `CreateHoliday_NewYear`, `_GoodFriday`, `_Easter`, `_FreedomDay`, `_LabourDay`, `_CorpusChristi`, `_PortugalDay`, `_Assumption`, `_RepublicDay`, `_AllSaints`, `_IndependenceDay`, `_ImmaculateConception` and `_Christmas`; the first one has no suffix but was renamed for consistency, hence 17 rather than 16. `GrantSheetUserRole2` became `GrantSheetUserRole_Admin`. In ProjectHours_GetWeekly, `ListAppend2/3/4` became `AppendDayTotal`, `AppendCell` and `AppendRow`.
  - None of the renamed nodes' outputs is used in an expression, and validation stays at 0 messages.
- **P05** (done first, as the gate for this module): `checks/node-names.md` added and documented in `checks/README.md` and `docs/conventions.md` › Node names. On the mutated module it reports 29 flows: 0 actions without folder, 0 unlabelled nodes, 0 suffixed Execute nodes.
- Folders and node names don't change ClaudeSheet_CS's public interface, so ClaudeSheet needs no dependency change.
- **Flow layout:** no node was moved; the flows keep their layout.
- **Merge:** `omlMerge` returned `merged: true`; live module re-checked with the report (all 0), 0 validation messages. Plan check round 1 approved and review round 1 approved (`reviews/ClaudeSheet_CS/TT8.md`).

### ClaudeSheet

- References refreshed (`omlRefreshReferences`) after the ClaudeSheet_CS publish. No dependency change was needed.
- Implemented 2026-10-19 in **1 `applyModelApiCode` save**, with 0 validation messages. It was preceded by one read-only dump of every unlabelled node with its content.
- **P03:** client-action folder **Minutes** created and **Minutes_Format** moved into it.
- **P04:**
  - 121 nodes labelled (Assign, Message and For Each nodes) across the client action and the screen, block and data actions. Recurring patterns use one label each:
    - `Keep error` (handler Assign of AllExceptions.ExceptionMessage)
    - `Clear error`
    - `Show error` (On After Fetch messages)
    - `Close popup` / `Close editor`
    - `Set <output>` (data action results)
  - The other labels say what the node does, e.g. `Format h:mm`, `Hour option`, `Next quarter`, `Build editor line`, `Offer end instead`, `Close and show year`, `Refresh blocks`. The lambda matches each label to its node by position and checks it; any mismatch would have stopped the save.
  - Execute nodes renamed in HoursCalendar.OnInitialize:
    - `ListAppend2` → `AppendMinuteOption`
    - `ListAppend` → `AppendHourOption` (not suffixed, but renamed for consistency, as in ClaudeSheet_CS)
  - ListAppend has no outputs, so no expression referenced either node.
- **Report** (`checks/node-names.md`) on the mutated module:
  - 102 flows
  - 0 actions without folder
  - 0 unlabelled nodes
  - 0 suffixed Execute nodes
- **Flow layout:** no node was moved.
- **Merge:** `omlMerge` returned `merged: true`; live module re-checked (0 unlabelled, 0 suffixed, Minutes_Format in Minutes). Plan check round 1 approved (`reviews/ClaudeSheet/TT8.md`).

## Trail

- Changes: `changes/TT8-ClaudeSheet_CS.md`, `changes/TT8-ClaudeSheet.md`
- Review: `reviews/ClaudeSheet_CS/TT8.md` (plan check + review round 1 approved), `reviews/ClaudeSheet/TT8.md` (plan check + review round 1 approved, N02 accepted despite Rejected)
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 16:28, ClaudeSheet 16:47; the MCP attempts were blocked)
- Snapshots: `snapshots/ClaudeSheet_CS/` (all 29 server actions) and `snapshots/ClaudeSheet/` (client action, 7 screens, 6 blocks) refreshed 2026-10-19, targeted
