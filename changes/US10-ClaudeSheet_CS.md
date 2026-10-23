# US10 · ClaudeSheet_CS changes

What each `applyModelApiCode` save for US10 changed in ClaudeSheet_CS, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet_CS/`.

## Save 1 · 2026-10-23 (before 13:04 UTC)

- **Published:** 2026-10-23 13:06 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Created the public server action `Project_GetAssignedInPeriod` in folder `Project`: the active projects (with an active client) the current user is assigned to on at least one day between StartDate and EndDate, each project once, as a list of `DaySheetProjectLine`. Changed `DaySheet_FillPending` so a pending day on which the user isn't assigned to the chosen project is skipped and counted in the new output `SkippedCount`, instead of failing the whole month.

## Save 2 · 2026-10-23 (before 13:04 UTC)

- **Published:** 2026-10-23 13:06 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Laid out the flows of `Project_GetAssignedInPeriod` and `DaySheet_FillPending` with the repository's layout tool (node positions only).

## Save 3 · 2026-10-23 (before 13:04 UTC)

- **Published:** 2026-10-23 13:06 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Moved the loop body of `DaySheet_FillPending` by hand so its three loop-back connectors don't cross other connectors or pass over nodes (node positions only).
