(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'FillPendingDays' ***/
    var fillPendingDays = eSpace.CreateServerAction("FillPendingDays");
    fillPendingDays.CacheInMinutes = null; // default value
    fillPendingDays.Description = "Fills and confirms every Pending day of the month for the current user: a Draft with a total > 0 is confirmed as is; any other Pending day is saved through SaveDaySheet with one TimeEntry (ProjectId, MinutesPerDay) and confirmed. Runs in one request, so any error rolls back every day. Returns the number of days filled.";
    fillPendingDays.Folder = null; // default value
    fillPendingDays.Function = false; // default value
    fillPendingDays.Icon = null; // default value
    fillPendingDays.Public = false; // default value
    fillPendingDays.Shareable = false; // default value
    
    var year = fillPendingDays.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = fillPendingDays.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var projectId = fillPendingDays.CreateInputParameter("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.Description = null; // default value
    projectId.IsMandatory = true; // default value
    
    var minutesPerDay = fillPendingDays.CreateInputParameter("MinutesPerDay");
    minutesPerDay.DataType = eSpace.IntegerType;
    minutesPerDay.Description = null; // default value
    minutesPerDay.IsMandatory = true; // default value
    
    var entry = fillPendingDays.CreateLocalVariable("Entry");
    var timeEntryInput = eSpace.Structures.Named("TimeEntryInput");
    entry.DataType = timeEntryInput;
    entry.Description = null; // default value
    
    var oneEntry = fillPendingDays.CreateLocalVariable("OneEntry");
    oneEntry.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    oneEntry.Description = null; // default value
    
    var sheetRec = fillPendingDays.CreateLocalVariable("SheetRec");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    sheetRec.DataType = daySheet;
    sheetRec.Description = null; // default value
    
    var startNode = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>();
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.HorizontalPosition = 5028;
    raiseExceptionNode.IsDisabled = false; // default value
    raiseExceptionNode.VerticalPosition = 3542;
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getMonthCalendar = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthCalendar").Below(ifNode, 1371);
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
    
    var assignNode = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getMonthCalendar, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Entry.ProjectId", "ProjectId");
    
    var assignment2 = assignNode.CreateAssignment("Entry.Minutes", "MinutesPerDay");
    
    var assignment3 = assignNode.CreateAssignment("FilledCount", "0");
    
    var listAppend = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
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
    
    var forEachNode = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(listAppend, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetMonthCalendar.Days");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode2 = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 1828);
    ifNode2.SetCondition("GetMonthCalendar.Days.Current.DayTypeId = Entities.CalendarDayType.Pending");
    ifNode2.FalseTarget = forEachNode;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Pending?";
    forEachNode.CycleTarget = ifNode2;
    
    var endNode = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 1371);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var ifNode3 = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode3.SetCondition("GetMonthCalendar.Days.Current.DaySheetId <> NullIdentifier() and GetMonthCalendar.Days.Current.TotalMinutes > 0");
    ifNode3.HorizontalPosition = 6856;
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Draft with hours?";
    ifNode3.VerticalPosition = 9026;
    ifNode2.TrueTarget = ifNode3;
    
    var saveDaySheet = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("SaveDaySheet");
    var saveDaySheet2 = eSpace.ServerActions.Named("SaveDaySheet");
    saveDaySheet.Action = saveDaySheet2;
    saveDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    saveDaySheet.HorizontalPosition = 8684;
    saveDaySheet.IsDisabled = false; // default value
    saveDaySheet.ServerRequestTimeout = null; // default value
    saveDaySheet.VerticalPosition = 10397;
    var entries = saveDaySheet2.InputParameters.Named("Entries");
    saveDaySheet.SetArgumentValue(entries, "OneEntry");
    var isDayOff = saveDaySheet2.InputParameters.Named("IsDayOff");
    saveDaySheet.SetArgumentValue(isDayOff, "False");
    var notes = saveDaySheet2.InputParameters.Named("Notes");
    saveDaySheet.SetArgumentValue(notes, "\"\"");
    var confirm = saveDaySheet2.InputParameters.Named("Confirm");
    saveDaySheet.SetArgumentValue(confirm, "True");
    var date = saveDaySheet2.InputParameters.Named("Date");
    saveDaySheet.SetArgumentValue(date, "GetMonthCalendar.Days.Current.Date");
    ifNode3.FalseTarget = saveDaySheet;
    
    var getDaySheetForUpdate = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetDaySheetForUpdate").Below(ifNode3, 1371);
    getDaySheetForUpdate.Action = daySheet.GetForUpdateAction;
    getDaySheetForUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getDaySheetForUpdate.IsDisabled = false; // default value
    getDaySheetForUpdate.ServerRequestTimeout = null; // default value
    var getDaySheetForUpdate2 = daySheet.GetForUpdateAction;
    var id = getDaySheetForUpdate2.InputParameters.Named("Id");
    getDaySheetForUpdate.SetArgumentValue(id, "GetMonthCalendar.Days.Current.DaySheetId");
    ifNode3.TrueTarget = getDaySheetForUpdate;
    
    var assignNode2 = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(saveDaySheet, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode;
    
    var assignment4 = assignNode2.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var assignNode3 = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getDaySheetForUpdate, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment5 = assignNode3.CreateAssignment("SheetRec", "GetDaySheetForUpdate.Record");
    
    var assignment6 = assignNode3.CreateAssignment("SheetRec.DayStatusId", "Entities.DayStatus.Confirmed");
    
    var assignment7 = assignNode3.CreateAssignment("SheetRec.UpdatedOn", "CurrDateTime()");
    
    var updateDaySheet = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateDaySheet").ConnectedBelow(assignNode3, 1371);
    updateDaySheet.Action = daySheet.UpdateAction;
    updateDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateDaySheet.IsDisabled = false; // default value
    updateDaySheet.ServerRequestTimeout = null; // default value
    var updateDaySheet2 = daySheet.UpdateAction;
    var source = updateDaySheet2.InputParameters.Named("Source");
    updateDaySheet.SetArgumentValue(source, "SheetRec");
    
    var assignNode4 = fillPendingDays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(updateDaySheet, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.Target = forEachNode;
    
    var assignment8 = assignNode4.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var filledCount = fillPendingDays.CreateOutputParameter("FilledCount");
    filledCount.DataType = eSpace.IntegerType;
    filledCount.SetDefaultValue(null); // default value
    filledCount.Description = null; // default value
}
