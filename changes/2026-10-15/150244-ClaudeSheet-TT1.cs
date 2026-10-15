// Published: 2026-10-15T15:37:48.393172+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T15:02:44.709328+00:00
// Story:    TT1
// Outcome:  saved
// Imports:  -
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
