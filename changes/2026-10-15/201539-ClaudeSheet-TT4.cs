// Published: 2026-10-15T21:59:00.172231+00:00 (manual 1-Click Publish; logged by Claude, applied by the main session)
// Module:   ClaudeSheet
// When:     2026-10-15T20:15:39+00:00
// Story:    TT4
// Outcome:  saved
// Imports:  -
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
