(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_IsLocked' ***/
    var monthSheet_IsLocked = eSpace.CreateServerAction("MonthSheet_IsLocked");
    monthSheet_IsLocked.CacheInMinutes = null; // default value
    monthSheet_IsLocked.Description = "True when the user\'s month (MonthSheet) is Submitted or Approved, so its days can\'t be saved, cleared or filled. No MonthSheet = not locked.";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_IsLocked.Folder = monthSheet;
    monthSheet_IsLocked.Function = true;
    monthSheet_IsLocked.Icon = null; // default value
    monthSheet_IsLocked.Public = false; // default value
    monthSheet_IsLocked.Shareable = false; // default value
    
    var userId = monthSheet_IsLocked.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId.DataType = user.IdentifierType;
    userId.Description = null; // default value
    userId.IsMandatory = true; // default value
    
    var year = monthSheet_IsLocked.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = monthSheet_IsLocked.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var startNode = monthSheet_IsLocked.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var getLockedMonth = monthSheet_IsLocked.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetLockedMonth").ConnectedBelow(startNode, 1371);
    getLockedMonth.CacheInMinutes = null; // default value
    getLockedMonth.Description = null; // default value
    getLockedMonth.IsDisabled = false; // default value
    getLockedMonth.SetMaxRecords("1");
    getLockedMonth.SetStartIndex(null); // default value
    getLockedMonth.Timeout = null; // default value
    var monthSheet3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("MonthSheet");
    var monthSheet2 = getLockedMonth.AsDatabaseAggregate.CreateSource(monthSheet3);
    getLockedMonth.AsDatabaseAggregate.CreateFilter("MonthSheet.UserId = UserId");
    getLockedMonth.AsDatabaseAggregate.CreateFilter("MonthSheet.Year = Year");
    getLockedMonth.AsDatabaseAggregate.CreateFilter("MonthSheet.Month = Month");
    getLockedMonth.AsDatabaseAggregate.CreateFilter("MonthSheet.PeriodStatusId = Entities.PeriodStatus.Submitted or MonthSheet.PeriodStatusId = Entities.PeriodStatus.Approved");
    
    
    var assignNode = monthSheet_IsLocked.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getLockedMonth, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set locked";
    
    var assignment = assignNode.CreateAssignment("IsLocked", "not GetLockedMonth.List.Empty");
    
    var endNode = monthSheet_IsLocked.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var isLocked = monthSheet_IsLocked.CreateOutputParameter("IsLocked");
    isLocked.DataType = eSpace.BooleanType;
    isLocked.SetDefaultValue(null); // default value
    isLocked.Description = null; // default value
}
