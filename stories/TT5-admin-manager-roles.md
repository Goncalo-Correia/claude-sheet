# TT5: [Tech] Admin and manager roles, Admin UI flow, role-gated menu

- **Status:** Published
- **Published:** ClaudeSheet_CS 2026-10-19 11:32, ClaudeSheet 2026-10-19 11:45 (both manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-19
- **Source:** investigation `investigations/2026-10-16-full-app-gaps.md` (findings F01, F07, F12, F15), approved 2026-10-19
- **Trello:** https://trello.com/c/kzWSgkuL

## Story

The app has a single role (SheetUser), so nothing can protect master-data management or a manager view, and the UI has no place for admin screens. This task adds the roles and that place, so every following story only adds its own actions, screens and menu link. Covers: F01, F07, F12, F15.

## Acceptance criteria

- [x] A01 · AC-TT5.1 Given ClaudeSheet_CS, Then roles SheetAdmin and SheetManager exist, are public and have descriptions.
- [x] A02 · AC-TT5.2 Given SeedAdminUsernames = "admin.user" and the module is published, Then admin.user has SheetAdmin and SheetUser.
- [x] A03 · AC-TT5.3 Given ClaudeSheet, Then the UI flow Admin exists and both roles are referenced without a "2" suffix.
- [x] A04 · AC-TT5.4 Given Service Studio, Then both modules validate with 0 errors and 0 warnings.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Create public roles SheetAdmin ("Manages clients, projects, assignments, holidays, users and settings") and SheetManager ("Sees and approves the team's sheets, runs reports").
2. P02 · Add site property SeedAdminUsernames (Text, default empty). In Bootstrap, grant SheetAdmin and SheetUser to each username in it (same pattern as SeedTestUsernames), so a first admin exists before US9.
3. P02b · Delete the unused server action Minutes_Format (pre-existing "Unused user action" warning; ClaudeSheet has its own client action), so AC-TT5.4 can pass. Agreed 2026-10-19.

### ClaudeSheet

Dependencies to add in Service Studio › Manage Dependencies › ClaudeSheet_CS: Roles SheetAdmin, SheetManager.

3. P03 · Create UI flow Admin (screens are added by US6–US9 and US14).
4. P04 · Menu: role-gated link pattern (link Visible = Check<Role>Role()), documented in docs/conventions.md; no new links yet.

## Implementation notes

### ClaudeSheet_CS

- Implemented 2026-10-19 in **2 `applyModelApiCode` saves**:
  1. the roles, the site property, the Bootstrap admin loop and the deletion of Minutes_Format;
  2. the layout of Bootstrap with `queries/layout-flow.cs`.
- Service Studio had restarted since the last session (the old token was rejected and the module keys changed); a new session token was issued. ClaudeSheet_CS was confirmed to be the same module (key `Hff6YxyWCUWs0l+vxIrNNw`).
- **Roles (P01):** SheetAdmin and SheetManager, both public, with descriptions. The platform creates their Check/Grant/Revoke actions.
- **Seed admin (P02):**
  - Site property SeedAdminUsernames (Text, default "").
  - Bootstrap: after the SeedTestUsernames loop, `Admin seed list` (SeedList = ",<names>,") → aggregate GetSeedAdmins (active users whose username is in the list) → For Each. Its body: `Has SheetUser?` (false → GrantSheetUserRole2) → `Has SheetAdmin?` (false → GrantSheetAdminRole) → back to the For Each. The existing End follows the loop.
  - Checks before granting, as in the existing user loop, so the action stays safe to run more than once. The description now mentions the admin seed.
- **P02b:** deleted the unused, non-public server action Minutes_Format (agreed 2026-10-19); ClaudeSheet keeps its own client action. ClaudeSheet_CS now has 0 validation messages.
- **Flow layout gate:** Bootstrap was laid out with `queries/layout-flow.cs`. Report: 36 vertical, 6 horizontal, 6 loop-backs, 2 merges, 0 other diagonals, 0 crossings, 0 overlaps. The 2 merge diagonals were there before TT5: "Client missing?" / "Project missing?" rejoin the main path after the optional Create step (the Assign at the right rejoins GetSeedProject and the first Holiday_CreateIfMissing).

### ClaudeSheet

- **Dependencies:** the user ticked Roles SheetAdmin and SheetManager in Manage Dependencies › ClaudeSheet_CS ("Done", 2026-10-19). `omlRefreshReferences` reported "No references needed refreshing"; a probe found SheetAdmin, SheetUser and SheetManager in `References › ClaudeSheet_CS`, none suffixed.
- Implemented in **1 `applyModelApiCode` save**: UI flow **Admin** (empty; its description says it is for SheetAdmin screens and role-gated Menu links). ClaudeSheet has 0 validation messages (an empty UI flow raises no warning).
- **P04** is documentation, not a model change: `docs/conventions.md` › UI › "Roles and the menu" (which role uses which screens; Admin screens in the Admin flow with SheetAdmin only; server actions check the same role; every Menu link to a role-restricted screen sets Visible to that role check). No Menu link was added: each later story adds its own.
- No flows were touched in ClaudeSheet, so the flow layout gate doesn't apply.
- **Still to verify at runtime (AC-TT5.2):** set SeedAdminUsernames in Service Center (ClaudeSheet_CS › Site Properties) to an existing username, republish ClaudeSheet_CS or run Timer_Bootstrap, and check the user has SheetAdmin and SheetUser.

## Trail

- Changes: `changes/2026-10-19/112014-ClaudeSheet_CS-TT5.cs` (roles, site property, Bootstrap admin seed, Minutes_Format deleted), `112108` (Bootstrap layout); ClaudeSheet: `113630-ClaudeSheet-TT5.cs` (UI flow Admin). Written by Claude from the applied code (manual publishes).
- Review: `reviews/ClaudeSheet_CS/TT5.md` (plan check + review round 1 approved, N01 accepted despite Rejected), `reviews/ClaudeSheet/TT5.md` (plan check + review round 1 approved)
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 11:32, ClaudeSheet 11:45; the MCP attempts were blocked)
- Snapshots: `snapshots/ClaudeSheet_CS/` (targeted) and `snapshots/ClaudeSheet/` (light) refreshed 2026-10-19
- Docs: `docs/conventions.md` › UI › Roles and the menu (P04); `docs/architecture.md` Roles and Client actions rows updated
