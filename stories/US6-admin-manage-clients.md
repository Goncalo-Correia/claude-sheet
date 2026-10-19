# US6: [Screen] Admin: manage clients

- **Status:** Published
- **Published:** ClaudeSheet_CS 2026-10-19 13:13, ClaudeSheet 2026-10-19 13:45 (both manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-19
- **Depends on:** TT5 (SheetAdmin role, Admin UI flow)
- **Source:** gap analysis of 2026-10-16, findings F02, F09, approved 2026-10-19 (the analysis file is in commit 5bb59c3)
- **Trello:** https://trello.com/c/LNT15Ag2

## Story

As an admin I want to create, rename and deactivate clients, so that projects sit under the right client without touching the database. Today clients only come from the Bootstrap seed. Covers: F02, F09.

## Acceptance criteria

- [x] A01 · AC6.1 Given a SheetAdmin user, When they open Clients, Then all active clients are listed with their project count.
- [x] A02 · AC6.2 Given a new client "Acme" is saved, Then it is listed; saving a second "Acme" shows "A client with this name already exists".
- [x] A03 · AC6.3 Given a client is deactivated, Then its projects no longer appear in users' day editors.
- [x] A04 · AC6.4 Given a SheetUser without SheetAdmin, When they open Clients or call Client_CreateOrUpdate, Then they go to InvalidPermissions / the action raises the role exception.
- [x] A05 · AC6.5 Given Service Studio, Then 0 errors and 0 warnings.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Public user exception ClientValidation.
2. P02 · Public server actions, each starting with the SheetAdmin check: Client_GetList (IncludeInactive; with project count), Client_GetById, Client_CreateOrUpdate (Name required and unique, raises ClientValidation), Client_SetActive (deactivate instead of delete).

### ClaudeSheet

Dependencies to add in Service Studio › Manage Dependencies › ClaudeSheet_CS: the 4 actions, ClientValidation, SheetAdmin (and the ClientSummary structure).

3. P03 · Screen Admin › Clients (SheetAdmin): table Name, Projects, Active; search; "Show inactive"; "New client".
4. P04 · Screen Admin › ClientDetail (ClientId input): Name, Active, read-only list of the client's projects; Save catches ClientValidation and shows its message.
5. P05 · Menu link "Clients", visible to SheetAdmin.

Decisions before building (2026-10-19): the list returns a public structure **ClientSummary** (Id, Name, IsActive, ProjectCount), named by the conventions because the plan doesn't name it; Client_GetList also takes a Search input so the screen's search runs on the server; Client_GetById returns the client and its projects (for the read-only list on ClientDetail). The Model API can't set a user exception's Public flag, so **ClientValidation › Public = Yes is set by hand** in Service Studio after the merge.

## Implementation notes

### ClaudeSheet_CS

- Implemented 2026-10-19 in **1 `applyModelApiCode` save** (one dry run before it), laid out as built: vertical main path, origin 3200/800, 1371 between rows. Every action starts with `Is SheetAdmin?` (`CheckSheetAdminRole()`); its False branch raises the **Not SheetAdmin** role exception ("Only administrators can manage clients.") to the right. 0 validation messages.
- **P01 · ClientValidation:** user exception created. **Public must be set by hand** in Service Studio (the Model API can't set it; same as DaySheetValidation in TT4) before ClaudeSheet_CS is published.
- **P02 · actions** (all public):
  - `Client_GetList(IncludeInactive, Search)` → `Clients` (List of **ClientSummary**: Id, Name, IsActive, ProjectCount). SQL node `GetClients`: `{Client}.*` plus a project-count subquery; filter by active unless IncludeInactive; `CHARINDEX(@Search, Name) > 0` when Search isn't empty; ordered by name.
    - The dry run's SQL (explicit columns, GROUP BY, LIKE) raised two Service Studio suggestions ("complete enumeration of Client attributes" and the LIKE case/accent note), so the final statement uses `{Client}.*` and CHARINDEX. The search is case-insensitive through the database collation.
  - `Client_GetById(ClientId)` → `ClientRecord` (Client) and `Projects` (List of Project, sorted by name). Raises ClientValidation "Client not found." The output is ClientRecord, not Client: an output named Client clashed with the entity and was renamed Client2 in the dry run.
  - `Client_CreateOrUpdate(ClientRecord)` → `ClientId`. `Name empty?` raises "Enter the client name."; aggregate GetSameName (same trimmed name, other Id) → `Name taken?` raises "A client with this name already exists."; the name is trimmed, then CreateOrUpdateClient. UQ_Client_Name still guards the database.
  - `Client_SetActive(ClientId, IsActive)`: GetClient → `Not found?` raises "Client not found." → assign IsActive → UpdateClient. No delete action: clients are deactivated (Project.ClientId is Protect).
- **Merge:** `omlMerge` timed out on Claude's side (300 s) while the Compare-and-Merge window was open; the user accepted it in Service Studio and set **ClientValidation › Public = Yes** by hand. Live module verified afterwards.
- **Flow layout gate:** the 4 actions are vertical with horizontal raise branches: 0 diagonals, 0 crossings, 0 overlaps. No other flow touched.

### ClaudeSheet

- **Dependencies:** the user ticked Client_GetList, Client_GetById, Client_CreateOrUpdate, Client_SetActive, ClientSummary and ClientValidation ("Done", 2026-10-19). `omlRefreshReferences` reported "No references needed refreshing"; a probe found the 4 actions and ClientSummary in `References › ClaudeSheet_CS`, none suffixed. The Model API doesn't list referenced user exceptions, so ClientValidation couldn't be checked that way.
- Implemented in **1 `applyModelApiCode` save**, after two dry runs. ClaudeSheet has 0 validation messages.
- **P03 · Admin › Clients** (roles: SheetAdmin only):
  - Locals: Search, ShowInactive.
  - Data action **GetClients** → Client_GetList(ShowInactive, Search) → Clients, plus ErrorMessage from an AllExceptions handler. **OnAfterFetch_GetClients** shows the error.
  - Toolbar: SearchInput, ShowInactiveCheckbox and "Show inactive" (both OnChange → **OnChange_Filters**, which refreshes GetClients), and NewClientButton → ClientDetail(ClientId = NullIdentifier()).
  - Table: a header row, then ClientList, where each row is a link to ClientDetail with Name, Projects (ProjectCount) and Active/Inactive. Empty state: "No clients found." ARIA roles table/row/columnheader/cell.
- **P04 · Admin › ClientDetail** (roles: SheetAdmin; input ClientId, optional):
  - Data action **GetClient**: a new client gets IsActive = True; otherwise Client_GetById → ClientRecord and Projects.
  - **OnAfterFetch_GetClient** copies ClientRecord into the local **EditClient**. It isn't called ClientRecord: a local with the same name as the data action output got the output renamed ClientRecord2 in the dry run, and the same happened to ErrorMessage, hence **FormError**.
  - Form: NameInput (EditClient.Name, mandatory, maxlength 100), a status line, Save, and Activate/Deactivate for existing clients. Read-only Projects table (name, code, status) with "No projects for this client yet.", plus "Back to clients".
  - **OnClick_SaveButton** (validates the form): Client_CreateOrUpdate(EditClient) → "Client saved." → Clients. **OnClick_ToggleActiveButton**: Client_SetActive(ClientId, not IsActive) → refresh GetClient → message.
  - **Deviation:** both screen actions catch **AllExceptions**, not ClientValidation, and put `AllExceptions.ExceptionMessage` in FormError, shown above the form (role="alert"). Exception handlers need a local exception, and the Model API doesn't expose ClaudeSheet_CS's ClientValidation in the consumer (`eSpace.UserExceptions` is empty). The user still sees ClientValidation's text ("A client with this name already exists.", "Enter the client name."). Recorded in `docs/conventions.md`. Catching ClientValidation by name can be set by hand in Service Studio if wanted.
- **P05 · Menu:**
  - New link **ClientsLink** "Clients" → Admin › Clients (role="menuitem"), second in PageLinks, so both admin screens pass ActiveItem = 1 to the Menu.
  - **Deviation:** Visible is `GetMenuAccess.IsSheetAdmin`, not `CheckSheetAdminRole()`. Referenced roles have no client-side check function (the reference's CheckRoleClientSideAction is null, and the expression gave "Unknown function"). So the Menu got a server data action **GetMenuAccess** (IsSheetAdmin = CheckSheetAdminRole()). `docs/conventions.md` › Roles and the menu is corrected.
- **Merge:** `omlMerge` timed out on Claude's side again (300 s); the merge completed in Service Studio and the live module was verified afterwards.
- **CSS:** `.admin-*` rules on both screens' stylesheets (screen-specific CSS stays on the screen). Move them to the theme once more admin screens use them.
- **Still to verify at runtime (A01–A04):** as an admin, open Clients, create "Acme" and try a second "Acme"; deactivate a client and check its projects are gone from a user's day editor; as a SheetUser without SheetAdmin, check the Clients link is hidden and the screens go to InvalidPermissions.
- **Flow layout gate:** the 6 new screen actions, the 2 screen data actions and GetMenuAccess are vertical, with handlers in their own columns: 0 diagonals, 0 crossings, 0 overlaps.

## Trail

- Changes: `changes/US6-ClaudeSheet_CS.md` (ClientValidation, ClientSummary, 4 Client actions), `changes/US6-ClaudeSheet.md` (Clients, ClientDetail, Menu). Written by Claude from the applied code (manual publishes).
- Review: `reviews/ClaudeSheet_CS/US6.md` (plan check + review round 1 approved, P01 accepted despite Rejected), `reviews/ClaudeSheet/US6.md` (plan check + review round 1 approved, P04 and P05 accepted despite Rejected)
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 13:13, ClaudeSheet 13:45; the MCP attempts were blocked)
- Snapshots: `snapshots/ClaudeSheet_CS/` and `snapshots/ClaudeSheet/` refreshed 2026-10-19 (targeted)
- Docs: `docs/conventions.md` › UI › Roles and the menu (GetMenuAccess pattern, corrected from TT5; catching CS user exceptions)
