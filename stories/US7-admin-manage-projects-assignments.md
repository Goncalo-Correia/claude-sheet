# US7: [Screen] Admin: manage projects and assign them to users

- **Status:** Published
- **Published:** ClaudeSheet_CS 2026-10-19 14:06, ClaudeSheet 2026-10-19 15:32 (both manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-19
- **Depends on:** TT5 (SheetAdmin role, Admin UI flow), US6 (Client_GetList for the client dropdown and filter)
- **Source:** investigation `investigations/2026-10-16-full-app-gaps.md` (findings F03, F04, F08, F23), approved 2026-10-19
- **Trello:** https://trello.com/c/qfGFuwQ0

## Story

As an admin I want to manage projects and assign consultants to them for a period, so that every consultant sees the right projects in MySheet and can log hours. Today a user without an assignment can't log any hours, and nobody can fix that in the app. Covers: F03, F04, F08, F23.

## Acceptance criteria

- [x] A01 · AC7.1 Given a SheetAdmin creates project "Portal | PTL" under ICNF, Then it is listed; a second "PTL" under ICNF is rejected with a clear message.
- [x] A02 · AC7.2 Given user A is assigned to Portal from 01/11/2026 with no end, When A opens MySheet on 02/11, Then Portal appears in the day editor and the weekly table.
- [x] A03 · AC7.3 Given A is assigned to Portal 01/11–30/11, When an admin adds 15/11–31/12 for A on Portal, Then it is rejected as overlapping.
- [x] A04 · AC7.4 Given A has entries on Portal, When the admin deletes the assignment, Then it is rejected and "End" is offered instead.
- [x] A05 · AC7.5 Given a project with assignments, Then it can't be deleted by accident (delete rule Protect) and can be deactivated.
- [x] A06 · AC7.6 Given a SheetUser without SheetAdmin, Then Projects is not in the menu and opening it goes to InvalidPermissions.
- [x] A07 · AC7.7 Given Service Studio, Then 0 errors and 0 warnings in both modules.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Public user exceptions ProjectValidation and AssignmentValidation.
2. P02 · Public server actions (SheetAdmin check): Project_GetList (ClientId, IncludeInactive), Project_GetById, Project_CreateOrUpdate (Name and Code required, Code unique per client, client active), Project_SetActive.
3. P03 · Public server actions (SheetAdmin check): ProjectAssignment_GetList (ProjectId or UserId), ProjectAssignment_CreateOrUpdate (StartDate <= EndDate or open end; project and client active; no overlap with another active period of the same user and project, per ADR 0002), ProjectAssignment_End (sets EndDate), ProjectAssignment_Delete (only when the user has no TimeEntry on the project in the period).
4. P04 · Change the ProjectAssignment.ProjectId delete rule from Delete to Protect.

### ClaudeSheet

Dependencies to add in Service Studio › Manage Dependencies › ClaudeSheet_CS: the 8 actions, the 2 exceptions, SheetAdmin (plus User_GetActive and the structures ProjectSummary, ProjectAssignmentSummary, UserOption).

- Server Actions: Project_GetList, Project_GetById, Project_CreateOrUpdate, Project_SetActive, ProjectAssignment_GetList, ProjectAssignment_CreateOrUpdate, ProjectAssignment_End, ProjectAssignment_Delete, User_GetActive
- Structures: ProjectSummary, ProjectAssignmentSummary, UserOption
- Exceptions: ProjectValidation, AssignmentValidation
- Roles: SheetAdmin (already referenced since TT5); Client_GetList / ClientSummary already referenced since US6

5. P05 · Screen Admin › Projects (SheetAdmin): table Client, Code, Name, Assigned users, Active; filter by client; "New project".
6. P06 · Screen Admin › ProjectDetail (ProjectId input): Client dropdown, Code, Name, Active; section "Assigned users" (user, start, end, status) with Add, Edit, End and Delete (popup with a user dropdown and dates); validation messages from the exceptions.
7. P07 · Menu link "Projects", visible to SheetAdmin.

Decisions before building (2026-10-19): the popup's user dropdown needs a user list the plan doesn't provide, so ClaudeSheet_CS gets a 9th public action **User_GetActive** (SheetAdmin check) returning the public structure **UserOption** (UserId, Name, Username) of every active user (user's choice; US9's User_GetList stays separate). The lists return public structures named by the conventions: **ProjectSummary** and **ProjectAssignmentSummary**. As in US6, the Model API can't set a user exception's Public flag, so **ProjectValidation › Public = Yes and AssignmentValidation › Public = Yes are set by hand** in Service Studio.

## Implementation notes

### ClaudeSheet_CS

- Implemented 2026-10-19 in **1 `applyModelApiCode` save** (one dry run before it, no renames, 0 validation messages), laid out as built: vertical main path, origin 3200/800, 1371 between rows. Every action starts with `Is SheetAdmin?` (`CheckSheetAdminRole()`); its False branch raises the **Not SheetAdmin** role exception to the right ("Only administrators can manage projects." / "… project assignments." / "… list users.").
- **P01:** user exceptions ProjectValidation and AssignmentValidation created. **Public must be set by hand** before ClaudeSheet_CS is published.
- **P02 · project actions** (all public):
  - `Project_GetList(ClientId, IncludeInactive)` → `Projects` (List of **ProjectSummary**: Id, ClientId, ClientName, Code, Name, IsActive, AssignedUsers). SQL node `GetProjects`: projects joined with their client; AssignedUsers = distinct users with an active assignment that hasn't ended (open end or EndDate >= today); empty ClientId = all clients; ordered by client and project name.
  - `Project_GetById(ProjectId)` → `ProjectRecord`. Raises ProjectValidation "Project not found."
  - `Project_CreateOrUpdate(ProjectRecord)` → `ProjectId`. Checks in order: "Enter the project name.", "Enter the project code.", "Choose a client." (client not found), "This client is inactive. Activate it first or choose another client.", "A project with this code already exists for this client." (GetSameCode: same client, same trimmed code, other Id). Name and Code are trimmed, then CreateOrUpdateProject.
  - `Project_SetActive(ProjectId, IsActive)`: GetProject → "Project not found." → UpdateProject.
- **P03 · assignment actions** (all public):
  - `ProjectAssignment_GetList(ProjectId, UserId)` → `Assignments` (List of **ProjectAssignmentSummary**: Id, ProjectId, ProjectCode, ProjectName, ClientName, UserId, UserName, StartDate, EndDate, IsActive, Status). Raises AssignmentValidation "Choose a project or a user." when both are empty. SQL node `GetAssignments` joins Project, Client and User; Status on today's date = Inactive / Upcoming / Ended / Active.
  - `ProjectAssignment_CreateOrUpdate(ProjectAssignmentRecord)` → `ProjectAssignmentId`. Checks: "Choose a user.", "Enter the start date.", "The end date can't be before the start date.", "Project not found.", "This project or its client is inactive.", then **GetOverlapping** (same user and project, other Id, active or same StartDate, periods intersect with open ends treated as unlimited) → "This user is already assigned to this project in an overlapping period." The same-StartDate condition also covers an inactive period, so UQ_PA_UserProjectStart can't fail with a database error. Saves with IsActive = True.
  - `ProjectAssignment_End(ProjectAssignmentId, EndDate)`: "Assignment not found.", "Enter the end date.", "The end date can't be before the start date." → UpdateProjectAssignment.
  - `ProjectAssignment_Delete(ProjectAssignmentId)`: GetEntriesInPeriod (TimeEntry ⋈ DaySheet: the assignment's user and project, SheetDate in the period) → "This assignment has time entries and can't be deleted. Use End to close it instead." → DeleteProjectAssignment.
  - `User_GetActive` → `Users` (List of **UserOption**), SQL node `GetUsers`: active users ordered by name.
- **P04:** ProjectAssignment.ProjectId delete rule is now **Protect**.
- **Flow layout gate:** the 9 actions are vertical with horizontal raise branches: 0 diagonals, 0 crossings, 0 overlaps. No other flow touched.
- **Merge:** `omlMerge` returned `merged: true`; the live module was re-checked afterwards. Plan check round 1 approved (P01 accepted despite Rejected), see `reviews/ClaudeSheet_CS/US7.md`.
- **Review:** round 1 approved, all Yes (`reviews/ClaudeSheet_CS/US7.md`). ProjectValidation and AssignmentValidation › Public = Yes set by the user.
- **Published** 2026-10-19 14:06 by the user (1-Click Publish; the MCP publish was blocked). Change log `changes/2026-10-19/135822-ClaudeSheet_CS-US7.cs`; snapshots refreshed (targeted).

### ClaudeSheet

- **Dependencies:** first check after "Done" found none of the 12 US7 elements in `References › ClaudeSheet_CS` (the ticks weren't applied yet). After the second "Done", `omlRefreshReferences` reported "No references needed refreshing" and the probe found all 9 actions and 3 structures, none suffixed.
- Implemented in **2 `applyModelApiCode` saves** (one dry run before them):
  1. Screens, data actions, screen actions, CSS and the menu link. The save left 6 errors in the dropdowns' Options Text/Value.
  2. Fix: a dropdown's Options Text/Value use the **bare attribute names of the list item** (`Id`, `Name`, `UserId`), as the existing FillProjectDropdown does (`ProjectId`). Neither `ClientSummary.Id` nor `GetClients.Clients.Current.Id` resolves. 0 validation messages after this save.
- **P05 · Admin › Projects** (roles: SheetAdmin only):
  - Locals: FilterClientId, ShowInactive.
  - Data actions: **GetClients** (Client_GetList, inactive included, for the filter) and **GetProjects** (Project_GetList(FilterClientId, ShowInactive) + ErrorMessage). **OnAfterFetch_GetProjects** shows the error.
  - Toolbar: ClientFilterDropdown ("All clients" empty option), ShowInactiveCheckbox (both OnChange → **OnChange_Filters**), NewProjectButton → ProjectDetail(NullIdentifier()).
  - Table: Client, Code, Name, Assigned users, Status. Each row links to ProjectDetail; empty state "No projects found."; ARIA roles.
- **P06 · Admin › ProjectDetail** (roles: SheetAdmin; input ProjectId, optional):
  - Data actions:
    - **GetProject**: a new project gets IsActive = True; otherwise Project_GetById. **OnAfterFetch_GetProject** copies it into **EditProject**.
    - **GetClients**: includes inactive clients, labelled "(inactive)", so an existing project keeps its client. The server still refuses an inactive client on save.
    - **GetUsers**: User_GetActive.
    - **GetAssignments**: ProjectAssignment_GetList(ProjectId); nothing for a new project. Its **OnAfterFetch_GetAssignments** shows errors.
  - Form: ClientDropdown, CodeInput (max 30) and NameInput (max 100), all mandatory; a status line; Save (built-in validation); Activate/Deactivate for an existing project.
    - **OnClick_SaveButton** calls Project_CreateOrUpdate. A new project then opens its own ProjectDetail, so users can be added right away. An existing one reloads GetProject.
    - **OnClick_ToggleActiveButton** calls Project_SetActive.
  - Section **Assigned users** (existing projects only): "Add user", then a table (User, Start, End or "No end", Status, Actions). Each row has the links Edit, End and Delete; empty state "No users assigned yet."
  - **Popup:** the built-in Reactive Popup widget **AssignmentPopup** (ShowPopup = ShowAssignmentPopup). OutSystems UI's Popup block isn't referenced, and the Model API can't add the reference.
    - Edit mode: UserDropdown, StartDateInput (mandatory), EndDateInput (empty = no end), Save → **OnClick_SaveAssignmentButton** (ProjectAssignment_CreateOrUpdate).
    - End mode: the user and start date, EndOnlyDateInput, "End assignment" → **OnClick_SaveEndButton** (ProjectAssignment_End).
    - Cancel → **OnClick_CancelPopupButton**.
    - Openers: **OnClick_AddAssignmentButton** starts today with no end. **OnClick_EditAssignmentLink** and **OnClick_EndAssignmentLink** take input Row (ProjectAssignmentSummary); End defaults to today when there's no end.
  - **AC7.4 "End is offered":** **OnClick_DeleteAssignmentLink** calls ProjectAssignment_Delete. When the server refuses ("…has time entries… Use End to close it instead."), its AllExceptions handler opens the popup in **End mode** with that message.
  - Errors: screen actions catch **AllExceptions** and show `AllExceptions.ExceptionMessage` (in FormError above the form, or PopupError in the popup), per `docs/conventions.md` › Catching a ClaudeSheet_CS user exception.
- **P07 · Menu:** new link **ProjectsLink** "Projects" → Admin › Projects, third in PageLinks (role="menuitem"), Visible = `GetMenuAccess.IsSheetAdmin`. Both new screens pass ActiveItem = 2.
- **CSS:** the `.admin-*` rules on both new screens' stylesheets (empty before, so they publish). The table grid now has 5 columns; Clients/ClientDetail keep their own 3-column copy from US6. Move these to the theme in a later story.
- **Flow layout gate:** 19 flows (15 screen actions and 6 data actions on the two screens): 0 diagonals, 0 crossings, 0 overlaps. Handlers sit in their own columns.
- **Merge:** `omlMerge` returned `merged: true`; live module re-checked, 0 validation messages. Plan check round 1 approved (`reviews/ClaudeSheet/US7.md`).
- **Still to verify at runtime (A01–A06):** as an admin, create "Portal | PTL" under ICNF and try a second PTL; assign A from 01/11/2026 with no end and check MySheet; try an overlapping period; delete an assignment that has entries (the popup should switch to End); deactivate a project; as a non-admin, check the menu and the screen.

## Trail

- Changes: `changes/2026-10-19/135822-ClaudeSheet_CS-US7.cs`, `changes/2026-10-19/142335-ClaudeSheet-US7.cs` (screens, menu), `changes/2026-10-19/152218-ClaudeSheet-US7.cs` (dropdown options fix)
- Review: `reviews/ClaudeSheet_CS/US7.md` (plan check + review round 1 approved), `reviews/ClaudeSheet/US7.md` (plan check + review round 1 approved, N02 accepted despite Rejected)
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 14:06, ClaudeSheet 15:32; the MCP attempts were blocked)
- Snapshots: `snapshots/ClaudeSheet_CS/` and `snapshots/ClaudeSheet/` refreshed 2026-10-19 (targeted)
