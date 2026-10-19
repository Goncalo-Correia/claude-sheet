# TT1 · ClaudeSheet changes

What each `applyModelApiCode` save for TT1 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-15 15:02:01 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Renamed the reusable server actions to `<Entity>_<Verb>[<Qualifier>]` (e.g. `GetMonthSummary` → `Calendar_GetMonthSummary`).

## Save 2 · 2026-10-15 15:02:25 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Renamed the reusable client actions to `<Entity>_<Verb>` (e.g. `FormatMinutes` → `Minutes_Format`).

## Save 3 · 2026-10-15 15:02:44 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Renamed the On After Fetch handlers of the blocks to `OnAfterFetch_<DataSource>`.

## Save 4 · 2026-10-15 15:03:06 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

HoursCalendar: renamed the event handlers to `<Event>_<Target>`, the lifecycle actions to the event name, and the helper actions to verb + noun.

## Save 5 · 2026-10-15 15:03:41 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

HoursCalendar: split `RecalcEditorTotal`, which was both an event handler and a helper. New handler `OnChange_EditorTime` (bound to both time dropdowns) calls the helper.

## Save 6 · 2026-10-15 15:04:18 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved

Re-laid out `DaySheet_Save` and `HoursCalendar.OnInitialize` on the standard grid (1828 × 1371). Positions only.
