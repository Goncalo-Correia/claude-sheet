# TT8 · ClaudeSheet_CS changes

What each `applyModelApiCode` save for TT8 changed in ClaudeSheet_CS, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet_CS/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-19 16:22:58 UTC

- **Published:** 2026-10-19 16:28:23 UTC (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Created 11 server-action folders (Calendar, Client, Date, DaySheet, Holiday, Project, ProjectAssignment, ProjectHours, Settings, User, Setup) and moved the 29 server actions into them. Labelled 84 Assign and For Each nodes. Renamed 17 Execute nodes, e.g. Bootstrap's `Holiday_CreateIfMissing2…13` → `CreateHoliday_<name>`, `GrantSheetUserRole_Admin`, `AppendDayTotal` / `AppendCell` / `AppendRow`.
