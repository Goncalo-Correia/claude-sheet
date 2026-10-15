# TT4: Move the backend from ClaudeSheet to ClaudeSheet_CS

- **Status:** Published (ClaudeSheet_CS 2026-10-15 18:13, ClaudeSheet 2026-10-15 21:59, manual 1-Click Publish). P13 (seed data and role grants at runtime) is checked by the user.
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-15
- **Depends on:** TT1, TT2, US4
- **Trello:** https://trello.com/c/tOVt9YEp
- **Decision:** [ADR 0004](../docs/decisions/0004-split-end-user-and-core.md)

## Story

### Goal

Split ClaudeSheet into two modules, as decided in **ADR 0004** (`docs/decisions/0004-split-end-user-and-core.md`): **ClaudeSheet_CS** (Core) becomes the backend, and **ClaudeSheet** (End-user) keeps only the front end. **No functional change**: MySheet behaves exactly as before, on top of the new module.

### Module placement (docs/conventions.md › Module placement)

- **ClaudeSheet_CS:** entities and static entities, timers, processes, server actions, structures, plus the SheetUser role, the user exceptions and the site properties those depend on.
- **ClaudeSheet:** screens, blocks, data actions, screen/block actions, client actions, theme, client variables, and the login template (DoLogin, DoLogout).
- ClaudeSheet references ClaudeSheet_CS, never the reverse. CS elements are public only when ClaudeSheet uses them, and entities are public with Read Only = Yes.

### What moves (inventory 2026-10-15; ClaudeSheet_CS is empty)

- **Static entities (2):** DayStatus, CalendarDayType. Keep the record identifiers and names, because expressions use Entities.X.Identifier.
- **Entities (6):** Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry, with the same attributes, indexes and delete rules.
- **Structures (12):** CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption, DayEditorLine, DayStatusKPI.
- **Server actions (12):** Bootstrap, Calendar_GetMonth, Calendar_GetMonthSummary, Date_GetWeekStart, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, Holiday_CreateIfMissing, Minutes_Format, Project_GetAssignedToUser, ProjectHours_GetWeekly. Plus a new public **Settings_GetDailyExpectedMinutes**, because HoursCalendar reads Site.DailyExpectedMinutes.
- **Timer:** Timer_Bootstrap. **Role:** SheetUser. **User exception:** DaySheetValidation. **Site properties:** DailyExpectedMinutes (480), SeedTestUsernames.
- **Stays in ClaudeSheet:** DoLogin, DoLogout, the client actions Minutes_Format and Date_GetWeekStart, all screens and blocks, the theme and client variables.

### How

- The Model API can't move an element between modules. Each element is **recreated** in ClaudeSheet_CS from its creation code in `snapshots/ClaudeSheet/` (data-model.cs, structures.cs, server-actions/*.cs, settings.cs, timers.cs, user-exceptions.cs, roles/). `AddDependency` lines are replaced by `eSpace.References.Named(...)`.
- ClaudeSheet then **rebinds** every use to the CS element: Execute Server Action nodes, structure data types, Entities.X expressions, role checks, the exception handler for DaySheetValidation, and Site.DailyExpectedMinutes. Only after that are the local copies deleted. Each rebind is checked with a throw-probe before it's saved.
- It's a multi-module story (CLAUDE.md › Multi-module stories): a ClaudeSheet_CS subagent goes first; then the CS review and publish; then **the user adds the CS dependencies in Manage Dependencies**; then the ClaudeSheet subagent runs after `omlRefreshReferences`.

### Data and access (to accept or plan for)

- Recreated entities are **new tables**. The Bootstrap timer reseeds the CS tables (clients, projects, holidays, assignments). Day sheets and time entries logged while testing are lost unless migrated by hand.
- SheetUser in CS is a **new role**. Bootstrap re-grants it to the seed users (SeedTestUsernames). Anyone else needs it granted again.

### Out of scope

- Any change to behaviour, names, layout or CSS
- The open question in ADR 0004 (UI-only structures could stay in ClaudeSheet). TT4 follows "all structures in CS".

### Dependencies

TT1, TT2, US4 (current state of the module). TT3 is independent.

## Acceptance criteria

