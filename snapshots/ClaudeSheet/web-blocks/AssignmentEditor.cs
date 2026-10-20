(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileBlock 'AssignmentEditor' ***/
    var assignmentEditor = admin.CreateBlock("AssignmentEditor");
    assignmentEditor.Description = "Assignments of a project (ProjectId) or of a user (UserId): table with Add, Edit, End and Delete, and the assignment popup. Pass exactly one of the two inputs. Used by ProjectDetail and UserDetail.";
    assignmentEditor.HorizontalPosition = 2800;
    assignmentEditor.Icon = null; // default value
    assignmentEditor.IsDisabled = false; // default value
    assignmentEditor.JavaScript = ""; // default value
    assignmentEditor.SetOnRenderJavascript(null); // default value
    assignmentEditor.Public = false; // default value
    assignmentEditor.Shareable = false; // default value
    assignmentEditor.StyleSheet = null; // default value
    assignmentEditor.VerticalPosition = 1400;
    
    var getAssignments = assignmentEditor.CreateDataAction("GetAssignments");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getAssignments.OutputParameters.ToList().ForEach(x => x.Delete());
    getAssignments.Description = null; // default value
    getAssignments.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getAssignments.ServerRequestTimeout = null; // default value
    
    var startNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("ProjectId = NullIdentifier() and UserId = NullIdentifier()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Nothing to show?";
    
    var projectAssignment_GetList = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_GetList").Below(ifNode, 1371);
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var projectAssignment_GetList2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_GetList");
    projectAssignment_GetList.Action = projectAssignment_GetList2;
    projectAssignment_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_GetList.IsDisabled = false; // default value
    projectAssignment_GetList.ServerRequestTimeout = null; // default value
    var userId = projectAssignment_GetList2.InputParameters.Named("UserId");
    projectAssignment_GetList.SetArgumentValue(userId, "UserId");
    var projectId = projectAssignment_GetList2.InputParameters.Named("ProjectId");
    projectAssignment_GetList.SetArgumentValue(projectId, "ProjectId");
    ifNode.FalseTarget = projectAssignment_GetList;
    
    var endNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode.IsDisabled = false; // default value
    ifNode.TrueTarget = endNode;
    
    var assignNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_GetList, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set assignments";
    
    var assignment = assignNode.CreateAssignment("Assignments", "ProjectAssignment_GetList.Assignments");
    
    var endNode2 = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode2.IsDisabled = false; // default value
    
    var allExceptions = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 8684;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode3 = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode3.IsDisabled = false; // default value
    
    var assignments = getAssignments.CreateOutputParameter("Assignments");
    var projectAssignmentSummary = claudeSheet_CS.Structures.Named("ProjectAssignmentSummary");
    assignments.DataType = eSpace.GetOrCreateListType(projectAssignmentSummary);
    assignments.Description = null; // default value
    
    var onAfterFetch_GetAssignments = assignmentEditor.CreateScreenAction("OnAfterFetch_GetAssignments");
    onAfterFetch_GetAssignments.Description = "Shows an error message when GetAssignments failed.";
    
    var startNode2 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode2 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode2.SetCondition("GetAssignments.ErrorMessage <> \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Failed?";
    
    var endNode4 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    endNode4.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode2, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetAssignments.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode5 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode5.IsDisabled = false; // default value
    getAssignments.OnAfterFetch.Destination = onAfterFetch_GetAssignments;
    ifNode2.FalseTarget = endNode4;
    ifNode2.TrueTarget = messageNode;
    
    var errorMessage = getAssignments.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var getProjects = assignmentEditor.CreateDataAction("GetProjects");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getProjects.OutputParameters.ToList().ForEach(x => x.Delete());
    getProjects.Description = null; // default value
    getProjects.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProjects.ServerRequestTimeout = null; // default value
    
    var startNode3 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var ifNode3 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode3, 1371);
    ifNode3.SetCondition("UserId = NullIdentifier()");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Project mode?";
    
    var project_GetList = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetList").Below(ifNode3, 1371);
    var project_GetList2 = claudeSheet_CS.ServerActions.Named("Project_GetList");
    project_GetList.Action = project_GetList2;
    project_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetList.IsDisabled = false; // default value
    project_GetList.ServerRequestTimeout = null; // default value
    var clientId = project_GetList2.InputParameters.Named("ClientId");
    project_GetList.SetArgumentValue(clientId, "NullIdentifier()");
    var includeInactive = project_GetList2.InputParameters.Named("IncludeInactive");
    project_GetList.SetArgumentValue(includeInactive, "False");
    ifNode3.FalseTarget = project_GetList;
    
    var endNode6 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode3, 1828);
    endNode6.IsDisabled = false; // default value
    ifNode3.TrueTarget = endNode6;
    
    var assignNode3 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetList, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Set projects";
    
    var assignment3 = assignNode3.CreateAssignment("Projects", "Project_GetList.Projects");
    
    var endNode7 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode7.IsDisabled = false; // default value
    
    var allExceptions2 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 8684;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = true; // default value
    allExceptions2.VerticalPosition = 800;
    
    var assignNode4 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Keep error";
    
    var assignment4 = assignNode4.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode8 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    endNode8.IsDisabled = false; // default value
    getProjects.OnAfterFetch.Destination = null; // default value
    
    var projects = getProjects.CreateOutputParameter("Projects");
    var projectSummary = claudeSheet_CS.Structures.Named("ProjectSummary");
    projects.DataType = eSpace.GetOrCreateListType(projectSummary);
    projects.Description = null; // default value
    
    var errorMessage2 = getProjects.CreateOutputParameter("ErrorMessage");
    errorMessage2.DataType = eSpace.TextType;
    errorMessage2.SetDefaultValue(null); // default value
    errorMessage2.Description = null; // default value
    
    var getUsers = assignmentEditor.CreateDataAction("GetUsers");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getUsers.OutputParameters.ToList().ForEach(x => x.Delete());
    getUsers.Description = null; // default value
    getUsers.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getUsers.ServerRequestTimeout = null; // default value
    
    var startNode4 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var ifNode4 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode4, 1371);
    ifNode4.SetCondition("ProjectId = NullIdentifier()");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "User mode?";
    
    var user_GetActive = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetActive").Below(ifNode4, 1371);
    var user_GetActive2 = claudeSheet_CS.ServerActions.Named("User_GetActive");
    user_GetActive.Action = user_GetActive2;
    user_GetActive.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_GetActive.IsDisabled = false; // default value
    user_GetActive.ServerRequestTimeout = null; // default value
    ifNode4.FalseTarget = user_GetActive;
    
    var endNode9 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode4, 1828);
    endNode9.IsDisabled = false; // default value
    ifNode4.TrueTarget = endNode9;
    
    var assignNode5 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(user_GetActive, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Set users";
    
    var assignment5 = assignNode5.CreateAssignment("Users", "User_GetActive.Users");
    
    var endNode10 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    endNode10.IsDisabled = false; // default value
    
    var allExceptions3 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 8684;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = true; // default value
    allExceptions3.VerticalPosition = 800;
    
    var assignNode6 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Keep error";
    
    var assignment6 = assignNode6.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode11 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode6, 1371);
    endNode11.IsDisabled = false; // default value
    getUsers.OnAfterFetch.Destination = null; // default value
    
    var users = getUsers.CreateOutputParameter("Users");
    var userOption = claudeSheet_CS.Structures.Named("UserOption");
    users.DataType = eSpace.GetOrCreateListType(userOption);
    users.Description = null; // default value
    
    var errorMessage3 = getUsers.CreateOutputParameter("ErrorMessage");
    errorMessage3.DataType = eSpace.TextType;
    errorMessage3.SetDefaultValue(null); // default value
    errorMessage3.Description = null; // default value
    
    var projectId2 = assignmentEditor.CreateInputParameter("ProjectId");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    projectId2.DataType = project.IdentifierType;
    projectId2.SetDefaultValue(null); // default value
    projectId2.Description = null; // default value
    projectId2.IsMandatory = false;
    
    var userId2 = assignmentEditor.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId2.DataType = user.IdentifierType;
    userId2.SetDefaultValue(null); // default value
    userId2.Description = null; // default value
    userId2.IsMandatory = false;
    
    var editAssignment = assignmentEditor.CreateLocalVariable("EditAssignment");
    var projectAssignment = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProjectAssignment");
    editAssignment.DataType = projectAssignment;
    editAssignment.Description = "Assignment being added, edited or ended in the popup.";
    
    var showAssignmentPopup = assignmentEditor.CreateLocalVariable("ShowAssignmentPopup");
    showAssignmentPopup.DataType = eSpace.BooleanType;
    showAssignmentPopup.SetDefaultValue("False");
    showAssignmentPopup.Description = null; // default value
    
    var isEndMode = assignmentEditor.CreateLocalVariable("IsEndMode");
    isEndMode.DataType = eSpace.BooleanType;
    isEndMode.SetDefaultValue("False");
    isEndMode.Description = "True when the popup only asks for the end date (End, or a refused Delete).";
    
    var popupError = assignmentEditor.CreateLocalVariable("PopupError");
    popupError.DataType = eSpace.TextType;
    popupError.SetDefaultValue(null); // default value
    popupError.Description = "Message shown in the popup (e.g. AssignmentValidation from a save, or why a Delete was refused).";
    
    var popupName = assignmentEditor.CreateLocalVariable("PopupName");
    popupName.DataType = eSpace.TextType;
    popupName.SetDefaultValue(null); // default value
    popupName.Description = "The assignment\'s user (project mode) or project (user mode), shown in End mode.";
    assignmentEditor.OnDestroy.Destination = null; // default value
    assignmentEditor.OnInitialize.Destination = null; // default value
    assignmentEditor.OnReady.Destination = null; // default value
    assignmentEditor.OnRender.Destination = null; // default value
    
    var onClick_AddAssignmentButton = assignmentEditor.CreateScreenAction("OnClick_AddAssignmentButton");
    onClick_AddAssignmentButton.Description = "Opens the popup for a new assignment of this project or user (start today, no end).";
    
    var startNode5 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var onParametersChanged = assignmentEditor.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads the assignments and the dropdown lists when ProjectId or UserId changes, and closes the popup.";
    
    var startNode6 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 800;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode6, 1371);
    refreshDataNode.DataSource = getAssignments;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var refreshDataNode2 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(refreshDataNode, 1371);
    refreshDataNode2.DataSource = getUsers;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var refreshDataNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(refreshDataNode2, 1371);
    refreshDataNode3.DataSource = getProjects;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    
    var assignNode7 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(refreshDataNode3, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "Close popup";
    
    var assignment7 = assignNode7.CreateAssignment("ShowAssignmentPopup", "False");
    
    var endNode12 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode7, 1371);
    endNode12.IsDisabled = false; // default value
    assignmentEditor.OnParametersChanged.Destination = onParametersChanged;
    
    var assignNode8 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "New assignment";
    
    var assignment8 = assignNode8.CreateAssignment("EditAssignment.Id", "NullIdentifier()");
    
    var assignment9 = assignNode8.CreateAssignment("EditAssignment.ProjectId", "ProjectId");
    
    var assignment10 = assignNode8.CreateAssignment("EditAssignment.UserId", "UserId");
    
    var assignment11 = assignNode8.CreateAssignment("EditAssignment.StartDate", "CurrDate()");
    
    var assignment12 = assignNode8.CreateAssignment("EditAssignment.EndDate", "NullDate()");
    
    var assignment13 = assignNode8.CreateAssignment("EditAssignment.IsActive", "True");
    
    var assignment14 = assignNode8.CreateAssignment("PopupName", "\"\"");
    
    var assignment15 = assignNode8.CreateAssignment("PopupError", "\"\"");
    
    var assignment16 = assignNode8.CreateAssignment("IsEndMode", "False");
    
    var assignment17 = assignNode8.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode13 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode8, 1371);
    endNode13.IsDisabled = false; // default value
    
    var onClick_CancelPopupButton = assignmentEditor.CreateScreenAction("OnClick_CancelPopupButton");
    onClick_CancelPopupButton.Description = "Closes the assignment popup without saving.";
    
    var startNode7 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.IsDisabled = false; // default value
    startNode7.VerticalPosition = 800;
    
    var assignNode9 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = "Close popup";
    
    var assignment18 = assignNode9.CreateAssignment("ShowAssignmentPopup", "False");
    
    var endNode14 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode9, 1371);
    endNode14.IsDisabled = false; // default value
    
    var onClick_DeleteAssignmentLink = assignmentEditor.CreateScreenAction("OnClick_DeleteAssignmentLink");
    onClick_DeleteAssignmentLink.Description = "Deletes an assignment through ProjectAssignment_Delete. When it is refused (the user has time entries), the popup opens in End mode with the reason, offering End instead.";
    
    var row = onClick_DeleteAssignmentLink.CreateInputParameter("Row");
    row.DataType = projectAssignmentSummary;
    row.Description = null; // default value
    row.IsMandatory = true; // default value
    
    var startNode8 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.HorizontalPosition = 3200;
    startNode8.IsDisabled = false; // default value
    startNode8.VerticalPosition = 800;
    
    var projectAssignment_Delete = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_Delete").ConnectedBelow(startNode8, 1371);
    var projectAssignment_Delete2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_Delete");
    projectAssignment_Delete.Action = projectAssignment_Delete2;
    projectAssignment_Delete.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_Delete.IsDisabled = false; // default value
    projectAssignment_Delete.ServerRequestTimeout = null; // default value
    var projectAssignmentId = projectAssignment_Delete2.InputParameters.Named("ProjectAssignmentId");
    projectAssignment_Delete.SetArgumentValue(projectAssignmentId, "Row.Id");
    
    var refreshDataNode4 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(projectAssignment_Delete, 1371);
    refreshDataNode4.DataSource = getAssignments;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords(null); // default value
    refreshDataNode4.SetStartIndex(null); // default value
    
    var messageNode2 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Show deleted";
    messageNode2.SetMessage("\"Assignment deleted.\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode15 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1371);
    endNode15.IsDisabled = false; // default value
    
    var allExceptions4 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.IsDisabled = false; // default value
    allExceptions4.LogError = false;
    allExceptions4.VerticalPosition = 800;
    
    var assignNode10 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions4, 1371);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = "Offer end instead";
    
    var assignment19 = assignNode10.CreateAssignment("EditAssignment.Id", "Row.Id");
    
    var assignment20 = assignNode10.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId");
    
    var assignment21 = assignNode10.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    
    var assignment22 = assignNode10.CreateAssignment("EditAssignment.StartDate", "Row.StartDate");
    
    var assignment23 = assignNode10.CreateAssignment("EditAssignment.EndDate", "If(Row.EndDate = NullDate(), CurrDate(), Row.EndDate)");
    
    var assignment24 = assignNode10.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    
    var assignment25 = assignNode10.CreateAssignment("PopupName", "If(ProjectId <> NullIdentifier(), Row.UserName, Row.ProjectName + \" (\" + Row.ClientName + \")\")");
    
    var assignment26 = assignNode10.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var assignment27 = assignNode10.CreateAssignment("IsEndMode", "True");
    
    var assignment28 = assignNode10.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode16 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode10, 1371);
    endNode16.IsDisabled = false; // default value
    
    var onClick_EditAssignmentLink = assignmentEditor.CreateScreenAction("OnClick_EditAssignmentLink");
    onClick_EditAssignmentLink.Description = "Opens the popup to edit an assignment (user or project, and dates).";
    
    var row2 = onClick_EditAssignmentLink.CreateInputParameter("Row");
    row2.DataType = projectAssignmentSummary;
    row2.Description = null; // default value
    row2.IsMandatory = true; // default value
    
    var startNode9 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.HorizontalPosition = 3200;
    startNode9.IsDisabled = false; // default value
    startNode9.VerticalPosition = 800;
    
    var assignNode11 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode9, 1371);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = "Edit assignment";
    
    var assignment29 = assignNode11.CreateAssignment("EditAssignment.Id", "Row.Id");
    
    var assignment30 = assignNode11.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId");
    
    var assignment31 = assignNode11.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    
    var assignment32 = assignNode11.CreateAssignment("EditAssignment.StartDate", "Row.StartDate");
    
    var assignment33 = assignNode11.CreateAssignment("EditAssignment.EndDate", "Row.EndDate");
    
    var assignment34 = assignNode11.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    
    var assignment35 = assignNode11.CreateAssignment("PopupName", "If(ProjectId <> NullIdentifier(), Row.UserName, Row.ProjectName + \" (\" + Row.ClientName + \")\")");
    
    var assignment36 = assignNode11.CreateAssignment("PopupError", "\"\"");
    
    var assignment37 = assignNode11.CreateAssignment("IsEndMode", "False");
    
    var assignment38 = assignNode11.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode17 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode11, 1371);
    endNode17.IsDisabled = false; // default value
    
    var onClick_EndAssignmentLink = assignmentEditor.CreateScreenAction("OnClick_EndAssignmentLink");
    onClick_EndAssignmentLink.Description = "Opens the popup in End mode: only the end date (today when the assignment has no end).";
    
    var row3 = onClick_EndAssignmentLink.CreateInputParameter("Row");
    row3.DataType = projectAssignmentSummary;
    row3.Description = null; // default value
    row3.IsMandatory = true; // default value
    
    var startNode10 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.IsDisabled = false; // default value
    startNode10.VerticalPosition = 800;
    
    var assignNode12 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode10, 1371);
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = "Open end popup";
    
    var assignment39 = assignNode12.CreateAssignment("EditAssignment.Id", "Row.Id");
    
    var assignment40 = assignNode12.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId");
    
    var assignment41 = assignNode12.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    
    var assignment42 = assignNode12.CreateAssignment("EditAssignment.StartDate", "Row.StartDate");
    
    var assignment43 = assignNode12.CreateAssignment("EditAssignment.EndDate", "If(Row.EndDate = NullDate(), CurrDate(), Row.EndDate)");
    
    var assignment44 = assignNode12.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    
    var assignment45 = assignNode12.CreateAssignment("PopupName", "If(ProjectId <> NullIdentifier(), Row.UserName, Row.ProjectName + \" (\" + Row.ClientName + \")\")");
    
    var assignment46 = assignNode12.CreateAssignment("PopupError", "\"\"");
    
    var assignment47 = assignNode12.CreateAssignment("IsEndMode", "True");
    
    var assignment48 = assignNode12.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode18 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode12, 1371);
    endNode18.IsDisabled = false; // default value
    
    var onClick_SaveAssignmentButton = assignmentEditor.CreateScreenAction("OnClick_SaveAssignmentButton");
    onClick_SaveAssignmentButton.Description = "Saves EditAssignment through ProjectAssignment_CreateOrUpdate, closes the popup and reloads GetAssignments. AssignmentValidation (or any other) error is shown in the popup.";
    
    var startNode11 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.IsDisabled = false; // default value
    startNode11.VerticalPosition = 800;
    
    var assignNode13 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode11, 1371);
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = "Clear error";
    
    var assignment49 = assignNode13.CreateAssignment("PopupError", "\"\"");
    
    var projectAssignment_CreateOrUpdate = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_CreateOrUpdate").ConnectedBelow(assignNode13, 1371);
    var projectAssignment_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_CreateOrUpdate");
    projectAssignment_CreateOrUpdate.Action = projectAssignment_CreateOrUpdate2;
    projectAssignment_CreateOrUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_CreateOrUpdate.IsDisabled = false; // default value
    projectAssignment_CreateOrUpdate.ServerRequestTimeout = null; // default value
    var projectAssignmentRecord = projectAssignment_CreateOrUpdate2.InputParameters.Named("ProjectAssignmentRecord");
    projectAssignment_CreateOrUpdate.SetArgumentValue(projectAssignmentRecord, "EditAssignment");
    
    var assignNode14 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_CreateOrUpdate, 1371);
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = "Close popup";
    
    var assignment50 = assignNode14.CreateAssignment("ShowAssignmentPopup", "False");
    
    var refreshDataNode5 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode14, 1371);
    refreshDataNode5.DataSource = getAssignments;
    refreshDataNode5.IsDisabled = false; // default value
    refreshDataNode5.SetMaxRecords(null); // default value
    refreshDataNode5.SetStartIndex(null); // default value
    
    var messageNode3 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode5, 1371);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = "Show saved";
    messageNode3.SetMessage("\"Assignment saved.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode19 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    endNode19.IsDisabled = false; // default value
    
    var allExceptions5 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions5.AbortTransaction = false;
    allExceptions5.Exception = eSpace.AllExceptions;
    allExceptions5.HorizontalPosition = 6856;
    allExceptions5.IsDisabled = false; // default value
    allExceptions5.LogError = false;
    allExceptions5.VerticalPosition = 800;
    
    var assignNode15 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions5, 1371);
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = "Keep error";
    
    var assignment51 = assignNode15.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode20 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode15, 1371);
    endNode20.IsDisabled = false; // default value
    
    var onClick_SaveEndButton = assignmentEditor.CreateScreenAction("OnClick_SaveEndButton");
    onClick_SaveEndButton.Description = "Ends the assignment on EditAssignment.EndDate through ProjectAssignment_End, closes the popup and reloads GetAssignments. Errors are shown in the popup.";
    
    var startNode12 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.IsDisabled = false; // default value
    startNode12.VerticalPosition = 800;
    
    var assignNode16 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode12, 1371);
    assignNode16.IsDisabled = false; // default value
    assignNode16.Label = "Clear error";
    
    var assignment52 = assignNode16.CreateAssignment("PopupError", "\"\"");
    
    var projectAssignment_End = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_End").ConnectedBelow(assignNode16, 1371);
    var projectAssignment_End2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_End");
    projectAssignment_End.Action = projectAssignment_End2;
    projectAssignment_End.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_End.IsDisabled = false; // default value
    projectAssignment_End.ServerRequestTimeout = null; // default value
    var endDate = projectAssignment_End2.InputParameters.Named("EndDate");
    projectAssignment_End.SetArgumentValue(endDate, "EditAssignment.EndDate");
    var projectAssignmentId2 = projectAssignment_End2.InputParameters.Named("ProjectAssignmentId");
    projectAssignment_End.SetArgumentValue(projectAssignmentId2, "EditAssignment.Id");
    
    var assignNode17 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_End, 1371);
    assignNode17.IsDisabled = false; // default value
    assignNode17.Label = "Close popup";
    
    var assignment53 = assignNode17.CreateAssignment("ShowAssignmentPopup", "False");
    
    var refreshDataNode6 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode17, 1371);
    refreshDataNode6.DataSource = getAssignments;
    refreshDataNode6.IsDisabled = false; // default value
    refreshDataNode6.SetMaxRecords(null); // default value
    refreshDataNode6.SetStartIndex(null); // default value
    
    var messageNode4 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode6, 1371);
    messageNode4.IsDisabled = false; // default value
    messageNode4.Label = "Show ended";
    messageNode4.SetMessage("\"Assignment ended.\"");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode21 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1371);
    endNode21.IsDisabled = false; // default value
    
    var allExceptions6 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions6.AbortTransaction = false;
    allExceptions6.Exception = eSpace.AllExceptions;
    allExceptions6.HorizontalPosition = 6856;
    allExceptions6.IsDisabled = false; // default value
    allExceptions6.LogError = false;
    allExceptions6.VerticalPosition = 800;
    
    var assignNode18 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions6, 1371);
    assignNode18.IsDisabled = false; // default value
    assignNode18.Label = "Keep error";
    
    var assignment54 = assignNode18.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode22 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode18, 1371);
    endNode22.IsDisabled = false; // default value
    
    var assignmentsSection = assignmentEditor.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsSection");
    assignmentsSection.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    assignmentsSection.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentsSection.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentsSection.Animate = false; // default value
    assignmentsSection.ColumnGap = null; // default value
    assignmentsSection.ColumnSpan = null; // default value
    assignmentsSection.CustomStyle = null; // default value
    assignmentsSection.FlexBasis = null; // default value
    assignmentsSection.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentsSection.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentsSection.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentsSection.Gap = null;
    assignmentsSection.GridColumns = null; // default value
    assignmentsSection.GridRows = null; // default value
    assignmentsSection.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentsSection.RowGap = null; // default value
    assignmentsSection.RowSpan = null; // default value
    assignmentsSection.SetStyle("\"admin-page\"");
    assignmentsSection.SetVisible("True"); // default value
    assignmentsSection.VisibleWasExpression = false; // default value
    assignmentsSection.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentsSection.Width = "(fill parent)"; // default value
    
    var assignmentsHead = assignmentsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsHead");
    assignmentsHead.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    assignmentsHead.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentsHead.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentsHead.Animate = false; // default value
    assignmentsHead.ColumnGap = null; // default value
    assignmentsHead.ColumnSpan = null; // default value
    assignmentsHead.CustomStyle = null; // default value
    assignmentsHead.FlexBasis = null; // default value
    assignmentsHead.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentsHead.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentsHead.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentsHead.Gap = null;
    assignmentsHead.GridColumns = null; // default value
    assignmentsHead.GridRows = null; // default value
    assignmentsHead.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentsHead.RowGap = null; // default value
    assignmentsHead.RowSpan = null; // default value
    assignmentsHead.SetStyle("\"admin-section__head\"");
    assignmentsHead.SetVisible("True"); // default value
    assignmentsHead.VisibleWasExpression = false; // default value
    assignmentsHead.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentsHead.Width = "(fill parent)"; // default value
    
    var advancedHtml = assignmentsHead.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    advancedHtml.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    advancedHtml.ColumnGap = null; // default value
    advancedHtml.ColumnSpan = null; // default value
    advancedHtml.CustomStyle = null; // default value
    advancedHtml.FlexBasis = null; // default value
    advancedHtml.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    advancedHtml.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    advancedHtml.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    advancedHtml.Gap = null;
    advancedHtml.GridColumns = null; // default value
    advancedHtml.GridRows = null; // default value
    advancedHtml.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    advancedHtml.RowGap = null; // default value
    advancedHtml.RowSpan = null; // default value
    advancedHtml.Tag = "h2";
    advancedHtml.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var expression = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("If(ProjectId <> NullIdentifier(), \"Assigned users\", \"Assigned projects\")");
    expression.Width = ""; // default value
    
    var addAssignmentButton = assignmentsHead.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("AddAssignmentButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    addAssignmentButton.Widgets.ToList().ForEach(x => x.Delete());
    addAssignmentButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    addAssignmentButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    addAssignmentButton.ColumnGap = null; // default value
    addAssignmentButton.ColumnSpan = null; // default value
    addAssignmentButton.SetConfirmationMessage(null); // default value
    addAssignmentButton.CustomStyle = null; // default value
    addAssignmentButton.SetEnabled("True");
    addAssignmentButton.FlexBasis = null; // default value
    addAssignmentButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    addAssignmentButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    addAssignmentButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    addAssignmentButton.Gap = null;
    addAssignmentButton.GridColumns = null; // default value
    addAssignmentButton.GridRows = null; // default value
    addAssignmentButton.IsDefault = false; // default value
    addAssignmentButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    addAssignmentButton.RowGap = null; // default value
    addAssignmentButton.RowSpan = null; // default value
    addAssignmentButton.SetStyle("\"btn btn-primary\"");
    addAssignmentButton.SetVisible("True"); // default value
    addAssignmentButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    addAssignmentButton.Width = ""; // default value
    
    var expression2 = addAssignmentButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("If(ProjectId <> NullIdentifier(), \"Add user\", \"Add project\")");
    expression2.Width = ""; // default value
    var builtinEvent = addAssignmentButton.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onClick_AddAssignmentButton;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var assignmentsTable = assignmentsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsTable");
    assignmentsTable.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    assignmentsTable.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentsTable.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentsTable.Animate = false; // default value
    assignmentsTable.ColumnGap = null; // default value
    assignmentsTable.ColumnSpan = null; // default value
    assignmentsTable.CustomStyle = null; // default value
    assignmentsTable.FlexBasis = null; // default value
    assignmentsTable.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentsTable.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentsTable.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentsTable.Gap = null;
    assignmentsTable.GridColumns = null; // default value
    assignmentsTable.GridRows = null; // default value
    assignmentsTable.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentsTable.RowGap = null; // default value
    assignmentsTable.RowSpan = null; // default value
    assignmentsTable.SetStyle("\"admin-table admin-table--5\"");
    assignmentsTable.SetVisible("True"); // default value
    assignmentsTable.VisibleWasExpression = false; // default value
    assignmentsTable.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentsTable.Width = "(fill parent)"; // default value
    
    var assignmentsHeader = assignmentsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsHeader");
    assignmentsHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    assignmentsHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentsHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentsHeader.Animate = false; // default value
    assignmentsHeader.ColumnGap = null; // default value
    assignmentsHeader.ColumnSpan = null; // default value
    assignmentsHeader.CustomStyle = null; // default value
    assignmentsHeader.FlexBasis = null; // default value
    assignmentsHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentsHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentsHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentsHeader.Gap = null;
    assignmentsHeader.GridColumns = null; // default value
    assignmentsHeader.GridRows = null; // default value
    assignmentsHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentsHeader.RowGap = null; // default value
    assignmentsHeader.RowSpan = null; // default value
    assignmentsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    assignmentsHeader.SetVisible("True"); // default value
    assignmentsHeader.VisibleWasExpression = false; // default value
    assignmentsHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentsHeader.Width = "(fill parent)"; // default value
    
    var container = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container.Animate = false; // default value
    container.ColumnGap = null; // default value
    container.ColumnSpan = null; // default value
    container.CustomStyle = null; // default value
    container.FlexBasis = null; // default value
    container.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container.Gap = null;
    container.GridColumns = null; // default value
    container.GridRows = null; // default value
    container.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container.RowGap = null; // default value
    container.RowSpan = null; // default value
    container.SetStyle("\"admin-table__cell\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var expression3 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("If(ProjectId <> NullIdentifier(), \"User\", \"Project\")");
    expression3.Width = ""; // default value
    
    var extendedProperty = container.CreateExtendedProperty();
    extendedProperty.Property = "role";
    extendedProperty.SetValue("\"columnheader\"");
    
    var container2 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container2.Animate = false; // default value
    container2.ColumnGap = null; // default value
    container2.ColumnSpan = null; // default value
    container2.CustomStyle = null; // default value
    container2.FlexBasis = null; // default value
    container2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container2.Gap = null;
    container2.GridColumns = null; // default value
    container2.GridRows = null; // default value
    container2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container2.RowGap = null; // default value
    container2.RowSpan = null; // default value
    container2.SetStyle("\"admin-table__cell\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var expression4 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("\"Start\"");
    expression4.Width = ""; // default value
    
    var extendedProperty2 = container2.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"columnheader\"");
    
    var container3 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container3.Animate = false; // default value
    container3.ColumnGap = null; // default value
    container3.ColumnSpan = null; // default value
    container3.CustomStyle = null; // default value
    container3.FlexBasis = null; // default value
    container3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container3.Gap = null;
    container3.GridColumns = null; // default value
    container3.GridRows = null; // default value
    container3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container3.RowGap = null; // default value
    container3.RowSpan = null; // default value
    container3.SetStyle("\"admin-table__cell\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var expression5 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("\"End\"");
    expression5.Width = ""; // default value
    
    var extendedProperty3 = container3.CreateExtendedProperty();
    extendedProperty3.Property = "role";
    extendedProperty3.SetValue("\"columnheader\"");
    
    var container4 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container4.Animate = false; // default value
    container4.ColumnGap = null; // default value
    container4.ColumnSpan = null; // default value
    container4.CustomStyle = null; // default value
    container4.FlexBasis = null; // default value
    container4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container4.Gap = null;
    container4.GridColumns = null; // default value
    container4.GridRows = null; // default value
    container4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container4.RowGap = null; // default value
    container4.RowSpan = null; // default value
    container4.SetStyle("\"admin-table__cell\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var expression6 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("\"Status\"");
    expression6.Width = ""; // default value
    
    var extendedProperty4 = container4.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
    var container5 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container5.Animate = false; // default value
    container5.ColumnGap = null; // default value
    container5.ColumnSpan = null; // default value
    container5.CustomStyle = null; // default value
    container5.FlexBasis = null; // default value
    container5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container5.Gap = null;
    container5.GridColumns = null; // default value
    container5.GridRows = null; // default value
    container5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container5.RowGap = null; // default value
    container5.RowSpan = null; // default value
    container5.SetStyle("\"admin-table__cell\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var expression7 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("\"Actions\"");
    expression7.Width = ""; // default value
    
    var extendedProperty5 = container5.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var extendedProperty6 = assignmentsHeader.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"row\"");
    
    var hasAssignments = assignmentsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasAssignments");
    hasAssignments.Animate = false; // default value
    hasAssignments.SetCondition("GetAssignments.Assignments.Length > 0 or not GetAssignments.IsDataFetched");
    hasAssignments.CustomStyle = null; // default value
    hasAssignments.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var assignmentList = hasAssignments.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("AssignmentList");
    assignmentList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentList.AnimateItems = true; // default value
    assignmentList.ColumnGap = null; // default value
    assignmentList.ColumnSpan = null; // default value
    assignmentList.CustomStyle = null; // default value
    assignmentList.ExpandedInWebEditor = true; // default value
    assignmentList.FlexBasis = null; // default value
    assignmentList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentList.Gap = null;
    assignmentList.GridColumns = null; // default value
    assignmentList.GridRows = null; // default value
    assignmentList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    assignmentList.RowGap = null; // default value
    assignmentList.RowSpan = null; // default value
    assignmentList.SetSource("GetAssignments.Assignments");
    assignmentList.SetStyle("\"admin-table__body\"");
    assignmentList.Tag = "div"; // default value
    assignmentList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentList.Width = "(fill parent)"; // default value
    
    var assignmentRow = assignmentList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentRow");
    assignmentRow.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    assignmentRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentRow.Animate = false; // default value
    assignmentRow.ColumnGap = null; // default value
    assignmentRow.ColumnSpan = null; // default value
    assignmentRow.CustomStyle = null; // default value
    assignmentRow.FlexBasis = null; // default value
    assignmentRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentRow.Gap = null;
    assignmentRow.GridColumns = null; // default value
    assignmentRow.GridRows = null; // default value
    assignmentRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentRow.RowGap = null; // default value
    assignmentRow.RowSpan = null; // default value
    assignmentRow.SetStyle("\"admin-table__row\"");
    assignmentRow.SetVisible("True"); // default value
    assignmentRow.VisibleWasExpression = false; // default value
    assignmentRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentRow.Width = "(fill parent)"; // default value
    
    var container6 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container6.Animate = false; // default value
    container6.ColumnGap = null; // default value
    container6.ColumnSpan = null; // default value
    container6.CustomStyle = null; // default value
    container6.FlexBasis = null; // default value
    container6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container6.Gap = null;
    container6.GridColumns = null; // default value
    container6.GridRows = null; // default value
    container6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container6.RowGap = null; // default value
    container6.RowSpan = null; // default value
    container6.SetStyle("\"admin-table__cell admin-table__name\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var expression8 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("If(ProjectId <> NullIdentifier(), GetAssignments.Assignments.Current.UserName, GetAssignments.Assignments.Current.ProjectName + \" (\" + GetAssignments.Assignments.Current.ClientName + \")\")");
    expression8.Width = ""; // default value
    
    var extendedProperty7 = container6.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"cell\"");
    
    var container7 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container7.Animate = false; // default value
    container7.ColumnGap = null; // default value
    container7.ColumnSpan = null; // default value
    container7.CustomStyle = null; // default value
    container7.FlexBasis = null; // default value
    container7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container7.Gap = null;
    container7.GridColumns = null; // default value
    container7.GridRows = null; // default value
    container7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container7.RowGap = null; // default value
    container7.RowSpan = null; // default value
    container7.SetStyle("\"admin-table__cell\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var expression9 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("If(GetAssignments.Assignments.Current.StartDate = NullDate(), \"-\", FormatDateTime(DateToDateTime(GetAssignments.Assignments.Current.StartDate), \"dd/MM/yyyy\"))");
    expression9.Width = ""; // default value
    
    var extendedProperty8 = container7.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"cell\"");
    
    var container8 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = false; // default value
    container8.ColumnGap = null; // default value
    container8.ColumnSpan = null; // default value
    container8.CustomStyle = null; // default value
    container8.FlexBasis = null; // default value
    container8.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container8.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container8.Gap = null;
    container8.GridColumns = null; // default value
    container8.GridRows = null; // default value
    container8.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container8.RowGap = null; // default value
    container8.RowSpan = null; // default value
    container8.SetStyle("\"admin-table__cell\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var expression10 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("If(GetAssignments.Assignments.Current.EndDate = NullDate(), \"No end\", FormatDateTime(DateToDateTime(GetAssignments.Assignments.Current.EndDate), \"dd/MM/yyyy\"))");
    expression10.Width = ""; // default value
    
    var extendedProperty9 = container8.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var container9 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container9.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container9.Animate = false; // default value
    container9.ColumnGap = null; // default value
    container9.ColumnSpan = null; // default value
    container9.CustomStyle = null; // default value
    container9.FlexBasis = null; // default value
    container9.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container9.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container9.Gap = null;
    container9.GridColumns = null; // default value
    container9.GridRows = null; // default value
    container9.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container9.RowGap = null; // default value
    container9.RowSpan = null; // default value
    container9.SetStyle("\"admin-table__cell is-\" + ToLower(GetAssignments.Assignments.Current.Status)");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var expression11 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle(null); // default value
    expression11.SetValue("GetAssignments.Assignments.Current.Status");
    expression11.Width = ""; // default value
    
    var extendedProperty10 = container9.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    
    var assignmentActions = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentActions");
    assignmentActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    assignmentActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentActions.Animate = false; // default value
    assignmentActions.ColumnGap = null; // default value
    assignmentActions.ColumnSpan = null; // default value
    assignmentActions.CustomStyle = null; // default value
    assignmentActions.FlexBasis = null; // default value
    assignmentActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentActions.Gap = null;
    assignmentActions.GridColumns = null; // default value
    assignmentActions.GridRows = null; // default value
    assignmentActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentActions.RowGap = null; // default value
    assignmentActions.RowSpan = null; // default value
    assignmentActions.SetStyle("\"admin-table__cell admin-table__actions\"");
    assignmentActions.SetVisible("True"); // default value
    assignmentActions.VisibleWasExpression = false; // default value
    assignmentActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentActions.Width = "(fill parent)"; // default value
    
    var editAssignmentLink = assignmentActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("EditAssignmentLink");
    editAssignmentLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    editAssignmentLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    editAssignmentLink.ColumnGap = null; // default value
    editAssignmentLink.ColumnSpan = null; // default value
    editAssignmentLink.SetConfirmationMessage(null); // default value
    editAssignmentLink.CustomStyle = null; // default value
    editAssignmentLink.SetEnabled("True");
    editAssignmentLink.FlexBasis = null; // default value
    editAssignmentLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    editAssignmentLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    editAssignmentLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    editAssignmentLink.Gap = null;
    editAssignmentLink.GridColumns = null; // default value
    editAssignmentLink.GridRows = null; // default value
    editAssignmentLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    editAssignmentLink.RowGap = null; // default value
    editAssignmentLink.RowSpan = null; // default value
    editAssignmentLink.SetStyle(null); // default value
    editAssignmentLink.SetVisible("True"); // default value
    editAssignmentLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    editAssignmentLink.Width = ""; // default value
    
    var textWidget = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)editAssignmentLink.Widgets.First();
    textWidget.CustomStyle = null; // default value
    textWidget.Name = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Edit";
    var builtinEvent2 = editAssignmentLink.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = onClick_EditAssignmentLink;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent2.SetArgumentValue(row2, "GetAssignments.Assignments.Current");
    
    var endAssignmentLink = assignmentActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("EndAssignmentLink");
    endAssignmentLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    endAssignmentLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    endAssignmentLink.ColumnGap = null; // default value
    endAssignmentLink.ColumnSpan = null; // default value
    endAssignmentLink.SetConfirmationMessage(null); // default value
    endAssignmentLink.CustomStyle = null; // default value
    endAssignmentLink.SetEnabled("True");
    endAssignmentLink.FlexBasis = null; // default value
    endAssignmentLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    endAssignmentLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    endAssignmentLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    endAssignmentLink.Gap = null;
    endAssignmentLink.GridColumns = null; // default value
    endAssignmentLink.GridRows = null; // default value
    endAssignmentLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    endAssignmentLink.RowGap = null; // default value
    endAssignmentLink.RowSpan = null; // default value
    endAssignmentLink.SetStyle(null); // default value
    endAssignmentLink.SetVisible("True"); // default value
    endAssignmentLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    endAssignmentLink.Width = ""; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)endAssignmentLink.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "End";
    var builtinEvent3 = endAssignmentLink.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = onClick_EndAssignmentLink;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent3.SetArgumentValue(row3, "GetAssignments.Assignments.Current");
    
    var deleteAssignmentLink = assignmentActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("DeleteAssignmentLink");
    deleteAssignmentLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    deleteAssignmentLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    deleteAssignmentLink.ColumnGap = null; // default value
    deleteAssignmentLink.ColumnSpan = null; // default value
    deleteAssignmentLink.SetConfirmationMessage(null); // default value
    deleteAssignmentLink.CustomStyle = null; // default value
    deleteAssignmentLink.SetEnabled("True");
    deleteAssignmentLink.FlexBasis = null; // default value
    deleteAssignmentLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    deleteAssignmentLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    deleteAssignmentLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    deleteAssignmentLink.Gap = null;
    deleteAssignmentLink.GridColumns = null; // default value
    deleteAssignmentLink.GridRows = null; // default value
    deleteAssignmentLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    deleteAssignmentLink.RowGap = null; // default value
    deleteAssignmentLink.RowSpan = null; // default value
    deleteAssignmentLink.SetStyle(null); // default value
    deleteAssignmentLink.SetVisible("True"); // default value
    deleteAssignmentLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    deleteAssignmentLink.Width = ""; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)deleteAssignmentLink.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Delete";
    var builtinEvent4 = deleteAssignmentLink.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = onClick_DeleteAssignmentLink;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent4.SetArgumentValue(row, "GetAssignments.Assignments.Current");
    
    var extendedProperty11 = assignmentActions.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"cell\"");
    
    var extendedProperty12 = assignmentRow.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"row\"");
    var builtinEvent5 = assignmentList.OnScrollEnding;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var noAssignments = hasAssignments.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoAssignments");
    noAssignments.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noAssignments.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noAssignments.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noAssignments.Animate = false; // default value
    noAssignments.ColumnGap = null; // default value
    noAssignments.ColumnSpan = null; // default value
    noAssignments.CustomStyle = null; // default value
    noAssignments.FlexBasis = null; // default value
    noAssignments.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noAssignments.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noAssignments.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noAssignments.Gap = null;
    noAssignments.GridColumns = null; // default value
    noAssignments.GridRows = null; // default value
    noAssignments.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noAssignments.RowGap = null; // default value
    noAssignments.RowSpan = null; // default value
    noAssignments.SetStyle("\"admin-table__empty\"");
    noAssignments.SetVisible("True"); // default value
    noAssignments.VisibleWasExpression = false; // default value
    noAssignments.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noAssignments.Width = "(fill parent)"; // default value
    
    var expression12 = noAssignments.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression12.ColumnSpan = null; // default value
    expression12.CustomStyle = null; // default value
    expression12.Example = null; // default value
    expression12.FlexBasis = null; // default value
    expression12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression12.RowSpan = null; // default value
    expression12.SetStyle(null); // default value
    expression12.SetValue("If(ProjectId <> NullIdentifier(), \"No users assigned yet.\", \"No projects assigned yet.\")");
    expression12.Width = ""; // default value
    
    var extendedProperty13 = assignmentsTable.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"table\"");
    
    var assignmentPopup = assignmentEditor.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("AssignmentPopup");
    assignmentPopup.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentPopup.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentPopup.ColumnGap = null; // default value
    assignmentPopup.ColumnSpan = null; // default value
    assignmentPopup.CustomStyle = null; // default value
    assignmentPopup.ExpandedInWebEditor = true; // default value
    assignmentPopup.FlexBasis = null; // default value
    assignmentPopup.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentPopup.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentPopup.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentPopup.Gap = null;
    assignmentPopup.GridColumns = null; // default value
    assignmentPopup.GridRows = null; // default value
    assignmentPopup.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentPopup.RowGap = null; // default value
    assignmentPopup.RowSpan = null; // default value
    assignmentPopup.SetShowPopup("ShowAssignmentPopup");
    assignmentPopup.SetStyle("\"popup-dialog\""); // default value
    assignmentPopup.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var assignmentPopupBody = assignmentPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentPopupBody");
    assignmentPopupBody.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    assignmentPopupBody.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    assignmentPopupBody.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    assignmentPopupBody.Animate = false; // default value
    assignmentPopupBody.ColumnGap = null; // default value
    assignmentPopupBody.ColumnSpan = null; // default value
    assignmentPopupBody.CustomStyle = null; // default value
    assignmentPopupBody.FlexBasis = null; // default value
    assignmentPopupBody.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    assignmentPopupBody.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    assignmentPopupBody.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    assignmentPopupBody.Gap = null;
    assignmentPopupBody.GridColumns = null; // default value
    assignmentPopupBody.GridRows = null; // default value
    assignmentPopupBody.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    assignmentPopupBody.RowGap = null; // default value
    assignmentPopupBody.RowSpan = null; // default value
    assignmentPopupBody.SetStyle("\"admin-popup\"");
    assignmentPopupBody.SetVisible("True"); // default value
    assignmentPopupBody.VisibleWasExpression = false; // default value
    assignmentPopupBody.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    assignmentPopupBody.Width = "(fill parent)"; // default value
    
    var advancedHtml2 = assignmentPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    advancedHtml2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    advancedHtml2.ColumnGap = null; // default value
    advancedHtml2.ColumnSpan = null; // default value
    advancedHtml2.CustomStyle = null; // default value
    advancedHtml2.FlexBasis = null; // default value
    advancedHtml2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    advancedHtml2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    advancedHtml2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    advancedHtml2.Gap = null;
    advancedHtml2.GridColumns = null; // default value
    advancedHtml2.GridRows = null; // default value
    advancedHtml2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    advancedHtml2.RowGap = null; // default value
    advancedHtml2.RowSpan = null; // default value
    advancedHtml2.Tag = "h3";
    advancedHtml2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var expression13 = advancedHtml2.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression13.ColumnSpan = null; // default value
    expression13.CustomStyle = null; // default value
    expression13.Example = null; // default value
    expression13.FlexBasis = null; // default value
    expression13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression13.RowSpan = null; // default value
    expression13.SetStyle(null); // default value
    expression13.SetValue("If(IsEndMode, \"End assignment\", If(EditAssignment.Id = NullIdentifier(), If(ProjectId <> NullIdentifier(), \"Add user\", \"Add project\"), \"Edit assignment\"))");
    expression13.Width = ""; // default value
    
    var hasPopupError = assignmentPopupBody.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPopupError");
    hasPopupError.Animate = false; // default value
    hasPopupError.SetCondition("PopupError <> \"\"");
    hasPopupError.CustomStyle = null; // default value
    hasPopupError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var popupErrorBox = hasPopupError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupErrorBox");
    popupErrorBox.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    popupErrorBox.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    popupErrorBox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    popupErrorBox.Animate = false; // default value
    popupErrorBox.ColumnGap = null; // default value
    popupErrorBox.ColumnSpan = null; // default value
    popupErrorBox.CustomStyle = null; // default value
    popupErrorBox.FlexBasis = null; // default value
    popupErrorBox.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    popupErrorBox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    popupErrorBox.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    popupErrorBox.Gap = null;
    popupErrorBox.GridColumns = null; // default value
    popupErrorBox.GridRows = null; // default value
    popupErrorBox.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    popupErrorBox.RowGap = null; // default value
    popupErrorBox.RowSpan = null; // default value
    popupErrorBox.SetStyle("\"admin-error\"");
    popupErrorBox.SetVisible("True"); // default value
    popupErrorBox.VisibleWasExpression = false; // default value
    popupErrorBox.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    popupErrorBox.Width = "(fill parent)"; // default value
    
    var expression14 = popupErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression14.ColumnSpan = null; // default value
    expression14.CustomStyle = null; // default value
    expression14.Example = null; // default value
    expression14.FlexBasis = null; // default value
    expression14.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression14.RowSpan = null; // default value
    expression14.SetStyle(null); // default value
    expression14.SetValue("PopupError");
    expression14.Width = ""; // default value
    
    var extendedProperty14 = popupErrorBox.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"alert\"");
    
    var isEndModeIf = assignmentPopupBody.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsEndModeIf");
    isEndModeIf.Animate = false; // default value
    isEndModeIf.SetCondition("IsEndMode");
    isEndModeIf.CustomStyle = null; // default value
    isEndModeIf.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var endFields = isEndModeIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EndFields");
    endFields.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    endFields.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    endFields.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    endFields.Animate = false; // default value
    endFields.ColumnGap = null; // default value
    endFields.ColumnSpan = null; // default value
    endFields.CustomStyle = null; // default value
    endFields.FlexBasis = null; // default value
    endFields.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    endFields.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    endFields.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    endFields.Gap = null;
    endFields.GridColumns = null; // default value
    endFields.GridRows = null; // default value
    endFields.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    endFields.RowGap = null; // default value
    endFields.RowSpan = null; // default value
    endFields.SetStyle("\"admin-form\"");
    endFields.SetVisible("True"); // default value
    endFields.VisibleWasExpression = false; // default value
    endFields.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    endFields.Width = "(fill parent)"; // default value
    
    var expression15 = endFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression15.ColumnSpan = null; // default value
    expression15.CustomStyle = null; // default value
    expression15.Example = null; // default value
    expression15.FlexBasis = null; // default value
    expression15.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression15.RowSpan = null; // default value
    expression15.SetStyle(null); // default value
    expression15.SetValue("PopupName + \", from \" + FormatDateTime(DateToDateTime(EditAssignment.StartDate), \"dd/MM/yyyy\")");
    expression15.Width = ""; // default value
    
    var textWidget4 = endFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "End date";
    
    var endOnlyDateInput = endFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("EndOnlyDateInput");
    endOnlyDateInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    endOnlyDateInput.ColumnSpan = null; // default value
    endOnlyDateInput.CustomStyle = null; // default value
    endOnlyDateInput.SetEnabled("True");
    endOnlyDateInput.FlexBasis = null; // default value
    endOnlyDateInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    endOnlyDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    endOnlyDateInput.SetMandatory("False");
    endOnlyDateInput.MaxLength = null;
    endOnlyDateInput.SetPrompt(null); // default value
    endOnlyDateInput.RowSpan = null; // default value
    endOnlyDateInput.SetStyle("\"form-control\""); // default value
    endOnlyDateInput.SetVariable("EditAssignment.EndDate");
    endOnlyDateInput.Width = "(fill parent)"; // default value
    var builtinEvent6 = endOnlyDateInput.OnChange;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = null; // default value
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty15 = endOnlyDateInput.CreateExtendedProperty();
    extendedProperty15.Property = "aria-label";
    extendedProperty15.SetValue("\"End date\"");
    
    var editFields = isEndModeIf.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EditFields");
    editFields.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    editFields.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    editFields.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    editFields.Animate = false; // default value
    editFields.ColumnGap = null; // default value
    editFields.ColumnSpan = null; // default value
    editFields.CustomStyle = null; // default value
    editFields.FlexBasis = null; // default value
    editFields.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    editFields.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    editFields.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    editFields.Gap = null;
    editFields.GridColumns = null; // default value
    editFields.GridRows = null; // default value
    editFields.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    editFields.RowGap = null; // default value
    editFields.RowSpan = null; // default value
    editFields.SetStyle("\"admin-form\"");
    editFields.SetVisible("True"); // default value
    editFields.VisibleWasExpression = false; // default value
    editFields.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    editFields.Width = "(fill parent)"; // default value
    
    var isProjectModeIf = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsProjectModeIf");
    isProjectModeIf.Animate = false; // default value
    isProjectModeIf.SetCondition("ProjectId <> NullIdentifier()");
    isProjectModeIf.CustomStyle = null; // default value
    isProjectModeIf.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var userField = isProjectModeIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("UserField");
    userField.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    userField.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    userField.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    userField.Animate = false; // default value
    userField.ColumnGap = null; // default value
    userField.ColumnSpan = null; // default value
    userField.CustomStyle = null; // default value
    userField.FlexBasis = null; // default value
    userField.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    userField.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    userField.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    userField.Gap = null;
    userField.GridColumns = null; // default value
    userField.GridRows = null; // default value
    userField.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    userField.RowGap = null; // default value
    userField.RowSpan = null; // default value
    userField.SetStyle("\"admin-form\"");
    userField.SetVisible("True"); // default value
    userField.VisibleWasExpression = false; // default value
    userField.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    userField.Width = "(fill parent)"; // default value
    
    var textWidget5 = userField.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "User";
    
    var userDropdown = userField.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("UserDropdown");
    userDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    userDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    userDropdown.ColumnGap = null; // default value
    userDropdown.ColumnSpan = null; // default value
    userDropdown.CustomStyle = null; // default value
    userDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    userDropdown.SetEmptyValue("\"Choose a user\"");
    userDropdown.SetEnabled("True");
    userDropdown.FlexBasis = null; // default value
    userDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    userDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    userDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    userDropdown.Gap = null;
    userDropdown.GridColumns = null; // default value
    userDropdown.GridRows = null; // default value
    userDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    userDropdown.SetLabels("Name + \" (\" + Username + \")\"");
    userDropdown.SetList("GetUsers.Users");
    userDropdown.SetMandatory("True");
    userDropdown.RowGap = null; // default value
    userDropdown.RowSpan = null; // default value
    userDropdown.SetStyle("\"dropdown-display\"");
    userDropdown.SetValues("UserId");
    userDropdown.SetVariable("EditAssignment.UserId");
    userDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    userDropdown.Width = ""; // default value
    var builtinEvent7 = userDropdown.OnChange;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = null; // default value
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty16 = userDropdown.CreateExtendedProperty();
    extendedProperty16.Property = "aria-label";
    extendedProperty16.SetValue("\"User\"");
    
    var projectField = isProjectModeIf.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectField");
    projectField.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    projectField.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectField.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectField.Animate = false; // default value
    projectField.ColumnGap = null; // default value
    projectField.ColumnSpan = null; // default value
    projectField.CustomStyle = null; // default value
    projectField.FlexBasis = null; // default value
    projectField.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectField.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectField.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectField.Gap = null;
    projectField.GridColumns = null; // default value
    projectField.GridRows = null; // default value
    projectField.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectField.RowGap = null; // default value
    projectField.RowSpan = null; // default value
    projectField.SetStyle("\"admin-form\"");
    projectField.SetVisible("True"); // default value
    projectField.VisibleWasExpression = false; // default value
    projectField.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectField.Width = "(fill parent)"; // default value
    
    var textWidget6 = projectField.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Project";
    
    var projectDropdown = projectField.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ProjectDropdown");
    projectDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectDropdown.ColumnGap = null; // default value
    projectDropdown.ColumnSpan = null; // default value
    projectDropdown.CustomStyle = null; // default value
    projectDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    projectDropdown.SetEmptyValue("\"Choose a project\"");
    projectDropdown.SetEnabled("True");
    projectDropdown.FlexBasis = null; // default value
    projectDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectDropdown.Gap = null;
    projectDropdown.GridColumns = null; // default value
    projectDropdown.GridRows = null; // default value
    projectDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectDropdown.SetLabels("Name + \" (\" + ClientName + \")\"");
    projectDropdown.SetList("GetProjects.Projects");
    projectDropdown.SetMandatory("True");
    projectDropdown.RowGap = null; // default value
    projectDropdown.RowSpan = null; // default value
    projectDropdown.SetStyle("\"dropdown-display\"");
    projectDropdown.SetValues("Id");
    projectDropdown.SetVariable("EditAssignment.ProjectId");
    projectDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectDropdown.Width = ""; // default value
    var builtinEvent8 = projectDropdown.OnChange;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = null; // default value
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty17 = projectDropdown.CreateExtendedProperty();
    extendedProperty17.Property = "aria-label";
    extendedProperty17.SetValue("\"Project\"");
    
    var textWidget7 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Start date";
    
    var startDateInput = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("StartDateInput");
    startDateInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    startDateInput.ColumnSpan = null; // default value
    startDateInput.CustomStyle = null; // default value
    startDateInput.SetEnabled("True");
    startDateInput.FlexBasis = null; // default value
    startDateInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    startDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    startDateInput.SetMandatory("True");
    startDateInput.MaxLength = null;
    startDateInput.SetPrompt(null); // default value
    startDateInput.RowSpan = null; // default value
    startDateInput.SetStyle("\"form-control\""); // default value
    startDateInput.SetVariable("EditAssignment.StartDate");
    startDateInput.Width = "(fill parent)"; // default value
    var builtinEvent9 = startDateInput.OnChange;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = null; // default value
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty18 = startDateInput.CreateExtendedProperty();
    extendedProperty18.Property = "aria-label";
    extendedProperty18.SetValue("\"Start date\"");
    
    var textWidget8 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "End date";
    
    var endDateInput = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("EndDateInput");
    endDateInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    endDateInput.ColumnSpan = null; // default value
    endDateInput.CustomStyle = null; // default value
    endDateInput.SetEnabled("True");
    endDateInput.FlexBasis = null; // default value
    endDateInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    endDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    endDateInput.SetMandatory("False");
    endDateInput.MaxLength = null;
    endDateInput.SetPrompt(null); // default value
    endDateInput.RowSpan = null; // default value
    endDateInput.SetStyle("\"form-control\""); // default value
    endDateInput.SetVariable("EditAssignment.EndDate");
    endDateInput.Width = "(fill parent)"; // default value
    var builtinEvent10 = endDateInput.OnChange;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = null; // default value
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty19 = endDateInput.CreateExtendedProperty();
    extendedProperty19.Property = "aria-label";
    extendedProperty19.SetValue("\"End date\"");
    
    var endDateHint = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EndDateHint");
    endDateHint.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    endDateHint.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    endDateHint.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    endDateHint.Animate = false; // default value
    endDateHint.ColumnGap = null; // default value
    endDateHint.ColumnSpan = null; // default value
    endDateHint.CustomStyle = null; // default value
    endDateHint.FlexBasis = null; // default value
    endDateHint.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    endDateHint.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    endDateHint.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    endDateHint.Gap = null;
    endDateHint.GridColumns = null; // default value
    endDateHint.GridRows = null; // default value
    endDateHint.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    endDateHint.RowGap = null; // default value
    endDateHint.RowSpan = null; // default value
    endDateHint.SetStyle("\"admin-form__hint\"");
    endDateHint.SetVisible("True"); // default value
    endDateHint.VisibleWasExpression = false; // default value
    endDateHint.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    endDateHint.Width = "(fill parent)"; // default value
    
    var textWidget9 = endDateHint.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.CustomStyle = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Leave the end date empty when the assignment has no end.";
    
    var popupActions = assignmentPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupActions");
    popupActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    popupActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    popupActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    popupActions.Animate = false; // default value
    popupActions.ColumnGap = null; // default value
    popupActions.ColumnSpan = null; // default value
    popupActions.CustomStyle = null; // default value
    popupActions.FlexBasis = null; // default value
    popupActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    popupActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    popupActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    popupActions.Gap = null;
    popupActions.GridColumns = null; // default value
    popupActions.GridRows = null; // default value
    popupActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    popupActions.RowGap = null; // default value
    popupActions.RowSpan = null; // default value
    popupActions.SetStyle("\"admin-form__actions\"");
    popupActions.SetVisible("True"); // default value
    popupActions.VisibleWasExpression = false; // default value
    popupActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    popupActions.Width = "(fill parent)"; // default value
    
    var saveAssignmentButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveAssignmentButton");
    saveAssignmentButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    saveAssignmentButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    saveAssignmentButton.ColumnGap = null; // default value
    saveAssignmentButton.ColumnSpan = null; // default value
    saveAssignmentButton.SetConfirmationMessage(null); // default value
    saveAssignmentButton.CustomStyle = null; // default value
    saveAssignmentButton.SetEnabled("True");
    saveAssignmentButton.FlexBasis = null; // default value
    saveAssignmentButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    saveAssignmentButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    saveAssignmentButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    saveAssignmentButton.Gap = null;
    saveAssignmentButton.GridColumns = null; // default value
    saveAssignmentButton.GridRows = null; // default value
    saveAssignmentButton.IsDefault = false; // default value
    saveAssignmentButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    saveAssignmentButton.RowGap = null; // default value
    saveAssignmentButton.RowSpan = null; // default value
    saveAssignmentButton.SetStyle("\"btn btn-primary\"");
    saveAssignmentButton.SetVisible("not IsEndMode");
    saveAssignmentButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveAssignmentButton.Width = ""; // default value
    
    var textWidget10 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveAssignmentButton.Widgets.First();
    textWidget10.CustomStyle = null; // default value
    textWidget10.Name = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Save";
    var builtinEvent11 = saveAssignmentButton.OnClick;
    builtinEvent11.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent11.Destination = onClick_SaveAssignmentButton;
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var saveEndButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveEndButton");
    saveEndButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    saveEndButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    saveEndButton.ColumnGap = null; // default value
    saveEndButton.ColumnSpan = null; // default value
    saveEndButton.SetConfirmationMessage(null); // default value
    saveEndButton.CustomStyle = null; // default value
    saveEndButton.SetEnabled("True");
    saveEndButton.FlexBasis = null; // default value
    saveEndButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    saveEndButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    saveEndButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    saveEndButton.Gap = null;
    saveEndButton.GridColumns = null; // default value
    saveEndButton.GridRows = null; // default value
    saveEndButton.IsDefault = false; // default value
    saveEndButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    saveEndButton.RowGap = null; // default value
    saveEndButton.RowSpan = null; // default value
    saveEndButton.SetStyle("\"btn btn-primary\"");
    saveEndButton.SetVisible("IsEndMode");
    saveEndButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveEndButton.Width = ""; // default value
    
    var textWidget11 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveEndButton.Widgets.First();
    textWidget11.CustomStyle = null; // default value
    textWidget11.Name = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "End assignment";
    var builtinEvent12 = saveEndButton.OnClick;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent12.Destination = onClick_SaveEndButton;
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelPopupButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelPopupButton");
    cancelPopupButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    cancelPopupButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    cancelPopupButton.ColumnGap = null; // default value
    cancelPopupButton.ColumnSpan = null; // default value
    cancelPopupButton.SetConfirmationMessage(null); // default value
    cancelPopupButton.CustomStyle = null; // default value
    cancelPopupButton.SetEnabled("True");
    cancelPopupButton.FlexBasis = null; // default value
    cancelPopupButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    cancelPopupButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    cancelPopupButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    cancelPopupButton.Gap = null;
    cancelPopupButton.GridColumns = null; // default value
    cancelPopupButton.GridRows = null; // default value
    cancelPopupButton.IsDefault = false; // default value
    cancelPopupButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    cancelPopupButton.RowGap = null; // default value
    cancelPopupButton.RowSpan = null; // default value
    cancelPopupButton.SetStyle("\"btn\""); // default value
    cancelPopupButton.SetVisible("True"); // default value
    cancelPopupButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    cancelPopupButton.Width = ""; // default value
    
    var textWidget12 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)cancelPopupButton.Widgets.First();
    textWidget12.CustomStyle = null; // default value
    textWidget12.Name = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Cancel";
    var builtinEvent13 = cancelPopupButton.OnClick;
    builtinEvent13.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent13.Destination = onClick_CancelPopupButton;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty20 = assignmentPopupBody.CreateExtendedProperty();
    extendedProperty20.Property = "role";
    extendedProperty20.SetValue("\"dialog\"");
}
