# US11 · ClaudeSheet_CS changes

What each `applyModelApiCode` save for US11 changed in ClaudeSheet_CS, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet_CS/`.

## Save 1 · 2026-10-23 13:55:20 UTC

- **Published:** 2026-10-23 14:04:03 UTC
- **Outcome:** saved
- **Touched:** creates folder `MonthSheet`, static entity `PeriodStatus`, server entity `MonthSheet`, user exception `PeriodValidation`, structure `MonthSheetSummary`, server action `MonthSheet_IsLocked`, server action `MonthSheet_GetTotals`; sets node labels

Created the data model for monthly submissions: static entity `PeriodStatus` (Open, Submitted, Approved, Rejected), entity `MonthSheet` (one row per user and month, unique on user, year and month), user exception `PeriodValidation` and structure `MonthSheetSummary`. Added two private helpers in folder `MonthSheet`: the function `MonthSheet_IsLocked` (a month is locked when Submitted or Approved) and `MonthSheet_GetTotals` (a user's logged and expected minutes for a month).

## Save 2 · 2026-10-23 13:58:21 UTC

- **Published:** 2026-10-23 14:04:03 UTC
- **Outcome:** saved with 3 validation error(s)
- **Error:** /MonthSheet_Approve/Submitted? - Invalid Expression
- **Error:** /MonthSheet_Reject/Submitted? - Invalid Expression
- **Error:** /MonthSheet_Reopen/Approved? - Invalid Expression
- **Touched:** renames elements; sets node labels; moves nodes

Created the six public `MonthSheet_*` actions: `MonthSheet_GetForUser` (status of the current user's month), `MonthSheet_Submit` (only for ended months with no pending days), `MonthSheet_GetSubmitted` (submitted months, optionally with approved ones, for managers) and the manager decisions `MonthSheet_Approve`, `MonthSheet_Reject` (comment required) and `MonthSheet_Reopen`. This save left three status checks with an invalid expression, fixed in the next save.

## Save 3 · 2026-10-23 13:58:47 UTC

- **Published:** 2026-10-23 14:04:03 UTC
- **Outcome:** saved
- **Touched:** sets node labels

Fixed the status checks in `MonthSheet_Approve`, `MonthSheet_Reject` and `MonthSheet_Reopen` so they read the month's status correctly.

## Save 4 · 2026-10-23 13:59:19 UTC

- **Published:** 2026-10-23 14:04:03 UTC
- **Outcome:** saved
- **Touched:** sets node labels; moves nodes

Locked submitted and approved months: `DaySheet_Save`, `DaySheet_Clear` and `DaySheet_FillPending` now refuse with "This month is submitted or approved." Added the `IsLocked` output to `Calendar_GetMonth`.
