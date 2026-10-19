(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileScreen 'Projects' ***/
    var projects = admin.CreateScreen("Projects");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    projects.Widgets.ToList().ForEach(x => x.Delete());
    projects.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    projects.Roles.Add(sheetAdmin);
    projects.AnonymousAccess = false; // default value
    projects.CustomURL = false; // default value
    projects.Description = "Admin list of projects (SheetAdmin): filter by client, show inactive, assigned users; opens ProjectDetail.";
    projects.HorizontalPosition = 400;
    projects.Icon = null; // default value
    projects.IsDisabled = false; // default value
    projects.PageName = null; // default value
    projects.Public = false; // default value
    projects.Shareable = false; // default value
    projects.StyleSheet = null; // default value
    projects.SetTitle("\"Projects\"");
    projects.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    projects.VerticalPosition = 1400;
    
    var getClients = projects.CreateDataAction("GetClients");
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
    
    var getProjects = projects.CreateDataAction("GetProjects");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getProjects.OutputParameters.ToList().ForEach(x => x.Delete());
    getProjects.Description = null; // default value
    getProjects.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProjects.ServerRequestTimeout = null; // default value
    
    var startNode2 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var project_GetList = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetList").ConnectedBelow(startNode2, 1371);
    var project_GetList2 = claudeSheet_CS.ServerActions.Named("Project_GetList");
    project_GetList.Action = project_GetList2;
    project_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetList.IsDisabled = false; // default value
    project_GetList.ServerRequestTimeout = null; // default value
    var includeInactive2 = project_GetList2.InputParameters.Named("IncludeInactive");
    project_GetList.SetArgumentValue(includeInactive2, "ShowInactive");
    var clientId = project_GetList2.InputParameters.Named("ClientId");
    project_GetList.SetArgumentValue(clientId, "FilterClientId");
    
    var assignNode3 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetList, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Set projects";
    
    var assignment3 = assignNode3.CreateAssignment("Projects", "Project_GetList.Projects");
    
    var endNode3 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode3.IsDisabled = false; // default value
    
    var allExceptions2 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = true; // default value
    allExceptions2.VerticalPosition = 800;
    
    var assignNode4 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Keep error";
    
    var assignment4 = assignNode4.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode4 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    endNode4.IsDisabled = false; // default value
    
    var projects2 = getProjects.CreateOutputParameter("Projects");
    var projectSummary = claudeSheet_CS.Structures.Named("ProjectSummary");
    projects2.DataType = eSpace.GetOrCreateListType(projectSummary);
    projects2.Description = null; // default value
    
    var onAfterFetch_GetProjects = projects.CreateScreenAction("OnAfterFetch_GetProjects");
    onAfterFetch_GetProjects.Description = "Shows an error message when GetProjects failed.";
    
    var startNode3 = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode3, 1371);
    ifNode.SetCondition("GetProjects.ErrorMessage <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Failed?";
    
    var endNode5 = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode5.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetProjects.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode6 = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode6.IsDisabled = false; // default value
    getProjects.OnAfterFetch.Destination = onAfterFetch_GetProjects;
    ifNode.FalseTarget = endNode5;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage2 = getProjects.CreateOutputParameter("ErrorMessage");
    errorMessage2.DataType = eSpace.TextType;
    errorMessage2.SetDefaultValue(null); // default value
    errorMessage2.Description = null; // default value
    
    var filterClientId = projects.CreateLocalVariable("FilterClientId");
    var client = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Client");
    filterClientId.DataType = client.IdentifierType;
    filterClientId.SetDefaultValue(null); // default value
    filterClientId.Description = null; // default value
    
    var showInactive = projects.CreateLocalVariable("ShowInactive");
    showInactive.DataType = eSpace.BooleanType;
    showInactive.SetDefaultValue("False");
    showInactive.Description = null; // default value
    projects.OnDestroy.Destination = null; // default value
    projects.OnInitialize.Destination = null; // default value
    projects.OnReady.Destination = null; // default value
    projects.OnRender.Destination = null; // default value
    
    var onChange_Filters = projects.CreateScreenAction("OnChange_Filters");
    onChange_Filters.Description = "OnChange of ClientFilterDropdown and ShowInactiveCheckbox: reloads GetProjects.";
    
    var startNode4 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var refreshDataNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode4, 1371);
    refreshDataNode.DataSource = getProjects;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode7 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode7.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = projects.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopMenuInstance.CustomStyle = null; // default value
    layoutTopMenuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    layoutTopMenuInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "2");
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Projects";
    
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
    
    var toolbar = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Toolbar");
    toolbar.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    toolbar.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    toolbar.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    toolbar.Animate = false; // default value
    toolbar.ColumnGap = null; // default value
    toolbar.ColumnSpan = null; // default value
    toolbar.CustomStyle = null; // default value
    toolbar.FlexBasis = null; // default value
    toolbar.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    toolbar.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    toolbar.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    toolbar.Gap = null;
    toolbar.GridColumns = null; // default value
    toolbar.GridRows = null; // default value
    toolbar.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    toolbar.RowGap = null; // default value
    toolbar.RowSpan = null; // default value
    toolbar.SetStyle("\"admin-toolbar\"");
    toolbar.SetVisible("True"); // default value
    toolbar.VisibleWasExpression = false; // default value
    toolbar.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    toolbar.Width = "(fill parent)"; // default value
    
    var clientFilterDropdown = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ClientFilterDropdown");
    clientFilterDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientFilterDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientFilterDropdown.ColumnGap = null; // default value
    clientFilterDropdown.ColumnSpan = null; // default value
    clientFilterDropdown.CustomStyle = null; // default value
    clientFilterDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    clientFilterDropdown.SetEmptyValue("\"All clients\"");
    clientFilterDropdown.SetEnabled("True");
    clientFilterDropdown.FlexBasis = null; // default value
    clientFilterDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientFilterDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientFilterDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientFilterDropdown.Gap = null;
    clientFilterDropdown.GridColumns = null; // default value
    clientFilterDropdown.GridRows = null; // default value
    clientFilterDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientFilterDropdown.SetLabels("Name");
    clientFilterDropdown.SetList("GetClients.Clients");
    clientFilterDropdown.SetMandatory("False");
    clientFilterDropdown.RowGap = null; // default value
    clientFilterDropdown.RowSpan = null; // default value
    clientFilterDropdown.SetStyle("\"dropdown-display admin-toolbar__filter\"");
    clientFilterDropdown.SetValues("Id");
    clientFilterDropdown.SetVariable("FilterClientId");
    clientFilterDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientFilterDropdown.Width = ""; // default value
    var builtinEvent = clientFilterDropdown.OnChange;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onChange_Filters;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty = clientFilterDropdown.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Filter by client\"");
    
    var showInactiveWrap = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ShowInactiveWrap");
    showInactiveWrap.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    showInactiveWrap.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    showInactiveWrap.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    showInactiveWrap.Animate = false; // default value
    showInactiveWrap.ColumnGap = null; // default value
    showInactiveWrap.ColumnSpan = null; // default value
    showInactiveWrap.CustomStyle = null; // default value
    showInactiveWrap.FlexBasis = null; // default value
    showInactiveWrap.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    showInactiveWrap.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    showInactiveWrap.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    showInactiveWrap.Gap = null;
    showInactiveWrap.GridColumns = null; // default value
    showInactiveWrap.GridRows = null; // default value
    showInactiveWrap.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    showInactiveWrap.RowGap = null; // default value
    showInactiveWrap.RowSpan = null; // default value
    showInactiveWrap.SetStyle("\"admin-toolbar__check\"");
    showInactiveWrap.SetVisible("True"); // default value
    showInactiveWrap.VisibleWasExpression = false; // default value
    showInactiveWrap.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    showInactiveWrap.Width = "(fill parent)"; // default value
    
    var showInactiveCheckbox = showInactiveWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox");
    showInactiveCheckbox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    showInactiveCheckbox.ColumnSpan = null; // default value
    showInactiveCheckbox.CustomStyle = null; // default value
    showInactiveCheckbox.SetEnabled("True");
    showInactiveCheckbox.FlexBasis = null; // default value
    showInactiveCheckbox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    showInactiveCheckbox.RowSpan = null; // default value
    showInactiveCheckbox.SetStyle("\"checkbox\""); // default value
    showInactiveCheckbox.SetVariable("ShowInactive");
    var builtinEvent2 = showInactiveCheckbox.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = onChange_Filters;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = showInactiveCheckbox.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Show inactive projects\"");
    
    var textWidget2 = showInactiveWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Show inactive";
    
    var newProjectButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NewProjectButton");
    newProjectButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    newProjectButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    newProjectButton.ColumnGap = null; // default value
    newProjectButton.ColumnSpan = null; // default value
    newProjectButton.SetConfirmationMessage(null); // default value
    newProjectButton.CustomStyle = null; // default value
    newProjectButton.SetEnabled("True");
    newProjectButton.FlexBasis = null; // default value
    newProjectButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    newProjectButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    newProjectButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    newProjectButton.Gap = null;
    newProjectButton.GridColumns = null; // default value
    newProjectButton.GridRows = null; // default value
    newProjectButton.IsDefault = false; // default value
    newProjectButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    newProjectButton.RowGap = null; // default value
    newProjectButton.RowSpan = null; // default value
    newProjectButton.SetStyle("\"btn btn-primary\"");
    newProjectButton.SetVisible("True"); // default value
    newProjectButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    newProjectButton.Width = ""; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)newProjectButton.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "New project";
    var builtinEvent3 = newProjectButton.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var projectDetail = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("ProjectDetail");
    builtinEvent3.Destination = projectDetail;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var projectId = projectDetail.InputParameters.Named("ProjectId");
    builtinEvent3.SetArgumentValue(projectId, "NullIdentifier()");
    
    var projectsTable = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsTable");
    projectsTable.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    projectsTable.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectsTable.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectsTable.Animate = false; // default value
    projectsTable.ColumnGap = null; // default value
    projectsTable.ColumnSpan = null; // default value
    projectsTable.CustomStyle = null; // default value
    projectsTable.FlexBasis = null; // default value
    projectsTable.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectsTable.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectsTable.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectsTable.Gap = null;
    projectsTable.GridColumns = null; // default value
    projectsTable.GridRows = null; // default value
    projectsTable.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectsTable.RowGap = null; // default value
    projectsTable.RowSpan = null; // default value
    projectsTable.SetStyle("\"admin-table admin-table--5\"");
    projectsTable.SetVisible("True"); // default value
    projectsTable.VisibleWasExpression = false; // default value
    projectsTable.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectsTable.Width = "(fill parent)"; // default value
    
    var projectsHeader = projectsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsHeader");
    projectsHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    projectsHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectsHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectsHeader.Animate = false; // default value
    projectsHeader.ColumnGap = null; // default value
    projectsHeader.ColumnSpan = null; // default value
    projectsHeader.CustomStyle = null; // default value
    projectsHeader.FlexBasis = null; // default value
    projectsHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectsHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectsHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectsHeader.Gap = null;
    projectsHeader.GridColumns = null; // default value
    projectsHeader.GridRows = null; // default value
    projectsHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectsHeader.RowGap = null; // default value
    projectsHeader.RowSpan = null; // default value
    projectsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    projectsHeader.SetVisible("True"); // default value
    projectsHeader.VisibleWasExpression = false; // default value
    projectsHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectsHeader.Width = "(fill parent)"; // default value
    
    var container = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget4 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Client";
    
    var extendedProperty3 = container.CreateExtendedProperty();
    extendedProperty3.Property = "role";
    extendedProperty3.SetValue("\"columnheader\"");
    
    var container2 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget5 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Code";
    
    var extendedProperty4 = container2.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
    var container3 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget6 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Name";
    
    var extendedProperty5 = container3.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var container4 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget7 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Assigned users";
    
    var extendedProperty6 = container4.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var container5 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget8 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Status";
    
    var extendedProperty7 = container5.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"columnheader\"");
    
    var extendedProperty8 = projectsHeader.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"row\"");
    
    var hasProjects = projectsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProjects");
    hasProjects.Animate = false; // default value
    hasProjects.SetCondition("GetProjects.Projects.Length > 0 or not GetProjects.IsDataFetched");
    hasProjects.CustomStyle = null; // default value
    hasProjects.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var projectList = hasProjects.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectList");
    projectList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectList.AnimateItems = true; // default value
    projectList.ColumnGap = null; // default value
    projectList.ColumnSpan = null; // default value
    projectList.CustomStyle = null; // default value
    projectList.ExpandedInWebEditor = true; // default value
    projectList.FlexBasis = null; // default value
    projectList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectList.Gap = null;
    projectList.GridColumns = null; // default value
    projectList.GridRows = null; // default value
    projectList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    projectList.RowGap = null; // default value
    projectList.RowSpan = null; // default value
    projectList.SetSource("GetProjects.Projects");
    projectList.SetStyle("\"admin-table__body\"");
    projectList.Tag = "div"; // default value
    projectList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectList.Width = "(fill parent)"; // default value
    
    var projectRow = projectList.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ProjectRow");
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    projectRow.Widgets.ToList().ForEach(x => x.Delete());
    projectRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectRow.ColumnGap = null; // default value
    projectRow.ColumnSpan = null; // default value
    projectRow.SetConfirmationMessage(null); // default value
    projectRow.CustomStyle = null; // default value
    projectRow.SetEnabled("True");
    projectRow.FlexBasis = null; // default value
    projectRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectRow.Gap = null;
    projectRow.GridColumns = null; // default value
    projectRow.GridRows = null; // default value
    projectRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectRow.RowGap = null; // default value
    projectRow.RowSpan = null; // default value
    projectRow.SetStyle("\"admin-table__row\"");
    projectRow.SetVisible("True"); // default value
    projectRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectRow.Width = ""; // default value
    
    var container6 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"admin-table__cell\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var expression = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetProjects.Projects.Current.ClientName");
    expression.Width = ""; // default value
    
    var extendedProperty9 = container6.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var container7 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression2 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetProjects.Projects.Current.Code");
    expression2.Width = ""; // default value
    
    var extendedProperty10 = container7.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    
    var container8 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container8.SetStyle("\"admin-table__cell admin-table__name\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var expression3 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("GetProjects.Projects.Current.Name");
    expression3.Width = ""; // default value
    
    var extendedProperty11 = container8.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"cell\"");
    
    var container9 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container9.SetStyle("\"admin-table__cell\"");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var expression4 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetProjects.Projects.Current.AssignedUsers");
    expression4.Width = ""; // default value
    
    var extendedProperty12 = container9.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"cell\"");
    
    var container10 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container10.Animate = false; // default value
    container10.ColumnGap = null; // default value
    container10.ColumnSpan = null; // default value
    container10.CustomStyle = null; // default value
    container10.FlexBasis = null; // default value
    container10.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container10.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container10.Gap = null;
    container10.GridColumns = null; // default value
    container10.GridRows = null; // default value
    container10.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container10.RowGap = null; // default value
    container10.RowSpan = null; // default value
    container10.SetStyle("\"admin-table__cell \" + If(GetProjects.Projects.Current.IsActive, \"is-active\", \"is-inactive\")");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var expression5 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("If(GetProjects.Projects.Current.IsActive, \"Active\", \"Inactive\")");
    expression5.Width = ""; // default value
    
    var extendedProperty13 = container10.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    var builtinEvent4 = projectRow.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = projectDetail;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent4.SetArgumentValue(projectId, "GetProjects.Projects.Current.Id");
    
    var extendedProperty14 = projectRow.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"row\"");
    var builtinEvent5 = projectList.OnScrollEnding;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var noProjects = hasProjects.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoProjects");
    noProjects.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noProjects.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noProjects.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noProjects.Animate = false; // default value
    noProjects.ColumnGap = null; // default value
    noProjects.ColumnSpan = null; // default value
    noProjects.CustomStyle = null; // default value
    noProjects.FlexBasis = null; // default value
    noProjects.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noProjects.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noProjects.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noProjects.Gap = null;
    noProjects.GridColumns = null; // default value
    noProjects.GridRows = null; // default value
    noProjects.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noProjects.RowGap = null; // default value
    noProjects.RowSpan = null; // default value
    noProjects.SetStyle("\"admin-table__empty\"");
    noProjects.SetVisible("True"); // default value
    noProjects.VisibleWasExpression = false; // default value
    noProjects.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noProjects.Width = "(fill parent)"; // default value
    
    var textWidget9 = noProjects.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.CustomStyle = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "No projects found.";
    
    var extendedProperty15 = projectsTable.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"table\"");
}
