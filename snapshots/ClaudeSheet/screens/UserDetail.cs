(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileScreen 'UserDetail' ***/
    var userDetail = admin.CreateScreen("UserDetail");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    userDetail.Widgets.ToList().ForEach(x => x.Delete());
    userDetail.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    userDetail.Roles.Add(sheetAdmin);
    userDetail.AnonymousAccess = false; // default value
    userDetail.CustomURL = false; // default value
    userDetail.Description = "Admin detail of a user (SheetAdmin): ClaudeSheet roles (SheetUser, SheetAdmin, SheetManager) with Save, and the user\'s project assignments (AssignmentEditor).";
    userDetail.HorizontalPosition = 1600;
    userDetail.Icon = null; // default value
    userDetail.IsDisabled = false; // default value
    userDetail.PageName = null; // default value
    userDetail.Public = false; // default value
    userDetail.Shareable = false; // default value
    userDetail.StyleSheet = null; // default value
    userDetail.SetTitle("\"User\"");
    userDetail.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    userDetail.VerticalPosition = 3400;
    
    var getUser = userDetail.CreateDataAction("GetUser");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getUser.OutputParameters.ToList().ForEach(x => x.Delete());
    getUser.Description = null; // default value
    getUser.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getUser.ServerRequestTimeout = null; // default value
    
    var startNode = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var getUserById = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetUserById").ConnectedBelow(startNode, 1371);
    getUserById.CacheInMinutes = null; // default value
    getUserById.Description = null; // default value
    getUserById.IsDisabled = false; // default value
    getUserById.SetMaxRecords(null); // default value
    getUserById.SetStartIndex(null); // default value
    getUserById.Timeout = null; // default value
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getUserById.AsDatabaseAggregate.CreateSource(user2);
    getUserById.AsDatabaseAggregate.CreateFilter("User.Id = UserId");
    
    
    var ifNode = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getUserById, 1371);
    ifNode.SetCondition("GetUserById.List.Empty");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "User missing?";
    
    var assignNode = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Copy user";
    
    var assignment = assignNode.CreateAssignment("UserAccess.UserId", "GetUserById.List.Current.User.Id");
    
    var assignment2 = assignNode.CreateAssignment("UserAccess.Name", "GetUserById.List.Current.User.Name");
    
    var assignment3 = assignNode.CreateAssignment("UserAccess.Username", "GetUserById.List.Current.User.Username");
    
    var assignment4 = assignNode.CreateAssignment("UserAccess.Email", "GetUserById.List.Current.User.Email");
    
    var assignment5 = assignNode.CreateAssignment("UserAccess.IsActive", "GetUserById.List.Current.User.Is_Active");
    
    var assignment6 = assignNode.CreateAssignment("UserAccess.IsSheetUser", "CheckSheetUserRole(UserId)");
    
    var assignment7 = assignNode.CreateAssignment("UserAccess.IsSheetAdmin", "CheckSheetAdminRole(UserId)");
    
    var assignment8 = assignNode.CreateAssignment("UserAccess.IsSheetManager", "CheckSheetManagerRole(UserId)");
    ifNode.FalseTarget = assignNode;
    
    var assignNode2 = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Not found";
    
    var assignment9 = assignNode2.CreateAssignment("ErrorMessage", "\"User not found.\"");
    ifNode.TrueTarget = assignNode2;
    
    var endNode = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var endNode2 = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var allExceptions = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 8684;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode3 = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Keep error";
    
    var assignment10 = assignNode3.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode3 = getUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode3.IsDisabled = false; // default value
    
    var userAccess = getUser.CreateOutputParameter("UserAccess");
    var userAccess2 = claudeSheet_CS.Structures.Named("UserAccess");
    userAccess.DataType = userAccess2;
    userAccess.Description = null; // default value
    
    var onAfterFetch_GetUser = userDetail.CreateScreenAction("OnAfterFetch_GetUser");
    onAfterFetch_GetUser.Description = "Copies the user\'s roles into EditAccess, or shows the error when GetUser failed.";
    
    var startNode2 = onAfterFetch_GetUser.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode2 = onAfterFetch_GetUser.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode2.SetCondition("GetUser.ErrorMessage <> \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Failed?";
    
    var messageNode = onAfterFetch_GetUser.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ToTheRightOf(ifNode2, 1828);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetUser.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var assignNode4 = onAfterFetch_GetUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Copy access";
    
    var assignment11 = assignNode4.CreateAssignment("EditAccess", "GetUser.UserAccess");
    
    var endNode4 = onAfterFetch_GetUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    
    var endNode5 = onAfterFetch_GetUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    endNode5.IsDisabled = false; // default value
    getUser.OnAfterFetch.Destination = onAfterFetch_GetUser;
    ifNode2.FalseTarget = assignNode4;
    ifNode2.TrueTarget = messageNode;
    
    var errorMessage = getUser.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var userId = userDetail.CreateInputParameter("UserId");
    userId.DataType = user2.IdentifierType;
    userId.Description = null; // default value
    userId.IsMandatory = true; // default value
    
    var editAccess = userDetail.CreateLocalVariable("EditAccess");
    editAccess.DataType = userAccess2;
    editAccess.Description = "Editable copy of the user\'s roles, bound to the switches (filled by OnAfterFetch_GetUser).";
    
    var formError = userDetail.CreateLocalVariable("FormError");
    formError.DataType = eSpace.TextType;
    formError.SetDefaultValue(null); // default value
    formError.Description = "Message of the last failed save (e.g. AssignmentValidation from User_SetRoles), shown above the form.";
    userDetail.OnDestroy.Destination = null; // default value
    userDetail.OnInitialize.Destination = null; // default value
    userDetail.OnReady.Destination = null; // default value
    userDetail.OnRender.Destination = null; // default value
    
    var onClick_SaveButton = userDetail.CreateScreenAction("OnClick_SaveButton");
    onClick_SaveButton.Description = "Saves the role switches through User_SetRoles and reloads GetUser. AssignmentValidation (own SheetAdmin) or any other error is shown above the form.";
    
    var startNode3 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var assignNode5 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Clear error";
    
    var assignment12 = assignNode5.CreateAssignment("FormError", "\"\"");
    
    var user_SetRoles = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_SetRoles").ConnectedBelow(assignNode5, 1371);
    var user_SetRoles2 = claudeSheet_CS.ServerActions.Named("User_SetRoles");
    user_SetRoles.Action = user_SetRoles2;
    user_SetRoles.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_SetRoles.IsDisabled = false; // default value
    user_SetRoles.ServerRequestTimeout = null; // default value
    var isSheetUser = user_SetRoles2.InputParameters.Named("IsSheetUser");
    user_SetRoles.SetArgumentValue(isSheetUser, "EditAccess.IsSheetUser");
    var userId2 = user_SetRoles2.InputParameters.Named("UserId");
    user_SetRoles.SetArgumentValue(userId2, "UserId");
    var isSheetAdmin = user_SetRoles2.InputParameters.Named("IsSheetAdmin");
    user_SetRoles.SetArgumentValue(isSheetAdmin, "EditAccess.IsSheetAdmin");
    var isSheetManager = user_SetRoles2.InputParameters.Named("IsSheetManager");
    user_SetRoles.SetArgumentValue(isSheetManager, "EditAccess.IsSheetManager");
    
    var refreshDataNode = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(user_SetRoles, 1371);
    refreshDataNode.DataSource = getUser;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var messageNode2 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Show saved";
    messageNode2.SetMessage("\"Access saved. It applies from the user\'s next login.\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode6 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1371);
    endNode6.IsDisabled = false; // default value
    
    var allExceptions2 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode6 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Keep error";
    
    var assignment13 = assignNode6.CreateAssignment("FormError", "AllExceptions.ExceptionMessage");
    
    var endNode7 = onClick_SaveButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode6, 1371);
    endNode7.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = userDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "4");
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "User";
    
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
    textWidget2.Text = "Back to users";
    var builtinEvent = backLink.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var users = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Users");
    builtinEvent.Destination = users;
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
    
    var accessForm = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AccessForm");
    accessForm.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    accessForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    accessForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    accessForm.Animate = false; // default value
    accessForm.ColumnGap = null; // default value
    accessForm.ColumnSpan = null; // default value
    accessForm.CustomStyle = null; // default value
    accessForm.FlexBasis = null; // default value
    accessForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    accessForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    accessForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    accessForm.Gap = null;
    accessForm.GridColumns = null; // default value
    accessForm.GridRows = null; // default value
    accessForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    accessForm.RowGap = null; // default value
    accessForm.RowSpan = null; // default value
    accessForm.SetStyle("\"admin-form\"");
    accessForm.SetVisible("True"); // default value
    accessForm.VisibleWasExpression = false; // default value
    accessForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    accessForm.Width = "(fill parent)"; // default value
    
    var advancedHtml = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
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
    
    var expression2 = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetUser.UserAccess.Name");
    expression2.Width = ""; // default value
    
    var userInfo = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("UserInfo");
    userInfo.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    userInfo.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    userInfo.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    userInfo.Animate = false; // default value
    userInfo.ColumnGap = null; // default value
    userInfo.ColumnSpan = null; // default value
    userInfo.CustomStyle = null; // default value
    userInfo.FlexBasis = null; // default value
    userInfo.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    userInfo.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    userInfo.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    userInfo.Gap = null;
    userInfo.GridColumns = null; // default value
    userInfo.GridRows = null; // default value
    userInfo.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    userInfo.RowGap = null; // default value
    userInfo.RowSpan = null; // default value
    userInfo.SetStyle("\"admin-form__hint\"");
    userInfo.SetVisible("True"); // default value
    userInfo.VisibleWasExpression = false; // default value
    userInfo.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    userInfo.Width = "(fill parent)"; // default value
    
    var expression3 = userInfo.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("GetUser.UserAccess.Username + If(GetUser.UserAccess.Email <> \"\", \" - \" + GetUser.UserAccess.Email, \"\") + If(GetUser.UserAccess.IsActive, \"\", \" - inactive user\")");
    expression3.Width = ""; // default value
    
    var sheetUserField = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("SheetUserField");
    sheetUserField.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    sheetUserField.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    sheetUserField.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sheetUserField.Animate = false; // default value
    sheetUserField.ColumnGap = null; // default value
    sheetUserField.ColumnSpan = null; // default value
    sheetUserField.CustomStyle = null; // default value
    sheetUserField.FlexBasis = null; // default value
    sheetUserField.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    sheetUserField.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sheetUserField.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    sheetUserField.Gap = null;
    sheetUserField.GridColumns = null; // default value
    sheetUserField.GridRows = null; // default value
    sheetUserField.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    sheetUserField.RowGap = null; // default value
    sheetUserField.RowSpan = null; // default value
    sheetUserField.SetStyle("\"admin-form__switch\"");
    sheetUserField.SetVisible("True"); // default value
    sheetUserField.VisibleWasExpression = false; // default value
    sheetUserField.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    sheetUserField.Width = "(fill parent)"; // default value
    
    var sheetUserSwitch = sheetUserField.CreateWidget<ServiceStudio.Plugin.NRWidgets.ISwitch>("SheetUserSwitch");
    sheetUserSwitch.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sheetUserSwitch.ColumnSpan = null; // default value
    sheetUserSwitch.CustomStyle = null; // default value
    sheetUserSwitch.SetEnabled("GetUser.IsDataFetched");
    sheetUserSwitch.FlexBasis = null; // default value
    sheetUserSwitch.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sheetUserSwitch.RowSpan = null; // default value
    sheetUserSwitch.SetStyle("\"switch\""); // default value
    sheetUserSwitch.SetVariable("EditAccess.IsSheetUser");
    var builtinEvent2 = sheetUserSwitch.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = null; // default value
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = sheetUserSwitch.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"SheetUser: records time in MySheet\"");
    
    var textWidget3 = sheetUserField.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "SheetUser: records time in MySheet";
    
    var sheetAdminField = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("SheetAdminField");
    sheetAdminField.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    sheetAdminField.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    sheetAdminField.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sheetAdminField.Animate = false; // default value
    sheetAdminField.ColumnGap = null; // default value
    sheetAdminField.ColumnSpan = null; // default value
    sheetAdminField.CustomStyle = null; // default value
    sheetAdminField.FlexBasis = null; // default value
    sheetAdminField.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    sheetAdminField.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sheetAdminField.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    sheetAdminField.Gap = null;
    sheetAdminField.GridColumns = null; // default value
    sheetAdminField.GridRows = null; // default value
    sheetAdminField.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    sheetAdminField.RowGap = null; // default value
    sheetAdminField.RowSpan = null; // default value
    sheetAdminField.SetStyle("\"admin-form__switch\"");
    sheetAdminField.SetVisible("True"); // default value
    sheetAdminField.VisibleWasExpression = false; // default value
    sheetAdminField.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    sheetAdminField.Width = "(fill parent)"; // default value
    
    var sheetAdminSwitch = sheetAdminField.CreateWidget<ServiceStudio.Plugin.NRWidgets.ISwitch>("SheetAdminSwitch");
    sheetAdminSwitch.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sheetAdminSwitch.ColumnSpan = null; // default value
    sheetAdminSwitch.CustomStyle = null; // default value
    sheetAdminSwitch.SetEnabled("GetUser.IsDataFetched and UserId <> GetUserId()");
    sheetAdminSwitch.FlexBasis = null; // default value
    sheetAdminSwitch.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sheetAdminSwitch.RowSpan = null; // default value
    sheetAdminSwitch.SetStyle("\"switch\""); // default value
    sheetAdminSwitch.SetVariable("EditAccess.IsSheetAdmin");
    var builtinEvent3 = sheetAdminSwitch.OnChange;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = null; // default value
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty3 = sheetAdminSwitch.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"SheetAdmin: manages clients, projects, holidays and users\"");
    
    var textWidget4 = sheetAdminField.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "SheetAdmin: manages clients, projects, holidays and users";
    
    var sheetManagerField = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("SheetManagerField");
    sheetManagerField.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    sheetManagerField.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    sheetManagerField.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sheetManagerField.Animate = false; // default value
    sheetManagerField.ColumnGap = null; // default value
    sheetManagerField.ColumnSpan = null; // default value
    sheetManagerField.CustomStyle = null; // default value
    sheetManagerField.FlexBasis = null; // default value
    sheetManagerField.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    sheetManagerField.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sheetManagerField.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    sheetManagerField.Gap = null;
    sheetManagerField.GridColumns = null; // default value
    sheetManagerField.GridRows = null; // default value
    sheetManagerField.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    sheetManagerField.RowGap = null; // default value
    sheetManagerField.RowSpan = null; // default value
    sheetManagerField.SetStyle("\"admin-form__switch\"");
    sheetManagerField.SetVisible("True"); // default value
    sheetManagerField.VisibleWasExpression = false; // default value
    sheetManagerField.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    sheetManagerField.Width = "(fill parent)"; // default value
    
    var sheetManagerSwitch = sheetManagerField.CreateWidget<ServiceStudio.Plugin.NRWidgets.ISwitch>("SheetManagerSwitch");
    sheetManagerSwitch.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sheetManagerSwitch.ColumnSpan = null; // default value
    sheetManagerSwitch.CustomStyle = null; // default value
    sheetManagerSwitch.SetEnabled("GetUser.IsDataFetched");
    sheetManagerSwitch.FlexBasis = null; // default value
    sheetManagerSwitch.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sheetManagerSwitch.RowSpan = null; // default value
    sheetManagerSwitch.SetStyle("\"switch\""); // default value
    sheetManagerSwitch.SetVariable("EditAccess.IsSheetManager");
    var builtinEvent4 = sheetManagerSwitch.OnChange;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = null; // default value
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty4 = sheetManagerSwitch.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"SheetManager: team view, approvals and reports\"");
    
    var textWidget5 = sheetManagerField.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "SheetManager: team view, approvals and reports";
    
    var selfHint = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("SelfHint");
    selfHint.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    selfHint.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    selfHint.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    selfHint.Animate = true;
    selfHint.ColumnGap = null; // default value
    selfHint.ColumnSpan = null; // default value
    selfHint.CustomStyle = null; // default value
    selfHint.FlexBasis = null; // default value
    selfHint.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    selfHint.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    selfHint.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    selfHint.Gap = null;
    selfHint.GridColumns = null; // default value
    selfHint.GridRows = null; // default value
    selfHint.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    selfHint.RowGap = null; // default value
    selfHint.RowSpan = null; // default value
    selfHint.SetStyle("\"admin-form__hint\"");
    selfHint.SetVisible("UserId = GetUserId()");
    selfHint.VisibleWasExpression = true;
    selfHint.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    selfHint.Width = "(fill parent)"; // default value
    
    var textWidget6 = selfHint.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "You can\'t remove your own administrator access.";
    
    var loginHint = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("LoginHint");
    loginHint.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    loginHint.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    loginHint.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    loginHint.Animate = false; // default value
    loginHint.ColumnGap = null; // default value
    loginHint.ColumnSpan = null; // default value
    loginHint.CustomStyle = null; // default value
    loginHint.FlexBasis = null; // default value
    loginHint.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    loginHint.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    loginHint.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    loginHint.Gap = null;
    loginHint.GridColumns = null; // default value
    loginHint.GridRows = null; // default value
    loginHint.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    loginHint.RowGap = null; // default value
    loginHint.RowSpan = null; // default value
    loginHint.SetStyle("\"admin-form__hint\"");
    loginHint.SetVisible("True"); // default value
    loginHint.VisibleWasExpression = false; // default value
    loginHint.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    loginHint.Width = "(fill parent)"; // default value
    
    var textWidget7 = loginHint.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Changes apply from the user\'s next login.";
    
    var formActions = accessForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FormActions");
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
    saveButton.SetEnabled("GetUser.IsDataFetched and GetUser.ErrorMessage = \"\"");
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
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveButton.Widgets.First();
    textWidget8.CustomStyle = null; // default value
    textWidget8.Name = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Save access";
    var builtinEvent5 = saveButton.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = onClick_SaveButton;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var userAssignments = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("UserAssignments");
    userAssignments.CustomStyle = null; // default value
    userAssignments.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var assignmentEditor = admin.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("AssignmentEditor");
    userAssignments.SourceBlock = assignmentEditor;
    var projectId = assignmentEditor.InputParameters.Named("ProjectId");
    userAssignments.SetArgumentValue(projectId, "NullIdentifier()");
    var userId3 = assignmentEditor.InputParameters.Named("UserId");
    userAssignments.SetArgumentValue(userId3, "UserId");
}
