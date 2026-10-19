# US4 · ClaudeSheet changes

Every `applyModelApiCode` save for US4 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-15 16:52:52 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved with 1 validation error(s)
- **Imports:** -
- **Error:** /ProjectHours_GetWeekly/Build day header/DayHeader.DayName = If(DayOfWeek(DayHeader.Date) = 1, "Monday", If(DayOfWeek(DayHeader.Date) = 2, "Tuesday", If(DayOfWeek(DayHeader.Date) = 3, "Wednesday", If(DayOfWeek(DayHeader.Date) = 4, "Thursday", If(DayOfWeek(DayHeader.Date) = 5, "Friday", If(DayOfWeek(DayHeader.Date) = 6, "Saturday", "Sunday")))))) - Invalid Data Type (fixed by the next save)

```csharp
eSpace => {
    // ---- US4: extend the TT0 weekly structures (unused until now) ----
    var wdh = eSpace.Structures.Named("WeekDayHeader");
    wdh.Attributes.Named("DayLabel").Name = "DayName";
    var dateLabel = wdh.CreateAttribute("DateLabel");
    dateLabel.DataType = eSpace.TextType; dateLabel.Length = 10;
    var wcell = eSpace.Structures.Named("WeeklyCell");
    var showDash = wcell.CreateAttribute("ShowDash");
    showDash.DataType = eSpace.BooleanType;
    showDash.Description = "True when the cell shows \"-\": no entry AND the day is in the future, a weekend, a holiday or a day off.";
    var wrow = eSpace.Structures.Named("WeeklyProjectRow");
    var wh = eSpace.Structures.Named("WeeklyHours");
    wh.Attributes.Named("TotalMinutes").Name = "GrandTotalMinutes";
    var dayTotals = wh.CreateAttribute("DayTotals"); dayTotals.DataType = eSpace.GetOrCreateListType(wcell);
    dayTotals.Description = "Total per day (7 cells); ShowDash when every row is \"-\" that day.";

    // ---- server action ProjectHours_GetWeekly ----
    var act = eSpace.CreateServerAction("ProjectHours_GetWeekly");
    act.Description = "Weekly grid for the current user: the Monday-Sunday week containing ReferenceDate, day headers classified like the calendar (Calendar_GetMonth), one row per project assigned during the week or with entries in it (sorted by client, project), 7 cells per row with the \"-\" rule, totals per row, per day and for the week.";
    var pRef = act.CreateInputParameter("ReferenceDate"); pRef.DataType = eSpace.DateType; pRef.IsMandatory = true;
    var outWeek = act.CreateOutputParameter("Week"); outWeek.DataType = wh;
    var vIdx = act.CreateLocalVariable("DayIndex"); vIdx.DataType = eSpace.IntegerType;
    var vDay = act.CreateLocalVariable("DayHeader"); vDay.DataType = wdh;
    var vRow = act.CreateLocalVariable("Row"); vRow.DataType = wrow;
    var vCell = act.CreateLocalVariable("Cell"); vCell.DataType = wcell;

    var start = act.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    start.HorizontalPosition = 3200; start.VerticalPosition = 800;

    var ifRole = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(start, 1371);
    ifRole.SetCondition("CheckSheetUserRole()"); ifRole.Label = "Is SheetUser?";
    var raise = act.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifRole, 1828);
    raise.Exception = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raise.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifRole.FalseTarget = raise;

    var aWeek = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifRole, 1371);
    aWeek.Label = "Week bounds";
    aWeek.CreateAssignment("Week.WeekStart", "Date_GetWeekStart(ReferenceDate)");
    aWeek.CreateAssignment("Week.WeekEnd", "DateTimeToDate(AddDays(Week.WeekStart, 6))");
    ifRole.TrueTarget = aWeek;

    var calGetMonth = eSpace.ServerActions.Named("Calendar_GetMonth");
    var m1 = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("StartMonth").ConnectedBelow(aWeek, 1371);
    m1.Action = calGetMonth;
    m1.SetArgumentValue(calGetMonth.InputParameters.Named("Year"), "Year(Week.WeekStart)");
    m1.SetArgumentValue(calGetMonth.InputParameters.Named("Month"), "Month(Week.WeekStart)");
    var m2 = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("EndMonth").ConnectedBelow(m1, 1371);
    m2.Action = calGetMonth;
    m2.SetArgumentValue(calGetMonth.InputParameters.Named("Year"), "Year(Week.WeekEnd)");
    m2.SetArgumentValue(calGetMonth.InputParameters.Named("Month"), "Month(Week.WeekEnd)");
    var sm = m1.Name; var em = m2.Name;

    var aIdx0 = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(m2, 1371);
    aIdx0.CreateAssignment("DayIndex", "0");
    var ifDays = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(aIdx0, 1371);
    ifDays.SetCondition("DayIndex < 7"); ifDays.Label = "More days?";

    Func<string, string> fromCal = attr => "If(Month(DayHeader.Date) = Month(Week.WeekStart), " + sm + ".Days[Day(DayHeader.Date) - 1]." + attr + ", " + em + ".Days[Day(DayHeader.Date) - 1]." + attr + ")";
    var aDay = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifDays, 1828);
    aDay.Label = "Build day header";
    aDay.CreateAssignment("DayHeader.Date", "DateTimeToDate(AddDays(Week.WeekStart, DayIndex))");
    aDay.CreateAssignment("DayHeader.DayName", "If(DayOfWeek(DayHeader.Date) = 1, \"Monday\", If(DayOfWeek(DayHeader.Date) = 2, \"Tuesday\", If(DayOfWeek(DayHeader.Date) = 3, \"Wednesday\", If(DayOfWeek(DayHeader.Date) = 4, \"Thursday\", If(DayOfWeek(DayHeader.Date) = 5, \"Friday\", If(DayOfWeek(DayHeader.Date) = 6, \"Saturday\", \"Sunday\"))))))");
    aDay.CreateAssignment("DayHeader.DateLabel", "FormatDateTime(DayHeader.Date, \"dd/MM\")");
    aDay.CreateAssignment("DayHeader.IsWeekend", "DayOfWeek(DayHeader.Date) = 0 or DayOfWeek(DayHeader.Date) = 6");
    aDay.CreateAssignment("DayHeader.IsToday", "DayHeader.Date = CurrDate()");
    aDay.CreateAssignment("DayHeader.DayTypeId", fromCal("DayTypeId"));
    aDay.CreateAssignment("DayHeader.CssClass", fromCal("CssClass"));
    aDay.CreateAssignment("DayHeader.IsEditable", fromCal("IsEditable"));
    aDay.CreateAssignment("Cell.Date", "DayHeader.Date");
    aDay.CreateAssignment("Cell.Minutes", "0");
    aDay.CreateAssignment("Cell.ShowDash", "True");
    ifDays.TrueTarget = aDay;

    var sys = eSpace.References.Named("(System)");
    var listAppend = sys.ServerActions.Named("ListAppend");
    var listClear = sys.ServerActions.Named("ListClear");
    Func<OutSystems.Model.Logic.Nodes.IActionNode, string, string, OutSystems.Model.Logic.Nodes.IExecuteServerActionNode> append = (prev, list, elem) => {
        var n = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(prev, 1371);
        n.Action = listAppend;
        n.SetArgumentValue(listAppend.InputParameters.Named("List"), list);
        n.SetArgumentValue(listAppend.InputParameters.Named("Element"), elem);
        return n;
    };
    var apDay = append(aDay, "Week.Days", "DayHeader");
    var apTot = append(apDay, "Week.DayTotals", "Cell");
    var aNextDay = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(apTot, 1371);
    aNextDay.CreateAssignment("DayIndex", "DayIndex + 1");
    aNextDay.Target = ifDays;

    // projects assigned during the week OR with entries in the week, sorted by client then project
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var sql = act.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetWeekProjects").Below(ifDays, 1371);
    ifDays.FalseTarget = sql;
    var userEnt = (OutSystems.Model.Data.IServerEntitySignature)sys.Entities.Named("User");
    var pUser = sql.CreateInputParameter("UserId"); pUser.DataType = userEnt.IdentifierType; sql.SetArgumentValue(pUser, "GetUserId()");
    var pWs = sql.CreateInputParameter("WeekStart"); pWs.DataType = eSpace.DateType; sql.SetArgumentValue(pWs, "Week.WeekStart");
    var pWe = sql.CreateInputParameter("WeekEnd"); pWe.DataType = eSpace.DateType; sql.SetArgumentValue(pWe, "Week.WeekEnd");
    var pNd = sql.CreateInputParameter("NullDate"); pNd.DataType = eSpace.DateType; sql.SetArgumentValue(pNd, "NullDate()");
    sql.CreateOutput(project);
    sql.CreateOutput(client);
    sql.Statement =
        "SELECT {Project}.*, {Client}.*\n" +
        "FROM {Project}\n" +
        "INNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\n" +
        "WHERE {Project}.[Id] IN (\n" +
        "    SELECT {ProjectAssignment}.[ProjectId] FROM {ProjectAssignment}\n" +
        "    WHERE {ProjectAssignment}.[UserId] = @UserId AND {ProjectAssignment}.[IsActive] = 1\n" +
        "      AND {ProjectAssignment}.[StartDate] <= @WeekEnd\n" +
        "      AND ({ProjectAssignment}.[EndDate] = @NullDate OR {ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] >= @WeekStart)\n" +
        "      AND {Project}.[IsActive] = 1 AND {Client}.[IsActive] = 1)\n" +
        "OR {Project}.[Id] IN (\n" +
        "    SELECT {TimeEntry}.[ProjectId] FROM {TimeEntry}\n" +
        "    INNER JOIN {DaySheet} ON {DaySheet}.[Id] = {TimeEntry}.[DaySheetId]\n" +
        "    WHERE {DaySheet}.[UserId] = @UserId AND {DaySheet}.[SheetDate] >= @WeekStart AND {DaySheet}.[SheetDate] <= @WeekEnd)\n" +
        "ORDER BY {Client}.[Name], {Project}.[Name]";
    var sqlName = sql.Name;

    // minutes per project per day (single grouped aggregate)
    var timeEntry = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var agg = act.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetWeekEntries").ConnectedBelow(sql, 1371);
    var dba = agg.AsDatabaseAggregate;
    var sTe = dba.CreateSource(timeEntry); var sDs = dba.CreateSource(daySheet);
    var j = dba.CreateJoin(); j.LeftSource = sTe; j.RightSource = sDs; j.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    j.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    dba.CreateFilter("DaySheet.UserId = GetUserId()");
    dba.CreateFilter("DaySheet.SheetDate >= Week.WeekStart and DaySheet.SheetDate <= Week.WeekEnd");
    dba.CreateGroupByAttribute("ProjectId").SetAttribute("TimeEntry.ProjectId");
    dba.CreateGroupByAttribute("SheetDate").SetAttribute("DaySheet.SheetDate");
    var sum = dba.CreateAggregatedAttribute("Minutes"); sum.SetAttribute("TimeEntry.Minutes"); sum.AggregationType = OutSystems.Model.Enumerations.AggregationType.Sum;
    var aggName = agg.Name;

    // one row per project, 7 cells each
    var feRows = act.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(agg, 1371);
    feRows.SetRecordList(sqlName + ".List");
    var cur = sqlName + ".List.Current";
    var aRow = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(feRows, 1828);
    aRow.Label = "Start row";
    aRow.CreateAssignment("Row.ProjectId", cur + ".Project.Id");
    aRow.CreateAssignment("Row.ClientName", cur + ".Client.Name");
    aRow.CreateAssignment("Row.ProjectName", cur + ".Project.Name");
    aRow.CreateAssignment("Row.ProjectCode", cur + ".Project.Code");
    aRow.CreateAssignment("Row.TotalMinutes", "0");
    aRow.CreateAssignment("DayIndex", "0");
    feRows.CycleTarget = aRow;
    var clr = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListClear").ConnectedBelow(aRow, 1371);
    clr.Action = listClear;
    clr.SetArgumentValue(listClear.InputParameters.Named("List"), "Row.Cells");
    var ifCells = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(clr, 1371);
    ifCells.SetCondition("DayIndex < 7"); ifCells.Label = "More cells?";
    var aCell = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifCells, 1828);
    aCell.Label = "Start cell";
    aCell.CreateAssignment("Cell.Date", "Week.Days[DayIndex].Date");
    aCell.CreateAssignment("Cell.Minutes", "0");
    ifCells.TrueTarget = aCell;
    var feEnt = act.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(aCell, 1371);
    feEnt.SetRecordList(aggName + ".List");
    var ifMatch = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(feEnt, 1828);
    ifMatch.SetCondition(aggName + ".List.Current.ProjectId = Row.ProjectId and " + aggName + ".List.Current.SheetDate = Cell.Date");
    ifMatch.Label = "Same project and day?";
    feEnt.CycleTarget = ifMatch;
    var aAdd = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifMatch, 1371);
    aAdd.CreateAssignment("Cell.Minutes", "Cell.Minutes + LongIntegerToInteger(" + aggName + ".List.Current.Minutes)");
    ifMatch.TrueTarget = aAdd;
    ifMatch.FalseTarget = feEnt;
    aAdd.Target = feEnt;
    var aTot = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(feEnt, 1371);
    aTot.Label = "Dash rule and totals";
    aTot.CreateAssignment("Cell.ShowDash", "Cell.Minutes = 0 and (Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Future or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Weekend or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Holiday or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.DayOff)");
    aTot.CreateAssignment("Row.TotalMinutes", "Row.TotalMinutes + Cell.Minutes");
    aTot.CreateAssignment("Week.DayTotals[DayIndex].Minutes", "Week.DayTotals[DayIndex].Minutes + Cell.Minutes");
    aTot.CreateAssignment("Week.DayTotals[DayIndex].ShowDash", "Week.DayTotals[DayIndex].ShowDash and Cell.ShowDash");
    aTot.CreateAssignment("Week.GrandTotalMinutes", "Week.GrandTotalMinutes + Cell.Minutes");
    feEnt.Target = aTot;
    var apCell = append(aTot, "Row.Cells", "Cell");
    var aNextCell = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(apCell, 1371);
    aNextCell.CreateAssignment("DayIndex", "DayIndex + 1");
    aNextCell.Target = ifCells;
    var apRow = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").Below(ifCells, 1371);
    apRow.Action = listAppend;
    apRow.SetArgumentValue(listAppend.InputParameters.Named("List"), "Week.Rows");
    apRow.SetArgumentValue(listAppend.InputParameters.Named("Element"), "Row");
    ifCells.FalseTarget = apRow;
    apRow.Target = feRows;

    var end = act.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(feRows, 1371);
    feRows.Target = end;
}
```

