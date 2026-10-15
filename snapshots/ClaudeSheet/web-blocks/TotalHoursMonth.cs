(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'TotalHoursMonth' ***/
    var totalHoursMonth = sheetBlocks.CreateBlock("TotalHoursMonth");
    totalHoursMonth.Description = "Total hours the current user logged in the selected month vs the hours expected, as a progress circle. Reloads when Year, Month or RefreshKey change.";
    totalHoursMonth.HorizontalPosition = 800;
    totalHoursMonth.Icon = null; // default value
    totalHoursMonth.IsDisabled = false; // default value
    totalHoursMonth.JavaScript = ""; // default value
    totalHoursMonth.SetOnRenderJavascript(null); // default value
    totalHoursMonth.Public = false; // default value
    totalHoursMonth.Shareable = false; // default value
    totalHoursMonth.StyleSheet = null; // default value
    totalHoursMonth.VerticalPosition = 700;
    
    var getSummary = totalHoursMonth.CreateDataAction("GetSummary");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getSummary.OutputParameters.ToList().ForEach(x => x.Delete());
    getSummary.Description = null; // default value
    getSummary.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getSummary.ServerRequestTimeout = null; // default value
    
    var startNode = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var calendar_GetMonthSummary = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonthSummary").ConnectedBelow(startNode, 1371);
    var eSpace = sheetBlocks.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var calendar_GetMonthSummary2 = claudeSheet_CS.ServerActions.Named("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.Action = calendar_GetMonthSummary2;
    calendar_GetMonthSummary.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonthSummary.IsDisabled = false; // default value
    calendar_GetMonthSummary.ServerRequestTimeout = null; // default value
    var month = calendar_GetMonthSummary2.InputParameters.Named("Month");
    calendar_GetMonthSummary.SetArgumentValue(month, "Month");
    var year = calendar_GetMonthSummary2.InputParameters.Named("Year");
    calendar_GetMonthSummary.SetArgumentValue(year, "Year");
    
    var assignNode = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonthSummary, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Summary", "Calendar_GetMonthSummary.Summary");
    
    var endNode = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var allExceptions = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 6856;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var summary = getSummary.CreateOutputParameter("Summary");
    var monthSummary = claudeSheet_CS.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    summary.Description = null; // default value
    
    var onAfterFetch_GetSummary = totalHoursMonth.CreateScreenAction("OnAfterFetch_GetSummary");
    onAfterFetch_GetSummary.Description = "Shows an error feedback message when GetSummary failed.";
    
    var startNode2 = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetSummary.ErrorMessage <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode3.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("GetSummary.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    getSummary.OnAfterFetch.Destination = onAfterFetch_GetSummary;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getSummary.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var year2 = totalHoursMonth.CreateInputParameter("Year");
    year2.DataType = eSpace.IntegerType;
    year2.Description = null; // default value
    year2.IsMandatory = true; // default value
    
    var month2 = totalHoursMonth.CreateInputParameter("Month");
    month2.DataType = eSpace.IntegerType;
    month2.Description = null; // default value
    month2.IsMandatory = true; // default value
    
    var refreshKey = totalHoursMonth.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the block to reload (e.g. after saving time). Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    totalHoursMonth.OnDestroy.Destination = null; // default value
    totalHoursMonth.OnInitialize.Destination = null; // default value
    totalHoursMonth.OnReady.Destination = null; // default value
    totalHoursMonth.OnRender.Destination = null; // default value
    
    var onParametersChanged = totalHoursMonth.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads GetSummary when Year, Month or RefreshKey change.";
    
    var startNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    totalHoursMonth.OnParametersChanged.Destination = onParametersChanged;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getSummary;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode5 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode5.IsDisabled = false; // default value
    
    var totalHours = totalHoursMonth.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("TotalHours");
    totalHours.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    totalHours.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    totalHours.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    totalHours.Animate = false; // default value
    totalHours.ColumnGap = null; // default value
    totalHours.ColumnSpan = null; // default value
    totalHours.CustomStyle = null; // default value
    totalHours.FlexBasis = null; // default value
    totalHours.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    totalHours.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    totalHours.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    totalHours.Gap = null;
    totalHours.GridColumns = null; // default value
    totalHours.GridRows = null; // default value
    totalHours.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    totalHours.RowGap = null; // default value
    totalHours.RowSpan = null; // default value
    totalHours.SetStyle("\"sheet-total\" + If(GetSummary.IsDataFetched, \"\", \" sheet-total--loading\")");
    totalHours.SetVisible("True"); // default value
    totalHours.VisibleWasExpression = false; // default value
    totalHours.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    totalHours.Width = "(fill parent)"; // default value
    
    var container = totalHours.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle("\"sheet-total__circle\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var progressTotal = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProgressTotal");
    progressTotal.CustomStyle = null; // default value
    progressTotal.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
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
    progressTotal.SetArgumentValue(optionalConfigs, null); // default value
    var progressColor = progressCircle.InputParameters.Named("ProgressColor");
    progressTotal.SetArgumentValue(progressColor, "TextToIdentifier(\"var(--sheet-confirmed)\")");
    var thickness = progressCircle.InputParameters.Named("Thickness");
    progressTotal.SetArgumentValue(thickness, null);
    var progress = progressCircle.InputParameters.Named("Progress");
    progressTotal.SetArgumentValue(progress, "If(GetSummary.IsDataFetched, DecimalToInteger(Round(GetSummary.Summary.ProgressPercent)), 0)");
    var extendedClass = progressCircle.InputParameters.Named("ExtendedClass");
    progressTotal.SetArgumentValue(extendedClass, null); // default value
    var eventHandler = progressTotal.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler.Handler = null; // default value
    
    var container2 = placeholderContentWidget.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"sheet-total__inner\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"sheet-total__label\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var expression = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("\"Total \" + FormatDateTime(NewDate(Year, Month, 1), \"MMMM\")");
    expression.Width = ""; // default value
    
    var container4 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"sheet-total__value\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var expression2 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("If(GetSummary.IsDataFetched, Minutes_Format(GetSummary.Summary.TotalMinutes, \"h\"), \"--\")");
    expression2.Width = ""; // default value
    
    var container5 = totalHours.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"sheet-total__caption\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var expression3 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("If(GetSummary.IsDataFetched, \"You currently worked \" + Replace(Replace(Replace(Replace(FormatDecimal(GetSummary.Summary.TotalMinutes / 60, 2, \".\", \"\") + \"#\", \"0#\", \"#\"), \"0#\", \"#\"), \".#\", \"\"), \"#\", \"\") + \" hours in \" + GetSummary.Summary.ElapsedDays + \" day(s)\", \"--\")");
    expression3.Width = ""; // default value
}
