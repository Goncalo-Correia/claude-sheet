(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_CreateOrUpdate' ***/
    var project_CreateOrUpdate = eSpace.CreateServerAction("Project_CreateOrUpdate");
    project_CreateOrUpdate.CacheInMinutes = null; // default value
    project_CreateOrUpdate.Description = "Creates or updates a project (SheetAdmin only). Name and Code are trimmed and required, the client must exist and be active, and Code is unique per client (case-insensitive by the database collation). Raises ProjectValidation with a user-facing message otherwise. Returns the project\'s Id.";
    var project = eSpace.Folders.Named("Project");
    project_CreateOrUpdate.Folder = project;
    project_CreateOrUpdate.Function = false; // default value
    project_CreateOrUpdate.Icon = null; // default value
    project_CreateOrUpdate.Public = true;
    project_CreateOrUpdate.Shareable = true;
    
    var projectRecord = project_CreateOrUpdate.CreateInputParameter("ProjectRecord");
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectRecord.DataType = project2;
    projectRecord.Description = null; // default value
    projectRecord.IsMandatory = true; // default value
    
    var startNode = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage projects.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("Trim(ProjectRecord.Name) = \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Name empty?";
    ifNode.TrueTarget = ifNode2;
    
    var ifNode3 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("Trim(ProjectRecord.Code) = \"\"");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Code empty?";
    ifNode2.FalseTarget = ifNode3;
    
    var raiseExceptionNode2 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var projectValidation = eSpace.UserExceptions.Named("ProjectValidation");
    raiseExceptionNode2.Exception = projectValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Enter the project name.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var getClient = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClient").Below(ifNode3, 1371);
    getClient.CacheInMinutes = null; // default value
    getClient.Description = null; // default value
    getClient.IsDisabled = false; // default value
    getClient.SetMaxRecords(null); // default value
    getClient.SetStartIndex(null); // default value
    getClient.Timeout = null; // default value
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client = getClient.AsDatabaseAggregate.CreateSource(client2);
    getClient.AsDatabaseAggregate.CreateFilter("Client.Id = ProjectRecord.ClientId");
    
    ifNode3.FalseTarget = getClient;
    
    var raiseExceptionNode3 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = projectValidation;
    raiseExceptionNode3.SetExceptionMessage("\"Enter the project code.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var ifNode4 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getClient, 1371);
    ifNode4.SetCondition("GetClient.List.Empty");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "No client?";
    
    var ifNode5 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode4, 1371);
    ifNode5.SetCondition("not GetClient.List.Current.Client.IsActive");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "Client inactive?";
    ifNode4.FalseTarget = ifNode5;
    
    var raiseExceptionNode4 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = projectValidation;
    raiseExceptionNode4.SetExceptionMessage("\"Choose a client.\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var getSameCode = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSameCode").Below(ifNode5, 1371);
    getSameCode.CacheInMinutes = null; // default value
    getSameCode.Description = null; // default value
    getSameCode.IsDisabled = false; // default value
    getSameCode.SetMaxRecords(null); // default value
    getSameCode.SetStartIndex(null); // default value
    getSameCode.Timeout = null; // default value
    var project3 = getSameCode.AsDatabaseAggregate.CreateSource(project2);
    getSameCode.AsDatabaseAggregate.CreateFilter("Project.ClientId = ProjectRecord.ClientId");
    getSameCode.AsDatabaseAggregate.CreateFilter("Project.Code = Trim(ProjectRecord.Code)");
    getSameCode.AsDatabaseAggregate.CreateFilter("Project.Id <> ProjectRecord.Id");
    
    ifNode5.FalseTarget = getSameCode;
    
    var raiseExceptionNode5 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode5, 1828);
    raiseExceptionNode5.Exception = projectValidation;
    raiseExceptionNode5.SetExceptionMessage("\"This client is inactive. Activate it first or choose another client.\"");
    raiseExceptionNode5.IsDisabled = false; // default value
    ifNode5.TrueTarget = raiseExceptionNode5;
    
    var ifNode6 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSameCode, 1371);
    ifNode6.SetCondition("not GetSameCode.List.Empty");
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = "Code taken?";
    
    var assignNode = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode6, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Trim name and code";
    
    var assignment = assignNode.CreateAssignment("ProjectRecord.Name", "Trim(ProjectRecord.Name)");
    
    var assignment2 = assignNode.CreateAssignment("ProjectRecord.Code", "Trim(ProjectRecord.Code)");
    ifNode6.FalseTarget = assignNode;
    
    var raiseExceptionNode6 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode6, 1828);
    raiseExceptionNode6.Exception = projectValidation;
    raiseExceptionNode6.SetExceptionMessage("\"A project with this code already exists for this client.\"");
    raiseExceptionNode6.IsDisabled = false; // default value
    ifNode6.TrueTarget = raiseExceptionNode6;
    
    var createOrUpdateProject = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateProject").ConnectedBelow(assignNode, 1371);
    createOrUpdateProject.Action = project2.CreateOrUpdateAction;
    createOrUpdateProject.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createOrUpdateProject.IsDisabled = false; // default value
    createOrUpdateProject.ServerRequestTimeout = null; // default value
    var createOrUpdateProject2 = project2.CreateOrUpdateAction;
    var source = createOrUpdateProject2.InputParameters.Named("Source");
    createOrUpdateProject.SetArgumentValue(source, "ProjectRecord");
    
    var assignNode2 = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createOrUpdateProject, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Return Id";
    
    var assignment3 = assignNode2.CreateAssignment("ProjectId", "CreateOrUpdateProject.Id");
    
    var endNode = project_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode.IsDisabled = false; // default value
    
    var projectId = project_CreateOrUpdate.CreateOutputParameter("ProjectId");
    projectId.DataType = project2.IdentifierType;
    projectId.SetDefaultValue(null); // default value
    projectId.Description = null; // default value
}
