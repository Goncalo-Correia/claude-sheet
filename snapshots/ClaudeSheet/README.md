# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-14, targeted mode, after the manual publish of story US1

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry |
| Static entities | 2 | DayStatus, CalendarDayType |
| Structures | 9 | CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours |
| Server actions | 9 | Bootstrap, DoLogin, DoLogout, FormatMinutes, GetMonthCalendar, GetMonthSummary, GetUserProjects, GetWeekStart, Holiday_CreateIfMissing |
| Client actions | 2 | FormatMinutes, GetWeekStart |
| Service actions | 0 | |
| Screens | 2 | Common/InvalidPermissions, Common/Login |
| Blocks | 10 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo, SheetBlocks/TotalHoursMonth |
| Roles | 3 | Anonymous, Registered, SheetUser |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 2 | DailyExpectedMinutes, SeedTestUsernames |
| Timers | 1 | Timer_Bootstrap |
| Client variables | 2 | LastURL, Username |

## Notes on this refresh

- Re-read for US1: `names/server-actions.json`, `names/client-actions.json`, `names/web-blocks.json`, `names/themes.json`, `server-actions/GetMonthSummary.cs`, `web-blocks/TotalHoursMonth.cs` (new), `themes/ClaudeSheet.cs`.
- Not re-read (unchanged by US1, still from the US-000 refresh or the initial snapshot): `data-model.cs`, `structures.cs`, `settings.cs`, `timers.cs`, `integrations.cs`, `user-exceptions.cs`, `client-variables.cs`, `session-variables.cs`, `names/service-actions.json`, `names/roles.json`, `names/screens.json`, `names/resources.json`, `names/scripts.json`.
- `structures.cs` was written by hand in the US-000 refresh from the saved tool result, because the result was too large for the transcript.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
