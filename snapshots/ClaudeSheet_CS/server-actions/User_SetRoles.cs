(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'User_SetRoles' ***/
    var user_SetRoles = eSpace.CreateServerAction("User_SetRoles");
    user_SetRoles.CacheInMinutes = null; // default value
    user_SetRoles.Description = "Grants or revokes SheetUser, SheetAdmin and SheetManager for a user (SheetAdmin only). An admin can\'t remove their own SheetAdmin. Role changes apply from the user\'s next login.";
    var user = eSpace.Folders.Named("User");
    user_SetRoles.Folder = user;
    user_SetRoles.Function = false; // default value
    user_SetRoles.Icon = null; // default value
    user_SetRoles.Public = true;
    user_SetRoles.Shareable = true;
    
    var userId = user_SetRoles.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId.DataType = user2.IdentifierType;
    userId.Description = null; // default value
    userId.IsMandatory = true; // default value
    
    var isSheetUser = user_SetRoles.CreateInputParameter("IsSheetUser");
    isSheetUser.DataType = eSpace.BooleanType;
    isSheetUser.Description = null; // default value
    isSheetUser.IsMandatory = true; // default value
    
    var isSheetAdmin = user_SetRoles.CreateInputParameter("IsSheetAdmin");
    isSheetAdmin.DataType = eSpace.BooleanType;
    isSheetAdmin.Description = null; // default value
    isSheetAdmin.IsMandatory = true; // default value
    
    var isSheetManager = user_SetRoles.CreateInputParameter("IsSheetManager");
    isSheetManager.DataType = eSpace.BooleanType;
    isSheetManager.Description = null; // default value
    isSheetManager.IsMandatory = true; // default value
    
    var startNode = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetAdminRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetAdmin?";
    
    var raiseExceptionNode = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only administrators can change user access.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getUser = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetUser").Below(ifNode, 1371);
    getUser.CacheInMinutes = null; // default value
    getUser.Description = null; // default value
    getUser.IsDisabled = false; // default value
    getUser.SetMaxRecords(null); // default value
    getUser.SetStartIndex(null); // default value
    getUser.Timeout = null; // default value
    var user3 = getUser.AsDatabaseAggregate.CreateSource(user2);
    getUser.AsDatabaseAggregate.CreateFilter("User.Id = UserId");
    
    ifNode.TrueTarget = getUser;
    
    var ifNode2 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getUser, 1371);
    ifNode2.SetCondition("GetUser.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "User missing?";
    
    var ifNode3 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("UserId = GetUserId() and not IsSheetAdmin");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Removing own SheetAdmin?";
    ifNode2.FalseTarget = ifNode3;
    
    var raiseExceptionNode2 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var assignmentValidation = eSpace.UserExceptions.Named("AssignmentValidation");
    raiseExceptionNode2.Exception = assignmentValidation;
    raiseExceptionNode2.SetExceptionMessage("\"User not found.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode4 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode3, 1371);
    ifNode4.SetCondition("IsSheetUser");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Grant SheetUser?";
    ifNode3.FalseTarget = ifNode4;
    
    var raiseExceptionNode3 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = assignmentValidation;
    raiseExceptionNode3.SetExceptionMessage("\"You can\'t remove your own administrator access.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var revokeSheetUserRole = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("RevokeSheetUserRole").Below(ifNode4, 1371);
    var sheetUser = eSpace.Roles.Named("SheetUser");
    var revokeSheetUserRole2 = sheetUser.RoleActions.Named("RevokeSheetUserRole");
    revokeSheetUserRole.Action = revokeSheetUserRole2;
    revokeSheetUserRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    revokeSheetUserRole.IsDisabled = false; // default value
    revokeSheetUserRole.ServerRequestTimeout = null; // default value
    var userId2 = revokeSheetUserRole2.InputParameters.Named("UserId");
    revokeSheetUserRole.SetArgumentValue(userId2, "UserId");
    ifNode4.FalseTarget = revokeSheetUserRole;
    
    var grantSheetUserRole = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetUserRole").ToTheRightOf(ifNode4, 1828);
    var grantSheetUserRole2 = sheetUser.RoleActions.Named("GrantSheetUserRole");
    grantSheetUserRole.Action = grantSheetUserRole2;
    grantSheetUserRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    grantSheetUserRole.IsDisabled = false; // default value
    grantSheetUserRole.ServerRequestTimeout = null; // default value
    var userId3 = grantSheetUserRole2.InputParameters.Named("UserId");
    grantSheetUserRole.SetArgumentValue(userId3, "UserId");
    ifNode4.TrueTarget = grantSheetUserRole;
    
    var ifNode5 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(revokeSheetUserRole, 1371);
    ifNode5.SetCondition("IsSheetAdmin");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "Grant SheetAdmin?";
    grantSheetUserRole.Target = ifNode5;
    
    var revokeSheetAdminRole = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("RevokeSheetAdminRole").Below(ifNode5, 1371);
    var sheetAdmin = eSpace.Roles.Named("SheetAdmin");
    var revokeSheetAdminRole2 = sheetAdmin.RoleActions.Named("RevokeSheetAdminRole");
    revokeSheetAdminRole.Action = revokeSheetAdminRole2;
    revokeSheetAdminRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    revokeSheetAdminRole.IsDisabled = false; // default value
    revokeSheetAdminRole.ServerRequestTimeout = null; // default value
    var userId4 = revokeSheetAdminRole2.InputParameters.Named("UserId");
    revokeSheetAdminRole.SetArgumentValue(userId4, "UserId");
    ifNode5.FalseTarget = revokeSheetAdminRole;
    
    var grantSheetAdminRole = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetAdminRole").ToTheRightOf(ifNode5, 1828);
    var grantSheetAdminRole2 = sheetAdmin.RoleActions.Named("GrantSheetAdminRole");
    grantSheetAdminRole.Action = grantSheetAdminRole2;
    grantSheetAdminRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    grantSheetAdminRole.IsDisabled = false; // default value
    grantSheetAdminRole.ServerRequestTimeout = null; // default value
    var userId5 = grantSheetAdminRole2.InputParameters.Named("UserId");
    grantSheetAdminRole.SetArgumentValue(userId5, "UserId");
    ifNode5.TrueTarget = grantSheetAdminRole;
    
    var ifNode6 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(revokeSheetAdminRole, 1371);
    ifNode6.SetCondition("IsSheetManager");
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = "Grant SheetManager?";
    grantSheetAdminRole.Target = ifNode6;
    
    var revokeSheetManagerRole = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("RevokeSheetManagerRole").Below(ifNode6, 1371);
    var sheetManager = eSpace.Roles.Named("SheetManager");
    var revokeSheetManagerRole2 = sheetManager.RoleActions.Named("RevokeSheetManagerRole");
    revokeSheetManagerRole.Action = revokeSheetManagerRole2;
    revokeSheetManagerRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    revokeSheetManagerRole.IsDisabled = false; // default value
    revokeSheetManagerRole.ServerRequestTimeout = null; // default value
    var userId6 = revokeSheetManagerRole2.InputParameters.Named("UserId");
    revokeSheetManagerRole.SetArgumentValue(userId6, "UserId");
    ifNode6.FalseTarget = revokeSheetManagerRole;
    
    var grantSheetManagerRole = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetManagerRole").ToTheRightOf(ifNode6, 1828);
    var grantSheetManagerRole2 = sheetManager.RoleActions.Named("GrantSheetManagerRole");
    grantSheetManagerRole.Action = grantSheetManagerRole2;
    grantSheetManagerRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    grantSheetManagerRole.IsDisabled = false; // default value
    grantSheetManagerRole.ServerRequestTimeout = null; // default value
    var userId7 = grantSheetManagerRole2.InputParameters.Named("UserId");
    grantSheetManagerRole.SetArgumentValue(userId7, "UserId");
    ifNode6.TrueTarget = grantSheetManagerRole;
    
    var endNode = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(revokeSheetManagerRole, 1371);
    endNode.IsDisabled = false; // default value
    
    var endNode2 = user_SetRoles.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(grantSheetManagerRole, 1371);
    endNode2.IsDisabled = false; // default value
}
