# Settings

Module-wide read in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating ISiteProperty 'DailyExpectedMinutes' ***/
    var dailyExpectedMinutes = eSpace.CreateSiteProperty(false, "DailyExpectedMinutes");
    dailyExpectedMinutes.DataType = eSpace.IntegerType;
    dailyExpectedMinutes.SetDefaultValue("480");
    dailyExpectedMinutes.Description = "Minutes a user is expected to record per working day.";
    
    /*** creating ISiteProperty 'SeedAdminUsernames' ***/
    var seedAdminUsernames = eSpace.CreateSiteProperty(false, "SeedAdminUsernames");
    seedAdminUsernames.DataType = eSpace.TextType;
    seedAdminUsernames.SetDefaultValue("\"\"");
    seedAdminUsernames.Description = "Comma-separated usernames that the Bootstrap timer grants SheetAdmin and SheetUser, so a first admin exists. Leave empty once admins are managed in the app.";
    
    /*** creating ISiteProperty 'SeedTestUsernames' ***/
    var seedTestUsernames = eSpace.CreateSiteProperty(false, "SeedTestUsernames");
    seedTestUsernames.DataType = eSpace.TextType;
    seedTestUsernames.SetDefaultValue("\"\"");
    seedTestUsernames.Description = "Comma-separated usernames that the Bootstrap timer grants SheetUser and assigns to the seed project. Leave empty outside test environments.";
}
```
