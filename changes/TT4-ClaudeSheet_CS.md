# TT4 · ClaudeSheet_CS changes

What each `applyModelApiCode` save for TT4 changed in ClaudeSheet_CS, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet_CS/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-15 17:50:23 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved

Created the site properties `DailyExpectedMinutes` (480) and `SeedTestUsernames`, the role `SheetUser` and the user exception `DaySheetValidation`, copied from ClaudeSheet.

## Save 2 · 2026-10-15 17:50:49 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved

Created the static entities `DayStatus` and `CalendarDayType` with their attributes and records.

## Save 3 · 2026-10-15 17:51:27 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved

Created the entities `Client`, `Project`, `ProjectAssignment`, `Holiday`, `DaySheet` and `TimeEntry` with their attributes, unique indexes and delete rules.

## Save 4 · 2026-10-15 17:52:20 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved

Created the 12 structures (`CalendarDay`, `MonthSummary`, `TimeEntryInput`, `DaySheetProjectLine`, `DaySheetDetail`, `WeekDayHeader`, `WeeklyCell`, `WeeklyProjectRow`, `WeeklyHours`, `NumberOption`, `DayEditorLine`, `DayStatusKPI`).

## Save 5 · 2026-10-15 17:55:12 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved

Created the first server actions: `Date_GetWeekStart`, `Minutes_Format`, `Holiday_CreateIfMissing`, `Project_GetAssignedToUser`, `Calendar_GetMonth`, `Settings_GetDailyExpectedMinutes`. Same logic and layout as in ClaudeSheet.

## Save 6 · 2026-10-15 17:56:35 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved

Created the remaining server actions (`Calendar_GetMonthSummary`, `DaySheet_GetDetail`, `DaySheet_Save`, `DaySheet_Clear`, `DaySheet_FillPending`, `ProjectHours_GetWeekly`, `Bootstrap`) and the timer `Timer_Bootstrap`.
