# Settings_GetDailyExpectedMinutes

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Settings_GetDailyExpectedMinutes' ***/
    var settings_GetDailyExpectedMinutes = eSpace.CreateServerAction("Settings_GetDailyExpectedMinutes");
    settings_GetDailyExpectedMinutes.Description = "Returns the DailyExpectedMinutes site property (minutes a user is expected to record per working day), for modules that can\'t read the site property directly.";
    var settings = eSpace.Folders.Named("Settings");
    settings_GetDailyExpectedMinutes.Folder = settings;
    settings_GetDailyExpectedMinutes.Public = true;
    settings_GetDailyExpectedMinutes.Shareable = true;
    
    var startNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.Label = "!Is SheetUser?";
    
    var raiseExceptionNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.Label = "Read site property";
    
    var assignment = assignNode.CreateAssignment("Minutes", "Site.DailyExpectedMinutes");
    ifNode.TrueTarget = assignNode;
    
    var endNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var minutes = settings_GetDailyExpectedMinutes.CreateOutputParameter("Minutes");
    minutes.DataType = eSpace.IntegerType;
}
```
