// Published: 2026-10-15T15:37:48.393172+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T15:03:06.757564+00:00
// Story:    TT1
// Outcome:  saved
// Imports:  -
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
