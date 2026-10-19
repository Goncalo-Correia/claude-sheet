(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_GetList' ***/
    var project_GetList = eSpace.CreateServerAction("Project_GetList");
    project_GetList.CacheInMinutes = null; // default value
    project_GetList.Description = "Projects for the admin list (SheetAdmin only), with client name and the number of distinct users with an active assignment that hasn\'t ended, sorted by client and project name. ClientId filters by client (empty = all); IncludeInactive also returns deactivated projects.";
    project_GetList.Folder = null; // default value
    project_GetList.Function = false; // default value
    project_GetList.Icon = null; // default value
    project_GetList.Public = true;
    project_GetList.Shareable = true;
    
    var clientId = project_GetList.CreateInputParameter("ClientId");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client.IdentifierType;
    clientId.SetDefaultValue(null); // default value
    clientId.Description = null; // default value
    clientId.IsMandatory = false;
    
    var includeInactive = project_GetList.CreateInputParameter("IncludeInactive");
    includeInactive.DataType = eSpace.BooleanType;
    includeInactive.SetDefaultValue(null); // default value
    includeInactive.Description = null; // default value
    includeInactive.IsMandatory = false;
    
    var startNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage projects.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getProjects = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetProjects").Below(ifNode, 1371);
    getProjects.CacheInMinutes = null; // default value
    getProjects.Description = null; // default value
    getProjects.IsDisabled = false; // default value
    getProjects.SetMaxRecords(null); // default value
    getProjects.Statement = "SELECT {Project}.[Id], {Project}.[ClientId], {Client}.[Name], {Project}.[Code], {Project}.[Name], {Project}.[IsActive],\n  (SELECT COUNT(DISTINCT {ProjectAssignment}.[UserId]) FROM {ProjectAssignment}\n   WHERE {ProjectAssignment}.[ProjectId] = {Project}.[Id] AND {ProjectAssignment}.[IsActive] = 1\n     AND ({ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] <= \'1900-01-01\' OR {ProjectAssignment}.[EndDate] >= @Today))\nFROM {Project}\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nWHERE (@IncludeInactive = 1 OR {Project}.[IsActive] = 1)\n  AND (COALESCE(@ClientId, 0) = 0 OR {Project}.[ClientId] = @ClientId)\nORDER BY {Client}.[Name], {Project}.[Name]";
    getProjects.Timeout = null; // default value
    
    var clientId2 = getProjects.CreateInputParameter("ClientId");
    clientId2.DataType = client.IdentifierType;
    clientId2.SetDefaultValue(null); // default value
    clientId2.Description = null; // default value
    clientId2.ExpandInline = false; // default value
    
    var includeInactive2 = getProjects.CreateInputParameter("IncludeInactive");
    includeInactive2.DataType = eSpace.BooleanType;
    includeInactive2.SetDefaultValue(null); // default value
    includeInactive2.Description = null; // default value
    includeInactive2.ExpandInline = false; // default value
    
    var today = getProjects.CreateInputParameter("Today");
    today.DataType = eSpace.DateType;
    today.SetDefaultValue(null); // default value
    today.Description = null; // default value
    today.ExpandInline = false; // default value
    
    var projectSummary = eSpace.Structures.Named("ProjectSummary");
    var projectSummary2 = getProjects.CreateOutput(projectSummary, "ProjectSummary");
    ifNode.TrueTarget = getProjects;
    
    var assignNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getProjects, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Projects", "GetProjects.List mapTo {  }");
    getProjects.SetArgumentValue(today, "CurrDate()");
    getProjects.SetArgumentValue(clientId2, "ClientId");
    getProjects.SetArgumentValue(includeInactive2, "IncludeInactive");
    
    var endNode = project_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var projects = project_GetList.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(projectSummary);
    projects.Description = null; // default value
}
