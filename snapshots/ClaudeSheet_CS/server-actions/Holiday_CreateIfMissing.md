# Holiday_CreateIfMissing

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_CreateIfMissing' ***/
    var holiday_CreateIfMissing = eSpace.CreateServerAction("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.Description = "Creates an active Holiday for HolidayDate unless one already exists for that date (active or not); Created tells whether it did. IsFixedDate (default True) is stored on create; when it is False and the existing holiday is still marked fixed, the existing one is marked movable. Used by Bootstrap and Holiday_CopyYear.";
    var holiday = eSpace.Folders.Named("Holiday");
    holiday_CreateIfMissing.Folder = holiday;
    
    var holidayDate = holiday_CreateIfMissing.CreateInputParameter("HolidayDate");
    holidayDate.DataType = eSpace.DateType;
    
    var name = holiday_CreateIfMissing.CreateInputParameter("Name");
    name.DataType = eSpace.TextType;
    
    var isFixedDate = holiday_CreateIfMissing.CreateInputParameter("IsFixedDate");
    isFixedDate.DataType = eSpace.BooleanType;
    isFixedDate.SetDefaultValue("True");
    isFixedDate.IsMandatory = false;
    
    var newHoliday = holiday_CreateIfMissing.CreateLocalVariable("NewHoliday");
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    newHoliday.DataType = holiday2;
    
    var startNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var getExistingHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetExistingHoliday").ConnectedBelow(startNode, 1371);
    var holiday3 = getExistingHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getExistingHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = HolidayDate");
    
    
    var ifNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getExistingHoliday, 1371);
    ifNode.SetCondition("GetExistingHoliday.List.Empty");
    ifNode.Label = "Missing?";
    
    var ifNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(ifNode, 1828);
    ifNode2.SetCondition("not IsFixedDate and GetExistingHoliday.List.Current.Holiday.IsFixedDate");
    ifNode2.Label = "Mark movable?";
    ifNode.FalseTarget = ifNode2;
    
    var assignNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.Label = "New holiday";
    
    var assignment = assignNode.CreateAssignment("NewHoliday.HolidayDate", "HolidayDate");
    
    var assignment2 = assignNode.CreateAssignment("NewHoliday.Name", "Name");
    
    var assignment3 = assignNode.CreateAssignment("NewHoliday.IsActive", "True");
    
    var assignment4 = assignNode.CreateAssignment("NewHoliday.IsFixedDate", "IsFixedDate");
    
    var assignment5 = assignNode.CreateAssignment("Created", "True");
    ifNode.TrueTarget = assignNode;
    
    var endNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    ifNode2.FalseTarget = endNode;
    
    var assignNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode2.Label = "Mark movable";
    
    var assignment6 = assignNode2.CreateAssignment("NewHoliday", "GetExistingHoliday.List.Current.Holiday");
    
    var assignment7 = assignNode2.CreateAssignment("NewHoliday.IsFixedDate", "False");
    ifNode2.TrueTarget = assignNode2;
    
    var createHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday").ConnectedBelow(assignNode, 1371);
    createHoliday.Action = holiday2.CreateAction;
    var createHoliday2 = holiday2.CreateAction;
    var source = createHoliday2.InputParameters.Named("Source");
    createHoliday.SetArgumentValue(source, "NewHoliday");
    
    var updateHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateHoliday").ConnectedBelow(assignNode2, 1371);
    updateHoliday.Action = holiday2.UpdateAction;
    var updateHoliday2 = holiday2.UpdateAction;
    var source2 = updateHoliday2.InputParameters.Named("Source");
    updateHoliday.SetArgumentValue(source2, "NewHoliday");
    
    var endNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(createHoliday, 1371);
    
    var endNode3 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateHoliday, 1371);
    
    var created = holiday_CreateIfMissing.CreateOutputParameter("Created");
    created.DataType = eSpace.BooleanType;
}
```
