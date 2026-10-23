(OutSystems.Model.UI.Mobile.IMobileFlow mainFlow) => {
    
    /*** creating IMobileScreen 'Approvals' ***/
    var approvals = mainFlow.CreateScreen("Approvals");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    approvals.Widgets.ToList().ForEach(x => x.Delete());
    approvals.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = mainFlow.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetManager = claudeSheet_CS.Roles.Named("SheetManager");
    approvals.Roles.Add(sheetManager);
    approvals.AnonymousAccess = false; // default value
    approvals.CustomURL = false; // default value
    approvals.Description = "Manager approval of monthly timesheets (SheetManager): submitted months with logged vs expected hours; Approve, Reject with a comment, and Reopen approved months (Show approved).";
    approvals.HorizontalPosition = 2200;
    approvals.Icon = null; // default value
    approvals.IsDisabled = false; // default value
    approvals.PageName = null; // default value
    approvals.Public = false; // default value
    approvals.Shareable = false; // default value
    approvals.StyleSheet = null; // default value
    approvals.SetTitle("\"Approvals\"");
    approvals.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    approvals.VerticalPosition = 400;
    
    var getApprovals = approvals.CreateDataAction("GetApprovals");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getApprovals.OutputParameters.ToList().ForEach(x => x.Delete());
    getApprovals.Description = null; // default value
    getApprovals.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getApprovals.ServerRequestTimeout = null; // default value
    
    var startNode = getApprovals.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var monthSheet_GetSubmitted = getApprovals.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("MonthSheet_GetSubmitted").ConnectedBelow(startNode, 1371);
    var monthSheet_GetSubmitted2 = claudeSheet_CS.ServerActions.Named("MonthSheet_GetSubmitted");
    monthSheet_GetSubmitted.Action = monthSheet_GetSubmitted2;
    monthSheet_GetSubmitted.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    monthSheet_GetSubmitted.IsDisabled = false; // default value
    monthSheet_GetSubmitted.ServerRequestTimeout = null; // default value
    var includeApproved = monthSheet_GetSubmitted2.InputParameters.Named("IncludeApproved");
    monthSheet_GetSubmitted.SetArgumentValue(includeApproved, "ShowApproved");
    
    var assignNode = getApprovals.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(monthSheet_GetSubmitted, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set months";
    
    var assignment = assignNode.CreateAssignment("Months", "MonthSheet_GetSubmitted.Months");
    
    var endNode = getApprovals.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var allExceptions = getApprovals.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = false;
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getApprovals.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getApprovals.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    getApprovals.OnAfterFetch.Destination = null; // default value
    
    var months = getApprovals.CreateOutputParameter("Months");
    var monthSheetSummary = claudeSheet_CS.Structures.Named("MonthSheetSummary");
    months.DataType = eSpace.GetOrCreateListType(monthSheetSummary);
    months.Description = null; // default value
    
    var errorMessage = getApprovals.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var showApproved = approvals.CreateLocalVariable("ShowApproved");
    showApproved.DataType = eSpace.BooleanType;
    showApproved.SetDefaultValue(null); // default value
    showApproved.Description = null; // default value
    
    var showRejectPopup = approvals.CreateLocalVariable("ShowRejectPopup");
    showRejectPopup.DataType = eSpace.BooleanType;
    showRejectPopup.SetDefaultValue(null); // default value
    showRejectPopup.Description = null; // default value
    
    var rejectMonthSheetId = approvals.CreateLocalVariable("RejectMonthSheetId");
    var monthSheet = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("MonthSheet");
    rejectMonthSheetId.DataType = monthSheet.IdentifierType;
    rejectMonthSheetId.SetDefaultValue(null); // default value
    rejectMonthSheetId.Description = null; // default value
    
    var rejectTitle = approvals.CreateLocalVariable("RejectTitle");
    rejectTitle.DataType = eSpace.TextType;
    rejectTitle.SetDefaultValue(null); // default value
    rejectTitle.Description = null; // default value
    
    var rejectComment = approvals.CreateLocalVariable("RejectComment");
    rejectComment.DataType = eSpace.TextType;
    rejectComment.SetDefaultValue(null); // default value
    rejectComment.Description = null; // default value
    
    var rejectError = approvals.CreateLocalVariable("RejectError");
    rejectError.DataType = eSpace.TextType;
    rejectError.SetDefaultValue(null); // default value
    rejectError.Description = null; // default value
    approvals.OnDestroy.Destination = null; // default value
    approvals.OnInitialize.Destination = null; // default value
    approvals.OnReady.Destination = null; // default value
    approvals.OnRender.Destination = null; // default value
    
    var onChange_ShowApprovedCheckbox = approvals.CreateScreenAction("OnChange_ShowApprovedCheckbox");
    onChange_ShowApprovedCheckbox.Description = "Reloads the list when Show approved changes.";
    
    var startNode2 = onChange_ShowApprovedCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var refreshDataNode = onChange_ShowApprovedCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode2, 1371);
    refreshDataNode.DataSource = getApprovals;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode3 = onChange_ShowApprovedCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode3.IsDisabled = false; // default value
    
    var onClick_ApproveLink = approvals.CreateScreenAction("OnClick_ApproveLink");
    onClick_ApproveLink.Description = "Approves the row\'s month (MonthSheet_Approve) and refreshes the list.";
    
    var row = onClick_ApproveLink.CreateInputParameter("Row");
    row.DataType = monthSheetSummary;
    row.Description = null; // default value
    row.IsMandatory = true; // default value
    
    var startNode3 = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var monthSheet_Approve = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("MonthSheet_Approve").ConnectedBelow(startNode3, 1371);
    var monthSheet_Approve2 = claudeSheet_CS.ServerActions.Named("MonthSheet_Approve");
    monthSheet_Approve.Action = monthSheet_Approve2;
    monthSheet_Approve.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    monthSheet_Approve.IsDisabled = false; // default value
    monthSheet_Approve.ServerRequestTimeout = null; // default value
    var monthSheetId = monthSheet_Approve2.InputParameters.Named("MonthSheetId");
    monthSheet_Approve.SetArgumentValue(monthSheetId, "Row.MonthSheetId");
    
    var refreshDataNode2 = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(monthSheet_Approve, 1371);
    refreshDataNode2.DataSource = getApprovals;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var messageNode = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode2, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show done";
    messageNode.SetMessage("Row.UserName + \": \" + FormatDateTime(NewDate(Row.Year, Row.Month, 1), \"MMMM yyyy\") + \" approved.\"");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode4 = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    
    var allExceptions2 = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var messageNode2 = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(allExceptions2, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Show error";
    messageNode2.SetMessage("AllExceptions.ExceptionMessage");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode5 = onClick_ApproveLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1371);
    endNode5.IsDisabled = false; // default value
    
    var onClick_CancelRejectButton = approvals.CreateScreenAction("OnClick_CancelRejectButton");
    onClick_CancelRejectButton.Description = "Closes RejectPopup without rejecting.";
    
    var startNode4 = onClick_CancelRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var assignNode3 = onClick_CancelRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Close popup";
    
    var assignment3 = assignNode3.CreateAssignment("ShowRejectPopup", "False");
    
    var endNode6 = onClick_CancelRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode6.IsDisabled = false; // default value
    
    var onClick_ConfirmRejectButton = approvals.CreateScreenAction("OnClick_ConfirmRejectButton");
    onClick_ConfirmRejectButton.Description = "Rejects the month with RejectComment (MonthSheet_Reject), closes the popup and refreshes the list. Errors (missing comment) stay in the popup.";
    
    var startNode5 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var monthSheet_Reject = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("MonthSheet_Reject").ConnectedBelow(startNode5, 1371);
    var monthSheet_Reject2 = claudeSheet_CS.ServerActions.Named("MonthSheet_Reject");
    monthSheet_Reject.Action = monthSheet_Reject2;
    monthSheet_Reject.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    monthSheet_Reject.IsDisabled = false; // default value
    monthSheet_Reject.ServerRequestTimeout = null; // default value
    var monthSheetId2 = monthSheet_Reject2.InputParameters.Named("MonthSheetId");
    monthSheet_Reject.SetArgumentValue(monthSheetId2, "RejectMonthSheetId");
    var comment = monthSheet_Reject2.InputParameters.Named("Comment");
    monthSheet_Reject.SetArgumentValue(comment, "RejectComment");
    
    var assignNode4 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(monthSheet_Reject, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Close popup";
    
    var assignment4 = assignNode4.CreateAssignment("ShowRejectPopup", "False");
    
    var refreshDataNode3 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode4, 1371);
    refreshDataNode3.DataSource = getApprovals;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    
    var messageNode3 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode3, 1371);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = "Show rejected";
    messageNode3.SetMessage("\"Month rejected. The user sees your comment.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode7 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    endNode7.IsDisabled = false; // default value
    
    var allExceptions3 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 6856;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode5 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Keep error";
    
    var assignment5 = assignNode5.CreateAssignment("RejectError", "AllExceptions.ExceptionMessage");
    
    var endNode8 = onClick_ConfirmRejectButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    endNode8.IsDisabled = false; // default value
    
    var onClick_RejectLink = approvals.CreateScreenAction("OnClick_RejectLink");
    onClick_RejectLink.Description = "Opens RejectPopup for the row\'s month with an empty comment.";
    
    var row2 = onClick_RejectLink.CreateInputParameter("Row");
    row2.DataType = monthSheetSummary;
    row2.Description = null; // default value
    row2.IsMandatory = true; // default value
    
    var startNode6 = onClick_RejectLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 800;
    
    var assignNode6 = onClick_RejectLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode6, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Open reject popup";
    
    var assignment6 = assignNode6.CreateAssignment("RejectMonthSheetId", "Row.MonthSheetId");
    
    var assignment7 = assignNode6.CreateAssignment("RejectTitle", "Row.UserName + \", \" + FormatDateTime(NewDate(Row.Year, Row.Month, 1), \"MMMM yyyy\")");
    
    var assignment8 = assignNode6.CreateAssignment("RejectComment", "\"\"");
    
    var assignment9 = assignNode6.CreateAssignment("RejectError", "\"\"");
    
    var assignment10 = assignNode6.CreateAssignment("ShowRejectPopup", "True");
    
    var endNode9 = onClick_RejectLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode6, 1371);
    endNode9.IsDisabled = false; // default value
    
    var onClick_ReopenLink = approvals.CreateScreenAction("OnClick_ReopenLink");
    onClick_ReopenLink.Description = "Reopens the row\'s approved month (MonthSheet_Reopen) and refreshes the list.";
    
    var row3 = onClick_ReopenLink.CreateInputParameter("Row");
    row3.DataType = monthSheetSummary;
    row3.Description = null; // default value
    row3.IsMandatory = true; // default value
    
    var startNode7 = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.IsDisabled = false; // default value
    startNode7.VerticalPosition = 800;
    
    var monthSheet_Reopen = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("MonthSheet_Reopen").ConnectedBelow(startNode7, 1371);
    var monthSheet_Reopen2 = claudeSheet_CS.ServerActions.Named("MonthSheet_Reopen");
    monthSheet_Reopen.Action = monthSheet_Reopen2;
    monthSheet_Reopen.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    monthSheet_Reopen.IsDisabled = false; // default value
    monthSheet_Reopen.ServerRequestTimeout = null; // default value
    var monthSheetId3 = monthSheet_Reopen2.InputParameters.Named("MonthSheetId");
    monthSheet_Reopen.SetArgumentValue(monthSheetId3, "Row.MonthSheetId");
    
    var refreshDataNode4 = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(monthSheet_Reopen, 1371);
    refreshDataNode4.DataSource = getApprovals;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords(null); // default value
    refreshDataNode4.SetStartIndex(null); // default value
    
    var messageNode4 = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1371);
    messageNode4.IsDisabled = false; // default value
    messageNode4.Label = "Show done";
    messageNode4.SetMessage("Row.UserName + \": \" + FormatDateTime(NewDate(Row.Year, Row.Month, 1), \"MMMM yyyy\") + \" reopened.\"");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode10 = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1371);
    endNode10.IsDisabled = false; // default value
    
    var allExceptions4 = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.IsDisabled = false; // default value
    allExceptions4.LogError = false;
    allExceptions4.VerticalPosition = 800;
    
    var messageNode5 = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(allExceptions4, 1371);
    messageNode5.IsDisabled = false; // default value
    messageNode5.Label = "Show error";
    messageNode5.SetMessage("AllExceptions.ExceptionMessage");
    messageNode5.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode11 = onClick_ReopenLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode5, 1371);
    endNode11.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = approvals.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopMenuInstance.CustomStyle = null; // default value
    layoutTopMenuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    layoutTopMenuInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "5");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Approvals";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var approvalsPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ApprovalsPage");
    approvalsPage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    approvalsPage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    approvalsPage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    approvalsPage.Animate = false; // default value
    approvalsPage.ColumnGap = null; // default value
    approvalsPage.ColumnSpan = null; // default value
    approvalsPage.CustomStyle = null; // default value
    approvalsPage.FlexBasis = null; // default value
    approvalsPage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    approvalsPage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    approvalsPage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    approvalsPage.Gap = null;
    approvalsPage.GridColumns = null; // default value
    approvalsPage.GridRows = null; // default value
    approvalsPage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    approvalsPage.RowGap = null; // default value
    approvalsPage.RowSpan = null; // default value
    approvalsPage.SetStyle("\"admin-page\"");
    approvalsPage.SetVisible("True"); // default value
    approvalsPage.VisibleWasExpression = false; // default value
    approvalsPage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    approvalsPage.Width = "(fill parent)"; // default value
    
    var toolbar = approvalsPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Toolbar");
    toolbar.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    toolbar.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    toolbar.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    toolbar.Animate = false; // default value
    toolbar.ColumnGap = null; // default value
    toolbar.ColumnSpan = null; // default value
    toolbar.CustomStyle = null; // default value
    toolbar.FlexBasis = null; // default value
    toolbar.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    toolbar.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    toolbar.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    toolbar.Gap = null;
    toolbar.GridColumns = null; // default value
    toolbar.GridRows = null; // default value
    toolbar.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    toolbar.RowGap = null; // default value
    toolbar.RowSpan = null; // default value
    toolbar.SetStyle("\"admin-toolbar\"");
    toolbar.SetVisible("True"); // default value
    toolbar.VisibleWasExpression = false; // default value
    toolbar.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    toolbar.Width = "(fill parent)"; // default value
    
    var showApprovedWrap = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ShowApprovedWrap");
    showApprovedWrap.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    showApprovedWrap.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    showApprovedWrap.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    showApprovedWrap.Animate = false; // default value
    showApprovedWrap.ColumnGap = null; // default value
    showApprovedWrap.ColumnSpan = null; // default value
    showApprovedWrap.CustomStyle = null; // default value
    showApprovedWrap.FlexBasis = null; // default value
    showApprovedWrap.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    showApprovedWrap.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    showApprovedWrap.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    showApprovedWrap.Gap = null;
    showApprovedWrap.GridColumns = null; // default value
    showApprovedWrap.GridRows = null; // default value
    showApprovedWrap.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    showApprovedWrap.RowGap = null; // default value
    showApprovedWrap.RowSpan = null; // default value
    showApprovedWrap.SetStyle("\"admin-toolbar__check\"");
    showApprovedWrap.SetVisible("True"); // default value
    showApprovedWrap.VisibleWasExpression = false; // default value
    showApprovedWrap.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    showApprovedWrap.Width = "(fill parent)"; // default value
    
    var showApprovedCheckbox = showApprovedWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowApprovedCheckbox");
    showApprovedCheckbox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    showApprovedCheckbox.ColumnSpan = null; // default value
    showApprovedCheckbox.CustomStyle = null; // default value
    showApprovedCheckbox.SetEnabled("True");
    showApprovedCheckbox.FlexBasis = null; // default value
    showApprovedCheckbox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    showApprovedCheckbox.RowSpan = null; // default value
    showApprovedCheckbox.SetStyle("\"checkbox\""); // default value
    showApprovedCheckbox.SetVariable("ShowApproved");
    var builtinEvent = showApprovedCheckbox.OnChange;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onChange_ShowApprovedCheckbox;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var textWidget2 = showApprovedWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Show approved";
    
    var hasLoadError = approvalsPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasLoadError");
    hasLoadError.Animate = false; // default value
    hasLoadError.SetCondition("GetApprovals.ErrorMessage <> \"\"");
    hasLoadError.CustomStyle = null; // default value
    hasLoadError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var loadError = hasLoadError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("LoadError");
    loadError.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    loadError.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    loadError.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    loadError.Animate = false; // default value
    loadError.ColumnGap = null; // default value
    loadError.ColumnSpan = null; // default value
    loadError.CustomStyle = null; // default value
    loadError.FlexBasis = null; // default value
    loadError.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    loadError.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    loadError.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    loadError.Gap = null;
    loadError.GridColumns = null; // default value
    loadError.GridRows = null; // default value
    loadError.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    loadError.RowGap = null; // default value
    loadError.RowSpan = null; // default value
    loadError.SetStyle("\"admin-error\"");
    loadError.SetVisible("True"); // default value
    loadError.VisibleWasExpression = false; // default value
    loadError.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    loadError.Width = "(fill parent)"; // default value
    
    var expression = loadError.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetApprovals.ErrorMessage");
    expression.Width = ""; // default value
    
    var approvalsTable = approvalsPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ApprovalsTable");
    approvalsTable.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    approvalsTable.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    approvalsTable.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    approvalsTable.Animate = false; // default value
    approvalsTable.ColumnGap = null; // default value
    approvalsTable.ColumnSpan = null; // default value
    approvalsTable.CustomStyle = null; // default value
    approvalsTable.FlexBasis = null; // default value
    approvalsTable.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    approvalsTable.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    approvalsTable.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    approvalsTable.Gap = null;
    approvalsTable.GridColumns = null; // default value
    approvalsTable.GridRows = null; // default value
    approvalsTable.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    approvalsTable.RowGap = null; // default value
    approvalsTable.RowSpan = null; // default value
    approvalsTable.SetStyle("\"admin-table admin-table--approvals\"");
    approvalsTable.SetVisible("True"); // default value
    approvalsTable.VisibleWasExpression = false; // default value
    approvalsTable.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    approvalsTable.Width = "(fill parent)"; // default value
    
    var approvalsHeader = approvalsTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ApprovalsHeader");
    approvalsHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    approvalsHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    approvalsHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    approvalsHeader.Animate = false; // default value
    approvalsHeader.ColumnGap = null; // default value
    approvalsHeader.ColumnSpan = null; // default value
    approvalsHeader.CustomStyle = null; // default value
    approvalsHeader.FlexBasis = null; // default value
    approvalsHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    approvalsHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    approvalsHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    approvalsHeader.Gap = null;
    approvalsHeader.GridColumns = null; // default value
    approvalsHeader.GridRows = null; // default value
    approvalsHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    approvalsHeader.RowGap = null; // default value
    approvalsHeader.RowSpan = null; // default value
    approvalsHeader.SetStyle("\"admin-table__row admin-table__head\"");
    approvalsHeader.SetVisible("True"); // default value
    approvalsHeader.VisibleWasExpression = false; // default value
    approvalsHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    approvalsHeader.Width = "(fill parent)"; // default value
    
    var container = approvalsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container.Animate = false; // default value
    container.ColumnGap = null; // default value
    container.ColumnSpan = null; // default value
    container.CustomStyle = null; // default value
    container.FlexBasis = null; // default value
    container.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container.Gap = null;
    container.GridColumns = null; // default value
    container.GridRows = null; // default value
    container.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container.RowGap = null; // default value
    container.RowSpan = null; // default value
    container.SetStyle("\"admin-table__cell\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var expression2 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("\"User\"");
    expression2.Width = ""; // default value
    
    var container2 = approvalsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container2.Animate = false; // default value
    container2.ColumnGap = null; // default value
    container2.ColumnSpan = null; // default value
    container2.CustomStyle = null; // default value
    container2.FlexBasis = null; // default value
    container2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container2.Gap = null;
    container2.GridColumns = null; // default value
    container2.GridRows = null; // default value
    container2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container2.RowGap = null; // default value
    container2.RowSpan = null; // default value
    container2.SetStyle("\"admin-table__cell\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var expression3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("\"Month\"");
    expression3.Width = ""; // default value
    
    var container3 = approvalsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container3.Animate = false; // default value
    container3.ColumnGap = null; // default value
    container3.ColumnSpan = null; // default value
    container3.CustomStyle = null; // default value
    container3.FlexBasis = null; // default value
    container3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container3.Gap = null;
    container3.GridColumns = null; // default value
    container3.GridRows = null; // default value
    container3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container3.RowGap = null; // default value
    container3.RowSpan = null; // default value
    container3.SetStyle("\"admin-table__cell\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var expression4 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("\"Hours (logged / expected)\"");
    expression4.Width = ""; // default value
    
    var container4 = approvalsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container4.Animate = false; // default value
    container4.ColumnGap = null; // default value
    container4.ColumnSpan = null; // default value
    container4.CustomStyle = null; // default value
    container4.FlexBasis = null; // default value
    container4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container4.Gap = null;
    container4.GridColumns = null; // default value
    container4.GridRows = null; // default value
    container4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container4.RowGap = null; // default value
    container4.RowSpan = null; // default value
    container4.SetStyle("\"admin-table__cell\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var expression5 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("\"Status\"");
    expression5.Width = ""; // default value
    
    var container5 = approvalsHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container5.Animate = false; // default value
    container5.ColumnGap = null; // default value
    container5.ColumnSpan = null; // default value
    container5.CustomStyle = null; // default value
    container5.FlexBasis = null; // default value
    container5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container5.Gap = null;
    container5.GridColumns = null; // default value
    container5.GridRows = null; // default value
    container5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container5.RowGap = null; // default value
    container5.RowSpan = null; // default value
    container5.SetStyle("\"admin-table__cell\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var expression6 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("\"Actions\"");
    expression6.Width = ""; // default value
    
    var hasMonths = approvalsTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasMonths");
    hasMonths.Animate = false; // default value
    hasMonths.SetCondition("GetApprovals.Months.Length > 0");
    hasMonths.CustomStyle = null; // default value
    hasMonths.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var monthList = hasMonths.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("MonthList");
    monthList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    monthList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    monthList.AnimateItems = true; // default value
    monthList.ColumnGap = null; // default value
    monthList.ColumnSpan = null; // default value
    monthList.CustomStyle = null; // default value
    monthList.ExpandedInWebEditor = true; // default value
    monthList.FlexBasis = null; // default value
    monthList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    monthList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    monthList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    monthList.Gap = null;
    monthList.GridColumns = null; // default value
    monthList.GridRows = null; // default value
    monthList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    monthList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    monthList.RowGap = null; // default value
    monthList.RowSpan = null; // default value
    monthList.SetSource("GetApprovals.Months");
    monthList.SetStyle("\"admin-table__body\"");
    monthList.Tag = "div"; // default value
    monthList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    monthList.Width = "(fill parent)"; // default value
    
    var monthRow = monthList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthRow");
    monthRow.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    monthRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    monthRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    monthRow.Animate = false; // default value
    monthRow.ColumnGap = null; // default value
    monthRow.ColumnSpan = null; // default value
    monthRow.CustomStyle = null; // default value
    monthRow.FlexBasis = null; // default value
    monthRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    monthRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    monthRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    monthRow.Gap = null;
    monthRow.GridColumns = null; // default value
    monthRow.GridRows = null; // default value
    monthRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    monthRow.RowGap = null; // default value
    monthRow.RowSpan = null; // default value
    monthRow.SetStyle("\"admin-table__row\"");
    monthRow.SetVisible("True"); // default value
    monthRow.VisibleWasExpression = false; // default value
    monthRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    monthRow.Width = "(fill parent)"; // default value
    
    var container6 = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container6.Animate = false; // default value
    container6.ColumnGap = null; // default value
    container6.ColumnSpan = null; // default value
    container6.CustomStyle = null; // default value
    container6.FlexBasis = null; // default value
    container6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container6.Gap = null;
    container6.GridColumns = null; // default value
    container6.GridRows = null; // default value
    container6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container6.RowGap = null; // default value
    container6.RowSpan = null; // default value
    container6.SetStyle("\"admin-table__cell admin-table__name\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var expression7 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("GetApprovals.Months.Current.UserName");
    expression7.Width = ""; // default value
    
    var container7 = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container7.Animate = false; // default value
    container7.ColumnGap = null; // default value
    container7.ColumnSpan = null; // default value
    container7.CustomStyle = null; // default value
    container7.FlexBasis = null; // default value
    container7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container7.Gap = null;
    container7.GridColumns = null; // default value
    container7.GridRows = null; // default value
    container7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container7.RowGap = null; // default value
    container7.RowSpan = null; // default value
    container7.SetStyle("\"admin-table__cell\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var expression8 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("FormatDateTime(NewDate(GetApprovals.Months.Current.Year, GetApprovals.Months.Current.Month, 1), \"MMMM yyyy\")");
    expression8.Width = ""; // default value
    
    var container8 = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = false; // default value
    container8.ColumnGap = null; // default value
    container8.ColumnSpan = null; // default value
    container8.CustomStyle = null; // default value
    container8.FlexBasis = null; // default value
    container8.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container8.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container8.Gap = null;
    container8.GridColumns = null; // default value
    container8.GridRows = null; // default value
    container8.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container8.RowGap = null; // default value
    container8.RowSpan = null; // default value
    container8.SetStyle("\"admin-table__cell\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var expression9 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("Minutes_Format(GetApprovals.Months.Current.TotalMinutes, \":\") + \" / \" + Minutes_Format(GetApprovals.Months.Current.ExpectedMinutes, \":\")");
    expression9.Width = ""; // default value
    
    var container9 = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container9.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container9.Animate = false; // default value
    container9.ColumnGap = null; // default value
    container9.ColumnSpan = null; // default value
    container9.CustomStyle = null; // default value
    container9.FlexBasis = null; // default value
    container9.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container9.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container9.Gap = null;
    container9.GridColumns = null; // default value
    container9.GridRows = null; // default value
    container9.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container9.RowGap = null; // default value
    container9.RowSpan = null; // default value
    container9.SetStyle("\"admin-table__cell\"");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var statusTag = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("StatusTag");
    statusTag.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    statusTag.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    statusTag.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    statusTag.Animate = false; // default value
    statusTag.ColumnGap = null; // default value
    statusTag.ColumnSpan = null; // default value
    statusTag.CustomStyle = null; // default value
    statusTag.FlexBasis = null; // default value
    statusTag.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    statusTag.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    statusTag.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    statusTag.Gap = null;
    statusTag.GridColumns = null; // default value
    statusTag.GridRows = null; // default value
    statusTag.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    statusTag.RowGap = null; // default value
    statusTag.RowSpan = null; // default value
    statusTag.SetStyle("\"admin-tag\" + If(GetApprovals.Months.Current.PeriodStatusId = Entities.PeriodStatus.Approved, \" admin-tag--admin\", \"\")");
    statusTag.SetVisible("True"); // default value
    statusTag.VisibleWasExpression = false; // default value
    statusTag.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    statusTag.Width = "(fill parent)"; // default value
    
    var expression10 = statusTag.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("If(GetApprovals.Months.Current.PeriodStatusId = Entities.PeriodStatus.Approved, \"Approved\", \"Submitted\")");
    expression10.Width = ""; // default value
    
    var monthActions = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthActions");
    monthActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    monthActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    monthActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    monthActions.Animate = false; // default value
    monthActions.ColumnGap = null; // default value
    monthActions.ColumnSpan = null; // default value
    monthActions.CustomStyle = null; // default value
    monthActions.FlexBasis = null; // default value
    monthActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    monthActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    monthActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    monthActions.Gap = null;
    monthActions.GridColumns = null; // default value
    monthActions.GridRows = null; // default value
    monthActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    monthActions.RowGap = null; // default value
    monthActions.RowSpan = null; // default value
    monthActions.SetStyle("\"admin-table__cell admin-table__actions\"");
    monthActions.SetVisible("True"); // default value
    monthActions.VisibleWasExpression = false; // default value
    monthActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    monthActions.Width = "(fill parent)"; // default value
    
    var approveLink = monthActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ApproveLink");
    approveLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    approveLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    approveLink.ColumnGap = null; // default value
    approveLink.ColumnSpan = null; // default value
    approveLink.SetConfirmationMessage(null); // default value
    approveLink.CustomStyle = null; // default value
    approveLink.SetEnabled("True");
    approveLink.FlexBasis = null; // default value
    approveLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    approveLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    approveLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    approveLink.Gap = null;
    approveLink.GridColumns = null; // default value
    approveLink.GridRows = null; // default value
    approveLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    approveLink.RowGap = null; // default value
    approveLink.RowSpan = null; // default value
    approveLink.SetStyle(null); // default value
    approveLink.SetVisible("GetApprovals.Months.Current.PeriodStatusId = Entities.PeriodStatus.Submitted");
    approveLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    approveLink.Width = ""; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)approveLink.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Approve";
    var builtinEvent2 = approveLink.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = onClick_ApproveLink;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent2.SetArgumentValue(row, "GetApprovals.Months.Current");
    
    var rejectLink = monthActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("RejectLink");
    rejectLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectLink.ColumnGap = null; // default value
    rejectLink.ColumnSpan = null; // default value
    rejectLink.SetConfirmationMessage(null); // default value
    rejectLink.CustomStyle = null; // default value
    rejectLink.SetEnabled("True");
    rejectLink.FlexBasis = null; // default value
    rejectLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectLink.Gap = null;
    rejectLink.GridColumns = null; // default value
    rejectLink.GridRows = null; // default value
    rejectLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectLink.RowGap = null; // default value
    rejectLink.RowSpan = null; // default value
    rejectLink.SetStyle(null); // default value
    rejectLink.SetVisible("GetApprovals.Months.Current.PeriodStatusId = Entities.PeriodStatus.Submitted");
    rejectLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rejectLink.Width = ""; // default value
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)rejectLink.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Reject";
    var builtinEvent3 = rejectLink.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = onClick_RejectLink;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent3.SetArgumentValue(row2, "GetApprovals.Months.Current");
    
    var reopenLink = monthActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ReopenLink");
    reopenLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    reopenLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    reopenLink.ColumnGap = null; // default value
    reopenLink.ColumnSpan = null; // default value
    reopenLink.SetConfirmationMessage(null); // default value
    reopenLink.CustomStyle = null; // default value
    reopenLink.SetEnabled("True");
    reopenLink.FlexBasis = null; // default value
    reopenLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    reopenLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    reopenLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    reopenLink.Gap = null;
    reopenLink.GridColumns = null; // default value
    reopenLink.GridRows = null; // default value
    reopenLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    reopenLink.RowGap = null; // default value
    reopenLink.RowSpan = null; // default value
    reopenLink.SetStyle(null); // default value
    reopenLink.SetVisible("GetApprovals.Months.Current.PeriodStatusId = Entities.PeriodStatus.Approved");
    reopenLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    reopenLink.Width = ""; // default value
    
    var textWidget5 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)reopenLink.Widgets.First();
    textWidget5.CustomStyle = null; // default value
    textWidget5.Name = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Reopen";
    var builtinEvent4 = reopenLink.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = onClick_ReopenLink;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent4.SetArgumentValue(row3, "GetApprovals.Months.Current");
    var builtinEvent5 = monthList.OnScrollEnding;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var noMonths = hasMonths.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoMonths");
    noMonths.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noMonths.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noMonths.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noMonths.Animate = true;
    noMonths.ColumnGap = null; // default value
    noMonths.ColumnSpan = null; // default value
    noMonths.CustomStyle = null; // default value
    noMonths.FlexBasis = null; // default value
    noMonths.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noMonths.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noMonths.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noMonths.Gap = null;
    noMonths.GridColumns = null; // default value
    noMonths.GridRows = null; // default value
    noMonths.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noMonths.RowGap = null; // default value
    noMonths.RowSpan = null; // default value
    noMonths.SetStyle("\"admin-table__empty\"");
    noMonths.SetVisible("GetApprovals.IsDataFetched");
    noMonths.VisibleWasExpression = true;
    noMonths.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noMonths.Width = "(fill parent)"; // default value
    
    var expression11 = noMonths.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle(null); // default value
    expression11.SetValue("If(ShowApproved, \"No submitted or approved months.\", \"No months waiting for approval.\")");
    expression11.Width = ""; // default value
    
    var rejectPopup = approvalsPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("RejectPopup");
    rejectPopup.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectPopup.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectPopup.ColumnGap = null; // default value
    rejectPopup.ColumnSpan = null; // default value
    rejectPopup.CustomStyle = null; // default value
    rejectPopup.ExpandedInWebEditor = true; // default value
    rejectPopup.FlexBasis = null; // default value
    rejectPopup.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectPopup.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectPopup.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectPopup.Gap = null;
    rejectPopup.GridColumns = null; // default value
    rejectPopup.GridRows = null; // default value
    rejectPopup.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectPopup.RowGap = null; // default value
    rejectPopup.RowSpan = null; // default value
    rejectPopup.SetShowPopup("ShowRejectPopup");
    rejectPopup.SetStyle("\"popup-dialog admin-popup\"");
    rejectPopup.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var rejectTitleBox = rejectPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("RejectTitleBox");
    rejectTitleBox.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    rejectTitleBox.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectTitleBox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectTitleBox.Animate = false; // default value
    rejectTitleBox.ColumnGap = null; // default value
    rejectTitleBox.ColumnSpan = null; // default value
    rejectTitleBox.CustomStyle = null; // default value
    rejectTitleBox.FlexBasis = null; // default value
    rejectTitleBox.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectTitleBox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectTitleBox.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectTitleBox.Gap = null;
    rejectTitleBox.GridColumns = null; // default value
    rejectTitleBox.GridRows = null; // default value
    rejectTitleBox.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectTitleBox.RowGap = null; // default value
    rejectTitleBox.RowSpan = null; // default value
    rejectTitleBox.SetStyle("\"admin-section__head\"");
    rejectTitleBox.SetVisible("True"); // default value
    rejectTitleBox.VisibleWasExpression = false; // default value
    rejectTitleBox.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rejectTitleBox.Width = "(fill parent)"; // default value
    
    var expression12 = rejectTitleBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression12.ColumnSpan = null; // default value
    expression12.CustomStyle = null; // default value
    expression12.Example = null; // default value
    expression12.FlexBasis = null; // default value
    expression12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression12.RowSpan = null; // default value
    expression12.SetStyle(null); // default value
    expression12.SetValue("\"Reject \" + RejectTitle");
    expression12.Width = ""; // default value
    
    var rejectForm = rejectPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("RejectForm");
    rejectForm.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    rejectForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectForm.Animate = false; // default value
    rejectForm.ColumnGap = null; // default value
    rejectForm.ColumnSpan = null; // default value
    rejectForm.CustomStyle = null; // default value
    rejectForm.FlexBasis = null; // default value
    rejectForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectForm.Gap = null;
    rejectForm.GridColumns = null; // default value
    rejectForm.GridRows = null; // default value
    rejectForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectForm.RowGap = null; // default value
    rejectForm.RowSpan = null; // default value
    rejectForm.SetStyle("\"admin-form\"");
    rejectForm.SetVisible("True"); // default value
    rejectForm.VisibleWasExpression = false; // default value
    rejectForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rejectForm.Width = "(fill parent)"; // default value
    
    var rejectCommentLabel = rejectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>("RejectCommentLabel");
    rejectCommentLabel.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectCommentLabel.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectCommentLabel.ColumnGap = null; // default value
    rejectCommentLabel.ColumnSpan = null; // default value
    rejectCommentLabel.CustomStyle = null; // default value
    rejectCommentLabel.FlexBasis = null; // default value
    rejectCommentLabel.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectCommentLabel.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectCommentLabel.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectCommentLabel.Gap = null;
    rejectCommentLabel.GridColumns = null; // default value
    rejectCommentLabel.GridRows = null; // default value
    rejectCommentLabel.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectCommentLabel.RowGap = null; // default value
    rejectCommentLabel.RowSpan = null; // default value
    rejectCommentLabel.SetStyle(null); // default value
    rejectCommentLabel.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rejectCommentLabel.Width = "(fill parent)"; // default value
    
    var textWidget6 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)rejectCommentLabel.Widgets.First();
    textWidget6.CustomStyle = null; // default value
    textWidget6.Name = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Comment for the user";
    
    var rejectCommentInput = rejectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITextArea>("RejectCommentInput");
    rejectCommentInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectCommentInput.ColumnSpan = null; // default value
    rejectCommentInput.CustomStyle = null; // default value
    rejectCommentInput.SetEnabled("True");
    rejectCommentInput.FlexBasis = null; // default value
    rejectCommentInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectCommentInput.SetMandatory("True");
    rejectCommentInput.MaxLength = 500; // default value
    rejectCommentInput.SetPrompt(null); // default value
    rejectCommentInput.RowSpan = null; // default value
    rejectCommentInput.SetStyle("\"form-control\""); // default value
    rejectCommentInput.TextLines = 3; // default value
    rejectCommentInput.SetVariable("RejectComment");
    rejectCommentInput.Width = "(fill parent)"; // default value
    var builtinEvent6 = rejectCommentInput.OnChange;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = null; // default value
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    rejectCommentLabel.TargetWidget = rejectCommentInput;
    
    var hasRejectError = rejectForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasRejectError");
    hasRejectError.Animate = false; // default value
    hasRejectError.SetCondition("RejectError <> \"\"");
    hasRejectError.CustomStyle = null; // default value
    hasRejectError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var rejectErrorBox = hasRejectError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("RejectErrorBox");
    rejectErrorBox.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    rejectErrorBox.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectErrorBox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectErrorBox.Animate = false; // default value
    rejectErrorBox.ColumnGap = null; // default value
    rejectErrorBox.ColumnSpan = null; // default value
    rejectErrorBox.CustomStyle = null; // default value
    rejectErrorBox.FlexBasis = null; // default value
    rejectErrorBox.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectErrorBox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectErrorBox.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectErrorBox.Gap = null;
    rejectErrorBox.GridColumns = null; // default value
    rejectErrorBox.GridRows = null; // default value
    rejectErrorBox.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectErrorBox.RowGap = null; // default value
    rejectErrorBox.RowSpan = null; // default value
    rejectErrorBox.SetStyle("\"admin-error\"");
    rejectErrorBox.SetVisible("True"); // default value
    rejectErrorBox.VisibleWasExpression = false; // default value
    rejectErrorBox.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rejectErrorBox.Width = "(fill parent)"; // default value
    
    var expression13 = rejectErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression13.ColumnSpan = null; // default value
    expression13.CustomStyle = null; // default value
    expression13.Example = null; // default value
    expression13.FlexBasis = null; // default value
    expression13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression13.RowSpan = null; // default value
    expression13.SetStyle(null); // default value
    expression13.SetValue("RejectError");
    expression13.Width = ""; // default value
    
    var rejectActions = rejectForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("RejectActions");
    rejectActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    rejectActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectActions.Animate = false; // default value
    rejectActions.ColumnGap = null; // default value
    rejectActions.ColumnSpan = null; // default value
    rejectActions.CustomStyle = null; // default value
    rejectActions.FlexBasis = null; // default value
    rejectActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectActions.Gap = null;
    rejectActions.GridColumns = null; // default value
    rejectActions.GridRows = null; // default value
    rejectActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectActions.RowGap = null; // default value
    rejectActions.RowSpan = null; // default value
    rejectActions.SetStyle("\"admin-form__actions\"");
    rejectActions.SetVisible("True"); // default value
    rejectActions.VisibleWasExpression = false; // default value
    rejectActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rejectActions.Width = "(fill parent)"; // default value
    
    var confirmRejectButton = rejectActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ConfirmRejectButton");
    confirmRejectButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    confirmRejectButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    confirmRejectButton.ColumnGap = null; // default value
    confirmRejectButton.ColumnSpan = null; // default value
    confirmRejectButton.SetConfirmationMessage(null); // default value
    confirmRejectButton.CustomStyle = null; // default value
    confirmRejectButton.SetEnabled("True");
    confirmRejectButton.FlexBasis = null; // default value
    confirmRejectButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    confirmRejectButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    confirmRejectButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    confirmRejectButton.Gap = null;
    confirmRejectButton.GridColumns = null; // default value
    confirmRejectButton.GridRows = null; // default value
    confirmRejectButton.IsDefault = false; // default value
    confirmRejectButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    confirmRejectButton.RowGap = null; // default value
    confirmRejectButton.RowSpan = null; // default value
    confirmRejectButton.SetStyle("\"btn btn-primary\"");
    confirmRejectButton.SetVisible("True"); // default value
    confirmRejectButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    confirmRejectButton.Width = ""; // default value
    
    var textWidget7 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)confirmRejectButton.Widgets.First();
    textWidget7.CustomStyle = null; // default value
    textWidget7.Name = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Reject month";
    var builtinEvent7 = confirmRejectButton.OnClick;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = onClick_ConfirmRejectButton;
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelRejectButton = rejectActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelRejectButton");
    cancelRejectButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    cancelRejectButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    cancelRejectButton.ColumnGap = null; // default value
    cancelRejectButton.ColumnSpan = null; // default value
    cancelRejectButton.SetConfirmationMessage(null); // default value
    cancelRejectButton.CustomStyle = null; // default value
    cancelRejectButton.SetEnabled("True");
    cancelRejectButton.FlexBasis = null; // default value
    cancelRejectButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    cancelRejectButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    cancelRejectButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    cancelRejectButton.Gap = null;
    cancelRejectButton.GridColumns = null; // default value
    cancelRejectButton.GridRows = null; // default value
    cancelRejectButton.IsDefault = false; // default value
    cancelRejectButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    cancelRejectButton.RowGap = null; // default value
    cancelRejectButton.RowSpan = null; // default value
    cancelRejectButton.SetStyle("\"btn\""); // default value
    cancelRejectButton.SetVisible("True"); // default value
    cancelRejectButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    cancelRejectButton.Width = ""; // default value
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)cancelRejectButton.Widgets.First();
    textWidget8.CustomStyle = null; // default value
    textWidget8.Name = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Cancel";
    var builtinEvent8 = cancelRejectButton.OnClick;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = onClick_CancelRejectButton;
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
}
