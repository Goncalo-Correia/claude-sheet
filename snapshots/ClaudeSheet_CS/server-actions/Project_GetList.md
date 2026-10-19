# Project_GetList

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_GetList' ***/
    var project_GetList = eSpace.CreateServerAction("Project_GetList");
    project_GetList.Description = "Projects for the admin list (SheetAdmin only), with client name and the number of distinct users with an active assignment that hasn\'t ended, sorted by client and project name. ClientId filters by client (empty = all); IncludeInactive also returns deactivated projects.";
    var project = eSpace.Folders.Named("Project");
    project_GetList.Folder = project;
    project_GetList.Public = true;
    project_GetList.Shareable = true;
    
    var clientId = project_GetList.CreateInputParameter("ClientId");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client.IdentifierType;
    clientId.IsMandatory = false;
    
    var includeInactive = project_GetList.CreateInputParameter("IncludeInactive");
    includeInactive.DataType = eSpace.BooleanType;
    includeInactive.IsMandatory = false;
    
    var startNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage projects.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getProjects = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetProjects").Below(ifNode, 1371);
    getProjects.Statement = "SELECT {Project}.[Id], {Project}.[ClientId], {Client}.[Name], {Project}.[Code], {Project}.[Name], {Project}.[IsActive],\n  (SELECT COUNT(DISTINCT {ProjectAssignment}.[UserId]) FROM {ProjectAssignment}\n   WHERE {ProjectAssignment}.[ProjectId] = {Project}.[Id] AND {ProjectAssignment}.[IsActive] = 1\n     AND ({ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] <= \'1900-01-01\' OR {ProjectAssignment}.[EndDate] >= @Today))\nFROM {Project}\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nWHERE (@IncludeInactive = 1 OR {Project}.[IsActive] = 1)\n  AND (COALESCE(@ClientId, 0) = 0 OR {Project}.[ClientId] = @ClientId)\nORDER BY {Client}.[Name], {Project}.[Name]";
    
    var clientId2 = getProjects.CreateInputParameter("ClientId");
    clientId2.DataType = client.IdentifierType;
    
    var includeInactive2 = getProjects.CreateInputParameter("IncludeInactive");
    includeInactive2.DataType = eSpace.BooleanType;
    
    var today = getProjects.CreateInputParameter("Today");
    today.DataType = eSpace.DateType;
    
    var projectSummary = eSpace.Structures.Named("ProjectSummary");
    var projectSummary2 = getProjects.CreateOutput(projectSummary, "ProjectSummary");
    ifNode.TrueTarget = getProjects;
    
    var assignNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getProjects, 1371);
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("Projects", "GetProjects.List mapTo {  }");
    getProjects.SetArgumentValue(today, "CurrDate()");
    getProjects.SetArgumentValue(clientId2, "ClientId");
    getProjects.SetArgumentValue(includeInactive2, "IncludeInactive");
    
    var endNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var projects = project_GetList.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(projectSummary);
}
```
