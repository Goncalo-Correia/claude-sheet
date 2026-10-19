# Project_SetActive

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_SetActive' ***/
    var project_SetActive = eSpace.CreateServerAction("Project_SetActive");
    project_SetActive.Description = "Activates or deactivates a project (SheetAdmin only); projects are deactivated instead of deleted (ProjectAssignment.ProjectId and TimeEntry.ProjectId are Protect). Inactive projects are no longer offered to users (Project_GetAssignedToUser filters on Project.IsActive).";
    var project = eSpace.Folders.Named("Project");
    project_SetActive.Folder = project;
    project_SetActive.Public = true;
    project_SetActive.Shareable = true;
    
    var projectId = project_SetActive.CreateInputParameter("ProjectId");
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project2.IdentifierType;
    
    var isActive = project_SetActive.CreateInputParameter("IsActive");
    isActive.DataType = eSpace.BooleanType;
    
    var projectRecord = project_SetActive.CreateLocalVariable("ProjectRecord");
    projectRecord.DataType = project2;
    
    var startNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage projects.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getProject = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProject").Below(ifNode, 1371);
    var project3 = getProject.AsDatabaseAggregate.CreateSource(project2);
    getProject.AsDatabaseAggregate.CreateFilter("Project.Id = ProjectId");
    
    ifNode.TrueTarget = getProject;
    
    var ifNode2 = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getProject, 1371);
    ifNode2.SetCondition("GetProject.List.Empty");
    ifNode2.Label = "Not found?";
    
    var assignNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.Label = "Set IsActive";
    
    var assignment = assignNode.CreateAssignment("ProjectRecord", "GetProject.List.Current.Project");
    
    var assignment2 = assignNode.CreateAssignment("ProjectRecord.IsActive", "IsActive");
    ifNode2.FalseTarget = assignNode;
    
    var raiseExceptionNode2 = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var projectValidation = eSpace.UserExceptions.Named("ProjectValidation");
    raiseExceptionNode2.Exception = projectValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Project not found.\"");
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var updateProject = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateProject").ConnectedBelow(assignNode, 1371);
    updateProject.Action = project2.UpdateAction;
    var updateProject2 = project2.UpdateAction;
    var source = updateProject2.InputParameters.Named("Source");
    updateProject.SetArgumentValue(source, "ProjectRecord");
    
    var endNode = project_SetActive.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateProject, 1371);
}
```
