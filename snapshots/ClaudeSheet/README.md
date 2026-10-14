# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-14, targeted mode, after the manual publish of story US-000

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
| Blocks | 9 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo |
| Roles | 3 | Anonymous, Registered, SheetUser |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 2 | DailyExpectedMinutes, SeedTestUsernames |
| Timers | 1 | Timer_Bootstrap |
| Client variables | 2 | LastURL, Username |

## Notes on this refresh

- Re-read for US-000: `data-model.cs`, `structures.cs`, `settings.cs`, `timers.cs`, `names/server-actions.json`, `names/client-actions.json`, `names/roles.json`, `names/themes.json`, and per-element files in `server-actions/`, `client-actions/`, `roles/`, `themes/`.
- Not re-read (unchanged by US-000, still from the initial snapshot): `integrations.cs`, `user-exceptions.cs`, `client-variables.cs`, `session-variables.cs`, `names/service-actions.json`, `names/screens.json`, `names/web-blocks.json`, `names/resources.json`, `names/scripts.json`.
- `structures.cs` was written by hand from the saved tool result, because the result was too large for the transcript and the extract script stored the truncation notice instead.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
