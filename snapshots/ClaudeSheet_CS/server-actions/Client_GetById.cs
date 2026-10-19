(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Client_GetById' ***/
    var client_GetById = eSpace.CreateServerAction("Client_GetById");
    client_GetById.CacheInMinutes = null; // default value
    client_GetById.Description = "One client (ClientRecord) with its projects sorted by name (SheetAdmin only). Raises ClientValidation when the client doesn\'t exist.";
    client_GetById.Folder = null; // default value
    client_GetById.Function = false; // default value
    client_GetById.Icon = null; // default value
    client_GetById.Public = true;
    client_GetById.Shareable = true;
    
    var clientId = client_GetById.CreateInputParameter("ClientId");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client.IdentifierType;
    clientId.Description = null; // default value
    clientId.IsMandatory = true; // default value
    
    var startNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage clients.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getClient = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClient").Below(ifNode, 1371);
    getClient.CacheInMinutes = null; // default value
    getClient.Description = null; // default value
    getClient.IsDisabled = false; // default value
    getClient.SetMaxRecords(null); // default value
    getClient.SetStartIndex(null); // default value
    getClient.Timeout = null; // default value
    var client2 = getClient.AsDatabaseAggregate.CreateSource(client);
    getClient.AsDatabaseAggregate.CreateFilter("Client.Id = ClientId");
    
    ifNode.TrueTarget = getClient;
    
    var ifNode2 = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getClient, 1371);
    ifNode2.SetCondition("GetClient.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Not found?";
    
    var getClientProjects = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClientProjects").Below(ifNode2, 1371);
    getClientProjects.CacheInMinutes = null; // default value
    getClientProjects.Description = null; // default value
    getClientProjects.IsDisabled = false; // default value
    getClientProjects.SetMaxRecords(null); // default value
    getClientProjects.SetStartIndex(null); // default value
    getClientProjects.Timeout = null; // default value
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project = getClientProjects.AsDatabaseAggregate.CreateSource(project2);
    getClientProjects.AsDatabaseAggregate.CreateFilter("Project.ClientId = ClientId");
    getClientProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Name");
    
    ifNode2.FalseTarget = getClientProjects;
    
    var raiseExceptionNode2 = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var clientValidation = eSpace.UserExceptions.Named("ClientValidation");
    raiseExceptionNode2.Exception = clientValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Client not found.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var assignNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getClientProjects, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("ClientRecord", "GetClient.List.Current.Client");
    
    var assignment2 = assignNode.CreateAssignment("Projects", "GetClientProjects.List mapTo {  }");
    
    var endNode = client_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var clientRecord = client_GetById.CreateOutputParameter("ClientRecord");
    clientRecord.DataType = client;
    clientRecord.Description = null; // default value
    
    var projects = client_GetById.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(project2);
    projects.Description = null; // default value
}
