# Menu

Block in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileBlock 'Menu' ***/
    var menu = common.CreateBlock("Menu");
    menu.Description = "Navigation element with links to the main screens of your application.";
    menu.HorizontalPosition = 4114;
    menu.Public = true;
    menu.Shareable = true;
    menu.VerticalPosition = 2514;
    
    var getMenuAccess = menu.CreateDataAction("GetMenuAccess");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getMenuAccess.OutputParameters.ToList().ForEach(x => x.Delete());
    getMenuAccess.Description = "Which role-gated menu links the user may see (server-side role checks; referenced roles have no client-side check).";
    
    var startNode = getMenuAccess.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var assignNode = getMenuAccess.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode, 1371);
    assignNode.Label = "Check admin role";
    
    var assignment = assignNode.CreateAssignment("IsSheetAdmin", "CheckSheetAdminRole()");
    
    var endNode = getMenuAccess.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var isSheetAdmin = getMenuAccess.CreateOutputParameter("IsSheetAdmin");
    var eSpace = common.GetESpace();
    isSheetAdmin.DataType = eSpace.BooleanType;
    
    var activeItem = menu.CreateInputParameter("ActiveItem");
    activeItem.DataType = eSpace.IntegerType;
    activeItem.SetDefaultValue("-1");
    activeItem.Description = "Manually set as active a menu item, using it\'s index.";
    activeItem.IsMandatory = false;
    
    var activeSubItem = menu.CreateInputParameter("ActiveSubItem");
    activeSubItem.DataType = eSpace.IntegerType;
    activeSubItem.SetDefaultValue("-1");
    activeSubItem.Description = "Manually set as active a submenu item, using it\'s index.";
    activeSubItem.IsMandatory = false;
    
    var hideMenu = menu.CreateScreenAction("HideMenu");
    
    var startNode2 = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var onDestroy = menu.CreateScreenAction("OnDestroy");
    
    var startNode3 = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var menuDestroy = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("MenuDestroy");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var menuDestroy2 = outSystemsUI.ClientActions.Named("MenuDestroy");
    menuDestroy.Action = menuDestroy2;
    menuDestroy.HorizontalPosition = 3192;
    menuDestroy.VerticalPosition = 2736;
    
    var endNode2 = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3196;
    endNode2.VerticalPosition = 5120;
    menu.OnDestroy.Destination = onDestroy;
    
    var onRender = menu.CreateScreenAction("OnRender");
    
    var startNode4 = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var setMenuListeners = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("SetMenuListeners");
    var setMenuListeners2 = outSystemsUI.ClientActions.Named("SetMenuListeners");
    setMenuListeners.Action = setMenuListeners2;
    setMenuListeners.HorizontalPosition = 3192;
    setMenuListeners.VerticalPosition = 2508;
    var widgetId = setMenuListeners2.InputParameters.Named("WidgetId");
    
    var endNode3 = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 3200;
    endNode3.VerticalPosition = 4343;
    menu.OnParametersChanged.Destination = onRender;
    
    var onReady = menu.CreateScreenAction("OnReady");
    
    var startNode5 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var menuReady = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("MenuReady");
    var menuReady2 = outSystemsUI.ClientActions.Named("MenuReady");
    menuReady.Action = menuReady2;
    menuReady.HorizontalPosition = 3192;
    menuReady.VerticalPosition = 2508;
    var callback = menuReady2.InputParameters.Named("Callback");
    
    var setActiveMenuItems = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("SetActiveMenuItems");
    var setActiveMenuItems2 = outSystemsUI.ClientActions.Named("SetActiveMenuItems");
    setActiveMenuItems.Action = setActiveMenuItems2;
    setActiveMenuItems.HorizontalPosition = 3200;
    setActiveMenuItems.VerticalPosition = 4343;
    var activeItem2 = setActiveMenuItems2.InputParameters.Named("ActiveItem");
    setActiveMenuItems.SetArgumentValue(activeItem2, "ActiveItem");
    var activeSubItem2 = setActiveMenuItems2.InputParameters.Named("ActiveSubItem");
    setActiveMenuItems.SetArgumentValue(activeSubItem2, "ActiveSubItem");
    var widgetId2 = setActiveMenuItems2.InputParameters.Named("WidgetId");
    
    var endNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(setActiveMenuItems, 1600);
    menu.OnReady.Destination = onReady;
    menu.OnRender.Destination = onRender;
    
    var toggleSideMenu = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ToggleSideMenu");
    var toggleSideMenu2 = outSystemsUI.ClientActions.Named("ToggleSideMenu");
    toggleSideMenu.Action = toggleSideMenu2;
    toggleSideMenu.HorizontalPosition = 3192;
    toggleSideMenu.VerticalPosition = 2736;
    startNode2.Target = toggleSideMenu;
    startNode3.Target = menuDestroy;
    menuDestroy.Target = endNode2;
    startNode4.Target = setMenuListeners;
    setMenuListeners.Target = endNode3;
    startNode5.Target = menuReady;
    menuReady.Target = setActiveMenuItems;
    
    var endNode5 = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode5.HorizontalPosition = 3196;
    endNode5.VerticalPosition = 5120;
    toggleSideMenu.Target = endNode5;
    
    var advancedHtml = menu.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.Gap = null;
    advancedHtml.Tag = "nav";
    
    var container = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"header-logo placeholder-empty\"");
    
    var ifWidget = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.SetCondition("False");
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowFalse;
    
    var applicationTitleInstance = ifWidget.FalseBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var applicationTitle = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("ApplicationTitle");
    applicationTitleInstance.SourceBlock = applicationTitle;
    
    var pageLinks = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageLinks");
    pageLinks.Gap = null;
    pageLinks.SetStyle("\"app-menu-links\"");
    
    var mySheetLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("MySheetLink");
    mySheetLink.SetEnabled("True");
    mySheetLink.Gap = null;
    
    var textWidget = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)mySheetLink.Widgets.First();
    textWidget.Text = "My Sheet";
    var builtinEvent = mySheetLink.OnClick;
    var mainFlow = eSpace.MobileFlows.Named("MainFlow");
    var mySheet = mainFlow.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("MySheet");
    builtinEvent.Destination = mySheet;
    
    var extendedProperty = mySheetLink.CreateExtendedProperty();
    extendedProperty.Property = "role";
    extendedProperty.SetValue("\"menuitem\"");
    
    var clientsLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ClientsLink");
    clientsLink.SetEnabled("True");
    clientsLink.Gap = null;
    clientsLink.SetVisible("GetMenuAccess.IsSheetAdmin");
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)clientsLink.Widgets.First();
    textWidget2.Text = "Clients";
    var builtinEvent2 = clientsLink.OnClick;
    var admin = eSpace.MobileFlows.Named("Admin");
    var clients = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Clients");
    builtinEvent2.Destination = clients;
    
    var extendedProperty2 = clientsLink.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"menuitem\"");
    
    var projectsLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ProjectsLink");
    projectsLink.SetEnabled("True");
    projectsLink.Gap = null;
    projectsLink.SetVisible("GetMenuAccess.IsSheetAdmin");
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)projectsLink.Widgets.First();
    textWidget3.Text = "Projects";
    var builtinEvent3 = projectsLink.OnClick;
    var projects = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Projects");
    builtinEvent3.Destination = projects;
    
    var extendedProperty3 = projectsLink.CreateExtendedProperty();
    extendedProperty3.Property = "role";
    extendedProperty3.SetValue("\"menuitem\"");
    
    var holidaysLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("HolidaysLink");
    holidaysLink.SetEnabled("True");
    holidaysLink.Gap = null;
    holidaysLink.SetVisible("GetMenuAccess.IsSheetAdmin");
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)holidaysLink.Widgets.First();
    textWidget4.Text = "Holidays";
    var builtinEvent4 = holidaysLink.OnClick;
    var holidays = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Holidays");
    builtinEvent4.Destination = holidays;
    
    var extendedProperty4 = holidaysLink.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"menuitem\"");
    
    var extendedProperty5 = pageLinks.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"menubar\"");
    
    var loginInfo = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("LoginInfo");
    loginInfo.Gap = null;
    loginInfo.SetStyle("\"app-login-info placeholder-empty\"");
    
    var userInfoInstance = loginInfo.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var userInfo = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("UserInfo");
    userInfoInstance.SourceBlock = userInfo;
    
    var extendedProperty6 = advancedHtml.CreateExtendedProperty();
    extendedProperty6.Property = "class";
    extendedProperty6.SetValue("\"app-menu-content display-flex\"");
    
    var extendedProperty7 = advancedHtml.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"navigation\"");
    
    var extendedProperty8 = advancedHtml.CreateExtendedProperty();
    extendedProperty8.Property = "aria-label";
    extendedProperty8.SetValue("\"Main navigation\"");
    
    var container2 = menu.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"app-menu-overlay\"");
    
    var extendedProperty9 = container2.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"button\"");
    
    var event2 = container2.CreateEvent();
    event2.Event = "onclick";
    event2.Handler = hideMenu;
}
```
