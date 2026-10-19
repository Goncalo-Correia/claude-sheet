(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_SetActive' ***/
    var project_SetActive = eSpace.CreateServerAction("Project_SetActive");
    project_SetActive.CacheInMinutes = null; // default value
    project_SetActive.Description = "Activates or deactivates a project (SheetAdmin only); projects are deactivated instead of deleted (ProjectAssignment.ProjectId and TimeEntry.ProjectId are Protect). Inactive projects are no longer offered to users (Project_GetAssignedToUser filters on Project.IsActive).";
    project_SetActive.Folder = null; // default value
    project_SetActive.Function = false; // default value
    project_SetActive.Icon = null; // default value
    project_SetActive.Public = true;
    project_SetActive.Shareable = true;
    
    var projectId = project_SetActive.CreateInputParameter("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.Description = null; // default value
    projectId.IsMandatory = true; // default value
    
    var isActive = project_SetActive.CreateInputParameter("IsActive");
    isActive.DataType = eSpace.BooleanType;
    isActive.Description = null; // default value
    isActive.IsMandatory = true; // default value
    
    var projectRecord = project_SetActive.CreateLocalVariable("ProjectRecord");
    projectRecord.DataType = project;
    projectRecord.Description = null; // default value
    
    var startNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage projects.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getProject = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProject").Below(ifNode, 1371);
    getProject.CacheInMinutes = null; // default value
    getProject.Description = null; // default value
    getProject.IsDisabled = false; // default value
    getProject.SetMaxRecords(null); // default value
    getProject.SetStartIndex(null); // default value
    getProject.Timeout = null; // default value
    var project2 = getProject.AsDatabaseAggregate.CreateSource(project);
    getProject.AsDatabaseAggregate.CreateFilter("Project.Id = ProjectId");
    
    ifNode.TrueTarget = getProject;
    
    var ifNode2 = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getProject, 1371);
    ifNode2.SetCondition("GetProject.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Not found?";
    
    var assignNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("ProjectRecord", "GetProject.List.Current.Project");
    
    var assignment2 = assignNode.CreateAssignment("ProjectRecord.IsActive", "IsActive");
    ifNode2.FalseTarget = assignNode;
    
    var raiseExceptionNode2 = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var projectValidation = eSpace.UserExceptions.Named("ProjectValidation");
    raiseExceptionNode2.Exception = projectValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Project not found.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var updateProject = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateProject").ConnectedBelow(assignNode, 1371);
    updateProject.Action = project.UpdateAction;
    updateProject.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateProject.IsDisabled = false; // default value
    updateProject.ServerRequestTimeout = null; // default value
    var updateProject2 = project.UpdateAction;
    var source = updateProject2.InputParameters.Named("Source");
    updateProject.SetArgumentValue(source, "ProjectRecord");
    
    var endNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateProject, 1371);
    endNode.IsDisabled = false; // default value
}
