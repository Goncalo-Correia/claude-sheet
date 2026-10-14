(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_CreateIfMissing' ***/
    var holiday_CreateIfMissing = eSpace.CreateServerAction("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.CacheInMinutes = null; // default value
    holiday_CreateIfMissing.Description = "Creates an active Holiday for HolidayDate unless one already exists for that date (active or not). Used by Bootstrap.";
    holiday_CreateIfMissing.Folder = null; // default value
    holiday_CreateIfMissing.Function = false; // default value
    holiday_CreateIfMissing.Icon = null; // default value
    holiday_CreateIfMissing.Public = false; // default value
    holiday_CreateIfMissing.Shareable = false; // default value
    
    var holidayDate = holiday_CreateIfMissing.CreateInputParameter("HolidayDate");
    holidayDate.DataType = eSpace.DateType;
    holidayDate.Description = null; // default value
    holidayDate.IsMandatory = true; // default value
    
    var name = holiday_CreateIfMissing.CreateInputParameter("Name");
    name.DataType = eSpace.TextType;
    name.Description = null; // default value
    name.IsMandatory = true; // default value
    
    var newHoliday = holiday_CreateIfMissing.CreateLocalVariable("NewHoliday");
    var holiday = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    newHoliday.DataType = holiday;
    newHoliday.Description = null; // default value
    
    var startNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var getExistingHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetExistingHoliday").ConnectedBelow(startNode, 1371);
    getExistingHoliday.CacheInMinutes = null; // default value
    getExistingHoliday.Description = null; // default value
    getExistingHoliday.IsDisabled = false; // default value
    getExistingHoliday.SetMaxRecords(null); // default value
    getExistingHoliday.SetStartIndex(null); // default value
    getExistingHoliday.Timeout = null; // default value
    var holiday2 = getExistingHoliday.AsDatabaseAggregate.CreateSource(holiday);
    getExistingHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = HolidayDate");
    
    
    var ifNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getExistingHoliday, 1371);
    ifNode.SetCondition("GetExistingHoliday.List.Empty");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Missing?";
    
    var endNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 5028;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 4913;
    ifNode.FalseTarget = endNode;
    
    var assignNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("NewHoliday.HolidayDate", "HolidayDate");
    
    var assignment2 = assignNode.CreateAssignment("NewHoliday.Name", "Name");
    
    var assignment3 = assignNode.CreateAssignment("NewHoliday.IsActive", "True");
    ifNode.TrueTarget = assignNode;
    
    var createHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday").ConnectedBelow(assignNode, 1371);
    createHoliday.Action = holiday.CreateAction;
    createHoliday.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createHoliday.IsDisabled = false; // default value
    createHoliday.ServerRequestTimeout = null; // default value
    createHoliday.Target = endNode;
    var createHoliday2 = holiday.CreateAction;
    var source = createHoliday2.InputParameters.Named("Source");
    createHoliday.SetArgumentValue(source, "NewHoliday");
}