## Save 2 · 2026-10-15 16:53:33 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // US4: renaming DayLabel -> DayName made Service Studio infer a Date type; it is the English day name (Text)
    var a = eSpace.Structures.Named("WeekDayHeader").Attributes.Named("DayName");
    a.DataType = eSpace.TextType;
    a.Length = 20;
}
```

## Save 3 · 2026-10-15 16:53:54 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    var targets = new[] { "Server ProjectHours_GetWeekly" };
    // ---------- flow layout (docs/conventions.md › Flow layout) ----------
    // Vertical main path; If False / Switch conditions leave horizontally to a new column right of everything below;
    // For Each body to the right first, loop continuation below the body; a branch that would join a shared End gets its own End.
    Func<OutSystems.Model.Logic.Nodes.IActionNode, List<Tuple<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>>> succ = n => {
        var r = new List<Tuple<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>>();
        var iff = n as OutSystems.Model.Logic.Nodes.IIfNode;
        var fe = n as OutSystems.Model.Logic.Nodes.IForEachNode;
        var sw = n as OutSystems.Model.Logic.Nodes.ISwitchNode;
        if (iff != null) {
            r.Add(Tuple.Create<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>(() => iff.TrueTarget, t => iff.TrueTarget = t, false));
            r.Add(Tuple.Create<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>(() => iff.FalseTarget, t => iff.FalseTarget = t, true));
        } else if (fe != null) {
            r.Add(Tuple.Create<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>(() => fe.Target, t => fe.Target = t, false));
            r.Add(Tuple.Create<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>(() => fe.CycleTarget, t => fe.CycleTarget = t, true));
        } else if (sw != null) {
            r.Add(Tuple.Create<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>(() => sw.OtherwiseTarget, t => sw.OtherwiseTarget = t, false));
            foreach (var c in sw.Conditions.ToList()) {
                var cc = c;
                r.Add(Tuple.Create<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>(() => cc.Target, t => cc.Target = t, true));
            }
        } else {
            var first = true;
            foreach (var c in n.Connectors.ToList()) {
                var cc = c;
                r.Add(Tuple.Create<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>(() => cc.Target, t => cc.Target = t, !first));
                first = false;
            }
        }
        return r;
    };

    Func<List<OutSystems.Model.Logic.Nodes.IActionNode>, Func<OutSystems.Model.Logic.Nodes.IActionNode>, int> layoutFlow = (nodes, makeEnd) => {
        var col = new Dictionary<OutSystems.Model.Logic.Nodes.IActionNode, int>();
        var row = new Dictionary<OutSystems.Model.Logic.Nodes.IActionNode, int>();
        var added = 0;
        Func<int, int, int> freeColIn = (r0, r1) => { var m = -1; foreach (var k in row.Keys) if (row[k] >= r0 && row[k] <= r1 && col[k] > m) m = col[k]; return m + 1; };
        Func<OutSystems.Model.Logic.Nodes.IActionNode, HashSet<OutSystems.Model.Logic.Nodes.IActionNode>, int> height = null;
        height = (n, seen) => {
            if (n == null || row.ContainsKey(n) || seen.Contains(n)) return 0;
            seen.Add(n);
            var fe0 = n as OutSystems.Model.Logic.Nodes.IForEachNode;
            if (fe0 != null) return Math.Max(1, height(fe0.CycleTarget, seen)) + height(fe0.Target, seen);
            var ss0 = succ(n);
            var hd = 0; var hs = 0;
            foreach (var x in ss0) { var hx = height(x.Item1(), seen); if (x.Item3) hs = Math.Max(hs, hx); else hd = Math.Max(hd, hx); }
            return Math.Max(1 + hd, hs);
        };
        Func<OutSystems.Model.Logic.Nodes.IActionNode, int, int> freeCol = (t, r0) => freeColIn(r0, r0 + Math.Max(1, height(t, new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>())) - 1);
        Action<OutSystems.Model.Logic.Nodes.IActionNode, int, int> setPos = (n, c, r) => { col[n] = c; row[n] = r; };
        Action<OutSystems.Model.Logic.Nodes.IActionNode, int, int> place = null;
        Func<Action<OutSystems.Model.Logic.Nodes.IActionNode>, int, int, OutSystems.Model.Logic.Nodes.IActionNode> newEnd = (set, c, r) => {
            var e = makeEnd(); added++; set(e); setPos(e, c, r); return e;
        };
        Func<int> maxRow = () => row.Values.Count == 0 ? 0 : row.Values.Max();
        place = (n, c, r) => {
            setPos(n, c, r);
            var fe = n as OutSystems.Model.Logic.Nodes.IForEachNode;
            var ss = succ(n);
            // a loop: For Each (body = CycleTarget) or an If whose branch leads back to the If itself
            Tuple<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool> loopBody = null, loopNext = null;
            if (fe != null) { loopBody = ss[1]; loopNext = ss[0]; }
            var iffL = n as OutSystems.Model.Logic.Nodes.IIfNode;
            if (iffL != null && ss.Count == 2) {
                Func<OutSystems.Model.Logic.Nodes.IActionNode, bool> backToMe = from => {
                    if (from == null || from == n) return false;
                    var seen = new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>(); var st = new Stack<OutSystems.Model.Logic.Nodes.IActionNode>(); st.Push(from);
                    while (st.Count > 0) { var x = st.Pop(); if (x == n) return true; if (!seen.Add(x) || row.ContainsKey(x)) continue; foreach (var cc in x.Connectors) if (cc.Target != null) st.Push(cc.Target); }
                    return false;
                };
                if (backToMe(ss[0].Item1()) && !backToMe(ss[1].Item1())) { loopBody = ss[0]; loopNext = ss[1]; }
                else if (backToMe(ss[1].Item1()) && !backToMe(ss[0].Item1())) { loopBody = ss[1]; loopNext = ss[0]; }
            }
            if (loopBody != null) {
                // body to the right on the same row, far enough that the loop-back diagonal clears the body nodes;
                // the continuation goes below the body's lowest node
                var before = new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>(row.Keys);
                var bt = loopBody.Item1();
                if (bt != null && !row.ContainsKey(bt)) {
                    var bh = Math.Max(1, height(bt, new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>()));
                    var bc = Math.Max(freeCol(bt, r), c + (int)Math.Ceiling(bh / 4.0));
                    place(bt, bc, r);
                }
                var bottom = r;
                foreach (var k in row.Keys) if (!before.Contains(k) && row[k] > bottom) bottom = row[k];
                var dt = loopNext.Item1();
                if (dt != null) {
                    if (!row.ContainsKey(dt)) place(dt, c, bottom + 1);
                    else if (dt is OutSystems.Model.Logic.Nodes.IEndNode) newEnd(loopNext.Item2, c, bottom + 1);
                }
                return;
            }
            var iff2 = n as OutSystems.Model.Logic.Nodes.IIfNode;
            if (iff2 != null && ss.Count == 2) {
                var hT = height(ss[0].Item1(), new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>());
                var hF = height(ss[1].Item1(), new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>());
                Func<OutSystems.Model.Logic.Nodes.IActionNode, OutSystems.Model.Logic.Nodes.IActionNode, bool> reaches = (from, to) => {
                    if (from == null || to == null || to is OutSystems.Model.Logic.Nodes.IForEachNode) return false;
                    var seen = new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>(); var st = new Stack<OutSystems.Model.Logic.Nodes.IActionNode>(); st.Push(from);
                    while (st.Count > 0) { var x = st.Pop(); if (x == to) return true; if (!seen.Add(x) || x == n || x is OutSystems.Model.Logic.Nodes.IForEachNode) continue; foreach (var c in x.Connectors) if (c.Target != null) st.Push(c.Target); }
                    return false;
                };
                var tT = ss[0].Item1(); var tF = ss[1].Item1();
                bool swap;
                if (reaches(tF, tT) && tF != tT) swap = false;       // False is an optional step rejoining True's target: True goes down
                else if (reaches(tT, tF) && tF != tT) swap = true;   // True is the optional step: False goes down
                else swap = hF > hT;                                 // otherwise the taller branch goes down
                if (swap) ss = new List<Tuple<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>> {
                    Tuple.Create(ss[1].Item1, ss[1].Item2, false), Tuple.Create(ss[0].Item1, ss[0].Item2, true) };
            }
            foreach (var s in ss.Where(x => !x.Item3)) {
                var t = s.Item1();
                if (t == null) continue;
                if (!row.ContainsKey(t)) place(t, c, r + 1);
                else if (t is OutSystems.Model.Logic.Nodes.IEndNode) newEnd(s.Item2, c, r + 1);
            }
            foreach (var s in ss.Where(x => x.Item3)) {
                var t = s.Item1();
                if (t == null) continue;
                if (!row.ContainsKey(t)) place(t, freeCol(t, r), r);
                else if (t is OutSystems.Model.Logic.Nodes.IEndNode) newEnd(s.Item2, freeColIn(r, r), r);
            }
        };
        var start = nodes.OfType<OutSystems.Model.Logic.Nodes.IStartNode>().FirstOrDefault();
        if (start != null) place(start, 0, 0);
        foreach (var h in nodes.OfType<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>())
            if (!row.ContainsKey(h)) { var hc = col.Count == 0 ? 0 : col.Values.Max() + 1; place(h, hc, 0); }
        var extraCol = col.Count == 0 ? 0 : col.Values.Max() + 1; var extraRow = 0;
        foreach (var n in nodes) if (!row.ContainsKey(n)) setPos(n, extraCol, extraRow++);
        foreach (var n in row.Keys) { n.HorizontalPosition = 3200 + col[n] * 1828; n.VerticalPosition = 800 + row[n] * 1371; }
        return added;
    };

    // ---------- flow metrics ----------
    Func<List<OutSystems.Model.Logic.Nodes.IActionNode>, int[]> measure = nodes => {
        int v = 0, h = 0, dLoop = 0, dMerge = 0, dOther = 0, x = 0, ov = 0;
        var segs = new List<double[]>();
        foreach (var n in nodes)
            foreach (var c in n.Connectors) {
                var t = c.Target;
                if (t == null || n.GetType().Name.StartsWith("Comment")) continue;
                double x1 = n.HorizontalPosition, y1 = n.VerticalPosition, x2 = t.HorizontalPosition, y2 = t.VerticalPosition;
                if (Math.Abs(x1 - x2) < 50) v++;
                else if (Math.Abs(y1 - y2) < 50) h++;
                else {
                    var fe = t as OutSystems.Model.Logic.Nodes.IForEachNode;
                    // back-edge: the target can reach the source again (For Each body or a loop built with an If)
                    var back = false;
                    { var seen = new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>(); var st = new Stack<OutSystems.Model.Logic.Nodes.IActionNode>(); st.Push(t);
                      while (st.Count > 0 && !back) { var xq = st.Pop(); if (xq == n) { back = true; break; } if (!seen.Add(xq)) continue; foreach (var cc in xq.Connectors) if (cc.Target != null) st.Push(cc.Target); } }
                    if ((fe != null && fe.NodesWithinCycle.Contains(n)) || back) dLoop++;
                    else if (t.IncomingConnectors.Count() > 1 && !(t is OutSystems.Model.Logic.Nodes.IEndNode)) dMerge++;
                    else dOther++;
                }
                segs.Add(new[] { x1, y1, x2, y2 });
            }
        Func<double, double, double, double, double, double, double> cr = (ax, ay, bx, by, cx, cy) => (bx - ax) * (cy - ay) - (by - ay) * (cx - ax);
        for (var i = 0; i < segs.Count; i++)
            for (var j = i + 1; j < segs.Count; j++) {
                var a = segs[i]; var b = segs[j];
                if ((a[0] == b[0] && a[1] == b[1]) || (a[0] == b[2] && a[1] == b[3]) || (a[2] == b[0] && a[3] == b[1]) || (a[2] == b[2] && a[3] == b[3])) continue;
                var d1 = cr(b[0], b[1], b[2], b[3], a[0], a[1]); var d2 = cr(b[0], b[1], b[2], b[3], a[2], a[3]);
                var d3 = cr(a[0], a[1], a[2], a[3], b[0], b[1]); var d4 = cr(a[0], a[1], a[2], a[3], b[2], b[3]);
                if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) && ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0))) x++;
            }
        foreach (var sg in segs)
            foreach (var nd in nodes) {
                if (nd.GetType().Name.StartsWith("Comment")) continue;
                double px = nd.HorizontalPosition, py = nd.VerticalPosition;
                if ((px == sg[0] && py == sg[1]) || (px == sg[2] && py == sg[3])) continue;
                double dx = sg[2] - sg[0], dy = sg[3] - sg[1], len2 = dx * dx + dy * dy;
                if (len2 == 0) continue;
                var tt = ((px - sg[0]) * dx + (py - sg[1]) * dy) / len2;
                if (tt <= 0 || tt >= 1) continue;
                double qx = sg[0] + tt * dx - px, qy = sg[1] + tt * dy - py;
                if (qx * qx + qy * qy < 400 * 400) ov++;
            }
        for (var i = 0; i < nodes.Count; i++)
            for (var j = i + 1; j < nodes.Count; j++)
                if (Math.Abs(nodes[i].HorizontalPosition - nodes[j].HorizontalPosition) < 900 && Math.Abs(nodes[i].VerticalPosition - nodes[j].VerticalPosition) < 900) ov++;
        return new[] { v, h, dLoop, dMerge, dOther, x, ov };
    };

    // every action flow of the module: name, nodes, End factory
    var flows = new List<Tuple<string, Func<List<OutSystems.Model.Logic.Nodes.IActionNode>>, Func<OutSystems.Model.Logic.Nodes.IActionNode>>>();
    foreach (var a in eSpace.ServerActions) { var aa = a; flows.Add(Tuple.Create<string, Func<List<OutSystems.Model.Logic.Nodes.IActionNode>>, Func<OutSystems.Model.Logic.Nodes.IActionNode>>("Server " + aa.Name, () => aa.Nodes.ToList(), () => aa.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>())); }
    foreach (var a in eSpace.ClientActions) { var aa = a; flows.Add(Tuple.Create<string, Func<List<OutSystems.Model.Logic.Nodes.IActionNode>>, Func<OutSystems.Model.Logic.Nodes.IActionNode>>("Client " + aa.Name, () => aa.Nodes.ToList(), () => aa.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>())); }
    foreach (var f in eSpace.MobileFlows)
        foreach (var n in f.Nodes) {
            var blk = n as OutSystems.Model.UI.Mobile.IMobileBlock;
            var scr = n as OutSystems.Model.UI.Mobile.IMobileScreen;
            if (blk == null && scr == null) continue;
            var owner = f.Name + "/" + (blk != null ? blk.Name : scr.Name);
            foreach (var a in (blk != null ? blk.ScreenActions : scr.ScreenActions)) { var aa = a; flows.Add(Tuple.Create<string, Func<List<OutSystems.Model.Logic.Nodes.IActionNode>>, Func<OutSystems.Model.Logic.Nodes.IActionNode>>(owner + " " + aa.Name, () => aa.Nodes.ToList(), () => aa.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>())); }
            foreach (var a in (blk != null ? blk.DataActions : scr.DataActions)) { var aa = a; flows.Add(Tuple.Create<string, Func<List<OutSystems.Model.Logic.Nodes.IActionNode>>, Func<OutSystems.Model.Logic.Nodes.IActionNode>>(owner + " data " + aa.Name, () => aa.Nodes.ToList(), () => aa.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>())); }
        }
    var found = 0;
    foreach (var fl in flows) {
        if (!targets.Contains(fl.Item1)) continue;
        layoutFlow(fl.Item2(), fl.Item3);
        found++;
    }
    if (found != targets.Length) throw new Exception("Found " + found + " of " + targets.Length + " target flows: check the names");
}
```

