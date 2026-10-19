# Projects

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
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
    projects.Description = "Admin list of projects (SheetAdmin): filter by client, show inactive, assigned users; opens ProjectDetail.";
    projects.HorizontalPosition = 400;
    projects.SetTitle("\"Projects\"");
    projects.VerticalPosition = 1400;
    
    var getClients = projects.CreateDataAction("GetClients");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getClients.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var client_GetList = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetList").ConnectedBelow(startNode, 1371);
    var client_GetList2 = claudeSheet_CS.ServerActions.Named("Client_GetList");
    client_GetList.Action = client_GetList2;
    var includeInactive = client_GetList2.InputParameters.Named("IncludeInactive");
    client_GetList.SetArgumentValue(includeInactive, "True");
    var search = client_GetList2.InputParameters.Named("Search");
    client_GetList.SetArgumentValue(search, "\"\"");
    
    var assignNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(client_GetList, 1371);
    assignNode.Label = "Set clients";
    
    var assignment = assignNode.CreateAssignment("Clients", "Client_GetList.Clients");
    
    var endNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var allExceptions = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var clients = getClients.CreateOutputParameter("Clients");
    var clientSummary = claudeSheet_CS.Structures.Named("ClientSummary");
    clients.DataType = eSpace.GetOrCreateListType(clientSummary);
    
    var errorMessage = getClients.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var getProjects = projects.CreateDataAction("GetProjects");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getProjects.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode2 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var project_GetList = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetList").ConnectedBelow(startNode2, 1371);
    var project_GetList2 = claudeSheet_CS.ServerActions.Named("Project_GetList");
    project_GetList.Action = project_GetList2;
    var includeInactive2 = project_GetList2.InputParameters.Named("IncludeInactive");
    project_GetList.SetArgumentValue(includeInactive2, "ShowInactive");
    var clientId = project_GetList2.InputParameters.Named("ClientId");
    project_GetList.SetArgumentValue(clientId, "FilterClientId");
    
    var assignNode3 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetList, 1371);
    assignNode3.Label = "Set projects";
    
    var assignment3 = assignNode3.CreateAssignment("Projects", "Project_GetList.Projects");
    
    var endNode3 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    
    var allExceptions2 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode4 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode4.Label = "Keep error";
    
    var assignment4 = assignNode4.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode4 = getProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    
    var projects2 = getProjects.CreateOutputParameter("Projects");
    var projectSummary = claudeSheet_CS.Structures.Named("ProjectSummary");
    projects2.DataType = eSpace.GetOrCreateListType(projectSummary);
    
    var onAfterFetch_GetProjects = projects.CreateScreenAction("OnAfterFetch_GetProjects");
    onAfterFetch_GetProjects.Description = "Shows an error message when GetProjects failed.";
    
    var startNode3 = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode3, 1371);
    ifNode.SetCondition("GetProjects.ErrorMessage <> \"\"");
    ifNode.Label = "Failed?";
    
    var endNode5 = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    
    var messageNode = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetProjects.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode6 = onAfterFetch_GetProjects.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getProjects.OnAfterFetch.Destination = onAfterFetch_GetProjects;
    ifNode.FalseTarget = endNode5;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage2 = getProjects.CreateOutputParameter("ErrorMessage");
    errorMessage2.DataType = eSpace.TextType;
    
    var filterClientId = projects.CreateLocalVariable("FilterClientId");
    var client = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Client");
    filterClientId.DataType = client.IdentifierType;
    
    var showInactive = projects.CreateLocalVariable("ShowInactive");
    showInactive.DataType = eSpace.BooleanType;
    showInactive.SetDefaultValue("False");
    
    var onChange_Filters = projects.CreateScreenAction("OnChange_Filters");
    onChange_Filters.Description = "OnChange of ClientFilterDropdown and ShowInactiveCheckbox: reloads GetProjects.";
    
    var startNode4 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.VerticalPosition = 800;
    
    var refreshDataNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode4, 1371);
    refreshDataNode.DataSource = getProjects;
    
    var endNode7 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    
    var layoutTopMenuInstance = projects.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "2");
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.Text = "Projects";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var adminPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    adminPage.Gap = null;
    adminPage.SetStyle("\"admin-page\"");
    
    var toolbar = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Toolbar");
    toolbar.Gap = null;
    toolbar.SetStyle("\"admin-toolbar\"");
    
    var clientFilterDropdown = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ClientFilterDropdown");
    clientFilterDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    clientFilterDropdown.SetEmptyValue("\"All clients\"");
    clientFilterDropdown.SetEnabled("True");
    clientFilterDropdown.Gap = null;
    clientFilterDropdown.SetLabels("Name");
    clientFilterDropdown.SetList("GetClients.Clients");
    clientFilterDropdown.SetMandatory("False");
    clientFilterDropdown.SetStyle("\"dropdown-display admin-toolbar__filter\"");
    clientFilterDropdown.SetValues("Id");
    clientFilterDropdown.SetVariable("FilterClientId");
    var builtinEvent = clientFilterDropdown.OnChange;
    builtinEvent.Destination = onChange_Filters;
    
    var extendedProperty = clientFilterDropdown.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Filter by client\"");
    
    var showInactiveWrap = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ShowInactiveWrap");
    showInactiveWrap.Gap = null;
    showInactiveWrap.SetStyle("\"admin-toolbar__check\"");
    
    var showInactiveCheckbox = showInactiveWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox");
    showInactiveCheckbox.SetEnabled("True");
    showInactiveCheckbox.SetVariable("ShowInactive");
    var builtinEvent2 = showInactiveCheckbox.OnChange;
    builtinEvent2.Destination = onChange_Filters;
    
    var extendedProperty2 = showInactiveCheckbox.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Show inactive projects\"");
    
    var textWidget2 = showInactiveWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.Text = "Show inactive";
    
    var newProjectButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NewProjectButton");
    newProjectButton.SetEnabled("True");
    newProjectButton.Gap = null;
    newProjectButton.SetStyle("\"btn btn-primary\"");
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)newProjectButton.Widgets.First();
    textWidget3.Text = "New project";
    var builtinEvent3 = newProjectButton.OnClick;
    var projectDetail = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("ProjectDetail");
    builtinEvent3.Destination = projectDetail;
    var projectId = projectDetail.InputParameters.Named("ProjectId");
    builtinEvent3.SetArgumentValue(projectId, "NullIdentifier()");
    
    var projectsTable = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsTable");
    projectsTable.Gap = null;
    projectsTable.SetStyle("\"admin-table admin-table--5\"");
    
    var projectsHeader = projectsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsHeader");
    projectsHeader.Gap = null;
    projectsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    
    var container = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"admin-table__cell\"");
    
    var textWidget4 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.Text = "Client";
    
    var extendedProperty3 = container.CreateExtendedProperty();
    extendedProperty3.Property = "role";
    extendedProperty3.SetValue("\"columnheader\"");
    
    var container2 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"admin-table__cell\"");
    
    var textWidget5 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.Text = "Code";
    
    var extendedProperty4 = container2.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
    var container3 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"admin-table__cell\"");
    
    var textWidget6 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.Text = "Name";
    
    var extendedProperty5 = container3.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var container4 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"admin-table__cell\"");
    
    var textWidget7 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.Text = "Assigned users";
    
    var extendedProperty6 = container4.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var container5 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"admin-table__cell\"");
    
    var textWidget8 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.Text = "Status";
    
    var extendedProperty7 = container5.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"columnheader\"");
    
    var extendedProperty8 = projectsHeader.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"row\"");
    
    var hasProjects = projectsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProjects");
    hasProjects.SetCondition("GetProjects.Projects.Length > 0 or not GetProjects.IsDataFetched");
    
    var projectList = hasProjects.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectList");
    projectList.Gap = null;
    projectList.SetSource("GetProjects.Projects");
    projectList.SetStyle("\"admin-table__body\"");
    
    var projectRow = projectList.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ProjectRow");
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    projectRow.Widgets.ToList().ForEach(x => x.Delete());
    projectRow.SetEnabled("True");
    projectRow.Gap = null;
    projectRow.SetStyle("\"admin-table__row\"");
    
    var container6 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"admin-table__cell\"");
    
    var expression = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.SetValue("GetProjects.Projects.Current.ClientName");
    
    var extendedProperty9 = container6.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var container7 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Gap = null;
    container7.SetStyle("\"admin-table__cell\"");
    
    var expression2 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("GetProjects.Projects.Current.Code");
    
    var extendedProperty10 = container7.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    
    var container8 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Gap = null;
    container8.SetStyle("\"admin-table__cell admin-table__name\"");
    
    var expression3 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("GetProjects.Projects.Current.Name");
    
    var extendedProperty11 = container8.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"cell\"");
    
    var container9 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Gap = null;
    container9.SetStyle("\"admin-table__cell\"");
    
    var expression4 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.SetValue("GetProjects.Projects.Current.AssignedUsers");
    
    var extendedProperty12 = container9.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"cell\"");
    
    var container10 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Gap = null;
    container10.SetStyle("\"admin-table__cell \" + If(GetProjects.Projects.Current.IsActive, \"is-active\", \"is-inactive\")");
    
    var expression5 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.SetValue("If(GetProjects.Projects.Current.IsActive, \"Active\", \"Inactive\")");
    
    var extendedProperty13 = container10.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    var builtinEvent4 = projectRow.OnClick;
    builtinEvent4.Destination = projectDetail;
    builtinEvent4.SetArgumentValue(projectId, "GetProjects.Projects.Current.Id");
    
    var extendedProperty14 = projectRow.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"row\"");
    var builtinEvent5 = projectList.OnScrollEnding;
    
    var noProjects = hasProjects.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoProjects");
    noProjects.Gap = null;
    noProjects.SetStyle("\"admin-table__empty\"");
    
    var textWidget9 = noProjects.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.Text = "No projects found.";
    
    var extendedProperty15 = projectsTable.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"table\"");
}
```
