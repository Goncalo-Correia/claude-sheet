// Published: 2026-10-19T15:32:29+00:00 (manual 1-Click Publish; logged by Claude from the session transcript)
// Module:   ClaudeSheet
// When:     2026-10-19T14:23:35.336Z
// Story:    US7
// Outcome:  saved
// Imports:  -
eSpace => {
  var cs = eSpace.References.Named("ClaudeSheet_CS");
  var adminFlow = eSpace.MobileFlows.Named("Admin");
  var common = eSpace.MobileFlows.Named("Common");
  var layoutBlk = eSpace.MobileFlows.Named("Layouts").Nodes.OfType<IMobileBlock>().Named("LayoutTopMenu");
  var menuBlk = common.Nodes.OfType<IMobileBlock>().Named("Menu");
  var projectEnt = cs.Entities.Single(e => e.Name == "Project");
  var paEnt = cs.Entities.Single(e => e.Name == "ProjectAssignment");
  var sProj = cs.Structures.Single(x => x.Name == "ProjectSummary");
  var sAsg = cs.Structures.Single(x => x.Name == "ProjectAssignmentSummary");
  var sUser = cs.Structures.Single(x => x.Name == "UserOption");
  var sClient = cs.Structures.Single(x => x.Name == "ClientSummary");
  var aClients = cs.ServerActions.Single(a => a.Name == "Client_GetList");
  Func<string, OutSystems.Model.Logic.IServerActionSignature> act = n => cs.ServerActions.Single(a => a.Name == n);
  var aPList = act("Project_GetList"); var aPGet = act("Project_GetById"); var aPSave = act("Project_CreateOrUpdate"); var aPAct = act("Project_SetActive");
  var aAList = act("ProjectAssignment_GetList"); var aASave = act("ProjectAssignment_CreateOrUpdate"); var aAEnd = act("ProjectAssignment_End"); var aADel = act("ProjectAssignment_Delete");
  var aUsers = act("User_GetActive");
  Func<OutSystems.Model.Logic.IServerActionSignature, string, OutSystems.Model.IInputParameterSignature> inp = (a, n) => a.InputParameters.Single(p => p.Name == n);

  Action<OutSystems.Model.Logic.Nodes.IActionNode, int, int> at = (n, c, r) => { n.HorizontalPosition = 3200 + c * 1828; n.VerticalPosition = 800 + r * 1371; };
  Func<string, string> q = t => "\"" + t + "\"";
  Func<string, IMobileScreen> newScreen = name => {
    var s = adminFlow.CreateScreen(name);
    s.Widgets.ToList().ForEach(w => w.Delete());
    foreach (var r in s.Roles.ToList()) if (r.Name != "SheetAdmin") s.Roles.Remove(r);
    return s;
  };
  Func<IMobileScreen, string, OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget> layoutOf = (s, title) => {
    var lay = s.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    lay.SourceBlock = layoutBlk;
    var mi = lay.PlaceholdersContent.First(p => p.Placeholder.Name == "Header").Widgets.OfType<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>().First();
    mi.SetArgumentValue(menuBlk.InputParameters.Named("ActiveItem"), "2");
    lay.PlaceholdersContent.First(p => p.Placeholder.Name == "Title").CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = title;
    return lay;
  };
  Func<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget, ServiceStudio.Plugin.NRWidgets.IContainer> mainOf = lay => {
    var c = lay.PlaceholdersContent.First(p => p.Placeholder.Name == "MainContent").CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    c.SetStyle(q("admin-page"));
    return c;
  };
  Func<ServiceStudio.Plugin.NRWidgets.IContainer, string, string, ServiceStudio.Plugin.NRWidgets.IContainer> box = (parent, name, css) => {
    var c = name == null ? parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>() : parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(name);
    c.SetStyle(q(css)); return c;
  };
  Action<ServiceStudio.Plugin.NRWidgets.IContainer, string> text = (parent, t) => parent.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = t;
  Action<ServiceStudio.Plugin.NRWidgets.IContainer, string> expr = (parent, e) => parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue(e);
  Func<OutSystems.Model.UI.Mobile.IScreenAction, OutSystems.Model.Logic.Nodes.IStartNode> start = a => { var st = a.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0); return st; };
  Func<OutSystems.Model.UI.Mobile.IScreenAction, int, int, OutSystems.Model.Logic.Nodes.IEndNode> end = (a, c, r) => { var e = a.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(e, c, r); return e; };
  Action<ServiceStudio.Plugin.NRWidgets.IContainer, string> role = (c, v) => { var p = c.CreateExtendedProperty(); p.Property = "role"; p.SetValue(q(v)); };
  Action<OutSystems.Model.UI.Mobile.IScreenAction, string, int> handler = (a, assignTo, col) => {
    var h = a.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(h, col, 0); h.Exception = eSpace.AllExceptions; h.AbortTransaction = false; h.LogError = false;
    var ha = a.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ha, col, 1); ha.CreateAssignment(assignTo, "AllExceptions.ExceptionMessage"); h.Target = ha;
    var he = a.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(he, col, 2); ha.Target = he;
  };
  Func<IMobileScreen, string, OutSystems.Model.Logic.IServerActionSignature, string[][], string[][], OutSystems.Model.UI.Mobile.IDataAction> dataAct = (scr, name, sa, args, outs) => {
    var d = scr.CreateDataAction(name);
    d.OutputParameters.ToList().ForEach(x => x.Delete());
    foreach (var o in outs) { var op = d.CreateOutputParameter(o[0]); op.DataType = sa.OutputParameters.Single(p => p.Name == o[1]).DataType; }
    var err = d.CreateOutputParameter("ErrorMessage"); err.DataType = eSpace.TextType;
    var st = d.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0);
    var ex = d.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>(sa.Name); at(ex, 0, 1);
    ex.Action = sa;
    foreach (var ar in args) ex.SetArgumentValue(inp(sa, ar[0]), ar[1]);
    st.Target = ex;
    var asg = d.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 2);
    foreach (var o in outs) asg.CreateAssignment(o[0], sa.Name + "." + o[1]);
    ex.Target = asg;
    var en = d.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(en, 0, 3); asg.Target = en;
    var h = d.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(h, 2, 0); h.Exception = eSpace.AllExceptions; h.AbortTransaction = false;
    var ha = d.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ha, 2, 1); ha.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage"); h.Target = ha;
    var he = d.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(he, 2, 2); ha.Target = he;
    return d;
  };
  Func<IMobileScreen, string, string, OutSystems.Model.UI.Mobile.IScreenAction> afterFetchErr = (scr, name, ds) => {
    var a = scr.CreateScreenAction(name); a.Description = "Shows an error message when " + ds + " failed.";
    var st = start(a);
    var iff = a.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(iff, 0, 1); iff.SetCondition(ds + ".ErrorMessage <> \"\""); iff.Label = "Failed?"; st.Target = iff;
    iff.FalseTarget = end(a, 1, 1);
    var m = a.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 2); m.SetMessage(ds + ".ErrorMessage"); m.Type = OutSystems.Model.Enumerations.MessageType.Error; iff.TrueTarget = m;
    m.Target = end(a, 0, 3);
    return a;
  };
  Func<ServiceStudio.Plugin.NRWidgets.IContainer, string, string, ServiceStudio.Plugin.NRWidgets.IButton> button = (parent, name, label) => {
    var b = parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>(name);
    b.Widgets.ToList().ForEach(w => w.Delete());
    b.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = label;
    b.SetEnabled("True");
    return b;
  };
  Func<ServiceStudio.Plugin.NRWidgets.IContainer, string, string, ServiceStudio.Plugin.NRWidgets.ILink> link = (parent, name, label) => {
    var l = parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>(name);
    l.Widgets.ToList().ForEach(w => w.Delete());
    l.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = label;
    return l;
  };
  Action<ServiceStudio.Plugin.NRWidgets.IInput, string> aria = (w, v) => { var p = w.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q(v)); };
  Func<string, string> fmtDate = d => "If(" + d + " = NullDate(), \"-\", FormatDateTime(DateToDateTime(" + d + "), \"dd/MM/yyyy\"))";

  var css = string.Join("\n", new[] {
    "/* Admin screens (US6, US7): toolbar, simple tables, forms, assignment popup. Same rules on every admin screen; move to the theme when more admin screens use them. */",
    ".admin-page { display: flex; flex-direction: column; gap: 16px; }",
    ".admin-toolbar { display: flex; flex-wrap: wrap; align-items: center; gap: 12px; }",
    ".admin-toolbar__filter { flex: 0 1 280px; }",
    ".admin-toolbar__check { display: flex; align-items: center; gap: 6px; }",
    ".admin-table { display: flex; flex-direction: column; border: 1px solid var(--color-neutral-4); border-radius: var(--border-radius-soft); overflow: hidden; }",
    ".admin-table__row { display: grid; grid-template-columns: minmax(0, 2fr) minmax(0, 1fr) minmax(0, 3fr) minmax(0, 1fr) minmax(0, 1fr); gap: 12px; padding: 12px 16px; border-top: 1px solid var(--color-neutral-3); color: inherit; text-decoration: none; align-items: center; }",
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
    ".admin-error { padding: 12px 16px; border-radius: var(--border-radius-soft); background-color: var(--color-error-lightest, #fdecea); color: var(--color-error, #c62828); }"
  });

  var projScr = newScreen("Projects");
  var detScr = newScreen("ProjectDetail");
  projScr.Description = "Admin list of projects (SheetAdmin): filter by client, show inactive, assigned users; opens ProjectDetail.";
  projScr.SetTitle(q("Projects")); projScr.HorizontalPosition = 400; projScr.VerticalPosition = 1400;
  detScr.Description = "Create or edit one project (SheetAdmin): client, code, name, activate/deactivate, and its assigned users (add, edit, end, delete in a popup). ProjectId empty = new project.";
  detScr.SetTitle(q("Project")); detScr.HorizontalPosition = 1600; detScr.VerticalPosition = 1400;
  var dId = detScr.CreateInputParameter("ProjectId"); dId.DataType = projectEnt.IdentifierType; dId.IsMandatory = false;
  projScr.StyleSheet = css; detScr.StyleSheet = css;

  var vClientF = projScr.CreateLocalVariable("FilterClientId"); vClientF.DataType = sClient.Attributes.Single(a => a.Name == "Id").DataType;
  var vInactive = projScr.CreateLocalVariable("ShowInactive"); vInactive.DataType = eSpace.BooleanType; vInactive.SetDefaultValue("False");
  var daC = dataAct(projScr, "GetClients", aClients, new[] { new[] { "IncludeInactive", "True" }, new[] { "Search", "\"\"" } }, new[] { new[] { "Clients", "Clients" } });
  var daP = dataAct(projScr, "GetProjects", aPList, new[] { new[] { "ClientId", "FilterClientId" }, new[] { "IncludeInactive", "ShowInactive" } }, new[] { new[] { "Projects", "Projects" } });
  daP.OnAfterFetch.Destination = afterFetchErr(projScr, "OnAfterFetch_GetProjects", "GetProjects");
  var flt = projScr.CreateScreenAction("OnChange_Filters"); flt.Description = "OnChange of ClientFilterDropdown and ShowInactiveCheckbox: reloads GetProjects.";
  { var st = start(flt); var r = flt.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(r, 0, 1); r.DataSource = daP; st.Target = r; r.Target = end(flt, 0, 2); }

  var lay1 = layoutOf(projScr, "Projects");
  var page1 = mainOf(lay1);
  var tb = box(page1, "Toolbar", "admin-toolbar");
  var cf = tb.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ClientFilterDropdown");
  cf.SetVariable("FilterClientId"); cf.SetList("GetClients.Clients"); cf.SetValues("GetClients.Clients.Current.Id"); cf.SetLabels("GetClients.Clients.Current.Name"); cf.SetEmptyValue(q("All clients")); cf.SetEnabled("True"); cf.SetMandatory("False"); cf.SetStyle(q("dropdown-display admin-toolbar__filter"));
  { var p = cf.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("Filter by client")); }
  cf.OnChange.Destination = flt;
  var chkWrap = box(tb, "ShowInactiveWrap", "admin-toolbar__check");
  var chk = chkWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox");
  chk.SetVariable("ShowInactive"); chk.SetEnabled("True");
  { var p = chk.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("Show inactive projects")); }
  chk.OnChange.Destination = flt;
  text(chkWrap, "Show inactive");
  var newBtn = button(tb, "NewProjectButton", "New project"); newBtn.SetStyle(q("btn btn-primary"));
  newBtn.OnClick.Destination = detScr;
  newBtn.OnClick.Arguments.First(a => a.Parameter.Name == "ProjectId").SetValue("NullIdentifier()");

  var table = box(page1, "ProjectsTable", "admin-table"); role(table, "table");
  var head = box(table, "ProjectsHeader", "admin-table__row admin-table__head"); role(head, "row");
  foreach (var h in new[] { "Client", "Code", "Name", "Assigned users", "Status" }) { var hc = box(head, null, "admin-table__cell"); role(hc, "columnheader"); text(hc, h); }
  var has = table.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProjects");
  has.SetCondition("GetProjects.Projects.Length > 0 or not GetProjects.IsDataFetched");
  var list = has.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectList");
  list.SetSource("GetProjects.Projects"); list.SetStyle(q("admin-table__body"));
  var row = list.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ProjectRow");
  row.Widgets.ToList().ForEach(w => w.Delete());
  row.SetStyle(q("admin-table__row"));
  { var p = row.CreateExtendedProperty(); p.Property = "role"; p.SetValue(q("row")); }
  row.OnClick.Destination = detScr;
  row.OnClick.Arguments.First(a => a.Parameter.Name == "ProjectId").SetValue("GetProjects.Projects.Current.Id");
  var cells = new List<ServiceStudio.Plugin.NRWidgets.IContainer>();
  foreach (var e in new[] { new[] { "admin-table__cell", "GetProjects.Projects.Current.ClientName" }, new[] { "admin-table__cell", "GetProjects.Projects.Current.Code" }, new[] { "admin-table__cell admin-table__name", "GetProjects.Projects.Current.Name" }, new[] { "admin-table__cell", "GetProjects.Projects.Current.AssignedUsers" } }) {
    var c = row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); c.SetStyle(q(e[0])); c.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue(e[1]); cells.Add(c);
  }
  { var c = row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); c.SetStyle("\"admin-table__cell \" + If(GetProjects.Projects.Current.IsActive, \"is-active\", \"is-inactive\")"); c.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("If(GetProjects.Projects.Current.IsActive, \"Active\", \"Inactive\")"); cells.Add(c); }
  foreach (var c in cells) role(c, "cell");
  var emptyC = has.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoProjects"); emptyC.SetStyle(q("admin-table__empty"));
  emptyC.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "No projects found.";

  var vEdit = detScr.CreateLocalVariable("EditProject"); vEdit.DataType = projectEnt; vEdit.Description = "Editable copy of the project, bound to the form (filled by OnAfterFetch_GetProject).";
  var vErr = detScr.CreateLocalVariable("FormError"); vErr.DataType = eSpace.TextType; vErr.Description = "Message of the last failed project save or activate/deactivate (e.g. ProjectValidation), shown above the form.";
  var vAsg = detScr.CreateLocalVariable("EditAssignment"); vAsg.DataType = paEnt; vAsg.Description = "Assignment being added, edited or ended in the popup.";
  var vShow = detScr.CreateLocalVariable("ShowAssignmentPopup"); vShow.DataType = eSpace.BooleanType; vShow.SetDefaultValue("False");
  var vEndMode = detScr.CreateLocalVariable("IsEndMode"); vEndMode.DataType = eSpace.BooleanType; vEndMode.SetDefaultValue("False"); vEndMode.Description = "True when the popup only asks for the end date (End, or a refused Delete).";
  var vPErr = detScr.CreateLocalVariable("PopupError"); vPErr.DataType = eSpace.TextType; vPErr.Description = "Message shown in the popup (e.g. AssignmentValidation from a save, or why a Delete was refused).";
  var vPUser = detScr.CreateLocalVariable("PopupUserName"); vPUser.DataType = eSpace.TextType; vPUser.Description = "Name of the assignment's user, shown in End mode.";

  var dp = detScr.CreateDataAction("GetProject");
  dp.OutputParameters.ToList().ForEach(x => x.Delete());
  { var o = dp.CreateOutputParameter("ProjectRecord"); o.DataType = projectEnt; }
  { var o = dp.CreateOutputParameter("ErrorMessage"); o.DataType = eSpace.TextType; }
  {
    var st = dp.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0);
    var iff = dp.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(iff, 0, 1); iff.SetCondition("ProjectId = NullIdentifier()"); iff.Label = "New project?"; st.Target = iff;
    var nw = dp.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(nw, 1, 1); nw.CreateAssignment("ProjectRecord.IsActive", "True"); iff.TrueTarget = nw;
    var nwE = dp.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(nwE, 1, 2); nw.Target = nwE;
    var ex = dp.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetById"); at(ex, 0, 2); ex.Action = aPGet; ex.SetArgumentValue(inp(aPGet, "ProjectId"), "ProjectId"); iff.FalseTarget = ex;
    var asg = dp.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 3); asg.CreateAssignment("ProjectRecord", "Project_GetById.ProjectRecord"); ex.Target = asg;
    var en = dp.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(en, 0, 4); asg.Target = en;
    var h = dp.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(h, 3, 0); h.Exception = eSpace.AllExceptions; h.AbortTransaction = false;
    var ha = dp.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ha, 3, 1); ha.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage"); h.Target = ha;
    var he = dp.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(he, 3, 2); ha.Target = he;
  }
  var dC = dataAct(detScr, "GetClients", aClients, new[] { new[] { "IncludeInactive", "True" }, new[] { "Search", "\"\"" } }, new[] { new[] { "Clients", "Clients" } });
  var dU = dataAct(detScr, "GetUsers", aUsers, new string[0][], new[] { new[] { "Users", "Users" } });
  var dA = detScr.CreateDataAction("GetAssignments");
  dA.OutputParameters.ToList().ForEach(x => x.Delete());
  { var o = dA.CreateOutputParameter("Assignments"); o.DataType = eSpace.GetOrCreateListType(sAsg); }
  { var o = dA.CreateOutputParameter("ErrorMessage"); o.DataType = eSpace.TextType; }
  {
    var st = dA.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0);
    var iff = dA.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(iff, 0, 1); iff.SetCondition("ProjectId = NullIdentifier()"); iff.Label = "New project?"; st.Target = iff;
    var nwE = dA.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(nwE, 1, 1); iff.TrueTarget = nwE;
    var ex = dA.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_GetList"); at(ex, 0, 2); ex.Action = aAList;
    ex.SetArgumentValue(inp(aAList, "ProjectId"), "ProjectId"); ex.SetArgumentValue(inp(aAList, "UserId"), "NullIdentifier()"); iff.FalseTarget = ex;
    var asg = dA.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 3); asg.CreateAssignment("Assignments", "ProjectAssignment_GetList.Assignments"); ex.Target = asg;
    var en = dA.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(en, 0, 4); asg.Target = en;
    var h = dA.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(h, 3, 0); h.Exception = eSpace.AllExceptions; h.AbortTransaction = false;
    var ha = dA.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ha, 3, 1); ha.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage"); h.Target = ha;
    var he = dA.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(he, 3, 2); ha.Target = he;
  }
  dA.OnAfterFetch.Destination = afterFetchErr(detScr, "OnAfterFetch_GetAssignments", "GetAssignments");

  var dpf = detScr.CreateScreenAction("OnAfterFetch_GetProject"); dpf.Description = "Copies the fetched project into EditProject; shows an error message when GetProject failed.";
  {
    var st = start(dpf);
    var asg = dpf.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1); asg.CreateAssignment("EditProject", "GetProject.ProjectRecord"); st.Target = asg;
    var iff = dpf.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(iff, 0, 2); iff.SetCondition("GetProject.ErrorMessage <> \"\""); iff.Label = "Failed?"; asg.Target = iff;
    iff.FalseTarget = end(dpf, 1, 2);
    var m = dpf.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 3); m.SetMessage("GetProject.ErrorMessage"); m.Type = OutSystems.Model.Enumerations.MessageType.Error; iff.TrueTarget = m;
    m.Target = end(dpf, 0, 4);
  }
  dp.OnAfterFetch.Destination = dpf;

  var sv = detScr.CreateScreenAction("OnClick_SaveButton"); sv.Description = "Saves EditProject through Project_CreateOrUpdate. A new project opens its own ProjectDetail (to add users); an existing one reloads GetProject. ProjectValidation (or any other) error is shown above the form.";
  {
    var st = start(sv);
    var clr = sv.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(clr, 0, 1); clr.CreateAssignment("FormError", "\"\""); st.Target = clr;
    var ex = sv.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_CreateOrUpdate"); at(ex, 0, 2); ex.Action = aPSave; ex.SetArgumentValue(inp(aPSave, "ProjectRecord"), "EditProject"); clr.Target = ex;
    var m = sv.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 3); m.SetMessage(q("Project saved.")); m.Type = OutSystems.Model.Enumerations.MessageType.Success; ex.Target = m;
    var iff = sv.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(iff, 0, 4); iff.SetCondition("ProjectId = NullIdentifier()"); iff.Label = "New project?"; m.Target = iff;
    var dst = sv.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>(); at(dst, 1, 4); dst.Destination = detScr; iff.TrueTarget = dst;
    dst.Arguments.First(a => a.Parameter.Name == "ProjectId").SetValue("Project_CreateOrUpdate.ProjectId");
    var rf = sv.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 5); rf.DataSource = dp; iff.FalseTarget = rf;
    rf.Target = end(sv, 0, 6);
  }
  handler(sv, "FormError", 3);

  var tg = detScr.CreateScreenAction("OnClick_ToggleActiveButton"); tg.Description = "Activates or deactivates the project through Project_SetActive and reloads GetProject.";
  {
    var st = start(tg);
    var clr = tg.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(clr, 0, 1); clr.CreateAssignment("FormError", "\"\""); st.Target = clr;
    var ex = tg.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_SetActive"); at(ex, 0, 2); ex.Action = aPAct;
    ex.SetArgumentValue(inp(aPAct, "ProjectId"), "ProjectId"); ex.SetArgumentValue(inp(aPAct, "IsActive"), "not GetProject.ProjectRecord.IsActive"); clr.Target = ex;
    var rf = tg.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 3); rf.DataSource = dp; ex.Target = rf;
    var m = tg.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 4); m.SetMessage("If(GetProject.ProjectRecord.IsActive, \"Project activated.\", \"Project deactivated.\")"); m.Type = OutSystems.Model.Enumerations.MessageType.Success; rf.Target = m;
    m.Target = end(tg, 0, 5);
  }
  handler(tg, "FormError", 2);

  var opAdd = detScr.CreateScreenAction("OnClick_AddAssignmentButton"); opAdd.Description = "Opens the popup for a new assignment of this project (start today, no end).";
  {
    var st = start(opAdd);
    var asg = opAdd.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1);
    asg.CreateAssignment("EditAssignment.Id", "NullIdentifier()"); asg.CreateAssignment("EditAssignment.ProjectId", "ProjectId"); asg.CreateAssignment("EditAssignment.UserId", "NullIdentifier()");
    asg.CreateAssignment("EditAssignment.StartDate", "CurrDate()"); asg.CreateAssignment("EditAssignment.EndDate", "NullDate()"); asg.CreateAssignment("EditAssignment.IsActive", "True");
    asg.CreateAssignment("PopupUserName", "\"\""); asg.CreateAssignment("PopupError", "\"\""); asg.CreateAssignment("IsEndMode", "False"); asg.CreateAssignment("ShowAssignmentPopup", "True");
    st.Target = asg; asg.Target = end(opAdd, 0, 2);
  }
  Func<string, string, bool, OutSystems.Model.UI.Mobile.IScreenAction> opRow = (name, desc, endMode) => {
    var a = detScr.CreateScreenAction(name); a.Description = desc;
    var rp = a.CreateInputParameter("Row"); rp.DataType = sAsg;
    var st = start(a);
    var asg = a.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1);
    asg.CreateAssignment("EditAssignment.Id", "Row.Id"); asg.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId"); asg.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    asg.CreateAssignment("EditAssignment.StartDate", "Row.StartDate");
    asg.CreateAssignment("EditAssignment.EndDate", endMode ? "If(Row.EndDate = NullDate(), CurrDate(), Row.EndDate)" : "Row.EndDate");
    asg.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    asg.CreateAssignment("PopupUserName", "Row.UserName"); asg.CreateAssignment("PopupError", "\"\""); asg.CreateAssignment("IsEndMode", endMode ? "True" : "False"); asg.CreateAssignment("ShowAssignmentPopup", "True");
    st.Target = asg; asg.Target = end(a, 0, 2);
    return a;
  };
  var opEdit = opRow("OnClick_EditAssignmentLink", "Opens the popup to edit an assignment (user and dates).", false);
  var opEnd = opRow("OnClick_EndAssignmentLink", "Opens the popup in End mode: only the end date (today when the assignment has no end).", true);

  var del = detScr.CreateScreenAction("OnClick_DeleteAssignmentLink"); del.Description = "Deletes an assignment through ProjectAssignment_Delete. When it is refused (the user has time entries), the popup opens in End mode with the reason, offering End instead.";
  { var rp = del.CreateInputParameter("Row"); rp.DataType = sAsg; }
  {
    var st = start(del);
    var ex = del.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_Delete"); at(ex, 0, 1); ex.Action = aADel; ex.SetArgumentValue(inp(aADel, "ProjectAssignmentId"), "Row.Id"); st.Target = ex;
    var rf = del.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 2); rf.DataSource = dA; ex.Target = rf;
    var m = del.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 3); m.SetMessage(q("Assignment deleted.")); m.Type = OutSystems.Model.Enumerations.MessageType.Success; rf.Target = m;
    m.Target = end(del, 0, 4);
    var h = del.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(h, 2, 0); h.Exception = eSpace.AllExceptions; h.AbortTransaction = false; h.LogError = false;
    var ha = del.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ha, 2, 1);
    ha.CreateAssignment("EditAssignment.Id", "Row.Id"); ha.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId"); ha.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    ha.CreateAssignment("EditAssignment.StartDate", "Row.StartDate"); ha.CreateAssignment("EditAssignment.EndDate", "If(Row.EndDate = NullDate(), CurrDate(), Row.EndDate)"); ha.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    ha.CreateAssignment("PopupUserName", "Row.UserName"); ha.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage"); ha.CreateAssignment("IsEndMode", "True"); ha.CreateAssignment("ShowAssignmentPopup", "True");
    h.Target = ha;
    var he = del.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(he, 2, 2); ha.Target = he;
  }

  Func<string, string, OutSystems.Model.Logic.IServerActionSignature, string[][], string, OutSystems.Model.UI.Mobile.IScreenAction> popSave = (name, desc, sa, args, okMsg) => {
    var a = detScr.CreateScreenAction(name); a.Description = desc;
    var st = start(a);
    var clr = a.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(clr, 0, 1); clr.CreateAssignment("PopupError", "\"\""); st.Target = clr;
    var ex = a.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>(sa.Name); at(ex, 0, 2); ex.Action = sa;
    foreach (var ar in args) ex.SetArgumentValue(inp(sa, ar[0]), ar[1]);
    clr.Target = ex;
    var cl = a.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cl, 0, 3); cl.CreateAssignment("ShowAssignmentPopup", "False"); ex.Target = cl;
    var rf = a.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 4); rf.DataSource = dA; cl.Target = rf;
    var m = a.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 5); m.SetMessage(q(okMsg)); m.Type = OutSystems.Model.Enumerations.MessageType.Success; rf.Target = m;
    m.Target = end(a, 0, 6);
    handler(a, "PopupError", 2);
    return a;
  };
  var psA = popSave("OnClick_SaveAssignmentButton", "Saves EditAssignment through ProjectAssignment_CreateOrUpdate, closes the popup and reloads GetAssignments. AssignmentValidation (or any other) error is shown in the popup.", aASave, new[] { new[] { "ProjectAssignmentRecord", "EditAssignment" } }, "Assignment saved.");
  var psE = popSave("OnClick_SaveEndButton", "Ends the assignment on EditAssignment.EndDate through ProjectAssignment_End, closes the popup and reloads GetAssignments. Errors are shown in the popup.", aAEnd, new[] { new[] { "ProjectAssignmentId", "EditAssignment.Id" }, new[] { "EndDate", "EditAssignment.EndDate" } }, "Assignment ended.");
  var pc = detScr.CreateScreenAction("OnClick_CancelPopupButton"); pc.Description = "Closes the assignment popup without saving.";
  { var st = start(pc); var asg = pc.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1); asg.CreateAssignment("ShowAssignmentPopup", "False"); st.Target = asg; asg.Target = end(pc, 0, 2); }

  var lay2 = layoutOf(detScr, "Project");
  var page2 = mainOf(lay2);
  var back = link(page2, "BackLink", "Back to projects"); back.OnClick.Destination = projScr;
  var errIf = page2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasError"); errIf.SetCondition("FormError <> \"\"");
  { var eb = errIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ErrorBox"); eb.SetStyle(q("admin-error")); role(eb, "alert"); expr(eb, "FormError"); }
  var form = box(page2, "ProjectForm", "admin-form");
  text(form, "Client");
  var clDd = form.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ClientDropdown");
  clDd.SetVariable("EditProject.ClientId"); clDd.SetList("GetClients.Clients"); clDd.SetValues("GetClients.Clients.Current.Id"); clDd.SetLabels("If(GetClients.Clients.Current.IsActive, GetClients.Clients.Current.Name, GetClients.Clients.Current.Name + \" (inactive)\")"); clDd.SetEmptyValue(q("Choose a client"));
  clDd.SetMandatory("True"); clDd.SetEnabled("GetProject.IsDataFetched"); clDd.SetStyle(q("dropdown-display"));
  { var p = clDd.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("Client")); }
  text(form, "Code");
  var codeIn = form.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("CodeInput");
  codeIn.SetVariable("EditProject.Code"); codeIn.SetMandatory("True"); codeIn.SetEnabled("GetProject.IsDataFetched"); codeIn.SetStyle(q("form-control"));
  { var p = codeIn.CreateExtendedProperty(); p.Property = "maxlength"; p.SetValue(q("30")); } aria(codeIn, "Project code");
  text(form, "Name");
  var nameIn = form.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("NameInput");
  nameIn.SetVariable("EditProject.Name"); nameIn.SetMandatory("True"); nameIn.SetEnabled("GetProject.IsDataFetched"); nameIn.SetStyle(q("form-control"));
  { var p = nameIn.CreateExtendedProperty(); p.Property = "maxlength"; p.SetValue(q("100")); } aria(nameIn, "Project name");
  var stat = box(form, "StatusLine", "admin-form__status");
  expr(stat, "If(ProjectId = NullIdentifier(), \"New project\", If(GetProject.ProjectRecord.IsActive, \"Status: active\", \"Status: inactive\"))");
  var acts = box(form, "FormActions", "admin-form__actions");
  var saveBtn = button(acts, "SaveButton", "Save"); saveBtn.SetEnabled("GetProject.IsDataFetched"); saveBtn.SetStyle(q("btn btn-primary"));
  saveBtn.OnClick.Destination = sv; saveBtn.OnClick.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
  var togIf = acts.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsExisting"); togIf.SetCondition("ProjectId <> NullIdentifier()");
  var togBtn = togIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ToggleActiveButton");
  togBtn.Widgets.ToList().ForEach(w => w.Delete());
  togBtn.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("If(GetProject.ProjectRecord.IsActive, \"Deactivate\", \"Activate\")");
  togBtn.SetEnabled("GetProject.IsDataFetched"); togBtn.SetStyle(q("btn")); togBtn.OnClick.Destination = tg;

  var secIf = page2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("ShowAssignments"); secIf.SetCondition("ProjectId <> NullIdentifier()");
  var sec = secIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsSection"); sec.SetStyle(q("admin-page"));
  var sh = box(sec, "AssignmentsHead", "admin-section__head");
  { var h2 = sh.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>(); h2.Tag = "h2"; h2.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Assigned users"; }
  var addBtn = button(sh, "AddAssignmentButton", "Add user"); addBtn.SetStyle(q("btn btn-primary")); addBtn.SetEnabled("GetProject.IsDataFetched"); addBtn.OnClick.Destination = opAdd;
  var at2 = box(sec, "AssignmentsTable", "admin-table"); role(at2, "table");
  var ah = box(at2, "AssignmentsHeader", "admin-table__row admin-table__head"); role(ah, "row");
  foreach (var h in new[] { "User", "Start", "End", "Status", "Actions" }) { var hc = box(ah, null, "admin-table__cell"); role(hc, "columnheader"); text(hc, h); }
  var hasA = at2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasAssignments"); hasA.SetCondition("GetAssignments.Assignments.Length > 0 or not GetAssignments.IsDataFetched");
  var al = hasA.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("AssignmentList"); al.SetSource("GetAssignments.Assignments"); al.SetStyle(q("admin-table__body"));
  var ar2 = al.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentRow"); ar2.SetStyle(q("admin-table__row")); role(ar2, "row");
  var acells = new List<ServiceStudio.Plugin.NRWidgets.IContainer>();
  { var c = box(ar2, null, "admin-table__cell admin-table__name"); expr(c, "GetAssignments.Assignments.Current.UserName"); acells.Add(c); }
  { var c = box(ar2, null, "admin-table__cell"); expr(c, fmtDate("GetAssignments.Assignments.Current.StartDate")); acells.Add(c); }
  { var c = box(ar2, null, "admin-table__cell"); expr(c, "If(GetAssignments.Assignments.Current.EndDate = NullDate(), \"No end\", FormatDateTime(DateToDateTime(GetAssignments.Assignments.Current.EndDate), \"dd/MM/yyyy\"))"); acells.Add(c); }
  { var c = ar2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); c.SetStyle("\"admin-table__cell is-\" + ToLower(GetAssignments.Assignments.Current.Status)"); expr(c, "GetAssignments.Assignments.Current.Status"); acells.Add(c); }
  var acCell = box(ar2, "AssignmentActions", "admin-table__cell admin-table__actions"); acells.Add(acCell);
  foreach (var c in acells) role(c, "cell");
  foreach (var e in new[] { new object[] { "EditAssignmentLink", "Edit", opEdit }, new object[] { "EndAssignmentLink", "End", opEnd }, new object[] { "DeleteAssignmentLink", "Delete", del } }) {
    var l = link(acCell, (string)e[0], (string)e[1]);
    l.OnClick.Destination = (OutSystems.Model.UI.Mobile.IScreenAction)e[2];
    l.OnClick.Arguments.First(a => a.Parameter.Name == "Row").SetValue("GetAssignments.Assignments.Current");
  }
  var aEmpty = hasA.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoAssignments"); aEmpty.SetStyle(q("admin-table__empty"));
  aEmpty.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "No users assigned yet.";

  var pop = page2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("AssignmentPopup");
  pop.SetShowPopup("ShowAssignmentPopup");
  var pb = pop.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentPopupBody"); pb.SetStyle(q("admin-popup")); role(pb, "dialog");
  { var h3 = pb.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>(); h3.Tag = "h3"; h3.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("If(IsEndMode, \"End assignment\", If(EditAssignment.Id = NullIdentifier(), \"Add user\", \"Edit assignment\"))"); }
  var pErrIf = pb.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPopupError"); pErrIf.SetCondition("PopupError <> \"\"");
  { var eb = pErrIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupErrorBox"); eb.SetStyle(q("admin-error")); role(eb, "alert"); expr(eb, "PopupError"); }
  var modeIf = pb.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsEndModeIf"); modeIf.SetCondition("IsEndMode");
  var endF = modeIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EndFields"); endF.SetStyle(q("admin-form"));
  expr(endF, "PopupUserName + \", from \" + FormatDateTime(DateToDateTime(EditAssignment.StartDate), \"dd/MM/yyyy\")");
  text(endF, "End date");
  var endIn = endF.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("EndOnlyDateInput");
  endIn.SetVariable("EditAssignment.EndDate"); endIn.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date; endIn.SetMandatory("False"); endIn.SetEnabled("True"); endIn.SetStyle(q("form-control")); aria(endIn, "End date");
  var edF = modeIf.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EditFields"); edF.SetStyle(q("admin-form"));
  text(edF, "User");
  var uDd = edF.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("UserDropdown");
  uDd.SetVariable("EditAssignment.UserId"); uDd.SetList("GetUsers.Users"); uDd.SetValues("GetUsers.Users.Current.UserId"); uDd.SetLabels("GetUsers.Users.Current.Name + \" (\" + GetUsers.Users.Current.Username + \")\""); uDd.SetEmptyValue(q("Choose a user"));
  uDd.SetMandatory("False"); uDd.SetEnabled("True"); uDd.SetStyle(q("dropdown-display"));
  { var p = uDd.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("User")); }
  text(edF, "Start date");
  var sIn = edF.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("StartDateInput");
  sIn.SetVariable("EditAssignment.StartDate"); sIn.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date; sIn.SetMandatory("True"); sIn.SetEnabled("True"); sIn.SetStyle(q("form-control")); aria(sIn, "Start date");
  text(edF, "End date");
  var eIn = edF.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("EndDateInput");
  eIn.SetVariable("EditAssignment.EndDate"); eIn.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date; eIn.SetMandatory("False"); eIn.SetEnabled("True"); eIn.SetStyle(q("form-control")); aria(eIn, "End date");
  { var hint = box(edF, "EndDateHint", "admin-form__hint"); text(hint, "Leave the end date empty when the assignment has no end."); }
  var pAct = box(pb, "PopupActions", "admin-form__actions");
  var bSaveA = button(pAct, "SaveAssignmentButton", "Save"); bSaveA.SetStyle(q("btn btn-primary")); bSaveA.SetVisible("not IsEndMode"); bSaveA.OnClick.Destination = psA;
  var bSaveE = button(pAct, "SaveEndButton", "End assignment"); bSaveE.SetStyle(q("btn btn-primary")); bSaveE.SetVisible("IsEndMode"); bSaveE.OnClick.Destination = psE;
  var bCancel = button(pAct, "CancelPopupButton", "Cancel"); bCancel.SetStyle(q("btn")); bCancel.OnClick.Destination = pc;

  var adv = menuBlk.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>().First();
  var pl = adv.Content.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IContainer>().First(c => c.Name == "PageLinks");
  var ml = pl.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ProjectsLink");
  ml.Widgets.ToList().ForEach(w => w.Delete());
  ml.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Projects";
  ml.OnClick.Destination = projScr;
  ml.SetVisible("GetMenuAccess.IsSheetAdmin");
  { var p = ml.CreateExtendedProperty(); p.Property = "role"; p.SetValue(q("menuitem")); }
}
