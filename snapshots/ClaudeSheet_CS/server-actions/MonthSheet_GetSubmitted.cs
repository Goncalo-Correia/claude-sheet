(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_GetSubmitted' ***/
    var monthSheet_GetSubmitted = eSpace.CreateServerAction("MonthSheet_GetSubmitted");
    monthSheet_GetSubmitted.CacheInMinutes = null; // default value
    monthSheet_GetSubmitted.Description = "Submitted months of every user for the Approvals screen (SheetManager), newest month first, then by user name, with hours from MonthSheet_GetTotals. IncludeApproved also returns Approved months.";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_GetSubmitted.Folder = monthSheet;
    monthSheet_GetSubmitted.Function = false; // default value
    monthSheet_GetSubmitted.Icon = null; // default value
    monthSheet_GetSubmitted.Public = true;
    monthSheet_GetSubmitted.Shareable = true;
    
    var includeApproved = monthSheet_GetSubmitted.CreateInputParameter("IncludeApproved");
    includeApproved.DataType = eSpace.BooleanType;
    includeApproved.SetDefaultValue(null); // default value
    includeApproved.Description = null; // default value
    includeApproved.IsMandatory = false;
    
    var line = monthSheet_GetSubmitted.CreateLocalVariable("Line");
    var monthSheetSummary = eSpace.Structures.Named("MonthSheetSummary");
    line.DataType = monthSheetSummary;
    line.Description = null; // default value
    
    var startNode = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetManagerRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetManager?";
    
    var raiseExceptionNode = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetManager");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only managers can see submitted months.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getMonths = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonths").Below(ifNode, 1371);
    getMonths.CacheInMinutes = null; // default value
    getMonths.Description = null; // default value
    getMonths.IsDisabled = false; // default value
    getMonths.SetMaxRecords(null); // default value
    getMonths.SetStartIndex(null); // default value
    getMonths.Timeout = null; // default value
    var monthSheet3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("MonthSheet");
    var monthSheet2 = getMonths.AsDatabaseAggregate.CreateSource(monthSheet3);
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getMonths.AsDatabaseAggregate.CreateSource(user2);
    var join = getMonths.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = monthSheet2;
    join.RightSource = user;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("MonthSheet.UserId = User.Id");
    getMonths.AsDatabaseAggregate.CreateFilter("MonthSheet.PeriodStatusId = Entities.PeriodStatus.Submitted or (IncludeApproved and MonthSheet.PeriodStatusId = Entities.PeriodStatus.Approved)");
    getMonths.AsDatabaseAggregate.CreateSort().SetAttribute("MonthSheet.Year");
    getMonths.AsDatabaseAggregate.CreateSort().SetAttribute("MonthSheet.Month");
    getMonths.AsDatabaseAggregate.CreateSort().SetAttribute("User.Name");
    
    ifNode.TrueTarget = getMonths;
    
    var forEachNode = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonths, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = "Each month";
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetMonths.List");
    forEachNode.SetStartIndex(null); // default value
    
    var monthSheet_GetTotals = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("MonthSheet_GetTotals").ToTheRightOf(forEachNode, 1828);
    var monthSheet_GetTotals2 = eSpace.ServerActions.Named("MonthSheet_GetTotals");
    monthSheet_GetTotals.Action = monthSheet_GetTotals2;
    monthSheet_GetTotals.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    monthSheet_GetTotals.IsDisabled = false; // default value
    monthSheet_GetTotals.ServerRequestTimeout = null; // default value
    var userId = monthSheet_GetTotals2.InputParameters.Named("UserId");
    monthSheet_GetTotals.SetArgumentValue(userId, "GetMonths.List.Current.MonthSheet.UserId");
    var year = monthSheet_GetTotals2.InputParameters.Named("Year");
    monthSheet_GetTotals.SetArgumentValue(year, "GetMonths.List.Current.MonthSheet.Year");
    var month = monthSheet_GetTotals2.InputParameters.Named("Month");
    monthSheet_GetTotals.SetArgumentValue(month, "GetMonths.List.Current.MonthSheet.Month");
    forEachNode.CycleTarget = monthSheet_GetTotals;
    
    var endNode = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 5484);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var assignNode = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(monthSheet_GetTotals, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Build line";
    
    var assignment = assignNode.CreateAssignment("Line.MonthSheetId", "GetMonths.List.Current.MonthSheet.Id");
    
    var assignment2 = assignNode.CreateAssignment("Line.UserId", "GetMonths.List.Current.MonthSheet.UserId");
    
    var assignment3 = assignNode.CreateAssignment("Line.UserName", "GetMonths.List.Current.User.Name");
    
    var assignment4 = assignNode.CreateAssignment("Line.Year", "GetMonths.List.Current.MonthSheet.Year");
    
    var assignment5 = assignNode.CreateAssignment("Line.Month", "GetMonths.List.Current.MonthSheet.Month");
    
    var assignment6 = assignNode.CreateAssignment("Line.PeriodStatusId", "GetMonths.List.Current.MonthSheet.PeriodStatusId");
    
    var assignment7 = assignNode.CreateAssignment("Line.SubmittedOn", "GetMonths.List.Current.MonthSheet.SubmittedOn");
    
    var assignment8 = assignNode.CreateAssignment("Line.DecidedOn", "GetMonths.List.Current.MonthSheet.DecidedOn");
    
    var assignment9 = assignNode.CreateAssignment("Line.Comment", "GetMonths.List.Current.MonthSheet.Comment");
    
    var assignment10 = assignNode.CreateAssignment("Line.TotalMinutes", "MonthSheet_GetTotals.TotalMinutes");
    
    var assignment11 = assignNode.CreateAssignment("Line.ExpectedMinutes", "MonthSheet_GetTotals.ExpectedMinutes");
    
    var listAppend = monthSheet_GetSubmitted.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    listAppend.Target = forEachNode;
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Months");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Line");
    
    var months = monthSheet_GetSubmitted.CreateOutputParameter("Months");
    months.DataType = eSpace.GetOrCreateListType(monthSheetSummary);
    months.Description = null; // default value
}
