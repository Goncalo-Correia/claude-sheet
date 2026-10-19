# ProjectAssignment_Delete

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectAssignment_Delete' ***/
    var projectAssignment_Delete = eSpace.CreateServerAction("ProjectAssignment_Delete");
    projectAssignment_Delete.Description = "Deletes an assignment (SheetAdmin only), only when its user has no TimeEntry on its project between StartDate and EndDate (open end = no limit). Otherwise raises AssignmentValidation asking to end it instead (ProjectAssignment_End).";
    var projectAssignment = eSpace.Folders.Named("ProjectAssignment");
    projectAssignment_Delete.Folder = projectAssignment;
    projectAssignment_Delete.Public = true;
    projectAssignment_Delete.Shareable = true;
    
    var projectAssignmentId = projectAssignment_Delete.CreateInputParameter("ProjectAssignmentId");
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    projectAssignmentId.DataType = projectAssignment2.IdentifierType;
    
    var startNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage project assignments.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignment = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignment").Below(ifNode, 1371);
    var projectAssignment3 = getAssignment.AsDatabaseAggregate.CreateSource(projectAssignment2);
    getAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id = ProjectAssignmentId");
    
    ifNode.TrueTarget = getAssignment;
    
    var ifNode2 = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getAssignment, 1371);
    ifNode2.SetCondition("GetAssignment.List.Empty");
    ifNode2.Label = "Not found?";
    
    var getEntriesInPeriod = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetEntriesInPeriod").Below(ifNode2, 1371);
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
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode3 = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getEntriesInPeriod, 1371);
    ifNode3.SetCondition("not GetEntriesInPeriod.List.Empty");
    ifNode3.Label = "Has entries?";
    
    var deleteProjectAssignment = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteProjectAssignment").Below(ifNode3, 1371);
    deleteProjectAssignment.Action = projectAssignment2.DeleteAction;
    var deleteProjectAssignment2 = projectAssignment2.DeleteAction;
    var id = deleteProjectAssignment2.InputParameters.Named("Id");
    deleteProjectAssignment.SetArgumentValue(id, "ProjectAssignmentId");
    ifNode3.FalseTarget = deleteProjectAssignment;
    
    var raiseExceptionNode3 = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = assignmentValidation;
    raiseExceptionNode3.SetExceptionMessage("\"This assignment has time entries and can\'t be deleted. Use End to close it instead.\"");
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var endNode = projectAssignment_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(deleteProjectAssignment, 1371);
}
```
