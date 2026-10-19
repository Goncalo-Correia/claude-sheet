(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_SetActive' ***/
    var holiday_SetActive = eSpace.CreateServerAction("Holiday_SetActive");
    holiday_SetActive.CacheInMinutes = null; // default value
    holiday_SetActive.Description = "Activates or deactivates a holiday (SheetAdmin only); holidays are deactivated instead of deleted. Inactive holidays are ignored by the calendar (Calendar_GetMonth filters on IsActive).";
    var holiday = eSpace.Folders.Named("Holiday");
    holiday_SetActive.Folder = holiday;
    holiday_SetActive.Function = false; // default value
    holiday_SetActive.Icon = null; // default value
    holiday_SetActive.Public = true;
    holiday_SetActive.Shareable = true;
    
    var holidayId = holiday_SetActive.CreateInputParameter("HolidayId");
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    holidayId.DataType = holiday2.IdentifierType;
    holidayId.Description = null; // default value
    holidayId.IsMandatory = true; // default value
    
    var isActive = holiday_SetActive.CreateInputParameter("IsActive");
    isActive.DataType = eSpace.BooleanType;
    isActive.Description = null; // default value
    isActive.IsMandatory = true; // default value
    
    var holidayRecord = holiday_SetActive.CreateLocalVariable("HolidayRecord");
    holidayRecord.DataType = holiday2;
    holidayRecord.Description = null; // default value
    
    var startNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage holidays.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getHoliday = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetHoliday").Below(ifNode, 1371);
    getHoliday.CacheInMinutes = null; // default value
    getHoliday.Description = null; // default value
    getHoliday.IsDisabled = false; // default value
    getHoliday.SetMaxRecords(null); // default value
    getHoliday.SetStartIndex(null); // default value
    getHoliday.Timeout = null; // default value
    var holiday3 = getHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getHoliday.AsDatabaseAggregate.CreateFilter("Holiday.Id = HolidayId");
    
    ifNode.TrueTarget = getHoliday;
    
    var ifNode2 = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getHoliday, 1371);
    ifNode2.SetCondition("GetHoliday.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Not found?";
    
    var assignNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set IsActive";
    
    var assignment = assignNode.CreateAssignment("HolidayRecord", "GetHoliday.List.Current.Holiday");
    
    var assignment2 = assignNode.CreateAssignment("HolidayRecord.IsActive", "IsActive");
    ifNode2.FalseTarget = assignNode;
    
    var raiseExceptionNode2 = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var holidayValidation = eSpace.UserExceptions.Named("HolidayValidation");
    raiseExceptionNode2.Exception = holidayValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Holiday not found.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var updateHoliday = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateHoliday").ConnectedBelow(assignNode, 1371);
    updateHoliday.Action = holiday2.UpdateAction;
    updateHoliday.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateHoliday.IsDisabled = false; // default value
    updateHoliday.ServerRequestTimeout = null; // default value
    var updateHoliday2 = holiday2.UpdateAction;
    var source = updateHoliday2.InputParameters.Named("Source");
    updateHoliday.SetArgumentValue(source, "HolidayRecord");
    
    var endNode = holiday_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateHoliday, 1371);
    endNode.IsDisabled = false; // default value
}
