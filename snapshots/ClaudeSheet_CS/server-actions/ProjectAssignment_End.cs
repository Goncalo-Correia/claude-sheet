(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectAssignment_End' ***/
    var projectAssignment_End = eSpace.CreateServerAction("ProjectAssignment_End");
    projectAssignment_End.CacheInMinutes = null; // default value
    projectAssignment_End.Description = "Ends an assignment on EndDate (SheetAdmin only): EndDate required and not before the assignment\'s StartDate. Raises AssignmentValidation otherwise. Use it instead of ProjectAssignment_Delete once the user has time entries.";
    var projectAssignment = eSpace.Folders.Named("ProjectAssignment");
    projectAssignment_End.Folder = projectAssignment;
    projectAssignment_End.Function = false; // default value
    projectAssignment_End.Icon = null; // default value
    projectAssignment_End.Public = true;
    projectAssignment_End.Shareable = true;
    
    var projectAssignmentId = projectAssignment_End.CreateInputParameter("ProjectAssignmentId");
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    projectAssignmentId.DataType = projectAssignment2.IdentifierType;
    projectAssignmentId.Description = null; // default value
    projectAssignmentId.IsMandatory = true; // default value
    
    var endDate = projectAssignment_End.CreateInputParameter("EndDate");
    endDate.DataType = eSpace.DateType;
    endDate.Description = null; // default value
    endDate.IsMandatory = true; // default value
    
    var projectAssignmentRecord = projectAssignment_End.CreateLocalVariable("ProjectAssignmentRecord");
    projectAssignmentRecord.DataType = projectAssignment2;
    projectAssignmentRecord.Description = null; // default value
    
    var startNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage project assignments.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignment = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignment").Below(ifNode, 1371);
    getAssignment.CacheInMinutes = null; // default value
    getAssignment.Description = null; // default value
    getAssignment.IsDisabled = false; // default value
    getAssignment.SetMaxRecords(null); // default value
    getAssignment.SetStartIndex(null); // default value
    getAssignment.Timeout = null; // default value
    var projectAssignment3 = getAssignment.AsDatabaseAggregate.CreateSource(projectAssignment2);
    getAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id = ProjectAssignmentId");
    
    ifNode.TrueTarget = getAssignment;
    
    var ifNode2 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getAssignment, 1371);
    ifNode2.SetCondition("GetAssignment.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Not found?";
    
    var ifNode3 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("EndDate = NullDate()");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "No end date?";
    ifNode2.FalseTarget = ifNode3;
    
    var raiseExceptionNode2 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var assignmentValidation = eSpace.UserExceptions.Named("AssignmentValidation");
    raiseExceptionNode2.Exception = assignmentValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Assignment not found.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode4 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode3, 1371);
    ifNode4.SetCondition("EndDate < GetAssignment.List.Current.ProjectAssignment.StartDate");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "End before start?";
    ifNode3.FalseTarget = ifNode4;
    
    var raiseExceptionNode3 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = assignmentValidation;
    raiseExceptionNode3.SetExceptionMessage("\"Enter the end date.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var assignNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set end date";
    
    var assignment = assignNode.CreateAssignment("ProjectAssignmentRecord", "GetAssignment.List.Current.ProjectAssignment");
    
    var assignment2 = assignNode.CreateAssignment("ProjectAssignmentRecord.EndDate", "EndDate");
    ifNode4.FalseTarget = assignNode;
    
    var raiseExceptionNode4 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = assignmentValidation;
    raiseExceptionNode4.SetExceptionMessage("\"The end date can\'t be before the start date.\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var updateProjectAssignment = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateProjectAssignment").ConnectedBelow(assignNode, 1371);
    updateProjectAssignment.Action = projectAssignment2.UpdateAction;
    updateProjectAssignment.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateProjectAssignment.IsDisabled = false; // default value
    updateProjectAssignment.ServerRequestTimeout = null; // default value
    var updateProjectAssignment2 = projectAssignment2.UpdateAction;
    var source = updateProjectAssignment2.InputParameters.Named("Source");
    updateProjectAssignment.SetArgumentValue(source, "ProjectAssignmentRecord");
    
    var endNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateProjectAssignment, 1371);
    endNode.IsDisabled = false; // default value
}
