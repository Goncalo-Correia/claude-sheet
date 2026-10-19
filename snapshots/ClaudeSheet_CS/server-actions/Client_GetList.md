# Client_GetList

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Client_GetList' ***/
    var client_GetList = eSpace.CreateServerAction("Client_GetList");
    client_GetList.Description = "Clients for the admin list (SheetAdmin only): name, active flag and number of projects, sorted by name. IncludeInactive also returns deactivated clients; Search filters by name (contains).";
    var client = eSpace.Folders.Named("Client");
    client_GetList.Folder = client;
    client_GetList.Public = true;
    client_GetList.Shareable = true;
    
    var includeInactive = client_GetList.CreateInputParameter("IncludeInactive");
    includeInactive.DataType = eSpace.BooleanType;
    includeInactive.IsMandatory = false;
    
    var search = client_GetList.CreateInputParameter("Search");
    search.DataType = eSpace.TextType;
    search.IsMandatory = false;
    
    var startNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage clients.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getClients = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetClients").Below(ifNode, 1371);
    getClients.Statement = "SELECT {Client}.*,\n  (SELECT COUNT(*) FROM {Project} WHERE {Project}.[ClientId] = {Client}.[Id])\nFROM {Client}\nWHERE (@IncludeInactive = 1 OR {Client}.[IsActive] = 1)\n  AND (@Search = \'\' OR CHARINDEX(@Search, {Client}.[Name]) > 0)\nORDER BY {Client}.[Name]";
    
    var includeInactive2 = getClients.CreateInputParameter("IncludeInactive");
    includeInactive2.DataType = eSpace.BooleanType;
    
    var search2 = getClients.CreateInputParameter("Search");
    search2.DataType = eSpace.TextType;
    
    var clientSummary = eSpace.Structures.Named("ClientSummary");
    var clientSummary2 = getClients.CreateOutput(clientSummary, "ClientSummary");
    ifNode.TrueTarget = getClients;
    
    var assignNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getClients, 1371);
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("Clients", "GetClients.List mapTo {  }");
    getClients.SetArgumentValue(includeInactive2, "IncludeInactive");
    getClients.SetArgumentValue(search2, "Trim(Search)");
    
    var endNode = client_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var clients = client_GetList.CreateOutputParameter("Clients");
    clients.DataType = eSpace.GetOrCreateListType(clientSummary);
}
```
