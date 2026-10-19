(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_CreateIfMissing' ***/
    var holiday_CreateIfMissing = eSpace.CreateServerAction("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.CacheInMinutes = null; // default value
    holiday_CreateIfMissing.Description = "Creates an active Holiday for HolidayDate unless one already exists for that date (active or not); Created tells whether it did. IsFixedDate (default True) is stored on create; when it is False and the existing holiday is still marked fixed, the existing one is marked movable. Used by Bootstrap and Holiday_CopyYear.";
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
    
    var isFixedDate = holiday_CreateIfMissing.CreateInputParameter("IsFixedDate");
    isFixedDate.DataType = eSpace.BooleanType;
    isFixedDate.SetDefaultValue("True");
    isFixedDate.Description = null; // default value
    isFixedDate.IsMandatory = false;
    
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
    
    var ifNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(ifNode, 1828);
    ifNode2.SetCondition("not IsFixedDate and GetExistingHoliday.List.Current.Holiday.IsFixedDate");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Mark movable?";
    ifNode.FalseTarget = ifNode2;
    
    var assignNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("NewHoliday.HolidayDate", "HolidayDate");
    
    var assignment2 = assignNode.CreateAssignment("NewHoliday.Name", "Name");
    
    var assignment3 = assignNode.CreateAssignment("NewHoliday.IsActive", "True");
    
    var assignment4 = assignNode.CreateAssignment("NewHoliday.IsFixedDate", "IsFixedDate");
    
    var assignment5 = assignNode.CreateAssignment("Created", "True");
    ifNode.TrueTarget = assignNode;
    
    var endNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    endNode.IsDisabled = false; // default value
    ifNode2.FalseTarget = endNode;
    
    var assignNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment6 = assignNode2.CreateAssignment("NewHoliday", "GetExistingHoliday.List.Current.Holiday");
    
    var assignment7 = assignNode2.CreateAssignment("NewHoliday.IsFixedDate", "False");
    ifNode2.TrueTarget = assignNode2;
    
    var createHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday").ConnectedBelow(assignNode, 1371);
    createHoliday.Action = holiday.CreateAction;
    createHoliday.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createHoliday.IsDisabled = false; // default value
    createHoliday.ServerRequestTimeout = null; // default value
    var createHoliday2 = holiday.CreateAction;
    var source = createHoliday2.InputParameters.Named("Source");
    createHoliday.SetArgumentValue(source, "NewHoliday");
    
    var updateHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateHoliday").ConnectedBelow(assignNode2, 1371);
    updateHoliday.Action = holiday.UpdateAction;
    updateHoliday.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateHoliday.IsDisabled = false; // default value
    updateHoliday.ServerRequestTimeout = null; // default value
    var updateHoliday2 = holiday.UpdateAction;
    var source2 = updateHoliday2.InputParameters.Named("Source");
    updateHoliday.SetArgumentValue(source2, "NewHoliday");
    
    var endNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(createHoliday, 1371);
    endNode2.IsDisabled = false; // default value
    
    var endNode3 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateHoliday, 1371);
    endNode3.IsDisabled = false; // default value
    
    var created = holiday_CreateIfMissing.CreateOutputParameter("Created");
    created.DataType = eSpace.BooleanType;
    created.SetDefaultValue(null); // default value
    created.Description = null; // default value
}
