(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_GetForUser' ***/
    var monthSheet_GetForUser = eSpace.CreateServerAction("MonthSheet_GetForUser");
    monthSheet_GetForUser.CacheInMinutes = null; // default value
    monthSheet_GetForUser.Description = "Status of the current user\'s month (SheetUser): PeriodStatusId (Open when there is no MonthSheet), the manager\'s Comment and DecidedOn, and IsLocked (Submitted or Approved).";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_GetForUser.Folder = monthSheet;
    monthSheet_GetForUser.Function = false; // default value
    monthSheet_GetForUser.Icon = null; // default value
    monthSheet_GetForUser.Public = true;
    monthSheet_GetForUser.Shareable = true;
    
    var year = monthSheet_GetForUser.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = monthSheet_GetForUser.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var startNode = monthSheet_GetForUser.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = monthSheet_GetForUser.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = monthSheet_GetForUser.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getMonthSheet = monthSheet_GetForUser.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthSheet").Below(ifNode, 1371);
    getMonthSheet.CacheInMinutes = null; // default value
    getMonthSheet.Description = null; // default value
    getMonthSheet.IsDisabled = false; // default value
    getMonthSheet.SetMaxRecords("1");
    getMonthSheet.SetStartIndex(null); // default value
    getMonthSheet.Timeout = null; // default value
    var monthSheet3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("MonthSheet");
    var monthSheet2 = getMonthSheet.AsDatabaseAggregate.CreateSource(monthSheet3);
    getMonthSheet.AsDatabaseAggregate.CreateFilter("MonthSheet.UserId = GetUserId()");
    getMonthSheet.AsDatabaseAggregate.CreateFilter("MonthSheet.Year = Year");
    getMonthSheet.AsDatabaseAggregate.CreateFilter("MonthSheet.Month = Month");
    
    ifNode.TrueTarget = getMonthSheet;
    
    var assignNode = monthSheet_GetForUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getMonthSheet, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set status";
    
    var assignment = assignNode.CreateAssignment("PeriodStatusId", "If(GetMonthSheet.List.Empty, Entities.PeriodStatus.Open, GetMonthSheet.List.Current.MonthSheet.PeriodStatusId)");
    
    var assignment2 = assignNode.CreateAssignment("Comment", "GetMonthSheet.List.Current.MonthSheet.Comment");
    
    var assignment3 = assignNode.CreateAssignment("DecidedOn", "GetMonthSheet.List.Current.MonthSheet.DecidedOn");
    
    var assignment4 = assignNode.CreateAssignment("IsLocked", "not GetMonthSheet.List.Empty and (GetMonthSheet.List.Current.MonthSheet.PeriodStatusId = Entities.PeriodStatus.Submitted or GetMonthSheet.List.Current.MonthSheet.PeriodStatusId = Entities.PeriodStatus.Approved)");
    
    var endNode = monthSheet_GetForUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var periodStatusId = monthSheet_GetForUser.CreateOutputParameter("PeriodStatusId");
    var periodStatus = eSpace.Entities.OfType<OutSystems.Model.Data.IStaticEntity>().Named("PeriodStatus");
    periodStatusId.DataType = periodStatus.IdentifierType;
    periodStatusId.SetDefaultValue(null); // default value
    periodStatusId.Description = null; // default value
    
    var comment = monthSheet_GetForUser.CreateOutputParameter("Comment");
    comment.DataType = eSpace.TextType;
    comment.SetDefaultValue(null); // default value
    comment.Description = null; // default value
    
    var decidedOn = monthSheet_GetForUser.CreateOutputParameter("DecidedOn");
    decidedOn.DataType = eSpace.DateTimeType;
    decidedOn.SetDefaultValue(null); // default value
    decidedOn.Description = null; // default value
    
    var isLocked = monthSheet_GetForUser.CreateOutputParameter("IsLocked");
    isLocked.DataType = eSpace.BooleanType;
    isLocked.SetDefaultValue(null); // default value
    isLocked.Description = null; // default value
}
