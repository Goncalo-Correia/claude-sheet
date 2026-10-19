(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileScreen 'Clients' ***/
    var clients = admin.CreateScreen("Clients");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    clients.Widgets.ToList().ForEach(x => x.Delete());
    clients.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    clients.Roles.Add(sheetAdmin);
    clients.AnonymousAccess = false; // default value
    clients.CustomURL = false; // default value
    clients.Description = "Admin list of clients (SheetAdmin): search, show inactive, project count; opens ClientDetail.";
    clients.HorizontalPosition = 400;
    clients.Icon = null; // default value
    clients.IsDisabled = false; // default value
    clients.PageName = null; // default value
    clients.Public = false; // default value
    clients.Shareable = false; // default value
    clients.StyleSheet = "/* Admin screens (US6): toolbar and a simple table. Same rules on Clients and ClientDetail; move to the theme when more admin screens use them. */\n.admin-page { display: flex; flex-direction: column; gap: 16px; }\n.admin-toolbar { display: flex; flex-wrap: wrap; align-items: center; gap: 12px; }\n.admin-toolbar__search { flex: 1 1 240px; max-width: 360px; }\n.admin-toolbar__check { display: flex; align-items: center; gap: 6px; }\n.admin-table { display: flex; flex-direction: column; border: 1px solid var(--color-neutral-4); border-radius: var(--border-radius-soft); overflow: hidden; }\n.admin-table__row { display: grid; grid-template-columns: minmax(0, 3fr) minmax(0, 1fr) minmax(0, 1fr); gap: 12px; padding: 12px 16px; border-top: 1px solid var(--color-neutral-3); color: inherit; text-decoration: none; }\n.admin-table__head { border-top: 0; background-color: var(--color-neutral-2); font-weight: 600; }\na.admin-table__row:hover { background-color: var(--color-neutral-1); }\n.admin-table__cell { min-width: 0; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }\n.admin-table__cell.is-inactive { color: var(--color-neutral-7); }\n.admin-table__empty { padding: 16px; color: var(--color-neutral-7); }\n.admin-form { display: flex; flex-direction: column; gap: 12px; max-width: 480px; }\n.admin-form__actions { display: flex; flex-wrap: wrap; gap: 8px; }\n.admin-error { padding: 12px 16px; border-radius: var(--border-radius-soft); background-color: var(--color-error-lightest, #fdecea); color: var(--color-error, #c62828); }";
    clients.SetTitle("\"Clients\"");
    clients.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    clients.VerticalPosition = 400;
    
    var getClients = clients.CreateDataAction("GetClients");
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
    client_GetList.SetArgumentValue(includeInactive, "ShowInactive");
    var search = client_GetList2.InputParameters.Named("Search");
    client_GetList.SetArgumentValue(search, "Search");
    
    var assignNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(client_GetList, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
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
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var clients2 = getClients.CreateOutputParameter("Clients");
    var clientSummary = claudeSheet_CS.Structures.Named("ClientSummary");
    clients2.DataType = eSpace.GetOrCreateListType(clientSummary);
    clients2.Description = null; // default value
    
    var onAfterFetch_GetClients = clients.CreateScreenAction("OnAfterFetch_GetClients");
    onAfterFetch_GetClients.Description = "Shows an error message when GetClients failed.";
    
    var startNode2 = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetClients.ErrorMessage <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode3.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("GetClients.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    getClients.OnAfterFetch.Destination = onAfterFetch_GetClients;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getClients.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var search2 = clients.CreateLocalVariable("Search");
    search2.DataType = eSpace.TextType;
    search2.SetDefaultValue(null); // default value
    search2.Description = null; // default value
    
    var showInactive = clients.CreateLocalVariable("ShowInactive");
    showInactive.DataType = eSpace.BooleanType;
    showInactive.SetDefaultValue("False");
    showInactive.Description = null; // default value
    clients.OnDestroy.Destination = null; // default value
    clients.OnInitialize.Destination = null; // default value
    clients.OnReady.Destination = null; // default value
    clients.OnRender.Destination = null; // default value
    
    var onChange_Filters = clients.CreateScreenAction("OnChange_Filters");
    onChange_Filters.Description = "OnChange of SearchInput and ShowInactiveCheckbox: reloads GetClients.";
    
    var startNode3 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var refreshDataNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getClients;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode5 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode5.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = clients.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    menuInstance.SetArgumentValue(activeItem, "1");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Clients";
    
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
    
    var searchInput = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("SearchInput");
    searchInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    searchInput.ColumnSpan = null; // default value
    searchInput.CustomStyle = null; // default value
    searchInput.SetEnabled("True");
    searchInput.FlexBasis = null; // default value
    searchInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    searchInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Search;
    searchInput.SetMandatory("False");
    searchInput.MaxLength = 50; // default value
    searchInput.SetPrompt(null); // default value
    searchInput.RowSpan = null; // default value
    searchInput.SetStyle("\"form-control admin-toolbar__search\"");
    searchInput.SetVariable("Search");
    searchInput.Width = "(fill parent)"; // default value
    var builtinEvent = searchInput.OnChange;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onChange_Filters;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty = searchInput.CreateExtendedProperty();
    extendedProperty.Property = "placeholder";
    extendedProperty.SetValue("\"Search clients\"");
    
    var extendedProperty2 = searchInput.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Search clients\"");
    
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
    
    var extendedProperty3 = showInactiveCheckbox.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Show inactive clients\"");
    
    var textWidget2 = showInactiveWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Show inactive";
    
    var newClientButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NewClientButton");
    newClientButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    newClientButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    newClientButton.ColumnGap = null; // default value
    newClientButton.ColumnSpan = null; // default value
    newClientButton.SetConfirmationMessage(null); // default value
    newClientButton.CustomStyle = null; // default value
    newClientButton.SetEnabled("True");
    newClientButton.FlexBasis = null; // default value
    newClientButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    newClientButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    newClientButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    newClientButton.Gap = null;
    newClientButton.GridColumns = null; // default value
    newClientButton.GridRows = null; // default value
    newClientButton.IsDefault = false; // default value
    newClientButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    newClientButton.RowGap = null; // default value
    newClientButton.RowSpan = null; // default value
    newClientButton.SetStyle("\"btn btn-primary\"");
    newClientButton.SetVisible("True"); // default value
    newClientButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    newClientButton.Width = ""; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)newClientButton.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "New client";
    var builtinEvent3 = newClientButton.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var clientDetail = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("ClientDetail");
    builtinEvent3.Destination = clientDetail;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var clientId = clientDetail.InputParameters.Named("ClientId");
    builtinEvent3.SetArgumentValue(clientId, "NullIdentifier()");
    
    var clientsTable = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ClientsTable");
    clientsTable.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    clientsTable.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientsTable.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientsTable.Animate = false; // default value
    clientsTable.ColumnGap = null; // default value
    clientsTable.ColumnSpan = null; // default value
    clientsTable.CustomStyle = null; // default value
    clientsTable.FlexBasis = null; // default value
    clientsTable.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientsTable.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientsTable.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientsTable.Gap = null;
    clientsTable.GridColumns = null; // default value
    clientsTable.GridRows = null; // default value
    clientsTable.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientsTable.RowGap = null; // default value
    clientsTable.RowSpan = null; // default value
    clientsTable.SetStyle("\"admin-table\"");
    clientsTable.SetVisible("True"); // default value
    clientsTable.VisibleWasExpression = false; // default value
    clientsTable.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientsTable.Width = "(fill parent)"; // default value
    
    var clientsHeader = clientsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ClientsHeader");
    clientsHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    clientsHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientsHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientsHeader.Animate = false; // default value
    clientsHeader.ColumnGap = null; // default value
    clientsHeader.ColumnSpan = null; // default value
    clientsHeader.CustomStyle = null; // default value
    clientsHeader.FlexBasis = null; // default value
    clientsHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientsHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientsHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientsHeader.Gap = null;
    clientsHeader.GridColumns = null; // default value
    clientsHeader.GridRows = null; // default value
    clientsHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientsHeader.RowGap = null; // default value
    clientsHeader.RowSpan = null; // default value
    clientsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    clientsHeader.SetVisible("True"); // default value
    clientsHeader.VisibleWasExpression = false; // default value
    clientsHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientsHeader.Width = "(fill parent)"; // default value
    
    var container = clientsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    textWidget4.Text = "Name";
    
    var extendedProperty4 = container.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
    var container2 = clientsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    textWidget5.Text = "Projects";
    
    var extendedProperty5 = container2.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var container3 = clientsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    textWidget6.Text = "Status";
    
    var extendedProperty6 = container3.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var extendedProperty7 = clientsHeader.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"row\"");
    
    var hasClients = clientsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasClients");
    hasClients.Animate = false; // default value
    hasClients.SetCondition("GetClients.Clients.Length > 0 or not GetClients.IsDataFetched");
    hasClients.CustomStyle = null; // default value
    hasClients.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var clientList = hasClients.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ClientList");
    clientList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientList.AnimateItems = true; // default value
    clientList.ColumnGap = null; // default value
    clientList.ColumnSpan = null; // default value
    clientList.CustomStyle = null; // default value
    clientList.ExpandedInWebEditor = true; // default value
    clientList.FlexBasis = null; // default value
    clientList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientList.Gap = null;
    clientList.GridColumns = null; // default value
    clientList.GridRows = null; // default value
    clientList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    clientList.RowGap = null; // default value
    clientList.RowSpan = null; // default value
    clientList.SetSource("GetClients.Clients");
    clientList.SetStyle("\"admin-table__body\"");
    clientList.Tag = "div"; // default value
    clientList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientList.Width = "(fill parent)"; // default value
    
    var clientRow = clientList.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ClientRow");
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    clientRow.Widgets.ToList().ForEach(x => x.Delete());
    clientRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientRow.ColumnGap = null; // default value
    clientRow.ColumnSpan = null; // default value
    clientRow.SetConfirmationMessage(null); // default value
    clientRow.CustomStyle = null; // default value
    clientRow.SetEnabled("True");
    clientRow.FlexBasis = null; // default value
    clientRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientRow.Gap = null;
    clientRow.GridColumns = null; // default value
    clientRow.GridRows = null; // default value
    clientRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientRow.RowGap = null; // default value
    clientRow.RowSpan = null; // default value
    clientRow.SetStyle("\"admin-table__row\"");
    clientRow.SetVisible("True"); // default value
    clientRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientRow.Width = ""; // default value
    
    var container4 = clientRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetClients.Clients.Current.Name");
    expression.Width = ""; // default value
    
    var extendedProperty8 = container4.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"cell\"");
    
    var container5 = clientRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression2 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetClients.Clients.Current.ProjectCount");
    expression2.Width = ""; // default value
    
    var extendedProperty9 = container5.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var container6 = clientRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"admin-table__cell \" + If(GetClients.Clients.Current.IsActive, \"is-active\", \"is-inactive\")");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var expression3 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("If(GetClients.Clients.Current.IsActive, \"Active\", \"Inactive\")");
    expression3.Width = ""; // default value
    
    var extendedProperty10 = container6.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    var builtinEvent4 = clientRow.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = clientDetail;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent4.SetArgumentValue(clientId, "GetClients.Clients.Current.Id");
    
    var extendedProperty11 = clientRow.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"row\"");
    var builtinEvent5 = clientList.OnScrollEnding;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var noClients = hasClients.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoClients");
    noClients.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noClients.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noClients.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noClients.Animate = false; // default value
    noClients.ColumnGap = null; // default value
    noClients.ColumnSpan = null; // default value
    noClients.CustomStyle = null; // default value
    noClients.FlexBasis = null; // default value
    noClients.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noClients.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noClients.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noClients.Gap = null;
    noClients.GridColumns = null; // default value
    noClients.GridRows = null; // default value
    noClients.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noClients.RowGap = null; // default value
    noClients.RowSpan = null; // default value
    noClients.SetStyle("\"admin-table__empty\"");
    noClients.SetVisible("True"); // default value
    noClients.VisibleWasExpression = false; // default value
    noClients.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noClients.Width = "(fill parent)"; // default value
    
    var textWidget7 = noClients.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "No clients found.";
    
    var extendedProperty12 = clientsTable.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"table\"");
}
