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
    getCalendar.Description = "Calendar cells (leading blank cells with DayNumber = 0, then one CalendarDay per day), the month summary (PendingCount) and Site.DailyExpectedMinutes.";
    getCalendar.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getCalendar.ServerRequestTimeout = null; // default value
    
    var blankDay = getCalendar.CreateLocalVariable("BlankDay");
    var eSpace = sheetBlocks.GetESpace();
    var calendarDay = eSpace.Structures.Named("CalendarDay");
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
    
    var getMonthCalendar = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthCalendar").ConnectedBelow(startNode, 1371);
    var getMonthCalendar2 = eSpace.ServerActions.Named("GetMonthCalendar");
    getMonthCalendar.Action = getMonthCalendar2;
    getMonthCalendar.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getMonthCalendar.IsDisabled = false; // default value
    getMonthCalendar.ServerRequestTimeout = null; // default value
    var month = getMonthCalendar2.InputParameters.Named("Month");
    getMonthCalendar.SetArgumentValue(month, "Month");
    var year = getMonthCalendar2.InputParameters.Named("Year");
    getMonthCalendar.SetArgumentValue(year, "Year");
    
    var getMonthSummary = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthSummary").ConnectedBelow(getMonthCalendar, 1371);
    var getMonthSummary2 = eSpace.ServerActions.Named("GetMonthSummary");
    getMonthSummary.Action = getMonthSummary2;
    getMonthSummary.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getMonthSummary.IsDisabled = false; // default value
    getMonthSummary.ServerRequestTimeout = null; // default value
    var year2 = getMonthSummary2.InputParameters.Named("Year");
    getMonthSummary.SetArgumentValue(year2, "Year");
    var month2 = getMonthSummary2.InputParameters.Named("Month");
    getMonthSummary.SetArgumentValue(month2, "Month");
    
    var assignNode = getCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getMonthSummary, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Summary", "GetMonthSummary.Summary");
    
    var assignment2 = assignNode.CreateAssignment("DailyExpectedMinutes", "Site.DailyExpectedMinutes");
    
    var assignment3 = assignNode.CreateAssignment("LeadingBlanks", "DecimalToInteger(Mod(DayOfWeek(NewDate(Year, Month, 1)) + 6, 7))");
    
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
    listAppendAll.SetArgumentValue(sourceList, "GetMonthCalendar.Days");
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
    var monthSummary = eSpace.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    summary.Description = null; // default value
    
    var dailyExpectedMinutes = getCalendar.CreateOutputParameter("DailyExpectedMinutes");
    dailyExpectedMinutes.DataType = eSpace.IntegerType;
    dailyExpectedMinutes.SetDefaultValue(null); // default value
    dailyExpectedMinutes.Description = null; // default value
    
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
    
    var year3 = onMonthChanged.CreateInputParameter("Year");
    year3.DataType = eSpace.IntegerType;
    year3.Description = null; // default value
    year3.IsMandatory = true; // default value
    
    var month3 = onMonthChanged.CreateInputParameter("Month");
    month3.DataType = eSpace.IntegerType;
    month3.Description = null; // default value
    month3.IsMandatory = true; // default value
    
    var year4 = hoursCalendar.CreateInputParameter("Year");
    year4.DataType = eSpace.IntegerType;
    year4.Description = null; // default value
    year4.IsMandatory = true; // default value
    
    var month4 = hoursCalendar.CreateInputParameter("Month");
    month4.DataType = eSpace.IntegerType;
    month4.Description = null; // default value
    month4.IsMandatory = true; // default value
    
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
    var dayEditorLine = eSpace.Structures.Named("DayEditorLine");
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
    var numberOption = eSpace.Structures.Named("NumberOption");
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
    var daySheetProjectLine = eSpace.Structures.Named("DaySheetProjectLine");
    fillProjects.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    fillProjects.Description = null; // default value
    
    var fillProjectId = hoursCalendar.CreateLocalVariable("FillProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
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
    
    var bindEscapeKey = hoursCalendar.CreateScreenAction("BindEscapeKey");
    bindEscapeKey.Description = "OnReady: Escape closes the open HoursCalendar popup (clicks its Cancel button). Bound once per page.";
    
    var startNode2 = bindEscapeKey.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var buildTimeOptions = hoursCalendar.CreateScreenAction("BuildTimeOptions");
    buildTimeOptions.Description = "Fills HourOptions (0-12) and MinuteOptions (00, 15, 30, 45) for the day editor dropdowns.";
    
    var i = buildTimeOptions.CreateLocalVariable("I");
    i.DataType = eSpace.IntegerType;
    i.SetDefaultValue(null); // default value
    i.Description = null; // default value
    
    var option = buildTimeOptions.CreateLocalVariable("Option");
    option.DataType = numberOption;
    option.Description = null; // default value
    
    var startNode3 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var assignNode2 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment4 = assignNode2.CreateAssignment("I", "0");
    
    var ifNode2 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode2, 1371);
    ifNode2.SetCondition("I <= 12");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!More hours?";
    
    var assignNode3 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment5 = assignNode3.CreateAssignment("I", "0");
    
    var assignNode4 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode4.HorizontalPosition = 5028;
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.VerticalPosition = 4913;
    
    var assignment6 = assignNode4.CreateAssignment("Option.Value", "I");
    
    var assignment7 = assignNode4.CreateAssignment("Option.Label", "I");
    
    var ifNode3 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode3, 1371);
    ifNode3.SetCondition("I <= 45");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "!More minutes?";
    
    var listAppend3 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppend").ConnectedBelow(assignNode4, 1371);
    var listAppend4 = _System_.ClientActions.Named("ListAppend");
    listAppend3.Action = listAppend4;
    listAppend3.IsDisabled = false; // default value
    var element2 = listAppend4.InputParameters.Named("Element");
    listAppend3.SetArgumentValue(element2, "Option");
    var list3 = listAppend4.InputParameters.Named("List");
    listAppend3.SetArgumentValue(list3, "HourOptions");
    
    var endNode2 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode3, 1371);
    endNode2.IsDisabled = false; // default value
    
    var assignNode5 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode5.HorizontalPosition = 5028;
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    assignNode5.VerticalPosition = 7655;
    
    var assignment8 = assignNode5.CreateAssignment("Option.Value", "I");
    
    var assignment9 = assignNode5.CreateAssignment("Option.Label", "If(I < 10, \"0\", \"\") + I");
    
    var assignNode6 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend3, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.Target = ifNode2;
    
    var assignment10 = assignNode6.CreateAssignment("I", "I + 1");
    
    var listAppend22 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppend2").ConnectedBelow(assignNode5, 1371);
    listAppend22.Action = listAppend4;
    listAppend22.IsDisabled = false; // default value
    listAppend22.SetArgumentValue(list3, "MinuteOptions");
    listAppend22.SetArgumentValue(element2, "Option");
    
    var assignNode7 = buildTimeOptions.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend22, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    assignNode7.Target = ifNode3;
    
    var assignment11 = assignNode7.CreateAssignment("I", "I + 15");
    hoursCalendar.OnInitialize.Destination = buildTimeOptions;
    
    var onParametersChanged = hoursCalendar.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads GetCalendar when Year, Month or RefreshKey change.";
    
    var startNode4 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode4, 1371);
    refreshDataNode.DataSource = getCalendar;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode3.IsDisabled = false; // default value
    hoursCalendar.OnParametersChanged.Destination = onParametersChanged;
    hoursCalendar.OnReady.Destination = bindEscapeKey;
    
    var escapeClosesPopup = bindEscapeKey.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("EscapeClosesPopup").ConnectedBelow(startNode2, 1371);
    escapeClosesPopup.Description = null; // default value
    escapeClosesPopup.IsDisabled = false; // default value
    escapeClosesPopup.JavaScript = "if (!window.sheetCalEscapeBound) {\n    window.sheetCalEscapeBound = true;\n    document.addEventListener(\'keydown\', function (e) {\n        if (e.key === \'Escape\') {\n            var buttons = document.querySelectorAll(\'.sheet-popup .sheet-popup__cancel\');\n            if (buttons.length > 0) { buttons[buttons.length - 1].click(); }\n        }\n    });\n}";
    ifNode2.FalseTarget = assignNode3;
    ifNode2.TrueTarget = assignNode4;
    ifNode3.FalseTarget = endNode2;
    ifNode3.TrueTarget = assignNode5;
    
    var endNode4 = bindEscapeKey.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(escapeClosesPopup, 1371);
    endNode4.IsDisabled = false; // default value
    
    var clearDay = hoursCalendar.CreateScreenAction("ClearDay");
    clearDay.Description = "Calls ClearDaySheet for EditDate (Clear day / Remove day off). Errors stay in the popup.";
    
    var startNode5 = clearDay.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var clearDaySheet = clearDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ClearDaySheet").ConnectedBelow(startNode5, 1371);
    var clearDaySheet2 = eSpace.ServerActions.Named("ClearDaySheet");
    clearDaySheet.Action = clearDaySheet2;
    clearDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    clearDaySheet.IsDisabled = false; // default value
    clearDaySheet.ServerRequestTimeout = null; // default value
    var date3 = clearDaySheet2.InputParameters.Named("Date");
    clearDaySheet.SetArgumentValue(date3, "EditDate");
    
    var dayEditorSaved = clearDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("DayEditorSaved").ConnectedBelow(clearDaySheet, 1371);
    dayEditorSaved.IsDisabled = false; // default value
    
    var dayEditorSaved2 = hoursCalendar.CreateScreenAction("DayEditorSaved");
    dayEditorSaved2.Description = "After a successful save/clear: closes the popup, refreshes GetCalendar, shows a success message and raises OnDaySaved(EditDate).";
    
    var successMessage = dayEditorSaved2.CreateInputParameter("SuccessMessage");
    successMessage.DataType = eSpace.TextType;
    successMessage.Description = null; // default value
    successMessage.IsMandatory = true; // default value
    
    var startNode6 = dayEditorSaved2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.IsDisabled = false; // default value
    
    var assignNode8 = dayEditorSaved2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode6, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    
    var assignment12 = assignNode8.CreateAssignment("ShowDayEditor", "False");
    
    var refreshDataNode2 = dayEditorSaved2.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode8, 1371);
    refreshDataNode2.DataSource = getCalendar;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var messageNode = dayEditorSaved2.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode2, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("SuccessMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var onDaySaved2 = dayEditorSaved2.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySaved").ConnectedBelow(messageNode, 1371);
    onDaySaved2.Description = null; // default value
    onDaySaved2.Event = onDaySaved;
    onDaySaved2.IsDisabled = false; // default value
    onDaySaved2.SetArgumentValue(date, "EditDate");
    
    var endNode5 = dayEditorSaved2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onDaySaved2, 1371);
    endNode5.IsDisabled = false; // default value
    dayEditorSaved.Action = dayEditorSaved2;
    
    var endNode6 = clearDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(dayEditorSaved, 1371);
    endNode6.IsDisabled = false; // default value
    dayEditorSaved.SetArgumentValue(successMessage, "If(IsFutureDay, \"Day off removed.\", \"Day cleared.\")");
    
    var allExceptions = clearDay.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = false;
    allExceptions.VerticalPosition = 800;
    
    var assignNode9 = clearDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    
    var assignment13 = assignNode9.CreateAssignment("EditorError", "AllExceptions.ExceptionMessage");
    
    var endNode7 = clearDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode9, 1371);
    endNode7.IsDisabled = false; // default value
    
    var closeDayEditor = hoursCalendar.CreateScreenAction("CloseDayEditor");
    closeDayEditor.Description = "Cancel: closes the day editor without saving.";
    
    var startNode7 = closeDayEditor.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.IsDisabled = false; // default value
    
    var assignNode10 = closeDayEditor.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 1371);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = null; // default value
    
    var assignment14 = assignNode10.CreateAssignment("ShowDayEditor", "False");
    
    var endNode8 = closeDayEditor.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode10, 1371);
    endNode8.IsDisabled = false; // default value
    
    var closeFillPopup = hoursCalendar.CreateScreenAction("CloseFillPopup");
    closeFillPopup.Description = "Cancel: closes FillPendingPopup without filling.";
    
    var startNode8 = closeFillPopup.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.IsDisabled = false; // default value
    
    var assignNode11 = closeFillPopup.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode8, 1371);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = null; // default value
    
    var assignment15 = assignNode11.CreateAssignment("ShowFillPopup", "False");
    
    var endNode9 = closeFillPopup.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode11, 1371);
    endNode9.IsDisabled = false; // default value
    
    var confirmFill = hoursCalendar.CreateScreenAction("ConfirmFill");
    confirmFill.Description = "Runs FillPendingDays for the month; on success shows \"X days filled\", refreshes GetCalendar and raises OnDaySaved(CurrDate()). Errors stay in the popup.";
    
    var startNode9 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.HorizontalPosition = 3200;
    startNode9.IsDisabled = false; // default value
    startNode9.VerticalPosition = 800;
    
    var ifNode4 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode9, 1371);
    ifNode4.SetCondition("FillProjectId = NullIdentifier()");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "!No project?";
    
    var fillPendingDays = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("FillPendingDays").Below(ifNode4, 1371);
    var fillPendingDays2 = eSpace.ServerActions.Named("FillPendingDays");
    fillPendingDays.Action = fillPendingDays2;
    fillPendingDays.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    fillPendingDays.IsDisabled = false; // default value
    fillPendingDays.ServerRequestTimeout = null; // default value
    var year5 = fillPendingDays2.InputParameters.Named("Year");
    fillPendingDays.SetArgumentValue(year5, "Year");
    var minutesPerDay = fillPendingDays2.InputParameters.Named("MinutesPerDay");
    fillPendingDays.SetArgumentValue(minutesPerDay, "FillHours * 60 + FillMins");
    var month5 = fillPendingDays2.InputParameters.Named("Month");
    fillPendingDays.SetArgumentValue(month5, "Month");
    var projectId = fillPendingDays2.InputParameters.Named("ProjectId");
    fillPendingDays.SetArgumentValue(projectId, "FillProjectId");
    ifNode4.FalseTarget = fillPendingDays;
    
    var assignNode12 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode12.HorizontalPosition = 5028;
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = null; // default value
    assignNode12.VerticalPosition = 3542;
    
    var assignment16 = assignNode12.CreateAssignment("FillError", "\"Choose a project.\"");
    ifNode4.TrueTarget = assignNode12;
    
    var assignNode13 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(fillPendingDays, 1371);
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = null; // default value
    
    var assignment17 = assignNode13.CreateAssignment("ShowFillPopup", "False");
    
    var endNode10 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode12, 1371);
    endNode10.IsDisabled = false; // default value
    
    var refreshDataNode3 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode13, 1371);
    refreshDataNode3.DataSource = getCalendar;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    
    var messageNode2 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode3, 1371);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = null; // default value
    messageNode2.SetMessage("FillPendingDays.FilledCount + \" days filled\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var onDaySaved3 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySaved").ConnectedBelow(messageNode2, 1371);
    onDaySaved3.Description = null; // default value
    onDaySaved3.Event = onDaySaved;
    onDaySaved3.IsDisabled = false; // default value
    onDaySaved3.SetArgumentValue(date, "CurrDate()");
    
    var endNode11 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onDaySaved3, 1371);
    endNode11.IsDisabled = false; // default value
    
    var allExceptions2 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions2.AbortTransaction = false;
    allExceptions2.Exception = eSpace.AllExceptions;
    allExceptions2.HorizontalPosition = 8684;
    allExceptions2.IsDisabled = false; // default value
    allExceptions2.LogError = false;
    allExceptions2.VerticalPosition = 800;
    
    var assignNode14 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions2, 1371);
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = null; // default value
    
    var assignment18 = assignNode14.CreateAssignment("FillError", "AllExceptions.ExceptionMessage");
    
    var endNode12 = confirmFill.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode14, 1371);
    endNode12.IsDisabled = false; // default value
    
    var dayOnClick = hoursCalendar.CreateScreenAction("DayOnClick");
    dayOnClick.Description = "Raises OnDaySelected for any day; opens the day editor only when the day is editable (not a weekend or holiday).";
    
    var day = dayOnClick.CreateInputParameter("Day");
    day.DataType = calendarDay;
    day.Description = null; // default value
    day.IsMandatory = true; // default value
    
    var startNode10 = dayOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.HorizontalPosition = 3200;
    startNode10.IsDisabled = false; // default value
    startNode10.VerticalPosition = 800;
    
    var onDaySelected2 = dayOnClick.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnDaySelected").ConnectedBelow(startNode10, 1371);
    onDaySelected2.Description = null; // default value
    onDaySelected2.Event = onDaySelected;
    onDaySelected2.IsDisabled = false; // default value
    onDaySelected2.SetArgumentValue(date2, "Day.Date");
    
    var ifNode5 = dayOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(onDaySelected2, 1371);
    ifNode5.SetCondition("Day.IsEditable");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "Editable?";
    
    var endNode13 = dayOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode5, 1371);
    endNode13.IsDisabled = false; // default value
    ifNode5.FalseTarget = endNode13;
    
    var openDayEditor = dayOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("OpenDayEditor").ToTheRightOf(ifNode5, 1828);
    openDayEditor.IsDisabled = false; // default value
    openDayEditor.Target = endNode13;
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
    
    var startNode11 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 3200;
    startNode11.IsDisabled = false; // default value
    startNode11.VerticalPosition = 800;
    
    var getDaySheetDetail = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetDaySheetDetail").ConnectedBelow(startNode11, 1371);
    var getDaySheetDetail2 = eSpace.ServerActions.Named("GetDaySheetDetail");
    getDaySheetDetail.Action = getDaySheetDetail2;
    getDaySheetDetail.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getDaySheetDetail.IsDisabled = false; // default value
    getDaySheetDetail.ServerRequestTimeout = null; // default value
    var date4 = getDaySheetDetail2.InputParameters.Named("Date");
    getDaySheetDetail.SetArgumentValue(date4, "Day.Date");
    
    var assignNode15 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getDaySheetDetail, 1371);
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = null; // default value
    
    var assignment19 = assignNode15.CreateAssignment("EditDate", "Day.Date");
    
    var assignment20 = assignNode15.CreateAssignment("IsFutureDay", "Day.Date > CurrDate()");
    
    var assignment21 = assignNode15.CreateAssignment("HasDaySheet", "GetDaySheetDetail.Detail.DaySheetId <> NullIdentifier()");
    
    var assignment22 = assignNode15.CreateAssignment("IsDayOff", "GetDaySheetDetail.Detail.DayStatusId = Entities.DayStatus.DayOff or Day.Date > CurrDate()");
    
    var assignment23 = assignNode15.CreateAssignment("EditorNotes", "GetDaySheetDetail.Detail.Notes");
    
    var assignment24 = assignNode15.CreateAssignment("SavedMinutes", "GetDaySheetDetail.Detail.TotalMinutes");
    
    var assignment25 = assignNode15.CreateAssignment("EditorError", "\"\"");
    
    var listClear = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListClear").ConnectedBelow(assignNode15, 1371);
    var listClear2 = _System_.ClientActions.Named("ListClear");
    listClear.Action = listClear2;
    listClear.IsDisabled = false; // default value
    var list4 = listClear2.InputParameters.Named("List");
    listClear.SetArgumentValue(list4, "EditorLines");
    
    var forEachNode = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(listClear, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetDaySheetDetail.Detail.Lines");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode16 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode16.IsDisabled = false; // default value
    assignNode16.Label = null; // default value
    
    var assignment26 = assignNode16.CreateAssignment("Line.ProjectId", "GetDaySheetDetail.Detail.Lines.Current.ProjectId");
    
    var assignment27 = assignNode16.CreateAssignment("Line.ClientName", "GetDaySheetDetail.Detail.Lines.Current.ClientName");
    
    var assignment28 = assignNode16.CreateAssignment("Line.ProjectName", "GetDaySheetDetail.Detail.Lines.Current.ProjectName");
    
    var assignment29 = assignNode16.CreateAssignment("Line.ProjectCode", "GetDaySheetDetail.Detail.Lines.Current.ProjectCode");
    
    var assignment30 = assignNode16.CreateAssignment("Line.Hours", "DecimalToInteger(Trunc(GetDaySheetDetail.Detail.Lines.Current.Minutes / 60))");
    
    var assignment31 = assignNode16.CreateAssignment("Line.Mins", "DecimalToInteger(Mod(GetDaySheetDetail.Detail.Lines.Current.Minutes, 60))");
    
    var assignment32 = assignNode16.CreateAssignment("Line.Description", "GetDaySheetDetail.Detail.Lines.Current.Description");
    
    var recalcEditorTotal = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RecalcEditorTotal").Below(forEachNode, 1371);
    recalcEditorTotal.IsDisabled = false; // default value
    
    var listAppend5 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppend").ConnectedBelow(assignNode16, 1371);
    listAppend5.Action = listAppend4;
    listAppend5.IsDisabled = false; // default value
    listAppend5.Target = forEachNode;
    listAppend5.SetArgumentValue(list3, "EditorLines");
    listAppend5.SetArgumentValue(element2, "Line");
    
    var assignNode17 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(recalcEditorTotal, 1371);
    assignNode17.IsDisabled = false; // default value
    assignNode17.Label = null; // default value
    
    var assignment33 = assignNode17.CreateAssignment("ShowDayEditor", "True");
    
    var endNode14 = openDayEditor2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode17, 1371);
    endNode14.IsDisabled = false; // default value
    openDayEditor.Action = openDayEditor2;
    openDayEditor.SetArgumentValue(day2, "Day");
    forEachNode.CycleTarget = assignNode16;
    forEachNode.Target = recalcEditorTotal;
    
    var recalcEditorTotal2 = hoursCalendar.CreateScreenAction("RecalcEditorTotal");
    recalcEditorTotal2.Description = "Recomputes EditorTotal (minutes) from the hours/minutes dropdowns of EditorLines.";
    
    var startNode12 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.HorizontalPosition = 3200;
    startNode12.IsDisabled = false; // default value
    startNode12.VerticalPosition = 800;
    
    var assignNode18 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode12, 1371);
    assignNode18.IsDisabled = false; // default value
    assignNode18.Label = null; // default value
    
    var assignment34 = assignNode18.CreateAssignment("EditorTotal", "0");
    
    var forEachNode2 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode18, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("EditorLines");
    forEachNode2.SetStartIndex(null); // default value
    
    var assignNode19 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode2, 1828);
    assignNode19.IsDisabled = false; // default value
    assignNode19.Label = null; // default value
    assignNode19.Target = forEachNode2;
    
    var assignment35 = assignNode19.CreateAssignment("EditorTotal", "EditorTotal + EditorLines.Current.Hours * 60 + EditorLines.Current.Mins");
    
    var endNode15 = recalcEditorTotal2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode2, 1371);
    endNode15.IsDisabled = false; // default value
    recalcEditorTotal.Action = recalcEditorTotal2;
    forEachNode2.CycleTarget = assignNode19;
    forEachNode2.Target = endNode15;
    
    var nextMonthOnClick = hoursCalendar.CreateScreenAction("NextMonthOnClick");
    nextMonthOnClick.Description = "Raises OnMonthChanged with the next month (December rolls over to January of the next year).";
    
    var startNode13 = nextMonthOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode13.HorizontalPosition = 3200;
    startNode13.IsDisabled = false; // default value
    startNode13.VerticalPosition = 800;
    
    var onMonthChanged2 = nextMonthOnClick.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnMonthChanged").ConnectedBelow(startNode13, 1371);
    onMonthChanged2.Description = null; // default value
    onMonthChanged2.Event = onMonthChanged;
    onMonthChanged2.IsDisabled = false; // default value
    onMonthChanged2.SetArgumentValue(year3, "If(Month = 12, Year + 1, Year)");
    onMonthChanged2.SetArgumentValue(month3, "If(Month = 12, 1, Month + 1)");
    
    var endNode16 = nextMonthOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onMonthChanged2, 1371);
    endNode16.IsDisabled = false; // default value
    
    var openFillPopup = hoursCalendar.CreateScreenAction("OpenFillPopup");
    openFillPopup.Description = "Loads the projects assigned today and opens FillPendingPopup with the time defaulting to Site.DailyExpectedMinutes.";
    
    var startNode14 = openFillPopup.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode14.IsDisabled = false; // default value
    
    var getUserProjects = openFillPopup.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetUserProjects").ConnectedBelow(startNode14, 1371);
    var getUserProjects2 = eSpace.ServerActions.Named("GetUserProjects");
    getUserProjects.Action = getUserProjects2;
    getUserProjects.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getUserProjects.IsDisabled = false; // default value
    getUserProjects.ServerRequestTimeout = null; // default value
    var forDate = getUserProjects2.InputParameters.Named("ForDate");
    getUserProjects.SetArgumentValue(forDate, "CurrDate()");
    
    var assignNode20 = openFillPopup.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getUserProjects, 1371);
    assignNode20.IsDisabled = false; // default value
    assignNode20.Label = null; // default value
    
    var assignment36 = assignNode20.CreateAssignment("FillProjects", "GetUserProjects.Projects");
    
    var assignment37 = assignNode20.CreateAssignment("FillProjectId", "GetUserProjects.Projects.Current.ProjectId");
    
    var assignment38 = assignNode20.CreateAssignment("FillHours", "DecimalToInteger(Trunc(GetCalendar.DailyExpectedMinutes / 60))");
    
    var assignment39 = assignNode20.CreateAssignment("FillMins", "DecimalToInteger(Mod(GetCalendar.DailyExpectedMinutes, 60))");
    
    var assignment40 = assignNode20.CreateAssignment("FillError", "\"\"");
    
    var assignment41 = assignNode20.CreateAssignment("ShowFillPopup", "True");
    
    var endNode17 = openFillPopup.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode20, 1371);
    endNode17.IsDisabled = false; // default value
    
    var previousMonthOnClick = hoursCalendar.CreateScreenAction("PreviousMonthOnClick");
    previousMonthOnClick.Description = "Raises OnMonthChanged with the previous month (January rolls back to December of the previous year).";
    
    var startNode15 = previousMonthOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode15.HorizontalPosition = 3200;
    startNode15.IsDisabled = false; // default value
    startNode15.VerticalPosition = 800;
    
    var onMonthChanged3 = previousMonthOnClick.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnMonthChanged").ConnectedBelow(startNode15, 1371);
    onMonthChanged3.Description = null; // default value
    onMonthChanged3.Event = onMonthChanged;
    onMonthChanged3.IsDisabled = false; // default value
    onMonthChanged3.SetArgumentValue(month3, "If(Month = 1, 12, Month - 1)");
    onMonthChanged3.SetArgumentValue(year3, "If(Month = 1, Year - 1, Year)");
    
    var endNode18 = previousMonthOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onMonthChanged3, 1371);
    endNode18.IsDisabled = false; // default value
    
    var saveDay = hoursCalendar.CreateScreenAction("SaveDay");
    saveDay.Description = "Builds the entries from EditorLines and calls SaveDaySheet. Validation errors stay in the popup (EditorError).";
    
    var confirm = saveDay.CreateInputParameter("Confirm");
    confirm.DataType = eSpace.BooleanType;
    confirm.Description = null; // default value
    confirm.IsMandatory = true; // default value
    
    var entries = saveDay.CreateLocalVariable("Entries");
    var timeEntryInput = eSpace.Structures.Named("TimeEntryInput");
    entries.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    entries.Description = null; // default value
    
    var entry = saveDay.CreateLocalVariable("Entry");
    entry.DataType = timeEntryInput;
    entry.Description = null; // default value
    
    var startNode16 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode16.HorizontalPosition = 3200;
    startNode16.IsDisabled = false; // default value
    startNode16.VerticalPosition = 800;
    
    var assignNode21 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode16, 1371);
    assignNode21.IsDisabled = false; // default value
    assignNode21.Label = null; // default value
    
    var assignment42 = assignNode21.CreateAssignment("EditorError", "\"\"");
    
    var forEachNode3 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode21, 1371);
    forEachNode3.IsDisabled = false; // default value
    forEachNode3.Label = null; // default value
    forEachNode3.SetMaximumIterations(null); // default value
    forEachNode3.SetRecordList("EditorLines");
    forEachNode3.SetStartIndex(null); // default value
    
    var assignNode22 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode22.IsDisabled = false; // default value
    assignNode22.Label = null; // default value
    
    var assignment43 = assignNode22.CreateAssignment("Entry.ProjectId", "EditorLines.Current.ProjectId");
    
    var assignment44 = assignNode22.CreateAssignment("Entry.Minutes", "If(IsDayOff, 0, EditorLines.Current.Hours * 60 + EditorLines.Current.Mins)");
    
    var assignment45 = assignNode22.CreateAssignment("Entry.Description", "EditorLines.Current.Description");
    forEachNode3.CycleTarget = assignNode22;
    
    var saveDaySheet = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("SaveDaySheet").Below(forEachNode3, 1371);
    var saveDaySheet2 = eSpace.ServerActions.Named("SaveDaySheet");
    saveDaySheet.Action = saveDaySheet2;
    saveDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    saveDaySheet.IsDisabled = false; // default value
    saveDaySheet.ServerRequestTimeout = null; // default value
    var date5 = saveDaySheet2.InputParameters.Named("Date");
    saveDaySheet.SetArgumentValue(date5, "EditDate");
    var notes = saveDaySheet2.InputParameters.Named("Notes");
    saveDaySheet.SetArgumentValue(notes, "EditorNotes");
    var entries2 = saveDaySheet2.InputParameters.Named("Entries");
    saveDaySheet.SetArgumentValue(entries2, "Entries");
    var confirm2 = saveDaySheet2.InputParameters.Named("Confirm");
    saveDaySheet.SetArgumentValue(confirm2, "Confirm");
    var isDayOff2 = saveDaySheet2.InputParameters.Named("IsDayOff");
    saveDaySheet.SetArgumentValue(isDayOff2, "IsDayOff");
    forEachNode3.Target = saveDaySheet;
    
    var listAppend6 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppend").ConnectedBelow(assignNode22, 1371);
    listAppend6.Action = listAppend4;
    listAppend6.IsDisabled = false; // default value
    listAppend6.Target = forEachNode3;
    listAppend6.SetArgumentValue(list3, "Entries");
    listAppend6.SetArgumentValue(element2, "Entry");
    
    var dayEditorSaved3 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("DayEditorSaved").ConnectedBelow(saveDaySheet, 1371);
    dayEditorSaved3.Action = dayEditorSaved2;
    dayEditorSaved3.IsDisabled = false; // default value
    dayEditorSaved3.SetArgumentValue(successMessage, "If(IsDayOff, \"Day off saved.\", If(Confirm, \"Day confirmed.\", \"Draft saved.\"))");
    
    var endNode19 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(dayEditorSaved3, 1371);
    endNode19.IsDisabled = false; // default value
    
    var allExceptions3 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions3.AbortTransaction = false;
    allExceptions3.Exception = eSpace.AllExceptions;
    allExceptions3.HorizontalPosition = 8684;
    allExceptions3.IsDisabled = false; // default value
    allExceptions3.LogError = false;
    allExceptions3.VerticalPosition = 800;
    
    var assignNode23 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions3, 1371);
    assignNode23.IsDisabled = false; // default value
    assignNode23.Label = null; // default value
    
    var assignment46 = assignNode23.CreateAssignment("EditorError", "AllExceptions.ExceptionMessage");
    
    var endNode20 = saveDay.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode23, 1371);
    endNode20.IsDisabled = false; // default value
    
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
    builtinEvent.Destination = previousMonthOnClick;
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
    builtinEvent2.Destination = nextMonthOnClick;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = nextMonth.CreateExtendedProperty();
    extendedProperty2.Property = "aria-label";
    extendedProperty2.SetValue("\"Next month\"");
    
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
    
    var isBlankCell = calendarDays.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsBlankCell");
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
    
    var expression2 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetCalendar.Cells.Current.DayNumber");
    expression2.Width = ""; // default value
    
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
    builtinEvent3.Destination = dayOnClick;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent3.SetArgumentValue(day, "GetCalendar.Cells.Current");
    
    var extendedProperty3 = dayCell.CreateExtendedProperty();
    extendedProperty3.Property = "title";
    extendedProperty3.SetValue("If(GetCalendar.Cells.Current.HolidayName <> \"\", GetCalendar.Cells.Current.HolidayName, If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"Confirmed\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"Day off\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"Holiday\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Pending, \"Pending\", If(GetCalendar.Cells.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"Weekend\", \"Future\"))))) + \" · \" + FormatMinutes(GetCalendar.Cells.Current.TotalMinutes, \":\"))");
    
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
    fillPendingButton.SetVisible("True"); // default value
    fillPendingButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fillPendingButton.Width = ""; // default value
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)fillPendingButton.Widgets.First();
    textWidget8.CustomStyle = null; // default value
    textWidget8.Name = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Fill all pending days";
    var builtinEvent5 = fillPendingButton.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = openFillPopup;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
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
    
    var expression3 = dayEditorTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("FormatDateTime(EditDate, \"dddd, d MMMM yyyy\")");
    expression3.Width = ""; // default value
    
    var dayKind = dayEditorPopup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButtonGroup>("DayKind");
    /*** Deleting Widgets that are created by default when instantiating an IButtonGroup ***/
    dayKind.Widgets.ToList().ForEach(x => x.Delete());
    dayKind.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayKind.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayKind.ColumnGap = null; // default value
    dayKind.ColumnSpan = null; // default value
    dayKind.CustomStyle = null; // default value
    dayKind.SetEnabled("True");
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
    
    var textWidget9 = workDayOption.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.CustomStyle = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Work day";
    
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
    
    var textWidget10 = dayOffOption.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.CustomStyle = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Day off";
    var builtinEvent6 = dayKind.OnChange;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = null; // default value
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
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
    
    var textWidget11 = container12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget11.CustomStyle = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Hours cannot be logged in the future. You can only set or remove a day off.";
    
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
    
    var projectLines = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectLines");
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
    
    var expression4 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("EditorLines.Current.ClientName");
    expression4.Width = ""; // default value
    
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
    
    var expression5 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("EditorLines.Current.ProjectName + \" | \" + EditorLines.Current.ProjectCode");
    expression5.Width = ""; // default value
    
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
    hoursDropdown.SetEnabled("True");
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
    var builtinEvent7 = hoursDropdown.OnChange;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = recalcEditorTotal2;
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty5 = hoursDropdown.CreateExtendedProperty();
    extendedProperty5.Property = "aria-label";
    extendedProperty5.SetValue("\"Hours for \" + EditorLines.Current.ProjectName");
    
    var textWidget12 = container18.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.CustomStyle = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "h";
    
    var minutesDropdown = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("MinutesDropdown");
    minutesDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    minutesDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    minutesDropdown.ColumnGap = null; // default value
    minutesDropdown.ColumnSpan = null; // default value
    minutesDropdown.CustomStyle = null; // default value
    minutesDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Text;
    minutesDropdown.SetEmptyValue(null); // default value
    minutesDropdown.SetEnabled("True");
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
    var builtinEvent8 = minutesDropdown.OnChange;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = recalcEditorTotal2;
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty6 = minutesDropdown.CreateExtendedProperty();
    extendedProperty6.Property = "aria-label";
    extendedProperty6.SetValue("\"Minutes for \" + EditorLines.Current.ProjectName");
    
    var descriptionInput = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("DescriptionInput");
    descriptionInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    descriptionInput.ColumnSpan = null; // default value
    descriptionInput.CustomStyle = null; // default value
    descriptionInput.SetEnabled("True");
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
    var builtinEvent9 = descriptionInput.OnChange;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = null; // default value
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty7 = descriptionInput.CreateExtendedProperty();
    extendedProperty7.Property = "placeholder";
    extendedProperty7.SetValue("\"Description (optional)\"");
    var builtinEvent10 = projectLines.OnScrollEnding;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = null; // default value
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var dayTotal = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DayTotal");
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
    
    var expression6 = dayTotal.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("\"Total of the day: \" + FormatMinutes(EditorTotal, \":\")");
    expression6.Width = ""; // default value
    
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
    
    var expression7 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("\"This day has \" + FormatMinutes(SavedMinutes, \":\") + \" logged. Setting it as a day off deletes those hours.\"");
    expression7.Width = ""; // default value
    
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
    
    var expression8 = editorErrorMessage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("EditorError");
    expression8.Width = ""; // default value
    
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
    saveDraftButton.SetVisible("not IsDayOff");
    saveDraftButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveDraftButton.Width = ""; // default value
    
    var expression9 = saveDraftButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("\"Save as draft\"");
    expression9.Width = ""; // default value
    var builtinEvent11 = saveDraftButton.OnClick;
    builtinEvent11.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent11.Destination = saveDay;
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent11.SetArgumentValue(confirm, "False");
    
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
    confirmDayButton.SetVisible("not IsDayOff");
    confirmDayButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    confirmDayButton.Width = ""; // default value
    
    var expression10 = confirmDayButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("\"Confirm day\"");
    expression10.Width = ""; // default value
    var builtinEvent12 = confirmDayButton.OnClick;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent12.Destination = saveDay;
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent12.SetArgumentValue(confirm, "True");
    
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
    saveDayOffButton.SetVisible("IsDayOff and SavedMinutes = 0");
    saveDayOffButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    saveDayOffButton.Width = ""; // default value
    
    var expression11 = saveDayOffButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle(null); // default value
    expression11.SetValue("\"Save day off\"");
    expression11.Width = ""; // default value
    var builtinEvent13 = saveDayOffButton.OnClick;
    builtinEvent13.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent13.Destination = saveDay;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent13.SetArgumentValue(confirm, "False");
    
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
    dayOffDeleteHoursButton.SetVisible("IsDayOff and SavedMinutes > 0");
    dayOffDeleteHoursButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayOffDeleteHoursButton.Width = ""; // default value
    
    var expression12 = dayOffDeleteHoursButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression12.ColumnSpan = null; // default value
    expression12.CustomStyle = null; // default value
    expression12.Example = null; // default value
    expression12.FlexBasis = null; // default value
    expression12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression12.RowSpan = null; // default value
    expression12.SetStyle(null); // default value
    expression12.SetValue("\"Set day off and delete hours\"");
    expression12.Width = ""; // default value
    var builtinEvent14 = dayOffDeleteHoursButton.OnClick;
    builtinEvent14.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent14.Destination = saveDay;
    builtinEvent14.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent14.SetArgumentValue(confirm, "False");
    
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
    clearDayButton.SetVisible("HasDaySheet");
    clearDayButton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    clearDayButton.Width = ""; // default value
    
    var expression13 = clearDayButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression13.ColumnSpan = null; // default value
    expression13.CustomStyle = null; // default value
    expression13.Example = null; // default value
    expression13.FlexBasis = null; // default value
    expression13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression13.RowSpan = null; // default value
    expression13.SetStyle(null); // default value
    expression13.SetValue("If(IsFutureDay, \"Remove day off\", \"Clear day\")");
    expression13.Width = ""; // default value
    var builtinEvent15 = clearDayButton.OnClick;
    builtinEvent15.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent15.Destination = clearDay;
    builtinEvent15.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
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
    
    var expression14 = cancelButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression14.ColumnSpan = null; // default value
    expression14.CustomStyle = null; // default value
    expression14.Example = null; // default value
    expression14.FlexBasis = null; // default value
    expression14.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression14.RowSpan = null; // default value
    expression14.SetStyle(null); // default value
    expression14.SetValue("\"Cancel\"");
    expression14.Width = ""; // default value
    var builtinEvent16 = cancelButton.OnClick;
    builtinEvent16.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent16.Destination = closeDayEditor;
    builtinEvent16.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
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
    
    var textWidget13 = container20.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.CustomStyle = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Fill all pending days";
    
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
    
    var textWidget14 = container22.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget14.CustomStyle = null; // default value
    textWidget14.SetStyleClasses(null); // default value
    textWidget14.Text = "Project";
    
    var fillProjectDropdown = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("FillProjectDropdown");
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
    var builtinEvent17 = fillProjectDropdown.OnChange;
    builtinEvent17.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent17.Destination = null; // default value
    builtinEvent17.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty9 = fillProjectDropdown.CreateExtendedProperty();
    extendedProperty9.Property = "aria-label";
    extendedProperty9.SetValue("\"Project\"");
    
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
    
    var textWidget15 = container24.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.CustomStyle = null; // default value
    textWidget15.SetStyleClasses(null); // default value
    textWidget15.Text = "Hours per day";
    
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
    var builtinEvent18 = fillHoursDropdown.OnChange;
    builtinEvent18.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent18.Destination = null; // default value
    builtinEvent18.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var extendedProperty10 = fillHoursDropdown.CreateExtendedProperty();
    extendedProperty10.Property = "aria-label";
    extendedProperty10.SetValue("\"Hours per day\"");
    
    var textWidget16 = container25.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget16.CustomStyle = null; // default value
    textWidget16.SetStyleClasses(null); // default value
    textWidget16.Text = "h";
    
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
    var builtinEvent19 = fillMinutesDropdown.OnChange;
    builtinEvent19.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent19.Destination = null; // default value
    builtinEvent19.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
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
    
    var expression15 = fillSummary.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression15.ColumnSpan = null; // default value
    expression15.CustomStyle = null; // default value
    expression15.Example = null; // default value
    expression15.FlexBasis = null; // default value
    expression15.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression15.RowSpan = null; // default value
    expression15.SetStyle(null); // default value
    expression15.SetValue("GetCalendar.Summary.PendingCount + \" pending days will be filled and confirmed\"");
    expression15.Width = ""; // default value
    
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
    
    var expression16 = container26.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression16.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression16.ColumnSpan = null; // default value
    expression16.CustomStyle = null; // default value
    expression16.Example = null; // default value
    expression16.FlexBasis = null; // default value
    expression16.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression16.RowSpan = null; // default value
    expression16.SetStyle(null); // default value
    expression16.SetValue("FillError");
    expression16.Width = ""; // default value
    
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
    
    var expression17 = confirmFillButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression17.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression17.ColumnSpan = null; // default value
    expression17.CustomStyle = null; // default value
    expression17.Example = null; // default value
    expression17.FlexBasis = null; // default value
    expression17.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression17.RowSpan = null; // default value
    expression17.SetStyle(null); // default value
    expression17.SetValue("\"Fill and confirm\"");
    expression17.Width = ""; // default value
    var builtinEvent20 = confirmFillButton.OnClick;
    builtinEvent20.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent20.Destination = confirmFill;
    builtinEvent20.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
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
    
    var expression18 = cancelFillButton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression18.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression18.ColumnSpan = null; // default value
    expression18.CustomStyle = null; // default value
    expression18.Example = null; // default value
    expression18.FlexBasis = null; // default value
    expression18.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression18.RowSpan = null; // default value
    expression18.SetStyle(null); // default value
    expression18.SetValue("\"Cancel\"");
    expression18.Width = ""; // default value
    var builtinEvent21 = cancelFillButton.OnClick;
    builtinEvent21.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent21.Destination = closeFillPopup;
    builtinEvent21.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
}
