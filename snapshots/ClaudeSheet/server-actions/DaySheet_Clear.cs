(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DaySheet_Clear' ***/
    var daySheet_Clear = eSpace.CreateServerAction("DaySheet_Clear");
    daySheet_Clear.CacheInMinutes = null; // default value
    daySheet_Clear.Description = "Deletes the current user\'s DaySheet for Date (its TimeEntries are deleted by the Delete rule). The day goes back to Pending or Future.";
    daySheet_Clear.Folder = null; // default value
    daySheet_Clear.Function = false; // default value
    daySheet_Clear.Icon = null; // default value
    daySheet_Clear.Public = false; // default value
    daySheet_Clear.Shareable = false; // default value
    
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
    
    var raiseExceptionNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode, 1371);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getDayRecord = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord");
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.HorizontalPosition = 5028;
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    getDayRecord.VerticalPosition = 3542;
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet2);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode.TrueTarget = getDayRecord;
    
    var ifNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode2.SetCondition("not GetDayRecord.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Has day sheet?";
    
    var endNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode2, 1371);
    endNode.IsDisabled = false; // default value
    ifNode2.FalseTarget = endNode;
    
    var deleteDaySheet = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteDaySheet");
    deleteDaySheet.Action = daySheet2.DeleteAction;
    deleteDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteDaySheet.HorizontalPosition = 6856;
    deleteDaySheet.IsDisabled = false; // default value
    deleteDaySheet.ServerRequestTimeout = null; // default value
    deleteDaySheet.Target = endNode;
    deleteDaySheet.VerticalPosition = 6284;
    var deleteDaySheet2 = daySheet2.DeleteAction;
    var id = deleteDaySheet2.InputParameters.Named("Id");
    deleteDaySheet.SetArgumentValue(id, "GetDayRecord.List.Current.DaySheet.Id");
    ifNode2.TrueTarget = deleteDaySheet;
}
