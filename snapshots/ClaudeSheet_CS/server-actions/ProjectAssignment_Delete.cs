(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectAssignment_Delete' ***/
    var projectAssignment_Delete = eSpace.CreateServerAction("ProjectAssignment_Delete");
    projectAssignment_Delete.CacheInMinutes = null; // default value
    projectAssignment_Delete.Description = "Deletes an assignment (SheetAdmin only), only when its user has no TimeEntry on its project between StartDate and EndDate (open end = no limit). Otherwise raises AssignmentValidation asking to end it instead (ProjectAssignment_End).";
    var projectAssignment = eSpace.Folders.Named("ProjectAssignment");
    projectAssignment_Delete.Folder = projectAssignment;
    projectAssignment_Delete.Function = false; // default value
    projectAssignment_Delete.Icon = null; // default value
    projectAssignment_Delete.Public = true;
    projectAssignment_Delete.Shareable = true;
    
    var projectAssignmentId = projectAssignment_Delete.CreateInputParameter("ProjectAssignmentId");
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    projectAssignmentId.DataType = projectAssignment2.IdentifierType;
    projectAssignmentId.Description = null; // default value
    projectAssignmentId.IsMandatory = true; // default value
    
    var startNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage project assignments.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignment = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignment").Below(ifNode, 1371);
    getAssignment.CacheInMinutes = null; // default value
    getAssignment.Description = null; // default value
    getAssignment.IsDisabled = false; // default value
    getAssignment.SetMaxRecords(null); // default value
    getAssignment.SetStartIndex(null); // default value
    getAssignment.Timeout = null; // default value
    var projectAssignment3 = getAssignment.AsDatabaseAggregate.CreateSource(projectAssignment2);
    getAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id = ProjectAssignmentId");
    
    ifNode.TrueTarget = getAssignment;
    
    var ifNode2 = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getAssignment, 1371);
    ifNode2.SetCondition("GetAssignment.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Not found?";
    
    var getEntriesInPeriod = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetEntriesInPeriod").Below(ifNode2, 1371);
    getEntriesInPeriod.CacheInMinutes = null; // default value
    getEntriesInPeriod.Description = null; // default value
    getEntriesInPeriod.IsDisabled = false; // default value
    getEntriesInPeriod.SetMaxRecords(null); // default value
    getEntriesInPeriod.SetStartIndex(null); // default value
    getEntriesInPeriod.Timeout = null; // default value
    var timeEntry2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var timeEntry = getEntriesInPeriod.AsDatabaseAggregate.CreateSource(timeEntry2);
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getEntriesInPeriod.AsDatabaseAggregate.CreateSource(daySheet2);
    var join = getEntriesInPeriod.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    getEntriesInPeriod.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetAssignment.List.Current.ProjectAssignment.UserId");
    getEntriesInPeriod.AsDatabaseAggregate.CreateFilter("TimeEntry.ProjectId = GetAssignment.List.Current.ProjectAssignment.ProjectId");
    getEntriesInPeriod.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= GetAssignment.List.Current.ProjectAssignment.StartDate");
    getEntriesInPeriod.AsDatabaseAggregate.CreateFilter("GetAssignment.List.Current.ProjectAssignment.EndDate = NullDate() or DaySheet.SheetDate <= GetAssignment.List.Current.ProjectAssignment.EndDate");
    
    ifNode2.FalseTarget = getEntriesInPeriod;
    
    var raiseExceptionNode2 = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var assignmentValidation = eSpace.UserExceptions.Named("AssignmentValidation");
    raiseExceptionNode2.Exception = assignmentValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Assignment not found.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode3 = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getEntriesInPeriod, 1371);
    ifNode3.SetCondition("not GetEntriesInPeriod.List.Empty");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Has entries?";
    
    var deleteProjectAssignment = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteProjectAssignment").Below(ifNode3, 1371);
    deleteProjectAssignment.Action = projectAssignment2.DeleteAction;
    deleteProjectAssignment.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteProjectAssignment.IsDisabled = false; // default value
    deleteProjectAssignment.ServerRequestTimeout = null; // default value
    var deleteProjectAssignment2 = projectAssignment2.DeleteAction;
    var id = deleteProjectAssignment2.InputParameters.Named("Id");
    deleteProjectAssignment.SetArgumentValue(id, "ProjectAssignmentId");
    ifNode3.FalseTarget = deleteProjectAssignment;
    
    var raiseExceptionNode3 = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = assignmentValidation;
    raiseExceptionNode3.SetExceptionMessage("\"This assignment has time entries and can\'t be deleted. Use End to close it instead.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var endNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(deleteProjectAssignment, 1371);
    endNode.IsDisabled = false; // default value
}
