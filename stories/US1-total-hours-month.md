# US1: [Block] Total hours worked per month

- **Status:** Published
- **Published:** 2026-10-14 17:01 (manual 1-Click Publish from Service Studio)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-14
- **Depends on:** TT0 (GetMonthSummary, FormatMinutes)
- **Renamed in TT1 (2026-10-15):** GetMonthSummary → Calendar_GetMonthSummary, FormatMinutes → Minutes_Format, GetSummaryOnAfterFetch → OnAfterFetch_GetSummary. This file keeps the names as they were when the story was built.

## Story

**User story**
As a consultant
I want to see the total hours I logged in the selected month, and how that compares with the hours expected
So that I know at a glance whether my timesheet is on track.

**Context**
Left-hand widget of the MySheet screen (see mockup): a progress circle with the label "Total <Month>", the total in "XhYY" format, and the sentence "You currently worked X hours in N day(s)" below it.

**Functional rules**

* Total: sum of TimeEntry.Minutes of the logged-in user for all DaySheets in the selected month (Draft and Confirmed)
* Expected: (weekdays of the month − holidays − days off) × DailyExpectedMinutes
* Progress: Total / Expected × 100, capped at 100%; 0% when Expected = 0
* N day(s): days elapsed in the selected month up to today (current month = today's day number; past month = number of days in the month; future month = 0)
* X hours in the sentence: total in decimal hours with up to 2 decimals (e.g. 37.5)
* The block reloads when Year, Month or RefreshKey change

**Block contract**

* Name: TotalHoursMonth (UI flow SheetBlocks)
* Inputs: Year (Integer, mandatory), Month (Integer, mandatory), RefreshKey (Integer, optional)
* Events: none

**UI**

* OutSystems UI ProgressCircle, dark track (#2A2A2A), progress colour = --sheet-confirmed
* Label "Total October" (small grey), value "0h00" (large, bold, white), sentence centred below in small grey text
* Skeleton/loading state while the data action runs

**Dependencies**
TT0 (GetMonthSummary, FormatMinutes)

## Acceptance criteria

- [x] A01 · AC1.1 Given Year=2026, Month=10 and no time entries, When the block loads, Then it shows "Total October", "0h00", an empty circle and "You currently worked 0 hours in 14 day(s)" (today = 14/10/2026)
- [x] A02 · AC1.2 Given entries of 8:00 on 01/10, 7:30 on 02/10 and 0:45 on 06/10, When the block loads, Then it shows "16h15" and "You currently worked 16.25 hours"
- [x] A03 · AC1.3 Given October 2026 has 22 weekdays, 1 holiday and 5 days off, and DailyExpectedMinutes = 480, When the user has 64h00, Then the circle shows 50% (64 / 128)
- [x] A04 · AC1.4 Given the total exceeds the expected hours, When the block loads, Then the circle is shown full (100%) and the text shows the real total
- [x] A05 · AC1.5 Given Month = September 2026 (past), When the block loads, Then the sentence says "in 30 day(s)"
- [x] A06 · AC1.6 Given Month = November 2026 (future), When the block loads, Then it shows "0h00" and "in 0 day(s)"
- [x] A07 · AC1.7 Given the block is showing October, When the parent changes Month to 11, Then the block reloads and shows "Total November"
- [x] A08 · AC1.8 Given the parent increments RefreshKey after saving hours, When the block refreshes, Then the new total is shown without reloading the page
- [x] A09 · AC1.9 Given entries from another user in the same month, When I load the block, Then they are not included in my total
- [x] A10 · AC1.10 Given entries on 30/09 and 01/11, When October is shown, Then they are not counted
- [x] A11 · AC1.11 Given the data is loading, When the block renders, Then a loading state is shown instead of "0h00"
- [x] A12 · AC1.12 Given a phone viewport, When the block renders, Then the circle and text are centred and don't overflow

## Implementation plan

1. P01 · Create UI flow SheetBlocks (if it doesn't exist) and block TotalHoursMonth
2. P02 · Add input parameters Year, Month (mandatory) and RefreshKey (optional, default 0)
3. P03 · Add data action GetSummary calling server action GetMonthSummary(Year, Month), fetch At Start
4. P04 · Add OnParametersChanged handler that refreshes the GetSummary data action
5. P05 · Add ProgressCircle (OutSystems UI) bound to GetSummary.Out.ProgressPercent, colour var(--sheet-confirmed), track dark grey
6. P06 · Inside the circle placeholder add the label "Total " + MonthName and the expression FormatMinutes(TotalMinutes, "h")
7. P07 · Below the circle add the sentence: "You currently worked " + FormatDecimal(TotalMinutes / 60, 2) + " hours in " + ElapsedDays + " day(s)"
8. P08 · Remove trailing zeros of the decimal hours (37.50 -> 37.5, 0.00 -> 0)
9. P09 · Add a loading state (skeleton or "--") while GetSummary.IsDataFetched = False
10. P10 · Add an error handler that shows a FeedbackMessage when GetSummary fails
11. P11 · Style the block with theme classes (.sheet-total, .sheet-total__value, .sheet-total__caption) using the CSS variables
12. P12 · Make the block responsive: circle 160px on desktop, 120px on phone

## Implementation notes

- **2026-10-14, definition replaced.** The description, plan (P01–P12) and acceptance criteria were redefined after the block was built. Every step was already in the live module, so round 1 made **no new saves**. The existing merged implementation was checked against the new plan; see `reviews/ClaudeSheet/US1.md`.
- **Not in the new plan:** the `GetMonthSummary` changes from the first implementation (whole-month Expected, calendar ElapsedDays, Progress capped at 100, DayOff excluded from Total) are still in the module. The functional rules depend on them, and the acceptance-criteria review covers them.
- **P07 expression (plan check round 2, 1 save, merged):** the sentence now uses `FormatDecimal(GetSummary.Summary.TotalMinutes / 60, 2, ".", "")`, as the plan says. Round 1 had `DecimalToText(Round(…, 2))` and was answered No. The `.` decimal separator matches "16.25" in AC1.2; the empty group separator avoids "1,000". Trailing zeros (P08) are removed with `Replace(Replace(Replace(Replace(<formatted> + "#", "0#", "#"), "0#", "#"), ".#", ""), "#", "")`: 37.50 → 37.5, 0.00 → 0, 40.00 → 40, 16.25 → 16.25.
- **P05 binding:** the plan's `GetSummary.Out.ProgressPercent` is `GetSummary.Summary.ProgressPercent` in the module. It is passed as `DecimalToInteger(Round(...))` because ProgressCircle.Progress is Integer, and 0 while not fetched.

- Implemented and merged 2026-10-14 in 3 `applyModelApiCode` saves (GetMonthSummary, block, theme CSS). Live module: 0 validation errors.
- **GetMonthSummary changed** to this story's rules (see plan step 1); new local `ExpectedDays`. The loop/final Assign nodes were rewritten in place, so flow layout is unchanged.
- **Error feedback** uses the built-in Message node (Type = Error), not the OutSystemsUI `FeedbackMessageShow` action: its `MessageType` is a bare Integer and the `MessageStatus` static has none of the expected records (Error/Warning/Info/Success/Danger probed). The Message node shows the same OutSystems UI feedback message.
- **Data action error handling:** the server data action can't call client actions, so `GetSummary` catches AllExceptions (no transaction abort, logged) into an `ErrorMessage` output; `GetSummaryOnAfterFetch` shows it. This also catches the SheetUser role exception: a user without the role sees "You are not allowed to use MySheet." instead of an unhandled error.
- **Decimal hours:** `DecimalToText(Round(TotalMinutes / 60, 2))` gives up to 2 decimals without trailing zeros (37.5, 0, 7.33) - equivalent to FormatDecimal + stripping zeros.
- **Month name:** `FormatDateTime(NewDate(Year, Month, 1), "MMMM")`, so it follows the app locale (English by default).
- **Progress** is passed as an Integer (`DecimalToInteger(Round(ProgressPercent))`) because ProgressCircle.Progress is Integer.
- **Colours:** ProgressColor/TrailColor are OutSystemsUI `Color` identifiers, so custom values go through `TextToIdentifier("var(--sheet-confirmed)")` / `TextToIdentifier("#2A2A2A")`.
- **Dark surface (assumption):** the value text is white, but `--sheet-card` is white, so the block uses its own `--sheet-surface-dark: #1E1E1E` background. Adjust when the MySheet screen design is final.
- `RefreshKey` shows an "unused" warning by design: it only exists to trigger OnParametersChanged.

## Trail

- Changes: not auto-logged to `changes/`. The publish was a manual 1-Click Publish after a `blocked` MCP attempt, and the publish hook only fires on an MCP publish. The first implementation (3 saves) ran in an earlier session; the P07 fix (1 save) is described in the implementation notes. The final module code is in `snapshots/ClaudeSheet/` (`server-actions/GetMonthSummary.md`, `web-blocks/TotalHoursMonth.md`, `themes/ClaudeSheet.md`).
- Review: `reviews/ClaudeSheet/US1.md` (plan check rounds 1–2, review round 1: all approved)
- Published: see `PUBLISHES.md` (2026-10-14 17:01, success (manual))
