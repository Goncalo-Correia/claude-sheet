# TT4 · ClaudeSheet changes

What each `applyModelApiCode` save for TT4 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-15 18:31:59 UTC

- **Published:** 2026-10-15 21:59:00 UTC (manual 1-Click Publish; logged by Claude, applied by the ClaudeSheet subagent)
- **Outcome:** saved

Switched ClaudeSheet to the ClaudeSheet_CS copies: every reference in the blocks, screens and client actions now points to the producer ("2"-suffixed while both existed). `GetCalendar` reads the daily minutes through `Settings_GetDailyExpectedMinutes`. Deleted the local backend elements.

## Save 2 · 2026-10-15 18:34:36 UTC

- **Published:** 2026-10-15 21:59:00 UTC (manual 1-Click Publish; logged by Claude, applied by the ClaudeSheet subagent)
- **Outcome:** saved

Updated the description of `HoursCalendar.OnClick_FillPendingButton`: the default time now comes from `Settings_GetDailyExpectedMinutes`.

## Save 3 · 2026-10-15 20:15:39 UTC

- **Published:** 2026-10-15 21:59:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved

Dropped the "2" suffix from the referenced ClaudeSheet_CS elements and from the Execute nodes named after them, now that the local copies are gone.
