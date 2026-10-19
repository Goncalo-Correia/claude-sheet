# ProjectAssignment_End

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'ProjectAssignment_End' ***/
    var projectAssignment_End = eSpace.CreateServerAction("ProjectAssignment_End");
    projectAssignment_End.Description = "Ends an assignment on EndDate (SheetAdmin only): EndDate required and not before the assignment\'s StartDate. Raises AssignmentValidation otherwise. Use it instead of ProjectAssignment_Delete once the user has time entries.";
    var projectAssignment = eSpace.Folders.Named("ProjectAssignment");
    projectAssignment_End.Folder = projectAssignment;
    projectAssignment_End.Public = true;
    projectAssignment_End.Shareable = true;
    
    var projectAssignmentId = projectAssignment_End.CreateInputParameter("ProjectAssignmentId");
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    projectAssignmentId.DataType = projectAssignment2.IdentifierType;
    
    var endDate = projectAssignment_End.CreateInputParameter("EndDate");
    endDate.DataType = eSpace.DateType;
    
    var projectAssignmentRecord = projectAssignment_End.CreateLocalVariable("ProjectAssignmentRecord");
    projectAssignmentRecord.DataType = projectAssignment2;
    
    var startNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can manage project assignments.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignment = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignment").Below(ifNode, 1371);
    var projectAssignment3 = getAssignment.AsDatabaseAggregate.CreateSource(projectAssignment2);
    getAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id = ProjectAssignmentId");
    
    ifNode.TrueTarget = getAssignment;
    
    var ifNode2 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getAssignment, 1371);
    ifNode2.SetCondition("GetAssignment.List.Empty");
    ifNode2.Label = "Not found?";
    
    var ifNode3 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("EndDate = NullDate()");
    ifNode3.Label = "No end date?";
    ifNode2.FalseTarget = ifNode3;
    
    var raiseExceptionNode2 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var assignmentValidation = eSpace.UserExceptions.Named("AssignmentValidation");
    raiseExceptionNode2.Exception = assignmentValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Assignment not found.\"");
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode4 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode3, 1371);
    ifNode4.SetCondition("EndDate < GetAssignment.List.Current.ProjectAssignment.StartDate");
    ifNode4.Label = "End before start?";
    ifNode3.FalseTarget = ifNode4;
    
    var raiseExceptionNode3 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = assignmentValidation;
    raiseExceptionNode3.SetExceptionMessage("\"Enter the end date.\"");
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var assignNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode.Label = "Set end date";
    
    var assignment = assignNode.CreateAssignment("ProjectAssignmentRecord", "GetAssignment.List.Current.ProjectAssignment");
    
    var assignment2 = assignNode.CreateAssignment("ProjectAssignmentRecord.EndDate", "EndDate");
    ifNode4.FalseTarget = assignNode;
    
    var raiseExceptionNode4 = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = assignmentValidation;
    raiseExceptionNode4.SetExceptionMessage("\"The end date can\'t be before the start date.\"");
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var updateProjectAssignment = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateProjectAssignment").ConnectedBelow(assignNode, 1371);
    updateProjectAssignment.Action = projectAssignment2.UpdateAction;
    var updateProjectAssignment2 = projectAssignment2.UpdateAction;
    var source = updateProjectAssignment2.InputParameters.Named("Source");
    updateProjectAssignment.SetArgumentValue(source, "ProjectAssignmentRecord");
    
    var endNode = projectAssignment_End.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateProjectAssignment, 1371);
}
```
