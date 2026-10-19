# Client_GetById

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Client_GetById' ***/
    var client_GetById = eSpace.CreateServerAction("Client_GetById");
    client_GetById.Description = "One client (ClientRecord) with its projects sorted by name (SheetAdmin only). Raises ClientValidation when the client doesn\'t exist.";
    var client = eSpace.Folders.Named("Client");
    client_GetById.Folder = client;
    client_GetById.Public = true;
    client_GetById.Shareable = true;
    
    var clientId = client_GetById.CreateInputParameter("ClientId");
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client2.IdentifierType;
    
    var startNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage clients.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getClient = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClient").Below(ifNode, 1371);
    var client3 = getClient.AsDatabaseAggregate.CreateSource(client2);
    getClient.AsDatabaseAggregate.CreateFilter("Client.Id = ClientId");
    
    ifNode.TrueTarget = getClient;
    
    var ifNode2 = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getClient, 1371);
    ifNode2.SetCondition("GetClient.List.Empty");
    ifNode2.Label = "Not found?";
    
    var getClientProjects = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClientProjects").Below(ifNode2, 1371);
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project = getClientProjects.AsDatabaseAggregate.CreateSource(project2);
    getClientProjects.AsDatabaseAggregate.CreateFilter("Project.ClientId = ClientId");
    getClientProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Name");
    
    ifNode2.FalseTarget = getClientProjects;
    
    var raiseExceptionNode2 = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var clientValidation = eSpace.UserExceptions.Named("ClientValidation");
    raiseExceptionNode2.Exception = clientValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Client not found.\"");
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var assignNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getClientProjects, 1371);
    assignNode.Label = "Set outputs";
    
    var assignment = assignNode.CreateAssignment("ClientRecord", "GetClient.List.Current.Client");
    
    var assignment2 = assignNode.CreateAssignment("Projects", "GetClientProjects.List mapTo {  }");
    
    var endNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var clientRecord = client_GetById.CreateOutputParameter("ClientRecord");
    clientRecord.DataType = client2;
    
    var projects = client_GetById.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(project2);
}
```
