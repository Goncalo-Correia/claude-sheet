# US3 · ClaudeSheet changes

What each `applyModelApiCode` save for US3 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-15 14:15:29 UTC

- **Published:** 2026-10-15 14:34:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Created the structure `DayStatusKPI` and the block `DayStatusKPIs` (inputs Year, Month, RefreshKey), with the data action `GetKPIs` (counts per KPI day type from the month summary) and its error handler. Kept the plural labels of Confirmed / Pending ("7 Confirmed", "0 Pending").

## Save 2 · 2026-10-15 14:16:03 UTC

- **Published:** 2026-10-15 14:34:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

DayStatusKPIs widgets: one card per KPI when loaded, 4 skeleton cards while loading.

## Save 3 · 2026-10-15 14:16:33 UTC

- **Published:** 2026-10-15 14:34:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved with 1 validation error(s)
- **Error:** /ClaudeSheet - Invalid Icon Library (expected sidecar error on theme stylesheet edits; Service Studio validation clean)

Theme CSS for the KPI cards and skeletons.
