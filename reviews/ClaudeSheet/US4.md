# Review: ClaudeSheet · US4 (WeeklyProjectHours block)

## Implementation plan check — round 1 — 2026-10-15 17:03

Implemented in 5 `applyModelApiCode` saves, merged with `omlMerge` (`merged: true`). Live after merge: identical to the validated state apart from the default container Gap written as null; 0 validation errors; all 7 US4 flows pass queries/flow-layout.cs.

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P01 | Server action (ReferenceDate), week bounds | Approved | ProjectHours_GetWeekly (name approved by the user instead of GetWeeklyProjectHours) | Yes |
| P02 | 7 WeekDayHeader items | Approved | 'More days?' loop; DayLabel renamed DayName (Text), DateLabel added | Yes |
| P03 | User's projects: assignments overlapping UNION entries | Approved | SQL node GetWeekProjects | Yes |
| P04 | Entries grouped by ProjectId and SheetDate | Approved | Aggregate GetWeekEntries, sum of Minutes | Yes |
| P05 | Holidays and DaySheets (DayOff) for the "-" rule | Rejected | Documented deviation: day types from Calendar_GetMonth (start and end month) | Yes |
| P06 | Rows with 7 cells, ShowDash, TotalMinutes | Approved | For Each GetWeekProjects + 'More cells?' loop | Yes |
| P07 | DayTotals and GrandTotalMinutes | Approved | Accumulated in 'Dash rule and totals' | Yes |
| P08 | Sort by client, project | Approved | ORDER BY in GetWeekProjects | Yes |
| P09 | Block, inputs, event | Approved | SheetBlocks › WeeklyProjectHours | Yes |
| P10 | CurrentReferenceDate, OnParametersChanged | Approved | OnInitialize + OnParametersChanged | Yes |
| P11 | Data action GetWeek | Approved | Calls ProjectHours_GetWeekly(CurrentReferenceDate) | Yes |
| P12 | Header row | Approved | 9-column header; verify at runtime | Yes |
| P13 | Arrows ±7, refresh, OnWeekChanged | Approved | OnClick_PreviousWeek / OnClick_NextWeek | Yes |
| P14 | Rounded dark cards, 9-column grid | Approved | --sheet-surface-dark (user's choice) | Yes |
| P15 | Cell expression, grey weekends | Approved | RowCells item | Yes |
| P16 | Total per day row + grand total | Approved | DayTotals list + GrandTotalMinutes | Yes |
| P17 | Today highlight | Approved | is-today → --sheet-accent | Yes |
| P18 | Empty state | Approved | HasRows If, False branch | Yes |
| P19 | Responsive ScrollableArea + sticky column | Approved | WeekScroll + media query; verify at runtime | Yes |
| P20 | Accessibility roles | Approved | table/row/columnheader/rowheader/cell | Yes |
| P21 | Publish and test | Rejected | Not done by this skill | Yes |

No "No" answers.

Outcome: all steps approved. P05 and P21 accepted despite the Rejected tag.

Plan check: approved

## Review — round 1 — 2026-10-15 17:08

Plan check approved earlier the same day (P05, P21 accepted despite Rejected tag), so the plan is not in this form. Changes merged, not published.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC4.1 Week 12/10–18/10, today in red | Approved | Date_GetWeekStart (Monday); is-today → accent. Verify at runtime: server date is 15/10 | Yes |
| A02 | AC4.2 Mockup row 0:00 ×3, - ×4, total 0:00 | Approved | Confirmed days not dash. Verify at runtime: 15/10 (today, pending) shows 0:00 | Yes |
| A03 | AC4.3 8:00 + 7:30 → row 15:30, day totals | Approved | GetWeekEntries sum → cells, Row.TotalMinutes, DayTotals | Yes |
| A04 | AC4.4 Two projects 4:00 → day total 8:00 | Approved | DayTotals / GrandTotalMinutes accumulate | Yes |
| A05 | AC4.5 Holiday / day off → "-" | Approved | Calendar_GetMonth day types + ShowDash rule | Yes |
| A06 | AC4.6 Weekend "-" grey | Approved | Weekend dash + is-weekend; verify at runtime | Yes |
| A07 | AC4.7 Past working day → 0:00 | Approved | Pending/Confirmed not dash | Yes |
| A08 | AC4.8 Right arrow → 19/10, OnWeekChanged(19/10) | Approved | OnClick_NextWeek | Yes |
| A09 | AC4.9 Left arrow → 28/09–04/10 | Approved | StartMonth/EndMonth across the boundary | Yes |
| A10 | AC4.10 Ended assignment with entries shown | Approved | GetWeekProjects entries half | Yes |
| A11 | AC4.11 Empty state | Approved | HasRows False branch | Yes |
| A12 | AC4.12 ReferenceDate 28/10 → 26/10–01/11 | Approved | OnParametersChanged | Yes |
| A13 | AC4.13 RefreshKey reloads | Approved | OnParametersChanged refresh; US5 must feed OnWeekChanged back into ReferenceDate | Yes |
| A14 | AC4.14 Other users never shown | Approved | GetUserId filters, role gate | Yes |
| A15 | AC4.15 Tablet: horizontal scroll, sticky column | Approved | ScrollableArea + media query; verify at runtime | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Modified | Data › Structures › WeekDayHeader | New | Yes |
| C02 | Modified | Data › Structures › WeeklyCell | New | Yes |
| C03 | Modified | Data › Structures › WeeklyHours | New | Yes |
| C04 | Created | Logic › Server Actions › ProjectHours_GetWeekly | New | Yes |
| C05 | Created | Interface › UI Flows › SheetBlocks › WeeklyProjectHours | New | Yes |
| C06 | Modified | Interface › Themes › ClaudeSheet (US4 section) | New | Yes |

### Conventions

| ID | Item | Tag | Evidence | Answer |
|---|---|---|---|---|
| N01 | No convention violations | Approved | Names, 7 flows pass flow-layout.cs, role check, CSS in theme | Yes |

No "No" answers. No rows accepted despite a Rejected tag.

Outcome: all rows approved in round 1. Note for US5: wire WeeklyProjectHours.OnWeekChanged back into its ReferenceDate.
