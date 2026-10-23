(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'MonthSheet_Reopen' ***/
    var monthSheet_Reopen = eSpace.CreateServerAction("MonthSheet_Reopen");
    monthSheet_Reopen.CacheInMinutes = null; // default value
    monthSheet_Reopen.Description = "Reopens an Approved month (SheetManager): status Open, DecidedBy/DecidedOn set, comment cleared, so the user can change and submit it again. Raises PeriodValidation when the month isn\'t Approved.";
    var monthSheet = eSpace.Folders.Named("MonthSheet");
    monthSheet_Reopen.Folder = monthSheet;
    monthSheet_Reopen.Function = false; // default value
    monthSheet_Reopen.Icon = null; // default value
    monthSheet_Reopen.Public = true;
    monthSheet_Reopen.Shareable = true;
    
    var monthSheetId = monthSheet_Reopen.CreateInputParameter("MonthSheetId");
    var monthSheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("MonthSheet");
    monthSheetId.DataType = monthSheet2.IdentifierType;
    monthSheetId.Description = null; // default value
    monthSheetId.IsMandatory = true; // default value
    
    var rec = monthSheet_Reopen.CreateLocalVariable("Rec");
    rec.DataType = monthSheet2;
    rec.Description = null; // default value
    
    var startNode = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetManagerRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetManager?";
    
    var raiseExceptionNode = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetManager");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"Only managers can approve, reject or reopen months.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getMonthSheetForUpdate = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthSheetForUpdate").Below(ifNode, 1371);
    getMonthSheetForUpdate.Action = monthSheet2.GetForUpdateAction;
    getMonthSheetForUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getMonthSheetForUpdate.IsDisabled = false; // default value
    getMonthSheetForUpdate.ServerRequestTimeout = null; // default value
    var getMonthSheetForUpdate2 = monthSheet2.GetForUpdateAction;
    var id = getMonthSheetForUpdate2.InputParameters.Named("Id");
    getMonthSheetForUpdate.SetArgumentValue(id, "MonthSheetId");
    ifNode.TrueTarget = getMonthSheetForUpdate;
    
    var ifNode2 = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getMonthSheetForUpdate, 1371);
    ifNode2.SetCondition("GetMonthSheetForUpdate.Record.MonthSheet.PeriodStatusId = Entities.PeriodStatus.Approved");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Approved?";
    
    var raiseExceptionNode2 = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var periodValidation = eSpace.UserExceptions.Named("PeriodValidation");
    raiseExceptionNode2.Exception = periodValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Only approved months can be reopened.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.FalseTarget = raiseExceptionNode2;
    
    var assignNode = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set open";
    
    var assignment = assignNode.CreateAssignment("Rec", "GetMonthSheetForUpdate.Record");
    
    var assignment2 = assignNode.CreateAssignment("Rec.PeriodStatusId", "Entities.PeriodStatus.Open");
    
    var assignment3 = assignNode.CreateAssignment("Rec.DecidedBy", "GetUserId()");
    
    var assignment4 = assignNode.CreateAssignment("Rec.DecidedOn", "CurrDateTime()");
    
    var assignment5 = assignNode.CreateAssignment("Rec.Comment", "\"\"");
    ifNode2.TrueTarget = assignNode;
    
    var updateMonthSheet = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateMonthSheet").ConnectedBelow(assignNode, 1371);
    updateMonthSheet.Action = monthSheet2.UpdateAction;
    updateMonthSheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateMonthSheet.IsDisabled = false; // default value
    updateMonthSheet.ServerRequestTimeout = null; // default value
    var updateMonthSheet2 = monthSheet2.UpdateAction;
    var source = updateMonthSheet2.InputParameters.Named("Source");
    updateMonthSheet.SetArgumentValue(source, "Rec");
    
    var endNode = monthSheet_Reopen.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(updateMonthSheet, 1371);
    endNode.IsDisabled = false; // default value
}
