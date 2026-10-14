(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'GetMonthCalendar' ***/
    var getMonthCalendar = eSpace.CreateServerAction("GetMonthCalendar");
    getMonthCalendar.CacheInMinutes = null; // default value
    getMonthCalendar.Description = "One CalendarDay per day of the month for the current user, classified (first match wins) as Holiday, DayOff, Confirmed, Weekend, Future (after today, server date) or Pending. IsEditable = weekday that is not a holiday.";
    getMonthCalendar.Folder = null; // default value
    getMonthCalendar.Function = false; // default value
    getMonthCalendar.Icon = null; // default value
    getMonthCalendar.Public = false; // default value
    getMonthCalendar.Shareable = false; // default value
    
    var year = getMonthCalendar.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = getMonthCalendar.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var firstDay = getMonthCalendar.CreateLocalVariable("FirstDay");
    firstDay.DataType = eSpace.DateType;
    firstDay.SetDefaultValue(null); // default value
    firstDay.Description = null; // default value
    
    var lastDay = getMonthCalendar.CreateLocalVariable("LastDay");
    lastDay.DataType = eSpace.DateType;
    lastDay.SetDefaultValue(null); // default value
    lastDay.Description = null; // default value
    
    var today = getMonthCalendar.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    today.SetDefaultValue(null); // default value
    today.Description = null; // default value
    
    var dayIndex = getMonthCalendar.CreateLocalVariable("DayIndex");
    dayIndex.DataType = eSpace.IntegerType;
    dayIndex.SetDefaultValue(null); // default value
    dayIndex.Description = null; // default value
    
    var calDay = getMonthCalendar.CreateLocalVariable("CalDay");
    var calendarDay = eSpace.Structures.Named("CalendarDay");
    calDay.DataType = calendarDay;
    calDay.Description = null; // default value
    
    var startNode = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>();
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.HorizontalPosition = 5028;
    raiseExceptionNode.IsDisabled = false; // default value
    raiseExceptionNode.VerticalPosition = 3542;
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("FirstDay", "NewDate(Year, Month, 1)");
    
    var assignment2 = assignNode.CreateAssignment("LastDay", "DateTimeToDate(AddDays(AddMonths(FirstDay, 1), -1))");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var assignment4 = assignNode.CreateAssignment("DayIndex", "1");
    ifNode.TrueTarget = assignNode;
    
    var ifNode2 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode2.SetCondition("DayIndex <= Day(LastDay)");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "More days?";
    
    var getMonthHolidays = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthHolidays");
    getMonthHolidays.CacheInMinutes = null; // default value
    getMonthHolidays.Description = null; // default value
    getMonthHolidays.HorizontalPosition = 5028;
    getMonthHolidays.IsDisabled = false; // default value
    getMonthHolidays.SetMaxRecords(null); // default value
    getMonthHolidays.SetStartIndex(null); // default value
    getMonthHolidays.Timeout = null; // default value
    getMonthHolidays.VerticalPosition = 6284;
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getMonthHolidays.AsDatabaseAggregate.CreateSource(holiday2);
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= FirstDay and Holiday.HolidayDate <= LastDay");
    
    ifNode2.FalseTarget = getMonthHolidays;
    
    var assignNode2 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment5 = assignNode2.CreateAssignment("CalDay.Date", "DateTimeToDate(AddDays(FirstDay, DayIndex - 1))");
    
    var assignment6 = assignNode2.CreateAssignment("CalDay.DayNumber", "DayIndex");
    
    var assignment7 = assignNode2.CreateAssignment("CalDay.WeekDay", "DecimalToInteger(Mod(DayOfWeek(CalDay.Date) + 6, 7)) + 1");
    
    var assignment8 = assignNode2.CreateAssignment("CalDay.IsWeekend", "CalDay.WeekDay >= 6");
    
    var assignment9 = assignNode2.CreateAssignment("CalDay.IsToday", "CalDay.Date = Today");
    ifNode2.TrueTarget = assignNode2;
    
    var forEachNode = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthHolidays, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetMonthHolidays.List");
    forEachNode.SetStartIndex(null); // default value
    
    var listAppend = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode2, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Days");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "CalDay");
    
    var assignNode3 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.Target = forEachNode;
    
    var assignment10 = assignNode3.CreateAssignment("Days[Day(GetMonthHolidays.List.Current.Holiday.HolidayDate) - 1].HolidayName", "GetMonthHolidays.List.Current.Holiday.Name");
    forEachNode.CycleTarget = assignNode3;
    
    var getMonthDaySheets = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthDaySheets").Below(forEachNode, 1371);
    getMonthDaySheets.CacheInMinutes = null; // default value
    getMonthDaySheets.Description = null; // default value
    getMonthDaySheets.IsDisabled = false; // default value
    getMonthDaySheets.SetMaxRecords(null); // default value
    getMonthDaySheets.SetStartIndex(null); // default value
    getMonthDaySheets.Timeout = null; // default value
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getMonthDaySheets.AsDatabaseAggregate.CreateSource(daySheet2);
    getMonthDaySheets.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getMonthDaySheets.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= FirstDay and DaySheet.SheetDate <= LastDay");
    
    forEachNode.Target = getMonthDaySheets;
    
    var assignNode4 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.Target = ifNode2;
    
    var assignment11 = assignNode4.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var forEachNode2 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthDaySheets, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("GetMonthDaySheets.List");
    forEachNode2.SetStartIndex(null); // default value
    
    var assignNode5 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode2, 1828);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    assignNode5.Target = forEachNode2;
    
    var assignment12 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DaySheetId", "GetMonthDaySheets.List.Current.DaySheet.Id");
    
    var assignment13 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DayStatusId", "GetMonthDaySheets.List.Current.DaySheet.DayStatusId");
    forEachNode2.CycleTarget = assignNode5;
    
    var getMonthMinutes = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthMinutes").Below(forEachNode2, 1371);
    getMonthMinutes.CacheInMinutes = null; // default value
    getMonthMinutes.Description = null; // default value
    getMonthMinutes.IsDisabled = false; // default value
    getMonthMinutes.SetMaxRecords(null); // default value
    getMonthMinutes.SetStartIndex(null); // default value
    getMonthMinutes.Timeout = null; // default value
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
    
    var forEachNode3 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthMinutes, 1371);
    forEachNode3.IsDisabled = false; // default value
    forEachNode3.Label = null; // default value
    forEachNode3.SetMaximumIterations(null); // default value
    forEachNode3.SetRecordList("GetMonthMinutes.List");
    forEachNode3.SetStartIndex(null); // default value
    
    var assignNode6 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.Target = forEachNode3;
    
    var assignment14 = assignNode6.CreateAssignment("Days[Day(GetMonthMinutes.List.Current.SheetDate) - 1].TotalMinutes", "LongIntegerToInteger(GetMonthMinutes.List.Current.TotalMinutes)");
    forEachNode3.CycleTarget = assignNode6;
    
    var forEachNode4 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(forEachNode3, 1371);
    forEachNode4.IsDisabled = false; // default value
    forEachNode4.Label = null; // default value
    forEachNode4.SetMaximumIterations(null); // default value
    forEachNode4.SetRecordList("Days");
    forEachNode4.SetStartIndex(null); // default value
    forEachNode3.Target = forEachNode4;
    
    var assignNode7 = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode4, 1828);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    assignNode7.Target = forEachNode4;
    
    var assignment15 = assignNode7.CreateAssignment("Days.Current.DayTypeId", "If(Days.Current.HolidayName <> \"\", Entities.CalendarDayType.Holiday, If(Days.Current.DayStatusId = Entities.DayStatus.DayOff, Entities.CalendarDayType.DayOff, If(Days.Current.DayStatusId = Entities.DayStatus.Confirmed, Entities.CalendarDayType.Confirmed, If(Days.Current.IsWeekend, Entities.CalendarDayType.Weekend, If(Days.Current.Date > Today, Entities.CalendarDayType.Future, Entities.CalendarDayType.Pending)))))");
    
    var assignment16 = assignNode7.CreateAssignment("Days.Current.CssClass", "If(Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"day-holiday\", If(Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"day-off\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"day-confirmed\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"day-weekend\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Future, \"day-future\", \"day-pending\")))))");
    
    var assignment17 = assignNode7.CreateAssignment("Days.Current.IsEditable", "not Days.Current.IsWeekend and Days.Current.HolidayName = \"\"");
    forEachNode4.CycleTarget = assignNode7;
    
    var endNode = getMonthCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode4, 1371);
    endNode.IsDisabled = false; // default value
    forEachNode4.Target = endNode;
    
    var days = getMonthCalendar.CreateOutputParameter("Days");
    days.DataType = eSpace.GetOrCreateListType(calendarDay);
    days.Description = null; // default value
}
