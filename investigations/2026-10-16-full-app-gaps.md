# Investigation: what a fully functional ClaudeSheet still needs

- **Date:** 2026-10-16 15:09
- **Requested by:** goncalo.c@bool.pt ("which US are needed to have a fully functional app"; known gap: a screen to manage projects and assign them to users)
- **Scope:** ClaudeSheet_CS (Core) and ClaudeSheet (End-user), whole modules
- **Lenses:** Gaps, plus Correctness and Security where they block the app
- **Subagents:** 2, read-only. ClaudeSheet_CS: 7 reads (2 probes failed to compile and ran nothing). ClaudeSheet: 11 reads (4 probes failed to compile and were retried).

## Checklist

| ID | Severity | Lens | Module | Element | Finding | Evidence | Suggestion |
|---|---|---|---|---|---|---|---|
| F01 | High | Security | ClaudeSheet_CS | Roles | No admin or manager role: SheetUser is the only application role | `eSpace.Roles` = SheetUser; every public action checks only `CheckSheetUserRole()` | Add public roles SheetAdmin and SheetManager; new actions check them |
| F02 | High | Gaps | ClaudeSheet_CS | Logic › Server Actions (no Client_*) | Clients can only be created by the Bootstrap seed | Client (Name, IsActive, UQ_Client_Name) is read-only public; only write is Bootstrap "CreateClient NewClient" | Client_GetList, Client_GetById, Client_CreateOrUpdate (unique name, user exception), Client_SetActive; SheetAdmin check |
| F03 | High | Gaps | ClaudeSheet_CS | Logic › Server Actions (no Project_* writes) | Projects can only be created by the Bootstrap seed | Project (ClientId, Name, Code, IsActive, UQ_Project_ClientCode) read-only; only Project action is Project_GetAssignedToUser | Project_GetList, Project_GetById, Project_CreateOrUpdate (unique client+code, active client), Project_SetActive |
| F04 | High | Gaps | ClaudeSheet_CS | Logic › Server Actions (no ProjectAssignment_*) | Users can't be assigned to projects; overlapping periods aren't prevented | ProjectAssignment only written by Bootstrap for Site.SeedTestUsernames; ADR 0002 requires overlap rejection; DaySheet_Save rejects unassigned projects | ProjectAssignment_GetList, _CreateOrUpdate (dates, overlap, user exception), _End, _Delete (no entries) |
| F05 | High | Gaps | ClaudeSheet_CS | Bootstrap / Holiday_CreateIfMissing | Holidays exist for 2026 only and can't be managed | 13 calls with NewDate(2026, …); Holiday_CreateIfMissing private, no role check | Holiday_GetList (year), Holiday_CreateOrUpdate, Holiday_SetActive, optional Holiday_CopyYear |
| F06 | High | Gaps | ClaudeSheet_CS | Bootstrap › GrantSheetUserRole | Users get SheetUser only via the seed; no user list or role management | GrantSheetUserRole called only in Bootstrap for SeedTestUsernames | User_GetList, User_SetRoles (grant/revoke SheetUser, SheetAdmin, SheetManager) behind SheetAdmin |
| F07 | High | Gaps | ClaudeSheet | Interface › UI Flows | No admin / back-office UI flow | Flows: SheetBlocks, Layouts, Common, MainFlow; 3 screens; Client/Project/ProjectAssignment/Holiday unused in the UI | Add an Admin UI flow gated by SheetAdmin |
| F08 | High | Gaps | ClaudeSheet | Module (no Projects / ProjectDetail) | No screen to manage projects or assign them to users (see F03, F04) | A user without assignments sees "No projects assigned for this week" and can't log hours; nobody can fix it in the app | Projects list + ProjectDetail with "Assigned users" table |
| F09 | High | Gaps | ClaudeSheet | Module (no Clients / ClientDetail) | No screen to manage clients (see F02) | Client referenced but unused in the UI | Clients list + ClientDetail |
| F10 | High | Gaps | ClaudeSheet | Module (no Holidays) | No screen to manage holidays (see F05) | Holiday referenced but unused; from 2027 no holidays | Holidays screen by year |
| F11 | High | Gaps | ClaudeSheet | Module (no Users / UserDetail) | No user or access administration (see F06) | Users without SheetUser land on InvalidPermissions with no explanation | Users + UserDetail (roles, assignments); clearer InvalidPermissions text |
| F12 | High | Security | ClaudeSheet | Screens › Roles | Admin and manager screens can't be secured: only SheetUser exists (see F01) | MySheet [SheetUser]; Login/InvalidPermissions [SheetUser, Anonymous, Registered] | Use SheetAdmin / SheetManager on the new screens |
| F13 | Medium | Gaps | ClaudeSheet_CS | DaySheet_Save, DaySheet_Clear; DayStatus | No submit/approve workflow; confirmed days stay editable and deletable | No check of existing DayStatus or a closed period; DaySheet_Clear deletes any status; no period entity | MonthSheet entity + MonthSheet_Submit/Approve/Reject/Reopen; lock edits |
| F14 | Medium | Gaps | ClaudeSheet_CS | Logic › Server Actions (no Report_*) | No reporting: every read is current-user only; no totals per project/client/user, no export | All actions filter on GetUserId(); none aggregates across users or exports | Report_GetHoursByProject, Report_GetHoursByUser, Report_ExportCsv, Calendar_GetMonthForUser |
| F15 | Medium | Gaps | ClaudeSheet | Common › Menu | Menu has one link and no role-based entries | Links = [MySheetLink] | Links per new screen with Visible = Check<Role>Role() |
| F16 | Medium | Gaps | ClaudeSheet | Module (no TeamSheets / UserSheet) | Managers can't see other users' sheets | All UI actions are current-user; no screen takes a UserId | TeamSheets + read-only UserSheet reusing the blocks |
| F17 | Medium | Gaps | ClaudeSheet | SheetBlocks › HoursCalendar; no Approvals screen | No "Submit month" button and no approval screen (see F13) | Calendar buttons: SaveDraft, ConfirmDay, SaveDayOff, ClearDay, FillPending | Submit month on MySheet, Approvals screen, IsLocked in the calendar |
| F18 | Medium | Gaps | ClaudeSheet | Module (no reports) | No reports or export screen (see F14) | Only current-user month and week views | ProjectHoursReport with filters and Export to Excel |
| F19 | Medium | Gaps | ClaudeSheet | HoursCalendar › ProjectLines | Day editor has no empty state when no project is assigned | No If on EditorLines empty; Save/Confirm shown and then fail on the server | If HasProjectLines with a message; hide Save/Confirm |
| F20 | Medium | Correctness | ClaudeSheet | HoursCalendar › OnClick_FillPendingButton | "Fill pending" offers today's projects but fills every pending day of the shown month | Project_GetAssignedToUser(ForDate = CurrDate()); fill rolls back on any unassigned day | Load projects assigned in the shown month; specific empty message |
| F21 | Low | Gaps | ClaudeSheet_CS | Site Properties › DailyExpectedMinutes | Expected hours are one global value; no admin change, no part-time | Settings_GetDailyExpectedMinutes read-only; used for every user | UserSetting entity with fallback; admin can change the default |
| F22 | Low | Gaps | ClaudeSheet_CS | Entities Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry | No audit fields on master data; no UpdatedBy on sheets | Master data has no CreatedOn/By, UpdatedOn/By | Add audit attributes, set them in the *_CreateOrUpdate actions |
| F23 | Low | Correctness | ClaudeSheet_CS | ProjectAssignment.ProjectId | Deleting a project silently deletes its assignments, against Protect-by-default | DeleteRule = Delete | Change to Protect; deactivate projects instead of deleting |
| F24 | Low | Gaps | ClaudeSheet_CS | Processes › Timers | No reminder for pending days | Only Timer_Bootstrap | Timer_SendPendingReminders + email |
| F25 | Low | Gaps | ClaudeSheet | HoursCalendar (EditorNotes) | Day notes are loaded and saved but can't be edited | EditorNotes passed to DaySheet_Save; no input bound to it | TextArea NotesInput bound to EditorNotes |
| F26 | Low | Gaps | ClaudeSheet | Module (no Settings) | No settings screen (see F21) | Site properties only changeable in Service Center | Settings screen for SheetAdmin |

