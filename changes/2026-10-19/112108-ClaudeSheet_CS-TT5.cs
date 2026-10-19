// Published: 2026-10-19T11:32:00+00:00 (manual 1-Click Publish; logged by Claude, applied by the main session)
// Module:   ClaudeSheet_CS
// When:     2026-10-19T11:21:08+00:00
// Story:    TT5
// Outcome:  saved
// Imports:  -
eSpace => {
    var targets = new[] { "Server Bootstrap" };
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
        place = (n, c, r) => {
            setPos(n, c, r);
            var fe = n as OutSystems.Model.Logic.Nodes.IForEachNode;
            var ss = succ(n);
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
                if (reaches(tF, tT) && tF != tT) swap = false;
                else if (reaches(tT, tF) && tF != tT) swap = true;
                else swap = hF > hT;
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
    var b = eSpace.ServerActions.Named("Bootstrap");
    if (!targets.Contains("Server " + b.Name)) throw new Exception("target");
    layoutFlow(b.Nodes.ToList(), () => b.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>());
}