- [ ] A01 · AC-TT4.1 Given ClaudeSheet_CS after TT4, Then it contains the 2 static entities (same records and identifiers), 6 entities (same attributes, indexes, delete rules), 12 structures, 13 server actions, Timer_Bootstrap, the SheetUser role, DaySheetValidation and the 2 site properties
- [ ] A02 · AC-TT4.2 Given ClaudeSheet after TT4, Then it has no entities, static entities, structures, timers, processes, site properties, user exceptions or roles of its own, and no server actions except DoLogin and DoLogout
- [ ] A03 · AC-TT4.3 Given the references, Then ClaudeSheet references ClaudeSheet_CS and ClaudeSheet_CS does not reference ClaudeSheet
- [ ] A04 · AC-TT4.4 Given ClaudeSheet_CS, Then its entities are public with Read Only = Yes, and only elements used by ClaudeSheet are public
- [ ] A05 · AC-TT4.5 Given both modules, Then getValidationMessages returns 0 errors and queries/flow-layout.cs reports no rule violations beyond the ones already accepted
- [ ] A06 · AC-TT4.6 Given both modules published and Timer_Bootstrap run, Then the seed clients, projects, holidays and assignments exist in the ClaudeSheet_CS tables and the seed users have the SheetUser role
- [ ] A07 · AC-TT4.7 Given the mockup scenario (October 2026), When I use TotalHoursMonth, HoursCalendar, DayStatusKPIs and WeeklyProjectHours, Then they behave exactly as before (totals, colours, KPI counts, day editor, fill pending days, weekly grid)
- [ ] A08 · AC-TT4.8 Given a user without SheetUser, When a block calls a ClaudeSheet_CS server action, Then the role exception is raised and no data is returned
- [ ] A09 · AC-TT4.9 Given a validation error in DaySheet_Save (e.g. more than 24:00), Then the day editor still shows the DaySheetValidation message
- [ ] A10 · AC-TT4.10 Given the repository, Then snapshots/ClaudeSheet_CS/ exists, snapshots/ClaudeSheet/ no longer has the moved elements, and docs/architecture.md and CLAUDE.md describe the finished split

## Implementation plan

### ClaudeSheet_CS

1. P01 · confirm the inventory against the live ClaudeSheet and that both modules are open (listApps); keep the creation code from snapshots/ClaudeSheet/ as the source
2. P02 · create the site properties DailyExpectedMinutes (480) and SeedTestUsernames, the role SheetUser (public) and the user exception DaySheetValidation (public)
3. P03 · create the static entities DayStatus and CalendarDayType with the same attributes and records (same identifiers), public
4. P04 · create the entities Client, Project, ProjectAssignment, Holiday, DaySheet and TimeEntry with the same attributes, indexes and delete rules; public, Read Only = Yes
5. P05 · create the 12 structures, public where ClaudeSheet uses them
6. P06 · create the 12 server actions (role gate, same logic) and the new public Settings_GetDailyExpectedMinutes; public only where ClaudeSheet calls them
7. P07 · create Timer_Bootstrap running Bootstrap
8. P08 · conventions gate (names, flow layout report, placement), 0 validation errors; plan check, merge, review, publish

### Hand-off (user)

9. P09 · in ClaudeSheet, Manage Dependencies, add every public ClaudeSheet_CS element ClaudeSheet uses (Claude lists them)

### ClaudeSheet

Dependencies to add in Service Studio › Manage Dependencies (ClaudeSheet → ClaudeSheet_CS), from what ClaudeSheet's blocks use:

