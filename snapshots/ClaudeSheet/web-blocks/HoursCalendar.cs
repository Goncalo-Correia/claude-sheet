(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'HoursCalendar' ***/
    var hoursCalendar = sheetBlocks.CreateBlock("HoursCalendar");
    hoursCalendar.Description = "Monthly calendar of the current user\'s day sheets: coloured day squares, month navigation, a day editor popup to log hours per project / mark a day off / confirm, and Fill all pending days.";
    hoursCalendar.HorizontalPosition = 3400;
    hoursCalendar.Icon = null; // default value
    hoursCalendar.IsDisabled = false; // default value
    hoursCalendar.JavaScript = ""; // default value
    hoursCalendar.SetOnRenderJavascript(null); // default value
    hoursCalendar.Public = false; // default value
    hoursCalendar.Shareable = false; // default value
    hoursCalendar.StyleSheet = null; // default value
    hoursCalendar.VerticalPosition = 700;
    
    var getCalendar = hoursCalendar.CreateDataAction("GetCalendar");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getCalendar.OutputParameters.ToList().ForEach(x => x.Delete());
    getCalendar.Description = "Calendar cells (leading blank cells with DayNumber = 0, then one CalendarDay per day), the month summary (PendingCount) and the daily expected minutes (Settings_GetDailyExpectedMinutes).";
    getCalendar.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getCalendar.ServerRequestTimeout = null; // default value
    
    var blankDay = getCalendar.CreateLocalVariable("BlankDay");
    var eSpace = sheetBlocks.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var calendarDay = claudeSheet_CS.Structures.Named("CalendarDay");
    blankDay.DataType = calendarDay;
    blankDay.Description = null; // default value
    
    var leadingBlanks = getCalendar.CreateLocalVariable("LeadingBlanks");
    leadingBlanks.DataType = eSpace.IntegerType;
    leadingBlanks.SetDefaultValue(null); // default value
    leadingBlanks.Description = null; // default value
    
    var startNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var calendar_GetMonth = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").ConnectedBelow(startNode, 1371);
    var calendar_GetMonth2 = claudeSheet_CS.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    calendar_GetMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonth.IsDisabled = false; // default value
    calendar_GetMonth.ServerRequestTimeout = null; // default value
    var month = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month, "Month");
    var year = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year, "Year");
    
    var calendar_GetMonthSummary = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonthSummary").ConnectedBelow(calendar_GetMonth, 1371);
    var calendar_GetMonthSummary2 = claudeSheet_CS.ServerActions.Named("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.Action = calendar_GetMonthSummary2;
    calendar_GetMonthSummary.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonthSummary.IsDisabled = false; // default value
    calendar_GetMonthSummary.ServerRequestTimeout = null; // default value
    var year2 = calendar_GetMonthSummary2.InputParameters.Named("Year");
    calendar_GetMonthSummary.SetArgumentValue(year2, "Year");
    var month2 = calendar_GetMonthSummary2.InputParameters.Named("Month");
    calendar_GetMonthSummary.SetArgumentValue(month2, "Month");
    
    var settings_GetDailyExpectedMinutes = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Settings_GetDailyExpectedMinutes").ConnectedBelow(calendar_GetMonthSummary, 1371);
    var settings_GetDailyExpectedMinutes2 = claudeSheet_CS.ServerActions.Named("Settings_GetDailyExpectedMinutes");
    settings_GetDailyExpectedMinutes.Action = settings_GetDailyExpectedMinutes2;
    settings_GetDailyExpectedMinutes.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    settings_GetDailyExpectedMinutes.IsDisabled = false; // default value
    settings_GetDailyExpectedMinutes.ServerRequestTimeout = null; // default value
    
    var monthSheet_GetForUser = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("MonthSheet_GetForUser").ConnectedBelow(settings_GetDailyExpectedMinutes, 1371);
    var monthSheet_GetForUser2 = claudeSheet_CS.ServerActions.Named("MonthSheet_GetForUser");
    monthSheet_GetForUser.Action = monthSheet_GetForUser2;
    monthSheet_GetForUser.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    monthSheet_GetForUser.IsDisabled = false; // default value
    monthSheet_GetForUser.ServerRequestTimeout = null; // default value
    var year3 = monthSheet_GetForUser2.InputParameters.Named("Year");
    monthSheet_GetForUser.SetArgumentValue(year3, "Year");
    var month3 = monthSheet_GetForUser2.InputParameters.Named("Month");
    monthSheet_GetForUser.SetArgumentValue(month3, "Month");
    
    var assignNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(monthSheet_GetForUser, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set outputs";
    
    var assignment = assignNode.CreateAssignment("Summary", "Calendar_GetMonthSummary.Summary");
    
    var assignment2 = assignNode.CreateAssignment("DailyExpectedMinutes", "Settings_GetDailyExpectedMinutes.Minutes");
    
    var assignment3 = assignNode.CreateAssignment("LeadingBlanks", "DecimalToInteger(Mod(DayOfWeek(NewDate(Year, Month, 1)) + 6, 7))");
    
    var assignment4 = assignNode.CreateAssignment("PeriodStatusId", "MonthSheet_GetForUser.PeriodStatusId");
    
    var assignment5 = assignNode.CreateAssignment("MonthComment", "MonthSheet_GetForUser.Comment");
    
    var assignment6 = assignNode.CreateAssignment("IsLocked", "Calendar_GetMonth.IsLocked");
    
    var ifNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode.SetCondition("Cells.Length < LeadingBlanks");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "More blanks?";
    
    var listAppendAll = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppendAll").Below(ifNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppendAll2 = _System_.ServerActions.Named("ListAppendAll");
    listAppendAll.Action = listAppendAll2;
    listAppendAll.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppendAll.IsDisabled = false; // default value
    listAppendAll.ServerRequestTimeout = null; // default value
    var sourceList = listAppendAll2.InputParameters.Named("SourceList");
    listAppendAll.SetArgumentValue(sourceList, "Calendar_GetMonth.Days");
    var list = listAppendAll2.InputParameters.Named("List");
    listAppendAll.SetArgumentValue(list, "Cells");
    ifNode.FalseTarget = listAppendAll;
    
    var listAppend = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ToTheRightOf(ifNode, 1828);
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    listAppend.Target = ifNode;
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "BlankDay");
    var list2 = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list2, "Cells");
    ifNode.TrueTarget = listAppend;
    
    var endNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(listAppendAll, 1371);
    endNode.IsDisabled = false; // default value
    getCalendar.OnAfterFetch.Destination = null; // default value
    
    var cells = getCalendar.CreateOutputParameter("Cells");
    cells.DataType = eSpace.GetOrCreateListType(calendarDay);
    cells.Description = null; // default value
    
    var summary = getCalendar.CreateOutputParameter("Summary");
    var monthSummary = claudeSheet_CS.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    summary.Description = null; // default value
    
    var dailyExpectedMinutes = getCalendar.CreateOutputParameter("DailyExpectedMinutes");
    dailyExpectedMinutes.DataType = eSpace.IntegerType;
    dailyExpectedMinutes.SetDefaultValue(null); // default value
    dailyExpectedMinutes.Description = null; // default value
    
    var periodStatusId = getCalendar.CreateOutputParameter("PeriodStatusId");
    var periodStatus = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("PeriodStatus");
    periodStatusId.DataType = periodStatus.IdentifierType;
    periodStatusId.SetDefaultValue(null); // default value
    periodStatusId.Description = null; // default value
    
    var monthComment = getCalendar.CreateOutputParameter("MonthComment");
    monthComment.DataType = eSpace.TextType;
    monthComment.SetDefaultValue(null); // default value
    monthComment.Description = null; // default value
    
    var isLocked = getCalendar.CreateOutputParameter("IsLocked");
    isLocked.DataType = eSpace.BooleanType;
    isLocked.SetDefaultValue(null); // default value
    isLocked.Description = null; // default value
    
    var onDaySaved = hoursCalendar.CreateEvent("OnDaySaved");
    onDaySaved.Description = "Raised after a day is saved or cleared, or after Fill all pending days.";
    onDaySaved.IsMandatory = false;
    
    var date = onDaySaved.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var onDaySelected = hoursCalendar.CreateEvent("OnDaySelected");
    onDaySelected.Description = "Raised when any day of the month is clicked.";
    onDaySelected.IsMandatory = false;
    
    var date2 = onDaySelected.CreateInputParameter("Date");
    date2.DataType = eSpace.DateType;
    date2.Description = null; // default value
    date2.IsMandatory = true; // default value
    
    var onMonthChanged = hoursCalendar.CreateEvent("OnMonthChanged");
    onMonthChanged.Description = "Raised by the previous/next arrows with the new Year and Month.";
    onMonthChanged.IsMandatory = false;
    
    var year4 = onMonthChanged.CreateInputParameter("Year");
    year4.DataType = eSpace.IntegerType;
    year4.Description = null; // default value
    year4.IsMandatory = true; // default value
    
    var month4 = onMonthChanged.CreateInputParameter("Month");
    month4.DataType = eSpace.IntegerType;
    month4.Description = null; // default value
    month4.IsMandatory = true; // default value
    
    var year5 = hoursCalendar.CreateInputParameter("Year");
    year5.DataType = eSpace.IntegerType;
    year5.Description = null; // default value
    year5.IsMandatory = true; // default value
    
    var month5 = hoursCalendar.CreateInputParameter("Month");
    month5.DataType = eSpace.IntegerType;
    month5.Description = null; // default value
    month5.IsMandatory = true; // default value
    
    var refreshKey = hoursCalendar.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the calendar to reload. Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    
    var editDate = hoursCalendar.CreateLocalVariable("EditDate");
    editDate.DataType = eSpace.DateType;
    editDate.SetDefaultValue(null); // default value
    editDate.Description = null; // default value
    
    var showDayEditor = hoursCalendar.CreateLocalVariable("ShowDayEditor");
    showDayEditor.DataType = eSpace.BooleanType;
    showDayEditor.SetDefaultValue(null); // default value
    showDayEditor.Description = null; // default value
    
    var isDayOff = hoursCalendar.CreateLocalVariable("IsDayOff");
    isDayOff.DataType = eSpace.BooleanType;
    isDayOff.SetDefaultValue(null); // default value
    isDayOff.Description = null; // default value
    
    var isFutureDay = hoursCalendar.CreateLocalVariable("IsFutureDay");
    isFutureDay.DataType = eSpace.BooleanType;
    isFutureDay.SetDefaultValue(null); // default value
    isFutureDay.Description = null; // default value
    
    var hasDaySheet = hoursCalendar.CreateLocalVariable("HasDaySheet");
    hasDaySheet.DataType = eSpace.BooleanType;
    hasDaySheet.SetDefaultValue(null); // default value
    hasDaySheet.Description = null; // default value
    
    var savedMinutes = hoursCalendar.CreateLocalVariable("SavedMinutes");
    savedMinutes.DataType = eSpace.IntegerType;
    savedMinutes.SetDefaultValue(null); // default value
    savedMinutes.Description = null; // default value
    
    var editorLines = hoursCalendar.CreateLocalVariable("EditorLines");
    var dayEditorLine = claudeSheet_CS.Structures.Named("DayEditorLine");
    editorLines.DataType = eSpace.GetOrCreateListType(dayEditorLine);
    editorLines.Description = null; // default value
    
    var editorNotes = hoursCalendar.CreateLocalVariable("EditorNotes");
    editorNotes.DataType = eSpace.TextType;
    editorNotes.SetDefaultValue(null); // default value
    editorNotes.Description = null; // default value
    
    var editorTotal = hoursCalendar.CreateLocalVariable("EditorTotal");
    editorTotal.DataType = eSpace.IntegerType;
    editorTotal.SetDefaultValue(null); // default value
    editorTotal.Description = null; // default value
    
    var editorError = hoursCalendar.CreateLocalVariable("EditorError");
    editorError.DataType = eSpace.TextType;
    editorError.SetDefaultValue(null); // default value
    editorError.Description = null; // default value
    
    var hourOptions = hoursCalendar.CreateLocalVariable("HourOptions");
    var numberOption = claudeSheet_CS.Structures.Named("NumberOption");
    hourOptions.DataType = eSpace.GetOrCreateListType(numberOption);
    hourOptions.Description = null; // default value
    
    var minuteOptions = hoursCalendar.CreateLocalVariable("MinuteOptions");
    minuteOptions.DataType = eSpace.GetOrCreateListType(numberOption);
    minuteOptions.Description = null; // default value
    
    var showFillPopup = hoursCalendar.CreateLocalVariable("ShowFillPopup");
    showFillPopup.DataType = eSpace.BooleanType;
    showFillPopup.SetDefaultValue(null); // default value
    showFillPopup.Description = null; // default value
    
    var fillProjects = hoursCalendar.CreateLocalVariable("FillProjects");
    var daySheetProjectLine = claudeSheet_CS.Structures.Named("DaySheetProjectLine");
    fillProjects.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    fillProjects.Description = null; // default value
    
    var fillProjectId = hoursCalendar.CreateLocalVariable("FillProjectId");
    var project = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Project");
    fillProjectId.DataType = project.IdentifierType;
    fillProjectId.SetDefaultValue(null); // default value
    fillProjectId.Description = null; // default value
    
    var fillHours = hoursCalendar.CreateLocalVariable("FillHours");
    fillHours.DataType = eSpace.IntegerType;
    fillHours.SetDefaultValue(null); // default value
    fillHours.Description = null; // default value
    
    var fillMins = hoursCalendar.CreateLocalVariable("FillMins");
    fillMins.DataType = eSpace.IntegerType;
    fillMins.SetDefaultValue(null); // default value
    fillMins.Description = null; // default value
    
    var fillError = hoursCalendar.CreateLocalVariable("FillError");
    fillError.DataType = eSpace.TextType;
    fillError.SetDefaultValue(null); // default value
    fillError.Description = null; // default value
    hoursCalendar.OnDestroy.Destination = null; // default value
    hoursCalendar.OnRender.Destination = null; // default value
    
    var finishDaySave = hoursCalendar.CreateScreenAction("FinishDaySave");
    finishDaySave.Description = "After a successful save/clear: closes the popup, refreshes GetCalendar, shows a success message and raises OnDaySaved(EditDate).";
    
    var successMessage = finishDaySave.CreateInputParameter("SuccessMessage");
    successMessage.DataType = eSpace.TextType;
    successMessage.Description = null; // default value
    successMessage.IsMandatory = true; // default value
    
    var onInitialize = hoursCalendar.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Fills HourOptions (0-12) and MinuteOptions (00, 15, 30, 45) for the day editor dropdowns.";
    
    var i = onInitialize.CreateLocalVariable("I");
    i.DataType = eSpace.IntegerType;
    i.SetDefaultValue(null); // default value
    i.Description = null; // default value
    
    var option = onInitialize.CreateLocalVariable("Option");
    option.DataType = numberOption;
    option.Description = null; // default value
    
    var startNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var assignNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode2, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Start hours at 0";
    
    var assignment7 = assignNode2.CreateAssignment("I", "0");
    
    var ifNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode2, 1371);
    ifNode2.SetCondition("I <= 12");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!More hours?";
    
    var assignNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Hour option";
    
    var assignment8 = assignNode3.CreateAssignment("Option.Value", "I");
    
    var assignment9 = assignNode3.CreateAssignment("Option.Label", "I");
    
    var appendHourOption = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("AppendHourOption").ConnectedBelow(assignNode3, 1371);
    var listAppend3 = _System_.ClientActions.Named("ListAppend");
    appendHourOption.Action = listAppend3;
    appendHourOption.IsDisabled = false; // default value
    var element2 = listAppend3.InputParameters.Named("Element");
    appendHourOption.SetArgumentValue(element2, "Option");
    var list3 = listAppend3.InputParameters.Named("List");
    appendHourOption.SetArgumentValue(list3, "HourOptions");
    
    var assignNode4 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(appendHourOption, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Next hour";
    assignNode4.Target = ifNode2;
    
    var assignment10 = assignNode4.CreateAssignment("I", "I + 1");
    
    var assignNode5 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 4113);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Start minutes at 0";
    
    var assignment11 = assignNode5.CreateAssignment("I", "0");
    
    var ifNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode5, 1371);
    ifNode3.SetCondition("I <= 45");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "!More minutes?";
    
    var assignNode6 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 1828);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Minute option";
    
    var assignment12 = assignNode6.CreateAssignment("Option.Value", "I");
    
    var assignment13 = assignNode6.CreateAssignment("Option.Label", "If(I < 10, \"0\", \"\") + I");
    
    var appendMinuteOption = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("AppendMinuteOption").ConnectedBelow(assignNode6, 1371);
    appendMinuteOption.Action = listAppend3;
    appendMinuteOption.IsDisabled = false; // default value
    appendMinuteOption.SetArgumentValue(list3, "MinuteOptions");
    appendMinuteOption.SetArgumentValue(element2, "Option");
    
    var assignNode7 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(appendMinuteOption, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "Next quarter";
    assignNode7.Target = ifNode3;
    
    var assignment14 = assignNode7.CreateAssignment("I", "I + 15");
    
    var endNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode3, 4113);
    endNode2.IsDisabled = false; // default value
    hoursCalendar.OnInitialize.Destination = onInitialize;
    
    var onParametersChanged = hoursCalendar.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads GetCalendar when Year, Month or RefreshKey change.";
    
    var startNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getCalendar;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode3.IsDisabled = false; // default value
    hoursCalendar.OnParametersChanged.Destination = onParametersChanged;
    
    var onReady = hoursCalendar.CreateScreenAction("OnReady");
    onReady.Description = "OnReady: Escape closes the open HoursCalendar popup (clicks its Cancel button). Bound once per page.";
    
    var startNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var escapeClosesPopup = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("EscapeClosesPopup").ConnectedBelow(startNode4, 1371);
    escapeClosesPopup.Description = null; // default value
    escapeClosesPopup.IsDisabled = false; // default value
    escapeClosesPopup.JavaScript = "if (!window.sheetCalEscapeBound) {\n    window.sheetCalEscapeBound = true;\n    document.addEventListener(\'keydown\', function (e) {\n        if (e.key === \'Escape\') {\n            var buttons = document.querySelectorAll(\'.sheet-popup .sheet-popup__cancel\');\n            if (buttons.length > 0) { buttons[buttons.length - 1].click(); }\n        }\n    });\n}";
    
    var endNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(escapeClosesPopup, 1371);
    endNode4.IsDisabled = false; // default value
    hoursCalendar.OnReady.Destination = onReady;
    ifNode2.FalseTarget = assignNode5;
    ifNode2.TrueTarget = assignNode3;
    ifNode3.FalseTarget = endNode2;
    ifNode3.TrueTarget = assignNode6;
    
    var startNode5 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.IsDisabled = false; // default value
    
    var assignNode8 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "Close editor";
    
    var assignment15 = assignNode8.CreateAssignment("ShowDayEditor", "False");
    
    var refreshDataNode2 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode8, 1371);
    refreshDataNode2.DataSource = getCalendar;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var messageNode = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode2, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Show success";
    messageNode.SetMessage("SuccessMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var onDaySaved2 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySaved").ConnectedBelow(messageNode, 1371);
    onDaySaved2.Description = null; // default value
    onDaySaved2.Event = onDaySaved;
    onDaySaved2.IsDisabled = false; // default value
    onDaySaved2.SetArgumentValue(date, "EditDate");
    
    var endNode5 = finishDaySave.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onDaySaved2, 1371);
    endNode5.IsDisabled = false; // default value
    
    var onChange_EditorTime = hoursCalendar.CreateScreenAction("OnChange_EditorTime");
    onChange_EditorTime.Description = "OnChange of HoursDropdown and MinutesDropdown in the day editor: recomputes the live total of the day.";
    
    var startNode6 = onChange_EditorTime.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 800;
    
    var recalcEditorTotal = onChange_EditorTime.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RecalcEditorTotal").ConnectedBelow(startNode6, 1371);
    recalcEditorTotal.IsDisabled = false; // default value
    
    var recalcEditorTotal2 = hoursCalendar.CreateScreenAction("RecalcEditorTotal");
    recalcEditorTotal2.Description = "Recomputes EditorTotal (minutes) from the hours/minutes dropdowns of EditorLines.";
    
    var startNode7 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 3200;
    startNode7.IsDisabled = false; // default value
    startNode7.VerticalPosition = 800;
    
    var assignNode9 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = "Reset total";
    
    var assignment16 = assignNode9.CreateAssignment("EditorTotal", "0");
    
    var forEachNode = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode9, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = "Each editor line";
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("EditorLines");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode10 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = "Add line minutes";
    assignNode10.Target = forEachNode;
    
    var assignment17 = assignNode10.CreateAssignment("EditorTotal", "EditorTotal + EditorLines.Current.Hours * 60 + EditorLines.Current.Mins");
    
    var endNode6 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 1371);
    endNode6.IsDisabled = false; // default value
    recalcEditorTotal.Action = recalcEditorTotal2;
    
    var endNode7 = onChange_EditorTime.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(recalcEditorTotal, 1371);
    endNode7.IsDisabled = false; // default value
    forEachNode.CycleTarget = assignNode10;
    forEachNode.Target = endNode6;
    
    var onClick_CancelButton = hoursCalendar.CreateScreenAction("OnClick_CancelButton");
    onClick_CancelButton.Description = "Cancel: closes the day editor without saving.";
    
    var startNode8 = onClick_CancelButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.IsDisabled = false; // default value
    
    var assignNode11 = onClick_CancelButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode8, 1371);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = "Close editor";
    
    var assignment18 = assignNode11.CreateAssignment("ShowDayEditor", "False");
    
    var endNode8 = onClick_CancelButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode11, 1371);
    endNode8.IsDisabled = false; // default value
    
    var onClick_CancelFillButton = hoursCalendar.CreateScreenAction("OnClick_CancelFillButton");
    onClick_CancelFillButton.Description = "Cancel: closes FillPendingPopup without filling.";
    
    var startNode9 = onClick_CancelFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.IsDisabled = false; // default value
    
    var assignNode12 = onClick_CancelFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode9, 1371);
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = "Close popup";
    
    var assignment19 = assignNode12.CreateAssignment("ShowFillPopup", "False");
    
    var endNode9 = onClick_CancelFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode12, 1371);
    endNode9.IsDisabled = false; // default value
    
    var onClick_ClearDayButton = hoursCalendar.CreateScreenAction("OnClick_ClearDayButton");
    onClick_ClearDayButton.Description = "Calls ClearDaySheet for EditDate (Clear day / Remove day off). Errors stay in the popup.";
    
    var startNode10 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.IsDisabled = false; // default value
    startNode10.VerticalPosition = 800;
    
    var daySheet_Clear = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_Clear").ConnectedBelow(startNode10, 1371);
    var daySheet_Clear2 = claudeSheet_CS.ServerActions.Named("DaySheet_Clear");
    daySheet_Clear.Action = daySheet_Clear2;
    daySheet_Clear.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    daySheet_Clear.IsDisabled = false; // default value
    daySheet_Clear.ServerRequestTimeout = null; // default value
    var date3 = daySheet_Clear2.InputParameters.Named("Date");
    daySheet_Clear.SetArgumentValue(date3, "EditDate");
    
    var finishDaySave2 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("FinishDaySave").ConnectedBelow(daySheet_Clear, 1371);
    finishDaySave2.Action = finishDaySave;
    finishDaySave2.IsDisabled = false; // default value
    finishDaySave2.SetArgumentValue(successMessage, "If(IsFutureDay, \"Day off removed.\", \"Day cleared.\")");
    
    var endNode10 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(finishDaySave2, 1371);
    endNode10.IsDisabled = false; // default value
    
    var allExceptions = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = false;
    allExceptions.VerticalPosition = 800;
    
    var assignNode13 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = "Keep error";
    
    var assignment20 = assignNode13.CreateAssignment("EditorError", "AllExceptions.ExceptionMessage");
    
    var endNode11 = onClick_ClearDayButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode13, 1371);
    endNode11.IsDisabled = false; // default value
    
    var onClick_ConfirmFillButton = hoursCalendar.CreateScreenAction("OnClick_ConfirmFillButton");
    onClick_ConfirmFillButton.Description = "Runs DaySheet_FillPending for the month; on success shows the days filled and skipped, refreshes GetCalendar and raises OnDaySaved(CurrDate()). Errors stay in the popup.";
    
    var startNode11 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.IsDisabled = false; // default value
    startNode11.VerticalPosition = 800;
    
    var ifNode4 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode11, 1371);
    ifNode4.SetCondition("FillProjectId = NullIdentifier()");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "!No project?";
    
    var daySheet_FillPending = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_FillPending").Below(ifNode4, 1371);
    var daySheet_FillPending2 = claudeSheet_CS.ServerActions.Named("DaySheet_FillPending");
    daySheet_FillPending.Action = daySheet_FillPending2;
    daySheet_FillPending.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    daySheet_FillPending.IsDisabled = false; // default value
    daySheet_FillPending.ServerRequestTimeout = null; // default value
    var year6 = daySheet_FillPending2.InputParameters.Named("Year");
    daySheet_FillPending.SetArgumentValue(year6, "Year");
    var minutesPerDay = daySheet_FillPending2.InputParameters.Named("MinutesPerDay");
    daySheet_FillPending.SetArgumentValue(minutesPerDay, "FillHours * 60 + FillMins");
    var month6 = daySheet_FillPending2.InputParameters.Named("Month");
    daySheet_FillPending.SetArgumentValue(month6, "Month");
    var projectId = daySheet_FillPending2.InputParameters.Named("ProjectId");
    daySheet_FillPending.SetArgumentValue(projectId, "FillProjectId");
    ifNode4.FalseTarget = daySheet_FillPending;
    
    var assignNode14 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode4, 1828);
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = "No project error";
    
    var assignment21 = assignNode14.CreateAssignment("FillError", "\"Choose a project.\"");
    ifNode4.TrueTarget = assignNode14;
    
    var assignNode15 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(daySheet_FillPending, 1371);
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = "Close popup";
    
    var assignment22 = assignNode15.CreateAssignment("ShowFillPopup", "False");
    
    var endNode12 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode14, 1371);
    endNode12.IsDisabled = false; // default value
    
    var refreshDataNode3 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode15, 1371);
    refreshDataNode3.DataSource = getCalendar;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    
    var messageNode2 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode3, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Show filled and skipped";
    messageNode2.SetMessage("DaySheet_FillPending.FilledCount + \" days filled, \" + DaySheet_FillPending.SkippedCount + \" skipped (not assigned to the project on that day)\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var onDaySaved3 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySaved").ConnectedBelow(messageNode2, 1371);
    onDaySaved3.Description = null; // default value
    onDaySaved3.Event = onDaySaved;
    onDaySaved3.IsDisabled = false; // default value
    onDaySaved3.SetArgumentValue(date, "CurrDate()");
    
    var endNode13 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onDaySaved3, 1371);
    endNode13.IsDisabled = false; // default value
    
    var allExceptions2 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 6856;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode16 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode16.IsDisabled = false; // default value
    assignNode16.Label = "Keep error";
    
    var assignment23 = assignNode16.CreateAssignment("FillError", "AllExceptions.ExceptionMessage");
    
    var endNode14 = onClick_ConfirmFillButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode16, 1371);
    endNode14.IsDisabled = false; // default value
    
    var onClick_DayCell = hoursCalendar.CreateScreenAction("OnClick_DayCell");
    onClick_DayCell.Description = "Raises OnDaySelected for any day; opens the day editor only when the day is editable (not a weekend or holiday).";
    
    var day = onClick_DayCell.CreateInputParameter("Day");
    day.DataType = calendarDay;
    day.Description = null; // default value
    day.IsMandatory = true; // default value
    
    var startNode12 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.IsDisabled = false; // default value
    startNode12.VerticalPosition = 800;
    
    var onDaySelected2 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySelected").ConnectedBelow(startNode12, 1371);
    onDaySelected2.Description = null; // default value
    onDaySelected2.Event = onDaySelected;
    onDaySelected2.IsDisabled = false; // default value
    onDaySelected2.SetArgumentValue(date2, "Day.Date");
    
    var ifNode5 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(onDaySelected2, 1371);
    ifNode5.SetCondition("Day.IsEditable");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "Editable?";
    
    var endNode15 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode5, 1828);
    endNode15.IsDisabled = false; // default value
    ifNode5.FalseTarget = endNode15;
    
    var openDayEditor = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("OpenDayEditor").Below(ifNode5, 1371);
    openDayEditor.IsDisabled = false; // default value
    ifNode5.TrueTarget = openDayEditor;
    
    var openDayEditor2 = hoursCalendar.CreateScreenAction("OpenDayEditor");
    openDayEditor2.Description = "Loads the day with GetDaySheetDetail and opens DayEditorPopup.";
    
    var day2 = openDayEditor2.CreateInputParameter("Day");
    day2.DataType = calendarDay;
    day2.Description = null; // default value
    day2.IsMandatory = true; // default value
    
    var line = openDayEditor2.CreateLocalVariable("Line");
    line.DataType = dayEditorLine;
    line.Description = null; // default value
    
    var startNode13 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode13.HorizontalPosition = 3200;
    startNode13.IsDisabled = false; // default value
    startNode13.VerticalPosition = 800;
    
    var daySheet_GetDetail = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_GetDetail").ConnectedBelow(startNode13, 1371);
    var daySheet_GetDetail2 = claudeSheet_CS.ServerActions.Named("DaySheet_GetDetail");
    daySheet_GetDetail.Action = daySheet_GetDetail2;
    daySheet_GetDetail.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    daySheet_GetDetail.IsDisabled = false; // default value
    daySheet_GetDetail.ServerRequestTimeout = null; // default value
    var date4 = daySheet_GetDetail2.InputParameters.Named("Date");
    daySheet_GetDetail.SetArgumentValue(date4, "Day.Date");
    
    var assignNode17 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(daySheet_GetDetail, 1371);
    assignNode17.IsDisabled = false; // default value
    assignNode17.Label = "Set day state";
    
    var assignment24 = assignNode17.CreateAssignment("EditDate", "Day.Date");
    
    var assignment25 = assignNode17.CreateAssignment("IsFutureDay", "Day.Date > CurrDate()");
    
    var assignment26 = assignNode17.CreateAssignment("HasDaySheet", "DaySheet_GetDetail.Detail.DaySheetId <> NullIdentifier()");
    
    var assignment27 = assignNode17.CreateAssignment("IsDayOff", "DaySheet_GetDetail.Detail.DayStatusId = Entities.DayStatus.DayOff or Day.Date > CurrDate()");
    
    var assignment28 = assignNode17.CreateAssignment("EditorNotes", "DaySheet_GetDetail.Detail.Notes");
    
    var assignment29 = assignNode17.CreateAssignment("SavedMinutes", "DaySheet_GetDetail.Detail.TotalMinutes");
    
    var assignment30 = assignNode17.CreateAssignment("EditorError", "\"\"");
    
    var listClear = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListClear").ConnectedBelow(assignNode17, 1371);
    var listClear2 = _System_.ClientActions.Named("ListClear");
    listClear.Action = listClear2;
    listClear.IsDisabled = false; // default value
    var list4 = listClear2.InputParameters.Named("List");
    listClear.SetArgumentValue(list4, "EditorLines");
    
    var forEachNode2 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(listClear, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = "Each detail line";
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("DaySheet_GetDetail.Detail.Lines");
    forEachNode2.SetStartIndex(null); // default value
    
    var assignNode18 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode2, 1828);
    assignNode18.IsDisabled = false; // default value
    assignNode18.Label = "Build editor line";
    
    var assignment31 = assignNode18.CreateAssignment("Line.ProjectId", "DaySheet_GetDetail.Detail.Lines.Current.ProjectId");
    
    var assignment32 = assignNode18.CreateAssignment("Line.ClientName", "DaySheet_GetDetail.Detail.Lines.Current.ClientName");
    
    var assignment33 = assignNode18.CreateAssignment("Line.ProjectName", "DaySheet_GetDetail.Detail.Lines.Current.ProjectName");
    
    var assignment34 = assignNode18.CreateAssignment("Line.ProjectCode", "DaySheet_GetDetail.Detail.Lines.Current.ProjectCode");
    
    var assignment35 = assignNode18.CreateAssignment("Line.Hours", "DecimalToInteger(Trunc(DaySheet_GetDetail.Detail.Lines.Current.Minutes / 60))");
    
    var assignment36 = assignNode18.CreateAssignment("Line.Mins", "DecimalToInteger(Mod(DaySheet_GetDetail.Detail.Lines.Current.Minutes, 60))");
    
    var assignment37 = assignNode18.CreateAssignment("Line.Description", "DaySheet_GetDetail.Detail.Lines.Current.Description");
    
    var recalcEditorTotal3 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RecalcEditorTotal").Below(forEachNode2, 1371);
    recalcEditorTotal3.Action = recalcEditorTotal2;
    recalcEditorTotal3.IsDisabled = false; // default value
    
    var listAppend4 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppend").ConnectedBelow(assignNode18, 1371);
    listAppend4.Action = listAppend3;
    listAppend4.IsDisabled = false; // default value
    listAppend4.Target = forEachNode2;
    listAppend4.SetArgumentValue(list3, "EditorLines");
    listAppend4.SetArgumentValue(element2, "Line");
    
    var assignNode19 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(recalcEditorTotal3, 1371);
    assignNode19.IsDisabled = false; // default value
    assignNode19.Label = "Open editor";
    
    var assignment38 = assignNode19.CreateAssignment("ShowDayEditor", "True");
    
    var endNode16 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode19, 1371);
    endNode16.IsDisabled = false; // default value
    openDayEditor.Action = openDayEditor2;
    
    var endNode17 = onClick_DayCell.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(openDayEditor, 1371);
    endNode17.IsDisabled = false; // default value
    openDayEditor.SetArgumentValue(day2, "Day");
    forEachNode2.CycleTarget = assignNode18;
    forEachNode2.Target = recalcEditorTotal3;
    
    var onClick_FillPendingButton = hoursCalendar.CreateScreenAction("OnClick_FillPendingButton");
    onClick_FillPendingButton.Description = "Loads the projects assigned to the current user in the shown month (Project_GetAssignedInPeriod) and opens FillPendingPopup with the time defaulting to the daily expected minutes (GetCalendar.DailyExpectedMinutes, from Settings_GetDailyExpectedMinutes).";
    
    var startNode14 = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode14.HorizontalPosition = 3200;
    startNode14.IsDisabled = false; // default value
    startNode14.VerticalPosition = 800;
    
    var project_GetAssignedInPeriod = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetAssignedInPeriod").ConnectedBelow(startNode14, 1371);
    var project_GetAssignedInPeriod2 = claudeSheet_CS.ServerActions.Named("Project_GetAssignedInPeriod");
    project_GetAssignedInPeriod.Action = project_GetAssignedInPeriod2;
    project_GetAssignedInPeriod.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetAssignedInPeriod.IsDisabled = false; // default value
    project_GetAssignedInPeriod.ServerRequestTimeout = null; // default value
    var endDate = project_GetAssignedInPeriod2.InputParameters.Named("EndDate");
    project_GetAssignedInPeriod.SetArgumentValue(endDate, "AddDays(AddMonths(NewDate(Year, Month, 1), 1), -1)");
    var startDate = project_GetAssignedInPeriod2.InputParameters.Named("StartDate");
    project_GetAssignedInPeriod.SetArgumentValue(startDate, "NewDate(Year, Month, 1)");
    
    var assignNode20 = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetAssignedInPeriod, 1371);
    assignNode20.IsDisabled = false; // default value
    assignNode20.Label = "Init fill popup";
    
    var assignment39 = assignNode20.CreateAssignment("FillProjects", "Project_GetAssignedInPeriod.Projects");
    
    var assignment40 = assignNode20.CreateAssignment("FillProjectId", "Project_GetAssignedInPeriod.Projects.Current.ProjectId");
    
    var assignment41 = assignNode20.CreateAssignment("FillHours", "DecimalToInteger(Trunc(GetCalendar.DailyExpectedMinutes / 60))");
    
    var assignment42 = assignNode20.CreateAssignment("FillMins", "DecimalToInteger(Mod(GetCalendar.DailyExpectedMinutes, 60))");
    
    var assignment43 = assignNode20.CreateAssignment("FillError", "\"\"");
    
    var assignment44 = assignNode20.CreateAssignment("ShowFillPopup", "True");
    
    var endNode18 = onClick_FillPendingButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode20, 1371);
    endNode18.IsDisabled = false; // default value
    
    var onClick_NextMonth = hoursCalendar.CreateScreenAction("OnClick_NextMonth");
    onClick_NextMonth.Description = "Raises OnMonthChanged with the next month (December rolls over to January of the next year).";
    
    var startNode15 = onClick_NextMonth.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode15.HorizontalPosition = 3200;
    startNode15.IsDisabled = false; // default value
    startNode15.VerticalPosition = 800;
    
    var onMonthChanged2 = onClick_NextMonth.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnMonthChanged").ConnectedBelow(startNode15, 1371);
    onMonthChanged2.Description = null; // default value
    onMonthChanged2.Event = onMonthChanged;
    onMonthChanged2.IsDisabled = false; // default value
    onMonthChanged2.SetArgumentValue(year4, "If(Month = 12, Year + 1, Year)");
    onMonthChanged2.SetArgumentValue(month4, "If(Month = 12, 1, Month + 1)");
    
    var endNode19 = onClick_NextMonth.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onMonthChanged2, 1371);
    endNode19.IsDisabled = false; // default value
    
    var onClick_PreviousMonth = hoursCalendar.CreateScreenAction("OnClick_PreviousMonth");
    onClick_PreviousMonth.Description = "Raises OnMonthChanged with the previous month (January rolls back to December of the previous year).";
    
    var startNode16 = onClick_PreviousMonth.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode16.HorizontalPosition = 3200;
    startNode16.IsDisabled = false; // default value
    startNode16.VerticalPosition = 800;
    
    var onMonthChanged3 = onClick_PreviousMonth.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnMonthChanged").ConnectedBelow(startNode16, 1371);
    onMonthChanged3.Description = null; // default value
    onMonthChanged3.Event = onMonthChanged;
    onMonthChanged3.IsDisabled = false; // default value
    onMonthChanged3.SetArgumentValue(month4, "If(Month = 1, 12, Month - 1)");
    onMonthChanged3.SetArgumentValue(year4, "If(Month = 1, Year - 1, Year)");
    
    var endNode20 = onClick_PreviousMonth.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onMonthChanged3, 1371);
    endNode20.IsDisabled = false; // default value
    
    var onClick_SaveDay = hoursCalendar.CreateScreenAction("OnClick_SaveDay");
    onClick_SaveDay.Description = "Builds the entries from EditorLines and calls SaveDaySheet. Validation errors stay in the popup (EditorError).";
    
    var confirm = onClick_SaveDay.CreateInputParameter("Confirm");
    confirm.DataType = eSpace.BooleanType;
    confirm.Description = null; // default value
    confirm.IsMandatory = true; // default value
    
    var entries = onClick_SaveDay.CreateLocalVariable("Entries");
    var timeEntryInput = claudeSheet_CS.Structures.Named("TimeEntryInput");
    entries.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    entries.Description = null; // default value
    
    var entry = onClick_SaveDay.CreateLocalVariable("Entry");
    entry.DataType = timeEntryInput;
    entry.Description = null; // default value
    
    var startNode17 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode17.HorizontalPosition = 3200;
    startNode17.IsDisabled = false; // default value
    startNode17.VerticalPosition = 800;
    
    var assignNode21 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode17, 1371);
    assignNode21.IsDisabled = false; // default value
    assignNode21.Label = "Clear error";
    
    var assignment45 = assignNode21.CreateAssignment("EditorError", "\"\"");
    
    var forEachNode3 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode21, 1371);
    forEachNode3.IsDisabled = false; // default value
    forEachNode3.Label = "Each editor line";
    forEachNode3.SetMaximumIterations(null); // default value
    forEachNode3.SetRecordList("EditorLines");
    forEachNode3.SetStartIndex(null); // default value
    
    var assignNode22 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode22.IsDisabled = false; // default value
    assignNode22.Label = "Build entry";
    
    var assignment46 = assignNode22.CreateAssignment("Entry.ProjectId", "EditorLines.Current.ProjectId");
    
    var assignment47 = assignNode22.CreateAssignment("Entry.Minutes", "If(IsDayOff, 0, EditorLines.Current.Hours * 60 + EditorLines.Current.Mins)");
    
    var assignment48 = assignNode22.CreateAssignment("Entry.Description", "EditorLines.Current.Description");
    forEachNode3.CycleTarget = assignNode22;
    
    var daySheet_Save = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_Save").Below(forEachNode3, 1371);
    var daySheet_Save2 = claudeSheet_CS.ServerActions.Named("DaySheet_Save");
    daySheet_Save.Action = daySheet_Save2;
    daySheet_Save.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    daySheet_Save.IsDisabled = false; // default value
    daySheet_Save.ServerRequestTimeout = null; // default value
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
    listAppend5.IsDisabled = false; // default value
    listAppend5.Target = forEachNode3;
    listAppend5.SetArgumentValue(list3, "Entries");
    listAppend5.SetArgumentValue(element2, "Entry");
    
    var finishDaySave3 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("FinishDaySave").ConnectedBelow(daySheet_Save, 1371);
    finishDaySave3.Action = finishDaySave;
    finishDaySave3.IsDisabled = false; // default value
    finishDaySave3.SetArgumentValue(successMessage, "If(IsDayOff, \"Day off saved.\", If(Confirm, \"Day confirmed.\", \"Draft saved.\"))");
    
    var endNode21 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(finishDaySave3, 1371);
    endNode21.IsDisabled = false; // default value
    
    var allExceptions3 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 8684;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode23 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode23.IsDisabled = false; // default value
    assignNode23.Label = "Keep error";
    
    var assignment49 = assignNode23.CreateAssignment("EditorError", "AllExceptions.ExceptionMessage");
    
    var endNode22 = onClick_SaveDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode23, 1371);
    endNode22.IsDisabled = false; // default value
    
    var onClick_SubmitMonthButton = hoursCalendar.CreateScreenAction("OnClick_SubmitMonthButton");
    onClick_SubmitMonthButton.Description = "Submits the shown month for approval (MonthSheet_Submit) and refreshes the calendar. Refusals (pending days, month not ended) are shown as an error message.";
    
    var startNode18 = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode18.HorizontalPosition = 3200;
    startNode18.IsDisabled = false; // default value
    startNode18.VerticalPosition = 800;
    
    var monthSheet_Submit = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("MonthSheet_Submit").ConnectedBelow(startNode18, 1371);
    var monthSheet_Submit2 = claudeSheet_CS.ServerActions.Named("MonthSheet_Submit");
    monthSheet_Submit.Action = monthSheet_Submit2;
    monthSheet_Submit.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    monthSheet_Submit.IsDisabled = false; // default value
    monthSheet_Submit.ServerRequestTimeout = null; // default value
    var year7 = monthSheet_Submit2.InputParameters.Named("Year");
    monthSheet_Submit.SetArgumentValue(year7, "Year");
    var month7 = monthSheet_Submit2.InputParameters.Named("Month");
    monthSheet_Submit.SetArgumentValue(month7, "Month");
    
    var refreshDataNode4 = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(monthSheet_Submit, 1371);
    refreshDataNode4.DataSource = getCalendar;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords(null); // default value
    refreshDataNode4.SetStartIndex(null); // default value
    
    var messageNode3 = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1371);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = "Show submitted";
    messageNode3.SetMessage("\"Month submitted for approval.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode23 = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode3, 1371);
    endNode23.IsDisabled = false; // default value
    
    var allExceptions4 = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions4.AbortTransaction = false;
    allExceptions4.Exception = eSpace.AllExceptions;
    allExceptions4.HorizontalPosition = 6856;
    allExceptions4.IsDisabled = false; // default value
    allExceptions4.LogError = false;
    allExceptions4.VerticalPosition = 800;
    
    var messageNode4 = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(allExceptions4, 1371);
    messageNode4.IsDisabled = false; // default value
    messageNode4.Label = "Show error";
    messageNode4.SetMessage("AllExceptions.ExceptionMessage");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode24 = onClick_SubmitMonthButton.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1371);
    endNode24.IsDisabled = false; // default value
    
    var calendar = hoursCalendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Calendar");
    calendar.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    calendar.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    calendar.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    calendar.Animate = false; // default value
    calendar.ColumnGap = null; // default value
    calendar.ColumnSpan = null; // default value
    calendar.CustomStyle = null; // default value
    calendar.FlexBasis = null; // default value
    calendar.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    calendar.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    calendar.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    calendar.Gap = null;
    calendar.GridColumns = null; // default value
    calendar.GridRows = null; // default value
    calendar.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    calendar.RowGap = null; // default value
    calendar.RowSpan = null; // default value
    calendar.SetStyle("\"sheet-cal\" + If(GetCalendar.IsDataFetched, \"\", \" sheet-cal--loading\")");
    calendar.SetVisible("True"); // default value
    calendar.VisibleWasExpression = false; // default value
    calendar.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    calendar.Width = "(fill parent)"; // default value
    
    var container = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle("\"sheet-cal__header\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var previousMonth = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("PreviousMonth");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    previousMonth.Widgets.ToList().ForEach(x => x.Delete());
    previousMonth.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    previousMonth.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    previousMonth.ColumnGap = null; // default value
    previousMonth.ColumnSpan = null; // default value
    previousMonth.SetConfirmationMessage(null); // default value
    previousMonth.CustomStyle = null; // default value
    previousMonth.SetEnabled("True");
    previousMonth.FlexBasis = null; // default value
    previousMonth.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    previousMonth.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    previousMonth.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    previousMonth.Gap = null;
    previousMonth.GridColumns = null; // default value
    previousMonth.GridRows = null; // default value
    previousMonth.IsDefault = false; // default value
    previousMonth.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    previousMonth.RowGap = null; // default value
    previousMonth.RowSpan = null; // default value
    previousMonth.SetStyle("\"sheet-cal__arrow\"");
    previousMonth.SetVisible("True"); // default value
    previousMonth.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    previousMonth.Width = ""; // default value
    
    var icon = previousMonth.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon.ColumnSpan = null; // default value
    icon.CustomStyle = null; // default value
    icon.FlexBasis = null; // default value
    icon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon.Icon = "chevron-left";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon.RowSpan = null; // default value
    icon.SetStyle("\"icon\""); // default value
    icon.SetVisible("True"); // default value
    icon.Weight = null; // default value
    var builtinEvent = previousMonth.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onClick_PreviousMonth;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty = previousMonth.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Previous month\"");
    
    var monthTitle = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthTitle");
    monthTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    monthTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    monthTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    monthTitle.Animate = false; // default value
    monthTitle.ColumnGap = null; // default value
    monthTitle.ColumnSpan = null; // default value
    monthTitle.CustomStyle = null; // default value
    monthTitle.FlexBasis = null; // default value
    monthTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    monthTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    monthTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    monthTitle.Gap = null;
    monthTitle.GridColumns = null; // default value
    monthTitle.GridRows = null; // default value
    monthTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    monthTitle.RowGap = null; // default value
    monthTitle.RowSpan = null; // default value
    monthTitle.SetStyle("\"sheet-cal__title\"");
    monthTitle.SetVisible("True"); // default value
    monthTitle.VisibleWasExpression = false; // default value
    monthTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    monthTitle.Width = "(fill parent)"; // default value
    
    var expression = monthTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("FormatDateTime(NewDate(Year, Month, 1), \"MMMM\") + \" \" + Year");
    expression.Width = ""; // default value
    
    var nextMonth = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NextMonth");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    nextMonth.Widgets.ToList().ForEach(x => x.Delete());
    nextMonth.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    nextMonth.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    nextMonth.ColumnGap = null; // default value
    nextMonth.ColumnSpan = null; // default value
    nextMonth.SetConfirmationMessage(null); // default value
    nextMonth.CustomStyle = null; // default value
    nextMonth.SetEnabled("True");
    nextMonth.FlexBasis = null; // default value
    nextMonth.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    nextMonth.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    nextMonth.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    nextMonth.Gap = null;
    nextMonth.GridColumns = null; // default value
    nextMonth.GridRows = null; // default value
    nextMonth.IsDefault = false; // default value
    nextMonth.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    nextMonth.RowGap = null; // default value
    nextMonth.RowSpan = null; // default value
    nextMonth.SetStyle("\"sheet-cal__arrow\"");
    nextMonth.SetVisible("True"); // default value
    nextMonth.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    nextMonth.Width = ""; // default value
    
    var icon2 = nextMonth.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "chevron-right";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon\""); // default value
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    var builtinEvent2 = nextMonth.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = onClick_NextMonth;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = nextMonth.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Next month\"");
    
    var monthStatus = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthStatus");
    monthStatus.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    monthStatus.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    monthStatus.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    monthStatus.Animate = true;
    monthStatus.ColumnGap = null; // default value
    monthStatus.ColumnSpan = null; // default value
    monthStatus.CustomStyle = null; // default value
    monthStatus.FlexBasis = null; // default value
    monthStatus.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    monthStatus.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    monthStatus.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    monthStatus.Gap = null;
    monthStatus.GridColumns = null; // default value
    monthStatus.GridRows = null; // default value
    monthStatus.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    monthStatus.RowGap = null; // default value
    monthStatus.RowSpan = null; // default value
    monthStatus.SetStyle("\"sheet-cal__status-row\"");
    monthStatus.SetVisible("GetCalendar.IsDataFetched");
    monthStatus.VisibleWasExpression = true;
    monthStatus.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    monthStatus.Width = "(fill parent)"; // default value
    
    var monthStatusBadge = monthStatus.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthStatusBadge");
    monthStatusBadge.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    monthStatusBadge.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    monthStatusBadge.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    monthStatusBadge.Animate = false; // default value
    monthStatusBadge.ColumnGap = null; // default value
    monthStatusBadge.ColumnSpan = null; // default value
    monthStatusBadge.CustomStyle = null; // default value
    monthStatusBadge.FlexBasis = null; // default value
    monthStatusBadge.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    monthStatusBadge.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    monthStatusBadge.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    monthStatusBadge.Gap = null;
    monthStatusBadge.GridColumns = null; // default value
    monthStatusBadge.GridRows = null; // default value
    monthStatusBadge.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    monthStatusBadge.RowGap = null; // default value
    monthStatusBadge.RowSpan = null; // default value
    monthStatusBadge.SetStyle("\"sheet-cal__status sheet-cal__status--\" + ToLower(If(GetCalendar.PeriodStatusId = Entities.PeriodStatus.Submitted, \"Submitted\", If(GetCalendar.PeriodStatusId = Entities.PeriodStatus.Approved, \"Approved\", If(GetCalendar.PeriodStatusId = Entities.PeriodStatus.Rejected, \"Rejected\", \"Open\"))))");
    monthStatusBadge.SetVisible("True"); // default value
    monthStatusBadge.VisibleWasExpression = false; // default value
    monthStatusBadge.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    monthStatusBadge.Width = "(fill parent)"; // default value
    
    var expression2 = monthStatusBadge.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("\"Month: \" + If(GetCalendar.PeriodStatusId = Entities.PeriodStatus.Submitted, \"Submitted\", If(GetCalendar.PeriodStatusId = Entities.PeriodStatus.Approved, \"Approved\", If(GetCalendar.PeriodStatusId = Entities.PeriodStatus.Rejected, \"Rejected\", \"Open\")))");
    expression2.Width = ""; // default value
    
    var hasRejectComment = monthStatus.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasRejectComment");
    hasRejectComment.Animate = false; // default value
    hasRejectComment.SetCondition("GetCalendar.PeriodStatusId = Entities.PeriodStatus.Rejected and GetCalendar.MonthComment <> \"\"");
    hasRejectComment.CustomStyle = null; // default value
    hasRejectComment.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var rejectComment = hasRejectComment.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("RejectComment");
    rejectComment.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    rejectComment.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rejectComment.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rejectComment.Animate = false; // default value
    rejectComment.ColumnGap = null; // default value
    rejectComment.ColumnSpan = null; // default value
    rejectComment.CustomStyle = null; // default value
    rejectComment.FlexBasis = null; // default value
    rejectComment.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rejectComment.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rejectComment.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rejectComment.Gap = null;
    rejectComment.GridColumns = null; // default value
    rejectComment.GridRows = null; // default value
    rejectComment.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rejectComment.RowGap = null; // default value
    rejectComment.RowSpan = null; // default value
    rejectComment.SetStyle("\"sheet-cal__comment\"");
    rejectComment.SetVisible("True"); // default value
    rejectComment.VisibleWasExpression = false; // default value
    rejectComment.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rejectComment.Width = "(fill parent)"; // default value
    
    var expression3 = rejectComment.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("\"Rejected by your manager: \" + GetCalendar.MonthComment");
    expression3.Width = ""; // default value
    
    var weekdayHeader = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("WeekdayHeader");
    weekdayHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    weekdayHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    weekdayHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    weekdayHeader.Animate = false; // default value
    weekdayHeader.ColumnGap = null; // default value
    weekdayHeader.ColumnSpan = null; // default value
    weekdayHeader.CustomStyle = null; // default value
    weekdayHeader.FlexBasis = null; // default value
    weekdayHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    weekdayHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    weekdayHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    weekdayHeader.Gap = null;
    weekdayHeader.GridColumns = null; // default value
    weekdayHeader.GridRows = null; // default value
    weekdayHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    weekdayHeader.RowGap = null; // default value
    weekdayHeader.RowSpan = null; // default value
    weekdayHeader.SetStyle("\"sheet-cal__weekdays\"");
    weekdayHeader.SetVisible("True"); // default value
    weekdayHeader.VisibleWasExpression = false; // default value
    weekdayHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    weekdayHeader.Width = "(fill parent)"; // default value
    
    var container2 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"sheet-cal__weekday\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var textWidget = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Mon";
    
    var container3 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"sheet-cal__weekday\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var textWidget2 = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Tue";
    
    var container4 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"sheet-cal__weekday\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var textWidget3 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Wed";
    
    var container5 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"sheet-cal__weekday\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var textWidget4 = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Thu";
    
    var container6 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"sheet-cal__weekday\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var textWidget5 = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Fri";
    
    var container7 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container7.SetStyle("\"sheet-cal__weekday\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var textWidget6 = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Sat";
    
    var container8 = weekdayHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container8.SetStyle("\"sheet-cal__weekday\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var textWidget7 = container8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Sun";
    
    var calendarDays = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("CalendarDays");
    calendarDays.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    calendarDays.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    calendarDays.AnimateItems = true; // default value
    calendarDays.ColumnGap = null; // default value
    calendarDays.ColumnSpan = null; // default value
    calendarDays.CustomStyle = null; // default value
    calendarDays.ExpandedInWebEditor = true; // default value
    calendarDays.FlexBasis = null; // default value
    calendarDays.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    calendarDays.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    calendarDays.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    calendarDays.Gap = null;
    calendarDays.GridColumns = null; // default value
    calendarDays.GridRows = null; // default value
    calendarDays.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    calendarDays.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    calendarDays.RowGap = null; // default value
    calendarDays.RowSpan = null; // default value
    calendarDays.SetSource("GetCalendar.Cells");
    calendarDays.SetStyle("\"sheet-cal__grid\"");
    calendarDays.Tag = "div"; // default value
    calendarDays.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    calendarDays.Width = "(fill parent)"; // default value
    
    var calendarCell = calendarDays.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("CalendarCell");
    calendarCell.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    calendarCell.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    calendarCell.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    calendarCell.Animate = false; // default value
    calendarCell.ColumnGap = null; // default value
    calendarCell.ColumnSpan = null; // default value
    calendarCell.CustomStyle = null; // default value
    calendarCell.FlexBasis = null; // default value
    calendarCell.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    calendarCell.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    calendarCell.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    calendarCell.Gap = null;
    calendarCell.GridColumns = null; // default value
    calendarCell.GridRows = null; // default value
    calendarCell.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    calendarCell.RowGap = null; // default value
    calendarCell.RowSpan = null; // default value
    calendarCell.SetStyle("\"sheet-cal__cell\"");
    calendarCell.SetVisible("True"); // default value
    calendarCell.VisibleWasExpression = false; // default value
    calendarCell.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    calendarCell.Width = "(fill parent)"; // default value
    
    var isBlankCell = calendarCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsBlankCell");
    isBlankCell.Animate = false; // default value
    isBlankCell.SetCondition("GetCalendar.Cells.Current.DayNumber = 0");
    isBlankCell.CustomStyle = null; // default value
    isBlankCell.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container9 = isBlankCell.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container9.SetStyle("\"sheet-cal__blank\"");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var dayCell = isBlankCell.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("DayCell");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    dayCell.Widgets.ToList().ForEach(x => x.Delete());
    dayCell.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayCell.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayCell.ColumnGap = null; // default value
    dayCell.ColumnSpan = null; // default value
    dayCell.SetConfirmationMessage(null); // default value
    dayCell.CustomStyle = null; // default value
    dayCell.SetEnabled("True");
    dayCell.FlexBasis = null; // default value
    dayCell.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayCell.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayCell.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayCell.Gap = null;
    dayCell.GridColumns = null; // default value
    dayCell.GridRows = null; // default value
    dayCell.IsDefault = false; // default value
    dayCell.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayCell.RowGap = null; // default value
    dayCell.RowSpan = null; // default value
    dayCell.SetStyle("\"sheet-cal__day \" + GetCalendar.Cells.Current.CssClass + If(GetCalendar.Cells.Current.IsEditable, \" is-editable\", \" is-readonly\")");
    dayCell.SetVisible("True"); // default value
    dayCell.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayCell.Width = ""; // default value
    
    var container10 = dayCell.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container10.Animate = false; // default value
    container10.ColumnGap = null; // default value
    container10.ColumnSpan = null; // default value
    container10.CustomStyle = null; // default value
    container10.FlexBasis = null; // default value
    container10.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container10.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container10.Gap = null;
    container10.GridColumns = null; // default value
    container10.GridRows = null; // default value
    container10.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container10.RowGap = null; // default value
    container10.RowSpan = null; // default value
    container10.SetStyle("\"sheet-cal__num\"");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var expression4 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetCalendar.Cells.Current.DayNumber");
    expression4.Width = ""; // default value
    
    var container11 = dayCell.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container11.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container11.Animate = false; // default value
    container11.ColumnGap = null; // default value
    container11.ColumnSpan = null; // default value
    container11.CustomStyle = null; // default value
    container11.FlexBasis = null; // default value
    container11.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container11.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container11.Gap = null;
    container11.GridColumns = null; // default value
    container11.GridRows = null; // default value
    container11.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container11.RowGap = null; // default value
    container11.RowSpan = null; // default value
    container11.SetStyle("\"sheet-cal__dot\" + If(GetCalendar.Cells.Current.IsToday, \" is-today\", If(GetCalendar.Cells.Current.DaySheetId <> NullIdentifier() or GetCalendar.Cells.Current.HolidayName <> \"\", \" has-record\", \"\"))");
    container11.SetVisible("True"); // default value
    container11.VisibleWasExpression = false; // default value
    container11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container11.Width = "(fill parent)"; // default value
    var builtinEvent3 = dayCell.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = onClick_DayCell;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent3.SetArgumentValue(day, "GetCalendar.Cells.Current");
    
    var extendedProperty3 = dayCell.CreateExtendedProperty();
    extendedProperty3.Property = "title";
    extendedProperty3.SetValue("If(GetCalendar.Cells.Current.HolidayName <> \"\", GetCalendar.Cells.Current.HolidayName, If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"Confirmed\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"Day off\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"Holiday\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Pending, \"Pending\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"Weekend\", \"Future\"))))) + \" · \" + Minutes_Format(GetCalendar.Cells.Current.TotalMinutes, \":\"))");
    
    var extendedProperty4 = dayCell.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("FormatDateTime(GetCalendar.Cells.Current.Date, \"d MMMM yyyy\") + \", \" + If(GetCalendar.Cells.Current.HolidayName <> \"\", GetCalendar.Cells.Current.HolidayName, If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"Confirmed\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"Day off\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"Holiday\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Pending, \"Pending\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"Weekend\", \"Future\")))))) + If(GetCalendar.Cells.Current.TotalMinutes > 0, \", \" + DecimalToText(Round(GetCalendar.Cells.Current.TotalMinutes / 60, 2)) + \" hours\", \"\")");
    var builtinEvent4 = calendarDays.OnScrollEnding;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = null; // default value
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var calendarFooter = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("CalendarFooter");
    calendarFooter.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    calendarFooter.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    calendarFooter.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    calendarFooter.Animate = false; // default value
    calendarFooter.ColumnGap = null; // default value
    calendarFooter.ColumnSpan = null; // default value
    calendarFooter.CustomStyle = null; // default value
    calendarFooter.FlexBasis = null; // default value
    calendarFooter.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    calendarFooter.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    calendarFooter.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    calendarFooter.Gap = null;
    calendarFooter.GridColumns = null; // default value
    calendarFooter.GridRows = null; // default value
    calendarFooter.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    calendarFooter.RowGap = null; // default value
    calendarFooter.RowSpan = null; // default value
    calendarFooter.SetStyle("\"sheet-cal__footer\"");
    calendarFooter.SetVisible("True"); // default value
    calendarFooter.VisibleWasExpression = false; // default value
    calendarFooter.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    calendarFooter.Width = "(fill parent)"; // default value
    
    var fillPendingButton = calendarFooter.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("FillPendingButton");
    fillPendingButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fillPendingButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fillPendingButton.ColumnGap = null; // default value
    fillPendingButton.ColumnSpan = null; // default value
    fillPendingButton.SetConfirmationMessage(null); // default value
    fillPendingButton.CustomStyle = null; // default value
    fillPendingButton.SetEnabled("GetCalendar.IsDataFetched and GetCalendar.Summary.PendingCount > 0");
    fillPendingButton.FlexBasis = null; // default value
    fillPendingButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fillPendingButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fillPendingButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fillPendingButton.Gap = null;
    fillPendingButton.GridColumns = null; // default value
    fillPendingButton.GridRows = null; // default value
    fillPendingButton.IsDefault = false; // default value
    fillPendingButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fillPendingButton.RowGap = null; // default value
    fillPendingButton.RowSpan = null; // default value
    fillPendingButton.SetStyle("\"btn btn-primary sheet-cal__fill\"");
    fillPendingButton.SetVisible("not GetCalendar.IsLocked");
    fillPendingButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fillPendingButton.Width = ""; // default value
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)fillPendingButton.Widgets.First();
    textWidget8.CustomStyle = null; // default value
    textWidget8.Name = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Fill all pending days";
    var builtinEvent5 = fillPendingButton.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = onClick_FillPendingButton;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var submitMonthButton = calendarFooter.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SubmitMonthButton");
    submitMonthButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    submitMonthButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    submitMonthButton.ColumnGap = null; // default value
    submitMonthButton.ColumnSpan = null; // default value
    submitMonthButton.SetConfirmationMessage(null); // default value
    submitMonthButton.CustomStyle = null; // default value
    submitMonthButton.SetEnabled("GetCalendar.IsDataFetched");
    submitMonthButton.FlexBasis = null; // default value
    submitMonthButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    submitMonthButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    submitMonthButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    submitMonthButton.Gap = null;
    submitMonthButton.GridColumns = null; // default value
    submitMonthButton.GridRows = null; // default value
    submitMonthButton.IsDefault = false; // default value
    submitMonthButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    submitMonthButton.RowGap = null; // default value
    submitMonthButton.RowSpan = null; // default value
    submitMonthButton.SetStyle("\"btn sheet-cal__submit\"");
    submitMonthButton.SetVisible("not GetCalendar.IsLocked");
    submitMonthButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    submitMonthButton.Width = ""; // default value
    
    var textWidget9 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)submitMonthButton.Widgets.First();
    textWidget9.CustomStyle = null; // default value
    textWidget9.Name = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Submit month";
    var builtinEvent6 = submitMonthButton.OnClick;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = onClick_SubmitMonthButton;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var dayEditorPopup = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("DayEditorPopup");
    dayEditorPopup.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayEditorPopup.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayEditorPopup.ColumnGap = null; // default value
    dayEditorPopup.ColumnSpan = null; // default value
    dayEditorPopup.CustomStyle = null; // default value
    dayEditorPopup.ExpandedInWebEditor = true; // default value
    dayEditorPopup.FlexBasis = null; // default value
    dayEditorPopup.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayEditorPopup.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayEditorPopup.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayEditorPopup.Gap = null;
    dayEditorPopup.GridColumns = null; // default value
    dayEditorPopup.GridRows = null; // default value
    dayEditorPopup.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayEditorPopup.RowGap = null; // default value
    dayEditorPopup.RowSpan = null; // default value
    dayEditorPopup.SetShowPopup("ShowDayEditor");
    dayEditorPopup.SetStyle("\"popup-dialog sheet-popup\"");
    dayEditorPopup.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var dayEditorTitle = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DayEditorTitle");
    dayEditorTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    dayEditorTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayEditorTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayEditorTitle.Animate = false; // default value
    dayEditorTitle.ColumnGap = null; // default value
    dayEditorTitle.ColumnSpan = null; // default value
    dayEditorTitle.CustomStyle = null; // default value
    dayEditorTitle.FlexBasis = null; // default value
    dayEditorTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayEditorTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayEditorTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayEditorTitle.Gap = null;
    dayEditorTitle.GridColumns = null; // default value
    dayEditorTitle.GridRows = null; // default value
    dayEditorTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayEditorTitle.RowGap = null; // default value
    dayEditorTitle.RowSpan = null; // default value
    dayEditorTitle.SetStyle("\"sheet-popup__title\"");
    dayEditorTitle.SetVisible("True"); // default value
    dayEditorTitle.VisibleWasExpression = false; // default value
    dayEditorTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayEditorTitle.Width = "(fill parent)"; // default value
    
    var expression5 = dayEditorTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("FormatDateTime(EditDate, \"dddd, d MMMM yyyy\")");
    expression5.Width = ""; // default value
    
    var dayKind = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButtonGroup>("DayKind");
    /*** Deleting Widgets that are created by default when instantiating an IButtonGroup ***/
    dayKind.Widgets.ToList().ForEach(x => x.Delete());
    dayKind.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayKind.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayKind.ColumnGap = null; // default value
    dayKind.ColumnSpan = null; // default value
    dayKind.CustomStyle = null; // default value
    dayKind.SetEnabled("not GetCalendar.IsLocked");
    dayKind.FlexBasis = null; // default value
    dayKind.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayKind.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayKind.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayKind.Gap = null;
    dayKind.GridColumns = null; // default value
    dayKind.GridRows = null; // default value
    dayKind.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayKind.SetMandatory("False");
    dayKind.RowGap = null; // default value
    dayKind.RowSpan = null; // default value
    dayKind.SetStyle("\"button-group\""); // default value
    dayKind.SetVariable("IsDayOff");
    dayKind.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayKind.Width = ""; // default value
    
    var workDayOption = dayKind.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButtonGroupItem>("WorkDayOption");
    /*** Deleting Widgets that are created by default when instantiating an IButtonGroupItem ***/
    workDayOption.Widgets.ToList().ForEach(x => x.Delete());
    workDayOption.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    workDayOption.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    workDayOption.ColumnGap = null; // default value
    workDayOption.ColumnSpan = null; // default value
    workDayOption.CustomStyle = null; // default value
    workDayOption.SetEnabled("True");
    workDayOption.FlexBasis = null; // default value
    workDayOption.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    workDayOption.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    workDayOption.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    workDayOption.Gap = null;
    workDayOption.GridColumns = null; // default value
    workDayOption.GridRows = null; // default value
    workDayOption.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    workDayOption.RowGap = null; // default value
    workDayOption.RowSpan = null; // default value
    workDayOption.SetStyle("\"button-group-item\""); // default value
    workDayOption.SetValue("False");
    workDayOption.SetVisible("not IsFutureDay");
    workDayOption.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget10 = workDayOption.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.CustomStyle = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Work day";
    
    var dayOffOption = dayKind.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButtonGroupItem>("DayOffOption");
    /*** Deleting Widgets that are created by default when instantiating an IButtonGroupItem ***/
    dayOffOption.Widgets.ToList().ForEach(x => x.Delete());
    dayOffOption.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayOffOption.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayOffOption.ColumnGap = null; // default value
    dayOffOption.ColumnSpan = null; // default value
    dayOffOption.CustomStyle = null; // default value
    dayOffOption.SetEnabled("True");
    dayOffOption.FlexBasis = null; // default value
    dayOffOption.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayOffOption.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayOffOption.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayOffOption.Gap = null;
    dayOffOption.GridColumns = null; // default value
    dayOffOption.GridRows = null; // default value
    dayOffOption.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayOffOption.RowGap = null; // default value
    dayOffOption.RowSpan = null; // default value
    dayOffOption.SetStyle("\"button-group-item\""); // default value
    dayOffOption.SetValue("True");
    dayOffOption.SetVisible("True"); // default value
    dayOffOption.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget11 = dayOffOption.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget11.CustomStyle = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Day off";
    var builtinEvent7 = dayKind.OnChange;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = null; // default value
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var isLockedHint = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsLockedHint");
    isLockedHint.Animate = false; // default value
    isLockedHint.SetCondition("GetCalendar.IsLocked");
    isLockedHint.CustomStyle = null; // default value
    isLockedHint.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var lockedMessage = isLockedHint.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("LockedMessage");
    lockedMessage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    lockedMessage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    lockedMessage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    lockedMessage.Animate = false; // default value
    lockedMessage.ColumnGap = null; // default value
    lockedMessage.ColumnSpan = null; // default value
    lockedMessage.CustomStyle = null; // default value
    lockedMessage.FlexBasis = null; // default value
    lockedMessage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    lockedMessage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    lockedMessage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    lockedMessage.Gap = null;
    lockedMessage.GridColumns = null; // default value
    lockedMessage.GridRows = null; // default value
    lockedMessage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    lockedMessage.RowGap = null; // default value
    lockedMessage.RowSpan = null; // default value
    lockedMessage.SetStyle("\"sheet-popup__hint\"");
    lockedMessage.SetVisible("True"); // default value
    lockedMessage.VisibleWasExpression = false; // default value
    lockedMessage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    lockedMessage.Width = "(fill parent)"; // default value
    
    var textWidget12 = lockedMessage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.CustomStyle = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "This month is submitted or approved, so its days are read-only.";
    
    var isFutureHint = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsFutureHint");
    isFutureHint.Animate = false; // default value
    isFutureHint.SetCondition("IsFutureDay");
    isFutureHint.CustomStyle = null; // default value
    isFutureHint.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container12 = isFutureHint.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container12.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container12.Animate = false; // default value
    container12.ColumnGap = null; // default value
    container12.ColumnSpan = null; // default value
    container12.CustomStyle = null; // default value
    container12.FlexBasis = null; // default value
    container12.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container12.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container12.Gap = null;
    container12.GridColumns = null; // default value
    container12.GridRows = null; // default value
    container12.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container12.RowGap = null; // default value
    container12.RowSpan = null; // default value
    container12.SetStyle("\"sheet-popup__hint\"");
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var textWidget13 = container12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.CustomStyle = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Hours cannot be logged in the future. You can only set or remove a day off.";
    
    var isWorkDay = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsWorkDay");
    isWorkDay.Animate = false; // default value
    isWorkDay.SetCondition("not IsDayOff");
    isWorkDay.CustomStyle = null; // default value
    isWorkDay.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container13 = isWorkDay.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container13.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container13.Animate = false; // default value
    container13.ColumnGap = null; // default value
    container13.ColumnSpan = null; // default value
    container13.CustomStyle = null; // default value
    container13.FlexBasis = null; // default value
    container13.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container13.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container13.Gap = null;
    container13.GridColumns = null; // default value
    container13.GridRows = null; // default value
    container13.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container13.RowGap = null; // default value
    container13.RowSpan = null; // default value
    container13.SetStyle("\"sheet-editor\"");
    container13.SetVisible("True"); // default value
    container13.VisibleWasExpression = false; // default value
    container13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container13.Width = "(fill parent)"; // default value
    
    var hasProjectLines = container13.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProjectLines");
    hasProjectLines.Animate = false; // default value
    hasProjectLines.SetCondition("EditorLines.Length > 0");
    hasProjectLines.CustomStyle = null; // default value
    hasProjectLines.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var projectLines = hasProjectLines.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectLines");
    projectLines.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectLines.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectLines.AnimateItems = true; // default value
    projectLines.ColumnGap = null; // default value
    projectLines.ColumnSpan = null; // default value
    projectLines.CustomStyle = null; // default value
    projectLines.ExpandedInWebEditor = true; // default value
    projectLines.FlexBasis = null; // default value
    projectLines.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectLines.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectLines.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectLines.Gap = null;
    projectLines.GridColumns = null; // default value
    projectLines.GridRows = null; // default value
    projectLines.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectLines.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    projectLines.RowGap = null; // default value
    projectLines.RowSpan = null; // default value
    projectLines.SetSource("EditorLines");
    projectLines.SetStyle("\"sheet-editor__lines\"");
    projectLines.Tag = "div"; // default value
    projectLines.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectLines.Width = "(fill parent)"; // default value
    
    var container14 = projectLines.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container14.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container14.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container14.Animate = false; // default value
    container14.ColumnGap = null; // default value
    container14.ColumnSpan = null; // default value
    container14.CustomStyle = null; // default value
    container14.FlexBasis = null; // default value
    container14.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container14.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container14.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container14.Gap = null;
    container14.GridColumns = null; // default value
    container14.GridRows = null; // default value
    container14.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container14.RowGap = null; // default value
    container14.RowSpan = null; // default value
    container14.SetStyle("\"sheet-editor__line\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "(fill parent)"; // default value
    
    var container15 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container15.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container15.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container15.Animate = false; // default value
    container15.ColumnGap = null; // default value
    container15.ColumnSpan = null; // default value
    container15.CustomStyle = null; // default value
    container15.FlexBasis = null; // default value
    container15.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container15.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container15.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container15.Gap = null;
    container15.GridColumns = null; // default value
    container15.GridRows = null; // default value
    container15.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container15.RowGap = null; // default value
    container15.RowSpan = null; // default value
    container15.SetStyle("\"sheet-editor__project\"");
    container15.SetVisible("True"); // default value
    container15.VisibleWasExpression = false; // default value
    container15.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container15.Width = "(fill parent)"; // default value
    
    var container16 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container16.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container16.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container16.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container16.Animate = false; // default value
    container16.ColumnGap = null; // default value
    container16.ColumnSpan = null; // default value
    container16.CustomStyle = null; // default value
    container16.FlexBasis = null; // default value
    container16.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container16.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container16.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container16.Gap = null;
    container16.GridColumns = null; // default value
    container16.GridRows = null; // default value
    container16.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container16.RowGap = null; // default value
    container16.RowSpan = null; // default value
    container16.SetStyle("\"sheet-editor__client\"");
    container16.SetVisible("True"); // default value
    container16.VisibleWasExpression = false; // default value
    container16.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container16.Width = "(fill parent)"; // default value
    
    var expression6 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("EditorLines.Current.ClientName");
    expression6.Width = ""; // default value
    
    var container17 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container17.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container17.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container17.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container17.Animate = false; // default value
    container17.ColumnGap = null; // default value
    container17.ColumnSpan = null; // default value
    container17.CustomStyle = null; // default value
    container17.FlexBasis = null; // default value
    container17.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container17.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container17.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container17.Gap = null;
    container17.GridColumns = null; // default value
    container17.GridRows = null; // default value
    container17.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container17.RowGap = null; // default value
    container17.RowSpan = null; // default value
    container17.SetStyle("\"sheet-editor__name\"");
    container17.SetVisible("True"); // default value
    container17.VisibleWasExpression = false; // default value
    container17.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container17.Width = "(fill parent)"; // default value
    
    var expression7 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("EditorLines.Current.ProjectName + \" | \" + EditorLines.Current.ProjectCode");
    expression7.Width = ""; // default value
    
    var container18 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container18.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container18.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container18.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container18.Animate = false; // default value
    container18.ColumnGap = null; // default value
    container18.ColumnSpan = null; // default value
    container18.CustomStyle = null; // default value
    container18.FlexBasis = null; // default value
    container18.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container18.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container18.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container18.Gap = null;
    container18.GridColumns = null; // default value
    container18.GridRows = null; // default value
    container18.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container18.RowGap = null; // default value
    container18.RowSpan = null; // default value
    container18.SetStyle("\"sheet-editor__time\"");
    container18.SetVisible("True"); // default value
    container18.VisibleWasExpression = false; // default value
    container18.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container18.Width = "(fill parent)"; // default value
    
    var hoursDropdown = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("HoursDropdown");
    hoursDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    hoursDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    hoursDropdown.ColumnGap = null; // default value
    hoursDropdown.ColumnSpan = null; // default value
    hoursDropdown.CustomStyle = null; // default value
    hoursDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    hoursDropdown.SetEmptyValue(null); // default value
    hoursDropdown.SetEnabled("not GetCalendar.IsLocked");
    hoursDropdown.FlexBasis = null; // default value
    hoursDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    hoursDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    hoursDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    hoursDropdown.Gap = null;
    hoursDropdown.GridColumns = null; // default value
    hoursDropdown.GridRows = null; // default value
    hoursDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    hoursDropdown.SetLabels("Label");
    hoursDropdown.SetList("HourOptions");
    hoursDropdown.SetMandatory("False");
    hoursDropdown.RowGap = null; // default value
    hoursDropdown.RowSpan = null; // default value
    hoursDropdown.SetStyle("\"dropdown\""); // default value
    hoursDropdown.SetValues("Value");
    hoursDropdown.SetVariable("EditorLines.Current.Hours");
    hoursDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    hoursDropdown.Width = ""; // default value
    var builtinEvent8 = hoursDropdown.OnChange;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = onChange_EditorTime;
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty5 = hoursDropdown.CreateExtendedProperty();
    extendedProperty5.Property = "aria-label";
    extendedProperty5.SetValue("\"Hours for \" + EditorLines.Current.ProjectName");
    
    var textWidget14 = container18.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget14.CustomStyle = null; // default value
    textWidget14.SetStyleClasses(null); // default value
    textWidget14.Text = "h";
    
    var minutesDropdown = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("MinutesDropdown");
    minutesDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    minutesDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    minutesDropdown.ColumnGap = null; // default value
    minutesDropdown.ColumnSpan = null; // default value
    minutesDropdown.CustomStyle = null; // default value
    minutesDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    minutesDropdown.SetEmptyValue(null); // default value
    minutesDropdown.SetEnabled("not GetCalendar.IsLocked");
    minutesDropdown.FlexBasis = null; // default value
    minutesDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    minutesDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    minutesDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    minutesDropdown.Gap = null;
    minutesDropdown.GridColumns = null; // default value
    minutesDropdown.GridRows = null; // default value
    minutesDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    minutesDropdown.SetLabels("Label");
    minutesDropdown.SetList("MinuteOptions");
    minutesDropdown.SetMandatory("False");
    minutesDropdown.RowGap = null; // default value
    minutesDropdown.RowSpan = null; // default value
    minutesDropdown.SetStyle("\"dropdown\""); // default value
    minutesDropdown.SetValues("Value");
    minutesDropdown.SetVariable("EditorLines.Current.Mins");
    minutesDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    minutesDropdown.Width = ""; // default value
    var builtinEvent9 = minutesDropdown.OnChange;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = onChange_EditorTime;
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty6 = minutesDropdown.CreateExtendedProperty();
    extendedProperty6.Property = "aria-label";
    extendedProperty6.SetValue("\"Minutes for \" + EditorLines.Current.ProjectName");
    
    var descriptionInput = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("DescriptionInput");
    descriptionInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    descriptionInput.ColumnSpan = null; // default value
    descriptionInput.CustomStyle = null; // default value
    descriptionInput.SetEnabled("not GetCalendar.IsLocked");
    descriptionInput.FlexBasis = null; // default value
    descriptionInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    descriptionInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    descriptionInput.SetMandatory("False");
    descriptionInput.MaxLength = 50; // default value
    descriptionInput.SetPrompt(null); // default value
    descriptionInput.RowSpan = null; // default value
    descriptionInput.SetStyle("\"form-control sheet-editor__desc\"");
    descriptionInput.SetVariable("EditorLines.Current.Description");
    descriptionInput.Width = "(fill parent)"; // default value
    var builtinEvent10 = descriptionInput.OnChange;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = null; // default value
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty7 = descriptionInput.CreateExtendedProperty();
    extendedProperty7.Property = "placeholder";
    extendedProperty7.SetValue("\"Description (optional)\"");
    var builtinEvent11 = projectLines.OnScrollEnding;
    builtinEvent11.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent11.Destination = null; // default value
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var dayTotal = hasProjectLines.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DayTotal");
    dayTotal.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    dayTotal.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayTotal.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayTotal.Animate = false; // default value
    dayTotal.ColumnGap = null; // default value
    dayTotal.ColumnSpan = null; // default value
    dayTotal.CustomStyle = null; // default value
    dayTotal.FlexBasis = null; // default value
    dayTotal.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayTotal.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayTotal.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayTotal.Gap = null;
    dayTotal.GridColumns = null; // default value
    dayTotal.GridRows = null; // default value
    dayTotal.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayTotal.RowGap = null; // default value
    dayTotal.RowSpan = null; // default value
    dayTotal.SetStyle("\"sheet-editor__total\"");
    dayTotal.SetVisible("True"); // default value
    dayTotal.VisibleWasExpression = false; // default value
    dayTotal.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayTotal.Width = "(fill parent)"; // default value
    
    var expression8 = dayTotal.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("\"Total of the day: \" + Minutes_Format(EditorTotal, \":\")");
    expression8.Width = ""; // default value
    
    var noProjectsMessage = hasProjectLines.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoProjectsMessage");
    noProjectsMessage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noProjectsMessage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noProjectsMessage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noProjectsMessage.Animate = false; // default value
    noProjectsMessage.ColumnGap = null; // default value
    noProjectsMessage.ColumnSpan = null; // default value
    noProjectsMessage.CustomStyle = null; // default value
    noProjectsMessage.FlexBasis = null; // default value
    noProjectsMessage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noProjectsMessage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noProjectsMessage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noProjectsMessage.Gap = null;
    noProjectsMessage.GridColumns = null; // default value
    noProjectsMessage.GridRows = null; // default value
    noProjectsMessage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noProjectsMessage.RowGap = null; // default value
    noProjectsMessage.RowSpan = null; // default value
    noProjectsMessage.SetStyle("\"sheet-popup__hint\"");
    noProjectsMessage.SetVisible("True"); // default value
    noProjectsMessage.VisibleWasExpression = false; // default value
    noProjectsMessage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noProjectsMessage.Width = "(fill parent)"; // default value
    
    var textWidget15 = noProjectsMessage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.CustomStyle = null; // default value
    textWidget15.SetStyleClasses(null); // default value
    textWidget15.Text = "No projects are assigned to you on this day. Ask your administrator.";
    
    var notesField = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NotesField");
    notesField.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    notesField.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    notesField.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    notesField.Animate = false; // default value
    notesField.ColumnGap = null; // default value
    notesField.ColumnSpan = null; // default value
    notesField.CustomStyle = null; // default value
    notesField.FlexBasis = null; // default value
    notesField.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    notesField.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    notesField.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    notesField.Gap = null;
    notesField.GridColumns = null; // default value
    notesField.GridRows = null; // default value
    notesField.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    notesField.RowGap = null; // default value
    notesField.RowSpan = null; // default value
    notesField.SetStyle("\"margin-top-base\"");
    notesField.SetVisible("True"); // default value
    notesField.VisibleWasExpression = false; // default value
    notesField.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    notesField.Width = "(fill parent)"; // default value
    
    var notesLabel = notesField.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>("NotesLabel");
    notesLabel.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    notesLabel.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    notesLabel.ColumnGap = null; // default value
    notesLabel.ColumnSpan = null; // default value
    notesLabel.CustomStyle = null; // default value
    notesLabel.FlexBasis = null; // default value
    notesLabel.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    notesLabel.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    notesLabel.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    notesLabel.Gap = null;
    notesLabel.GridColumns = null; // default value
    notesLabel.GridRows = null; // default value
    notesLabel.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    notesLabel.RowGap = null; // default value
    notesLabel.RowSpan = null; // default value
    notesLabel.SetStyle(null); // default value
    notesLabel.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    notesLabel.Width = "(fill parent)"; // default value
    
    var textWidget16 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)notesLabel.Widgets.First();
    textWidget16.CustomStyle = null; // default value
    textWidget16.Name = null; // default value
    textWidget16.SetStyleClasses(null); // default value
    textWidget16.Text = "Notes";
    
    var notesInput = notesField.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITextArea>("NotesInput");
    notesInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    notesInput.ColumnSpan = null; // default value
    notesInput.CustomStyle = null; // default value
    notesInput.SetEnabled("not GetCalendar.IsLocked");
    notesInput.FlexBasis = null; // default value
    notesInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    notesInput.SetMandatory("False");
    notesInput.MaxLength = 500; // default value
    notesInput.SetPrompt(null); // default value
    notesInput.RowSpan = null; // default value
    notesInput.SetStyle("\"form-control\""); // default value
    notesInput.TextLines = 3; // default value
    notesInput.SetVariable("EditorNotes");
    notesInput.Width = "(fill parent)"; // default value
    var builtinEvent12 = notesInput.OnChange;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent12.Destination = null; // default value
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    notesLabel.TargetWidget = notesInput;
    
    var hasHoursWarning = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasHoursWarning");
    hasHoursWarning.Animate = false; // default value
    hasHoursWarning.SetCondition("IsDayOff and SavedMinutes > 0");
    hasHoursWarning.CustomStyle = null; // default value
    hasHoursWarning.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container19 = hasHoursWarning.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container19.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container19.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container19.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container19.Animate = false; // default value
    container19.ColumnGap = null; // default value
    container19.ColumnSpan = null; // default value
    container19.CustomStyle = null; // default value
    container19.FlexBasis = null; // default value
    container19.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container19.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container19.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container19.Gap = null;
    container19.GridColumns = null; // default value
    container19.GridRows = null; // default value
    container19.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container19.RowGap = null; // default value
    container19.RowSpan = null; // default value
    container19.SetStyle("\"sheet-popup__warning\"");
    container19.SetVisible("True"); // default value
    container19.VisibleWasExpression = false; // default value
    container19.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container19.Width = "(fill parent)"; // default value
    
    var expression9 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("\"This day has \" + Minutes_Format(SavedMinutes, \":\") + \" logged. Setting it as a day off deletes those hours.\"");
    expression9.Width = ""; // default value
    
    var hasEditorError = dayEditorPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasEditorError");
    hasEditorError.Animate = false; // default value
    hasEditorError.SetCondition("EditorError <> \"\"");
    hasEditorError.CustomStyle = null; // default value
    hasEditorError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var editorErrorMessage = hasEditorError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("EditorErrorMessage");
    editorErrorMessage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    editorErrorMessage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    editorErrorMessage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    editorErrorMessage.Animate = false; // default value
    editorErrorMessage.ColumnGap = null; // default value
    editorErrorMessage.ColumnSpan = null; // default value
    editorErrorMessage.CustomStyle = null; // default value
    editorErrorMessage.FlexBasis = null; // default value
    editorErrorMessage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    editorErrorMessage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    editorErrorMessage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    editorErrorMessage.Gap = null;
    editorErrorMessage.GridColumns = null; // default value
    editorErrorMessage.GridRows = null; // default value
    editorErrorMessage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    editorErrorMessage.RowGap = null; // default value
    editorErrorMessage.RowSpan = null; // default value
    editorErrorMessage.SetStyle("\"sheet-popup__error\"");
    editorErrorMessage.SetVisible("True"); // default value
    editorErrorMessage.VisibleWasExpression = false; // default value
    editorErrorMessage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    editorErrorMessage.Width = "(fill parent)"; // default value
    
    var expression10 = editorErrorMessage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("EditorError");
    expression10.Width = ""; // default value
    
    var extendedProperty8 = editorErrorMessage.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"alert\"");
    
    var dayEditorActions = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DayEditorActions");
    dayEditorActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    dayEditorActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayEditorActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayEditorActions.Animate = false; // default value
    dayEditorActions.ColumnGap = null; // default value
    dayEditorActions.ColumnSpan = null; // default value
    dayEditorActions.CustomStyle = null; // default value
    dayEditorActions.FlexBasis = null; // default value
    dayEditorActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayEditorActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayEditorActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayEditorActions.Gap = null;
    dayEditorActions.GridColumns = null; // default value
    dayEditorActions.GridRows = null; // default value
    dayEditorActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayEditorActions.RowGap = null; // default value
    dayEditorActions.RowSpan = null; // default value
    dayEditorActions.SetStyle("\"sheet-popup__actions\"");
    dayEditorActions.SetVisible("True"); // default value
    dayEditorActions.VisibleWasExpression = false; // default value
    dayEditorActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayEditorActions.Width = "(fill parent)"; // default value
    
    var saveDraftButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveDraftButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    saveDraftButton.Widgets.ToList().ForEach(x => x.Delete());
    saveDraftButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    saveDraftButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    saveDraftButton.ColumnGap = null; // default value
    saveDraftButton.ColumnSpan = null; // default value
    saveDraftButton.SetConfirmationMessage(null); // default value
    saveDraftButton.CustomStyle = null; // default value
    saveDraftButton.SetEnabled("True");
    saveDraftButton.FlexBasis = null; // default value
    saveDraftButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    saveDraftButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    saveDraftButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    saveDraftButton.Gap = null;
    saveDraftButton.GridColumns = null; // default value
    saveDraftButton.GridRows = null; // default value
    saveDraftButton.IsDefault = false; // default value
    saveDraftButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    saveDraftButton.RowGap = null; // default value
    saveDraftButton.RowSpan = null; // default value
    saveDraftButton.SetStyle("\"btn\""); // default value
    saveDraftButton.SetVisible("(not IsDayOff and EditorLines.Length > 0) and not GetCalendar.IsLocked");
    saveDraftButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveDraftButton.Width = ""; // default value
    
    var expression11 = saveDraftButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle(null); // default value
    expression11.SetValue("\"Save as draft\"");
    expression11.Width = ""; // default value
    var builtinEvent13 = saveDraftButton.OnClick;
    builtinEvent13.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent13.Destination = onClick_SaveDay;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent13.SetArgumentValue(confirm, "False");
    
    var confirmDayButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ConfirmDayButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    confirmDayButton.Widgets.ToList().ForEach(x => x.Delete());
    confirmDayButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    confirmDayButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    confirmDayButton.ColumnGap = null; // default value
    confirmDayButton.ColumnSpan = null; // default value
    confirmDayButton.SetConfirmationMessage(null); // default value
    confirmDayButton.CustomStyle = null; // default value
    confirmDayButton.SetEnabled("True");
    confirmDayButton.FlexBasis = null; // default value
    confirmDayButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    confirmDayButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    confirmDayButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    confirmDayButton.Gap = null;
    confirmDayButton.GridColumns = null; // default value
    confirmDayButton.GridRows = null; // default value
    confirmDayButton.IsDefault = false; // default value
    confirmDayButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    confirmDayButton.RowGap = null; // default value
    confirmDayButton.RowSpan = null; // default value
    confirmDayButton.SetStyle("\"btn btn-primary\"");
    confirmDayButton.SetVisible("(not IsDayOff and EditorLines.Length > 0) and not GetCalendar.IsLocked");
    confirmDayButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    confirmDayButton.Width = ""; // default value
    
    var expression12 = confirmDayButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression12.ColumnSpan = null; // default value
    expression12.CustomStyle = null; // default value
    expression12.Example = null; // default value
    expression12.FlexBasis = null; // default value
    expression12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression12.RowSpan = null; // default value
    expression12.SetStyle(null); // default value
    expression12.SetValue("\"Confirm day\"");
    expression12.Width = ""; // default value
    var builtinEvent14 = confirmDayButton.OnClick;
    builtinEvent14.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent14.Destination = onClick_SaveDay;
    builtinEvent14.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent14.SetArgumentValue(confirm, "True");
    
    var saveDayOffButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("SaveDayOffButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    saveDayOffButton.Widgets.ToList().ForEach(x => x.Delete());
    saveDayOffButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    saveDayOffButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    saveDayOffButton.ColumnGap = null; // default value
    saveDayOffButton.ColumnSpan = null; // default value
    saveDayOffButton.SetConfirmationMessage(null); // default value
    saveDayOffButton.CustomStyle = null; // default value
    saveDayOffButton.SetEnabled("True");
    saveDayOffButton.FlexBasis = null; // default value
    saveDayOffButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    saveDayOffButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    saveDayOffButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    saveDayOffButton.Gap = null;
    saveDayOffButton.GridColumns = null; // default value
    saveDayOffButton.GridRows = null; // default value
    saveDayOffButton.IsDefault = false; // default value
    saveDayOffButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    saveDayOffButton.RowGap = null; // default value
    saveDayOffButton.RowSpan = null; // default value
    saveDayOffButton.SetStyle("\"btn btn-primary\"");
    saveDayOffButton.SetVisible("(IsDayOff and SavedMinutes = 0) and not GetCalendar.IsLocked");
    saveDayOffButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveDayOffButton.Width = ""; // default value
    
    var expression13 = saveDayOffButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression13.ColumnSpan = null; // default value
    expression13.CustomStyle = null; // default value
    expression13.Example = null; // default value
    expression13.FlexBasis = null; // default value
    expression13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression13.RowSpan = null; // default value
    expression13.SetStyle(null); // default value
    expression13.SetValue("\"Save day off\"");
    expression13.Width = ""; // default value
    var builtinEvent15 = saveDayOffButton.OnClick;
    builtinEvent15.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent15.Destination = onClick_SaveDay;
    builtinEvent15.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent15.SetArgumentValue(confirm, "False");
    
    var dayOffDeleteHoursButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("DayOffDeleteHoursButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    dayOffDeleteHoursButton.Widgets.ToList().ForEach(x => x.Delete());
    dayOffDeleteHoursButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayOffDeleteHoursButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayOffDeleteHoursButton.ColumnGap = null; // default value
    dayOffDeleteHoursButton.ColumnSpan = null; // default value
    dayOffDeleteHoursButton.SetConfirmationMessage("\"This day has hours logged. Setting it as a day off deletes them. Continue?\"");
    dayOffDeleteHoursButton.CustomStyle = null; // default value
    dayOffDeleteHoursButton.SetEnabled("True");
    dayOffDeleteHoursButton.FlexBasis = null; // default value
    dayOffDeleteHoursButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayOffDeleteHoursButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayOffDeleteHoursButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayOffDeleteHoursButton.Gap = null;
    dayOffDeleteHoursButton.GridColumns = null; // default value
    dayOffDeleteHoursButton.GridRows = null; // default value
    dayOffDeleteHoursButton.IsDefault = false; // default value
    dayOffDeleteHoursButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayOffDeleteHoursButton.RowGap = null; // default value
    dayOffDeleteHoursButton.RowSpan = null; // default value
    dayOffDeleteHoursButton.SetStyle("\"btn btn-primary\"");
    dayOffDeleteHoursButton.SetVisible("(IsDayOff and SavedMinutes > 0) and not GetCalendar.IsLocked");
    dayOffDeleteHoursButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayOffDeleteHoursButton.Width = ""; // default value
    
    var expression14 = dayOffDeleteHoursButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression14.ColumnSpan = null; // default value
    expression14.CustomStyle = null; // default value
    expression14.Example = null; // default value
    expression14.FlexBasis = null; // default value
    expression14.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression14.RowSpan = null; // default value
    expression14.SetStyle(null); // default value
    expression14.SetValue("\"Set day off and delete hours\"");
    expression14.Width = ""; // default value
    var builtinEvent16 = dayOffDeleteHoursButton.OnClick;
    builtinEvent16.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent16.Destination = onClick_SaveDay;
    builtinEvent16.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent16.SetArgumentValue(confirm, "False");
    
    var clearDayButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ClearDayButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    clearDayButton.Widgets.ToList().ForEach(x => x.Delete());
    clearDayButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    clearDayButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    clearDayButton.ColumnGap = null; // default value
    clearDayButton.ColumnSpan = null; // default value
    clearDayButton.SetConfirmationMessage("If(IsFutureDay, \"Remove the day off?\", \"Clear this day? Its hours and status will be removed.\")");
    clearDayButton.CustomStyle = null; // default value
    clearDayButton.SetEnabled("True");
    clearDayButton.FlexBasis = null; // default value
    clearDayButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    clearDayButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    clearDayButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    clearDayButton.Gap = null;
    clearDayButton.GridColumns = null; // default value
    clearDayButton.GridRows = null; // default value
    clearDayButton.IsDefault = false; // default value
    clearDayButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    clearDayButton.RowGap = null; // default value
    clearDayButton.RowSpan = null; // default value
    clearDayButton.SetStyle("\"btn btn-error\"");
    clearDayButton.SetVisible("(HasDaySheet) and not GetCalendar.IsLocked");
    clearDayButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clearDayButton.Width = ""; // default value
    
    var expression15 = clearDayButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression15.ColumnSpan = null; // default value
    expression15.CustomStyle = null; // default value
    expression15.Example = null; // default value
    expression15.FlexBasis = null; // default value
    expression15.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression15.RowSpan = null; // default value
    expression15.SetStyle(null); // default value
    expression15.SetValue("If(IsFutureDay, \"Remove day off\", \"Clear day\")");
    expression15.Width = ""; // default value
    var builtinEvent17 = clearDayButton.OnClick;
    builtinEvent17.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent17.Destination = onClick_ClearDayButton;
    builtinEvent17.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelButton = dayEditorActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    cancelButton.Widgets.ToList().ForEach(x => x.Delete());
    cancelButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    cancelButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    cancelButton.ColumnGap = null; // default value
    cancelButton.ColumnSpan = null; // default value
    cancelButton.SetConfirmationMessage(null); // default value
    cancelButton.CustomStyle = null; // default value
    cancelButton.SetEnabled("True");
    cancelButton.FlexBasis = null; // default value
    cancelButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    cancelButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    cancelButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    cancelButton.Gap = null;
    cancelButton.GridColumns = null; // default value
    cancelButton.GridRows = null; // default value
    cancelButton.IsDefault = false; // default value
    cancelButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    cancelButton.RowGap = null; // default value
    cancelButton.RowSpan = null; // default value
    cancelButton.SetStyle("\"btn sheet-popup__cancel\"");
    cancelButton.SetVisible("True"); // default value
    cancelButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    cancelButton.Width = ""; // default value
    
    var expression16 = cancelButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression16.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression16.ColumnSpan = null; // default value
    expression16.CustomStyle = null; // default value
    expression16.Example = null; // default value
    expression16.FlexBasis = null; // default value
    expression16.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression16.RowSpan = null; // default value
    expression16.SetStyle(null); // default value
    expression16.SetValue("\"Cancel\"");
    expression16.Width = ""; // default value
    var builtinEvent18 = cancelButton.OnClick;
    builtinEvent18.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent18.Destination = onClick_CancelButton;
    builtinEvent18.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var fillPendingPopup = calendar.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>("FillPendingPopup");
    fillPendingPopup.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fillPendingPopup.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fillPendingPopup.ColumnGap = null; // default value
    fillPendingPopup.ColumnSpan = null; // default value
    fillPendingPopup.CustomStyle = null; // default value
    fillPendingPopup.ExpandedInWebEditor = true; // default value
    fillPendingPopup.FlexBasis = null; // default value
    fillPendingPopup.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fillPendingPopup.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fillPendingPopup.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fillPendingPopup.Gap = null;
    fillPendingPopup.GridColumns = null; // default value
    fillPendingPopup.GridRows = null; // default value
    fillPendingPopup.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fillPendingPopup.RowGap = null; // default value
    fillPendingPopup.RowSpan = null; // default value
    fillPendingPopup.SetShowPopup("ShowFillPopup");
    fillPendingPopup.SetStyle("\"popup-dialog sheet-popup\"");
    fillPendingPopup.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var container20 = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container20.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container20.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container20.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container20.Animate = false; // default value
    container20.ColumnGap = null; // default value
    container20.ColumnSpan = null; // default value
    container20.CustomStyle = null; // default value
    container20.FlexBasis = null; // default value
    container20.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container20.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container20.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container20.Gap = null;
    container20.GridColumns = null; // default value
    container20.GridRows = null; // default value
    container20.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container20.RowGap = null; // default value
    container20.RowSpan = null; // default value
    container20.SetStyle("\"sheet-popup__title\"");
    container20.SetVisible("True"); // default value
    container20.VisibleWasExpression = false; // default value
    container20.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container20.Width = "(fill parent)"; // default value
    
    var textWidget17 = container20.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget17.CustomStyle = null; // default value
    textWidget17.SetStyleClasses(null); // default value
    textWidget17.Text = "Fill all pending days";
    
    var container21 = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container21.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container21.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container21.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container21.Animate = false; // default value
    container21.ColumnGap = null; // default value
    container21.ColumnSpan = null; // default value
    container21.CustomStyle = null; // default value
    container21.FlexBasis = null; // default value
    container21.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container21.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container21.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container21.Gap = null;
    container21.GridColumns = null; // default value
    container21.GridRows = null; // default value
    container21.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container21.RowGap = null; // default value
    container21.RowSpan = null; // default value
    container21.SetStyle("\"sheet-fill__field\"");
    container21.SetVisible("True"); // default value
    container21.VisibleWasExpression = false; // default value
    container21.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container21.Width = "(fill parent)"; // default value
    
    var container22 = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container22.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container22.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container22.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container22.Animate = false; // default value
    container22.ColumnGap = null; // default value
    container22.ColumnSpan = null; // default value
    container22.CustomStyle = null; // default value
    container22.FlexBasis = null; // default value
    container22.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container22.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container22.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container22.Gap = null;
    container22.GridColumns = null; // default value
    container22.GridRows = null; // default value
    container22.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container22.RowGap = null; // default value
    container22.RowSpan = null; // default value
    container22.SetStyle("\"sheet-fill__label\"");
    container22.SetVisible("True"); // default value
    container22.VisibleWasExpression = false; // default value
    container22.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container22.Width = "(fill parent)"; // default value
    
    var textWidget18 = container22.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget18.CustomStyle = null; // default value
    textWidget18.SetStyleClasses(null); // default value
    textWidget18.Text = "Project";
    
    var hasFillProjects = container21.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasFillProjects");
    hasFillProjects.Animate = false; // default value
    hasFillProjects.SetCondition("FillProjects.Length > 0");
    hasFillProjects.CustomStyle = null; // default value
    hasFillProjects.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var fillProjectDropdown = hasFillProjects.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("FillProjectDropdown");
    fillProjectDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fillProjectDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fillProjectDropdown.ColumnGap = null; // default value
    fillProjectDropdown.ColumnSpan = null; // default value
    fillProjectDropdown.CustomStyle = null; // default value
    fillProjectDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    fillProjectDropdown.SetEmptyValue(null); // default value
    fillProjectDropdown.SetEnabled("True");
    fillProjectDropdown.FlexBasis = null; // default value
    fillProjectDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fillProjectDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fillProjectDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fillProjectDropdown.Gap = null;
    fillProjectDropdown.GridColumns = null; // default value
    fillProjectDropdown.GridRows = null; // default value
    fillProjectDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fillProjectDropdown.SetLabels("ClientName + \" / \" + ProjectName + \" | \" + ProjectCode");
    fillProjectDropdown.SetList("FillProjects");
    fillProjectDropdown.SetMandatory("False");
    fillProjectDropdown.RowGap = null; // default value
    fillProjectDropdown.RowSpan = null; // default value
    fillProjectDropdown.SetStyle("\"dropdown\""); // default value
    fillProjectDropdown.SetValues("ProjectId");
    fillProjectDropdown.SetVariable("FillProjectId");
    fillProjectDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fillProjectDropdown.Width = ""; // default value
    var builtinEvent19 = fillProjectDropdown.OnChange;
    builtinEvent19.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent19.Destination = null; // default value
    builtinEvent19.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty9 = fillProjectDropdown.CreateExtendedProperty();
    extendedProperty9.Property = "aria-label";
    extendedProperty9.SetValue("\"Project\"");
    
    var noFillProjectsMessage = hasFillProjects.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("NoFillProjectsMessage");
    noFillProjectsMessage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    noFillProjectsMessage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    noFillProjectsMessage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    noFillProjectsMessage.Animate = false; // default value
    noFillProjectsMessage.ColumnGap = null; // default value
    noFillProjectsMessage.ColumnSpan = null; // default value
    noFillProjectsMessage.CustomStyle = null; // default value
    noFillProjectsMessage.FlexBasis = null; // default value
    noFillProjectsMessage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    noFillProjectsMessage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    noFillProjectsMessage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    noFillProjectsMessage.Gap = null;
    noFillProjectsMessage.GridColumns = null; // default value
    noFillProjectsMessage.GridRows = null; // default value
    noFillProjectsMessage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    noFillProjectsMessage.RowGap = null; // default value
    noFillProjectsMessage.RowSpan = null; // default value
    noFillProjectsMessage.SetStyle("\"sheet-popup__hint\"");
    noFillProjectsMessage.SetVisible("True"); // default value
    noFillProjectsMessage.VisibleWasExpression = false; // default value
    noFillProjectsMessage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    noFillProjectsMessage.Width = "(fill parent)"; // default value
    
    var textWidget19 = noFillProjectsMessage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget19.CustomStyle = null; // default value
    textWidget19.SetStyleClasses(null); // default value
    textWidget19.Text = "No projects assigned this month";
    
    var container23 = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container23.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container23.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container23.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container23.Animate = false; // default value
    container23.ColumnGap = null; // default value
    container23.ColumnSpan = null; // default value
    container23.CustomStyle = null; // default value
    container23.FlexBasis = null; // default value
    container23.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container23.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container23.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container23.Gap = null;
    container23.GridColumns = null; // default value
    container23.GridRows = null; // default value
    container23.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container23.RowGap = null; // default value
    container23.RowSpan = null; // default value
    container23.SetStyle("\"sheet-fill__field\"");
    container23.SetVisible("True"); // default value
    container23.VisibleWasExpression = false; // default value
    container23.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container23.Width = "(fill parent)"; // default value
    
    var container24 = container23.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container24.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container24.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container24.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container24.Animate = false; // default value
    container24.ColumnGap = null; // default value
    container24.ColumnSpan = null; // default value
    container24.CustomStyle = null; // default value
    container24.FlexBasis = null; // default value
    container24.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container24.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container24.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container24.Gap = null;
    container24.GridColumns = null; // default value
    container24.GridRows = null; // default value
    container24.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container24.RowGap = null; // default value
    container24.RowSpan = null; // default value
    container24.SetStyle("\"sheet-fill__label\"");
    container24.SetVisible("True"); // default value
    container24.VisibleWasExpression = false; // default value
    container24.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container24.Width = "(fill parent)"; // default value
    
    var textWidget20 = container24.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget20.CustomStyle = null; // default value
    textWidget20.SetStyleClasses(null); // default value
    textWidget20.Text = "Hours per day";
    
    var container25 = container23.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container25.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container25.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container25.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container25.Animate = false; // default value
    container25.ColumnGap = null; // default value
    container25.ColumnSpan = null; // default value
    container25.CustomStyle = null; // default value
    container25.FlexBasis = null; // default value
    container25.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container25.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container25.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container25.Gap = null;
    container25.GridColumns = null; // default value
    container25.GridRows = null; // default value
    container25.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container25.RowGap = null; // default value
    container25.RowSpan = null; // default value
    container25.SetStyle("\"sheet-editor__time\"");
    container25.SetVisible("True"); // default value
    container25.VisibleWasExpression = false; // default value
    container25.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container25.Width = "(fill parent)"; // default value
    
    var fillHoursDropdown = container25.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("FillHoursDropdown");
    fillHoursDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fillHoursDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fillHoursDropdown.ColumnGap = null; // default value
    fillHoursDropdown.ColumnSpan = null; // default value
    fillHoursDropdown.CustomStyle = null; // default value
    fillHoursDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    fillHoursDropdown.SetEmptyValue(null); // default value
    fillHoursDropdown.SetEnabled("True");
    fillHoursDropdown.FlexBasis = null; // default value
    fillHoursDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fillHoursDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fillHoursDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fillHoursDropdown.Gap = null;
    fillHoursDropdown.GridColumns = null; // default value
    fillHoursDropdown.GridRows = null; // default value
    fillHoursDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fillHoursDropdown.SetLabels("Label");
    fillHoursDropdown.SetList("HourOptions");
    fillHoursDropdown.SetMandatory("False");
    fillHoursDropdown.RowGap = null; // default value
    fillHoursDropdown.RowSpan = null; // default value
    fillHoursDropdown.SetStyle("\"dropdown\""); // default value
    fillHoursDropdown.SetValues("Value");
    fillHoursDropdown.SetVariable("FillHours");
    fillHoursDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fillHoursDropdown.Width = ""; // default value
    var builtinEvent20 = fillHoursDropdown.OnChange;
    builtinEvent20.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent20.Destination = null; // default value
    builtinEvent20.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty10 = fillHoursDropdown.CreateExtendedProperty();
    extendedProperty10.Property = "aria-label";
    extendedProperty10.SetValue("\"Hours per day\"");
    
    var textWidget21 = container25.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget21.CustomStyle = null; // default value
    textWidget21.SetStyleClasses(null); // default value
    textWidget21.Text = "h";
    
    var fillMinutesDropdown = container25.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("FillMinutesDropdown");
    fillMinutesDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fillMinutesDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fillMinutesDropdown.ColumnGap = null; // default value
    fillMinutesDropdown.ColumnSpan = null; // default value
    fillMinutesDropdown.CustomStyle = null; // default value
    fillMinutesDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    fillMinutesDropdown.SetEmptyValue(null); // default value
    fillMinutesDropdown.SetEnabled("True");
    fillMinutesDropdown.FlexBasis = null; // default value
    fillMinutesDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fillMinutesDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fillMinutesDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fillMinutesDropdown.Gap = null;
    fillMinutesDropdown.GridColumns = null; // default value
    fillMinutesDropdown.GridRows = null; // default value
    fillMinutesDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fillMinutesDropdown.SetLabels("Label");
    fillMinutesDropdown.SetList("MinuteOptions");
    fillMinutesDropdown.SetMandatory("False");
    fillMinutesDropdown.RowGap = null; // default value
    fillMinutesDropdown.RowSpan = null; // default value
    fillMinutesDropdown.SetStyle("\"dropdown\""); // default value
    fillMinutesDropdown.SetValues("Value");
    fillMinutesDropdown.SetVariable("FillMins");
    fillMinutesDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fillMinutesDropdown.Width = ""; // default value
    var builtinEvent21 = fillMinutesDropdown.OnChange;
    builtinEvent21.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent21.Destination = null; // default value
    builtinEvent21.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty11 = fillMinutesDropdown.CreateExtendedProperty();
    extendedProperty11.Property = "aria-label";
    extendedProperty11.SetValue("\"Minutes per day\"");
    
    var fillSummary = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FillSummary");
    fillSummary.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    fillSummary.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fillSummary.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fillSummary.Animate = false; // default value
    fillSummary.ColumnGap = null; // default value
    fillSummary.ColumnSpan = null; // default value
    fillSummary.CustomStyle = null; // default value
    fillSummary.FlexBasis = null; // default value
    fillSummary.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fillSummary.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fillSummary.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fillSummary.Gap = null;
    fillSummary.GridColumns = null; // default value
    fillSummary.GridRows = null; // default value
    fillSummary.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fillSummary.RowGap = null; // default value
    fillSummary.RowSpan = null; // default value
    fillSummary.SetStyle("\"sheet-popup__hint\"");
    fillSummary.SetVisible("True"); // default value
    fillSummary.VisibleWasExpression = false; // default value
    fillSummary.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fillSummary.Width = "(fill parent)"; // default value
    
    var expression17 = fillSummary.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression17.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression17.ColumnSpan = null; // default value
    expression17.CustomStyle = null; // default value
    expression17.Example = null; // default value
    expression17.FlexBasis = null; // default value
    expression17.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression17.RowSpan = null; // default value
    expression17.SetStyle(null); // default value
    expression17.SetValue("GetCalendar.Summary.PendingCount + \" pending days will be filled and confirmed\"");
    expression17.Width = ""; // default value
    
    var hasFillError = fillPendingPopup.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasFillError");
    hasFillError.Animate = false; // default value
    hasFillError.SetCondition("FillError <> \"\"");
    hasFillError.CustomStyle = null; // default value
    hasFillError.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container26 = hasFillError.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container26.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container26.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container26.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container26.Animate = false; // default value
    container26.ColumnGap = null; // default value
    container26.ColumnSpan = null; // default value
    container26.CustomStyle = null; // default value
    container26.FlexBasis = null; // default value
    container26.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container26.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container26.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container26.Gap = null;
    container26.GridColumns = null; // default value
    container26.GridRows = null; // default value
    container26.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container26.RowGap = null; // default value
    container26.RowSpan = null; // default value
    container26.SetStyle("\"sheet-popup__error\"");
    container26.SetVisible("True"); // default value
    container26.VisibleWasExpression = false; // default value
    container26.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container26.Width = "(fill parent)"; // default value
    
    var expression18 = container26.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression18.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression18.ColumnSpan = null; // default value
    expression18.CustomStyle = null; // default value
    expression18.Example = null; // default value
    expression18.FlexBasis = null; // default value
    expression18.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression18.RowSpan = null; // default value
    expression18.SetStyle(null); // default value
    expression18.SetValue("FillError");
    expression18.Width = ""; // default value
    
    var extendedProperty12 = container26.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"alert\"");
    
    var fillActions = fillPendingPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FillActions");
    fillActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    fillActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fillActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fillActions.Animate = false; // default value
    fillActions.ColumnGap = null; // default value
    fillActions.ColumnSpan = null; // default value
    fillActions.CustomStyle = null; // default value
    fillActions.FlexBasis = null; // default value
    fillActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fillActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fillActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fillActions.Gap = null;
    fillActions.GridColumns = null; // default value
    fillActions.GridRows = null; // default value
    fillActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fillActions.RowGap = null; // default value
    fillActions.RowSpan = null; // default value
    fillActions.SetStyle("\"sheet-popup__actions\"");
    fillActions.SetVisible("True"); // default value
    fillActions.VisibleWasExpression = false; // default value
    fillActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fillActions.Width = "(fill parent)"; // default value
    
    var confirmFillButton = fillActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("ConfirmFillButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    confirmFillButton.Widgets.ToList().ForEach(x => x.Delete());
    confirmFillButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    confirmFillButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    confirmFillButton.ColumnGap = null; // default value
    confirmFillButton.ColumnSpan = null; // default value
    confirmFillButton.SetConfirmationMessage(null); // default value
    confirmFillButton.CustomStyle = null; // default value
    confirmFillButton.SetEnabled("True");
    confirmFillButton.FlexBasis = null; // default value
    confirmFillButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    confirmFillButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    confirmFillButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    confirmFillButton.Gap = null;
    confirmFillButton.GridColumns = null; // default value
    confirmFillButton.GridRows = null; // default value
    confirmFillButton.IsDefault = false; // default value
    confirmFillButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    confirmFillButton.RowGap = null; // default value
    confirmFillButton.RowSpan = null; // default value
    confirmFillButton.SetStyle("\"btn btn-primary\"");
    confirmFillButton.SetVisible("True"); // default value
    confirmFillButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    confirmFillButton.Width = ""; // default value
    
    var expression19 = confirmFillButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression19.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression19.ColumnSpan = null; // default value
    expression19.CustomStyle = null; // default value
    expression19.Example = null; // default value
    expression19.FlexBasis = null; // default value
    expression19.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression19.RowSpan = null; // default value
    expression19.SetStyle(null); // default value
    expression19.SetValue("\"Fill and confirm\"");
    expression19.Width = ""; // default value
    var builtinEvent22 = confirmFillButton.OnClick;
    builtinEvent22.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent22.Destination = onClick_ConfirmFillButton;
    builtinEvent22.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var cancelFillButton = fillActions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("CancelFillButton");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    cancelFillButton.Widgets.ToList().ForEach(x => x.Delete());
    cancelFillButton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    cancelFillButton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    cancelFillButton.ColumnGap = null; // default value
    cancelFillButton.ColumnSpan = null; // default value
    cancelFillButton.SetConfirmationMessage(null); // default value
    cancelFillButton.CustomStyle = null; // default value
    cancelFillButton.SetEnabled("True");
    cancelFillButton.FlexBasis = null; // default value
    cancelFillButton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    cancelFillButton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    cancelFillButton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    cancelFillButton.Gap = null;
    cancelFillButton.GridColumns = null; // default value
    cancelFillButton.GridRows = null; // default value
    cancelFillButton.IsDefault = false; // default value
    cancelFillButton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    cancelFillButton.RowGap = null; // default value
    cancelFillButton.RowSpan = null; // default value
    cancelFillButton.SetStyle("\"btn sheet-popup__cancel\"");
    cancelFillButton.SetVisible("True"); // default value
    cancelFillButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    cancelFillButton.Width = ""; // default value
    
    var expression20 = cancelFillButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression20.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression20.ColumnSpan = null; // default value
    expression20.CustomStyle = null; // default value
    expression20.Example = null; // default value
    expression20.FlexBasis = null; // default value
    expression20.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression20.RowSpan = null; // default value
    expression20.SetStyle(null); // default value
    expression20.SetValue("\"Cancel\"");
    expression20.Width = ""; // default value
    var builtinEvent23 = cancelFillButton.OnClick;
    builtinEvent23.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent23.Destination = onClick_CancelFillButton;
    builtinEvent23.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty13 = calendar.CreateExtendedProperty();
    extendedProperty13.Property = "data-refresh-key";
    extendedProperty13.SetValue("RefreshKey");
}
