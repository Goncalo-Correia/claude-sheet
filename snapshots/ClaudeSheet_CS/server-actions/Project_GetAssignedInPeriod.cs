(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Project_GetAssignedInPeriod' ***/
    var project_GetAssignedInPeriod = eSpace.CreateServerAction("Project_GetAssignedInPeriod");
    project_GetAssignedInPeriod.CacheInMinutes = null; // default value
    project_GetAssignedInPeriod.Description = "Active projects (with active client) the current user is assigned to on at least one day between StartDate and EndDate, each project once, sorted by client and project name.";
    var project = eSpace.Folders.Named("Project");
    project_GetAssignedInPeriod.Folder = project;
    project_GetAssignedInPeriod.Function = false; // default value
    project_GetAssignedInPeriod.Icon = null; // default value
    project_GetAssignedInPeriod.Public = true;
    project_GetAssignedInPeriod.Shareable = true;
    
    var startDate = project_GetAssignedInPeriod.CreateInputParameter("StartDate");
    startDate.DataType = eSpace.DateType;
    startDate.Description = null; // default value
    startDate.IsMandatory = true; // default value
    
    var endDate = project_GetAssignedInPeriod.CreateInputParameter("EndDate");
    endDate.DataType = eSpace.DateType;
    endDate.Description = null; // default value
    endDate.IsMandatory = true; // default value
    
    var line = project_GetAssignedInPeriod.CreateLocalVariable("Line");
    var daySheetProjectLine = eSpace.Structures.Named("DaySheetProjectLine");
    line.DataType = daySheetProjectLine;
    line.Description = null; // default value
    
    var startNode = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignedProjects = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignedProjects").Below(ifNode, 1371);
    getAssignedProjects.CacheInMinutes = null; // default value
    getAssignedProjects.Description = null; // default value
    getAssignedProjects.IsDisabled = false; // default value
    getAssignedProjects.SetMaxRecords(null); // default value
    getAssignedProjects.SetStartIndex(null); // default value
    getAssignedProjects.Timeout = null; // default value
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
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.StartDate <= EndDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.EndDate = NullDate() or ProjectAssignment.EndDate >= StartDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Project.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Client.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Client.Name");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Name");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Id");
    
    ifNode.TrueTarget = getAssignedProjects;
    
    var forEachNode = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getAssignedProjects, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = "Each assignment";
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetAssignedProjects.List");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode2 = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 1828);
    ifNode2.SetCondition("Projects.Length > 0 and Projects[Projects.Length - 1].ProjectId = GetAssignedProjects.List.Current.Project.Id");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Already listed?";
    ifNode2.TrueTarget = forEachNode;
    forEachNode.CycleTarget = ifNode2;
    
    var endNode = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 4113);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var assignNode = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Build line";
    
    var assignment = assignNode.CreateAssignment("Line.ProjectId", "GetAssignedProjects.List.Current.Project.Id");
    
    var assignment2 = assignNode.CreateAssignment("Line.ClientName", "GetAssignedProjects.List.Current.Client.Name");
    
    var assignment3 = assignNode.CreateAssignment("Line.ProjectName", "GetAssignedProjects.List.Current.Project.Name");
    
    var assignment4 = assignNode.CreateAssignment("Line.ProjectCode", "GetAssignedProjects.List.Current.Project.Code");
    
    var assignment5 = assignNode.CreateAssignment("Line.TimeEntryId", "NullIdentifier()");
    
    var assignment6 = assignNode.CreateAssignment("Line.Minutes", "0");
    
    var assignment7 = assignNode.CreateAssignment("Line.Description", "\"\"");
    ifNode2.FalseTarget = assignNode;
    
    var listAppend = project_GetAssignedInPeriod.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    listAppend.Target = forEachNode;
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Projects");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Line");
    
    var projects = project_GetAssignedInPeriod.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    projects.Description = null; // default value
}
