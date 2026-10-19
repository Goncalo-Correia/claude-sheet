(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Client_GetList' ***/
    var client_GetList = eSpace.CreateServerAction("Client_GetList");
    client_GetList.CacheInMinutes = null; // default value
    client_GetList.Description = "Clients for the admin list (SheetAdmin only): name, active flag and number of projects, sorted by name. IncludeInactive also returns deactivated clients; Search filters by name (contains).";
    var client = eSpace.Folders.Named("Client");
    client_GetList.Folder = client;
    client_GetList.Function = false; // default value
    client_GetList.Icon = null; // default value
    client_GetList.Public = true;
    client_GetList.Shareable = true;
    
    var includeInactive = client_GetList.CreateInputParameter("IncludeInactive");
    includeInactive.DataType = eSpace.BooleanType;
    includeInactive.SetDefaultValue(null); // default value
    includeInactive.Description = null; // default value
    includeInactive.IsMandatory = false;
    
    var search = client_GetList.CreateInputParameter("Search");
    search.DataType = eSpace.TextType;
    search.SetDefaultValue(null); // default value
    search.Description = null; // default value
    search.IsMandatory = false;
    
    var startNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage clients.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getClients = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetClients").Below(ifNode, 1371);
    getClients.CacheInMinutes = null; // default value
    getClients.Description = null; // default value
    getClients.IsDisabled = false; // default value
    getClients.SetMaxRecords(null); // default value
    getClients.Statement = "SELECT {Client}.*,\n  (SELECT COUNT(*) FROM {Project} WHERE {Project}.[ClientId] = {Client}.[Id])\nFROM {Client}\nWHERE (@IncludeInactive = 1 OR {Client}.[IsActive] = 1)\n  AND (@Search = \'\' OR CHARINDEX(@Search, {Client}.[Name]) > 0)\nORDER BY {Client}.[Name]";
    getClients.Timeout = null; // default value
    
    var includeInactive2 = getClients.CreateInputParameter("IncludeInactive");
    includeInactive2.DataType = eSpace.BooleanType;
    includeInactive2.SetDefaultValue(null); // default value
    includeInactive2.Description = null; // default value
    includeInactive2.ExpandInline = false; // default value
    
    var search2 = getClients.CreateInputParameter("Search");
    search2.DataType = eSpace.TextType;
    search2.SetDefaultValue(null); // default value
    search2.Description = null; // default value
    search2.ExpandInline = false; // default value
    
    var clientSummary = eSpace.Structures.Named("ClientSummary");
    var clientSummary2 = getClients.CreateOutput(clientSummary, "ClientSummary");
    ifNode.TrueTarget = getClients;
    
    var assignNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getClients, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("Clients", "GetClients.List mapTo {  }");
    getClients.SetArgumentValue(includeInactive2, "IncludeInactive");
    getClients.SetArgumentValue(search2, "Trim(Search)");
    
    var endNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var clients = client_GetList.CreateOutputParameter("Clients");
    clients.DataType = eSpace.GetOrCreateListType(clientSummary);
    clients.Description = null; // default value
}
