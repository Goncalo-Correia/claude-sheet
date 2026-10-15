# Review: ClaudeSheet · TT2 (re-lay out action flows: vertical first, no crossings)

## Implementation plan check — round 1 — 2026-10-15 16:22

Implemented in 3 `applyModelApiCode` saves. The `omlMerge` tool call showed as rejected in the conversation, but the merge reached Service Studio: the out files were deleted (a successful merge deletes them) and the live module shows exactly the validated state (queries/flow-layout.cs: 64 flows, 0 other diagonals, 1 crossing, 3 merges, 0 overlaps; 0 validation errors). A retry was refused because no working copy was tracked any more.

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P01 | Run flow-layout.cs, confirm the 15 flows | Approved | 64 flows, 41 other diagonals, 8 crossings, same 15 flows as the card | Yes |
| P02 | Run layout-flow.cs on the 15 flows | Approved | 3 saves; tool improved during the work (taller branch down, optional steps right, loops clear, range-aware columns, through-node check); 7 new End nodes | Yes |
| P03 | Same connectors except End redirects | Approved | Checked inside every save; only 7 End → new End redirects | Yes |
| P04 | Fix the DaySheet_Save crossing | Approved | 8 → 0 crossings; main path in column 0 | Yes |
| P05 | Merge diagonals horizontal or justified | Approved | 3 merges justified in implementation notes (DaySheet_Save 1, Bootstrap 2) | Yes |
| P06 | Final report clean, 0 errors, merge | Rejected | 1 crossing in DaySheet_FillPending (two loop-back branches), justified; everything else clean | Yes |
| P07 | Review, publish, snapshots | Rejected | Not done by this skill | Yes |

No "No" answers.

Outcome: all steps approved. P06 and P07 accepted despite the Rejected tag.

Plan check: approved

## Review — round 1 — 2026-10-15 16:31

Plan check approved earlier the same day (P06, P07 accepted despite Rejected tag), so the plan is not in this form. Changes merged, not published.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC2.1 flow-layout.cs: 0 other diagonals, 0 crossings, 0 overlaps | Rejected | 1 justified crossing in DaySheet_FillPending (two loop-back branches); rest clean | Yes |
| A02 | AC2.2 Main path down, If False / Switch branches horizontal | Rejected | Documented deviation: the shorter branch leaves (not always False); all 45 side branches on the same row | Yes |
| A03 | AC2.3 Ending branches on their own End | Approved | 0 shared End nodes; 7 added | Yes |
| A04 | AC2.4 For Each body right, continuation below | Rejected | 17/20 comply; GetKPIs, OnClick_SaveDay, OpenDayEditor (untouched) have the continuation on the last body row | Yes |
| A05 | AC2.5 Merge diagonals justified | Approved | 3 merges in implementation notes | Yes |
| A06 | AC2.6 Only positions and End nodes changed | Approved | Pre-TT2 snapshots vs live identical apart from positions, connector notation, End nodes | Yes |
| A07 | AC2.7 Behaviour unchanged | Approved | No logic change; verify at runtime | Yes |
| A08 | AC2.8 0 errors | Approved | 0 errors | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Modified | Logic › Server Actions › DaySheet_Save | New | Yes |
| C02 | Modified | Logic › Server Actions › Bootstrap | New | Yes |
| C03 | Modified | Logic › Server Actions › DaySheet_FillPending | New | Yes |
| C04 | Modified | Logic › Server Actions › DaySheet_Clear | New | Yes |
| C05 | Modified | Logic › Server Actions › Calendar_GetMonth | New | Yes |
| C06 | Modified | Logic › Server Actions › DoLogout | New | Yes |
| C07 | Modified | Logic › Server Actions › Holiday_CreateIfMissing | New | Yes |
| C08 | Modified | Logic › Server Actions › DaySheet_GetDetail | New | Yes |
| C09 | Modified | Logic › Server Actions › Calendar_GetMonthSummary | New | Yes |
| C10 | Modified | Logic › Server Actions › Project_GetAssignedToUser | New | Yes |
| C11 | Modified | Interface › UI Flows › SheetBlocks › HoursCalendar | New | Yes |
| C12 | Modified | Interface › UI Flows › SheetBlocks › TotalHoursMonth | New | Yes |
| C13 | Modified | Interface › UI Flows › SheetBlocks › DayStatusKPIs | New | Yes |
| C14 | Modified | Other › queries/layout-flow.cs, queries/flow-layout.cs, docs/conventions.md | New | Yes |

### Conventions

| ID | Item | Tag | Reason | Answer |
|---|---|---|---|---|
| N01 | DaySheet_FillPending crossing | Rejected | Justified (two loop-back branches) | Yes |
| N02 | DaySheet_Save merge | Rejected | Justified (rejoin before CreateOrUpdateDaySheet) | Yes |
| N03 | Bootstrap merges | Rejected | Justified (lookup-or-create) | Yes |

No "No" answers. Accepted despite Rejected tag: A01, A02, A04, N01, N02, N03.

Outcome: all rows approved in round 1. Follow-up noted: For Each layout in GetKPIs, OnClick_SaveDay and OpenDayEditor.
