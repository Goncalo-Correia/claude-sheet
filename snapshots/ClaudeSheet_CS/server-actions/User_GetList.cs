(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'User_GetList' ***/
    var user_GetList = eSpace.CreateServerAction("User_GetList");
    user_GetList.CacheInMinutes = null; // default value
    user_GetList.Description = "Users with their ClaudeSheet roles and active assignment count (SheetAdmin only). Search matches name, username or email; OnlyWithAccess keeps users with at least one of SheetUser, SheetAdmin, SheetManager. Sorted by name.";
    var user = eSpace.Folders.Named("User");
    user_GetList.Folder = user;
    user_GetList.Function = false; // default value
    user_GetList.Icon = null; // default value
    user_GetList.Public = true;
    user_GetList.Shareable = true;
    
    var search = user_GetList.CreateInputParameter("Search");
    search.DataType = eSpace.TextType;
    search.SetDefaultValue(null); // default value
    search.Description = null; // default value
    search.IsMandatory = false;
    
    var onlyWithAccess = user_GetList.CreateInputParameter("OnlyWithAccess");
    onlyWithAccess.DataType = eSpace.BooleanType;
    onlyWithAccess.SetDefaultValue(null); // default value
    onlyWithAccess.Description = null; // default value
    onlyWithAccess.IsMandatory = false;
    
    var startNode = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can list users.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getUsers = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetUsers").Below(ifNode, 1371);
    getUsers.CacheInMinutes = null; // default value
    getUsers.Description = null; // default value
    getUsers.IsDisabled = false; // default value
    getUsers.SetMaxRecords(null); // default value
    getUsers.Statement = "SELECT {User}.[Id], {User}.[Name], {User}.[Username], {User}.[Email], {User}.[Is_Active],\n  CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit),\n  (SELECT COUNT(*) FROM {ProjectAssignment}\n   WHERE {ProjectAssignment}.[UserId] = {User}.[Id] AND {ProjectAssignment}.[IsActive] = 1\n     AND ({ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] <= \'1900-01-01\' OR {ProjectAssignment}.[EndDate] >= @Today))\nFROM {User}\nWHERE @Search = \'\'\n   OR CHARINDEX(@Search, {User}.[Name]) > 0\n   OR CHARINDEX(@Search, {User}.[Username]) > 0\n   OR CHARINDEX(@Search, {User}.[Email]) > 0\nORDER BY {User}.[Name]";
    getUsers.Timeout = null; // default value
    
    var search2 = getUsers.CreateInputParameter("Search");
    search2.DataType = eSpace.TextType;
    search2.SetDefaultValue(null); // default value
    search2.Description = null; // default value
    search2.ExpandInline = false; // default value
    
    var today = getUsers.CreateInputParameter("Today");
    today.DataType = eSpace.DateType;
    today.SetDefaultValue(null); // default value
    today.Description = null; // default value
    today.ExpandInline = false; // default value
    
    var userAccess = eSpace.Structures.Named("UserAccess");
    var userAccess2 = getUsers.CreateOutput(userAccess, "UserAccess");
    ifNode.TrueTarget = getUsers;
    
    var forEachNode = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getUsers, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = "Each user";
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetUsers.List");
    forEachNode.SetStartIndex(null); // default value
    getUsers.SetArgumentValue(today, "CurrDate()");
    getUsers.SetArgumentValue(search2, "Trim(Search)");
    
    var assignNode = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Read roles";
    
    var assignment = assignNode.CreateAssignment("GetUsers.List.Current.UserAccess.IsSheetUser", "CheckSheetUserRole(GetUsers.List.Current.UserAccess.UserId)");
    
    var assignment2 = assignNode.CreateAssignment("GetUsers.List.Current.UserAccess.IsSheetAdmin", "CheckSheetAdminRole(GetUsers.List.Current.UserAccess.UserId)");
    
    var assignment3 = assignNode.CreateAssignment("GetUsers.List.Current.UserAccess.IsSheetManager", "CheckSheetManagerRole(GetUsers.List.Current.UserAccess.UserId)");
    forEachNode.CycleTarget = assignNode;
    
    var endNode = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 4113);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var ifNode2 = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode2.SetCondition("not OnlyWithAccess or GetUsers.List.Current.UserAccess.IsSheetUser or GetUsers.List.Current.UserAccess.IsSheetAdmin or GetUsers.List.Current.UserAccess.IsSheetManager");
    ifNode2.FalseTarget = forEachNode;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Include user?";
    
    var listAppend = user_GetList.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").Below(ifNode2, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    listAppend.Target = forEachNode;
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Users");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "GetUsers.List.Current.UserAccess");
    ifNode2.TrueTarget = listAppend;
    
    var users = user_GetList.CreateOutputParameter("Users");
    users.DataType = eSpace.GetOrCreateListType(userAccess);
    users.Description = null; // default value
}
