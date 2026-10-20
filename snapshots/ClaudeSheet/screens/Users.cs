(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileScreen 'Users' ***/
    var users = admin.CreateScreen("Users");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    users.Widgets.ToList().ForEach(x => x.Delete());
    users.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    users.Roles.Add(sheetAdmin);
    users.AnonymousAccess = false; // default value
    users.CustomURL = false; // default value
    users.Description = "Admin list of users (SheetAdmin): search, only users with access, roles as tags, active assignments; opens UserDetail.";
    users.HorizontalPosition = 400;
    users.Icon = null; // default value
    users.IsDisabled = false; // default value
    users.PageName = null; // default value
    users.Public = false; // default value
    users.Shareable = false; // default value
    users.StyleSheet = null; // default value
    users.SetTitle("\"Users\"");
    users.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    users.VerticalPosition = 3400;
    
    var getUsers = users.CreateDataAction("GetUsers");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getUsers.OutputParameters.ToList().ForEach(x => x.Delete());
    getUsers.Description = null; // default value
    getUsers.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getUsers.ServerRequestTimeout = null; // default value
    
    var startNode = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var user_GetList = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetList").ConnectedBelow(startNode, 1371);
    var user_GetList2 = claudeSheet_CS.ServerActions.Named("User_GetList");
    user_GetList.Action = user_GetList2;
    user_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_GetList.IsDisabled = false; // default value
    user_GetList.ServerRequestTimeout = null; // default value
    var onlyWithAccess = user_GetList2.InputParameters.Named("OnlyWithAccess");
    user_GetList.SetArgumentValue(onlyWithAccess, "OnlyWithAccess");
    var search = user_GetList2.InputParameters.Named("Search");
    user_GetList.SetArgumentValue(search, "Search");
    
    var assignNode = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(user_GetList, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set users";
    
    var assignment = assignNode.CreateAssignment("Users", "User_GetList.Users");
    
    var endNode = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var allExceptions = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var users2 = getUsers.CreateOutputParameter("Users");
    var userAccess = claudeSheet_CS.Structures.Named("UserAccess");
    users2.DataType = eSpace.GetOrCreateListType(userAccess);
    users2.Description = null; // default value
    
    var onAfterFetch_GetUsers = users.CreateScreenAction("OnAfterFetch_GetUsers");
    onAfterFetch_GetUsers.Description = "Shows an error message when GetUsers failed.";
    
    var startNode2 = onAfterFetch_GetUsers.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetUsers.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetUsers.ErrorMessage <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode3.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetUsers.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetUsers.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    getUsers.OnAfterFetch.Destination = onAfterFetch_GetUsers;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getUsers.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var search2 = users.CreateLocalVariable("Search");
    search2.DataType = eSpace.TextType;
    search2.SetDefaultValue(null); // default value
    search2.Description = null; // default value
    
    var onlyWithAccess2 = users.CreateLocalVariable("OnlyWithAccess");
    onlyWithAccess2.DataType = eSpace.BooleanType;
    onlyWithAccess2.SetDefaultValue("False");
    onlyWithAccess2.Description = null; // default value
    users.OnDestroy.Destination = null; // default value
    users.OnInitialize.Destination = null; // default value
    users.OnReady.Destination = null; // default value
    users.OnRender.Destination = null; // default value
    
    var onChange_Filters = users.CreateScreenAction("OnChange_Filters");
    onChange_Filters.Description = "OnChange of SearchInput and OnlyWithAccessCheckbox: reloads GetUsers.";
    
    var startNode3 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var refreshDataNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getUsers;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode5 = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode5.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = users.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopMenuInstance.CustomStyle = null; // default value
    layoutTopMenuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    layoutTopMenuInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "4");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Users";
    
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
    extendedProperty.SetValue("\"Search name, username or email\"");
    
    var extendedProperty2 = searchInput.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Search users\"");
    
    var onlyWithAccessWrap = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OnlyWithAccessWrap");
    onlyWithAccessWrap.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    onlyWithAccessWrap.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    onlyWithAccessWrap.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    onlyWithAccessWrap.Animate = false; // default value
    onlyWithAccessWrap.ColumnGap = null; // default value
    onlyWithAccessWrap.ColumnSpan = null; // default value
    onlyWithAccessWrap.CustomStyle = null; // default value
    onlyWithAccessWrap.FlexBasis = null; // default value
    onlyWithAccessWrap.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    onlyWithAccessWrap.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    onlyWithAccessWrap.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    onlyWithAccessWrap.Gap = null;
    onlyWithAccessWrap.GridColumns = null; // default value
    onlyWithAccessWrap.GridRows = null; // default value
    onlyWithAccessWrap.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    onlyWithAccessWrap.RowGap = null; // default value
    onlyWithAccessWrap.RowSpan = null; // default value
    onlyWithAccessWrap.SetStyle("\"admin-toolbar__check\"");
    onlyWithAccessWrap.SetVisible("True"); // default value
    onlyWithAccessWrap.VisibleWasExpression = false; // default value
    onlyWithAccessWrap.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    onlyWithAccessWrap.Width = "(fill parent)"; // default value
    
    var onlyWithAccessCheckbox = onlyWithAccessWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("OnlyWithAccessCheckbox");
    onlyWithAccessCheckbox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    onlyWithAccessCheckbox.ColumnSpan = null; // default value
    onlyWithAccessCheckbox.CustomStyle = null; // default value
    onlyWithAccessCheckbox.SetEnabled("True");
    onlyWithAccessCheckbox.FlexBasis = null; // default value
    onlyWithAccessCheckbox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    onlyWithAccessCheckbox.RowSpan = null; // default value
    onlyWithAccessCheckbox.SetStyle("\"checkbox\""); // default value
    onlyWithAccessCheckbox.SetVariable("OnlyWithAccess");
    var builtinEvent2 = onlyWithAccessCheckbox.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = onChange_Filters;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty3 = onlyWithAccessCheckbox.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Only users with access\"");
    
    var textWidget2 = onlyWithAccessWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Only users with access";
    
    var usersTable = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("UsersTable");
    usersTable.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    usersTable.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    usersTable.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    usersTable.Animate = false; // default value
    usersTable.ColumnGap = null; // default value
    usersTable.ColumnSpan = null; // default value
    usersTable.CustomStyle = null; // default value
    usersTable.FlexBasis = null; // default value
    usersTable.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    usersTable.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    usersTable.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    usersTable.Gap = null;
    usersTable.GridColumns = null; // default value
    usersTable.GridRows = null; // default value
    usersTable.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    usersTable.RowGap = null; // default value
    usersTable.RowSpan = null; // default value
    usersTable.SetStyle("\"admin-table admin-table--4\"");
    usersTable.SetVisible("True"); // default value
    usersTable.VisibleWasExpression = false; // default value
    usersTable.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    usersTable.Width = "(fill parent)"; // default value
    
    var usersHeader = usersTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("UsersHeader");
    usersHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    usersHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    usersHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    usersHeader.Animate = false; // default value
    usersHeader.ColumnGap = null; // default value
    usersHeader.ColumnSpan = null; // default value
    usersHeader.CustomStyle = null; // default value
    usersHeader.FlexBasis = null; // default value
    usersHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    usersHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    usersHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    usersHeader.Gap = null;
    usersHeader.GridColumns = null; // default value
    usersHeader.GridRows = null; // default value
    usersHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    usersHeader.RowGap = null; // default value
    usersHeader.RowSpan = null; // default value
    usersHeader.SetStyle("\"admin-table__row admin-table__head\"");
    usersHeader.SetVisible("True"); // default value
    usersHeader.VisibleWasExpression = false; // default value
    usersHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    usersHeader.Width = "(fill parent)"; // default value
    
    var container = usersHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("\"Name\"");
    expression.Width = ""; // default value
    
    var extendedProperty4 = container.CreateExtendedProperty();
    extendedProperty4.Property = "role";
    extendedProperty4.SetValue("\"columnheader\"");
    
    var container2 = usersHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression2 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("\"Username\"");
    expression2.Width = ""; // default value
    
    var extendedProperty5 = container2.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var container3 = usersHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression3 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("\"Access\"");
    expression3.Width = ""; // default value
    
    var extendedProperty6 = container3.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var container4 = usersHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression4 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("\"Active assignments\"");
    expression4.Width = ""; // default value
    
    var extendedProperty7 = container4.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"columnheader\"");
    
    var extendedProperty8 = usersHeader.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"row\"");
    
    var hasUsers = usersTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasUsers");
    hasUsers.Animate = false; // default value
    hasUsers.SetCondition("GetUsers.Users.Length > 0 or not GetUsers.IsDataFetched");
    hasUsers.CustomStyle = null; // default value
    hasUsers.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var userList = hasUsers.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("UserList");
    userList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    userList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    userList.AnimateItems = true; // default value
    userList.ColumnGap = null; // default value
    userList.ColumnSpan = null; // default value
    userList.CustomStyle = null; // default value
    userList.ExpandedInWebEditor = true; // default value
    userList.FlexBasis = null; // default value
    userList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    userList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    userList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    userList.Gap = null;
    userList.GridColumns = null; // default value
    userList.GridRows = null; // default value
    userList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    userList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    userList.RowGap = null; // default value
    userList.RowSpan = null; // default value
    userList.SetSource("GetUsers.Users");
    userList.SetStyle("\"admin-table__body\"");
    userList.Tag = "div"; // default value
    userList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    userList.Width = "(fill parent)"; // default value
    
    var userRow = userList.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("UserRow");
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    userRow.Widgets.ToList().ForEach(x => x.Delete());
    userRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    userRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    userRow.ColumnGap = null; // default value
    userRow.ColumnSpan = null; // default value
    userRow.SetConfirmationMessage(null); // default value
    userRow.CustomStyle = null; // default value
    userRow.SetEnabled("True");
    userRow.FlexBasis = null; // default value
    userRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    userRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    userRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    userRow.Gap = null;
    userRow.GridColumns = null; // default value
    userRow.GridRows = null; // default value
    userRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    userRow.RowGap = null; // default value
    userRow.RowSpan = null; // default value
    userRow.SetStyle("\"admin-table__row\"");
    userRow.SetVisible("True"); // default value
    userRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    userRow.Width = ""; // default value
    
    var container5 = userRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"admin-table__cell admin-table__name\" + If(GetUsers.Users.Current.IsActive, \"\", \" is-inactive\")");
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
    expression5.SetValue("GetUsers.Users.Current.Name");
    expression5.Width = ""; // default value
    
    var extendedProperty9 = container5.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var container6 = userRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression6 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("GetUsers.Users.Current.Username");
    expression6.Width = ""; // default value
    
    var extendedProperty10 = container6.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"cell\"");
    
    var accessTags = userRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AccessTags");
    accessTags.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    accessTags.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    accessTags.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    accessTags.Animate = false; // default value
    accessTags.ColumnGap = null; // default value
    accessTags.ColumnSpan = null; // default value
    accessTags.CustomStyle = null; // default value
    accessTags.FlexBasis = null; // default value
    accessTags.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    accessTags.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    accessTags.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    accessTags.Gap = null;
    accessTags.GridColumns = null; // default value
    accessTags.GridRows = null; // default value
    accessTags.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    accessTags.RowGap = null; // default value
    accessTags.RowSpan = null; // default value
    accessTags.SetStyle("\"admin-table__cell admin-tags\"");
    accessTags.SetVisible("True"); // default value
    accessTags.VisibleWasExpression = false; // default value
    accessTags.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    accessTags.Width = "(fill parent)"; // default value
    
    var container7 = accessTags.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container7.Animate = true;
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
    container7.SetStyle("\"admin-tag\"");
    container7.SetVisible("GetUsers.Users.Current.IsSheetUser");
    container7.VisibleWasExpression = true;
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var textWidget3 = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "SheetUser";
    
    var container8 = accessTags.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = true;
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
    container8.SetStyle("\"admin-tag admin-tag--admin\"");
    container8.SetVisible("GetUsers.Users.Current.IsSheetAdmin");
    container8.VisibleWasExpression = true;
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var textWidget4 = container8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "SheetAdmin";
    
    var container9 = accessTags.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container9.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container9.Animate = true;
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
    container9.SetStyle("\"admin-tag\"");
    container9.SetVisible("GetUsers.Users.Current.IsSheetManager");
    container9.VisibleWasExpression = true;
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var textWidget5 = container9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "SheetManager";
    
    var container10 = accessTags.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container10.Animate = true;
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
    container10.SetStyle("\"admin-tag admin-tag--none\"");
    container10.SetVisible("not GetUsers.Users.Current.IsSheetUser and not GetUsers.Users.Current.IsSheetAdmin and not GetUsers.Users.Current.IsSheetManager");
    container10.VisibleWasExpression = true;
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var textWidget6 = container10.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "No access";
    
    var extendedProperty11 = accessTags.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"cell\"");
    
    var container11 = userRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container11.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container11.Animate = false; // default value
    container11.ColumnGap = null; // default value
    container11.ColumnSpan = null; // default value
    container11.CustomStyle = null; // default value
    container11.FlexBasis = null; // default value
    container11.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container11.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container11.Gap = null;
    container11.GridColumns = null; // default value
    container11.GridRows = null; // default value
    container11.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container11.RowGap = null; // default value
    container11.RowSpan = null; // default value
    container11.SetStyle("\"admin-table__cell\"");
    container11.SetVisible("True"); // default value
    container11.VisibleWasExpression = false; // default value
    container11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container11.Width = "(fill parent)"; // default value
    
    var expression7 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("GetUsers.Users.Current.ActiveAssignments");
    expression7.Width = ""; // default value
    
    var extendedProperty12 = container11.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"cell\"");
    var builtinEvent3 = userRow.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var userDetail = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("UserDetail");
    builtinEvent3.Destination = userDetail;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var userId = userDetail.InputParameters.Named("UserId");
    builtinEvent3.SetArgumentValue(userId, "GetUsers.Users.Current.UserId");
    
    var extendedProperty13 = userRow.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"row\"");
    var builtinEvent4 = userList.OnScrollEnding;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = null; // default value
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var noUsers = hasUsers.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoUsers");
    noUsers.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noUsers.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noUsers.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noUsers.Animate = false; // default value
    noUsers.ColumnGap = null; // default value
    noUsers.ColumnSpan = null; // default value
    noUsers.CustomStyle = null; // default value
    noUsers.FlexBasis = null; // default value
    noUsers.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noUsers.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noUsers.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noUsers.Gap = null;
    noUsers.GridColumns = null; // default value
    noUsers.GridRows = null; // default value
    noUsers.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noUsers.RowGap = null; // default value
    noUsers.RowSpan = null; // default value
    noUsers.SetStyle("\"admin-table__empty\"");
    noUsers.SetVisible("True"); // default value
    noUsers.VisibleWasExpression = false; // default value
    noUsers.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noUsers.Width = "(fill parent)"; // default value
    
    var textWidget7 = noUsers.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "No users found.";
    
    var extendedProperty14 = usersTable.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"table\"");
}
