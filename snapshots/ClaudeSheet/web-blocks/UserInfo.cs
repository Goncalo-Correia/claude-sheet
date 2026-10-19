(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileBlock 'UserInfo' ***/
    var userInfo = common.CreateBlock("UserInfo");
    userInfo.Description = "Displays the currently logged in user and allows to log out. If no user is logged in, it links to the login screen.";
    userInfo.HorizontalPosition = 457;
    userInfo.Icon = null; // default value
    userInfo.IsDisabled = false; // default value
    userInfo.JavaScript = ""; // default value
    userInfo.SetOnRenderJavascript(null); // default value
    userInfo.Public = true;
    userInfo.Shareable = true;
    userInfo.StyleSheet = null; // default value
    userInfo.VerticalPosition = 2514;
    
    var getUserNameFromServer = userInfo.CreateDataAction("GetUserNameFromServer");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getUserNameFromServer.OutputParameters.ToList().ForEach(x => x.Delete());
    getUserNameFromServer.Description = null; // default value
    getUserNameFromServer.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.OnDemand;
    getUserNameFromServer.ServerRequestTimeout = null; // default value
    
    var startNode = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var user_GetName = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetName");
    var eSpace = common.GetESpace();
    var users = eSpace.References.Named("Users");
    var user_GetName2 = users.ServerActions.Named("User_GetName");
    user_GetName.Action = user_GetName2;
    user_GetName.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_GetName.HorizontalPosition = 3192;
    user_GetName.IsDisabled = false; // default value
    user_GetName.ServerRequestTimeout = null; // default value
    user_GetName.VerticalPosition = 2508;
    startNode.Target = user_GetName;
    
    var assignNode = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(user_GetName, 1140);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Out1";
    
    var assignment = assignNode.CreateAssignment("UserName", "User_GetName.Name");
    
    var endNode = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 5120;
    assignNode.Target = endNode;
    getUserNameFromServer.OnAfterFetch.Destination = null; // default value
    
    var userName = getUserNameFromServer.CreateOutputParameter("UserName");
    userName.DataType = eSpace.TextType;
    userName.SetDefaultValue(null); // default value
    userName.Description = null; // default value
    userInfo.OnDestroy.Destination = null; // default value
    userInfo.OnParametersChanged.Destination = null; // default value
    userInfo.OnReady.Destination = null; // default value
    userInfo.OnRender.Destination = null; // default value
    
    var clientLogin = userInfo.CreateScreenAction("ClientLogin");
    clientLogin.Description = null; // default value
    
    var startNode2 = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var onInitialize = userInfo.CreateScreenAction("OnInitialize");
    onInitialize.Description = null; // default value
    
    var startNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 1141;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 1141;
    
    var getUsername = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("GetUsername");
    getUsername.Description = null; // default value
    getUsername.HorizontalPosition = 3199;
    getUsername.IsDisabled = false; // default value
    getUsername.JavaScript = "$actions.GetUsername();";
    getUsername.VerticalPosition = 1142;
    
    var endNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3202;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 3200;
    userInfo.OnInitialize.Destination = onInitialize;
    
    var user_GetUnifiedLoginUrl = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetUnifiedLoginUrl");
    var user_GetUnifiedLoginUrl2 = users.ServerActions.Named("User_GetUnifiedLoginUrl");
    user_GetUnifiedLoginUrl.Action = user_GetUnifiedLoginUrl2;
    user_GetUnifiedLoginUrl.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_GetUnifiedLoginUrl.HorizontalPosition = 3192;
    user_GetUnifiedLoginUrl.IsDisabled = false; // default value
    user_GetUnifiedLoginUrl.ServerRequestTimeout = null; // default value
    user_GetUnifiedLoginUrl.VerticalPosition = 2742;
    var originalUrl = user_GetUnifiedLoginUrl2.InputParameters.Named("OriginalUrl");
    user_GetUnifiedLoginUrl.SetArgumentValue(originalUrl, "GetBookmarkableURL()");
    var toolName = user_GetUnifiedLoginUrl2.InputParameters.Named("ToolName");
    user_GetUnifiedLoginUrl.SetArgumentValue(toolName, null); // default value
    startNode2.Target = user_GetUnifiedLoginUrl;
    startNode3.Target = getUsername;
    getUsername.Target = endNode2;
    
    var ifNode = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(user_GetUnifiedLoginUrl, 1829);
    ifNode.SetCondition("User_GetUnifiedLoginUrl.Url <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Found Unified Login URL";
    
    var destinationNode = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().Below(ifNode, 1828);
    var login = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Login");
    destinationNode.Destination = login;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    ifNode.FalseTarget = destinationNode;
    
    var destinationNode2 = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode, 1605);
    var redirectToURL = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IExternalSite>().Named("RedirectToURL");
    destinationNode2.Destination = redirectToURL;
    destinationNode2.IsDisabled = false; // default value
    destinationNode2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var uRL = redirectToURL.InputParameters.Named("URL");
    destinationNode2.SetArgumentValue(uRL, "User_GetUnifiedLoginUrl.Url");
    ifNode.TrueTarget = destinationNode2;
    
    var clientLogout = userInfo.CreateScreenAction("ClientLogout");
    clientLogout.Description = null; // default value
    
    var startNode4 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var doLogout = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DoLogout").ConnectedBelow(startNode4, 1942);
    var doLogout2 = eSpace.ServerActions.Named("DoLogout");
    doLogout.Action = doLogout2;
    doLogout.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    doLogout.IsDisabled = false; // default value
    doLogout.ServerRequestTimeout = null; // default value
    
    var ifNode2 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode2.SetCondition("DoLogout.RedirectURL <> \"\"");
    ifNode2.HorizontalPosition = 3192;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Found Logout URL";
    ifNode2.VerticalPosition = 4560;
    doLogout.Target = ifNode2;
    
    var destinationNode3 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>();
    destinationNode3.Destination = login;
    destinationNode3.HorizontalPosition = 3196;
    destinationNode3.IsDisabled = false; // default value
    destinationNode3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    destinationNode3.VerticalPosition = 6384;
    ifNode2.FalseTarget = destinationNode3;
    
    var destinationNode4 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode2, 1833);
    destinationNode4.Destination = redirectToURL;
    destinationNode4.IsDisabled = false; // default value
    destinationNode4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    destinationNode4.SetArgumentValue(uRL, "DoLogout.RedirectURL");
    ifNode2.TrueTarget = destinationNode4;
    
    var getUsername2 = userInfo.CreateScreenAction("GetUsername");
    getUsername2.Description = null; // default value
    
    var endNode3 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 5485;
    endNode3.IsDisabled = false; // default value
    endNode3.VerticalPosition = 1140;
    
    var startNode5 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 1141;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 1141;
    
    var ifNode3 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode3.SetCondition("Client.Username = \"\" and GetUserId() <> NullIdentifier()");
    ifNode3.FalseTarget = endNode3;
    ifNode3.HorizontalPosition = 3428;
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "No Username?";
    ifNode3.VerticalPosition = 1142;
    startNode5.Target = ifNode3;
    
    var refreshDataNode = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>();
    refreshDataNode.DataSource = getUserNameFromServer;
    refreshDataNode.HorizontalPosition = 3420;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    refreshDataNode.VerticalPosition = 3192;
    ifNode3.TrueTarget = refreshDataNode;
    
    var assignNode2 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(refreshDataNode, 1377);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Set username";
    
    var assignment2 = assignNode2.CreateAssignment("Client.Username", "GetUserNameFromServer.UserName");
    
    var endNode4 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3428;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 6169;
    assignNode2.Target = endNode4;
    
    var container = userInfo.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle("\"user-info\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var ifWidget = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("GetUserId() <> NullIdentifier()");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container2 = ifWidget.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle(null); // default value
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var image = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = "height: 24px;";
    image.DefaultImage = null; // default value
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var user = eSpace.Images.Named("User");
    image.Image = user;
    image.SetImageContent(null); // default value
    image.RowSpan = null; // default value
    image.SetStyle("\"img-circle\"");
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Static; // default value
    image.SetUrl(null); // default value
    image.Width = "24px";
    
    var extendedProperty = image.CreateExtendedProperty();
    extendedProperty.Property = "alt";
    extendedProperty.SetValue("\"\"");
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"margin-left-s\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var expression = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = "Username";
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("Client.Username");
    expression.Width = ""; // default value
    
    var container4 = ifWidget.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"margin-left-s\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var link = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link.Widgets.ToList().ForEach(x => x.Delete());
    link.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link.ColumnGap = null; // default value
    link.ColumnSpan = null; // default value
    link.SetConfirmationMessage(null); // default value
    link.CustomStyle = null; // default value
    link.SetEnabled("True");
    link.FlexBasis = null; // default value
    link.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link.Gap = null;
    link.GridColumns = null; // default value
    link.GridRows = null; // default value
    link.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link.RowGap = null; // default value
    link.RowSpan = null; // default value
    link.SetStyle(null); // default value
    link.SetVisible("True"); // default value
    link.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link.Width = "(fill parent)";
    
    var icon3 = link.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("Icon3");
    icon3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon3.ColumnSpan = null; // default value
    icon3.CustomStyle = null; // default value
    icon3.FlexBasis = null; // default value
    icon3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon3.Icon = "sign-out";
    icon3.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon3.RowSpan = null; // default value
    icon3.SetStyle("\"icon\""); // default value
    icon3.SetVisible("True"); // default value
    icon3.Weight = null; // default value
    
    var textWidget = link.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses("\"margin-left-s wcag-hide-text\"");
    textWidget.Text = "Log out";
    var builtinEvent = link.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = clientLogout;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var link2 = ifWidget.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link2.Widgets.ToList().ForEach(x => x.Delete());
    link2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link2.ColumnGap = null; // default value
    link2.ColumnSpan = null; // default value
    link2.SetConfirmationMessage(null); // default value
    link2.CustomStyle = null; // default value
    link2.SetEnabled("True");
    link2.FlexBasis = null; // default value
    link2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link2.Gap = null;
    link2.GridColumns = null; // default value
    link2.GridRows = null; // default value
    link2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link2.RowGap = null; // default value
    link2.RowSpan = null; // default value
    link2.SetStyle(null); // default value
    link2.SetVisible("True"); // default value
    link2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link2.Width = "(fill parent)";
    
    var icon4 = link2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("Icon4");
    icon4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon4.ColumnSpan = null; // default value
    icon4.CustomStyle = null; // default value
    icon4.FlexBasis = null; // default value
    icon4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon4.Icon = "sign-in";
    icon4.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon4.RowSpan = null; // default value
    icon4.SetStyle("\"icon\""); // default value
    icon4.SetVisible("True"); // default value
    icon4.Weight = null; // default value
    
    var textWidget2 = link2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses("\"margin-left-s\"");
    textWidget2.Text = "Login";
    var builtinEvent2 = link2.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = clientLogin;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
}
