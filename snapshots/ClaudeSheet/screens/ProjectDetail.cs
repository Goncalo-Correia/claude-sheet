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
    projectDetail.StyleSheet = null; // default value
    projectDetail.SetTitle("\"Project\"");
    projectDetail.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    projectDetail.VerticalPosition = 1400;
    
    var getClients = projectDetail.CreateDataAction("GetClients");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getClients.OutputParameters.ToList().ForEach(x => x.Delete());
    getClients.Description = null; // default value
    getClients.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getClients.ServerRequestTimeout = null; // default value
    
    var startNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var client_GetList = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetList").ConnectedBelow(startNode, 1371);
    var client_GetList2 = claudeSheet_CS.ServerActions.Named("Client_GetList");
    client_GetList.Action = client_GetList2;
    client_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    client_GetList.IsDisabled = false; // default value
    client_GetList.ServerRequestTimeout = null; // default value
    var includeInactive = client_GetList2.InputParameters.Named("IncludeInactive");
    client_GetList.SetArgumentValue(includeInactive, "True");
    var search = client_GetList2.InputParameters.Named("Search");
    client_GetList.SetArgumentValue(search, "\"\"");
    
    var assignNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(client_GetList, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set clients";
    
    var assignment = assignNode.CreateAssignment("Clients", "Client_GetList.Clients");
    
    var endNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var allExceptions = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    getClients.OnAfterFetch.Destination = null; // default value
    
    var clients = getClients.CreateOutputParameter("Clients");
    var clientSummary = claudeSheet_CS.Structures.Named("ClientSummary");
    clients.DataType = eSpace.GetOrCreateListType(clientSummary);
    clients.Description = null; // default value
    
    var errorMessage = getClients.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var getProject = projectDetail.CreateDataAction("GetProject");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getProject.OutputParameters.ToList().ForEach(x => x.Delete());
    getProject.Description = null; // default value
    getProject.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProject.ServerRequestTimeout = null; // default value
    
    var startNode2 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("ProjectId = NullIdentifier()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "New project?";
    
    var project_GetById = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetById").Below(ifNode, 1371);
    var project_GetById2 = claudeSheet_CS.ServerActions.Named("Project_GetById");
    project_GetById.Action = project_GetById2;
    project_GetById.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetById.IsDisabled = false; // default value
    project_GetById.ServerRequestTimeout = null; // default value
    var projectId = project_GetById2.InputParameters.Named("ProjectId");
    project_GetById.SetArgumentValue(projectId, "ProjectId");
    ifNode.FalseTarget = project_GetById;
    
    var assignNode3 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "New project defaults";
    
    var assignment3 = assignNode3.CreateAssignment("ProjectRecord.IsActive", "True");
    ifNode.TrueTarget = assignNode3;
    
    var assignNode4 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetById, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Set project";
    
    var assignment4 = assignNode4.CreateAssignment("ProjectRecord", "Project_GetById.ProjectRecord");
    
    var endNode3 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode3.IsDisabled = false; // default value
    
    var endNode4 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    endNode4.IsDisabled = false; // default value
    
    var allExceptions2 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 8684;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = true; // default value
    allExceptions2.VerticalPosition = 800;
    
    var assignNode5 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Keep error";
    
    var assignment5 = assignNode5.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode5 = getProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    endNode5.IsDisabled = false; // default value
    
    var projectRecord = getProject.CreateOutputParameter("ProjectRecord");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    projectRecord.DataType = project;
    projectRecord.Description = null; // default value
    
    var onAfterFetch_GetProject = projectDetail.CreateScreenAction("OnAfterFetch_GetProject");
    onAfterFetch_GetProject.Description = "Copies the fetched project into EditProject; shows an error message when GetProject failed.";
    
    var startNode3 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var assignNode6 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Copy to form";
    
    var assignment6 = assignNode6.CreateAssignment("EditProject", "GetProject.ProjectRecord");
    
    var ifNode2 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode6, 1371);
    ifNode2.SetCondition("GetProject.ErrorMessage <> \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Failed?";
    
    var endNode6 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    endNode6.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode2, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetProject.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode7 = onAfterFetch_GetProject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode7.IsDisabled = false; // default value
    getProject.OnAfterFetch.Destination = onAfterFetch_GetProject;
    ifNode2.FalseTarget = endNode6;
    ifNode2.TrueTarget = messageNode;
    
    var errorMessage2 = getProject.CreateOutputParameter("ErrorMessage");
    errorMessage2.DataType = eSpace.TextType;
    errorMessage2.SetDefaultValue(null); // default value
    errorMessage2.Description = null; // default value
    
    var projectId2 = projectDetail.CreateInputParameter("ProjectId");
    projectId2.DataType = project.IdentifierType;
    projectId2.SetDefaultValue(null); // default value
    projectId2.Description = null; // default value
    projectId2.IsMandatory = false;
    
    var editProject = projectDetail.CreateLocalVariable("EditProject");
    editProject.DataType = project;
    editProject.Description = "Editable copy of the project, bound to the form (filled by OnAfterFetch_GetProject).";
    
    var formError = projectDetail.CreateLocalVariable("FormError");
    formError.DataType = eSpace.TextType;
    formError.SetDefaultValue(null); // default value
    formError.Description = "Message of the last failed project save or activate/deactivate (e.g. ProjectValidation), shown above the form.";
    projectDetail.OnDestroy.Destination = null; // default value
    projectDetail.OnInitialize.Destination = null; // default value
    projectDetail.OnReady.Destination = null; // default value
    projectDetail.OnRender.Destination = null; // default value
    
    var onClick_SaveButton = projectDetail.CreateScreenAction("OnClick_SaveButton");
    onClick_SaveButton.Description = "Saves EditProject through Project_CreateOrUpdate. A new project opens its own ProjectDetail (to add users); an existing one reloads GetProject. ProjectValidation (or any other) error is shown above the form.";
    
    var startNode4 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var assignNode7 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "Clear error";
    
    var assignment7 = assignNode7.CreateAssignment("FormError", "\"\"");
    
    var project_CreateOrUpdate = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_CreateOrUpdate").ConnectedBelow(assignNode7, 1371);
    var project_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("Project_CreateOrUpdate");
    project_CreateOrUpdate.Action = project_CreateOrUpdate2;
    project_CreateOrUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_CreateOrUpdate.IsDisabled = false; // default value
    project_CreateOrUpdate.ServerRequestTimeout = null; // default value
    var projectRecord2 = project_CreateOrUpdate2.InputParameters.Named("ProjectRecord");
    project_CreateOrUpdate.SetArgumentValue(projectRecord2, "EditProject");
    
    var messageNode2 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(project_CreateOrUpdate, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Show saved";
    messageNode2.SetMessage("\"Project saved.\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var ifNode3 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(messageNode2, 1371);
    ifNode3.SetCondition("ProjectId = NullIdentifier()");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "New project?";
    
    var refreshDataNode = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().Below(ifNode3, 1371);
    refreshDataNode.DataSource = getProject;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    ifNode3.FalseTarget = refreshDataNode;
    
    var destinationNode = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode3, 1828);
    destinationNode.Destination = projectDetail;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    destinationNode.SetArgumentValue(projectId2, "Project_CreateOrUpdate.ProjectId");
    ifNode3.TrueTarget = destinationNode;
    
    var endNode8 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode8.IsDisabled = false; // default value
    
    var allExceptions3 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 8684;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode8 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "Keep error";
    
    var assignment8 = assignNode8.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode9 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode8, 1371);
    endNode9.IsDisabled = false; // default value
    
    var onClick_ToggleActiveButton = projectDetail.CreateScreenAction("OnClick_ToggleActiveButton");
    onClick_ToggleActiveButton.Description = "Activates or deactivates the project through Project_SetActive and reloads GetProject.";
    
    var startNode5 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var assignNode9 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = "Clear error";
    
    var assignment9 = assignNode9.CreateAssignment("FormError", "\"\"");
    
    var project_SetActive = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_SetActive").ConnectedBelow(assignNode9, 1371);
    var project_SetActive2 = claudeSheet_CS.ServerActions.Named("Project_SetActive");
    project_SetActive.Action = project_SetActive2;
    project_SetActive.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_SetActive.IsDisabled = false; // default value
    project_SetActive.ServerRequestTimeout = null; // default value
    var projectId3 = project_SetActive2.InputParameters.Named("ProjectId");
    project_SetActive.SetArgumentValue(projectId3, "ProjectId");
    var isActive = project_SetActive2.InputParameters.Named("IsActive");
    project_SetActive.SetArgumentValue(isActive, "not GetProject.ProjectRecord.IsActive");
    
    var refreshDataNode2 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(project_SetActive, 1371);
    refreshDataNode2.DataSource = getProject;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var messageNode3 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode2, 1371);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = "Show status";
    messageNode3.SetMessage("If(GetProject.ProjectRecord.IsActive, \"Project activated.\", \"Project deactivated.\")");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode10 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    endNode10.IsDisabled = false; // default value
    
    var allExceptions4 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.IsDisabled = false; // default value
    allExceptions4.LogError = false;
    allExceptions4.VerticalPosition = 800;
    
    var assignNode10 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions4, 1371);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = "Keep error";
    
    var assignment10 = assignNode10.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode11 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode10, 1371);
    endNode11.IsDisabled = false; // default value
    
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
    
    var projectAssignments = showAssignments.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProjectAssignments");
    projectAssignments.CustomStyle = null; // default value
    projectAssignments.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var assignmentEditor = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("AssignmentEditor");
    projectAssignments.SourceBlock = assignmentEditor;
    var userId = assignmentEditor.InputParameters.Named("UserId");
    projectAssignments.SetArgumentValue(userId, "NullIdentifier()");
    var projectId4 = assignmentEditor.InputParameters.Named("ProjectId");
    projectAssignments.SetArgumentValue(projectId4, "ProjectId");
}
