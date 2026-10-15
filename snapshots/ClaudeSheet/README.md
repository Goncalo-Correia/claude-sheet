# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user): the front end; the backend lives in ClaudeSheet_CS (see `../ClaudeSheet_CS/`)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-15, targeted mode, after the manual publish of TT4 (consumer part)

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities, static entities, structures | 0 | Moved to ClaudeSheet_CS in TT4 |
| Server actions | 2 | DoLogin, DoLogout (login template) |
| Client actions | 2 | Date_GetWeekStart, Minutes_Format |
| Screens | 2 | Common/InvalidPermissions, Common/Login |
| Blocks | 13 | ApplicationTitle, LayoutBase, LayoutBaseSection, LayoutBlank, LayoutSideMenu, LayoutTopMenu, Menu, MenuIcon, UserInfo, SheetBlocks/TotalHoursMonth, SheetBlocks/HoursCalendar, SheetBlocks/DayStatusKPIs, SheetBlocks/WeeklyProjectHours |
| Roles | 2 | Anonymous, Registered (SheetUser moved to ClaudeSheet_CS) |
| Timers, user exceptions | 0 | Moved to ClaudeSheet_CS |
| Themes | 1 | ClaudeSheet |
| Resources | 1 | favicon.png |
| Site properties | 0 own | (TenantName, TenantId are template); DailyExpectedMinutes and SeedTestUsernames moved to ClaudeSheet_CS |
| Client variables | 2 | LastURL, Username |
| References | | ClaudeSheet_CS, OutSystemsUI, OutSystemsCharts, OutSystemsMaps, (System) |

## Notes on this refresh

- TT4 moved the backend to ClaudeSheet_CS. The per-object files of the moved server actions and of the SheetUser role were deleted; `server-actions/DoLogin.cs` is new (the template action had no snapshot before). The 4 SheetBlocks were re-read: they now call ClaudeSheet_CS actions and use its structures and entities.
- Light mode was re-read: `data-model.cs`, `structures.cs`, `timers.cs`, `settings.cs` and `user-exceptions.cs` now hold no own elements (the large deletions in the diff), and the names lists match.
- Reads made over raw HTTP and written by an equivalent of `extract-snapshot.mjs`.
