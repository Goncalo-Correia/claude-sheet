# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-15, targeted mode, after the manual publish of TT2

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry |
| Static entities | 2 | DayStatus, CalendarDayType |
| Structures | 12 | CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption (US2), DayEditorLine (US2), DayStatusKPI (US3) |
| Server actions | 13 | Bootstrap, Calendar_GetMonth, Calendar_GetMonthSummary, Date_GetWeekStart, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, DoLogin, DoLogout, Holiday_CreateIfMissing, Minutes_Format, Project_GetAssignedToUser |
| Client actions | 2 | Date_GetWeekStart, Minutes_Format |
| Service actions | 0 | |
| Screens | 2 | Common/InvalidPermissions, Common/Login |
| Blocks | 12 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo, SheetBlocks/TotalHoursMonth, SheetBlocks/HoursCalendar, SheetBlocks/DayStatusKPIs |
| User exceptions | 1 | DaySheetValidation |
| Roles | 3 | Anonymous, Registered, SheetUser |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 2 | DailyExpectedMinutes, SeedTestUsernames |
| Timers | 1 | Timer_Bootstrap |
| Client variables | 2 | LastURL, Username |

## Notes on this refresh

- TT2 re-laid out 15 action flows (positions, plus 7 new End nodes). Re-read: `server-actions/` DaySheet_Save, Bootstrap, DaySheet_FillPending, DaySheet_Clear, Calendar_GetMonth, Holiday_CreateIfMissing, DaySheet_GetDetail, Calendar_GetMonthSummary and Project_GetAssignedToUser; `server-actions/DoLogout.cs` (new: the template action had no snapshot before); and `web-blocks/HoursCalendar.cs`, `TotalHoursMonth.cs` and `DayStatusKPIs.cs`.
- Light mode was re-read too. The module-wide files and names lists are unchanged, as TT2 changed no data, names or CSS.
- The diffs are node positions (`HorizontalPosition` / `VerticalPosition` and layout helper calls), connector notation, and the added End nodes.
- Reads made over raw HTTP and written by an equivalent of `extract-snapshot.mjs`, as for US3 and TT1.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
