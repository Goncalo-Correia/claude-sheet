# TT8 · ClaudeSheet changes

Every `applyModelApiCode` save for TT8 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-19 16:38:18 UTC

- **Published:** 2026-10-19 16:47:23 UTC (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  var raw = new[] {
    "C:Minutes_Format#2|Format h:mm",
    "DayStatusKPIs.OnAfterFetch_GetKPIs#4|Show error","DayStatusKPIs.data:GetKPIs#4|Keep error","DayStatusKPIs.data:GetKPIs#7|Each day type",
    "TotalHoursMonth.OnAfterFetch_GetSummary#4|Show error","TotalHoursMonth.data:GetSummary#4|Keep error","TotalHoursMonth.data:GetSummary#5|Set summary",
    "WeeklyProjectHours.OnInitialize#2|Set reference date","WeeklyProjectHours.OnParametersChanged#2|Set reference date","WeeklyProjectHours.OnClick_PreviousWeek#2|Previous week","WeeklyProjectHours.OnAfterFetch_GetWeek#3|Show error","WeeklyProjectHours.OnClick_NextWeek#2|Next week","WeeklyProjectHours.data:GetWeek#4|Keep error","WeeklyProjectHours.data:GetWeek#5|Set week",
    "HoursCalendar.OnClick_ConfirmFillButton#4|No project error","HoursCalendar.OnClick_ConfirmFillButton#5|Keep error","HoursCalendar.OnClick_ConfirmFillButton#9|Close popup","HoursCalendar.OnClick_ConfirmFillButton#11|Show filled count",
    "HoursCalendar.OnClick_SaveDay#3|Clear error","HoursCalendar.OnClick_SaveDay#4|Keep error","HoursCalendar.OnClick_SaveDay#5|Each editor line","HoursCalendar.OnClick_SaveDay#6|Build entry",
    "HoursCalendar.OnInitialize#2|Start hours at 0","HoursCalendar.OnInitialize#4|Hour option","HoursCalendar.OnInitialize#6|Next hour","HoursCalendar.OnInitialize#7|Start minutes at 0","HoursCalendar.OnInitialize#9|Minute option","HoursCalendar.OnInitialize#11|Next quarter",
    "HoursCalendar.OpenDayEditor#3|Set day state","HoursCalendar.OpenDayEditor#5|Each detail line","HoursCalendar.OpenDayEditor#6|Build editor line","HoursCalendar.OpenDayEditor#9|Open editor",
    "HoursCalendar.OnClick_ClearDayButton#4|Keep error","HoursCalendar.OnClick_FillPendingButton#3|Init fill popup","HoursCalendar.FinishDaySave#2|Close editor","HoursCalendar.FinishDaySave#4|Show success","HoursCalendar.OnClick_CancelButton#2|Close editor",
    "HoursCalendar.RecalcEditorTotal#2|Reset total","HoursCalendar.RecalcEditorTotal#3|Each editor line","HoursCalendar.RecalcEditorTotal#4|Add line minutes","HoursCalendar.OnClick_CancelFillButton#2|Close popup","HoursCalendar.data:GetCalendar#5|Set outputs",
    "UserInfo.GetUsername#5|Set username","Login.OnInitialize#2|Not executing","Login.Login#5|Start executing","Menu.data:GetMenuAccess#2|Check admin role",
    "ProjectDetail.OnClick_AddAssignmentButton#2|New assignment","ProjectDetail.OnClick_SaveButton#3|Clear error","ProjectDetail.OnClick_SaveButton#4|Keep error","ProjectDetail.OnClick_SaveButton#7|Show saved","ProjectDetail.OnAfterFetch_GetAssignments#4|Show error","ProjectDetail.OnClick_CancelPopupButton#2|Close popup",
    "ProjectDetail.OnClick_SaveAssignmentButton#3|Clear error","ProjectDetail.OnClick_SaveAssignmentButton#4|Keep error","ProjectDetail.OnClick_SaveAssignmentButton#7|Close popup","ProjectDetail.OnClick_SaveAssignmentButton#9|Show saved",
    "ProjectDetail.OnClick_EditAssignmentLink#2|Edit assignment","ProjectDetail.OnClick_EndAssignmentLink#2|Open end popup",
    "ProjectDetail.OnClick_SaveEndButton#3|Clear error","ProjectDetail.OnClick_SaveEndButton#4|Keep error","ProjectDetail.OnClick_SaveEndButton#7|Close popup","ProjectDetail.OnClick_SaveEndButton#9|Show ended",
    "ProjectDetail.OnClick_DeleteAssignmentLink#4|Offer end instead","ProjectDetail.OnClick_DeleteAssignmentLink#7|Show deleted",
    "ProjectDetail.OnClick_ToggleActiveButton#3|Clear error","ProjectDetail.OnClick_ToggleActiveButton#4|Keep error","ProjectDetail.OnClick_ToggleActiveButton#8|Show status",
    "ProjectDetail.OnAfterFetch_GetProject#2|Copy to form","ProjectDetail.OnAfterFetch_GetProject#5|Show error",
    "ProjectDetail.data:GetUsers#4|Keep error","ProjectDetail.data:GetUsers#5|Set users","ProjectDetail.data:GetAssignments#5|Keep error","ProjectDetail.data:GetAssignments#8|Set assignments",
    "ProjectDetail.data:GetProject#4|New project defaults","ProjectDetail.data:GetProject#5|Keep error","ProjectDetail.data:GetProject#9|Set project","ProjectDetail.data:GetClients#4|Keep error","ProjectDetail.data:GetClients#5|Set clients",
    "ClientDetail.OnClick_ToggleActiveButton#3|Clear error","ClientDetail.OnClick_ToggleActiveButton#4|Keep error","ClientDetail.OnClick_ToggleActiveButton#8|Show status",
    "ClientDetail.OnClick_SaveButton#3|Clear error","ClientDetail.OnClick_SaveButton#4|Keep error","ClientDetail.OnClick_SaveButton#7|Show saved",
    "ClientDetail.OnAfterFetch_GetClient#2|Copy to form","ClientDetail.OnAfterFetch_GetClient#5|Show error",
    "ClientDetail.data:GetClient#4|New client defaults","ClientDetail.data:GetClient#5|Keep error","ClientDetail.data:GetClient#9|Set client and projects",
    "Projects.OnAfterFetch_GetProjects#4|Show error","Projects.data:GetClients#4|Keep error","Projects.data:GetClients#5|Set clients","Projects.data:GetProjects#4|Keep error","Projects.data:GetProjects#5|Set projects",
    "Clients.OnAfterFetch_GetClients#4|Show error","Clients.data:GetClients#4|Keep error","Clients.data:GetClients#5|Set clients",
    "Holidays.OnClick_NextYearButton#2|Next year","Holidays.OnClick_PreviousYearButton#2|Previous year","Holidays.OnClick_AddHolidayButton#2|New holiday","Holidays.OnClick_EditHolidayLink#2|Edit holiday",
    "Holidays.OnClick_SaveHolidayButton#3|Clear error","Holidays.OnClick_SaveHolidayButton#4|Keep error","Holidays.OnClick_SaveHolidayButton#7|Close and show year","Holidays.OnClick_SaveHolidayButton#9|Show saved",
    "Holidays.OnAfterFetch_GetHolidays#4|Show error","Holidays.OnClick_ToggleHolidayLink#3|Clear error","Holidays.OnClick_ToggleHolidayLink#4|Keep error","Holidays.OnClick_ToggleHolidayLink#8|Show status",
    "Holidays.OnInitialize#2|Current year","Holidays.OnClick_CopyYearButton#3|Clear error","Holidays.OnClick_CopyYearButton#4|Keep error","Holidays.OnClick_CopyYearButton#7|Show copied count",
    "Holidays.OnClick_CancelPopupButton#2|Close popup","Holidays.data:GetHolidays#4|Keep error","Holidays.data:GetHolidays#5|Set holidays",
    "MySheet.OnWeekChanged_WeeklyProjectHours#2|Set week date","MySheet.OnInitialize#2|Current month","MySheet.OnMonthChanged_HoursCalendar#2|Set month","MySheet.OnDaySelected_HoursCalendar#2|Set week date","MySheet.OnDaySaved_HoursCalendar#2|Refresh blocks"
  };
  var labels = raw.ToDictionary(r => r.Split('|')[0], r => r.Split('|')[1]);
  var renames = new Dictionary<string, string> { {"HoursCalendar.OnInitialize/ListAppend","AppendHourOption"}, {"HoursCalendar.OnInitialize/ListAppend2","AppendMinuteOption"} };
  var flows = new List<Tuple<string, List<OutSystems.Model.Logic.Nodes.IActionNode>>>();
  foreach (var a in eSpace.ClientActions) flows.Add(Tuple.Create("C:" + a.Name, a.Nodes.ToList()));
  foreach (var f in eSpace.MobileFlows) foreach (var n in f.Nodes) {
    var b = n as IMobileBlock; var sc = n as IMobileScreen; if (b == null && sc == null) continue;
    var owner = b != null ? b.Name : sc.Name;
    foreach (var a in (b != null ? b.ScreenActions : sc.ScreenActions)) flows.Add(Tuple.Create(owner + "." + a.Name, a.Nodes.ToList()));
    foreach (var a in (b != null ? b.DataActions : sc.DataActions)) flows.Add(Tuple.Create(owner + ".data:" + a.Name, a.Nodes.ToList()));
  }
  var used = new HashSet<string>(); var problems = new List<string>();
  foreach (var fl in flows) {
    int i = 0;
    foreach (var n in fl.Item2.OrderBy(x => x.VerticalPosition).ThenBy(x => x.HorizontalPosition).ToList()) {
      i++; string lbl; var key = fl.Item1 + "#" + i;
      if (labels.TryGetValue(key, out lbl)) {
        used.Add(key); string old = null;
        if (n is OutSystems.Model.Logic.Nodes.IAssignNode) { var x = (OutSystems.Model.Logic.Nodes.IAssignNode)n; old = x.Label; x.Label = lbl; }
        else if (n is OutSystems.Model.Logic.Nodes.IForEachNode) { var x = (OutSystems.Model.Logic.Nodes.IForEachNode)n; old = x.Label; x.Label = lbl; }
        else if (n is OutSystems.Model.Logic.Nodes.IMessageNode) { var x = (OutSystems.Model.Logic.Nodes.IMessageNode)n; old = x.Label; x.Label = lbl; }
        else if (n is OutSystems.Model.Logic.Nodes.IIfNode) { var x = (OutSystems.Model.Logic.Nodes.IIfNode)n; old = x.Label; x.Label = lbl; }
        else { problems.Add(key + " is " + n.GetType().Name); continue; }
        if (!string.IsNullOrWhiteSpace(old)) problems.Add(key + " already labelled " + old);
      }
    }
    foreach (var e in fl.Item2.OfType<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>().ToList()) {
      string nn; var rk = fl.Item1 + "/" + e.Name;
      if (renames.TryGetValue(rk, out nn)) { used.Add(rk); e.Name = nn; if (e.Name != nn) problems.Add(rk + " became " + e.Name); }
    }
  }
  foreach (var k in labels.Keys.Concat(renames.Keys)) if (!used.Contains(k)) problems.Add("not found: " + k);
  var mf = eSpace.ClientActions.Single(a => a.Name == "Minutes_Format");
  mf.Folder = eSpace.CreateFolder(OutSystems.Model.Enumerations.ESpaceTreeFolder.ClientActions, "Minutes");
  if (problems.Count > 0) throw new Exception("PROBLEMS:" + string.Join("; ", problems));
}
```
