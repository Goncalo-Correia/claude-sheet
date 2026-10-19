# Client_SetActive

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Client_SetActive' ***/
    var client_SetActive = eSpace.CreateServerAction("Client_SetActive");
    client_SetActive.Description = "Activates or deactivates a client (SheetAdmin only); clients are deactivated instead of deleted. Projects of an inactive client are no longer offered to users (Project_GetAssignedToUser filters on the client\'s IsActive).";
    var client = eSpace.Folders.Named("Client");
    client_SetActive.Folder = client;
    client_SetActive.Public = true;
    client_SetActive.Shareable = true;
    
    var clientId = client_SetActive.CreateInputParameter("ClientId");
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client2.IdentifierType;
    
    var isActive = client_SetActive.CreateInputParameter("IsActive");
    isActive.DataType = eSpace.BooleanType;
    
    var clientRecord = client_SetActive.CreateLocalVariable("ClientRecord");
    clientRecord.DataType = client2;
    
    var startNode = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage clients.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getClient = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClient").Below(ifNode, 1371);
    var client3 = getClient.AsDatabaseAggregate.CreateSource(client2);
    getClient.AsDatabaseAggregate.CreateFilter("Client.Id = ClientId");
    
    ifNode.TrueTarget = getClient;
    
    var ifNode2 = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getClient, 1371);
    ifNode2.SetCondition("GetClient.List.Empty");
    ifNode2.Label = "Not found?";
    
    var assignNode = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.Label = "Set IsActive";
    
    var assignment = assignNode.CreateAssignment("ClientRecord", "GetClient.List.Current.Client");
    
    var assignment2 = assignNode.CreateAssignment("ClientRecord.IsActive", "IsActive");
    ifNode2.FalseTarget = assignNode;
    
    var raiseExceptionNode2 = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var clientValidation = eSpace.UserExceptions.Named("ClientValidation");
    raiseExceptionNode2.Exception = clientValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Client not found.\"");
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var updateClient = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateClient").ConnectedBelow(assignNode, 1371);
    updateClient.Action = client2.UpdateAction;
    var updateClient2 = client2.UpdateAction;
    var source = updateClient2.InputParameters.Named("Source");
    updateClient.SetArgumentValue(source, "ClientRecord");
    
    var endNode = client_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateClient, 1371);
}
```
