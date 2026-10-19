// Published: 2026-10-19T13:45:11+00:00 (manual 1-Click Publish; logged by Claude, applied by the main session)
// Module:   ClaudeSheet
// When:     2026-10-19T13:28:39+00:00
// Story:    US6
// Outcome:  saved
// Imports:  -
eSpace => {
  var cs = eSpace.References.Named("ClaudeSheet_CS");
  var adminFlow = eSpace.MobileFlows.Named("Admin");
  var common = eSpace.MobileFlows.Named("Common");
  var layoutBlk = eSpace.MobileFlows.Named("Layouts").Nodes.OfType<IMobileBlock>().Named("LayoutTopMenu");
  var menuBlk = common.Nodes.OfType<IMobileBlock>().Named("Menu");
  var clientEnt = cs.Entities.Single(e => e.Name == "Client");
  var projectEnt = cs.Entities.Single(e => e.Name == "Project");
  var summary = cs.Structures.Single(x => x.Name == "ClientSummary");
  var aGetList = cs.ServerActions.Single(a => a.Name == "Client_GetList");
  var aGetById = cs.ServerActions.Single(a => a.Name == "Client_GetById");
  var aSave = cs.ServerActions.Single(a => a.Name == "Client_CreateOrUpdate");
  var aSetActive = cs.ServerActions.Single(a => a.Name == "Client_SetActive");
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
    mi.SetArgumentValue(menuBlk.InputParameters.Named("ActiveItem"), "1");
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

  var clientsScr = newScreen("Clients");
  var detailScr = newScreen("ClientDetail");
  clientsScr.Description = "Admin list of clients (SheetAdmin): search, show inactive, project count; opens ClientDetail.";
  clientsScr.SetTitle(q("Clients")); clientsScr.HorizontalPosition = 400; clientsScr.VerticalPosition = 400;
  detailScr.Description = "Create or edit one client (SheetAdmin): name, activate/deactivate, read-only list of its projects. ClientId empty = new client.";
  detailScr.SetTitle(q("Client")); detailScr.HorizontalPosition = 1600; detailScr.VerticalPosition = 400;
  var dId = detailScr.CreateInputParameter("ClientId"); dId.DataType = clientEnt.IdentifierType; dId.IsMandatory = false;

  var vSearch = clientsScr.CreateLocalVariable("Search"); vSearch.DataType = eSpace.TextType;
  var vInactive = clientsScr.CreateLocalVariable("ShowInactive"); vInactive.DataType = eSpace.BooleanType; vInactive.SetDefaultValue("False");

  var da = clientsScr.CreateDataAction("GetClients");
  da.OutputParameters.ToList().ForEach(x => x.Delete());
  var daOut = da.CreateOutputParameter("Clients"); daOut.DataType = eSpace.GetOrCreateListType(summary);
  var daErr = da.CreateOutputParameter("ErrorMessage"); daErr.DataType = eSpace.TextType;
  var daSt = da.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(daSt, 0, 0);
  var daEx = da.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetList"); at(daEx, 0, 1);
  daEx.Action = aGetList;
  daEx.SetArgumentValue(aGetList.InputParameters.Single(p => p.Name == "IncludeInactive"), "ShowInactive");
  daEx.SetArgumentValue(aGetList.InputParameters.Single(p => p.Name == "Search"), "Search");
  daSt.Target = daEx;
  var daAsg = da.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(daAsg, 0, 2); daAsg.CreateAssignment("Clients", "Client_GetList.Clients"); daEx.Target = daAsg;
  var daEnd = da.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(daEnd, 0, 3); daAsg.Target = daEnd;
  var daH = da.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(daH, 2, 0); daH.Exception = eSpace.AllExceptions; daH.AbortTransaction = false;
  var daHA = da.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(daHA, 2, 1); daHA.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage"); daH.Target = daHA;
  var daHE = da.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(daHE, 2, 2); daHA.Target = daHE;

  var oaf = clientsScr.CreateScreenAction("OnAfterFetch_GetClients"); oaf.Description = "Shows an error message when GetClients failed.";
  var oafSt = start(oaf);
  var oafIf = oaf.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(oafIf, 0, 1); oafIf.SetCondition("GetClients.ErrorMessage <> \"\""); oafIf.Label = "Failed?"; oafSt.Target = oafIf;
  oafIf.FalseTarget = end(oaf, 1, 1);
  var oafMsg = oaf.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(oafMsg, 0, 2); oafMsg.SetMessage("GetClients.ErrorMessage"); oafMsg.Type = OutSystems.Model.Enumerations.MessageType.Error; oafIf.TrueTarget = oafMsg;
  oafMsg.Target = end(oaf, 0, 3);
  da.OnAfterFetch.Destination = oaf;

  var flt = clientsScr.CreateScreenAction("OnChange_Filters"); flt.Description = "OnChange of SearchInput and ShowInactiveCheckbox: reloads GetClients.";
  var fltSt = start(flt);
  var fltR = flt.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(fltR, 0, 1); fltR.DataSource = da; fltSt.Target = fltR;
  fltR.Target = end(flt, 0, 2);

  var lay1 = layoutOf(clientsScr, "Clients");
  var page1 = mainOf(lay1);
  var tb = box(page1, "Toolbar", "admin-toolbar");
  var search = tb.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("SearchInput");
  search.SetVariable("Search"); search.SetMandatory("False"); search.SetEnabled("True"); search.SetStyle(q("form-control admin-toolbar__search"));
  var sph = search.CreateExtendedProperty(); sph.Property = "placeholder"; sph.SetValue(q("Search clients"));
  var sar = search.CreateExtendedProperty(); sar.Property = "aria-label"; sar.SetValue(q("Search clients"));
  search.OnChange.Destination = flt;
  var chkWrap = box(tb, "ShowInactiveWrap", "admin-toolbar__check");
  var chk = chkWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox");
  chk.SetVariable("ShowInactive"); chk.SetEnabled("True");
  var chkA = chk.CreateExtendedProperty(); chkA.Property = "aria-label"; chkA.SetValue(q("Show inactive clients"));
  chk.OnChange.Destination = flt;
  text(chkWrap, "Show inactive");
  var newBtn = tb.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NewClientButton");
  newBtn.Widgets.ToList().ForEach(w => w.Delete());
  newBtn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "New client";
  newBtn.SetEnabled("True"); newBtn.SetStyle(q("btn btn-primary"));
  newBtn.OnClick.Destination = detailScr;
  newBtn.OnClick.Arguments.First(a => a.Parameter.Name == "ClientId").SetValue("NullIdentifier()");

  var table = box(page1, "ClientsTable", "admin-table"); role(table, "table");
  var head = box(table, "ClientsHeader", "admin-table__row admin-table__head"); role(head, "row");
  foreach (var h in new[] { "Name", "Projects", "Status" }) { var hc = box(head, null, "admin-table__cell"); role(hc, "columnheader"); text(hc, h); }
  var has = table.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasClients");
  has.SetCondition("GetClients.Clients.Length > 0 or not GetClients.IsDataFetched");
  var list = has.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ClientList");
  list.SetSource("GetClients.Clients"); list.SetStyle(q("admin-table__body"));
  var row = list.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ClientRow");
  row.Widgets.ToList().ForEach(w => w.Delete());
  row.SetStyle(q("admin-table__row"));
  var rr = row.CreateExtendedProperty(); rr.Property = "role"; rr.SetValue(q("row"));
  row.OnClick.Destination = detailScr;
  row.OnClick.Arguments.First(a => a.Parameter.Name == "ClientId").SetValue("GetClients.Clients.Current.Id");
  var c1 = row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); c1.SetStyle(q("admin-table__cell admin-table__name")); c1.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("GetClients.Clients.Current.Name");
  var c2 = row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); c2.SetStyle(q("admin-table__cell")); c2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("GetClients.Clients.Current.ProjectCount");
  var c3 = row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); c3.SetStyle("\"admin-table__cell \" + If(GetClients.Clients.Current.IsActive, \"is-active\", \"is-inactive\")"); c3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("If(GetClients.Clients.Current.IsActive, \"Active\", \"Inactive\")");
  foreach (var cc in new[] { c1, c2, c3 }) role(cc, "cell");
  var emptyC = has.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoClients"); emptyC.SetStyle(q("admin-table__empty"));
  emptyC.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "No clients found.";

  var css = string.Join("\n", new[] {
    "/* Admin screens (US6): toolbar and a simple table. Same rules on Clients and ClientDetail; move to the theme when more admin screens use them. */",
    ".admin-page { display: flex; flex-direction: column; gap: 16px; }",
    ".admin-toolbar { display: flex; flex-wrap: wrap; align-items: center; gap: 12px; }",
    ".admin-toolbar__search { flex: 1 1 240px; max-width: 360px; }",
    ".admin-toolbar__check { display: flex; align-items: center; gap: 6px; }",
    ".admin-table { display: flex; flex-direction: column; border: 1px solid var(--color-neutral-4); border-radius: var(--border-radius-soft); overflow: hidden; }",
    ".admin-table__row { display: grid; grid-template-columns: minmax(0, 3fr) minmax(0, 1fr) minmax(0, 1fr); gap: 12px; padding: 12px 16px; border-top: 1px solid var(--color-neutral-3); color: inherit; text-decoration: none; }",
    ".admin-table__head { border-top: 0; background-color: var(--color-neutral-2); font-weight: 600; }",
    "a.admin-table__row:hover { background-color: var(--color-neutral-1); }",
    ".admin-table__cell { min-width: 0; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }",
    ".admin-table__cell.is-inactive { color: var(--color-neutral-7); }",
    ".admin-table__empty { padding: 16px; color: var(--color-neutral-7); }",
    ".admin-form { display: flex; flex-direction: column; gap: 12px; max-width: 480px; }",
    ".admin-form__actions { display: flex; flex-wrap: wrap; gap: 8px; }",
    ".admin-error { padding: 12px 16px; border-radius: var(--border-radius-soft); background-color: var(--color-error-lightest, #fdecea); color: var(--color-error, #c62828); }"
  });
  clientsScr.StyleSheet = css;
  detailScr.StyleSheet = css;

  var vRec = detailScr.CreateLocalVariable("EditClient"); vRec.DataType = clientEnt; vRec.Description = "Editable copy of the client, bound to the form (filled by OnAfterFetch_GetClient).";
  var vErr = detailScr.CreateLocalVariable("FormError"); vErr.DataType = eSpace.TextType; vErr.Description = "Message of the last failed save or activate/deactivate (e.g. ClientValidation), shown above the form.";
  var dd = detailScr.CreateDataAction("GetClient");
  dd.OutputParameters.ToList().ForEach(x => x.Delete());
  var ddRec = dd.CreateOutputParameter("ClientRecord"); ddRec.DataType = clientEnt;
  var ddProj = dd.CreateOutputParameter("Projects"); ddProj.DataType = eSpace.GetOrCreateListType(projectEnt);
  var ddErr = dd.CreateOutputParameter("ErrorMessage"); ddErr.DataType = eSpace.TextType;
  var ddSt = dd.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(ddSt, 0, 0);
  var ddIf = dd.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(ddIf, 0, 1); ddIf.SetCondition("ClientId = NullIdentifier()"); ddIf.Label = "New client?"; ddSt.Target = ddIf;
  var ddNew = dd.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ddNew, 1, 1); ddNew.CreateAssignment("ClientRecord.IsActive", "True"); ddIf.TrueTarget = ddNew;
  var ddNewEnd = dd.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(ddNewEnd, 1, 2); ddNew.Target = ddNewEnd;
  var ddEx = dd.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetById"); at(ddEx, 0, 2);
  ddEx.Action = aGetById; ddEx.SetArgumentValue(aGetById.InputParameters.Single(p => p.Name == "ClientId"), "ClientId"); ddIf.FalseTarget = ddEx;
  var ddAsg = dd.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ddAsg, 0, 3);
  ddAsg.CreateAssignment("ClientRecord", "Client_GetById.ClientRecord"); ddAsg.CreateAssignment("Projects", "Client_GetById.Projects"); ddEx.Target = ddAsg;
  var ddEnd = dd.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(ddEnd, 0, 4); ddAsg.Target = ddEnd;
  var ddH = dd.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(ddH, 3, 0); ddH.Exception = eSpace.AllExceptions; ddH.AbortTransaction = false;
  var ddHA = dd.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ddHA, 3, 1); ddHA.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage"); ddH.Target = ddHA;
  var ddHE = dd.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(ddHE, 3, 2); ddHA.Target = ddHE;

  var daf = detailScr.CreateScreenAction("OnAfterFetch_GetClient"); daf.Description = "Copies the fetched client into EditClient; shows an error message when GetClient failed.";
  var dafSt = start(daf);
  var dafAsg = daf.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(dafAsg, 0, 1); dafAsg.CreateAssignment("EditClient", "GetClient.ClientRecord"); dafSt.Target = dafAsg;
  var dafIf = daf.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(dafIf, 0, 2); dafIf.SetCondition("GetClient.ErrorMessage <> \"\""); dafIf.Label = "Failed?"; dafAsg.Target = dafIf;
  dafIf.FalseTarget = end(daf, 1, 2);
  var dafMsg = daf.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(dafMsg, 0, 3); dafMsg.SetMessage("GetClient.ErrorMessage"); dafMsg.Type = OutSystems.Model.Enumerations.MessageType.Error; dafIf.TrueTarget = dafMsg;
  dafMsg.Target = end(daf, 0, 4);
  dd.OnAfterFetch.Destination = daf;

  var sv = detailScr.CreateScreenAction("OnClick_SaveButton"); sv.Description = "Saves EditClient through Client_CreateOrUpdate and returns to Clients; a ClientValidation (or any other) error is shown above the form.";
  var svSt = start(sv);
  var svClr = sv.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(svClr, 0, 1); svClr.CreateAssignment("FormError", "\"\""); svSt.Target = svClr;
  var svEx = sv.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_CreateOrUpdate"); at(svEx, 0, 2);
  svEx.Action = aSave; svEx.SetArgumentValue(aSave.InputParameters.Single(p => p.Name == "ClientRecord"), "EditClient"); svClr.Target = svEx;
  var svMsg = sv.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(svMsg, 0, 3); svMsg.SetMessage(q("Client saved.")); svMsg.Type = OutSystems.Model.Enumerations.MessageType.Success; svEx.Target = svMsg;
  var svDest = sv.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>(); at(svDest, 0, 4); svDest.Destination = clientsScr; svMsg.Target = svDest;
  var svH = sv.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(svH, 2, 0); svH.Exception = eSpace.AllExceptions; svH.AbortTransaction = false; svH.LogError = false;
  var svHA = sv.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(svHA, 2, 1); svHA.CreateAssignment("FormError", "AllExceptions.ExceptionMessage"); svH.Target = svHA;
  var svHE = sv.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(svHE, 2, 2); svHA.Target = svHE;

  var tg = detailScr.CreateScreenAction("OnClick_ToggleActiveButton"); tg.Description = "Activates or deactivates the client through Client_SetActive and reloads GetClient.";
  var tgSt = start(tg);
  var tgClr = tg.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(tgClr, 0, 1); tgClr.CreateAssignment("FormError", "\"\""); tgSt.Target = tgClr;
  var tgEx = tg.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_SetActive"); at(tgEx, 0, 2);
  tgEx.Action = aSetActive;
  tgEx.SetArgumentValue(aSetActive.InputParameters.Single(p => p.Name == "ClientId"), "ClientId");
  tgEx.SetArgumentValue(aSetActive.InputParameters.Single(p => p.Name == "IsActive"), "not GetClient.ClientRecord.IsActive");
  tgClr.Target = tgEx;
  var tgR = tg.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(tgR, 0, 3); tgR.DataSource = dd; tgEx.Target = tgR;
  var tgMsg = tg.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(tgMsg, 0, 4); tgMsg.SetMessage("If(GetClient.ClientRecord.IsActive, \"Client activated.\", \"Client deactivated.\")"); tgMsg.Type = OutSystems.Model.Enumerations.MessageType.Success; tgR.Target = tgMsg;
  tgMsg.Target = end(tg, 0, 5);
  var tgH = tg.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(tgH, 2, 0); tgH.Exception = eSpace.AllExceptions; tgH.AbortTransaction = false; tgH.LogError = false;
  var tgHA = tg.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(tgHA, 2, 1); tgHA.CreateAssignment("FormError", "AllExceptions.ExceptionMessage"); tgH.Target = tgHA;
  var tgHE = tg.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(tgHE, 2, 2); tgHA.Target = tgHE;

  var lay2 = layoutOf(detailScr, "Client");
  var page2 = mainOf(lay2);
  var back = page2.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("BackLink");
  back.Widgets.ToList().ForEach(w => w.Delete());
  back.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Back to clients";
  back.OnClick.Destination = clientsScr;
  var errIf = page2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasError"); errIf.SetCondition("FormError <> \"\"");
  var errBox = errIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ErrorBox"); errBox.SetStyle(q("admin-error")); role(errBox, "alert");
  errBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("FormError");
  var form = box(page2, "ClientForm", "admin-form");
  text(form, "Name");
  var nameIn = form.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("NameInput");
  nameIn.SetVariable("EditClient.Name"); nameIn.SetMandatory("True"); nameIn.SetEnabled("GetClient.IsDataFetched"); nameIn.SetStyle(q("form-control"));
  var nml = nameIn.CreateExtendedProperty(); nml.Property = "maxlength"; nml.SetValue(q("100"));
  var nma = nameIn.CreateExtendedProperty(); nma.Property = "aria-label"; nma.SetValue(q("Client name"));
  var stat = box(form, "StatusLine", "admin-form__status");
  expr(stat, "If(ClientId = NullIdentifier(), \"New client\", If(GetClient.ClientRecord.IsActive, \"Status: active\", \"Status: inactive\"))");
  var acts = box(form, "FormActions", "admin-form__actions");
  var saveBtn = acts.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveButton");
  saveBtn.Widgets.ToList().ForEach(w => w.Delete());
  saveBtn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Save";
  saveBtn.SetEnabled("GetClient.IsDataFetched"); saveBtn.SetStyle(q("btn btn-primary"));
  saveBtn.OnClick.Destination = sv;
  saveBtn.OnClick.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
  var togIf = acts.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsExisting"); togIf.SetCondition("ClientId <> NullIdentifier()");
  var togBtn = togIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ToggleActiveButton");
  togBtn.Widgets.ToList().ForEach(w => w.Delete());
  togBtn.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("If(GetClient.ClientRecord.IsActive, \"Deactivate\", \"Activate\")");
  togBtn.SetEnabled("GetClient.IsDataFetched"); togBtn.SetStyle(q("btn"));
  togBtn.OnClick.Destination = tg;
  var projIf = page2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("ShowProjects"); projIf.SetCondition("ClientId <> NullIdentifier()");
  var projSec = projIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsSection"); projSec.SetStyle(q("admin-page"));
  var ph = projSec.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>(); ph.Tag = "h2"; ph.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Projects";
  var ptable = box(projSec, "ProjectsTable", "admin-table"); role(ptable, "table");
  var phead = box(ptable, "ProjectsHeader", "admin-table__row admin-table__head"); role(phead, "row");
  foreach (var h in new[] { "Project", "Code", "Status" }) { var hc = box(phead, null, "admin-table__cell"); role(hc, "columnheader"); text(hc, h); }
  var hasP = ptable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProjects"); hasP.SetCondition("GetClient.Projects.Length > 0 or not GetClient.IsDataFetched");
  var plist = hasP.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectList"); plist.SetSource("GetClient.Projects"); plist.SetStyle(q("admin-table__body"));
  var prow = plist.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectRow"); prow.SetStyle(q("admin-table__row")); role(prow, "row");
  var p1 = box(prow, null, "admin-table__cell admin-table__name"); expr(p1, "GetClient.Projects.Current.Name");
  var p2 = box(prow, null, "admin-table__cell"); expr(p2, "GetClient.Projects.Current.Code");
  var p3 = prow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); p3.SetStyle("\"admin-table__cell \" + If(GetClient.Projects.Current.IsActive, \"is-active\", \"is-inactive\")"); expr(p3, "If(GetClient.Projects.Current.IsActive, \"Active\", \"Inactive\")");
  foreach (var cc in new[] { p1, p2, p3 }) role(cc, "cell");
  var pEmpty = hasP.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoProjects"); pEmpty.SetStyle(q("admin-table__empty"));
  pEmpty.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "No projects for this client yet.";

  var ma = menuBlk.CreateDataAction("GetMenuAccess");
  ma.Description = "Which role-gated menu links the user may see (server-side role checks; referenced roles have no client-side check).";
  ma.OutputParameters.ToList().ForEach(x => x.Delete());
  var maOut = ma.CreateOutputParameter("IsSheetAdmin"); maOut.DataType = eSpace.BooleanType;
  var maSt = ma.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(maSt, 0, 0);
  var maAsg = ma.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(maAsg, 0, 1); maAsg.CreateAssignment("IsSheetAdmin", "CheckSheetAdminRole()"); maSt.Target = maAsg;
  var maEnd = ma.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(maEnd, 0, 2); maAsg.Target = maEnd;
  var adv = menuBlk.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>().First();
  var pl = adv.Content.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IContainer>().First(c => c.Name == "PageLinks");
  var link = pl.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ClientsLink");
  link.Widgets.ToList().ForEach(w => w.Delete());
  link.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Clients";
  link.OnClick.Destination = clientsScr;
  link.SetVisible("GetMenuAccess.IsSheetAdmin");
  var lr = link.CreateExtendedProperty(); lr.Property = "role"; lr.SetValue(q("menuitem"));
}
