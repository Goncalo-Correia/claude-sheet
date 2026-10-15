# US2: [Block] Monthly calendar to fill in daily hours

- **Status:** Published
- **Published:** 2026-10-14 17:36 (manual 1-Click Publish from Service Studio)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-14
- **Depends on:** TT0

## Story

**User story**
As a consultant
I want a monthly calendar where each day shows its status and where I can open a day to log hours per project, mark it as a day off, or confirm it
So that I can fill in and confirm my timesheet quickly and see which days still need attention.

**Context**
Centre widget of the MySheet screen: month/year title in red, previous/next arrows, Monday-first grid, coloured day squares and the "Fill all pending days" button below.

**Functional rules**

Day display (classification from TT0):

* Confirmed = green square, Day off = blue square, Holiday = purple square, Pending = orange square
* Future weekday = white number, no background; Weekend = grey number, no background
* Small dot under the number: red when the day has saved information (DaySheet or holiday), white for today
* Tooltip on hover: status label + total hours (e.g. "Confirmed · 8:00") or the holiday name
* Leading empty cells before the 1st so that weekdays align with Mon..Sun

Navigation

* Previous/next arrows move one month and raise OnMonthChanged(Year, Month)
* The month title shows "<Month name> <Year>" in the accent colour

Day editor (popup), opened by clicking an editable day:

* Weekends and holidays are not clickable
* Past/today weekday: choose "Work day" or "Day off"
   * Work day: one line per project assigned to the user on that date (client bold + "Project | Code"), each with an hours dropdown (0–12) and minutes dropdown (00/15/30/45) plus an optional description; live "Total of the day"
   * Buttons: Save as draft, Confirm day, Clear day, Cancel
* Future weekday: only "Day off" can be set or removed (hours cannot be logged in the future)
* Validation: total of the day ≤ 24:00; confirming needs a total > 0:00; only assigned projects are accepted
* Setting "Day off" on a day with hours asks for confirmation and then deletes the hours
* "Clear day" deletes the DaySheet and its entries (the day goes back to Pending/Future)
* After any save, raise OnDaySaved(Date) so the parent refreshes the other blocks
* Clicking any day of the month also raises OnDaySelected(Date) (used to move the weekly table)

Fill all pending days

* Enabled only when the month has at least 1 Pending day; otherwise disabled (grey)
* Opens a popup: project (assigned projects dropdown) + hours per day (default = DailyExpectedMinutes) + a summary "X pending days will be filled and confirmed"
* On confirm, for every Pending day: if it already has a Draft with total > 0, confirm it as is; otherwise create/update the DaySheet with one TimeEntry for the chosen project and confirm it
* Runs in one server transaction; shows "X days filled" feedback and raises OnDaySaved

**Block contract**

* Name: HoursCalendar (UI flow SheetBlocks)
* Inputs: Year, Month (mandatory), RefreshKey (optional)
* Events: OnMonthChanged(Year, Month), OnDaySaved(Date), OnDaySelected(Date)

**Server actions**

* GetDaySheetDetail(Date) → DaySheetDetail (assigned projects merged with existing entries)
* SaveDaySheet(Date, IsDayOff, Confirm, Entries: List of TimeEntryInput, Notes)
* ClearDaySheet(Date)
* FillPendingDays(Year, Month, ProjectId, MinutesPerDay) → FilledCount

**Out of scope**

* Logging hours on weekends/holidays, approval workflow, month locking

**Dependencies**
TT0

## Acceptance criteria

