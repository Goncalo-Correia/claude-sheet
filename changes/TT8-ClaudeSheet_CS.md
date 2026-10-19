# TT8 · ClaudeSheet_CS changes

Every `applyModelApiCode` save for TT8 in ClaudeSheet_CS, oldest first. Logged when the module was published.

## Save 1 · 2026-10-19 16:22:58 UTC

- **Published:** 2026-10-19 16:28:23 UTC (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  var labels = new Dictionary<string, string> {
    {"Bootstrap#4","Use existing client"},{"Bootstrap#5","New ICNF client"},{"Bootstrap#7","Keep new client Id"},{"Bootstrap#10","Use existing project"},{"Bootstrap#11","New RUBUS project"},{"Bootstrap#13","Keep new project Id"},{"Bootstrap#27","Test user list"},{"Bootstrap#29","Each test user"},{"Bootstrap#34","New RUBUS assignment"},{"Bootstrap#38","Each admin user"},
    {"Calendar_GetMonth#4","Month bounds"},{"Calendar_GetMonth#6","Build day"},{"Calendar_GetMonth#8","Next day"},{"Calendar_GetMonth#10","Each holiday"},{"Calendar_GetMonth#11","Mark holiday"},{"Calendar_GetMonth#13","Each day sheet"},{"Calendar_GetMonth#14","Copy day sheet"},{"Calendar_GetMonth#16","Each day total"},{"Calendar_GetMonth#17","Set day minutes"},{"Calendar_GetMonth#18","Each day"},{"Calendar_GetMonth#19","Classify day"},
    {"Calendar_GetMonthSummary#5","Init summary"},{"Calendar_GetMonthSummary#6","Each day"},{"Calendar_GetMonthSummary#7","Add day to totals"},{"Calendar_GetMonthSummary#8","Expected and progress"},
    {"Client_CreateOrUpdate#9","Trim name"},{"Client_CreateOrUpdate#11","Return Id"},{"Client_GetById#8","Set outputs"},{"Client_GetList#5","Set output"},{"Client_SetActive#7","Set IsActive"},
    {"Date_GetWeekStart#2","Monday of week"},
    {"DaySheet_FillPending#5","Init entry"},{"DaySheet_FillPending#7","Each day"},{"DaySheet_FillPending#12","Count filled"},{"DaySheet_FillPending#13","Confirm draft"},{"DaySheet_FillPending#15","Count filled"},
    {"DaySheet_GetDetail#8","Fill header"},{"DaySheet_GetDetail#9","Each line"},{"DaySheet_GetDetail#10","Each entry"},{"DaySheet_GetDetail#12","Merge entry"},
    {"DaySheet_Save#10","Each entry"},{"DaySheet_Save#13","Add to total"},{"DaySheet_Save#15","Each assigned project"},{"DaySheet_Save#17","Mark assigned"},{"DaySheet_Save#28","Use existing sheet"},{"DaySheet_Save#29","New sheet"},{"DaySheet_Save#30","Set status and notes"},{"DaySheet_Save#32","Keep sheet Id"},{"DaySheet_Save#34","Each saved entry"},{"DaySheet_Save#35","Reset found"},{"DaySheet_Save#36","Each entry"},{"DaySheet_Save#38","Update entry"},{"DaySheet_Save#44","Each entry"},{"DaySheet_Save#46","Reset found"},{"DaySheet_Save#47","Each saved entry"},{"DaySheet_Save#49","Mark found"},{"DaySheet_Save#51","New entry"},
    {"Holiday_CopyYear#7","Each fixed holiday"},{"Holiday_CopyYear#9","Count copied"},{"Holiday_CreateIfMissing#6","New holiday"},{"Holiday_CreateIfMissing#7","Mark movable"},{"Holiday_CreateOrUpdate#11","Trim name"},{"Holiday_CreateOrUpdate#13","Return Id"},{"Holiday_GetList#5","Set output"},{"Holiday_SetActive#7","Set IsActive"},
    {"Project_CreateOrUpdate#16","Trim name and code"},{"Project_CreateOrUpdate#18","Return Id"},{"Project_GetAssignedToUser#5","Each project"},{"Project_GetAssignedToUser#6","Build line"},{"Project_GetById#7","Set output"},{"Project_GetList#5","Set output"},{"Project_SetActive#7","Set IsActive"},
    {"ProjectAssignment_CreateOrUpdate#18","Mark active"},{"ProjectAssignment_CreateOrUpdate#20","Return Id"},{"ProjectAssignment_End#11","Set end date"},{"ProjectAssignment_GetList#7","Set output"},
    {"ProjectHours_GetWeekly#7","Start at Monday"},{"ProjectHours_GetWeekly#12","Next day"},{"ProjectHours_GetWeekly#15","Each project"},{"ProjectHours_GetWeekly#20","Each entry"},{"ProjectHours_GetWeekly#22","Add minutes"},{"ProjectHours_GetWeekly#25","Next day"},
    {"Settings_GetDailyExpectedMinutes#4","Read site property"},{"User_GetActive#5","Set output"}
  };
  var renames = new Dictionary<string, string> {
    {"Bootstrap/Holiday_CreateIfMissing","CreateHoliday_NewYear"},{"Bootstrap/Holiday_CreateIfMissing2","CreateHoliday_GoodFriday"},{"Bootstrap/Holiday_CreateIfMissing3","CreateHoliday_Easter"},{"Bootstrap/Holiday_CreateIfMissing4","CreateHoliday_FreedomDay"},{"Bootstrap/Holiday_CreateIfMissing5","CreateHoliday_LabourDay"},{"Bootstrap/Holiday_CreateIfMissing6","CreateHoliday_CorpusChristi"},{"Bootstrap/Holiday_CreateIfMissing7","CreateHoliday_PortugalDay"},{"Bootstrap/Holiday_CreateIfMissing8","CreateHoliday_Assumption"},{"Bootstrap/Holiday_CreateIfMissing9","CreateHoliday_RepublicDay"},{"Bootstrap/Holiday_CreateIfMissing10","CreateHoliday_AllSaints"},{"Bootstrap/Holiday_CreateIfMissing11","CreateHoliday_IndependenceDay"},{"Bootstrap/Holiday_CreateIfMissing12","CreateHoliday_ImmaculateConception"},{"Bootstrap/Holiday_CreateIfMissing13","CreateHoliday_Christmas"},
    {"Bootstrap/GrantSheetUserRole2","GrantSheetUserRole_Admin"},{"ProjectHours_GetWeekly/ListAppend2","AppendDayTotal"},{"ProjectHours_GetWeekly/ListAppend3","AppendCell"},{"ProjectHours_GetWeekly/ListAppend4","AppendRow"}
  };
  var used = new HashSet<string>(); var problems = new List<string>();
  foreach (var a in eSpace.ServerActions) {
    int i = 0;
    foreach (var n in a.Nodes.OrderBy(x => x.VerticalPosition).ThenBy(x => x.HorizontalPosition).ToList()) {
      i++;
      string lbl; var key = a.Name + "#" + i;
      if (labels.TryGetValue(key, out lbl)) {
        used.Add(key);
        if (n is OutSystems.Model.Logic.Nodes.IAssignNode) { var x = (OutSystems.Model.Logic.Nodes.IAssignNode)n; if (!string.IsNullOrWhiteSpace(x.Label)) problems.Add(key + " already labelled"); x.Label = lbl; }
        else if (n is OutSystems.Model.Logic.Nodes.IForEachNode) { var x = (OutSystems.Model.Logic.Nodes.IForEachNode)n; if (!string.IsNullOrWhiteSpace(x.Label)) problems.Add(key + " already labelled"); x.Label = lbl; }
        else problems.Add(key + " is " + n.GetType().Name);
      }
    }
    foreach (var e in a.Nodes.OfType<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>().ToList()) {
      string nn; var rk = a.Name + "/" + e.Name;
      if (renames.TryGetValue(rk, out nn)) { used.Add(rk); e.Name = nn; if (e.Name != nn) problems.Add(rk + " became " + e.Name); }
    }
  }
  foreach (var k in labels.Keys.Concat(renames.Keys)) if (!used.Contains(k)) problems.Add("not found: " + k);
  var folders = new Dictionary<string, OutSystems.Model.IFolder>();
  foreach (var a in eSpace.ServerActions.ToList()) {
    var fname = a.Name == "Bootstrap" ? "Setup" : a.Name.Split('_')[0];
    OutSystems.Model.IFolder f;
    if (!folders.TryGetValue(fname, out f)) { f = eSpace.CreateFolder(OutSystems.Model.Enumerations.ESpaceTreeFolder.ServerActions, fname); folders[fname] = f; }
    a.Folder = f;
  }
  if (problems.Count > 0) throw new Exception("PROBLEMS:" + string.Join("; ", problems));
}
```
