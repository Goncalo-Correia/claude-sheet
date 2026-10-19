# TT1 · ClaudeSheet changes

Every `applyModelApiCode` save for TT1 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-15 15:02:01 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // TT1 P02: reusable server actions -> <Entity>_<Verb>[<Qualifier>]
    var map = new Dictionary<string, string> {
        { "ClearDaySheet", "DaySheet_Clear" },
        { "FillPendingDays", "DaySheet_FillPending" },
        { "GetDaySheetDetail", "DaySheet_GetDetail" },
        { "SaveDaySheet", "DaySheet_Save" },
        { "GetMonthCalendar", "Calendar_GetMonth" },
        { "GetMonthSummary", "Calendar_GetMonthSummary" },
        { "GetUserProjects", "Project_GetAssignedToUser" },
        { "FormatMinutes", "Minutes_Format" },
        { "GetWeekStart", "Date_GetWeekStart" }
    };
    foreach (var kv in map) {
        var a = eSpace.ServerActions.Named(kv.Key);
        a.Name = kv.Value;
        if (a.Name != kv.Value) throw new Exception("Rename of " + kv.Key + " gave " + a.Name);
    }
}
```

## Save 2 · 2026-10-15 15:02:25 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // TT1 P03: reusable client actions -> <Entity>_<Verb>
    var map = new Dictionary<string, string> { { "FormatMinutes", "Minutes_Format" }, { "GetWeekStart", "Date_GetWeekStart" } };
    foreach (var kv in map) {
        var a = eSpace.ClientActions.Named(kv.Key);
        a.Name = kv.Value;
        if (a.Name != kv.Value) throw new Exception("Rename of " + kv.Key + " gave " + a.Name);
    }
}
```

## Save 3 · 2026-10-15 15:02:44 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // TT1 P04: On After Fetch handlers -> OnAfterFetch_<DataSource>
    var flow = eSpace.MobileFlows.Named("SheetBlocks");
    var blocks = flow.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().ToList();
    var renames = new[] {
        new[] { "TotalHoursMonth", "GetSummaryOnAfterFetch", "OnAfterFetch_GetSummary" },
        new[] { "DayStatusKPIs", "GetKPIsOnAfterFetch", "OnAfterFetch_GetKPIs" }
    };
    foreach (var r in renames) {
        var a = blocks.Single(b => b.Name == r[0]).ScreenActions.Single(x => x.Name == r[1]);
        a.Name = r[2];
        if (a.Name != r[2]) throw new Exception("Rename of " + r[1] + " gave " + a.Name);
    }
}
```

## Save 4 · 2026-10-15 15:03:06 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // TT1 P05 + P06: HoursCalendar event handlers -> <Event>_<Target>, lifecycle -> event name, helper -> verb + noun
    var block = eSpace.MobileFlows.Named("SheetBlocks").Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Single(b => b.Name == "HoursCalendar");
    var map = new Dictionary<string, string> {
        { "PreviousMonthOnClick", "OnClick_PreviousMonth" },
        { "NextMonthOnClick", "OnClick_NextMonth" },
        { "DayOnClick", "OnClick_DayCell" },
        { "OpenFillPopup", "OnClick_FillPendingButton" },
        { "SaveDay", "OnClick_SaveDay" },
        { "ClearDay", "OnClick_ClearDayButton" },
        { "CloseDayEditor", "OnClick_CancelButton" },
        { "ConfirmFill", "OnClick_ConfirmFillButton" },
        { "CloseFillPopup", "OnClick_CancelFillButton" },
        { "BuildTimeOptions", "OnInitialize" },
        { "BindEscapeKey", "OnReady" },
        { "DayEditorSaved", "FinishDaySave" }
    };
    foreach (var kv in map) {
        var a = block.ScreenActions.Single(x => x.Name == kv.Key);
        a.Name = kv.Value;
        if (a.Name != kv.Value) throw new Exception("Rename of " + kv.Key + " gave " + a.Name);
    }
}
```

## Save 5 · 2026-10-15 15:03:41 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // TT1 P07: RecalcEditorTotal is bound to two OnChange events AND called by OpenDayEditor.
    // Split: new handler OnChange_EditorTime (bound to both dropdowns) calls the helper RecalcEditorTotal.
    var block = eSpace.MobileFlows.Named("SheetBlocks").Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Single(b => b.Name == "HoursCalendar");
    var recalc = block.ScreenActions.Single(x => x.Name == "RecalcEditorTotal");

    var handler = block.CreateScreenAction("OnChange_EditorTime");
    handler.Description = "OnChange of HoursDropdown and MinutesDropdown in the day editor: recomputes the live total of the day.";
    var start = handler.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    start.HorizontalPosition = 3200;
    start.VerticalPosition = 800;
    var call = handler.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RecalcEditorTotal").ConnectedBelow(start, 1371);
    call.Action = recalc;
    handler.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(call, 1371);

    var names = new[] { "HoursDropdown", "MinutesDropdown" };
    var dropdowns = block.GetAllDescendantsOfType<ServiceStudio.Plugin.NRWidgets.IDropdown>().Where(d => names.Contains(d.Name)).ToList();
    if (dropdowns.Count != 2) throw new Exception("Expected 2 dropdowns, found " + dropdowns.Count);
    foreach (var d in dropdowns) {
        if (d.OnChange.Destination != recalc) throw new Exception(d.Name + " OnChange is not RecalcEditorTotal");
        d.OnChange.Destination = handler;
    }
    if (handler.Name != "OnChange_EditorTime") throw new Exception("Handler named " + handler.Name);
}
```

## Save 6 · 2026-10-15 15:04:18 UTC

- **Published:** 2026-10-15 15:37:48 UTC (manual 1-Click Publish; logged by Claude, not the hook)
- **Outcome:** saved
- **Imports:** -

```csharp
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
```
