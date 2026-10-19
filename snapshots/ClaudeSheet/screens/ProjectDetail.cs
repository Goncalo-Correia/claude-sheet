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
    projectDetail.AnonymousAccess = false; // default value
    projectDetail.CustomURL = false; // default value
    projectDetail.Description = "Create or edit one project (SheetAdmin): client, code, name, activate/deactivate, and its assigned users (add, edit, end, delete in a popup). ProjectId empty = new project.";
    projectDetail.HorizontalPosition = 1600;
    projectDetail.Icon = null; // default value
    projectDetail.IsDisabled = false; // default value
    projectDetail.PageName = null; // default value
    projectDetail.Public = false; // default value
    projectDetail.Shareable = false; // default value
    projectDetail.StyleSheet = "/* Admin screens (US6, US7): toolbar, simple tables, forms, assignment popup. Same rules on every admin screen; move to the theme when more admin screens use them. */\n.admin-page { display: flex; flex-direction: column; gap: 16px; }\n.admin-toolbar { display: flex; flex-wrap: wrap; align-items: center; gap: 12px; }\n.admin-toolbar__filter { flex: 0 1 280px; }\n.admin-toolbar__check { display: flex; align-items: center; gap: 6px; }\n.admin-table { display: flex; flex-direction: column; border: 1px solid var(--color-neutral-4); border-radius: var(--border-radius-soft); overflow: hidden; }\n.admin-table__row { display: grid; grid-template-columns: minmax(0, 2fr) minmax(0, 1fr) minmax(0, 3fr) minmax(0, 1fr) minmax(0, 1fr); gap: 12px; padding: 12px 16px; border-top: 1px solid var(--color-neutral-3); color: inherit; text-decoration: none; align-items: center; }\n.admin-table__head { border-top: 0; background-color: var(--color-neutral-2); font-weight: 600; }\na.admin-table__row:hover { background-color: var(--color-neutral-1); }\n.admin-table__cell { min-width: 0; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }\n.admin-table__cell.is-inactive, .admin-table__cell.is-ended { color: var(--color-neutral-7); }\n.admin-table__actions { display: flex; flex-wrap: wrap; gap: 12px; }\n.admin-table__empty { padding: 16px; color: var(--color-neutral-7); }\n.admin-section__head { display: flex; flex-wrap: wrap; align-items: center; justify-content: space-between; gap: 12px; }\n.admin-form { display: flex; flex-direction: column; gap: 12px; max-width: 480px; }\n.admin-form__actions { display: flex; flex-wrap: wrap; gap: 8px; }\n.admin-form__hint { font-size: var(--font-size-xs); color: var(--color-neutral-7); }\n.admin-popup { display: flex; flex-direction: column; gap: 12px; min-width: min(420px, 90vw); }\n.admin-error { padding: 12px 16px; border-radius: var(--border-radius-soft); background-color: var(--color-error-lightest, #fdecea); color: var(--color-error, #c62828); }";
    projectDetail.SetTitle("\"Project\"");
    projectDetail.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    projectDetail.VerticalPosition = 1400;
    
    var getAssignments = projectDetail.CreateDataAction("GetAssignments");
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
    ifNode.SetCondition("ProjectId = NullIdentifier()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "New project?";
    
    var projectAssignment_GetList = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_GetList").Below(ifNode, 1371);
    var projectAssignment_GetList2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_GetList");
    projectAssignment_GetList.Action = projectAssignment_GetList2;
    projectAssignment_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_GetList.IsDisabled = false; // default value
    projectAssignment_GetList.ServerRequestTimeout = null; // default value
    var projectId = projectAssignment_GetList2.InputParameters.Named("ProjectId");
    projectAssignment_GetList.SetArgumentValue(projectId, "ProjectId");
    var userId = projectAssignment_GetList2.InputParameters.Named("UserId");
    projectAssignment_GetList.SetArgumentValue(userId, "NullIdentifier()");
    ifNode.FalseTarget = projectAssignment_GetList;
    
    var endNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode.IsDisabled = false; // default value
    ifNode.TrueTarget = endNode;
    
    var assignNode = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_GetList, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
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
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode3 = getAssignments.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode3.IsDisabled = false; // default value
    
    var assignments = getAssignments.CreateOutputParameter("Assignments");
    var projectAssignmentSummary = claudeSheet_CS.Structures.Named("ProjectAssignmentSummary");
    assignments.DataType = eSpace.GetOrCreateListType(projectAssignmentSummary);
    assignments.Description = null; // default value
    
    var onAfterFetch_GetAssignments = projectDetail.CreateScreenAction("OnAfterFetch_GetAssignments");
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
    messageNode.Label = null; // default value
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
    
    var getClients = projectDetail.CreateDataAction("GetClients");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getClients.OutputParameters.ToList().ForEach(x => x.Delete());
    getClients.Description = null; // default value
    getClients.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getClients.ServerRequestTimeout = null; // default value
    
    var startNode3 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var client_GetList = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetList").ConnectedBelow(startNode3, 1371);
    var client_GetList2 = claudeSheet_CS.ServerActions.Named("Client_GetList");
    client_GetList.Action = client_GetList2;
    client_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    client_GetList.IsDisabled = false; // default value
    client_GetList.ServerRequestTimeout = null; // default value
    var includeInactive = client_GetList2.InputParameters.Named("IncludeInactive");
    client_GetList.SetArgumentValue(includeInactive, "True");
    var search = client_GetList2.InputParameters.Named("Search");
    client_GetList.SetArgumentValue(search, "\"\"");
    
    var assignNode3 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(client_GetList, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment3 = assignNode3.CreateAssignment("Clients", "Client_GetList.Clients");
    
    var endNode6 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode6.IsDisabled = false; // default value
    
    var allExceptions2 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = true; // default value
    allExceptions2.VerticalPosition = 800;
    
    var assignNode4 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment4 = assignNode4.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode7 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    endNode7.IsDisabled = false; // default value
    getClients.OnAfterFetch.Destination = null; // default value
    
    var clients = getClients.CreateOutputParameter("Clients");
    var clientSummary = claudeSheet_CS.Structures.Named("ClientSummary");
    clients.DataType = eSpace.GetOrCreateListType(clientSummary);
    clients.Description = null; // default value
    
    var errorMessage2 = getClients.CreateOutputParameter("ErrorMessage");
    errorMessage2.DataType = eSpace.TextType;
    errorMessage2.SetDefaultValue(null); // default value
    errorMessage2.Description = null; // default value
    
    var getProject = projectDetail.CreateDataAction("GetProject");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getProject.OutputParameters.ToList().ForEach(x => x.Delete());
    getProject.Description = null; // default value
    getProject.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProject.ServerRequestTimeout = null; // default value
    
    var startNode4 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var ifNode3 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode4, 1371);
    ifNode3.SetCondition("ProjectId = NullIdentifier()");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "New project?";
    
    var project_GetById = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetById").Below(ifNode3, 1371);
    var project_GetById2 = claudeSheet_CS.ServerActions.Named("Project_GetById");
    project_GetById.Action = project_GetById2;
    project_GetById.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetById.IsDisabled = false; // default value
    project_GetById.ServerRequestTimeout = null; // default value
    var projectId2 = project_GetById2.InputParameters.Named("ProjectId");
    project_GetById.SetArgumentValue(projectId2, "ProjectId");
    ifNode3.FalseTarget = project_GetById;
    
    var assignNode5 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 1828);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment5 = assignNode5.CreateAssignment("ProjectRecord.IsActive", "True");
    ifNode3.TrueTarget = assignNode5;
    
    var assignNode6 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetById, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    
    var assignment6 = assignNode6.CreateAssignment("ProjectRecord", "Project_GetById.ProjectRecord");
    
    var endNode8 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    endNode8.IsDisabled = false; // default value
    
    var endNode9 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode6, 1371);
    endNode9.IsDisabled = false; // default value
    
    var allExceptions3 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 8684;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = true; // default value
    allExceptions3.VerticalPosition = 800;
    
    var assignNode7 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    
    var assignment7 = assignNode7.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode10 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode7, 1371);
    endNode10.IsDisabled = false; // default value
    
    var projectRecord = getProject.CreateOutputParameter("ProjectRecord");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    projectRecord.DataType = project;
    projectRecord.Description = null; // default value
    
    var onAfterFetch_GetProject = projectDetail.CreateScreenAction("OnAfterFetch_GetProject");
    onAfterFetch_GetProject.Description = "Copies the fetched project into EditProject; shows an error message when GetProject failed.";
    
    var startNode5 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var assignNode8 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    
    var assignment8 = assignNode8.CreateAssignment("EditProject", "GetProject.ProjectRecord");
    
    var ifNode4 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode8, 1371);
    ifNode4.SetCondition("GetProject.ErrorMessage <> \"\"");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Failed?";
    
    var endNode11 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode4, 1828);
    endNode11.IsDisabled = false; // default value
    
    var messageNode2 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode4, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = null; // default value
    messageNode2.SetMessage("GetProject.ErrorMessage");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode12 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1371);
    endNode12.IsDisabled = false; // default value
    getProject.OnAfterFetch.Destination = onAfterFetch_GetProject;
    ifNode4.FalseTarget = endNode11;
    ifNode4.TrueTarget = messageNode2;
    
    var errorMessage3 = getProject.CreateOutputParameter("ErrorMessage");
    errorMessage3.DataType = eSpace.TextType;
    errorMessage3.SetDefaultValue(null); // default value
    errorMessage3.Description = null; // default value
    
    var getUsers = projectDetail.CreateDataAction("GetUsers");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getUsers.OutputParameters.ToList().ForEach(x => x.Delete());
    getUsers.Description = null; // default value
    getUsers.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getUsers.ServerRequestTimeout = null; // default value
    
    var startNode6 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 800;
    
    var user_GetActive = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetActive").ConnectedBelow(startNode6, 1371);
    var user_GetActive2 = claudeSheet_CS.ServerActions.Named("User_GetActive");
    user_GetActive.Action = user_GetActive2;
    user_GetActive.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_GetActive.IsDisabled = false; // default value
    user_GetActive.ServerRequestTimeout = null; // default value
    
    var assignNode9 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(user_GetActive, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    
    var assignment9 = assignNode9.CreateAssignment("Users", "User_GetActive.Users");
    
    var endNode13 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode9, 1371);
    endNode13.IsDisabled = false; // default value
    
    var allExceptions4 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.IsDisabled = false; // default value
    allExceptions4.LogError = true; // default value
    allExceptions4.VerticalPosition = 800;
    
    var assignNode10 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions4, 1371);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = null; // default value
    
    var assignment10 = assignNode10.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode14 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode10, 1371);
    endNode14.IsDisabled = false; // default value
    getUsers.OnAfterFetch.Destination = null; // default value
    
    var users = getUsers.CreateOutputParameter("Users");
    var userOption = claudeSheet_CS.Structures.Named("UserOption");
    users.DataType = eSpace.GetOrCreateListType(userOption);
    users.Description = null; // default value
    
    var errorMessage4 = getUsers.CreateOutputParameter("ErrorMessage");
    errorMessage4.DataType = eSpace.TextType;
    errorMessage4.SetDefaultValue(null); // default value
    errorMessage4.Description = null; // default value
    
    var projectId3 = projectDetail.CreateInputParameter("ProjectId");
    projectId3.DataType = project.IdentifierType;
    projectId3.SetDefaultValue(null); // default value
    projectId3.Description = null; // default value
    projectId3.IsMandatory = false;
    
    var editProject = projectDetail.CreateLocalVariable("EditProject");
    editProject.DataType = project;
    editProject.Description = "Editable copy of the project, bound to the form (filled by OnAfterFetch_GetProject).";
    
    var formError = projectDetail.CreateLocalVariable("FormError");
    formError.DataType = eSpace.TextType;
    formError.SetDefaultValue(null); // default value
    formError.Description = "Message of the last failed project save or activate/deactivate (e.g. ProjectValidation), shown above the form.";
    
    var editAssignment = projectDetail.CreateLocalVariable("EditAssignment");
    var projectAssignment = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProjectAssignment");
    editAssignment.DataType = projectAssignment;
    editAssignment.Description = "Assignment being added, edited or ended in the popup.";
    
    var showAssignmentPopup = projectDetail.CreateLocalVariable("ShowAssignmentPopup");
    showAssignmentPopup.DataType = eSpace.BooleanType;
    showAssignmentPopup.SetDefaultValue("False");
    showAssignmentPopup.Description = null; // default value
    
    var isEndMode = projectDetail.CreateLocalVariable("IsEndMode");
    isEndMode.DataType = eSpace.BooleanType;
    isEndMode.SetDefaultValue("False");
    isEndMode.Description = "True when the popup only asks for the end date (End, or a refused Delete).";
    
    var popupError = projectDetail.CreateLocalVariable("PopupError");
    popupError.DataType = eSpace.TextType;
    popupError.SetDefaultValue(null); // default value
    popupError.Description = "Message shown in the popup (e.g. AssignmentValidation from a save, or why a Delete was refused).";
    
    var popupUserName = projectDetail.CreateLocalVariable("PopupUserName");
    popupUserName.DataType = eSpace.TextType;
    popupUserName.SetDefaultValue(null); // default value
    popupUserName.Description = "Name of the assignment\'s user, shown in End mode.";
    projectDetail.OnDestroy.Destination = null; // default value
    projectDetail.OnInitialize.Destination = null; // default value
    projectDetail.OnReady.Destination = null; // default value
    projectDetail.OnRender.Destination = null; // default value
    
    var onClick_AddAssignmentButton = projectDetail.CreateScreenAction("OnClick_AddAssignmentButton");
    onClick_AddAssignmentButton.Description = "Opens the popup for a new assignment of this project (start today, no end).";
    
    var startNode7 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.IsDisabled = false; // default value
    startNode7.VerticalPosition = 800;
    
    var assignNode11 = onClick_AddAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = null; // default value
    
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
    endNode15.IsDisabled = false; // default value
    
    var onClick_CancelPopupButton = projectDetail.CreateScreenAction("OnClick_CancelPopupButton");
    onClick_CancelPopupButton.Description = "Closes the assignment popup without saving.";
    
    var startNode8 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.HorizontalPosition = 3200;
    startNode8.IsDisabled = false; // default value
    startNode8.VerticalPosition = 800;
    
    var assignNode12 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode8, 1371);
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = null; // default value
    
    var assignment21 = assignNode12.CreateAssignment("ShowAssignmentPopup", "False");
    
    var endNode16 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode12, 1371);
    endNode16.IsDisabled = false; // default value
    
    var onClick_DeleteAssignmentLink = projectDetail.CreateScreenAction("OnClick_DeleteAssignmentLink");
    onClick_DeleteAssignmentLink.Description = "Deletes an assignment through ProjectAssignment_Delete. When it is refused (the user has time entries), the popup opens in End mode with the reason, offering End instead.";
    
    var row = onClick_DeleteAssignmentLink.CreateInputParameter("Row");
    row.DataType = projectAssignmentSummary;
    row.Description = null; // default value
    row.IsMandatory = true; // default value
    
    var startNode9 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.HorizontalPosition = 3200;
    startNode9.IsDisabled = false; // default value
    startNode9.VerticalPosition = 800;
    
    var projectAssignment_Delete = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_Delete").ConnectedBelow(startNode9, 1371);
    var projectAssignment_Delete2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_Delete");
    projectAssignment_Delete.Action = projectAssignment_Delete2;
    projectAssignment_Delete.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_Delete.IsDisabled = false; // default value
    projectAssignment_Delete.ServerRequestTimeout = null; // default value
    var projectAssignmentId = projectAssignment_Delete2.InputParameters.Named("ProjectAssignmentId");
    projectAssignment_Delete.SetArgumentValue(projectAssignmentId, "Row.Id");
    
    var refreshDataNode = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(projectAssignment_Delete, 1371);
    refreshDataNode.DataSource = getAssignments;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var messageNode3 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode, 1371);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = null; // default value
    messageNode3.SetMessage("\"Assignment deleted.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode17 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    endNode17.IsDisabled = false; // default value
    
    var allExceptions5 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions5.AbortTransaction = false;
    allExceptions5.Exception = eSpace.AllExceptions;
    allExceptions5.HorizontalPosition = 6856;
    allExceptions5.IsDisabled = false; // default value
    allExceptions5.LogError = false;
    allExceptions5.VerticalPosition = 800;
    
    var assignNode13 = onClick_DeleteAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions5, 1371);
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = null; // default value
    
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
    endNode18.IsDisabled = false; // default value
    
    var onClick_EditAssignmentLink = projectDetail.CreateScreenAction("OnClick_EditAssignmentLink");
    onClick_EditAssignmentLink.Description = "Opens the popup to edit an assignment (user and dates).";
    
    var row2 = onClick_EditAssignmentLink.CreateInputParameter("Row");
    row2.DataType = projectAssignmentSummary;
    row2.Description = null; // default value
    row2.IsMandatory = true; // default value
    
    var startNode10 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.IsDisabled = false; // default value
    startNode10.VerticalPosition = 800;
    
    var assignNode14 = onClick_EditAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode10, 1371);
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = null; // default value
    
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
    endNode19.IsDisabled = false; // default value
    
    var onClick_EndAssignmentLink = projectDetail.CreateScreenAction("OnClick_EndAssignmentLink");
    onClick_EndAssignmentLink.Description = "Opens the popup in End mode: only the end date (today when the assignment has no end).";
    
    var row3 = onClick_EndAssignmentLink.CreateInputParameter("Row");
    row3.DataType = projectAssignmentSummary;
    row3.Description = null; // default value
    row3.IsMandatory = true; // default value
    
    var startNode11 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.IsDisabled = false; // default value
    startNode11.VerticalPosition = 800;
    
    var assignNode15 = onClick_EndAssignmentLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode11, 1371);
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = null; // default value
    
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
    endNode20.IsDisabled = false; // default value
    
    var onClick_SaveAssignmentButton = projectDetail.CreateScreenAction("OnClick_SaveAssignmentButton");
    onClick_SaveAssignmentButton.Description = "Saves EditAssignment through ProjectAssignment_CreateOrUpdate, closes the popup and reloads GetAssignments. AssignmentValidation (or any other) error is shown in the popup.";
    
    var startNode12 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.IsDisabled = false; // default value
    startNode12.VerticalPosition = 800;
    
    var assignNode16 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode12, 1371);
    assignNode16.IsDisabled = false; // default value
    assignNode16.Label = null; // default value
    
    var assignment52 = assignNode16.CreateAssignment("PopupError", "\"\"");
    
    var projectAssignment_CreateOrUpdate = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_CreateOrUpdate").ConnectedBelow(assignNode16, 1371);
    var projectAssignment_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_CreateOrUpdate");
    projectAssignment_CreateOrUpdate.Action = projectAssignment_CreateOrUpdate2;
    projectAssignment_CreateOrUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_CreateOrUpdate.IsDisabled = false; // default value
    projectAssignment_CreateOrUpdate.ServerRequestTimeout = null; // default value
    var projectAssignmentRecord = projectAssignment_CreateOrUpdate2.InputParameters.Named("ProjectAssignmentRecord");
    projectAssignment_CreateOrUpdate.SetArgumentValue(projectAssignmentRecord, "EditAssignment");
    
    var assignNode17 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_CreateOrUpdate, 1371);
    assignNode17.IsDisabled = false; // default value
    assignNode17.Label = null; // default value
    
    var assignment53 = assignNode17.CreateAssignment("ShowAssignmentPopup", "False");
    
    var refreshDataNode2 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode17, 1371);
    refreshDataNode2.DataSource = getAssignments;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var messageNode4 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode2, 1371);
    messageNode4.IsDisabled = false; // default value
    messageNode4.Label = null; // default value
    messageNode4.SetMessage("\"Assignment saved.\"");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode21 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1371);
    endNode21.IsDisabled = false; // default value
    
    var allExceptions6 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions6.AbortTransaction = false;
    allExceptions6.Exception = eSpace.AllExceptions;
    allExceptions6.HorizontalPosition = 6856;
    allExceptions6.IsDisabled = false; // default value
    allExceptions6.LogError = false;
    allExceptions6.VerticalPosition = 800;
    
    var assignNode18 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions6, 1371);
    assignNode18.IsDisabled = false; // default value
    assignNode18.Label = null; // default value
    
    var assignment54 = assignNode18.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode22 = onClick_SaveAssignmentButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode18, 1371);
    endNode22.IsDisabled = false; // default value
    
    var onClick_SaveButton = projectDetail.CreateScreenAction("OnClick_SaveButton");
    onClick_SaveButton.Description = "Saves EditProject through Project_CreateOrUpdate. A new project opens its own ProjectDetail (to add users); an existing one reloads GetProject. ProjectValidation (or any other) error is shown above the form.";
    
    var startNode13 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode13.HorizontalPosition = 3200;
    startNode13.IsDisabled = false; // default value
    startNode13.VerticalPosition = 800;
    
    var assignNode19 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode13, 1371);
    assignNode19.IsDisabled = false; // default value
    assignNode19.Label = null; // default value
    
    var assignment55 = assignNode19.CreateAssignment("FormError", "\"\"");
    
    var project_CreateOrUpdate = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_CreateOrUpdate").ConnectedBelow(assignNode19, 1371);
    var project_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("Project_CreateOrUpdate");
    project_CreateOrUpdate.Action = project_CreateOrUpdate2;
    project_CreateOrUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_CreateOrUpdate.IsDisabled = false; // default value
    project_CreateOrUpdate.ServerRequestTimeout = null; // default value
    var projectRecord2 = project_CreateOrUpdate2.InputParameters.Named("ProjectRecord");
    project_CreateOrUpdate.SetArgumentValue(projectRecord2, "EditProject");
    
    var messageNode5 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(project_CreateOrUpdate, 1371);
    messageNode5.IsDisabled = false; // default value
    messageNode5.Label = null; // default value
    messageNode5.SetMessage("\"Project saved.\"");
    messageNode5.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var ifNode5 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(messageNode5, 1371);
    ifNode5.SetCondition("ProjectId = NullIdentifier()");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "New project?";
    
    var refreshDataNode3 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().Below(ifNode5, 1371);
    refreshDataNode3.DataSource = getProject;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    ifNode5.FalseTarget = refreshDataNode3;
    
    var destinationNode = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode5, 1828);
    destinationNode.Destination = projectDetail;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    destinationNode.SetArgumentValue(projectId3, "Project_CreateOrUpdate.ProjectId");
    ifNode5.TrueTarget = destinationNode;
    
    var endNode23 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode3, 1371);
    endNode23.IsDisabled = false; // default value
    
    var allExceptions7 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions7.AbortTransaction = false;
    allExceptions7.Exception = eSpace.AllExceptions;
    allExceptions7.HorizontalPosition = 8684;
    allExceptions7.IsDisabled = false; // default value
    allExceptions7.LogError = false;
    allExceptions7.VerticalPosition = 800;
    
    var assignNode20 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions7, 1371);
    assignNode20.IsDisabled = false; // default value
    assignNode20.Label = null; // default value
    
    var assignment56 = assignNode20.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode24 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode20, 1371);
    endNode24.IsDisabled = false; // default value
    
    var onClick_SaveEndButton = projectDetail.CreateScreenAction("OnClick_SaveEndButton");
    onClick_SaveEndButton.Description = "Ends the assignment on EditAssignment.EndDate through ProjectAssignment_End, closes the popup and reloads GetAssignments. Errors are shown in the popup.";
    
    var startNode14 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode14.HorizontalPosition = 3200;
    startNode14.IsDisabled = false; // default value
    startNode14.VerticalPosition = 800;
    
    var assignNode21 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode14, 1371);
    assignNode21.IsDisabled = false; // default value
    assignNode21.Label = null; // default value
    
    var assignment57 = assignNode21.CreateAssignment("PopupError", "\"\"");
    
    var projectAssignment_End = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectAssignment_End").ConnectedBelow(assignNode21, 1371);
    var projectAssignment_End2 = claudeSheet_CS.ServerActions.Named("ProjectAssignment_End");
    projectAssignment_End.Action = projectAssignment_End2;
    projectAssignment_End.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectAssignment_End.IsDisabled = false; // default value
    projectAssignment_End.ServerRequestTimeout = null; // default value
    var projectAssignmentId2 = projectAssignment_End2.InputParameters.Named("ProjectAssignmentId");
    projectAssignment_End.SetArgumentValue(projectAssignmentId2, "EditAssignment.Id");
    var endDate = projectAssignment_End2.InputParameters.Named("EndDate");
    projectAssignment_End.SetArgumentValue(endDate, "EditAssignment.EndDate");
    
    var assignNode22 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectAssignment_End, 1371);
    assignNode22.IsDisabled = false; // default value
    assignNode22.Label = null; // default value
    
    var assignment58 = assignNode22.CreateAssignment("ShowAssignmentPopup", "False");
    
    var refreshDataNode4 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode22, 1371);
    refreshDataNode4.DataSource = getAssignments;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords(null); // default value
    refreshDataNode4.SetStartIndex(null); // default value
    
    var messageNode6 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1371);
    messageNode6.IsDisabled = false; // default value
    messageNode6.Label = null; // default value
    messageNode6.SetMessage("\"Assignment ended.\"");
    messageNode6.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode25 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode6, 1371);
    endNode25.IsDisabled = false; // default value
    
    var allExceptions8 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions8.AbortTransaction = false;
    allExceptions8.Exception = eSpace.AllExceptions;
    allExceptions8.HorizontalPosition = 6856;
    allExceptions8.IsDisabled = false; // default value
    allExceptions8.LogError = false;
    allExceptions8.VerticalPosition = 800;
    
    var assignNode23 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions8, 1371);
    assignNode23.IsDisabled = false; // default value
    assignNode23.Label = null; // default value
    
    var assignment59 = assignNode23.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode26 = onClick_SaveEndButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode23, 1371);
    endNode26.IsDisabled = false; // default value
    
    var onClick_ToggleActiveButton = projectDetail.CreateScreenAction("OnClick_ToggleActiveButton");
    onClick_ToggleActiveButton.Description = "Activates or deactivates the project through Project_SetActive and reloads GetProject.";
    
    var startNode15 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode15.HorizontalPosition = 3200;
    startNode15.IsDisabled = false; // default value
    startNode15.VerticalPosition = 800;
    
    var assignNode24 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode15, 1371);
    assignNode24.IsDisabled = false; // default value
    assignNode24.Label = null; // default value
    
    var assignment60 = assignNode24.CreateAssignment("FormError", "\"\"");
    
    var project_SetActive = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_SetActive").ConnectedBelow(assignNode24, 1371);
    var project_SetActive2 = claudeSheet_CS.ServerActions.Named("Project_SetActive");
    project_SetActive.Action = project_SetActive2;
    project_SetActive.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_SetActive.IsDisabled = false; // default value
    project_SetActive.ServerRequestTimeout = null; // default value
    var projectId4 = project_SetActive2.InputParameters.Named("ProjectId");
    project_SetActive.SetArgumentValue(projectId4, "ProjectId");
    var isActive = project_SetActive2.InputParameters.Named("IsActive");
    project_SetActive.SetArgumentValue(isActive, "not GetProject.ProjectRecord.IsActive");
    
    var refreshDataNode5 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(project_SetActive, 1371);
    refreshDataNode5.DataSource = getProject;
    refreshDataNode5.IsDisabled = false; // default value
    refreshDataNode5.SetMaxRecords(null); // default value
    refreshDataNode5.SetStartIndex(null); // default value
    
    var messageNode7 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode5, 1371);
    messageNode7.IsDisabled = false; // default value
    messageNode7.Label = null; // default value
    messageNode7.SetMessage("If(GetProject.ProjectRecord.IsActive, \"Project activated.\", \"Project deactivated.\")");
    messageNode7.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode27 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode7, 1371);
    endNode27.IsDisabled = false; // default value
    
    var allExceptions9 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions9.AbortTransaction = false;
    allExceptions9.Exception = eSpace.AllExceptions;
    allExceptions9.HorizontalPosition = 6856;
    allExceptions9.IsDisabled = false; // default value
    allExceptions9.LogError = false;
    allExceptions9.VerticalPosition = 800;
    
    var assignNode25 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions9, 1371);
    assignNode25.IsDisabled = false; // default value
    assignNode25.Label = null; // default value
    
    var assignment61 = assignNode25.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode28 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode25, 1371);
    endNode28.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = projectDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopMenuInstance.CustomStyle = null; // default value
    layoutTopMenuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    layoutTopMenuInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "2");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Project";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var adminPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    adminPage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    adminPage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    adminPage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    adminPage.Animate = false; // default value
    adminPage.ColumnGap = null; // default value
    adminPage.ColumnSpan = null; // default value
    adminPage.CustomStyle = null; // default value
    adminPage.FlexBasis = null; // default value
    adminPage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    adminPage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    adminPage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    adminPage.Gap = null;
    adminPage.GridColumns = null; // default value
    adminPage.GridRows = null; // default value
    adminPage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    adminPage.RowGap = null; // default value
    adminPage.RowSpan = null; // default value
    adminPage.SetStyle("\"admin-page\"");
    adminPage.SetVisible("True"); // default value
    adminPage.VisibleWasExpression = false; // default value
    adminPage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    adminPage.Width = "(fill parent)"; // default value
    
    var backLink = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("BackLink");
    backLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    backLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    backLink.ColumnGap = null; // default value
    backLink.ColumnSpan = null; // default value
    backLink.SetConfirmationMessage(null); // default value
    backLink.CustomStyle = null; // default value
    backLink.SetEnabled("True");
    backLink.FlexBasis = null; // default value
    backLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    backLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    backLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    backLink.Gap = null;
    backLink.GridColumns = null; // default value
    backLink.GridRows = null; // default value
    backLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    backLink.RowGap = null; // default value
    backLink.RowSpan = null; // default value
    backLink.SetStyle(null); // default value
    backLink.SetVisible("True"); // default value
    backLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    backLink.Width = ""; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)backLink.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Back to projects";
    var builtinEvent = backLink.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var projects = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Projects");
    builtinEvent.Destination = projects;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var hasError = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasError");
    hasError.Animate = false; // default value
    hasError.SetCondition("FormError <> \"\"");
    hasError.CustomStyle = null; // default value
    hasError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var errorBox = hasError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ErrorBox");
    errorBox.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    errorBox.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    errorBox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    errorBox.Animate = false; // default value
    errorBox.ColumnGap = null; // default value
    errorBox.ColumnSpan = null; // default value
    errorBox.CustomStyle = null; // default value
    errorBox.FlexBasis = null; // default value
    errorBox.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    errorBox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    errorBox.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    errorBox.Gap = null;
    errorBox.GridColumns = null; // default value
    errorBox.GridRows = null; // default value
    errorBox.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    errorBox.RowGap = null; // default value
    errorBox.RowSpan = null; // default value
    errorBox.SetStyle("\"admin-error\"");
    errorBox.SetVisible("True"); // default value
    errorBox.VisibleWasExpression = false; // default value
    errorBox.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    errorBox.Width = "(fill parent)"; // default value
    
    var expression = errorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("FormError");
    expression.Width = ""; // default value
    
    var extendedProperty = errorBox.CreateExtendedProperty();
    extendedProperty.Property = "role";
    extendedProperty.SetValue("\"alert\"");
    
    var projectForm = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectForm");
    projectForm.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    projectForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectForm.Animate = false; // default value
    projectForm.ColumnGap = null; // default value
    projectForm.ColumnSpan = null; // default value
    projectForm.CustomStyle = null; // default value
    projectForm.FlexBasis = null; // default value
    projectForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectForm.Gap = null;
    projectForm.GridColumns = null; // default value
    projectForm.GridRows = null; // default value
    projectForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectForm.RowGap = null; // default value
    projectForm.RowSpan = null; // default value
    projectForm.SetStyle("\"admin-form\"");
    projectForm.SetVisible("True"); // default value
    projectForm.VisibleWasExpression = false; // default value
    projectForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectForm.Width = "(fill parent)"; // default value
    
    var textWidget3 = projectForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Client";
    
    var clientDropdown = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ClientDropdown");
    clientDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientDropdown.ColumnGap = null; // default value
    clientDropdown.ColumnSpan = null; // default value
    clientDropdown.CustomStyle = null; // default value
    clientDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    clientDropdown.SetEmptyValue("\"Choose a client\"");
    clientDropdown.SetEnabled("GetProject.IsDataFetched");
    clientDropdown.FlexBasis = null; // default value
    clientDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientDropdown.Gap = null;
    clientDropdown.GridColumns = null; // default value
    clientDropdown.GridRows = null; // default value
    clientDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientDropdown.SetLabels("If(IsActive, Name, Name + \" (inactive)\")");
    clientDropdown.SetList("GetClients.Clients");
    clientDropdown.SetMandatory("True");
    clientDropdown.RowGap = null; // default value
    clientDropdown.RowSpan = null; // default value
    clientDropdown.SetStyle("\"dropdown-display\"");
    clientDropdown.SetValues("Id");
    clientDropdown.SetVariable("EditProject.ClientId");
    clientDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientDropdown.Width = ""; // default value
    var builtinEvent2 = clientDropdown.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = null; // default value
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = clientDropdown.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Client\"");
    
    var textWidget4 = projectForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Code";
    
    var codeInput = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("CodeInput");
    codeInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    codeInput.ColumnSpan = null; // default value
    codeInput.CustomStyle = null; // default value
    codeInput.SetEnabled("GetProject.IsDataFetched");
    codeInput.FlexBasis = null; // default value
    codeInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    codeInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    codeInput.SetMandatory("True");
    codeInput.MaxLength = 30;
    codeInput.SetPrompt(null); // default value
    codeInput.RowSpan = null; // default value
    codeInput.SetStyle("\"form-control\""); // default value
    codeInput.SetVariable("EditProject.Code");
    codeInput.Width = "(fill parent)"; // default value
    var builtinEvent3 = codeInput.OnChange;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = null; // default value
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty3 = codeInput.CreateExtendedProperty();
    extendedProperty3.Property = "maxlength";
    extendedProperty3.SetValue("\"30\"");
    
    var extendedProperty4 = codeInput.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Project code\"");
    
    var textWidget5 = projectForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Name";
    
    var nameInput = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("NameInput");
    nameInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    nameInput.ColumnSpan = null; // default value
    nameInput.CustomStyle = null; // default value
    nameInput.SetEnabled("GetProject.IsDataFetched");
    nameInput.FlexBasis = null; // default value
    nameInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    nameInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    nameInput.SetMandatory("True");
    nameInput.MaxLength = 100;
    nameInput.SetPrompt(null); // default value
    nameInput.RowSpan = null; // default value
    nameInput.SetStyle("\"form-control\""); // default value
    nameInput.SetVariable("EditProject.Name");
    nameInput.Width = "(fill parent)"; // default value
    var builtinEvent4 = nameInput.OnChange;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = null; // default value
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty5 = nameInput.CreateExtendedProperty();
    extendedProperty5.Property = "maxlength";
    extendedProperty5.SetValue("\"100\"");
    
    var extendedProperty6 = nameInput.CreateExtendedProperty();
    extendedProperty6.Property = "aria-label";
    extendedProperty6.SetValue("\"Project name\"");
    
    var statusLine = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("StatusLine");
    statusLine.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    statusLine.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    statusLine.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    statusLine.Animate = false; // default value
    statusLine.ColumnGap = null; // default value
    statusLine.ColumnSpan = null; // default value
    statusLine.CustomStyle = null; // default value
    statusLine.FlexBasis = null; // default value
    statusLine.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    statusLine.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    statusLine.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    statusLine.Gap = null;
    statusLine.GridColumns = null; // default value
    statusLine.GridRows = null; // default value
    statusLine.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    statusLine.RowGap = null; // default value
    statusLine.RowSpan = null; // default value
    statusLine.SetStyle("\"admin-form__status\"");
    statusLine.SetVisible("True"); // default value
    statusLine.VisibleWasExpression = false; // default value
    statusLine.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    statusLine.Width = "(fill parent)"; // default value
    
    var expression2 = statusLine.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("If(ProjectId = NullIdentifier(), \"New project\", If(GetProject.ProjectRecord.IsActive, \"Status: active\", \"Status: inactive\"))");
    expression2.Width = ""; // default value
    
    var formActions = projectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FormActions");
    formActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    formActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    formActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    formActions.Animate = false; // default value
    formActions.ColumnGap = null; // default value
    formActions.ColumnSpan = null; // default value
    formActions.CustomStyle = null; // default value
    formActions.FlexBasis = null; // default value
    formActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    formActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    formActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    formActions.Gap = null;
    formActions.GridColumns = null; // default value
    formActions.GridRows = null; // default value
    formActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    formActions.RowGap = null; // default value
    formActions.RowSpan = null; // default value
    formActions.SetStyle("\"admin-form__actions\"");
    formActions.SetVisible("True"); // default value
    formActions.VisibleWasExpression = false; // default value
    formActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    formActions.Width = "(fill parent)"; // default value
    
    var saveButton = formActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveButton");
    saveButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    saveButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    saveButton.ColumnGap = null; // default value
    saveButton.ColumnSpan = null; // default value
    saveButton.SetConfirmationMessage(null); // default value
    saveButton.CustomStyle = null; // default value
    saveButton.SetEnabled("GetProject.IsDataFetched");
    saveButton.FlexBasis = null; // default value
    saveButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    saveButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    saveButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    saveButton.Gap = null;
    saveButton.GridColumns = null; // default value
    saveButton.GridRows = null; // default value
    saveButton.IsDefault = false; // default value
    saveButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    saveButton.RowGap = null; // default value
    saveButton.RowSpan = null; // default value
    saveButton.SetStyle("\"btn btn-primary\"");
    saveButton.SetVisible("True"); // default value
    saveButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveButton.Width = ""; // default value
    
    var textWidget6 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveButton.Widgets.First();
    textWidget6.CustomStyle = null; // default value
    textWidget6.Name = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Save";
    var builtinEvent5 = saveButton.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent5.Destination = onClick_SaveButton;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var isExisting = formActions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsExisting");
    isExisting.Animate = false; // default value
    isExisting.SetCondition("ProjectId <> NullIdentifier()");
    isExisting.CustomStyle = null; // default value
    isExisting.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var toggleActiveButton = isExisting.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ToggleActiveButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    toggleActiveButton.Widgets.ToList().ForEach(x => x.Delete());
    toggleActiveButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    toggleActiveButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    toggleActiveButton.ColumnGap = null; // default value
    toggleActiveButton.ColumnSpan = null; // default value
    toggleActiveButton.SetConfirmationMessage(null); // default value
    toggleActiveButton.CustomStyle = null; // default value
    toggleActiveButton.SetEnabled("GetProject.IsDataFetched");
    toggleActiveButton.FlexBasis = null; // default value
    toggleActiveButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    toggleActiveButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    toggleActiveButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    toggleActiveButton.Gap = null;
    toggleActiveButton.GridColumns = null; // default value
    toggleActiveButton.GridRows = null; // default value
    toggleActiveButton.IsDefault = false; // default value
    toggleActiveButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    toggleActiveButton.RowGap = null; // default value
    toggleActiveButton.RowSpan = null; // default value
    toggleActiveButton.SetStyle("\"btn\""); // default value
    toggleActiveButton.SetVisible("True"); // default value
    toggleActiveButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    toggleActiveButton.Width = ""; // default value
    
    var expression3 = toggleActiveButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("If(GetProject.ProjectRecord.IsActive, \"Deactivate\", \"Activate\")");
    expression3.Width = ""; // default value
    var builtinEvent6 = toggleActiveButton.OnClick;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = onClick_ToggleActiveButton;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var showAssignments = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("ShowAssignments");
    showAssignments.Animate = false; // default value
    showAssignments.SetCondition("ProjectId <> NullIdentifier()");
    showAssignments.CustomStyle = null; // default value
    showAssignments.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var assignmentsSection = showAssignments.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AssignmentsSection");
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
    
    var textWidget7 = advancedHtml.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Assigned users";
    
    var addAssignmentButton = assignmentsHead.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("AddAssignmentButton");
    addAssignmentButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    addAssignmentButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    addAssignmentButton.ColumnGap = null; // default value
    addAssignmentButton.ColumnSpan = null; // default value
    addAssignmentButton.SetConfirmationMessage(null); // default value
    addAssignmentButton.CustomStyle = null; // default value
    addAssignmentButton.SetEnabled("GetProject.IsDataFetched");
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
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)addAssignmentButton.Widgets.First();
    textWidget8.CustomStyle = null; // default value
    textWidget8.Name = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Add user";
    var builtinEvent7 = addAssignmentButton.OnClick;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = onClick_AddAssignmentButton;
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
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
    assignmentsTable.SetStyle("\"admin-table\"");
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
    
    var textWidget9 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.CustomStyle = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "User";
    
    var extendedProperty7 = container.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"columnheader\"");
    
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
    
    var textWidget10 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.CustomStyle = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Start";
    
    var extendedProperty8 = container2.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"columnheader\"");
    
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
    
    var textWidget11 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget11.CustomStyle = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "End";
    
    var extendedProperty9 = container3.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"columnheader\"");
    
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
    
    var textWidget12 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.CustomStyle = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Status";
    
    var extendedProperty10 = container4.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"columnheader\"");
    
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
    
    var textWidget13 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.CustomStyle = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Actions";
    
    var extendedProperty11 = container5.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"columnheader\"");
    
    var extendedProperty12 = assignmentsHeader.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"row\"");
    
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
    
    var expression4 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetAssignments.Assignments.Current.UserName");
    expression4.Width = ""; // default value
    
    var extendedProperty13 = container6.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    
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
    
    var expression5 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("If(GetAssignments.Assignments.Current.StartDate = NullDate(), \"-\", FormatDateTime(DateToDateTime(GetAssignments.Assignments.Current.StartDate), \"dd/MM/yyyy\"))");
    expression5.Width = ""; // default value
    
    var extendedProperty14 = container7.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"cell\"");
    
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
    
    var expression6 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("If(GetAssignments.Assignments.Current.EndDate = NullDate(), \"No end\", FormatDateTime(DateToDateTime(GetAssignments.Assignments.Current.EndDate), \"dd/MM/yyyy\"))");
    expression6.Width = ""; // default value
    
    var extendedProperty15 = container8.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"cell\"");
    
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
    
    var expression7 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("GetAssignments.Assignments.Current.Status");
    expression7.Width = ""; // default value
    
    var extendedProperty16 = container9.CreateExtendedProperty();
    extendedProperty16.Property = "role";
    extendedProperty16.SetValue("\"cell\"");
    
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
    
    var textWidget14 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)editAssignmentLink.Widgets.First();
    textWidget14.CustomStyle = null; // default value
    textWidget14.Name = null; // default value
    textWidget14.SetStyleClasses(null); // default value
    textWidget14.Text = "Edit";
    var builtinEvent8 = editAssignmentLink.OnClick;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = onClick_EditAssignmentLink;
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent8.SetArgumentValue(row2, "GetAssignments.Assignments.Current");
    
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
    
    var textWidget15 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)endAssignmentLink.Widgets.First();
    textWidget15.CustomStyle = null; // default value
    textWidget15.Name = null; // default value
    textWidget15.SetStyleClasses(null); // default value
    textWidget15.Text = "End";
    var builtinEvent9 = endAssignmentLink.OnClick;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = onClick_EndAssignmentLink;
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent9.SetArgumentValue(row3, "GetAssignments.Assignments.Current");
    
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
    
    var textWidget16 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)deleteAssignmentLink.Widgets.First();
    textWidget16.CustomStyle = null; // default value
    textWidget16.Name = null; // default value
    textWidget16.SetStyleClasses(null); // default value
    textWidget16.Text = "Delete";
    var builtinEvent10 = deleteAssignmentLink.OnClick;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = onClick_DeleteAssignmentLink;
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent10.SetArgumentValue(row, "GetAssignments.Assignments.Current");
    
    var extendedProperty17 = assignmentActions.CreateExtendedProperty();
    extendedProperty17.Property = "role";
    extendedProperty17.SetValue("\"cell\"");
    
    var extendedProperty18 = assignmentRow.CreateExtendedProperty();
    extendedProperty18.Property = "role";
    extendedProperty18.SetValue("\"row\"");
    var builtinEvent11 = assignmentList.OnScrollEnding;
    builtinEvent11.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent11.Destination = null; // default value
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
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
    
    var textWidget17 = noAssignments.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget17.CustomStyle = null; // default value
    textWidget17.SetStyleClasses(null); // default value
    textWidget17.Text = "No users assigned yet.";
    
    var extendedProperty19 = assignmentsTable.CreateExtendedProperty();
    extendedProperty19.Property = "role";
    extendedProperty19.SetValue("\"table\"");
    
    var assignmentPopup = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("AssignmentPopup");
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
    
    var expression8 = advancedHtml2.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("If(IsEndMode, \"End assignment\", If(EditAssignment.Id = NullIdentifier(), \"Add user\", \"Edit assignment\"))");
    expression8.Width = ""; // default value
    
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
    
    var expression9 = popupErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("PopupError");
    expression9.Width = ""; // default value
    
    var extendedProperty20 = popupErrorBox.CreateExtendedProperty();
    extendedProperty20.Property = "role";
    extendedProperty20.SetValue("\"alert\"");
    
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
    
    var expression10 = endFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("PopupUserName + \", from \" + FormatDateTime(DateToDateTime(EditAssignment.StartDate), \"dd/MM/yyyy\")");
    expression10.Width = ""; // default value
    
    var textWidget18 = endFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget18.CustomStyle = null; // default value
    textWidget18.SetStyleClasses(null); // default value
    textWidget18.Text = "End date";
    
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
    var builtinEvent12 = endOnlyDateInput.OnChange;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent12.Destination = null; // default value
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty21 = endOnlyDateInput.CreateExtendedProperty();
    extendedProperty21.Property = "aria-label";
    extendedProperty21.SetValue("\"End date\"");
    
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
    
    var textWidget19 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget19.CustomStyle = null; // default value
    textWidget19.SetStyleClasses(null); // default value
    textWidget19.Text = "User";
    
    var userDropdown = editFields.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("UserDropdown");
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
    userDropdown.SetMandatory("False");
    userDropdown.RowGap = null; // default value
    userDropdown.RowSpan = null; // default value
    userDropdown.SetStyle("\"dropdown-display\"");
    userDropdown.SetValues("UserId");
    userDropdown.SetVariable("EditAssignment.UserId");
    userDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    userDropdown.Width = ""; // default value
    var builtinEvent13 = userDropdown.OnChange;
    builtinEvent13.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent13.Destination = null; // default value
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty22 = userDropdown.CreateExtendedProperty();
    extendedProperty22.Property = "aria-label";
    extendedProperty22.SetValue("\"User\"");
    
    var textWidget20 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget20.CustomStyle = null; // default value
    textWidget20.SetStyleClasses(null); // default value
    textWidget20.Text = "Start date";
    
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
    var builtinEvent14 = startDateInput.OnChange;
    builtinEvent14.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent14.Destination = null; // default value
    builtinEvent14.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty23 = startDateInput.CreateExtendedProperty();
    extendedProperty23.Property = "aria-label";
    extendedProperty23.SetValue("\"Start date\"");
    
    var textWidget21 = editFields.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget21.CustomStyle = null; // default value
    textWidget21.SetStyleClasses(null); // default value
    textWidget21.Text = "End date";
    
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
    var builtinEvent15 = endDateInput.OnChange;
    builtinEvent15.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent15.Destination = null; // default value
    builtinEvent15.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty24 = endDateInput.CreateExtendedProperty();
    extendedProperty24.Property = "aria-label";
    extendedProperty24.SetValue("\"End date\"");
    
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
    
    var textWidget22 = endDateHint.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget22.CustomStyle = null; // default value
    textWidget22.SetStyleClasses(null); // default value
    textWidget22.Text = "Leave the end date empty when the assignment has no end.";
    
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
    
    var textWidget23 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveAssignmentButton.Widgets.First();
    textWidget23.CustomStyle = null; // default value
    textWidget23.Name = null; // default value
    textWidget23.SetStyleClasses(null); // default value
    textWidget23.Text = "Save";
    var builtinEvent16 = saveAssignmentButton.OnClick;
    builtinEvent16.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent16.Destination = onClick_SaveAssignmentButton;
    builtinEvent16.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
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
    
    var textWidget24 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveEndButton.Widgets.First();
    textWidget24.CustomStyle = null; // default value
    textWidget24.Name = null; // default value
    textWidget24.SetStyleClasses(null); // default value
    textWidget24.Text = "End assignment";
    var builtinEvent17 = saveEndButton.OnClick;
    builtinEvent17.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent17.Destination = onClick_SaveEndButton;
    builtinEvent17.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
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
    
    var textWidget25 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)cancelPopupButton.Widgets.First();
    textWidget25.CustomStyle = null; // default value
    textWidget25.Name = null; // default value
    textWidget25.SetStyleClasses(null); // default value
    textWidget25.Text = "Cancel";
    var builtinEvent18 = cancelPopupButton.OnClick;
    builtinEvent18.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent18.Destination = onClick_CancelPopupButton;
    builtinEvent18.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty25 = assignmentPopupBody.CreateExtendedProperty();
    extendedProperty25.Property = "role";
    extendedProperty25.SetValue("\"dialog\"");
}
