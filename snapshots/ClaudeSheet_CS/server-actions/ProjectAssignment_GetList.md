# ProjectAssignment_GetList

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectAssignment_GetList' ***/
    var projectAssignment_GetList = eSpace.CreateServerAction("ProjectAssignment_GetList");
    projectAssignment_GetList.Description = "Assignments of a project (ProjectId) or of a user (UserId), or both, with project, client and user names and Status on today\'s date (Inactive, Upcoming, Ended, Active), sorted by client, project, user and start date (SheetAdmin only). Raises AssignmentValidation when both are empty.";
    var projectAssignment = eSpace.Folders.Named("ProjectAssignment");
    projectAssignment_GetList.Folder = projectAssignment;
    projectAssignment_GetList.Public = true;
    projectAssignment_GetList.Shareable = true;
    
    var projectId = projectAssignment_GetList.CreateInputParameter("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.IsMandatory = false;
    
    var userId = projectAssignment_GetList.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId.DataType = user.IdentifierType;
    userId.IsMandatory = false;
    
    var startNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage project assignments.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("ProjectId = NullIdentifier() and UserId = NullIdentifier()");
    ifNode2.Label = "No filter?";
    ifNode.TrueTarget = ifNode2;
    
    var getAssignments = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetAssignments").Below(ifNode2, 1371);
    getAssignments.Statement = "SELECT {ProjectAssignment}.[Id], {ProjectAssignment}.[ProjectId], {Project}.[Code], {Project}.[Name], {Client}.[Name],\n  {ProjectAssignment}.[UserId], {User}.[Name], {ProjectAssignment}.[StartDate], {ProjectAssignment}.[EndDate], {ProjectAssignment}.[IsActive],\n  CASE WHEN {ProjectAssignment}.[IsActive] = 0 THEN \'Inactive\'\n       WHEN {ProjectAssignment}.[StartDate] > @Today THEN \'Upcoming\'\n       WHEN {ProjectAssignment}.[EndDate] > \'1900-01-01\' AND {ProjectAssignment}.[EndDate] < @Today THEN \'Ended\'\n       ELSE \'Active\' END\nFROM {ProjectAssignment}\nINNER JOIN {Project} ON {Project}.[Id] = {ProjectAssignment}.[ProjectId]\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nINNER JOIN {User} ON {User}.[Id] = {ProjectAssignment}.[UserId]\nWHERE (COALESCE(@ProjectId, 0) = 0 OR {ProjectAssignment}.[ProjectId] = @ProjectId)\n  AND (COALESCE(@UserId, 0) = 0 OR {ProjectAssignment}.[UserId] = @UserId)\nORDER BY {Client}.[Name], {Project}.[Name], {User}.[Name], {ProjectAssignment}.[StartDate]";
    
    var projectId2 = getAssignments.CreateInputParameter("ProjectId");
    projectId2.DataType = project.IdentifierType;
    
    var userId2 = getAssignments.CreateInputParameter("UserId");
    userId2.DataType = user.IdentifierType;
    
    var today = getAssignments.CreateInputParameter("Today");
    today.DataType = eSpace.DateType;
    
    var projectAssignmentSummary = eSpace.Structures.Named("ProjectAssignmentSummary");
    var projectAssignmentSummary2 = getAssignments.CreateOutput(projectAssignmentSummary, "ProjectAssignmentSummary");
    ifNode2.FalseTarget = getAssignments;
    
    var raiseExceptionNode2 = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var assignmentValidation = eSpace.UserExceptions.Named("AssignmentValidation");
    raiseExceptionNode2.Exception = assignmentValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Choose a project or a user.\"");
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var assignNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getAssignments, 1371);
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("Assignments", "GetAssignments.List mapTo {  }");
    getAssignments.SetArgumentValue(today, "CurrDate()");
    getAssignments.SetArgumentValue(userId2, "UserId");
    getAssignments.SetArgumentValue(projectId2, "ProjectId");
    
    var endNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var assignments = projectAssignment_GetList.CreateOutputParameter("Assignments");
    assignments.DataType = eSpace.GetOrCreateListType(projectAssignmentSummary);
}
```
