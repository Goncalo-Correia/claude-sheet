# US4 · ClaudeSheet changes

What each `applyModelApiCode` save for US4 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-15 16:52:52 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved with 1 validation error(s)
- **Error:** /ProjectHours_GetWeekly/Build day header/DayHeader.DayName = If(DayOfWeek(DayHeader.Date) = 1, "Monday", If(DayOfWeek(DayHeader.Date) = 2, "Tuesday", If(DayOfWeek(DayHeader.Date) = 3, "Wednesday", If(DayOfWeek(DayHeader.Date) = 4, "Thursday", If(DayOfWeek(DayHeader.Date) = 5, "Friday", If(DayOfWeek(DayHeader.Date) = 6, "Saturday", "Sunday")))))) - Invalid Data Type (fixed by the next save)

Extended the weekly structures (`DateLabel`, `ShowDash`, `DayTotals`) and created the server action `ProjectHours_GetWeekly`: projects assigned or with entries in the week, sorted by client then project, minutes per project per day.

## Save 2 · 2026-10-15 16:53:33 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Set `WeekDayHeader.DayName` back to Text (renaming it had made Service Studio infer a Date).

## Save 3 · 2026-10-15 16:53:54 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Laid out `ProjectHours_GetWeekly` (positions only).

## Save 4 · 2026-10-15 16:57:20 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Created the block `WeeklyProjectHours` (inputs ReferenceDate, RefreshKey; event OnWeekChanged), its data action `GetWeek`, the week navigation and the table widgets.

## Save 5 · 2026-10-15 16:57:53 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved with 1 validation error(s)
- **Error:** /ClaudeSheet - Invalid Icon Library (expected sidecar error on theme stylesheet edits; Service Studio validation clean)

Theme CSS for the weekly table.
