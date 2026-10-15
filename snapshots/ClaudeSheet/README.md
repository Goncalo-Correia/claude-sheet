# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-14, targeted mode, after the manual publish of story US2

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry |
| Static entities | 2 | DayStatus, CalendarDayType |
| Structures | 11 | CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption (US2), DayEditorLine (US2) |
| Server actions | 13 | Bootstrap, ClearDaySheet, DoLogin, DoLogout, FillPendingDays, FormatMinutes, GetDaySheetDetail, GetMonthCalendar, GetMonthSummary, GetUserProjects, GetWeekStart, Holiday_CreateIfMissing, SaveDaySheet |
| Client actions | 2 | FormatMinutes, GetWeekStart |
| Service actions | 0 | |
| Screens | 2 | Common/InvalidPermissions, Common/Login |
| Blocks | 11 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo, SheetBlocks/TotalHoursMonth, SheetBlocks/HoursCalendar |
| User exceptions | 1 | DaySheetValidation |
| Roles | 3 | Anonymous, Registered, SheetUser |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 2 | DailyExpectedMinutes, SeedTestUsernames |
| Timers | 1 | Timer_Bootstrap |
| Client variables | 2 | LastURL, Username |

## Notes on this refresh

- Re-read for US2: `names/server-actions.json`, `names/web-blocks.json`, `user-exceptions.cs`, `server-actions/GetDaySheetDetail.cs`, `server-actions/SaveDaySheet.cs`, `server-actions/ClearDaySheet.cs`, `server-actions/FillPendingDays.cs`, `web-blocks/HoursCalendar.cs` (new), `themes/ClaudeSheet.cs`.
- `web-blocks/HoursCalendar.cs` was written by hand from the saved tool result: the read (about 200,000 characters) was too large for the transcript and the extract script stored the truncation notice instead.
- **`structures.cs` was not refreshed.** The read was skipped on request, so the two US2 structures `NumberOption` (Value, Label) and `DayEditorLine` (ProjectId, ClientName, ProjectName, ProjectCode, Hours, Mins, Description) are not in it yet. Refresh it on the next snapshot.
- Not re-read (unchanged by US2): `data-model.cs`, `settings.cs`, `timers.cs`, `integrations.cs`, `client-variables.cs`, `session-variables.cs`, `names/client-actions.json`, `names/service-actions.json`, `names/roles.json`, `names/screens.json`, `names/themes.json`, `names/resources.json`, `names/scripts.json`.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
