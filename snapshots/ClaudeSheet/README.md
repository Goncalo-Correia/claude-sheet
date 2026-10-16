# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user): the front end; the backend lives in ClaudeSheet_CS (see `../ClaudeSheet_CS/`)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-16, targeted mode, after the manual publish of US5 (MySheet screen)

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities, static entities, structures | 0 | In ClaudeSheet_CS |
| Server actions | 2 | DoLogin, DoLogout (login template) |
| Client actions | 1 | Minutes_Format |
| Screens | 3 | Common/InvalidPermissions, Common/Login, MainFlow/MySheet (default screen) |
| Blocks | 13 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo, SheetBlocks/TotalHoursMonth, SheetBlocks/HoursCalendar, SheetBlocks/DayStatusKPIs, SheetBlocks/WeeklyProjectHours |
| Roles | 2 | Anonymous, Registered (SheetUser is the ClaudeSheet_CS reference, no longer suffixed "2") |
| Timers, user exceptions | 0 | In ClaudeSheet_CS |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 0 own | In ClaudeSheet_CS |
| Client variables | 2 | LastURL, Username |
| References | | ClaudeSheet_CS, OutSystemsUI, OutSystemsCharts, OutSystemsMaps, (System) |

## Notes on this refresh

- US5 added `screens/MySheet.cs` and changed `screens/Login.cs` and `screens/InvalidPermissions.cs` (Everyone access). `screens/` is new in this refresh: the screens had no per-object snapshot before. `web-blocks/Menu.cs` is new (the "My Sheet" link).
- The 4 SheetBlocks were re-read: each root container has `data-refresh-key`, and HoursCalendar's CalendarDays list now has the single child `CalendarCell`.
- `client-actions/Date_GetWeekStart.cs` was deleted with the client action (the CS server action is unchanged).
- `names/roles.json` shows `SheetUser` (was `SheetUser2`).
- Written by `extract-snapshot.mjs`. `web-blocks/HoursCalendar.cs` and `web-blocks/WeeklyProjectHours.cs` were too large for the transcript, so the script wrote an error message into them; they were rebuilt from the saved tool outputs in the same format (no fences, LF).
