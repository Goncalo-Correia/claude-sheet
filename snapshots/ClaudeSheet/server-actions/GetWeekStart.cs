(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'GetWeekStart' ***/
    var getWeekStart = eSpace.CreateServerAction("GetWeekStart");
    getWeekStart.CacheInMinutes = null; // default value
    getWeekStart.Description = "Returns the Monday of the week that contains Date.";
    getWeekStart.Folder = null; // default value
    getWeekStart.Function = true;
    getWeekStart.Icon = null; // default value
    getWeekStart.Public = false; // default value
    getWeekStart.Shareable = false; // default value
    
    var date = getWeekStart.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode = getWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = getWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("WeekStart", "DateTimeToDate(AddDays(Date, -DecimalToInteger(Mod(DayOfWeek(Date) + 6, 7))))");
    
    var endNode = getWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var weekStart = getWeekStart.CreateOutputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.SetDefaultValue(null); // default value
    weekStart.Description = null; // default value
}
