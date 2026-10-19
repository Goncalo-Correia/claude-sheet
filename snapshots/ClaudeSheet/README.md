# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user): the front end; the backend lives in ClaudeSheet_CS (see `../ClaudeSheet_CS/`)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-19, targeted mode, after the manual publish of US8 (Admin › Holidays, Menu HolidaysLink)

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities, static entities, structures | 0 | In ClaudeSheet_CS |
| Server actions | 2 | DoLogin, DoLogout (login template) |
| Client actions | 1 | Minutes_Format |
| Screens | 8 | Admin/Holidays (US8), Admin/Projects, Admin/ProjectDetail (US7), Admin/Clients, Admin/ClientDetail, Common/InvalidPermissions, Common/Login, MainFlow/MySheet (default screen) |
| Blocks | 13 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo, SheetBlocks/TotalHoursMonth, SheetBlocks/HoursCalendar, SheetBlocks/DayStatusKPIs, SheetBlocks/WeeklyProjectHours |
| Roles | 2 | Anonymous, Registered. Referenced from ClaudeSheet_CS: SheetUser, SheetAdmin, SheetManager |
| Timers, user exceptions | 0 | In ClaudeSheet_CS |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 0 own | In ClaudeSheet_CS |
| Client variables | 2 | LastURL, Username |
| References | | ClaudeSheet_CS, OutSystemsUI, OutSystemsCharts, OutSystemsMaps, (System) |

## Notes on this refresh

- US8 (2026-10-19), targeted mode: `names/screens.json`, `web-blocks/Menu.cs` (HolidaysLink) and the new `screens/Holidays.cs` (rebuilt from the saved tool output, too large for the transcript).

### TT7 refresh (2026-10-19)

- TT7 (2026-10-19), targeted mode: `themes/ClaudeSheet.cs` (new "Admin screens" section) and the four admin screens (`screens/Clients.cs`, `ClientDetail.cs`, `Projects.cs`, `ProjectDetail.cs`: stylesheet emptied, table modifier class). Three screens were rebuilt from the saved tool output (too large for the transcript).

### US7 refresh (2026-10-19)

- US7 (2026-10-19), targeted mode: `names/screens.json`, `web-blocks/Menu.cs` (ProjectsLink) and the new `screens/Projects.cs` and `screens/ProjectDetail.cs`. Both screens were too large for the transcript and were rebuilt from the saved tool output in the same format. Other files are as of the US6 refresh (unchanged by US7).

### US6 refresh (2026-10-19)

- US6 (2026-10-19, targeted): new `screens/Clients.cs` and `screens/ClientDetail.cs`; `web-blocks/Menu.cs` now has the server data action GetMenuAccess and the link ClientsLink (Visible = GetMenuAccess.IsSheetAdmin); `names/screens.json` lists the two Admin screens. Only `getScreenNames` was re-read of the module-wide lists; the others are unchanged by US6. `screens/ClientDetail.cs` was too large for the transcript, so it was rebuilt from the saved tool output (no fences, LF). References to ClaudeSheet_CS now include Client_GetList, Client_GetById, Client_CreateOrUpdate, Client_SetActive, ClientSummary and ClientValidation.

### TT5 refresh (2026-10-19)

- TT5 (2026-10-19): `names/roles.json` gained the referenced roles SheetAdmin and SheetManager. The new, empty UI flow **Admin** has no snapshot file (UI flows aren't read separately; its screens will appear in `names/screens.json`).
- `names/client-actions.json` and `names/web-blocks.json` were not re-read this time (about 70 KB, unchanged by TT5); they are as of the US5 refresh.

### US5 refresh (2026-10-16)

- US5 added `screens/MySheet.cs` and changed `screens/Login.cs` and `screens/InvalidPermissions.cs` (Everyone access). `screens/` is new in this refresh: the screens had no per-object snapshot before. `web-blocks/Menu.cs` is new (the "My Sheet" link).
- The 4 SheetBlocks were re-read: each root container has `data-refresh-key`, and HoursCalendar's CalendarDays list now has the single child `CalendarCell`.
- `client-actions/Date_GetWeekStart.cs` was deleted with the client action (the CS server action is unchanged).
- `names/roles.json` shows `SheetUser` (was `SheetUser2`).
- Written by `extract-snapshot.mjs`. `web-blocks/HoursCalendar.cs` and `web-blocks/WeeklyProjectHours.cs` were too large for the transcript, so the script wrote an error message into them; they were rebuilt from the saved tool outputs in the same format (no fences, LF).
