# Holidays

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
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
    holidays.Description = "Admin holiday calendar per year (SheetAdmin): year selector, list with weekday and status, add/edit in a popup, activate/deactivate, copy the fixed-date holidays to the next year.";
    holidays.HorizontalPosition = 400;
    holidays.SetTitle("\"Holidays\"");
    holidays.VerticalPosition = 2400;
    
    var getHolidays = holidays.CreateDataAction("GetHolidays");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getHolidays.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var holiday_GetList = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_GetList").ConnectedBelow(startNode, 1371);
    var holiday_GetList2 = claudeSheet_CS.ServerActions.Named("Holiday_GetList");
    holiday_GetList.Action = holiday_GetList2;
    var includeInactive = holiday_GetList2.InputParameters.Named("IncludeInactive");
    holiday_GetList.SetArgumentValue(includeInactive, "ShowInactive");
    var year = holiday_GetList2.InputParameters.Named("Year");
    holiday_GetList.SetArgumentValue(year, "SelectedYear");
    
    var assignNode = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(holiday_GetList, 1371);
    assignNode.Label = "Set holidays";
    
    var assignment = assignNode.CreateAssignment("Holidays", "Holiday_GetList.Holidays");
    
    var endNode = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var allExceptions = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var holidays2 = getHolidays.CreateOutputParameter("Holidays");
    var holiday = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Holiday");
    holidays2.DataType = eSpace.GetOrCreateListType(holiday);
    
    var onAfterFetch_GetHolidays = holidays.CreateScreenAction("OnAfterFetch_GetHolidays");
    onAfterFetch_GetHolidays.Description = "Shows an error message when GetHolidays failed.";
    
    var startNode2 = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetHolidays.ErrorMessage <> \"\"");
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    
    var messageNode = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetHolidays.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetHolidays.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getHolidays.OnAfterFetch.Destination = onAfterFetch_GetHolidays;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getHolidays.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var selectedYear = holidays.CreateLocalVariable("SelectedYear");
    selectedYear.DataType = eSpace.IntegerType;
    selectedYear.Description = "Year shown; set to the current year in OnInitialize.";
    
    var showInactive = holidays.CreateLocalVariable("ShowInactive");
    showInactive.DataType = eSpace.BooleanType;
    showInactive.SetDefaultValue("False");
    
    var editHoliday = holidays.CreateLocalVariable("EditHoliday");
    editHoliday.DataType = holiday;
    editHoliday.Description = "Holiday being added or edited in the popup.";
    
    var showHolidayPopup = holidays.CreateLocalVariable("ShowHolidayPopup");
    showHolidayPopup.DataType = eSpace.BooleanType;
    showHolidayPopup.SetDefaultValue("False");
    
    var popupError = holidays.CreateLocalVariable("PopupError");
    popupError.DataType = eSpace.TextType;
    popupError.Description = "Message of the last failed save (e.g. HolidayValidation), shown in the popup.";
    
    var pageError = holidays.CreateLocalVariable("PageError");
    pageError.DataType = eSpace.TextType;
    pageError.Description = "Message of the last failed activate/deactivate or copy, shown above the list.";
    
    var onChange_ShowInactiveCheckbox = holidays.CreateScreenAction("OnChange_ShowInactiveCheckbox");
    onChange_ShowInactiveCheckbox.Description = "Reloads GetHolidays with or without inactive holidays.";
    
    var startNode3 = onChange_ShowInactiveCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var onInitialize = holidays.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Shows the current year.";
    
    var startNode4 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.VerticalPosition = 800;
    
    var assignNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode3.Label = "Current year";
    
    var assignment3 = assignNode3.CreateAssignment("SelectedYear", "Year(CurrDate())");
    
    var endNode5 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    holidays.OnInitialize.Destination = onInitialize;
    
    var refreshDataNode = onChange_ShowInactiveCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getHolidays;
    
    var endNode6 = onChange_ShowInactiveCheckbox.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    
    var onClick_AddHolidayButton = holidays.CreateScreenAction("OnClick_AddHolidayButton");
    onClick_AddHolidayButton.Description = "Opens the popup for a new holiday (active, same date every year).";
    
    var startNode5 = onClick_AddHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.VerticalPosition = 800;
    
    var assignNode4 = onClick_AddHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode4.Label = "New holiday";
    
    var assignment4 = assignNode4.CreateAssignment("EditHoliday.Id", "NullIdentifier()");
    
    var assignment5 = assignNode4.CreateAssignment("EditHoliday.Name", "\"\"");
    
    var assignment6 = assignNode4.CreateAssignment("EditHoliday.HolidayDate", "NullDate()");
    
    var assignment7 = assignNode4.CreateAssignment("EditHoliday.IsActive", "True");
    
    var assignment8 = assignNode4.CreateAssignment("EditHoliday.IsFixedDate", "True");
    
    var assignment9 = assignNode4.CreateAssignment("PopupError", "\"\"");
    
    var assignment10 = assignNode4.CreateAssignment("ShowHolidayPopup", "True");
    
    var endNode7 = onClick_AddHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    
    var onClick_CancelPopupButton = holidays.CreateScreenAction("OnClick_CancelPopupButton");
    onClick_CancelPopupButton.Description = "Closes the holiday popup without saving.";
    
    var startNode6 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.VerticalPosition = 800;
    
    var assignNode5 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode6, 1371);
    assignNode5.Label = "Close popup";
    
    var assignment11 = assignNode5.CreateAssignment("ShowHolidayPopup", "False");
    
    var endNode8 = onClick_CancelPopupButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    
    var onClick_CopyYearButton = holidays.CreateScreenAction("OnClick_CopyYearButton");
    onClick_CopyYearButton.Description = "Copies the fixed-date holidays of SelectedYear to the next year through Holiday_CopyYear and reports how many were added.";
    
    var startNode7 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.VerticalPosition = 800;
    
    var assignNode6 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode6.Label = "Clear error";
    
    var assignment12 = assignNode6.CreateAssignment("PageError", "\"\"");
    
    var holiday_CopyYear = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CopyYear").ConnectedBelow(assignNode6, 1371);
    var holiday_CopyYear2 = claudeSheet_CS.ServerActions.Named("Holiday_CopyYear");
    holiday_CopyYear.Action = holiday_CopyYear2;
    var toYear = holiday_CopyYear2.InputParameters.Named("ToYear");
    holiday_CopyYear.SetArgumentValue(toYear, "SelectedYear + 1");
    var fromYear = holiday_CopyYear2.InputParameters.Named("FromYear");
    holiday_CopyYear.SetArgumentValue(fromYear, "SelectedYear");
    
    var messageNode2 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(holiday_CopyYear, 1371);
    messageNode2.Label = "Show copied count";
    messageNode2.SetMessage("If(Holiday_CopyYear.CopiedCount = 0, \"Nothing to copy: \" + (SelectedYear + 1) + \" already has these holidays.\", Holiday_CopyYear.CopiedCount + \" holiday(s) copied to \" + (SelectedYear + 1) + \".\")");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode9 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1371);
    
    var allExceptions2 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode7 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode7.Label = "Keep error";
    
    var assignment13 = assignNode7.CreateAssignment("PageError", "AllExceptions.ExceptionMessage");
    
    var endNode10 = onClick_CopyYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode7, 1371);
    
    var onClick_EditHolidayLink = holidays.CreateScreenAction("OnClick_EditHolidayLink");
    onClick_EditHolidayLink.Description = "Opens the popup to edit a holiday.";
    
    var row = onClick_EditHolidayLink.CreateInputParameter("Row");
    row.DataType = holiday;
    
    var startNode8 = onClick_EditHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.HorizontalPosition = 3200;
    startNode8.VerticalPosition = 800;
    
    var assignNode8 = onClick_EditHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode8, 1371);
    assignNode8.Label = "Edit holiday";
    
    var assignment14 = assignNode8.CreateAssignment("EditHoliday", "Row");
    
    var assignment15 = assignNode8.CreateAssignment("PopupError", "\"\"");
    
    var assignment16 = assignNode8.CreateAssignment("ShowHolidayPopup", "True");
    
    var endNode11 = onClick_EditHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode8, 1371);
    
    var onClick_NextYearButton = holidays.CreateScreenAction("OnClick_NextYearButton");
    onClick_NextYearButton.Description = "Shows the next year.";
    
    var startNode9 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.HorizontalPosition = 3200;
    startNode9.VerticalPosition = 800;
    
    var assignNode9 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode9, 1371);
    assignNode9.Label = "Next year";
    
    var assignment17 = assignNode9.CreateAssignment("SelectedYear", "SelectedYear + 1");
    
    var assignment18 = assignNode9.CreateAssignment("PageError", "\"\"");
    
    var refreshDataNode2 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode9, 1371);
    refreshDataNode2.DataSource = getHolidays;
    
    var endNode12 = onClick_NextYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode2, 1371);
    
    var onClick_PreviousYearButton = holidays.CreateScreenAction("OnClick_PreviousYearButton");
    onClick_PreviousYearButton.Description = "Shows the previous year.";
    
    var startNode10 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.VerticalPosition = 800;
    
    var assignNode10 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode10, 1371);
    assignNode10.Label = "Previous year";
    
    var assignment19 = assignNode10.CreateAssignment("SelectedYear", "SelectedYear - 1");
    
    var assignment20 = assignNode10.CreateAssignment("PageError", "\"\"");
    
    var refreshDataNode3 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode10, 1371);
    refreshDataNode3.DataSource = getHolidays;
    
    var endNode13 = onClick_PreviousYearButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode3, 1371);
    
    var onClick_SaveHolidayButton = holidays.CreateScreenAction("OnClick_SaveHolidayButton");
    onClick_SaveHolidayButton.Description = "Saves EditHoliday through Holiday_CreateOrUpdate, closes the popup and shows the holiday\'s year. HolidayValidation (or any other) error is shown in the popup.";
    
    var startNode11 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.VerticalPosition = 800;
    
    var assignNode11 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode11, 1371);
    assignNode11.Label = "Clear error";
    
    var assignment21 = assignNode11.CreateAssignment("PopupError", "\"\"");
    
    var holiday_CreateOrUpdate = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateOrUpdate").ConnectedBelow(assignNode11, 1371);
    var holiday_CreateOrUpdate2 = claudeSheet_CS.ServerActions.Named("Holiday_CreateOrUpdate");
    holiday_CreateOrUpdate.Action = holiday_CreateOrUpdate2;
    var holidayRecord = holiday_CreateOrUpdate2.InputParameters.Named("HolidayRecord");
    holiday_CreateOrUpdate.SetArgumentValue(holidayRecord, "EditHoliday");
    
    var assignNode12 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(holiday_CreateOrUpdate, 1371);
    assignNode12.Label = "Close and show year";
    
    var assignment22 = assignNode12.CreateAssignment("ShowHolidayPopup", "False");
    
    var assignment23 = assignNode12.CreateAssignment("SelectedYear", "Year(EditHoliday.HolidayDate)");
    
    var refreshDataNode4 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode12, 1371);
    refreshDataNode4.DataSource = getHolidays;
    
    var messageNode3 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1371);
    messageNode3.Label = "Show saved";
    messageNode3.SetMessage("\"Holiday saved.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode14 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    
    var allExceptions3 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 6856;
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode13 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode13.Label = "Keep error";
    
    var assignment24 = assignNode13.CreateAssignment("PopupError", "AllExceptions.ExceptionMessage");
    
    var endNode15 = onClick_SaveHolidayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode13, 1371);
    
    var onClick_ToggleHolidayLink = holidays.CreateScreenAction("OnClick_ToggleHolidayLink");
    onClick_ToggleHolidayLink.Description = "Activates or deactivates a holiday through Holiday_SetActive and reloads GetHolidays.";
    
    var row2 = onClick_ToggleHolidayLink.CreateInputParameter("Row");
    row2.DataType = holiday;
    
    var startNode12 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.VerticalPosition = 800;
    
    var assignNode14 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode12, 1371);
    assignNode14.Label = "Clear error";
    
    var assignment25 = assignNode14.CreateAssignment("PageError", "\"\"");
    
    var holiday_SetActive = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_SetActive").ConnectedBelow(assignNode14, 1371);
    var holiday_SetActive2 = claudeSheet_CS.ServerActions.Named("Holiday_SetActive");
    holiday_SetActive.Action = holiday_SetActive2;
    var isActive = holiday_SetActive2.InputParameters.Named("IsActive");
    holiday_SetActive.SetArgumentValue(isActive, "not Row.IsActive");
    var holidayId = holiday_SetActive2.InputParameters.Named("HolidayId");
    holiday_SetActive.SetArgumentValue(holidayId, "Row.Id");
    
    var refreshDataNode5 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(holiday_SetActive, 1371);
    refreshDataNode5.DataSource = getHolidays;
    
    var messageNode4 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode5, 1371);
    messageNode4.Label = "Show status";
    messageNode4.SetMessage("If(Row.IsActive, \"Holiday deactivated.\", \"Holiday activated.\")");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode16 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1371);
    
    var allExceptions4 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.LogError = false;
    allExceptions4.VerticalPosition = 800;
    
    var assignNode15 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions4, 1371);
    assignNode15.Label = "Keep error";
    
    var assignment26 = assignNode15.CreateAssignment("PageError", "AllExceptions.ExceptionMessage");
    
    var endNode17 = onClick_ToggleHolidayLink.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode15, 1371);
    
    var layoutTopMenuInstance = holidays.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "3");
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.Text = "Holidays";
    
    var placeholderContentWidget3 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var adminPage = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("AdminPage");
    adminPage.Gap = null;
    adminPage.SetStyle("\"admin-page\"");
    
    var toolbar = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Toolbar");
    toolbar.Gap = null;
    toolbar.SetStyle("\"admin-toolbar\"");
    
    var previousYearButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("PreviousYearButton");
    previousYearButton.SetEnabled("True");
    previousYearButton.Gap = null;
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)previousYearButton.Widgets.First();
    textWidget2.Text = "<";
    var builtinEvent = previousYearButton.OnClick;
    builtinEvent.Destination = onClick_PreviousYearButton;
    
    var extendedProperty = previousYearButton.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Previous year\"");
    
    var yearLabel = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("YearLabel");
    yearLabel.Gap = null;
    yearLabel.SetStyle("\"text-bold\"");
    
    var expression = yearLabel.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.SetValue("SelectedYear");
    
    var extendedProperty2 = yearLabel.CreateExtendedProperty();
    extendedProperty2.Property = "aria-live";
    extendedProperty2.SetValue("\"polite\"");
    
    var nextYearButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NextYearButton");
    nextYearButton.SetEnabled("True");
    nextYearButton.Gap = null;
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)nextYearButton.Widgets.First();
    textWidget3.Text = ">";
    var builtinEvent2 = nextYearButton.OnClick;
    builtinEvent2.Destination = onClick_NextYearButton;
    
    var extendedProperty3 = nextYearButton.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Next year\"");
    
    var showInactiveWrap = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ShowInactiveWrap");
    showInactiveWrap.Gap = null;
    showInactiveWrap.SetStyle("\"admin-toolbar__check\"");
    
    var showInactiveCheckbox = showInactiveWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("ShowInactiveCheckbox");
    showInactiveCheckbox.SetEnabled("True");
    showInactiveCheckbox.SetVariable("ShowInactive");
    var builtinEvent3 = showInactiveCheckbox.OnChange;
    builtinEvent3.Destination = onChange_ShowInactiveCheckbox;
    
    var extendedProperty4 = showInactiveCheckbox.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Show inactive holidays\"");
    
    var textWidget4 = showInactiveWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.Text = "Show inactive";
    
    var addHolidayButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("AddHolidayButton");
    addHolidayButton.SetEnabled("True");
    addHolidayButton.Gap = null;
    addHolidayButton.SetStyle("\"btn btn-primary\"");
    
    var textWidget5 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)addHolidayButton.Widgets.First();
    textWidget5.Text = "Add holiday";
    var builtinEvent4 = addHolidayButton.OnClick;
    builtinEvent4.Destination = onClick_AddHolidayButton;
    
    var copyYearButton = toolbar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CopyYearButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    copyYearButton.Widgets.ToList().ForEach(x => x.Delete());
    copyYearButton.SetEnabled("GetHolidays.IsDataFetched");
    copyYearButton.Gap = null;
    
    var expression2 = copyYearButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("\"Copy fixed holidays to \" + (SelectedYear + 1)");
    var builtinEvent5 = copyYearButton.OnClick;
    builtinEvent5.Destination = onClick_CopyYearButton;
    
    var hasPageError = adminPage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPageError");
    hasPageError.SetCondition("PageError <> \"\"");
    
    var pageErrorBox = hasPageError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageErrorBox");
    pageErrorBox.Gap = null;
    pageErrorBox.SetStyle("\"admin-error\"");
    
    var expression3 = pageErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("PageError");
    
    var extendedProperty5 = pageErrorBox.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"alert\"");
    
    var holidaysTable = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidaysTable");
    holidaysTable.Gap = null;
    holidaysTable.SetStyle("\"admin-table admin-table--5\"");
    
    var holidaysHeader = holidaysTable.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidaysHeader");
    holidaysHeader.Gap = null;
    holidaysHeader.SetStyle("\"admin-table__row admin-table__head\"");
    
    var container = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"admin-table__cell\"");
    
    var textWidget6 = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.Text = "Date";
    
    var extendedProperty6 = container.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"columnheader\"");
    
    var container2 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"admin-table__cell\"");
    
    var textWidget7 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.Text = "Weekday";
    
    var extendedProperty7 = container2.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"columnheader\"");
    
    var container3 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"admin-table__cell\"");
    
    var textWidget8 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.Text = "Name";
    
    var extendedProperty8 = container3.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"columnheader\"");
    
    var container4 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"admin-table__cell\"");
    
    var textWidget9 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.Text = "Status";
    
    var extendedProperty9 = container4.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"columnheader\"");
    
    var container5 = holidaysHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"admin-table__cell\"");
    
    var textWidget10 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.Text = "Actions";
    
    var extendedProperty10 = container5.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"columnheader\"");
    
    var extendedProperty11 = holidaysHeader.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"row\"");
    
    var hasHolidays = holidaysTable.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasHolidays");
    hasHolidays.SetCondition("GetHolidays.Holidays.Length > 0 or not GetHolidays.IsDataFetched");
    
    var holidayList = hasHolidays.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("HolidayList");
    holidayList.Gap = null;
    holidayList.SetSource("GetHolidays.Holidays");
    holidayList.SetStyle("\"admin-table__body\"");
    
    var holidayRow = holidayList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayRow");
    holidayRow.Gap = null;
    holidayRow.SetStyle("\"admin-table__row\"");
    
    var container6 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"admin-table__cell\"");
    
    var expression4 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.SetValue("FormatDateTime(DateToDateTime(GetHolidays.Holidays.Current.HolidayDate), \"dd/MM/yyyy\")");
    
    var extendedProperty12 = container6.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"cell\"");
    
    var container7 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Gap = null;
    container7.SetStyle("\"admin-table__cell\"");
    
    var expression5 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.SetValue("FormatDateTime(DateToDateTime(GetHolidays.Holidays.Current.HolidayDate), \"dddd\")");
    
    var extendedProperty13 = container7.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    
    var container8 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Gap = null;
    container8.SetStyle("\"admin-table__cell admin-table__name\"");
    
    var expression6 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.SetValue("GetHolidays.Holidays.Current.Name + If(GetHolidays.Holidays.Current.IsFixedDate, \"\", \" (movable)\")");
    
    var extendedProperty14 = container8.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"cell\"");
    
    var container9 = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Gap = null;
    container9.SetStyle("\"admin-table__cell \" + If(GetHolidays.Holidays.Current.IsActive, \"is-active\", \"is-inactive\")");
    
    var expression7 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.SetValue("If(GetHolidays.Holidays.Current.IsActive, \"Active\", \"Inactive\")");
    
    var extendedProperty15 = container9.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"cell\"");
    
    var holidayActions = holidayRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayActions");
    holidayActions.Gap = null;
    holidayActions.SetStyle("\"admin-table__cell admin-table__actions\"");
    
    var editHolidayLink = holidayActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("EditHolidayLink");
    editHolidayLink.SetEnabled("True");
    editHolidayLink.Gap = null;
    
    var textWidget11 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)editHolidayLink.Widgets.First();
    textWidget11.Text = "Edit";
    var builtinEvent6 = editHolidayLink.OnClick;
    builtinEvent6.Destination = onClick_EditHolidayLink;
    builtinEvent6.SetArgumentValue(row, "GetHolidays.Holidays.Current");
    
    var toggleHolidayLink = holidayActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>("ToggleHolidayLink");
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    toggleHolidayLink.Widgets.ToList().ForEach(x => x.Delete());
    toggleHolidayLink.SetEnabled("True");
    toggleHolidayLink.Gap = null;
    
    var expression8 = toggleHolidayLink.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.SetValue("If(GetHolidays.Holidays.Current.IsActive, \"Deactivate\", \"Activate\")");
    var builtinEvent7 = toggleHolidayLink.OnClick;
    builtinEvent7.Destination = onClick_ToggleHolidayLink;
    builtinEvent7.SetArgumentValue(row2, "GetHolidays.Holidays.Current");
    
    var extendedProperty16 = holidayActions.CreateExtendedProperty();
    extendedProperty16.Property = "role";
    extendedProperty16.SetValue("\"cell\"");
    
    var extendedProperty17 = holidayRow.CreateExtendedProperty();
    extendedProperty17.Property = "role";
    extendedProperty17.SetValue("\"row\"");
    var builtinEvent8 = holidayList.OnScrollEnding;
    
    var noHolidays = hasHolidays.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoHolidays");
    noHolidays.Gap = null;
    noHolidays.SetStyle("\"admin-table__empty\"");
    
    var expression9 = noHolidays.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.SetValue("\"No holidays for \" + SelectedYear + \" yet.\"");
    
    var extendedProperty18 = holidaysTable.CreateExtendedProperty();
    extendedProperty18.Property = "role";
    extendedProperty18.SetValue("\"table\"");
    
    var holidayPopup = adminPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("HolidayPopup");
    holidayPopup.Gap = null;
    holidayPopup.SetShowPopup("ShowHolidayPopup");
    
    var holidayPopupBody = holidayPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayPopupBody");
    holidayPopupBody.Gap = null;
    holidayPopupBody.SetStyle("\"admin-popup\"");
    
    var advancedHtml = holidayPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.Gap = null;
    advancedHtml.Tag = "h3";
    
    var expression10 = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.SetValue("If(EditHoliday.Id = NullIdentifier(), \"Add holiday\", \"Edit holiday\")");
    
    var hasPopupError = holidayPopupBody.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasPopupError");
    hasPopupError.SetCondition("PopupError <> \"\"");
    
    var popupErrorBox = hasPopupError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupErrorBox");
    popupErrorBox.Gap = null;
    popupErrorBox.SetStyle("\"admin-error\"");
    
    var expression11 = popupErrorBox.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.SetValue("PopupError");
    
    var extendedProperty19 = popupErrorBox.CreateExtendedProperty();
    extendedProperty19.Property = "role";
    extendedProperty19.SetValue("\"alert\"");
    
    var holidayForm = holidayPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("HolidayForm");
    holidayForm.Gap = null;
    holidayForm.SetStyle("\"admin-form\"");
    
    var textWidget12 = holidayForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.Text = "Date";
    
    var holidayDateInput = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("HolidayDateInput");
    holidayDateInput.SetEnabled("True");
    holidayDateInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    holidayDateInput.SetMandatory("True");
    holidayDateInput.MaxLength = null;
    holidayDateInput.SetVariable("EditHoliday.HolidayDate");
    var builtinEvent9 = holidayDateInput.OnChange;
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty20 = holidayDateInput.CreateExtendedProperty();
    extendedProperty20.Property = "aria-label";
    extendedProperty20.SetValue("\"Holiday date\"");
    
    var textWidget13 = holidayForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.Text = "Name";
    
    var holidayNameInput = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("HolidayNameInput");
    holidayNameInput.SetEnabled("True");
    holidayNameInput.SetMandatory("True");
    holidayNameInput.MaxLength = 100;
    holidayNameInput.SetVariable("EditHoliday.Name");
    var builtinEvent10 = holidayNameInput.OnChange;
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty21 = holidayNameInput.CreateExtendedProperty();
    extendedProperty21.Property = "aria-label";
    extendedProperty21.SetValue("\"Holiday name\"");
    
    var extendedProperty22 = holidayNameInput.CreateExtendedProperty();
    extendedProperty22.Property = "maxlength";
    extendedProperty22.SetValue("\"100\"");
    
    var fixedDateWrap = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FixedDateWrap");
    fixedDateWrap.Gap = null;
    fixedDateWrap.SetStyle("\"admin-toolbar__check\"");
    
    var fixedDateCheckbox = fixedDateWrap.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("FixedDateCheckbox");
    fixedDateCheckbox.SetEnabled("True");
    fixedDateCheckbox.SetVariable("EditHoliday.IsFixedDate");
    var builtinEvent11 = fixedDateCheckbox.OnChange;
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty23 = fixedDateCheckbox.CreateExtendedProperty();
    extendedProperty23.Property = "aria-label";
    extendedProperty23.SetValue("\"Same date every year\"");
    
    var textWidget14 = fixedDateWrap.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget14.Text = "Same date every year";
    
    var fixedDateHint = holidayForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FixedDateHint");
    fixedDateHint.Gap = null;
    fixedDateHint.SetStyle("\"admin-form__hint\"");
    
    var textWidget15 = fixedDateHint.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.Text = "Clear it for movable holidays (Good Friday, Easter, Corpus Christi): they are not copied to the next year.";
    
    var popupActions = holidayPopupBody.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupActions");
    popupActions.Gap = null;
    popupActions.SetStyle("\"admin-form__actions\"");
    
    var saveHolidayButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveHolidayButton");
    saveHolidayButton.SetEnabled("True");
    saveHolidayButton.Gap = null;
    saveHolidayButton.SetStyle("\"btn btn-primary\"");
    
    var textWidget16 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)saveHolidayButton.Widgets.First();
    textWidget16.Text = "Save";
    var builtinEvent12 = saveHolidayButton.OnClick;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent12.Destination = onClick_SaveHolidayButton;
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelPopupButton = popupActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelPopupButton");
    cancelPopupButton.SetEnabled("True");
    cancelPopupButton.Gap = null;
    
    var textWidget17 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)cancelPopupButton.Widgets.First();
    textWidget17.Text = "Cancel";
    var builtinEvent13 = cancelPopupButton.OnClick;
    builtinEvent13.Destination = onClick_CancelPopupButton;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty24 = holidayPopupBody.CreateExtendedProperty();
    extendedProperty24.Property = "role";
    extendedProperty24.SetValue("\"dialog\"");
}
```
