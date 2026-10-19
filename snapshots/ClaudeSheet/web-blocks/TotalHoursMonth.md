# TotalHoursMonth

Block in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'TotalHoursMonth' ***/
    var totalHoursMonth = sheetBlocks.CreateBlock("TotalHoursMonth");
    totalHoursMonth.Description = "Total hours the current user logged in the selected month vs the hours expected, as a progress circle. Reloads when Year, Month or RefreshKey change.";
    totalHoursMonth.HorizontalPosition = 800;
    totalHoursMonth.VerticalPosition = 700;
    
    var getSummary = totalHoursMonth.CreateDataAction("GetSummary");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getSummary.OutputParameters.ToList().ForEach(x => x.Delete());
    
    var startNode = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var calendar_GetMonthSummary = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonthSummary").ConnectedBelow(startNode, 1371);
    var eSpace = sheetBlocks.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var calendar_GetMonthSummary2 = claudeSheet_CS.ServerActions.Named("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.Action = calendar_GetMonthSummary2;
    var month = calendar_GetMonthSummary2.InputParameters.Named("Month");
    calendar_GetMonthSummary.SetArgumentValue(month, "Month");
    var year = calendar_GetMonthSummary2.InputParameters.Named("Year");
    calendar_GetMonthSummary.SetArgumentValue(year, "Year");
    
    var assignNode = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonthSummary, 1371);
    assignNode.Label = "Set summary";
    
    var assignment = assignNode.CreateAssignment("Summary", "Calendar_GetMonthSummary.Summary");
    
    var endNode = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    
    var allExceptions = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.Label = "Keep error";
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var summary = getSummary.CreateOutputParameter("Summary");
    var monthSummary = claudeSheet_CS.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    
    var onAfterFetch_GetSummary = totalHoursMonth.CreateScreenAction("OnAfterFetch_GetSummary");
    onAfterFetch_GetSummary.Description = "Shows an error feedback message when GetSummary failed.";
    
    var startNode2 = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetSummary.ErrorMessage <> \"\"");
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    
    var messageNode = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetSummary.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getSummary.OnAfterFetch.Destination = onAfterFetch_GetSummary;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getSummary.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var year2 = totalHoursMonth.CreateInputParameter("Year");
    year2.DataType = eSpace.IntegerType;
    
    var month2 = totalHoursMonth.CreateInputParameter("Month");
    month2.DataType = eSpace.IntegerType;
    
    var refreshKey = totalHoursMonth.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the block to reload (e.g. after saving time). Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    
    var onParametersChanged = totalHoursMonth.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads GetSummary when Year, Month or RefreshKey change.";
    
    var startNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    totalHoursMonth.OnParametersChanged.Destination = onParametersChanged;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getSummary;
    
    var endNode5 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    
    var totalHours = totalHoursMonth.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("TotalHours");
    totalHours.Gap = null;
    totalHours.SetStyle("\"sheet-total\" + If(GetSummary.IsDataFetched, \"\", \" sheet-total--loading\")");
    
    var container = totalHours.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"sheet-total__circle\"");
    
    var progressTotal = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProgressTotal");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var numbers = outSystemsUI.MobileFlows.Named("Numbers");
    var progressCircle = numbers.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ProgressCircle");
    progressTotal.SourceBlock = progressCircle;
    // delete default content in Content placeholder
    var placeholderContentWidget = progressTotal.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var size = progressCircle.InputParameters.Named("Size");
    progressTotal.SetArgumentValue(size, "\"auto\"");
    var trailColor = progressCircle.InputParameters.Named("TrailColor");
    progressTotal.SetArgumentValue(trailColor, "TextToIdentifier(\"#2A2A2A\")");
    var optionalConfigs = progressCircle.InputParameters.Named("OptionalConfigs");
    var progressColor = progressCircle.InputParameters.Named("ProgressColor");
    progressTotal.SetArgumentValue(progressColor, "TextToIdentifier(\"var(--sheet-confirmed)\")");
    var thickness = progressCircle.InputParameters.Named("Thickness");
    progressTotal.SetArgumentValue(thickness, null);
    var progress = progressCircle.InputParameters.Named("Progress");
    progressTotal.SetArgumentValue(progress, "If(GetSummary.IsDataFetched, DecimalToInteger(Round(GetSummary.Summary.ProgressPercent)), 0)");
    var extendedClass = progressCircle.InputParameters.Named("ExtendedClass");
    var eventHandler = progressTotal.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    
    var container2 = placeholderContentWidget.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"sheet-total__inner\"");
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"sheet-total__label\"");
    
    var expression = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.SetValue("\"Total \" + FormatDateTime(NewDate(Year, Month, 1), \"MMMM\")");
    
    var container4 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"sheet-total__value\"");
    
    var expression2 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.SetValue("If(GetSummary.IsDataFetched, Minutes_Format(GetSummary.Summary.TotalMinutes, \"h\"), \"--\")");
    
    var container5 = totalHours.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"sheet-total__caption\"");
    
    var expression3 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.SetValue("If(GetSummary.IsDataFetched, \"You currently worked \" + Replace(Replace(Replace(Replace(FormatDecimal(GetSummary.Summary.TotalMinutes / 60, 2, \".\", \"\") + \"#\", \"0#\", \"#\"), \"0#\", \"#\"), \".#\", \"\"), \"#\", \"\") + \" hours in \" + GetSummary.Summary.ElapsedDays + \" day(s)\", \"--\")");
    
    var extendedProperty = totalHours.CreateExtendedProperty();
    extendedProperty.Property = "data-refresh-key";
    extendedProperty.SetValue("RefreshKey");
}
```
