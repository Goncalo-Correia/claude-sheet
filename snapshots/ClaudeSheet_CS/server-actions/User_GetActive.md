# User_GetActive

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'User_GetActive' ***/
    var user_GetActive = eSpace.CreateServerAction("User_GetActive");
    user_GetActive.Description = "Active users for a dropdown (SheetAdmin only): UserId, name and username, sorted by name. Used by the assignment popup in ClaudeSheet.";
    var user = eSpace.Folders.Named("User");
    user_GetActive.Folder = user;
    user_GetActive.Public = true;
    user_GetActive.Shareable = true;
    
    var startNode = user_GetActive.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var ifNode = user_GetActive.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = user_GetActive.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can list users.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getUsers = user_GetActive.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetUsers").Below(ifNode, 1371);
    getUsers.Statement = "SELECT {User}.[Id], {User}.[Name], {User}.[Username]\nFROM {User}\nWHERE {User}.[Is_Active] = 1\nORDER BY {User}.[Name]";
    
    var userOption = eSpace.Structures.Named("UserOption");
    var userOption2 = getUsers.CreateOutput(userOption, "UserOption");
    ifNode.TrueTarget = getUsers;
    
    var assignNode = user_GetActive.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getUsers, 1371);
    assignNode.Label = "Set output";
    
    var assignment = assignNode.CreateAssignment("Users", "GetUsers.List mapTo {  }");
    
    var endNode = user_GetActive.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var users = user_GetActive.CreateOutputParameter("Users");
    users.DataType = eSpace.GetOrCreateListType(userOption);
}
```
