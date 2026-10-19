// Published: 2026-10-19T15:42:00+00:00 (manual 1-Click Publish; logged by Claude from the session transcript)
// Module:   ClaudeSheet
// When:     2026-10-19T15:37:02.872Z
// Story:    TT7
// Outcome:  saved
// Imports:  -
eSpace => {
  var theme = eSpace.GetAllDescendantsOfType<IMobileTheme>().Single(t => t.Name == "ClaudeSheet");
  var admin = string.Join("\n", new[] {
    "",
    "",
    ".admin-theme-start { }",
    "/* Admin screens (TT7): shared by every screen in the Admin UI flow (Clients, ClientDetail, Projects, ProjectDetail, ...). Tables pick their columns with admin-table--3 or admin-table--5. */",
    ".admin-page { display: flex; flex-direction: column; gap: 16px; }",
    ".admin-toolbar { display: flex; flex-wrap: wrap; align-items: center; gap: 12px; }",
    ".admin-toolbar__search { flex: 1 1 240px; max-width: 360px; }",
    ".admin-toolbar__filter { flex: 0 1 280px; }",
    ".admin-toolbar__check { display: flex; align-items: center; gap: 6px; }",
    ".admin-table { display: flex; flex-direction: column; border: 1px solid var(--color-neutral-4); border-radius: var(--border-radius-soft); overflow: hidden; }",
    ".admin-table__row { display: grid; gap: 12px; padding: 12px 16px; border-top: 1px solid var(--color-neutral-3); color: inherit; text-decoration: none; }",
    ".admin-table--3 .admin-table__row { grid-template-columns: minmax(0, 3fr) minmax(0, 1fr) minmax(0, 1fr); }",
    ".admin-table--5 .admin-table__row { grid-template-columns: minmax(0, 2fr) minmax(0, 1fr) minmax(0, 3fr) minmax(0, 1fr) minmax(0, 1fr); align-items: center; }",
    ".admin-table__head { border-top: 0; background-color: var(--color-neutral-2); font-weight: 600; }",
    "a.admin-table__row:hover { background-color: var(--color-neutral-1); }",
    ".admin-table__cell { min-width: 0; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }",
    ".admin-table__cell.is-inactive, .admin-table__cell.is-ended { color: var(--color-neutral-7); }",
    ".admin-table__actions { display: flex; flex-wrap: wrap; gap: 12px; }",
    ".admin-table__empty { padding: 16px; color: var(--color-neutral-7); }",
    ".admin-section__head { display: flex; flex-wrap: wrap; align-items: center; justify-content: space-between; gap: 12px; }",
    ".admin-form { display: flex; flex-direction: column; gap: 12px; max-width: 480px; }",
    ".admin-form__actions { display: flex; flex-wrap: wrap; gap: 8px; }",
    ".admin-form__hint { font-size: var(--font-size-xs); color: var(--color-neutral-7); }",
    ".admin-popup { display: flex; flex-direction: column; gap: 12px; min-width: min(420px, 90vw); }",
    ".admin-error { padding: 12px 16px; border-radius: var(--border-radius-soft); background-color: var(--color-error-lightest, #fdecea); color: var(--color-error, #c62828); }",
    ""
  });
  var cur = theme.StyleSheet ?? "";
  if (cur.Contains(".admin-theme-start")) throw new Exception("admin section already in the theme");
  theme.StyleSheet = cur.TrimEnd() + admin;

  var flow = eSpace.MobileFlows.Named("Admin");
  Action<string, string, string> mod = (screen, container, cls) => {
    var sc = flow.Nodes.OfType<IMobileScreen>().Named(screen);
    var c = sc.GetAllDescendantsOfType<ServiceStudio.Plugin.NRWidgets.IContainer>().Single(x => x.Name == container);
    c.SetStyle("\"admin-table " + cls + "\"");
  };
  mod("Clients", "ClientsTable", "admin-table--3");
  mod("ClientDetail", "ProjectsTable", "admin-table--3");
  mod("Projects", "ProjectsTable", "admin-table--5");
  mod("ProjectDetail", "AssignmentsTable", "admin-table--5");
  foreach (var sc in flow.Nodes.OfType<IMobileScreen>().Where(z => new[] { "Clients", "ClientDetail", "Projects", "ProjectDetail" }.Contains(z.Name))) sc.StyleSheet = "";
}
