(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectAssignment_CreateOrUpdate' ***/
    var projectAssignment_CreateOrUpdate = eSpace.CreateServerAction("ProjectAssignment_CreateOrUpdate");
    projectAssignment_CreateOrUpdate.CacheInMinutes = null; // default value
    projectAssignment_CreateOrUpdate.Description = "Creates or updates an assignment (SheetAdmin only): user and start date required; EndDate empty (open end) or not before StartDate; project and its client active; no overlap with another active period of the same user and project, nor the same start date (ADR 0002). Raises AssignmentValidation with a user-facing message otherwise. Saves the assignment as active and returns its Id.";
    var projectAssignment = eSpace.Folders.Named("ProjectAssignment");
    projectAssignment_CreateOrUpdate.Folder = projectAssignment;
    projectAssignment_CreateOrUpdate.Function = false; // default value
    projectAssignment_CreateOrUpdate.Icon = null; // default value
    projectAssignment_CreateOrUpdate.Public = true;
    projectAssignment_CreateOrUpdate.Shareable = true;
    
    var projectAssignmentRecord = projectAssignment_CreateOrUpdate.CreateInputParameter("ProjectAssignmentRecord");
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    projectAssignmentRecord.DataType = projectAssignment2;
    projectAssignmentRecord.Description = null; // default value
    projectAssignmentRecord.IsMandatory = true; // default value
    
    var startNode = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage project assignments.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("ProjectAssignmentRecord.UserId = NullIdentifier()");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "No user?";
    ifNode.TrueTarget = ifNode2;
    
    var ifNode3 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("ProjectAssignmentRecord.StartDate = NullDate()");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "No start date?";
    ifNode2.FalseTarget = ifNode3;
    
    var raiseExceptionNode2 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var assignmentValidation = eSpace.UserExceptions.Named("AssignmentValidation");
    raiseExceptionNode2.Exception = assignmentValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Choose a user.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode4 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode3, 1371);
    ifNode4.SetCondition("ProjectAssignmentRecord.EndDate <> NullDate() and ProjectAssignmentRecord.EndDate < ProjectAssignmentRecord.StartDate");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "End before start?";
    ifNode3.FalseTarget = ifNode4;
    
    var raiseExceptionNode3 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = assignmentValidation;
    raiseExceptionNode3.SetExceptionMessage("\"Enter the start date.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var getProject = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProject").Below(ifNode4, 1371);
    getProject.CacheInMinutes = null; // default value
    getProject.Description = null; // default value
    getProject.IsDisabled = false; // default value
    getProject.SetMaxRecords(null); // default value
    getProject.SetStartIndex(null); // default value
    getProject.Timeout = null; // default value
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project = getProject.AsDatabaseAggregate.CreateSource(project2);
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client = getProject.AsDatabaseAggregate.CreateSource(client2);
    var join = getProject.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = project;
    join.RightSource = client;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("Project.ClientId = Client.Id");
    getProject.AsDatabaseAggregate.CreateFilter("Project.Id = ProjectAssignmentRecord.ProjectId");
    
    ifNode4.FalseTarget = getProject;
    
    var raiseExceptionNode4 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = assignmentValidation;
    raiseExceptionNode4.SetExceptionMessage("\"The end date can\'t be before the start date.\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var ifNode5 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getProject, 1371);
    ifNode5.SetCondition("GetProject.List.Empty");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "No project?";
    
    var ifNode6 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode5, 1371);
    ifNode6.SetCondition("not GetProject.List.Current.Project.IsActive or not GetProject.List.Current.Client.IsActive");
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = "Project inactive?";
    ifNode5.FalseTarget = ifNode6;
    
    var raiseExceptionNode5 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode5, 1828);
    raiseExceptionNode5.Exception = assignmentValidation;
    raiseExceptionNode5.SetExceptionMessage("\"Project not found.\"");
    raiseExceptionNode5.IsDisabled = false; // default value
    ifNode5.TrueTarget = raiseExceptionNode5;
    
    var getOverlapping = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetOverlapping").Below(ifNode6, 1371);
    getOverlapping.CacheInMinutes = null; // default value
    getOverlapping.Description = null; // default value
    getOverlapping.IsDisabled = false; // default value
    getOverlapping.SetMaxRecords(null); // default value
    getOverlapping.SetStartIndex(null); // default value
    getOverlapping.Timeout = null; // default value
    var projectAssignment3 = getOverlapping.AsDatabaseAggregate.CreateSource(projectAssignment2);
    getOverlapping.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = ProjectAssignmentRecord.UserId");
    getOverlapping.AsDatabaseAggregate.CreateFilter("ProjectAssignment.ProjectId = ProjectAssignmentRecord.ProjectId");
    getOverlapping.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id <> ProjectAssignmentRecord.Id");
    getOverlapping.AsDatabaseAggregate.CreateFilter("ProjectAssignment.IsActive or ProjectAssignment.StartDate = ProjectAssignmentRecord.StartDate");
    getOverlapping.AsDatabaseAggregate.CreateFilter("ProjectAssignmentRecord.EndDate = NullDate() or ProjectAssignment.StartDate <= ProjectAssignmentRecord.EndDate");
    getOverlapping.AsDatabaseAggregate.CreateFilter("ProjectAssignment.EndDate = NullDate() or ProjectAssignment.EndDate >= ProjectAssignmentRecord.StartDate");
    
    ifNode6.FalseTarget = getOverlapping;
    
    var raiseExceptionNode6 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode6, 1828);
    raiseExceptionNode6.Exception = assignmentValidation;
    raiseExceptionNode6.SetExceptionMessage("\"This project or its client is inactive.\"");
    raiseExceptionNode6.IsDisabled = false; // default value
    ifNode6.TrueTarget = raiseExceptionNode6;
    
    var ifNode7 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getOverlapping, 1371);
    ifNode7.SetCondition("not GetOverlapping.List.Empty");
    ifNode7.IsDisabled = false; // default value
    ifNode7.Label = "Overlaps?";
    
    var assignNode = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode7, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Mark active";
    
    var assignment = assignNode.CreateAssignment("ProjectAssignmentRecord.IsActive", "True");
    ifNode7.FalseTarget = assignNode;
    
    var raiseExceptionNode7 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode7, 1828);
    raiseExceptionNode7.Exception = assignmentValidation;
    raiseExceptionNode7.SetExceptionMessage("\"This user is already assigned to this project in an overlapping period.\"");
    raiseExceptionNode7.IsDisabled = false; // default value
    ifNode7.TrueTarget = raiseExceptionNode7;
    
    var createOrUpdateProjectAssignment = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateProjectAssignment").ConnectedBelow(assignNode, 1371);
    createOrUpdateProjectAssignment.Action = projectAssignment2.CreateOrUpdateAction;
    createOrUpdateProjectAssignment.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createOrUpdateProjectAssignment.IsDisabled = false; // default value
    createOrUpdateProjectAssignment.ServerRequestTimeout = null; // default value
    var createOrUpdateProjectAssignment2 = projectAssignment2.CreateOrUpdateAction;
    var source = createOrUpdateProjectAssignment2.InputParameters.Named("Source");
    createOrUpdateProjectAssignment.SetArgumentValue(source, "ProjectAssignmentRecord");
    
    var assignNode2 = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createOrUpdateProjectAssignment, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Return Id";
    
    var assignment2 = assignNode2.CreateAssignment("ProjectAssignmentId", "CreateOrUpdateProjectAssignment.Id");
    
    var endNode = projectAssignment_CreateOrUpdate.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode.IsDisabled = false; // default value
    
    var projectAssignmentId = projectAssignment_CreateOrUpdate.CreateOutputParameter("ProjectAssignmentId");
    projectAssignmentId.DataType = projectAssignment2.IdentifierType;
    projectAssignmentId.SetDefaultValue(null); // default value
    projectAssignmentId.Description = null; // default value
}
