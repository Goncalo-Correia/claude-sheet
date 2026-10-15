// Published: 2026-10-15T18:13:37.734328+00:00 (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
// Module:   ClaudeSheet_CS
// When:     2026-10-15T17:55:12+00:00
// Story:    TT4
// Outcome:  saved
// Imports:  -
eSpace => {
  { // Date_GetWeekStart
    
    /*** creating IServerAction 'Date_GetWeekStart' ***/
    var date_GetWeekStart = eSpace.CreateServerAction("Date_GetWeekStart");
    date_GetWeekStart.CacheInMinutes = null; // default value
    date_GetWeekStart.Description = "Returns the Monday of the week that contains Date.";
    date_GetWeekStart.Folder = null; // default value
    date_GetWeekStart.Function = true;
    date_GetWeekStart.Icon = null; // default value
    date_GetWeekStart.Public = false; // default value
    
    var date = date_GetWeekStart.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("WeekStart", "DateTimeToDate(AddDays(Date, -DecimalToInteger(Mod(DayOfWeek(Date) + 6, 7))))");
    
    var endNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var weekStart = date_GetWeekStart.CreateOutputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.SetDefaultValue(null); // default value
    weekStart.Description = null; // default value
  }
  { // Minutes_Format
    
    /*** creating IServerAction 'Minutes_Format' ***/
    var minutes_Format = eSpace.CreateServerAction("Minutes_Format");
    minutes_Format.CacheInMinutes = null; // default value
    minutes_Format.Description = "Formats a number of minutes as hours and minutes, e.g. 2250 -> \"37h30\" (Separator \"h\") or \"37:30\" (Separator \":\"). Minutes always have 2 digits.";
    minutes_Format.Folder = null; // default value
    minutes_Format.Function = true;
    minutes_Format.Icon = null; // default value
    minutes_Format.Public = false; // default value
    
    var minutes = minutes_Format.CreateInputParameter("Minutes");
    minutes.DataType = eSpace.IntegerType;
    minutes.Description = null; // default value
    minutes.IsMandatory = true; // default value
    
    var separator = minutes_Format.CreateInputParameter("Separator");
    separator.DataType = eSpace.TextType;
    separator.Description = "Text between hours and minutes, e.g. \"h\" or \":\".";
    separator.IsMandatory = true; // default value
    
    var startNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Result", "If(Minutes < 0, \"-\", \"\") + IntegerToText(DecimalToInteger(Trunc(Abs(Minutes) / 60))) + Separator + If(Mod(Abs(Minutes), 60) < 10, \"0\", \"\") + IntegerToText(DecimalToInteger(Mod(Abs(Minutes), 60)))");
    
    var endNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var result = minutes_Format.CreateOutputParameter("Result");
    result.DataType = eSpace.TextType;
    result.SetDefaultValue(null); // default value
    result.Description = null; // default value
  }
  { // Holiday_CreateIfMissing
    
    /*** creating IServerAction 'Holiday_CreateIfMissing' ***/
    var holiday_CreateIfMissing = eSpace.CreateServerAction("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.CacheInMinutes = null; // default value
    holiday_CreateIfMissing.Description = "Creates an active Holiday for HolidayDate unless one already exists for that date (active or not). Used by Bootstrap.";
    holiday_CreateIfMissing.Folder = null; // default value
    holiday_CreateIfMissing.Function = false; // default value
    holiday_CreateIfMissing.Icon = null; // default value
    holiday_CreateIfMissing.Public = false; // default value
    
    var holidayDate = holiday_CreateIfMissing.CreateInputParameter("HolidayDate");
    holidayDate.DataType = eSpace.DateType;
    holidayDate.Description = null; // default value
    holidayDate.IsMandatory = true; // default value
    
    var name = holiday_CreateIfMissing.CreateInputParameter("Name");
    name.DataType = eSpace.TextType;
    name.Description = null; // default value
    name.IsMandatory = true; // default value
    
    var newHoliday = holiday_CreateIfMissing.CreateLocalVariable("NewHoliday");
    var holiday = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    newHoliday.DataType = holiday;
    newHoliday.Description = null; // default value
    
    var startNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var getExistingHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetExistingHoliday").ConnectedBelow(startNode, 1371);
    getExistingHoliday.CacheInMinutes = null; // default value
    getExistingHoliday.Description = null; // default value
    getExistingHoliday.IsDisabled = false; // default value
    getExistingHoliday.SetMaxRecords(null); // default value
    getExistingHoliday.SetStartIndex(null); // default value
    getExistingHoliday.Timeout = null; // default value
    var holiday2 = getExistingHoliday.AsDatabaseAggregate.CreateSource(holiday);
    getExistingHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = HolidayDate");
    
    
    var ifNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getExistingHoliday, 1371);
    ifNode.SetCondition("GetExistingHoliday.List.Empty");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Missing?";
    
    var endNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode.IsDisabled = false; // default value
    ifNode.FalseTarget = endNode;
    
    var assignNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("NewHoliday.HolidayDate", "HolidayDate");
    
    var assignment2 = assignNode.CreateAssignment("NewHoliday.Name", "Name");
    
    var assignment3 = assignNode.CreateAssignment("NewHoliday.IsActive", "True");
    ifNode.TrueTarget = assignNode;
    
    var createHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday").ConnectedBelow(assignNode, 1371);
    createHoliday.Action = holiday.CreateAction;
    createHoliday.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createHoliday.IsDisabled = false; // default value
    createHoliday.ServerRequestTimeout = null; // default value
    var createHoliday2 = holiday.CreateAction;
    var source = createHoliday2.InputParameters.Named("Source");
    createHoliday.SetArgumentValue(source, "NewHoliday");
    
    var endNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(createHoliday, 1371);
    endNode2.IsDisabled = false; // default value
  }
  { // Project_GetAssignedToUser
    
    /*** creating IServerAction 'Project_GetAssignedToUser' ***/
    var project_GetAssignedToUser = eSpace.CreateServerAction("Project_GetAssignedToUser");
    project_GetAssignedToUser.CacheInMinutes = null; // default value
    project_GetAssignedToUser.Description = "Active projects (with active client) the current user is assigned to on ForDate, sorted by client and project name.";
    project_GetAssignedToUser.Folder = null; // default value
    project_GetAssignedToUser.Function = false; // default value
    project_GetAssignedToUser.Icon = null; // default value
    project_GetAssignedToUser.Public = false; // default value (set to true at the end)
    
    var forDate = project_GetAssignedToUser.CreateInputParameter("ForDate");
    forDate.DataType = eSpace.DateType;
    forDate.Description = null; // default value
    forDate.IsMandatory = true; // default value
    
    var line = project_GetAssignedToUser.CreateLocalVariable("Line");
    var daySheetProjectLine = eSpace.Structures.Named("DaySheetProjectLine");
    line.DataType = daySheetProjectLine;
    line.Description = null; // default value
    
    var startNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignedProjects = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignedProjects").Below(ifNode, 1371);
    getAssignedProjects.CacheInMinutes = null; // default value
    getAssignedProjects.Description = null; // default value
    getAssignedProjects.IsDisabled = false; // default value
    getAssignedProjects.SetMaxRecords(null); // default value
    getAssignedProjects.SetStartIndex(null); // default value
    getAssignedProjects.Timeout = null; // default value
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    var projectAssignment = getAssignedProjects.AsDatabaseAggregate.CreateSource(projectAssignment2);
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client = getAssignedProjects.AsDatabaseAggregate.CreateSource(client2);
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project = getAssignedProjects.AsDatabaseAggregate.CreateSource(project2);
    var join = getAssignedProjects.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = projectAssignment;
    join.RightSource = project;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("ProjectAssignment.ProjectId = Project.Id");
    var join2 = getAssignedProjects.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = project;
    join2.RightSource = client;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Project.ClientId = Client.Id");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = GetUserId()");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.StartDate <= ForDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.EndDate = NullDate() or ProjectAssignment.EndDate >= ForDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Project.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Client.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Client.Name");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Name");
    
    ifNode.TrueTarget = getAssignedProjects;
    
    var forEachNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getAssignedProjects, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetAssignedProjects.List");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Line.ProjectId", "GetAssignedProjects.List.Current.Project.Id");
    
    var assignment2 = assignNode.CreateAssignment("Line.ClientName", "GetAssignedProjects.List.Current.Client.Name");
    
    var assignment3 = assignNode.CreateAssignment("Line.ProjectName", "GetAssignedProjects.List.Current.Project.Name");
    
    var assignment4 = assignNode.CreateAssignment("Line.ProjectCode", "GetAssignedProjects.List.Current.Project.Code");
    
    var assignment5 = assignNode.CreateAssignment("Line.TimeEntryId", "NullIdentifier()");
    
    var assignment6 = assignNode.CreateAssignment("Line.Minutes", "0");
    
    var assignment7 = assignNode.CreateAssignment("Line.Description", "\"\"");
    forEachNode.CycleTarget = assignNode;
    
    var endNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 2742);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var listAppend = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    listAppend.Target = forEachNode;
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Line");
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Projects");
    
    var projects = project_GetAssignedToUser.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    projects.Description = null; // default value
    project_GetAssignedToUser.Public = true;
  }
  { // Calendar_GetMonth
    
    /*** creating IServerAction 'Calendar_GetMonth' ***/
    var calendar_GetMonth = eSpace.CreateServerAction("Calendar_GetMonth");
    calendar_GetMonth.CacheInMinutes = null; // default value
    calendar_GetMonth.Description = "One CalendarDay per day of the month for the current user, classified (first match wins) as Holiday, DayOff, Confirmed, Weekend, Future (after today, server date) or Pending. IsEditable = weekday that is not a holiday.";
    calendar_GetMonth.Folder = null; // default value
    calendar_GetMonth.Function = false; // default value
    calendar_GetMonth.Icon = null; // default value
    calendar_GetMonth.Public = false; // default value (set to true at the end)
    
    var year = calendar_GetMonth.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = calendar_GetMonth.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var firstDay = calendar_GetMonth.CreateLocalVariable("FirstDay");
    firstDay.DataType = eSpace.DateType;
    firstDay.SetDefaultValue(null); // default value
    firstDay.Description = null; // default value
    
    var lastDay = calendar_GetMonth.CreateLocalVariable("LastDay");
    lastDay.DataType = eSpace.DateType;
    lastDay.SetDefaultValue(null); // default value
    lastDay.Description = null; // default value
    
    var today = calendar_GetMonth.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    today.SetDefaultValue(null); // default value
    today.Description = null; // default value
    
    var dayIndex = calendar_GetMonth.CreateLocalVariable("DayIndex");
    dayIndex.DataType = eSpace.IntegerType;
    dayIndex.SetDefaultValue(null); // default value
    dayIndex.Description = null; // default value
    
    var calDay = calendar_GetMonth.CreateLocalVariable("CalDay");
    var calendarDay = eSpace.Structures.Named("CalendarDay");
    calDay.DataType = calendarDay;
    calDay.Description = null; // default value
    
    var startNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("FirstDay", "NewDate(Year, Month, 1)");
    
    var assignment2 = assignNode.CreateAssignment("LastDay", "DateTimeToDate(AddDays(AddMonths(FirstDay, 1), -1))");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var assignment4 = assignNode.CreateAssignment("DayIndex", "1");
    ifNode.TrueTarget = assignNode;
    
    var ifNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode2.SetCondition("DayIndex <= Day(LastDay)");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "More days?";
    
    var getMonthHolidays = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthHolidays").Below(ifNode2, 4113);
    getMonthHolidays.CacheInMinutes = null; // default value
    getMonthHolidays.Description = null; // default value
    getMonthHolidays.IsDisabled = false; // default value
    getMonthHolidays.SetMaxRecords(null); // default value
    getMonthHolidays.SetStartIndex(null); // default value
    getMonthHolidays.Timeout = null; // default value
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getMonthHolidays.AsDatabaseAggregate.CreateSource(holiday2);
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= FirstDay and Holiday.HolidayDate <= LastDay");
    
    ifNode2.FalseTarget = getMonthHolidays;
    
    var assignNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment5 = assignNode2.CreateAssignment("CalDay.Date", "DateTimeToDate(AddDays(FirstDay, DayIndex - 1))");
    
    var assignment6 = assignNode2.CreateAssignment("CalDay.DayNumber", "DayIndex");
    
    var assignment7 = assignNode2.CreateAssignment("CalDay.WeekDay", "DecimalToInteger(Mod(DayOfWeek(CalDay.Date) + 6, 7)) + 1");
    
    var assignment8 = assignNode2.CreateAssignment("CalDay.IsWeekend", "CalDay.WeekDay >= 6");
    
    var assignment9 = assignNode2.CreateAssignment("CalDay.IsToday", "CalDay.Date = Today");
    ifNode2.TrueTarget = assignNode2;
    
    var forEachNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthHolidays, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetMonthHolidays.List");
    forEachNode.SetStartIndex(null); // default value
    
    var listAppend = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode2, 1371);
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
    
    var assignNode3 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.Target = forEachNode;
    
    var assignment10 = assignNode3.CreateAssignment("Days[Day(GetMonthHolidays.List.Current.Holiday.HolidayDate) - 1].HolidayName", "GetMonthHolidays.List.Current.Holiday.Name");
    forEachNode.CycleTarget = assignNode3;
    
    var getMonthDaySheets = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthDaySheets").Below(forEachNode, 1371);
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
    
    var assignNode4 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.Target = ifNode2;
    
    var assignment11 = assignNode4.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var forEachNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthDaySheets, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("GetMonthDaySheets.List");
    forEachNode2.SetStartIndex(null); // default value
    
    var assignNode5 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode2, 1828);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    assignNode5.Target = forEachNode2;
    
    var assignment12 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DaySheetId", "GetMonthDaySheets.List.Current.DaySheet.Id");
    
    var assignment13 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DayStatusId", "GetMonthDaySheets.List.Current.DaySheet.DayStatusId");
    forEachNode2.CycleTarget = assignNode5;
    
    var getMonthMinutes = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthMinutes").Below(forEachNode2, 1371);
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
    
    var forEachNode3 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthMinutes, 1371);
    forEachNode3.IsDisabled = false; // default value
    forEachNode3.Label = null; // default value
    forEachNode3.SetMaximumIterations(null); // default value
    forEachNode3.SetRecordList("GetMonthMinutes.List");
    forEachNode3.SetStartIndex(null); // default value
    
    var assignNode6 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.Target = forEachNode3;
    
    var assignment14 = assignNode6.CreateAssignment("Days[Day(GetMonthMinutes.List.Current.SheetDate) - 1].TotalMinutes", "LongIntegerToInteger(GetMonthMinutes.List.Current.TotalMinutes)");
    forEachNode3.CycleTarget = assignNode6;
    
    var forEachNode4 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(forEachNode3, 1371);
    forEachNode4.IsDisabled = false; // default value
    forEachNode4.Label = null; // default value
    forEachNode4.SetMaximumIterations(null); // default value
    forEachNode4.SetRecordList("Days");
    forEachNode4.SetStartIndex(null); // default value
    forEachNode3.Target = forEachNode4;
    
    var assignNode7 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode4, 1828);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    assignNode7.Target = forEachNode4;
    
    var assignment15 = assignNode7.CreateAssignment("Days.Current.DayTypeId", "If(Days.Current.HolidayName <> \"\", Entities.CalendarDayType.Holiday, If(Days.Current.DayStatusId = Entities.DayStatus.DayOff, Entities.CalendarDayType.DayOff, If(Days.Current.DayStatusId = Entities.DayStatus.Confirmed, Entities.CalendarDayType.Confirmed, If(Days.Current.IsWeekend, Entities.CalendarDayType.Weekend, If(Days.Current.Date > Today, Entities.CalendarDayType.Future, Entities.CalendarDayType.Pending)))))");
    
    var assignment16 = assignNode7.CreateAssignment("Days.Current.CssClass", "If(Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"day-holiday\", If(Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"day-off\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"day-confirmed\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"day-weekend\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Future, \"day-future\", \"day-pending\")))))");
    
    var assignment17 = assignNode7.CreateAssignment("Days.Current.IsEditable", "not Days.Current.IsWeekend and Days.Current.HolidayName = \"\"");
    forEachNode4.CycleTarget = assignNode7;
    
    var endNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode4, 1371);
    endNode.IsDisabled = false; // default value
    forEachNode4.Target = endNode;
    
    var days = calendar_GetMonth.CreateOutputParameter("Days");
    days.DataType = eSpace.GetOrCreateListType(calendarDay);
    days.Description = null; // default value
    calendar_GetMonth.Public = true;
  }
  { // Settings
    /*** creating IServerAction 'Settings_GetDailyExpectedMinutes' (new in TT4) ***/
    var settings_GetDailyExpectedMinutes = eSpace.CreateServerAction("Settings_GetDailyExpectedMinutes");
    settings_GetDailyExpectedMinutes.Description = "Returns the DailyExpectedMinutes site property (minutes a user is expected to record per working day), for modules that can't read the site property directly.";
    settings_GetDailyExpectedMinutes.Public = true;
    var minutes = settings_GetDailyExpectedMinutes.CreateOutputParameter("Minutes");
    minutes.DataType = eSpace.IntegerType;
    var startNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    var ifNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.Label = "!Is SheetUser?";
    var raiseExceptionNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    var assignNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.CreateAssignment("Minutes", "Site.DailyExpectedMinutes");
    ifNode.TrueTarget = assignNode;
    var endNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
  }
}
