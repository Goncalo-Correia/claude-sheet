# US4: [Block] Weekly project hours with totals per day and per week

- **Status:** Published
- **Published:** 2026-10-15 17:09 (manual 1-Click Publish from Service Studio)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-15
- **Depends on:** TT0, US2
- **Trello:** https://trello.com/c/KWjg4EYy

## Story

### User story

**As a** consultant
**I want** to see, for one week, the hours I logged per project and per day, with totals per day and per week
**So that** I can review how my time was split across projects and spot missing hours.

### Context

Bottom widget of the MySheet screen: "Project daily hours" label, previous/next week arrows, 7 day columns (day name + dd/MM, today in red), a "Total per week" column, one row per project and a final "Total per day" row.

### Functional rules

- The week runs **Monday to Sunday** and contains the ReferenceDate input (default: today)
- **Rows**: projects assigned to the user during the week, plus any project with entries that week, sorted by client then project
  - Row label: client name in bold, "Project name | Code" below
- **Cells**: hours in "H:MM" format (e.g. "7:30", "0:00")
  - Show "-" when the project has no entry AND the day is in the future, a weekend, a holiday or a day off
  - Show "0:00" for past/today working days without an entry for that project
- **Total per week** (last column): sum of the row
- **Total per day** (last row): sum of the column, using the same "-" rule (all rows "-" → "-")
- **Grand total** (bottom-right): sum of the week
- Header: day name (Monday…Sunday) + "dd/MM"; today in the accent colour (red); weekend headers in grey
- Arrows move ±7 days and raise **OnWeekChanged(WeekStartDate)**
- Empty state: "No projects assigned for this week"
- Reloads when ReferenceDate or RefreshKey change

### Block contract

- Name: **WeeklyProjectHours** (UI flow SheetBlocks)
- Inputs: ReferenceDate (Date, default CurrDate()), RefreshKey (optional)
- Events: OnWeekChanged(WeekStartDate)

### Server action

- GetWeeklyProjectHours(ReferenceDate) → WeeklyHours

### Out of scope

- Editing hours directly in the table (done in the calendar, US2)

### Dependencies

TT0; US2 for real data (can be tested with seed data)

## Acceptance criteria

- [ ] A01 · AC4.1 Given ReferenceDate = 14/10/2026, When the block loads, Then the columns are Monday 12/10 to Sunday 18/10 and "Wednesday 14/10" is shown in red
- [ ] A02 · AC4.2 Given the mockup scenario (12, 13, 14 confirmed without hours), Then the ICNF / "Resina e Pinha | RUBUS" row shows 0:00, 0:00, 0:00, -, -, -, - and total 0:00
- [ ] A03 · AC4.3 Given 8:00 on Mon and 7:30 on Tue for RUBUS, Then the row shows 8:00 and 7:30, the row total shows 15:30 and the "Total per day" row shows 8:00 and 7:30
- [ ] A04 · AC4.4 Given 2 projects with 4:00 each on Monday, Then "Total per day" for Monday shows 8:00 and the grand total includes both
- [ ] A05 · AC4.5 Given a week with a holiday (05/10) or a day off (08/10) without entries, Then those cells show "-"
- [ ] A06 · AC4.6 Given weekend days without entries, Then they show "-" with grey styling
- [ ] A07 · AC4.7 Given a past working day without an entry for a project, Then the cell shows "0:00"
- [ ] A08 · AC4.8 Given I click the right arrow, Then the week 19/10–25/10 is shown and OnWeekChanged is raised with 19/10/2026
- [ ] A09 · AC4.9 Given I click the left arrow on the week of 05/10, Then the week 28/09–04/10 is shown, crossing the month boundary correctly
- [ ] A10 · AC4.10 Given a project whose assignment ended last month but that has entries this week, Then the project row is still shown
- [ ] A11 · AC4.11 Given no assigned projects and no entries, Then "No projects assigned for this week" is displayed
- [ ] A12 · AC4.12 Given the parent changes ReferenceDate to 28/10/2026, Then the block shows the week 26/10–01/11
- [ ] A13 · AC4.13 Given the parent increments RefreshKey after a save, Then the new hours appear without a page reload
- [ ] A14 · AC4.14 Given another user's entries, Then they are never shown
- [ ] A15 · AC4.15 Given a tablet viewport, Then the table scrolls horizontally and the project column stays visible

