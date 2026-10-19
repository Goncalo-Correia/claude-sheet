# DaySheet_Clear

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DaySheet_Clear' ***/
    var daySheet_Clear = eSpace.CreateServerAction("DaySheet_Clear");
    daySheet_Clear.Description = "Deletes the current user\'s DaySheet for Date (its TimeEntries are deleted by the Delete rule). The day goes back to Pending or Future.";
    var daySheet = eSpace.Folders.Named("DaySheet");
    daySheet_Clear.Folder = daySheet;
    daySheet_Clear.Public = true;
    daySheet_Clear.Shareable = true;
    
    var date = daySheet_Clear.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    
    var startNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.Label = "!Is SheetUser?";
    
    var raiseExceptionNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getDayRecord = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode, 1371);
    var daySheet3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet2 = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet3);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode.TrueTarget = getDayRecord;
    
    var ifNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode2.SetCondition("not GetDayRecord.List.Empty");
    ifNode2.Label = "!Has day sheet?";
    
    var endNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    ifNode2.FalseTarget = endNode;
    
    var deleteDaySheet = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteDaySheet").Below(ifNode2, 1371);
    deleteDaySheet.Action = daySheet3.DeleteAction;
    var deleteDaySheet2 = daySheet3.DeleteAction;
    var id = deleteDaySheet2.InputParameters.Named("Id");
    deleteDaySheet.SetArgumentValue(id, "GetDayRecord.List.Current.DaySheet.Id");
    ifNode2.TrueTarget = deleteDaySheet;
    
    var endNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(deleteDaySheet, 1371);
}
```
