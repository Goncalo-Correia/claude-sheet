# ClaudeSheet snapshot

- **Module:** ClaudeSheet (Reactive, End-user)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-15, targeted mode, after the manual publish of story US3

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry |
| Static entities | 2 | DayStatus, CalendarDayType |
| Structures | 12 | CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption (US2), DayEditorLine (US2), DayStatusKPI (US3) |
| Server actions | 13 | Bootstrap, ClearDaySheet, DoLogin, DoLogout, FillPendingDays, FormatMinutes, GetDaySheetDetail, GetMonthCalendar, GetMonthSummary, GetUserProjects, GetWeekStart, Holiday_CreateIfMissing, SaveDaySheet |
| Client actions | 2 | FormatMinutes, GetWeekStart |
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

- Light mode (all module-wide reads) plus `web-blocks/DayStatusKPIs.cs` (new) and `themes/ClaudeSheet.cs`.
- The reads were made over raw HTTP (the Service Studio MCP tools didn't load into the Claude session), so `extract-snapshot.mjs` could not find them in the transcript. The files were written by an equivalent script with the same cleaning rules (no fences, LF, names sorted).
- US3 changes visible here: `data-model.cs` (PluralLabel of CalendarDayType Confirmed and Pending), `structures.cs` (DayStatusKPI), `names/web-blocks.json` and `web-blocks/DayStatusKPIs.cs`, `themes/ClaudeSheet.cs` (US3 stylesheet section).
- `structures.cs` is complete again: it now also contains the US2 structures `NumberOption` and `DayEditorLine`, which the previous refresh skipped. That explains most of its diff (structures are now in the read order).
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
