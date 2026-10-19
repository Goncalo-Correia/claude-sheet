# ClaudeSheet_CS · US7 · review log

## Implementation plan check — round 1 — 2026-10-19

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P01 | Public user exceptions ProjectValidation and AssignmentValidation | Rejected | Both created; Public can't be set through the Model API, set by hand in Service Studio (as in US6) | Yes |
| P02 | Public server actions Project_GetList, Project_GetById, Project_CreateOrUpdate, Project_SetActive (SheetAdmin check) | Approved | All public with the Is SheetAdmin? gate; Project_GetList returns ProjectSummary; CreateOrUpdate checks name, code, client found/active, code unique per client (ProjectValidation) | Yes |
| P03 | Public server actions ProjectAssignment_GetList, _CreateOrUpdate, _End, _Delete (SheetAdmin check) | Approved | All public with the gate; GetOverlapping (ADR 0002) and GetEntriesInPeriod checks raise AssignmentValidation; plus User_GetActive / UserOption (user's choice) | Yes |
| P04 | ProjectAssignment.ProjectId delete rule Delete → Protect | Approved | Delete Rule = Protect | Yes |

No reasons (no No answers).

Outcome: merged (`merged: true`), live module re-checked after the merge: 9 actions public, 2 exceptions, 3 structures, delete rule Protect. P01 accepted despite Rejected tag.

Plan check: approved

## Review — round 1 — 2026-10-19

Plan check approved earlier today (round 1; P01 accepted despite Rejected), so the plan steps were not in this form.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC7.1 project "Portal \| PTL" listed; second "PTL" under ICNF rejected | Approved | Project_CreateOrUpdate › Code taken? (ProjectValidation); producer part only | Yes |
| A02 | AC7.2 open-ended assignment shows Portal in MySheet | Approved | open EndDate accepted; Project_GetAssignedToUser handles open ends; verify at runtime; producer part only | Yes |
| A03 | AC7.3 overlapping period rejected | Approved | GetOverlapping › Overlaps? (AssignmentValidation) | Yes |
| A04 | AC7.4 delete with entries rejected, End offered | Approved | ProjectAssignment_Delete › Has entries?; ProjectAssignment_End; producer part only | Yes |
| A05 | AC7.5 Protect + deactivate | Approved | ProjectAssignment.ProjectId Protect; Project_SetActive | Yes |
| A06 | AC7.6 non-admin refused | Approved | SheetAdmin gate in all 9 actions; producer part only | Yes |
| A07 | AC7.7 0 errors / 0 warnings | Approved | 0 validation messages in ClaudeSheet_CS; producer part only | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Created | Data › Structures › ProjectSummary | New | Yes |
| C02 | Created | Data › Structures › ProjectAssignmentSummary | New | Yes |
| C03 | Created | Data › Structures › UserOption | New | Yes |
| C04 | Modified | Data › Database › ProjectAssignment (ProjectId → Protect) | New | Yes |
| C05 | Created | Logic › Exceptions › ProjectValidation, AssignmentValidation (Public set by hand) | New | Yes |
| C06 | Created | Logic › Server Actions › Project_GetList | New | Yes |
| C07 | Created | Logic › Server Actions › Project_GetById | New | Yes |
| C08 | Created | Logic › Server Actions › Project_CreateOrUpdate | New | Yes |
| C09 | Created | Logic › Server Actions › Project_SetActive | New | Yes |
| C10 | Created | Logic › Server Actions › ProjectAssignment_GetList | New | Yes |
| C11 | Created | Logic › Server Actions › ProjectAssignment_CreateOrUpdate | New | Yes |
| C12 | Created | Logic › Server Actions › ProjectAssignment_End | New | Yes |
| C13 | Created | Logic › Server Actions › ProjectAssignment_Delete | New | Yes |
| C14 | Created | Logic › Server Actions › User_GetActive | New | Yes |
| C15 | Unchanged (dependency) | Logic › Server Actions › Project_GetAssignedToUser | New | Yes |

### Conventions

| ID | Item | Tag | Answer |
|---|---|---|---|
| N01 | No convention violations (names, placement, role checks, delete rule, flow layout) | Approved | Yes |

Outcome: all Yes, no fixes, 0 validation errors. Cleared for publish.
