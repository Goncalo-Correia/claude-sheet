// Published: 2026-10-15T18:13:37.734328+00:00 (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
// Module:   ClaudeSheet_CS
// When:     2026-10-15T17:50:23+00:00
// Story:    TT4
// Outcome:  saved
// Imports:  -
eSpace => {
    var dailyExpectedMinutes = eSpace.CreateSiteProperty(false, "DailyExpectedMinutes");
    dailyExpectedMinutes.DataType = eSpace.IntegerType;
    dailyExpectedMinutes.SetDefaultValue("480");
    dailyExpectedMinutes.Description = "Minutes a user is expected to record per working day.";
    var seedTestUsernames = eSpace.CreateSiteProperty(false, "SeedTestUsernames");
    seedTestUsernames.DataType = eSpace.TextType;
    seedTestUsernames.SetDefaultValue("\"\"");
    seedTestUsernames.Description = "Comma-separated usernames that the Bootstrap timer grants SheetUser and assigns to the seed project. Leave empty outside test environments.";
    var sheetUser = eSpace.CreateRole("SheetUser");
    sheetUser.Description = "Users who record their time in MySheet.";
    sheetUser.Public = true;
    var daySheetValidation = eSpace.CreateUserException("DaySheetValidation");
}
