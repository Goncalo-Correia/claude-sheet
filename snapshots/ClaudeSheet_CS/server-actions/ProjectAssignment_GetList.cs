(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectAssignment_GetList' ***/
    var projectAssignment_GetList = eSpace.CreateServerAction("ProjectAssignment_GetList");
    projectAssignment_GetList.CacheInMinutes = null; // default value
    projectAssignment_GetList.Description = "Assignments of a project (ProjectId) or of a user (UserId), or both, with project, client and user names and Status on today\'s date (Inactive, Upcoming, Ended, Active), sorted by client, project, user and start date (SheetAdmin only). Raises AssignmentValidation when both are empty.";
    var projectAssignment = eSpace.Folders.Named("ProjectAssignment");
    projectAssignment_GetList.Folder = projectAssignment;
    projectAssignment_GetList.Function = false; // default value
    projectAssignment_GetList.Icon = null; // default value
    projectAssignment_GetList.Public = true;
    projectAssignment_GetList.Shareable = true;
    
    var projectId = projectAssignment_GetList.CreateInputParameter("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.SetDefaultValue(null); // default value
    projectId.Description = null; // default value
    projectId.IsMandatory = false;
    
    var userId = projectAssignment_GetList.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId.DataType = user.IdentifierType;
    userId.SetDefaultValue(null); // default value
    userId.Description = null; // default value
    userId.IsMandatory = false;
    
    var startNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage project assignments.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("ProjectId = NullIdentifier() and UserId = NullIdentifier()");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "No filter?";
    ifNode.TrueTarget = ifNode2;
    
    var getAssignments = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetAssignments").Below(ifNode2, 1371);
    getAssignments.CacheInMinutes = null; // default value
    getAssignments.Description = null; // default value
    getAssignments.IsDisabled = false; // default value
    getAssignments.SetMaxRecords(null); // default value
    getAssignments.Statement = "SELECT {ProjectAssignment}.[Id], {ProjectAssignment}.[ProjectId], {Project}.[Code], {Project}.[Name], {Client}.[Name],\n  {ProjectAssignment}.[UserId], {User}.[Name], {ProjectAssignment}.[StartDate], {ProjectAssignment}.[EndDate], {ProjectAssignment}.[IsActive],\n  CASE WHEN {ProjectAssignment}.[IsActive] = 0 THEN \'Inactive\'\n       WHEN {ProjectAssignment}.[StartDate] > @Today THEN \'Upcoming\'\n       WHEN {ProjectAssignment}.[EndDate] > \'1900-01-01\' AND {ProjectAssignment}.[EndDate] < @Today THEN \'Ended\'\n       ELSE \'Active\' END\nFROM {ProjectAssignment}\nINNER JOIN {Project} ON {Project}.[Id] = {ProjectAssignment}.[ProjectId]\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nINNER JOIN {User} ON {User}.[Id] = {ProjectAssignment}.[UserId]\nWHERE (COALESCE(@ProjectId, 0) = 0 OR {ProjectAssignment}.[ProjectId] = @ProjectId)\n  AND (COALESCE(@UserId, 0) = 0 OR {ProjectAssignment}.[UserId] = @UserId)\nORDER BY {Client}.[Name], {Project}.[Name], {User}.[Name], {ProjectAssignment}.[StartDate]";
    getAssignments.Timeout = null; // default value
    
    var projectId2 = getAssignments.CreateInputParameter("ProjectId");
    projectId2.DataType = project.IdentifierType;
    projectId2.SetDefaultValue(null); // default value
    projectId2.Description = null; // default value
    projectId2.ExpandInline = false; // default value
    
    var userId2 = getAssignments.CreateInputParameter("UserId");
    userId2.DataType = user.IdentifierType;
    userId2.SetDefaultValue(null); // default value
    userId2.Description = null; // default value
    userId2.ExpandInline = false; // default value
    
    var today = getAssignments.CreateInputParameter("Today");
    today.DataType = eSpace.DateType;
    today.SetDefaultValue(null); // default value
    today.Description = null; // default value
    today.ExpandInline = false; // default value
    
    var projectAssignmentSummary = eSpace.Structures.Named("ProjectAssignmentSummary");
    var projectAssignmentSummary2 = getAssignments.CreateOutput(projectAssignmentSummary, "ProjectAssignmentSummary");
    ifNode2.FalseTarget = getAssignments;
    
    var raiseExceptionNode2 = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var assignmentValidation = eSpace.UserExceptions.Named("AssignmentValidation");
    raiseExceptionNode2.Exception = assignmentValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Choose a project or a user.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var assignNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getAssignments, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("Assignments", "GetAssignments.List mapTo {  }");
    getAssignments.SetArgumentValue(today, "CurrDate()");
    getAssignments.SetArgumentValue(userId2, "UserId");
    getAssignments.SetArgumentValue(projectId2, "ProjectId");
    
    var endNode = projectAssignment_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var assignments = projectAssignment_GetList.CreateOutputParameter("Assignments");
    assignments.DataType = eSpace.GetOrCreateListType(projectAssignmentSummary);
    assignments.Description = null; // default value
}
