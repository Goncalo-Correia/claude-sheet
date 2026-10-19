# US8: [Screen] Admin: manage holidays per year

- **Status:** Published
- **Published:** ClaudeSheet_CS 2026-10-19 16:00, ClaudeSheet 2026-10-19 16:13 (both manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-19
- **Depends on:** TT5 (SheetAdmin role, Admin UI flow), TT7 (admin CSS in the theme)
- **Source:** gap analysis of 2026-10-16, findings F05, F10, approved 2026-10-19 (the analysis file is in commit 5bb59c3)
- **Trello:** https://trello.com/c/PIuqB7g0 (moved to Done 2026-10-19)

## Story

As an admin I want to maintain the holiday calendar per year, so that the calendar, KPIs and expected hours stay right after 2026. Today only the 2026 Portuguese holidays exist, from the Bootstrap seed. Covers: F05, F10.

## Acceptance criteria

- [x] A01 · AC8.1 Given year 2026, Then the 13 seeded holidays are listed.
- [x] A02 · AC8.2 Given "Copy fixed holidays to 2027", Then 2027 gets the fixed-date ones; running it again adds nothing.
- [x] A03 · AC8.3 Given a holiday is added on 08/12/2027, Then MySheet for December 2027 shows it as a holiday and expected hours drop by one day.
- [x] A04 · AC8.4 Given two holidays on the same date, Then the second save is rejected with a clear message.
- [x] A05 · AC8.5 Given a SheetUser without SheetAdmin, Then the screen and actions are refused; 0 errors and 0 warnings.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Public user exception HolidayValidation.
2. P02 · Public server actions (SheetAdmin check): Holiday_GetList (Year, IncludeInactive), Holiday_CreateOrUpdate (date unique, name required), Holiday_SetActive, Holiday_CopyYear (FromYear, ToYear; copies fixed-date holidays, skips existing dates, returns the count).

### ClaudeSheet

Dependencies to add in Service Studio › Manage Dependencies › ClaudeSheet_CS: the 4 actions, HolidayValidation, SheetAdmin.

- Server Actions: Holiday_GetList, Holiday_CreateOrUpdate, Holiday_SetActive, Holiday_CopyYear
- Exceptions: HolidayValidation
- Entities: Holiday (already referenced; refresh brings IsFixedDate). Roles: SheetAdmin (already referenced)

3. P03 · Screen Admin › Holidays (SheetAdmin): year selector; table Date, Weekday, Name, Active; add and edit in a popup; deactivate; "Copy fixed holidays to next year".
4. P04 · Menu link "Holidays", visible to SheetAdmin.

Decisions before building (2026-10-19): the Holiday entity can't tell fixed-date holidays from movable ones, so (user's choice) ClaudeSheet_CS gets **Holiday.IsFixedDate** (Boolean, default True). Bootstrap marks the 3 seeded movable 2026 holidays (03/04 Good Friday, 05/04 Easter, 04/06 Corpus Christi) as not fixed, and the Holidays popup gets a "Same date every year" checkbox. Holiday_CopyYear copies only IsFixedDate = True.

## Implementation notes

### ClaudeSheet_CS

- Implemented 2026-10-19 in **1 `applyModelApiCode` save** (one dry run before it), 0 validation messages.
- **P01:** user exception HolidayValidation created. **Public must be set by hand** in Service Studio before publishing (Model API limit, as in US6/US7).
- **Added (decision above):**
  - **Holiday.IsFixedDate** (Boolean, mandatory, default True). Existing rows get True when the column is added.
  - **Holiday_CreateIfMissing** (private) gets an optional input IsFixedDate (default True) and an output **Created**. When the holiday exists, IsFixedDate is False and the existing one is still marked fixed, the new branch `Mark movable?` → UpdateHoliday marks it movable.
  - **Bootstrap:** only arguments change. The 3 movable 2026 nodes pass IsFixedDate = False, so the next publish (Timer_Bootstrap, WhenPublished) corrects the existing 2026 rows.
- **P02 · actions** (all public, each starting with `Is SheetAdmin?`, role exception "Only administrators can manage holidays."):
  - `Holiday_GetList(Year, IncludeInactive)` → `Holidays` (List of Holiday). Aggregate **GetYearHolidays**: date within Year, active unless IncludeInactive, sorted by date. Named non-default on purpose: the default `GetHolidays` was auto-renamed `GetHolidaysByIsActive` in the dry run.
  - `Holiday_CreateOrUpdate(HolidayRecord)` → `HolidayId`. Checks "Enter the holiday name." and "Enter the holiday date.", then GetSameDate (same date, other Id, inactive included, matching UQ_Holiday_HolidayDate) → "There is already a holiday on this date (<name>)." Then the name is trimmed and CreateOrUpdateHoliday runs.
  - `Holiday_SetActive(HolidayId, IsActive)`: GetHoliday → "Holiday not found." → UpdateHoliday.
  - `Holiday_CopyYear(FromYear, ToYear)` → `CopiedCount`. "Choose a different year to copy to." when the years are equal.
    - GetFixedHolidays: active, IsFixedDate, in FromYear, skipping 29 February.
    - For Each → Holiday_CreateIfMissing(NewDate(ToYear, month, day), name, IsFixedDate True) → CopiedCount + 1 when Created. Dates that already exist are skipped, so a second run copies 0.