- **Server Actions (9):** Calendar_GetMonth, Calendar_GetMonthSummary, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, Project_GetAssignedToUser, ProjectHours_GetWeekly, Settings_GetDailyExpectedMinutes
- **Structures (12):** CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption, DayEditorLine, DayStatusKPI
- **Static Entities (2):** CalendarDayType (source of the GetKPIs aggregate), DayStatus (Entities.DayStatus in HoursCalendar)
- **Entities (1):** Project (type of HoursCalendar's FillProjectId)
- Not needed yet: the SheetUser role (no screen uses it until US5) and DaySheetValidation (HoursCalendar catches AllExceptions).

10. P10 · omlRefreshReferences, then rebind every use of the local backend elements to the ClaudeSheet_CS ones (server action calls, structure types, Entities.X expressions, role checks, the DaySheetValidation handler); replace Site.DailyExpectedMinutes with Settings_GetDailyExpectedMinutes
11. P11 · delete the local entities, static entities, structures, server actions (except DoLogin, DoLogout), timer, role, user exception and site properties
12. P12 · 0 validation errors, flow layout report clean; plan check, merge, review, publish

### After both modules

13. P13 · Run Timer_Bootstrap in ClaudeSheet_CS once and check the seed data and the SheetUser grants
14. P14 · Snapshots: create snapshots/ClaudeSheet_CS/ and refresh snapshots/ClaudeSheet/ (remove the moved elements' files); update docs/architecture.md status and the CLAUDE.md 'until TT4' note

## Implementation notes

### ClaudeSheet_CS

- Implemented 2026-10-15 by a **ClaudeSheet_CS subagent**, as described in CLAUDE.md › Multi-module stories. A first subagent was stopped by the user, and Service Studio then restarted; its single unmerged save was lost. The MCP endpoint moved from port 41820 to **41821** and a new session token was issued. A second subagent redid the work from scratch.
- **6 saves**, every one with 0 errors. The code is in the session scratchpad (`tt4cs/r2_*.cs`, logged in `saves2.log`) and goes to `changes/` at publish:
  1. site properties, role, user exception;
  2. static entities;
  3. entities;
  4. structures;
  5. actions part 1 (Date_GetWeekStart, Minutes_Format, Holiday_CreateIfMissing, Project_GetAssignedToUser, Calendar_GetMonth, Settings_GetDailyExpectedMinutes);
  6. actions part 2 and the timer (Calendar_GetMonthSummary, DaySheet_GetDetail, DaySheet_Save, DaySheet_Clear, DaySheet_FillPending, ProjectHours_GetWeekly, Bootstrap, Timer_Bootstrap).
- **Source:** the actions were generated from the **live** ClaudeSheet actions, read at the start, and the structures and entities from `snapshots/ClaudeSheet/`. Read back and diffed, each of the 12 copied actions matches ClaudeSheet; the only differences are Public/Shareable and the order of argument lines. The entities' attributes, indexes and delete rules, the structures' attributes, and the static records (identifiers and Ids) match line for line. Node positions were copied, so the flow-layout report shows only the accepted carry-overs: DaySheet_FillPending with 1 crossing, and merges in DaySheet_Save (1) and Bootstrap (2).
- **Public:** all entities and static entities (Read Only = Yes), all 12 structures, the SheetUser role, and the server actions ClaudeSheet calls (found by probing its blocks): Calendar_GetMonth, Calendar_GetMonthSummary, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, Project_GetAssignedToUser, ProjectHours_GetWeekly, and the new Settings_GetDailyExpectedMinutes. Not public: Bootstrap and Holiday_CreateIfMissing (timer only), and Date_GetWeekStart and Minutes_Format (only server code calls them; the UI uses its client actions of the same names).
- **Settings_GetDailyExpectedMinutes (new):** a role gate (If `CheckSheetUserRole()`, else Raise the SheetUser role exception), then `Minutes = Site.DailyExpectedMinutes`, then End.
- **Deviation, DaySheetValidation isn't public.** The Model API's `IUserException` has no `Public` property. It has to be set to Public by hand in Service Studio (ClaudeSheet_CS › Logic › Exceptions › DaySheetValidation › Public = Yes) before CS is published, otherwise ClaudeSheet can't catch it (AC-TT4.9).
- **Replay fixes:** the read-back line `RoleActions.Named("GrantSheetUserRole")` doesn't compile, so it became `sheetUser.GrantRoleAction`. `SetDefaultValue(null)` on SQL node parameters throws, so those lines were dropped. And `Public = true` must be set last on each action: a later `Shareable = false` line reset it.
- **Order:** static records are stored in a different order inside each entity, with the same identifiers and Ids.

### Hand-off (P09)

- ClaudeSheet_CS was published by the user (2026-10-15 18:13). The dependency checklist (24 elements) was confirmed Done, `omlRefreshReferences` ran on ClaudeSheet, and a probe found all 24 in the ClaudeSheet → ClaudeSheet_CS reference.
- **What a reference lists:** the ticked elements **plus what they depend on**. Client, Holiday, ProjectAssignment, DaySheet and TimeEntry came along through the structures' identifier types. It doesn't list every public element.
- **Name clashes:** every CS element with a same-named local copy in ClaudeSheet came in with a **"2" suffix** (`DaySheet_Save2`, `CalendarDay2`, `CalendarDayType2`, …). `Settings_GetDailyExpectedMinutes` kept its name. A dry run that deleted the local copies in memory left the suffix in place (and 138 errors before rebinding).
- **Decision (user):** rebind every use to the `…2` elements and delete the local copies in one chain. After the merge, refresh references and check whether the suffix drops. If it doesn't, the user removes and re-adds the dependencies in Service Studio once, and the result is checked again.

### ClaudeSheet

- Implemented 2026-10-15 by a **ClaudeSheet subagent** in **2 saves**, logged in the session scratchpad (`tt4cl/saves.log`):
  1. rebind and delete (P10 + P11) in one lambda, dry-run first to 0 errors;
  2. a description fix (HoursCalendar › OnClick_FillPendingButton mentioned Site.DailyExpectedMinutes).
- **Rebinding method:** the Model API's `ReplaceReferences(old, new)`, called on every descendant object of every UI flow and client action, for the 8 server actions, 12 structures and 8 entities (local → "2"), their list types and the entities' identifier types. It rebinds Execute nodes (arguments kept), data types, aggregate sources and expression text in one pass. The platform renamed the Execute nodes after their new actions (e.g. `DaySheet_GetDetail2`), and every expression that used them was rewritten (`Calendar_GetMonthSummary2.Summary`, `Entities.CalendarDayType2.Pending`, …).
- **GetKPIDayTypes** (DayStatusKPIs) kept its name. It now reads ClaudeSheet_CS CalendarDayType2, with the same filter and sort.
- **Site.DailyExpectedMinutes → Settings_GetDailyExpectedMinutes:** in HoursCalendar › GetCalendar, an Execute Server Action node goes between Calendar_GetMonthSummary2 and the Assign, and `DailyExpectedMinutes = Settings_GetDailyExpectedMinutes.Minutes`. The nodes below moved down one row, so the main column stays vertical.
- **Deleted (P11):** Timer_Bootstrap; the 12 server actions (all except DoLogin and DoLogout, including the server Minutes_Format and Date_GetWeekStart); the 12 structures; the 8 entities and static entities; DaySheetValidation; the site properties DailyExpectedMinutes and SeedTestUsernames; and the SheetUser role. No screen or block used the role: Login and InvalidPermissions have empty Roles, and a dry run with and without the delete gave the same error count.
- **Kept:** the client actions Minutes_Format and Date_GetWeekStart, DoLogin and DoLogout, and the template site properties (TenantName, TenantId).
- **Verified by the main session** on the mutated module: no structures, entities, timers, roles or user exceptions left. The blocks call only ClaudeSheet_CS actions: HoursCalendar (8, including Settings_GetDailyExpectedMinutes), TotalHoursMonth and DayStatusKPIs (Calendar_GetMonthSummary2), and WeeklyProjectHours (ProjectHours_GetWeekly2). 0 validation errors, and flow-layout.cs passes on all 59 flows.
- **Merge:** my HTTP client timed out after 15 minutes while the Compare-and-Merge window was open. The merge still went through when the user finished it: the out file was deleted, and the live module shows the validated state. A retry was refused because there was no working copy left to merge.
- **The "2" suffix, solved without manual steps.** `omlRefreshReferences` after the merge didn't drop it ("No references needed refreshing"). However, the consumer's local name of a referenced element can be renamed through the Model API (`ReferenceAction.Name = …`, and likewise for structures and entities). One extra save renamed the 28 suffixed references back to their CS names. The Execute nodes and every expression followed automatically (e.g. `Calendar_GetMonthSummary.Summary`, `Entities.CalendarDayType.Pending`), with 0 errors and all 59 flows passing the layout report. The user didn't need to remove and re-add dependencies.

## Trail

- Changes: `changes/2026-10-15/175023…175635-ClaudeSheet_CS-TT4.cs` (6, by the CS subagent), `183159-ClaudeSheet-TT4.cs` and `183436-ClaudeSheet-TT4.cs` (by the ClaudeSheet subagent), `201539-ClaudeSheet-TT4.cs` (suffix cleanup, main session). Written by Claude: the publishes were manual and subagent calls aren't in the main transcript.
- Reviews: `reviews/ClaudeSheet_CS/TT4.md`, `reviews/ClaudeSheet/TT4.md` (plan checks and reviews, round 1, all approved)
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 18:13, ClaudeSheet 21:59)
- Snapshots: `snapshots/ClaudeSheet_CS/` (first, full), `snapshots/ClaudeSheet/` (targeted; moved elements' files removed)
- Manual steps by the user: DaySheetValidation Public = Yes in ClaudeSheet_CS; the 24 ClaudeSheet_CS dependencies in ClaudeSheet
- Open: P13, check at runtime that Timer_Bootstrap seeded the CS tables and that the seed users have the CS SheetUser role
