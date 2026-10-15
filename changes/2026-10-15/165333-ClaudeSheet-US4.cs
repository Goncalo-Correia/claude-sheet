// Published: 2026-10-15T17:09:41.123041+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T16:53:33.252447+00:00
// Story:    US4
// Outcome:  saved
// Imports:  -
eSpace => {
    // US4: renaming DayLabel -> DayName made Service Studio infer a Date type; it is the English day name (Text)
    var a = eSpace.Structures.Named("WeekDayHeader").Attributes.Named("DayName");
    a.DataType = eSpace.TextType;
    a.Length = 20;
}
