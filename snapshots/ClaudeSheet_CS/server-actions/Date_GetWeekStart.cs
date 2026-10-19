(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Date_GetWeekStart' ***/
    var date_GetWeekStart = eSpace.CreateServerAction("Date_GetWeekStart");
    date_GetWeekStart.CacheInMinutes = null; // default value
    date_GetWeekStart.Description = "Returns the Monday of the week that contains Date.";
    var date = eSpace.Folders.Named("Date");
    date_GetWeekStart.Folder = date;
    date_GetWeekStart.Function = true;
    date_GetWeekStart.Icon = null; // default value
    date_GetWeekStart.Public = false; // default value
    date_GetWeekStart.Shareable = false; // default value
    
    var date2 = date_GetWeekStart.CreateInputParameter("Date");
    date2.DataType = eSpace.DateType;
    date2.Description = null; // default value
    date2.IsMandatory = true; // default value
    
    var startNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Monday of week";
    
    var assignment = assignNode.CreateAssignment("WeekStart", "DateTimeToDate(AddDays(Date, -DecimalToInteger(Mod(DayOfWeek(Date) + 6, 7))))");
    
    var endNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var weekStart = date_GetWeekStart.CreateOutputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.SetDefaultValue(null); // default value
    weekStart.Description = null; // default value
}
