(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DaySheet_Save' ***/
    var daySheet_Save = eSpace.CreateServerAction("DaySheet_Save");
    daySheet_Save.CacheInMinutes = null; // default value
    daySheet_Save.Description = "Validates and saves the current user\'s day: weekday, not a holiday, no hours in the future, projects assigned on Date, minutes multiple of 15, total <= 24:00, Confirm needs a total > 0. Upserts the DaySheet (DayOff / Confirmed / Draft) and its TimeEntries; IsDayOff deletes the day\'s entries. Raises DaySheetValidation with a user-facing message.";
    daySheet_Save.Folder = null; // default value
    daySheet_Save.Function = false; // default value
    daySheet_Save.Icon = null; // default value
    daySheet_Save.Public = false; // default value
    daySheet_Save.Shareable = false; // default value
    
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
    notes.SetDefaultValue(null); // default value
    notes.Description = null; // default value
    notes.IsMandatory = false;
    
    var total = daySheet_Save.CreateLocalVariable("Total");
    total.DataType = eSpace.IntegerType;
    total.SetDefaultValue(null); // default value
    total.Description = null; // default value
    
    var found = daySheet_Save.CreateLocalVariable("Found");
    found.DataType = eSpace.BooleanType;
    found.SetDefaultValue(null); // default value
    found.Description = null; // default value
    
    var sheetId = daySheet_Save.CreateLocalVariable("SheetId");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    sheetId.DataType = daySheet.IdentifierType;
    sheetId.SetDefaultValue(null); // default value
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
    
    var raiseExceptionNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>();
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.HorizontalPosition = 5028;
    raiseExceptionNode.IsDisabled = false; // default value
    raiseExceptionNode.VerticalPosition = 3542;
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("Mod(DayOfWeek(Date) + 6, 7) >= 5");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Weekend?";
    ifNode.TrueTarget = ifNode2;
    
    var getDayHoliday = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayHoliday");
    getDayHoliday.CacheInMinutes = null; // default value
    getDayHoliday.Description = null; // default value
    getDayHoliday.HorizontalPosition = 5028;
    getDayHoliday.IsDisabled = false; // default value
    getDayHoliday.SetMaxRecords(null); // default value
    getDayHoliday.SetStartIndex(null); // default value
    getDayHoliday.Timeout = null; // default value
    getDayHoliday.VerticalPosition = 4913;
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getDayHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = Date");
    
    ifNode2.FalseTarget = getDayHoliday;
    
    var raiseExceptionNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode2, 1371);
    var daySheetValidation = eSpace.UserExceptions.Named("DaySheetValidation");
    raiseExceptionNode2.Exception = daySheetValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Nothing can be saved on a weekend.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayHoliday, 1371);
    ifNode3.SetCondition("not GetDayHoliday.List.Empty");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "!Holiday?";
    
    var project_GetAssignedToUser = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetAssignedToUser");
    var project_GetAssignedToUser2 = eSpace.ServerActions.Named("Project_GetAssignedToUser");
    project_GetAssignedToUser.Action = project_GetAssignedToUser2;
    project_GetAssignedToUser.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetAssignedToUser.HorizontalPosition = 6856;
    project_GetAssignedToUser.IsDisabled = false; // default value
    project_GetAssignedToUser.ServerRequestTimeout = null; // default value
    project_GetAssignedToUser.VerticalPosition = 7655;
    var forDate = project_GetAssignedToUser2.InputParameters.Named("ForDate");
    project_GetAssignedToUser.SetArgumentValue(forDate, "Date");
    ifNode3.FalseTarget = project_GetAssignedToUser;
    
    var raiseExceptionNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode3, 1371);
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
    
    var ifNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 1828);
    ifNode4.SetCondition("Entries.Current.Minutes < 0 or Mod(Entries.Current.Minutes, 15) <> 0");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "!Invalid minutes?";
    forEachNode.CycleTarget = ifNode4;
    
    var ifNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode, 1371);
    ifNode5.SetCondition("not IsDayOff and Total > 0 and Date > CurrDate()");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "!Hours in the future?";
    forEachNode.Target = ifNode5;
    
    var assignNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 10512;
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    assignNode.VerticalPosition = 10397;
    
    var assignment = assignNode.CreateAssignment("Total", "Total + Entries.Current.Minutes");
    
    var assignment2 = assignNode.CreateAssignment("Found", "False");
    ifNode4.FalseTarget = assignNode;
    
    var raiseExceptionNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode4, 1371);
    raiseExceptionNode4.Exception = daySheetValidation;
    raiseExceptionNode4.SetExceptionMessage("\"Minutes must be a multiple of 15.\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var ifNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode6.SetCondition("not IsDayOff and Total > 1440");
    ifNode6.HorizontalPosition = 8684;
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = "!Over 24:00?";
    ifNode6.VerticalPosition = 11768;
    ifNode5.FalseTarget = ifNode6;
    
    var raiseExceptionNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode5, 1371);
    raiseExceptionNode5.Exception = daySheetValidation;
    raiseExceptionNode5.SetExceptionMessage("\"Hours cannot be logged in the future.\"");
    raiseExceptionNode5.IsDisabled = false; // default value
    ifNode5.TrueTarget = raiseExceptionNode5;
    
    var ifNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode7.SetCondition("Entries.Current.Minutes > 0");
    ifNode7.FalseTarget = forEachNode;
    ifNode7.IsDisabled = false; // default value
    ifNode7.Label = "Has minutes?";
    
    var ifNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode8.SetCondition("Confirm and not IsDayOff and Total = 0");
    ifNode8.HorizontalPosition = 10512;
    ifNode8.IsDisabled = false; // default value
    ifNode8.Label = "!Confirm without hours?";
    ifNode8.VerticalPosition = 13139;
    ifNode6.FalseTarget = ifNode8;
    
    var raiseExceptionNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode6, 1371);
    raiseExceptionNode6.Exception = daySheetValidation;
    raiseExceptionNode6.SetExceptionMessage("\"A day cannot exceed 24:00\"");
    raiseExceptionNode6.IsDisabled = false; // default value
    ifNode6.TrueTarget = raiseExceptionNode6;
    
    var forEachNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>();
    forEachNode2.HorizontalPosition = 12340;
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("Project_GetAssignedToUser.Projects");
    forEachNode2.SetStartIndex(null); // default value
    forEachNode2.VerticalPosition = 13139;
    ifNode7.TrueTarget = forEachNode2;
    
    var getDayRecord = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord");
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.HorizontalPosition = 12340;
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    getDayRecord.VerticalPosition = 14510;
    var daySheet2 = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode8.FalseTarget = getDayRecord;
    
    var raiseExceptionNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode8, 1371);
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
    
    var ifNode10 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode10.SetCondition("not Found");
    ifNode10.FalseTarget = forEachNode;
    ifNode10.HorizontalPosition = 14168;
    ifNode10.IsDisabled = false; // default value
    ifNode10.Label = "!Not assigned?";
    ifNode10.VerticalPosition = 14510;
    forEachNode2.Target = ifNode10;
    
    var ifNode11 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode11.SetCondition("GetDayRecord.List.Empty");
    ifNode11.IsDisabled = false; // default value
    ifNode11.Label = "!New day sheet?";
    
    var assignNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode2.HorizontalPosition = 15996;
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode2;
    assignNode2.VerticalPosition = 14510;
    
    var assignment3 = assignNode2.CreateAssignment("Found", "True");
    ifNode9.TrueTarget = assignNode2;
    
    var raiseExceptionNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>();
    raiseExceptionNode8.Exception = daySheetValidation;
    raiseExceptionNode8.SetExceptionMessage("\"You are not assigned to one of these projects on this day.\"");
    raiseExceptionNode8.HorizontalPosition = 10512;
    raiseExceptionNode8.IsDisabled = false; // default value
    raiseExceptionNode8.VerticalPosition = 15881;
    ifNode10.TrueTarget = raiseExceptionNode8;
    
    var assignNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode3.HorizontalPosition = 14168;
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.VerticalPosition = 17252;
    
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
    
    var assignNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    
    var assignment12 = assignNode7.CreateAssignment("Found", "False");
    forEachNode3.CycleTarget = assignNode7;
    
    var ifNode12 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode3, 1371);
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
    
    var endNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 14168;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 26849;
    ifNode12.FalseTarget = endNode;
    
    var forEachNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(ifNode12, 1371);
    forEachNode5.IsDisabled = false; // default value
    forEachNode5.Label = null; // default value
    forEachNode5.SetMaximumIterations(null); // default value
    forEachNode5.SetRecordList("Entries");
    forEachNode5.SetStartIndex(null); // default value
    forEachNode5.Target = endNode;
    ifNode12.TrueTarget = forEachNode5;
    
    var ifNode13 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode4, 1828);
    ifNode13.SetCondition("not IsDayOff and Entries.Current.ProjectId = GetSheetEntries.List.Current.TimeEntry.ProjectId and Entries.Current.Minutes > 0");
    ifNode13.FalseTarget = forEachNode4;
    ifNode13.IsDisabled = false; // default value
    ifNode13.Label = "!Still sent?";
    forEachNode4.CycleTarget = ifNode13;
    
    var ifNode14 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode14.SetCondition("not Found");
    ifNode14.FalseTarget = forEachNode3;
    ifNode14.HorizontalPosition = 17824;
    ifNode14.IsDisabled = false; // default value
    ifNode14.Label = "!Not sent anymore?";
    ifNode14.VerticalPosition = 26849;
    forEachNode4.Target = ifNode14;
    
    var ifNode15 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode5, 3656);
    ifNode15.SetCondition("Entries.Current.Minutes > 0");
    ifNode15.FalseTarget = forEachNode5;
    ifNode15.IsDisabled = false; // default value
    ifNode15.Label = "!Has minutes?";
    forEachNode5.CycleTarget = ifNode15;
    
    var assignNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode8.HorizontalPosition = 19652;
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    assignNode8.VerticalPosition = 26849;
    
    var assignment13 = assignNode8.CreateAssignment("EntryRec", "GetSheetEntries.List.Current.TimeEntry");
    
    var assignment14 = assignNode8.CreateAssignment("EntryRec.Minutes", "Entries.Current.Minutes");
    
    var assignment15 = assignNode8.CreateAssignment("EntryRec.Description", "Entries.Current.Description");
    
    var assignment16 = assignNode8.CreateAssignment("EntryRec.UpdatedOn", "CurrDateTime()");
    
    var assignment17 = assignNode8.CreateAssignment("Found", "True");
    ifNode13.TrueTarget = assignNode8;
    
    var deleteTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteTimeEntry");
    deleteTimeEntry.Action = timeEntry.DeleteAction;
    deleteTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteTimeEntry.HorizontalPosition = 15996;
    deleteTimeEntry.IsDisabled = false; // default value
    deleteTimeEntry.ServerRequestTimeout = null; // default value
    deleteTimeEntry.Target = forEachNode3;
    deleteTimeEntry.VerticalPosition = 28220;
    var deleteTimeEntry2 = timeEntry.DeleteAction;
    var id = deleteTimeEntry2.InputParameters.Named("Id");
    deleteTimeEntry.SetArgumentValue(id, "GetSheetEntries.List.Current.TimeEntry.Id");
    ifNode14.TrueTarget = deleteTimeEntry;
    
    var assignNode9 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode9.HorizontalPosition = 14168;
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    assignNode9.VerticalPosition = 28220;
    
    var assignment18 = assignNode9.CreateAssignment("Found", "False");
    ifNode15.TrueTarget = assignNode9;
    
    var updateTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateTimeEntry");
    updateTimeEntry.Action = timeEntry.UpdateAction;
    updateTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateTimeEntry.HorizontalPosition = 17824;
    updateTimeEntry.IsDisabled = false; // default value
    updateTimeEntry.ServerRequestTimeout = null; // default value
    updateTimeEntry.Target = forEachNode4;
    updateTimeEntry.VerticalPosition = 28220;
    var updateTimeEntry2 = timeEntry.UpdateAction;
    var source2 = updateTimeEntry2.InputParameters.Named("Source");
    updateTimeEntry.SetArgumentValue(source2, "EntryRec");
    assignNode8.Target = updateTimeEntry;
    
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
    
    var ifNode17 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode6, 1371);
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
}
