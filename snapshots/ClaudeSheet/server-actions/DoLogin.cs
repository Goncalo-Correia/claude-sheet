(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DoLogin' ***/
    var doLogin = eSpace.CreateServerAction("DoLogin");
    doLogin.CacheInMinutes = null; // default value
    doLogin.Description = null; // default value
    var authentication = eSpace.Folders.Named("Authentication");
    doLogin.Folder = authentication;
    doLogin.Function = false; // default value
    doLogin.Icon = null; // default value
    doLogin.Public = false; // default value
    doLogin.Shareable = false; // default value
    
    var username = doLogin.CreateInputParameter("Username");
    username.DataType = eSpace.TextType;
    username.Description = null; // default value
    username.IsMandatory = true; // default value
    
    var password = doLogin.CreateInputParameter("Password");
    password.DataType = eSpace.TextType;
    password.Description = null; // default value
    password.IsMandatory = true; // default value
    
    var rememberLogin = doLogin.CreateInputParameter("RememberLogin");
    rememberLogin.DataType = eSpace.BooleanType;
    rememberLogin.SetDefaultValue(null); // default value
    rememberLogin.Description = null; // default value
    rememberLogin.IsMandatory = false;
    
    var startNode = doLogin.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var user_Login = doLogin.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_Login");
    var users = eSpace.References.Named("Users");
    var user_Login2 = users.ServerActions.Named("User_Login");
    user_Login.Action = user_Login2;
    user_Login.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    user_Login.HorizontalPosition = 3192;
    user_Login.IsDisabled = false; // default value
    user_Login.ServerRequestTimeout = null; // default value
    user_Login.VerticalPosition = 2971;
    var username2 = user_Login2.InputParameters.Named("Username");
    user_Login.SetArgumentValue(username2, "Username");
    var rememberLogin2 = user_Login2.InputParameters.Named("RememberLogin");
    user_Login.SetArgumentValue(rememberLogin2, "RememberLogin");
    var password2 = user_Login2.InputParameters.Named("Password");
    user_Login.SetArgumentValue(password2, "Password");
    startNode.Target = user_Login;
    
    var endNode = doLogin.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 5028;
    user_Login.Target = endNode;
    
    var commentNode = doLogin.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 4571;
    commentNode.IsDisabled = false; // default value
    commentNode.IsReminder = false; // default value
    commentNode.Text = "This action alllows you to define specific validation logic \nover the parameters supplied by the device before \ninvoking the User Provider login action.";
    commentNode.VerticalPosition = 1599;
}
