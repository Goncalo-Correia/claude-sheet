// Published: 2026-10-15T16:33:52.760370+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T16:12:09.908681+00:00
// Story:    TT2
// Outcome:  saved
// Imports:  -
eSpace => {
    // TT2 P04/P05: re-apply the improved layout (taller branch down, range-aware columns) to the 15 flows that break the Flow layout rules (queries/flow-layout.cs, 2026-10-15)
    var targets = new[] {
        "Server DaySheet_Save", "Server Bootstrap", "Server DaySheet_FillPending", "Server DaySheet_Clear", "Server Calendar_GetMonth",
        "Server DoLogout", "Server Holiday_CreateIfMissing", "Server DaySheet_GetDetail", "Server Calendar_GetMonthSummary",
        "Server Project_GetAssignedToUser", "SheetBlocks/HoursCalendar OnInitialize", "SheetBlocks/HoursCalendar OnClick_ConfirmFillButton",
        "SheetBlocks/HoursCalendar OnClick_DayCell", "SheetBlocks/DayStatusKPIs OnAfterFetch_GetKPIs", "SheetBlocks/TotalHoursMonth OnAfterFetch_GetSummary" };
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
            if (fe != null) {
                // body first (to the right, same row), then the continuation below the body's lowest node
                var before = new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>(row.Keys);
                var body = ss[1];
                var bt = body.Item1();
                if (bt != null && !row.ContainsKey(bt)) place(bt, freeCol(bt, r), r);
                var bottom = r;
                foreach (var k in row.Keys) if (!before.Contains(k) && row[k] > bottom) bottom = row[k];
                var dt = ss[0].Item1();
                if (dt != null) {
                    if (!row.ContainsKey(dt)) place(dt, c, bottom + 1);
                    else if (dt is OutSystems.Model.Logic.Nodes.IEndNode) newEnd(ss[0].Item2, c, bottom + 1);
                }
                return;
            }
            var iff2 = n as OutSystems.Model.Logic.Nodes.IIfNode;
            if (iff2 != null && ss.Count == 2) {
                var hT = height(ss[0].Item1(), new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>());
                var hF = height(ss[1].Item1(), new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>());
                if (hF > hT) ss = new List<Tuple<Func<OutSystems.Model.Logic.Nodes.IActionNode>, Action<OutSystems.Model.Logic.Nodes.IActionNode>, bool>> {
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
                    if (fe != null && fe.NodesWithinCycle.Contains(n)) dLoop++;
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
    // TT2 P03: same connectors before/after; the only allowed change is a connector moved from an End to a new End
    var found = 0; var newEnds = 0; var report = "";
    foreach (var fl in flows) {
        if (!targets.Contains(fl.Item1)) continue;
        found++;
        var before = new List<Tuple<OutSystems.Model.Logic.Nodes.IActionNode, OutSystems.Model.Logic.IActionConnector, OutSystems.Model.Logic.Nodes.IActionNode>>();
        var oldNodes = new HashSet<OutSystems.Model.Logic.Nodes.IActionNode>(fl.Item2());
        foreach (var n in fl.Item2()) foreach (var c in n.Connectors) before.Add(Tuple.Create(n, c, c.Target));
        var added = layoutFlow(fl.Item2(), fl.Item3);
        newEnds += added;
        foreach (var b in before) {
            var now = b.Item2.Target;
            if (now == b.Item3) continue;
            var ok = b.Item3 is OutSystems.Model.Logic.Nodes.IEndNode && now is OutSystems.Model.Logic.Nodes.IEndNode && !oldNodes.Contains(now);
            if (!ok) throw new Exception("Connector changed in " + fl.Item1);
        }
        var m = measure(fl.Item2());
        report += fl.Item1 + ": +" + added + " End, merge " + m[3] + ", other diagonal " + m[4] + ", crossings " + m[5] + ", overlaps " + m[6] + "\n";
    }
    if (found != targets.Length) throw new Exception("Found " + found + " of " + targets.Length + " target flows");
}
