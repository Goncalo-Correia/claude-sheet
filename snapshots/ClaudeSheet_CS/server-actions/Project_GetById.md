# Project_GetById

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_GetById' ***/
    var project_GetById = eSpace.CreateServerAction("Project_GetById");
    project_GetById.Description = "One project (ProjectRecord) for the admin detail screen (SheetAdmin only). Raises ProjectValidation when the project doesn\'t exist.";
    var project = eSpace.Folders.Named("Project");
    project_GetById.Folder = project;
    project_GetById.Public = true;
    project_GetById.Shareable = true;
    
    var projectId = project_GetById.CreateInputParameter("ProjectId");
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project2.IdentifierType;
    
    var startNode = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage projects.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getProject = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProject").Below(ifNode, 1371);
    var project3 = getProject.AsDatabaseAggregate.CreateSource(project2);
    getProject.AsDatabaseAggregate.CreateFilter("Project.Id = ProjectId");
    
    ifNode.TrueTarget = getProject;
    
    var ifNode2 = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getProject, 1371);
    ifNode2.SetCondition("GetProject.List.Empty");
    ifNode2.Label = "Not found?";
    
    var assignNode = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("ProjectRecord", "GetProject.List.Current.Project");
    ifNode2.FalseTarget = assignNode;
    
    var raiseExceptionNode2 = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var projectValidation = eSpace.UserExceptions.Named("ProjectValidation");
    raiseExceptionNode2.Exception = projectValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Project not found.\"");
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var endNode = project_GetById.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var projectRecord = project_GetById.CreateOutputParameter("ProjectRecord");
    projectRecord.DataType = project2;
}
```
