# Review: ClaudeSheet · TT1 (apply the naming and flow layout conventions)

## Implementation plan check — round 1 — 2026-10-15 15:09

Implemented in 6 `applyModelApiCode` saves, merged with `omlMerge` (`merged: true`). Live after merge: names, calls and bindings as validated, overlap probe "no overlaps" (64 flows), 0 validation errors.

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P01 | Inventory, overlap probe, confirm rename list | Approved | Live inventory matched the card; rename dry-run safe; list confirmed by the user | Yes |
| P02 | Rename 9 reusable server actions | Approved | Logic › Server Actions read back, no suffixes | Yes |
| P03 | Rename 2 client actions | Approved | Minutes_Format, Date_GetWeekStart | Yes |
| P04 | Rename On After Fetch handlers | Approved | OnAfterFetch_GetSummary, OnAfterFetch_GetKPIs, still bound | Yes |
| P05 | Rename 9 HoursCalendar click handlers | Approved | OnClick_*; widget bindings intact | Yes |
| P06 | Lifecycle handlers and helper | Approved | OnInitialize, OnReady bound; FinishDaySave helper | Yes |
| P07 | Split RecalcEditorTotal | Approved | OnChange_EditorTime bound to both dropdowns → helper RecalcEditorTotal | Yes |
| P08 | Re-lay out DaySheet_Save and OnInitialize | Approved | Standard grid; connectors verified unchanged | Yes |
| P09 | Overlap probe, validation, merge | Approved | No overlaps (64 flows), 0 errors | Yes |
| P10 | Review, publish, refresh snapshots | Rejected | Not done by this skill | Yes |
| P11 | Update old names in cards, stories, docs | Rejected | US4 items and ADR 0002 updated; finished stories got "Renamed in TT1" notes instead of rewrites; review logs untouched | Yes |

No "No" answers.

Outcome: all steps approved. P10 and P11 accepted despite the Rejected tag.

Plan check: approved

## Review — round 1 — 2026-10-15 15:34

Plan check approved earlier the same day (P10, P11 accepted despite Rejected tag), so the plan is not in this form. Changes merged (`merged: true`), not published.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC1.1 Server actions <Entity>_<Verb> except DoLogin, DoLogout, Bootstrap | Approved | Logic › Server Actions read back | Yes |
| A02 | AC1.2 Client actions Minutes_Format, Date_GetWeekStart | Approved | Logic › Client Actions | Yes |
| A03 | AC1.3 Event-bound block actions follow <Event>_<Target> | Rejected | True for SheetBlocks; template blocks keep SkipToContentOnClick (LayoutBase/SideMenu/TopMenu), OnClick (MenuIcon, ApplicationTitle), HideMenu (Menu), ClientLogin/ClientLogout/GetUsername (UserInfo) | Yes |
| A04 | AC1.4 No action both bound and called; helpers verb + noun | Approved | Only calls: OpenDayEditor, RecalcEditorTotal, FinishDaySave (helpers) | Yes |
| A05 | AC1.5 Overlap probe: no overlaps | Approved | 64 flows, no overlaps | Yes |
| A06 | AC1.6 0 errors, call sites renamed | Approved | 0 errors; 8 Description texts still mention old names (accepted as is) | Yes |
| A07 | AC1.7 Only names and positions changed | Rejected | Normalised diff vs pre-TT1 snapshots: identical except generator numbering, plus the planned P07 split (OnChange_EditorTime + 2 rebound OnChange events) | Yes |
| A08 | AC1.8 Behaviour unchanged after publish | Approved | No logic change, 17 bindings intact; verify at runtime | Yes |
| A09 | AC1.9 Reviews check conventions | Approved | Code-review Conventions section; this review is its first run | Yes |
| A10 | AC1.10 Snapshots: old names gone, new present | Rejected | Done in the publish step after this review | Yes |
| A11 | AC1.11 Cards and docs use new names | Approved | US4 items, ADR 0002; stories with rename notes (P11) | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Modified | Logic › Server Actions › DaySheet_Clear (was ClearDaySheet) | New | Yes |
| C02 | Modified | Logic › Server Actions › DaySheet_FillPending (was FillPendingDays) | New | Yes |
| C03 | Modified | Logic › Server Actions › DaySheet_GetDetail (was GetDaySheetDetail) | New | Yes |
| C04 | Modified | Logic › Server Actions › DaySheet_Save (was SaveDaySheet; re-laid out) | New | Yes |
| C05 | Modified | Logic › Server Actions › Calendar_GetMonth (was GetMonthCalendar) | New | Yes |
| C06 | Modified | Logic › Server Actions › Calendar_GetMonthSummary (was GetMonthSummary) | New | Yes |
| C07 | Modified | Logic › Server Actions › Project_GetAssignedToUser (was GetUserProjects) | New | Yes |
| C08 | Modified | Logic › Server Actions › Minutes_Format (was FormatMinutes) | New | Yes |
| C09 | Modified | Logic › Server Actions › Date_GetWeekStart (was GetWeekStart) | New | Yes |
| C10 | Modified | Logic › Client Actions › Minutes_Format (was FormatMinutes) | New | Yes |
| C11 | Modified | Logic › Client Actions › Date_GetWeekStart (was GetWeekStart) | New | Yes |
| C12 | Modified | Interface › UI Flows › SheetBlocks › TotalHoursMonth | New | Yes |
| C13 | Modified | Interface › UI Flows › SheetBlocks › HoursCalendar | New | Yes |
| C14 | Modified | Interface › UI Flows › SheetBlocks › DayStatusKPIs | New | Yes |

### Conventions

| ID | Item | Tag | Evidence | Answer |
|---|---|---|---|---|
| N01 | No convention violations | Approved | Names of all ledger elements, overlap probe, no other applicable rule | Yes |

No "No" answers. Accepted despite Rejected tag: A03, A07, A10.

Outcome: all rows approved in round 1.
