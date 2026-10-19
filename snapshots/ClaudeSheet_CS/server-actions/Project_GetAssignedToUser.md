# Project_GetAssignedToUser

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_GetAssignedToUser' ***/
    var project_GetAssignedToUser = eSpace.CreateServerAction("Project_GetAssignedToUser");
    project_GetAssignedToUser.Description = "Active projects (with active client) the current user is assigned to on ForDate, sorted by client and project name.";
    var project = eSpace.Folders.Named("Project");
    project_GetAssignedToUser.Folder = project;
    project_GetAssignedToUser.Public = true;
    project_GetAssignedToUser.Shareable = true;
    
    var forDate = project_GetAssignedToUser.CreateInputParameter("ForDate");
    forDate.DataType = eSpace.DateType;
    
    var line = project_GetAssignedToUser.CreateLocalVariable("Line");
    var daySheetProjectLine = eSpace.Structures.Named("DaySheetProjectLine");
    line.DataType = daySheetProjectLine;
    
    var startNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignedProjects = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignedProjects").Below(ifNode, 1371);
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    var projectAssignment = getAssignedProjects.AsDatabaseAggregate.CreateSource(projectAssignment2);
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client = getAssignedProjects.AsDatabaseAggregate.CreateSource(client2);
    var project3 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project2 = getAssignedProjects.AsDatabaseAggregate.CreateSource(project3);
    var join = getAssignedProjects.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = projectAssignment;
    join.RightSource = project2;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("ProjectAssignment.ProjectId = Project.Id");
    var join2 = getAssignedProjects.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = project2;
    join2.RightSource = client;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Project.ClientId = Client.Id");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = GetUserId()");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.StartDate <= ForDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.EndDate = NullDate() or ProjectAssignment.EndDate >= ForDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Project.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Client.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Client.Name");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Name");
    
    ifNode.TrueTarget = getAssignedProjects;
    
    var forEachNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getAssignedProjects, 1371);
    forEachNode.Label = "Each project";
    forEachNode.SetRecordList("GetAssignedProjects.List");
    
    var assignNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode.Label = "Build line";
    
    var assignment = assignNode.CreateAssignment("Line.ProjectId", "GetAssignedProjects.List.Current.Project.Id");
    
    var assignment2 = assignNode.CreateAssignment("Line.ClientName", "GetAssignedProjects.List.Current.Client.Name");
    
    var assignment3 = assignNode.CreateAssignment("Line.ProjectName", "GetAssignedProjects.List.Current.Project.Name");
    
    var assignment4 = assignNode.CreateAssignment("Line.ProjectCode", "GetAssignedProjects.List.Current.Project.Code");
    
    var assignment5 = assignNode.CreateAssignment("Line.TimeEntryId", "NullIdentifier()");
    
    var assignment6 = assignNode.CreateAssignment("Line.Minutes", "0");
    
    var assignment7 = assignNode.CreateAssignment("Line.Description", "\"\"");
    forEachNode.CycleTarget = assignNode;
    
    var endNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 2742);
    forEachNode.Target = endNode;
    
    var listAppend = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.Target = forEachNode;
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Projects");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Line");
    
    var projects = project_GetAssignedToUser.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
}
```
