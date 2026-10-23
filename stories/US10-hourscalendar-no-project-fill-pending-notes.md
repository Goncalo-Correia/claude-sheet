# US10: [Block] HoursCalendar: no-project state, fill pending for the shown month, day notes

- **Status:** Done
- **Published:** ClaudeSheet_CS 2026-10-23 13:06, ClaudeSheet 2026-10-23 13:20 (both manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-23
- **Source:** gap analysis of 2026-10-16, findings F19, F20, F25 (the analysis file is in commit 5bb59c3)
- **Trello:** https://trello.com/c/2RBtjW9O (moved to Done 2026-10-23)

## Story

As a consultant I want the day editor and "Fill all pending days" to work for the month I'm looking at, so that I'm not offered actions that then fail. Today the editor shows Save and Confirm with no project lines, "Fill pending" lists only today's projects and rolls back the whole month when one day isn't covered, and day notes can't be edited. Covers: F19, F20, F25.

## Acceptance criteria

- [x] A01 · AC10.1 Given a day without assigned projects, When the editor opens, Then the message is shown and Save/Confirm are hidden.
- [x] A02 · AC10.2 Given an assignment that starts on the 15th and pending days before and after it, When the user fills pending with that project, Then days from the 15th are filled, earlier days are skipped, and the message gives both counts.
- [x] A03 · AC10.3 Given the user browses last month, Then Fill pending offers last month's projects.
- [x] A04 · AC10.4 Given notes are typed and the day is saved, When the day is reopened, Then the notes are shown.
- [x] A05 · AC10.5 Given Service Studio, Then 0 errors and 0 warnings.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Public server action Project_GetAssignedInPeriod (StartDate, EndDate): active projects the current user is assigned to at any point in the period (SheetUser check).
2. P02 · DaySheet_FillPending: skip pending days on which the chosen project isn't assigned instead of failing; return FilledCount and SkippedCount.

### ClaudeSheet

Dependencies to add in Service Studio › Manage Dependencies › ClaudeSheet_CS: Project_GetAssignedInPeriod; refreshed DaySheet_FillPending.

- Server Actions: Project_GetAssignedInPeriod, DaySheet_FillPending (refresh)
- Done 2026-10-23 through the MCP (`outsystems-mcp-add-reference`, then `omlRefreshReferences`); see the implementation notes.

3. P03 · HoursCalendar day editor: If HasProjectLines (EditorLines.Length > 0); when false, show "No projects are assigned to you on this day. Ask your administrator." and hide Save draft and Confirm day.
4. P04 · OnClick_FillPendingButton: load the shown month's projects with Project_GetAssignedInPeriod; when there are none, show "No projects assigned this month" instead of the dropdown; after the fill, report filled and skipped days.
5. P05 · Day editor: TextArea NotesInput bound to EditorNotes (max 500), labelled "Notes".

## Implementation notes

### ClaudeSheet_CS

- Implemented 2026-10-23 in **3 `applyModelApiCode` saves** (build, `checks/layout-flow.cs` on the two touched flows, then a hand placement of DaySheet_FillPending's loop body), with 0 validation messages. Flow-layout report: both flows 0 other diagonals, 0 crossings, 0 overlaps; node names: 0 unlabelled, 0 suffixed, every action in a folder.
- **P01 · Project_GetAssignedInPeriod(StartDate, EndDate) → Projects (List of DaySheetProjectLine)**, public, folder Project. Same output structure as Project_GetAssignedToUser, so the Fill pending dropdown can switch to it without other changes.
  - `Is SheetUser?`; when False, raises the SheetUser role exception "You are not allowed to use MySheet."
  - Aggregate `GetAssignedProjects`: ProjectAssignment ⋈ Project ⋈ Client; current user, active assignment, `StartDate <= EndDate` and (`EndDate` null or `>= StartDate`) — the assignment overlaps the period; active project and client. Sorted by client name, project name, project Id.
  - For Each `Each assignment` → `Already listed?` (same project as the last line appended; the sort keeps a project's assignments together) → `Build line` → ListAppend. A user with two assignments to the same project in the period gets the project once.
- **P02 · DaySheet_FillPending**: new output **SkippedCount** (Integer); `Init entry` also sets SkippedCount = 0. On a Pending day that isn't a draft with hours, the new aggregate `GetAssignmentOnDay` (max 1 record; same filters as DaySheet_Save's assignment check: current user, ProjectId, active assignment covering the day, active project and client) feeds `Assigned on day?`: True → DaySheet_Save + `Count filled` as before; False → `Count skipped` and on to the next day. Description updated. Drafts with hours are still confirmed as before, whatever their project.
- **Layout:** `checks/layout-flow.cs` left 2 crossings and 2 overlaps in DaySheet_FillPending (three loop-backs from the body columns). Placed by hand: the save branch runs down column 2, the draft branch down column 3, and the last node of each branch (`Count skipped`, both `Count filled`) sits in column 1 so its loop-back diagonal to `Each day` crosses nothing. The draft branch's `UpdateDaySheet` is 4 rows below `Confirm draft` so its return passes under the save branch.

### ClaudeSheet

- **Dependency** (2026-10-23, at the user's request with the `outsystems-mcp-add-reference` skill instead of Manage Dependencies): loaded a copy of ClaudeSheet_CS's OML (as published at 13:06) and added `Project_GetAssignedInPeriod` to the ClaudeSheet_CS reference with `AddDependency` (1 save, merged), then `omlRefreshReferences` so `DaySheet_FillPending` shows `SkippedCount`. Probe afterwards: both found, no suffixed names. The user should still look at Manage Dependencies before the publish.
- Implemented in **2 `applyModelApiCode` saves** (build, then a rename), 0 errors and 0 warnings. One earlier attempt failed with "Specified cast is not valid" and saved nothing: a widget's `Parent` is the container's placeholder, not the container.
- **P03:** in HoursCalendar › DayEditorPopup › IsWorkDay, new If widget **HasProjectLines** (`EditorLines.Length > 0`); ProjectLines and DayTotal moved into its True branch; the False branch has container NoProjectsMessage (`sheet-popup__hint`) with "No projects are assigned to you on this day. Ask your administrator." SaveDraftButton and ConfirmDayButton: Visible = `not IsDayOff and EditorLines.Length > 0`.
- **P04:** OnClick_FillPendingButton rebuilt (Start, Execute **Project_GetAssignedInPeriod**, `Init fill popup`, End): StartDate = `NewDate(Year, Month, 1)`, EndDate = `AddDays(AddMonths(NewDate(Year, Month, 1), 1), -1)` — the shown month. In FillPendingPopup, new If widget **HasFillProjects** (`FillProjects.Length > 0`): FillProjectDropdown moved into the True branch; the False branch shows NoFillProjectsMessage "No projects assigned this month". OnClick_ConfirmFillButton's message (label `Show filled and skipped`) is now `<FilledCount> days filled, <SkippedCount> skipped (not assigned to the project on that day)`.
  - Not in the plan, so not done: Confirm in FillPendingPopup stays visible when there are no projects; clicking it shows the existing "Choose a project." error.
- **P05:** container **NotesField** (`margin-top-base`) right after IsWorkDay in DayEditorPopup, so notes can also be written on a day off: Label **NotesLabel** "Notes" (target NotesInput) and TextArea **NotesInput** bound to EditorNotes, MaxLength 500 (DaySheet.Notes is Text 500), style `form-control`. `SetVariable` renamed the text area to `TextArea_EditorNotes`; save 2 renamed it back to NotesInput. OnClick_SaveDay already sends EditorNotes to DaySheet_Save, and OpenDayEditor already loads it.
- **Layout:** OnClick_FillPendingButton and OnClick_ConfirmFillButton have 0 other diagonals, 0 crossings, 0 overlaps and 0 unlabelled nodes.

## Trail

- Changes: `changes/US10-ClaudeSheet_CS.md`, `changes/US10-ClaudeSheet.md`
- Review: `reviews/ClaudeSheet_CS/US10.md`, `reviews/ClaudeSheet/US10.md`
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 2026-10-23 13:06, then ClaudeSheet 13:20; both manual 1-Click Publish)
- Plan check and review: approved in one round per module (2026-10-23)
- Snapshots: refreshed 2026-10-23 (`snapshots/ClaudeSheet_CS/`, `snapshots/ClaudeSheet/`)
