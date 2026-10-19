# ClaudeSheet_CS snapshot

- **Module:** ClaudeSheet_CS (Reactive, Core): the backend of ClaudeSheet (ADR 0004)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-19, targeted mode, after the manual publish of US6's producer part

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry (Public, Read Only) |
| Static entities | 2 | DayStatus, CalendarDayType (Public) |
| Structures | 13 | ClientSummary (US6), CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption, DayEditorLine, DayStatusKPI (Public) |
| Server actions | 16 | Public: Client_GetList, Client_GetById, Client_CreateOrUpdate, Client_SetActive (US6), Calendar_GetMonth, Calendar_GetMonthSummary, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, Project_GetAssignedToUser, ProjectHours_GetWeekly, Settings_GetDailyExpectedMinutes. Private: Bootstrap, Holiday_CreateIfMissing, Date_GetWeekStart |
| Roles | 3 own (+ Anonymous, Registered) | SheetUser, SheetAdmin, SheetManager (Public) |
| User exceptions | 2 | DaySheetValidation, ClientValidation (both Public, set by hand: the Model API can't read or set that flag) |
| Site properties | 3 own (+ TenantName, TenantId) | DailyExpectedMinutes (480), SeedTestUsernames, SeedAdminUsernames |
| Timers | 1 | Timer_Bootstrap (Bootstrap, WhenPublished) |
| References | | (System) only |

## Notes on this refresh

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