- [x] A01 · AC2.1 Given October 2026, When the calendar loads, Then the title shows "October 2026", the grid starts on Monday and 01/10 is in the Thursday column
- [x] A02 · AC2.2 Given the seed scenario of the mockup, When the calendar loads, Then 1,2,6,7,12,13,14 are green, 8,9,28,29,30 blue, 5 purple, 15-16,19-23,26-27 white, weekends grey
- [x] A03 · AC2.3 Given today is 14/10/2026, When the calendar loads, Then 14 shows a white dot and the days with saved data show a red dot
- [x] A04 · AC2.4 Given a past weekday with no DaySheet, When the calendar loads, Then the day is orange (Pending)
- [x] A05 · AC2.5 Given I click the right arrow on December 2026, Then OnMonthChanged is raised with Year=2027, Month=1
- [x] A06 · AC2.6 Given I click the left arrow on October 2026, Then OnMonthChanged is raised with Year=2026, Month=9
- [x] A07 · AC2.7 Given I click a Saturday or a holiday, Then no popup opens
- [x] A08 · AC2.8 Given I click 13/10 (past weekday), Then the day editor opens with "Tuesday, 13 October 2026" and one line per assigned project (e.g. "ICNF / Resina e Pinha | RUBUS")
- [x] A09 · AC2.9 Given I enter 7:30 on a project and click "Confirm day", Then the popup closes, 13/10 turns green, a success message is shown and OnDaySaved is raised
- [x] A10 · AC2.10 Given I enter 4:00 and click "Save as draft", Then the day stays orange (Pending) and reopening the popup shows 4:00
- [x] A11 · AC2.11 Given all projects at 0:00, When I click "Confirm day", Then the error "Log at least 15 minutes to confirm the day" is shown and nothing is saved
- [x] A12 · AC2.12 Given the total of the day would exceed 24:00, When I save, Then the error "A day cannot exceed 24:00" is shown
- [x] A13 · AC2.13 Given a day with 8:00 logged, When I choose Day off and confirm the warning, Then the hours are deleted and the day turns blue
- [x] A14 · AC2.14 Given I click a future weekday (e.g. 20/10), Then only the Day off option is available and hours cannot be entered
- [x] A15 · AC2.15 Given a day with data, When I click "Clear day" and confirm, Then the DaySheet is removed and the day becomes Pending (past) or white (future)
- [x] A16 · AC2.16 Given a crafted request to SaveDaySheet with hours on a future date, a weekend, a holiday or an unassigned project, Then the server rejects it with a validation error
- [x] A17 · AC2.17 Given the month has 0 pending days, Then "Fill all pending days" is disabled and grey
- [x] A18 · AC2.18 Given the month has 3 pending days (one Draft with 4:00), When I fill with ICNF/RUBUS at 8:00, Then the summary says "3 pending days will be filled", the Draft is confirmed with 4:00, the other 2 are confirmed with 8:00, and "3 days filled" is shown
- [x] A19 · AC2.19 Given Fill all pending days runs, Then future days, weekends, holidays and days off are never modified
- [x] A20 · AC2.20 Given an error happens in the middle of FillPendingDays, Then no day is changed (single transaction)
- [x] A21 · AC2.21 Given I click any day of the month, Then OnDaySelected is raised with that date
- [x] A22 · AC2.22 Given the parent increments RefreshKey, Then the calendar reloads its data
- [x] A23 · AC2.23 Given user B, When user A saves a day, Then user B's calendar is not affected
- [x] A24 · AC2.24 Given keyboard-only navigation, Then I can tab to a day, open it with Enter and close the popup with Escape

## Implementation plan

1. P01 · Create block HoursCalendar in UI flow SheetBlocks with inputs Year, Month, RefreshKey and events OnMonthChanged, OnDaySaved, OnDaySelected
2. P02 · Add data action GetCalendar calling GetMonthCalendar(Year, Month) and GetMonthSummary(Year, Month) (PendingCount for the button)
3. P03 · Add OnParametersChanged handler that refreshes GetCalendar
4. P04 · Build the header: left arrow button, title MonthName + " " + Year (accent colour), right arrow button
5. P05 · Implement the arrow click actions: calculate previous/next Year/Month (Dec->Jan rolls the year) and trigger OnMonthChanged
6. P06 · Build the weekday header row Mon..Sun (7-column CSS grid)
7. P07 · Render leading empty cells: (DayOfWeek(first day) + 6) mod 7 blanks so that Monday is column 1
8. P08 · Render one cell per CalendarDay with the CssClass of its CalendarDayType and the day number
9. P09 · Add the dot indicator: white when IsToday, red when HasRecord, hidden otherwise
10. P10 · Add a tooltip per day with the status label and FormatMinutes(TotalMinutes, ":") or the HolidayName
11. P11 · Make cells clickable only when IsEditable; set cursor and hover state; raise OnDaySelected on any day click
12. P12 · Create server action GetDaySheetDetail(Date): returns the DaySheet status and notes plus one line per project from GetUserProjects(Date), merged with existing TimeEntry minutes
13. P13 · Create the popup DayEditorPopup (OutSystems UI Popup) with the date as title and a "Work day / Day off" ButtonGroup
14. P14 · For Work day render the project lines with hours dropdown (0-12), minutes dropdown (00,15,30,45) and a description input, plus the live total
15. P15 · For future dates hide the Work day option and show only Day off / Remove day off
16. P16 · Create server action SaveDaySheet(Date, IsDayOff, Confirm, Entries, Notes) with validations: weekday, not a holiday, no hours in the future, projects assigned on that date, minutes multiple of 15, day total <= 1440, Confirm requires total > 0
17. P17 · In SaveDaySheet upsert the DaySheet (status DayOff / Confirmed / Draft), upsert TimeEntries with Minutes > 0, delete TimeEntries with 0 minutes or for projects no longer sent, update UpdatedOn
18. P18 · When IsDayOff = True and entries exist, delete the entries (the client asks for confirmation first)
19. P19 · Create server action ClearDaySheet(Date) that deletes the user's DaySheet (entries cascade)
20. P20 · Wire popup buttons: Save as draft (Confirm=False), Confirm day (Confirm=True), Clear day (with confirmation), Cancel
21. P21 · After a successful save/clear: close the popup, refresh GetCalendar, show a success FeedbackMessage and trigger OnDaySaved(Date)
22. P22 · Show server validation errors inside the popup without closing it
23. P23 · Add the button "Fill all pending days" under the calendar, disabled when PendingCount = 0
24. P24 · Create the popup FillPendingPopup with a project dropdown (GetUserProjects(today)), hours/minutes dropdowns defaulting to DailyExpectedMinutes, and the summary text with the pending count
25. P25 · Create server action FillPendingDays(Year, Month, ProjectId, MinutesPerDay): loop the Pending days from GetMonthCalendar, confirm drafts with total > 0, otherwise create/update the DaySheet with one entry and Confirmed status; return FilledCount
26. P26 · On success show "X days filled", refresh GetCalendar and trigger OnDaySaved(CurrDate())
27. P27 · Style: dark card (--sheet-card), 6px rounded squares, colours from CSS variables, grey weekends, white future days
28. P28 · Accessibility: day cells are buttons with aria-label "14 October 2026, Confirmed, 8 hours"; popups trap focus
29. P29 · Responsive: calendar width 100% on phone with square cells; arrows remain visible

