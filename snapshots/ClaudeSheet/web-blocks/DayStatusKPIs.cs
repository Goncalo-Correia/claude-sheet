(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'DayStatusKPIs' ***/
    var dayStatusKPIs = sheetBlocks.CreateBlock("DayStatusKPIs");
    dayStatusKPIs.Description = "Monthly KPI cards (Confirmed, Days off, Holidays, Pending) for the current user, built from CalendarDayType where ShowInKPI = True ordered by Order. The coloured squares double as the calendar legend. Reloads when Year, Month or RefreshKey change.";
    dayStatusKPIs.HorizontalPosition = 800;
    dayStatusKPIs.Icon = null; // default value
    dayStatusKPIs.IsDisabled = false; // default value
    dayStatusKPIs.JavaScript = ""; // default value
    dayStatusKPIs.SetOnRenderJavascript(null); // default value
    dayStatusKPIs.Public = false; // default value
    dayStatusKPIs.Shareable = false; // default value
    dayStatusKPIs.StyleSheet = null; // default value
    dayStatusKPIs.VerticalPosition = 1300;
    
    var getKPIs = dayStatusKPIs.CreateDataAction("GetKPIs");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getKPIs.OutputParameters.ToList().ForEach(x => x.Delete());
    getKPIs.Description = "Counts per KPI day type for the selected month (from GetMonthSummary), one record per CalendarDayType with ShowInKPI = True, ordered by Order.";
    getKPIs.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getKPIs.ServerRequestTimeout = null; // default value
    
    var kPI = getKPIs.CreateLocalVariable("KPI");
    var eSpace = sheetBlocks.GetESpace();
    var dayStatusKPI = eSpace.Structures.Named("DayStatusKPI");
    kPI.DataType = dayStatusKPI;
    kPI.Description = null; // default value
    
    var startNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var calendar_GetMonthSummary = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonthSummary").ConnectedBelow(startNode, 1371);
    var calendar_GetMonthSummary2 = eSpace.ServerActions.Named("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.Action = calendar_GetMonthSummary2;
    calendar_GetMonthSummary.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonthSummary.IsDisabled = false; // default value
    calendar_GetMonthSummary.ServerRequestTimeout = null; // default value
    var month = calendar_GetMonthSummary2.InputParameters.Named("Month");
    calendar_GetMonthSummary.SetArgumentValue(month, "Month");
    var year = calendar_GetMonthSummary2.InputParameters.Named("Year");
    calendar_GetMonthSummary.SetArgumentValue(year, "Year");
    
    var getKPIDayTypes = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetKPIDayTypes").ConnectedBelow(calendar_GetMonthSummary, 1371);
    getKPIDayTypes.CacheInMinutes = null; // default value
    getKPIDayTypes.Description = null; // default value
    getKPIDayTypes.IsDisabled = false; // default value
    getKPIDayTypes.SetMaxRecords(null); // default value
    getKPIDayTypes.SetStartIndex(null); // default value
    getKPIDayTypes.Timeout = null; // default value
    var calendarDayType2 = eSpace.Entities.OfType<OutSystems.Model.Data.IStaticEntity>().Named("CalendarDayType");
    var calendarDayType = getKPIDayTypes.AsDatabaseAggregate.CreateSource(calendarDayType2);
    getKPIDayTypes.AsDatabaseAggregate.CreateFilter("CalendarDayType.ShowInKPI = True");
    getKPIDayTypes.AsDatabaseAggregate.CreateSort().SetAttribute("CalendarDayType.Order");
    
    
    var forEachNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getKPIDayTypes, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetKPIDayTypes.List");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Build KPI";
    
    var assignment = assignNode.CreateAssignment("KPI.CalendarDayTypeId", "GetKPIDayTypes.List.Current.CalendarDayType.Id");
    
    var assignment2 = assignNode.CreateAssignment("KPI.CssClass", "GetKPIDayTypes.List.Current.CalendarDayType.CssClass");
    
    var assignment3 = assignNode.CreateAssignment("KPI.Count", "If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.Confirmed, Calendar_GetMonthSummary.Summary.ConfirmedCount, If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.DayOff, Calendar_GetMonthSummary.Summary.DayOffCount, If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.Holiday, Calendar_GetMonthSummary.Summary.HolidayCount, If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.Pending, Calendar_GetMonthSummary.Summary.PendingCount, 0))))");
    
    var assignment4 = assignNode.CreateAssignment("KPI.Label", "If(KPI.Count = 1, GetKPIDayTypes.List.Current.CalendarDayType.Label, GetKPIDayTypes.List.Current.CalendarDayType.PluralLabel)");
    forEachNode.CycleTarget = assignNode;
    
    var endNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 1371);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var listAppend = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    listAppend.Target = forEachNode;
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "KPIs");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "KPI");
    
    var allExceptions = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 8684;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment5 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var kPIs = getKPIs.CreateOutputParameter("KPIs");
    kPIs.DataType = eSpace.GetOrCreateListType(dayStatusKPI);
    kPIs.Description = null; // default value
    
    var onAfterFetch_GetKPIs = dayStatusKPIs.CreateScreenAction("OnAfterFetch_GetKPIs");
    onAfterFetch_GetKPIs.Description = "Shows an error feedback message when GetKPIs failed.";
    
    var startNode2 = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetKPIs.ErrorMessage <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode3.IsDisabled = false; // default value
    
    var messageNode = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("GetKPIs.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    getKPIs.OnAfterFetch.Destination = onAfterFetch_GetKPIs;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getKPIs.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var year2 = dayStatusKPIs.CreateInputParameter("Year");
    year2.DataType = eSpace.IntegerType;
    year2.Description = null; // default value
    year2.IsMandatory = true; // default value
    
    var month2 = dayStatusKPIs.CreateInputParameter("Month");
    month2.DataType = eSpace.IntegerType;
    month2.Description = null; // default value
    month2.IsMandatory = true; // default value
    
    var refreshKey = dayStatusKPIs.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the block to reload (e.g. after saving a day). Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    dayStatusKPIs.OnDestroy.Destination = null; // default value
    dayStatusKPIs.OnInitialize.Destination = null; // default value
    dayStatusKPIs.OnReady.Destination = null; // default value
    dayStatusKPIs.OnRender.Destination = null; // default value
    
    var onParametersChanged = dayStatusKPIs.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads GetKPIs when Year, Month or RefreshKey change.";
    
    var startNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    dayStatusKPIs.OnParametersChanged.Destination = onParametersChanged;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getKPIs;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode5 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode5.IsDisabled = false; // default value
    
    var kPICards = dayStatusKPIs.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPICards");
    kPICards.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    kPICards.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    kPICards.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    kPICards.Animate = false; // default value
    kPICards.ColumnGap = null; // default value
    kPICards.ColumnSpan = null; // default value
    kPICards.CustomStyle = null; // default value
    kPICards.FlexBasis = null; // default value
    kPICards.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    kPICards.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    kPICards.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    kPICards.Gap = null;
    kPICards.GridColumns = null; // default value
    kPICards.GridRows = null; // default value
    kPICards.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    kPICards.RowGap = null; // default value
    kPICards.RowSpan = null; // default value
    kPICards.SetStyle("\"sheet-kpis\" + If(GetKPIs.IsDataFetched, \"\", \" sheet-kpis--loading\")");
    kPICards.SetVisible("True"); // default value
    kPICards.VisibleWasExpression = false; // default value
    kPICards.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    kPICards.Width = "(fill parent)"; // default value
    
    var isLoaded = kPICards.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsLoaded");
    isLoaded.Animate = false; // default value
    isLoaded.SetCondition("GetKPIs.IsDataFetched");
    isLoaded.CustomStyle = null; // default value
    isLoaded.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var kPIList = isLoaded.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("KPIList");
    kPIList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    kPIList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    kPIList.AnimateItems = true; // default value
    kPIList.ColumnGap = null; // default value
    kPIList.ColumnSpan = null; // default value
    kPIList.CustomStyle = null; // default value
    kPIList.ExpandedInWebEditor = true; // default value
    kPIList.FlexBasis = null; // default value
    kPIList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    kPIList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    kPIList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    kPIList.Gap = null;
    kPIList.GridColumns = null; // default value
    kPIList.GridRows = null; // default value
    kPIList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    kPIList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    kPIList.RowGap = null; // default value
    kPIList.RowSpan = null; // default value
    kPIList.SetSource("GetKPIs.KPIs");
    kPIList.SetStyle("\"sheet-kpis__list\"");
    kPIList.Tag = "div"; // default value
    kPIList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    kPIList.Width = "(fill parent)"; // default value
    
    var kPICard = kPIList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPICard");
    kPICard.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    kPICard.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    kPICard.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    kPICard.Animate = false; // default value
    kPICard.ColumnGap = null; // default value
    kPICard.ColumnSpan = null; // default value
    kPICard.CustomStyle = null; // default value
    kPICard.FlexBasis = null; // default value
    kPICard.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    kPICard.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    kPICard.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    kPICard.Gap = null;
    kPICard.GridColumns = null; // default value
    kPICard.GridRows = null; // default value
    kPICard.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    kPICard.RowGap = null; // default value
    kPICard.RowSpan = null; // default value
    kPICard.SetStyle("\"sheet-kpis__card\"");
    kPICard.SetVisible("True"); // default value
    kPICard.VisibleWasExpression = false; // default value
    kPICard.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    kPICard.Width = "(fill parent)"; // default value
    
    var container = kPICard.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle("\"sheet-kpis__square \" + GetKPIs.KPIs.Current.CssClass");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var extendedProperty = container.CreateExtendedProperty();
    extendedProperty.Property = "aria-hidden";
    extendedProperty.SetValue("\"true\"");
    
    var container2 = kPICard.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"sheet-kpis__text\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var expression = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = "7 Confirmed";
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetKPIs.KPIs.Current.Count + \" \" + GetKPIs.KPIs.Current.Label");
    expression.Width = ""; // default value
    
    var extendedProperty2 = kPICard.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"group\"");
    
    var extendedProperty3 = kPICard.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("GetKPIs.KPIs.Current.Count + \" \" + If(GetKPIs.KPIs.Current.CalendarDayTypeId = Entities.CalendarDayType.Holiday, If(GetKPIs.KPIs.Current.Count = 1, \"holiday\", \"holidays\"), If(GetKPIs.KPIs.Current.Count = 1, \"day\", \"days\") + \" \" + If(GetKPIs.KPIs.Current.CalendarDayTypeId = Entities.CalendarDayType.DayOff, \"off\", ToLower(GetKPIs.KPIs.Current.Label))) + \" in \" + FormatDateTime(NewDate(Year, Month, 1), \"MMMM yyyy\")");
    var builtinEvent = kPIList.OnScrollEnding;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = null; // default value
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var kPISkeleton = isLoaded.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPISkeleton");
    kPISkeleton.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    kPISkeleton.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    kPISkeleton.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    kPISkeleton.Animate = false; // default value
    kPISkeleton.ColumnGap = null; // default value
    kPISkeleton.ColumnSpan = null; // default value
    kPISkeleton.CustomStyle = null; // default value
    kPISkeleton.FlexBasis = null; // default value
    kPISkeleton.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    kPISkeleton.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    kPISkeleton.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    kPISkeleton.Gap = null;
    kPISkeleton.GridColumns = null; // default value
    kPISkeleton.GridRows = null; // default value
    kPISkeleton.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    kPISkeleton.RowGap = null; // default value
    kPISkeleton.RowSpan = null; // default value
    kPISkeleton.SetStyle("\"sheet-kpis__list\"");
    kPISkeleton.SetVisible("True"); // default value
    kPISkeleton.VisibleWasExpression = false; // default value
    kPISkeleton.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    kPISkeleton.Width = "(fill parent)"; // default value
    
    var container3 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"sheet-kpis__card is-skeleton\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var container4 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"sheet-kpis__square\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var container5 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"sheet-kpis__text\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var container6 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"sheet-kpis__card is-skeleton\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var container7 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container7.SetStyle("\"sheet-kpis__square\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var container8 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container8.SetStyle("\"sheet-kpis__text\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var container9 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container9.SetStyle("\"sheet-kpis__card is-skeleton\"");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var container10 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container10.SetStyle("\"sheet-kpis__square\"");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var container11 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container11.SetStyle("\"sheet-kpis__text\"");
    container11.SetVisible("True"); // default value
    container11.VisibleWasExpression = false; // default value
    container11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container11.Width = "(fill parent)"; // default value
    
    var container12 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container12.SetStyle("\"sheet-kpis__card is-skeleton\"");
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var container13 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container13.SetStyle("\"sheet-kpis__square\"");
    container13.SetVisible("True"); // default value
    container13.VisibleWasExpression = false; // default value
    container13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container13.Width = "(fill parent)"; // default value
    
    var container14 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container14.SetStyle("\"sheet-kpis__text\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "(fill parent)"; // default value
    
    var extendedProperty4 = kPISkeleton.CreateExtendedProperty();
    extendedProperty4.Property = "aria-hidden";
    extendedProperty4.SetValue("\"true\"");
    
    var extendedProperty5 = kPICards.CreateExtendedProperty();
    extendedProperty5.Property = "aria-busy";
    extendedProperty5.SetValue("If(GetKPIs.IsDataFetched, \"false\", \"true\")");
}
