# ClaudeSheet_CS snapshot

- **Module:** ClaudeSheet_CS (Reactive, Core): the backend of ClaudeSheet (ADR 0004)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-23, targeted mode, after the publish of US11's ClaudeSheet_CS part

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 7 | MonthSheet (US11), Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry (Public, Read Only) |
| Static entities | 3 | PeriodStatus (US11), DayStatus, CalendarDayType (Public) |
| Structures | 18 | MonthSheetSummary (US11), UserAccess (US9), ProjectSummary, ProjectAssignmentSummary, UserOption (US7), ClientSummary (US6), CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption, DayEditorLine, DayStatusKPI (Public) |
| Server actions | 40 | Public: MonthSheet_GetForUser, MonthSheet_Submit, MonthSheet_GetSubmitted, MonthSheet_Approve, MonthSheet_Reject, MonthSheet_Reopen (US11), Project_GetAssignedInPeriod (US10), User_GetList, User_SetRoles (US9), Holiday_GetList, Holiday_CreateOrUpdate, Holiday_SetActive, Holiday_CopyYear (US8), Project_GetList, Project_GetById, Project_CreateOrUpdate, Project_SetActive, ProjectAssignment_GetList, ProjectAssignment_CreateOrUpdate, ProjectAssignment_End, ProjectAssignment_Delete, User_GetActive (US7), Client_GetList, Client_GetById, Client_CreateOrUpdate, Client_SetActive (US6), Calendar_GetMonth, Calendar_GetMonthSummary, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, Project_GetAssignedToUser, ProjectHours_GetWeekly, Settings_GetDailyExpectedMinutes. Private: MonthSheet_IsLocked (function), MonthSheet_GetTotals (US11), Bootstrap, Holiday_CreateIfMissing, Date_GetWeekStart |
| Roles | 3 own (+ Anonymous, Registered) | SheetUser, SheetAdmin, SheetManager (Public) |
| User exceptions | 6 | PeriodValidation (US11, not Public), DaySheetValidation, ClientValidation, ProjectValidation, AssignmentValidation, HolidayValidation (Public, set by hand: the Model API can't read or set that flag) |
| Site properties | 3 own (+ TenantName, TenantId) | DailyExpectedMinutes (480), SeedTestUsernames, SeedAdminUsernames |
| Timers | 1 | Timer_Bootstrap (Bootstrap, WhenPublished) |
| References | | (System) only |

## Notes on this refresh

- US11 (2026-10-23), targeted mode: `data-model.cs` (MonthSheet, PeriodStatus), `structures.cs` (MonthSheetSummary; its diff also renumbers variables of existing structures), `user-exceptions.cs`, `names/server-actions.json`, the 8 new `server-actions/MonthSheet_*.cs` and the changed `DaySheet_Save`, `DaySheet_Clear`, `DaySheet_FillPending`, `Calendar_GetMonth`. The other module-wide files were not re-read (unchanged by US11). The read-back omits aggregate sort directions: MonthSheet_GetSubmitted's GetMonths sorts Year and Month **descending** (checked with a probe), although `MonthSheet_GetSubmitted.cs` doesn't show it. `getDataModel` doesn't output indexes: MonthSheet has the unique index UQ_MonthSheet_UserYearMonth (UserId, Year, Month).

### US10 refresh (2026-10-23)

- US10 (2026-10-23), targeted mode: light reads plus `server-actions/Project_GetAssignedInPeriod.cs` (new) and `server-actions/DaySheet_FillPending.cs` (SkippedCount output, GetAssignmentOnDay + Count skipped, loop body re-laid out). The diff also shows both in `names/server-actions.json`; every other module-wide file is unchanged. Counts above now include US9's UserAccess, User_GetList and User_SetRoles.

### TT8 refresh (2026-10-19)

- TT8 (2026-10-19), targeted mode: all 29 `server-actions/*.cs`. Every action is now in a server-action folder (Calendar, Client, Date, DaySheet, Holiday, Project, ProjectAssignment, ProjectHours, Settings, User, Setup), 84 nodes got labels and 17 Execute nodes were renamed (Bootstrap `CreateHoliday_<name>`, `GrantSheetUserRole_Admin`; ProjectHours_GetWeekly `AppendDayTotal`/`AppendCell`/`AppendRow`). Other files are as of the US8 refresh.

### US8 refresh (2026-10-19)

- US8 (2026-10-19), targeted mode: `data-model.cs` (Holiday.IsFixedDate), `user-exceptions.cs`, `names/server-actions.json`, `server-actions/Bootstrap.cs` (IsFixedDate = False on the 3 movable holidays), `server-actions/Holiday_CreateIfMissing.cs` (IsFixedDate in, Created out) and the 4 new `server-actions/Holiday_*.cs`. Other files are as of the US7 refresh.

### US7 refresh (2026-10-19)

- US7 (2026-10-19), targeted mode: `data-model.cs` (ProjectAssignment.ProjectId delete rule Delete → Protect), `structures.cs`, `user-exceptions.cs`, `names/server-actions.json` and the 9 new `server-actions/Project*.cs` / `User_GetActive.cs`. The other module-wide files were not re-read (unchanged by US7). `structures.cs` was rebuilt from the saved tool output (too large for the transcript); its diff also renumbers variables of existing structures.

### US6 refresh (2026-10-19)

- US6 (2026-10-19), targeted mode: `names/server-actions.json`, `structures.cs`, `user-exceptions.cs` and the 4 new `server-actions/Client_*.cs`. Other files are as of the TT5 refresh (unchanged by US6). `structures.cs` was rebuilt from the saved tool output (too large for the transcript).

### TT5 refresh (2026-10-19)

- TT5 (2026-10-19), targeted mode: light reads plus `server-actions/Bootstrap.cs` (admin seed loop) and the new `roles/SheetAdmin.cs` and `roles/SheetManager.cs`.
- `server-actions/Minutes_Format.cs` was deleted with the action (it was unused; ClaudeSheet has its own client action).
- `settings.cs` gained SeedAdminUsernames. `data-model.cs`, `structures.cs`, `timers.cs` and `user-exceptions.cs` are unchanged.
- Written by `extract-snapshot.mjs`; `structures.cs` was too large for the transcript and was rebuilt from the saved tool output in the same format.

## Notes on the first snapshot (2026-10-15)

- Full mode: every module-wide read, every server action (`server-actions/`) and the role (`roles/SheetUser.cs`).
- Reads made over raw HTTP (the Service Studio MCP tools aren't loaded in the Claude session) and written by an equivalent of `extract-snapshot.mjs`.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
