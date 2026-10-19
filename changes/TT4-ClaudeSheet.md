# TT4 · ClaudeSheet changes

Every `applyModelApiCode` save for TT4 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-15 18:31:59 UTC

- **Published:** 2026-10-15 21:59:00 UTC (manual 1-Click Publish; logged by Claude, applied by the ClaudeSheet subagent)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    var s = "";
    var cs = eSpace.References.Single(r => r.Name == "ClaudeSheet_CS");
    // P10a: pair every local backend element with its ClaudeSheet_CS copy ("2" suffix)
    var pairs = new List<Tuple<IObjectSignature, IObjectSignature>>();
    foreach (var a in eSpace.ServerActions.ToList()) { var t = cs.ServerActions.SingleOrDefault(x => x.Name == a.Name + "2"); if (t != null) pairs.Add(Tuple.Create((IObjectSignature)a, (IObjectSignature)t)); }
    foreach (var a in eSpace.Structures.ToList()) { var t = cs.Structures.SingleOrDefault(x => x.Name == a.Name + "2"); if (t != null) pairs.Add(Tuple.Create((IObjectSignature)a, (IObjectSignature)t)); }
    foreach (var a in eSpace.Entities.ToList()) { var t = cs.Entities.SingleOrDefault(x => x.Name == a.Name + "2"); if (t != null) pairs.Add(Tuple.Create((IObjectSignature)a, (IObjectSignature)t)); }
    var tpairs = new List<Tuple<IObjectSignature, IObjectSignature>>();
    foreach (var p in pairs) {
        if (p.Item1 is OutSystems.Model.Types.ITypeSignature)
            tpairs.Add(Tuple.Create((IObjectSignature)eSpace.GetOrCreateListType((OutSystems.Model.Types.ITypeSignature)p.Item1), (IObjectSignature)eSpace.GetOrCreateListType((OutSystems.Model.Types.ITypeSignature)p.Item2)));
        var e1 = p.Item1 as OutSystems.Model.Data.IEntitySignature; var e2 = p.Item2 as OutSystems.Model.Data.IEntitySignature;
        if (e1 != null && e2 != null) tpairs.Add(Tuple.Create((IObjectSignature)e1.IdentifierType, (IObjectSignature)e2.IdentifierType));
    }
    pairs.InsertRange(0, tpairs);
    s += "pairs " + pairs.Count + "\n";
    // P10b: replace the references in every UI flow object (blocks, screens, their actions and widgets) and client action
    var objs = new List<IObjectSignature>();
    foreach (var f in eSpace.MobileFlows) objs.AddRange(f.GetAllDescendantsOfType<IObjectSignature>());
    foreach (var c in eSpace.ClientActions) objs.AddRange(c.GetAllDescendantsOfType<IObjectSignature>());
    int fails = 0;
    foreach (var o in objs) foreach (var p in pairs) {
        try { o.ReplaceReferences(p.Item1, p.Item2); } catch (Exception ex) { if (fails++ < 5) s += "FAIL " + o + ": " + ex.Message + "\n"; }
    }
    s += "objs " + objs.Count + " fails " + fails + "\n";
    // P10c: HoursCalendar > GetCalendar: Site.DailyExpectedMinutes -> Settings_GetDailyExpectedMinutes
    var hc = eSpace.GetAllDescendantsOfType<IMobileBlock>().Single(b => b.Name == "HoursCalendar");
    var gc = hc.GetAllDescendantsOfType<OutSystems.Model.UI.Mobile.IDataAction>().Single(d => d.Name == "GetCalendar");
    var sumNode = gc.Nodes.OfType<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>().Single(n => n.Name.StartsWith("Calendar_GetMonthSummary"));
    var asg = (OutSystems.Model.Logic.Nodes.IAssignNode)sumNode.Target;
    int y0 = asg.VerticalPosition;
    foreach (var n in gc.Nodes.ToList()) if (n.VerticalPosition >= y0) n.VerticalPosition = n.VerticalPosition + 1371;
    var setAct = cs.ServerActions.Single(a => a.Name == "Settings_GetDailyExpectedMinutes");
    var setNode = gc.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Settings_GetDailyExpectedMinutes");
    setNode.Action = setAct;
    setNode.HorizontalPosition = sumNode.HorizontalPosition;
    setNode.VerticalPosition = y0;
    sumNode.Target = setNode;
    setNode.Target = asg;
    int changed = 0;
    foreach (var a in asg.Assignments) {
        var v = a.Value.ToString(); var cut = v.IndexOf(" ["); if (cut >= 0) v = v.Substring(0, cut);
        if (v == "Site.DailyExpectedMinutes") { a.SetValue(setNode.Name + ".Minutes"); changed++; }
    }
    s += "setNode " + setNode.Name + " at " + setNode.HorizontalPosition + "," + setNode.VerticalPosition + " assignments changed " + changed + "\n";
    gc.Description = "Calendar cells (leading blank cells with DayNumber = 0, then one CalendarDay per day), the month summary (PendingCount) and the daily expected minutes (Settings_GetDailyExpectedMinutes).";

    // P11: delete the local backend elements
    foreach (var t in eSpace.Timers.ToList()) t.Delete();
    foreach (var a in eSpace.ServerActions.ToList()) if (a.Name != "DoLogin" && a.Name != "DoLogout") a.Delete();
    foreach (var a in eSpace.Structures.ToList()) a.Delete();
    foreach (var a in eSpace.Entities.ToList()) a.Delete();
    foreach (var a in eSpace.UserExceptions.ToList()) a.Delete();
    foreach (var a in eSpace.SiteProperties.ToList()) if (a.Name == "DailyExpectedMinutes" || a.Name == "SeedTestUsernames") a.Delete();
    foreach (var a in eSpace.Roles.ToList()) if (a.Name == "SheetUser") a.Delete();
}
```

## Save 2 · 2026-10-15 18:34:36 UTC

- **Published:** 2026-10-15 21:59:00 UTC (manual 1-Click Publish; logged by Claude, applied by the ClaudeSheet subagent)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    var hc = eSpace.GetAllDescendantsOfType<IMobileBlock>().Single(b => b.Name == "HoursCalendar");
    var act = hc.GetAllDescendantsOfType<OutSystems.Model.UI.Mobile.IScreenAction>().Single(a => a.Name == "OnClick_FillPendingButton");
    act.Description = "Loads the projects assigned today and opens FillPendingPopup with the time defaulting to the daily expected minutes (GetCalendar.DailyExpectedMinutes, from Settings_GetDailyExpectedMinutes).";
}
```

