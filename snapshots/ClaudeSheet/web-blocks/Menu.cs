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
    
    var activeItem = menu.CreateInputParameter("ActiveItem");
    var eSpace = common.GetESpace();
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
    
    var startNode = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var onDestroy = menu.CreateScreenAction("OnDestroy");
    onDestroy.Description = null; // default value
    
    var startNode2 = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var menuDestroy = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("MenuDestroy");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var menuDestroy2 = outSystemsUI.ClientActions.Named("MenuDestroy");
    menuDestroy.Action = menuDestroy2;
    menuDestroy.HorizontalPosition = 3192;
    menuDestroy.IsDisabled = false; // default value
    menuDestroy.VerticalPosition = 2736;
    
    var endNode = onDestroy.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 5120;
    menu.OnDestroy.Destination = onDestroy;
    
    var onRender = menu.CreateScreenAction("OnRender");
    onRender.Description = null; // default value
    
    var startNode3 = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var setMenuListeners = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("SetMenuListeners");
    var setMenuListeners2 = outSystemsUI.ClientActions.Named("SetMenuListeners");
    setMenuListeners.Action = setMenuListeners2;
    setMenuListeners.HorizontalPosition = 3192;
    setMenuListeners.IsDisabled = false; // default value
    setMenuListeners.VerticalPosition = 2508;
    var widgetId = setMenuListeners2.InputParameters.Named("WidgetId");
    setMenuListeners.SetArgumentValue(widgetId, null); // default value
    
    var endNode2 = onRender.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3200;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 4343;
    menu.OnParametersChanged.Destination = onRender;
    
    var onReady = menu.CreateScreenAction("OnReady");
    onReady.Description = null; // default value
    
    var startNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
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
    
    var endNode3 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(setActiveMenuItems, 1600);
    endNode3.IsDisabled = false; // default value
    menu.OnReady.Destination = onReady;
    menu.OnRender.Destination = onRender;
    
    var toggleSideMenu = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ToggleSideMenu");
    var toggleSideMenu2 = outSystemsUI.ClientActions.Named("ToggleSideMenu");
    toggleSideMenu.Action = toggleSideMenu2;
    toggleSideMenu.HorizontalPosition = 3192;
    toggleSideMenu.IsDisabled = false; // default value
    toggleSideMenu.VerticalPosition = 2736;
    startNode.Target = toggleSideMenu;
    startNode2.Target = menuDestroy;
    menuDestroy.Target = endNode;
    startNode3.Target = setMenuListeners;
    setMenuListeners.Target = endNode2;
    startNode4.Target = menuReady;
    menuReady.Target = setActiveMenuItems;
    
    var endNode4 = hideMenu.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3196;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 5120;
    toggleSideMenu.Target = endNode4;
    
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
    
    var extendedProperty2 = pageLinks.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"menubar\"");
    
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
    
    var extendedProperty3 = advancedHtml.CreateExtendedProperty();
    extendedProperty3.Property = "class";
    extendedProperty3.SetValue("\"app-menu-content display-flex\"");
    
    var extendedProperty4 = advancedHtml.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"navigation\"");
    
    var extendedProperty5 = advancedHtml.CreateExtendedProperty();
    extendedProperty5.Property = "aria-label";
    extendedProperty5.SetValue("\"Main navigation\"");
    
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
    
    var extendedProperty6 = container2.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"button\"");
    
    var event2 = container2.CreateEvent();
    event2.Event = "onclick";
    event2.Handler = hideMenu;
}
