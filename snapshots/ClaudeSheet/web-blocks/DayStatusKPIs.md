# DayStatusKPIs

Block in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'DayStatusKPIs' ***/
    var dayStatusKPIs = sheetBlocks.CreateBlock("DayStatusKPIs");
    dayStatusKPIs.Description = "Monthly KPI cards (Confirmed, Days off, Holidays, Pending) for the current user, built from CalendarDayType where ShowInKPI = True ordered by Order. The coloured squares double as the calendar legend. Reloads when Year, Month or RefreshKey change.";
    dayStatusKPIs.HorizontalPosition = 800;
    dayStatusKPIs.VerticalPosition = 1300;
    
    var getKPIs = dayStatusKPIs.CreateDataAction("GetKPIs");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getKPIs.OutputParameters.ToList().ForEach(x => x.Delete());
    getKPIs.Description = "Counts per KPI day type for the selected month (from GetMonthSummary), one record per CalendarDayType with ShowInKPI = True, ordered by Order.";
    
    var kPI = getKPIs.CreateLocalVariable("KPI");
    var eSpace = sheetBlocks.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var dayStatusKPI = claudeSheet_CS.Structures.Named("DayStatusKPI");
    kPI.DataType = dayStatusKPI;
    
    var startNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var calendar_GetMonthSummary = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonthSummary").ConnectedBelow(startNode, 1371);
    var calendar_GetMonthSummary2 = claudeSheet_CS.ServerActions.Named("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.Action = calendar_GetMonthSummary2;
    var month = calendar_GetMonthSummary2.InputParameters.Named("Month");
    calendar_GetMonthSummary.SetArgumentValue(month, "Month");
    var year = calendar_GetMonthSummary2.InputParameters.Named("Year");
    calendar_GetMonthSummary.SetArgumentValue(year, "Year");
    
    var getKPIDayTypes = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetKPIDayTypes").ConnectedBelow(calendar_GetMonthSummary, 1371);
    var calendarDayType2 = claudeSheet_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("CalendarDayType");
    var calendarDayType = getKPIDayTypes.AsDatabaseAggregate.CreateSource(calendarDayType2);
    getKPIDayTypes.AsDatabaseAggregate.CreateFilter("CalendarDayType.ShowInKPI = True");
    getKPIDayTypes.AsDatabaseAggregate.CreateSort().SetAttribute("CalendarDayType.Order");
    
    
    var forEachNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getKPIDayTypes, 1371);
    forEachNode.Label = "Each day type";
    forEachNode.SetRecordList("GetKPIDayTypes.List");
    
    var assignNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode.Label = "Build KPI";
    
    var assignment = assignNode.CreateAssignment("KPI.CalendarDayTypeId", "GetKPIDayTypes.List.Current.CalendarDayType.Id");
    
    var assignment2 = assignNode.CreateAssignment("KPI.CssClass", "GetKPIDayTypes.List.Current.CalendarDayType.CssClass");
    
    var assignment3 = assignNode.CreateAssignment("KPI.Count", "If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.Confirmed, Calendar_GetMonthSummary.Summary.ConfirmedCount, If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.DayOff, Calendar_GetMonthSummary.Summary.DayOffCount, If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.Holiday, Calendar_GetMonthSummary.Summary.HolidayCount, If(GetKPIDayTypes.List.Current.CalendarDayType.Id = Entities.CalendarDayType.Pending, Calendar_GetMonthSummary.Summary.PendingCount, 0))))");
    
    var assignment4 = assignNode.CreateAssignment("KPI.Label", "If(KPI.Count = 1, GetKPIDayTypes.List.Current.CalendarDayType.Label, GetKPIDayTypes.List.Current.CalendarDayType.PluralLabel)");
    forEachNode.CycleTarget = assignNode;
    
    var endNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 1371);
    forEachNode.Target = endNode;
    
    var listAppend = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.Target = forEachNode;
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "KPIs");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "KPI");
    
    var allExceptions = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 8684;
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.Label = "Keep error";
    
    var assignment5 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var kPIs = getKPIs.CreateOutputParameter("KPIs");
    kPIs.DataType = eSpace.GetOrCreateListType(dayStatusKPI);
    
    var onAfterFetch_GetKPIs = dayStatusKPIs.CreateScreenAction("OnAfterFetch_GetKPIs");
    onAfterFetch_GetKPIs.Description = "Shows an error feedback message when GetKPIs failed.";
    
    var startNode2 = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetKPIs.ErrorMessage <> \"\"");
    ifNode.Label = "Failed?";
    
    var endNode3 = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    
    var messageNode = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().Below(ifNode, 1371);
    messageNode.Label = "Show error";
    messageNode.SetMessage("GetKPIs.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = onAfterFetch_GetKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    getKPIs.OnAfterFetch.Destination = onAfterFetch_GetKPIs;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getKPIs.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    
    var year2 = dayStatusKPIs.CreateInputParameter("Year");
    year2.DataType = eSpace.IntegerType;
    
    var month2 = dayStatusKPIs.CreateInputParameter("Month");
    month2.DataType = eSpace.IntegerType;
    
    var refreshKey = dayStatusKPIs.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the block to reload (e.g. after saving a day). Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    
    var onParametersChanged = dayStatusKPIs.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Reloads GetKPIs when Year, Month or RefreshKey change.";
    
    var startNode3 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    dayStatusKPIs.OnParametersChanged.Destination = onParametersChanged;
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(startNode3, 1371);
    refreshDataNode.DataSource = getKPIs;
    
    var endNode5 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    
    var kPICards = dayStatusKPIs.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPICards");
    kPICards.Gap = null;
    kPICards.SetStyle("\"sheet-kpis\" + If(GetKPIs.IsDataFetched, \"\", \" sheet-kpis--loading\")");
    
    var isLoaded = kPICards.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsLoaded");
    isLoaded.SetCondition("GetKPIs.IsDataFetched");
    
    var kPIList = isLoaded.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("KPIList");
    kPIList.Gap = null;
    kPIList.SetSource("GetKPIs.KPIs");
    kPIList.SetStyle("\"sheet-kpis__list\"");
    
    var kPICard = kPIList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPICard");
    kPICard.Gap = null;
    kPICard.SetStyle("\"sheet-kpis__card\"");
    
    var container = kPICard.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"sheet-kpis__square \" + GetKPIs.KPIs.Current.CssClass");
    
    var extendedProperty = container.CreateExtendedProperty();
    extendedProperty.Property = "aria-hidden";
    extendedProperty.SetValue("\"true\"");
    
    var container2 = kPICard.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"sheet-kpis__text\"");
    
    var expression = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.Example = "7 Confirmed";
    expression.SetValue("GetKPIs.KPIs.Current.Count + \" \" + GetKPIs.KPIs.Current.Label");
    
    var extendedProperty2 = kPICard.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"group\"");
    
    var extendedProperty3 = kPICard.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("GetKPIs.KPIs.Current.Count + \" \" + If(GetKPIs.KPIs.Current.CalendarDayTypeId = Entities.CalendarDayType.Holiday, If(GetKPIs.KPIs.Current.Count = 1, \"holiday\", \"holidays\"), If(GetKPIs.KPIs.Current.Count = 1, \"day\", \"days\") + \" \" + If(GetKPIs.KPIs.Current.CalendarDayTypeId = Entities.CalendarDayType.DayOff, \"off\", ToLower(GetKPIs.KPIs.Current.Label))) + \" in \" + FormatDateTime(NewDate(Year, Month, 1), \"MMMM yyyy\")");
    var builtinEvent = kPIList.OnScrollEnding;
    
    var kPISkeleton = isLoaded.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPISkeleton");
    kPISkeleton.Gap = null;
    kPISkeleton.SetStyle("\"sheet-kpis__list\"");
    
    var container3 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Gap = null;
    container3.SetStyle("\"sheet-kpis__card is-skeleton\"");
    
    var container4 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"sheet-kpis__square\"");
    
    var container5 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    container5.SetStyle("\"sheet-kpis__text\"");
    
    var container6 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"sheet-kpis__card is-skeleton\"");
    
    var container7 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Gap = null;
    container7.SetStyle("\"sheet-kpis__square\"");
    
    var container8 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Gap = null;
    container8.SetStyle("\"sheet-kpis__text\"");
    
    var container9 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Gap = null;
    container9.SetStyle("\"sheet-kpis__card is-skeleton\"");
    
    var container10 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Gap = null;
    container10.SetStyle("\"sheet-kpis__square\"");
    
    var container11 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Gap = null;
    container11.SetStyle("\"sheet-kpis__text\"");
    
    var container12 = kPISkeleton.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Gap = null;
    container12.SetStyle("\"sheet-kpis__card is-skeleton\"");
    
    var container13 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Gap = null;
    container13.SetStyle("\"sheet-kpis__square\"");
    
    var container14 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container14.Gap = null;
    container14.SetStyle("\"sheet-kpis__text\"");
    
    var extendedProperty4 = kPISkeleton.CreateExtendedProperty();
    extendedProperty4.Property = "aria-hidden";
    extendedProperty4.SetValue("\"true\"");
    
    var extendedProperty5 = kPICards.CreateExtendedProperty();
    extendedProperty5.Property = "aria-busy";
    extendedProperty5.SetValue("If(GetKPIs.IsDataFetched, \"false\", \"true\")");
    
    var extendedProperty6 = kPICards.CreateExtendedProperty();
    extendedProperty6.Property = "data-refresh-key";
    extendedProperty6.SetValue("RefreshKey");
}
```
