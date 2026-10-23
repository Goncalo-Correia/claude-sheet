(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_Reject' ***/
    var monthSheet_Reject = eSpace.CreateServerAction("MonthSheet_Reject");
    monthSheet_Reject.CacheInMinutes = null; // default value
    monthSheet_Reject.Description = "Rejects a Submitted month with a Comment (SheetManager, Comment required): status Rejected, DecidedBy/DecidedOn set. The month is editable again and the user sees the comment. Raises PeriodValidation otherwise.";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_Reject.Folder = monthSheet;
    monthSheet_Reject.Function = false; // default value
    monthSheet_Reject.Icon = null; // default value
    monthSheet_Reject.Public = true;
    monthSheet_Reject.Shareable = true;
    
    var monthSheetId = monthSheet_Reject.CreateInputParameter("MonthSheetId");
    var monthSheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("MonthSheet");
    monthSheetId.DataType = monthSheet2.IdentifierType;
    monthSheetId.Description = null; // default value
    monthSheetId.IsMandatory = true; // default value
    
    var comment = monthSheet_Reject.CreateInputParameter("Comment");
    comment.DataType = eSpace.TextType;
    comment.Description = null; // default value
    comment.IsMandatory = true; // default value
    
    var rec = monthSheet_Reject.CreateLocalVariable("Rec");
    rec.DataType = monthSheet2;
    rec.Description = null; // default value
    
    var startNode = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetManagerRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetManager?";
    
    var raiseExceptionNode = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetManager");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only managers can approve, reject or reopen months.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("Trim(Comment) <> \"\"");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Comment given?";
    ifNode.TrueTarget = ifNode2;
    
    var raiseExceptionNode2 = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var periodValidation = eSpace.UserExceptions.Named("PeriodValidation");
    raiseExceptionNode2.Exception = periodValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Write a comment for the user explaining the rejection.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.FalseTarget = raiseExceptionNode2;
    
    var getMonthSheetForUpdate = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthSheetForUpdate").Below(ifNode2, 1371);
    getMonthSheetForUpdate.Action = monthSheet2.GetForUpdateAction;
    getMonthSheetForUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getMonthSheetForUpdate.IsDisabled = false; // default value
    getMonthSheetForUpdate.ServerRequestTimeout = null; // default value
    var getMonthSheetForUpdate2 = monthSheet2.GetForUpdateAction;
    var id = getMonthSheetForUpdate2.InputParameters.Named("Id");
    getMonthSheetForUpdate.SetArgumentValue(id, "MonthSheetId");
    ifNode2.TrueTarget = getMonthSheetForUpdate;
    
    var ifNode3 = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getMonthSheetForUpdate, 1371);
    ifNode3.SetCondition("GetMonthSheetForUpdate.Record.MonthSheet.PeriodStatusId = Entities.PeriodStatus.Submitted");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Submitted?";
    
    var raiseExceptionNode3 = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = periodValidation;
    raiseExceptionNode3.SetExceptionMessage("\"Only submitted months can be rejected.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.FalseTarget = raiseExceptionNode3;
    
    var assignNode = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode3, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set rejected";
    
    var assignment = assignNode.CreateAssignment("Rec", "GetMonthSheetForUpdate.Record");
    
    var assignment2 = assignNode.CreateAssignment("Rec.PeriodStatusId", "Entities.PeriodStatus.Rejected");
    
    var assignment3 = assignNode.CreateAssignment("Rec.DecidedBy", "GetUserId()");
    
    var assignment4 = assignNode.CreateAssignment("Rec.DecidedOn", "CurrDateTime()");
    
    var assignment5 = assignNode.CreateAssignment("Rec.Comment", "Trim(Comment)");
    ifNode3.TrueTarget = assignNode;
    
    var updateMonthSheet = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateMonthSheet").ConnectedBelow(assignNode, 1371);
    updateMonthSheet.Action = monthSheet2.UpdateAction;
    updateMonthSheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateMonthSheet.IsDisabled = false; // default value
    updateMonthSheet.ServerRequestTimeout = null; // default value
    var updateMonthSheet2 = monthSheet2.UpdateAction;
    var source = updateMonthSheet2.InputParameters.Named("Source");
    updateMonthSheet.SetArgumentValue(source, "Rec");
    
    var endNode = monthSheet_Reject.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateMonthSheet, 1371);
    endNode.IsDisabled = false; // default value
}
