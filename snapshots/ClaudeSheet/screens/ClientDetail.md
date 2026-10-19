# ClientDetail

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileScreen 'ClientDetail' ***/
    var clientDetail = admin.CreateScreen("ClientDetail");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    clientDetail.Widgets.ToList().ForEach(x => x.Delete());
    clientDetail.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    clientDetail.Roles.Add(sheetAdmin);
    clientDetail.Description = "Create or edit one client (SheetAdmin): name, activate/deactivate, read-only list of its projects. ClientId empty = new client.";
    clientDetail.HorizontalPosition = 1600;
    clientDetail.SetTitle("\"Client\"");
    clientDetail.VerticalPosition = 400;
    
    var getClient = clientDetail.CreateDataAction("GetClient");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getClient.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("ClientId = NullIdentifier()");
    ifNode.Label = "New client?";
    
    var client_GetById = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetById").Below(ifNode, 1371);
    var client_GetById2 = claudeSheet_CS.ServerActions.Named("Client_GetById");
    client_GetById.Action = client_GetById2;
    var clientId = client_GetById2.InputParameters.Named("ClientId");
    client_GetById.SetArgumentValue(clientId, "ClientId");
    ifNode.FalseTarget = client_GetById;
    
    var assignNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode.Label = "New client defaults";
    
    var assignment = assignNode.CreateAssignment("ClientRecord.IsActive", "True");
    ifNode.TrueTarget = assignNode;
    
    var assignNode2 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(client_GetById, 1371);
    assignNode2.Label = "Set client and projects";
    
    var assignment2 = assignNode2.CreateAssignment("ClientRecord", "Client_GetById.ClientRecord");
    
    var assignment3 = assignNode2.CreateAssignment("Projects", "Client_GetById.Projects");
    
    var endNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var endNode2 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var allExceptions = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 8684;
    allExceptions.VerticalPosition = 800;
    
    var assignNode3 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode3.Label = "Keep error";
    
    var assignment4 = assignNode3.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode3 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    
    var clientRecord = getClient.CreateOutputParameter("ClientRecord");
    var client = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Client");
    clientRecord.DataType = client;
    
    var onAfterFetch_GetClient = clientDetail.CreateScreenAction("OnAfterFetch_GetClient");
    onAfterFetch_GetClient.Description = "Copies the fetched client into EditClient; shows an error message when GetClient failed.";
    
    var startNode2 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var assignNode4 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode2, 1371);
    assignNode4.Label = "Copy to form";
    
    var assignment5 = assignNode4.CreateAssignment("EditClient", "GetClient.ClientRecord");
    
    var ifNode2 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode4, 1371);
    ifNode2.SetCondition("GetClient.ErrorMessage <> \"\"");
    ifNode2.Label = "Failed?";
    
    var endNode4 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    
    var messageNode = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode2, 1371);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetClient.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode5 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getClient.OnAfterFetch.Destination = onAfterFetch_GetClient;
    ifNode2.FalseTarget = endNode4;
    ifNode2.TrueTarget = messageNode;
    
    var projects = getClient.CreateOutputParameter("Projects");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    projects.DataType = eSpace.GetOrCreateListType(project);
    
    var errorMessage = getClient.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var clientId2 = clientDetail.CreateInputParameter("ClientId");
    clientId2.DataType = client.IdentifierType;
    clientId2.IsMandatory = false;
    
    var editClient = clientDetail.CreateLocalVariable("EditClient");
    editClient.DataType = client;
    editClient.Description = "Editable copy of the client, bound to the form (filled by OnAfterFetch_GetClient).";
    
    var formError = clientDetail.CreateLocalVariable("FormError");
    formError.DataType = eSpace.TextType;
    formError.Description = "Message of the last failed save or activate/deactivate (e.g. ClientValidation), shown above the form.";
    
    var onClick_SaveButton = clientDetail.CreateScreenAction("OnClick_SaveButton");
    onClick_SaveButton.Description = "Saves EditClient through Client_CreateOrUpdate and returns to Clients; a ClientValidation (or any other) error is shown above the form.";
    
    var startNode3 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var assignNode5 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode5.Label = "Clear error";
    
    var assignment6 = assignNode5.CreateAssignment("FormError", "\"\"");
    
    var client_CreateOrUpdate = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_CreateOrUpdate").ConnectedBelow(assignNode5, 1371);
    var client_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("Client_CreateOrUpdate");
    client_CreateOrUpdate.Action = client_CreateOrUpdate2;
    var clientRecord2 = client_CreateOrUpdate2.InputParameters.Named("ClientRecord");
    client_CreateOrUpdate.SetArgumentValue(clientRecord2, "EditClient");
    
    var messageNode2 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(client_CreateOrUpdate, 1371);
    messageNode2.Label = "Show saved";
    messageNode2.SetMessage("\"Client saved.\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var destinationNode = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ConnectedBelow(messageNode2, 1371);
    var clients = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Clients");
    destinationNode.Destination = clients;
    
    var allExceptions2 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode6 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode6.Label = "Keep error";
    
    var assignment7 = assignNode6.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode6 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode6, 1371);
    
    var onClick_ToggleActiveButton = clientDetail.CreateScreenAction("OnClick_ToggleActiveButton");
    onClick_ToggleActiveButton.Description = "Activates or deactivates the client through Client_SetActive and reloads GetClient.";
    
    var startNode4 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.VerticalPosition = 800;
    
    var assignNode7 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode7.Label = "Clear error";
    
    var assignment8 = assignNode7.CreateAssignment("FormError", "\"\"");
    
    var client_SetActive = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_SetActive").ConnectedBelow(assignNode7, 1371);
    var client_SetActive2 = claudeSheet_CS.ServerActions.Named("Client_SetActive");
    client_SetActive.Action = client_SetActive2;
    var isActive = client_SetActive2.InputParameters.Named("IsActive");
    client_SetActive.SetArgumentValue(isActive, "not GetClient.ClientRecord.IsActive");
    var clientId3 = client_SetActive2.InputParameters.Named("ClientId");
    client_SetActive.SetArgumentValue(clientId3, "ClientId");
    
    var refreshDataNode = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(client_SetActive, 1371);
    refreshDataNode.DataSource = getClient;
    
    var messageNode3 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode, 1371);
    messageNode3.Label = "Show status";
    messageNode3.SetMessage("If(GetClient.ClientRecord.IsActive, \"Client activated.\", \"Client deactivated.\")");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode7 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    
    var allExceptions3 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 6856;
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode8 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode8.Label = "Keep error";
    
    var assignment9 = assignNode8.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode8 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode8, 1371);
    
    var layoutTopMenuInstance = clientDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "1");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.Text = "Client";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var adminPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    adminPage.Gap = null;
    adminPage.SetStyle("\"admin-page\"");
    
    var backLink = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("BackLink");
    backLink.SetEnabled("True");
    backLink.Gap = null;
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)backLink.Widgets.First();
    textWidget2.Text = "Back to clients";
    var builtinEvent = backLink.OnClick;
    builtinEvent.Destination = clients;
    
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
    
    var clientForm = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ClientForm");
    clientForm.Gap = null;
    clientForm.SetStyle("\"admin-form\"");
    
    var textWidget3 = clientForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.Text = "Name";
    
    var nameInput = clientForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("NameInput");
    nameInput.SetEnabled("GetClient.IsDataFetched");
    nameInput.SetMandatory("True");
    nameInput.MaxLength = 100;
    nameInput.SetVariable("EditClient.Name");
    var builtinEvent2 = nameInput.OnChange;
    
    var extendedProperty2 = nameInput.CreateExtendedProperty();
    extendedProperty2.Property = "maxlength";
    extendedProperty2.SetValue("\"100\"");
    
    var extendedProperty3 = nameInput.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Client name\"");
    
    var statusLine = clientForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("StatusLine");
    statusLine.Gap = null;
    statusLine.SetStyle("\"admin-form__status\"");
    
    var expression2 = statusLine.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("If(ClientId = NullIdentifier(), \"New client\", If(GetClient.ClientRecord.IsActive, \"Status: active\", \"Status: inactive\"))");
    
    var formActions = clientForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FormActions");
    formActions.Gap = null;
    formActions.SetStyle("\"admin-form__actions\"");
    
    var saveButton = formActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveButton");
    saveButton.SetEnabled("GetClient.IsDataFetched");
    saveButton.Gap = null;
    saveButton.SetStyle("\"btn btn-primary\"");
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveButton.Widgets.First();
    textWidget4.Text = "Save";
    var builtinEvent3 = saveButton.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent3.Destination = onClick_SaveButton;
    
    var isExisting = formActions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsExisting");
    isExisting.SetCondition("ClientId <> NullIdentifier()");
    
    var toggleActiveButton = isExisting.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ToggleActiveButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    toggleActiveButton.Widgets.ToList().ForEach(x => x.Delete());
    toggleActiveButton.SetEnabled("GetClient.IsDataFetched");
    toggleActiveButton.Gap = null;
    
    var expression3 = toggleActiveButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("If(GetClient.ClientRecord.IsActive, \"Deactivate\", \"Activate\")");
    var builtinEvent4 = toggleActiveButton.OnClick;
    builtinEvent4.Destination = onClick_ToggleActiveButton;
    
    var showProjects = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("ShowProjects");
    showProjects.SetCondition("ClientId <> NullIdentifier()");
    
    var projectsSection = showProjects.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsSection");
    projectsSection.Gap = null;
    projectsSection.SetStyle("\"admin-page\"");
    
    var advancedHtml = projectsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.Gap = null;
    advancedHtml.Tag = "h2";
    
    var textWidget5 = advancedHtml.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.Text = "Projects";
    
    var projectsTable = projectsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsTable");
    projectsTable.Gap = null;
    projectsTable.SetStyle("\"admin-table admin-table--3\"");
    
    var projectsHeader = projectsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsHeader");
    projectsHeader.Gap = null;
    projectsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    
    var container = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"admin-table__cell\"");
    
    var textWidget6 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.Text = "Project";
    
    var extendedProperty4 = container.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
    var container2 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"admin-table__cell\"");
    
    var textWidget7 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.Text = "Code";
    
    var extendedProperty5 = container2.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var container3 = projectsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"admin-table__cell\"");
    
    var textWidget8 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.Text = "Status";
    
    var extendedProperty6 = container3.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var extendedProperty7 = projectsHeader.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"row\"");
    
    var hasProjects = projectsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProjects");
    hasProjects.SetCondition("GetClient.Projects.Length > 0 or not GetClient.IsDataFetched");
    
    var projectList = hasProjects.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectList");
    projectList.Gap = null;
    projectList.SetSource("GetClient.Projects");
    projectList.SetStyle("\"admin-table__body\"");
    
    var projectRow = projectList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectRow");
    projectRow.Gap = null;
    projectRow.SetStyle("\"admin-table__row\"");
    
    var container4 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"admin-table__cell admin-table__name\"");
    
    var expression4 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.SetValue("GetClient.Projects.Current.Name");
    
    var extendedProperty8 = container4.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"cell\"");
    
    var container5 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"admin-table__cell\"");
    
    var expression5 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.SetValue("GetClient.Projects.Current.Code");
    
    var extendedProperty9 = container5.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var container6 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"admin-table__cell \" + If(GetClient.Projects.Current.IsActive, \"is-active\", \"is-inactive\")");
    
    var expression6 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.SetValue("If(GetClient.Projects.Current.IsActive, \"Active\", \"Inactive\")");
    
    var extendedProperty10 = container6.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    
    var extendedProperty11 = projectRow.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"row\"");
    var builtinEvent5 = projectList.OnScrollEnding;
    
    var noProjects = hasProjects.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoProjects");
    noProjects.Gap = null;
    noProjects.SetStyle("\"admin-table__empty\"");
    
    var textWidget9 = noProjects.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.Text = "No projects for this client yet.";
    
    var extendedProperty12 = projectsTable.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"table\"");
}
```
