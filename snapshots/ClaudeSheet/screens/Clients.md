# Clients

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
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
    clients.Description = "Admin list of clients (SheetAdmin): search, show inactive, project count; opens ClientDetail.";
    clients.HorizontalPosition = 400;
    clients.SetTitle("\"Clients\"");
    clients.VerticalPosition = 400;
    
    var getClients = clients.CreateDataAction("GetClients");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getClients.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var client_GetList = getClients.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Client_GetList").ConnectedBelow(startNode, 1371);
    var client_GetList2 = claudeSheet_CS.ServerActions.Named("Client_GetList");
    client_GetList.Action = client_GetList2;
    var includeInactive = client_GetList2.InputParameters.Named("IncludeInactive");
    client_GetList.SetArgumentValue(includeInactive, "ShowInactive");
    var search = client_GetList2.InputParameters.Named("Search");
    client_GetList.SetArgumentValue(search, "Search");
    
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
    
    var clients2 = getClients.CreateOutputParameter("Clients");
    var clientSummary = claudeSheet_CS.Structures.Named("ClientSummary");
    clients2.DataType = eSpace.GetOrCreateListType(clientSummary);
    
    var onAfterFetch_GetClients = clients.CreateScreenAction("OnAfterFetch_GetClients");
    onAfterFetch_GetClients.Description = "Shows an error message when GetClients failed.";
    
    var startNode2 = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetClients.ErrorMessage <> \"\"");
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    
    var messageNode = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetClients.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetClients.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getClients.OnAfterFetch.Destination = onAfterFetch_GetClients;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getClients.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var search2 = clients.CreateLocalVariable("Search");
    search2.DataType = eSpace.TextType;
    
    var showInactive = clients.CreateLocalVariable("ShowInactive");
    showInactive.DataType = eSpace.BooleanType;
    showInactive.SetDefaultValue("False");
    
    var onChange_Filters = clients.CreateScreenAction("OnChange_Filters");
    onChange_Filters.Description = "OnChange of SearchInput and ShowInactiveCheckbox: reloads GetClients.";
    
    var startNode3 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var refreshDataNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getClients;
    
    var endNode5 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    
    var layoutTopMenuInstance = clients.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    menuInstance.SetArgumentValue(activeItem, "1");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.Text = "Clients";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var adminPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    adminPage.Gap = null;
    adminPage.SetStyle("\"admin-page\"");
    
    var toolbar = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Toolbar");
    toolbar.Gap = null;
    toolbar.SetStyle("\"admin-toolbar\"");
    
    var searchInput = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("SearchInput");
    searchInput.SetEnabled("True");
    searchInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Search;
    searchInput.SetMandatory("False");
    searchInput.SetStyle("\"form-control admin-toolbar__search\"");
    searchInput.SetVariable("Search");
    var builtinEvent = searchInput.OnChange;
    builtinEvent.Destination = onChange_Filters;
    
    var extendedProperty = searchInput.CreateExtendedProperty();
    extendedProperty.Property = "placeholder";
    extendedProperty.SetValue("\"Search clients\"");
    
    var extendedProperty2 = searchInput.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Search clients\"");
    
    var showInactiveWrap = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ShowInactiveWrap");
    showInactiveWrap.Gap = null;
    showInactiveWrap.SetStyle("\"admin-toolbar__check\"");
    
    var showInactiveCheckbox = showInactiveWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox");
    showInactiveCheckbox.SetEnabled("True");
    showInactiveCheckbox.SetVariable("ShowInactive");
    var builtinEvent2 = showInactiveCheckbox.OnChange;
    builtinEvent2.Destination = onChange_Filters;
    
    var extendedProperty3 = showInactiveCheckbox.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Show inactive clients\"");
    
    var textWidget2 = showInactiveWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.Text = "Show inactive";
    
    var newClientButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NewClientButton");
    newClientButton.SetEnabled("True");
    newClientButton.Gap = null;
    newClientButton.SetStyle("\"btn btn-primary\"");
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)newClientButton.Widgets.First();
    textWidget3.Text = "New client";
    var builtinEvent3 = newClientButton.OnClick;
    var clientDetail = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("ClientDetail");
    builtinEvent3.Destination = clientDetail;
    var clientId = clientDetail.InputParameters.Named("ClientId");
    builtinEvent3.SetArgumentValue(clientId, "NullIdentifier()");
    
    var clientsTable = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ClientsTable");
    clientsTable.Gap = null;
    clientsTable.SetStyle("\"admin-table admin-table--3\"");
    
    var clientsHeader = clientsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ClientsHeader");
    clientsHeader.Gap = null;
    clientsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    
    var container = clientsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"admin-table__cell\"");
    
    var textWidget4 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.Text = "Name";
    
    var extendedProperty4 = container.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
    var container2 = clientsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"admin-table__cell\"");
    
    var textWidget5 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.Text = "Projects";
    
    var extendedProperty5 = container2.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var container3 = clientsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"admin-table__cell\"");
    
    var textWidget6 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.Text = "Status";
    
    var extendedProperty6 = container3.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var extendedProperty7 = clientsHeader.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"row\"");
    
    var hasClients = clientsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasClients");
    hasClients.SetCondition("GetClients.Clients.Length > 0 or not GetClients.IsDataFetched");
    
    var clientList = hasClients.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ClientList");
    clientList.Gap = null;
    clientList.SetSource("GetClients.Clients");
    clientList.SetStyle("\"admin-table__body\"");
    
    var clientRow = clientList.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ClientRow");
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    clientRow.Widgets.ToList().ForEach(x => x.Delete());
    clientRow.SetEnabled("True");
    clientRow.Gap = null;
    clientRow.SetStyle("\"admin-table__row\"");
    
    var container4 = clientRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"admin-table__cell admin-table__name\"");
    
    var expression = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.SetValue("GetClients.Clients.Current.Name");
    
    var extendedProperty8 = container4.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"cell\"");
    
    var container5 = clientRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"admin-table__cell\"");
    
    var expression2 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("GetClients.Clients.Current.ProjectCount");
    
    var extendedProperty9 = container5.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var container6 = clientRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"admin-table__cell \" + If(GetClients.Clients.Current.IsActive, \"is-active\", \"is-inactive\")");
    
    var expression3 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("If(GetClients.Clients.Current.IsActive, \"Active\", \"Inactive\")");
    
    var extendedProperty10 = container6.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    var builtinEvent4 = clientRow.OnClick;
    builtinEvent4.Destination = clientDetail;
    builtinEvent4.SetArgumentValue(clientId, "GetClients.Clients.Current.Id");
    
    var extendedProperty11 = clientRow.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"row\"");
    var builtinEvent5 = clientList.OnScrollEnding;
    
    var noClients = hasClients.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoClients");
    noClients.Gap = null;
    noClients.SetStyle("\"admin-table__empty\"");
    
    var textWidget7 = noClients.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.Text = "No clients found.";
    
    var extendedProperty12 = clientsTable.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"table\"");
}
```
