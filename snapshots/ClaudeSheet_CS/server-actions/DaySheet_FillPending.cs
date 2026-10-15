(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DaySheet_FillPending' ***/
    var daySheet_FillPending = eSpace.CreateServerAction("DaySheet_FillPending");
    daySheet_FillPending.CacheInMinutes = null; // default value
    daySheet_FillPending.Description = "Fills and confirms every Pending day of the month for the current user: a Draft with a total > 0 is confirmed as is; any other Pending day is saved through SaveDaySheet with one TimeEntry (ProjectId, MinutesPerDay) and confirmed. Runs in one request, so any error rolls back every day. Returns the number of days filled.";
    daySheet_FillPending.Folder = null; // default value
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
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Entry");
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "OneEntry");
    
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
    filledCount.SetDefaultValue(null); // default value
    filledCount.Description = null; // default value
}
