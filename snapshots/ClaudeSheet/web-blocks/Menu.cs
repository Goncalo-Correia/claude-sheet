(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileBlock 'Menu' ***/
    var menu = common.CreateBlock("Menu");
    menu.Description = "Navigation element with links to the main screens of your application.";
    menu.HorizontalPosition = 4114;
    menu.Icon = null; // default value
    menu.IsDisabled = false; // default value
    menu.JavaScript = ""; // default value
    menu.SetOnRenderJavascript(null); // default value
    menu.Public = true;
    menu.Shareable = true;
    menu.StyleSheet = null; // default value
    menu.VerticalPosition = 2514;
    
    var getMenuAccess = menu.CreateDataAction("GetMenuAccess");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getMenuAccess.OutputParameters.ToList().ForEach(x => x.Delete());
    getMenuAccess.Description = "Which role-gated menu links the user may see (server-side role checks; referenced roles have no client-side check).";
    getMenuAccess.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getMenuAccess.ServerRequestTimeout = null; // default value
    
    var startNode = getMenuAccess.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var assignNode = getMenuAccess.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Check roles";
    
    var assignment = assignNode.CreateAssignment("IsSheetAdmin", "CheckSheetAdminRole()");
    
    var assignment2 = assignNode.CreateAssignment("IsSheetManager", "CheckSheetManagerRole()");
    
    var endNode = getMenuAccess.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    getMenuAccess.OnAfterFetch.Destination = null; // default value
    
    var isSheetAdmin = getMenuAccess.CreateOutputParameter("IsSheetAdmin");
    var eSpace = common.GetESpace();
    isSheetAdmin.DataType = eSpace.BooleanType;
    isSheetAdmin.SetDefaultValue(null); // default value
    isSheetAdmin.Description = null; // default value
    
    var isSheetManager = getMenuAccess.CreateOutputParameter("IsSheetManager");
    isSheetManager.DataType = eSpace.BooleanType;
    isSheetManager.SetDefaultValue(null); // default value
    isSheetManager.Description = null; // default value
    
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
    menu.OnInitialize.Destination = null; // default value
    
    var hideMenu = menu.CreateScreenAction("HideMenu");
    hideMenu.Description = null; // default value
    
    var startNode2 = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var onDestroy = menu.CreateScreenAction("OnDestroy");
    onDestroy.Description = null; // default value
    
    var startNode3 = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var menuDestroy = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("MenuDestroy");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var menuDestroy2 = outSystemsUI.ClientActions.Named("MenuDestroy");
    menuDestroy.Action = menuDestroy2;
    menuDestroy.HorizontalPosition = 3192;
    menuDestroy.IsDisabled = false; // default value
    menuDestroy.VerticalPosition = 2736;
    
    var endNode2 = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3196;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 5120;
    menu.OnDestroy.Destination = onDestroy;
    
    var onRender = menu.CreateScreenAction("OnRender");
    onRender.Description = null; // default value
    
    var startNode4 = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var setMenuListeners = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("SetMenuListeners");
    var setMenuListeners2 = outSystemsUI.ClientActions.Named("SetMenuListeners");
    setMenuListeners.Action = setMenuListeners2;
    setMenuListeners.HorizontalPosition = 3192;
    setMenuListeners.IsDisabled = false; // default value
    setMenuListeners.VerticalPosition = 2508;
    var widgetId = setMenuListeners2.InputParameters.Named("WidgetId");
    setMenuListeners.SetArgumentValue(widgetId, null); // default value
    
    var endNode3 = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 3200;
    endNode3.IsDisabled = false; // default value
    endNode3.VerticalPosition = 4343;
    menu.OnParametersChanged.Destination = onRender;
    
    var onReady = menu.CreateScreenAction("OnReady");
    onReady.Description = null; // default value
    
    var startNode5 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.IsDisabled = false; // default value
    
    var menuReady = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("MenuReady");
    var menuReady2 = outSystemsUI.ClientActions.Named("MenuReady");
    menuReady.Action = menuReady2;
    menuReady.HorizontalPosition = 3192;
    menuReady.IsDisabled = false; // default value
    menuReady.VerticalPosition = 2508;
    var callback = menuReady2.InputParameters.Named("Callback");
    menuReady.SetArgumentValue(callback, null); // default value
    
    var setActiveMenuItems = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("SetActiveMenuItems");
    var setActiveMenuItems2 = outSystemsUI.ClientActions.Named("SetActiveMenuItems");
    setActiveMenuItems.Action = setActiveMenuItems2;
    setActiveMenuItems.HorizontalPosition = 3200;
    setActiveMenuItems.IsDisabled = false; // default value
    setActiveMenuItems.VerticalPosition = 4343;
    var activeItem2 = setActiveMenuItems2.InputParameters.Named("ActiveItem");
    setActiveMenuItems.SetArgumentValue(activeItem2, "ActiveItem");
    var activeSubItem2 = setActiveMenuItems2.InputParameters.Named("ActiveSubItem");
    setActiveMenuItems.SetArgumentValue(activeSubItem2, "ActiveSubItem");
    var widgetId2 = setActiveMenuItems2.InputParameters.Named("WidgetId");
    setActiveMenuItems.SetArgumentValue(widgetId2, null); // default value
    
    var endNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(setActiveMenuItems, 1600);
    endNode4.IsDisabled = false; // default value
    menu.OnReady.Destination = onReady;
    menu.OnRender.Destination = onRender;
    
    var toggleSideMenu = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ToggleSideMenu");
    var toggleSideMenu2 = outSystemsUI.ClientActions.Named("ToggleSideMenu");
    toggleSideMenu.Action = toggleSideMenu2;
    toggleSideMenu.HorizontalPosition = 3192;
    toggleSideMenu.IsDisabled = false; // default value
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
    endNode5.IsDisabled = false; // default value
    endNode5.VerticalPosition = 5120;
    toggleSideMenu.Target = endNode5;
    
    var advancedHtml = menu.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
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
    advancedHtml.Tag = "nav";
    advancedHtml.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var container = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle("\"header-logo placeholder-empty\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var ifWidget = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("False");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowFalse;
    
    var applicationTitleInstance = ifWidget.FalseBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    applicationTitleInstance.CustomStyle = null; // default value
    applicationTitleInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var applicationTitle = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("ApplicationTitle");
    applicationTitleInstance.SourceBlock = applicationTitle;
    
    var pageLinks = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageLinks");
    pageLinks.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    pageLinks.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    pageLinks.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    pageLinks.Animate = false; // default value
    pageLinks.ColumnGap = null; // default value
    pageLinks.ColumnSpan = null; // default value
    pageLinks.CustomStyle = null; // default value
    pageLinks.FlexBasis = null; // default value
    pageLinks.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    pageLinks.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    pageLinks.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    pageLinks.Gap = null;
    pageLinks.GridColumns = null; // default value
    pageLinks.GridRows = null; // default value
    pageLinks.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    pageLinks.RowGap = null; // default value
    pageLinks.RowSpan = null; // default value
    pageLinks.SetStyle("\"app-menu-links\"");
    pageLinks.SetVisible("True"); // default value
    pageLinks.VisibleWasExpression = false; // default value
    pageLinks.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    pageLinks.Width = "(fill parent)"; // default value
    
    var mySheetLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("MySheetLink");
    mySheetLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    mySheetLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    mySheetLink.ColumnGap = null; // default value
    mySheetLink.ColumnSpan = null; // default value
    mySheetLink.SetConfirmationMessage(null); // default value
    mySheetLink.CustomStyle = null; // default value
    mySheetLink.SetEnabled("True");
    mySheetLink.FlexBasis = null; // default value
    mySheetLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    mySheetLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    mySheetLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    mySheetLink.Gap = null;
    mySheetLink.GridColumns = null; // default value
    mySheetLink.GridRows = null; // default value
    mySheetLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    mySheetLink.RowGap = null; // default value
    mySheetLink.RowSpan = null; // default value
    mySheetLink.SetStyle(null); // default value
    mySheetLink.SetVisible("True"); // default value
    mySheetLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    mySheetLink.Width = ""; // default value
    
    var textWidget = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)mySheetLink.Widgets.First();
    textWidget.CustomStyle = null; // default value
    textWidget.Name = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "My Sheet";
    var builtinEvent = mySheetLink.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var mainFlow = eSpace.MobileFlows.Named("MainFlow");
    var mySheet = mainFlow.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("MySheet");
    builtinEvent.Destination = mySheet;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty = mySheetLink.CreateExtendedProperty();
    extendedProperty.Property = "role";
    extendedProperty.SetValue("\"menuitem\"");
    
    var clientsLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ClientsLink");
    clientsLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clientsLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clientsLink.ColumnGap = null; // default value
    clientsLink.ColumnSpan = null; // default value
    clientsLink.SetConfirmationMessage(null); // default value
    clientsLink.CustomStyle = null; // default value
    clientsLink.SetEnabled("True");
    clientsLink.FlexBasis = null; // default value
    clientsLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clientsLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clientsLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clientsLink.Gap = null;
    clientsLink.GridColumns = null; // default value
    clientsLink.GridRows = null; // default value
    clientsLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clientsLink.RowGap = null; // default value
    clientsLink.RowSpan = null; // default value
    clientsLink.SetStyle(null); // default value
    clientsLink.SetVisible("GetMenuAccess.IsSheetAdmin");
    clientsLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clientsLink.Width = ""; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)clientsLink.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Clients";
    var builtinEvent2 = clientsLink.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var admin = eSpace.MobileFlows.Named("Admin");
    var clients = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Clients");
    builtinEvent2.Destination = clients;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = clientsLink.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"menuitem\"");
    
    var projectsLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ProjectsLink");
    projectsLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectsLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectsLink.ColumnGap = null; // default value
    projectsLink.ColumnSpan = null; // default value
    projectsLink.SetConfirmationMessage(null); // default value
    projectsLink.CustomStyle = null; // default value
    projectsLink.SetEnabled("True");
    projectsLink.FlexBasis = null; // default value
    projectsLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectsLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectsLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectsLink.Gap = null;
    projectsLink.GridColumns = null; // default value
    projectsLink.GridRows = null; // default value
    projectsLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectsLink.RowGap = null; // default value
    projectsLink.RowSpan = null; // default value
    projectsLink.SetStyle(null); // default value
    projectsLink.SetVisible("GetMenuAccess.IsSheetAdmin");
    projectsLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectsLink.Width = ""; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)projectsLink.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Projects";
    var builtinEvent3 = projectsLink.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var projects = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Projects");
    builtinEvent3.Destination = projects;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty3 = projectsLink.CreateExtendedProperty();
    extendedProperty3.Property = "role";
    extendedProperty3.SetValue("\"menuitem\"");
    
    var holidaysLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("HolidaysLink");
    holidaysLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidaysLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidaysLink.ColumnGap = null; // default value
    holidaysLink.ColumnSpan = null; // default value
    holidaysLink.SetConfirmationMessage(null); // default value
    holidaysLink.CustomStyle = null; // default value
    holidaysLink.SetEnabled("True");
    holidaysLink.FlexBasis = null; // default value
    holidaysLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidaysLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidaysLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidaysLink.Gap = null;
    holidaysLink.GridColumns = null; // default value
    holidaysLink.GridRows = null; // default value
    holidaysLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidaysLink.RowGap = null; // default value
    holidaysLink.RowSpan = null; // default value
    holidaysLink.SetStyle(null); // default value
    holidaysLink.SetVisible("GetMenuAccess.IsSheetAdmin");
    holidaysLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidaysLink.Width = ""; // default value
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)holidaysLink.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Holidays";
    var builtinEvent4 = holidaysLink.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var holidays = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Holidays");
    builtinEvent4.Destination = holidays;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty4 = holidaysLink.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"menuitem\"");
    
    var usersLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("UsersLink");
    usersLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    usersLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    usersLink.ColumnGap = null; // default value
    usersLink.ColumnSpan = null; // default value
    usersLink.SetConfirmationMessage(null); // default value
    usersLink.CustomStyle = null; // default value
    usersLink.SetEnabled("True");
    usersLink.FlexBasis = null; // default value
    usersLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    usersLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    usersLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    usersLink.Gap = null;
    usersLink.GridColumns = null; // default value
    usersLink.GridRows = null; // default value
    usersLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    usersLink.RowGap = null; // default value
    usersLink.RowSpan = null; // default value
    usersLink.SetStyle(null); // default value
    usersLink.SetVisible("GetMenuAccess.IsSheetAdmin");
    usersLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    usersLink.Width = ""; // default value
    
    var textWidget5 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)usersLink.Widgets.First();
    textWidget5.CustomStyle = null; // default value
    textWidget5.Name = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Users";
    var builtinEvent5 = usersLink.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var users = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Users");
    builtinEvent5.Destination = users;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty5 = usersLink.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"menuitem\"");
    
    var approvalsLink = pageLinks.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ApprovalsLink");
    approvalsLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    approvalsLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    approvalsLink.ColumnGap = null; // default value
    approvalsLink.ColumnSpan = null; // default value
    approvalsLink.SetConfirmationMessage(null); // default value
    approvalsLink.CustomStyle = null; // default value
    approvalsLink.SetEnabled("True");
    approvalsLink.FlexBasis = null; // default value
    approvalsLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    approvalsLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    approvalsLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    approvalsLink.Gap = null;
    approvalsLink.GridColumns = null; // default value
    approvalsLink.GridRows = null; // default value
    approvalsLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    approvalsLink.RowGap = null; // default value
    approvalsLink.RowSpan = null; // default value
    approvalsLink.SetStyle(null); // default value
    approvalsLink.SetVisible("GetMenuAccess.IsSheetManager");
    approvalsLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    approvalsLink.Width = ""; // default value
    
    var textWidget6 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)approvalsLink.Widgets.First();
    textWidget6.CustomStyle = null; // default value
    textWidget6.Name = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Approvals";
    var builtinEvent6 = approvalsLink.OnClick;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var approvals = mainFlow.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Approvals");
    builtinEvent6.Destination = approvals;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty6 = approvalsLink.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"menuitem\"");
    
    var extendedProperty7 = pageLinks.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"menubar\"");
    
    var loginInfo = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("LoginInfo");
    loginInfo.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    loginInfo.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    loginInfo.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    loginInfo.Animate = false; // default value
    loginInfo.ColumnGap = null; // default value
    loginInfo.ColumnSpan = null; // default value
    loginInfo.CustomStyle = null; // default value
    loginInfo.FlexBasis = null; // default value
    loginInfo.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    loginInfo.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    loginInfo.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    loginInfo.Gap = null;
    loginInfo.GridColumns = null; // default value
    loginInfo.GridRows = null; // default value
    loginInfo.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    loginInfo.RowGap = null; // default value
    loginInfo.RowSpan = null; // default value
    loginInfo.SetStyle("\"app-login-info placeholder-empty\"");
    loginInfo.SetVisible("True"); // default value
    loginInfo.VisibleWasExpression = false; // default value
    loginInfo.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    loginInfo.Width = "(fill parent)"; // default value
    
    var userInfoInstance = loginInfo.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    userInfoInstance.CustomStyle = null; // default value
    userInfoInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var userInfo = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("UserInfo");
    userInfoInstance.SourceBlock = userInfo;
    
    var extendedProperty8 = advancedHtml.CreateExtendedProperty();
    extendedProperty8.Property = "class";
    extendedProperty8.SetValue("\"app-menu-content display-flex\"");
    
    var extendedProperty9 = advancedHtml.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"navigation\"");
    
    var extendedProperty10 = advancedHtml.CreateExtendedProperty();
    extendedProperty10.Property = "aria-label";
    extendedProperty10.SetValue("\"Main navigation\"");
    
    var container2 = menu.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"app-menu-overlay\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var extendedProperty11 = container2.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"button\"");
    
    var event2 = container2.CreateEvent();
    event2.Event = "onclick";
    event2.Handler = hideMenu;
}
