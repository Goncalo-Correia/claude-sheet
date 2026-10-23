# US11 · ClaudeSheet changes

What each `applyModelApiCode` save for US11 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`.

## Save 1 · 2026-10-23 14:05:43 UTC

- **Published:** 2026-10-23 14:22:44 UTC
- **Outcome:** saved
- **Touched:** existing elements only (see the snapshot diff)

Added the new ClaudeSheet_CS elements to the reference, using a copy of ClaudeSheet_CS's OML: the six `MonthSheet_*` actions, structure `MonthSheetSummary` and static entity `PeriodStatus`. The references were then refreshed, so `Calendar_GetMonth` shows its new `IsLocked` output.

## Save 2 · 2026-10-23 14:08:40 UTC

- **Published:** 2026-10-23 14:22:44 UTC
- **Outcome:** saved
- **Touched:** creates screen action `OnClick_SubmitMonthButton`; renames elements; sets node labels

In `HoursCalendar`, the calendar now shows the month's status ("Month: Open / Submitted / Approved / Rejected") and, for a rejected month, the manager's comment. A new "Submit month" button submits the shown month. When the month is submitted or approved, the day editor opens read-only with a short note, and "Fill all pending days" and "Submit month" are hidden.

## Save 3 · 2026-10-23 14:11:46 UTC

- **Published:** 2026-10-23 14:22:44 UTC
- **Outcome:** saved
- **Touched:** creates screen `Approvals`, data action `GetApprovals`; deletes elements; renames elements; sets node labels

Added the manager screen `Approvals` (SheetManager only). It lists submitted months with logged vs expected hours and lets a manager approve, reject with a comment, or reopen an approved month ("Show approved"). The menu shows an "Approvals" link to managers only.

## Save 4 · 2026-10-23 14:12:18 UTC

- **Published:** 2026-10-23 14:22:44 UTC
- **Outcome:** saved with 1 validation error(s)
- **Error:** /ClaudeSheet - Invalid Icon Library
- **Touched:** renames elements

Added the theme styles for the month status badge, the rejection comment and the Submit button's spacing, and a column layout for the Approvals table.
