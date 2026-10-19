(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Holiday_CopyYear' ***/
    var holiday_CopyYear = eSpace.CreateServerAction("Holiday_CopyYear");
    holiday_CopyYear.CacheInMinutes = null; // default value
    holiday_CopyYear.Description = "Copies the active fixed-date holidays (IsFixedDate) of FromYear to the same day and month of ToYear (SheetAdmin only), skipping dates that already have a holiday and 29 February. Returns CopiedCount; running it again copies nothing.";
    var holiday = eSpace.Folders.Named("Holiday");
    holiday_CopyYear.Folder = holiday;
    holiday_CopyYear.Function = false; // default value
    holiday_CopyYear.Icon = null; // default value
    holiday_CopyYear.Public = true;
    holiday_CopyYear.Shareable = true;
    
    var fromYear = holiday_CopyYear.CreateInputParameter("FromYear");
    fromYear.DataType = eSpace.IntegerType;
    fromYear.Description = null; // default value
    fromYear.IsMandatory = true; // default value
    
    var toYear = holiday_CopyYear.CreateInputParameter("ToYear");
    toYear.DataType = eSpace.IntegerType;
    toYear.Description = null; // default value
    toYear.IsMandatory = true; // default value
    
    var startNode = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage holidays.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("FromYear = ToYear");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Same year?";
    ifNode.TrueTarget = ifNode2;
    
    var getFixedHolidays = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetFixedHolidays").Below(ifNode2, 1371);
    getFixedHolidays.CacheInMinutes = null; // default value
    getFixedHolidays.Description = null; // default value
    getFixedHolidays.IsDisabled = false; // default value
    getFixedHolidays.SetMaxRecords(null); // default value
    getFixedHolidays.SetStartIndex(null); // default value
    getFixedHolidays.Timeout = null; // default value
    var holiday3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday2 = getFixedHolidays.AsDatabaseAggregate.CreateSource(holiday3);
    getFixedHolidays.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= NewDate(FromYear, 1, 1) and Holiday.HolidayDate <= NewDate(FromYear, 12, 31)");
    getFixedHolidays.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getFixedHolidays.AsDatabaseAggregate.CreateFilter("Holiday.IsFixedDate");
    getFixedHolidays.AsDatabaseAggregate.CreateFilter("Month(Holiday.HolidayDate) <> 2 or Day(Holiday.HolidayDate) <> 29");
    getFixedHolidays.AsDatabaseAggregate.CreateSort().SetAttribute("Holiday.HolidayDate");
    
    ifNode2.FalseTarget = getFixedHolidays;
    
    var raiseExceptionNode2 = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var holidayValidation = eSpace.UserExceptions.Named("HolidayValidation");
    raiseExceptionNode2.Exception = holidayValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Choose a different year to copy to.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var forEachNode = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getFixedHolidays, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = "Each fixed holiday";
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetFixedHolidays.List");
    forEachNode.SetStartIndex(null); // default value
    
    var holiday_CreateIfMissing = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing").ToTheRightOf(forEachNode, 1828);
    var holiday_CreateIfMissing2 = eSpace.ServerActions.Named("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing.IsDisabled = false; // default value
    holiday_CreateIfMissing.ServerRequestTimeout = null; // default value
    var holidayDate = holiday_CreateIfMissing2.InputParameters.Named("HolidayDate");
    holiday_CreateIfMissing.SetArgumentValue(holidayDate, "NewDate(ToYear, Month(GetFixedHolidays.List.Current.Holiday.HolidayDate), Day(GetFixedHolidays.List.Current.Holiday.HolidayDate))");
    var isFixedDate = holiday_CreateIfMissing2.InputParameters.Named("IsFixedDate");
    holiday_CreateIfMissing.SetArgumentValue(isFixedDate, "True");
    var name = holiday_CreateIfMissing2.InputParameters.Named("Name");
    holiday_CreateIfMissing.SetArgumentValue(name, "GetFixedHolidays.List.Current.Holiday.Name");
    forEachNode.CycleTarget = holiday_CreateIfMissing;
    
    var endNode = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 2742);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var assignNode = holiday_CopyYear.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(holiday_CreateIfMissing, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Count copied";
    assignNode.Target = forEachNode;
    
    var assignment = assignNode.CreateAssignment("CopiedCount", "CopiedCount + If(Holiday_CreateIfMissing.Created, 1, 0)");
    
    var copiedCount = holiday_CopyYear.CreateOutputParameter("CopiedCount");
    copiedCount.DataType = eSpace.IntegerType;
    copiedCount.SetDefaultValue(null); // default value
    copiedCount.Description = null; // default value
}
