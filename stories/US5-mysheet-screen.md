# US5: [Screen] MySheet: compose the timesheet screen

- **Status:** Published
- **Published:** 2026-10-16 11:38 (manual 1-Click Publish from Service Studio; the MCP publish was blocked)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-16
- **Depends on:** US1, US2, US3, US4
- **Trello:** https://trello.com/c/Ihrt0hEQ

## Story

### User story

**As a** consultant
**I want** a single "My Sheet" screen that brings together my monthly total, calendar, status KPIs and weekly project hours, all kept in sync
**So that** I can fill in and review my timesheet in one place.

### Context

The screen implements the full mockup by composing the blocks from US1–US4.

### Layout (desktop)

- Title "My Sheet" (H1) and subtitle "What did you work on today?"
- Row 1, 3 columns: **TotalHoursMonth** (left, ~25%) | **HoursCalendar** (centre, ~45%) | **DayStatusKPIs** (right, ~25%), vertically centred
- Row 2, full width: **WeeklyProjectHours**
- Black background (--sheet-bg), white text, dark theme
- Tablet: KPIs move below the calendar; phone: everything stacked (Total → Calendar → KPIs → Weekly)

### Behaviour / synchronisation

- Local variables: SelectedYear, SelectedMonth (default: current month), WeekReferenceDate (default: today), RefreshKey (Integer, 0)
- Calendar **OnMonthChanged** → update SelectedYear/SelectedMonth (Total, Calendar and KPIs reload); WeekReferenceDate = today if it's the current month, otherwise the 1st of the selected month
- Calendar **OnDaySaved** → RefreshKey + 1 (all 4 blocks reload)
- Calendar **OnDaySelected** → WeekReferenceDate = selected date (the weekly table jumps to that week)
- Weekly **OnWeekChanged** → just stores the new week (does not change the month)

### Security & navigation

- Screen in MainFlow, accessible to role **SheetUser** only (non-authorised users → InvalidPermissions; anonymous → Login)
- Set as the module's **default screen** and add a "My Sheet" entry to the Menu
- Fixes the 2 current "Missing roles" validation warnings

### Dependencies

US1, US2, US3, US4

## Acceptance criteria

- [ ] A01 · AC5.1 Given a logged-in user with role SheetUser, When they open the app, Then MySheet is the default screen and shows "My Sheet" / "What did you work on today?"
- [ ] A02 · AC5.2 Given an anonymous visitor, When they open MySheet, Then they are redirected to Login
- [ ] A03 · AC5.3 Given a logged-in user without SheetUser, When they open MySheet, Then they are redirected to InvalidPermissions
- [ ] A04 · AC5.4 Given today is 14/10/2026, When the screen opens, Then the total, calendar and KPIs show October 2026 and the weekly table shows 12/10–18/10
- [ ] A05 · AC5.5 Given I click the calendar's right arrow, Then the total ("Total November"), calendar and KPIs all show November 2026 and the weekly table moves to the week of 01/11/2026
- [ ] A06 · AC5.6 Given I go back to October, Then the weekly table returns to the week containing today
- [ ] A07 · AC5.7 Given I confirm a pending day with 8:00 on ICNF/RUBUS, Then without a page reload the total increases by 8h00, the KPIs show Confirmed +1 / Pending -1, the day turns green and the weekly table shows 8:00 in the right cell
- [ ] A08 · AC5.8 Given I click 28/10 in the calendar, Then the weekly table shows the week 26/10–01/11
- [ ] A09 · AC5.9 Given I navigate weeks in the weekly table, Then the calendar month does not change
- [ ] A10 · AC5.10 Given "Fill all pending days" fills 3 days, Then all blocks reflect the new data immediately
- [ ] A11 · AC5.11 Given a desktop viewport (>= 1280px), Then the layout matches the mockup: total left, calendar centre, KPIs right, weekly table below
- [ ] A12 · AC5.12 Given a phone viewport, Then all blocks are stacked in the order Total, Calendar, KPIs, Weekly, with no horizontal page scroll
- [ ] A13 · AC5.13 Given Service Studio, When validating the module, Then there are 0 errors and 0 warnings
- [ ] A14 · AC5.14 Given the menu, Then a "My Sheet" entry exists and is highlighted on this screen

