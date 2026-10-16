// Published: 2026-10-16T11:38:00+00:00 (manual 1-Click Publish; logged by Claude, applied by the main session)
// Module:   ClaudeSheet
// When:     2026-10-16T11:16:49+00:00
// Story:    US5
// Outcome:  saved
// Imports:  -
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
