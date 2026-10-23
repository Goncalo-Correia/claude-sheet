(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DaySheet_Clear' ***/
    var daySheet_Clear = eSpace.CreateServerAction("DaySheet_Clear");
    daySheet_Clear.CacheInMinutes = null; // default value
    daySheet_Clear.Description = "Deletes the current user\'s DaySheet for Date (its TimeEntries are deleted by the Delete rule). The day goes back to Pending or Future. Refused (DaySheetValidation) when the month is Submitted or Approved.";
    var daySheet = eSpace.Folders.Named("DaySheet");
    daySheet_Clear.Folder = daySheet;
    daySheet_Clear.Function = false; // default value
    daySheet_Clear.Icon = null; // default value
    daySheet_Clear.Public = true;
    daySheet_Clear.Shareable = true;
    
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
    
    var ifNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("MonthSheet_IsLocked(GetUserId(), Year(Date), Month(Date))");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Month locked?";
    ifNode.TrueTarget = ifNode2;
    
    var getDayRecord = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode2, 1371);
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
    
    ifNode2.FalseTarget = getDayRecord;
    
    var raiseExceptionNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var daySheetValidation = eSpace.UserExceptions.Named("DaySheetValidation");
    raiseExceptionNode2.Exception = daySheetValidation;
    raiseExceptionNode2.SetExceptionMessage("\"This month is submitted or approved.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode3 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode3.SetCondition("not GetDayRecord.List.Empty");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "!Has day sheet?";
    
    var endNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode3, 1828);
    endNode.IsDisabled = false; // default value
    ifNode3.FalseTarget = endNode;
    
    var deleteDaySheet = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteDaySheet").Below(ifNode3, 1371);
    deleteDaySheet.Action = daySheet3.DeleteAction;
    deleteDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteDaySheet.IsDisabled = false; // default value
    deleteDaySheet.ServerRequestTimeout = null; // default value
    var deleteDaySheet2 = daySheet3.DeleteAction;
    var id = deleteDaySheet2.InputParameters.Named("Id");
    deleteDaySheet.SetArgumentValue(id, "GetDayRecord.List.Current.DaySheet.Id");
    ifNode3.TrueTarget = deleteDaySheet;
    
    var endNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(deleteDaySheet, 1371);
    endNode2.IsDisabled = false; // default value
}
