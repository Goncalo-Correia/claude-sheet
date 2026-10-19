# Calendar_GetMonth

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Calendar_GetMonth' ***/
    var calendar_GetMonth = eSpace.CreateServerAction("Calendar_GetMonth");
    calendar_GetMonth.Description = "One CalendarDay per day of the month for the current user, classified (first match wins) as Holiday, DayOff, Confirmed, Weekend, Future (after today, server date) or Pending. IsEditable = weekday that is not a holiday.";
    var calendar = eSpace.Folders.Named("Calendar");
    calendar_GetMonth.Folder = calendar;
    calendar_GetMonth.Public = true;
    calendar_GetMonth.Shareable = true;
    
    var year = calendar_GetMonth.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    
    var month = calendar_GetMonth.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    
    var firstDay = calendar_GetMonth.CreateLocalVariable("FirstDay");
    firstDay.DataType = eSpace.DateType;
    
    var lastDay = calendar_GetMonth.CreateLocalVariable("LastDay");
    lastDay.DataType = eSpace.DateType;
    
    var today = calendar_GetMonth.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    
    var dayIndex = calendar_GetMonth.CreateLocalVariable("DayIndex");
    dayIndex.DataType = eSpace.IntegerType;
    
    var calDay = calendar_GetMonth.CreateLocalVariable("CalDay");
    var calendarDay = eSpace.Structures.Named("CalendarDay");
    calDay.DataType = calendarDay;
    
    var startNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.Label = "Month bounds";
    
    var assignment = assignNode.CreateAssignment("FirstDay", "NewDate(Year, Month, 1)");
    
    var assignment2 = assignNode.CreateAssignment("LastDay", "DateTimeToDate(AddDays(AddMonths(FirstDay, 1), -1))");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var assignment4 = assignNode.CreateAssignment("DayIndex", "1");
    ifNode.TrueTarget = assignNode;
    
    var ifNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode2.SetCondition("DayIndex <= Day(LastDay)");
    ifNode2.Label = "More days?";
    
    var getMonthHolidays = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthHolidays").Below(ifNode2, 4113);
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getMonthHolidays.AsDatabaseAggregate.CreateSource(holiday2);
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= FirstDay and Holiday.HolidayDate <= LastDay");
    
    ifNode2.FalseTarget = getMonthHolidays;
    
    var assignNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode2.Label = "Build day";
    
    var assignment5 = assignNode2.CreateAssignment("CalDay.Date", "DateTimeToDate(AddDays(FirstDay, DayIndex - 1))");
    
    var assignment6 = assignNode2.CreateAssignment("CalDay.DayNumber", "DayIndex");
    
    var assignment7 = assignNode2.CreateAssignment("CalDay.WeekDay", "DecimalToInteger(Mod(DayOfWeek(CalDay.Date) + 6, 7)) + 1");
    
    var assignment8 = assignNode2.CreateAssignment("CalDay.IsWeekend", "CalDay.WeekDay >= 6");
    
    var assignment9 = assignNode2.CreateAssignment("CalDay.IsToday", "CalDay.Date = Today");
    ifNode2.TrueTarget = assignNode2;
    
    var forEachNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthHolidays, 1371);
    forEachNode.Label = "Each holiday";
    forEachNode.SetRecordList("GetMonthHolidays.List");
    
    var listAppend = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode2, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Days");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "CalDay");
    
    var assignNode3 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode3.Label = "Mark holiday";
    assignNode3.Target = forEachNode;
    
    var assignment10 = assignNode3.CreateAssignment("Days[Day(GetMonthHolidays.List.Current.Holiday.HolidayDate) - 1].HolidayName", "GetMonthHolidays.List.Current.Holiday.Name");
    forEachNode.CycleTarget = assignNode3;
    
    var getMonthDaySheets = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthDaySheets").Below(forEachNode, 1371);
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getMonthDaySheets.AsDatabaseAggregate.CreateSource(daySheet2);
    getMonthDaySheets.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getMonthDaySheets.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= FirstDay and DaySheet.SheetDate <= LastDay");
    
    forEachNode.Target = getMonthDaySheets;
    
    var assignNode4 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend, 1371);
    assignNode4.Label = "Next day";
    assignNode4.Target = ifNode2;
    
    var assignment11 = assignNode4.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var forEachNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthDaySheets, 1371);
    forEachNode2.Label = "Each day sheet";
    forEachNode2.SetRecordList("GetMonthDaySheets.List");
    
    var assignNode5 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode2, 1828);
    assignNode5.Label = "Copy day sheet";
    assignNode5.Target = forEachNode2;
    
    var assignment12 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DaySheetId", "GetMonthDaySheets.List.Current.DaySheet.Id");
    
    var assignment13 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DayStatusId", "GetMonthDaySheets.List.Current.DaySheet.DayStatusId");
    forEachNode2.CycleTarget = assignNode5;
    
    var getMonthMinutes = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthMinutes").Below(forEachNode2, 1371);
    var timeEntry2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var timeEntry = getMonthMinutes.AsDatabaseAggregate.CreateSource(timeEntry2);
    var daySheet3 = getMonthMinutes.AsDatabaseAggregate.CreateSource(daySheet2);
    var join = getMonthMinutes.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet3;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    getMonthMinutes.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getMonthMinutes.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= FirstDay and DaySheet.SheetDate <= LastDay");
    getMonthMinutes.AsDatabaseAggregate.CreateGroupByAttribute("SheetDate").SetAttribute("DaySheet.SheetDate");
    var totalMinutes = getMonthMinutes.AsDatabaseAggregate.CreateAggregatedAttribute("TotalMinutes");
    totalMinutes.SetAttribute("TimeEntry.Minutes");
    totalMinutes.AggregationType = AggregationType.Sum;
    
    forEachNode2.Target = getMonthMinutes;
    
    var forEachNode3 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthMinutes, 1371);
    forEachNode3.Label = "Each day total";
    forEachNode3.SetRecordList("GetMonthMinutes.List");
    
    var assignNode6 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode6.Label = "Set day minutes";
    assignNode6.Target = forEachNode3;
    
    var assignment14 = assignNode6.CreateAssignment("Days[Day(GetMonthMinutes.List.Current.SheetDate) - 1].TotalMinutes", "LongIntegerToInteger(GetMonthMinutes.List.Current.TotalMinutes)");
    forEachNode3.CycleTarget = assignNode6;
    
    var forEachNode4 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(forEachNode3, 1371);
    forEachNode4.Label = "Each day";
    forEachNode4.SetRecordList("Days");
    forEachNode3.Target = forEachNode4;
    
    var assignNode7 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode4, 1828);
    assignNode7.Label = "Classify day";
    assignNode7.Target = forEachNode4;
    
    var assignment15 = assignNode7.CreateAssignment("Days.Current.DayTypeId", "If(Days.Current.HolidayName <> \"\", Entities.CalendarDayType.Holiday, If(Days.Current.DayStatusId = Entities.DayStatus.DayOff, Entities.CalendarDayType.DayOff, If(Days.Current.DayStatusId = Entities.DayStatus.Confirmed, Entities.CalendarDayType.Confirmed, If(Days.Current.IsWeekend, Entities.CalendarDayType.Weekend, If(Days.Current.Date > Today, Entities.CalendarDayType.Future, Entities.CalendarDayType.Pending)))))");
    
    var assignment16 = assignNode7.CreateAssignment("Days.Current.CssClass", "If(Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"day-holiday\", If(Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"day-off\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"day-confirmed\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"day-weekend\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Future, \"day-future\", \"day-pending\")))))");
    
    var assignment17 = assignNode7.CreateAssignment("Days.Current.IsEditable", "not Days.Current.IsWeekend and Days.Current.HolidayName = \"\"");
    forEachNode4.CycleTarget = assignNode7;
    
    var endNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode4, 1371);
    forEachNode4.Target = endNode;
    
    var days = calendar_GetMonth.CreateOutputParameter("Days");
    days.DataType = eSpace.GetOrCreateListType(calendarDay);
}
```
