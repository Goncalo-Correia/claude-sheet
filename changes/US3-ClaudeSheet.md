# US3 · ClaudeSheet changes

Every `applyModelApiCode` save for US3 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-15 14:15:29 UTC

- **Published:** 2026-10-15 14:34:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // US3 decision: PluralLabel of Confirmed / Pending must not change ("7 Confirmed", "0 Pending")
    var cdt = (OutSystems.Model.Data.IStaticEntity)eSpace.Entities.Named("CalendarDayType");
    var pluralLabel = cdt.Attributes.Named("PluralLabel");
    cdt.Records.Single(r => r.Identifier == "Confirmed").SetAttributeValue(pluralLabel, "\"Confirmed\"");
    cdt.Records.Single(r => r.Identifier == "Pending").SetAttributeValue(pluralLabel, "\"Pending\"");

    // Structure DayStatusKPI
    var kpiStruct = eSpace.CreateStructure("DayStatusKPI");
    kpiStruct.Description = "One KPI card of the DayStatusKPIs block: a CalendarDayType with its count in the selected month and the label already in singular/plural.";
    var sId = kpiStruct.CreateAttribute("CalendarDayTypeId");
    sId.DataType = cdt.IdentifierType;
    sId.Label = "Calendar Day Type";
    var sLabel = kpiStruct.CreateAttribute("Label");
    sLabel.DataType = eSpace.TextType;
    sLabel.Length = 50;
    var sCss = kpiStruct.CreateAttribute("CssClass");
    sCss.DataType = eSpace.TextType;
    sCss.Length = 50;
    var sCount = kpiStruct.CreateAttribute("Count");
    sCount.DataType = eSpace.IntegerType;

    // Block
    var flow = eSpace.MobileFlows.Named("SheetBlocks");
    var block = flow.CreateBlock("DayStatusKPIs");
    block.Description = "Monthly KPI cards (Confirmed, Days off, Holidays, Pending) for the current user, built from CalendarDayType where ShowInKPI = True ordered by Order. The coloured squares double as the calendar legend. Reloads when Year, Month or RefreshKey change.";
    block.HorizontalPosition = 800;
    block.VerticalPosition = 1300;

    var pYear = block.CreateInputParameter("Year");
    pYear.DataType = eSpace.IntegerType;
    pYear.IsMandatory = true;
    var pMonth = block.CreateInputParameter("Month");
    pMonth.DataType = eSpace.IntegerType;
    pMonth.IsMandatory = true;
    var pRefresh = block.CreateInputParameter("RefreshKey");
    pRefresh.DataType = eSpace.IntegerType;
    pRefresh.SetDefaultValue("0");
    pRefresh.Description = "Change this value to force the block to reload (e.g. after saving a day). Only used to trigger OnParametersChanged.";
    pRefresh.IsMandatory = false;

    // Data action GetKPIs
    var getKPIs = block.CreateDataAction("GetKPIs");
    getKPIs.OutputParameters.ToList().ForEach(x => x.Delete());
    getKPIs.Description = "Counts per KPI day type for the selected month (from GetMonthSummary), one record per CalendarDayType with ShowInKPI = True, ordered by Order.";

    var kpi = getKPIs.CreateLocalVariable("KPI");
    kpi.DataType = kpiStruct;

    var startNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;

    var getMonthSummary = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetMonthSummary").ConnectedBelow(startNode, 1371);
    var gms = eSpace.ServerActions.Named("GetMonthSummary");
    getMonthSummary.Action = gms;
    getMonthSummary.SetArgumentValue(gms.InputParameters.Named("Year"), "Year");
    getMonthSummary.SetArgumentValue(gms.InputParameters.Named("Month"), "Month");

    var agg = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetKPIDayTypes").ConnectedBelow(getMonthSummary, 1371);
    agg.AsDatabaseAggregate.CreateSource(cdt);
    agg.AsDatabaseAggregate.CreateFilter("CalendarDayType.ShowInKPI = True");
    agg.AsDatabaseAggregate.CreateSort().SetAttribute("CalendarDayType.Order");
    var aggName = agg.Name;

    var forEach = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(agg, 1371);
    forEach.SetRecordList(aggName + ".List");

    var cur = aggName + ".List.Current.CalendarDayType";
    var assignKpi = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEach, 1828);
    assignKpi.Label = "Build KPI";
    assignKpi.CreateAssignment("KPI.CalendarDayTypeId", cur + ".Id");
    assignKpi.CreateAssignment("KPI.CssClass", cur + ".CssClass");
    assignKpi.CreateAssignment("KPI.Count",
        "If(" + cur + ".Id = Entities.CalendarDayType.Confirmed, GetMonthSummary.Summary.ConfirmedCount, " +
        "If(" + cur + ".Id = Entities.CalendarDayType.DayOff, GetMonthSummary.Summary.DayOffCount, " +
        "If(" + cur + ".Id = Entities.CalendarDayType.Holiday, GetMonthSummary.Summary.HolidayCount, " +
        "If(" + cur + ".Id = Entities.CalendarDayType.Pending, GetMonthSummary.Summary.PendingCount, 0))))");
    assignKpi.CreateAssignment("KPI.Label", "If(KPI.Count = 1, " + cur + ".Label, " + cur + ".PluralLabel)");
    forEach.CycleTarget = assignKpi;

    var sys = eSpace.References.Named("(System)");
    var listAppend = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").Below(assignKpi, 1371);
    var la = sys.ServerActions.Named("ListAppend");
    listAppend.Action = la;
    listAppend.SetArgumentValue(la.InputParameters.Named("List"), "KPIs");
    listAppend.SetArgumentValue(la.InputParameters.Named("Element"), "KPI");
    assignKpi.Target = listAppend;
    listAppend.Target = forEach;

    var endNode = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEach, 1371);
    forEach.Target = endNode;

    var allEx = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allEx.AbortTransaction = false;
    allEx.Exception = eSpace.AllExceptions;
    allEx.HorizontalPosition = 8684;
    allEx.VerticalPosition = 800;
    var assignErr = getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allEx, 1371);
    assignErr.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    getKPIs.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignErr, 1371);

    var outKpis = getKPIs.CreateOutputParameter("KPIs");
    outKpis.DataType = eSpace.GetOrCreateListType(kpiStruct);
    var outErr = getKPIs.CreateOutputParameter("ErrorMessage");
    outErr.DataType = eSpace.TextType;

    // OnAfterFetch: error feedback
    var afterFetch = block.CreateScreenAction("GetKPIsOnAfterFetch");
    afterFetch.Description = "Shows an error feedback message when GetKPIs failed.";
    var st2 = afterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    st2.HorizontalPosition = 3200;
    st2.VerticalPosition = 800;
    var ifFail = afterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(st2, 1371);
    ifFail.SetCondition("GetKPIs.ErrorMessage <> \"\"");
    ifFail.Label = "Failed?";
    var msg = afterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ToTheRightOf(ifFail, 1828);
    msg.SetMessage("GetKPIs.ErrorMessage");
    msg.Type = OutSystems.Model.Enumerations.MessageType.Error;
    var end3 = afterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifFail, 1371);
    ifFail.TrueTarget = msg;
    ifFail.FalseTarget = end3;
    msg.Target = end3;
    getKPIs.OnAfterFetch.Destination = afterFetch;

    // OnParametersChanged
    var opc = block.CreateScreenAction("OnParametersChanged");
    opc.Description = "Reloads GetKPIs when Year, Month or RefreshKey change.";
    var st3 = opc.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    st3.HorizontalPosition = 3200;
    st3.VerticalPosition = 800;
    block.OnParametersChanged.Destination = opc;
    var refresh = opc.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(st3, 1371);
    refresh.DataSource = getKPIs;
    opc.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refresh, 1371);
}
```

## Save 2 · 2026-10-15 14:16:03 UTC

- **Published:** 2026-10-15 14:34:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    var flow = eSpace.MobileFlows.Named("SheetBlocks");
    var block = flow.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Single(b => b.Name == "DayStatusKPIs");

    var root = block.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPICards");
    root.SetStyle("\"sheet-kpis\" + If(GetKPIs.IsDataFetched, \"\", \" sheet-kpis--loading\")");
    var rootBusy = root.CreateExtendedProperty();
    rootBusy.Property = "aria-busy";
    rootBusy.SetValue("If(GetKPIs.IsDataFetched, \"false\", \"true\")");

    var isLoaded = root.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsLoaded");
    isLoaded.SetCondition("GetKPIs.IsDataFetched");

    // Loaded: one card per KPI
    var list = isLoaded.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("KPIList");
    list.SetSource("GetKPIs.KPIs");
    list.SetStyle("\"sheet-kpis__list\"");
    list.Tag = "div";

    var card = list.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPICard");
    card.SetStyle("\"sheet-kpis__card\"");
    var cardRole = card.CreateExtendedProperty();
    cardRole.Property = "role";
    cardRole.SetValue("\"group\"");
    var cardAria = card.CreateExtendedProperty();
    cardAria.Property = "aria-label";
    cardAria.SetValue(
        "GetKPIs.KPIs.Current.Count + \" \" + " +
        "If(GetKPIs.KPIs.Current.CalendarDayTypeId = Entities.CalendarDayType.Holiday, If(GetKPIs.KPIs.Current.Count = 1, \"holiday\", \"holidays\"), " +
        "If(GetKPIs.KPIs.Current.Count = 1, \"day\", \"days\") + \" \" + If(GetKPIs.KPIs.Current.CalendarDayTypeId = Entities.CalendarDayType.DayOff, \"off\", ToLower(GetKPIs.KPIs.Current.Label))) + " +
        "\" in \" + FormatDateTime(NewDate(Year, Month, 1), \"MMMM yyyy\")");

    var square = card.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    square.SetStyle("\"sheet-kpis__square \" + GetKPIs.KPIs.Current.CssClass");
    var squareHidden = square.CreateExtendedProperty();
    squareHidden.Property = "aria-hidden";
    squareHidden.SetValue("\"true\"");

    var text = card.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    text.SetStyle("\"sheet-kpis__text\"");
    var textExpr = text.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    textExpr.SetValue("GetKPIs.KPIs.Current.Count + \" \" + GetKPIs.KPIs.Current.Label");
    textExpr.Example = "7 Confirmed";

    // Loading: 4 skeleton cards
    var skel = isLoaded.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPISkeleton");
    skel.SetStyle("\"sheet-kpis__list\"");
    var skelHidden = skel.CreateExtendedProperty();
    skelHidden.Property = "aria-hidden";
    skelHidden.SetValue("\"true\"");
    for (var i = 0; i < 4; i++) {
        var sc = skel.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
        sc.SetStyle("\"sheet-kpis__card is-skeleton\"");
        var ss = sc.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
        ss.SetStyle("\"sheet-kpis__square\"");
        var st = sc.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
        st.SetStyle("\"sheet-kpis__text\"");
    }
}
```

