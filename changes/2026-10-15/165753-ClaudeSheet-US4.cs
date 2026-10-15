// Published: 2026-10-15T17:09:41.123041+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T16:57:53.136808+00:00
// Story:    US4
// Outcome:  saved with 1 validation error(s)
// Imports:  -
// Error:    /ClaudeSheet - Invalid Icon Library (expected sidecar error on theme stylesheet edits; Service Studio validation clean)
eSpace => {
    var theme = eSpace.MobileThemes.Named("ClaudeSheet");
    if (theme.StyleSheet.Contains(".sheet-week")) throw new Exception("sheet-week CSS already present");
    var css =
        "\n.sheet-week-start { }\n\n" +
        "/* MySheet - WeeklyProjectHours block (US4) */\n" +
        ".sheet-week { width: 100%; }\n" +
        ".sheet-week--loading .sheet-week__body { opacity: .4; }\n" +
        ".sheet-week__table { display: flex; flex-direction: column; gap: 8px; }\n" +
        ".sheet-week__body, .sheet-week__rows { display: flex; flex-direction: column; gap: 8px; }\n" +
        ".sheet-week__row { display: grid; grid-template-columns: minmax(200px, 2fr) repeat(7, minmax(64px, 1fr)) minmax(110px, 1fr); align-items: center; column-gap: 8px; }\n" +
        ".sheet-week__contents { display: contents; }\n" +
        ".sheet-week__head { padding: 0 16px; color: var(--color-neutral-8); }\n" +
        ".sheet-week__card { padding: 12px 16px; border-radius: 12px; background-color: var(--sheet-surface-dark); color: #FFFFFF; }\n" +
        ".sheet-week__label { display: flex; align-items: center; justify-content: space-between; gap: 8px; min-width: 0; }\n" +
        ".sheet-week__label--project { flex-direction: column; align-items: flex-start; justify-content: center; gap: 2px; }\n" +
        ".sheet-week__title { font-size: 16px; font-weight: 700; }\n" +
        ".sheet-week__client { font-weight: 700; }\n" +
        ".sheet-week__project { font-size: 13px; color: var(--sheet-muted); }\n" +
        ".sheet-week__day { display: flex; flex-direction: column; align-items: center; font-size: 13px; line-height: 1.3; }\n" +
        ".sheet-week__day-name { font-weight: 600; }\n" +
        ".sheet-week__day.is-weekend { color: var(--sheet-muted); }\n" +
        ".sheet-week__day.is-today { color: var(--sheet-accent); }\n" +
        ".sheet-week__total-head { display: flex; align-items: center; gap: 8px; font-size: 13px; font-weight: 600; }\n" +
        ".sheet-week__arrow { display: inline-flex; align-items: center; justify-content: center; flex: 0 0 auto; width: 32px; height: 32px; padding: 0; border: 1px solid var(--color-neutral-5); border-radius: 6px; background: transparent; color: inherit; cursor: pointer; }\n" +
        ".sheet-week__arrow:hover { background-color: var(--color-neutral-3); }\n" +
        ".sheet-week__arrow:focus-visible { outline: 2px solid var(--sheet-accent); outline-offset: 2px; }\n" +
        ".sheet-week__cell { text-align: center; font-variant-numeric: tabular-nums; }\n" +
        ".sheet-week__cell.is-weekend { color: var(--sheet-muted); }\n" +
        ".sheet-week__total { font-weight: 700; }\n" +
        ".sheet-week__totals { font-weight: 700; }\n" +
        ".sheet-week__empty { padding: 24px; border-radius: 12px; background-color: var(--sheet-surface-dark); color: var(--sheet-muted); text-align: center; }\n" +
        "@media (max-width: 1023px) {\n" +
        "    .sheet-week__table { min-width: 960px; }\n" +
        "    .sheet-week__label { position: sticky; left: 0; z-index: 1; }\n" +
        "    .sheet-week__card .sheet-week__label { background-color: var(--sheet-surface-dark); }\n" +
        "    .sheet-week__head .sheet-week__label { background-color: var(--sheet-bg); }\n" +
        "}\n";
    theme.StyleSheet = theme.StyleSheet + css;
}
