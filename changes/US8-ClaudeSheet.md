# US8 · ClaudeSheet changes

Every `applyModelApiCode` save for US8 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-19 16:07:53 UTC

- **Published:** 2026-10-19 16:13:05 UTC (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  var cs = eSpace.References.Named("ClaudeSheet_CS");
  var adminFlow = eSpace.MobileFlows.Named("Admin");
  var layoutBlk = eSpace.MobileFlows.Named("Layouts").Nodes.OfType<IMobileBlock>().Named("LayoutTopMenu");
  var menuBlk = eSpace.MobileFlows.Named("Common").Nodes.OfType<IMobileBlock>().Named("Menu");
  var holEnt = cs.Entities.Single(e => e.Name == "Holiday");
  Func<string, OutSystems.Model.Logic.IServerActionSignature> act = n => cs.ServerActions.Single(a => a.Name == n);
  var aList = act("Holiday_GetList"); var aSave = act("Holiday_CreateOrUpdate"); var aAct = act("Holiday_SetActive"); var aCopy = act("Holiday_CopyYear");
  Func<OutSystems.Model.Logic.IServerActionSignature, string, OutSystems.Model.IInputParameterSignature> inp = (a, n) => a.InputParameters.Single(p => p.Name == n);
  Action<OutSystems.Model.Logic.Nodes.IActionNode, int, int> at = (n, c, r) => { n.HorizontalPosition = 3200 + c * 1828; n.VerticalPosition = 800 + r * 1371; };
  Func<string, string> q = t => "\"" + t + "\"";
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
  Func<ServiceStudio.Plugin.NRWidgets.IContainer, string, string, ServiceStudio.Plugin.NRWidgets.IButton> button = (parent, name, label) => {
    var b = parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>(name);
    b.Widgets.ToList().ForEach(w => w.Delete());
    b.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = label;
    b.SetEnabled("True"); return b;
  };
  Func<ServiceStudio.Plugin.NRWidgets.IContainer, string, string, ServiceStudio.Plugin.NRWidgets.ILink> link = (parent, name, label) => {
    var l = parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>(name);
    l.Widgets.ToList().ForEach(w => w.Delete());
    l.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = label;
    return l;
  };
  Action<ServiceStudio.Plugin.NRWidgets.IInput, string> aria = (w, v) => { var p = w.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q(v)); };

  var scr = adminFlow.CreateScreen("Holidays");
  scr.Widgets.ToList().ForEach(w => w.Delete());
  foreach (var r in scr.Roles.ToList()) if (r.Name != "SheetAdmin") scr.Roles.Remove(r);
  scr.Description = "Admin holiday calendar per year (SheetAdmin): year selector, list with weekday and status, add/edit in a popup, activate/deactivate, copy the fixed-date holidays to the next year.";
  scr.SetTitle(q("Holidays")); scr.HorizontalPosition = 400; scr.VerticalPosition = 2400;

  var vYear = scr.CreateLocalVariable("SelectedYear"); vYear.DataType = eSpace.IntegerType; vYear.Description = "Year shown; set to the current year in OnInitialize.";
  var vInact = scr.CreateLocalVariable("ShowInactive"); vInact.DataType = eSpace.BooleanType; vInact.SetDefaultValue("False");
  var vEdit = scr.CreateLocalVariable("EditHoliday"); vEdit.DataType = holEnt; vEdit.Description = "Holiday being added or edited in the popup.";
  var vShow = scr.CreateLocalVariable("ShowHolidayPopup"); vShow.DataType = eSpace.BooleanType; vShow.SetDefaultValue("False");
  var vPErr = scr.CreateLocalVariable("PopupError"); vPErr.DataType = eSpace.TextType; vPErr.Description = "Message of the last failed save (e.g. HolidayValidation), shown in the popup.";
  var vErr = scr.CreateLocalVariable("PageError"); vErr.DataType = eSpace.TextType; vErr.Description = "Message of the last failed activate/deactivate or copy, shown above the list.";

  var oi = scr.CreateScreenAction("OnInitialize"); oi.Description = "Shows the current year.";
  { var st = start(oi); var asg = oi.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1); asg.CreateAssignment("SelectedYear", "Year(CurrDate())"); st.Target = asg; asg.Target = end(oi, 0, 2); }
  scr.OnInitialize.Destination = oi;

  var da = scr.CreateDataAction("GetHolidays");
  da.OutputParameters.ToList().ForEach(x => x.Delete());
  { var o = da.CreateOutputParameter("Holidays"); o.DataType = aList.OutputParameters.Single(p => p.Name == "Holidays").DataType; }
  { var o = da.CreateOutputParameter("ErrorMessage"); o.DataType = eSpace.TextType; }
  {
    var st = da.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0);
    var ex = da.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_GetList"); at(ex, 0, 1); ex.Action = aList;
    ex.SetArgumentValue(inp(aList, "Year"), "SelectedYear"); ex.SetArgumentValue(inp(aList, "IncludeInactive"), "ShowInactive"); st.Target = ex;
    var asg = da.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 2); asg.CreateAssignment("Holidays", "Holiday_GetList.Holidays"); ex.Target = asg;
    var en = da.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(en, 0, 3); asg.Target = en;
    var h = da.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions"); at(h, 2, 0); h.Exception = eSpace.AllExceptions; h.AbortTransaction = false;
    var ha = da.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(ha, 2, 1); ha.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage"); h.Target = ha;
    var he = da.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(he, 2, 2); ha.Target = he;
  }
  var oaf = scr.CreateScreenAction("OnAfterFetch_GetHolidays"); oaf.Description = "Shows an error message when GetHolidays failed.";
  {
    var st = start(oaf);
    var iff = oaf.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(iff, 0, 1); iff.SetCondition("GetHolidays.ErrorMessage <> \"\""); iff.Label = "Failed?"; st.Target = iff;
    iff.FalseTarget = end(oaf, 1, 1);
    var m = oaf.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 2); m.SetMessage("GetHolidays.ErrorMessage"); m.Type = OutSystems.Model.Enumerations.MessageType.Error; iff.TrueTarget = m;
    m.Target = end(oaf, 0, 3);
  }
  da.OnAfterFetch.Destination = oaf;

  Func<string, string, string, OutSystems.Model.UI.Mobile.IScreenAction> yearAct = (name, desc, exprY) => {
    var a = scr.CreateScreenAction(name); a.Description = desc;
    var st = start(a);
    var asg = a.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1); asg.CreateAssignment("SelectedYear", exprY); asg.CreateAssignment("PageError", "\"\""); st.Target = asg;
    var rf = a.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 2); rf.DataSource = da; asg.Target = rf;
    rf.Target = end(a, 0, 3);
    return a;
  };
  var prevY = yearAct("OnClick_PreviousYearButton", "Shows the previous year.", "SelectedYear - 1");
  var nextY = yearAct("OnClick_NextYearButton", "Shows the next year.", "SelectedYear + 1");
  var flt = scr.CreateScreenAction("OnChange_ShowInactiveCheckbox"); flt.Description = "Reloads GetHolidays with or without inactive holidays.";
  { var st = start(flt); var rf = flt.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 1); rf.DataSource = da; st.Target = rf; rf.Target = end(flt, 0, 2); }

  var opAdd = scr.CreateScreenAction("OnClick_AddHolidayButton"); opAdd.Description = "Opens the popup for a new holiday (active, same date every year).";
  {
    var st = start(opAdd);
    var asg = opAdd.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1);
    asg.CreateAssignment("EditHoliday.Id", "NullIdentifier()"); asg.CreateAssignment("EditHoliday.Name", "\"\""); asg.CreateAssignment("EditHoliday.HolidayDate", "NullDate()");
    asg.CreateAssignment("EditHoliday.IsActive", "True"); asg.CreateAssignment("EditHoliday.IsFixedDate", "True");
    asg.CreateAssignment("PopupError", "\"\""); asg.CreateAssignment("ShowHolidayPopup", "True");
    st.Target = asg; asg.Target = end(opAdd, 0, 2);
  }
  var opEdit = scr.CreateScreenAction("OnClick_EditHolidayLink"); opEdit.Description = "Opens the popup to edit a holiday.";
  { var rp = opEdit.CreateInputParameter("Row"); rp.DataType = holEnt; }
  {
    var st = start(opEdit);
    var asg = opEdit.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1);
    asg.CreateAssignment("EditHoliday", "Row"); asg.CreateAssignment("PopupError", "\"\""); asg.CreateAssignment("ShowHolidayPopup", "True");
    st.Target = asg; asg.Target = end(opEdit, 0, 2);
  }
  var sv = scr.CreateScreenAction("OnClick_SaveHolidayButton"); sv.Description = "Saves EditHoliday through Holiday_CreateOrUpdate, closes the popup and shows the holiday's year. HolidayValidation (or any other) error is shown in the popup.";
  {
    var st = start(sv);
    var clr = sv.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(clr, 0, 1); clr.CreateAssignment("PopupError", "\"\""); st.Target = clr;
    var ex = sv.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateOrUpdate"); at(ex, 0, 2); ex.Action = aSave; ex.SetArgumentValue(inp(aSave, "HolidayRecord"), "EditHoliday"); clr.Target = ex;
    var cl = sv.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cl, 0, 3); cl.CreateAssignment("ShowHolidayPopup", "False"); cl.CreateAssignment("SelectedYear", "Year(EditHoliday.HolidayDate)"); ex.Target = cl;
    var rf = sv.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 4); rf.DataSource = da; cl.Target = rf;
    var m = sv.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 5); m.SetMessage(q("Holiday saved.")); m.Type = OutSystems.Model.Enumerations.MessageType.Success; rf.Target = m;
    m.Target = end(sv, 0, 6);
  }
  handler(sv, "PopupError", 2);
  var pc = scr.CreateScreenAction("OnClick_CancelPopupButton"); pc.Description = "Closes the holiday popup without saving.";
  { var st = start(pc); var asg = pc.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(asg, 0, 1); asg.CreateAssignment("ShowHolidayPopup", "False"); st.Target = asg; asg.Target = end(pc, 0, 2); }
  var tg = scr.CreateScreenAction("OnClick_ToggleHolidayLink"); tg.Description = "Activates or deactivates a holiday through Holiday_SetActive and reloads GetHolidays.";
  { var rp = tg.CreateInputParameter("Row"); rp.DataType = holEnt; }
  {
    var st = start(tg);
    var clr = tg.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(clr, 0, 1); clr.CreateAssignment("PageError", "\"\""); st.Target = clr;
    var ex = tg.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_SetActive"); at(ex, 0, 2); ex.Action = aAct;
    ex.SetArgumentValue(inp(aAct, "HolidayId"), "Row.Id"); ex.SetArgumentValue(inp(aAct, "IsActive"), "not Row.IsActive"); clr.Target = ex;
    var rf = tg.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>(); at(rf, 0, 3); rf.DataSource = da; ex.Target = rf;
    var m = tg.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 4); m.SetMessage("If(Row.IsActive, \"Holiday deactivated.\", \"Holiday activated.\")"); m.Type = OutSystems.Model.Enumerations.MessageType.Success; rf.Target = m;
    m.Target = end(tg, 0, 5);
  }
  handler(tg, "PageError", 2);
  var cp = scr.CreateScreenAction("OnClick_CopyYearButton"); cp.Description = "Copies the fixed-date holidays of SelectedYear to the next year through Holiday_CopyYear and reports how many were added.";
  {
    var st = start(cp);
    var clr = cp.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(clr, 0, 1); clr.CreateAssignment("PageError", "\"\""); st.Target = clr;
    var ex = cp.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CopyYear"); at(ex, 0, 2); ex.Action = aCopy;
    ex.SetArgumentValue(inp(aCopy, "FromYear"), "SelectedYear"); ex.SetArgumentValue(inp(aCopy, "ToYear"), "SelectedYear + 1"); clr.Target = ex;
    var m = cp.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>(); at(m, 0, 3);
    m.SetMessage("If(Holiday_CopyYear.CopiedCount = 0, \"Nothing to copy: \" + (SelectedYear + 1) + \" already has these holidays.\", Holiday_CopyYear.CopiedCount + \" holiday(s) copied to \" + (SelectedYear + 1) + \".\")");
    m.Type = OutSystems.Model.Enumerations.MessageType.Success; ex.Target = m;
    m.Target = end(cp, 0, 4);
  }
  handler(cp, "PageError", 2);

  var lay = scr.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
  lay.SourceBlock = layoutBlk;
  { var mi = lay.PlaceholdersContent.First(p => p.Placeholder.Name == "Header").Widgets.OfType<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>().First(); mi.SetArgumentValue(menuBlk.InputParameters.Named("ActiveItem"), "3"); }
  lay.PlaceholdersContent.First(p => p.Placeholder.Name == "Title").CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Holidays";
  var page = lay.PlaceholdersContent.First(p => p.Placeholder.Name == "MainContent").CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage"); page.SetStyle(q("admin-page"));
  var tb = box(page, "Toolbar", "admin-toolbar");
  var bPrev = button(tb, "PreviousYearButton", "<"); bPrev.SetStyle(q("btn")); bPrev.OnClick.Destination = prevY;
  { var p = bPrev.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("Previous year")); }
  var yl = box(tb, "YearLabel", "text-bold"); expr(yl, "SelectedYear");
  { var p = yl.CreateExtendedProperty(); p.Property = "aria-live"; p.SetValue(q("polite")); }
  var bNext = button(tb, "NextYearButton", ">"); bNext.SetStyle(q("btn")); bNext.OnClick.Destination = nextY;
  { var p = bNext.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("Next year")); }
  var chkWrap = box(tb, "ShowInactiveWrap", "admin-toolbar__check");
  var chk = chkWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox"); chk.SetVariable("ShowInactive"); chk.SetEnabled("True");
  { var p = chk.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("Show inactive holidays")); }
  chk.OnChange.Destination = flt; text(chkWrap, "Show inactive");
  var bAdd = button(tb, "AddHolidayButton", "Add holiday"); bAdd.SetStyle(q("btn btn-primary")); bAdd.OnClick.Destination = opAdd;
  var bCopy = tb.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CopyYearButton"); bCopy.Widgets.ToList().ForEach(w => w.Delete());
  bCopy.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("\"Copy fixed holidays to \" + (SelectedYear + 1)");
  bCopy.SetEnabled("GetHolidays.IsDataFetched"); bCopy.SetStyle(q("btn")); bCopy.OnClick.Destination = cp;
  var errIf = page.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPageError"); errIf.SetCondition("PageError <> \"\"");
  { var eb = errIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageErrorBox"); eb.SetStyle(q("admin-error")); role(eb, "alert"); expr(eb, "PageError"); }

  var table = box(page, "HolidaysTable", "admin-table admin-table--5"); role(table, "table");
  var head = box(table, "HolidaysHeader", "admin-table__row admin-table__head"); role(head, "row");
  foreach (var h in new[] { "Date", "Weekday", "Name", "Status", "Actions" }) { var hc = box(head, null, "admin-table__cell"); role(hc, "columnheader"); text(hc, h); }
  var has = table.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasHolidays"); has.SetCondition("GetHolidays.Holidays.Length > 0 or not GetHolidays.IsDataFetched");
  var list = has.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("HolidayList"); list.SetSource("GetHolidays.Holidays"); list.SetStyle(q("admin-table__body"));
  var row = list.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayRow"); row.SetStyle(q("admin-table__row")); role(row, "row");
  var cells = new List<ServiceStudio.Plugin.NRWidgets.IContainer>();
  { var c = box(row, null, "admin-table__cell"); expr(c, "FormatDateTime(DateToDateTime(GetHolidays.Holidays.Current.HolidayDate), \"dd/MM/yyyy\")"); cells.Add(c); }
  { var c = box(row, null, "admin-table__cell"); expr(c, "FormatDateTime(DateToDateTime(GetHolidays.Holidays.Current.HolidayDate), \"dddd\")"); cells.Add(c); }
  { var c = box(row, null, "admin-table__cell admin-table__name"); expr(c, "GetHolidays.Holidays.Current.Name + If(GetHolidays.Holidays.Current.IsFixedDate, \"\", \" (movable)\")"); cells.Add(c); }
  { var c = row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); c.SetStyle("\"admin-table__cell \" + If(GetHolidays.Holidays.Current.IsActive, \"is-active\", \"is-inactive\")"); expr(c, "If(GetHolidays.Holidays.Current.IsActive, \"Active\", \"Inactive\")"); cells.Add(c); }
  var acCell = box(row, "HolidayActions", "admin-table__cell admin-table__actions"); cells.Add(acCell);
  foreach (var c in cells) role(c, "cell");
  { var l = link(acCell, "EditHolidayLink", "Edit"); l.OnClick.Destination = opEdit; l.OnClick.Arguments.First(a => a.Parameter.Name == "Row").SetValue("GetHolidays.Holidays.Current"); }
  {
    var l = acCell.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ToggleHolidayLink"); l.Widgets.ToList().ForEach(w => w.Delete());
    l.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("If(GetHolidays.Holidays.Current.IsActive, \"Deactivate\", \"Activate\")");
    l.OnClick.Destination = tg; l.OnClick.Arguments.First(a => a.Parameter.Name == "Row").SetValue("GetHolidays.Holidays.Current");
  }
  var empty = has.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoHolidays"); empty.SetStyle(q("admin-table__empty"));
  expr(empty, "\"No holidays for \" + SelectedYear + \" yet.\"");

  var pop = page.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("HolidayPopup"); pop.SetShowPopup("ShowHolidayPopup");
  var pb = pop.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayPopupBody"); pb.SetStyle(q("admin-popup")); role(pb, "dialog");
  { var h3 = pb.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>(); h3.Tag = "h3"; h3.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue("If(EditHoliday.Id = NullIdentifier(), \"Add holiday\", \"Edit holiday\")"); }
  var pErrIf = pb.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPopupError"); pErrIf.SetCondition("PopupError <> \"\"");
  { var eb = pErrIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupErrorBox"); eb.SetStyle(q("admin-error")); role(eb, "alert"); expr(eb, "PopupError"); }
  var form = box(pb, "HolidayForm", "admin-form");
  text(form, "Date");
  var dIn = form.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("HolidayDateInput");
  dIn.SetVariable("EditHoliday.HolidayDate"); dIn.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date; dIn.SetMandatory("True"); dIn.SetEnabled("True"); dIn.SetStyle(q("form-control")); aria(dIn, "Holiday date");
  text(form, "Name");
  var nIn = form.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("HolidayNameInput");
  nIn.SetVariable("EditHoliday.Name"); nIn.SetMandatory("True"); nIn.SetEnabled("True"); nIn.SetStyle(q("form-control")); aria(nIn, "Holiday name");
  { var p = nIn.CreateExtendedProperty(); p.Property = "maxlength"; p.SetValue(q("100")); }
  var fxWrap = box(form, "FixedDateWrap", "admin-toolbar__check");
  var fxChk = fxWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("FixedDateCheckbox"); fxChk.SetVariable("EditHoliday.IsFixedDate"); fxChk.SetEnabled("True");
  { var p = fxChk.CreateExtendedProperty(); p.Property = "aria-label"; p.SetValue(q("Same date every year")); }
  text(fxWrap, "Same date every year");
  { var hint = box(form, "FixedDateHint", "admin-form__hint"); text(hint, "Clear it for movable holidays (Good Friday, Easter, Corpus Christi): they are not copied to the next year."); }
  var pAct = box(pb, "PopupActions", "admin-form__actions");
  var bSave = button(pAct, "SaveHolidayButton", "Save"); bSave.SetStyle(q("btn btn-primary")); bSave.OnClick.Destination = sv; bSave.OnClick.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
  var bCancel = button(pAct, "CancelPopupButton", "Cancel"); bCancel.SetStyle(q("btn")); bCancel.OnClick.Destination = pc;

  var adv = menuBlk.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>().First();
  var pl = adv.Content.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IContainer>().First(c => c.Name == "PageLinks");
  var ml = pl.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("HolidaysLink");
  ml.Widgets.ToList().ForEach(w => w.Delete());
  ml.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "Holidays";
  ml.OnClick.Destination = scr; ml.SetVisible("GetMenuAccess.IsSheetAdmin");
  { var p = ml.CreateExtendedProperty(); p.Property = "role"; p.SetValue(q("menuitem")); }
}
```