## Implementation plan

1. P01 · Create server action GetWeeklyProjectHours(ReferenceDate): WeekStart = Date_GetWeekStart(ReferenceDate), WeekEnd = WeekStart + 6
2. P02 · Build the 7 WeekDayHeader items (DayName in English, DateLabel dd/MM, IsToday against server CurrDate(), IsWeekend)
3. P03 · Fetch the user's projects: active assignments overlapping WeekStart..WeekEnd UNION projects with TimeEntries in the week
4. P04 · Fetch TimeEntries of GetUserId() for the week grouped by ProjectId and SheetDate (single aggregate, sum of Minutes)
5. P05 · Fetch holidays and DaySheets (DayOff) of the week to apply the "-" rule
6. P06 · Build one WeeklyProjectRow per project with 7 WeeklyCells (Minutes, ShowDash) and TotalMinutes
7. P07 · Build DayTotals (7 cells, ShowDash when every row is dash for that day) and GrandTotalMinutes
8. P08 · Sort rows by ClientName, ProjectName
9. P09 · Create block WeeklyProjectHours with inputs ReferenceDate, RefreshKey and event OnWeekChanged
10. P10 · Add local variable CurrentReferenceDate initialised from the ReferenceDate input; OnParametersChanged syncs it and refreshes the data
11. P11 · Add data action GetWeek calling GetWeeklyProjectHours(CurrentReferenceDate)
12. P12 · Build the header row: label "Project daily hours", left arrow, 7 day headers, right arrow, "Total per week"
13. P13 · Implement the arrows: CurrentReferenceDate +/- 7 days, refresh GetWeek, trigger OnWeekChanged(WeekStart)
14. P14 · Render the project rows as rounded dark cards (--sheet-card) with a 9-column grid: label, 7 cells, total
15. P15 · Render cells with If(ShowDash, "-", Minutes_Format(Minutes, ":")); weekend cells in grey
16. P16 · Render the "Total per day" row with the same grid and the grand total in the last column
17. P17 · Highlight today's header (day name and date) with var(--sheet-accent)
18. P18 · Add the empty state when Rows is empty
19. P19 · Responsive: below 1024px wrap the table in a horizontal ScrollableArea and keep the project column sticky
20. P20 · Accessibility: use role="table"/"row"/"cell" with column headers for screen readers
21. P21 · Publish and test every acceptance criterion

## Implementation notes

- Implemented 2026-10-15 in **5 `applyModelApiCode` saves**:
  1. structures and server action;
  2. a type fix on `WeekDayHeader.DayName`;
  3. the layout of the server action;
  4. the block, with logic and widgets in one save because they're the same element;
  5. the theme CSS.

  Calls went over raw HTTP, as in TT1 and TT2.
- **Decisions confirmed with the user before building:**
  - The server action is **`ProjectHours_GetWeekly`** (plan: GetWeeklyProjectHours) to follow `<Entity>_<Verb>[<Qualifier>]`.
  - The project cards use **`--sheet-surface-dark`** (plan: `--sheet-card`, which is white; see ADR 0003).
