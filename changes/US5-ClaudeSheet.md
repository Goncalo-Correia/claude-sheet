# US5 · ClaudeSheet changes

Every `applyModelApiCode` save for US5 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-16 11:11:54 UTC

- **Published:** 2026-10-16 11:38:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => { var role = eSpace.References.Named("ClaudeSheet_CS").Roles.Named("SheetUser2"); role.Name = "SheetUser"; var common = eSpace.MobileFlows.Named("Common"); foreach (var n in new[] { "Login", "InvalidPermissions" }) { var sc = common.Nodes.OfType<IMobileScreen>().Named(n); if (!sc.Roles.Contains(eSpace.AnonymousRole)) sc.Roles.Add(eSpace.AnonymousRole); } }
```

## Save 2 · 2026-10-16 11:15:02 UTC

- **Published:** 2026-10-16 11:38:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved
- **Imports:** OutSystems.Model.Logic.Nodes

```csharp
eSpace => {
  var mainFlow = eSpace.MobileFlows.Named("MainFlow");
  var common = eSpace.MobileFlows.Named("Common");
  var blocksFlow = eSpace.MobileFlows.Named("SheetBlocks");
  var layoutBlk = eSpace.MobileFlows.Named("Layouts").Nodes.OfType<IMobileBlock>().Named("LayoutTopMenu");
  var menuBlk = common.Nodes.OfType<IMobileBlock>().Named("Menu");
  var total = blocksFlow.Nodes.OfType<IMobileBlock>().Named("TotalHoursMonth");
  var cal = blocksFlow.Nodes.OfType<IMobileBlock>().Named("HoursCalendar");
  var kpis = blocksFlow.Nodes.OfType<IMobileBlock>().Named("DayStatusKPIs");
  var week = blocksFlow.Nodes.OfType<IMobileBlock>().Named("WeeklyProjectHours");

  var scr = mainFlow.CreateScreen("MySheet");
  scr.Widgets.ToList().ForEach(w => w.Delete());
  scr.Description = "The consultant's timesheet: monthly total, calendar, day status KPIs and weekly project hours, kept in sync. Default screen of the module.";
  scr.SetTitle("\"My Sheet\"");
  scr.HorizontalPosition = 400; scr.VerticalPosition = 400;
  foreach (var r in scr.Roles.ToList()) if (r.Name != "SheetUser") scr.Roles.Remove(r);

  var vYear = scr.CreateLocalVariable("SelectedYear"); vYear.DataType = eSpace.IntegerType; vYear.Description = "Year shown by the total, calendar and KPIs. Set in OnInitialize and by OnMonthChanged_HoursCalendar.";
  var vMonth = scr.CreateLocalVariable("SelectedMonth"); vMonth.DataType = eSpace.IntegerType; vMonth.Description = "Month (1-12) shown by the total, calendar and KPIs. Set in OnInitialize and by OnMonthChanged_HoursCalendar.";
  var vWeek = scr.CreateLocalVariable("WeekReferenceDate"); vWeek.DataType = eSpace.DateType; vWeek.Description = "A date in the week shown by WeeklyProjectHours. Today at start; follows the selected month, the selected day and the week arrows.";
  var vKey = scr.CreateLocalVariable("RefreshKey"); vKey.DataType = eSpace.IntegerType; vKey.SetDefaultValue("0"); vKey.Description = "Incremented after a day is saved so every block reloads.";

  Func<string, string, OutSystems.Model.UI.Mobile.IScreenAction> newAction = (name, desc) => { var a = scr.CreateScreenAction(name); a.Description = desc; return a; };
  Func<OutSystems.Model.UI.Mobile.IScreenAction, string[][], OutSystems.Model.Logic.Nodes.IEndNode> buildFlow = (a, assigns) => {
    var st = a.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); st.HorizontalPosition = 3200; st.VerticalPosition = 800;
    var asg = a.CreateNode<IAssignNode>().ConnectedBelow(st, 1371);
    foreach (var p in assigns) asg.CreateAssignment(p[0], p[1]);
    return a.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(asg, 1371);
  };

  var onInit = newAction("OnInitialize", "Starts on the current month and the current week.");
  buildFlow(onInit, new[] { new[] { "SelectedYear", "Year(CurrDate())" }, new[] { "SelectedMonth", "Month(CurrDate())" }, new[] { "WeekReferenceDate", "CurrDate()" } });
  scr.OnInitialize.Destination = onInit;

  var onMonth = newAction("OnMonthChanged_HoursCalendar", "The calendar moved to another month: the total, calendar and KPIs follow it, and the weekly table shows the current week (current month) or the week of the 1st (other months).");
  var pYear = onMonth.CreateInputParameter("Year"); pYear.DataType = eSpace.IntegerType;
  var pMonth = onMonth.CreateInputParameter("Month"); pMonth.DataType = eSpace.IntegerType;
  buildFlow(onMonth, new[] { new[] { "SelectedYear", "Year" }, new[] { "SelectedMonth", "Month" }, new[] { "WeekReferenceDate", "If(Year = Year(CurrDate()) and Month = Month(CurrDate()), CurrDate(), NewDate(Year, Month, 1))" } });

  var onSaved = newAction("OnDaySaved_HoursCalendar", "A day was saved in the calendar: increments RefreshKey so all four blocks reload.");
  buildFlow(onSaved, new[] { new[] { "RefreshKey", "RefreshKey + 1" } });

  var onSelected = newAction("OnDaySelected_HoursCalendar", "A day was clicked in the calendar: the weekly table jumps to that day's week.");
  var pDate = onSelected.CreateInputParameter("Date"); pDate.DataType = eSpace.DateType;
  buildFlow(onSelected, new[] { new[] { "WeekReferenceDate", "Date" } });

  var onWeek = newAction("OnWeekChanged_WeeklyProjectHours", "The weekly table moved to another week: keeps WeekReferenceDate in step (the month doesn't change), so selecting a day later always moves the table.");
  var pWeekStart = onWeek.CreateInputParameter("WeekStartDate"); pWeekStart.DataType = eSpace.DateType;
  buildFlow(onWeek, new[] { new[] { "WeekReferenceDate", "WeekStartDate" } });

  var layout = scr.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
  layout.SourceBlock = layoutBlk;
  layout.SetArgumentValue(layoutBlk.InputParameters.Named("ExtendedClass"), "\"sheet-layout\"");
  var phHeader = layout.PlaceholdersContent.First(p => p.Placeholder.Name == "Header");
  var menuInst = phHeader.Widgets.OfType<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>().First();
  menuInst.SetArgumentValue(menuBlk.InputParameters.Named("ActiveItem"), "0");
  var main = layout.PlaceholdersContent.First(p => p.Placeholder.Name == "MainContent");

  var page = main.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("SheetPage"); page.SetStyle("\"sheet-page\"");
  var header = page.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageHeader"); header.SetStyle("\"sheet-page__header\"");
  var h1 = header.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>(); h1.Tag = "h1";
  var h1c = h1.CreateExtendedProperty(); h1c.Property = "class"; h1c.SetValue("\"sheet-page__title\"");
  h1.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "My Sheet";
  var h4 = header.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>(); h4.Tag = "h4";
  var h4c = h4.CreateExtendedProperty(); h4c.Property = "class"; h4c.SetValue("\"sheet-page__subtitle\"");
  h4.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "What did you work on today?";

  var top = page.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthRow"); top.SetStyle("\"sheet-page__top\"");
  Func<ServiceStudio.Plugin.NRWidgets.IContainer, string, string, IMobileBlock, OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget> place = (parent, cname, css, blk) => {
    var c = parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(cname); c.SetStyle("\"" + css + "\"");
    var i = c.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>(); i.SourceBlock = blk; return i;
  };
  var totalInst = place(top, "TotalColumn", "sheet-page__total", total);
  var calInst = place(top, "CalendarColumn", "sheet-page__calendar", cal);
  var kpisInst = place(top, "KPIsColumn", "sheet-page__kpis", kpis);
  foreach (var pair in new[] { Tuple.Create(totalInst, total), Tuple.Create(calInst, cal), Tuple.Create(kpisInst, kpis) }) {
    pair.Item1.SetArgumentValue(pair.Item2.InputParameters.Named("Year"), "SelectedYear");
    pair.Item1.SetArgumentValue(pair.Item2.InputParameters.Named("Month"), "SelectedMonth");
    pair.Item1.SetArgumentValue(pair.Item2.InputParameters.Named("RefreshKey"), "RefreshKey");
  }
  var weekRow = page.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("WeekRow"); weekRow.SetStyle("\"sheet-page__week\"");
  var weekInst = weekRow.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>(); weekInst.SourceBlock = week;
  weekInst.SetArgumentValue(week.InputParameters.Named("ReferenceDate"), "WeekReferenceDate");
  weekInst.SetArgumentValue(week.InputParameters.Named("RefreshKey"), "RefreshKey");

  var ehMonth = calInst.EventHandlers.First(h => h.Event.Name == "OnMonthChanged"); ehMonth.Handler = onMonth;
  ehMonth.SetArgumentValue(pYear, "Year"); ehMonth.SetArgumentValue(pMonth, "Month");
  var ehSaved = calInst.EventHandlers.First(h => h.Event.Name == "OnDaySaved"); ehSaved.Handler = onSaved;
  var ehSel = calInst.EventHandlers.First(h => h.Event.Name == "OnDaySelected"); ehSel.Handler = onSelected;
  ehSel.SetArgumentValue(pDate, "Date");
  var ehWeek = weekInst.EventHandlers.First(h => h.Event.Name == "OnWeekChanged"); ehWeek.Handler = onWeek;
  ehWeek.SetArgumentValue(pWeekStart, "WeekStartDate");

  scr.StyleSheet = string.Join("\n", new[] {
    "/* MySheet (US5): dark page, 3-column month row, weekly table below. Rules are scoped with :has(.sheet-page) so they stop applying when another screen is shown. */",
    ":root:has(.sheet-page) { --sheet-bg: #000000; }",
    "body:has(.sheet-page), body:has(.sheet-page) .layout, body:has(.sheet-page) .main, body:has(.sheet-page) .header, body:has(.sheet-page) .content { background-color: var(--sheet-bg); color: #FFFFFF; }",
    "body:has(.sheet-page) .header { border-bottom: 1px solid var(--sheet-track); box-shadow: none; }",
    "body:has(.sheet-page) .app-menu-links a { color: #FFFFFF; }",
    "body:has(.sheet-page) .app-menu-links a.active { color: var(--sheet-accent); }",
    ".sheet-page { display: flex; flex-direction: column; gap: 32px; color: #FFFFFF; }",
    ".sheet-page__header { display: flex; flex-direction: column; gap: 4px; }",
    ".sheet-page__title { margin: 0; color: #FFFFFF; }",
    ".sheet-page__subtitle { margin: 0; color: #FFFFFF; font-weight: 700; }",
    ".sheet-page__top { display: grid; grid-template-columns: minmax(0, 1fr) minmax(0, 1.8fr) minmax(0, 1fr); align-items: center; gap: 32px; }",
    ".sheet-page__top > *, .sheet-page__week { min-width: 0; }",
    "/* Tablet: Total + Calendar on row 1, KPIs (2x2) on a full-width row below. */",
    "@media (max-width: 1024px) { .sheet-page__top { grid-template-columns: minmax(0, 1fr) minmax(0, 1.8fr); } .sheet-page__kpis { grid-column: 1 / -1; } }",
    "/* Phone: one column, Total, Calendar, KPIs, Weekly. */",
    "@media (max-width: 767px) { .sheet-page__top { grid-template-columns: minmax(0, 1fr); } }"
  });

  Func<IEnumerable<OutSystems.Model.UI.Mobile.Widgets.IMobileWidget>, ServiceStudio.Plugin.NRWidgets.IContainer> findLinks = null;
  findLinks = ws => {
    foreach (var w in ws) {
      var c = w as ServiceStudio.Plugin.NRWidgets.IContainer;
      if (c != null) { if (c.Name == "PageLinks") return c; var r = findLinks(c.Widgets); if (r != null) return r; }
      var a = w as ServiceStudio.Plugin.NRWidgets.IAdvancedHtml;
      if (a != null) { var r = findLinks(a.Content.Widgets); if (r != null) return r; }
    }
    return null;
  };
  var pageLinks = findLinks(menuBlk.Widgets);
  var link = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("MySheetLink");
  link.Widgets.ToList().ForEach(x => x.Delete());
  link.OnClick.Destination = scr;
  link.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>().Text = "My Sheet";
  var lr = link.CreateExtendedProperty(); lr.Property = "role"; lr.SetValue("\"menuitem\"");

  eSpace.DefaultScreen = scr;
}
```

## Save 3 · 2026-10-16 11:16:49 UTC

- **Published:** 2026-10-16 11:38:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  Func<object, string> txt = o => o.ToString().Split(new[] { " [" }, StringSplitOptions.None)[0];
  var flow = eSpace.MobileFlows.Named("SheetBlocks");
  foreach (var n in new[] { "TotalHoursMonth", "HoursCalendar", "DayStatusKPIs", "WeeklyProjectHours" }) {
    var b = flow.Nodes.OfType<IMobileBlock>().Named(n);
    var root = b.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IContainer>().First();
    var ep = root.CreateExtendedProperty(); ep.Property = "data-refresh-key"; ep.SetValue("RefreshKey");
  }

  var cal = flow.Nodes.OfType<IMobileBlock>().Named("HoursCalendar");
  Func<IEnumerable<OutSystems.Model.UI.Mobile.Widgets.IMobileWidget>, ServiceStudio.Plugin.NRWidgets.IList> findList = null;
  findList = ws => { foreach (var w in ws) { var l = w as ServiceStudio.Plugin.NRWidgets.IList; if (l != null && l.Name == "CalendarDays") return l; var c = w as ServiceStudio.Plugin.NRWidgets.IContainer; if (c != null) { var r = findList(c.Widgets); if (r != null) return r; } var a = w as ServiceStudio.Plugin.NRWidgets.IAdvancedHtml; if (a != null) { var r = findList(a.Content.Widgets); if (r != null) return r; } var i = w as OutSystems.Model.UI.Mobile.Widgets.IIfWidget; if (i != null) { var r = findList(i.TrueBranch.Widgets.Concat(i.FalseBranch.Widgets)); if (r != null) return r; } } return null; };
  var list = findList(cal.Widgets);
  var oldIf = list.Widgets.OfType<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>().Single();
  var oldBlank = (ServiceStudio.Plugin.NRWidgets.IContainer)oldIf.TrueBranch.Widgets.Single();
  var oldBtn = (ServiceStudio.Plugin.NRWidgets.IButton)oldIf.FalseBranch.Widgets.Single();
  var condText = txt(oldIf.Condition);
  var blankStyle = txt(oldBlank.Style);
  var btnStyle = txt(oldBtn.Style);
  var btnEnabled = txt(oldBtn.Enabled);
  var dest = oldBtn.OnClick.Destination;
  var argVals = oldBtn.OnClick.Arguments.Select(a => Tuple.Create(a.Parameter.Name, txt(a.Value))).ToList();
  var exts = oldBtn.ExtendedProperties.Select(e => Tuple.Create(e.Property, txt(e.Value))).ToList();
  var kids = oldBtn.Widgets.Cast<ServiceStudio.Plugin.NRWidgets.IContainer>().Select(c => Tuple.Create(txt(c.Style), c.Widgets.OfType<ServiceStudio.Plugin.NRWidgets.IExpression>().Select(x => txt(x.Value)).ToList())).ToList();
  oldIf.Delete();

  var cell = list.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("CalendarCell");
  cell.SetStyle("\"sheet-cal__cell\"");
  var newIf = cell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsBlankCell");
  newIf.SetCondition(condText);
  var blank = newIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
  blank.SetStyle(blankStyle);
  var btn = newIf.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("DayCell");
  btn.Widgets.ToList().ForEach(x => x.Delete());
  btn.SetEnabled(btnEnabled);
  btn.SetStyle(btnStyle);
  foreach (var k in kids) {
    var c = btn.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    c.SetStyle(k.Item1);
    foreach (var v in k.Item2) c.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>().SetValue(v);
  }
  btn.OnClick.Destination = dest;
  foreach (var av in argVals) btn.OnClick.Arguments.First(a => a.Parameter.Name == av.Item1).SetValue(av.Item2);
  foreach (var e in exts) { var ep = btn.CreateExtendedProperty(); ep.Property = e.Item1; ep.SetValue(e.Item2); }

  eSpace.ClientActions.Named("Date_GetWeekStart").Delete();
}
```
