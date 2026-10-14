# US-000: MySheet foundation (data model, shared logic, theme, seed data)

- **Status:** Published (2026-10-14, manual 1-Click Publish)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-14

## Story

As the MySheet team, we want the database, shared server logic, theme variables and seed data in place, so that user stories US1–US5 can be built on top of them.

## Acceptance criteria

- [ ] AC0.1 Given the module is published, When I open the Data tab, Then all 6 entities, 2 static entities and 9 structures exist with the defined attributes and indexes
- [ ] AC0.2 Given a user already has a DaySheet for 14/10/2026, When a second DaySheet for the same user and date is inserted, Then the database rejects it (unique index)
- [ ] AC0.3 Given a DaySheet with 2 TimeEntries, When the DaySheet is deleted, Then its TimeEntries are deleted too
- [ ] AC0.4 Given a Project with TimeEntries, When someone tries to delete it, Then the delete is blocked (Protect)
- [ ] AC0.5 Given the module is published twice, When the Bootstrap timer runs, Then the seed data is not duplicated
- [ ] AC0.6 Given today is 14/10/2026 and Oct 5 is a holiday, When GetMonthCalendar(2026,10) runs, Then 05/10 = Holiday, Sat/Sun = Weekend, 15/10..31/10 weekdays = Future, and past weekdays without a DaySheet = Pending
- [ ] AC0.7 Given a Draft DaySheet on a past weekday, When GetMonthCalendar runs, Then that day is classified as Pending
- [ ] AC0.8 Given a DayOff DaySheet on a holiday date, When GetMonthCalendar runs, Then the day is classified as Holiday (precedence)
- [ ] AC0.9 Given FormatMinutes(2250,"h"), Then it returns "37h30"; FormatMinutes(0,":") returns "0:00"; FormatMinutes(65,":") returns "1:05"
- [ ] AC0.10 Given GetWeekStart(14/10/2026), Then it returns 12/10/2026; GetWeekStart(18/10/2026) returns 12/10/2026
- [ ] AC0.11 Given a logged-in user without the SheetUser role, When any server action is called, Then a NotRegistered/Security exception is raised and no data is returned
- [ ] AC0.12 Given two users A and B, When user A calls GetMonthCalendar, Then only user A's DaySheets and TimeEntries are considered

Scope checklist:

- [ ] Entities: Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry (with the unique indexes and delete rules below)
- [ ] Static entities: DayStatus (Draft, Confirmed, DayOff), CalendarDayType (Confirmed, DayOff, Holiday, Pending, Future, Weekend)
- [ ] Structures: CalendarDay, MonthSummary, TimeEntryInput, DaySheetDetail, DaySheetProjectLine, WeekDayHeader, WeeklyCell, WeeklyProjectRow, WeeklyHours
- [ ] Role SheetUser, granted to the test users by the bootstrap timer
- [ ] Site property DailyExpectedMinutes = 480
- [ ] FormatMinutes(Minutes, Separator) and GetWeekStart(Date) as client and server functions
- [ ] Server actions GetUserProjects, GetMonthCalendar, GetMonthSummary, all checking SheetUser and none taking a UserId input
- [ ] Theme CSS variables and day-* classes
- [ ] Timer_Bootstrap (When Published) seeding ICNF / "Resina e Pinha" (RUBUS) / assignments / Portuguese 2026 holidays, only when missing

### Day classification (first match wins)

1. Holiday: an active Holiday exists for the date
2. Day off: the user's DaySheet status = DayOff
3. Confirmed: the user's DaySheet status = Confirmed
4. Weekend: Saturday/Sunday
5. Future: date > today (server date)
6. Pending: anything else

IsEditable = True only for weekdays that are not holidays (future weekdays: Day off only).

### Out of scope

Back-office screens for clients, projects, assignments and holidays (seeded via bootstrap for now).

## Implementation plan

1. Role SheetUser; site properties DailyExpectedMinutes (480) and SeedTestUsernames.
2. Static entities DayStatus, CalendarDayType.
3. Entities Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry + indexes.
4. Structures.
5. Functions FormatMinutes, GetWeekStart (client + server).
6. Server actions GetUserProjects, GetMonthCalendar, GetMonthSummary.
7. Bootstrap server action + Timer_Bootstrap.
8. Theme CSS.

## Implementation notes

