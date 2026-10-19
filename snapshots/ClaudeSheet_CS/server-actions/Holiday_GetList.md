# Holiday_GetList

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_GetList' ***/
    var holiday_GetList = eSpace.CreateServerAction("Holiday_GetList");
    holiday_GetList.Description = "Holidays of Year sorted by date (SheetAdmin only). IncludeInactive also returns deactivated holidays.";
    var holiday = eSpace.Folders.Named("Holiday");
    holiday_GetList.Folder = holiday;
    holiday_GetList.Public = true;
    holiday_GetList.Shareable = true;
    
    var year = holiday_GetList.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    
    var includeInactive = holiday_GetList.CreateInputParameter("IncludeInactive");
    includeInactive.DataType = eSpace.BooleanType;
    includeInactive.IsMandatory = false;
    
    var startNode = holiday_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = holiday_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = holiday_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage holidays.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getYearHolidays = holiday_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetYearHolidays").Below(ifNode, 1371);
    var holiday3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday2 = getYearHolidays.AsDatabaseAggregate.CreateSource(holiday3);
    getYearHolidays.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= NewDate(Year, 1, 1) and Holiday.HolidayDate <= NewDate(Year, 12, 31)");
    getYearHolidays.AsDatabaseAggregate.CreateFilter("IncludeInactive or Holiday.IsActive");
    getYearHolidays.AsDatabaseAggregate.CreateSort().SetAttribute("Holiday.HolidayDate");
    
    ifNode.TrueTarget = getYearHolidays;
    
    var assignNode = holiday_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getYearHolidays, 1371);
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("Holidays", "GetYearHolidays.List mapTo {  }");
    
    var endNode = holiday_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var holidays = holiday_GetList.CreateOutputParameter("Holidays");
    holidays.DataType = eSpace.GetOrCreateListType(holiday3);
}
```
