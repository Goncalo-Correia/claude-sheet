# ClaudeSheet_CS snapshot

- **Module:** ClaudeSheet_CS (Reactive, Core): the backend of ClaudeSheet (ADR 0004)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-15, full mode, first snapshot, after the manual publish of TT4's producer part

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry (Public, Read Only) |
| Static entities | 2 | DayStatus, CalendarDayType (Public) |
| Structures | 12 | CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption, DayEditorLine, DayStatusKPI (Public) |
| Server actions | 13 | Public: Calendar_GetMonth, Calendar_GetMonthSummary, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, Project_GetAssignedToUser, ProjectHours_GetWeekly, Settings_GetDailyExpectedMinutes. Private: Bootstrap, Holiday_CreateIfMissing, Date_GetWeekStart, Minutes_Format |
| Roles | 1 own (+ Anonymous, Registered) | SheetUser (Public) |
| User exceptions | 1 | DaySheetValidation (Public, set by hand: the Model API can't read or set that flag) |
| Site properties | 2 own (+ TenantName, TenantId) | DailyExpectedMinutes (480), SeedTestUsernames |
| Timers | 1 | Timer_Bootstrap (Bootstrap, WhenPublished) |
| References | | (System) only |

## Notes on this refresh

- Full mode: every module-wide read, every server action (`server-actions/`) and the role (`roles/SheetUser.cs`).
- Reads made over raw HTTP (the Service Studio MCP tools aren't loaded in the Claude session) and written by an equivalent of `extract-snapshot.mjs`.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
