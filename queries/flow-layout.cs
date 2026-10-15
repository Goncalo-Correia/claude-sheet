// Flow layout report for every action flow of the open module (server, client, screen/block and data actions).
// Rules in docs/conventions.md › Flow layout. Per flow: vertical / horizontal connectors, loop-back diagonals (allowed),
// merge diagonals (allowed exception, must be justified), other diagonals, crossings, and overlaps (two nodes too close, or a
// connector passing through a node; not allowed). A loop-back is any back-edge: For Each body or a loop built with an If.
// Run it as a throw-probe: send it as the code of applyModelApiCode with these // header lines removed. It always throws,
// so nothing is saved; the report is in exceptionMessage. Set onlyFlows to limit the report (names as printed, e.g. "Server DaySheet_Save").
eSpace => {
    var onlyFlows = new string[] { };
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
    var rep = "";
    int[] tot = new int[7];
    var bad = 0;
    foreach (var fl in flows) {
        if (onlyFlows.Length > 0 && !onlyFlows.Contains(fl.Item1)) continue;
        var m = measure(fl.Item2());
        for (var i = 0; i < 7; i++) tot[i] += m[i];
        if (m[3] + m[4] + m[5] + m[6] > 0) {
            if (m[4] + m[5] + m[6] > 0) bad++;
            rep += fl.Item1 + ": merge " + m[3] + ", other diagonal " + m[4] + ", crossings " + m[5] + ", overlaps " + m[6] + "\n";
        }
    }
    throw new Exception("PROBE:" + flows.Count + " flows, totals: vertical " + tot[0] + ", horizontal " + tot[1] + ", loop-back " + tot[2] + ", merge " + tot[3]
        + ", other diagonal " + tot[4] + ", crossings " + tot[5] + ", overlaps " + tot[6] + "\nflows breaking the rules: " + bad + "\n" + (rep == "" ? "all flows follow the layout rules" : rep));
}
