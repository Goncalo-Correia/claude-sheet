# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-15, targeted mode, after the manual publish of TT1

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

- TT1 renamed 9 server actions, 2 client actions and 13 block actions (see `stories/TT1-apply-conventions.md`). The per-object files of the old names were deleted and the new ones written: `server-actions/` (9), `client-actions/` (2), `web-blocks/TotalHoursMonth.cs`, `HoursCalendar.cs`, `DayStatusKPIs.cs`.
- Light mode was re-read too. `data-model.cs`, `structures.cs`, the other module-wide files and `themes/ClaudeSheet.cs` are unchanged, as TT1 changed no data, structure or CSS. `names/server-actions.json` and `names/client-actions.json` show the new names.
- Old names still appear in 8 Description texts (a rename doesn't rewrite free text). This was accepted in the TT1 review (A06).
- Reads made over raw HTTP and written by an equivalent of `extract-snapshot.mjs` (same cleaning and sort order), as for US3.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
