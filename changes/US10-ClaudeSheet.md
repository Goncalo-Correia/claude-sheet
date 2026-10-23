# US10 · ClaudeSheet changes

What each `applyModelApiCode` save for US10 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`.

## Save 1 · 2026-10-23 (between 13:06 and 13:20 UTC)

- **Published:** 2026-10-23 13:20 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved (merged on its own, before the build saves)

Added `Project_GetAssignedInPeriod` to the `ClaudeSheet_CS` reference, using a copy of ClaudeSheet_CS's OML instead of Manage Dependencies. The references were then refreshed so `DaySheet_FillPending` shows its new `SkippedCount` output.

## Save 2 · 2026-10-23 (between 13:06 and 13:20 UTC)

- **Published:** 2026-10-23 13:20 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

In `HoursCalendar`, the day editor now shows "No projects are assigned to you on this day. Ask your administrator." instead of the project lines when there are none, and hides Save as draft and Confirm day in that case. It also has a "Notes" text area (500 characters) for the day's notes. "Fill all pending days" now offers the projects assigned in the month on screen. When there are none, the popup says "No projects assigned this month" instead of showing the dropdown. After the fill, the message gives the days filled and the days skipped.

## Save 3 · 2026-10-23 (between 13:06 and 13:20 UTC)

- **Published:** 2026-10-23 13:20 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Renamed the notes text area back to `NotesInput`: binding it to `EditorNotes` had renamed it automatically.
