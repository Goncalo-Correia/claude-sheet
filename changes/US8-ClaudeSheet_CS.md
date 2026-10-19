# US8 · ClaudeSheet_CS changes

What each `applyModelApiCode` save for US8 changed in ClaudeSheet_CS, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet_CS/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-19 15:49:17 UTC

- **Published:** 2026-10-19 16:00:38 UTC (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Created the user exception `HolidayValidation`, the attribute `Holiday.IsFixedDate`, and the server actions `Holiday_GetList`, `Holiday_CreateOrUpdate`, `Holiday_SetActive` and `Holiday_CopyYear`. `Holiday_CreateIfMissing` got an IsFixedDate input and a Created output, and `Bootstrap` marks the 3 movable 2026 holidays.