## Save 3 · 2026-10-15 14:16:33 UTC

- **Published:** 2026-10-15 14:34:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved with 1 validation error(s)
- **Imports:** -
- **Error:** /ClaudeSheet - Invalid Icon Library (expected sidecar error on theme stylesheet edits; Service Studio validation clean)

```csharp
eSpace => {
    var theme = eSpace.MobileThemes.Named("ClaudeSheet");
    if (theme.StyleSheet.Contains(".sheet-kpis")) throw new Exception("sheet-kpis CSS already present");
    var css =
        "\n.sheet-kpis-start { }\n\n" +
        "/* MySheet - DayStatusKPIs block (US3). Status colours match .sheet-cal */\n" +
        ".sheet-kpis { --sheet-dayoff: #2f7fd8; --sheet-holiday: #8a4fd1; width: 100%; }\n" +
        ".sheet-kpis__list { display: flex; flex-direction: column; gap: 16px; }\n" +
        ".sheet-kpis__card { display: flex; align-items: center; gap: 12px; min-width: 0; padding: 16px; border-radius: 12px; background-color: var(--sheet-surface-dark); color: #FFFFFF; font-size: 16px; font-weight: 600; line-height: 1.2; }\n" +
        ".sheet-kpis__square { flex: 0 0 24px; width: 24px; height: 24px; border-radius: 6px; }\n" +
        ".sheet-kpis__text { min-width: 0; overflow: hidden; text-overflow: ellipsis; white-space: nowrap; }\n" +
        ".sheet-kpis .day-confirmed { background-color: var(--sheet-confirmed); border: 0; }\n" +
        ".sheet-kpis .day-off { background-color: var(--sheet-dayoff); border: 0; }\n" +
        ".sheet-kpis .day-holiday { background-color: var(--sheet-holiday); border: 0; }\n" +
        ".sheet-kpis .day-pending { background-color: var(--sheet-pending); border: 0; }\n" +
        ".sheet-kpis__card.is-skeleton .sheet-kpis__square, .sheet-kpis__card.is-skeleton .sheet-kpis__text { background: linear-gradient(90deg, var(--sheet-track) 25%, #3A3A3A 50%, var(--sheet-track) 75%); background-size: 200% 100%; animation: sheet-shimmer 1.2s ease-in-out infinite; }\n" +
        ".sheet-kpis__card.is-skeleton .sheet-kpis__text { width: 96px; height: 16px; border-radius: 4px; }\n" +
        ".tablet .sheet-kpis__list, .phone .sheet-kpis__list { display: grid; grid-template-columns: repeat(2, minmax(0, 1fr)); }\n" +
        "@media (max-width: 1023px) { .sheet-kpis__list { display: grid; grid-template-columns: repeat(2, minmax(0, 1fr)); } }\n";
    theme.StyleSheet = theme.StyleSheet + css;
}
```
