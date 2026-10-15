// Published: 2026-10-15T18:13:37.734328+00:00 (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
// Module:   ClaudeSheet_CS
// When:     2026-10-15T17:56:35+00:00
// Story:    TT4
// Outcome:  saved
// Imports:  -
eSpace => {
  { // Calendar_GetMonthSummary
    
    /*** creating IServerAction 'Calendar_GetMonthSummary' ***/
    var calendar_GetMonthSummary = eSpace.CreateServerAction("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.CacheInMinutes = null; // default value
    calendar_GetMonthSummary.Description = "Month totals and KPI counts for the current user, derived from GetMonthCalendar. TotalMinutes = minutes on Draft/Confirmed day sheets. ExpectedMinutes = (weekdays - holidays - days off of the whole month) * Site.DailyExpectedMinutes. ProgressPercent = Total/Expected*100 capped at 100 (0 when Expected = 0). ElapsedDays = calendar days elapsed in the month up to today (past month = days in month, future month = 0).";
    calendar_GetMonthSummary.Folder = null; // default value
    calendar_GetMonthSummary.Function = false; // default value
    calendar_GetMonthSummary.Icon = null; // default value
    calendar_GetMonthSummary.Public = false; // default value (set to true at the end)
    
    var year = calendar_GetMonthSummary.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = calendar_GetMonthSummary.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var today = calendar_GetMonthSummary.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    today.Description = null; // default value
    
    var expectedDays = calendar_GetMonthSummary.CreateLocalVariable("ExpectedDays");
    expectedDays.DataType = eSpace.IntegerType;
    expectedDays.Description = null; // default value
    
    var startNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var calendar_GetMonth = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").Below(ifNode, 1371);
    var calendar_GetMonth2 = eSpace.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    calendar_GetMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonth.IsDisabled = false; // default value
    calendar_GetMonth.ServerRequestTimeout = null; // default value
    var month2 = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month2, "Month");
    var year2 = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year2, "Year");
    ifNode.TrueTarget = calendar_GetMonth;
    
    var assignNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonth, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Summary.Year", "Year");
    
    var assignment2 = assignNode.CreateAssignment("Summary.Month", "Month");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var forEachNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Calendar_GetMonth.Days");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode2 = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode;
    
    var assignment4 = assignNode2.CreateAssignment("Summary.TotalMinutes", "Summary.TotalMinutes + If(Calendar_GetMonth.Days.Current.DayStatusId = Entities.DayStatus.DayOff, 0, Calendar_GetMonth.Days.Current.TotalMinutes)");
    
    var assignment5 = assignNode2.CreateAssignment("Summary.ConfirmedCount", "Summary.ConfirmedCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, 1, 0)");
    
    var assignment6 = assignNode2.CreateAssignment("Summary.DayOffCount", "Summary.DayOffCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, 1, 0)");
    
    var assignment7 = assignNode2.CreateAssignment("Summary.HolidayCount", "Summary.HolidayCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, 1, 0)");
    
    var assignment8 = assignNode2.CreateAssignment("Summary.PendingCount", "Summary.PendingCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Pending, 1, 0)");
    
    var assignment9 = assignNode2.CreateAssignment("ExpectedDays", "ExpectedDays + If(not Calendar_GetMonth.Days.Current.IsWeekend and Calendar_GetMonth.Days.Current.DayTypeId <> Entities.CalendarDayType.Holiday and Calendar_GetMonth.Days.Current.DayTypeId <> Entities.CalendarDayType.DayOff, 1, 0)");
    forEachNode.CycleTarget = assignNode2;
    
    var assignNode3 = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(forEachNode, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment10 = assignNode3.CreateAssignment("Summary.ElapsedDays", "If(Year * 100 + Month < Year(Today) * 100 + Month(Today), Calendar_GetMonth.Days.Length, If(Year = Year(Today) and Month = Month(Today), Day(Today), 0))");
    
    var assignment11 = assignNode3.CreateAssignment("Summary.ExpectedMinutes", "ExpectedDays * Site.DailyExpectedMinutes");
    
    var assignment12 = assignNode3.CreateAssignment("Summary.ProgressPercent", "If(Summary.ExpectedMinutes > 0, Min(100, Summary.TotalMinutes * 100 / Summary.ExpectedMinutes), 0)");
    forEachNode.Target = assignNode3;
    
    var endNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode.IsDisabled = false; // default value
    
    var summary = calendar_GetMonthSummary.CreateOutputParameter("Summary");
    var monthSummary = eSpace.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    summary.Description = null; // default value
    calendar_GetMonthSummary.Public = true;
  }
  { // DaySheet_GetDetail
    
    /*** creating IServerAction 'DaySheet_GetDetail' ***/
    var daySheet_GetDetail = eSpace.CreateServerAction("DaySheet_GetDetail");
    daySheet_GetDetail.CacheInMinutes = null; // default value
    daySheet_GetDetail.Description = "Day editor data for the current user: DaySheet status and notes, holiday, and one line per project assigned on Date (GetUserProjects) merged with the existing TimeEntry minutes and description.";
    daySheet_GetDetail.Folder = null; // default value
    daySheet_GetDetail.Function = false; // default value
    daySheet_GetDetail.Icon = null; // default value
    daySheet_GetDetail.Public = false; // default value (set to true at the end)
    
    var date = daySheet_GetDetail.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "!Is SheetUser?";
    
    var raiseExceptionNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getDayRecord = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode, 1371);
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet2);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode.TrueTarget = getDayRecord;
    
    var getDayEntries = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayEntries").ConnectedBelow(getDayRecord, 1371);
    getDayEntries.CacheInMinutes = null; // default value
    getDayEntries.Description = null; // default value
    getDayEntries.IsDisabled = false; // default value
    getDayEntries.SetMaxRecords(null); // default value
    getDayEntries.SetStartIndex(null); // default value
    getDayEntries.Timeout = null; // default value
    var timeEntry2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var timeEntry = getDayEntries.AsDatabaseAggregate.CreateSource(timeEntry2);
    var daySheet3 = getDayEntries.AsDatabaseAggregate.CreateSource(daySheet2);
    var join = getDayEntries.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet3;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    getDayEntries.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayEntries.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    
    var getDayHoliday = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayHoliday").ConnectedBelow(getDayEntries, 1371);
    getDayHoliday.CacheInMinutes = null; // default value
    getDayHoliday.Description = null; // default value
    getDayHoliday.IsDisabled = false; // default value
    getDayHoliday.SetMaxRecords(null); // default value
    getDayHoliday.SetStartIndex(null); // default value
    getDayHoliday.Timeout = null; // default value
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getDayHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = Date");
    
    
    var project_GetAssignedToUser = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetAssignedToUser").ConnectedBelow(getDayHoliday, 1371);
    var project_GetAssignedToUser2 = eSpace.ServerActions.Named("Project_GetAssignedToUser");
    project_GetAssignedToUser.Action = project_GetAssignedToUser2;
    project_GetAssignedToUser.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetAssignedToUser.IsDisabled = false; // default value
    project_GetAssignedToUser.ServerRequestTimeout = null; // default value
    var forDate = project_GetAssignedToUser2.InputParameters.Named("ForDate");
    project_GetAssignedToUser.SetArgumentValue(forDate, "Date");
    
    var assignNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetAssignedToUser, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Detail.SheetDate", "Date");
    
    var assignment2 = assignNode.CreateAssignment("Detail.DaySheetId", "GetDayRecord.List.Current.DaySheet.Id");
    
    var assignment3 = assignNode.CreateAssignment("Detail.DayStatusId", "GetDayRecord.List.Current.DaySheet.DayStatusId");
    
    var assignment4 = assignNode.CreateAssignment("Detail.Notes", "GetDayRecord.List.Current.DaySheet.Notes");
    
    var assignment5 = assignNode.CreateAssignment("Detail.HolidayName", "GetDayHoliday.List.Current.Holiday.Name");
    
    var assignment6 = assignNode.CreateAssignment("Detail.IsEditable", "Mod(DayOfWeek(Date) + 6, 7) < 5 and GetDayHoliday.List.Empty");
    
    var assignment7 = assignNode.CreateAssignment("Detail.DayTypeId", "If(not GetDayHoliday.List.Empty, Entities.CalendarDayType.Holiday, If(GetDayRecord.List.Current.DaySheet.DayStatusId = Entities.DayStatus.DayOff, Entities.CalendarDayType.DayOff, If(GetDayRecord.List.Current.DaySheet.DayStatusId = Entities.DayStatus.Confirmed, Entities.CalendarDayType.Confirmed, If(Mod(DayOfWeek(Date) + 6, 7) >= 5, Entities.CalendarDayType.Weekend, If(Date > CurrDate(), Entities.CalendarDayType.Future, Entities.CalendarDayType.Pending)))))");
    
    var assignment8 = assignNode.CreateAssignment("Detail.Lines", "Project_GetAssignedToUser.Projects");
    
    var forEachNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Detail.Lines");
    forEachNode.SetStartIndex(null); // default value
    
    var forEachNode2 = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ToTheRightOf(forEachNode, 1828);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("GetDayEntries.List");
    forEachNode2.SetStartIndex(null); // default value
    forEachNode2.Target = forEachNode;
    forEachNode.CycleTarget = forEachNode2;
    
    var endNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 2742);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var ifNode2 = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode2, 1828);
    ifNode2.SetCondition("GetDayEntries.List.Current.TimeEntry.ProjectId = Detail.Lines.Current.ProjectId");
    ifNode2.FalseTarget = forEachNode2;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Same project?";
    forEachNode2.CycleTarget = ifNode2;
    
    var assignNode2 = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode2;
    
    var assignment9 = assignNode2.CreateAssignment("Detail.Lines.Current.TimeEntryId", "GetDayEntries.List.Current.TimeEntry.Id");
    
    var assignment10 = assignNode2.CreateAssignment("Detail.Lines.Current.Minutes", "GetDayEntries.List.Current.TimeEntry.Minutes");
    
    var assignment11 = assignNode2.CreateAssignment("Detail.Lines.Current.Description", "GetDayEntries.List.Current.TimeEntry.Description");
    
    var assignment12 = assignNode2.CreateAssignment("Detail.TotalMinutes", "Detail.TotalMinutes + GetDayEntries.List.Current.TimeEntry.Minutes");
    ifNode2.TrueTarget = assignNode2;
    
    var detail = daySheet_GetDetail.CreateOutputParameter("Detail");
    var daySheetDetail = eSpace.Structures.Named("DaySheetDetail");
    detail.DataType = daySheetDetail;
    detail.Description = null; // default value
    daySheet_GetDetail.Public = true;
  }
  { // DaySheet_Save
    
    /*** creating IServerAction 'DaySheet_Save' ***/
    var daySheet_Save = eSpace.CreateServerAction("DaySheet_Save");
    daySheet_Save.CacheInMinutes = null; // default value
    daySheet_Save.Description = "Validates and saves the current user\'s day: weekday, not a holiday, no hours in the future, projects assigned on Date, minutes multiple of 15, total <= 24:00, Confirm needs a total > 0. Upserts the DaySheet (DayOff / Confirmed / Draft) and its TimeEntries; IsDayOff deletes the day\'s entries. Raises DaySheetValidation with a user-facing message.";
    daySheet_Save.Folder = null; // default value
    daySheet_Save.Function = false; // default value
    daySheet_Save.Icon = null; // default value
    daySheet_Save.Public = false; // default value (set to true at the end)
    
    var date = daySheet_Save.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var isDayOff = daySheet_Save.CreateInputParameter("IsDayOff");
    isDayOff.DataType = eSpace.BooleanType;
    isDayOff.Description = null; // default value
    isDayOff.IsMandatory = true; // default value
    
    var confirm = daySheet_Save.CreateInputParameter("Confirm");
    confirm.DataType = eSpace.BooleanType;
    confirm.Description = null; // default value
    confirm.IsMandatory = true; // default value
    
    var entries = daySheet_Save.CreateInputParameter("Entries");
    var timeEntryInput = eSpace.Structures.Named("TimeEntryInput");
    entries.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    entries.Description = null; // default value
    entries.IsMandatory = false;
    
    var notes = daySheet_Save.CreateInputParameter("Notes");
    notes.DataType = eSpace.TextType;
    notes.Description = null; // default value
    notes.IsMandatory = false;
    
    var total = daySheet_Save.CreateLocalVariable("Total");
    total.DataType = eSpace.IntegerType;
    total.Description = null; // default value
    
    var found = daySheet_Save.CreateLocalVariable("Found");
    found.DataType = eSpace.BooleanType;
    found.Description = null; // default value
    
    var sheetId = daySheet_Save.CreateLocalVariable("SheetId");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    sheetId.DataType = daySheet.IdentifierType;
    sheetId.Description = null; // default value
    
    var sheetRec = daySheet_Save.CreateLocalVariable("SheetRec");
    sheetRec.DataType = daySheet;
    sheetRec.Description = null; // default value
    
    var entryRec = daySheet_Save.CreateLocalVariable("EntryRec");
    var timeEntry = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    entryRec.DataType = timeEntry;
    entryRec.Description = null; // default value
    
    var startNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("Mod(DayOfWeek(Date) + 6, 7) >= 5");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Weekend?";
    ifNode.TrueTarget = ifNode2;
    
    var getDayHoliday = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayHoliday").Below(ifNode2, 1371);
    getDayHoliday.CacheInMinutes = null; // default value
    getDayHoliday.Description = null; // default value
    getDayHoliday.IsDisabled = false; // default value
    getDayHoliday.SetMaxRecords(null); // default value
    getDayHoliday.SetStartIndex(null); // default value
    getDayHoliday.Timeout = null; // default value
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getDayHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = Date");
    
    ifNode2.FalseTarget = getDayHoliday;
    
    var raiseExceptionNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var daySheetValidation = eSpace.UserExceptions.Named("DaySheetValidation");
    raiseExceptionNode2.Exception = daySheetValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Nothing can be saved on a weekend.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayHoliday, 1371);
    ifNode3.SetCondition("not GetDayHoliday.List.Empty");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "!Holiday?";
    
    var project_GetAssignedToUser = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetAssignedToUser").Below(ifNode3, 1371);
    var project_GetAssignedToUser2 = eSpace.ServerActions.Named("Project_GetAssignedToUser");
    project_GetAssignedToUser.Action = project_GetAssignedToUser2;
    project_GetAssignedToUser.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetAssignedToUser.IsDisabled = false; // default value
    project_GetAssignedToUser.ServerRequestTimeout = null; // default value
    var forDate = project_GetAssignedToUser2.InputParameters.Named("ForDate");
    project_GetAssignedToUser.SetArgumentValue(forDate, "Date");
    ifNode3.FalseTarget = project_GetAssignedToUser;
    
    var raiseExceptionNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = daySheetValidation;
    raiseExceptionNode3.SetExceptionMessage("GetDayHoliday.List.Current.Holiday.Name + \" is a holiday: nothing can be saved on this day.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var forEachNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(project_GetAssignedToUser, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Entries");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode4.SetCondition("Entries.Current.Minutes < 0 or Mod(Entries.Current.Minutes, 15) <> 0");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "!Invalid minutes?";
    forEachNode.CycleTarget = ifNode4;
    
    var ifNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode, 9597);
    ifNode5.SetCondition("not IsDayOff and Total > 0 and Date > CurrDate()");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "!Hours in the future?";
    forEachNode.Target = ifNode5;
    
    var assignNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Total", "Total + Entries.Current.Minutes");
    
    var assignment2 = assignNode.CreateAssignment("Found", "False");
    ifNode4.FalseTarget = assignNode;
    
    var raiseExceptionNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = daySheetValidation;
    raiseExceptionNode4.SetExceptionMessage("\"Minutes must be a multiple of 15.\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var ifNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode5, 1371);
    ifNode6.SetCondition("not IsDayOff and Total > 1440");
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = "!Over 24:00?";
    ifNode5.FalseTarget = ifNode6;
    
    var raiseExceptionNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode5, 1828);
    raiseExceptionNode5.Exception = daySheetValidation;
    raiseExceptionNode5.SetExceptionMessage("\"Hours cannot be logged in the future.\"");
    raiseExceptionNode5.IsDisabled = false; // default value
    ifNode5.TrueTarget = raiseExceptionNode5;
    
    var ifNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode7.SetCondition("Entries.Current.Minutes > 0");
    ifNode7.FalseTarget = forEachNode;
    ifNode7.IsDisabled = false; // default value
    ifNode7.Label = "Has minutes?";
    
    var ifNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode6, 1371);
    ifNode8.SetCondition("Confirm and not IsDayOff and Total = 0");
    ifNode8.IsDisabled = false; // default value
    ifNode8.Label = "!Confirm without hours?";
    ifNode6.FalseTarget = ifNode8;
    
    var raiseExceptionNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode6, 1828);
    raiseExceptionNode6.Exception = daySheetValidation;
    raiseExceptionNode6.SetExceptionMessage("\"A day cannot exceed 24:00\"");
    raiseExceptionNode6.IsDisabled = false; // default value
    ifNode6.TrueTarget = raiseExceptionNode6;
    
    var forEachNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(ifNode7, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("Project_GetAssignedToUser.Projects");
    forEachNode2.SetStartIndex(null); // default value
    ifNode7.TrueTarget = forEachNode2;
    
    var getDayRecord = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode8, 1371);
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    var daySheet2 = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode8.FalseTarget = getDayRecord;
    
    var raiseExceptionNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode8, 1828);
    raiseExceptionNode7.Exception = daySheetValidation;
    raiseExceptionNode7.SetExceptionMessage("\"Log at least 15 minutes to confirm the day\"");
    raiseExceptionNode7.IsDisabled = false; // default value
    ifNode8.TrueTarget = raiseExceptionNode7;
    
    var ifNode9 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode2, 1828);
    ifNode9.SetCondition("Project_GetAssignedToUser.Projects.Current.ProjectId = Entries.Current.ProjectId");
    ifNode9.FalseTarget = forEachNode2;
    ifNode9.IsDisabled = false; // default value
    ifNode9.Label = "!Same project?";
    forEachNode2.CycleTarget = ifNode9;
    
    var ifNode10 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode2, 2742);
    ifNode10.SetCondition("not Found");
    ifNode10.FalseTarget = forEachNode;
    ifNode10.IsDisabled = false; // default value
    ifNode10.Label = "!Not assigned?";
    forEachNode2.Target = ifNode10;
    
    var ifNode11 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode11.SetCondition("GetDayRecord.List.Empty");
    ifNode11.IsDisabled = false; // default value
    ifNode11.Label = "!New day sheet?";
    
    var assignNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode9, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode2;
    
    var assignment3 = assignNode2.CreateAssignment("Found", "True");
    ifNode9.TrueTarget = assignNode2;
    
    var raiseExceptionNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode10, 1371);
    raiseExceptionNode8.Exception = daySheetValidation;
    raiseExceptionNode8.SetExceptionMessage("\"You are not assigned to one of these projects on this day.\"");
    raiseExceptionNode8.IsDisabled = false; // default value
    ifNode10.TrueTarget = raiseExceptionNode8;
    
    var assignNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode11, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment4 = assignNode3.CreateAssignment("SheetRec", "GetDayRecord.List.Current.DaySheet");
    ifNode11.FalseTarget = assignNode3;
    
    var assignNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode11, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment5 = assignNode4.CreateAssignment("SheetRec.UserId", "GetUserId()");
    
    var assignment6 = assignNode4.CreateAssignment("SheetRec.SheetDate", "Date");
    
    var assignment7 = assignNode4.CreateAssignment("SheetRec.CreatedOn", "CurrDateTime()");
    ifNode11.TrueTarget = assignNode4;
    
    var assignNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode4, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment8 = assignNode5.CreateAssignment("SheetRec.DayStatusId", "If(IsDayOff, Entities.DayStatus.DayOff, If(Confirm, Entities.DayStatus.Confirmed, Entities.DayStatus.Draft))");
    
    var assignment9 = assignNode5.CreateAssignment("SheetRec.Notes", "Notes");
    
    var assignment10 = assignNode5.CreateAssignment("SheetRec.UpdatedOn", "CurrDateTime()");
    assignNode3.Target = assignNode5;
    
    var createOrUpdateDaySheet = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateDaySheet").ConnectedBelow(assignNode5, 1371);
    createOrUpdateDaySheet.Action = daySheet.CreateOrUpdateAction;
    createOrUpdateDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createOrUpdateDaySheet.IsDisabled = false; // default value
    createOrUpdateDaySheet.ServerRequestTimeout = null; // default value
    var createOrUpdateDaySheet2 = daySheet.CreateOrUpdateAction;
    var source = createOrUpdateDaySheet2.InputParameters.Named("Source");
    createOrUpdateDaySheet.SetArgumentValue(source, "SheetRec");
    
    var assignNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createOrUpdateDaySheet, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    
    var assignment11 = assignNode6.CreateAssignment("SheetId", "CreateOrUpdateDaySheet.Id");
    
    var getSheetEntries = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSheetEntries").ConnectedBelow(assignNode6, 1371);
    getSheetEntries.CacheInMinutes = null; // default value
    getSheetEntries.Description = null; // default value
    getSheetEntries.IsDisabled = false; // default value
    getSheetEntries.SetMaxRecords(null); // default value
    getSheetEntries.SetStartIndex(null); // default value
    getSheetEntries.Timeout = null; // default value
    var timeEntry2 = getSheetEntries.AsDatabaseAggregate.CreateSource(timeEntry);
    getSheetEntries.AsDatabaseAggregate.CreateFilter("TimeEntry.DaySheetId = SheetId");
    
    
    var forEachNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getSheetEntries, 1371);
    forEachNode3.IsDisabled = false; // default value
    forEachNode3.Label = null; // default value
    forEachNode3.SetMaximumIterations(null); // default value
    forEachNode3.SetRecordList("GetSheetEntries.List");
    forEachNode3.SetStartIndex(null); // default value
    
    var assignNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 3656);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    
    var assignment12 = assignNode7.CreateAssignment("Found", "False");
    forEachNode3.CycleTarget = assignNode7;
    
    var ifNode12 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode3, 8226);
    ifNode12.SetCondition("not IsDayOff");
    ifNode12.IsDisabled = false; // default value
    ifNode12.Label = "Work day?";
    forEachNode3.Target = ifNode12;
    
    var forEachNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode7, 1371);
    forEachNode4.IsDisabled = false; // default value
    forEachNode4.Label = null; // default value
    forEachNode4.SetMaximumIterations(null); // default value
    forEachNode4.SetRecordList("Entries");
    forEachNode4.SetStartIndex(null); // default value
    
    var endNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode12, 1828);
    endNode.IsDisabled = false; // default value
    ifNode12.FalseTarget = endNode;
    
    var forEachNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(ifNode12, 1371);
    forEachNode5.IsDisabled = false; // default value
    forEachNode5.Label = null; // default value
    forEachNode5.SetMaximumIterations(null); // default value
    forEachNode5.SetRecordList("Entries");
    forEachNode5.SetStartIndex(null); // default value
    ifNode12.TrueTarget = forEachNode5;
    
    var ifNode13 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode4, 1828);
    ifNode13.SetCondition("not IsDayOff and Entries.Current.ProjectId = GetSheetEntries.List.Current.TimeEntry.ProjectId and Entries.Current.Minutes > 0");
    ifNode13.FalseTarget = forEachNode4;
    ifNode13.IsDisabled = false; // default value
    ifNode13.Label = "!Still sent?";
    forEachNode4.CycleTarget = ifNode13;
    
    var ifNode14 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode4, 4113);
    ifNode14.SetCondition("not Found");
    ifNode14.FalseTarget = forEachNode3;
    ifNode14.IsDisabled = false; // default value
    ifNode14.Label = "!Not sent anymore?";
    forEachNode4.Target = ifNode14;
    
    var ifNode15 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode5, 3656);
    ifNode15.SetCondition("Entries.Current.Minutes > 0");
    ifNode15.FalseTarget = forEachNode5;
    ifNode15.IsDisabled = false; // default value
    ifNode15.Label = "!Has minutes?";
    forEachNode5.CycleTarget = ifNode15;
    
    var endNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode5, 9597);
    endNode2.IsDisabled = false; // default value
    forEachNode5.Target = endNode2;
    
    var assignNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode13, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    
    var assignment13 = assignNode8.CreateAssignment("EntryRec", "GetSheetEntries.List.Current.TimeEntry");
    
    var assignment14 = assignNode8.CreateAssignment("EntryRec.Minutes", "Entries.Current.Minutes");
    
    var assignment15 = assignNode8.CreateAssignment("EntryRec.Description", "Entries.Current.Description");
    
    var assignment16 = assignNode8.CreateAssignment("EntryRec.UpdatedOn", "CurrDateTime()");
    
    var assignment17 = assignNode8.CreateAssignment("Found", "True");
    ifNode13.TrueTarget = assignNode8;
    
    var deleteTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteTimeEntry").Below(ifNode14, 1371);
    deleteTimeEntry.Action = timeEntry.DeleteAction;
    deleteTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteTimeEntry.IsDisabled = false; // default value
    deleteTimeEntry.ServerRequestTimeout = null; // default value
    deleteTimeEntry.Target = forEachNode3;
    var deleteTimeEntry2 = timeEntry.DeleteAction;
    var id = deleteTimeEntry2.InputParameters.Named("Id");
    deleteTimeEntry.SetArgumentValue(id, "GetSheetEntries.List.Current.TimeEntry.Id");
    ifNode14.TrueTarget = deleteTimeEntry;
    
    var assignNode9 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode15, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    
    var assignment18 = assignNode9.CreateAssignment("Found", "False");
    ifNode15.TrueTarget = assignNode9;
    
    var updateTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateTimeEntry").ConnectedBelow(assignNode8, 1371);
    updateTimeEntry.Action = timeEntry.UpdateAction;
    updateTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateTimeEntry.IsDisabled = false; // default value
    updateTimeEntry.ServerRequestTimeout = null; // default value
    updateTimeEntry.Target = forEachNode4;
    var updateTimeEntry2 = timeEntry.UpdateAction;
    var source2 = updateTimeEntry2.InputParameters.Named("Source");
    updateTimeEntry.SetArgumentValue(source2, "EntryRec");
    
    var forEachNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode9, 1371);
    forEachNode6.IsDisabled = false; // default value
    forEachNode6.Label = null; // default value
    forEachNode6.SetMaximumIterations(null); // default value
    forEachNode6.SetRecordList("GetSheetEntries.List");
    forEachNode6.SetStartIndex(null); // default value
    
    var ifNode16 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode6, 1828);
    ifNode16.SetCondition("GetSheetEntries.List.Current.TimeEntry.ProjectId = Entries.Current.ProjectId");
    ifNode16.FalseTarget = forEachNode6;
    ifNode16.IsDisabled = false; // default value
    ifNode16.Label = "!Already saved?";
    forEachNode6.CycleTarget = ifNode16;
    
    var ifNode17 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode6, 2742);
    ifNode17.SetCondition("not Found");
    ifNode17.FalseTarget = forEachNode5;
    ifNode17.IsDisabled = false; // default value
    ifNode17.Label = "!New entry?";
    forEachNode6.Target = ifNode17;
    
    var assignNode10 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode16, 1371);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = null; // default value
    assignNode10.Target = forEachNode6;
    
    var assignment19 = assignNode10.CreateAssignment("Found", "True");
    ifNode16.TrueTarget = assignNode10;
    
    var assignNode11 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode17, 1371);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = null; // default value
    
    var assignment20 = assignNode11.CreateAssignment("EntryRec.Id", "NullIdentifier()");
    
    var assignment21 = assignNode11.CreateAssignment("EntryRec.DaySheetId", "SheetId");
    
    var assignment22 = assignNode11.CreateAssignment("EntryRec.ProjectId", "Entries.Current.ProjectId");
    
    var assignment23 = assignNode11.CreateAssignment("EntryRec.Minutes", "Entries.Current.Minutes");
    
    var assignment24 = assignNode11.CreateAssignment("EntryRec.Description", "Entries.Current.Description");
    
    var assignment25 = assignNode11.CreateAssignment("EntryRec.CreatedOn", "CurrDateTime()");
    
    var assignment26 = assignNode11.CreateAssignment("EntryRec.UpdatedOn", "CurrDateTime()");
    ifNode17.TrueTarget = assignNode11;
    
    var createTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateTimeEntry").ConnectedBelow(assignNode11, 1371);
    createTimeEntry.Action = timeEntry.CreateAction;
    createTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createTimeEntry.IsDisabled = false; // default value
    createTimeEntry.ServerRequestTimeout = null; // default value
    createTimeEntry.Target = forEachNode5;
    var createTimeEntry2 = timeEntry.CreateAction;
    var source3 = createTimeEntry2.InputParameters.Named("Source");
    createTimeEntry.SetArgumentValue(source3, "EntryRec");
    daySheet_Save.Public = true;
  }
  { // DaySheet_Clear
    
    /*** creating IServerAction 'DaySheet_Clear' ***/
    var daySheet_Clear = eSpace.CreateServerAction("DaySheet_Clear");
    daySheet_Clear.CacheInMinutes = null; // default value
    daySheet_Clear.Description = "Deletes the current user\'s DaySheet for Date (its TimeEntries are deleted by the Delete rule). The day goes back to Pending or Future.";
    daySheet_Clear.Folder = null; // default value
    daySheet_Clear.Function = false; // default value
    daySheet_Clear.Icon = null; // default value
    daySheet_Clear.Public = false; // default value (set to true at the end)
    
    var date = daySheet_Clear.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "!Is SheetUser?";
    
    var raiseExceptionNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getDayRecord = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode, 1371);
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet2);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode.TrueTarget = getDayRecord;
    
    var ifNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode2.SetCondition("not GetDayRecord.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Has day sheet?";
    
    var endNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    endNode.IsDisabled = false; // default value
    ifNode2.FalseTarget = endNode;
    
    var deleteDaySheet = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteDaySheet").Below(ifNode2, 1371);
    deleteDaySheet.Action = daySheet2.DeleteAction;
    deleteDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteDaySheet.IsDisabled = false; // default value
    deleteDaySheet.ServerRequestTimeout = null; // default value
    var deleteDaySheet2 = daySheet2.DeleteAction;
    var id = deleteDaySheet2.InputParameters.Named("Id");
    deleteDaySheet.SetArgumentValue(id, "GetDayRecord.List.Current.DaySheet.Id");
    ifNode2.TrueTarget = deleteDaySheet;
    
    var endNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(deleteDaySheet, 1371);
    endNode2.IsDisabled = false; // default value
    daySheet_Clear.Public = true;
  }
  { // DaySheet_FillPending
    
    /*** creating IServerAction 'DaySheet_FillPending' ***/
    var daySheet_FillPending = eSpace.CreateServerAction("DaySheet_FillPending");
    daySheet_FillPending.CacheInMinutes = null; // default value
    daySheet_FillPending.Description = "Fills and confirms every Pending day of the month for the current user: a Draft with a total > 0 is confirmed as is; any other Pending day is saved through SaveDaySheet with one TimeEntry (ProjectId, MinutesPerDay) and confirmed. Runs in one request, so any error rolls back every day. Returns the number of days filled.";
    daySheet_FillPending.Folder = null; // default value
    daySheet_FillPending.Function = false; // default value
    daySheet_FillPending.Icon = null; // default value
    daySheet_FillPending.Public = false; // default value (set to true at the end)
    
    var year = daySheet_FillPending.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = daySheet_FillPending.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var projectId = daySheet_FillPending.CreateInputParameter("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.Description = null; // default value
    projectId.IsMandatory = true; // default value
    
    var minutesPerDay = daySheet_FillPending.CreateInputParameter("MinutesPerDay");
    minutesPerDay.DataType = eSpace.IntegerType;
    minutesPerDay.Description = null; // default value
    minutesPerDay.IsMandatory = true; // default value
    
    var entry = daySheet_FillPending.CreateLocalVariable("Entry");
    var timeEntryInput = eSpace.Structures.Named("TimeEntryInput");
    entry.DataType = timeEntryInput;
    entry.Description = null; // default value
    
    var oneEntry = daySheet_FillPending.CreateLocalVariable("OneEntry");
    oneEntry.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    oneEntry.Description = null; // default value
    
    var sheetRec = daySheet_FillPending.CreateLocalVariable("SheetRec");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    sheetRec.DataType = daySheet;
    sheetRec.Description = null; // default value
    
    var startNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var calendar_GetMonth = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").Below(ifNode, 1371);
    var calendar_GetMonth2 = eSpace.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    calendar_GetMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonth.IsDisabled = false; // default value
    calendar_GetMonth.ServerRequestTimeout = null; // default value
    var month2 = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month2, "Month");
    var year2 = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year2, "Year");
    ifNode.TrueTarget = calendar_GetMonth;
    
    var assignNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonth, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Entry.ProjectId", "ProjectId");
    
    var assignment2 = assignNode.CreateAssignment("Entry.Minutes", "MinutesPerDay");
    
    var assignment3 = assignNode.CreateAssignment("FilledCount", "0");
    
    var listAppend = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "OneEntry");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Entry");
    
    var forEachNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(listAppend, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Calendar_GetMonth.Days");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode2 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode2.SetCondition("Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Pending");
    ifNode2.FalseTarget = forEachNode;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Pending?";
    forEachNode.CycleTarget = ifNode2;
    
    var endNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 8226);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var ifNode3 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("Calendar_GetMonth.Days.Current.DaySheetId <> NullIdentifier() and Calendar_GetMonth.Days.Current.TotalMinutes > 0");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Draft with hours?";
    ifNode2.TrueTarget = ifNode3;
    
    var daySheet_Save = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_Save").ToTheRightOf(ifNode3, 1828);
    var daySheet_Save2 = eSpace.ServerActions.Named("DaySheet_Save");
    daySheet_Save.Action = daySheet_Save2;
    daySheet_Save.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    daySheet_Save.IsDisabled = false; // default value
    daySheet_Save.ServerRequestTimeout = null; // default value
    var entries = daySheet_Save2.InputParameters.Named("Entries");
    daySheet_Save.SetArgumentValue(entries, "OneEntry");
    var isDayOff = daySheet_Save2.InputParameters.Named("IsDayOff");
    daySheet_Save.SetArgumentValue(isDayOff, "False");
    var notes = daySheet_Save2.InputParameters.Named("Notes");
    daySheet_Save.SetArgumentValue(notes, "\"\"");
    var confirm = daySheet_Save2.InputParameters.Named("Confirm");
    daySheet_Save.SetArgumentValue(confirm, "True");
    var date = daySheet_Save2.InputParameters.Named("Date");
    daySheet_Save.SetArgumentValue(date, "Calendar_GetMonth.Days.Current.Date");
    ifNode3.FalseTarget = daySheet_Save;
    
    var getDaySheetForUpdate = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetDaySheetForUpdate").Below(ifNode3, 1371);
    getDaySheetForUpdate.Action = daySheet.GetForUpdateAction;
    getDaySheetForUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getDaySheetForUpdate.IsDisabled = false; // default value
    getDaySheetForUpdate.ServerRequestTimeout = null; // default value
    var getDaySheetForUpdate2 = daySheet.GetForUpdateAction;
    var id = getDaySheetForUpdate2.InputParameters.Named("Id");
    getDaySheetForUpdate.SetArgumentValue(id, "Calendar_GetMonth.Days.Current.DaySheetId");
    ifNode3.TrueTarget = getDaySheetForUpdate;
    
    var assignNode2 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(daySheet_Save, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode;
    
    var assignment4 = assignNode2.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var assignNode3 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getDaySheetForUpdate, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment5 = assignNode3.CreateAssignment("SheetRec", "GetDaySheetForUpdate.Record");
    
    var assignment6 = assignNode3.CreateAssignment("SheetRec.DayStatusId", "Entities.DayStatus.Confirmed");
    
    var assignment7 = assignNode3.CreateAssignment("SheetRec.UpdatedOn", "CurrDateTime()");
    
    var updateDaySheet = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateDaySheet").ConnectedBelow(assignNode3, 1371);
    updateDaySheet.Action = daySheet.UpdateAction;
    updateDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateDaySheet.IsDisabled = false; // default value
    updateDaySheet.ServerRequestTimeout = null; // default value
    var updateDaySheet2 = daySheet.UpdateAction;
    var source = updateDaySheet2.InputParameters.Named("Source");
    updateDaySheet.SetArgumentValue(source, "SheetRec");
    
    var assignNode4 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(updateDaySheet, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.Target = forEachNode;
    
    var assignment8 = assignNode4.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var filledCount = daySheet_FillPending.CreateOutputParameter("FilledCount");
    filledCount.DataType = eSpace.IntegerType;
    filledCount.Description = null; // default value
    daySheet_FillPending.Public = true;
  }
  { // ProjectHours_GetWeekly
    
    /*** creating IServerAction 'ProjectHours_GetWeekly' ***/
    var projectHours_GetWeekly = eSpace.CreateServerAction("ProjectHours_GetWeekly");
    projectHours_GetWeekly.CacheInMinutes = null; // default value
    projectHours_GetWeekly.Description = "Weekly grid for the current user: the Monday-Sunday week containing ReferenceDate, day headers classified like the calendar (Calendar_GetMonth), one row per project assigned during the week or with entries in it (sorted by client, project), 7 cells per row with the \"-\" rule, totals per row, per day and for the week.";
    projectHours_GetWeekly.Folder = null; // default value
    projectHours_GetWeekly.Function = false; // default value
    projectHours_GetWeekly.Icon = null; // default value
    projectHours_GetWeekly.Public = false; // default value (set to true at the end)
    
    var referenceDate = projectHours_GetWeekly.CreateInputParameter("ReferenceDate");
    referenceDate.DataType = eSpace.DateType;
    referenceDate.Description = null; // default value
    referenceDate.IsMandatory = true; // default value
    
    var dayIndex = projectHours_GetWeekly.CreateLocalVariable("DayIndex");
    dayIndex.DataType = eSpace.IntegerType;
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
    endMonth.SetArgumentValue(month, "Month(Week.WeekEnd)");
    endMonth.SetArgumentValue(year, "Year(Week.WeekEnd)");
    
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
    userId.Description = null; // default value
    userId.ExpandInline = false; // default value
    
    var weekStart = getWeekProjects.CreateInputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.Description = null; // default value
    weekStart.ExpandInline = false; // default value
    
    var weekEnd = getWeekProjects.CreateInputParameter("WeekEnd");
    weekEnd.DataType = eSpace.DateType;
    weekEnd.Description = null; // default value
    weekEnd.ExpandInline = false; // default value
    
    var nullDate = getWeekProjects.CreateInputParameter("NullDate");
    nullDate.DataType = eSpace.DateType;
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
    
    getWeekProjects.SetArgumentValue(weekStart, "Week.WeekStart");
    getWeekProjects.SetArgumentValue(nullDate, "NullDate()");
    getWeekProjects.SetArgumentValue(weekEnd, "Week.WeekEnd");
    getWeekProjects.SetArgumentValue(userId, "GetUserId()");
    
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
    listAppend22.SetArgumentValue(list, "Week.DayTotals");
    listAppend22.SetArgumentValue(element, "Cell");
    
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
    listAppend4.SetArgumentValue(list, "Week.Rows");
    listAppend4.SetArgumentValue(element, "Row");
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
    projectHours_GetWeekly.Public = true;
  }
  { // Bootstrap
    
    /*** creating IServerAction 'Bootstrap' ***/
    var bootstrap = eSpace.CreateServerAction("Bootstrap");
    bootstrap.CacheInMinutes = null; // default value
    bootstrap.Description = "Seeds reference data when missing: client ICNF, project RUBUS, Portuguese 2026 holidays, and SheetUser + RUBUS assignment for the users in Site.SeedTestUsernames. Safe to run more than once. Run by Timer_Bootstrap.";
    bootstrap.Folder = null; // default value
    bootstrap.Function = false; // default value
    bootstrap.Icon = null; // default value
    bootstrap.Public = false; // default value
    
    var clientId = bootstrap.CreateLocalVariable("ClientId");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client.IdentifierType;
    clientId.Description = null; // default value
    
    var projectId = bootstrap.CreateLocalVariable("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.Description = null; // default value
    
    var newClient = bootstrap.CreateLocalVariable("NewClient");
    newClient.DataType = client;
    newClient.Description = null; // default value
    
    var newProject = bootstrap.CreateLocalVariable("NewProject");
    newProject.DataType = project;
    newProject.Description = null; // default value
    
    var newAssignment = bootstrap.CreateLocalVariable("NewAssignment");
    var projectAssignment = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    newAssignment.DataType = projectAssignment;
    newAssignment.Description = null; // default value
    
    var seedList = bootstrap.CreateLocalVariable("SeedList");
    seedList.DataType = eSpace.TextType;
    seedList.Description = null; // default value
    
    var startNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var getSeedClient = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedClient").ConnectedBelow(startNode, 1371);
    getSeedClient.CacheInMinutes = null; // default value
    getSeedClient.Description = null; // default value
    getSeedClient.IsDisabled = false; // default value
    getSeedClient.SetMaxRecords(null); // default value
    getSeedClient.SetStartIndex(null); // default value
    getSeedClient.Timeout = null; // default value
    var client2 = getSeedClient.AsDatabaseAggregate.CreateSource(client);
    getSeedClient.AsDatabaseAggregate.CreateFilter("Client.Name = \"ICNF\"");
    
    
    var ifNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedClient, 1371);
    ifNode.SetCondition("GetSeedClient.List.Empty");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Client missing?";
    
    var assignNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("ClientId", "GetSeedClient.List.Current.Client.Id");
    ifNode.FalseTarget = assignNode;
    
    var assignNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("NewClient.Name", "\"ICNF\"");
    
    var assignment3 = assignNode2.CreateAssignment("NewClient.IsActive", "True");
    ifNode.TrueTarget = assignNode2;
    
    var getSeedProject = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedProject");
    getSeedProject.CacheInMinutes = null; // default value
    getSeedProject.Description = null; // default value
    getSeedProject.HorizontalPosition = 3200;
    getSeedProject.IsDisabled = false; // default value
    getSeedProject.SetMaxRecords(null); // default value
    getSeedProject.SetStartIndex(null); // default value
    getSeedProject.Timeout = null; // default value
    getSeedProject.VerticalPosition = 9026;
    var project2 = getSeedProject.AsDatabaseAggregate.CreateSource(project);
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.ClientId = ClientId");
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.Code = \"RUBUS\"");
    
    assignNode.Target = getSeedProject;
    
    var createClient = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateClient").ConnectedBelow(assignNode2, 1371);
    createClient.Action = client.CreateAction;
    createClient.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createClient.IsDisabled = false; // default value
    createClient.ServerRequestTimeout = null; // default value
    var createClient2 = client.CreateAction;
    var source = createClient2.InputParameters.Named("Source");
    createClient.SetArgumentValue(source, "NewClient");
    
    var ifNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedProject, 1371);
    ifNode2.SetCondition("GetSeedProject.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Project missing?";
    
    var assignNode3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createClient, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.Target = getSeedProject;
    
    var assignment4 = assignNode3.CreateAssignment("ClientId", "CreateClient.Id");
    
    var assignNode4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment5 = assignNode4.CreateAssignment("ProjectId", "GetSeedProject.List.Current.Project.Id");
    ifNode2.FalseTarget = assignNode4;
    
    var assignNode5 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment6 = assignNode5.CreateAssignment("NewProject.ClientId", "ClientId");
    
    var assignment7 = assignNode5.CreateAssignment("NewProject.Name", "\"Resina e Pinha\"");
    
    var assignment8 = assignNode5.CreateAssignment("NewProject.Code", "\"RUBUS\"");
    
    var assignment9 = assignNode5.CreateAssignment("NewProject.IsActive", "True");
    ifNode2.TrueTarget = assignNode5;
    
    var holiday_CreateIfMissing = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing");
    var holiday_CreateIfMissing2 = eSpace.ServerActions.Named("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing.HorizontalPosition = 3200;
    holiday_CreateIfMissing.IsDisabled = false; // default value
    holiday_CreateIfMissing.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing.VerticalPosition = 15881;
    var holidayDate = holiday_CreateIfMissing2.InputParameters.Named("HolidayDate");
    holiday_CreateIfMissing.SetArgumentValue(holidayDate, "NewDate(2026, 1, 1)");
    var name = holiday_CreateIfMissing2.InputParameters.Named("Name");
    holiday_CreateIfMissing.SetArgumentValue(name, "\"Ano Novo\"");
    assignNode4.Target = holiday_CreateIfMissing;
    
    var createProject = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateProject").ConnectedBelow(assignNode5, 1371);
    createProject.Action = project.CreateAction;
    createProject.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createProject.IsDisabled = false; // default value
    createProject.ServerRequestTimeout = null; // default value
    var createProject2 = project.CreateAction;
    var source2 = createProject2.InputParameters.Named("Source");
    createProject.SetArgumentValue(source2, "NewProject");
    
    var holiday_CreateIfMissing22 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing2").ConnectedBelow(holiday_CreateIfMissing, 1371);
    holiday_CreateIfMissing22.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing22.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing22.IsDisabled = false; // default value
    holiday_CreateIfMissing22.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing22.SetArgumentValue(name, "\"Sexta-feira Santa\"");
    holiday_CreateIfMissing22.SetArgumentValue(holidayDate, "NewDate(2026, 4, 3)");
    
    var assignNode6 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createProject, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.Target = holiday_CreateIfMissing;
    
    var assignment10 = assignNode6.CreateAssignment("ProjectId", "CreateProject.Id");
    
    var holiday_CreateIfMissing3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing3").ConnectedBelow(holiday_CreateIfMissing22, 1371);
    holiday_CreateIfMissing3.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing3.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing3.IsDisabled = false; // default value
    holiday_CreateIfMissing3.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing3.SetArgumentValue(holidayDate, "NewDate(2026, 4, 5)");
    holiday_CreateIfMissing3.SetArgumentValue(name, "\"Páscoa\"");
    
    var holiday_CreateIfMissing4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing4").ConnectedBelow(holiday_CreateIfMissing3, 1371);
    holiday_CreateIfMissing4.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing4.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing4.IsDisabled = false; // default value
    holiday_CreateIfMissing4.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing4.SetArgumentValue(name, "\"Dia da Liberdade\"");
    holiday_CreateIfMissing4.SetArgumentValue(holidayDate, "NewDate(2026, 4, 25)");
    
    var holiday_CreateIfMissing5 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing5").ConnectedBelow(holiday_CreateIfMissing4, 1371);
    holiday_CreateIfMissing5.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing5.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing5.IsDisabled = false; // default value
    holiday_CreateIfMissing5.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing5.SetArgumentValue(name, "\"Dia do Trabalhador\"");
    holiday_CreateIfMissing5.SetArgumentValue(holidayDate, "NewDate(2026, 5, 1)");
    
    var holiday_CreateIfMissing6 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing6").ConnectedBelow(holiday_CreateIfMissing5, 1371);
    holiday_CreateIfMissing6.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing6.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing6.IsDisabled = false; // default value
    holiday_CreateIfMissing6.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing6.SetArgumentValue(name, "\"Corpo de Deus\"");
    holiday_CreateIfMissing6.SetArgumentValue(holidayDate, "NewDate(2026, 6, 4)");
    
    var holiday_CreateIfMissing7 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing7").ConnectedBelow(holiday_CreateIfMissing6, 1371);
    holiday_CreateIfMissing7.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing7.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing7.IsDisabled = false; // default value
    holiday_CreateIfMissing7.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing7.SetArgumentValue(name, "\"Dia de Portugal\"");
    holiday_CreateIfMissing7.SetArgumentValue(holidayDate, "NewDate(2026, 6, 10)");
    
    var holiday_CreateIfMissing8 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing8").ConnectedBelow(holiday_CreateIfMissing7, 1371);
    holiday_CreateIfMissing8.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing8.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing8.IsDisabled = false; // default value
    holiday_CreateIfMissing8.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing8.SetArgumentValue(holidayDate, "NewDate(2026, 8, 15)");
    holiday_CreateIfMissing8.SetArgumentValue(name, "\"Assunção de Nossa Senhora\"");
    
    var holiday_CreateIfMissing9 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing9").ConnectedBelow(holiday_CreateIfMissing8, 1371);
    holiday_CreateIfMissing9.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing9.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing9.IsDisabled = false; // default value
    holiday_CreateIfMissing9.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing9.SetArgumentValue(name, "\"Implantação da República\"");
    holiday_CreateIfMissing9.SetArgumentValue(holidayDate, "NewDate(2026, 10, 5)");
    
    var holiday_CreateIfMissing10 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing10").ConnectedBelow(holiday_CreateIfMissing9, 1371);
    holiday_CreateIfMissing10.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing10.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing10.IsDisabled = false; // default value
    holiday_CreateIfMissing10.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing10.SetArgumentValue(name, "\"Dia de Todos-os-Santos\"");
    holiday_CreateIfMissing10.SetArgumentValue(holidayDate, "NewDate(2026, 11, 1)");
    
    var holiday_CreateIfMissing11 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing11").ConnectedBelow(holiday_CreateIfMissing10, 1371);
    holiday_CreateIfMissing11.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing11.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing11.IsDisabled = false; // default value
    holiday_CreateIfMissing11.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing11.SetArgumentValue(name, "\"Restauração da Independência\"");
    holiday_CreateIfMissing11.SetArgumentValue(holidayDate, "NewDate(2026, 12, 1)");
    
    var holiday_CreateIfMissing12 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing12").ConnectedBelow(holiday_CreateIfMissing11, 1371);
    holiday_CreateIfMissing12.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing12.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing12.IsDisabled = false; // default value
    holiday_CreateIfMissing12.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing12.SetArgumentValue(name, "\"Imaculada Conceição\"");
    holiday_CreateIfMissing12.SetArgumentValue(holidayDate, "NewDate(2026, 12, 8)");
    
    var holiday_CreateIfMissing13 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing13").ConnectedBelow(holiday_CreateIfMissing12, 1371);
    holiday_CreateIfMissing13.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing13.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing13.IsDisabled = false; // default value
    holiday_CreateIfMissing13.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing13.SetArgumentValue(holidayDate, "NewDate(2026, 12, 25)");
    holiday_CreateIfMissing13.SetArgumentValue(name, "\"Natal\"");
    
    var assignNode7 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(holiday_CreateIfMissing13, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    
    var assignment11 = assignNode7.CreateAssignment("SeedList", "\",\" + Replace(Site.SeedTestUsernames, \" \", \"\") + \",\"");
    
    var getSeedUsers = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedUsers").ConnectedBelow(assignNode7, 1371);
    getSeedUsers.CacheInMinutes = null; // default value
    getSeedUsers.Description = null; // default value
    getSeedUsers.IsDisabled = false; // default value
    getSeedUsers.SetMaxRecords(null); // default value
    getSeedUsers.SetStartIndex(null); // default value
    getSeedUsers.Timeout = null; // default value
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getSeedUsers.AsDatabaseAggregate.CreateSource(user2);
    getSeedUsers.AsDatabaseAggregate.CreateFilter("User.Is_Active");
    getSeedUsers.AsDatabaseAggregate.CreateFilter("Index(SeedList, \",\" + User.Username + \",\") >= 0");
    
    
    var forEachNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getSeedUsers, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetSeedUsers.List");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode3.SetCondition("CheckSheetUserRole(GetSeedUsers.List.Current.User.Id)");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Has SheetUser?";
    forEachNode.CycleTarget = ifNode3;
    
    var endNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 6855);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var grantSheetUserRole = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetUserRole").ToTheRightOf(ifNode3, 1828);
    var sheetUser = eSpace.Roles.Named("SheetUser");
    var grantSheetUserRole2 = sheetUser.GrantRoleAction;
    grantSheetUserRole.Action = grantSheetUserRole2;
    grantSheetUserRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    grantSheetUserRole.IsDisabled = false; // default value
    grantSheetUserRole.ServerRequestTimeout = null; // default value
    var userId = grantSheetUserRole2.InputParameters.Named("UserId");
    grantSheetUserRole.SetArgumentValue(userId, "GetSeedUsers.List.Current.User.Id");
    ifNode3.FalseTarget = grantSheetUserRole;
    
    var getSeedAssignment = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedAssignment").Below(ifNode3, 1371);
    getSeedAssignment.CacheInMinutes = null; // default value
    getSeedAssignment.Description = null; // default value
    getSeedAssignment.IsDisabled = false; // default value
    getSeedAssignment.SetMaxRecords(null); // default value
    getSeedAssignment.SetStartIndex(null); // default value
    getSeedAssignment.Timeout = null; // default value
    var projectAssignment2 = getSeedAssignment.AsDatabaseAggregate.CreateSource(projectAssignment);
    getSeedAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = GetSeedUsers.List.Current.User.Id");
    getSeedAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.ProjectId = ProjectId");
    
    ifNode3.TrueTarget = getSeedAssignment;
    grantSheetUserRole.Target = getSeedAssignment;
    
    var ifNode4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedAssignment, 1371);
    ifNode4.SetCondition("GetSeedAssignment.List.Empty");
    ifNode4.FalseTarget = forEachNode;
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Assignment missing?";
    
    var assignNode8 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    
    var assignment12 = assignNode8.CreateAssignment("NewAssignment.UserId", "GetSeedUsers.List.Current.User.Id");
    
    var assignment13 = assignNode8.CreateAssignment("NewAssignment.ProjectId", "ProjectId");
    
    var assignment14 = assignNode8.CreateAssignment("NewAssignment.StartDate", "NewDate(2026, 1, 1)");
    
    var assignment15 = assignNode8.CreateAssignment("NewAssignment.EndDate", "NullDate()");
    
    var assignment16 = assignNode8.CreateAssignment("NewAssignment.IsActive", "True");
    ifNode4.TrueTarget = assignNode8;
    
    var createProjectAssignment = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateProjectAssignment").ConnectedBelow(assignNode8, 1371);
    createProjectAssignment.Action = projectAssignment.CreateAction;
    createProjectAssignment.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createProjectAssignment.IsDisabled = false; // default value
    createProjectAssignment.ServerRequestTimeout = null; // default value
    createProjectAssignment.Target = forEachNode;
    var createProjectAssignment2 = projectAssignment.CreateAction;
    var source3 = createProjectAssignment2.InputParameters.Named("Source");
    createProjectAssignment.SetArgumentValue(source3, "NewAssignment");
  }
  { // Timer
    
    /*** creating ITimer 'Timer_Bootstrap' ***/
    var timer_Bootstrap = eSpace.CreateTimer("Timer_Bootstrap");
    var bootstrap = eSpace.ServerActions.Named("Bootstrap");
    timer_Bootstrap.Action = bootstrap;
    timer_Bootstrap.CronSchedule = "WhenPublished";
    timer_Bootstrap.Description = "Seeds reference data (ICNF, RUBUS, PT 2026 holidays, test users) on every publish. Bootstrap only inserts what is missing.";
    timer_Bootstrap.Folder = null; // default value
    timer_Bootstrap.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    timer_Bootstrap.Priority = OutSystems.Model.Enumerations.Priority.Normal; // default value
    timer_Bootstrap.Schedule = "WhenPublished";
    timer_Bootstrap.TimeoutInMinutes = 20; // default value
  }
}
