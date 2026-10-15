// Published: 2026-10-15T17:09:41.123041+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T16:57:20.929468+00:00
// Story:    US4
// Outcome:  saved
// Imports:  -
eSpace => {
    var flow = eSpace.MobileFlows.Named("SheetBlocks");
    var wh = eSpace.Structures.Named("WeeklyHours");
    var getWeekly = eSpace.ServerActions.Named("ProjectHours_GetWeekly");

    // ---- P09 block, inputs, event ----
    var block = flow.CreateBlock("WeeklyProjectHours");
    block.Description = "Weekly grid of the current user's hours per project and per day (Monday-Sunday week containing ReferenceDate), with totals per day, per week and a grand total. Previous/next arrows move one week and raise OnWeekChanged. Reloads when ReferenceDate or RefreshKey change.";
    block.HorizontalPosition = 800; block.VerticalPosition = 1900;
    var pRef = block.CreateInputParameter("ReferenceDate");
    pRef.DataType = eSpace.DateType; pRef.IsMandatory = false; pRef.SetDefaultValue("CurrDate()");
    pRef.Description = "Any day of the week to show (default: today).";
    var pKey = block.CreateInputParameter("RefreshKey");
    pKey.DataType = eSpace.IntegerType; pKey.IsMandatory = false; pKey.SetDefaultValue("0");
    pKey.Description = "Change this value to force the block to reload (e.g. after saving a day). Only used to trigger OnParametersChanged.";
    var evWeek = block.CreateEvent("OnWeekChanged");
    evWeek.Description = "Raised by the previous/next arrows with the Monday of the new week.";
    evWeek.IsMandatory = false;
    var evArg = evWeek.CreateInputParameter("WeekStartDate"); evArg.DataType = eSpace.DateType;

    // ---- P10 local variable ----
    var vRef = block.CreateLocalVariable("CurrentReferenceDate");
    vRef.DataType = eSpace.DateType;
    vRef.Description = "Day of the week being shown; starts from the ReferenceDate input and moves with the arrows.";

    // ---- P11 data action GetWeek ----
    var getWeek = block.CreateDataAction("GetWeek");
    getWeek.OutputParameters.ToList().ForEach(x => x.Delete());
    getWeek.Description = "The weekly grid for CurrentReferenceDate (ProjectHours_GetWeekly).";
    var st = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); st.HorizontalPosition = 3200; st.VerticalPosition = 800;
    var call = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectHours_GetWeekly").ConnectedBelow(st, 1371);
    call.Action = getWeekly;
    call.SetArgumentValue(getWeekly.InputParameters.Named("ReferenceDate"), "CurrentReferenceDate");
    var asg = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(call, 1371);
    asg.CreateAssignment("Week", call.Name + ".Week");
    getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(asg, 1371);
    var allEx = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allEx.AbortTransaction = false; allEx.Exception = eSpace.AllExceptions; allEx.HorizontalPosition = 5028; allEx.VerticalPosition = 800;
    var asgErr = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allEx, 1371);
    asgErr.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(asgErr, 1371);
    var oWeek = getWeek.CreateOutputParameter("Week"); oWeek.DataType = wh;
    var oErr = getWeek.CreateOutputParameter("ErrorMessage"); oErr.DataType = eSpace.TextType;

    var after = block.CreateScreenAction("OnAfterFetch_GetWeek");
    after.Description = "Shows an error feedback message when GetWeek failed.";
    var st2 = after.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); st2.HorizontalPosition = 3200; st2.VerticalPosition = 800;
    var ifFail = after.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(st2, 1371);
    ifFail.SetCondition("GetWeek.ErrorMessage <> \"\""); ifFail.Label = "Failed?";
    var msg = after.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ToTheRightOf(ifFail, 1828);
    msg.SetMessage("GetWeek.ErrorMessage"); msg.Type = OutSystems.Model.Enumerations.MessageType.Error;
    var endA = after.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifFail, 1371);
    var endB = after.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(msg, 1371);
    ifFail.TrueTarget = msg; ifFail.FalseTarget = endA;
    getWeek.OnAfterFetch.Destination = after;

    // ---- P10 OnInitialize / OnParametersChanged ----
    var init = block.CreateScreenAction("OnInitialize");
    init.Description = "Starts CurrentReferenceDate from the ReferenceDate input (today when empty).";
    var st3 = init.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); st3.HorizontalPosition = 3200; st3.VerticalPosition = 800;
    var a3 = init.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(st3, 1371);
    a3.CreateAssignment("CurrentReferenceDate", "If(ReferenceDate = NullDate(), CurrDate(), ReferenceDate)");
    init.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(a3, 1371);
    block.OnInitialize.Destination = init;

    var opc = block.CreateScreenAction("OnParametersChanged");
    opc.Description = "Syncs CurrentReferenceDate with the ReferenceDate input and reloads GetWeek (also when RefreshKey changes).";
    var st4 = opc.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); st4.HorizontalPosition = 3200; st4.VerticalPosition = 800;
    var a4 = opc.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(st4, 1371);
    a4.CreateAssignment("CurrentReferenceDate", "If(ReferenceDate = NullDate(), CurrDate(), ReferenceDate)");
    var r4 = opc.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(a4, 1371);
    r4.DataSource = getWeek;
    opc.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(r4, 1371);
    block.OnParametersChanged.Destination = opc;

    // ---- P13 arrows ----
    Func<string, int, OutSystems.Model.UI.Mobile.IScreenAction> arrowAction = (name, days) => {
        var a = block.CreateScreenAction(name);
        a.Description = "Moves the grid " + (days < 0 ? "one week back" : "one week forward") + ", reloads GetWeek and raises OnWeekChanged with the new Monday.";
        var s0 = a.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); s0.HorizontalPosition = 3200; s0.VerticalPosition = 800;
        var aa = a.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(s0, 1371);
        aa.CreateAssignment("CurrentReferenceDate", "DateTimeToDate(AddDays(GetWeek.Week.WeekStart, " + days + "))");
        var rr = a.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(aa, 1371);
        rr.DataSource = getWeek;
        var tr = a.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnWeekChanged").ConnectedBelow(rr, 1371);
        tr.Event = evWeek;
        tr.SetArgumentValue(evArg, "CurrentReferenceDate");
        a.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(tr, 1371);
        return a;
    };
    var prevAct = arrowAction("OnClick_PreviousWeek", -7);
    var nextAct = arrowAction("OnClick_NextWeek", 7);

    // ---- widgets ----
    Func<object, string, ServiceStudio.Plugin.NRWidgets.IContainer> box = null;
    box = (parent, cls) => {
        ServiceStudio.Plugin.NRWidgets.IContainer c;
        var pc = parent as ServiceStudio.Plugin.NRWidgets.IContainer;
        var pl = parent as ServiceStudio.Plugin.NRWidgets.IList;
        var pp = parent as OutSystems.Model.UI.Mobile.Widgets.IPlaceholderContentWidget;
        var pb = parent as OutSystems.Model.UI.Mobile.IMobileBlock;
        var pi = parent as OutSystems.Model.UI.Mobile.Widgets.IIfWidget;
        if (pc != null) c = pc.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
        else if (pl != null) c = pl.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
        else if (pp != null) c = pp.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
        else if (pb != null) c = pb.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
        else throw new Exception("box parent");
        c.SetStyle(cls);
        return c;
    };
    Action<ServiceStudio.Plugin.NRWidgets.IContainer, string, string> attr = (w, prop, val) => { var ep = w.CreateExtendedProperty(); ep.Property = prop; ep.SetValue(val); };
    Action<ServiceStudio.Plugin.NRWidgets.IContainer, string> text = (w, expr) => { var e = w.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>(); e.SetValue(expr); };
    Func<ServiceStudio.Plugin.NRWidgets.IContainer, string, string, string, OutSystems.Model.UI.Mobile.IScreenAction, ServiceStudio.Plugin.NRWidgets.IButton> arrow = (parent, name, icon, label, dest) => {
        var b = parent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>(name);
        b.Widgets.ToList().ForEach(x => x.Delete());
        b.SetEnabled("True");
        b.SetStyle("\"sheet-week__arrow\"");
        var ic = b.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>(); ic.Icon = icon;
        b.OnClick.Destination = dest;
        var ep = b.CreateExtendedProperty(); ep.Property = "aria-label"; ep.SetValue(label);
        return b;
    };
    Func<string, string> cellText = cur => "If(" + cur + ".ShowDash, \"-\", Minutes_Format(" + cur + ".Minutes, \":\"))";

    var root = box(block, "\"sheet-week\" + If(GetWeek.IsDataFetched, \"\", \" sheet-week--loading\")");
    root.Name = "WeekGrid";
    attr(root, "aria-busy", "If(GetWeek.IsDataFetched, \"false\", \"true\")");

    // P19 horizontal ScrollableArea
    var osui = eSpace.References.Named("OutSystemsUI");
    var scrollSig = osui.MobileFlows.Named("Interaction").Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ScrollableArea");
    var scroll = root.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("WeekScroll");
    scroll.SourceBlock = scrollSig;
    scroll.SetArgumentValue(scrollSig.InputParameters.Named("ExtendedClass"), "\"sheet-week__scroll\"");
    var content = scroll.PlaceholdersContent.First(p => p.Placeholder != null && p.Placeholder.Name == "Content");
    content.Widgets.ToList().ForEach(w => w.Delete());

    var table = box(content, "\"sheet-week__table\"");
    attr(table, "role", "\"table\"");
    attr(table, "aria-label", "\"Project daily hours, week of \" + FormatDateTime(GetWeek.Week.WeekStart, \"dd/MM/yyyy\")");

    // P12 header row
    var head = box(table, "\"sheet-week__row sheet-week__head\"");
    attr(head, "role", "\"row\"");
    var hLabel = box(head, "\"sheet-week__label\"");
    attr(hLabel, "role", "\"columnheader\"");
    var hTitle = box(hLabel, "\"sheet-week__title\"");
    text(hTitle, "\"Project daily hours\"");
    arrow(hLabel, "PreviousWeek", "chevron-left", "\"Previous week\"", prevAct);
    var dayList = head.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("DayHeaders");
    dayList.SetSource("GetWeek.Week.Days"); dayList.SetStyle("\"sheet-week__contents\""); dayList.Tag = "div";
    var dCur = "GetWeek.Week.Days.Current";
    var dayCell = box(dayList, "\"sheet-week__day\" + If(" + dCur + ".IsToday, \" is-today\", \"\") + If(" + dCur + ".IsWeekend, \" is-weekend\", \"\")");
    attr(dayCell, "role", "\"columnheader\"");
    var dn = box(dayCell, "\"sheet-week__day-name\""); text(dn, dCur + ".DayName");
    var dd = box(dayCell, "\"sheet-week__day-date\""); text(dd, dCur + ".DateLabel");
    var hTot = box(head, "\"sheet-week__total-head\"");
    attr(hTot, "role", "\"columnheader\"");
    arrow(hTot, "NextWeek", "chevron-right", "\"Next week\"", nextAct);
    var hTotText = box(hTot, "\"sheet-week__total-label\""); text(hTotText, "\"Total per week\"");

    // P18 empty state vs rows
    var hasRows = table.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasRows");
    hasRows.SetCondition("GetWeek.Week.Rows.Length > 0 or not GetWeek.IsDataFetched");

    // P14/P15 project rows
    var rowsHolder = hasRows.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); rowsHolder.SetStyle("\"sheet-week__body\"");
    var rowList = rowsHolder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectRows");
    rowList.SetSource("GetWeek.Week.Rows"); rowList.SetStyle("\"sheet-week__rows\""); rowList.Tag = "div";
    var rCur = "GetWeek.Week.Rows.Current";
    var row = box(rowList, "\"sheet-week__row sheet-week__card\"");
    attr(row, "role", "\"row\"");
    var rLabel = box(row, "\"sheet-week__label sheet-week__label--project\"");
    attr(rLabel, "role", "\"rowheader\"");
    var rClient = box(rLabel, "\"sheet-week__client\""); text(rClient, rCur + ".ClientName");
    var rProj = box(rLabel, "\"sheet-week__project\""); text(rProj, rCur + ".ProjectName + \" | \" + " + rCur + ".ProjectCode");
    var cellList = row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("RowCells");
    cellList.SetSource(rCur + ".Cells"); cellList.SetStyle("\"sheet-week__contents\""); cellList.Tag = "div";
    var cCur = rCur + ".Cells.Current";
    var cell = box(cellList, "\"sheet-week__cell\" + If(DayOfWeek(" + cCur + ".Date) = 0 or DayOfWeek(" + cCur + ".Date) = 6, \" is-weekend\", \"\")");
    attr(cell, "role", "\"cell\"");
    text(cell, cellText(cCur));
    var rTot = box(row, "\"sheet-week__cell sheet-week__total\"");
    attr(rTot, "role", "\"cell\"");
    text(rTot, "Minutes_Format(" + rCur + ".TotalMinutes, \":\")");

    // P16 total per day row + grand total
    var totRow = box(rowsHolder, "\"sheet-week__row sheet-week__card sheet-week__totals\"");
    attr(totRow, "role", "\"row\"");
    var tLabel = box(totRow, "\"sheet-week__label sheet-week__label--project\"");
    attr(tLabel, "role", "\"rowheader\"");
    text(tLabel, "\"Total per day\"");
    var totList = totRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("DayTotals");
    totList.SetSource("GetWeek.Week.DayTotals"); totList.SetStyle("\"sheet-week__contents\""); totList.Tag = "div";
    var tCur = "GetWeek.Week.DayTotals.Current";
    var tCell = box(totList, "\"sheet-week__cell\" + If(DayOfWeek(" + tCur + ".Date) = 0 or DayOfWeek(" + tCur + ".Date) = 6, \" is-weekend\", \"\")");
    attr(tCell, "role", "\"cell\"");
    text(tCell, cellText(tCur));
    var grand = box(totRow, "\"sheet-week__cell sheet-week__total\"");
    attr(grand, "role", "\"cell\"");
    text(grand, "Minutes_Format(GetWeek.Week.GrandTotalMinutes, \":\")");

    // P18 empty state
    var empty = hasRows.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>(); empty.SetStyle("\"sheet-week__empty\"");
    attr(empty, "role", "\"status\"");
    text(empty, "\"No projects assigned for this week\"");
}
