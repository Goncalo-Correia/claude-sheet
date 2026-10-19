# Holiday_SetActive

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_SetActive' ***/
    var holiday_SetActive = eSpace.CreateServerAction("Holiday_SetActive");
    holiday_SetActive.Description = "Activates or deactivates a holiday (SheetAdmin only); holidays are deactivated instead of deleted. Inactive holidays are ignored by the calendar (Calendar_GetMonth filters on IsActive).";
    var holiday = eSpace.Folders.Named("Holiday");
    holiday_SetActive.Folder = holiday;
    holiday_SetActive.Public = true;
    holiday_SetActive.Shareable = true;
    
    var holidayId = holiday_SetActive.CreateInputParameter("HolidayId");
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    holidayId.DataType = holiday2.IdentifierType;
    
    var isActive = holiday_SetActive.CreateInputParameter("IsActive");
    isActive.DataType = eSpace.BooleanType;
    
    var holidayRecord = holiday_SetActive.CreateLocalVariable("HolidayRecord");
    holidayRecord.DataType = holiday2;
    
    var startNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage holidays.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getHoliday = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetHoliday").Below(ifNode, 1371);
    var holiday3 = getHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getHoliday.AsDatabaseAggregate.CreateFilter("Holiday.Id = HolidayId");
    
    ifNode.TrueTarget = getHoliday;
    
    var ifNode2 = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getHoliday, 1371);
    ifNode2.SetCondition("GetHoliday.List.Empty");
    ifNode2.Label = "Not found?";
    
    var assignNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.Label = "Set IsActive";
    
    var assignment = assignNode.CreateAssignment("HolidayRecord", "GetHoliday.List.Current.Holiday");
    
    var assignment2 = assignNode.CreateAssignment("HolidayRecord.IsActive", "IsActive");
    ifNode2.FalseTarget = assignNode;
    
    var raiseExceptionNode2 = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var holidayValidation = eSpace.UserExceptions.Named("HolidayValidation");
    raiseExceptionNode2.Exception = holidayValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Holiday not found.\"");
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var updateHoliday = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateHoliday").ConnectedBelow(assignNode, 1371);
    updateHoliday.Action = holiday2.UpdateAction;
    var updateHoliday2 = holiday2.UpdateAction;
    var source = updateHoliday2.InputParameters.Named("Source");
    updateHoliday.SetArgumentValue(source, "HolidayRecord");
    
    var endNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateHoliday, 1371);
}
```
