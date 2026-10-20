# ClaudeSheet · US9 · review log

## Implementation plan check — round 1 — 2026-10-20

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P03 | Screen Admin › Users (SheetAdmin): search; table Name, Username, roles as tags, active assignments | Approved | Admin › Users, SheetAdmin; SearchInput + OnlyWithAccessCheckbox → GetUsers (User_GetList); admin-table--4 with role tags; rows open UserDetail; verify at runtime | Yes |
| P04 | Screen Admin › UserDetail (UserId input): role toggles with Save; the user's assignments, reusing the US7 assignment popup | Approved | Admin › UserDetail, SheetAdmin; 3 Switch widgets + Save access → User_SetRoles; own SheetAdmin switch disabled; new shared block Admin › AssignmentEditor (user's choice), also used by ProjectDetail; verify at runtime | Yes |
| P05 | InvalidPermissions text | Approved | Heading "You don't have access to ClaudeSheet yet.", line "Ask an administrator to give you access." | Yes |
| P06 | Menu link "Users", visible to SheetAdmin | Approved | Menu › PageLinks › UsersLink, Visible = GetMenuAccess.IsSheetAdmin; ActiveItem 4 | Yes |

No reasons (no No answers).

Outcome: merged (`merged: true`). Re-checked on the live module afterwards: block AssignmentEditor (9 screen actions, 3 data actions), Users and UserDetail with SheetAdmin, ProjectDetail without its old assignment actions, UsersLink, the InvalidPermissions texts, the theme CSS. 0 validation messages.

Plan check: approved

## Review — round 1 — 2026-10-20

Plan check approved earlier today (round 1, all Yes), so the plan steps were not in this form.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC9.1 SheetUser granted → B opens MySheet after next login | Approved | Users lists users without roles → UserDetail switch → User_SetRoles; verify at runtime | Yes |
| A02 | AC9.2 SheetUser revoked → InvalidPermissions with new text | Approved | RevokeSheetUserRole; new InvalidPermissions texts; verify at runtime | Yes |
| A03 | AC9.3 Own SheetAdmin can't be removed | Approved | SheetAdminSwitch disabled on own user + SelfHint; server refusal in User_SetRoles | Yes |
| A04 | AC9.4 A's assignments listed, added, ended | Approved | UserAssignments block (UserId mode), project dropdown, End link; verify at runtime | Yes |
| A05 | AC9.5 Non-admin refused; 0 errors, 0 warnings | Approved | Screens SheetAdmin only, link gated, actions check SheetAdmin; 0 validation messages | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Created | Interface › UI Flows › Admin › AssignmentEditor | New | Yes |
| C02 | Modified | Interface › UI Flows › Admin › ProjectDetail | New | Yes |
| C03 | Created | Interface › UI Flows › Admin › UserDetail | New | Yes |
| C04 | Created | Interface › UI Flows › Admin › Users | New | Yes |
| C05 | Modified | Interface › UI Flows › Common › InvalidPermissions | New | Yes |
| C06 | Modified | Interface › UI Flows › Common › Menu | New | Yes |
| C07 | Modified | Interface › Themes › ClaudeSheet | New | Yes |

### Conventions

| ID | Row | Tag | Reason | Answer |
|---|---|---|---|---|
| N01 | UserDetail › GetUser reads (System) User directly | Rejected | Read in the front end instead of a Core action (read only, screen role SheetAdmin) | Yes |
| N02 | No other convention violations | Approved | names, Admin flow + SheetAdmin, gated link, theme CSS, 23 flows clean | Yes |

No reasons (no No answers). N01 accepted despite Rejected tag. Outcome: all Yes; 0 validation errors; next step is to publish.