## Implementation plan

### ClaudeSheet

1. P01 · Create screen MySheet in MainFlow using LayoutTopMenu (or LayoutBlank if no menu is wanted)
2. P02 · Set Roles = SheetUser on MySheet; review Login and InvalidPermissions access settings so there are no "Missing roles" warnings
3. P03 · Set MySheet as the module's default screen and add a "My Sheet" menu item to the Menu block
4. P04 · Add local variables SelectedYear, SelectedMonth (initialised in OnInitialize from CurrDate()), WeekReferenceDate (CurrDate()) and RefreshKey (0)
5. P05 · Add title "My Sheet" (H1) and subtitle "What did you work on today?" (H4, bold)
6. P06 · Add a 3-column responsive row (custom CSS grid 1fr 1.8fr 1fr with align-items center) and place TotalHoursMonth, HoursCalendar and DayStatusKPIs
7. P07 · Bind Year/Month/RefreshKey inputs of the 3 blocks to SelectedYear, SelectedMonth and RefreshKey
8. P08 · Add WeeklyProjectHours below in a full-width row and bind ReferenceDate = WeekReferenceDate and RefreshKey
9. P09 · Handle HoursCalendar.OnMonthChanged: assign SelectedYear, SelectedMonth and WeekReferenceDate (today if current month, else 1st day of the selected month)
10. P10 · Handle HoursCalendar.OnDaySaved: RefreshKey = RefreshKey + 1
11. P11 · Handle HoursCalendar.OnDaySelected: WeekReferenceDate = selected date
12. P12 · Handle WeeklyProjectHours.OnWeekChanged (no month change; optional: keep it in a local variable)
13. P13 · Apply the screen-level dark theme (body background --sheet-bg, white text, spacing 32px between rows)
14. P14 · Responsive: tablet = Total + Calendar on row 1 and KPIs as a 2x2 row below; phone = single column in the order Total, Calendar, KPIs, Weekly
15. P15 · Verify via Service Studio that there are no errors or warnings, then publish
16. P16 · Run the end-to-end test scenario with a SheetUser test user and a user without the role
17. P17 · Rename the ClaudeSheet_CS role reference `SheetUser2` to `SheetUser` in ClaudeSheet (TT4 leftover; agreed 2026-10-16)
18. P18 · Clear the other ClaudeSheet warnings so A13 can pass (agreed 2026-10-16): use RefreshKey in TotalHoursMonth, HoursCalendar, DayStatusKPIs and WeeklyProjectHours; delete the unused client action Date_GetWeekStart; give the HoursCalendar CalendarDays list a single direct child