### Not checked

- **ClaudeSheet_CS:** runtime data (whether SeedTestUsernames is set, who has SheetUser); structure field details; Service Center user provisioning (LDAP/SSO). Validation: one warning, Minutes_Format "Unused user action" (server copy, not used in CS), not reported as a finding.
- **ClaudeSheet:** the redirect target after login and password recovery; handler names of screen-level actions (the probe found none); theme, CSS, responsiveness and performance (outside the lenses).

## Proposed stories

Suggested order: TT5 first (every other story needs its roles), then US6, US7, US8, US9 (an admin can run the app), US10, then US11, US12, US13 (manager side), then US14 and TT6. Each plan is grouped per module, producer first: publish ClaudeSheet_CS and tick the new elements in ClaudeSheet › Manage Dependencies before the ClaudeSheet part.

| ID | Title | Priority | Size | Modules | Covers | Decision | Trello |
|---|---|---|---|---|---|---|---|
| TT5 | [Tech] Admin and manager roles, Admin UI flow, role-gated menu | High | S | CS, ClaudeSheet | F01, F07, F12, F15 | Approved | https://trello.com/c/kzWSgkuL |
| US6 | [Screen] Admin: manage clients | High | M | CS, ClaudeSheet | F02, F09 | Approved | https://trello.com/c/LNT15Ag2 |
| US7 | [Screen] Admin: manage projects and assign them to users | High | L | CS, ClaudeSheet | F03, F04, F08, F23 | Approved | https://trello.com/c/qfGFuwQ0 |
| US8 | [Screen] Admin: manage holidays per year | High | M | CS, ClaudeSheet | F05, F10 | Approved | https://trello.com/c/PIuqB7g0 |
| US9 | [Screen] Admin: manage users and their access | High | M | CS, ClaudeSheet | F06, F11 | Approved | https://trello.com/c/F12gaDuW |
| US10 | [Block] HoursCalendar: no-project state, fill pending for the shown month, day notes | Medium | M | CS, ClaudeSheet | F19, F20, F25 | Approved | https://trello.com/c/2RBtjW9O |
| US11 | [Screen] Submit, approve and lock monthly timesheets | Medium | L | CS, ClaudeSheet | F13, F17 | Approved | https://trello.com/c/LrYU7ORJ |
| US12 | [Screen] Manager: team overview and read-only user sheet | Medium | L | CS, ClaudeSheet | F16 | Approved | https://trello.com/c/3R2nkRhR |
| US13 | [Screen] Reports: hours per project, client and user with export | Medium | M | CS, ClaudeSheet | F14, F18 | Approved | https://trello.com/c/g3PTAvGl |
| US14 | [Screen] Admin: settings and per-user expected hours | Low | M | CS, ClaudeSheet | F21, F26 | Approved | https://trello.com/c/Ee94MYym |
| TT6 | [Tech] Audit fields on master data and day sheets | Low | S | CS | F22 | Approved | https://trello.com/c/tnEEFpke |

### TT5 · [Tech] Admin and manager roles, Admin UI flow, role-gated menu

**Description.** The app has a single role (SheetUser), so nothing can protect master-data management or a manager view, and the UI has no place for admin screens. This task adds the roles and that place, so every following story only adds its own actions, screens and menu link. Covers: F01, F07, F12, F15.

**Implementation plan**
- ClaudeSheet_CS
  1. Create public roles SheetAdmin ("Manages clients, projects, assignments, holidays, users and settings") and SheetManager ("Sees and approves the team's sheets, runs reports").
  2. Add site property SeedAdminUsernames (Text, default empty). In Bootstrap, grant SheetAdmin and SheetUser to each username in it (same pattern as SeedTestUsernames), so a first admin exists before US9.
- ClaudeSheet (dependencies: roles SheetAdmin, SheetManager)
  3. Create UI flow Admin (screens are added by US6–US9 and US14).
  4. Menu: role-gated link pattern (link Visible = Check<Role>Role()), documented in docs/conventions.md; no new links yet.

**Acceptance criteria**
- AC-TT5.1 Given ClaudeSheet_CS, Then roles SheetAdmin and SheetManager exist, are public and have descriptions.
- AC-TT5.2 Given SeedAdminUsernames = "admin.user" and the module is published, Then admin.user has SheetAdmin and SheetUser.
- AC-TT5.3 Given ClaudeSheet, Then the UI flow Admin exists and both roles are referenced without a "2" suffix.
- AC-TT5.4 Given Service Studio, Then both modules validate with 0 errors and 0 warnings.

### US6 · [Screen] Admin: manage clients

