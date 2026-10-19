(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DaySheet_GetDetail' ***/
    var daySheet_GetDetail = eSpace.CreateServerAction("DaySheet_GetDetail");
    daySheet_GetDetail.CacheInMinutes = null; // default value
    daySheet_GetDetail.Description = "Day editor data for the current user: DaySheet status and notes, holiday, and one line per project assigned on Date (GetUserProjects) merged with the existing TimeEntry minutes and description.";
    var daySheet = eSpace.Folders.Named("DaySheet");
    daySheet_GetDetail.Folder = daySheet;
    daySheet_GetDetail.Function = false; // default value
    daySheet_GetDetail.Icon = null; // default value
    daySheet_GetDetail.Public = true;
    daySheet_GetDetail.Shareable = true;
    
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
    var daySheet3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet2 = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet3);
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
    var daySheet4 = getDayEntries.AsDatabaseAggregate.CreateSource(daySheet3);
    var join = getDayEntries.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet4;
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
    assignNode.Label = "Fill header";
    
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
    forEachNode.Label = "Each line";
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Detail.Lines");
    forEachNode.SetStartIndex(null); // default value
    
    var forEachNode2 = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ToTheRightOf(forEachNode, 1828);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = "Each entry";
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
    assignNode2.Label = "Merge entry";
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
}
