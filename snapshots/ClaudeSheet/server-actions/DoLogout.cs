(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DoLogout' ***/
    var doLogout = eSpace.CreateServerAction("DoLogout");
    doLogout.CacheInMinutes = null; // default value
    doLogout.Description = null; // default value
    var authentication = eSpace.Folders.Named("Authentication");
    doLogout.Folder = authentication;
    doLogout.Function = false; // default value
    doLogout.Icon = null; // default value
    doLogout.Public = false; // default value
    doLogout.Shareable = false; // default value
    
    var startNode = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var user_GetUnifiedLogoutUrl = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetUnifiedLogoutUrl").ConnectedBelow(startNode, 1371);
    var users = eSpace.References.Named("Users");
    var user_GetUnifiedLogoutUrl2 = users.ServerActions.Named("User_GetUnifiedLogoutUrl");
    user_GetUnifiedLogoutUrl.Action = user_GetUnifiedLogoutUrl2;
    user_GetUnifiedLogoutUrl.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_GetUnifiedLogoutUrl.IsDisabled = false; // default value
    user_GetUnifiedLogoutUrl.ServerRequestTimeout = null; // default value
    var originalUrl = user_GetUnifiedLogoutUrl2.InputParameters.Named("OriginalUrl");
    user_GetUnifiedLogoutUrl.SetArgumentValue(originalUrl, "If(GetExceptionURL() = \"\", GetOwnerURLPath(), GetExceptionURL())");
    
    var ifNode = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(user_GetUnifiedLogoutUrl, 1371);
    ifNode.SetCondition("User_GetUnifiedLogoutUrl.Url <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Found Unified Logout URL";
    
    var user_Logout = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_Logout").Below(ifNode, 1371);
    var user_Logout2 = users.ServerActions.Named("User_Logout");
    user_Logout.Action = user_Logout2;
    user_Logout.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_Logout.IsDisabled = false; // default value
    user_Logout.ServerRequestTimeout = null; // default value
    ifNode.FalseTarget = user_Logout;
    
    var assignNode = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set redirect URL";
    
    var assignment = assignNode.CreateAssignment("RedirectURL", "User_GetUnifiedLogoutUrl.Url");
    ifNode.TrueTarget = assignNode;
    
    var user_GetUnifiedLoginUrl = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_GetUnifiedLoginUrl").ConnectedBelow(user_Logout, 1371);
    var user_GetUnifiedLoginUrl2 = users.ServerActions.Named("User_GetUnifiedLoginUrl");
    user_GetUnifiedLoginUrl.Action = user_GetUnifiedLoginUrl2;
    user_GetUnifiedLoginUrl.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_GetUnifiedLoginUrl.IsDisabled = false; // default value
    user_GetUnifiedLoginUrl.ServerRequestTimeout = null; // default value
    var originalUrl2 = user_GetUnifiedLoginUrl2.InputParameters.Named("OriginalUrl");
    user_GetUnifiedLoginUrl.SetArgumentValue(originalUrl2, "If(GetExceptionURL() = \"\", GetOwnerURLPath(), GetExceptionURL())");
    var toolName = user_GetUnifiedLoginUrl2.InputParameters.Named("ToolName");
    user_GetUnifiedLoginUrl.SetArgumentValue(toolName, null); // default value
    
    var endNode = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var ifNode2 = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(user_GetUnifiedLoginUrl, 1371);
    ifNode2.SetCondition("User_GetUnifiedLoginUrl.Url <> \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Found Unified Login URL";
    
    var endNode2 = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    endNode2.IsDisabled = false; // default value
    ifNode2.FalseTarget = endNode2;
    
    var assignNode2 = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Set redirect URL";
    
    var assignment2 = assignNode2.CreateAssignment("RedirectURL", "User_GetUnifiedLoginUrl.Url");
    ifNode2.TrueTarget = assignNode2;
    
    var endNode3 = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode3.IsDisabled = false; // default value
    
    var commentNode = doLogout.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 6856;
    commentNode.IsDisabled = false; // default value
    commentNode.IsReminder = false; // default value
    commentNode.Text = "This action alllows you to define specific validation logic \nover the parameters supplied by the device before \ninvoking the User Provider logout action.";
    commentNode.VerticalPosition = 800;
    
    var redirectURL = doLogout.CreateOutputParameter("RedirectURL");
    redirectURL.DataType = eSpace.TextType;
    redirectURL.SetDefaultValue(null); // default value
    redirectURL.Description = "Returns the Url used for custom unified logout patterns.";
}
