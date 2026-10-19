# UserInfo

Block in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileBlock 'UserInfo' ***/
    var userInfo = common.CreateBlock("UserInfo");
    userInfo.Description = "Displays the currently logged in user and allows to log out. If no user is logged in, it links to the login screen.";
    userInfo.HorizontalPosition = 457;
    userInfo.Public = true;
    userInfo.Shareable = true;
    userInfo.VerticalPosition = 2514;
    
    var getUserNameFromServer = userInfo.CreateDataAction("GetUserNameFromServer");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getUserNameFromServer.OutputParameters.ToList().ForEach(x => x.Delete());
    getUserNameFromServer.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.OnDemand;
    
    var startNode = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var user_GetName = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetName");
    var eSpace = common.GetESpace();
    var users = eSpace.References.Named("Users");
    var user_GetName2 = users.ServerActions.Named("User_GetName");
    user_GetName.Action = user_GetName2;
    user_GetName.HorizontalPosition = 3192;
    user_GetName.VerticalPosition = 2508;
    startNode.Target = user_GetName;
    
    var assignNode = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(user_GetName, 1140);
    assignNode.Label = "Out1";
    
    var assignment = assignNode.CreateAssignment("UserName", "User_GetName.Name");
    
    var endNode = getUserNameFromServer.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.VerticalPosition = 5120;
    assignNode.Target = endNode;
    
    var userName = getUserNameFromServer.CreateOutputParameter("UserName");
    userName.DataType = eSpace.TextType;
    
    var clientLogin = userInfo.CreateScreenAction("ClientLogin");
    
    var startNode2 = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var onInitialize = userInfo.CreateScreenAction("OnInitialize");
    
    var startNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 1141;
    startNode3.VerticalPosition = 1141;
    
    var getUsername = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("GetUsername");
    getUsername.HorizontalPosition = 3199;
    getUsername.JavaScript = "$actions.GetUsername();";
    getUsername.VerticalPosition = 1142;
    
    var endNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3202;
    endNode2.VerticalPosition = 3200;
    userInfo.OnInitialize.Destination = onInitialize;
    
    var user_GetUnifiedLoginUrl = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetUnifiedLoginUrl");
    var user_GetUnifiedLoginUrl2 = users.ServerActions.Named("User_GetUnifiedLoginUrl");
    user_GetUnifiedLoginUrl.Action = user_GetUnifiedLoginUrl2;
    user_GetUnifiedLoginUrl.HorizontalPosition = 3192;
    user_GetUnifiedLoginUrl.VerticalPosition = 2742;
    var originalUrl = user_GetUnifiedLoginUrl2.InputParameters.Named("OriginalUrl");
    user_GetUnifiedLoginUrl.SetArgumentValue(originalUrl, "GetBookmarkableURL()");
    var toolName = user_GetUnifiedLoginUrl2.InputParameters.Named("ToolName");
    startNode2.Target = user_GetUnifiedLoginUrl;
    startNode3.Target = getUsername;
    getUsername.Target = endNode2;
    
    var ifNode = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(user_GetUnifiedLoginUrl, 1829);
    ifNode.SetCondition("User_GetUnifiedLoginUrl.Url <> \"\"");
    ifNode.Label = "Found Unified Login URL";
    
    var destinationNode = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().Below(ifNode, 1828);
    var login = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Login");
    destinationNode.Destination = login;
    ifNode.FalseTarget = destinationNode;
    
    var destinationNode2 = clientLogin.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode, 1605);
    var redirectToURL = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IExternalSite>().Named("RedirectToURL");
    destinationNode2.Destination = redirectToURL;
    var uRL = redirectToURL.InputParameters.Named("URL");
    destinationNode2.SetArgumentValue(uRL, "User_GetUnifiedLoginUrl.Url");
    ifNode.TrueTarget = destinationNode2;
    
    var clientLogout = userInfo.CreateScreenAction("ClientLogout");
    
    var startNode4 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var doLogout = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DoLogout").ConnectedBelow(startNode4, 1942);
    var doLogout2 = eSpace.ServerActions.Named("DoLogout");
    doLogout.Action = doLogout2;
    
    var ifNode2 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode2.SetCondition("DoLogout.RedirectURL <> \"\"");
    ifNode2.HorizontalPosition = 3192;
    ifNode2.Label = "Found Logout URL";
    ifNode2.VerticalPosition = 4560;
    doLogout.Target = ifNode2;
    
    var destinationNode3 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>();
    destinationNode3.Destination = login;
    destinationNode3.HorizontalPosition = 3196;
    destinationNode3.VerticalPosition = 6384;
    ifNode2.FalseTarget = destinationNode3;
    
    var destinationNode4 = clientLogout.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode2, 1833);
    destinationNode4.Destination = redirectToURL;
    destinationNode4.SetArgumentValue(uRL, "DoLogout.RedirectURL");
    ifNode2.TrueTarget = destinationNode4;
    
    var getUsername2 = userInfo.CreateScreenAction("GetUsername");
    
    var endNode3 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 5485;
    endNode3.VerticalPosition = 1140;
    
    var startNode5 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 1141;
    startNode5.VerticalPosition = 1141;
    
    var ifNode3 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode3.SetCondition("Client.Username = \"\" and GetUserId() <> NullIdentifier()");
    ifNode3.FalseTarget = endNode3;
    ifNode3.HorizontalPosition = 3428;
    ifNode3.Label = "No Username?";
    ifNode3.VerticalPosition = 1142;
    startNode5.Target = ifNode3;
    
    var refreshDataNode = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>();
    refreshDataNode.DataSource = getUserNameFromServer;
    refreshDataNode.HorizontalPosition = 3420;
    refreshDataNode.VerticalPosition = 3192;
    ifNode3.TrueTarget = refreshDataNode;
    
    var assignNode2 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(refreshDataNode, 1377);
    assignNode2.Label = "Set username";
    
    var assignment2 = assignNode2.CreateAssignment("Client.Username", "GetUserNameFromServer.UserName");
    
    var endNode4 = getUsername2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3428;
    endNode4.VerticalPosition = 6169;
    assignNode2.Target = endNode4;
    
    var container = userInfo.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"user-info\"");
    
    var ifWidget = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.SetCondition("GetUserId() <> NullIdentifier()");
    
    var container2 = ifWidget.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    
    var image = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.CustomStyle = "height: 24px;";
    var user = eSpace.Images.Named("User");
    image.Image = user;
    image.SetStyle("\"img-circle\"");
    image.Width = "24px";
    
    var extendedProperty = image.CreateExtendedProperty();
    extendedProperty.Property = "alt";
    extendedProperty.SetValue("\"\"");
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"margin-left-s\"");
    
    var expression = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.Example = "Username";
    expression.SetValue("Client.Username");
    
    var container4 = ifWidget.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"margin-left-s\"");
    
    var link = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link.Widgets.ToList().ForEach(x => x.Delete());
    link.SetEnabled("True");
    link.Gap = null;
    link.Width = "(fill parent)";
    
    var icon3 = link.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("Icon3");
    icon3.Icon = "sign-out";
    icon3.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    
    var textWidget = link.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.SetStyleClasses("\"margin-left-s wcag-hide-text\"");
    textWidget.Text = "Log out";
    var builtinEvent = link.OnClick;
    builtinEvent.Destination = clientLogout;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var link2 = ifWidget.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link2.Widgets.ToList().ForEach(x => x.Delete());
    link2.SetEnabled("True");
    link2.Gap = null;
    link2.Width = "(fill parent)";
    
    var icon4 = link2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("Icon4");
    icon4.Icon = "sign-in";
    icon4.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    
    var textWidget2 = link2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.SetStyleClasses("\"margin-left-s\"");
    textWidget2.Text = "Login";
    var builtinEvent2 = link2.OnClick;
    builtinEvent2.Destination = clientLogin;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
}
```
