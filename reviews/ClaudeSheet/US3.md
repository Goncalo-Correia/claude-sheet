# Review: ClaudeSheet · US3 (DayStatusKPIs block)

## Implementation plan check — round 1 — 2026-10-15 14:22

Implemented in 3 `applyModelApiCode` saves, merged with `omlMerge` (`merged: true`). Live validation after the merge: 0 errors.

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P01 | Create block DayStatusKPIs in SheetBlocks with inputs Year, Month (mandatory) and RefreshKey (optional) | Approved | SheetBlocks › DayStatusKPIs; Year/Month Integer mandatory, RefreshKey Integer optional default 0 | Yes |
| P02 | Data action GetKPIs calling GetMonthSummary and returning {CalendarDayTypeId, Label, CssClass, Count} from CalendarDayType where ShowInKPI = True ordered by Order | Approved | GetMonthSummary → aggregate GetKPIDayTypes (filter ShowInKPI, sort Order) → For Each → ListAppend; output KPIs (List of new structure DayStatusKPI) + ErrorMessage | Yes |
| P03 | Map the counts per type | Approved | "Build KPI" assign, nested If on Entities.CalendarDayType → GetMonthSummary.Summary.*Count | Yes |
| P04 | Label when Count = 1, PluralLabel otherwise | Approved | KPI.Label = If(KPI.Count = 1, Label, PluralLabel); PluralLabel of Confirmed/Pending changed to "Confirmed"/"Pending" (user's decision) | Yes |
| P05 | OnParametersChanged refreshes GetKPIs | Approved | Refresh Data GetKPIs | Yes |
| P06 | List with one card per KPI: square (CssClass) + Count + " " + label | Approved | KPIList → KPICard → square + expression | Yes |
| P07 | Loading state (skeleton cards) | Approved | If IsLoaded; 4 shimmer cards in KPISkeleton; verify at runtime | Yes |
| P08 | aria-label per card | Approved | role="group" + aria-label "7 days confirmed in October 2026" etc. | Yes |
| P09 | Theme variables; vertical stack on desktop, 2x2 grid below 1024px | Approved | Theme .sheet-kpis*, --sheet-surface-dark (user's decision); verify at runtime | Yes |
| P10 | Publish and test with the mockup scenario and edge cases | Rejected | Not done: this skill never publishes; publish and runtime tests follow the review | Yes |

No "No" answers.

Outcome: all steps approved. P10 accepted despite the Rejected tag (publish and tests happen after the acceptance-criteria review).

Plan check: approved

## Review — round 1 — 2026-10-15 14:31

Plan check approved on 2026-10-15 14:22 (P10 accepted despite Rejected tag), so the plan is not in this form. Changes merged (`merged: true`), not published.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC3.1 Mockup scenario: 7 Confirmed, 5 Days off, 1 Holiday, 0 Pending, in that order | Approved | Sort by Order; Label/PluralLabel; counts from GetMonthSummary. Verify at runtime: server date is 15/10, so 15/10 is Pending unless it has data | Yes |
| A02 | AC3.2 Square colour = calendar colour | Approved | CssClass + .sheet-kpis overrides identical to .sheet-cal; verify at runtime | Yes |
| A03 | AC3.3 "1 Day off" / "0 Days off" | Approved | If(Count = 1, Label, PluralLabel) | Yes |
| A04 | AC3.4 3 past weekdays without data → Pending 3 | Approved | GetMonthCalendar classification → PendingCount | Yes |
| A05 | AC3.5 Draft counts as Pending | Approved | Only DayOff/Confirmed DayStatus match; Draft falls to Pending | Yes |
| A06 | AC3.6 Future month: Pending 0, Days off / Holidays counted | Approved | Holiday/DayOff checked before Future | Yes |
| A07 | AC3.7 Future Day off on 30/10 counted | Approved | DayOff before Future | Yes |
| A08 | AC3.8 Saturday holiday counted | Approved | Holiday checked first | Yes |
| A09 | AC3.9 RefreshKey increment updates counts | Approved | OnParametersChanged → Refresh Data GetKPIs; parent wiring in US5; verify at runtime | Yes |
| A10 | AC3.10 Weekday sum identity | Approved | One type per day; SaveDaySheet rejects weekends | Yes |
| A11 | AC3.11 Other users' data isolated | Approved | DaySheet.UserId = GetUserId() filters; role check | Yes |
| A12 | AC3.12 Phone 2x2 grid | Approved | .phone/.tablet + max-width 1023px grid; verify at runtime | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Modified | Data › Database › CalendarDayType (PluralLabel Confirmed/Pending) | New | Yes |
| C02 | Created | Data › Structures › DayStatusKPI | New | Yes |
| C03 | Created | Interface › UI Flows › SheetBlocks › DayStatusKPIs | New | Yes |
| C04 | Modified | Interface › Themes › ClaudeSheet (US3 stylesheet section) | New | Yes |

No "No" answers. No rows accepted despite a Rejected tag.

Outcome: all rows approved in round 1.
