(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Client_CreateOrUpdate' ***/
    var client_CreateOrUpdate = eSpace.CreateServerAction("Client_CreateOrUpdate");
    client_CreateOrUpdate.CacheInMinutes = null; // default value
    client_CreateOrUpdate.Description = "Creates or updates a client (SheetAdmin only). Name is trimmed, required and unique (case-insensitive by the database collation); raises ClientValidation with a user-facing message otherwise. Returns the client\'s Id.";
    var client = eSpace.Folders.Named("Client");
    client_CreateOrUpdate.Folder = client;
    client_CreateOrUpdate.Function = false; // default value
    client_CreateOrUpdate.Icon = null; // default value
    client_CreateOrUpdate.Public = true;
    client_CreateOrUpdate.Shareable = true;
    
    var clientRecord = client_CreateOrUpdate.CreateInputParameter("ClientRecord");
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientRecord.DataType = client2;
    clientRecord.Description = null; // default value
    clientRecord.IsMandatory = true; // default value
    
    var startNode = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage clients.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("Trim(ClientRecord.Name) = \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Name empty?";
    ifNode.TrueTarget = ifNode2;
    
    var getSameName = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSameName").Below(ifNode2, 1371);
    getSameName.CacheInMinutes = null; // default value
    getSameName.Description = null; // default value
    getSameName.IsDisabled = false; // default value
    getSameName.SetMaxRecords(null); // default value
    getSameName.SetStartIndex(null); // default value
    getSameName.Timeout = null; // default value
    var client3 = getSameName.AsDatabaseAggregate.CreateSource(client2);
    getSameName.AsDatabaseAggregate.CreateFilter("Client.Name = Trim(ClientRecord.Name)");
    getSameName.AsDatabaseAggregate.CreateFilter("Client.Id <> ClientRecord.Id");
    
    ifNode2.FalseTarget = getSameName;
    
    var raiseExceptionNode2 = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var clientValidation = eSpace.UserExceptions.Named("ClientValidation");
    raiseExceptionNode2.Exception = clientValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Enter the client name.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode3 = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSameName, 1371);
    ifNode3.SetCondition("not GetSameName.List.Empty");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Name taken?";
    
    var assignNode = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode3, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Trim name";
    
    var assignment = assignNode.CreateAssignment("ClientRecord.Name", "Trim(ClientRecord.Name)");
    ifNode3.FalseTarget = assignNode;
    
    var raiseExceptionNode3 = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = clientValidation;
    raiseExceptionNode3.SetExceptionMessage("\"A client with this name already exists.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var createOrUpdateClient = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateClient").ConnectedBelow(assignNode, 1371);
    createOrUpdateClient.Action = client2.CreateOrUpdateAction;
    createOrUpdateClient.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createOrUpdateClient.IsDisabled = false; // default value
    createOrUpdateClient.ServerRequestTimeout = null; // default value
    var createOrUpdateClient2 = client2.CreateOrUpdateAction;
    var source = createOrUpdateClient2.InputParameters.Named("Source");
    createOrUpdateClient.SetArgumentValue(source, "ClientRecord");
    
    var assignNode2 = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createOrUpdateClient, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Return Id";
    
    var assignment2 = assignNode2.CreateAssignment("ClientId", "CreateOrUpdateClient.Id");
    
    var endNode = client_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode.IsDisabled = false; // default value
    
    var clientId = client_CreateOrUpdate.CreateOutputParameter("ClientId");
    clientId.DataType = client2.IdentifierType;
    clientId.SetDefaultValue(null); // default value
    clientId.Description = null; // default value
}
