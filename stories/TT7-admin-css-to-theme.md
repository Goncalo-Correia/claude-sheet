# TT7: [Tech] Move the admin `.admin-*` CSS into the ClaudeSheet theme

- **Status:** Published
- **Published:** ClaudeSheet 2026-10-19 15:42 (manual 1-Click Publish; the MCP publish was blocked)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-19
- **Source:** US7 review, convention item N02 (accepted 2026-10-19 with this follow-up)
- **Trello:** https://trello.com/c/07cGUJhu (created in Done 2026-10-19)

## Story

`docs/conventions.md` › UI says app-wide CSS goes in the theme, but US6 and US7 copied the admin rules onto each admin screen's stylesheet: Clients and ClientDetail carry one version (3-column table), Projects and ProjectDetail another (5-column table plus filter, actions, section head, hint and popup rules). This task keeps one set of `.admin-*` rules in the ClaudeSheet theme, so the next admin screens (US8, US9, US14) use them instead of copying them again.

## Acceptance criteria

- [x] A01 · The ClaudeSheet theme stylesheet holds every `.admin-*` rule used by the four admin screens, once, with table modifiers `.admin-table--3` and `.admin-table--5` for the two column layouts.
- [x] A02 · The stylesheets of Admin › Clients, ClientDetail, Projects and ProjectDetail are empty.
- [x] A03 · ClientsTable and ClientDetail's ProjectsTable use `admin-table admin-table--3`; Projects' ProjectsTable and ProjectDetail's AssignmentsTable use `admin-table admin-table--5`.
- [x] A04 · The four admin screens look the same as before at runtime, and the non-admin screens (MySheet, Login) are unaffected.
- [x] A05 · Service Studio: 0 errors and 0 warnings (the sidecar's InvalidIconLibrary message after a theme edit is expected and isn't a Service Studio error).

## Implementation plan

### ClaudeSheet

1. P01 · Append an "Admin screens" section to the ClaudeSheet theme stylesheet (starting with a guard rule `.admin-theme-start { }`), with the union of the US6 and US7 rules; the table column template moves to `.admin-table--3 .admin-table__row` / `.admin-table--5 .admin-table__row`.
2. P02 · Add the modifier class to the four admin tables (ClientsTable, ClientDetail › ProjectsTable, Projects › ProjectsTable, ProjectDetail › AssignmentsTable).
3. P03 · Empty the four admin screen stylesheets.
4. P04 · `docs/conventions.md` › UI: admin screens use the theme's `.admin-*` classes; don't copy CSS onto admin screens.

## Implementation notes

- Implemented 2026-10-19 in **1 `applyModelApiCode` save** on ClaudeSheet.
- **P01:** the theme stylesheet grew from 11,630 to 13,876 characters. The new section starts with the guard rule `.admin-theme-start { }`; the read-back shows the join with the preceding `sheet-week` media query intact (no dropped characters). It holds 21 `.admin-*` rules, the union of US6 and US7. The US6-only `.admin-toolbar__search` is kept (Clients uses it). `.admin-table__row` lost its column template, which moved to `.admin-table--3` (3fr 1fr 1fr) and `.admin-table--5` (2fr 1fr 3fr 1fr 1fr, plus `align-items: center`, as in US7), so each screen keeps its exact layout.
- **P02:** ClientsTable and ClientDetail › ProjectsTable are `admin-table admin-table--3`; Projects › ProjectsTable and ProjectDetail › AssignmentsTable are `admin-table admin-table--5`.
- **P03:** the four admin screen stylesheets are empty.
- **P04:** `docs/conventions.md` › UI now says admin screens use the theme's admin section and table modifiers.
- **Validation:** the sidecar reports **InvalidIconLibrary** on the theme. That is expected after a theme stylesheet edit (servicestudio-mcp-oml § 3.1), and IconLibrary was left null. Service Studio's own validation is checked after the merge.
- **Risk to check at runtime:** edits to non-empty *screen* stylesheets haven't always deployed. If a screen keeps its old CSS after publishing, nothing breaks: the old rules match the theme, and `.admin-table__row`'s old column template only agrees with that table's modifier. Check the served CSS or the screens after publishing.
- No action flows were touched, so there is no flow layout gate.
- **Merge:** `omlMerge` returned `merged: true`; live module re-checked; Service Studio reports 0 validation messages (the sidecar InvalidIconLibrary does not appear there). Plan check round 1 approved (`reviews/ClaudeSheet/TT7.md`).

## Trail

- Changes: `changes/TT7-ClaudeSheet.md`
- Review: `reviews/ClaudeSheet/TT7.md` (plan check + review round 1 approved)
- Published: see `PUBLISHES.md` (ClaudeSheet 15:42; the MCP attempt was blocked)
- Snapshots: `snapshots/ClaudeSheet/` refreshed 2026-10-19 (targeted: theme and the four admin screens)
- Docs: `docs/conventions.md` › UI (admin screens use the theme's admin section)
