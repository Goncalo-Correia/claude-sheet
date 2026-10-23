(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_Approve' ***/
    var monthSheet_Approve = eSpace.CreateServerAction("MonthSheet_Approve");
    monthSheet_Approve.CacheInMinutes = null; // default value
    monthSheet_Approve.Description = "Approves a Submitted month (SheetManager): status Approved, DecidedBy/DecidedOn set, comment cleared. The month stays locked. Raises PeriodValidation when the month isn\'t Submitted.";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_Approve.Folder = monthSheet;
    monthSheet_Approve.Function = false; // default value
    monthSheet_Approve.Icon = null; // default value
    monthSheet_Approve.Public = true;
    monthSheet_Approve.Shareable = true;
    
    var monthSheetId = monthSheet_Approve.CreateInputParameter("MonthSheetId");
    var monthSheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("MonthSheet");
    monthSheetId.DataType = monthSheet2.IdentifierType;
    monthSheetId.Description = null; // default value
    monthSheetId.IsMandatory = true; // default value
    
    var rec = monthSheet_Approve.CreateLocalVariable("Rec");
    rec.DataType = monthSheet2;
    rec.Description = null; // default value
    
    var startNode = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetManagerRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetManager?";
    
    var raiseExceptionNode = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetManager");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only managers can approve, reject or reopen months.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getMonthSheetForUpdate = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthSheetForUpdate").Below(ifNode, 1371);
    getMonthSheetForUpdate.Action = monthSheet2.GetForUpdateAction;
    getMonthSheetForUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getMonthSheetForUpdate.IsDisabled = false; // default value
    getMonthSheetForUpdate.ServerRequestTimeout = null; // default value
    var getMonthSheetForUpdate2 = monthSheet2.GetForUpdateAction;
    var id = getMonthSheetForUpdate2.InputParameters.Named("Id");
    getMonthSheetForUpdate.SetArgumentValue(id, "MonthSheetId");
    ifNode.TrueTarget = getMonthSheetForUpdate;
    
    var ifNode2 = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getMonthSheetForUpdate, 1371);
    ifNode2.SetCondition("GetMonthSheetForUpdate.Record.MonthSheet.PeriodStatusId = Entities.PeriodStatus.Submitted");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Submitted?";
    
    var raiseExceptionNode2 = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var periodValidation = eSpace.UserExceptions.Named("PeriodValidation");
    raiseExceptionNode2.Exception = periodValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Only submitted months can be approved.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.FalseTarget = raiseExceptionNode2;
    
    var assignNode = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set approved";
    
    var assignment = assignNode.CreateAssignment("Rec", "GetMonthSheetForUpdate.Record");
    
    var assignment2 = assignNode.CreateAssignment("Rec.PeriodStatusId", "Entities.PeriodStatus.Approved");
    
    var assignment3 = assignNode.CreateAssignment("Rec.DecidedBy", "GetUserId()");
    
    var assignment4 = assignNode.CreateAssignment("Rec.DecidedOn", "CurrDateTime()");
    
    var assignment5 = assignNode.CreateAssignment("Rec.Comment", "\"\"");
    ifNode2.TrueTarget = assignNode;
    
    var updateMonthSheet = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateMonthSheet").ConnectedBelow(assignNode, 1371);
    updateMonthSheet.Action = monthSheet2.UpdateAction;
    updateMonthSheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateMonthSheet.IsDisabled = false; // default value
    updateMonthSheet.ServerRequestTimeout = null; // default value
    var updateMonthSheet2 = monthSheet2.UpdateAction;
    var source = updateMonthSheet2.InputParameters.Named("Source");
    updateMonthSheet.SetArgumentValue(source, "Rec");
    
    var endNode = monthSheet_Approve.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateMonthSheet, 1371);
    endNode.IsDisabled = false; // default value
}
