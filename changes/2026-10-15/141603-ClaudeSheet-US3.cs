// Published: 2026-10-15T14:34:48.815335+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T14:16:03.834193+00:00
// Story:    US3
// Outcome:  saved
// Imports:  -
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
