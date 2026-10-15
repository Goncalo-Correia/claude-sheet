# Review: ClaudeSheet · US2 (HoursCalendar block)

## Implementation plan check — round 1 — 2026-10-14 17:31

Implemented in 6 `applyModelApiCode` saves, merged with `omlMerge` (`merged: true`). Live validation: 0 errors.

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P01 | Create block HoursCalendar with inputs Year, Month, RefreshKey and events OnMonthChanged, OnDaySaved, OnDaySelected | Approved | SheetBlocks › HoursCalendar, inputs and events as specified | Yes |
| P02 | Data action GetCalendar calling GetMonthCalendar and GetMonthSummary | Approved | GetCalendar (At Start), outputs Cells, Summary, DailyExpectedMinutes | Yes |
| P03 | OnParametersChanged refreshes GetCalendar | Approved | Refresh Data GetCalendar | Yes |
| P04 | Header: arrows + title MonthName + Year (accent colour) | Approved | PreviousMonth / MonthTitle / NextMonth; verify at runtime | Yes |
| P05 | Arrow actions roll Dec→Jan and trigger OnMonthChanged | Approved | PreviousMonthOnClick / NextMonthOnClick | Yes |
| P06 | Weekday header Mon..Sun (7-column grid) | Approved | WeekdayHeader + .sheet-cal__weekdays | Yes |
| P07 | Leading empty cells (DayOfWeek(first day)+6) mod 7 | Approved | Blank CalendarDay records prepended in GetCalendar.Cells; IsBlankCell If | Yes |
| P08 | One cell per CalendarDay with CssClass and day number | Approved | DayCell button in CalendarDays list | Yes |
| P09 | Dot: white today, red HasRecord, hidden otherwise | Approved | Computed in the block (user's choice) | Yes |
| P10 | Tooltip with status + FormatMinutes or HolidayName | Approved | title attribute; verify at runtime | Yes |
| P11 | Clickable only when IsEditable; OnDaySelected on any click | Approved | DayOnClick; verify at runtime | Yes |
| P12 | Server action GetDaySheetDetail(Date) | Approved | Role gate, merge of GetUserProjects with TimeEntries | Yes |
| P13 | DayEditorPopup with date title and Work day / Day off ButtonGroup | Approved | Popup + ButtonGroup DayKind | Yes |
| P14 | Project lines with hours/minutes dropdowns, description, live total | Approved | ProjectLines list, RecalcEditorTotal | Yes |
| P15 | Future dates: Day off / Remove day off only | Approved | WorkDayOption hidden when IsFutureDay | Yes |
| P16 | SaveDaySheet with validations | Approved | DaySheetValidation raised for each rule; role gate | Yes |
| P17 | Upsert DaySheet and TimeEntries, delete removed | Approved | CreateOrUpdateDaySheet, Update/Delete/CreateTimeEntry | Yes |
| P18 | IsDayOff deletes entries; client confirms | Approved | DayOffDeleteHoursButton with confirmation | Yes |
| P19 | ClearDaySheet(Date) | Approved | DeleteDaySheet; TimeEntry delete rule Delete | Yes |
| P20 | Popup buttons Save as draft, Confirm day, Clear day, Cancel | Approved | SaveDay / ClearDay / CloseDayEditor | Yes |
| P21 | After save/clear: close, refresh, success message, OnDaySaved | Approved | DayEditorSaved (Message node Success) | Yes |
| P22 | Server errors inside the popup | Approved | AllExceptions → EditorError | Yes |
| P23 | Fill all pending days button, disabled when PendingCount = 0 | Approved | FillPendingButton Enabled expression | Yes |
| P24 | FillPendingPopup: project, hours/minutes default, summary | Approved | OpenFillPopup + FillPendingPopup | Yes |
| P25 | FillPendingDays(Year, Month, ProjectId, MinutesPerDay) | Approved | Drafts confirmed; others via SaveDaySheet; single request | Yes |
| P26 | On success: X days filled, refresh, OnDaySaved(CurrDate()) | Approved | ConfirmFill | Yes |
| P27 | Style: dark card, 6px squares, CSS variables | Approved | Theme US2 section; verify at runtime | Yes |
| P28 | Accessibility: aria-label buttons, popups trap focus | Approved | aria-label + Escape; focus trap from Popup widget; verify at runtime | Yes |
| P29 | Responsive: 100% width, square cells, arrows visible | Approved | Theme .phone rules; verify at runtime | Yes |

**No reasons:** none.

**Outcome:** all 29 steps approved in round 1. No steps accepted despite a Rejected tag. Merged in Service Studio, not published.

Plan check: approved

## Review — round 1 — 2026-10-14 17:34

Plan check approved on 2026-10-14 (all 29 steps Yes in round 1), so the plan isn't in this form.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 · AC2.1 | Title, Monday-first grid, 01/10 on Thursday | Approved | see form | Yes |
| A02 · AC2.2 | Seed scenario colours | Approved | see form | Yes |
| A03 · AC2.3 | White dot today, red dot on saved days (low contrast on Draft squares) | Approved | see form | Yes |
| A04 · AC2.4 | Past weekday without DaySheet is orange | Approved | see form | Yes |
| A05 · AC2.5 | Right arrow Dec 2026 → 2027/1 | Approved | see form | Yes |
| A06 · AC2.6 | Left arrow Oct 2026 → 2026/9 | Approved | see form | Yes |
| A07 · AC2.7 | Saturday / holiday opens no popup | Approved | see form | Yes |
| A08 · AC2.8 | 13/10 editor title and project lines (client on its own line) | Approved | see form | Yes |
| A09 · AC2.9 | 7:30 Confirm day → green, message, OnDaySaved | Approved | see form | Yes |
| A10 · AC2.10 | 4:00 Save as draft → stays orange, reopens 4:00 | Approved | see form | Yes |
| A11 · AC2.11 | 0:00 Confirm → 'Log at least 15 minutes…', nothing saved | Approved | see form | Yes |
| A12 · AC2.12 | Over 24:00 → 'A day cannot exceed 24:00' | Approved | see form | Yes |
| A13 · AC2.13 | Day off on 8:00 day deletes hours, blue | Approved | see form | Yes |
| A14 · AC2.14 | Future weekday: Day off only | Approved | see form | Yes |
| A15 · AC2.15 | Clear day → Pending / white | Approved | see form | Yes |
| A16 · AC2.16 | Crafted SaveDaySheet rejected | Approved | see form | Yes |
| A17 · AC2.17 | 0 pending → Fill disabled and grey | Approved | see form | Yes |
| A18 · AC2.18 | Fill 3 pending (Draft kept at 4:00) → '3 days filled' | Approved | see form | Yes |
| A19 · AC2.19 | Fill never touches future/weekend/holiday/day off | Approved | see form | Yes |
| A20 · AC2.20 | Fill error rolls back everything | Approved | see form | Yes |
| A21 · AC2.21 | Any day click raises OnDaySelected | Approved | see form | Yes |
| A22 · AC2.22 | RefreshKey reloads | Approved | see form | Yes |
| A23 · AC2.23 | User isolation | Approved | see form | Yes |
| A24 · AC2.24 | Keyboard: Tab, Enter, Escape | Approved | see form | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Created | Data › Structures › NumberOption | New | Yes |
| C02 | Created | Data › Structures › DayEditorLine | New | Yes |
| C03 | Created | Logic › Exceptions › DaySheetValidation | New | Yes |
| C04 | Created | Logic › Server Actions › GetDaySheetDetail | New | Yes |
| C05 | Created | Logic › Server Actions › ClearDaySheet | New | Yes |
| C06 | Created | Logic › Server Actions › SaveDaySheet | New | Yes |
| C07 | Created | Logic › Server Actions › FillPendingDays | New | Yes |
| C08 | Created | Interface › UI Flows › SheetBlocks › HoursCalendar | New | Yes |
| C09 | Modified | Interface › Themes › ClaudeSheet | New | Yes |

**No reasons:** none.

**Outcome:** all 33 rows approved (24 criteria, 9 changes). No rows accepted despite a Rejected tag. 0 validation errors. MCP publish `blocked` twice (permission not granted); published manually by the user from Service Studio on 2026-10-14 17:36.
