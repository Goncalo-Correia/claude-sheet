# WeeklyProjectHours

Block in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'WeeklyProjectHours' ***/
    var weeklyProjectHours = sheetBlocks.CreateBlock("WeeklyProjectHours");
    weeklyProjectHours.Description = "Weekly grid of the current user\'s hours per project and per day (Monday-Sunday week containing ReferenceDate), with totals per day, per week and a grand total. Previous/next arrows move one week and raise OnWeekChanged. Reloads when ReferenceDate or RefreshKey change.";
    weeklyProjectHours.HorizontalPosition = 800;
    weeklyProjectHours.VerticalPosition = 1900;
    
    var getWeek = weeklyProjectHours.CreateDataAction("GetWeek");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getWeek.OutputParameters.ToList().ForEach(x => x.Delete());
    getWeek.Description = "The weekly grid for CurrentReferenceDate (ProjectHours_GetWeekly).";
    
    var startNode = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var projectHours_GetWeekly = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectHours_GetWeekly").ConnectedBelow(startNode, 1371);
    var eSpace = sheetBlocks.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var projectHours_GetWeekly2 = claudeSheet_CS.ServerActions.Named("ProjectHours_GetWeekly");
    projectHours_GetWeekly.Action = projectHours_GetWeekly2;
    var referenceDate = projectHours_GetWeekly2.InputParameters.Named("ReferenceDate");
    projectHours_GetWeekly.SetArgumentValue(referenceDate, "CurrentReferenceDate");
    
    var assignNode = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectHours_GetWeekly, 1371);
    assignNode.Label = "Set week";
    
    var assignment = assignNode.CreateAssignment("Week", "ProjectHours_GetWeekly.Week");
    
    var endNode = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var allExceptions = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 5028;
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var week = getWeek.CreateOutputParameter("Week");
    var weeklyHours = claudeSheet_CS.Structures.Named("WeeklyHours");
    week.DataType = weeklyHours;
    
    var onAfterFetch_GetWeek = weeklyProjectHours.CreateScreenAction("OnAfterFetch_GetWeek");
    onAfterFetch_GetWeek.Description = "Shows an error feedback message when GetWeek failed.";
    
    var startNode2 = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetWeek.ErrorMessage <> \"\"");
    ifNode.Label = "Failed?";
    
    var messageNode = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ToTheRightOf(ifNode, 1828);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetWeek.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode3 = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode, 1371);
    
    var endNode4 = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getWeek.OnAfterFetch.Destination = onAfterFetch_GetWeek;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getWeek.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var onWeekChanged = weeklyProjectHours.CreateEvent("OnWeekChanged");
    onWeekChanged.Description = "Raised by the previous/next arrows with the Monday of the new week.";
    onWeekChanged.IsMandatory = false;
    
    var weekStartDate = onWeekChanged.CreateInputParameter("WeekStartDate");
    weekStartDate.DataType = eSpace.DateType;
    
    var referenceDate2 = weeklyProjectHours.CreateInputParameter("ReferenceDate");
    referenceDate2.DataType = eSpace.DateType;
    referenceDate2.SetDefaultValue("CurrDate()");
    referenceDate2.Description = "Any day of the week to show (default: today).";
    referenceDate2.IsMandatory = false;
    
    var refreshKey = weeklyProjectHours.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the block to reload (e.g. after saving a day). Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    
    var currentReferenceDate = weeklyProjectHours.CreateLocalVariable("CurrentReferenceDate");
    currentReferenceDate.DataType = eSpace.DateType;
    currentReferenceDate.Description = "Day of the week being shown; starts from the ReferenceDate input and moves with the arrows.";
    
    var onClick_NextWeek = weeklyProjectHours.CreateScreenAction("OnClick_NextWeek");
    onClick_NextWeek.Description = "Moves the grid one week forward, reloads GetWeek and raises OnWeekChanged with the new Monday.";
    
    var startNode3 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var onInitialize = weeklyProjectHours.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Starts CurrentReferenceDate from the ReferenceDate input (today when empty).";
    
    var startNode4 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.VerticalPosition = 800;
    
    var assignNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode3.Label = "Set reference date";
    
    var assignment3 = assignNode3.CreateAssignment("CurrentReferenceDate", "If(ReferenceDate = NullDate(), CurrDate(), ReferenceDate)");
    
    var endNode5 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    weeklyProjectHours.OnInitialize.Destination = onInitialize;
    
    var onParametersChanged = weeklyProjectHours.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Syncs CurrentReferenceDate with the ReferenceDate input and reloads GetWeek (also when RefreshKey changes).";
    
    var startNode5 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.VerticalPosition = 800;
    
    var assignNode4 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode4.Label = "Set reference date";
    
    var assignment4 = assignNode4.CreateAssignment("CurrentReferenceDate", "If(ReferenceDate = NullDate(), CurrDate(), ReferenceDate)");
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode4, 1371);
    refreshDataNode.DataSource = getWeek;
    
    var endNode6 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    weeklyProjectHours.OnParametersChanged.Destination = onParametersChanged;
    
    var assignNode5 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode5.Label = "Next week";
    
    var assignment5 = assignNode5.CreateAssignment("CurrentReferenceDate", "DateTimeToDate(AddDays(GetWeek.Week.WeekStart, 7))");
    
    var refreshDataNode2 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode5, 1371);
    refreshDataNode2.DataSource = getWeek;
    
    var onWeekChanged2 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnWeekChanged").ConnectedBelow(refreshDataNode2, 1371);
    onWeekChanged2.Event = onWeekChanged;
    onWeekChanged2.SetArgumentValue(weekStartDate, "CurrentReferenceDate");
    
    var endNode7 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onWeekChanged2, 1371);
    
    var onClick_PreviousWeek = weeklyProjectHours.CreateScreenAction("OnClick_PreviousWeek");
    onClick_PreviousWeek.Description = "Moves the grid one week back, reloads GetWeek and raises OnWeekChanged with the new Monday.";
    
    var startNode6 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.VerticalPosition = 800;
    
    var assignNode6 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode6, 1371);
    assignNode6.Label = "Previous week";
    
    var assignment6 = assignNode6.CreateAssignment("CurrentReferenceDate", "DateTimeToDate(AddDays(GetWeek.Week.WeekStart, -7))");
    
    var refreshDataNode3 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode6, 1371);
    refreshDataNode3.DataSource = getWeek;
    
    var onWeekChanged3 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnWeekChanged").ConnectedBelow(refreshDataNode3, 1371);
    onWeekChanged3.Event = onWeekChanged;
    onWeekChanged3.SetArgumentValue(weekStartDate, "CurrentReferenceDate");
    
    var endNode8 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onWeekChanged3, 1371);
    
    var weekGrid = weeklyProjectHours.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("WeekGrid");
    weekGrid.Gap = null;
    weekGrid.SetStyle("\"sheet-week\" + If(GetWeek.IsDataFetched, \"\", \" sheet-week--loading\")");
    
    var weekScroll = weekGrid.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("WeekScroll");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var scrollableArea = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ScrollableArea");
    weekScroll.SourceBlock = scrollableArea;
    var width = scrollableArea.InputParameters.Named("Width");
    var height = scrollableArea.InputParameters.Named("Height");
    var orientation = scrollableArea.InputParameters.Named("Orientation");
    weekScroll.SetArgumentValue(orientation, null);
    var scrollbarStyle = scrollableArea.InputParameters.Named("ScrollbarStyle");
    weekScroll.SetArgumentValue(scrollbarStyle, null);
    var extendedClass = scrollableArea.InputParameters.Named("ExtendedClass");
    weekScroll.SetArgumentValue(extendedClass, "\"sheet-week__scroll\"");
    
    var placeholderContentWidget = weekScroll.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var container = placeholderContentWidget.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"sheet-week__table\"");
    
    var container2 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"sheet-week__row sheet-week__head\"");
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"sheet-week__label\"");
    
    var container4 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"sheet-week__title\"");
    
    var expression = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.SetValue("\"Project daily hours\"");
    
    var previousWeek = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("PreviousWeek");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    previousWeek.Widgets.ToList().ForEach(x => x.Delete());
    previousWeek.SetEnabled("True");
    previousWeek.Gap = null;
    previousWeek.SetStyle("\"sheet-week__arrow\"");
    
    var icon = previousWeek.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.Icon = "chevron-left";
    var builtinEvent = previousWeek.OnClick;
    builtinEvent.Destination = onClick_PreviousWeek;
    
    var extendedProperty = previousWeek.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Previous week\"");
    
    var extendedProperty2 = container3.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"columnheader\"");
    
    var dayHeaders = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("DayHeaders");
    dayHeaders.Gap = null;
    dayHeaders.SetSource("GetWeek.Week.Days");
    dayHeaders.SetStyle("\"sheet-week__contents\"");
    
    var container5 = dayHeaders.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"sheet-week__day\" + If(GetWeek.Week.Days.Current.IsToday, \" is-today\", \"\") + If(GetWeek.Week.Days.Current.IsWeekend, \" is-weekend\", \"\")");
    
    var container6 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"sheet-week__day-name\"");
    
    var expression2 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("GetWeek.Week.Days.Current.DayName");
    
    var container7 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Gap = null;
    container7.SetStyle("\"sheet-week__day-date\"");
    
    var expression3 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("GetWeek.Week.Days.Current.DateLabel");
    
    var extendedProperty3 = container5.CreateExtendedProperty();
    extendedProperty3.Property = "role";
    extendedProperty3.SetValue("\"columnheader\"");
    var builtinEvent2 = dayHeaders.OnScrollEnding;
    
    var container8 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Gap = null;
    container8.SetStyle("\"sheet-week__total-head\"");
    
    var nextWeek = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NextWeek");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    nextWeek.Widgets.ToList().ForEach(x => x.Delete());
    nextWeek.SetEnabled("True");
    nextWeek.Gap = null;
    nextWeek.SetStyle("\"sheet-week__arrow\"");
    
    var icon2 = nextWeek.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.Icon = "chevron-right";
    var builtinEvent3 = nextWeek.OnClick;
    builtinEvent3.Destination = onClick_NextWeek;
    
    var extendedProperty4 = nextWeek.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Next week\"");
    
    var container9 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Gap = null;
    container9.SetStyle("\"sheet-week__total-label\"");
    
    var expression4 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.SetValue("\"Total per week\"");
    
    var extendedProperty5 = container8.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var extendedProperty6 = container2.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"row\"");
    
    var hasRows = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasRows");
    hasRows.SetCondition("GetWeek.Week.Rows.Length > 0 or not GetWeek.IsDataFetched");
    
    var container10 = hasRows.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Gap = null;
    container10.SetStyle("\"sheet-week__body\"");
    
    var projectRows = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectRows");
    projectRows.Gap = null;
    projectRows.SetSource("GetWeek.Week.Rows");
    projectRows.SetStyle("\"sheet-week__rows\"");
    
    var container11 = projectRows.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Gap = null;
    container11.SetStyle("\"sheet-week__row sheet-week__card\"");
    
    var container12 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Gap = null;
    container12.SetStyle("\"sheet-week__label sheet-week__label--project\"");
    
    var container13 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Gap = null;
    container13.SetStyle("\"sheet-week__client\"");
    
    var expression5 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.SetValue("GetWeek.Week.Rows.Current.ClientName");
    
    var container14 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container14.Gap = null;
    container14.SetStyle("\"sheet-week__project\"");
    
    var expression6 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.SetValue("GetWeek.Week.Rows.Current.ProjectName + \" | \" + GetWeek.Week.Rows.Current.ProjectCode");
    
    var extendedProperty7 = container12.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"rowheader\"");
    
    var rowCells = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("RowCells");
    rowCells.Gap = null;
    rowCells.SetSource("GetWeek.Week.Rows.Current.Cells");
    rowCells.SetStyle("\"sheet-week__contents\"");
    
    var container15 = rowCells.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container15.Gap = null;
    container15.SetStyle("\"sheet-week__cell\" + If(DayOfWeek(GetWeek.Week.Rows.Current.Cells.Current.Date) = 0 or DayOfWeek(GetWeek.Week.Rows.Current.Cells.Current.Date) = 6, \" is-weekend\", \"\")");
    
    var expression7 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.SetValue("If(GetWeek.Week.Rows.Current.Cells.Current.ShowDash, \"-\", Minutes_Format(GetWeek.Week.Rows.Current.Cells.Current.Minutes, \":\"))");
    
    var extendedProperty8 = container15.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"cell\"");
    var builtinEvent4 = rowCells.OnScrollEnding;
    
    var container16 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container16.Gap = null;
    container16.SetStyle("\"sheet-week__cell sheet-week__total\"");
    
    var expression8 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.SetValue("Minutes_Format(GetWeek.Week.Rows.Current.TotalMinutes, \":\")");
    
    var extendedProperty9 = container16.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var extendedProperty10 = container11.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"row\"");
    var builtinEvent5 = projectRows.OnScrollEnding;
    
    var container17 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container17.Gap = null;
    container17.SetStyle("\"sheet-week__row sheet-week__card sheet-week__totals\"");
    
    var container18 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container18.Gap = null;
    container18.SetStyle("\"sheet-week__label sheet-week__label--project\"");
    
    var expression9 = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.SetValue("\"Total per day\"");
    
    var extendedProperty11 = container18.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"rowheader\"");
    
    var dayTotals = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("DayTotals");
    dayTotals.Gap = null;
    dayTotals.SetSource("GetWeek.Week.DayTotals");
    dayTotals.SetStyle("\"sheet-week__contents\"");
    
    var container19 = dayTotals.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container19.Gap = null;
    container19.SetStyle("\"sheet-week__cell\" + If(DayOfWeek(GetWeek.Week.DayTotals.Current.Date) = 0 or DayOfWeek(GetWeek.Week.DayTotals.Current.Date) = 6, \" is-weekend\", \"\")");
    
    var expression10 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.SetValue("If(GetWeek.Week.DayTotals.Current.ShowDash, \"-\", Minutes_Format(GetWeek.Week.DayTotals.Current.Minutes, \":\"))");
    
    var extendedProperty12 = container19.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"cell\"");
    var builtinEvent6 = dayTotals.OnScrollEnding;
    
    var container20 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container20.Gap = null;
    container20.SetStyle("\"sheet-week__cell sheet-week__total\"");
    
    var expression11 = container20.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.SetValue("Minutes_Format(GetWeek.Week.GrandTotalMinutes, \":\")");
    
    var extendedProperty13 = container20.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    
    var extendedProperty14 = container17.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"row\"");
    
    var container21 = hasRows.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container21.Gap = null;
    container21.SetStyle("\"sheet-week__empty\"");
    
    var expression12 = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.SetValue("\"No projects assigned for this week\"");
    
    var extendedProperty15 = container21.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"status\"");
    
    var extendedProperty16 = container.CreateExtendedProperty();
    extendedProperty16.Property = "role";
    extendedProperty16.SetValue("\"table\"");
    
    var extendedProperty17 = container.CreateExtendedProperty();
    extendedProperty17.Property = "aria-label";
    extendedProperty17.SetValue("\"Project daily hours, week of \" + FormatDateTime(GetWeek.Week.WeekStart, \"dd/MM/yyyy\")");
    
    var extendedProperty18 = weekGrid.CreateExtendedProperty();
    extendedProperty18.Property = "aria-busy";
    extendedProperty18.SetValue("If(GetWeek.IsDataFetched, \"false\", \"true\")");
    
    var extendedProperty19 = weekGrid.CreateExtendedProperty();
    extendedProperty19.Property = "data-refresh-key";
    extendedProperty19.SetValue("RefreshKey");
}
```
