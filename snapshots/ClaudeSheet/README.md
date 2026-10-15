# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-15, targeted mode, after the manual publish of US4

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry |
| Static entities | 2 | DayStatus, CalendarDayType |
| Structures | 12 | CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption (US2), DayEditorLine (US2), DayStatusKPI (US3) |
| Server actions | 14 | Bootstrap, Calendar_GetMonth, Calendar_GetMonthSummary, Date_GetWeekStart, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, DoLogin, DoLogout, Holiday_CreateIfMissing, Minutes_Format, Project_GetAssignedToUser, ProjectHours_GetWeekly (US4) |
| Client actions | 2 | Date_GetWeekStart, Minutes_Format |
| Service actions | 0 | |
| Screens | 2 | Common/InvalidPermissions, Common/Login |
| Blocks | 13 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo, SheetBlocks/TotalHoursMonth, SheetBlocks/HoursCalendar, SheetBlocks/DayStatusKPIs, SheetBlocks/WeeklyProjectHours (US4) |
| User exceptions | 1 | DaySheetValidation |
| Roles | 3 | Anonymous, Registered, SheetUser |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 2 | DailyExpectedMinutes, SeedTestUsernames |
| Timers | 1 | Timer_Bootstrap |
| Client variables | 2 | LastURL, Username |

## Notes on this refresh

- US4 created `server-actions/ProjectHours_GetWeekly.cs` and `web-blocks/WeeklyProjectHours.cs`, extended the TT0 weekly structures (`structures.cs`: WeekDayHeader.DayName/DateLabel, WeeklyCell.ShowDash, WeeklyHours.GrandTotalMinutes/DayTotals) and appended the US4 section to `themes/ClaudeSheet.cs`.
- Light mode was re-read too. The names lists gained ProjectHours_GetWeekly and WeeklyProjectHours; the other module-wide files are unchanged.
- Reads made over raw HTTP and written by an equivalent of `extract-snapshot.mjs`, as for US3, TT1 and TT2.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
