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
    clientDetail.AnonymousAccess = false; // default value
    clientDetail.CustomURL = false; // default value
    clientDetail.Description = "Create or edit one client (SheetAdmin): name, activate/deactivate, read-only list of its projects. ClientId empty = new client.";
    clientDetail.HorizontalPosition = 1600;
    clientDetail.Icon = null; // default value
    clientDetail.IsDisabled = false; // default value
    clientDetail.PageName = null; // default value
    clientDetail.Public = false; // default value
    clientDetail.Shareable = false; // default value
    clientDetail.StyleSheet = null; // default value
    clientDetail.SetTitle("\"Client\"");
    clientDetail.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    clientDetail.VerticalPosition = 400;
    
    var getClient = clientDetail.CreateDataAction("GetClient");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getClient.OutputParameters.ToList().ForEach(x => x.Delete());
    getClient.Description = null; // default value
    getClient.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getClient.ServerRequestTimeout = null; // default value
    
    var startNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("ClientId = NullIdentifier()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "New client?";
    
    var client_GetById = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetById").Below(ifNode, 1371);
    var client_GetById2 = claudeSheet_CS.ServerActions.Named("Client_GetById");
    client_GetById.Action = client_GetById2;
    client_GetById.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    client_GetById.IsDisabled = false; // default value
    client_GetById.ServerRequestTimeout = null; // default value
    var clientId = client_GetById2.InputParameters.Named("ClientId");
    client_GetById.SetArgumentValue(clientId, "ClientId");
    ifNode.FalseTarget = client_GetById;
    
    var assignNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("ClientRecord.IsActive", "True");
    ifNode.TrueTarget = assignNode;
    
    var assignNode2 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(client_GetById, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("ClientRecord", "Client_GetById.ClientRecord");
    
    var assignment3 = assignNode2.CreateAssignment("Projects", "Client_GetById.Projects");
    
    var endNode = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var endNode2 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var allExceptions = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 8684;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode3 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment4 = assignNode3.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode3 = getClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode3.IsDisabled = false; // default value
    
    var clientRecord = getClient.CreateOutputParameter("ClientRecord");
    var client = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Client");
    clientRecord.DataType = client;
    clientRecord.Description = null; // default value
    
    var onAfterFetch_GetClient = clientDetail.CreateScreenAction("OnAfterFetch_GetClient");
    onAfterFetch_GetClient.Description = "Copies the fetched client into EditClient; shows an error message when GetClient failed.";
    
    var startNode2 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var assignNode4 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode2, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment5 = assignNode4.CreateAssignment("EditClient", "GetClient.ClientRecord");
    
    var ifNode2 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode4, 1371);
    ifNode2.SetCondition("GetClient.ErrorMessage <> \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Failed?";
    
    var endNode4 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    endNode4.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode2, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("GetClient.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode5 = onAfterFetch_GetClient.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode5.IsDisabled = false; // default value
    getClient.OnAfterFetch.Destination = onAfterFetch_GetClient;
    ifNode2.FalseTarget = endNode4;
    ifNode2.TrueTarget = messageNode;
    
    var projects = getClient.CreateOutputParameter("Projects");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    projects.DataType = eSpace.GetOrCreateListType(project);
    projects.Description = null; // default value
    
    var errorMessage = getClient.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var clientId2 = clientDetail.CreateInputParameter("ClientId");
    clientId2.DataType = client.IdentifierType;
    clientId2.SetDefaultValue(null); // default value
    clientId2.Description = null; // default value
    clientId2.IsMandatory = false;
    
    var editClient = clientDetail.CreateLocalVariable("EditClient");
    editClient.DataType = client;
    editClient.Description = "Editable copy of the client, bound to the form (filled by OnAfterFetch_GetClient).";
    
    var formError = clientDetail.CreateLocalVariable("FormError");
    formError.DataType = eSpace.TextType;
    formError.SetDefaultValue(null); // default value
    formError.Description = "Message of the last failed save or activate/deactivate (e.g. ClientValidation), shown above the form.";
    clientDetail.OnDestroy.Destination = null; // default value
    clientDetail.OnInitialize.Destination = null; // default value
    clientDetail.OnReady.Destination = null; // default value
    clientDetail.OnRender.Destination = null; // default value
    
    var onClick_SaveButton = clientDetail.CreateScreenAction("OnClick_SaveButton");
    onClick_SaveButton.Description = "Saves EditClient through Client_CreateOrUpdate and returns to Clients; a ClientValidation (or any other) error is shown above the form.";
    
    var startNode3 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var assignNode5 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment6 = assignNode5.CreateAssignment("FormError", "\"\"");
    
    var client_CreateOrUpdate = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_CreateOrUpdate").ConnectedBelow(assignNode5, 1371);
    var client_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("Client_CreateOrUpdate");
    client_CreateOrUpdate.Action = client_CreateOrUpdate2;
    client_CreateOrUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    client_CreateOrUpdate.IsDisabled = false; // default value
    client_CreateOrUpdate.ServerRequestTimeout = null; // default value
    var clientRecord2 = client_CreateOrUpdate2.InputParameters.Named("ClientRecord");
    client_CreateOrUpdate.SetArgumentValue(clientRecord2, "EditClient");
    
    var messageNode2 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(client_CreateOrUpdate, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = null; // default value
    messageNode2.SetMessage("\"Client saved.\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var destinationNode = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ConnectedBelow(messageNode2, 1371);
    var clients = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Clients");
    destinationNode.Destination = clients;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var allExceptions2 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode6 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    
    var assignment7 = assignNode6.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode6 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode6, 1371);
    endNode6.IsDisabled = false; // default value
    
    var onClick_ToggleActiveButton = clientDetail.CreateScreenAction("OnClick_ToggleActiveButton");
    onClick_ToggleActiveButton.Description = "Activates or deactivates the client through Client_SetActive and reloads GetClient.";
    
    var startNode4 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var assignNode7 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    
    var assignment8 = assignNode7.CreateAssignment("FormError", "\"\"");
    
    var client_SetActive = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_SetActive").ConnectedBelow(assignNode7, 1371);
    var client_SetActive2 = claudeSheet_CS.ServerActions.Named("Client_SetActive");
    client_SetActive.Action = client_SetActive2;
    client_SetActive.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    client_SetActive.IsDisabled = false; // default value
    client_SetActive.ServerRequestTimeout = null; // default value
    var isActive = client_SetActive2.InputParameters.Named("IsActive");
    client_SetActive.SetArgumentValue(isActive, "not GetClient.ClientRecord.IsActive");
    var clientId3 = client_SetActive2.InputParameters.Named("ClientId");
    client_SetActive.SetArgumentValue(clientId3, "ClientId");
    
    var refreshDataNode = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(client_SetActive, 1371);
    refreshDataNode.DataSource = getClient;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var messageNode3 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode, 1371);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = null; // default value
    messageNode3.SetMessage("If(GetClient.ClientRecord.IsActive, \"Client activated.\", \"Client deactivated.\")");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode7 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    endNode7.IsDisabled = false; // default value
    
    var allExceptions3 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 6856;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode8 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    
    var assignment9 = assignNode8.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode8 = onClick_ToggleActiveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode8, 1371);
    endNode8.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = clientDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "1");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Client";
    
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
    textWidget2.Text = "Back to clients";
    var builtinEvent = backLink.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = clients;
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
    
    var clientForm = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ClientForm");
    clientForm.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    clientForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientForm.Animate = false; // default value
    clientForm.ColumnGap = null; // default value
    clientForm.ColumnSpan = null; // default value
    clientForm.CustomStyle = null; // default value
    clientForm.FlexBasis = null; // default value
    clientForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientForm.Gap = null;
    clientForm.GridColumns = null; // default value
    clientForm.GridRows = null; // default value
    clientForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientForm.RowGap = null; // default value
    clientForm.RowSpan = null; // default value
    clientForm.SetStyle("\"admin-form\"");
    clientForm.SetVisible("True"); // default value
    clientForm.VisibleWasExpression = false; // default value
    clientForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientForm.Width = "(fill parent)"; // default value
    
    var textWidget3 = clientForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Name";
    
    var nameInput = clientForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("NameInput");
    nameInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    nameInput.ColumnSpan = null; // default value
    nameInput.CustomStyle = null; // default value
    nameInput.SetEnabled("GetClient.IsDataFetched");
    nameInput.FlexBasis = null; // default value
    nameInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    nameInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    nameInput.SetMandatory("True");
    nameInput.MaxLength = 100;
    nameInput.SetPrompt(null); // default value
    nameInput.RowSpan = null; // default value
    nameInput.SetStyle("\"form-control\""); // default value
    nameInput.SetVariable("EditClient.Name");
    nameInput.Width = "(fill parent)"; // default value
    var builtinEvent2 = nameInput.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = null; // default value
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = nameInput.CreateExtendedProperty();
    extendedProperty2.Property = "maxlength";
    extendedProperty2.SetValue("\"100\"");
    
    var extendedProperty3 = nameInput.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Client name\"");
    
    var statusLine = clientForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("StatusLine");
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
    expression2.SetValue("If(ClientId = NullIdentifier(), \"New client\", If(GetClient.ClientRecord.IsActive, \"Status: active\", \"Status: inactive\"))");
    expression2.Width = ""; // default value
    
    var formActions = clientForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FormActions");
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
    saveButton.SetEnabled("GetClient.IsDataFetched");
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
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveButton.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Save";
    var builtinEvent3 = saveButton.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent3.Destination = onClick_SaveButton;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var isExisting = formActions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsExisting");
    isExisting.Animate = false; // default value
    isExisting.SetCondition("ClientId <> NullIdentifier()");
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
    toggleActiveButton.SetEnabled("GetClient.IsDataFetched");
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
    expression3.SetValue("If(GetClient.ClientRecord.IsActive, \"Deactivate\", \"Activate\")");
    expression3.Width = ""; // default value
    var builtinEvent4 = toggleActiveButton.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = onClick_ToggleActiveButton;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var showProjects = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("ShowProjects");
    showProjects.Animate = false; // default value
    showProjects.SetCondition("ClientId <> NullIdentifier()");
    showProjects.CustomStyle = null; // default value
    showProjects.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var projectsSection = showProjects.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsSection");
    projectsSection.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    projectsSection.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectsSection.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectsSection.Animate = false; // default value
    projectsSection.ColumnGap = null; // default value
    projectsSection.ColumnSpan = null; // default value
    projectsSection.CustomStyle = null; // default value
    projectsSection.FlexBasis = null; // default value
    projectsSection.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectsSection.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectsSection.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectsSection.Gap = null;
    projectsSection.GridColumns = null; // default value
    projectsSection.GridRows = null; // default value
    projectsSection.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectsSection.RowGap = null; // default value
    projectsSection.RowSpan = null; // default value
    projectsSection.SetStyle("\"admin-page\"");
    projectsSection.SetVisible("True"); // default value
    projectsSection.VisibleWasExpression = false; // default value
    projectsSection.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectsSection.Width = "(fill parent)"; // default value
    
    var advancedHtml = projectsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
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
    
    var textWidget5 = advancedHtml.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Projects";
    
    var projectsTable = projectsSection.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectsTable");
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
    projectsTable.SetStyle("\"admin-table admin-table--3\"");
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
    
    var textWidget6 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Project";
    
    var extendedProperty4 = container.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
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
    
    var textWidget7 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Code";
    
    var extendedProperty5 = container2.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
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
    
    var textWidget8 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Status";
    
    var extendedProperty6 = container3.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var extendedProperty7 = projectsHeader.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"row\"");
    
    var hasProjects = projectsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProjects");
    hasProjects.Animate = false; // default value
    hasProjects.SetCondition("GetClient.Projects.Length > 0 or not GetClient.IsDataFetched");
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
    projectList.SetSource("GetClient.Projects");
    projectList.SetStyle("\"admin-table__body\"");
    projectList.Tag = "div"; // default value
    projectList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectList.Width = "(fill parent)"; // default value
    
    var projectRow = projectList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProjectRow");
    projectRow.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    projectRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectRow.Animate = false; // default value
    projectRow.ColumnGap = null; // default value
    projectRow.ColumnSpan = null; // default value
    projectRow.CustomStyle = null; // default value
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
    projectRow.VisibleWasExpression = false; // default value
    projectRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectRow.Width = "(fill parent)"; // default value
    
    var container4 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"admin-table__cell admin-table__name\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var expression4 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetClient.Projects.Current.Name");
    expression4.Width = ""; // default value
    
    var extendedProperty8 = container4.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"cell\"");
    
    var container5 = projectRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression5 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("GetClient.Projects.Current.Code");
    expression5.Width = ""; // default value
    
    var extendedProperty9 = container5.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
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
    container6.SetStyle("\"admin-table__cell \" + If(GetClient.Projects.Current.IsActive, \"is-active\", \"is-inactive\")");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var expression6 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("If(GetClient.Projects.Current.IsActive, \"Active\", \"Inactive\")");
    expression6.Width = ""; // default value
    
    var extendedProperty10 = container6.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    
    var extendedProperty11 = projectRow.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"row\"");
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
    textWidget9.Text = "No projects for this client yet.";
    
    var extendedProperty12 = projectsTable.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"table\"");
}
