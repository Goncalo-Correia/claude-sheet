// Lists overlapping nodes in every action flow of the open module (server, client, screen/block and data actions).
// Two nodes overlap when they are less than 900 units apart both horizontally and vertically (see docs/conventions.md).
// Run it as a throw-probe: pass it as the code of applyModelApiCode. It always throws, so nothing is saved;
// the result is in exceptionMessage ("PROBE:flows=N" then one line per overlapping pair, or "no overlaps").
eSpace => {
    var s = "";
    var total = 0;
    Action<string, IEnumerable<OutSystems.Model.Logic.Nodes.IActionNode>> check = (name, nodes) => {
        var list = nodes.ToList();
        total++;
        for (var i = 0; i < list.Count; i++)
            for (var j = i + 1; j < list.Count; j++) {
                var a = list[i]; var b = list[j];
                var dx = Math.Abs(a.HorizontalPosition - b.HorizontalPosition);
                var dy = Math.Abs(a.VerticalPosition - b.VerticalPosition);
                if (dx < 900 && dy < 900)
                    s += name + ": " + a.GetType().Name.Replace("Node", "") + "(" + a.HorizontalPosition + "," + a.VerticalPosition + ") ~ " + b.GetType().Name.Replace("Node", "") + "(" + b.HorizontalPosition + "," + b.VerticalPosition + ")\n";
            }
    };
    foreach (var a in eSpace.ServerActions) check("Server " + a.Name, a.Nodes);
    foreach (var a in eSpace.ClientActions) check("Client " + a.Name, a.Nodes);
    foreach (var f in eSpace.MobileFlows)
        foreach (var n in f.Nodes) {
            var blk = n as OutSystems.Model.UI.Mobile.IMobileBlock;
            var scr = n as OutSystems.Model.UI.Mobile.IMobileScreen;
            var sas = blk != null ? blk.ScreenActions : scr != null ? scr.ScreenActions : null;
            var das = blk != null ? blk.DataActions : scr != null ? scr.DataActions : null;
            if (sas != null) foreach (var a in sas) check(f.Name + "/" + (blk != null ? blk.Name : scr.Name) + " " + a.Name, a.Nodes);
            if (das != null) foreach (var a in das) check(f.Name + "/" + (blk != null ? blk.Name : scr.Name) + " data " + a.Name, a.Nodes);
        }
    throw new Exception("PROBE:flows=" + total + "\n" + (s == "" ? "no overlaps" : s));
}