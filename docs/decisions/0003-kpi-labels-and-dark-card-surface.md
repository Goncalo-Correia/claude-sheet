# 0003. KPI labels come from CalendarDayType, cards use the dark surface

- **Date:** 2026-10-15
- **Status:** Accepted
- **Story:** US3

## Context

The DayStatusKPIs block (US3) shows "<count> <label>" per day type, using `Label` when the count is 1 and `PluralLabel` otherwise. The seed data from TT0 had `PluralLabel` = "Confirmed days" / "Pending days", while the story wants "7 Confirmed" / "0 Pending" (those two labels don't change).

The story also specified `--sheet-card` as the card background with white text. `--sheet-card` is `#ffffff`, so the text would be invisible. US2 had already hit the same conflict with the calendar and used `--sheet-surface-dark`.

## Decision

- The `PluralLabel` of the CalendarDayType records Confirmed and Pending is "Confirmed" and "Pending". The block keeps a single rule (Label when Count = 1, PluralLabel otherwise) with no special cases per type.
- The MySheet widgets (TotalHoursMonth, HoursCalendar, DayStatusKPIs) use `--sheet-surface-dark` as their card background with white text. `--sheet-card` stays white and backs the US-000 `.day-pending` / `.day-future` classes only.
- The status colours inside the widgets are the calendar's (blue Day off, purple Holiday, filled orange Pending). Each widget redefines `--sheet-dayoff` / `--sheet-holiday` in its own scope (`.sheet-cal`, `.sheet-kpis`), so the theme's TT0 values elsewhere are unchanged.

## Consequences

- Wording for the KPI cards is data: change it in the CalendarDayType records, not in the block.
- Any new MySheet widget that shows status colours must use the same scoped overrides (or the overrides should be moved to a shared scope) so its colours match the calendar.
- Stories that name `--sheet-card` for a widget background with light text should be read as `--sheet-surface-dark`.
