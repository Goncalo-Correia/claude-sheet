(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Settings_GetDailyExpectedMinutes' ***/
    var settings_GetDailyExpectedMinutes = eSpace.CreateServerAction("Settings_GetDailyExpectedMinutes");
    settings_GetDailyExpectedMinutes.CacheInMinutes = null; // default value
    settings_GetDailyExpectedMinutes.Description = "Returns the DailyExpectedMinutes site property (minutes a user is expected to record per working day), for modules that can\'t read the site property directly.";
    settings_GetDailyExpectedMinutes.Folder = null; // default value
    settings_GetDailyExpectedMinutes.Function = false; // default value
    settings_GetDailyExpectedMinutes.Icon = null; // default value
    settings_GetDailyExpectedMinutes.Public = true;
    settings_GetDailyExpectedMinutes.Shareable = true;
    
    var startNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "!Is SheetUser?";
    
    var raiseExceptionNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Minutes", "Site.DailyExpectedMinutes");
    ifNode.TrueTarget = assignNode;
    
    var endNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var minutes = settings_GetDailyExpectedMinutes.CreateOutputParameter("Minutes");
    minutes.DataType = eSpace.IntegerType;
    minutes.SetDefaultValue(null); // default value
    minutes.Description = null; // default value
}
