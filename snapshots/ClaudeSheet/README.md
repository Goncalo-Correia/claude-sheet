# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user): the front end; the backend lives in ClaudeSheet_CS (see `../ClaudeSheet_CS/`)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-19, targeted mode, after the manual publish of TT8 (action folders and node names)

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

- Format (2026-10-19, repository refactor): every file is Markdown, `<element>.md` with the code in one `csharp` block (name lists in a `json` block), and lines that only set a default value are left out. The content is otherwise unchanged.
- TT8 (2026-10-19), targeted mode: `client-actions/Minutes_Format.md` (folder Minutes), the 7 screens (ClientDetail, Clients, Holidays, Login, MySheet, ProjectDetail, Projects) and the 6 blocks (DayStatusKPIs, HoursCalendar, Menu, TotalHoursMonth, UserInfo, WeeklyProjectHours): 121 node labels, and AppendHourOption/AppendMinuteOption in HoursCalendar.OnInitialize. `web-blocks/UserInfo.md` is new. HoursCalendar, WeeklyProjectHours, Login, ProjectDetail, Holidays, Projects and ClientDetail were rebuilt from the saved tool output (too large for the transcript). Other files are as of the US8 refresh.

### US8 refresh (2026-10-19)

- US8 (2026-10-19), targeted mode: `names/screens.md`, `web-blocks/Menu.md` (HolidaysLink) and the new `screens/Holidays.md` (rebuilt from the saved tool output, too large for the transcript).

### TT7 refresh (2026-10-19)

- TT7 (2026-10-19), targeted mode: `themes/ClaudeSheet.md` (new "Admin screens" section) and the four admin screens (`screens/Clients.md`, `ClientDetail.cs`, `Projects.cs`, `ProjectDetail.cs`: stylesheet emptied, table modifier class). Three screens were rebuilt from the saved tool output (too large for the transcript).

### US7 refresh (2026-10-19)

- US7 (2026-10-19), targeted mode: `names/screens.md`, `web-blocks/Menu.md` (ProjectsLink) and the new `screens/Projects.md` and `screens/ProjectDetail.md`. Both screens were too large for the transcript and were rebuilt from the saved tool output in the same format. Other files are as of the US6 refresh (unchanged by US7).

### US6 refresh (2026-10-19)

- US6 (2026-10-19, targeted): new `screens/Clients.md` and `screens/ClientDetail.md`; `web-blocks/Menu.md` now has the server data action GetMenuAccess and the link ClientsLink (Visible = GetMenuAccess.IsSheetAdmin); `names/screens.md` lists the two Admin screens. Only `getScreenNames` was re-read of the module-wide lists; the others are unchanged by US6. `screens/ClientDetail.md` was too large for the transcript, so it was rebuilt from the saved tool output (no fences, LF). References to ClaudeSheet_CS now include Client_GetList, Client_GetById, Client_CreateOrUpdate, Client_SetActive, ClientSummary and ClientValidation.

### TT5 refresh (2026-10-19)

- TT5 (2026-10-19): `names/roles.md` gained the referenced roles SheetAdmin and SheetManager. The new, empty UI flow **Admin** has no snapshot file (UI flows aren't read separately; its screens will appear in `names/screens.md`).
- `names/client-actions.md` and `names/web-blocks.md` were not re-read this time (about 70 KB, unchanged by TT5); they are as of the US5 refresh.

### US5 refresh (2026-10-16)

- US5 added `screens/MySheet.md` and changed `screens/Login.md` and `screens/InvalidPermissions.md` (Everyone access). `screens/` is new in this refresh: the screens had no per-object snapshot before. `web-blocks/Menu.md` is new (the "My Sheet" link).
- The 4 SheetBlocks were re-read: each root container has `data-refresh-key`, and HoursCalendar's CalendarDays list now has the single child `CalendarCell`.
- `client-actions/Date_GetWeekStart.md` was deleted with the client action (the CS server action is unchanged).
- `names/roles.md` shows `SheetUser` (was `SheetUser2`).
- Written by `extract-snapshot.mjs`. `web-blocks/HoursCalendar.md` and `web-blocks/WeeklyProjectHours.md` were too large for the transcript, so the script wrote an error message into them; they were rebuilt from the saved tool outputs in the same format (no fences, LF).