**Description.** As an admin I want to create, rename and deactivate clients, so that projects sit under the right client without touching the database. Today clients only come from the Bootstrap seed. Covers: F02, F09.

**Implementation plan**
- ClaudeSheet_CS
  1. Public user exception ClientValidation.
  2. Public server actions, each starting with the SheetAdmin check: Client_GetList (IncludeInactive; with project count), Client_GetById, Client_CreateOrUpdate (Name required and unique, raises ClientValidation), Client_SetActive (deactivate instead of delete).
- ClaudeSheet (dependencies: the 4 actions, ClientValidation, SheetAdmin)
  3. Screen Admin › Clients (SheetAdmin): table Name, Projects, Active; search; "Show inactive"; "New client".
  4. Screen Admin › ClientDetail (ClientId input): Name, Active, read-only list of the client's projects; Save catches ClientValidation and shows its message.
  5. Menu link "Clients", visible to SheetAdmin.

**Acceptance criteria**
- AC6.1 Given a SheetAdmin user, When they open Clients, Then all active clients are listed with their project count.
- AC6.2 Given a new client "Acme" is saved, Then it is listed; saving a second "Acme" shows "A client with this name already exists".
- AC6.3 Given a client is deactivated, Then its projects no longer appear in users' day editors.
- AC6.4 Given a SheetUser without SheetAdmin, When they open Clients or call Client_CreateOrUpdate, Then they go to InvalidPermissions / the action raises the role exception.
- AC6.5 Given Service Studio, Then 0 errors and 0 warnings.

### US7 · [Screen] Admin: manage projects and assign them to users

**Description.** As an admin I want to manage projects and assign consultants to them for a period, so that every consultant sees the right projects in MySheet and can log hours. Today a user without an assignment can't log any hours, and nobody can fix that in the app. Covers: F03, F04, F08, F23.

**Implementation plan**
- ClaudeSheet_CS
  1. Public user exceptions ProjectValidation and AssignmentValidation.
  2. Public server actions (SheetAdmin check): Project_GetList (ClientId, IncludeInactive), Project_GetById, Project_CreateOrUpdate (Name and Code required, Code unique per client, client active), Project_SetActive.
  3. Public server actions (SheetAdmin check): ProjectAssignment_GetList (ProjectId or UserId), ProjectAssignment_CreateOrUpdate (StartDate <= EndDate or open end; project and client active; no overlap with another active period of the same user and project, per ADR 0002), ProjectAssignment_End (sets EndDate), ProjectAssignment_Delete (only when the user has no TimeEntry on the project in the period).
  4. Change the ProjectAssignment.ProjectId delete rule from Delete to Protect.
- ClaudeSheet (dependencies: the 8 actions, the 2 exceptions, SheetAdmin)
  5. Screen Admin › Projects (SheetAdmin): table Client, Code, Name, Assigned users, Active; filter by client; "New project".
  6. Screen Admin › ProjectDetail (ProjectId input): Client dropdown, Code, Name, Active; section "Assigned users" (user, start, end, status) with Add, Edit, End and Delete (popup with a user dropdown and dates); validation messages from the exceptions.
  7. Menu link "Projects", visible to SheetAdmin.

**Acceptance criteria**
- AC7.1 Given a SheetAdmin creates project "Portal | PTL" under ICNF, Then it is listed; a second "PTL" under ICNF is rejected with a clear message.
- AC7.2 Given user A is assigned to Portal from 01/11/2026 with no end, When A opens MySheet on 02/11, Then Portal appears in the day editor and the weekly table.
- AC7.3 Given A is assigned to Portal 01/11–30/11, When an admin adds 15/11–31/12 for A on Portal, Then it is rejected as overlapping.
- AC7.4 Given A has entries on Portal, When the admin deletes the assignment, Then it is rejected and "End" is offered instead.
- AC7.5 Given a project with assignments, Then it can't be deleted by accident (delete rule Protect) and can be deactivated.
- AC7.6 Given a SheetUser without SheetAdmin, Then Projects is not in the menu and opening it goes to InvalidPermissions.
- AC7.7 Given Service Studio, Then 0 errors and 0 warnings in both modules.

### US8 · [Screen] Admin: manage holidays per year

**Description.** As an admin I want to maintain the holiday calendar per year, so that the calendar, KPIs and expected hours stay right after 2026. Today only the 2026 Portuguese holidays exist, from the Bootstrap seed. Covers: F05, F10.

**Implementation plan**
- ClaudeSheet_CS
  1. Public user exception HolidayValidation.
  2. Public server actions (SheetAdmin check): Holiday_GetList (Year, IncludeInactive), Holiday_CreateOrUpdate (date unique, name required), Holiday_SetActive, Holiday_CopyYear (FromYear, ToYear; copies fixed-date holidays, skips existing dates, returns the count).
- ClaudeSheet (dependencies: the 4 actions, HolidayValidation, SheetAdmin)
  3. Screen Admin › Holidays (SheetAdmin): year selector; table Date, Weekday, Name, Active; add and edit in a popup; deactivate; "Copy fixed holidays to next year".
  4. Menu link "Holidays", visible to SheetAdmin.

**Acceptance criteria**
- AC8.1 Given year 2026, Then the 13 seeded holidays are listed.
- AC8.2 Given "Copy fixed holidays to 2027", Then 2027 gets the fixed-date ones; running it again adds nothing.
- AC8.3 Given a holiday is added on 08/12/2027, Then MySheet for December 2027 shows it as a holiday and expected hours drop by one day.
- AC8.4 Given two holidays on the same date, Then the second save is rejected with a clear message.
- AC8.5 Given a SheetUser without SheetAdmin, Then the screen and actions are refused; 0 errors and 0 warnings.

### US9 · [Screen] Admin: manage users and their access

**Description.** As an admin I want to see the app's users and grant or revoke access (SheetUser, SheetAdmin, SheetManager), so that new consultants can start without a database or Service Center change. Today access comes only from the SeedTestUsernames site property, and users without the role get a bare InvalidPermissions page. Covers: F06, F11.

