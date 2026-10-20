# US9: [Screen] Admin: manage users and their access

- **Status:** Published
- **Published:** ClaudeSheet_CS 2026-10-20 12:23, ClaudeSheet 2026-10-20 13:37 (both manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-20
- **Depends on:** TT5 (SheetAdmin role, Admin UI flow, GetMenuAccess), TT7 (admin CSS in the theme), US7 (ProjectAssignment actions, assignment popup)
- **Source:** gap analysis of 2026-10-16, findings F06, F11, approved 2026-10-19 (the analysis file is in commit 5bb59c3)
- **Trello:** https://trello.com/c/F12gaDuW (moved to Done 2026-10-20)

## Story

As an admin I want to see the app's users and grant or revoke access (SheetUser, SheetAdmin, SheetManager), so that new consultants can start without a database or Service Center change. Today access comes only from the SeedTestUsernames site property, and users without the role get a bare InvalidPermissions page. Covers: F06, F11.

## Acceptance criteria

- [x] A01 · AC9.1 Given a registered user B without roles, When an admin ticks SheetUser for B and saves, Then B can open MySheet after the next login.
- [x] A02 · AC9.2 Given an admin revokes SheetUser from B, Then B goes to InvalidPermissions with the new text.
- [x] A03 · AC9.3 Given an admin edits their own user, Then SheetAdmin can't be removed.
- [x] A04 · AC9.4 Given UserDetail for A, Then A's assignments are listed and can be added or ended.
- [x] A05 · AC9.5 Given a SheetUser without SheetAdmin, Then screens and actions are refused; 0 errors and 0 warnings.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Structure UserAccess (UserId, Name, Username, Email, IsActive, IsSheetUser, IsSheetAdmin, IsSheetManager, ActiveAssignments).
2. P02 · Public server actions (SheetAdmin check): User_GetList (Search, OnlyWithAccess) returning UserAccess; User_SetRoles (UserId, IsSheetUser, IsSheetAdmin, IsSheetManager; Grant/Revoke<Role>Role; an admin can't remove their own SheetAdmin).

### ClaudeSheet

Dependencies to add in Service Studio › Manage Dependencies › ClaudeSheet_CS: UserAccess, the 2 actions, SheetAdmin; the ProjectAssignment actions from US7.

- Server Actions: User_GetList, User_SetRoles
- Structures: UserAccess
- Already referenced (no tick needed): SheetAdmin (TT5); ProjectAssignment_GetList, ProjectAssignment_CreateOrUpdate, ProjectAssignment_End, ProjectAssignment_Delete, Project_GetList, User_GetActive, ProjectAssignmentSummary, ProjectSummary, UserOption (US7)

3. P03 · Screen Admin › Users (SheetAdmin): search; table Name, Username, roles as tags, active assignments.
4. P04 · Screen Admin › UserDetail (UserId input): role toggles with Save; the user's assignments, reusing the US7 assignment popup.
5. P05 · InvalidPermissions: text "You don't have access to ClaudeSheet yet. Ask an administrator to give you access."
6. P06 · Menu link "Users", visible to SheetAdmin.

## Implementation notes

Decisions before building (2026-10-20, user's choice):
- **P04 popup:** US7's assignment popup is a Popup widget inside ProjectDetail, not a block, so it can't be reused as it is. It is extracted into a shared block that both ProjectDetail and UserDetail use. This also changes ProjectDetail.
- **AC9.3 exception:** P02 names no exception. User_SetRoles raises the existing **AssignmentValidation** (no new user exception, so nothing to set Public by hand).

### ClaudeSheet_CS

- Implemented 2026-10-20 in **1 `applyModelApiCode` save** (one dry run before it), with 0 validation messages. The dry run gave a warning ("LIKE in advanced query", AdvancedQuery_AuroraLikeWarning), so the search uses `CHARINDEX` instead of `LIKE` (AC9.5 requires 0 warnings).
- **P01 · UserAccess** (public structure): UserId (User Identifier), Name, Username, Email (Text 250), IsActive, IsSheetUser, IsSheetAdmin, IsSheetManager (Boolean), ActiveAssignments (Integer).
- **P02 · User_GetList(Search, OnlyWithAccess) → Users (List of UserAccess)**, public, folder User:
  - `Is SheetAdmin?`; when False, raises the SheetAdmin role exception "Only administrators can list users."
  - SQL `GetUsers`: every user, with the count of active, not-ended assignments (same rule as Project_GetList's AssignedUsers). Search is trimmed and matched against name, username or email. Ordered by name.
  - For Each `Each user` → `Read roles` (CheckSheetUserRole / CheckSheetAdminRole / CheckSheetManagerRole with the user's Id) → `Include user?` (`not OnlyWithAccess or` any role) → ListAppend to Users.
- **P02 · User_SetRoles(UserId, IsSheetUser, IsSheetAdmin, IsSheetManager)**, public, folder User:
  - `Is SheetAdmin?`; when False, raises the role exception "Only administrators can change user access."
  - Aggregate `GetUser` → `User missing?` → AssignmentValidation "User not found."
  - `Removing own SheetAdmin?` (`UserId = GetUserId() and not IsSheetAdmin`) → AssignmentValidation "You can't remove your own administrator access."
  - Then for each role, `Grant <Role>?` → Grant<Role>Role (right) or Revoke<Role>Role (down).
- **Flow layout gate:** User_GetList has 2 loop-back diagonals (allowed). User_SetRoles has 2 merge diagonals, GrantSheetUserRole → `Grant SheetAdmin?` and GrantSheetAdminRole → `Grant SheetManager?`. Allowed, because both branches must rejoin before the next role is set. Both flows: 0 other diagonals, 0 crossings, 0 overlaps. Node names gate: 0 actions without a folder, 0 unlabelled nodes, 0 suffixed Execute nodes.
- **Review:** round 1 approved, all Yes (N01 accepted despite Rejected), see `reviews/ClaudeSheet_CS/US9.md`.
- **Published** 2026-10-20 12:23 by the user (1-Click Publish; the MCP publish was blocked). Change log in `changes/US9-ClaudeSheet_CS.md`; snapshots refreshed (targeted).
- **Merge:** `omlMerge` timed out on the client after 300 s while Compare-and-Merge was open. The live module was re-checked afterwards: all elements are present, with 0 errors and 0 warnings. Plan check round 1 approved (`reviews/ClaudeSheet_CS/US9.md`).

### ClaudeSheet

- **Dependencies:** after the user's "Done", `omlRefreshReferences` refreshed the references. The probe found User_GetList, User_SetRoles, UserAccess and the US7 elements, none suffixed.
- Implemented 2026-10-20 in **1 `applyModelApiCode` save** (one dry run before it). The only validation message is the sidecar's `InvalidIconLibrary` on the theme, expected whenever the theme stylesheet is edited (see the `servicestudio-mcp-oml` skill: Service Studio's own validation is clean and the merge works).
- **P04 decision · shared block Admin › AssignmentEditor** (user's choice: extract US7's popup into a block):
  - Inputs ProjectId and UserId; pass exactly one. With ProjectId it lists the project's users (user dropdown, "Assigned users" / "Add user"); with UserId it lists the user's projects (project dropdown, "Assigned projects" / "Add project").
  - Data actions:
    - GetAssignments: ProjectAssignment_GetList, with OnAfterFetch_GetAssignments.
    - GetUsers: User_GetActive, only in project mode.
    - GetProjects: Project_GetList for active projects, only in user mode.
  - OnParametersChanged reloads all three and closes the popup.
  - The 7 popup and row actions are US7's, unchanged except that they work in both modes. `PopupUserName` became `PopupName` (the user or the project).
  - **ProjectDetail changed accordingly:**
    - Its AssignmentsSection and AssignmentPopup widgets are replaced by the block instance `ProjectAssignments` (ProjectId = ProjectId), inside the existing `ShowAssignments` If.
    - Its 8 assignment screen actions, the data actions GetAssignments and GetUsers, and 5 locals are deleted.
    - Behaviour is the same as US7, including the refused Delete switching the popup to End mode.
- **P03 · Admin › Users** (roles: SheetAdmin only):
  - Locals: Search, OnlyWithAccess (default False, so users without roles are listed too, for AC9.1).
  - Data action **GetUsers**: User_GetList(Search, OnlyWithAccess), with **OnAfterFetch_GetUsers**. **OnChange_Filters** reloads it from SearchInput and OnlyWithAccessCheckbox.
  - Table `admin-table--4`: Name (greyed when the user is inactive), Username, Access (tags SheetUser / SheetAdmin / SheetManager, or "No access"), Active assignments. Each row links to UserDetail; empty state "No users found."
- **P04 · Admin › UserDetail** (roles: SheetAdmin; input UserId, mandatory):
  - Data action **GetUser**: aggregate GetUserById on (System) User, then roles via `Check<Role>Role(UserId)` (server side, like the Menu's GetMenuAccess). "User not found." when missing.
  - **OnAfterFetch_GetUser** copies the result into the local **EditAccess**.
  - Form: name, username, email and inactive flag; three **Switch** widgets for SheetUser, SheetAdmin and SheetManager.
    - The SheetAdmin switch is disabled on your own user, with the hint "You can't remove your own administrator access." User_SetRoles also refuses it on the server (AC9.3).
    - Hint: "Changes apply from the user's next login."
  - **OnClick_SaveButton** calls User_SetRoles and reloads GetUser. Errors go to FormError (AllExceptions).
  - Below the form, block instance `UserAssignments` (UserId = UserId).
- **P05 · InvalidPermissions:** the heading now reads "You don't have access to ClaudeSheet yet." and the line below it "Ask an administrator to give you access." (the plan's sentence, split across the screen's two text widgets). The same text also shows when a SheetUser opens an admin screen.
- **P06 · Menu:** new link **UsersLink** "Users" → Admin › Users, fifth in PageLinks (role menuitem), Visible = `GetMenuAccess.IsSheetAdmin`. Users and UserDetail pass ActiveItem = 4.
- **Theme CSS** (Admin screens section, appended after a guard rule `.admin-users-start { }`): `.admin-table--4`, `.admin-tags`, `.admin-tag`, `.admin-tag--admin`, `.admin-tag--none`, `.admin-form__switch`.
- **Merge:** `omlMerge` returned `merged: true`; live module re-checked, 0 validation messages. Plan check round 1 approved (`reviews/ClaudeSheet/US9.md`).
- **Review:** round 1 approved, all Yes (N01 accepted despite Rejected: UserDetail's GetUser reads the User entity directly), see `reviews/ClaudeSheet/US9.md`.
- **Published** 2026-10-20 13:37 by the user (1-Click Publish; the MCP publish was blocked). Change log in `changes/US9-ClaudeSheet.md`; snapshots refreshed (targeted).
- **Still to verify at runtime (A01–A05):** as an admin, give a user without roles SheetUser and check MySheet after their next login; revoke it and check InvalidPermissions; try to switch off your own SheetAdmin; add and end a project for a user on UserDetail; check that ProjectDetail's assignments still work (Add, Edit, End, refused Delete → End); as a non-admin, check the menu and the Users screen.
- **Flow layout gate:** 23 flows (the block's 12, Users' 3, UserDetail's 3, ProjectDetail's remaining 5): 0 other diagonals, 0 crossings, 0 overlaps, 0 unlabelled nodes, 0 suffixed Execute nodes.

## Trail

- Changes: `changes/US9-ClaudeSheet_CS.md` (save 1), `changes/US9-ClaudeSheet.md` (save 1)
- Review: `reviews/ClaudeSheet_CS/US9.md` (plan check + review round 1 approved), `reviews/ClaudeSheet/US9.md` (plan check + review round 1 approved)
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 12:23, ClaudeSheet 13:37; the MCP attempts were blocked)
- Snapshots: `snapshots/ClaudeSheet_CS/` and `snapshots/ClaudeSheet/` refreshed 2026-10-20 (targeted)