## Save 3 · 2026-10-15 20:15:39 UTC

- **Published:** 2026-10-15 21:59:00 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    // TT4 cleanup: ClaudeSheet's local names for the ClaudeSheet_CS elements got a "2" suffix while
    // ClaudeSheet still had its own copies. The copies are gone, so drop the suffix (references and the Execute nodes named after them).
    var cs = eSpace.References.Single(r => r.Name == "ClaudeSheet_CS");
    var renamed = new List<string>();
    Func<string, string> baseName = n => n.EndsWith("2") ? n.Substring(0, n.Length - 1) : n;
    foreach (var a in cs.ServerActions.ToList()) if (a.Name.EndsWith("2")) { var b = baseName(a.Name); a.Name = b; renamed.Add(a.Name); }
    foreach (var st in cs.Structures.ToList()) if (st.Name.EndsWith("2")) { var b = baseName(st.Name); st.Name = b; renamed.Add(st.Name); }
    foreach (var en in cs.Entities.ToList()) if (en.Name.EndsWith("2")) { var b = baseName(en.Name); en.Name = b; renamed.Add(en.Name); }
    // Execute Server Action nodes named "<Action>2" -> "<Action>"
    var nodesRenamed = 0;
    foreach (var f in eSpace.MobileFlows)
        foreach (var n in f.Nodes) {
            var blk = n as OutSystems.Model.UI.Mobile.IMobileBlock;
            if (blk == null) continue;
            var flows = blk.ScreenActions.Select(x => x.Nodes).Concat(blk.DataActions.Select(x => x.Nodes));
            foreach (var nodes in flows)
                foreach (var x in nodes.OfType<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>())
                    if (x.Action != null && x.Name == x.Action.Name + "2") { x.Name = x.Action.Name; nodesRenamed++; }
        }
    if (renamed.Count != 28) throw new Exception("Expected 28 renamed references, got " + renamed.Count + ": " + string.Join(", ", renamed));
}
```