## Save 4 · 2026-10-15 16:57:20 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
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
```

## Save 5 · 2026-10-15 16:57:53 UTC

- **Published:** 2026-10-15 17:09:41 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved with 1 validation error(s)
- **Imports:** -
- **Error:** /ClaudeSheet - Invalid Icon Library (expected sidecar error on theme stylesheet edits; Service Studio validation clean)

```csharp
eSpace => {
    var theme = eSpace.MobileThemes.Named("ClaudeSheet");
    if (theme.StyleSheet.Contains(".sheet-week")) throw new Exception("sheet-week CSS already present");
    var css =
        "\n.sheet-week-start { }\n\n" +
        "/* MySheet - WeeklyProjectHours block (US4) */\n" +
        ".sheet-week { width: 100%; }\n" +
        ".sheet-week--loading .sheet-week__body { opacity: .4; }\n" +
        ".sheet-week__table { display: flex; flex-direction: column; gap: 8px; }\n" +
        ".sheet-week__body, .sheet-week__rows { display: flex; flex-direction: column; gap: 8px; }\n" +
        ".sheet-week__row { display: grid; grid-template-columns: minmax(200px, 2fr) repeat(7, minmax(64px, 1fr)) minmax(110px, 1fr); align-items: center; column-gap: 8px; }\n" +
        ".sheet-week__contents { display: contents; }\n" +
        ".sheet-week__head { padding: 0 16px; color: var(--color-neutral-8); }\n" +
        ".sheet-week__card { padding: 12px 16px; border-radius: 12px; background-color: var(--sheet-surface-dark); color: #FFFFFF; }\n" +
        ".sheet-week__label { display: flex; align-items: center; justify-content: space-between; gap: 8px; min-width: 0; }\n" +
        ".sheet-week__label--project { flex-direction: column; align-items: flex-start; justify-content: center; gap: 2px; }\n" +
        ".sheet-week__title { font-size: 16px; font-weight: 700; }\n" +
        ".sheet-week__client { font-weight: 700; }\n" +
        ".sheet-week__project { font-size: 13px; color: var(--sheet-muted); }\n" +
        ".sheet-week__day { display: flex; flex-direction: column; align-items: center; font-size: 13px; line-height: 1.3; }\n" +
        ".sheet-week__day-name { font-weight: 600; }\n" +
        ".sheet-week__day.is-weekend { color: var(--sheet-muted); }\n" +
        ".sheet-week__day.is-today { color: var(--sheet-accent); }\n" +
        ".sheet-week__total-head { display: flex; align-items: center; gap: 8px; font-size: 13px; font-weight: 600; }\n" +
        ".sheet-week__arrow { display: inline-flex; align-items: center; justify-content: center; flex: 0 0 auto; width: 32px; height: 32px; padding: 0; border: 1px solid var(--color-neutral-5); border-radius: 6px; background: transparent; color: inherit; cursor: pointer; }\n" +
        ".sheet-week__arrow:hover { background-color: var(--color-neutral-3); }\n" +
        ".sheet-week__arrow:focus-visible { outline: 2px solid var(--sheet-accent); outline-offset: 2px; }\n" +
        ".sheet-week__cell { text-align: center; font-variant-numeric: tabular-nums; }\n" +
        ".sheet-week__cell.is-weekend { color: var(--sheet-muted); }\n" +
        ".sheet-week__total { font-weight: 700; }\n" +
        ".sheet-week__totals { font-weight: 700; }\n" +
        ".sheet-week__empty { padding: 24px; border-radius: 12px; background-color: var(--sheet-surface-dark); color: var(--sheet-muted); text-align: center; }\n" +
        "@media (max-width: 1023px) {\n" +
        "    .sheet-week__table { min-width: 960px; }\n" +
        "    .sheet-week__label { position: sticky; left: 0; z-index: 1; }\n" +
        "    .sheet-week__card .sheet-week__label { background-color: var(--sheet-surface-dark); }\n" +
        "    .sheet-week__head .sheet-week__label { background-color: var(--sheet-bg); }\n" +
        "}\n";
    theme.StyleSheet = theme.StyleSheet + css;
}
```
