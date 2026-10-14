(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'GetMonthSummary' ***/
    var getMonthSummary = eSpace.CreateServerAction("GetMonthSummary");
    getMonthSummary.CacheInMinutes = null; // default value
    getMonthSummary.Description = "Month totals and KPI counts for the current user, derived from GetMonthCalendar. ElapsedDays = Confirmed or Pending days up to today; ExpectedMinutes = ElapsedDays * Site.DailyExpectedMinutes.";
    getMonthSummary.Folder = null; // default value
    getMonthSummary.Function = false; // default value
    getMonthSummary.Icon = null; // default value
    getMonthSummary.Public = false; // default value
    getMonthSummary.Shareable = false; // default value
    
    var year = getMonthSummary.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = getMonthSummary.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var today = getMonthSummary.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    today.SetDefaultValue(null); // default value
    today.Description = null; // default value
    
    var startNode = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>();
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.HorizontalPosition = 5028;
    raiseExceptionNode.IsDisabled = false; // default value
    raiseExceptionNode.VerticalPosition = 3542;
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getMonthCalendar = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthCalendar").Below(ifNode, 1371);
    var getMonthCalendar2 = eSpace.ServerActions.Named("GetMonthCalendar");
    getMonthCalendar.Action = getMonthCalendar2;
    getMonthCalendar.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getMonthCalendar.IsDisabled = false; // default value
    getMonthCalendar.ServerRequestTimeout = null; // default value
    var month2 = getMonthCalendar2.InputParameters.Named("Month");
    getMonthCalendar.SetArgumentValue(month2, "Month");
    var year2 = getMonthCalendar2.InputParameters.Named("Year");
    getMonthCalendar.SetArgumentValue(year2, "Year");
    ifNode.TrueTarget = getMonthCalendar;
    
    var assignNode = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getMonthCalendar, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Summary.Year", "Year");
    
    var assignment2 = assignNode.CreateAssignment("Summary.Month", "Month");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var forEachNode = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetMonthCalendar.Days");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode2 = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode;
    
    var assignment4 = assignNode2.CreateAssignment("Summary.TotalMinutes", "Summary.TotalMinutes + GetMonthCalendar.Days.Current.TotalMinutes");
    
    var assignment5 = assignNode2.CreateAssignment("Summary.ConfirmedCount", "Summary.ConfirmedCount + If(GetMonthCalendar.Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, 1, 0)");
    
    var assignment6 = assignNode2.CreateAssignment("Summary.DayOffCount", "Summary.DayOffCount + If(GetMonthCalendar.Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, 1, 0)");
    
    var assignment7 = assignNode2.CreateAssignment("Summary.HolidayCount", "Summary.HolidayCount + If(GetMonthCalendar.Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, 1, 0)");
    
    var assignment8 = assignNode2.CreateAssignment("Summary.PendingCount", "Summary.PendingCount + If(GetMonthCalendar.Days.Current.DayTypeId = Entities.CalendarDayType.Pending, 1, 0)");
    
    var assignment9 = assignNode2.CreateAssignment("Summary.ElapsedDays", "Summary.ElapsedDays + If(GetMonthCalendar.Days.Current.Date <= Today and (GetMonthCalendar.Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed or GetMonthCalendar.Days.Current.DayTypeId = Entities.CalendarDayType.Pending), 1, 0)");
    forEachNode.CycleTarget = assignNode2;
    
    var assignNode3 = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(forEachNode, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment10 = assignNode3.CreateAssignment("Summary.ExpectedMinutes", "Summary.ElapsedDays * Site.DailyExpectedMinutes");
    
    var assignment11 = assignNode3.CreateAssignment("Summary.ProgressPercent", "If(Summary.ExpectedMinutes > 0, Summary.TotalMinutes * 100 / Summary.ExpectedMinutes, 0)");
    forEachNode.Target = assignNode3;
    
    var endNode = getMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode.IsDisabled = false; // default value
    
    var summary = getMonthSummary.CreateOutputParameter("Summary");
    var monthSummary = eSpace.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    summary.Description = null; // default value
}