- Decision (2026-10-14): "test users" = usernames listed in the site property `SeedTestUsernames` (comma-separated). The timer grants SheetUser and creates the RUBUS assignment for each existing active user in that list.
- Decision (2026-10-14): timer named `Timer_Bootstrap` per `docs/conventions.md`; its action is `Bootstrap`.
- Implemented and merged 2026-10-14 (3 `applyModelApiCode` saves: data layer, logic, theme CSS). Live module: 0 validation errors.
- **Entities** all have a Long Integer auto-number `Id`. Unique indexes: `UQ_Client_Name`, `UQ_Project_ClientCode`, `UQ_PA_UserProjectStart` (UserId, ProjectId, StartDate), `UQ_Holiday_HolidayDate`, `UQ_DaySheet_UserDate`, `UQ_TimeEntry_SheetProject`. ProjectAssignment's index changed in review round 1: a user can have several periods on the same project, so the guideline's (UserId, ProjectId) index was replaced. The database does not block overlapping periods; the future back-office save action should check for them. `DaySheet.DayStatusId` defaults to `Draft`. Static entities also have `Is_Active` (standard template).
- **Structure fields were designed here** (the analysis document's data model wasn't available). Check them against it before US1–US5: `CalendarDay` (Date, DayNumber, WeekDay 1=Mon…7=Sun, IsWeekend, IsToday, DayTypeId, CssClass, DaySheetId, DayStatusId, HolidayName, TotalMinutes, IsEditable); `MonthSummary` (Year, Month, TotalMinutes, ElapsedDays, ExpectedMinutes, ProgressPercent, ConfirmedCount, DayOffCount, HolidayCount, PendingCount); `DaySheetDetail` has a `Lines` list of `DaySheetProjectLine`; `WeeklyHours` has `Days` (WeekDayHeader list) and `Rows` (WeeklyProjectRow list, each with a `Cells` WeeklyCell list).
- **GetUserProjects** returns `Projects` as a `DaySheetProjectLine` list (Minutes 0, TimeEntryId null), so the day editor can use it directly.
- **GetMonthCalendar** builds the days, then fills them from 3 aggregates (`GetMonthHolidays`, `GetMonthDaySheets`, `GetMonthMinutes` grouped by SheetDate) by indexing `Days[Day(date) - 1]`, then classifies. `IsEditable` = weekday and not a holiday. The "future weekdays only for Day off" restriction must be enforced by the save actions in US1–US5, not here.
- **GetMonthSummary**: `ElapsedDays` = days up to today classified Confirmed or Pending (working days, excluding weekends, holidays and days off). `ExpectedMinutes` = ElapsedDays × `Site.DailyExpectedMinutes`. `ProgressPercent` = TotalMinutes × 100 / ExpectedMinutes (0 when nothing is expected), not rounded. The KPI counts are Confirmed, DayOff, Holiday and Pending (the types with ShowInKPI = True).
- **Role check**: GetUserProjects, GetMonthCalendar and GetMonthSummary start with `If CheckSheetUserRole()` and otherwise raise the SheetUser role exception. No action takes a UserId; they all use `GetUserId()`. The pure functions FormatMinutes and GetWeekStart (server + client) touch no data and have no role check. `Bootstrap` and `Holiday_CreateIfMissing` run from the timer without a user session, so they have no role check either.
- **Bootstrap** is idempotent. It finds or creates client ICNF and project RUBUS, calls `Holiday_CreateIfMissing` 13 times, then for each active user whose username is in `Site.SeedTestUsernames` it grants SheetUser if missing and creates the RUBUS assignment (start 2026-01-01, no end) if missing. **`SeedTestUsernames` defaults to empty**: set it in Service Center, then re-run the timer.
- **Theme**: `--sheet-*` variables and `.day-*` classes appended to the ClaudeSheet theme after a `.ms-theme-start { }` guard rule. The sidecar's `InvalidIconLibrary` error on theme edits is expected; Service Studio's validation is clean.
- Pre-existing, not changed: the template's Login and InvalidPermissions screens are not marked Anonymous (warning `AnonymousAccess_NoPermissions`).

## Trail

- Changes: not in `changes/`. Two MCP publishes were blocked and you then published from Service Studio, so the hook never logged the lambdas. They were: (1) data layer: role, site properties, static entities, entities, indexes, structures; (2) functions, GetUserProjects, GetMonthCalendar, GetMonthSummary, Holiday_CreateIfMissing, Bootstrap, Timer_Bootstrap; (3) theme CSS; (4) review fix: ProjectAssignment index (UserId, ProjectId, StartDate). Final state is in `snapshots/ClaudeSheet/`
- Review: `reviews/ClaudeSheet/US-000.md`
- Published: see `PUBLISHES.md` (2026-10-14 15:45, success (manual))
- Snapshot: `snapshots/ClaudeSheet/` refreshed 2026-10-14 (targeted)

## Follow-ups

- Set `SeedTestUsernames` in Service Center, then run Timer_Bootstrap again so test users get SheetUser and the RUBUS assignment.
- Test the data-dependent ACs (0.2–0.8, 0.11, 0.12) on the published module.
- The template's Login and InvalidPermissions screens are not Anonymous (pre-existing).
