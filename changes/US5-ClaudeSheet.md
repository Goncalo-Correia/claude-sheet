# US5 · ClaudeSheet changes

What each `applyModelApiCode` save for US5 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-16 11:11:54 UTC

- **Published:** 2026-10-16 11:38:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved

Renamed the referenced role `SheetUser2` to `SheetUser`, and gave Login and InvalidPermissions the Anonymous role (no more "Missing roles" warnings).

## Save 2 · 2026-10-16 11:15:02 UTC

- **Published:** 2026-10-16 11:38:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved

Created the screen `MySheet` (default screen, SheetUser): the four blocks kept in sync through SelectedYear, SelectedMonth, WeekReferenceDate and RefreshKey, plus its menu link.

## Save 3 · 2026-10-16 11:16:49 UTC

- **Published:** 2026-10-16 11:38:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved

Warning cleanup: the four blocks use RefreshKey (`data-refresh-key` on their root container), the HoursCalendar `CalendarDays` list got a single direct child (`CalendarCell`), and the unused client action `Date_GetWeekStart` was deleted.