## Implementation notes

- Implemented 2026-10-14 in **6 `applyModelApiCode` saves** (block skeleton + GetCalendar; header/grid/day cells; GetDaySheetDetail + ClearDaySheet + user exception; SaveDaySheet + FillPendingDays; day editor + fill popups; theme CSS + Escape key), then one `omlMerge` (`merged: true`). Live module: **0 validation errors**.
- **Decisions confirmed with the user before building:** the card uses `--sheet-surface-dark` (not `--sheet-card`, which is white and backs the US-000 `.day-pending` / `.day-future` classes); the red dot is computed in the block (`DaySheetId <> NullIdentifier() or HolidayName <> ""`) instead of a new `CalendarDay.HasRecord` attribute.
- **Added by the conventions (not named in the plan):** every new server action starts with the `CheckSheetUserRole()` gate (role exception, like the TT0 actions); user exception **`DaySheetValidation`** carries SaveDaySheet's business-rule messages, which the popup catches (AllExceptions handler → `EditorError`).
- **Leading blanks (P07):** `GetCalendar` prepends `Mod(DayOfWeek(first day) + 6, 7)` blank `CalendarDay` records (DayNumber = 0) to `Cells`, so one List renders blanks and days in the 7-column grid; the `IsBlankCell` If renders `.sheet-cal__blank` for them.
- **Day cells (P11):** every day is a Button so any click raises `OnDaySelected`; `DayOnClick` opens the editor only when `IsEditable`. Read-only days get `is-readonly` (default cursor, no hover).
- **Tooltip (P10)** is the native `title` attribute; **aria-label (P28)** is built the same way (`"14 October 2026, Confirmed, 8 hours"`).
- **New structures:** `DayEditorLine` (time split into Hours/Mins for the dropdowns) and `NumberOption` (dropdown options, filled in `BuildTimeOptions` on OnInitialize).
- **Day off with hours (P18):** the client shows a "Set day off and delete hours" button with a confirmation message when the day has saved minutes; SaveDaySheet deletes the entries when `IsDayOff`.
- **Future days (P15):** the "Work day" option is hidden, `IsDayOff` starts True; "Save day off" sets it, "Remove day off" (Clear day) removes it.
- **Success feedback (P21, P26)** uses the Message node (Type Success), which shows the OutSystems UI feedback message (same reason as US1: `FeedbackMessageShow`'s `MessageType` can't be set to a proper type).
- **FillPendingDays (P25):** Drafts with a total > 0 are confirmed with `UpdateDaySheet`; every other Pending day goes through `SaveDaySheet` (same validations) with one entry and `Confirm = True`, so a failure rolls back the whole request. Those days are saved with empty Notes.
- **Colours (P27):** inside `.sheet-cal` the variables `--sheet-dayoff` (blue `#2f7fd8`) and `--sheet-holiday` (purple `#8a4fd1`) are redefined, so the TT0 theme values elsewhere are unchanged; Pending uses `--sheet-pending` as a filled square.
- **Accessibility (P28):** `BindEscapeKey` (block OnReady, JavaScript) makes Escape click the open popup's Cancel button. Focus trapping relies on the platform Popup widget; there is no custom focus-trap code. Verify at runtime.
- **Theme edit:** the sidecar reports `InvalidIconLibrary` on any theme stylesheet edit; it was ignored as documented (Service Studio's own validation after the merge is clean, `IconLibrary` left null).
- **Open warning:** TrueChange suggests the `CalendarDays` List should have a single direct child; its only child is the `IsBlankCell` If, so the suggestion was left as is.

## Trail

- Changes: not auto-logged to `changes/`. The publish was a manual 1-Click Publish after two `blocked` MCP attempts, and the publish hook only fires on an MCP publish. The 6 saves are described in the implementation notes; the final code is in `snapshots/ClaudeSheet/` (`web-blocks/HoursCalendar.cs`, `server-actions/{GetDaySheetDetail,SaveDaySheet,ClearDaySheet,FillPendingDays}.cs`, `user-exceptions.cs`, `themes/ClaudeSheet.cs`).
- Review: `reviews/ClaudeSheet/US2.md` (plan check round 1 and review round 1: all approved)
- Published: see `PUBLISHES.md` (2026-10-14 17:36, success (manual))
