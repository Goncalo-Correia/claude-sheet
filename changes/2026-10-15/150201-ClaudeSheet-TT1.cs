// Published: 2026-10-15T15:37:48.393172+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T15:02:01.692868+00:00
// Story:    TT1
// Outcome:  saved
// Imports:  -
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
