# ProjectDetail

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileScreen 'ProjectDetail' ***/
    var projectDetail = admin.CreateScreen("ProjectDetail");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    projectDetail.Widgets.ToList().ForEach(x => x.Delete());
    projectDetail.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    projectDetail.Roles.Add(sheetAdmin);
    projectDetail.Description = "Create or edit one project (SheetAdmin): client, code, name, activate/deactivate, and its assigned users (add, edit, end, delete in a popup). ProjectId empty = new project.";
    projectDetail.HorizontalPosition = 1600;
    projectDetail.SetTitle("\"Project\"");
    projectDetail.VerticalPosition = 1400;
    
    var getAssignments = projectDetail.CreateDataAction("GetAssignments");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getAssignments.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("ProjectId = NullIdentifier()");
    ifNode.Label = "New project?";
    
    var projectAssignment_GetList = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_GetList").Below(ifNode, 1371);
    var projectAssignment_GetList2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_GetList");
    projectAssignment_GetList.Action = projectAssignment_GetList2;
    var projectId = projectAssignment_GetList2.InputParameters.Named("ProjectId");
    projectAssignment_GetList.SetArgumentValue(projectId, "ProjectId");
    var userId = projectAssignment_GetList2.InputParameters.Named("UserId");
    projectAssignment_GetList.SetArgumentValue(userId, "NullIdentifier()");
    ifNode.FalseTarget = projectAssignment_GetList;
    
    var endNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    ifNode.TrueTarget = endNode;
    
    var assignNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_GetList, 1371);
    assignNode.Label = "Set assignments";
    
    var assignment = assignNode.CreateAssignment("Assignments", "ProjectAssignment_GetList.Assignments");
    
    var endNode2 = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var allExceptions = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 8684;
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode3 = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var assignments = getAssignments.CreateOutputParameter("Assignments");
    var projectAssignmentSummary = claudeSheet_CS.Structures.Named("ProjectAssignmentSummary");
    assignments.DataType = eSpace.GetOrCreateListType(projectAssignmentSummary);
    
    var onAfterFetch_GetAssignments = projectDetail.CreateScreenAction("OnAfterFetch_GetAssignments");
    onAfterFetch_GetAssignments.Description = "Shows an error message when GetAssignments failed.";
    
    var startNode2 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var ifNode2 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode2.SetCondition("GetAssignments.ErrorMessage <> \"\"");
    ifNode2.Label = "Failed?";
    
    var endNode4 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    
    var messageNode = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode2, 1371);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetAssignments.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode5 = onAfterFetch_GetAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getAssignments.OnAfterFetch.Destination = onAfterFetch_GetAssignments;
    ifNode2.FalseTarget = endNode4;
    ifNode2.TrueTarget = messageNode;
    
    var errorMessage = getAssignments.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var getClients = projectDetail.CreateDataAction("GetClients");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getClients.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode3 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var client_GetList = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetList").ConnectedBelow(startNode3, 1371);
    var client_GetList2 = claudeSheet_CS.ServerActions.Named("Client_GetList");
    client_GetList.Action = client_GetList2;
    var includeInactive = client_GetList2.InputParameters.Named("IncludeInactive");
    client_GetList.SetArgumentValue(includeInactive, "True");
    var search = client_GetList2.InputParameters.Named("Search");
    client_GetList.SetArgumentValue(search, "\"\"");
    
    var assignNode3 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(client_GetList, 1371);
    assignNode3.Label = "Set clients";
    
    var assignment3 = assignNode3.CreateAssignment("Clients", "Client_GetList.Clients");
    
    var endNode6 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    
    var allExceptions2 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode4 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode4.Label = "Keep error";
    
    var assignment4 = assignNode4.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode7 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    
    var clients = getClients.CreateOutputParameter("Clients");
    var clientSummary = claudeSheet_CS.Structures.Named("ClientSummary");
    clients.DataType = eSpace.GetOrCreateListType(clientSummary);
    
    var errorMessage2 = getClients.CreateOutputParameter("ErrorMessage");
    errorMessage2.DataType = eSpace.TextType;
    
    var getProject = projectDetail.CreateDataAction("GetProject");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getProject.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode4 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.VerticalPosition = 800;
    
    var ifNode3 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode4, 1371);
    ifNode3.SetCondition("ProjectId = NullIdentifier()");
    ifNode3.Label = "New project?";
    
    var project_GetById = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetById").Below(ifNode3, 1371);
    var project_GetById2 = claudeSheet_CS.ServerActions.Named("Project_GetById");
    project_GetById.Action = project_GetById2;
    var projectId2 = project_GetById2.InputParameters.Named("ProjectId");
    project_GetById.SetArgumentValue(projectId2, "ProjectId");
    ifNode3.FalseTarget = project_GetById;
    
    var assignNode5 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 1828);
    assignNode5.Label = "New project defaults";
    
    var assignment5 = assignNode5.CreateAssignment("ProjectRecord.IsActive", "True");
    ifNode3.TrueTarget = assignNode5;
    
    var assignNode6 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetById, 1371);
    assignNode6.Label = "Set project";
    
    var assignment6 = assignNode6.CreateAssignment("ProjectRecord", "Project_GetById.ProjectRecord");
    
    var endNode8 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    
    var endNode9 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode6, 1371);
    
    var allExceptions3 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 8684;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode7 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode7.Label = "Keep error";
    
    var assignment7 = assignNode7.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode10 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode7, 1371);
    
    var projectRecord = getProject.CreateOutputParameter("ProjectRecord");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    projectRecord.DataType = project;
    
    var onAfterFetch_GetProject = projectDetail.CreateScreenAction("OnAfterFetch_GetProject");
    onAfterFetch_GetProject.Description = "Copies the fetched project into EditProject; shows an error message when GetProject failed.";
    
    var startNode5 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.VerticalPosition = 800;
    
    var assignNode8 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode8.Label = "Copy to form";
    
    var assignment8 = assignNode8.CreateAssignment("EditProject", "GetProject.ProjectRecord");
    
    var ifNode4 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode8, 1371);
    ifNode4.SetCondition("GetProject.ErrorMessage <> \"\"");
    ifNode4.Label = "Failed?";
    
    var endNode11 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode4, 1828);
    
    var messageNode2 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode4, 1371);
    messageNode2.Label = "Show error";
    messageNode2.SetMessage("GetProject.ErrorMessage");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode12 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1371);
    getProject.OnAfterFetch.Destination = onAfterFetch_GetProject;
    ifNode4.FalseTarget = endNode11;
    ifNode4.TrueTarget = messageNode2;
    
    var errorMessage3 = getProject.CreateOutputParameter("ErrorMessage");
    errorMessage3.DataType = eSpace.TextType;
    
    var getUsers = projectDetail.CreateDataAction("GetUsers");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getUsers.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode6 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.VerticalPosition = 800;
    
    var user_GetActive = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetActive").ConnectedBelow(startNode6, 1371);
    var user_GetActive2 = claudeSheet_CS.ServerActions.Named("User_GetActive");
    user_GetActive.Action = user_GetActive2;
    
    var assignNode9 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(user_GetActive, 1371);
    assignNode9.Label = "Set users";
    
    var assignment9 = assignNode9.CreateAssignment("Users", "User_GetActive.Users");
    
    var endNode13 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode9, 1371);
    
    var allExceptions4 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.VerticalPosition = 800;
    
    var assignNode10 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions4, 1371);
    assignNode10.Label = "Keep error";
    
    var assignment10 = assignNode10.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode14 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode10, 1371);
    
    var users = getUsers.CreateOutputParameter("Users");
    var userOption = claudeSheet_CS.Structures.Named("UserOption");
    users.DataType = eSpace.GetOrCreateListType(userOption);
    
    var errorMessage4 = getUsers.CreateOutputParameter("ErrorMessage");
    errorMessage4.DataType = eSpace.TextType;
    
    var projectId3 = projectDetail.CreateInputParameter("ProjectId");
    projectId3.DataType = project.IdentifierType;
    projectId3.IsMandatory = false;
    
    var editProject = projectDetail.CreateLocalVariable("EditProject");
    editProject.DataType = project;
    editProject.Description = "Editable copy of the project, bound to the form (filled by OnAfterFetch_GetProject).";
    
    var formError = projectDetail.CreateLocalVariable("FormError");
    formError.DataType = eSpace.TextType;
    formError.Description = "Message of the last failed project save or activate/deactivate (e.g. ProjectValidation), shown above the form.";
    
    var editAssignment = projectDetail.CreateLocalVariable("EditAssignment");
    var projectAssignment = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProjectAssignment");
    editAssignment.DataType = projectAssignment;
    editAssignment.Description = "Assignment being added, edited or ended in the popup.";
    
    var showAssignmentPopup = projectDetail.CreateLocalVariable("ShowAssignmentPopup");
    showAssignmentPopup.DataType = eSpace.BooleanType;
    showAssignmentPopup.SetDefaultValue("False");
    
    var isEndMode = projectDetail.CreateLocalVariable("IsEndMode");
    isEndMode.DataType = eSpace.BooleanType;
    isEndMode.SetDefaultValue("False");
    isEndMode.Description = "True when the popup only asks for the end date (End, or a refused Delete).";
    
    var popupError = projectDetail.CreateLocalVariable("PopupError");
    popupError.DataType = eSpace.TextType;
    popupError.Description = "Message shown in the popup (e.g. AssignmentValidation from a save, or why a Delete was refused).";
    
    var popupUserName = projectDetail.CreateLocalVariable("PopupUserName");
    popupUserName.DataType = eSpace.TextType;
    popupUserName.Description = "Name of the assignment\'s user, shown in End mode.";
    
    var onClick_AddAssignmentButton = projectDetail.CreateScreenAction("OnClick_AddAssignmentButton");
    onClick_AddAssignmentButton.Description = "Opens the popup for a new assignment of this project (start today, no end).";
    
    var startNode7 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.VerticalPosition = 800;
    
    var assignNode11 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode11.Label = "New assignment";
    
    var assignment11 = assignNode11.CreateAssignment("EditAssignment.Id", "NullIdentifier()");
    
    var assignment12 = assignNode11.CreateAssignment("EditAssignment.ProjectId", "ProjectId");
    
    var assignment13 = assignNode11.CreateAssignment("EditAssignment.UserId", "NullIdentifier()");
    
    var assignment14 = assignNode11.CreateAssignment("EditAssignment.StartDate", "CurrDate()");
    
    var assignment15 = assignNode11.CreateAssignment("EditAssignment.EndDate", "NullDate()");
    
    var assignment16 = assignNode11.CreateAssignment("EditAssignment.IsActive", "True");
    
    var assignment17 = assignNode11.CreateAssignment("PopupUserName", "\"\"");
    
    var assignment18 = assignNode11.CreateAssignment("PopupError", "\"\"");
    
    var assignment19 = assignNode11.CreateAssignment("IsEndMode", "False");
    
    var assignment20 = assignNode11.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode15 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode11, 1371);
    
    var onClick_CancelPopupButton = projectDetail.CreateScreenAction("OnClick_CancelPopupButton");
    onClick_CancelPopupButton.Description = "Closes the assignment popup without saving.";
    
    var startNode8 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.HorizontalPosition = 3200;
    startNode8.VerticalPosition = 800;
    
    var assignNode12 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode8, 1371);
    assignNode12.Label = "Close popup";
    
    var assignment21 = assignNode12.CreateAssignment("ShowAssignmentPopup", "False");
    
    var endNode16 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode12, 1371);
    
    var onClick_DeleteAssignmentLink = projectDetail.CreateScreenAction("OnClick_DeleteAssignmentLink");
    onClick_DeleteAssignmentLink.Description = "Deletes an assignment through ProjectAssignment_Delete. When it is refused (the user has time entries), the popup opens in End mode with the reason, offering End instead.";
    
    var row = onClick_DeleteAssignmentLink.CreateInputParameter("Row");
    row.DataType = projectAssignmentSummary;
    
    var startNode9 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.HorizontalPosition = 3200;
    startNode9.VerticalPosition = 800;
    
    var projectAssignment_Delete = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_Delete").ConnectedBelow(startNode9, 1371);
    var projectAssignment_Delete2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_Delete");
    projectAssignment_Delete.Action = projectAssignment_Delete2;
    var projectAssignmentId = projectAssignment_Delete2.InputParameters.Named("ProjectAssignmentId");
    projectAssignment_Delete.SetArgumentValue(projectAssignmentId, "Row.Id");
    
    var refreshDataNode = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(projectAssignment_Delete, 1371);
    refreshDataNode.DataSource = getAssignments;
    
    var messageNode3 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode, 1371);
    messageNode3.Label = "Show deleted";
    messageNode3.SetMessage("\"Assignment deleted.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode17 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    
    var allExceptions5 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions5.AbortTransaction = false;
    allExceptions5.Exception = eSpace.AllExceptions;
    allExceptions5.HorizontalPosition = 6856;
    allExceptions5.LogError = false;
    allExceptions5.VerticalPosition = 800;
    
    var assignNode13 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions5, 1371);
    assignNode13.Label = "Offer end instead";
    
    var assignment22 = assignNode13.CreateAssignment("EditAssignment.Id", "Row.Id");
    
    var assignment23 = assignNode13.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId");
    
    var assignment24 = assignNode13.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    
    var assignment25 = assignNode13.CreateAssignment("EditAssignment.StartDate", "Row.StartDate");
    
    var assignment26 = assignNode13.CreateAssignment("EditAssignment.EndDate", "If(Row.EndDate = NullDate(), CurrDate(), Row.EndDate)");
    
    var assignment27 = assignNode13.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    
    var assignment28 = assignNode13.CreateAssignment("PopupUserName", "Row.UserName");
    
    var assignment29 = assignNode13.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var assignment30 = assignNode13.CreateAssignment("IsEndMode", "True");
    
    var assignment31 = assignNode13.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode18 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode13, 1371);
    
    var onClick_EditAssignmentLink = projectDetail.CreateScreenAction("OnClick_EditAssignmentLink");
    onClick_EditAssignmentLink.Description = "Opens the popup to edit an assignment (user and dates).";
    
    var row2 = onClick_EditAssignmentLink.CreateInputParameter("Row");
    row2.DataType = projectAssignmentSummary;
    
    var startNode10 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.VerticalPosition = 800;
    
    var assignNode14 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode10, 1371);
    assignNode14.Label = "Edit assignment";
    
    var assignment32 = assignNode14.CreateAssignment("EditAssignment.Id", "Row.Id");
    
    var assignment33 = assignNode14.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId");
    
    var assignment34 = assignNode14.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    
    var assignment35 = assignNode14.CreateAssignment("EditAssignment.StartDate", "Row.StartDate");
    
    var assignment36 = assignNode14.CreateAssignment("EditAssignment.EndDate", "Row.EndDate");
    
    var assignment37 = assignNode14.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    
    var assignment38 = assignNode14.CreateAssignment("PopupUserName", "Row.UserName");
    
    var assignment39 = assignNode14.CreateAssignment("PopupError", "\"\"");
    
    var assignment40 = assignNode14.CreateAssignment("IsEndMode", "False");
    
    var assignment41 = assignNode14.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode19 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode14, 1371);
    
    var onClick_EndAssignmentLink = projectDetail.CreateScreenAction("OnClick_EndAssignmentLink");
    onClick_EndAssignmentLink.Description = "Opens the popup in End mode: only the end date (today when the assignment has no end).";
    
    var row3 = onClick_EndAssignmentLink.CreateInputParameter("Row");
    row3.DataType = projectAssignmentSummary;
    
    var startNode11 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.VerticalPosition = 800;
    
    var assignNode15 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode11, 1371);
    assignNode15.Label = "Open end popup";
    
    var assignment42 = assignNode15.CreateAssignment("EditAssignment.Id", "Row.Id");
    
    var assignment43 = assignNode15.CreateAssignment("EditAssignment.ProjectId", "Row.ProjectId");
    
    var assignment44 = assignNode15.CreateAssignment("EditAssignment.UserId", "Row.UserId");
    
    var assignment45 = assignNode15.CreateAssignment("EditAssignment.StartDate", "Row.StartDate");
    
    var assignment46 = assignNode15.CreateAssignment("EditAssignment.EndDate", "If(Row.EndDate = NullDate(), CurrDate(), Row.EndDate)");
    
    var assignment47 = assignNode15.CreateAssignment("EditAssignment.IsActive", "Row.IsActive");
    
    var assignment48 = assignNode15.CreateAssignment("PopupUserName", "Row.UserName");
    
    var assignment49 = assignNode15.CreateAssignment("PopupError", "\"\"");
    
    var assignment50 = assignNode15.CreateAssignment("IsEndMode", "True");
    
    var assignment51 = assignNode15.CreateAssignment("ShowAssignmentPopup", "True");
    
    var endNode20 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode15, 1371);
    
    var onClick_SaveAssignmentButton = projectDetail.CreateScreenAction("OnClick_SaveAssignmentButton");
    onClick_SaveAssignmentButton.Description = "Saves EditAssignment through ProjectAssignment_CreateOrUpdate, closes the popup and reloads GetAssignments. AssignmentValidation (or any other) error is shown in the popup.";
    
    var startNode12 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.VerticalPosition = 800;
    
    var assignNode16 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode12, 1371);
    assignNode16.Label = "Clear error";
    
    var assignment52 = assignNode16.CreateAssignment("PopupError", "\"\"");
    
    var projectAssignment_CreateOrUpdate = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_CreateOrUpdate").ConnectedBelow(assignNode16, 1371);
    var projectAssignment_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_CreateOrUpdate");
    projectAssignment_CreateOrUpdate.Action = projectAssignment_CreateOrUpdate2;
    var projectAssignmentRecord = projectAssignment_CreateOrUpdate2.InputParameters.Named("ProjectAssignmentRecord");
    projectAssignment_CreateOrUpdate.SetArgumentValue(projectAssignmentRecord, "EditAssignment");
    
    var assignNode17 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_CreateOrUpdate, 1371);
    assignNode17.Label = "Close popup";
    
    var assignment53 = assignNode17.CreateAssignment("ShowAssignmentPopup", "False");
    
    var refreshDataNode2 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode17, 1371);
    refreshDataNode2.DataSource = getAssignments;
    
    var messageNode4 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode2, 1371);
    messageNode4.Label = "Show saved";
    messageNode4.SetMessage("\"Assignment saved.\"");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode21 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1371);
    
    var allExceptions6 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions6.AbortTransaction = false;
    allExceptions6.Exception = eSpace.AllExceptions;
    allExceptions6.HorizontalPosition = 6856;
    allExceptions6.LogError = false;
    allExceptions6.VerticalPosition = 800;
    
    var assignNode18 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions6, 1371);
    assignNode18.Label = "Keep error";
    
    var assignment54 = assignNode18.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode22 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode18, 1371);
    
    var onClick_SaveButton = projectDetail.CreateScreenAction("OnClick_SaveButton");
    onClick_SaveButton.Description = "Saves EditProject through Project_CreateOrUpdate. A new project opens its own ProjectDetail (to add users); an existing one reloads GetProject. ProjectValidation (or any other) error is shown above the form.";
    
    var startNode13 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode13.HorizontalPosition = 3200;
    startNode13.VerticalPosition = 800;
    
    var assignNode19 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode13, 1371);
    assignNode19.Label = "Clear error";
    
    var assignment55 = assignNode19.CreateAssignment("FormError", "\"\"");
    
    var project_CreateOrUpdate = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_CreateOrUpdate").ConnectedBelow(assignNode19, 1371);
    var project_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("Project_CreateOrUpdate");
    project_CreateOrUpdate.Action = project_CreateOrUpdate2;
    var projectRecord2 = project_CreateOrUpdate2.InputParameters.Named("ProjectRecord");
    project_CreateOrUpdate.SetArgumentValue(projectRecord2, "EditProject");
    
    var messageNode5 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(project_CreateOrUpdate, 1371);
    messageNode5.Label = "Show saved";
    messageNode5.SetMessage("\"Project saved.\"");
    messageNode5.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var ifNode5 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(messageNode5, 1371);
    ifNode5.SetCondition("ProjectId = NullIdentifier()");
    ifNode5.Label = "New project?";
    
    var refreshDataNode3 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().Below(ifNode5, 1371);
    refreshDataNode3.DataSource = getProject;
    ifNode5.FalseTarget = refreshDataNode3;
    
    var destinationNode = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode5, 1828);
    destinationNode.Destination = projectDetail;
    destinationNode.SetArgumentValue(projectId3, "Project_CreateOrUpdate.ProjectId");
    ifNode5.TrueTarget = destinationNode;
    
    var endNode23 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode3, 1371);
    
    var allExceptions7 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions7.AbortTransaction = false;
    allExceptions7.Exception = eSpace.AllExceptions;
    allExceptions7.HorizontalPosition = 8684;
    allExceptions7.LogError = false;
    allExceptions7.VerticalPosition = 800;
    
    var assignNode20 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions7, 1371);
    assignNode20.Label = "Keep error";
    
    var assignment56 = assignNode20.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode24 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode20, 1371);
    
    var onClick_SaveEndButton = projectDetail.CreateScreenAction("OnClick_SaveEndButton");
    onClick_SaveEndButton.Description = "Ends the assignment on EditAssignment.EndDate through ProjectAssignment_End, closes the popup and reloads GetAssignments. Errors are shown in the popup.";
    
    var startNode14 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode14.HorizontalPosition = 3200;
    startNode14.VerticalPosition = 800;
    
    var assignNode21 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode14, 1371);
    assignNode21.Label = "Clear error";
    
    var assignment57 = assignNode21.CreateAssignment("PopupError", "\"\"");
    
    var projectAssignment_End = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_End").ConnectedBelow(assignNode21, 1371);
    var projectAssignment_End2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_End");
    projectAssignment_End.Action = projectAssignment_End2;
    var projectAssignmentId2 = projectAssignment_End2.InputParameters.Named("ProjectAssignmentId");
    projectAssignment_End.SetArgumentValue(projectAssignmentId2, "EditAssignment.Id");
    var endDate = projectAssignment_End2.InputParameters.Named("EndDate");
    projectAssignment_End.SetArgumentValue(endDate, "EditAssignment.EndDate");
    
    var assignNode22 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_End, 1371);
    assignNode22.Label = "Close popup";
    
    var assignment58 = assignNode22.CreateAssignment("ShowAssignmentPopup", "False");
    
    var refreshDataNode4 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode22, 1371);
    refreshDataNode4.DataSource = getAssignments;
    
    var messageNode6 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1371);
    messageNode6.Label = "Show ended";
    messageNode6.SetMessage("\"Assignment ended.\"");
    messageNode6.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode25 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode6, 1371);
    
    var allExceptions8 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions8.AbortTransaction = false;
    allExceptions8.Exception = eSpace.AllExceptions;
    allExceptions8.HorizontalPosition = 6856;
    allExceptions8.LogError = false;
    allExceptions8.VerticalPosition = 800;
    
    var assignNode23 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions8, 1371);
    assignNode23.Label = "Keep error";
    
    var assignment59 = assignNode23.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode26 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode23, 1371);
    
    var onClick_ToggleActiveButton = projectDetail.CreateScreenAction("OnClick_ToggleActiveButton");
    onClick_ToggleActiveButton.Description = "Activates or deactivates the project through Project_SetActive and reloads GetProject.";
    
    var startNode15 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode15.HorizontalPosition = 3200;
    startNode15.VerticalPosition = 800;
    
    var assignNode24 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode15, 1371);
    assignNode24.Label = "Clear error";
    
    var assignment60 = assignNode24.CreateAssignment("FormError", "\"\"");
    
    var project_SetActive = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_SetActive").ConnectedBelow(assignNode24, 1371);
    var project_SetActive2 = claudeSheet_CS.ServerActions.Named("Project_SetActive");
    project_SetActive.Action = project_SetActive2;
    var projectId4 = project_SetActive2.InputParameters.Named("ProjectId");
    project_SetActive.SetArgumentValue(projectId4, "ProjectId");
    var isActive = project_SetActive2.InputParameters.Named("IsActive");
    project_SetActive.SetArgumentValue(isActive, "not GetProject.ProjectRecord.IsActive");
    
    var refreshDataNode5 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(project_SetActive, 1371);
    refreshDataNode5.DataSource = getProject;
    
    var messageNode7 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode5, 1371);
    messageNode7.Label = "Show status";
    messageNode7.SetMessage("If(GetProject.ProjectRecord.IsActive, \"Project activated.\", \"Project deactivated.\")");
    messageNode7.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode27 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode7, 1371);
    
    var allExceptions9 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions9.AbortTransaction = false;
    allExceptions9.Exception = eSpace.AllExceptions;
    allExceptions9.HorizontalPosition = 6856;
    allExceptions9.LogError = false;
    allExceptions9.VerticalPosition = 800;
    
    var assignNode25 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions9, 1371);
    assignNode25.Label = "Keep error";
    
    var assignment61 = assignNode25.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode28 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode25, 1371);
    
    var layoutTopMenuInstance = projectDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "2");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.Text = "Project";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var adminPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    adminPage.Gap = null;
    adminPage.SetStyle("\"admin-page\"");
    
    var backLink = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("BackLink");
    backLink.SetEnabled("True");
    backLink.Gap = null;
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)backLink.Widgets.First();
    textWidget2.Text = "Back to projects";
    var builtinEvent = backLink.OnClick;
    var projects = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Projects");
    builtinEvent.Destination = projects;
    
    var hasError = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasError");
    hasError.SetCondition("FormError <> \"\"");
    
    var errorBox = hasError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ErrorBox");
    errorBox.Gap = null;
    errorBox.SetStyle("\"admin-error\"");
    
    var expression = errorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.SetValue("FormError");
    
    var extendedProperty = errorBox.CreateExtendedProperty();
    extendedProperty.Property = "role";
    extendedProperty.SetValue("\"alert\"");
    
    var projectForm = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectForm");
    projectForm.Gap = null;
    projectForm.SetStyle("\"admin-form\"");
    
    var textWidget3 = projectForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.Text = "Client";
    
    var clientDropdown = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ClientDropdown");
    clientDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    clientDropdown.SetEmptyValue("\"Choose a client\"");
    clientDropdown.SetEnabled("GetProject.IsDataFetched");
    clientDropdown.Gap = null;
    clientDropdown.SetLabels("If(IsActive, Name, Name + \" (inactive)\")");
    clientDropdown.SetList("GetClients.Clients");
    clientDropdown.SetMandatory("True");
    clientDropdown.SetStyle("\"dropdown-display\"");
    clientDropdown.SetValues("Id");
    clientDropdown.SetVariable("EditProject.ClientId");
    var builtinEvent2 = clientDropdown.OnChange;
    
    var extendedProperty2 = clientDropdown.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Client\"");
    
    var textWidget4 = projectForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.Text = "Code";
    
    var codeInput = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("CodeInput");
    codeInput.SetEnabled("GetProject.IsDataFetched");
    codeInput.SetMandatory("True");
    codeInput.MaxLength = 30;
    codeInput.SetVariable("EditProject.Code");
    var builtinEvent3 = codeInput.OnChange;
    
    var extendedProperty3 = codeInput.CreateExtendedProperty();
    extendedProperty3.Property = "maxlength";
    extendedProperty3.SetValue("\"30\"");
    
    var extendedProperty4 = codeInput.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Project code\"");
    
    var textWidget5 = projectForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.Text = "Name";
    
    var nameInput = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("NameInput");
    nameInput.SetEnabled("GetProject.IsDataFetched");
    nameInput.SetMandatory("True");
    nameInput.MaxLength = 100;
    nameInput.SetVariable("EditProject.Name");
    var builtinEvent4 = nameInput.OnChange;
    
    var extendedProperty5 = nameInput.CreateExtendedProperty();
    extendedProperty5.Property = "maxlength";
    extendedProperty5.SetValue("\"100\"");
    
    var extendedProperty6 = nameInput.CreateExtendedProperty();
    extendedProperty6.Property = "aria-label";
    extendedProperty6.SetValue("\"Project name\"");
    
    var statusLine = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("StatusLine");
    statusLine.Gap = null;
    statusLine.SetStyle("\"admin-form__status\"");
    
    var expression2 = statusLine.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("If(ProjectId = NullIdentifier(), \"New project\", If(GetProject.ProjectRecord.IsActive, \"Status: active\", \"Status: inactive\"))");
    
    var formActions = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FormActions");
    formActions.Gap = null;
    formActions.SetStyle("\"admin-form__actions\"");
    
    var saveButton = formActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveButton");
    saveButton.SetEnabled("GetProject.IsDataFetched");
    saveButton.Gap = null;
    saveButton.SetStyle("\"btn btn-primary\"");
    
    var textWidget6 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveButton.Widgets.First();
    textWidget6.Text = "Save";
    var builtinEvent5 = saveButton.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent5.Destination = onClick_SaveButton;
    
    var isExisting = formActions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsExisting");
    isExisting.SetCondition("ProjectId <> NullIdentifier()");
    
    var toggleActiveButton = isExisting.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ToggleActiveButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    toggleActiveButton.Widgets.ToList().ForEach(x => x.Delete());
    toggleActiveButton.SetEnabled("GetProject.IsDataFetched");
    toggleActiveButton.Gap = null;
    
    var expression3 = toggleActiveButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("If(GetProject.ProjectRecord.IsActive, \"Deactivate\", \"Activate\")");
    var builtinEvent6 = toggleActiveButton.OnClick;
    builtinEvent6.Destination = onClick_ToggleActiveButton;
    
    var showAssignments = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("ShowAssignments");
    showAssignments.SetCondition("ProjectId <> NullIdentifier()");
    
    var assignmentsSection = showAssignments.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsSection");
    assignmentsSection.Gap = null;
    assignmentsSection.SetStyle("\"admin-page\"");
    
    var assignmentsHead = assignmentsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsHead");
    assignmentsHead.Gap = null;
    assignmentsHead.SetStyle("\"admin-section__head\"");
    
    var advancedHtml = assignmentsHead.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.Gap = null;
    advancedHtml.Tag = "h2";
    
    var textWidget7 = advancedHtml.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.Text = "Assigned users";
    
    var addAssignmentButton = assignmentsHead.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("AddAssignmentButton");
    addAssignmentButton.SetEnabled("GetProject.IsDataFetched");
    addAssignmentButton.Gap = null;
    addAssignmentButton.SetStyle("\"btn btn-primary\"");
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)addAssignmentButton.Widgets.First();
    textWidget8.Text = "Add user";
    var builtinEvent7 = addAssignmentButton.OnClick;
    builtinEvent7.Destination = onClick_AddAssignmentButton;
    
    var assignmentsTable = assignmentsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsTable");
    assignmentsTable.Gap = null;
    assignmentsTable.SetStyle("\"admin-table admin-table--5\"");
    
    var assignmentsHeader = assignmentsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsHeader");
    assignmentsHeader.Gap = null;
    assignmentsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    
    var container = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"admin-table__cell\"");
    
    var textWidget9 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.Text = "User";
    
    var extendedProperty7 = container.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"columnheader\"");
    
    var container2 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"admin-table__cell\"");
    
    var textWidget10 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.Text = "Start";
    
    var extendedProperty8 = container2.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"columnheader\"");
    
    var container3 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"admin-table__cell\"");
    
    var textWidget11 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget11.Text = "End";
    
    var extendedProperty9 = container3.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"columnheader\"");
    
    var container4 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"admin-table__cell\"");
    
    var textWidget12 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.Text = "Status";
    
    var extendedProperty10 = container4.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"columnheader\"");
    
    var container5 = assignmentsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"admin-table__cell\"");
    
    var textWidget13 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.Text = "Actions";
    
    var extendedProperty11 = container5.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"columnheader\"");
    
    var extendedProperty12 = assignmentsHeader.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"row\"");
    
    var hasAssignments = assignmentsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasAssignments");
    hasAssignments.SetCondition("GetAssignments.Assignments.Length > 0 or not GetAssignments.IsDataFetched");
    
    var assignmentList = hasAssignments.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("AssignmentList");
    assignmentList.Gap = null;
    assignmentList.SetSource("GetAssignments.Assignments");
    assignmentList.SetStyle("\"admin-table__body\"");
    
    var assignmentRow = assignmentList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentRow");
    assignmentRow.Gap = null;
    assignmentRow.SetStyle("\"admin-table__row\"");
    
    var container6 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"admin-table__cell admin-table__name\"");
    
    var expression4 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.SetValue("GetAssignments.Assignments.Current.UserName");
    
    var extendedProperty13 = container6.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    
    var container7 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Gap = null;
    container7.SetStyle("\"admin-table__cell\"");
    
    var expression5 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.SetValue("If(GetAssignments.Assignments.Current.StartDate = NullDate(), \"-\", FormatDateTime(DateToDateTime(GetAssignments.Assignments.Current.StartDate), \"dd/MM/yyyy\"))");
    
    var extendedProperty14 = container7.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"cell\"");
    
    var container8 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Gap = null;
    container8.SetStyle("\"admin-table__cell\"");
    
    var expression6 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.SetValue("If(GetAssignments.Assignments.Current.EndDate = NullDate(), \"No end\", FormatDateTime(DateToDateTime(GetAssignments.Assignments.Current.EndDate), \"dd/MM/yyyy\"))");
    
    var extendedProperty15 = container8.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"cell\"");
    
    var container9 = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Gap = null;
    container9.SetStyle("\"admin-table__cell is-\" + ToLower(GetAssignments.Assignments.Current.Status)");
    
    var expression7 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.SetValue("GetAssignments.Assignments.Current.Status");
    
    var extendedProperty16 = container9.CreateExtendedProperty();
    extendedProperty16.Property = "role";
    extendedProperty16.SetValue("\"cell\"");
    
    var assignmentActions = assignmentRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentActions");
    assignmentActions.Gap = null;
    assignmentActions.SetStyle("\"admin-table__cell admin-table__actions\"");
    
    var editAssignmentLink = assignmentActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("EditAssignmentLink");
    editAssignmentLink.SetEnabled("True");
    editAssignmentLink.Gap = null;
    
    var textWidget14 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)editAssignmentLink.Widgets.First();
    textWidget14.Text = "Edit";
    var builtinEvent8 = editAssignmentLink.OnClick;
    builtinEvent8.Destination = onClick_EditAssignmentLink;
    builtinEvent8.SetArgumentValue(row2, "GetAssignments.Assignments.Current");
    
    var endAssignmentLink = assignmentActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("EndAssignmentLink");
    endAssignmentLink.SetEnabled("True");
    endAssignmentLink.Gap = null;
    
    var textWidget15 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)endAssignmentLink.Widgets.First();
    textWidget15.Text = "End";
    var builtinEvent9 = endAssignmentLink.OnClick;
    builtinEvent9.Destination = onClick_EndAssignmentLink;
    builtinEvent9.SetArgumentValue(row3, "GetAssignments.Assignments.Current");
    
    var deleteAssignmentLink = assignmentActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("DeleteAssignmentLink");
    deleteAssignmentLink.SetEnabled("True");
    deleteAssignmentLink.Gap = null;
    
    var textWidget16 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)deleteAssignmentLink.Widgets.First();
    textWidget16.Text = "Delete";
    var builtinEvent10 = deleteAssignmentLink.OnClick;
    builtinEvent10.Destination = onClick_DeleteAssignmentLink;
    builtinEvent10.SetArgumentValue(row, "GetAssignments.Assignments.Current");
    
    var extendedProperty17 = assignmentActions.CreateExtendedProperty();
    extendedProperty17.Property = "role";
    extendedProperty17.SetValue("\"cell\"");
    
    var extendedProperty18 = assignmentRow.CreateExtendedProperty();
    extendedProperty18.Property = "role";
    extendedProperty18.SetValue("\"row\"");
    var builtinEvent11 = assignmentList.OnScrollEnding;
    
    var noAssignments = hasAssignments.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoAssignments");
    noAssignments.Gap = null;
    noAssignments.SetStyle("\"admin-table__empty\"");
    
    var textWidget17 = noAssignments.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget17.Text = "No users assigned yet.";
    
    var extendedProperty19 = assignmentsTable.CreateExtendedProperty();
    extendedProperty19.Property = "role";
    extendedProperty19.SetValue("\"table\"");
    
    var assignmentPopup = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("AssignmentPopup");
    assignmentPopup.Gap = null;
    assignmentPopup.SetShowPopup("ShowAssignmentPopup");
    
    var assignmentPopupBody = assignmentPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentPopupBody");
    assignmentPopupBody.Gap = null;
    assignmentPopupBody.SetStyle("\"admin-popup\"");
    
    var advancedHtml2 = assignmentPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml2.Gap = null;
    advancedHtml2.Tag = "h3";
    
    var expression8 = advancedHtml2.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.SetValue("If(IsEndMode, \"End assignment\", If(EditAssignment.Id = NullIdentifier(), \"Add user\", \"Edit assignment\"))");
    
    var hasPopupError = assignmentPopupBody.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPopupError");
    hasPopupError.SetCondition("PopupError <> \"\"");
    
    var popupErrorBox = hasPopupError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupErrorBox");
    popupErrorBox.Gap = null;
    popupErrorBox.SetStyle("\"admin-error\"");
    
    var expression9 = popupErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.SetValue("PopupError");
    
    var extendedProperty20 = popupErrorBox.CreateExtendedProperty();
    extendedProperty20.Property = "role";
    extendedProperty20.SetValue("\"alert\"");
    
    var isEndModeIf = assignmentPopupBody.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsEndModeIf");
    isEndModeIf.SetCondition("IsEndMode");
    
    var endFields = isEndModeIf.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EndFields");
    endFields.Gap = null;
    endFields.SetStyle("\"admin-form\"");
    
    var expression10 = endFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.SetValue("PopupUserName + \", from \" + FormatDateTime(DateToDateTime(EditAssignment.StartDate), \"dd/MM/yyyy\")");
    
    var textWidget18 = endFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget18.Text = "End date";
    
    var endOnlyDateInput = endFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("EndOnlyDateInput");
    endOnlyDateInput.SetEnabled("True");
    endOnlyDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    endOnlyDateInput.SetMandatory("False");
    endOnlyDateInput.MaxLength = null;
    endOnlyDateInput.SetVariable("EditAssignment.EndDate");
    var builtinEvent12 = endOnlyDateInput.OnChange;
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty21 = endOnlyDateInput.CreateExtendedProperty();
    extendedProperty21.Property = "aria-label";
    extendedProperty21.SetValue("\"End date\"");
    
    var editFields = isEndModeIf.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EditFields");
    editFields.Gap = null;
    editFields.SetStyle("\"admin-form\"");
    
    var textWidget19 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget19.Text = "User";
    
    var userDropdown = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("UserDropdown");
    userDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    userDropdown.SetEmptyValue("\"Choose a user\"");
    userDropdown.SetEnabled("True");
    userDropdown.Gap = null;
    userDropdown.SetLabels("Name + \" (\" + Username + \")\"");
    userDropdown.SetList("GetUsers.Users");
    userDropdown.SetMandatory("False");
    userDropdown.SetStyle("\"dropdown-display\"");
    userDropdown.SetValues("UserId");
    userDropdown.SetVariable("EditAssignment.UserId");
    var builtinEvent13 = userDropdown.OnChange;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty22 = userDropdown.CreateExtendedProperty();
    extendedProperty22.Property = "aria-label";
    extendedProperty22.SetValue("\"User\"");
    
    var textWidget20 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget20.Text = "Start date";
    
    var startDateInput = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("StartDateInput");
    startDateInput.SetEnabled("True");
    startDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    startDateInput.SetMandatory("True");
    startDateInput.MaxLength = null;
    startDateInput.SetVariable("EditAssignment.StartDate");
    var builtinEvent14 = startDateInput.OnChange;
    builtinEvent14.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty23 = startDateInput.CreateExtendedProperty();
    extendedProperty23.Property = "aria-label";
    extendedProperty23.SetValue("\"Start date\"");
    
    var textWidget21 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget21.Text = "End date";
    
    var endDateInput = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("EndDateInput");
    endDateInput.SetEnabled("True");
    endDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    endDateInput.SetMandatory("False");
    endDateInput.MaxLength = null;
    endDateInput.SetVariable("EditAssignment.EndDate");
    var builtinEvent15 = endDateInput.OnChange;
    builtinEvent15.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty24 = endDateInput.CreateExtendedProperty();
    extendedProperty24.Property = "aria-label";
    extendedProperty24.SetValue("\"End date\"");
    
    var endDateHint = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EndDateHint");
    endDateHint.Gap = null;
    endDateHint.SetStyle("\"admin-form__hint\"");
    
    var textWidget22 = endDateHint.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget22.Text = "Leave the end date empty when the assignment has no end.";
    
    var popupActions = assignmentPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupActions");
    popupActions.Gap = null;
    popupActions.SetStyle("\"admin-form__actions\"");
    
    var saveAssignmentButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveAssignmentButton");
    saveAssignmentButton.SetEnabled("True");
    saveAssignmentButton.Gap = null;
    saveAssignmentButton.SetStyle("\"btn btn-primary\"");
    saveAssignmentButton.SetVisible("not IsEndMode");
    
    var textWidget23 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveAssignmentButton.Widgets.First();
    textWidget23.Text = "Save";
    var builtinEvent16 = saveAssignmentButton.OnClick;
    builtinEvent16.Destination = onClick_SaveAssignmentButton;
    builtinEvent16.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var saveEndButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveEndButton");
    saveEndButton.SetEnabled("True");
    saveEndButton.Gap = null;
    saveEndButton.SetStyle("\"btn btn-primary\"");
    saveEndButton.SetVisible("IsEndMode");
    
    var textWidget24 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveEndButton.Widgets.First();
    textWidget24.Text = "End assignment";
    var builtinEvent17 = saveEndButton.OnClick;
    builtinEvent17.Destination = onClick_SaveEndButton;
    builtinEvent17.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelPopupButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelPopupButton");
    cancelPopupButton.SetEnabled("True");
    cancelPopupButton.Gap = null;
    
    var textWidget25 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)cancelPopupButton.Widgets.First();
    textWidget25.Text = "Cancel";
    var builtinEvent18 = cancelPopupButton.OnClick;
    builtinEvent18.Destination = onClick_CancelPopupButton;
    builtinEvent18.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty25 = assignmentPopupBody.CreateExtendedProperty();
    extendedProperty25.Property = "role";
    extendedProperty25.SetValue("\"dialog\"");
}
```
