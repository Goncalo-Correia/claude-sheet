# Action folders and node names report

**Report**: saves nothing.

## What it does

Checks the open module against `docs/conventions.md` › Action folders and Node names. It lists:

- reusable server and client actions that are not in a folder;
- per flow (server, client, screen/block and data actions), the Assign, If, Message, For Each and Switch nodes without a label;
- Execute Action nodes whose name ends in a digit.

## How to run it

Send the code block as the `code` of `applyModelApiCode`. It always throws, so nothing is saved; the report comes back in `exceptionMessage`.

## Passing

Every count is 0 before the merge.

## Code

```csharp
eSpace => {
    Func<OutSystems.Model.Logic.Nodes.IActionNode, bool> unlabelled = n =>
        (n is OutSystems.Model.Logic.Nodes.IAssignNode && string.IsNullOrWhiteSpace(((OutSystems.Model.Logic.Nodes.IAssignNode)n).Label)) ||
        (n is OutSystems.Model.Logic.Nodes.IIfNode && string.IsNullOrWhiteSpace(((OutSystems.Model.Logic.Nodes.IIfNode)n).Label)) ||
        (n is OutSystems.Model.Logic.Nodes.IMessageNode && string.IsNullOrWhiteSpace(((OutSystems.Model.Logic.Nodes.IMessageNode)n).Label)) ||
        (n is OutSystems.Model.Logic.Nodes.IForEachNode && string.IsNullOrWhiteSpace(((OutSystems.Model.Logic.Nodes.IForEachNode)n).Label)) ||
        (n is OutSystems.Model.Logic.Nodes.ISwitchNode && string.IsNullOrWhiteSpace(((OutSystems.Model.Logic.Nodes.ISwitchNode)n).Label));
    Func<string, bool> suffixed = s => !string.IsNullOrEmpty(s) && char.IsDigit(s[s.Length - 1]);
    Func<OutSystems.Model.Logic.Nodes.IActionNode, string> execName = n =>
        n is OutSystems.Model.Logic.Nodes.IExecuteServerActionNode ? ((OutSystems.Model.Logic.Nodes.IExecuteServerActionNode)n).Name :
        n is OutSystems.Model.Logic.Nodes.IExecuteClientActionNode ? ((OutSystems.Model.Logic.Nodes.IExecuteClientActionNode)n).Name : null;

    var flows = new List<Tuple<string, List<OutSystems.Model.Logic.Nodes.IActionNode>>>();
    foreach (var a in eSpace.ServerActions) flows.Add(Tuple.Create("Server " + a.Name, a.Nodes.ToList()));
    foreach (var a in eSpace.ClientActions) flows.Add(Tuple.Create("Client " + a.Name, a.Nodes.ToList()));
    foreach (var f in eSpace.MobileFlows)
        foreach (var n in f.Nodes) {
            var blk = n as OutSystems.Model.UI.Mobile.IMobileBlock;
            var scr = n as OutSystems.Model.UI.Mobile.IMobileScreen;
            if (blk == null && scr == null) continue;
            var owner = f.Name + "/" + (blk != null ? blk.Name : scr.Name);
            foreach (var a in (blk != null ? blk.ScreenActions : scr.ScreenActions)) flows.Add(Tuple.Create(owner + " " + a.Name, a.Nodes.ToList()));
            foreach (var a in (blk != null ? blk.DataActions : scr.DataActions)) flows.Add(Tuple.Create(owner + " data " + a.Name, a.Nodes.ToList()));
        }

    var noFolder = eSpace.ServerActions.Where(a => a.Folder == null).Select(a => "Server " + a.Name)
        .Concat(eSpace.ClientActions.Where(a => a.Folder == null).Select(a => "Client " + a.Name)).ToList();
    int totalUnlabelled = 0, totalSuffixed = 0; var rep = new List<string>();
    foreach (var fl in flows) {
        var u = fl.Item2.Count(unlabelled);
        var s = fl.Item2.Select(execName).Where(suffixed).ToList();
        totalUnlabelled += u; totalSuffixed += s.Count;
        if (u + s.Count > 0) rep.Add(fl.Item1 + ": unlabelled " + u + (s.Count > 0 ? ", suffixed " + string.Join("/", s) : ""));
    }
    throw new Exception("PROBE:" + flows.Count + " flows. Actions without folder: " + noFolder.Count + (noFolder.Count > 0 ? " (" + string.Join(", ", noFolder) + ")" : "")
        + "; unlabelled nodes: " + totalUnlabelled + "; suffixed Execute nodes: " + totalSuffixed + "\n"
        + (rep.Count == 0 ? "all flows follow the naming rules" : string.Join("\n", rep)));
}
```
