// Lays out the named action flows following docs/conventions.md › Flow layout, and SAVES the result (run it with applyModelApiCode).
// Main path vertical from Start: at an If the taller branch goes down and the shorter one (e.g. an error) leaves horizontally to the
// first free column in the rows it uses; an optional step that rejoins goes right; loops (For Each, or an If branch that leads back to
// its If) put the body to the right, far enough for the loop-back to clear it, with the continuation below the body; a branch that
// would draw back to a shared End gets its own End node;
// exception handlers in their own columns; unconnected nodes (comments, orphans) stacked in a last column. Only positions change,
// plus new End nodes (not a logic change). Merges into a non-End node stay diagonal: review them by hand.
// Set targets to the flow names as printed by queries/flow-layout.cs, e.g. "Server DaySheet_Save", "SheetBlocks/HoursCalendar OnInitialize".
// Remove these // header lines before sending. Then run queries/flow-layout.cs to check the result.
eSpace => {
    var targets = new[] { "Server DaySheet_Save" };
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
