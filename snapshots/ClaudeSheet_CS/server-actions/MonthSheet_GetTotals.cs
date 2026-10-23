(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_GetTotals' ***/
    var monthSheet_GetTotals = eSpace.CreateServerAction("MonthSheet_GetTotals");
    monthSheet_GetTotals.CacheInMinutes = null; // default value
    monthSheet_GetTotals.Description = "Hours of a user\'s month: TotalMinutes = minutes on the user\'s day sheets; ExpectedMinutes = (weekdays - active holidays on weekdays - days off) * Site.DailyExpectedMinutes, as in Calendar_GetMonthSummary.";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_GetTotals.Folder = monthSheet;
    monthSheet_GetTotals.Function = false; // default value
    monthSheet_GetTotals.Icon = null; // default value
    monthSheet_GetTotals.Public = false; // default value
    monthSheet_GetTotals.Shareable = false; // default value
    
    var userId = monthSheet_GetTotals.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId.DataType = user.IdentifierType;
    userId.Description = null; // default value
    userId.IsMandatory = true; // default value
    
    var year = monthSheet_GetTotals.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = monthSheet_GetTotals.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var firstDay = monthSheet_GetTotals.CreateLocalVariable("FirstDay");
    firstDay.DataType = eSpace.DateType;
    firstDay.SetDefaultValue(null); // default value
    firstDay.Description = null; // default value
    
    var lastDay = monthSheet_GetTotals.CreateLocalVariable("LastDay");
    lastDay.DataType = eSpace.DateType;
    lastDay.SetDefaultValue(null); // default value
    lastDay.Description = null; // default value
    
    var currentDay = monthSheet_GetTotals.CreateLocalVariable("CurrentDay");
    currentDay.DataType = eSpace.DateType;
    currentDay.SetDefaultValue(null); // default value
    currentDay.Description = null; // default value
    
    var weekdays = monthSheet_GetTotals.CreateLocalVariable("Weekdays");
    weekdays.DataType = eSpace.IntegerType;
    weekdays.SetDefaultValue(null); // default value
    weekdays.Description = null; // default value
    
    var startNode = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var assignNode = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Month bounds";
    
    var assignment = assignNode.CreateAssignment("FirstDay", "NewDate(Year, Month, 1)");
    
    var assignment2 = assignNode.CreateAssignment("LastDay", "AddDays(AddMonths(NewDate(Year, Month, 1), 1), -1)");
    
    var assignment3 = assignNode.CreateAssignment("CurrentDay", "NewDate(Year, Month, 1)");
    
    var assignment4 = assignNode.CreateAssignment("Weekdays", "0");
    
    var ifNode = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode.SetCondition("CurrentDay <= LastDay");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "More days?";
    
    var getWeekdayHolidays = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetWeekdayHolidays").Below(ifNode, 1371);
    getWeekdayHolidays.CacheInMinutes = null; // default value
    getWeekdayHolidays.Description = null; // default value
    getWeekdayHolidays.IsDisabled = false; // default value
    getWeekdayHolidays.SetMaxRecords(null); // default value
    getWeekdayHolidays.SetStartIndex(null); // default value
    getWeekdayHolidays.Timeout = null; // default value
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getWeekdayHolidays.AsDatabaseAggregate.CreateSource(holiday2);
    getWeekdayHolidays.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getWeekdayHolidays.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= FirstDay and Holiday.HolidayDate <= LastDay");
    getWeekdayHolidays.AsDatabaseAggregate.CreateFilter("DayOfWeek(Holiday.HolidayDate) <> 0 and DayOfWeek(Holiday.HolidayDate) <> 6");
    
    ifNode.FalseTarget = getWeekdayHolidays;
    
    var assignNode2 = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Count weekday";
    assignNode2.Target = ifNode;
    
    var assignment5 = assignNode2.CreateAssignment("Weekdays", "Weekdays + If(Mod(DayOfWeek(CurrentDay) + 6, 7) < 5, 1, 0)");
    
    var assignment6 = assignNode2.CreateAssignment("CurrentDay", "AddDays(CurrentDay, 1)");
    ifNode.TrueTarget = assignNode2;
    
    var getDaysOff = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDaysOff").ConnectedBelow(getWeekdayHolidays, 1371);
    getDaysOff.CacheInMinutes = null; // default value
    getDaysOff.Description = null; // default value
    getDaysOff.IsDisabled = false; // default value
    getDaysOff.SetMaxRecords(null); // default value
    getDaysOff.SetStartIndex(null); // default value
    getDaysOff.Timeout = null; // default value
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getDaysOff.AsDatabaseAggregate.CreateSource(daySheet2);
    getDaysOff.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = UserId");
    getDaysOff.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= FirstDay and DaySheet.SheetDate <= LastDay");
    getDaysOff.AsDatabaseAggregate.CreateFilter("DaySheet.DayStatusId = Entities.DayStatus.DayOff");
    
    
    var getMonthMinutes = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthMinutes").ConnectedBelow(getDaysOff, 1371);
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
    getMonthMinutes.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = UserId");
    getMonthMinutes.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= FirstDay and DaySheet.SheetDate <= LastDay");
    getMonthMinutes.AsDatabaseAggregate.CreateGroupByAttribute("UserId").SetAttribute("DaySheet.UserId");
    var totalMinutes = getMonthMinutes.AsDatabaseAggregate.CreateAggregatedAttribute("TotalMinutes");
    totalMinutes.SetAttribute("TimeEntry.Minutes");
    totalMinutes.AggregationType = AggregationType.Sum;
    
    
    var assignNode3 = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getMonthMinutes, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Set totals";
    
    var assignment7 = assignNode3.CreateAssignment("TotalMinutes", "If(GetMonthMinutes.List.Empty, 0, LongIntegerToInteger(GetMonthMinutes.List.Current.TotalMinutes))");
    
    var assignment8 = assignNode3.CreateAssignment("ExpectedMinutes", "Max(0, Weekdays - LongIntegerToInteger(GetWeekdayHolidays.Count) - LongIntegerToInteger(GetDaysOff.Count)) * Site.DailyExpectedMinutes");
    
    var endNode = monthSheet_GetTotals.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode.IsDisabled = false; // default value
    
    var totalMinutes2 = monthSheet_GetTotals.CreateOutputParameter("TotalMinutes");
    totalMinutes2.DataType = eSpace.IntegerType;
    totalMinutes2.SetDefaultValue(null); // default value
    totalMinutes2.Description = null; // default value
    
    var expectedMinutes = monthSheet_GetTotals.CreateOutputParameter("ExpectedMinutes");
    expectedMinutes.DataType = eSpace.IntegerType;
    expectedMinutes.SetDefaultValue(null); // default value
    expectedMinutes.Description = null; // default value
}
