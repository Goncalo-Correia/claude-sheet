# DoLogin

Server action in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'DoLogin' ***/
    var doLogin = eSpace.CreateServerAction("DoLogin");
    var authentication = eSpace.Folders.Named("Authentication");
    doLogin.Folder = authentication;
    
    var username = doLogin.CreateInputParameter("Username");
    username.DataType = eSpace.TextType;
    
    var password = doLogin.CreateInputParameter("Password");
    password.DataType = eSpace.TextType;
    
    var rememberLogin = doLogin.CreateInputParameter("RememberLogin");
    rememberLogin.DataType = eSpace.BooleanType;
    rememberLogin.IsMandatory = false;
    
    var startNode = doLogin.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var user_Login = doLogin.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("User_Login");
    var users = eSpace.References.Named("Users");
    var user_Login2 = users.ServerActions.Named("User_Login");
    user_Login.Action = user_Login2;
    user_Login.HorizontalPosition = 3192;
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
    endNode.VerticalPosition = 5028;
    user_Login.Target = endNode;
    
    var commentNode = doLogin.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 4571;
    commentNode.Text = "This action alllows you to define specific validation logic \nover the parameters supplied by the device before \ninvoking the User Provider login action.";
    commentNode.VerticalPosition = 1599;
}
```
