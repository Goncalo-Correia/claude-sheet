(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectHours_GetWeekly' ***/
    var projectHours_GetWeekly = eSpace.CreateServerAction("ProjectHours_GetWeekly");
    projectHours_GetWeekly.CacheInMinutes = null; // default value
    projectHours_GetWeekly.Description = "Weekly grid for the current user: the Monday-Sunday week containing ReferenceDate, day headers classified like the calendar (Calendar_GetMonth), one row per project assigned during the week or with entries in it (sorted by client, project), 7 cells per row with the \"-\" rule, totals per row, per day and for the week.";
    projectHours_GetWeekly.Folder = null; // default value
    projectHours_GetWeekly.Function = false; // default value
    projectHours_GetWeekly.Icon = null; // default value
    projectHours_GetWeekly.Public = true;
    projectHours_GetWeekly.Shareable = true;
    
    var referenceDate = projectHours_GetWeekly.CreateInputParameter("ReferenceDate");
    referenceDate.DataType = eSpace.DateType;
    referenceDate.Description = null; // default value
    referenceDate.IsMandatory = true; // default value
    
    var dayIndex = projectHours_GetWeekly.CreateLocalVariable("DayIndex");
    dayIndex.DataType = eSpace.IntegerType;
    dayIndex.SetDefaultValue(null); // default value
    dayIndex.Description = null; // default value
    
    var dayHeader = projectHours_GetWeekly.CreateLocalVariable("DayHeader");
    var weekDayHeader = eSpace.Structures.Named("WeekDayHeader");
    dayHeader.DataType = weekDayHeader;
    dayHeader.Description = null; // default value
    
    var row = projectHours_GetWeekly.CreateLocalVariable("Row");
    var weeklyProjectRow = eSpace.Structures.Named("WeeklyProjectRow");
    row.DataType = weeklyProjectRow;
    row.Description = null; // default value
    
    var cell = projectHours_GetWeekly.CreateLocalVariable("Cell");
    var weeklyCell = eSpace.Structures.Named("WeeklyCell");
    cell.DataType = weeklyCell;
    cell.Description = null; // default value
    
    var startNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Week bounds";
    
    var assignment = assignNode.CreateAssignment("Week.WeekStart", "Date_GetWeekStart(ReferenceDate)");
    
    var assignment2 = assignNode.CreateAssignment("Week.WeekEnd", "DateTimeToDate(AddDays(Week.WeekStart, 6))");
    ifNode.TrueTarget = assignNode;
    
    var startMonth = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("StartMonth").ConnectedBelow(assignNode, 1371);
    var calendar_GetMonth = eSpace.ServerActions.Named("Calendar_GetMonth");
    startMonth.Action = calendar_GetMonth;
    startMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    startMonth.IsDisabled = false; // default value
    startMonth.ServerRequestTimeout = null; // default value
    var year = calendar_GetMonth.InputParameters.Named("Year");
    startMonth.SetArgumentValue(year, "Year(Week.WeekStart)");
    var month = calendar_GetMonth.InputParameters.Named("Month");
    startMonth.SetArgumentValue(month, "Month(Week.WeekStart)");
    
    var endMonth = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("EndMonth").ConnectedBelow(startMonth, 1371);
    endMonth.Action = calendar_GetMonth;
    endMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    endMonth.IsDisabled = false; // default value
    endMonth.ServerRequestTimeout = null; // default value
    endMonth.SetArgumentValue(year, "Year(Week.WeekEnd)");
    endMonth.SetArgumentValue(month, "Month(Week.WeekEnd)");
    
    var assignNode2 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(endMonth, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment3 = assignNode2.CreateAssignment("DayIndex", "0");
    
    var ifNode2 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode2, 1371);
    ifNode2.SetCondition("DayIndex < 7");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "More days?";
    
    var getWeekProjects = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetWeekProjects").Below(ifNode2, 5484);
    getWeekProjects.CacheInMinutes = null; // default value
    getWeekProjects.Description = null; // default value
    getWeekProjects.IsDisabled = false; // default value
    getWeekProjects.SetMaxRecords(null); // default value
    getWeekProjects.Statement = "SELECT {Project}.*, {Client}.*\nFROM {Project}\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nWHERE {Project}.[Id] IN (\n    SELECT {ProjectAssignment}.[ProjectId] FROM {ProjectAssignment}\n    WHERE {ProjectAssignment}.[UserId] = @UserId AND {ProjectAssignment}.[IsActive] = 1\n      AND {ProjectAssignment}.[StartDate] <= @WeekEnd\n      AND ({ProjectAssignment}.[EndDate] = @NullDate OR {ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] >= @WeekStart)\n      AND {Project}.[IsActive] = 1 AND {Client}.[IsActive] = 1)\nOR {Project}.[Id] IN (\n    SELECT {TimeEntry}.[ProjectId] FROM {TimeEntry}\n    INNER JOIN {DaySheet} ON {DaySheet}.[Id] = {TimeEntry}.[DaySheetId]\n    WHERE {DaySheet}.[UserId] = @UserId AND {DaySheet}.[SheetDate] >= @WeekStart AND {DaySheet}.[SheetDate] <= @WeekEnd)\nORDER BY {Client}.[Name], {Project}.[Name]";
    getWeekProjects.Timeout = null; // default value
    
    var userId = getWeekProjects.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId.DataType = user.IdentifierType;
    userId.SetDefaultValue(null); // default value
    userId.Description = null; // default value
    userId.ExpandInline = false; // default value
    
    var weekStart = getWeekProjects.CreateInputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.SetDefaultValue(null); // default value
    weekStart.Description = null; // default value
    weekStart.ExpandInline = false; // default value
    
    var weekEnd = getWeekProjects.CreateInputParameter("WeekEnd");
    weekEnd.DataType = eSpace.DateType;
    weekEnd.SetDefaultValue(null); // default value
    weekEnd.Description = null; // default value
    weekEnd.ExpandInline = false; // default value
    
    var nullDate = getWeekProjects.CreateInputParameter("NullDate");
    nullDate.DataType = eSpace.DateType;
    nullDate.SetDefaultValue(null); // default value
    nullDate.Description = null; // default value
    nullDate.ExpandInline = false; // default value
    
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project2 = getWeekProjects.CreateOutput(project, "Project");
    
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client2 = getWeekProjects.CreateOutput(client, "Client");
    ifNode2.FalseTarget = getWeekProjects;
    
    var assignNode3 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Build day header";
    
    var assignment4 = assignNode3.CreateAssignment("DayHeader.Date", "DateTimeToDate(AddDays(Week.WeekStart, DayIndex))");
    
    var assignment5 = assignNode3.CreateAssignment("DayHeader.DayName", "If(DayOfWeek(DayHeader.Date) = 1, \"Monday\", If(DayOfWeek(DayHeader.Date) = 2, \"Tuesday\", If(DayOfWeek(DayHeader.Date) = 3, \"Wednesday\", If(DayOfWeek(DayHeader.Date) = 4, \"Thursday\", If(DayOfWeek(DayHeader.Date) = 5, \"Friday\", If(DayOfWeek(DayHeader.Date) = 6, \"Saturday\", \"Sunday\"))))))");
    
    var assignment6 = assignNode3.CreateAssignment("DayHeader.DateLabel", "FormatDateTime(DayHeader.Date, \"dd/MM\")");
    
    var assignment7 = assignNode3.CreateAssignment("DayHeader.IsWeekend", "DayOfWeek(DayHeader.Date) = 0 or DayOfWeek(DayHeader.Date) = 6");
    
    var assignment8 = assignNode3.CreateAssignment("DayHeader.IsToday", "DayHeader.Date = CurrDate()");
    
    var assignment9 = assignNode3.CreateAssignment("DayHeader.DayTypeId", "If(Month(DayHeader.Date) = Month(Week.WeekStart), StartMonth.Days[Day(DayHeader.Date) - 1].DayTypeId, EndMonth.Days[Day(DayHeader.Date) - 1].DayTypeId)");
    
    var assignment10 = assignNode3.CreateAssignment("DayHeader.CssClass", "If(Month(DayHeader.Date) = Month(Week.WeekStart), StartMonth.Days[Day(DayHeader.Date) - 1].CssClass, EndMonth.Days[Day(DayHeader.Date) - 1].CssClass)");
    
    var assignment11 = assignNode3.CreateAssignment("DayHeader.IsEditable", "If(Month(DayHeader.Date) = Month(Week.WeekStart), StartMonth.Days[Day(DayHeader.Date) - 1].IsEditable, EndMonth.Days[Day(DayHeader.Date) - 1].IsEditable)");
    
    var assignment12 = assignNode3.CreateAssignment("Cell.Date", "DayHeader.Date");
    
    var assignment13 = assignNode3.CreateAssignment("Cell.Minutes", "0");
    
    var assignment14 = assignNode3.CreateAssignment("Cell.ShowDash", "True");
    ifNode2.TrueTarget = assignNode3;
    
    var getWeekEntries = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetWeekEntries").ConnectedBelow(getWeekProjects, 1371);
    getWeekEntries.CacheInMinutes = null; // default value
    getWeekEntries.Description = null; // default value
    getWeekEntries.IsDisabled = false; // default value
    getWeekEntries.SetMaxRecords(null); // default value
    getWeekEntries.SetStartIndex(null); // default value
    getWeekEntries.Timeout = null; // default value
    var timeEntry2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var timeEntry = getWeekEntries.AsDatabaseAggregate.CreateSource(timeEntry2);
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getWeekEntries.AsDatabaseAggregate.CreateSource(daySheet2);
    var join = getWeekEntries.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    getWeekEntries.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getWeekEntries.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= Week.WeekStart and DaySheet.SheetDate <= Week.WeekEnd");
    getWeekEntries.AsDatabaseAggregate.CreateGroupByAttribute("ProjectId").SetAttribute("TimeEntry.ProjectId");
    getWeekEntries.AsDatabaseAggregate.CreateGroupByAttribute("SheetDate").SetAttribute("DaySheet.SheetDate");
    var minutes = getWeekEntries.AsDatabaseAggregate.CreateAggregatedAttribute("Minutes");
    minutes.SetAttribute("TimeEntry.Minutes");
    minutes.AggregationType = AggregationType.Sum;
    
    getWeekProjects.SetArgumentValue(userId, "GetUserId()");
    getWeekProjects.SetArgumentValue(nullDate, "NullDate()");
    getWeekProjects.SetArgumentValue(weekStart, "Week.WeekStart");
    getWeekProjects.SetArgumentValue(weekEnd, "Week.WeekEnd");
    
    var listAppend = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode3, 1371);
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Week.Days");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "DayHeader");
    
    var forEachNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getWeekEntries, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetWeekProjects.List");
    forEachNode.SetStartIndex(null); // default value
    
    var listAppend22 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend2").ConnectedBelow(listAppend, 1371);
    listAppend22.Action = listAppend2;
    listAppend22.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend22.IsDisabled = false; // default value
    listAppend22.ServerRequestTimeout = null; // default value
    listAppend22.SetArgumentValue(element, "Cell");
    listAppend22.SetArgumentValue(list, "Week.DayTotals");
    
    var assignNode4 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 5484);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Start row";
    
    var assignment15 = assignNode4.CreateAssignment("Row.ProjectId", "GetWeekProjects.List.Current.Project.Id");
    
    var assignment16 = assignNode4.CreateAssignment("Row.ClientName", "GetWeekProjects.List.Current.Client.Name");
    
    var assignment17 = assignNode4.CreateAssignment("Row.ProjectName", "GetWeekProjects.List.Current.Project.Name");
    
    var assignment18 = assignNode4.CreateAssignment("Row.ProjectCode", "GetWeekProjects.List.Current.Project.Code");
    
    var assignment19 = assignNode4.CreateAssignment("Row.TotalMinutes", "0");
    
    var assignment20 = assignNode4.CreateAssignment("DayIndex", "0");
    forEachNode.CycleTarget = assignNode4;
    
    var endNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 12339);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var assignNode5 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend22, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    assignNode5.Target = ifNode2;
    
    var assignment21 = assignNode5.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var listClear = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListClear").ConnectedBelow(assignNode4, 1371);
    var listClear2 = _System_.ServerActions.Named("ListClear");
    listClear.Action = listClear2;
    listClear.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listClear.IsDisabled = false; // default value
    listClear.ServerRequestTimeout = null; // default value
    var list2 = listClear2.InputParameters.Named("List");
    listClear.SetArgumentValue(list2, "Row.Cells");
    
    var ifNode3 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(listClear, 1371);
    ifNode3.SetCondition("DayIndex < 7");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "More cells?";
    
    var listAppend4 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend4").Below(ifNode3, 8226);
    listAppend4.Action = listAppend2;
    listAppend4.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend4.IsDisabled = false; // default value
    listAppend4.ServerRequestTimeout = null; // default value
    listAppend4.Target = forEachNode;
    listAppend4.SetArgumentValue(element, "Row");
    listAppend4.SetArgumentValue(list, "Week.Rows");
    ifNode3.FalseTarget = listAppend4;
    
    var assignNode6 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 3656);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Start cell";
    
    var assignment22 = assignNode6.CreateAssignment("Cell.Date", "Week.Days[DayIndex].Date");
    
    var assignment23 = assignNode6.CreateAssignment("Cell.Minutes", "0");
    ifNode3.TrueTarget = assignNode6;
    
    var forEachNode2 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode6, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("GetWeekEntries.List");
    forEachNode2.SetStartIndex(null); // default value
    
    var ifNode4 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode2, 1828);
    ifNode4.SetCondition("GetWeekEntries.List.Current.ProjectId = Row.ProjectId and GetWeekEntries.List.Current.SheetDate = Cell.Date");
    ifNode4.FalseTarget = forEachNode2;
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Same project and day?";
    forEachNode2.CycleTarget = ifNode4;
    
    var assignNode7 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(forEachNode2, 2742);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "Dash rule and totals";
    
    var assignment24 = assignNode7.CreateAssignment("Cell.ShowDash", "Cell.Minutes = 0 and (Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Future or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Weekend or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Holiday or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.DayOff)");
    
    var assignment25 = assignNode7.CreateAssignment("Row.TotalMinutes", "Row.TotalMinutes + Cell.Minutes");
    
    var assignment26 = assignNode7.CreateAssignment("Week.DayTotals[DayIndex].Minutes", "Week.DayTotals[DayIndex].Minutes + Cell.Minutes");
    
    var assignment27 = assignNode7.CreateAssignment("Week.DayTotals[DayIndex].ShowDash", "Week.DayTotals[DayIndex].ShowDash and Cell.ShowDash");
    
    var assignment28 = assignNode7.CreateAssignment("Week.GrandTotalMinutes", "Week.GrandTotalMinutes + Cell.Minutes");
    forEachNode2.Target = assignNode7;
    
    var assignNode8 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    assignNode8.Target = forEachNode2;
    
    var assignment29 = assignNode8.CreateAssignment("Cell.Minutes", "Cell.Minutes + LongIntegerToInteger(GetWeekEntries.List.Current.Minutes)");
    ifNode4.TrueTarget = assignNode8;
    
    var listAppend3 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend3").ConnectedBelow(assignNode7, 1371);
    listAppend3.Action = listAppend2;
    listAppend3.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend3.IsDisabled = false; // default value
    listAppend3.ServerRequestTimeout = null; // default value
    listAppend3.SetArgumentValue(list, "Row.Cells");
    listAppend3.SetArgumentValue(element, "Cell");
    
    var assignNode9 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend3, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    assignNode9.Target = ifNode3;
    
    var assignment30 = assignNode9.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var week = projectHours_GetWeekly.CreateOutputParameter("Week");
    var weeklyHours = eSpace.Structures.Named("WeeklyHours");
    week.DataType = weeklyHours;
    week.Description = null; // default value
}