**Implementation plan**
- ClaudeSheet_CS
  1. Structure UserAccess (UserId, Name, Username, Email, IsActive, IsSheetUser, IsSheetAdmin, IsSheetManager, ActiveAssignments).
  2. Public server actions (SheetAdmin check): User_GetList (Search, OnlyWithAccess) returning UserAccess; User_SetRoles (UserId, IsSheetUser, IsSheetAdmin, IsSheetManager; Grant/Revoke<Role>Role; an admin can't remove their own SheetAdmin).
- ClaudeSheet (dependencies: UserAccess, the 2 actions, SheetAdmin; the ProjectAssignment actions from US7)
  3. Screen Admin › Users (SheetAdmin): search; table Name, Username, roles as tags, active assignments.
  4. Screen Admin › UserDetail (UserId input): role toggles with Save; the user's assignments, reusing the US7 assignment popup.
  5. InvalidPermissions: text "You don't have access to ClaudeSheet yet. Ask an administrator to give you access."
  6. Menu link "Users", visible to SheetAdmin.

**Acceptance criteria**
- AC9.1 Given a registered user B without roles, When an admin ticks SheetUser for B and saves, Then B can open MySheet after the next login.
- AC9.2 Given an admin revokes SheetUser from B, Then B goes to InvalidPermissions with the new text.
- AC9.3 Given an admin edits their own user, Then SheetAdmin can't be removed.
- AC9.4 Given UserDetail for A, Then A's assignments are listed and can be added or ended.
- AC9.5 Given a SheetUser without SheetAdmin, Then screens and actions are refused; 0 errors and 0 warnings.

### US10 · [Block] HoursCalendar: no-project state, fill pending for the shown month, day notes

**Description.** As a consultant I want the day editor and "Fill all pending days" to work for the month I'm looking at, so that I'm not offered actions that then fail. Today the editor shows Save and Confirm with no project lines, "Fill pending" lists only today's projects and rolls back the whole month when one day isn't covered, and day notes can't be edited. Covers: F19, F20, F25.

**Implementation plan**
- ClaudeSheet_CS
  1. Public server action Project_GetAssignedInPeriod (StartDate, EndDate): active projects the current user is assigned to at any point in the period (SheetUser check).
  2. DaySheet_FillPending: skip pending days on which the chosen project isn't assigned instead of failing; return FilledCount and SkippedCount.
- ClaudeSheet (dependencies: Project_GetAssignedInPeriod; refreshed DaySheet_FillPending)
  3. HoursCalendar day editor: If HasProjectLines (EditorLines.Length > 0); when false, show "No projects are assigned to you on this day. Ask your administrator." and hide Save draft and Confirm day.
  4. OnClick_FillPendingButton: load the shown month's projects with Project_GetAssignedInPeriod; when there are none, show "No projects assigned this month" instead of the dropdown; after the fill, report filled and skipped days.
  5. Day editor: TextArea NotesInput bound to EditorNotes (max 500), labelled "Notes".

**Acceptance criteria**
- AC10.1 Given a day without assigned projects, When the editor opens, Then the message is shown and Save/Confirm are hidden.
- AC10.2 Given an assignment that starts on the 15th and pending days before and after it, When the user fills pending with that project, Then days from the 15th are filled, earlier days are skipped, and the message gives both counts.
- AC10.3 Given the user browses last month, Then Fill pending offers last month's projects.
- AC10.4 Given notes are typed and the day is saved, When the day is reopened, Then the notes are shown.
- AC10.5 Given Service Studio, Then 0 errors and 0 warnings.

### US11 · [Screen] Submit, approve and lock monthly timesheets

**Description.** As a consultant I want to submit my month, and as a manager I want to approve or reject it, so that approved hours can't change afterwards. Today confirmed days can still be edited or cleared, and there is no submission or approval. Covers: F13, F17.

**Implementation plan**
- ClaudeSheet_CS
  1. Static entity PeriodStatus (Open, Submitted, Approved, Rejected) and entity MonthSheet (UserId, Year, Month, PeriodStatusId, SubmittedOn, DecidedBy, DecidedOn, Comment; unique index UserId+Year+Month; public, read-only).
  2. Public server actions: MonthSheet_GetForUser (Year, Month; current user), MonthSheet_Submit (SheetUser; only when the month has no Pending days), MonthSheet_GetSubmitted (SheetManager), MonthSheet_Approve, MonthSheet_Reject (Comment required), MonthSheet_Reopen (SheetManager). User exception PeriodValidation.
  3. DaySheet_Save, DaySheet_Clear and DaySheet_FillPending raise DaySheetValidation ("This month is submitted or approved") when the month is Submitted or Approved.
  4. Calendar_GetMonth: add IsLocked to the result.
- ClaudeSheet (dependencies: PeriodStatus, MonthSheet, the 6 actions, PeriodValidation, SheetManager)
  5. MySheet / HoursCalendar: month status badge and "Submit month" button; locked days open read-only.
  6. Screen MainFlow › Approvals (SheetManager): submitted months (user, month, total vs expected) with Approve, Reject with comment, and Reopen.
  7. Menu link "Approvals", visible to SheetManager.

**Acceptance criteria**
- AC11.1 Given a month with no Pending days, When the user submits it, Then the badge shows Submitted and its days can't be edited or cleared.
- AC11.2 Given a month with Pending days, Then Submit is refused with a message giving how many are pending.
- AC11.3 Given a manager approves it, Then it stays locked; When they reject it with a comment, Then it reopens and the user sees the comment.
- AC11.4 Given a user calls DaySheet_Save on an approved month directly, Then the server refuses it.
- AC11.5 Given a SheetUser without SheetManager, Then Approvals is refused; 0 errors and 0 warnings.

### US12 · [Screen] Manager: team overview and read-only user sheet

**Description.** As a manager I want to see every consultant's month at a glance and open anyone's sheet read-only, so that I can follow up missing hours before approving. Today every action and screen works on the current user only. Covers: F16.

**Implementation plan**
- ClaudeSheet_CS
  1. Structure UserMonthStatus (UserId, Name, TotalMinutes, ExpectedMinutes, PendingCount, PeriodStatusId).
  2. Public server actions (SheetManager check): Team_GetMonthStatus (Year, Month) returning a list of UserMonthStatus for users with SheetUser; Calendar_GetMonthForUser, Calendar_GetMonthSummaryForUser and ProjectHours_GetWeeklyForUser (UserId plus the existing inputs), sharing the current-user logic.
- ClaudeSheet (dependencies: UserMonthStatus, the 4 actions, SheetManager)
  3. Screen MainFlow › TeamSheets (SheetManager): month selector; table Name, Total vs expected, Pending, Status; a row opens UserSheet.
  4. Screen MainFlow › UserSheet (UserId, Year, Month inputs; SheetManager): the MySheet blocks read-only. Add optional inputs UserId and IsReadOnly to TotalHoursMonth, HoursCalendar, DayStatusKPIs and WeeklyProjectHours; with UserId set they call the *ForUser actions and hide every edit control.
  5. Menu link "Team", visible to SheetManager.

**Acceptance criteria**
- AC12.1 Given October 2026, Then TeamSheets lists every SheetUser with total, expected, pending count and status.
- AC12.2 Given a click on user A, Then UserSheet shows A's calendar, KPIs, total and weekly table with no edit controls.
- AC12.3 Given MySheet, Then the blocks behave exactly as before (UserId empty).
- AC12.4 Given a SheetUser without SheetManager calls Calendar_GetMonthForUser for another user, Then it is refused.
- AC12.5 Given Service Studio, Then 0 errors and 0 warnings.

### US13 · [Screen] Reports: hours per project, client and user with export

**Description.** As a manager I want hours per project, client and consultant over a period, exportable for Excel, so that I can bill clients and report effort. Today nothing aggregates beyond the user's own month and week. Covers: F14, F18.

**Implementation plan**
- ClaudeSheet_CS
  1. Structure ProjectHoursRow (ClientName, ProjectCode, ProjectName, UserName, TotalMinutes).
  2. Public server actions (SheetManager or SheetAdmin check): Report_GetProjectHours (StartDate, EndDate, ClientId, ProjectId, UserId, GroupBy) returning a list of ProjectHoursRow; Report_ExportProjectHours (same inputs) returning a CSV as Binary Data (Excel opens it; a Record List To Excel output can't be referenced through the Model API).
- ClaudeSheet (dependencies: ProjectHoursRow, the 2 actions, SheetManager, SheetAdmin)
  3. Screen MainFlow › ProjectHoursReport (SheetManager, SheetAdmin): filters date range (default current month), client, project, user and group by; table with totals; "Export" downloads the CSV.
  4. Menu link "Reports", visible to SheetManager or SheetAdmin.

**Acceptance criteria**
- AC13.1 Given October 2026 grouped by project, Then each project's total equals the sum of its TimeEntries in the period.
- AC13.2 Given a client filter, Then only that client's projects appear and the grand total matches.
- AC13.3 Given Export, Then a CSV with a header and the same rows opens in Excel.
- AC13.4 Given a SheetUser without SheetManager or SheetAdmin, Then the screen and actions are refused; 0 errors and 0 warnings.

### US14 · [Screen] Admin: settings and per-user expected hours

**Description.** As an admin I want to change the default expected hours per day and set different hours for part-time consultants, so that totals and progress are right for everyone. Today there is one global site property that only Service Center can change. Covers: F21, F26.

**Implementation plan**
- ClaudeSheet_CS
  1. Entity UserSetting (UserId unique, DailyExpectedMinutes, IsActive; public, read-only).
  2. Public server actions: UserSetting_GetDailyExpectedMinutes (UserId; falls back to the site property), UserSetting_CreateOrUpdate (SheetAdmin), Settings_SetDailyExpectedMinutes (SheetAdmin; sets the site property).
  3. Calendar_GetMonthSummary (and its *ForUser variant from US12, if built) uses UserSetting_GetDailyExpectedMinutes.
- ClaudeSheet (dependencies: UserSetting, the 3 actions, SheetAdmin)
  4. Screen Admin › Settings (SheetAdmin): default expected hours per day (H:MM, multiple of 15 minutes).
  5. UserDetail (US9): "Expected hours per day" field; empty means the default.
  6. Menu link "Settings", visible to SheetAdmin.

**Acceptance criteria**
- AC14.1 Given the default changes from 8:00 to 7:30, Then MySheet's expected hours use 7:30 for users without their own setting.
- AC14.2 Given user A is set to 4:00, Then A's progress uses 4:00 per working day and other users are unchanged.
- AC14.3 Given a value that isn't a multiple of 15 minutes, Then it is rejected.
- AC14.4 Given a SheetUser without SheetAdmin, Then Settings is refused; 0 errors and 0 warnings.

### TT6 · [Tech] Audit fields on master data and day sheets

**Description.** Once admins and managers edit data, we need to know who changed what and when. Client, Project, ProjectAssignment and Holiday have no audit fields, and DaySheet and TimeEntry have no UpdatedBy. Covers: F22.

**Implementation plan**
- ClaudeSheet_CS
  1. Add CreatedOn, CreatedBy (User Identifier), UpdatedOn and UpdatedBy to Client, Project, ProjectAssignment and Holiday; add UpdatedBy to DaySheet and TimeEntry (delete rule Protect on the User references).
  2. Set them in Bootstrap and in every *_CreateOrUpdate or *_Save action that exists at the time.
  3. docs/conventions.md: "Master-data entities carry CreatedOn/By and UpdatedOn/By, set by their Core actions."

**Acceptance criteria**
- AC-TT6.1 Given the six entities, Then they have the audit attributes with the listed types.
- AC-TT6.2 Given an admin edits a project, Then UpdatedOn and UpdatedBy reflect the edit.
- AC-TT6.3 Given Service Studio, Then 0 errors and 0 warnings.

## Not proposed

| Finding | Reason |
|---|---|
| F24 No reminder for pending days | Needs a product decision first (email or in-app, SMTP set up on the environment). Propose again once email sending is configured. |

## Decisions — 2026-10-19

All 11 proposed stories approved; none rejected. Registered on Trello (Backlog) at the user's request, links in the table above.

## Approved prompt

````text
Implement the following approved stories one at a time with outsystems-mcp-implementation, in this order.
Each story's description, implementation plan and acceptance criteria are below: use them as the story definition instead of asking for them.
Source: investigations/2026-10-16-full-app-gaps.md (Trello cards in Backlog)

## TT5 · [Tech] Admin and manager roles, Admin UI flow, role-gated menu
Trello: https://trello.com/c/kzWSgkuL
### Description
The app has a single role (SheetUser), so nothing can protect master-data management or a manager view, and the UI has no place for admin screens. This task adds the roles and that place, so every following story only adds its own actions, screens and menu link. Covers: F01, F07, F12, F15.
### Implementation plan
#### ClaudeSheet_CS
1. Create public roles SheetAdmin ("Manages clients, projects, assignments, holidays, users and settings") and SheetManager ("Sees and approves the team's sheets, runs reports").
2. Add site property SeedAdminUsernames (Text, default empty). In Bootstrap, grant SheetAdmin and SheetUser to each username in it (same pattern as SeedTestUsernames), so a first admin exists before US9.
#### ClaudeSheet
Dependencies: roles SheetAdmin, SheetManager
3. Create UI flow Admin (screens are added by US6–US9 and US14).
4. Menu: role-gated link pattern (link Visible = Check<Role>Role()), documented in docs/conventions.md; no new links yet.
### Acceptance criteria
- AC-TT5.1 Given ClaudeSheet_CS, Then roles SheetAdmin and SheetManager exist, are public and have descriptions.
- AC-TT5.2 Given SeedAdminUsernames = "admin.user" and the module is published, Then admin.user has SheetAdmin and SheetUser.
- AC-TT5.3 Given ClaudeSheet, Then the UI flow Admin exists and both roles are referenced without a "2" suffix.
- AC-TT5.4 Given Service Studio, Then both modules validate with 0 errors and 0 warnings.

## US6 · [Screen] Admin: manage clients
Trello: https://trello.com/c/LNT15Ag2
### Description
As an admin I want to create, rename and deactivate clients, so that projects sit under the right client without touching the database. Today clients only come from the Bootstrap seed. Covers: F02, F09.
### Implementation plan
#### ClaudeSheet_CS
1. Public user exception ClientValidation.
2. Public server actions, each starting with the SheetAdmin check: Client_GetList (IncludeInactive; with project count), Client_GetById, Client_CreateOrUpdate (Name required and unique, raises ClientValidation), Client_SetActive (deactivate instead of delete).
#### ClaudeSheet
Dependencies: the 4 actions, ClientValidation, SheetAdmin
3. Screen Admin › Clients (SheetAdmin): table Name, Projects, Active; search; "Show inactive"; "New client".
4. Screen Admin › ClientDetail (ClientId input): Name, Active, read-only list of the client's projects; Save catches ClientValidation and shows its message.
5. Menu link "Clients", visible to SheetAdmin.
### Acceptance criteria
- AC6.1 Given a SheetAdmin user, When they open Clients, Then all active clients are listed with their project count.
- AC6.2 Given a new client "Acme" is saved, Then it is listed; saving a second "Acme" shows "A client with this name already exists".
- AC6.3 Given a client is deactivated, Then its projects no longer appear in users' day editors.
- AC6.4 Given a SheetUser without SheetAdmin, When they open Clients or call Client_CreateOrUpdate, Then they go to InvalidPermissions / the action raises the role exception.
- AC6.5 Given Service Studio, Then 0 errors and 0 warnings.

## US7 · [Screen] Admin: manage projects and assign them to users
Trello: https://trello.com/c/qfGFuwQ0
### Description
As an admin I want to manage projects and assign consultants to them for a period, so that every consultant sees the right projects in MySheet and can log hours. Today a user without an assignment can't log any hours, and nobody can fix that in the app. Covers: F03, F04, F08, F23.
### Implementation plan
#### ClaudeSheet_CS
1. Public user exceptions ProjectValidation and AssignmentValidation.
2. Public server actions (SheetAdmin check): Project_GetList (ClientId, IncludeInactive), Project_GetById, Project_CreateOrUpdate (Name and Code required, Code unique per client, client active), Project_SetActive.
3. Public server actions (SheetAdmin check): ProjectAssignment_GetList (ProjectId or UserId), ProjectAssignment_CreateOrUpdate (StartDate <= EndDate or open end; project and client active; no overlap with another active period of the same user and project, per ADR 0002), ProjectAssignment_End (sets EndDate), ProjectAssignment_Delete (only when the user has no TimeEntry on the project in the period).
4. Change the ProjectAssignment.ProjectId delete rule from Delete to Protect.
#### ClaudeSheet
Dependencies: the 8 actions, the 2 exceptions, SheetAdmin
5. Screen Admin › Projects (SheetAdmin): table Client, Code, Name, Assigned users, Active; filter by client; "New project".
6. Screen Admin › ProjectDetail (ProjectId input): Client dropdown, Code, Name, Active; section "Assigned users" (user, start, end, status) with Add, Edit, End and Delete (popup with a user dropdown and dates); validation messages from the exceptions.
7. Menu link "Projects", visible to SheetAdmin.
### Acceptance criteria
- AC7.1 Given a SheetAdmin creates project "Portal | PTL" under ICNF, Then it is listed; a second "PTL" under ICNF is rejected with a clear message.
- AC7.2 Given user A is assigned to Portal from 01/11/2026 with no end, When A opens MySheet on 02/11, Then Portal appears in the day editor and the weekly table.
- AC7.3 Given A is assigned to Portal 01/11–30/11, When an admin adds 15/11–31/12 for A on Portal, Then it is rejected as overlapping.
- AC7.4 Given A has entries on Portal, When the admin deletes the assignment, Then it is rejected and "End" is offered instead.
- AC7.5 Given a project with assignments, Then it can't be deleted by accident (delete rule Protect) and can be deactivated.
- AC7.6 Given a SheetUser without SheetAdmin, Then Projects is not in the menu and opening it goes to InvalidPermissions.
- AC7.7 Given Service Studio, Then 0 errors and 0 warnings in both modules.

## US8 · [Screen] Admin: manage holidays per year
Trello: https://trello.com/c/PIuqB7g0
### Description
As an admin I want to maintain the holiday calendar per year, so that the calendar, KPIs and expected hours stay right after 2026. Today only the 2026 Portuguese holidays exist, from the Bootstrap seed. Covers: F05, F10.
### Implementation plan
#### ClaudeSheet_CS
1. Public user exception HolidayValidation.
2. Public server actions (SheetAdmin check): Holiday_GetList (Year, IncludeInactive), Holiday_CreateOrUpdate (date unique, name required), Holiday_SetActive, Holiday_CopyYear (FromYear, ToYear; copies fixed-date holidays, skips existing dates, returns the count).
#### ClaudeSheet
Dependencies: the 4 actions, HolidayValidation, SheetAdmin
3. Screen Admin › Holidays (SheetAdmin): year selector; table Date, Weekday, Name, Active; add and edit in a popup; deactivate; "Copy fixed holidays to next year".
4. Menu link "Holidays", visible to SheetAdmin.
### Acceptance criteria
- AC8.1 Given year 2026, Then the 13 seeded holidays are listed.
- AC8.2 Given "Copy fixed holidays to 2027", Then 2027 gets the fixed-date ones; running it again adds nothing.
- AC8.3 Given a holiday is added on 08/12/2027, Then MySheet for December 2027 shows it as a holiday and expected hours drop by one day.
- AC8.4 Given two holidays on the same date, Then the second save is rejected with a clear message.
- AC8.5 Given a SheetUser without SheetAdmin, Then the screen and actions are refused; 0 errors and 0 warnings.

## US9 · [Screen] Admin: manage users and their access
Trello: https://trello.com/c/F12gaDuW
### Description
As an admin I want to see the app's users and grant or revoke access (SheetUser, SheetAdmin, SheetManager), so that new consultants can start without a database or Service Center change. Today access comes only from the SeedTestUsernames site property, and users without the role get a bare InvalidPermissions page. Covers: F06, F11.
### Implementation plan
#### ClaudeSheet_CS
1. Structure UserAccess (UserId, Name, Username, Email, IsActive, IsSheetUser, IsSheetAdmin, IsSheetManager, ActiveAssignments).
2. Public server actions (SheetAdmin check): User_GetList (Search, OnlyWithAccess) returning UserAccess; User_SetRoles (UserId, IsSheetUser, IsSheetAdmin, IsSheetManager; Grant/Revoke<Role>Role; an admin can't remove their own SheetAdmin).
#### ClaudeSheet
Dependencies: UserAccess, the 2 actions, SheetAdmin; the ProjectAssignment actions from US7
3. Screen Admin › Users (SheetAdmin): search; table Name, Username, roles as tags, active assignments.
4. Screen Admin › UserDetail (UserId input): role toggles with Save; the user's assignments, reusing the US7 assignment popup.
5. InvalidPermissions: text "You don't have access to ClaudeSheet yet. Ask an administrator to give you access."
6. Menu link "Users", visible to SheetAdmin.
### Acceptance criteria
- AC9.1 Given a registered user B without roles, When an admin ticks SheetUser for B and saves, Then B can open MySheet after the next login.
- AC9.2 Given an admin revokes SheetUser from B, Then B goes to InvalidPermissions with the new text.
- AC9.3 Given an admin edits their own user, Then SheetAdmin can't be removed.
- AC9.4 Given UserDetail for A, Then A's assignments are listed and can be added or ended.
- AC9.5 Given a SheetUser without SheetAdmin, Then screens and actions are refused; 0 errors and 0 warnings.

## US10 · [Block] HoursCalendar: no-project state, fill pending for the shown month, day notes
Trello: https://trello.com/c/2RBtjW9O
### Description
As a consultant I want the day editor and "Fill all pending days" to work for the month I'm looking at, so that I'm not offered actions that then fail. Today the editor shows Save and Confirm with no project lines, "Fill pending" lists only today's projects and rolls back the whole month when one day isn't covered, and day notes can't be edited. Covers: F19, F20, F25.
### Implementation plan
#### ClaudeSheet_CS
1. Public server action Project_GetAssignedInPeriod (StartDate, EndDate): active projects the current user is assigned to at any point in the period (SheetUser check).
2. DaySheet_FillPending: skip pending days on which the chosen project isn't assigned instead of failing; return FilledCount and SkippedCount.
#### ClaudeSheet
Dependencies: Project_GetAssignedInPeriod; refreshed DaySheet_FillPending
3. HoursCalendar day editor: If HasProjectLines (EditorLines.Length > 0); when false, show "No projects are assigned to you on this day. Ask your administrator." and hide Save draft and Confirm day.
4. OnClick_FillPendingButton: load the shown month's projects with Project_GetAssignedInPeriod; when there are none, show "No projects assigned this month" instead of the dropdown; after the fill, report filled and skipped days.
5. Day editor: TextArea NotesInput bound to EditorNotes (max 500), labelled "Notes".
### Acceptance criteria
- AC10.1 Given a day without assigned projects, When the editor opens, Then the message is shown and Save/Confirm are hidden.
- AC10.2 Given an assignment that starts on the 15th and pending days before and after it, When the user fills pending with that project, Then days from the 15th are filled, earlier days are skipped, and the message gives both counts.
- AC10.3 Given the user browses last month, Then Fill pending offers last month's projects.
- AC10.4 Given notes are typed and the day is saved, When the day is reopened, Then the notes are shown.
- AC10.5 Given Service Studio, Then 0 errors and 0 warnings.

## US11 · [Screen] Submit, approve and lock monthly timesheets
Trello: https://trello.com/c/LrYU7ORJ
### Description
As a consultant I want to submit my month, and as a manager I want to approve or reject it, so that approved hours can't change afterwards. Today confirmed days can still be edited or cleared, and there is no submission or approval. Covers: F13, F17.
### Implementation plan
#### ClaudeSheet_CS
1. Static entity PeriodStatus (Open, Submitted, Approved, Rejected) and entity MonthSheet (UserId, Year, Month, PeriodStatusId, SubmittedOn, DecidedBy, DecidedOn, Comment; unique index UserId+Year+Month; public, read-only).
2. Public server actions: MonthSheet_GetForUser (Year, Month; current user), MonthSheet_Submit (SheetUser; only when the month has no Pending days), MonthSheet_GetSubmitted (SheetManager), MonthSheet_Approve, MonthSheet_Reject (Comment required), MonthSheet_Reopen (SheetManager). User exception PeriodValidation.
3. DaySheet_Save, DaySheet_Clear and DaySheet_FillPending raise DaySheetValidation ("This month is submitted or approved") when the month is Submitted or Approved.
4. Calendar_GetMonth: add IsLocked to the result.
#### ClaudeSheet
Dependencies: PeriodStatus, MonthSheet, the 6 actions, PeriodValidation, SheetManager
5. MySheet / HoursCalendar: month status badge and "Submit month" button; locked days open read-only.
6. Screen MainFlow › Approvals (SheetManager): submitted months (user, month, total vs expected) with Approve, Reject with comment, and Reopen.
7. Menu link "Approvals", visible to SheetManager.
### Acceptance criteria
- AC11.1 Given a month with no Pending days, When the user submits it, Then the badge shows Submitted and its days can't be edited or cleared.
- AC11.2 Given a month with Pending days, Then Submit is refused with a message giving how many are pending.
- AC11.3 Given a manager approves it, Then it stays locked; When they reject it with a comment, Then it reopens and the user sees the comment.
- AC11.4 Given a user calls DaySheet_Save on an approved month directly, Then the server refuses it.
- AC11.5 Given a SheetUser without SheetManager, Then Approvals is refused; 0 errors and 0 warnings.

## US12 · [Screen] Manager: team overview and read-only user sheet
Trello: https://trello.com/c/3R2nkRhR
### Description
As a manager I want to see every consultant's month at a glance and open anyone's sheet read-only, so that I can follow up missing hours before approving. Today every action and screen works on the current user only. Covers: F16.
### Implementation plan
#### ClaudeSheet_CS
1. Structure UserMonthStatus (UserId, Name, TotalMinutes, ExpectedMinutes, PendingCount, PeriodStatusId).
2. Public server actions (SheetManager check): Team_GetMonthStatus (Year, Month) returning a list of UserMonthStatus for users with SheetUser; Calendar_GetMonthForUser, Calendar_GetMonthSummaryForUser and ProjectHours_GetWeeklyForUser (UserId plus the existing inputs), sharing the current-user logic.
#### ClaudeSheet
Dependencies: UserMonthStatus, the 4 actions, SheetManager
3. Screen MainFlow › TeamSheets (SheetManager): month selector; table Name, Total vs expected, Pending, Status; a row opens UserSheet.
4. Screen MainFlow › UserSheet (UserId, Year, Month inputs; SheetManager): the MySheet blocks read-only. Add optional inputs UserId and IsReadOnly to TotalHoursMonth, HoursCalendar, DayStatusKPIs and WeeklyProjectHours; with UserId set they call the *ForUser actions and hide every edit control.
5. Menu link "Team", visible to SheetManager.
### Acceptance criteria
- AC12.1 Given October 2026, Then TeamSheets lists every SheetUser with total, expected, pending count and status.
- AC12.2 Given a click on user A, Then UserSheet shows A's calendar, KPIs, total and weekly table with no edit controls.
- AC12.3 Given MySheet, Then the blocks behave exactly as before (UserId empty).
- AC12.4 Given a SheetUser without SheetManager calls Calendar_GetMonthForUser for another user, Then it is refused.
- AC12.5 Given Service Studio, Then 0 errors and 0 warnings.

## US13 · [Screen] Reports: hours per project, client and user with export
Trello: https://trello.com/c/g3PTAvGl
### Description
As a manager I want hours per project, client and consultant over a period, exportable for Excel, so that I can bill clients and report effort. Today nothing aggregates beyond the user's own month and week. Covers: F14, F18.
### Implementation plan
#### ClaudeSheet_CS
1. Structure ProjectHoursRow (ClientName, ProjectCode, ProjectName, UserName, TotalMinutes).
2. Public server actions (SheetManager or SheetAdmin check): Report_GetProjectHours (StartDate, EndDate, ClientId, ProjectId, UserId, GroupBy) returning a list of ProjectHoursRow; Report_ExportProjectHours (same inputs) returning a CSV as Binary Data (Excel opens it; a Record List To Excel output can't be referenced through the Model API).
#### ClaudeSheet
Dependencies: ProjectHoursRow, the 2 actions, SheetManager, SheetAdmin
3. Screen MainFlow › ProjectHoursReport (SheetManager, SheetAdmin): filters date range (default current month), client, project, user and group by; table with totals; "Export" downloads the CSV.
4. Menu link "Reports", visible to SheetManager or SheetAdmin.
### Acceptance criteria
- AC13.1 Given October 2026 grouped by project, Then each project's total equals the sum of its TimeEntries in the period.
- AC13.2 Given a client filter, Then only that client's projects appear and the grand total matches.
- AC13.3 Given Export, Then a CSV with a header and the same rows opens in Excel.
- AC13.4 Given a SheetUser without SheetManager or SheetAdmin, Then the screen and actions are refused; 0 errors and 0 warnings.

## US14 · [Screen] Admin: settings and per-user expected hours
Trello: https://trello.com/c/Ee94MYym
### Description
As an admin I want to change the default expected hours per day and set different hours for part-time consultants, so that totals and progress are right for everyone. Today there is one global site property that only Service Center can change. Covers: F21, F26.
### Implementation plan
#### ClaudeSheet_CS
1. Entity UserSetting (UserId unique, DailyExpectedMinutes, IsActive; public, read-only).
2. Public server actions: UserSetting_GetDailyExpectedMinutes (UserId; falls back to the site property), UserSetting_CreateOrUpdate (SheetAdmin), Settings_SetDailyExpectedMinutes (SheetAdmin; sets the site property).
3. Calendar_GetMonthSummary (and its *ForUser variant from US12, if built) uses UserSetting_GetDailyExpectedMinutes.
#### ClaudeSheet
Dependencies: UserSetting, the 3 actions, SheetAdmin
4. Screen Admin › Settings (SheetAdmin): default expected hours per day (H:MM, multiple of 15 minutes).
5. UserDetail (US9): "Expected hours per day" field; empty means the default.
6. Menu link "Settings", visible to SheetAdmin.
### Acceptance criteria
- AC14.1 Given the default changes from 8:00 to 7:30, Then MySheet's expected hours use 7:30 for users without their own setting.
- AC14.2 Given user A is set to 4:00, Then A's progress uses 4:00 per working day and other users are unchanged.
- AC14.3 Given a value that isn't a multiple of 15 minutes, Then it is rejected.
- AC14.4 Given a SheetUser without SheetAdmin, Then Settings is refused; 0 errors and 0 warnings.

## TT6 · [Tech] Audit fields on master data and day sheets
Trello: https://trello.com/c/tnEEFpke
### Description
Once admins and managers edit data, we need to know who changed what and when. Client, Project, ProjectAssignment and Holiday have no audit fields, and DaySheet and TimeEntry have no UpdatedBy. Covers: F22.
### Implementation plan
#### ClaudeSheet_CS
1. Add CreatedOn, CreatedBy (User Identifier), UpdatedOn and UpdatedBy to Client, Project, ProjectAssignment and Holiday; add UpdatedBy to DaySheet and TimeEntry (delete rule Protect on the User references).
2. Set them in Bootstrap and in every *_CreateOrUpdate or *_Save action that exists at the time.
3. docs/conventions.md: "Master-data entities carry CreatedOn/By and UpdatedOn/By, set by their Core actions."
### Acceptance criteria
- AC-TT6.1 Given the six entities, Then they have the audit attributes with the listed types.
- AC-TT6.2 Given an admin edits a project, Then UpdatedOn and UpdatedBy reflect the edit.
- AC-TT6.3 Given Service Studio, Then 0 errors and 0 warnings.
````
