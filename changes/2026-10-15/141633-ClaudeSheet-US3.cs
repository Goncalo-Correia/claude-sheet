// Published: 2026-10-15T14:34:48.815335+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T14:16:33.849451+00:00
// Story:    US3
// Outcome:  saved with 1 validation error(s)
// Imports:  -
// Error:    /ClaudeSheet - Invalid Icon Library (expected sidecar error on theme stylesheet edits; Service Studio validation clean)
eSpace => {
    var theme = eSpace.MobileThemes.Named("ClaudeSheet");
    if (theme.StyleSheet.Contains(".sheet-kpis")) throw new Exception("sheet-kpis CSS already present");
    var css =
        "\n.sheet-kpis-start { }\n\n" +
        "/* MySheet - DayStatusKPIs block (US3). Status colours match .sheet-cal */\n" +
        ".sheet-kpis { --sheet-dayoff: #2f7fd8; --sheet-holiday: #8a4fd1; width: 100%; }\n" +
        ".sheet-kpis__list { display: flex; flex-direction: column; gap: 16px; }\n" +
        ".sheet-kpis__card { display: flex; align-items: center; gap: 12px; min-width: 0; padding: 16px; border-radius: 12px; background-color: var(--sheet-surface-dark); color: #FFFFFF; font-size: 16px; font-weight: 600; line-height: 1.2; }\n" +
        ".sheet-kpis__square { flex: 0 0 24px; width: 24px; height: 24px; border-radius: 6px; }\n" +
        ".sheet-kpis__text { min-width: 0; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }\n" +
        ".sheet-kpis .day-confirmed { background-color: var(--sheet-confirmed); border: 0; }\n" +
        ".sheet-kpis .day-off { background-color: var(--sheet-dayoff); border: 0; }\n" +
        ".sheet-kpis .day-holiday { background-color: var(--sheet-holiday); border: 0; }\n" +
        ".sheet-kpis .day-pending { background-color: var(--sheet-pending); border: 0; }\n" +
        ".sheet-kpis__card.is-skeleton .sheet-kpis__square, .sheet-kpis__card.is-skeleton .sheet-kpis__text { background: linear-gradient(90deg, var(--sheet-track) 25%, #3A3A3A 50%, var(--sheet-track) 75%); background-size: 200% 100%; animation: sheet-shimmer 1.2s ease-in-out infinite; }\n" +
        ".sheet-kpis__card.is-skeleton .sheet-kpis__text { width: 96px; height: 16px; border-radius: 4px; }\n" +
        ".tablet .sheet-kpis__list, .phone .sheet-kpis__list { display: grid; grid-template-columns: repeat(2, minmax(0, 1fr)); }\n" +
        "@media (max-width: 1023px) { .sheet-kpis__list { display: grid; grid-template-columns: repeat(2, minmax(0, 1fr)); } }\n";
    theme.StyleSheet = theme.StyleSheet + css;
}
