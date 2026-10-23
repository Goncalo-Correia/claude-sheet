(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DaySheet_FillPending' ***/
    var daySheet_FillPending = eSpace.CreateServerAction("DaySheet_FillPending");
    daySheet_FillPending.CacheInMinutes = null; // default value
    daySheet_FillPending.Description = "Fills and confirms every Pending day of the month for the current user: a Draft with a total > 0 is confirmed as is; any other Pending day on which the user is assigned to ProjectId (active project and client) is saved through DaySheet_Save with one TimeEntry (ProjectId, MinutesPerDay) and confirmed; Pending days without that assignment are skipped. Runs in one request, so any other error rolls back every day. Returns the number of days filled and skipped. Refused (DaySheetValidation) when the month is Submitted or Approved.";
    var daySheet = eSpace.Folders.Named("DaySheet");
    daySheet_FillPending.Folder = daySheet;
    daySheet_FillPending.Function = false; // default value
    daySheet_FillPending.Icon = null; // default value
    daySheet_FillPending.Public = true;
    daySheet_FillPending.Shareable = true;
    
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
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    sheetRec.DataType = daySheet2;
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
    
    var ifNode2 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("MonthSheet_IsLocked(GetUserId(), Year, Month)");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Month locked?";
    ifNode.TrueTarget = ifNode2;
    
    var calendar_GetMonth = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").Below(ifNode2, 1371);
    var calendar_GetMonth2 = eSpace.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    calendar_GetMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonth.IsDisabled = false; // default value
    calendar_GetMonth.ServerRequestTimeout = null; // default value
    var month2 = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month2, "Month");
    var year2 = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year2, "Year");
    ifNode2.FalseTarget = calendar_GetMonth;
    
    var raiseExceptionNode2 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var daySheetValidation = eSpace.UserExceptions.Named("DaySheetValidation");
    raiseExceptionNode2.Exception = daySheetValidation;
    raiseExceptionNode2.SetExceptionMessage("\"This month is submitted or approved.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var assignNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonth, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Init entry";
    
    var assignment = assignNode.CreateAssignment("Entry.ProjectId", "ProjectId");
    
    var assignment2 = assignNode.CreateAssignment("Entry.Minutes", "MinutesPerDay");
    
    var assignment3 = assignNode.CreateAssignment("FilledCount", "0");
    
    var assignment4 = assignNode.CreateAssignment("SkippedCount", "0");
    
    var listAppend = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Entry");
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "OneEntry");
    
    var forEachNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(listAppend, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = "Each day";
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Calendar_GetMonth.Days");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode3 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode3.SetCondition("Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Pending");
    ifNode3.FalseTarget = forEachNode;
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "!Pending?";
    forEachNode.CycleTarget = ifNode3;
    
    var endNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 9597);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var ifNode4 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode3, 1371);
    ifNode4.SetCondition("Calendar_GetMonth.Days.Current.DaySheetId <> NullIdentifier() and Calendar_GetMonth.Days.Current.TotalMinutes > 0");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Draft with hours?";
    ifNode3.TrueTarget = ifNode4;
    
    var getAssignmentOnDay = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignmentOnDay").Below(ifNode4, 1371);
    getAssignmentOnDay.CacheInMinutes = null; // default value
    getAssignmentOnDay.Description = null; // default value
    getAssignmentOnDay.IsDisabled = false; // default value
    getAssignmentOnDay.SetMaxRecords("1");
    getAssignmentOnDay.SetStartIndex(null); // default value
    getAssignmentOnDay.Timeout = null; // default value
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    var projectAssignment = getAssignmentOnDay.AsDatabaseAggregate.CreateSource(projectAssignment2);
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client = getAssignmentOnDay.AsDatabaseAggregate.CreateSource(client2);
    var project2 = getAssignmentOnDay.AsDatabaseAggregate.CreateSource(project);
    var join = getAssignmentOnDay.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = projectAssignment;
    join.RightSource = project2;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("ProjectAssignment.ProjectId = Project.Id");
    var join2 = getAssignmentOnDay.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = project2;
    join2.RightSource = client;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Project.ClientId = Client.Id");
    getAssignmentOnDay.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = GetUserId()");
    getAssignmentOnDay.AsDatabaseAggregate.CreateFilter("ProjectAssignment.ProjectId = ProjectId");
    getAssignmentOnDay.AsDatabaseAggregate.CreateFilter("ProjectAssignment.IsActive");
    getAssignmentOnDay.AsDatabaseAggregate.CreateFilter("ProjectAssignment.StartDate <= Calendar_GetMonth.Days.Current.Date");
    getAssignmentOnDay.AsDatabaseAggregate.CreateFilter("ProjectAssignment.EndDate = NullDate() or ProjectAssignment.EndDate >= Calendar_GetMonth.Days.Current.Date");
    getAssignmentOnDay.AsDatabaseAggregate.CreateFilter("Project.IsActive");
    getAssignmentOnDay.AsDatabaseAggregate.CreateFilter("Client.IsActive");
    
    ifNode4.FalseTarget = getAssignmentOnDay;
    
    var getDaySheetForUpdate = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetDaySheetForUpdate").ToTheRightOf(ifNode4, 1828);
    getDaySheetForUpdate.Action = daySheet2.GetForUpdateAction;
    getDaySheetForUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getDaySheetForUpdate.IsDisabled = false; // default value
    getDaySheetForUpdate.ServerRequestTimeout = null; // default value
    var getDaySheetForUpdate2 = daySheet2.GetForUpdateAction;
    var id = getDaySheetForUpdate2.InputParameters.Named("Id");
    getDaySheetForUpdate.SetArgumentValue(id, "Calendar_GetMonth.Days.Current.DaySheetId");
    ifNode4.TrueTarget = getDaySheetForUpdate;
    
    var ifNode5 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getAssignmentOnDay, 1371);
    ifNode5.SetCondition("not GetAssignmentOnDay.List.Empty");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "Assigned on day?";
    
    var assignNode2 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getDaySheetForUpdate, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Confirm draft";
    
    var assignment5 = assignNode2.CreateAssignment("SheetRec", "GetDaySheetForUpdate.Record");
    
    var assignment6 = assignNode2.CreateAssignment("SheetRec.DayStatusId", "Entities.DayStatus.Confirmed");
    
    var assignment7 = assignNode2.CreateAssignment("SheetRec.UpdatedOn", "CurrDateTime()");
    
    var assignNode3 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheLeftOf(ifNode5, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Count skipped";
    assignNode3.Target = forEachNode;
    
    var assignment8 = assignNode3.CreateAssignment("SkippedCount", "SkippedCount + 1");
    ifNode5.FalseTarget = assignNode3;
    
    var daySheet_Save = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_Save").Below(ifNode5, 1371);
    var daySheet_Save2 = eSpace.ServerActions.Named("DaySheet_Save");
    daySheet_Save.Action = daySheet_Save2;
    daySheet_Save.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    daySheet_Save.IsDisabled = false; // default value
    daySheet_Save.ServerRequestTimeout = null; // default value
    var notes = daySheet_Save2.InputParameters.Named("Notes");
    daySheet_Save.SetArgumentValue(notes, "\"\"");
    var date = daySheet_Save2.InputParameters.Named("Date");
    daySheet_Save.SetArgumentValue(date, "Calendar_GetMonth.Days.Current.Date");
    var isDayOff = daySheet_Save2.InputParameters.Named("IsDayOff");
    daySheet_Save.SetArgumentValue(isDayOff, "False");
    var confirm = daySheet_Save2.InputParameters.Named("Confirm");
    daySheet_Save.SetArgumentValue(confirm, "True");
    var entries = daySheet_Save2.InputParameters.Named("Entries");
    daySheet_Save.SetArgumentValue(entries, "OneEntry");
    ifNode5.TrueTarget = daySheet_Save;
    
    var updateDaySheet = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateDaySheet").ConnectedBelow(assignNode2, 5484);
    updateDaySheet.Action = daySheet2.UpdateAction;
    updateDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateDaySheet.IsDisabled = false; // default value
    updateDaySheet.ServerRequestTimeout = null; // default value
    var updateDaySheet2 = daySheet2.UpdateAction;
    var source = updateDaySheet2.InputParameters.Named("Source");
    updateDaySheet.SetArgumentValue(source, "SheetRec");
    
    var assignNode4 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedToTheLeftOf(daySheet_Save, 1828);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Count filled";
    assignNode4.Target = forEachNode;
    
    var assignment9 = assignNode4.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var assignNode5 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedToTheLeftOf(updateDaySheet, 3656);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Count filled";
    assignNode5.Target = forEachNode;
    
    var assignment10 = assignNode5.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var filledCount = daySheet_FillPending.CreateOutputParameter("FilledCount");
    filledCount.DataType = eSpace.IntegerType;
    filledCount.SetDefaultValue(null); // default value
    filledCount.Description = null; // default value
    
    var skippedCount = daySheet_FillPending.CreateOutputParameter("SkippedCount");
    skippedCount.DataType = eSpace.IntegerType;
    skippedCount.SetDefaultValue(null); // default value
    skippedCount.Description = null; // default value
}
