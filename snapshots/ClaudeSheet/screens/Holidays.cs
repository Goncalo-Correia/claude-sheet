(OutSystems.Model.UI.Mobile.IMobileFlow admin) => {
    
    /*** creating IMobileScreen 'Holidays' ***/
    var holidays = admin.CreateScreen("Holidays");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    holidays.Widgets.ToList().ForEach(x => x.Delete());
    holidays.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = admin.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    holidays.Roles.Add(sheetAdmin);
    holidays.AnonymousAccess = false; // default value
    holidays.CustomURL = false; // default value
    holidays.Description = "Admin holiday calendar per year (SheetAdmin): year selector, list with weekday and status, add/edit in a popup, activate/deactivate, copy the fixed-date holidays to the next year.";
    holidays.HorizontalPosition = 400;
    holidays.Icon = null; // default value
    holidays.IsDisabled = false; // default value
    holidays.PageName = null; // default value
    holidays.Public = false; // default value
    holidays.Shareable = false; // default value
    holidays.StyleSheet = null; // default value
    holidays.SetTitle("\"Holidays\"");
    holidays.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    holidays.VerticalPosition = 2400;
    
    var getHolidays = holidays.CreateDataAction("GetHolidays");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getHolidays.OutputParameters.ToList().ForEach(x => x.Delete());
    getHolidays.Description = null; // default value
    getHolidays.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getHolidays.ServerRequestTimeout = null; // default value
    
    var startNode = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var holiday_GetList = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_GetList").ConnectedBelow(startNode, 1371);
    var holiday_GetList2 = claudeSheet_CS.ServerActions.Named("Holiday_GetList");
    holiday_GetList.Action = holiday_GetList2;
    holiday_GetList.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_GetList.IsDisabled = false; // default value
    holiday_GetList.ServerRequestTimeout = null; // default value
    var includeInactive = holiday_GetList2.InputParameters.Named("IncludeInactive");
    holiday_GetList.SetArgumentValue(includeInactive, "ShowInactive");
    var year = holiday_GetList2.InputParameters.Named("Year");
    holiday_GetList.SetArgumentValue(year, "SelectedYear");
    
    var assignNode = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(holiday_GetList, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set holidays";
    
    var assignment = assignNode.CreateAssignment("Holidays", "Holiday_GetList.Holidays");
    
    var endNode = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var allExceptions = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var holidays2 = getHolidays.CreateOutputParameter("Holidays");
    var holiday = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Holiday");
    holidays2.DataType = eSpace.GetOrCreateListType(holiday);
    holidays2.Description = null; // default value
    
    var onAfterFetch_GetHolidays = holidays.CreateScreenAction("OnAfterFetch_GetHolidays");
    onAfterFetch_GetHolidays.Description = "Shows an error message when GetHolidays failed.";
    
    var startNode2 = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetHolidays.ErrorMessage <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode3.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetHolidays.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    getHolidays.OnAfterFetch.Destination = onAfterFetch_GetHolidays;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getHolidays.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var selectedYear = holidays.CreateLocalVariable("SelectedYear");
    selectedYear.DataType = eSpace.IntegerType;
    selectedYear.SetDefaultValue(null); // default value
    selectedYear.Description = "Year shown; set to the current year in OnInitialize.";
    
    var showInactive = holidays.CreateLocalVariable("ShowInactive");
    showInactive.DataType = eSpace.BooleanType;
    showInactive.SetDefaultValue("False");
    showInactive.Description = null; // default value
    
    var editHoliday = holidays.CreateLocalVariable("EditHoliday");
    editHoliday.DataType = holiday;
    editHoliday.Description = "Holiday being added or edited in the popup.";
    
    var showHolidayPopup = holidays.CreateLocalVariable("ShowHolidayPopup");
    showHolidayPopup.DataType = eSpace.BooleanType;
    showHolidayPopup.SetDefaultValue("False");
    showHolidayPopup.Description = null; // default value
    
    var popupError = holidays.CreateLocalVariable("PopupError");
    popupError.DataType = eSpace.TextType;
    popupError.SetDefaultValue(null); // default value
    popupError.Description = "Message of the last failed save (e.g. HolidayValidation), shown in the popup.";
    
    var pageError = holidays.CreateLocalVariable("PageError");
    pageError.DataType = eSpace.TextType;
    pageError.SetDefaultValue(null); // default value
    pageError.Description = "Message of the last failed activate/deactivate or copy, shown above the list.";
    holidays.OnDestroy.Destination = null; // default value
    holidays.OnReady.Destination = null; // default value
    holidays.OnRender.Destination = null; // default value
    
    var onChange_ShowInactiveCheckbox = holidays.CreateScreenAction("OnChange_ShowInactiveCheckbox");
    onChange_ShowInactiveCheckbox.Description = "Reloads GetHolidays with or without inactive holidays.";
    
    var startNode3 = onChange_ShowInactiveCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var onInitialize = holidays.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Shows the current year.";
    
    var startNode4 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var assignNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Current year";
    
    var assignment3 = assignNode3.CreateAssignment("SelectedYear", "Year(CurrDate())");
    
    var endNode5 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode5.IsDisabled = false; // default value
    holidays.OnInitialize.Destination = onInitialize;
    
    var refreshDataNode = onChange_ShowInactiveCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getHolidays;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode6 = onChange_ShowInactiveCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode6.IsDisabled = false; // default value
    
    var onClick_AddHolidayButton = holidays.CreateScreenAction("OnClick_AddHolidayButton");
    onClick_AddHolidayButton.Description = "Opens the popup for a new holiday (active, same date every year).";
    
    var startNode5 = onClick_AddHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var assignNode4 = onClick_AddHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "New holiday";
    
    var assignment4 = assignNode4.CreateAssignment("EditHoliday.Id", "NullIdentifier()");
    
    var assignment5 = assignNode4.CreateAssignment("EditHoliday.Name", "\"\"");
    
    var assignment6 = assignNode4.CreateAssignment("EditHoliday.HolidayDate", "NullDate()");
    
    var assignment7 = assignNode4.CreateAssignment("EditHoliday.IsActive", "True");
    
    var assignment8 = assignNode4.CreateAssignment("EditHoliday.IsFixedDate", "True");
    
    var assignment9 = assignNode4.CreateAssignment("PopupError", "\"\"");
    
    var assignment10 = assignNode4.CreateAssignment("ShowHolidayPopup", "True");
    
    var endNode7 = onClick_AddHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    endNode7.IsDisabled = false; // default value
    
    var onClick_CancelPopupButton = holidays.CreateScreenAction("OnClick_CancelPopupButton");
    onClick_CancelPopupButton.Description = "Closes the holiday popup without saving.";
    
    var startNode6 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 800;
    
    var assignNode5 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode6, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Close popup";
    
    var assignment11 = assignNode5.CreateAssignment("ShowHolidayPopup", "False");
    
    var endNode8 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    endNode8.IsDisabled = false; // default value
    
    var onClick_CopyYearButton = holidays.CreateScreenAction("OnClick_CopyYearButton");
    onClick_CopyYearButton.Description = "Copies the fixed-date holidays of SelectedYear to the next year through Holiday_CopyYear and reports how many were added.";
    
    var startNode7 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.IsDisabled = false; // default value
    startNode7.VerticalPosition = 800;
    
    var assignNode6 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Clear error";
    
    var assignment12 = assignNode6.CreateAssignment("PageError", "\"\"");
    
    var holiday_CopyYear = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CopyYear").ConnectedBelow(assignNode6, 1371);
    var holiday_CopyYear2 = claudeSheet_CS.ServerActions.Named("Holiday_CopyYear");
    holiday_CopyYear.Action = holiday_CopyYear2;
    holiday_CopyYear.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CopyYear.IsDisabled = false; // default value
    holiday_CopyYear.ServerRequestTimeout = null; // default value
    var toYear = holiday_CopyYear2.InputParameters.Named("ToYear");
    holiday_CopyYear.SetArgumentValue(toYear, "SelectedYear + 1");
    var fromYear = holiday_CopyYear2.InputParameters.Named("FromYear");
    holiday_CopyYear.SetArgumentValue(fromYear, "SelectedYear");
    
    var messageNode2 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(holiday_CopyYear, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Show copied count";
    messageNode2.SetMessage("If(Holiday_CopyYear.CopiedCount = 0, \"Nothing to copy: \" + (SelectedYear + 1) + \" already has these holidays.\", Holiday_CopyYear.CopiedCount + \" holiday(s) copied to \" + (SelectedYear + 1) + \".\")");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode9 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1371);
    endNode9.IsDisabled = false; // default value
    
    var allExceptions2 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode7 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "Keep error";
    
    var assignment13 = assignNode7.CreateAssignment("PageError", "AllExceptions.ExceptionMessage");
    
    var endNode10 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode7, 1371);
    endNode10.IsDisabled = false; // default value
    
    var onClick_EditHolidayLink = holidays.CreateScreenAction("OnClick_EditHolidayLink");
    onClick_EditHolidayLink.Description = "Opens the popup to edit a holiday.";
    
    var row = onClick_EditHolidayLink.CreateInputParameter("Row");
    row.DataType = holiday;
    row.Description = null; // default value
    row.IsMandatory = true; // default value
    
    var startNode8 = onClick_EditHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.HorizontalPosition = 3200;
    startNode8.IsDisabled = false; // default value
    startNode8.VerticalPosition = 800;
    
    var assignNode8 = onClick_EditHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode8, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "Edit holiday";
    
    var assignment14 = assignNode8.CreateAssignment("EditHoliday", "Row");
    
    var assignment15 = assignNode8.CreateAssignment("PopupError", "\"\"");
    
    var assignment16 = assignNode8.CreateAssignment("ShowHolidayPopup", "True");
    
    var endNode11 = onClick_EditHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode8, 1371);
    endNode11.IsDisabled = false; // default value
    
    var onClick_NextYearButton = holidays.CreateScreenAction("OnClick_NextYearButton");
    onClick_NextYearButton.Description = "Shows the next year.";
    
    var startNode9 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.HorizontalPosition = 3200;
    startNode9.IsDisabled = false; // default value
    startNode9.VerticalPosition = 800;
    
    var assignNode9 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode9, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = "Next year";
    
    var assignment17 = assignNode9.CreateAssignment("SelectedYear", "SelectedYear + 1");
    
    var assignment18 = assignNode9.CreateAssignment("PageError", "\"\"");
    
    var refreshDataNode2 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode9, 1371);
    refreshDataNode2.DataSource = getHolidays;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var endNode12 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode2, 1371);
    endNode12.IsDisabled = false; // default value
    
    var onClick_PreviousYearButton = holidays.CreateScreenAction("OnClick_PreviousYearButton");
    onClick_PreviousYearButton.Description = "Shows the previous year.";
    
    var startNode10 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.IsDisabled = false; // default value
    startNode10.VerticalPosition = 800;
    
    var assignNode10 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode10, 1371);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = "Previous year";
    
    var assignment19 = assignNode10.CreateAssignment("SelectedYear", "SelectedYear - 1");
    
    var assignment20 = assignNode10.CreateAssignment("PageError", "\"\"");
    
    var refreshDataNode3 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode10, 1371);
    refreshDataNode3.DataSource = getHolidays;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    
    var endNode13 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode3, 1371);
    endNode13.IsDisabled = false; // default value
    
    var onClick_SaveHolidayButton = holidays.CreateScreenAction("OnClick_SaveHolidayButton");
    onClick_SaveHolidayButton.Description = "Saves EditHoliday through Holiday_CreateOrUpdate, closes the popup and shows the holiday\'s year. HolidayValidation (or any other) error is shown in the popup.";
    
    var startNode11 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.IsDisabled = false; // default value
    startNode11.VerticalPosition = 800;
    
    var assignNode11 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode11, 1371);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = "Clear error";
    
    var assignment21 = assignNode11.CreateAssignment("PopupError", "\"\"");
    
    var holiday_CreateOrUpdate = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateOrUpdate").ConnectedBelow(assignNode11, 1371);
    var holiday_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("Holiday_CreateOrUpdate");
    holiday_CreateOrUpdate.Action = holiday_CreateOrUpdate2;
    holiday_CreateOrUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateOrUpdate.IsDisabled = false; // default value
    holiday_CreateOrUpdate.ServerRequestTimeout = null; // default value
    var holidayRecord = holiday_CreateOrUpdate2.InputParameters.Named("HolidayRecord");
    holiday_CreateOrUpdate.SetArgumentValue(holidayRecord, "EditHoliday");
    
    var assignNode12 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(holiday_CreateOrUpdate, 1371);
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = "Close and show year";
    
    var assignment22 = assignNode12.CreateAssignment("ShowHolidayPopup", "False");
    
    var assignment23 = assignNode12.CreateAssignment("SelectedYear", "Year(EditHoliday.HolidayDate)");
    
    var refreshDataNode4 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode12, 1371);
    refreshDataNode4.DataSource = getHolidays;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords(null); // default value
    refreshDataNode4.SetStartIndex(null); // default value
    
    var messageNode3 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1371);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = "Show saved";
    messageNode3.SetMessage("\"Holiday saved.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode14 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    endNode14.IsDisabled = false; // default value
    
    var allExceptions3 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 6856;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode13 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = "Keep error";
    
    var assignment24 = assignNode13.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode15 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode13, 1371);
    endNode15.IsDisabled = false; // default value
    
    var onClick_ToggleHolidayLink = holidays.CreateScreenAction("OnClick_ToggleHolidayLink");
    onClick_ToggleHolidayLink.Description = "Activates or deactivates a holiday through Holiday_SetActive and reloads GetHolidays.";
    
    var row2 = onClick_ToggleHolidayLink.CreateInputParameter("Row");
    row2.DataType = holiday;
    row2.Description = null; // default value
    row2.IsMandatory = true; // default value
    
    var startNode12 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.IsDisabled = false; // default value
    startNode12.VerticalPosition = 800;
    
    var assignNode14 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode12, 1371);
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = "Clear error";
    
    var assignment25 = assignNode14.CreateAssignment("PageError", "\"\"");
    
    var holiday_SetActive = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_SetActive").ConnectedBelow(assignNode14, 1371);
    var holiday_SetActive2 = claudeSheet_CS.ServerActions.Named("Holiday_SetActive");
    holiday_SetActive.Action = holiday_SetActive2;
    holiday_SetActive.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_SetActive.IsDisabled = false; // default value
    holiday_SetActive.ServerRequestTimeout = null; // default value
    var isActive = holiday_SetActive2.InputParameters.Named("IsActive");
    holiday_SetActive.SetArgumentValue(isActive, "not Row.IsActive");
    var holidayId = holiday_SetActive2.InputParameters.Named("HolidayId");
    holiday_SetActive.SetArgumentValue(holidayId, "Row.Id");
    
    var refreshDataNode5 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(holiday_SetActive, 1371);
    refreshDataNode5.DataSource = getHolidays;
    refreshDataNode5.IsDisabled = false; // default value
    refreshDataNode5.SetMaxRecords(null); // default value
    refreshDataNode5.SetStartIndex(null); // default value
    
    var messageNode4 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode5, 1371);
    messageNode4.IsDisabled = false; // default value
    messageNode4.Label = "Show status";
    messageNode4.SetMessage("If(Row.IsActive, \"Holiday deactivated.\", \"Holiday activated.\")");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode16 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1371);
    endNode16.IsDisabled = false; // default value
    
    var allExceptions4 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.IsDisabled = false; // default value
    allExceptions4.LogError = false;
    allExceptions4.VerticalPosition = 800;
    
    var assignNode15 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions4, 1371);
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = "Keep error";
    
    var assignment26 = assignNode15.CreateAssignment("PageError", "AllExceptions.ExceptionMessage");
    
    var endNode17 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode15, 1371);
    endNode17.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = holidays.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopMenuInstance.CustomStyle = null; // default value
    layoutTopMenuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    layoutTopMenuInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "3");
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Holidays";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var adminPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    adminPage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    adminPage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    adminPage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    adminPage.Animate = false; // default value
    adminPage.ColumnGap = null; // default value
    adminPage.ColumnSpan = null; // default value
    adminPage.CustomStyle = null; // default value
    adminPage.FlexBasis = null; // default value
    adminPage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    adminPage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    adminPage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    adminPage.Gap = null;
    adminPage.GridColumns = null; // default value
    adminPage.GridRows = null; // default value
    adminPage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    adminPage.RowGap = null; // default value
    adminPage.RowSpan = null; // default value
    adminPage.SetStyle("\"admin-page\"");
    adminPage.SetVisible("True"); // default value
    adminPage.VisibleWasExpression = false; // default value
    adminPage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    adminPage.Width = "(fill parent)"; // default value
    
    var toolbar = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Toolbar");
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
    
    var previousYearButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("PreviousYearButton");
    previousYearButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    previousYearButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    previousYearButton.ColumnGap = null; // default value
    previousYearButton.ColumnSpan = null; // default value
    previousYearButton.SetConfirmationMessage(null); // default value
    previousYearButton.CustomStyle = null; // default value
    previousYearButton.SetEnabled("True");
    previousYearButton.FlexBasis = null; // default value
    previousYearButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    previousYearButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    previousYearButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    previousYearButton.Gap = null;
    previousYearButton.GridColumns = null; // default value
    previousYearButton.GridRows = null; // default value
    previousYearButton.IsDefault = false; // default value
    previousYearButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    previousYearButton.RowGap = null; // default value
    previousYearButton.RowSpan = null; // default value
    previousYearButton.SetStyle("\"btn\""); // default value
    previousYearButton.SetVisible("True"); // default value
    previousYearButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    previousYearButton.Width = ""; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)previousYearButton.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "<";
    var builtinEvent = previousYearButton.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onClick_PreviousYearButton;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty = previousYearButton.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Previous year\"");
    
    var yearLabel = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("YearLabel");
    yearLabel.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    yearLabel.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    yearLabel.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    yearLabel.Animate = false; // default value
    yearLabel.ColumnGap = null; // default value
    yearLabel.ColumnSpan = null; // default value
    yearLabel.CustomStyle = null; // default value
    yearLabel.FlexBasis = null; // default value
    yearLabel.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    yearLabel.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    yearLabel.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    yearLabel.Gap = null;
    yearLabel.GridColumns = null; // default value
    yearLabel.GridRows = null; // default value
    yearLabel.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    yearLabel.RowGap = null; // default value
    yearLabel.RowSpan = null; // default value
    yearLabel.SetStyle("\"text-bold\"");
    yearLabel.SetVisible("True"); // default value
    yearLabel.VisibleWasExpression = false; // default value
    yearLabel.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    yearLabel.Width = "(fill parent)"; // default value
    
    var expression = yearLabel.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("SelectedYear");
    expression.Width = ""; // default value
    
    var extendedProperty2 = yearLabel.CreateExtendedProperty();
    extendedProperty2.Property = "aria-live";
    extendedProperty2.SetValue("\"polite\"");
    
    var nextYearButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NextYearButton");
    nextYearButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    nextYearButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    nextYearButton.ColumnGap = null; // default value
    nextYearButton.ColumnSpan = null; // default value
    nextYearButton.SetConfirmationMessage(null); // default value
    nextYearButton.CustomStyle = null; // default value
    nextYearButton.SetEnabled("True");
    nextYearButton.FlexBasis = null; // default value
    nextYearButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    nextYearButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    nextYearButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    nextYearButton.Gap = null;
    nextYearButton.GridColumns = null; // default value
    nextYearButton.GridRows = null; // default value
    nextYearButton.IsDefault = false; // default value
    nextYearButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    nextYearButton.RowGap = null; // default value
    nextYearButton.RowSpan = null; // default value
    nextYearButton.SetStyle("\"btn\""); // default value
    nextYearButton.SetVisible("True"); // default value
    nextYearButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    nextYearButton.Width = ""; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)nextYearButton.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = ">";
    var builtinEvent2 = nextYearButton.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = onClick_NextYearButton;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty3 = nextYearButton.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Next year\"");
    
    var showInactiveWrap = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ShowInactiveWrap");
    showInactiveWrap.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    showInactiveWrap.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    showInactiveWrap.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    showInactiveWrap.Animate = false; // default value
    showInactiveWrap.ColumnGap = null; // default value
    showInactiveWrap.ColumnSpan = null; // default value
    showInactiveWrap.CustomStyle = null; // default value
    showInactiveWrap.FlexBasis = null; // default value
    showInactiveWrap.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    showInactiveWrap.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    showInactiveWrap.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    showInactiveWrap.Gap = null;
    showInactiveWrap.GridColumns = null; // default value
    showInactiveWrap.GridRows = null; // default value
    showInactiveWrap.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    showInactiveWrap.RowGap = null; // default value
    showInactiveWrap.RowSpan = null; // default value
    showInactiveWrap.SetStyle("\"admin-toolbar__check\"");
    showInactiveWrap.SetVisible("True"); // default value
    showInactiveWrap.VisibleWasExpression = false; // default value
    showInactiveWrap.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    showInactiveWrap.Width = "(fill parent)"; // default value
    
    var showInactiveCheckbox = showInactiveWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox");
    showInactiveCheckbox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    showInactiveCheckbox.ColumnSpan = null; // default value
    showInactiveCheckbox.CustomStyle = null; // default value
    showInactiveCheckbox.SetEnabled("True");
    showInactiveCheckbox.FlexBasis = null; // default value
    showInactiveCheckbox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    showInactiveCheckbox.RowSpan = null; // default value
    showInactiveCheckbox.SetStyle("\"checkbox\""); // default value
    showInactiveCheckbox.SetVariable("ShowInactive");
    var builtinEvent3 = showInactiveCheckbox.OnChange;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = onChange_ShowInactiveCheckbox;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty4 = showInactiveCheckbox.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Show inactive holidays\"");
    
    var textWidget4 = showInactiveWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Show inactive";
    
    var addHolidayButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("AddHolidayButton");
    addHolidayButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    addHolidayButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    addHolidayButton.ColumnGap = null; // default value
    addHolidayButton.ColumnSpan = null; // default value
    addHolidayButton.SetConfirmationMessage(null); // default value
    addHolidayButton.CustomStyle = null; // default value
    addHolidayButton.SetEnabled("True");
    addHolidayButton.FlexBasis = null; // default value
    addHolidayButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    addHolidayButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    addHolidayButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    addHolidayButton.Gap = null;
    addHolidayButton.GridColumns = null; // default value
    addHolidayButton.GridRows = null; // default value
    addHolidayButton.IsDefault = false; // default value
    addHolidayButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    addHolidayButton.RowGap = null; // default value
    addHolidayButton.RowSpan = null; // default value
    addHolidayButton.SetStyle("\"btn btn-primary\"");
    addHolidayButton.SetVisible("True"); // default value
    addHolidayButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    addHolidayButton.Width = ""; // default value
    
    var textWidget5 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)addHolidayButton.Widgets.First();
    textWidget5.CustomStyle = null; // default value
    textWidget5.Name = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Add holiday";
    var builtinEvent4 = addHolidayButton.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = onClick_AddHolidayButton;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var copyYearButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CopyYearButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    copyYearButton.Widgets.ToList().ForEach(x => x.Delete());
    copyYearButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    copyYearButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    copyYearButton.ColumnGap = null; // default value
    copyYearButton.ColumnSpan = null; // default value
    copyYearButton.SetConfirmationMessage(null); // default value
    copyYearButton.CustomStyle = null; // default value
    copyYearButton.SetEnabled("GetHolidays.IsDataFetched");
    copyYearButton.FlexBasis = null; // default value
    copyYearButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    copyYearButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    copyYearButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    copyYearButton.Gap = null;
    copyYearButton.GridColumns = null; // default value
    copyYearButton.GridRows = null; // default value
    copyYearButton.IsDefault = false; // default value
    copyYearButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    copyYearButton.RowGap = null; // default value
    copyYearButton.RowSpan = null; // default value
    copyYearButton.SetStyle("\"btn\""); // default value
    copyYearButton.SetVisible("True"); // default value
    copyYearButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    copyYearButton.Width = ""; // default value
    
    var expression2 = copyYearButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("\"Copy fixed holidays to \" + (SelectedYear + 1)");
    expression2.Width = ""; // default value
    var builtinEvent5 = copyYearButton.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = onClick_CopyYearButton;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var hasPageError = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPageError");
    hasPageError.Animate = false; // default value
    hasPageError.SetCondition("PageError <> \"\"");
    hasPageError.CustomStyle = null; // default value
    hasPageError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var pageErrorBox = hasPageError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageErrorBox");
    pageErrorBox.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    pageErrorBox.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    pageErrorBox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    pageErrorBox.Animate = false; // default value
    pageErrorBox.ColumnGap = null; // default value
    pageErrorBox.ColumnSpan = null; // default value
    pageErrorBox.CustomStyle = null; // default value
    pageErrorBox.FlexBasis = null; // default value
    pageErrorBox.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    pageErrorBox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    pageErrorBox.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    pageErrorBox.Gap = null;
    pageErrorBox.GridColumns = null; // default value
    pageErrorBox.GridRows = null; // default value
    pageErrorBox.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    pageErrorBox.RowGap = null; // default value
    pageErrorBox.RowSpan = null; // default value
    pageErrorBox.SetStyle("\"admin-error\"");
    pageErrorBox.SetVisible("True"); // default value
    pageErrorBox.VisibleWasExpression = false; // default value
    pageErrorBox.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    pageErrorBox.Width = "(fill parent)"; // default value
    
    var expression3 = pageErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("PageError");
    expression3.Width = ""; // default value
    
    var extendedProperty5 = pageErrorBox.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"alert\"");
    
    var holidaysTable = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidaysTable");
    holidaysTable.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    holidaysTable.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidaysTable.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidaysTable.Animate = false; // default value
    holidaysTable.ColumnGap = null; // default value
    holidaysTable.ColumnSpan = null; // default value
    holidaysTable.CustomStyle = null; // default value
    holidaysTable.FlexBasis = null; // default value
    holidaysTable.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidaysTable.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidaysTable.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidaysTable.Gap = null;
    holidaysTable.GridColumns = null; // default value
    holidaysTable.GridRows = null; // default value
    holidaysTable.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidaysTable.RowGap = null; // default value
    holidaysTable.RowSpan = null; // default value
    holidaysTable.SetStyle("\"admin-table admin-table--5\"");
    holidaysTable.SetVisible("True"); // default value
    holidaysTable.VisibleWasExpression = false; // default value
    holidaysTable.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidaysTable.Width = "(fill parent)"; // default value
    
    var holidaysHeader = holidaysTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidaysHeader");
    holidaysHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    holidaysHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidaysHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidaysHeader.Animate = false; // default value
    holidaysHeader.ColumnGap = null; // default value
    holidaysHeader.ColumnSpan = null; // default value
    holidaysHeader.CustomStyle = null; // default value
    holidaysHeader.FlexBasis = null; // default value
    holidaysHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidaysHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidaysHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidaysHeader.Gap = null;
    holidaysHeader.GridColumns = null; // default value
    holidaysHeader.GridRows = null; // default value
    holidaysHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidaysHeader.RowGap = null; // default value
    holidaysHeader.RowSpan = null; // default value
    holidaysHeader.SetStyle("\"admin-table__row admin-table__head\"");
    holidaysHeader.SetVisible("True"); // default value
    holidaysHeader.VisibleWasExpression = false; // default value
    holidaysHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidaysHeader.Width = "(fill parent)"; // default value
    
    var container = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget6 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Date";
    
    var extendedProperty6 = container.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var container2 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget7 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Weekday";
    
    var extendedProperty7 = container2.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"columnheader\"");
    
    var container3 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget8 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Name";
    
    var extendedProperty8 = container3.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"columnheader\"");
    
    var container4 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget9 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.CustomStyle = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Status";
    
    var extendedProperty9 = container4.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"columnheader\"");
    
    var container5 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget10 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.CustomStyle = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Actions";
    
    var extendedProperty10 = container5.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"columnheader\"");
    
    var extendedProperty11 = holidaysHeader.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"row\"");
    
    var hasHolidays = holidaysTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasHolidays");
    hasHolidays.Animate = false; // default value
    hasHolidays.SetCondition("GetHolidays.Holidays.Length > 0 or not GetHolidays.IsDataFetched");
    hasHolidays.CustomStyle = null; // default value
    hasHolidays.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var holidayList = hasHolidays.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("HolidayList");
    holidayList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidayList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayList.AnimateItems = true; // default value
    holidayList.ColumnGap = null; // default value
    holidayList.ColumnSpan = null; // default value
    holidayList.CustomStyle = null; // default value
    holidayList.ExpandedInWebEditor = true; // default value
    holidayList.FlexBasis = null; // default value
    holidayList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidayList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidayList.Gap = null;
    holidayList.GridColumns = null; // default value
    holidayList.GridRows = null; // default value
    holidayList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidayList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    holidayList.RowGap = null; // default value
    holidayList.RowSpan = null; // default value
    holidayList.SetSource("GetHolidays.Holidays");
    holidayList.SetStyle("\"admin-table__body\"");
    holidayList.Tag = "div"; // default value
    holidayList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidayList.Width = "(fill parent)"; // default value
    
    var holidayRow = holidayList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayRow");
    holidayRow.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    holidayRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidayRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayRow.Animate = false; // default value
    holidayRow.ColumnGap = null; // default value
    holidayRow.ColumnSpan = null; // default value
    holidayRow.CustomStyle = null; // default value
    holidayRow.FlexBasis = null; // default value
    holidayRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidayRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidayRow.Gap = null;
    holidayRow.GridColumns = null; // default value
    holidayRow.GridRows = null; // default value
    holidayRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidayRow.RowGap = null; // default value
    holidayRow.RowSpan = null; // default value
    holidayRow.SetStyle("\"admin-table__row\"");
    holidayRow.SetVisible("True"); // default value
    holidayRow.VisibleWasExpression = false; // default value
    holidayRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidayRow.Width = "(fill parent)"; // default value
    
    var container6 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"admin-table__cell\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var expression4 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("FormatDateTime(DateToDateTime(GetHolidays.Holidays.Current.HolidayDate), \"dd/MM/yyyy\")");
    expression4.Width = ""; // default value
    
    var extendedProperty12 = container6.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"cell\"");
    
    var container7 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression5 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("FormatDateTime(DateToDateTime(GetHolidays.Holidays.Current.HolidayDate), \"dddd\")");
    expression5.Width = ""; // default value
    
    var extendedProperty13 = container7.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    
    var container8 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container8.SetStyle("\"admin-table__cell admin-table__name\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var expression6 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("GetHolidays.Holidays.Current.Name + If(GetHolidays.Holidays.Current.IsFixedDate, \"\", \" (movable)\")");
    expression6.Width = ""; // default value
    
    var extendedProperty14 = container8.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"cell\"");
    
    var container9 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container9.SetStyle("\"admin-table__cell \" + If(GetHolidays.Holidays.Current.IsActive, \"is-active\", \"is-inactive\")");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var expression7 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("If(GetHolidays.Holidays.Current.IsActive, \"Active\", \"Inactive\")");
    expression7.Width = ""; // default value
    
    var extendedProperty15 = container9.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"cell\"");
    
    var holidayActions = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayActions");
    holidayActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    holidayActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidayActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayActions.Animate = false; // default value
    holidayActions.ColumnGap = null; // default value
    holidayActions.ColumnSpan = null; // default value
    holidayActions.CustomStyle = null; // default value
    holidayActions.FlexBasis = null; // default value
    holidayActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidayActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidayActions.Gap = null;
    holidayActions.GridColumns = null; // default value
    holidayActions.GridRows = null; // default value
    holidayActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidayActions.RowGap = null; // default value
    holidayActions.RowSpan = null; // default value
    holidayActions.SetStyle("\"admin-table__cell admin-table__actions\"");
    holidayActions.SetVisible("True"); // default value
    holidayActions.VisibleWasExpression = false; // default value
    holidayActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidayActions.Width = "(fill parent)"; // default value
    
    var editHolidayLink = holidayActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("EditHolidayLink");
    editHolidayLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    editHolidayLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    editHolidayLink.ColumnGap = null; // default value
    editHolidayLink.ColumnSpan = null; // default value
    editHolidayLink.SetConfirmationMessage(null); // default value
    editHolidayLink.CustomStyle = null; // default value
    editHolidayLink.SetEnabled("True");
    editHolidayLink.FlexBasis = null; // default value
    editHolidayLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    editHolidayLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    editHolidayLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    editHolidayLink.Gap = null;
    editHolidayLink.GridColumns = null; // default value
    editHolidayLink.GridRows = null; // default value
    editHolidayLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    editHolidayLink.RowGap = null; // default value
    editHolidayLink.RowSpan = null; // default value
    editHolidayLink.SetStyle(null); // default value
    editHolidayLink.SetVisible("True"); // default value
    editHolidayLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    editHolidayLink.Width = ""; // default value
    
    var textWidget11 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)editHolidayLink.Widgets.First();
    textWidget11.CustomStyle = null; // default value
    textWidget11.Name = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Edit";
    var builtinEvent6 = editHolidayLink.OnClick;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = onClick_EditHolidayLink;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent6.SetArgumentValue(row, "GetHolidays.Holidays.Current");
    
    var toggleHolidayLink = holidayActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ToggleHolidayLink");
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    toggleHolidayLink.Widgets.ToList().ForEach(x => x.Delete());
    toggleHolidayLink.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    toggleHolidayLink.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    toggleHolidayLink.ColumnGap = null; // default value
    toggleHolidayLink.ColumnSpan = null; // default value
    toggleHolidayLink.SetConfirmationMessage(null); // default value
    toggleHolidayLink.CustomStyle = null; // default value
    toggleHolidayLink.SetEnabled("True");
    toggleHolidayLink.FlexBasis = null; // default value
    toggleHolidayLink.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    toggleHolidayLink.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    toggleHolidayLink.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    toggleHolidayLink.Gap = null;
    toggleHolidayLink.GridColumns = null; // default value
    toggleHolidayLink.GridRows = null; // default value
    toggleHolidayLink.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    toggleHolidayLink.RowGap = null; // default value
    toggleHolidayLink.RowSpan = null; // default value
    toggleHolidayLink.SetStyle(null); // default value
    toggleHolidayLink.SetVisible("True"); // default value
    toggleHolidayLink.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    toggleHolidayLink.Width = ""; // default value
    
    var expression8 = toggleHolidayLink.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("If(GetHolidays.Holidays.Current.IsActive, \"Deactivate\", \"Activate\")");
    expression8.Width = ""; // default value
    var builtinEvent7 = toggleHolidayLink.OnClick;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = onClick_ToggleHolidayLink;
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent7.SetArgumentValue(row2, "GetHolidays.Holidays.Current");
    
    var extendedProperty16 = holidayActions.CreateExtendedProperty();
    extendedProperty16.Property = "role";
    extendedProperty16.SetValue("\"cell\"");
    
    var extendedProperty17 = holidayRow.CreateExtendedProperty();
    extendedProperty17.Property = "role";
    extendedProperty17.SetValue("\"row\"");
    var builtinEvent8 = holidayList.OnScrollEnding;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = null; // default value
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var noHolidays = hasHolidays.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoHolidays");
    noHolidays.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noHolidays.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noHolidays.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noHolidays.Animate = false; // default value
    noHolidays.ColumnGap = null; // default value
    noHolidays.ColumnSpan = null; // default value
    noHolidays.CustomStyle = null; // default value
    noHolidays.FlexBasis = null; // default value
    noHolidays.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noHolidays.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noHolidays.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noHolidays.Gap = null;
    noHolidays.GridColumns = null; // default value
    noHolidays.GridRows = null; // default value
    noHolidays.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noHolidays.RowGap = null; // default value
    noHolidays.RowSpan = null; // default value
    noHolidays.SetStyle("\"admin-table__empty\"");
    noHolidays.SetVisible("True"); // default value
    noHolidays.VisibleWasExpression = false; // default value
    noHolidays.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noHolidays.Width = "(fill parent)"; // default value
    
    var expression9 = noHolidays.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("\"No holidays for \" + SelectedYear + \" yet.\"");
    expression9.Width = ""; // default value
    
    var extendedProperty18 = holidaysTable.CreateExtendedProperty();
    extendedProperty18.Property = "role";
    extendedProperty18.SetValue("\"table\"");
    
    var holidayPopup = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("HolidayPopup");
    holidayPopup.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidayPopup.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayPopup.ColumnGap = null; // default value
    holidayPopup.ColumnSpan = null; // default value
    holidayPopup.CustomStyle = null; // default value
    holidayPopup.ExpandedInWebEditor = true; // default value
    holidayPopup.FlexBasis = null; // default value
    holidayPopup.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidayPopup.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayPopup.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidayPopup.Gap = null;
    holidayPopup.GridColumns = null; // default value
    holidayPopup.GridRows = null; // default value
    holidayPopup.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidayPopup.RowGap = null; // default value
    holidayPopup.RowSpan = null; // default value
    holidayPopup.SetShowPopup("ShowHolidayPopup");
    holidayPopup.SetStyle("\"popup-dialog\""); // default value
    holidayPopup.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var holidayPopupBody = holidayPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayPopupBody");
    holidayPopupBody.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    holidayPopupBody.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidayPopupBody.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayPopupBody.Animate = false; // default value
    holidayPopupBody.ColumnGap = null; // default value
    holidayPopupBody.ColumnSpan = null; // default value
    holidayPopupBody.CustomStyle = null; // default value
    holidayPopupBody.FlexBasis = null; // default value
    holidayPopupBody.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidayPopupBody.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayPopupBody.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidayPopupBody.Gap = null;
    holidayPopupBody.GridColumns = null; // default value
    holidayPopupBody.GridRows = null; // default value
    holidayPopupBody.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidayPopupBody.RowGap = null; // default value
    holidayPopupBody.RowSpan = null; // default value
    holidayPopupBody.SetStyle("\"admin-popup\"");
    holidayPopupBody.SetVisible("True"); // default value
    holidayPopupBody.VisibleWasExpression = false; // default value
    holidayPopupBody.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidayPopupBody.Width = "(fill parent)"; // default value
    
    var advancedHtml = holidayPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    advancedHtml.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    advancedHtml.ColumnGap = null; // default value
    advancedHtml.ColumnSpan = null; // default value
    advancedHtml.CustomStyle = null; // default value
    advancedHtml.FlexBasis = null; // default value
    advancedHtml.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    advancedHtml.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    advancedHtml.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    advancedHtml.Gap = null;
    advancedHtml.GridColumns = null; // default value
    advancedHtml.GridRows = null; // default value
    advancedHtml.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    advancedHtml.RowGap = null; // default value
    advancedHtml.RowSpan = null; // default value
    advancedHtml.Tag = "h3";
    advancedHtml.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var expression10 = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("If(EditHoliday.Id = NullIdentifier(), \"Add holiday\", \"Edit holiday\")");
    expression10.Width = ""; // default value
    
    var hasPopupError = holidayPopupBody.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPopupError");
    hasPopupError.Animate = false; // default value
    hasPopupError.SetCondition("PopupError <> \"\"");
    hasPopupError.CustomStyle = null; // default value
    hasPopupError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var popupErrorBox = hasPopupError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupErrorBox");
    popupErrorBox.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    popupErrorBox.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    popupErrorBox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    popupErrorBox.Animate = false; // default value
    popupErrorBox.ColumnGap = null; // default value
    popupErrorBox.ColumnSpan = null; // default value
    popupErrorBox.CustomStyle = null; // default value
    popupErrorBox.FlexBasis = null; // default value
    popupErrorBox.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    popupErrorBox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    popupErrorBox.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    popupErrorBox.Gap = null;
    popupErrorBox.GridColumns = null; // default value
    popupErrorBox.GridRows = null; // default value
    popupErrorBox.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    popupErrorBox.RowGap = null; // default value
    popupErrorBox.RowSpan = null; // default value
    popupErrorBox.SetStyle("\"admin-error\"");
    popupErrorBox.SetVisible("True"); // default value
    popupErrorBox.VisibleWasExpression = false; // default value
    popupErrorBox.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    popupErrorBox.Width = "(fill parent)"; // default value
    
    var expression11 = popupErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle(null); // default value
    expression11.SetValue("PopupError");
    expression11.Width = ""; // default value
    
    var extendedProperty19 = popupErrorBox.CreateExtendedProperty();
    extendedProperty19.Property = "role";
    extendedProperty19.SetValue("\"alert\"");
    
    var holidayForm = holidayPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayForm");
    holidayForm.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    holidayForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    holidayForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayForm.Animate = false; // default value
    holidayForm.ColumnGap = null; // default value
    holidayForm.ColumnSpan = null; // default value
    holidayForm.CustomStyle = null; // default value
    holidayForm.FlexBasis = null; // default value
    holidayForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    holidayForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    holidayForm.Gap = null;
    holidayForm.GridColumns = null; // default value
    holidayForm.GridRows = null; // default value
    holidayForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    holidayForm.RowGap = null; // default value
    holidayForm.RowSpan = null; // default value
    holidayForm.SetStyle("\"admin-form\"");
    holidayForm.SetVisible("True"); // default value
    holidayForm.VisibleWasExpression = false; // default value
    holidayForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    holidayForm.Width = "(fill parent)"; // default value
    
    var textWidget12 = holidayForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.CustomStyle = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Date";
    
    var holidayDateInput = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("HolidayDateInput");
    holidayDateInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayDateInput.ColumnSpan = null; // default value
    holidayDateInput.CustomStyle = null; // default value
    holidayDateInput.SetEnabled("True");
    holidayDateInput.FlexBasis = null; // default value
    holidayDateInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    holidayDateInput.SetMandatory("True");
    holidayDateInput.MaxLength = null;
    holidayDateInput.SetPrompt(null); // default value
    holidayDateInput.RowSpan = null; // default value
    holidayDateInput.SetStyle("\"form-control\""); // default value
    holidayDateInput.SetVariable("EditHoliday.HolidayDate");
    holidayDateInput.Width = "(fill parent)"; // default value
    var builtinEvent9 = holidayDateInput.OnChange;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = null; // default value
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty20 = holidayDateInput.CreateExtendedProperty();
    extendedProperty20.Property = "aria-label";
    extendedProperty20.SetValue("\"Holiday date\"");
    
    var textWidget13 = holidayForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.CustomStyle = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Name";
    
    var holidayNameInput = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("HolidayNameInput");
    holidayNameInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    holidayNameInput.ColumnSpan = null; // default value
    holidayNameInput.CustomStyle = null; // default value
    holidayNameInput.SetEnabled("True");
    holidayNameInput.FlexBasis = null; // default value
    holidayNameInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    holidayNameInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    holidayNameInput.SetMandatory("True");
    holidayNameInput.MaxLength = 100;
    holidayNameInput.SetPrompt(null); // default value
    holidayNameInput.RowSpan = null; // default value
    holidayNameInput.SetStyle("\"form-control\""); // default value
    holidayNameInput.SetVariable("EditHoliday.Name");
    holidayNameInput.Width = "(fill parent)"; // default value
    var builtinEvent10 = holidayNameInput.OnChange;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = null; // default value
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty21 = holidayNameInput.CreateExtendedProperty();
    extendedProperty21.Property = "aria-label";
    extendedProperty21.SetValue("\"Holiday name\"");
    
    var extendedProperty22 = holidayNameInput.CreateExtendedProperty();
    extendedProperty22.Property = "maxlength";
    extendedProperty22.SetValue("\"100\"");
    
    var fixedDateWrap = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FixedDateWrap");
    fixedDateWrap.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    fixedDateWrap.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fixedDateWrap.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fixedDateWrap.Animate = false; // default value
    fixedDateWrap.ColumnGap = null; // default value
    fixedDateWrap.ColumnSpan = null; // default value
    fixedDateWrap.CustomStyle = null; // default value
    fixedDateWrap.FlexBasis = null; // default value
    fixedDateWrap.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fixedDateWrap.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fixedDateWrap.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fixedDateWrap.Gap = null;
    fixedDateWrap.GridColumns = null; // default value
    fixedDateWrap.GridRows = null; // default value
    fixedDateWrap.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fixedDateWrap.RowGap = null; // default value
    fixedDateWrap.RowSpan = null; // default value
    fixedDateWrap.SetStyle("\"admin-toolbar__check\"");
    fixedDateWrap.SetVisible("True"); // default value
    fixedDateWrap.VisibleWasExpression = false; // default value
    fixedDateWrap.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fixedDateWrap.Width = "(fill parent)"; // default value
    
    var fixedDateCheckbox = fixedDateWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("FixedDateCheckbox");
    fixedDateCheckbox.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fixedDateCheckbox.ColumnSpan = null; // default value
    fixedDateCheckbox.CustomStyle = null; // default value
    fixedDateCheckbox.SetEnabled("True");
    fixedDateCheckbox.FlexBasis = null; // default value
    fixedDateCheckbox.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fixedDateCheckbox.RowSpan = null; // default value
    fixedDateCheckbox.SetStyle("\"checkbox\""); // default value
    fixedDateCheckbox.SetVariable("EditHoliday.IsFixedDate");
    var builtinEvent11 = fixedDateCheckbox.OnChange;
    builtinEvent11.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent11.Destination = null; // default value
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty23 = fixedDateCheckbox.CreateExtendedProperty();
    extendedProperty23.Property = "aria-label";
    extendedProperty23.SetValue("\"Same date every year\"");
    
    var textWidget14 = fixedDateWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget14.CustomStyle = null; // default value
    textWidget14.SetStyleClasses(null); // default value
    textWidget14.Text = "Same date every year";
    
    var fixedDateHint = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FixedDateHint");
    fixedDateHint.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    fixedDateHint.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fixedDateHint.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fixedDateHint.Animate = false; // default value
    fixedDateHint.ColumnGap = null; // default value
    fixedDateHint.ColumnSpan = null; // default value
    fixedDateHint.CustomStyle = null; // default value
    fixedDateHint.FlexBasis = null; // default value
    fixedDateHint.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fixedDateHint.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fixedDateHint.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fixedDateHint.Gap = null;
    fixedDateHint.GridColumns = null; // default value
    fixedDateHint.GridRows = null; // default value
    fixedDateHint.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fixedDateHint.RowGap = null; // default value
    fixedDateHint.RowSpan = null; // default value
    fixedDateHint.SetStyle("\"admin-form__hint\"");
    fixedDateHint.SetVisible("True"); // default value
    fixedDateHint.VisibleWasExpression = false; // default value
    fixedDateHint.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fixedDateHint.Width = "(fill parent)"; // default value
    
    var textWidget15 = fixedDateHint.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.CustomStyle = null; // default value
    textWidget15.SetStyleClasses(null); // default value
    textWidget15.Text = "Clear it for movable holidays (Good Friday, Easter, Corpus Christi): they are not copied to the next year.";
    
    var popupActions = holidayPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupActions");
    popupActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    popupActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    popupActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    popupActions.Animate = false; // default value
    popupActions.ColumnGap = null; // default value
    popupActions.ColumnSpan = null; // default value
    popupActions.CustomStyle = null; // default value
    popupActions.FlexBasis = null; // default value
    popupActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    popupActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    popupActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    popupActions.Gap = null;
    popupActions.GridColumns = null; // default value
    popupActions.GridRows = null; // default value
    popupActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    popupActions.RowGap = null; // default value
    popupActions.RowSpan = null; // default value
    popupActions.SetStyle("\"admin-form__actions\"");
    popupActions.SetVisible("True"); // default value
    popupActions.VisibleWasExpression = false; // default value
    popupActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    popupActions.Width = "(fill parent)"; // default value
    
    var saveHolidayButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveHolidayButton");
    saveHolidayButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    saveHolidayButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    saveHolidayButton.ColumnGap = null; // default value
    saveHolidayButton.ColumnSpan = null; // default value
    saveHolidayButton.SetConfirmationMessage(null); // default value
    saveHolidayButton.CustomStyle = null; // default value
    saveHolidayButton.SetEnabled("True");
    saveHolidayButton.FlexBasis = null; // default value
    saveHolidayButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    saveHolidayButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    saveHolidayButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    saveHolidayButton.Gap = null;
    saveHolidayButton.GridColumns = null; // default value
    saveHolidayButton.GridRows = null; // default value
    saveHolidayButton.IsDefault = false; // default value
    saveHolidayButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    saveHolidayButton.RowGap = null; // default value
    saveHolidayButton.RowSpan = null; // default value
    saveHolidayButton.SetStyle("\"btn btn-primary\"");
    saveHolidayButton.SetVisible("True"); // default value
    saveHolidayButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveHolidayButton.Width = ""; // default value
    
    var textWidget16 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveHolidayButton.Widgets.First();
    textWidget16.CustomStyle = null; // default value
    textWidget16.Name = null; // default value
    textWidget16.SetStyleClasses(null); // default value
    textWidget16.Text = "Save";
    var builtinEvent12 = saveHolidayButton.OnClick;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent12.Destination = onClick_SaveHolidayButton;
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelPopupButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelPopupButton");
    cancelPopupButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    cancelPopupButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    cancelPopupButton.ColumnGap = null; // default value
    cancelPopupButton.ColumnSpan = null; // default value
    cancelPopupButton.SetConfirmationMessage(null); // default value
    cancelPopupButton.CustomStyle = null; // default value
    cancelPopupButton.SetEnabled("True");
    cancelPopupButton.FlexBasis = null; // default value
    cancelPopupButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    cancelPopupButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    cancelPopupButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    cancelPopupButton.Gap = null;
    cancelPopupButton.GridColumns = null; // default value
    cancelPopupButton.GridRows = null; // default value
    cancelPopupButton.IsDefault = false; // default value
    cancelPopupButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    cancelPopupButton.RowGap = null; // default value
    cancelPopupButton.RowSpan = null; // default value
    cancelPopupButton.SetStyle("\"btn\""); // default value
    cancelPopupButton.SetVisible("True"); // default value
    cancelPopupButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    cancelPopupButton.Width = ""; // default value
    
    var textWidget17 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)cancelPopupButton.Widgets.First();
    textWidget17.CustomStyle = null; // default value
    textWidget17.Name = null; // default value
    textWidget17.SetStyleClasses(null); // default value
    textWidget17.Text = "Cancel";
    var builtinEvent13 = cancelPopupButton.OnClick;
    builtinEvent13.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent13.Destination = onClick_CancelPopupButton;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty24 = holidayPopupBody.CreateExtendedProperty();
    extendedProperty24.Property = "role";
    extendedProperty24.SetValue("\"dialog\"");
}
