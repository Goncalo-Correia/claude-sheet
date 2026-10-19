# US3: [Block] Monthly KPIs: Confirmed, Days off, Holidays, Pending

- **Status:** Published
- **Published:** 2026-10-15 14:34 (manual 1-Click Publish from Service Studio)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-15
- **Depends on:** TT0
- **Trello:** https://trello.com/c/pRJ6eRV5
- **Renamed in TT1 (2026-10-15):** GetMonthCalendar → Calendar_GetMonth, GetMonthSummary → Calendar_GetMonthSummary, GetKPIsOnAfterFetch → OnAfterFetch_GetKPIs. This file keeps the names as they were when the story was built.

## Story

**User story**
As a consultant
I want to see how many days of the selected month are confirmed, days off, holidays and pending
So that I immediately know how much of my timesheet is still missing.

**Context**
Right-hand widget of the MySheet screen: 4 stacked cards, each with a coloured square (which also acts as the calendar legend) and the text "<count> <label>".

**Functional rules** (selected month, logged-in user, same classification as the calendar)

* Confirmed: days classified as Confirmed (green)
* Days off: days classified as Day off, including future days (blue)
* Holidays: active holidays in the month (purple)
* Pending: past/today weekdays that are not holidays and have no Confirmed/DayOff DaySheet (orange); always 0 for future months
* Singular/plural labels: "1 Holiday" / "2 Holidays", "1 Day off" / "5 Days off" ("Confirmed" and "Pending" don't change)
* Cards are rendered from the static entity CalendarDayType where ShowInKPI = True, sorted by Order
* Counts must always match the colours displayed in the calendar
* Reloads when Year, Month or RefreshKey change

**Block contract**

* Name: DayStatusKPIs (UI flow SheetBlocks)
* Inputs: Year, Month (mandatory), RefreshKey (optional)
* Events: none

**UI**

* Cards: background --sheet-card, 12px radius, 16px padding, 16px gap
* 24px rounded square in the status colour + white text
* Desktop: vertical stack; tablet/phone: 2×2 grid

**Dependencies**
TT0 (GetMonthSummary, CalendarDayType)

## Acceptance criteria

- [ ] A01 · AC3.1 Given the mockup scenario (today 14/10/2026), When the block loads for October 2026, Then it shows "7 Confirmed", "5 Days off", "1 Holiday", "0 Pending" in that order
- [ ] A02 · AC3.2 Given each card, Then its square uses the same colour as the calendar (green, blue, purple, orange)
- [ ] A03 · AC3.3 Given exactly 1 day off, Then the label reads "1 Day off"; given 0 days off, "0 Days off"
- [ ] A04 · AC3.4 Given 3 past weekdays without data, Then Pending shows 3
- [ ] A05 · AC3.5 Given a past weekday with a Draft DaySheet, Then it is counted as Pending, not Confirmed
- [ ] A06 · AC3.6 Given a future month, Then Pending = 0 while Days off and Holidays still count the future records
- [ ] A07 · AC3.7 Given a Day off registered on 30/10 (future), Then it is counted in Days off for October
- [ ] A08 · AC3.8 Given a holiday on a Saturday, Then it is still counted in Holidays (the classification puts Holiday first)
- [ ] A09 · AC3.9 Given I confirm a pending day in the calendar and the parent increments RefreshKey, Then Confirmed increases by 1 and Pending decreases by 1 without a page reload
- [ ] A10 · AC3.10 Given any month, Then Confirmed + Days off + Holidays(on weekdays) + Pending + Future weekdays = number of weekdays in the month
- [ ] A11 · AC3.11 Given another user's data, Then it never affects my counts
- [ ] A12 · AC3.12 Given a phone viewport, Then the 4 cards are shown as a 2x2 grid

## Implementation plan

1. P01 · Create block DayStatusKPIs in UI flow SheetBlocks with inputs Year, Month (mandatory) and RefreshKey (optional)
2. P02 · Add data action GetKPIs calling GetMonthSummary(Year, Month) and returning a list of {CalendarDayTypeId, Label, CssClass, Count} built from the CalendarDayType records where ShowInKPI = True ordered by Order
3. P03 · Map the counts: Confirmed -> ConfirmedCount, DayOff -> DayOffCount, Holiday -> HolidayCount, Pending -> PendingCount
4. P04 · Apply the singular/plural rule: Label when Count = 1, PluralLabel otherwise
5. P05 · Add OnParametersChanged handler that refreshes GetKPIs
6. P06 · Render a List with one card per KPI: coloured square (CssClass) + Count + " " + label
7. P07 · Add a loading state (skeleton cards) while data is being fetched
8. P08 · Add aria-label to each card, e.g. "7 days confirmed in October 2026"
9. P09 · Style the cards with theme variables; vertical stack on desktop, 2x2 grid below 1024px
10. P10 · Publish and test with the mockup scenario and edge cases

## Implementation notes

- Implemented 2026-10-15 in **3 `applyModelApiCode` saves** (static data + structure + block logic; widget tree; theme CSS), merged with one `omlMerge`. The Service Studio MCP tools didn't load into the Claude session, so the calls went to the same endpoint over raw HTTP (same tools, same session token).
- **Decisions confirmed with the user before building:**
  - The `PluralLabel` of the CalendarDayType records **Confirmed** and **Pending** changed from "Confirmed days" / "Pending days" to "Confirmed" / "Pending". With that, P04 (`Label` when Count = 1, `PluralLabel` otherwise) produces AC3.1's "7 Confirmed" / "0 Pending". `PluralLabel` was not used anywhere else.
  - The cards use `--sheet-surface-dark` instead of `--sheet-card`. `--sheet-card` is white, and white text on it would be invisible. This matches the US1 and US2 blocks.
- **New structure `DayStatusKPI`** {CalendarDayTypeId (CalendarDayType Identifier), Label, CssClass, Count}: the record type of `GetKPIs.KPIs`.
- **GetKPIs (P02–P04):** GetMonthSummary(Year, Month) → aggregate `GetKPIDayTypes` (CalendarDayType, filter `ShowInKPI = True`, sort by `Order`) → For Each: the `Build KPI` assign maps the count per Id and picks Label/PluralLabel → ListAppend to `KPIs`. An AllExceptions handler sets `ErrorMessage`, and `GetKPIsOnAfterFetch` shows it as an error message (same pattern as US1). The role check is done by GetMonthSummary (`CheckSheetUserRole()`).
- **Counts** come from GetMonthSummary, which classifies days with GetMonthCalendar (the calendar's own source), so the KPIs always match the calendar colours. Holiday on a Saturday, future day off and Draft = Pending are all handled by that classification. No new counting logic.
- **Widget tree (P06–P08):** `KPICards` (class `sheet-kpis`, `aria-busy` while loading) → If `IsLoaded` (`GetKPIs.IsDataFetched`). True: List `KPIList` → `KPICard` (`role="group"`, aria-label like "7 days confirmed in October 2026", "1 holiday in October 2026", "5 days off in …") with the square (`sheet-kpis__square` + CssClass, `aria-hidden`) and the text `Count + " " + Label`. False: `KPISkeleton`, 4 static shimmer cards (`aria-hidden`).
- **CSS (P09)** is in the ClaudeSheet theme, after the US2 section, starting with the `.sheet-kpis-start { }` guard rule. Inside `.sheet-kpis`, `--sheet-dayoff` / `--sheet-holiday` are redefined to the calendar's blue/purple (the same overrides `.sheet-cal` uses), and Pending is a filled orange square, so the squares match the calendar (AC3.2). The 2×2 grid applies below 1024px (media query plus `.tablet` / `.phone`).
- **Theme edit:** the sidecar reports `InvalidIconLibrary` on the theme stylesheet edit; ignored as documented (`IconLibrary` left null).
- **P10 (publish and test)** is not done here: this skill does not publish. It was tagged Rejected in the plan check and accepted by the user as is; publishing follows the review with `outsystems-mcp-publish`.
- **Open warning:** "RefreshKey is never used" (same as TotalHoursMonth and HoursCalendar). The input is there only to trigger OnParametersChanged.

## Trail

- Changes: `changes/US3-ClaudeSheet.md`: save 1 (static data, structure, block logic), 2 (widgets), 3 (theme CSS). Written by Claude from the applied lambdas: the publish was manual and the MCP calls went over raw HTTP, so the publish hook didn't fire.
- Review: `reviews/ClaudeSheet/US3.md` (plan check round 1 and review round 1: all approved)
- Published: see `PUBLISHES.md` (2026-10-15 14:34, success (manual))
- Snapshots: `snapshots/ClaudeSheet/` refreshed 2026-10-15 (targeted)
