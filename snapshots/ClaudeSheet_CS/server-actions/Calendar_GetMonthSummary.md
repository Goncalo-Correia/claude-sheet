# Calendar_GetMonthSummary

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Calendar_GetMonthSummary' ***/
    var calendar_GetMonthSummary = eSpace.CreateServerAction("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.Description = "Month totals and KPI counts for the current user, derived from GetMonthCalendar. TotalMinutes = minutes on Draft/Confirmed day sheets. ExpectedMinutes = (weekdays - holidays - days off of the whole month) * Site.DailyExpectedMinutes. ProgressPercent = Total/Expected*100 capped at 100 (0 when Expected = 0). ElapsedDays = calendar days elapsed in the month up to today (past month = days in month, future month = 0).";
    var calendar = eSpace.Folders.Named("Calendar");
    calendar_GetMonthSummary.Folder = calendar;
    calendar_GetMonthSummary.Public = true;
    calendar_GetMonthSummary.Shareable = true;
    
    var year = calendar_GetMonthSummary.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    
    var month = calendar_GetMonthSummary.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    
    var today = calendar_GetMonthSummary.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    
    var expectedDays = calendar_GetMonthSummary.CreateLocalVariable("ExpectedDays");
    expectedDays.DataType = eSpace.IntegerType;
    
    var startNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var calendar_GetMonth = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").Below(ifNode, 1371);
    var calendar_GetMonth2 = eSpace.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    var year2 = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year2, "Year");
    var month2 = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month2, "Month");
    ifNode.TrueTarget = calendar_GetMonth;
    
    var assignNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonth, 1371);
    assignNode.Label = "Init summary";
    
    var assignment = assignNode.CreateAssignment("Summary.Year", "Year");
    
    var assignment2 = assignNode.CreateAssignment("Summary.Month", "Month");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var forEachNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode, 1371);
    forEachNode.Label = "Each day";
    forEachNode.SetRecordList("Calendar_GetMonth.Days");
    
    var assignNode2 = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode2.Label = "Add day to totals";
    assignNode2.Target = forEachNode;
    
    var assignment4 = assignNode2.CreateAssignment("Summary.TotalMinutes", "Summary.TotalMinutes + If(Calendar_GetMonth.Days.Current.DayStatusId = Entities.DayStatus.DayOff, 0, Calendar_GetMonth.Days.Current.TotalMinutes)");
    
    var assignment5 = assignNode2.CreateAssignment("Summary.ConfirmedCount", "Summary.ConfirmedCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, 1, 0)");
    
    var assignment6 = assignNode2.CreateAssignment("Summary.DayOffCount", "Summary.DayOffCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, 1, 0)");
    
    var assignment7 = assignNode2.CreateAssignment("Summary.HolidayCount", "Summary.HolidayCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, 1, 0)");
    
    var assignment8 = assignNode2.CreateAssignment("Summary.PendingCount", "Summary.PendingCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Pending, 1, 0)");
    
    var assignment9 = assignNode2.CreateAssignment("ExpectedDays", "ExpectedDays + If(not Calendar_GetMonth.Days.Current.IsWeekend and Calendar_GetMonth.Days.Current.DayTypeId <> Entities.CalendarDayType.Holiday and Calendar_GetMonth.Days.Current.DayTypeId <> Entities.CalendarDayType.DayOff, 1, 0)");
    forEachNode.CycleTarget = assignNode2;
    
    var assignNode3 = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(forEachNode, 1371);
    assignNode3.Label = "Expected and progress";
    
    var assignment10 = assignNode3.CreateAssignment("Summary.ElapsedDays", "If(Year * 100 + Month < Year(Today) * 100 + Month(Today), Calendar_GetMonth.Days.Length, If(Year = Year(Today) and Month = Month(Today), Day(Today), 0))");
    
    var assignment11 = assignNode3.CreateAssignment("Summary.ExpectedMinutes", "ExpectedDays * Site.DailyExpectedMinutes");
    
    var assignment12 = assignNode3.CreateAssignment("Summary.ProgressPercent", "If(Summary.ExpectedMinutes > 0, Min(100, Summary.TotalMinutes * 100 / Summary.ExpectedMinutes), 0)");
    forEachNode.Target = assignNode3;
    
    var endNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    
    var summary = calendar_GetMonthSummary.CreateOutputParameter("Summary");
    var monthSummary = eSpace.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
}
```
