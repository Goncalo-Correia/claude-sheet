// Published: 2026-10-15T21:59:00.172231+00:00 (manual 1-Click Publish; logged by Claude, applied by the ClaudeSheet subagent)
// Module:   ClaudeSheet
// When:     2026-10-15T18:34:36+00:00
// Story:    TT4
// Outcome:  saved
// Imports:  -
eSpace => {
    var hc = eSpace.GetAllDescendantsOfType<IMobileBlock>().Single(b => b.Name == "HoursCalendar");
    var act = hc.GetAllDescendantsOfType<OutSystems.Model.UI.Mobile.IScreenAction>().Single(a => a.Name == "OnClick_FillPendingButton");
    act.Description = "Loads the projects assigned today and opens FillPendingPopup with the time defaulting to the daily expected minutes (GetCalendar.DailyExpectedMinutes, from Settings_GetDailyExpectedMinutes).";
}
