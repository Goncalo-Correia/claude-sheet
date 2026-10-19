# Server action names

Name list in ClaudeSheet_CS, as returned by Service Studio at the last publish.

```json
[
  {
    "Name": "AbortTransaction",
    "Description": "Issues a database ROLLBACK command that undoes all changes performed on the database since the last commit.",
    "IsReferenced": true
  },
  {
    "Name": "Bootstrap",
    "Description": "Seeds reference data when missing: client ICNF, project RUBUS, Portuguese 2026 holidays, SheetUser + RUBUS assignment for the users in Site.SeedTestUsernames, and SheetAdmin + SheetUser for the users in Site.SeedAdminUsernames. Safe to run more than once. Run by Timer_Bootstrap.",
    "IsReferenced": false
  },
  {
    "Name": "Calendar_GetMonth",
    "Description": "One CalendarDay per day of the month for the current user, classified (first match wins) as Holiday, DayOff, Confirmed, Weekend, Future (after today, server date) or Pending. IsEditable = weekday that is not a holiday.",
    "IsReferenced": false
  },
  {
    "Name": "Calendar_GetMonthSummary",
    "Description": "Month totals and KPI counts for the current user, derived from GetMonthCalendar. TotalMinutes = minutes on Draft/Confirmed day sheets. ExpectedMinutes = (weekdays - holidays - days off of the whole month) * Site.DailyExpectedMinutes. ProgressPercent = Total/Expected*100 capped at 100 (0 when Expected = 0). ElapsedDays = calendar days elapsed in the month up to today (past month = days in month, future month = 0).",
    "IsReferenced": false
  },
  {
    "Name": "Client_CreateOrUpdate",
    "Description": "Creates or updates a client (SheetAdmin only). Name is trimmed, required and unique (case-insensitive by the database collation); raises ClientValidation with a user-facing message otherwise. Returns the client's Id.",
    "IsReferenced": false
  },
  {
    "Name": "Client_GetById",
    "Description": "One client (ClientRecord) with its projects sorted by name (SheetAdmin only). Raises ClientValidation when the client doesn't exist.",
    "IsReferenced": false
  },
  {
    "Name": "Client_GetList",
    "Description": "Clients for the admin list (SheetAdmin only): name, active flag and number of projects, sorted by name. IncludeInactive also returns deactivated clients; Search filters by name (contains).",
    "IsReferenced": false
  },
  {
    "Name": "Client_SetActive",
    "Description": "Activates or deactivates a client (SheetAdmin only); clients are deactivated instead of deleted. Projects of an inactive client are no longer offered to users (Project_GetAssignedToUser filters on the client's IsActive).",
    "IsReferenced": false
  },
  {
    "Name": "CommitTransaction",
    "Description": "Issues a database COMMIT command that makes effective all changes done on the database since the last commit.",
    "IsReferenced": true
  },
  {
    "Name": "Date_GetWeekStart",
    "Description": "Returns the Monday of the week that contains Date.",
    "IsReferenced": false
  },
  {
    "Name": "DaySheet_Clear",
    "Description": "Deletes the current user's DaySheet for Date (its TimeEntries are deleted by the Delete rule). The day goes back to Pending or Future.",
    "IsReferenced": false
  },
  {
    "Name": "DaySheet_FillPending",
    "Description": "Fills and confirms every Pending day of the month for the current user: a Draft with a total > 0 is confirmed as is; any other Pending day is saved through SaveDaySheet with one TimeEntry (ProjectId, MinutesPerDay) and confirmed. Runs in one request, so any error rolls back every day. Returns the number of days filled.",
    "IsReferenced": false
  },
  {
    "Name": "DaySheet_GetDetail",
    "Description": "Day editor data for the current user: DaySheet status and notes, holiday, and one line per project assigned on Date (GetUserProjects) merged with the existing TimeEntry minutes and description.",
    "IsReferenced": false
  },
  {
    "Name": "DaySheet_Save",
    "Description": "Validates and saves the current user's day: weekday, not a holiday, no hours in the future, projects assigned on Date, minutes multiple of 15, total <= 24:00, Confirm needs a total > 0. Upserts the DaySheet (DayOff / Confirmed / Draft) and its TimeEntries; IsDayOff deletes the day's entries. Raises DaySheetValidation with a user-facing message.",
    "IsReferenced": false
  },
  {
    "Name": "Holiday_CopyYear",
    "Description": "Copies the active fixed-date holidays (IsFixedDate) of FromYear to the same day and month of ToYear (SheetAdmin only), skipping dates that already have a holiday and 29 February. Returns CopiedCount; running it again copies nothing.",
    "IsReferenced": false
  },
  {
    "Name": "Holiday_CreateIfMissing",
    "Description": "Creates an active Holiday for HolidayDate unless one already exists for that date (active or not); Created tells whether it did. IsFixedDate (default True) is stored on create; when it is False and the existing holiday is still marked fixed, the existing one is marked movable. Used by Bootstrap and Holiday_CopyYear.",
    "IsReferenced": false
  },
  {
    "Name": "Holiday_CreateOrUpdate",
    "Description": "Creates or updates a holiday (SheetAdmin only). Name is trimmed and required, the date is required and unique (UQ_Holiday_HolidayDate, inactive holidays included). Raises HolidayValidation with a user-facing message otherwise. Returns the holiday's Id.",
    "IsReferenced": false
  },
  {
    "Name": "Holiday_GetList",
    "Description": "Holidays of Year sorted by date (SheetAdmin only). IncludeInactive also returns deactivated holidays.",
    "IsReferenced": false
  },
  {
    "Name": "Holiday_SetActive",
    "Description": "Activates or deactivates a holiday (SheetAdmin only); holidays are deactivated instead of deleted. Inactive holidays are ignored by the calendar (Calendar_GetMonth filters on IsActive).",
    "IsReferenced": false
  },
  {
    "Name": "ListAll",
    "Description": "Determines if all the elements in the list satisfy the given condition.",
    "IsReferenced": true
  },
  {
    "Name": "ListAny",
    "Description": "Determines if any of the elements in the list satisfies the given condition.",
    "IsReferenced": true
  },
  {
    "Name": "ListAppend",
    "Description": "Adds an element to the end of a list.",
    "IsReferenced": true
  },
  {
    "Name": "ListAppendAll",
    "Description": "Adds the elements of the source list to the end of the destination list.",
    "IsReferenced": true
  },
  {
    "Name": "ListClear",
    "Description": "Removes all elements from a list.",
    "IsReferenced": true
  },
  {
    "Name": "ListDuplicate",
    "Description": "Duplicates the elements of a list into another list.",
    "IsReferenced": true
  },
  {
    "Name": "ListFilter",
    "Description": "Returns a new list with the elements from the List parameter satisfying the given condition.",
    "IsReferenced": true
  },
  {
    "Name": "ListIndexOf",
    "Description": "Returns the position of the first element of the List parameter that satisfies the given condition, or -1 if no element satisfying the condition was found.",
    "IsReferenced": true
  },
  {
    "Name": "ListInsert",
    "Description": "Inserts an element in a specific position of a list.",
    "IsReferenced": true
  },
  {
    "Name": "ListRemove",
    "Description": "Removes an element from a specific position of a list.",
    "IsReferenced": true
  },
  {
    "Name": "ListSort",
    "Description": "Sorts the elements of the List parameter by the given criteria.",
    "IsReferenced": true
  },
  {
    "Name": "LogMessage",
    "Description": "Registers information that can be presented in Service Center in the module general log.",
    "IsReferenced": true
  },
  {
    "Name": "Project_CreateOrUpdate",
    "Description": "Creates or updates a project (SheetAdmin only). Name and Code are trimmed and required, the client must exist and be active, and Code is unique per client (case-insensitive by the database collation). Raises ProjectValidation with a user-facing message otherwise. Returns the project's Id.",
    "IsReferenced": false
  },
  {
    "Name": "Project_GetAssignedToUser",
    "Description": "Active projects (with active client) the current user is assigned to on ForDate, sorted by client and project name.",
    "IsReferenced": false
  },
  {
    "Name": "Project_GetById",
    "Description": "One project (ProjectRecord) for the admin detail screen (SheetAdmin only). Raises ProjectValidation when the project doesn't exist.",
    "IsReferenced": false
  },
  {
    "Name": "Project_GetList",
    "Description": "Projects for the admin list (SheetAdmin only), with client name and the number of distinct users with an active assignment that hasn't ended, sorted by client and project name. ClientId filters by client (empty = all); IncludeInactive also returns deactivated projects.",
    "IsReferenced": false
  },
  {
    "Name": "Project_SetActive",
    "Description": "Activates or deactivates a project (SheetAdmin only); projects are deactivated instead of deleted (ProjectAssignment.ProjectId and TimeEntry.ProjectId are Protect). Inactive projects are no longer offered to users (Project_GetAssignedToUser filters on Project.IsActive).",
    "IsReferenced": false
  },
  {
    "Name": "ProjectAssignment_CreateOrUpdate",
    "Description": "Creates or updates an assignment (SheetAdmin only): user and start date required; EndDate empty (open end) or not before StartDate; project and its client active; no overlap with another active period of the same user and project, nor the same start date (ADR 0002). Raises AssignmentValidation with a user-facing message otherwise. Saves the assignment as active and returns its Id.",
    "IsReferenced": false
  },
  {
    "Name": "ProjectAssignment_Delete",
    "Description": "Deletes an assignment (SheetAdmin only), only when its user has no TimeEntry on its project between StartDate and EndDate (open end = no limit). Otherwise raises AssignmentValidation asking to end it instead (ProjectAssignment_End).",
    "IsReferenced": false
  },
  {
    "Name": "ProjectAssignment_End",
    "Description": "Ends an assignment on EndDate (SheetAdmin only): EndDate required and not before the assignment's StartDate. Raises AssignmentValidation otherwise. Use it instead of ProjectAssignment_Delete once the user has time entries.",
    "IsReferenced": false
  },
  {
    "Name": "ProjectAssignment_GetList",
    "Description": "Assignments of a project (ProjectId) or of a user (UserId), or both, with project, client and user names and Status on today's date (Inactive, Upcoming, Ended, Active), sorted by client, project, user and start date (SheetAdmin only). Raises AssignmentValidation when both are empty.",
    "IsReferenced": false
  },
  {
    "Name": "ProjectHours_GetWeekly",
    "Description": "Weekly grid for the current user: the Monday-Sunday week containing ReferenceDate, day headers classified like the calendar (Calendar_GetMonth), one row per project assigned during the week or with entries in it (sorted by client, project), 7 cells per row with the \"-\" rule, totals per row, per day and for the week.",
    "IsReferenced": false
  },
  {
    "Name": "SetCurrentLocale",
    "Description": "Sets the language locale of the user session to change the application presentation language.",
    "IsReferenced": true
  },
  {
    "Name": "Settings_GetDailyExpectedMinutes",
    "Description": "Returns the DailyExpectedMinutes site property (minutes a user is expected to record per working day), for modules that can't read the site property directly.",
    "IsReferenced": false
  },
  {
    "Name": "User_GetActive",
    "Description": "Active users for a dropdown (SheetAdmin only): UserId, name and username, sorted by name. Used by the assignment popup in ClaudeSheet.",
    "IsReferenced": false
  }
]
```
