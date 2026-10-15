// Published: 2026-10-15T15:37:48.393172+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T15:04:18.225797+00:00
// Story:    TT1
// Outcome:  saved
// Imports:  -
eSpace => {
    // TT1 P08: re-lay out DaySheet_Save and HoursCalendar.OnInitialize on the standard grid (1828 x 1371), positions only.
    var report = "";
    Action<string, IEnumerable<OutSystems.Model.Logic.Nodes.IActionNode>> layout = (name, nodesEnum) => {
        var nodes = nodesEnum.ToList();
        var targetsBefore = nodes.ToDictionary(n => n, n => string.Join(",", n.Connectors.Select(c => c.Target == null ? "-" : c.Target.GetHashCode().ToString())));
        var start = nodes.OfType<OutSystems.Model.Logic.Nodes.IStartNode>().Single();
        var depth = new Dictionary<OutSystems.Model.Logic.Nodes.IActionNode, int>();
        var col = new Dictionary<OutSystems.Model.Logic.Nodes.IActionNode, int>();
        var order = new List<OutSystems.Model.Logic.Nodes.IActionNode>();
        var queue = new Queue<OutSystems.Model.Logic.Nodes.IActionNode>();
        depth[start] = 0; col[start] = 0; queue.Enqueue(start); order.Add(start);
        while (queue.Count > 0) {
            var cur = queue.Dequeue();
            var loop = cur as OutSystems.Model.Logic.Nodes.IForEachNode;
            var iff = cur as OutSystems.Model.Logic.Nodes.IIfNode;
            foreach (var connector in cur.Connectors) {
                var t = connector.Target;
                if (t == null || depth.ContainsKey(t)) continue;
                bool side = (loop != null && t == loop.CycleTarget) || (iff != null && t == iff.FalseTarget);
                depth[t] = (side && loop != null) ? depth[cur] : depth[cur] + 1;
                col[t] = col[cur] + (side ? 1 : 0);
                queue.Enqueue(t); order.Add(t);
            }
        }
        int nextCol = col.Values.Max() + 2;
        foreach (var handler in nodes.OfType<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>()) {
            OutSystems.Model.Logic.Nodes.IActionNode cur = handler;
            int row = 0;
            while (cur != null && !depth.ContainsKey(cur)) {
                depth[cur] = row++; col[cur] = nextCol; order.Add(cur);
                cur = cur.Connectors.Select(c => c.Target).FirstOrDefault();
            }
            nextCol++;
        }
        // collision resolution: first come keeps the cell, later nodes move right
        var taken = new HashSet<string>();
        foreach (var n in order) {
            while (taken.Contains(col[n] + ":" + depth[n])) col[n]++;
            taken.Add(col[n] + ":" + depth[n]);
            n.HorizontalPosition = 3200 + col[n] * 1828;
            n.VerticalPosition = 800 + depth[n] * 1371;
        }
        var unreached = nodes.Count(n => !depth.ContainsKey(n));
        foreach (var n in nodes)
            if (targetsBefore[n] != string.Join(",", n.Connectors.Select(c => c.Target == null ? "-" : c.Target.GetHashCode().ToString())))
                throw new Exception("Connector changed in " + name);
        report += name + ": " + order.Count + " nodes placed, " + unreached + " unreached\n";
        if (unreached > 0) throw new Exception(name + " has unreached nodes");
    };

    layout("DaySheet_Save", eSpace.ServerActions.Named("DaySheet_Save").Nodes);
    var block = eSpace.MobileFlows.Named("SheetBlocks").Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Single(b => b.Name == "HoursCalendar");
    layout("HoursCalendar.OnInitialize", block.ScreenActions.Single(a => a.Name == "OnInitialize").Nodes);
}
