// Published: 2026-10-15T15:37:48.393172+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T15:03:41.487411+00:00
// Story:    TT1
// Outcome:  saved
// Imports:  -
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
