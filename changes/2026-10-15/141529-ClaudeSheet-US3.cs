// Published: 2026-10-15T14:34:48.815335+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T14:15:29.338431+00:00
// Story:    US3
// Outcome:  saved
// Imports:  -
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