Decisions agreed before building (2026-10-16): layout LayoutTopMenu; Login and InvalidPermissions accessible by Everyone; handlers named `OnInitialize`, `OnMonthChanged_HoursCalendar`, `OnDaySaved_HoursCalendar`, `OnDaySelected_HoursCalendar`, `OnWeekChanged_WeeklyProjectHours` (stores the week in WeekReferenceDate); black background on MySheet only (`--sheet-bg` overridden to #000 in the screen's scope; the theme token stays #f4f5f7); CSS in the MySheet screen stylesheet. P15 (publish) and P16 (end-to-end test) are done after the review, not in the implementation.

## Implementation notes

### ClaudeSheet

- Implemented 2026-10-16 in **3 `applyModelApiCode` saves**, merged with `omlMerge` (`merged: true`). Calls went through the Service Studio MCP tools (no raw HTTP).
  1. P17 + P02: role reference renamed and Login / InvalidPermissions set to Everyone;
  2. the MySheet screen (P01, P03–P14), the Menu link and the default screen;
  3. P18: the warning cleanup in the four blocks.
- **Role (P02, P17):** the reference element `ClaudeSheet_CS › SheetUser2` was renamed to `SheetUser`; the CS role itself is unchanged. MySheet has only SheetUser (the default Registered role was removed), so anonymous users go to Login and authenticated users without the role go to InvalidPermissions. Login and InvalidPermissions got the Anonymous role, which Service Studio stores as Anonymous + Registered + SheetUser ("Everyone").
- **Default screen and menu (P03):** module Default Screen = MySheet. Common › Menu › PageLinks got the link `MySheetLink` ("My Sheet", `role="menuitem"`) → MySheet. The Menu instance in MySheet's header has `ActiveItem = 0` so the entry is highlighted.
- **Screen (P01, P04–P08):**
  - `LayoutTopMenu` with ExtendedClass `sheet-layout`.
  - MainContent › `SheetPage` (`sheet-page`) › `PageHeader` with an h1 "My Sheet" and an h4 "What did you work on today?" (HTML Element widgets, so the headings are semantic).
  - Then `MonthRow` (`sheet-page__top`) holding `TotalColumn`, `CalendarColumn` and `KPIsColumn`, then `WeekRow` (`sheet-page__week`).
  - Local variable defaults must be literals, so only RefreshKey has a default (0). SelectedYear, SelectedMonth and WeekReferenceDate are set in OnInitialize.
- **Handlers (P09–P12):** each is Start → Assign → End.
  - `OnMonthChanged_HoursCalendar(Year, Month)`: WeekReferenceDate = `If(Year = Year(CurrDate()) and Month = Month(CurrDate()), CurrDate(), NewDate(Year, Month, 1))`.
  - `OnDaySaved_HoursCalendar` has no Date input, so nothing is left unused.
  - `OnWeekChanged_WeeklyProjectHours` stores the new week start in WeekReferenceDate (agreed before building). Without this, after the user changes weeks, clicking the day that was already selected would not change the variable, and the weekly table would not move.
- **CSS (P13, P14):** in the MySheet screen stylesheet. It was empty before, so the first write publishes (see `servicestudio-mcp-oml` § 8).
  - Every page-level rule is scoped with `:has(.sheet-page)`, because Reactive screen CSS can stay loaded after navigating away.
  - `--sheet-bg` is overridden to `#000000` only while MySheet is shown; the theme token stays `#f4f5f7`.
  - The header and menu links are dark too, with the active link in `--sheet-accent`.
  - Tablet (≤1024px): Total and Calendar share row 1 and the KPIs span the next row. The 2x2 card layout comes from the block's existing `.sheet-kpis__list` grid.
  - Phone (≤767px): a single column in DOM order (Total, Calendar, KPIs, Weekly).
- **Warnings (P18):**
  - Each block's root container (TotalHours, Calendar, KPICards, WeekGrid) has `data-refresh-key = RefreshKey`. That clears "never used" without changing logic: OnParametersChanged already reloads on any input change.
  - `Date_GetWeekStart` (client action) was deleted; the CS server action of the same name is unchanged.
  - HoursCalendar › CalendarDays now has one direct child, `CalendarCell` (`sheet-cal__cell`). The Model API can't move a widget to a new parent, so `IsBlankCell`, the blank container and the `DayCell` button were rebuilt inside it. Their style, condition, Enabled, OnClick (`OnClick_DayCell`, Day = `GetCalendar.Cells.Current`), title / aria-label and inner day number / dot were copied from the live widgets. The theme rule `.sheet-cal__grid > *` (min-width 0) now applies to the wrapper; verify the calendar looks unchanged.
  - Result: 0 validation messages in ClaudeSheet.
- **Flow layout gate:** the 5 MySheet actions are vertical (Start 3200,800 → Assign 3200,2171 → End 3200,3542): 0 diagonals, 0 crossings, 0 overlaps. No other flows were touched.
- **P15 / P16:** validation done (0 errors, 0 warnings). Published 2026-10-16 11:38 by the user from Service Studio after the review. **P16, the end-to-end test with a SheetUser user and a user without the role, is still to do** (A01–A12 and A14 were approved as "verify at runtime").

## Trail

- Changes: `changes/US5-ClaudeSheet.md`: save 1 (role reference + Everyone on Login / InvalidPermissions), 2 (MySheet screen, menu link, default screen), 3 (warning cleanup in the blocks). Written by Claude from the applied code (manual publish, so the hook logged nothing).
- Review: `reviews/ClaudeSheet/US5.md` (plan check round 1: all approved; P15 and P16 accepted despite Rejected tag)
- Review: round 1 approved all 26 rows.
- Published: see `PUBLISHES.md` (2026-10-16 11:38, success (manual); the 11:36 MCP attempt was blocked)
- Snapshots: `snapshots/ClaudeSheet/` refreshed 2026-10-16 (targeted)
