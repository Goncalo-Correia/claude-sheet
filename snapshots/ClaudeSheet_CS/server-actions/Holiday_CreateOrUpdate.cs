(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_CreateOrUpdate' ***/
    var holiday_CreateOrUpdate = eSpace.CreateServerAction("Holiday_CreateOrUpdate");
    holiday_CreateOrUpdate.CacheInMinutes = null; // default value
    holiday_CreateOrUpdate.Description = "Creates or updates a holiday (SheetAdmin only). Name is trimmed and required, the date is required and unique (UQ_Holiday_HolidayDate, inactive holidays included). Raises HolidayValidation with a user-facing message otherwise. Returns the holiday\'s Id.";
    var holiday = eSpace.Folders.Named("Holiday");
    holiday_CreateOrUpdate.Folder = holiday;
    holiday_CreateOrUpdate.Function = false; // default value
    holiday_CreateOrUpdate.Icon = null; // default value
    holiday_CreateOrUpdate.Public = true;
    holiday_CreateOrUpdate.Shareable = true;
    
    var holidayRecord = holiday_CreateOrUpdate.CreateInputParameter("HolidayRecord");
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    holidayRecord.DataType = holiday2;
    holidayRecord.Description = null; // default value
    holidayRecord.IsMandatory = true; // default value
    
    var startNode = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage holidays.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("Trim(HolidayRecord.Name) = \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Name empty?";
    ifNode.TrueTarget = ifNode2;
    
    var ifNode3 = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("HolidayRecord.HolidayDate = NullDate()");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "No date?";
    ifNode2.FalseTarget = ifNode3;
    
    var raiseExceptionNode2 = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var holidayValidation = eSpace.UserExceptions.Named("HolidayValidation");
    raiseExceptionNode2.Exception = holidayValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Enter the holiday name.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var getSameDate = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSameDate").Below(ifNode3, 1371);
    getSameDate.CacheInMinutes = null; // default value
    getSameDate.Description = null; // default value
    getSameDate.IsDisabled = false; // default value
    getSameDate.SetMaxRecords(null); // default value
    getSameDate.SetStartIndex(null); // default value
    getSameDate.Timeout = null; // default value
    var holiday3 = getSameDate.AsDatabaseAggregate.CreateSource(holiday2);
    getSameDate.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = HolidayRecord.HolidayDate");
    getSameDate.AsDatabaseAggregate.CreateFilter("Holiday.Id <> HolidayRecord.Id");
    
    ifNode3.FalseTarget = getSameDate;
    
    var raiseExceptionNode3 = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = holidayValidation;
    raiseExceptionNode3.SetExceptionMessage("\"Enter the holiday date.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var ifNode4 = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSameDate, 1371);
    ifNode4.SetCondition("not GetSameDate.List.Empty");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Date taken?";
    
    var assignNode = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Trim name";
    
    var assignment = assignNode.CreateAssignment("HolidayRecord.Name", "Trim(HolidayRecord.Name)");
    ifNode4.FalseTarget = assignNode;
    
    var raiseExceptionNode4 = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = holidayValidation;
    raiseExceptionNode4.SetExceptionMessage("\"There is already a holiday on this date (\" + GetSameDate.List.Current.Holiday.Name + \").\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var createOrUpdateHoliday = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateHoliday").ConnectedBelow(assignNode, 1371);
    createOrUpdateHoliday.Action = holiday2.CreateOrUpdateAction;
    createOrUpdateHoliday.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createOrUpdateHoliday.IsDisabled = false; // default value
    createOrUpdateHoliday.ServerRequestTimeout = null; // default value
    var createOrUpdateHoliday2 = holiday2.CreateOrUpdateAction;
    var source = createOrUpdateHoliday2.InputParameters.Named("Source");
    createOrUpdateHoliday.SetArgumentValue(source, "HolidayRecord");
    
    var assignNode2 = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createOrUpdateHoliday, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Return Id";
    
    var assignment2 = assignNode2.CreateAssignment("HolidayId", "CreateOrUpdateHoliday.Id");
    
    var endNode = holiday_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode.IsDisabled = false; // default value
    
    var holidayId = holiday_CreateOrUpdate.CreateOutputParameter("HolidayId");
    holidayId.DataType = holiday2.IdentifierType;
    holidayId.SetDefaultValue(null); // default value
    holidayId.Description = null; // default value
}