- **TT0 structures extended** (they weren't used by any story yet):
  - `WeekDayHeader.DayLabel` was renamed to `DayName`. Service Studio inferred a Date type from the new name, so save 2 sets it back to Text (length 20).
  - Added `WeekDayHeader.DateLabel` (Text 10) and `WeeklyCell.ShowDash` (Boolean).
  - Renamed `WeeklyHours.TotalMinutes` to `GrandTotalMinutes`, and added `WeeklyHours.DayTotals` (List of WeeklyCell). `WeeklyHours.Rows` already existed.
- **ProjectHours_GetWeekly (P01–P08):**
  - Role gate (`CheckSheetUserRole()`). The week runs from `Date_GetWeekStart(ReferenceDate)` to +6.
  - **Day headers:** `Calendar_GetMonth` is called for the week's start month and end month (the `StartMonth` and `EndMonth` nodes), and each header takes its DayTypeId, CssClass and IsEditable from there. The "-" rule therefore uses the calendar's own classification: Future, Weekend, Holiday or DayOff.
  - **Projects (P03, P08):** a SQL node, `GetWeekProjects`, because "assigned OR has entries", de-duplicated and sorted by client then project, can't be written as one aggregate. The assignment half mirrors `Project_GetAssignedToUser`: active assignment overlapping the week (open end = NullDate or NULL), project and client active. The entries half includes any project with the user's TimeEntries in the week, even if its assignment ended (AC4.10).
  - **Minutes (P04):** one grouped aggregate, `GetWeekEntries` (TimeEntry ⋈ DaySheet, the user, the week; grouped by ProjectId and SheetDate; sum of Minutes).
  - **Rows (P06, P07):** each row loops 7 cells, matching minutes by project and day. ShowDash = no minutes AND a dash day type. Row, day and grand totals accumulate in the same pass. A DayTotals cell starts as a dash and stays one only while every row is a dash that day.
- **Deviation, P05:** holidays and day-off DaySheets aren't fetched by the action's own aggregates; they come through `Calendar_GetMonth` (above). The effect is the same, and the weekly grid's "-" days always match the calendar's colours.
- **WeeklyProjectHours block (P09–P20):**
  - **Inputs and state:** ReferenceDate (default `CurrDate()`), RefreshKey (default 0), the event `OnWeekChanged(WeekStartDate)` and the local `CurrentReferenceDate`. `OnInitialize` sets it from the input, falling back to today when empty. `OnParametersChanged` syncs it and refreshes `GetWeek`.
  - **Data:** data action `GetWeek` (`ProjectHours_GetWeekly(CurrentReferenceDate)`) with an AllExceptions handler, and `OnAfterFetch_GetWeek` showing errors, with one End per branch.
  - **Arrows:** `OnClick_PreviousWeek` / `OnClick_NextWeek` set `CurrentReferenceDate` to the week start ±7 days, refresh `GetWeek` and trigger `OnWeekChanged(CurrentReferenceDate)`, the new Monday.
  - **Grid:** 9 columns (CSS grid). Lists use `display: contents` so their items sit in the grid. The header row has "Project daily hours" and the previous arrow in column 1, the 7 day headers (`is-today` in `--sheet-accent`, `is-weekend` grey), and the next arrow plus "Total per week" in column 9.
  - **Rows:** each project row shows the client in bold, then "Project | Code", the 7 cells (`If(ShowDash, "-", Minutes_Format(Minutes, ":"))`, grey on weekends) and the row total. A final "Total per day" row shows the DayTotals and the grand total.
  - **Empty state:** If `HasRows` (`Rows.Length > 0 or not IsDataFetched`) is false, it shows "No projects assigned for this week".
  - **P19:** the table sits in an OutSystems UI `ScrollableArea` (horizontal by default). Below 1024px the table gets `min-width: 960px` and the project column is `position: sticky`.
  - **P20:** role="table" (with an aria-label naming the week), "row", "columnheader", "rowheader" and "cell"; arrows have aria-labels; the root has `aria-busy` while loading. Verify with a screen reader.
- **Flow layout gate:** `ProjectHours_GetWeekly` was laid out with `checks/layout-flow.md`, and the block's actions were built vertically with one End per branch. The report on all 7 US4 flows shows 0 other diagonals, 0 crossings and 0 overlaps. Not touched: the 3 For Each layouts from TT2's follow-up (TT3).
- **Theme edit:** the expected `InvalidIconLibrary` sidecar error is ignored as documented. Warnings: "RefreshKey never used", as on the other blocks.
- **P21 (publish and test)** is not done here.

## Trail

- Changes: `changes/US4-ClaudeSheet.md`: save 1 (structures + ProjectHours_GetWeekly), 2 (DayName back to Text), 3 (layout of ProjectHours_GetWeekly), 4 (WeeklyProjectHours block), 5 (theme CSS). Written by Claude from the applied lambdas (manual publish, MCP over raw HTTP).
- Review: `reviews/ClaudeSheet/US4.md` (plan check round 1, review round 1: all approved; P05 and P21 accepted despite Rejected tag)
- Published: see `PUBLISHES.md` (2026-10-15 17:09, success (manual))
- Snapshots: `snapshots/ClaudeSheet/` refreshed 2026-10-15 (targeted)
- Note for US5: wire WeeklyProjectHours.OnWeekChanged back into its ReferenceDate, otherwise a RefreshKey reload jumps back to the parent's week