- **Flow layout gate:** Holiday_GetList, Holiday_CreateOrUpdate, Holiday_SetActive, Holiday_CopyYear and Holiday_CreateIfMissing have 0 other diagonals, 0 crossings, 0 overlaps. CopyYear has the allowed For Each loop-back. Bootstrap (arguments only) is unchanged: its 2 existing merge diagonals predate this story.
- **Merge:** `omlMerge` returned `merged: true`; live module re-checked, 0 validation messages. Plan check round 1 approved (P01 accepted despite Rejected), see `reviews/ClaudeSheet_CS/US8.md`.

### ClaudeSheet

- **Dependencies:** the first check after "Done" found the 4 Holiday actions missing (the ticks weren't applied yet); the refresh had already brought Holiday.IsFixedDate. After the second "Done", the probe found all 4 actions, none suffixed.
- Implemented in **1 `applyModelApiCode` save** (one dry run before it), 0 validation messages. The dry run showed that a local's default must be a literal, so **SelectedYear** is set to `Year(CurrDate())` in the screen's **OnInitialize**.
- **P03 · Admin › Holidays** (roles: SheetAdmin only; styles from the theme's admin section, TT7; no screen CSS):
  - Locals: SelectedYear, ShowInactive, EditHoliday (Holiday), ShowHolidayPopup, PopupError, PageError.
  - Data action **GetHolidays**: Holiday_GetList(SelectedYear, ShowInactive) + ErrorMessage. **OnAfterFetch_GetHolidays** shows the error.
  - Toolbar: year selector (PreviousYearButton / YearLabel / NextYearButton → **OnClick_PreviousYearButton** / **OnClick_NextYearButton**), ShowInactiveCheckbox (→ **OnChange_ShowInactiveCheckbox**), AddHolidayButton, and CopyYearButton "Copy fixed holidays to <year + 1>".
  - **OnClick_CopyYearButton** calls Holiday_CopyYear(SelectedYear, SelectedYear + 1) and reports "<n> holiday(s) copied to <year>.", or "Nothing to copy: <year> already has these holidays."
  - Table (`admin-table--5`): Date (dd/MM/yyyy), Weekday (dddd), Name (+ "(movable)" when not fixed), Status, and actions Edit / Activate-Deactivate (**OnClick_EditHolidayLink**, **OnClick_ToggleHolidayLink** → Holiday_SetActive). Empty state "No holidays for <year> yet."
  - **HolidayPopup** (built-in Popup): HolidayDateInput and HolidayNameInput (mandatory, max 100) and FixedDateCheckbox "Same date every year" with a hint.
    - Save → **OnClick_SaveHolidayButton** (Holiday_CreateOrUpdate). It then closes the popup and switches SelectedYear to the holiday's year.
    - Cancel → **OnClick_CancelPopupButton**.
  - Errors: screen actions catch **AllExceptions** and show its message (PopupError in the popup, PageError above the list), per the conventions.
- **P04 · Menu:** new **HolidaysLink** "Holidays" → Admin › Holidays, fourth in PageLinks (role="menuitem"), Visible = `GetMenuAccess.IsSheetAdmin`. The screen passes ActiveItem = 3.
- **Flow layout gate:** 12 flows (11 screen actions and the data action): 0 diagonals, 0 crossings, 0 overlaps.
- **Merge:** `omlMerge` returned `merged: true`; live module re-checked, 0 validation messages. Plan check round 1 approved (`reviews/ClaudeSheet/US8.md`).

## Trail

- Changes: `changes/US8-ClaudeSheet_CS.md`, `changes/US8-ClaudeSheet.md`
- Review: `reviews/ClaudeSheet_CS/US8.md` (plan check + review round 1 approved, P01 accepted despite Rejected), `reviews/ClaudeSheet/US8.md` (plan check + review round 1 approved)
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 16:00, ClaudeSheet 16:13; the MCP attempts were blocked)
- Snapshots: `snapshots/ClaudeSheet_CS/` and `snapshots/ClaudeSheet/` refreshed 2026-10-19 (targeted)
