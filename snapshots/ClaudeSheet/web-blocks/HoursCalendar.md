# HoursCalendar

Block in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'HoursCalendar' ***/
    var hoursCalendar = sheetBlocks.CreateBlock("HoursCalendar");
    hoursCalendar.Description = "Monthly calendar of the current user\'s day sheets: coloured day squares, month navigation, a day editor popup to log hours per project / mark a day off / confirm, and Fill all pending days.";
    hoursCalendar.HorizontalPosition = 3400;
    hoursCalendar.VerticalPosition = 700;
    
    var getCalendar = hoursCalendar.CreateDataAction("GetCalendar");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getCalendar.OutputParameters.ToList().ForEach(x => x.Delete());
    getCalendar.Description = "Calendar cells (leading blank cells with DayNumber = 0, then one CalendarDay per day), the month summary (PendingCount) and the daily expected minutes (Settings_GetDailyExpectedMinutes).";
    
    var blankDay = getCalendar.CreateLocalVariable("BlankDay");
    var eSpace = sheetBlocks.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var calendarDay = claudeSheet_CS.Structures.Named("CalendarDay");
    blankDay.DataType = calendarDay;
    
    var leadingBlanks = getCalendar.CreateLocalVariable("LeadingBlanks");
    leadingBlanks.DataType = eSpace.IntegerType;
    
    var startNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var calendar_GetMonth = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").ConnectedBelow(startNode, 1371);
    var calendar_GetMonth2 = claudeSheet_CS.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    var month = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month, "Month");
    var year = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year, "Year");
    
    var calendar_GetMonthSummary = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonthSummary").ConnectedBelow(calendar_GetMonth, 1371);
    var calendar_GetMonthSummary2 = claudeSheet_CS.ServerActions.Named("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.Action = calendar_GetMonthSummary2;
    var year2 = calendar_GetMonthSummary2.InputParameters.Named("Year");
    calendar_GetMonthSummary.SetArgumentValue(year2, "Year");
    var month2 = calendar_GetMonthSummary2.InputParameters.Named("Month");
    calendar_GetMonthSummary.SetArgumentValue(month2, "Month");
    
    var settings_GetDailyExpectedMinutes = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Settings_GetDailyExpectedMinutes").ConnectedBelow(calendar_GetMonthSummary, 1371);
    var settings_GetDailyExpectedMinutes2 = claudeSheet_CS.ServerActions.Named("Settings_GetDailyExpectedMinutes");
    settings_GetDailyExpectedMinutes.Action = settings_GetDailyExpectedMinutes2;
    
    var assignNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(settings_GetDailyExpectedMinutes, 1371);
    assignNode.Label = "Set outputs";
    
    var assignment = assignNode.CreateAssignment("Summary", "Calendar_GetMonthSummary.Summary");
    
    var assignment2 = assignNode.CreateAssignment("DailyExpectedMinutes", "Settings_GetDailyExpectedMinutes.Minutes");
    
    var assignment3 = assignNode.CreateAssignment("LeadingBlanks", "DecimalToInteger(Mod(DayOfWeek(NewDate(Year, Month, 1)) + 6, 7))");
    
    var ifNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode.SetCondition("Cells.Length < LeadingBlanks");
    ifNode.Label = "More blanks?";
    
    var listAppendAll = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppendAll").Below(ifNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppendAll2 = _System_.ServerActions.Named("ListAppendAll");
    listAppendAll.Action = listAppendAll2;
    var sourceList = listAppendAll2.InputParameters.Named("SourceList");
    listAppendAll.SetArgumentValue(sourceList, "Calendar_GetMonth.Days");
    var list = listAppendAll2.InputParameters.Named("List");
    listAppendAll.SetArgumentValue(list, "Cells");
    ifNode.FalseTarget = listAppendAll;
    
    var listAppend = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ToTheRightOf(ifNode, 1828);
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.Target = ifNode;
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "BlankDay");
    var list2 = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list2, "Cells");
    ifNode.TrueTarget = listAppend;
    
    var endNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(listAppendAll, 1371);
    
    var cells = getCalendar.CreateOutputParameter("Cells");
    cells.DataType = eSpace.GetOrCreateListType(calendarDay);
    
    var summary = getCalendar.CreateOutputParameter("Summary");
    var monthSummary = claudeSheet_CS.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    
    var dailyExpectedMinutes = getCalendar.CreateOutputParameter("DailyExpectedMinutes");
    dailyExpectedMinutes.DataType = eSpace.IntegerType;
    
    var onDaySaved = hoursCalendar.CreateEvent("OnDaySaved");
    onDaySaved.Description = "Raised after a day is saved or cleared, or after Fill all pending days.";
    onDaySaved.IsMandatory = false;
    
    var date = onDaySaved.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    
    var onDaySelected = hoursCalendar.CreateEvent("OnDaySelected");
    onDaySelected.Description = "Raised when any day of the month is clicked.";
    onDaySelected.IsMandatory = false;
    
    var date2 = onDaySelected.CreateInputParameter("Date");
    date2.DataType = eSpace.DateType;
    
    var onMonthChanged = hoursCalendar.CreateEvent("OnMonthChanged");
    onMonthChanged.Description = "Raised by the previous/next arrows with the new Year and Month.";
    onMonthChanged.IsMandatory = false;
    
    var year3 = onMonthChanged.CreateInputParameter("Year");
    year3.DataType = eSpace.IntegerType;
    
    var month3 = onMonthChanged.CreateInputParameter("Month");
    month3.DataType = eSpace.IntegerType;
    
    var year4 = hoursCalendar.CreateInputParameter("Year");
    year4.DataType = eSpace.IntegerType;
    
    var month4 = hoursCalendar.CreateInputParameter("Month");
    month4.DataType = eSpace.IntegerType;
    
    var refreshKey = hoursCalendar.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the calendar to reload. Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    
    var editDate = hoursCalendar.CreateLocalVariable("EditDate");
    editDate.DataType = eSpace.DateType;
    
    var showDayEditor = hoursCalendar.CreateLocalVariable("ShowDayEditor");
    showDayEditor.DataType = eSpace.BooleanType;
    
    var isDayOff = hoursCalendar.CreateLocalVariable("IsDayOff");
    isDayOff.DataType = eSpace.BooleanType;
    
    var isFutureDay = hoursCalendar.CreateLocalVariable("IsFutureDay");
    isFutureDay.DataType = eSpace.BooleanType;
    
    var hasDaySheet = hoursCalendar.CreateLocalVariable("HasDaySheet");
    hasDaySheet.DataType = eSpace.BooleanType;
    
    var savedMinutes = hoursCalendar.CreateLocalVariable("SavedMinutes");
    savedMinutes.DataType = eSpace.IntegerType;
    
    var editorLines = hoursCalendar.CreateLocalVariable("EditorLines");
    var dayEditorLine = claudeSheet_CS.Structures.Named("DayEditorLine");
    editorLines.DataType = eSpace.GetOrCreateListType(dayEditorLine);
    
    var editorNotes = hoursCalendar.CreateLocalVariable("EditorNotes");
    editorNotes.DataType = eSpace.TextType;
    
    var editorTotal = hoursCalendar.CreateLocalVariable("EditorTotal");
    editorTotal.DataType = eSpace.IntegerType;
    
    var editorError = hoursCalendar.CreateLocalVariable("EditorError");
    editorError.DataType = eSpace.TextType;
    
    var hourOptions = hoursCalendar.CreateLocalVariable("HourOptions");
    var numberOption = claudeSheet_CS.Structures.Named("NumberOption");
    hourOptions.DataType = eSpace.GetOrCreateListType(numberOption);
    
    var minuteOptions = hoursCalendar.CreateLocalVariable("MinuteOptions");
    minuteOptions.DataType = eSpace.GetOrCreateListType(numberOption);
    
    var showFillPopup = hoursCalendar.CreateLocalVariable("ShowFillPopup");
    showFillPopup.DataType = eSpace.BooleanType;
    
    var fillProjects = hoursCalendar.CreateLocalVariable("FillProjects");
    var daySheetProjectLine = claudeSheet_CS.Structures.Named("DaySheetProjectLine");
    fillProjects.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    
    var fillProjectId = hoursCalendar.CreateLocalVariable("FillProjectId");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    fillProjectId.DataType = project.IdentifierType;
    
    var fillHours = hoursCalendar.CreateLocalVariable("FillHours");
    fillHours.DataType = eSpace.IntegerType;
    
    var fillMins = hoursCalendar.CreateLocalVariable("FillMins");
    fillMins.DataType = eSpace.IntegerType;
    
    var fillError = hoursCalendar.CreateLocalVariable("FillError");
    fillError.DataType = eSpace.TextType;
    
    var finishDaySave = hoursCalendar.CreateScreenAction("FinishDaySave");
    finishDaySave.Description = "After a successful save/clear: closes the popup, refreshes GetCalendar, shows a success message and raises OnDaySaved(EditDate).";
    
    var successMessage = finishDaySave.CreateInputParameter("SuccessMessage");
    successMessage.DataType = eSpace.TextType;
    
    var onInitialize = hoursCalendar.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Fills HourOptions (0-12) and MinuteOptions (00, 15, 30, 45) for the day editor dropdowns.";
    
    var i = onInitialize.CreateLocalVariable("I");
    i.DataType = eSpace.IntegerType;
    
    var option = onInitialize.CreateLocalVariable("Option");
    option.DataType = numberOption;
    
    var startNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var assignNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode2, 1371);
    assignNode2.Label = "Start hours at 0";
    
    var assignment4 = assignNode2.CreateAssignment("I", "0");
    
    var ifNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode2, 1371);
    ifNode2.SetCondition("I <= 12");
    ifNode2.Label = "!More hours?";
    
    var assignNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode3.Label = "Hour option";
    
    var assignment5 = assignNode3.CreateAssignment("Option.Value", "I");
    
    var assignment6 = assignNode3.CreateAssignment("Option.Label", "I");
    
    var appendHourOption = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("AppendHourOption").ConnectedBelow(assignNode3, 1371);
    var listAppend3 = _System_.ClientActions.Named("ListAppend");
    appendHourOption.Action = listAppend3;
    var element2 = listAppend3.InputParameters.Named("Element");
    appendHourOption.SetArgumentValue(element2, "Option");
    var list3 = listAppend3.InputParameters.Named("List");
    appendHourOption.SetArgumentValue(list3, "HourOptions");
    
    var assignNode4 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(appendHourOption, 1371);
    assignNode4.Label = "Next hour";
    assignNode4.Target = ifNode2;
    
    var assignment7 = assignNode4.CreateAssignment("I", "I + 1");
    
    var assignNode5 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 4113);
    assignNode5.Label = "Start minutes at 0";
    
    var assignment8 = assignNode5.CreateAssignment("I", "0");
    
    var ifNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode5, 1371);
    ifNode3.SetCondition("I <= 45");
    ifNode3.Label = "!More minutes?";
    
    var assignNode6 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 1828);
    assignNode6.Label = "Minute option";
    
    var assignment9 = assignNode6.CreateAssignment("Option.Value", "I");
    
    var assignment10 = assignNode6.CreateAssignment("Option.Label", "If(I < 10, \"0\", \"\") + I");
    
    var appendMinuteOption = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("AppendMinuteOption").ConnectedBelow(assignNode6, 1371);
    appendMinuteOption.Action = listAppend3;
    appendMinuteOption.SetArgumentValue(list3, "MinuteOptions");
    appendMinuteOption.SetArgumentValue(element2, "Option");
    
    var assignNode7 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(appendMinuteOption, 1371);
    assignNode7.Label = "Next quarter";
    assignNode7.Target = ifNode3;
    
    var assignment11 = assignNode7.CreateAssignment("I", "I + 15");
    
    var endNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode3, 4113);
    hoursCalendar.OnInitialize.Destination = onInitialize;
    
    var onParametersChanged = hoursCalendar.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads GetCalendar when Year, Month or RefreshKey change.";
    
    var startNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getCalendar;
    
    var endNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    hoursCalendar.OnParametersChanged.Destination = onParametersChanged;
    
    var onReady = hoursCalendar.CreateScreenAction("OnReady");
    onReady.Description = "OnReady: Escape closes the open HoursCalendar popup (clicks its Cancel button). Bound once per page.";
    
    var startNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.VerticalPosition = 800;
    
    var escapeClosesPopup = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("EscapeClosesPopup").ConnectedBelow(startNode4, 1371);
    escapeClosesPopup.JavaScript = "if (!window.sheetCalEscapeBound) {\n    window.sheetCalEscapeBound = true;\n    document.addEventListener(\'keydown\', function (e) {\n        if (e.key === \'Escape\') {\n            var buttons = document.querySelectorAll(\'.sheet-popup .sheet-popup__cancel\');\n            if (buttons.length > 0) { buttons[buttons.length - 1].click(); }\n        }\n    });\n}";
    
    var endNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(escapeClosesPopup, 1371);
    hoursCalendar.OnReady.Destination = onReady;
    ifNode2.FalseTarget = assignNode5;
    ifNode2.TrueTarget = assignNode3;
    ifNode3.FalseTarget = endNode2;
    ifNode3.TrueTarget = assignNode6;
    
    var startNode5 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var assignNode8 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode8.Label = "Close editor";
    
    var assignment12 = assignNode8.CreateAssignment("ShowDayEditor", "False");
    
    var refreshDataNode2 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode8, 1371);
    refreshDataNode2.DataSource = getCalendar;
    
    var messageNode = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode2, 1371);
    messageNode.Label = "Show success";
    messageNode.SetMessage("SuccessMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var onDaySaved2 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySaved").ConnectedBelow(messageNode, 1371);
    onDaySaved2.Event = onDaySaved;
    onDaySaved2.SetArgumentValue(date, "EditDate");
    
    var endNode5 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onDaySaved2, 1371);
    
    var onChange_EditorTime = hoursCalendar.CreateScreenAction("OnChange_EditorTime");
    onChange_EditorTime.Description = "OnChange of HoursDropdown and MinutesDropdown in the day editor: recomputes the live total of the day.";
    
    var startNode6 = onChange_EditorTime.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.VerticalPosition = 800;
    
    var recalcEditorTotal = onChange_EditorTime.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RecalcEditorTotal").ConnectedBelow(startNode6, 1371);
    
    var recalcEditorTotal2 = hoursCalendar.CreateScreenAction("RecalcEditorTotal");
    recalcEditorTotal2.Description = "Recomputes EditorTotal (minutes) from the hours/minutes dropdowns of EditorLines.";
    
    var startNode7 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.VerticalPosition = 800;
    
    var assignNode9 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode9.Label = "Reset total";
    
    var assignment13 = assignNode9.CreateAssignment("EditorTotal", "0");
    
    var forEachNode = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode9, 1371);
    forEachNode.Label = "Each editor line";
    forEachNode.SetRecordList("EditorLines");
    
    var assignNode10 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode10.Label = "Add line minutes";
    assignNode10.Target = forEachNode;
    
    var assignment14 = assignNode10.CreateAssignment("EditorTotal", "EditorTotal + EditorLines.Current.Hours * 60 + EditorLines.Current.Mins");
    
    var endNode6 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 1371);
    recalcEditorTotal.Action = recalcEditorTotal2;
    
    var endNode7 = onChange_EditorTime.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(recalcEditorTotal, 1371);
    forEachNode.CycleTarget = assignNode10;
    forEachNode.Target = endNode6;
    
    var onClick_CancelButton = hoursCalendar.CreateScreenAction("OnClick_CancelButton");
    onClick_CancelButton.Description = "Cancel: closes the day editor without saving.";
    
    var startNode8 = onClick_CancelButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var assignNode11 = onClick_CancelButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode8, 1371);
    assignNode11.Label = "Close editor";
    
    var assignment15 = assignNode11.CreateAssignment("ShowDayEditor", "False");
    
    var endNode8 = onClick_CancelButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode11, 1371);
    
    var onClick_CancelFillButton = hoursCalendar.CreateScreenAction("OnClick_CancelFillButton");
    onClick_CancelFillButton.Description = "Cancel: closes FillPendingPopup without filling.";
    
    var startNode9 = onClick_CancelFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var assignNode12 = onClick_CancelFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode9, 1371);
    assignNode12.Label = "Close popup";
    
    var assignment16 = assignNode12.CreateAssignment("ShowFillPopup", "False");
    
    var endNode9 = onClick_CancelFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode12, 1371);
    
    var onClick_ClearDayButton = hoursCalendar.CreateScreenAction("OnClick_ClearDayButton");
    onClick_ClearDayButton.Description = "Calls ClearDaySheet for EditDate (Clear day / Remove day off). Errors stay in the popup.";
    
    var startNode10 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.VerticalPosition = 800;
    
    var daySheet_Clear = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_Clear").ConnectedBelow(startNode10, 1371);
    var daySheet_Clear2 = claudeSheet_CS.ServerActions.Named("DaySheet_Clear");
    daySheet_Clear.Action = daySheet_Clear2;
    var date3 = daySheet_Clear2.InputParameters.Named("Date");
    daySheet_Clear.SetArgumentValue(date3, "EditDate");
    
    var finishDaySave2 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("FinishDaySave").ConnectedBelow(daySheet_Clear, 1371);
    finishDaySave2.Action = finishDaySave;
    finishDaySave2.SetArgumentValue(successMessage, "If(IsFutureDay, \"Day off removed.\", \"Day cleared.\")");
    
    var endNode10 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(finishDaySave2, 1371);
    
    var allExceptions = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.LogError = false;
    allExceptions.VerticalPosition = 800;
    
    var assignNode13 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode13.Label = "Keep error";
    
    var assignment17 = assignNode13.CreateAssignment("EditorError", "AllExceptions.ExceptionMessage");
    
    var endNode11 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode13, 1371);
    
    var onClick_ConfirmFillButton = hoursCalendar.CreateScreenAction("OnClick_ConfirmFillButton");
    onClick_ConfirmFillButton.Description = "Runs FillPendingDays for the month; on success shows \"X days filled\", refreshes GetCalendar and raises OnDaySaved(CurrDate()). Errors stay in the popup.";
    
    var startNode11 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.VerticalPosition = 800;
    
    var ifNode4 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode11, 1371);
    ifNode4.SetCondition("FillProjectId = NullIdentifier()");
    ifNode4.Label = "!No project?";
    
    var daySheet_FillPending = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_FillPending").Below(ifNode4, 1371);
    var daySheet_FillPending2 = claudeSheet_CS.ServerActions.Named("DaySheet_FillPending");
    daySheet_FillPending.Action = daySheet_FillPending2;
    var year5 = daySheet_FillPending2.InputParameters.Named("Year");
    daySheet_FillPending.SetArgumentValue(year5, "Year");
    var minutesPerDay = daySheet_FillPending2.InputParameters.Named("MinutesPerDay");
    daySheet_FillPending.SetArgumentValue(minutesPerDay, "FillHours * 60 + FillMins");
    var month5 = daySheet_FillPending2.InputParameters.Named("Month");
    daySheet_FillPending.SetArgumentValue(month5, "Month");
    var projectId = daySheet_FillPending2.InputParameters.Named("ProjectId");
    daySheet_FillPending.SetArgumentValue(projectId, "FillProjectId");
    ifNode4.FalseTarget = daySheet_FillPending;
    
    var assignNode14 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode4, 1828);
    assignNode14.Label = "No project error";
    
    var assignment18 = assignNode14.CreateAssignment("FillError", "\"Choose a project.\"");
    ifNode4.TrueTarget = assignNode14;
    
    var assignNode15 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(daySheet_FillPending, 1371);
    assignNode15.Label = "Close popup";
    
    var assignment19 = assignNode15.CreateAssignment("ShowFillPopup", "False");
    
    var endNode12 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode14, 1371);
    
    var refreshDataNode3 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode15, 1371);
    refreshDataNode3.DataSource = getCalendar;
    
    var messageNode2 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode3, 1371);
    messageNode2.Label = "Show filled count";
    messageNode2.SetMessage("DaySheet_FillPending.FilledCount + \" days filled\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var onDaySaved3 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySaved").ConnectedBelow(messageNode2, 1371);
    onDaySaved3.Event = onDaySaved;
    onDaySaved3.SetArgumentValue(date, "CurrDate()");
    
    var endNode13 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onDaySaved3, 1371);
    
    var allExceptions2 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode16 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode16.Label = "Keep error";
    
    var assignment20 = assignNode16.CreateAssignment("FillError", "AllExceptions.ExceptionMessage");
    
    var endNode14 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode16, 1371);
    
    var onClick_DayCell = hoursCalendar.CreateScreenAction("OnClick_DayCell");
    onClick_DayCell.Description = "Raises OnDaySelected for any day; opens the day editor only when the day is editable (not a weekend or holiday).";
    
    var day = onClick_DayCell.CreateInputParameter("Day");
    day.DataType = calendarDay;
    
    var startNode12 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.VerticalPosition = 800;
    
    var onDaySelected2 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySelected").ConnectedBelow(startNode12, 1371);
    onDaySelected2.Event = onDaySelected;
    onDaySelected2.SetArgumentValue(date2, "Day.Date");
    
    var ifNode5 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(onDaySelected2, 1371);
    ifNode5.SetCondition("Day.IsEditable");
    ifNode5.Label = "Editable?";
    
    var endNode15 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode5, 1828);
    ifNode5.FalseTarget = endNode15;
    
    var openDayEditor = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("OpenDayEditor").Below(ifNode5, 1371);
    ifNode5.TrueTarget = openDayEditor;
    
    var openDayEditor2 = hoursCalendar.CreateScreenAction("OpenDayEditor");
    openDayEditor2.Description = "Loads the day with GetDaySheetDetail and opens DayEditorPopup.";
    
    var day2 = openDayEditor2.CreateInputParameter("Day");
    day2.DataType = calendarDay;
    
    var line = openDayEditor2.CreateLocalVariable("Line");
    line.DataType = dayEditorLine;
    
    var startNode13 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode13.HorizontalPosition = 3200;
    startNode13.VerticalPosition = 800;
    
    var daySheet_GetDetail = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_GetDetail").ConnectedBelow(startNode13, 1371);
    var daySheet_GetDetail2 = claudeSheet_CS.ServerActions.Named("DaySheet_GetDetail");
    daySheet_GetDetail.Action = daySheet_GetDetail2;
    var date4 = daySheet_GetDetail2.InputParameters.Named("Date");
    daySheet_GetDetail.SetArgumentValue(date4, "Day.Date");
    
    var assignNode17 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(daySheet_GetDetail, 1371);
    assignNode17.Label = "Set day state";
    
    var assignment21 = assignNode17.CreateAssignment("EditDate", "Day.Date");
    
    var assignment22 = assignNode17.CreateAssignment("IsFutureDay", "Day.Date > CurrDate()");
    
    var assignment23 = assignNode17.CreateAssignment("HasDaySheet", "DaySheet_GetDetail.Detail.DaySheetId <> NullIdentifier()");
    
    var assignment24 = assignNode17.CreateAssignment("IsDayOff", "DaySheet_GetDetail.Detail.DayStatusId = Entities.DayStatus.DayOff or Day.Date > CurrDate()");
    
    var assignment25 = assignNode17.CreateAssignment("EditorNotes", "DaySheet_GetDetail.Detail.Notes");
    
    var assignment26 = assignNode17.CreateAssignment("SavedMinutes", "DaySheet_GetDetail.Detail.TotalMinutes");
    
    var assignment27 = assignNode17.CreateAssignment("EditorError", "\"\"");
    
    var listClear = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListClear").ConnectedBelow(assignNode17, 1371);
    var listClear2 = _System_.ClientActions.Named("ListClear");
    listClear.Action = listClear2;
    var list4 = listClear2.InputParameters.Named("List");
    listClear.SetArgumentValue(list4, "EditorLines");
    
    var forEachNode2 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(listClear, 1371);
    forEachNode2.Label = "Each detail line";
    forEachNode2.SetRecordList("DaySheet_GetDetail.Detail.Lines");
    
    var assignNode18 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode2, 1828);
    assignNode18.Label = "Build editor line";
    
    var assignment28 = assignNode18.CreateAssignment("Line.ProjectId", "DaySheet_GetDetail.Detail.Lines.Current.ProjectId");
    
    var assignment29 = assignNode18.CreateAssignment("Line.ClientName", "DaySheet_GetDetail.Detail.Lines.Current.ClientName");
    
    var assignment30 = assignNode18.CreateAssignment("Line.ProjectName", "DaySheet_GetDetail.Detail.Lines.Current.ProjectName");
    
    var assignment31 = assignNode18.CreateAssignment("Line.ProjectCode", "DaySheet_GetDetail.Detail.Lines.Current.ProjectCode");
    
    var assignment32 = assignNode18.CreateAssignment("Line.Hours", "DecimalToInteger(Trunc(DaySheet_GetDetail.Detail.Lines.Current.Minutes / 60))");
    
    var assignment33 = assignNode18.CreateAssignment("Line.Mins", "DecimalToInteger(Mod(DaySheet_GetDetail.Detail.Lines.Current.Minutes, 60))");
    
    var assignment34 = assignNode18.CreateAssignment("Line.Description", "DaySheet_GetDetail.Detail.Lines.Current.Description");
    
    var recalcEditorTotal3 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RecalcEditorTotal").Below(forEachNode2, 1371);
    recalcEditorTotal3.Action = recalcEditorTotal2;
    
    var listAppend4 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppend").ConnectedBelow(assignNode18, 1371);
    listAppend4.Action = listAppend3;
    listAppend4.Target = forEachNode2;
    listAppend4.SetArgumentValue(list3, "EditorLines");
    listAppend4.SetArgumentValue(element2, "Line");
    
    var assignNode19 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(recalcEditorTotal3, 1371);
    assignNode19.Label = "Open editor";
    
    var assignment35 = assignNode19.CreateAssignment("ShowDayEditor", "True");
    
    var endNode16 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode19, 1371);
    openDayEditor.Action = openDayEditor2;
    
    var endNode17 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(openDayEditor, 1371);
    openDayEditor.SetArgumentValue(day2, "Day");
    forEachNode2.CycleTarget = assignNode18;
    forEachNode2.Target = recalcEditorTotal3;
    
    var onClick_FillPendingButton = hoursCalendar.CreateScreenAction("OnClick_FillPendingButton");
    onClick_FillPendingButton.Description = "Loads the projects assigned today and opens FillPendingPopup with the time defaulting to the daily expected minutes (GetCalendar.DailyExpectedMinutes, from Settings_GetDailyExpectedMinutes).";
    
    var startNode14 = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var project_GetAssignedToUser = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetAssignedToUser").ConnectedBelow(startNode14, 1371);
    var project_GetAssignedToUser2 = claudeSheet_CS.ServerActions.Named("Project_GetAssignedToUser");
    project_GetAssignedToUser.Action = project_GetAssignedToUser2;
    var forDate = project_GetAssignedToUser2.InputParameters.Named("ForDate");
    project_GetAssignedToUser.SetArgumentValue(forDate, "CurrDate()");
    
    var assignNode20 = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetAssignedToUser, 1371);
    assignNode20.Label = "Init fill popup";
    
    var assignment36 = assignNode20.CreateAssignment("FillProjects", "Project_GetAssignedToUser.Projects");
    
    var assignment37 = assignNode20.CreateAssignment("FillProjectId", "Project_GetAssignedToUser.Projects.Current.ProjectId");
    
    var assignment38 = assignNode20.CreateAssignment("FillHours", "DecimalToInteger(Trunc(GetCalendar.DailyExpectedMinutes / 60))");
    
    var assignment39 = assignNode20.CreateAssignment("FillMins", "DecimalToInteger(Mod(GetCalendar.DailyExpectedMinutes, 60))");
    
    var assignment40 = assignNode20.CreateAssignment("FillError", "\"\"");
    
    var assignment41 = assignNode20.CreateAssignment("ShowFillPopup", "True");
    
    var endNode18 = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode20, 1371);
    
    var onClick_NextMonth = hoursCalendar.CreateScreenAction("OnClick_NextMonth");
    onClick_NextMonth.Description = "Raises OnMonthChanged with the next month (December rolls over to January of the next year).";
    
    var startNode15 = onClick_NextMonth.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode15.HorizontalPosition = 3200;
    startNode15.VerticalPosition = 800;
    
    var onMonthChanged2 = onClick_NextMonth.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnMonthChanged").ConnectedBelow(startNode15, 1371);
    onMonthChanged2.Event = onMonthChanged;
    onMonthChanged2.SetArgumentValue(year3, "If(Month = 12, Year + 1, Year)");
    onMonthChanged2.SetArgumentValue(month3, "If(Month = 12, 1, Month + 1)");
    
    var endNode19 = onClick_NextMonth.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onMonthChanged2, 1371);
    
    var onClick_PreviousMonth = hoursCalendar.CreateScreenAction("OnClick_PreviousMonth");
    onClick_PreviousMonth.Description = "Raises OnMonthChanged with the previous month (January rolls back to December of the previous year).";
    
    var startNode16 = onClick_PreviousMonth.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode16.HorizontalPosition = 3200;
    startNode16.VerticalPosition = 800;
    
    var onMonthChanged3 = onClick_PreviousMonth.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnMonthChanged").ConnectedBelow(startNode16, 1371);
    onMonthChanged3.Event = onMonthChanged;
    onMonthChanged3.SetArgumentValue(month3, "If(Month = 1, 12, Month - 1)");
    onMonthChanged3.SetArgumentValue(year3, "If(Month = 1, Year - 1, Year)");
    
    var endNode20 = onClick_PreviousMonth.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onMonthChanged3, 1371);
    
    var onClick_SaveDay = hoursCalendar.CreateScreenAction("OnClick_SaveDay");
    onClick_SaveDay.Description = "Builds the entries from EditorLines and calls SaveDaySheet. Validation errors stay in the popup (EditorError).";
    
    var confirm = onClick_SaveDay.CreateInputParameter("Confirm");
    confirm.DataType = eSpace.BooleanType;
    
    var entries = onClick_SaveDay.CreateLocalVariable("Entries");
    var timeEntryInput = claudeSheet_CS.Structures.Named("TimeEntryInput");
    entries.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    
    var entry = onClick_SaveDay.CreateLocalVariable("Entry");
    entry.DataType = timeEntryInput;
    
    var startNode17 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode17.HorizontalPosition = 3200;
    startNode17.VerticalPosition = 800;
    
    var assignNode21 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode17, 1371);
    assignNode21.Label = "Clear error";
    
    var assignment42 = assignNode21.CreateAssignment("EditorError", "\"\"");
    
    var forEachNode3 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode21, 1371);
    forEachNode3.Label = "Each editor line";
    forEachNode3.SetRecordList("EditorLines");
    
    var assignNode22 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode22.Label = "Build entry";
    
    var assignment43 = assignNode22.CreateAssignment("Entry.ProjectId", "EditorLines.Current.ProjectId");
    
    var assignment44 = assignNode22.CreateAssignment("Entry.Minutes", "If(IsDayOff, 0, EditorLines.Current.Hours * 60 + EditorLines.Current.Mins)");
    
    var assignment45 = assignNode22.CreateAssignment("Entry.Description", "EditorLines.Current.Description");
    forEachNode3.CycleTarget = assignNode22;
    
    var daySheet_Save = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_Save").Below(forEachNode3, 1371);
    var daySheet_Save2 = claudeSheet_CS.ServerActions.Named("DaySheet_Save");
    daySheet_Save.Action = daySheet_Save2;
    var date5 = daySheet_Save2.InputParameters.Named("Date");
    daySheet_Save.SetArgumentValue(date5, "EditDate");
    var notes = daySheet_Save2.InputParameters.Named("Notes");
    daySheet_Save.SetArgumentValue(notes, "EditorNotes");
    var entries2 = daySheet_Save2.InputParameters.Named("Entries");
    daySheet_Save.SetArgumentValue(entries2, "Entries");
    var confirm2 = daySheet_Save2.InputParameters.Named("Confirm");
    daySheet_Save.SetArgumentValue(confirm2, "Confirm");
    var isDayOff2 = daySheet_Save2.InputParameters.Named("IsDayOff");
    daySheet_Save.SetArgumentValue(isDayOff2, "IsDayOff");
    forEachNode3.Target = daySheet_Save;
    
    var listAppend5 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppend").ConnectedBelow(assignNode22, 1371);
    listAppend5.Action = listAppend3;
    listAppend5.Target = forEachNode3;
    listAppend5.SetArgumentValue(list3, "Entries");
    listAppend5.SetArgumentValue(element2, "Entry");
    
    var finishDaySave3 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("FinishDaySave").ConnectedBelow(daySheet_Save, 1371);
    finishDaySave3.Action = finishDaySave;
    finishDaySave3.SetArgumentValue(successMessage, "If(IsDayOff, \"Day off saved.\", If(Confirm, \"Day confirmed.\", \"Draft saved.\"))");
    
    var endNode21 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(finishDaySave3, 1371);
    
    var allExceptions3 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 8684;
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode23 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode23.Label = "Keep error";
    
    var assignment46 = assignNode23.CreateAssignment("EditorError", "AllExceptions.ExceptionMessage");
    
    var endNode22 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode23, 1371);
    
    var calendar = hoursCalendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Calendar");
    calendar.Gap = null;
    calendar.SetStyle("\"sheet-cal\" + If(GetCalendar.IsDataFetched, \"\", \" sheet-cal--loading\")");
    
    var container = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"sheet-cal__header\"");
    
    var previousMonth = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("PreviousMonth");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    previousMonth.Widgets.ToList().ForEach(x => x.Delete());
    previousMonth.SetEnabled("True");
    previousMonth.Gap = null;
    previousMonth.SetStyle("\"sheet-cal__arrow\"");
    
    var icon = previousMonth.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.Icon = "chevron-left";
    var builtinEvent = previousMonth.OnClick;
    builtinEvent.Destination = onClick_PreviousMonth;
    
    var extendedProperty = previousMonth.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Previous month\"");
    
    var monthTitle = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthTitle");
    monthTitle.Gap = null;
    monthTitle.SetStyle("\"sheet-cal__title\"");
    
    var expression = monthTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.SetValue("FormatDateTime(NewDate(Year, Month, 1), \"MMMM\") + \" \" + Year");
    
    var nextMonth = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NextMonth");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    nextMonth.Widgets.ToList().ForEach(x => x.Delete());
    nextMonth.SetEnabled("True");
    nextMonth.Gap = null;
    nextMonth.SetStyle("\"sheet-cal__arrow\"");
    
    var icon2 = nextMonth.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.Icon = "chevron-right";
    var builtinEvent2 = nextMonth.OnClick;
    builtinEvent2.Destination = onClick_NextMonth;
    
    var extendedProperty2 = nextMonth.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Next month\"");
    
    var weekdayHeader = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("WeekdayHeader");
    weekdayHeader.Gap = null;
    weekdayHeader.SetStyle("\"sheet-cal__weekdays\"");
    
    var container2 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"sheet-cal__weekday\"");
    
    var textWidget = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.Text = "Mon";
    
    var container3 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"sheet-cal__weekday\"");
    
    var textWidget2 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.Text = "Tue";
    
    var container4 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"sheet-cal__weekday\"");
    
    var textWidget3 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.Text = "Wed";
    
    var container5 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"sheet-cal__weekday\"");
    
    var textWidget4 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.Text = "Thu";
    
    var container6 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"sheet-cal__weekday\"");
    
    var textWidget5 = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.Text = "Fri";
    
    var container7 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Gap = null;
    container7.SetStyle("\"sheet-cal__weekday\"");
    
    var textWidget6 = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.Text = "Sat";
    
    var container8 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Gap = null;
    container8.SetStyle("\"sheet-cal__weekday\"");
    
    var textWidget7 = container8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.Text = "Sun";
    
    var calendarDays = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("CalendarDays");
    calendarDays.Gap = null;
    calendarDays.SetSource("GetCalendar.Cells");
    calendarDays.SetStyle("\"sheet-cal__grid\"");
    
    var calendarCell = calendarDays.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("CalendarCell");
    calendarCell.Gap = null;
    calendarCell.SetStyle("\"sheet-cal__cell\"");
    
    var isBlankCell = calendarCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsBlankCell");
    isBlankCell.SetCondition("GetCalendar.Cells.Current.DayNumber = 0");
    
    var container9 = isBlankCell.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Gap = null;
    container9.SetStyle("\"sheet-cal__blank\"");
    
    var dayCell = isBlankCell.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("DayCell");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    dayCell.Widgets.ToList().ForEach(x => x.Delete());
    dayCell.SetEnabled("True");
    dayCell.Gap = null;
    dayCell.SetStyle("\"sheet-cal__day \" + GetCalendar.Cells.Current.CssClass + If(GetCalendar.Cells.Current.IsEditable, \" is-editable\", \" is-readonly\")");
    
    var container10 = dayCell.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Gap = null;
    container10.SetStyle("\"sheet-cal__num\"");
    
    var expression2 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("GetCalendar.Cells.Current.DayNumber");
    
    var container11 = dayCell.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Gap = null;
    container11.SetStyle("\"sheet-cal__dot\" + If(GetCalendar.Cells.Current.IsToday, \" is-today\", If(GetCalendar.Cells.Current.DaySheetId <> NullIdentifier() or GetCalendar.Cells.Current.HolidayName <> \"\", \" has-record\", \"\"))");
    var builtinEvent3 = dayCell.OnClick;
    builtinEvent3.Destination = onClick_DayCell;
    builtinEvent3.SetArgumentValue(day, "GetCalendar.Cells.Current");
    
    var extendedProperty3 = dayCell.CreateExtendedProperty();
    extendedProperty3.Property = "title";
    extendedProperty3.SetValue("If(GetCalendar.Cells.Current.HolidayName <> \"\", GetCalendar.Cells.Current.HolidayName, If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"Confirmed\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"Day off\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"Holiday\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Pending, \"Pending\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"Weekend\", \"Future\"))))) + \" · \" + Minutes_Format(GetCalendar.Cells.Current.TotalMinutes, \":\"))");
    
    var extendedProperty4 = dayCell.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("FormatDateTime(GetCalendar.Cells.Current.Date, \"d MMMM yyyy\") + \", \" + If(GetCalendar.Cells.Current.HolidayName <> \"\", GetCalendar.Cells.Current.HolidayName, If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"Confirmed\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"Day off\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"Holiday\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Pending, \"Pending\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"Weekend\", \"Future\")))))) + If(GetCalendar.Cells.Current.TotalMinutes > 0, \", \" + DecimalToText(Round(GetCalendar.Cells.Current.TotalMinutes / 60, 2)) + \" hours\", \"\")");
    var builtinEvent4 = calendarDays.OnScrollEnding;
    
    var calendarFooter = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("CalendarFooter");
    calendarFooter.Gap = null;
    calendarFooter.SetStyle("\"sheet-cal__footer\"");
    
    var fillPendingButton = calendarFooter.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("FillPendingButton");
    fillPendingButton.SetEnabled("GetCalendar.IsDataFetched and GetCalendar.Summary.PendingCount > 0");
    fillPendingButton.Gap = null;
    fillPendingButton.SetStyle("\"btn btn-primary sheet-cal__fill\"");
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)fillPendingButton.Widgets.First();
    textWidget8.Text = "Fill all pending days";
    var builtinEvent5 = fillPendingButton.OnClick;
    builtinEvent5.Destination = onClick_FillPendingButton;
    
    var dayEditorPopup = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("DayEditorPopup");
    dayEditorPopup.Gap = null;
    dayEditorPopup.SetShowPopup("ShowDayEditor");
    dayEditorPopup.SetStyle("\"popup-dialog sheet-popup\"");
    
    var dayEditorTitle = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DayEditorTitle");
    dayEditorTitle.Gap = null;
    dayEditorTitle.SetStyle("\"sheet-popup__title\"");
    
    var expression3 = dayEditorTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("FormatDateTime(EditDate, \"dddd, d MMMM yyyy\")");
    
    var dayKind = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButtonGroup>("DayKind");
    /*** Deleting Widgets that are created by default when instantiating an IButtonGroup ***/
    dayKind.Widgets.ToList().ForEach(x => x.Delete());
    dayKind.SetEnabled("True");
    dayKind.Gap = null;
    dayKind.SetMandatory("False");
    dayKind.SetVariable("IsDayOff");
    
    var workDayOption = dayKind.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButtonGroupItem>("WorkDayOption");
    /*** Deleting Widgets that are created by default when instantiating an IButtonGroupItem ***/
    workDayOption.Widgets.ToList().ForEach(x => x.Delete());
    workDayOption.SetEnabled("True");
    workDayOption.Gap = null;
    workDayOption.SetValue("False");
    workDayOption.SetVisible("not IsFutureDay");
    
    var textWidget9 = workDayOption.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.Text = "Work day";
    
    var dayOffOption = dayKind.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButtonGroupItem>("DayOffOption");
    /*** Deleting Widgets that are created by default when instantiating an IButtonGroupItem ***/
    dayOffOption.Widgets.ToList().ForEach(x => x.Delete());
    dayOffOption.SetEnabled("True");
    dayOffOption.Gap = null;
    dayOffOption.SetValue("True");
    
    var textWidget10 = dayOffOption.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.Text = "Day off";
    var builtinEvent6 = dayKind.OnChange;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var isFutureHint = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsFutureHint");
    isFutureHint.SetCondition("IsFutureDay");
    
    var container12 = isFutureHint.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Gap = null;
    container12.SetStyle("\"sheet-popup__hint\"");
    
    var textWidget11 = container12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget11.Text = "Hours cannot be logged in the future. You can only set or remove a day off.";
    
    var isWorkDay = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsWorkDay");
    isWorkDay.SetCondition("not IsDayOff");
    
    var container13 = isWorkDay.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Gap = null;
    container13.SetStyle("\"sheet-editor\"");
    
    var projectLines = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectLines");
    projectLines.Gap = null;
    projectLines.SetSource("EditorLines");
    projectLines.SetStyle("\"sheet-editor__lines\"");
    
    var container14 = projectLines.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container14.Gap = null;
    container14.SetStyle("\"sheet-editor__line\"");
    
    var container15 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container15.Gap = null;
    container15.SetStyle("\"sheet-editor__project\"");
    
    var container16 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container16.Gap = null;
    container16.SetStyle("\"sheet-editor__client\"");
    
    var expression4 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.SetValue("EditorLines.Current.ClientName");
    
    var container17 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container17.Gap = null;
    container17.SetStyle("\"sheet-editor__name\"");
    
    var expression5 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.SetValue("EditorLines.Current.ProjectName + \" | \" + EditorLines.Current.ProjectCode");
    
    var container18 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container18.Gap = null;
    container18.SetStyle("\"sheet-editor__time\"");
    
    var hoursDropdown = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("HoursDropdown");
    hoursDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    hoursDropdown.SetEnabled("True");
    hoursDropdown.Gap = null;
    hoursDropdown.SetLabels("Label");
    hoursDropdown.SetList("HourOptions");
    hoursDropdown.SetMandatory("False");
    hoursDropdown.SetValues("Value");
    hoursDropdown.SetVariable("EditorLines.Current.Hours");
    var builtinEvent7 = hoursDropdown.OnChange;
    builtinEvent7.Destination = onChange_EditorTime;
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty5 = hoursDropdown.CreateExtendedProperty();
    extendedProperty5.Property = "aria-label";
    extendedProperty5.SetValue("\"Hours for \" + EditorLines.Current.ProjectName");
    
    var textWidget12 = container18.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.Text = "h";
    
    var minutesDropdown = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("MinutesDropdown");
    minutesDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    minutesDropdown.SetEnabled("True");
    minutesDropdown.Gap = null;
    minutesDropdown.SetLabels("Label");
    minutesDropdown.SetList("MinuteOptions");
    minutesDropdown.SetMandatory("False");
    minutesDropdown.SetValues("Value");
    minutesDropdown.SetVariable("EditorLines.Current.Mins");
    var builtinEvent8 = minutesDropdown.OnChange;
    builtinEvent8.Destination = onChange_EditorTime;
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty6 = minutesDropdown.CreateExtendedProperty();
    extendedProperty6.Property = "aria-label";
    extendedProperty6.SetValue("\"Minutes for \" + EditorLines.Current.ProjectName");
    
    var descriptionInput = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("DescriptionInput");
    descriptionInput.SetEnabled("True");
    descriptionInput.SetMandatory("False");
    descriptionInput.SetStyle("\"form-control sheet-editor__desc\"");
    descriptionInput.SetVariable("EditorLines.Current.Description");
    var builtinEvent9 = descriptionInput.OnChange;
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty7 = descriptionInput.CreateExtendedProperty();
    extendedProperty7.Property = "placeholder";
    extendedProperty7.SetValue("\"Description (optional)\"");
    var builtinEvent10 = projectLines.OnScrollEnding;
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var dayTotal = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DayTotal");
    dayTotal.Gap = null;
    dayTotal.SetStyle("\"sheet-editor__total\"");
    
    var expression6 = dayTotal.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.SetValue("\"Total of the day: \" + Minutes_Format(EditorTotal, \":\")");
    
    var hasHoursWarning = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasHoursWarning");
    hasHoursWarning.SetCondition("IsDayOff and SavedMinutes > 0");
    
    var container19 = hasHoursWarning.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container19.Gap = null;
    container19.SetStyle("\"sheet-popup__warning\"");
    
    var expression7 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.SetValue("\"This day has \" + Minutes_Format(SavedMinutes, \":\") + \" logged. Setting it as a day off deletes those hours.\"");
    
    var hasEditorError = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasEditorError");
    hasEditorError.SetCondition("EditorError <> \"\"");
    
    var editorErrorMessage = hasEditorError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EditorErrorMessage");
    editorErrorMessage.Gap = null;
    editorErrorMessage.SetStyle("\"sheet-popup__error\"");
    
    var expression8 = editorErrorMessage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.SetValue("EditorError");
    
    var extendedProperty8 = editorErrorMessage.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"alert\"");
    
    var dayEditorActions = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DayEditorActions");
    dayEditorActions.Gap = null;
    dayEditorActions.SetStyle("\"sheet-popup__actions\"");
    
    var saveDraftButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveDraftButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    saveDraftButton.Widgets.ToList().ForEach(x => x.Delete());
    saveDraftButton.SetEnabled("True");
    saveDraftButton.Gap = null;
    saveDraftButton.SetVisible("not IsDayOff");
    
    var expression9 = saveDraftButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.SetValue("\"Save as draft\"");
    var builtinEvent11 = saveDraftButton.OnClick;
    builtinEvent11.Destination = onClick_SaveDay;
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent11.SetArgumentValue(confirm, "False");
    
    var confirmDayButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ConfirmDayButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    confirmDayButton.Widgets.ToList().ForEach(x => x.Delete());
    confirmDayButton.SetEnabled("True");
    confirmDayButton.Gap = null;
    confirmDayButton.SetStyle("\"btn btn-primary\"");
    confirmDayButton.SetVisible("not IsDayOff");
    
    var expression10 = confirmDayButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.SetValue("\"Confirm day\"");
    var builtinEvent12 = confirmDayButton.OnClick;
    builtinEvent12.Destination = onClick_SaveDay;
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent12.SetArgumentValue(confirm, "True");
    
    var saveDayOffButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveDayOffButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    saveDayOffButton.Widgets.ToList().ForEach(x => x.Delete());
    saveDayOffButton.SetEnabled("True");
    saveDayOffButton.Gap = null;
    saveDayOffButton.SetStyle("\"btn btn-primary\"");
    saveDayOffButton.SetVisible("IsDayOff and SavedMinutes = 0");
    
    var expression11 = saveDayOffButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.SetValue("\"Save day off\"");
    var builtinEvent13 = saveDayOffButton.OnClick;
    builtinEvent13.Destination = onClick_SaveDay;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent13.SetArgumentValue(confirm, "False");
    
    var dayOffDeleteHoursButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("DayOffDeleteHoursButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    dayOffDeleteHoursButton.Widgets.ToList().ForEach(x => x.Delete());
    dayOffDeleteHoursButton.SetConfirmationMessage("\"This day has hours logged. Setting it as a day off deletes them. Continue?\"");
    dayOffDeleteHoursButton.SetEnabled("True");
    dayOffDeleteHoursButton.Gap = null;
    dayOffDeleteHoursButton.SetStyle("\"btn btn-primary\"");
    dayOffDeleteHoursButton.SetVisible("IsDayOff and SavedMinutes > 0");
    
    var expression12 = dayOffDeleteHoursButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.SetValue("\"Set day off and delete hours\"");
    var builtinEvent14 = dayOffDeleteHoursButton.OnClick;
    builtinEvent14.Destination = onClick_SaveDay;
    builtinEvent14.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent14.SetArgumentValue(confirm, "False");
    
    var clearDayButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ClearDayButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    clearDayButton.Widgets.ToList().ForEach(x => x.Delete());
    clearDayButton.SetConfirmationMessage("If(IsFutureDay, \"Remove the day off?\", \"Clear this day? Its hours and status will be removed.\")");
    clearDayButton.SetEnabled("True");
    clearDayButton.Gap = null;
    clearDayButton.SetStyle("\"btn btn-error\"");
    clearDayButton.SetVisible("HasDaySheet");
    
    var expression13 = clearDayButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression13.SetValue("If(IsFutureDay, \"Remove day off\", \"Clear day\")");
    var builtinEvent15 = clearDayButton.OnClick;
    builtinEvent15.Destination = onClick_ClearDayButton;
    builtinEvent15.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    cancelButton.Widgets.ToList().ForEach(x => x.Delete());
    cancelButton.SetEnabled("True");
    cancelButton.Gap = null;
    cancelButton.SetStyle("\"btn sheet-popup__cancel\"");
    
    var expression14 = cancelButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression14.SetValue("\"Cancel\"");
    var builtinEvent16 = cancelButton.OnClick;
    builtinEvent16.Destination = onClick_CancelButton;
    builtinEvent16.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var fillPendingPopup = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("FillPendingPopup");
    fillPendingPopup.Gap = null;
    fillPendingPopup.SetShowPopup("ShowFillPopup");
    fillPendingPopup.SetStyle("\"popup-dialog sheet-popup\"");
    
    var container20 = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container20.Gap = null;
    container20.SetStyle("\"sheet-popup__title\"");
    
    var textWidget13 = container20.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.Text = "Fill all pending days";
    
    var container21 = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container21.Gap = null;
    container21.SetStyle("\"sheet-fill__field\"");
    
    var container22 = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container22.Gap = null;
    container22.SetStyle("\"sheet-fill__label\"");
    
    var textWidget14 = container22.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget14.Text = "Project";
    
    var fillProjectDropdown = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("FillProjectDropdown");
    fillProjectDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    fillProjectDropdown.SetEnabled("True");
    fillProjectDropdown.Gap = null;
    fillProjectDropdown.SetLabels("ClientName + \" / \" + ProjectName + \" | \" + ProjectCode");
    fillProjectDropdown.SetList("FillProjects");
    fillProjectDropdown.SetMandatory("False");
    fillProjectDropdown.SetValues("ProjectId");
    fillProjectDropdown.SetVariable("FillProjectId");
    var builtinEvent17 = fillProjectDropdown.OnChange;
    builtinEvent17.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty9 = fillProjectDropdown.CreateExtendedProperty();
    extendedProperty9.Property = "aria-label";
    extendedProperty9.SetValue("\"Project\"");
    
    var container23 = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container23.Gap = null;
    container23.SetStyle("\"sheet-fill__field\"");
    
    var container24 = container23.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container24.Gap = null;
    container24.SetStyle("\"sheet-fill__label\"");
    
    var textWidget15 = container24.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.Text = "Hours per day";
    
    var container25 = container23.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container25.Gap = null;
    container25.SetStyle("\"sheet-editor__time\"");
    
    var fillHoursDropdown = container25.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("FillHoursDropdown");
    fillHoursDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    fillHoursDropdown.SetEnabled("True");
    fillHoursDropdown.Gap = null;
    fillHoursDropdown.SetLabels("Label");
    fillHoursDropdown.SetList("HourOptions");
    fillHoursDropdown.SetMandatory("False");
    fillHoursDropdown.SetValues("Value");
    fillHoursDropdown.SetVariable("FillHours");
    var builtinEvent18 = fillHoursDropdown.OnChange;
    builtinEvent18.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty10 = fillHoursDropdown.CreateExtendedProperty();
    extendedProperty10.Property = "aria-label";
    extendedProperty10.SetValue("\"Hours per day\"");
    
    var textWidget16 = container25.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget16.Text = "h";
    
    var fillMinutesDropdown = container25.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("FillMinutesDropdown");
    fillMinutesDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    fillMinutesDropdown.SetEnabled("True");
    fillMinutesDropdown.Gap = null;
    fillMinutesDropdown.SetLabels("Label");
    fillMinutesDropdown.SetList("MinuteOptions");
    fillMinutesDropdown.SetMandatory("False");
    fillMinutesDropdown.SetValues("Value");
    fillMinutesDropdown.SetVariable("FillMins");
    var builtinEvent19 = fillMinutesDropdown.OnChange;
    builtinEvent19.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty11 = fillMinutesDropdown.CreateExtendedProperty();
    extendedProperty11.Property = "aria-label";
    extendedProperty11.SetValue("\"Minutes per day\"");
    
    var fillSummary = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FillSummary");
    fillSummary.Gap = null;
    fillSummary.SetStyle("\"sheet-popup__hint\"");
    
    var expression15 = fillSummary.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression15.SetValue("GetCalendar.Summary.PendingCount + \" pending days will be filled and confirmed\"");
    
    var hasFillError = fillPendingPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasFillError");
    hasFillError.SetCondition("FillError <> \"\"");
    
    var container26 = hasFillError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container26.Gap = null;
    container26.SetStyle("\"sheet-popup__error\"");
    
    var expression16 = container26.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression16.SetValue("FillError");
    
    var extendedProperty12 = container26.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"alert\"");
    
    var fillActions = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FillActions");
    fillActions.Gap = null;
    fillActions.SetStyle("\"sheet-popup__actions\"");
    
    var confirmFillButton = fillActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ConfirmFillButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    confirmFillButton.Widgets.ToList().ForEach(x => x.Delete());
    confirmFillButton.SetEnabled("True");
    confirmFillButton.Gap = null;
    confirmFillButton.SetStyle("\"btn btn-primary\"");
    
    var expression17 = confirmFillButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression17.SetValue("\"Fill and confirm\"");
    var builtinEvent20 = confirmFillButton.OnClick;
    builtinEvent20.Destination = onClick_ConfirmFillButton;
    builtinEvent20.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelFillButton = fillActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelFillButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    cancelFillButton.Widgets.ToList().ForEach(x => x.Delete());
    cancelFillButton.SetEnabled("True");
    cancelFillButton.Gap = null;
    cancelFillButton.SetStyle("\"btn sheet-popup__cancel\"");
    
    var expression18 = cancelFillButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression18.SetValue("\"Cancel\"");
    var builtinEvent21 = cancelFillButton.OnClick;
    builtinEvent21.Destination = onClick_CancelFillButton;
    builtinEvent21.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty13 = calendar.CreateExtendedProperty();
    extendedProperty13.Property = "data-refresh-key";
    extendedProperty13.SetValue("RefreshKey");
}
```
