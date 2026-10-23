(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_Submit' ***/
    var monthSheet_Submit = eSpace.CreateServerAction("MonthSheet_Submit");
    monthSheet_Submit.CacheInMinutes = null; // default value
    monthSheet_Submit.Description = "Submits the current user\'s month (SheetUser) for approval. Refused with PeriodValidation when the month hasn\'t ended, when it still has Pending days (the message gives how many), or when it is already Submitted or Approved. A Rejected month can be submitted again; its comment is cleared.";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_Submit.Folder = monthSheet;
    monthSheet_Submit.Function = false; // default value
    monthSheet_Submit.Icon = null; // default value
    monthSheet_Submit.Public = true;
    monthSheet_Submit.Shareable = true;
    
    var year = monthSheet_Submit.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = monthSheet_Submit.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var rec = monthSheet_Submit.CreateLocalVariable("Rec");
    var monthSheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("MonthSheet");
    rec.DataType = monthSheet2;
    rec.Description = null; // default value
    
    var startNode = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("DateTimeToDate(AddMonths(NewDate(Year, Month, 1), 1)) <= CurrDate()");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Month ended?";
    ifNode.TrueTarget = ifNode2;
    
    var raiseExceptionNode2 = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var periodValidation = eSpace.UserExceptions.Named("PeriodValidation");
    raiseExceptionNode2.Exception = periodValidation;
    raiseExceptionNode2.SetExceptionMessage("\"You can submit a month once it has ended.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.FalseTarget = raiseExceptionNode2;
    
    var calendar_GetMonth = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").Below(ifNode2, 1371);
    var calendar_GetMonth2 = eSpace.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    calendar_GetMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonth.IsDisabled = false; // default value
    calendar_GetMonth.ServerRequestTimeout = null; // default value
    var year2 = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year2, "Year");
    var month2 = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month2, "Month");
    ifNode2.TrueTarget = calendar_GetMonth;
    
    var getPendingDays = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetPendingDays").ConnectedBelow(calendar_GetMonth, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listFilter = _System_.ServerActions.Named("ListFilter");
    getPendingDays.Action = listFilter;
    getPendingDays.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getPendingDays.IsDisabled = false; // default value
    getPendingDays.ServerRequestTimeout = null; // default value
    var condition = listFilter.InputParameters.Named("Condition");
    getPendingDays.SetArgumentValue(condition, "Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Pending");
    var sourceList = listFilter.InputParameters.Named("SourceList");
    getPendingDays.SetArgumentValue(sourceList, "Calendar_GetMonth.Days");
    
    var ifNode3 = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getPendingDays, 1371);
    ifNode3.SetCondition("GetPendingDays.FilteredList.Length > 0");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Pending days?";
    
    var getMonthSheet = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthSheet").Below(ifNode3, 1371);
    getMonthSheet.CacheInMinutes = null; // default value
    getMonthSheet.Description = null; // default value
    getMonthSheet.IsDisabled = false; // default value
    getMonthSheet.SetMaxRecords("1");
    getMonthSheet.SetStartIndex(null); // default value
    getMonthSheet.Timeout = null; // default value
    var monthSheet3 = getMonthSheet.AsDatabaseAggregate.CreateSource(monthSheet2);
    getMonthSheet.AsDatabaseAggregate.CreateFilter("MonthSheet.UserId = GetUserId()");
    getMonthSheet.AsDatabaseAggregate.CreateFilter("MonthSheet.Year = Year");
    getMonthSheet.AsDatabaseAggregate.CreateFilter("MonthSheet.Month = Month");
    
    ifNode3.FalseTarget = getMonthSheet;
    
    var raiseExceptionNode3 = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = periodValidation;
    raiseExceptionNode3.SetExceptionMessage("If(GetPendingDays.FilteredList.Length = 1, \"1 day is\", GetPendingDays.FilteredList.Length + \" days are\") + \" still pending. Confirm them or mark them as days off before submitting.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var ifNode4 = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getMonthSheet, 1371);
    ifNode4.SetCondition("not GetMonthSheet.List.Empty and (GetMonthSheet.List.Current.MonthSheet.PeriodStatusId = Entities.PeriodStatus.Submitted or GetMonthSheet.List.Current.MonthSheet.PeriodStatusId = Entities.PeriodStatus.Approved)");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Already submitted?";
    
    var assignNode = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Build submission";
    
    var assignment = assignNode.CreateAssignment("Rec", "GetMonthSheet.List.Current.MonthSheet");
    
    var assignment2 = assignNode.CreateAssignment("Rec.UserId", "GetUserId()");
    
    var assignment3 = assignNode.CreateAssignment("Rec.Year", "Year");
    
    var assignment4 = assignNode.CreateAssignment("Rec.Month", "Month");
    
    var assignment5 = assignNode.CreateAssignment("Rec.PeriodStatusId", "Entities.PeriodStatus.Submitted");
    
    var assignment6 = assignNode.CreateAssignment("Rec.SubmittedOn", "CurrDateTime()");
    
    var assignment7 = assignNode.CreateAssignment("Rec.DecidedBy", "NullIdentifier()");
    
    var assignment8 = assignNode.CreateAssignment("Rec.DecidedOn", "NullDate()");
    
    var assignment9 = assignNode.CreateAssignment("Rec.Comment", "\"\"");
    ifNode4.FalseTarget = assignNode;
    
    var raiseExceptionNode4 = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = periodValidation;
    raiseExceptionNode4.SetExceptionMessage("\"This month is already submitted or approved.\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var createOrUpdateMonthSheet = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateMonthSheet").ConnectedBelow(assignNode, 1371);
    createOrUpdateMonthSheet.Action = monthSheet2.CreateOrUpdateAction;
    createOrUpdateMonthSheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createOrUpdateMonthSheet.IsDisabled = false; // default value
    createOrUpdateMonthSheet.ServerRequestTimeout = null; // default value
    var createOrUpdateMonthSheet2 = monthSheet2.CreateOrUpdateAction;
    var source = createOrUpdateMonthSheet2.InputParameters.Named("Source");
    createOrUpdateMonthSheet.SetArgumentValue(source, "Rec");
    
    var endNode = monthSheet_Submit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(createOrUpdateMonthSheet, 1371);
    endNode.IsDisabled = false; // default value
}
