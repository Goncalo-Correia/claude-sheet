# ClaudeSheet · US7 · review log

## Implementation plan check — round 1 — 2026-10-19

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P05 | Screen Admin › Projects (SheetAdmin): table, filter by client, "New project" | Approved | Roles SheetAdmin; GetClients + GetProjects; ClientFilterDropdown, ShowInactiveCheckbox → OnChange_Filters; NewProjectButton; 5-column table linking to ProjectDetail; verify at runtime | Yes |
| P06 | Screen Admin › ProjectDetail: form, "Assigned users" with Add/Edit/End/Delete popup, exception messages | Approved | Form + Save/Activate; assignments table with Edit/End/Delete; AssignmentPopup (built-in Popup) with Edit and End modes; refused Delete opens End mode; AllExceptions shows the CS messages (convention); verify at runtime | Yes |
| P07 | Menu link "Projects", visible to SheetAdmin | Approved | ProjectsLink, Visible = GetMenuAccess.IsSheetAdmin, ActiveItem 2 | Yes |

No reasons (no No answers).

Outcome: merged (`merged: true`); live module re-checked after the merge: Projects and ProjectDetail (SheetAdmin), AssignmentPopup, ProjectsLink; 0 validation messages.

Plan check: approved

## Review — round 1 — 2026-10-19

Plan check approved earlier today (round 1, all Yes), so the plan steps were not in this form.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC7.1 project "Portal \| PTL" listed; second "PTL" under ICNF rejected | Approved | ProjectDetail form → Project_CreateOrUpdate; message in ErrorBox; verify at runtime | Yes |
| A02 | AC7.2 open-ended assignment shows Portal in MySheet | Approved | empty EndDateInput = open end; MySheet unchanged; verify at runtime | Yes |
| A03 | AC7.3 overlapping period rejected | Approved | OnClick_SaveAssignmentButton handler → PopupError; verify at runtime | Yes |
| A04 | AC7.4 delete with entries rejected, End offered | Approved | OnClick_DeleteAssignmentLink handler opens the popup in End mode with the message; verify at runtime | Yes |
| A05 | AC7.5 Protect + deactivate | Approved | no delete on screens; ToggleActiveButton → Project_SetActive; Protect in CS | Yes |
| A06 | AC7.6 non-admin refused | Approved | ProjectsLink Visible = GetMenuAccess.IsSheetAdmin; screens SheetAdmin only; verify at runtime | Yes |
| A07 | AC7.7 0 errors / 0 warnings in both modules | Approved | 0 validation messages in both | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Modified | References › ClaudeSheet_CS (12 US7 elements, ticked by the user) | New | Yes |
| C02 | Created | Interface › UI Flows › Admin › Projects | New | Yes |
| C03 | Created | Interface › UI Flows › Admin › ProjectDetail | New | Yes |
| C04 | Modified | Interface › UI Flows › Common › Menu (ProjectsLink) | New | Yes |

### Conventions

| ID | Item | Tag | Answer |
|---|---|---|---|
| N01 | Names, placement, roles, flow layout | Approved | Yes |
| N02 | Admin CSS on 4 screen stylesheets instead of the theme | Rejected (justified in implementation notes) | Yes |

Outcome: all Yes, no fixes, 0 validation errors. N02 accepted despite Rejected tag. Cleared for publish.
