// Published: 2026-10-15T15:37:48.393172+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T15:02:25.135167+00:00
// Story:    TT1
// Outcome:  saved
// Imports:  -
eSpace => {
    // TT1 P03: reusable client actions -> <Entity>_<Verb>
    var map = new Dictionary<string, string> { { "FormatMinutes", "Minutes_Format" }, { "GetWeekStart", "Date_GetWeekStart" } };
    foreach (var kv in map) {
        var a = eSpace.ClientActions.Named(kv.Key);
        a.Name = kv.Value;
        if (a.Name != kv.Value) throw new Exception("Rename of " + kv.Key + " gave " + a.Name);
    }
}
