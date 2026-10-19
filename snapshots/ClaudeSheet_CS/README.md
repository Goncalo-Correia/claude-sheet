# ClaudeSheet_CS snapshot

- **Module:** ClaudeSheet_CS (Reactive, Core): the backend of ClaudeSheet (ADR 0004)
- **Version:** 0 (from `listApps`)
- **Last refresh:** 2026-10-19, targeted mode, after the manual publish of TT8's ClaudeSheet_CS part

## Own elements (references excluded)

| Kind | Count | Elements |
|---|---|---|
| Entities | 6 | Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry (Public, Read Only) |
| Static entities | 2 | DayStatus, CalendarDayType (Public) |
| Structures | 16 | ProjectSummary, ProjectAssignmentSummary, UserOption (US7), ClientSummary (US6), CalendarDay, MonthSummary, TimeEntryInput, DaySheetProjectLine, DaySheetDetail, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours, NumberOption, DayEditorLine, DayStatusKPI (Public) |
| Server actions | 29 | Public: Holiday_GetList, Holiday_CreateOrUpdate, Holiday_SetActive, Holiday_CopyYear (US8), Project_GetList, Project_GetById, Project_CreateOrUpdate, Project_SetActive, ProjectAssignment_GetList, ProjectAssignment_CreateOrUpdate, ProjectAssignment_End, ProjectAssignment_Delete, User_GetActive (US7), Client_GetList, Client_GetById, Client_CreateOrUpdate, Client_SetActive (US6), Calendar_GetMonth, Calendar_GetMonthSummary, DaySheet_Clear, DaySheet_FillPending, DaySheet_GetDetail, DaySheet_Save, Project_GetAssignedToUser, ProjectHours_GetWeekly, Settings_GetDailyExpectedMinutes. Private: Bootstrap, Holiday_CreateIfMissing, Date_GetWeekStart |
| Roles | 3 own (+ Anonymous, Registered) | SheetUser, SheetAdmin, SheetManager (Public) |
| User exceptions | 5 | DaySheetValidation, ClientValidation, ProjectValidation, AssignmentValidation, HolidayValidation (all Public, set by hand: the Model API can't read or set that flag) |
| Site properties | 3 own (+ TenantName, TenantId) | DailyExpectedMinutes (480), SeedTestUsernames, SeedAdminUsernames |
| Timers | 1 | Timer_Bootstrap (Bootstrap, WhenPublished) |
| References | | (System) only |

## Notes on this refresh

- Format (2026-10-19, repository refactor): every file is Markdown, `<element>.md` with the code in one `csharp` block (name lists in a `json` block), and lines that only set a default value are left out. The content is otherwise unchanged.
- TT8 (2026-10-19), targeted mode: all 29 `server-actions/*.cs`. Every action is now in a server-action folder (Calendar, Client, Date, DaySheet, Holiday, Project, ProjectAssignment, ProjectHours, Settings, User, Setup), 84 nodes got labels and 17 Execute nodes were renamed (Bootstrap `CreateHoliday_<name>`, `GrantSheetUserRole_Admin`; ProjectHours_GetWeekly `AppendDayTotal`/`AppendCell`/`AppendRow`). Other files are as of the US8 refresh.

### US8 refresh (2026-10-19)

- US8 (2026-10-19), targeted mode: `data-model.md` (Holiday.IsFixedDate), `user-exceptions.md`, `names/server-actions.md`, `server-actions/Bootstrap.md` (IsFixedDate = False on the 3 movable holidays), `server-actions/Holiday_CreateIfMissing.md` (IsFixedDate in, Created out) and the 4 new `server-actions/Holiday_*.cs`. Other files are as of the US7 refresh.

### US7 refresh (2026-10-19)

- US7 (2026-10-19), targeted mode: `data-model.md` (ProjectAssignment.ProjectId delete rule Delete → Protect), `structures.md`, `user-exceptions.md`, `names/server-actions.md` and the 9 new `server-actions/Project*.cs` / `User_GetActive.cs`. The other module-wide files were not re-read (unchanged by US7). `structures.md` was rebuilt from the saved tool output (too large for the transcript); its diff also renumbers variables of existing structures.

### US6 refresh (2026-10-19)

- US6 (2026-10-19), targeted mode: `names/server-actions.md`, `structures.md`, `user-exceptions.md` and the 4 new `server-actions/Client_*.cs`. Other files are as of the TT5 refresh (unchanged by US6). `structures.md` was rebuilt from the saved tool output (too large for the transcript).

### TT5 refresh (2026-10-19)

- TT5 (2026-10-19), targeted mode: light reads plus `server-actions/Bootstrap.md` (admin seed loop) and the new `roles/SheetAdmin.md` and `roles/SheetManager.md`.
- `server-actions/Minutes_Format.md` was deleted with the action (it was unused; ClaudeSheet has its own client action).
- `settings.md` gained SeedAdminUsernames. `data-model.md`, `structures.md`, `timers.md` and `user-exceptions.md` are unchanged.
- Written by `extract-snapshot.mjs`; `structures.md` was too large for the transcript and was rebuilt from the saved tool output in the same format.

## Notes on the first snapshot (2026-10-15)

- Full mode: every module-wide read, every server action (`server-actions/`) and the role (`roles/SheetUser.md`).
- Reads made over raw HTTP (the Service Studio MCP tools aren't loaded in the Claude session) and written by an equivalent of `extract-snapshot.mjs`.
- `getDataModel` does not output entity indexes. The unique indexes are listed in `stories/US-000-mysheet-foundation.md`.
