(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating ISiteProperty 'DailyExpectedMinutes' ***/
    var dailyExpectedMinutes = eSpace.CreateSiteProperty(false, "DailyExpectedMinutes");
    dailyExpectedMinutes.DataType = eSpace.IntegerType;
    dailyExpectedMinutes.SetDefaultValue("480");
    dailyExpectedMinutes.Description = "Minutes a user is expected to record per working day.";
    dailyExpectedMinutes.Folder = null; // default value
    dailyExpectedMinutes.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    dailyExpectedMinutes.IsSecret = false; // default value
    
    /*** creating ISiteProperty 'SeedAdminUsernames' ***/
    var seedAdminUsernames = eSpace.CreateSiteProperty(false, "SeedAdminUsernames");
    seedAdminUsernames.DataType = eSpace.TextType;
    seedAdminUsernames.SetDefaultValue("\"\"");
    seedAdminUsernames.Description = "Comma-separated usernames that the Bootstrap timer grants SheetAdmin and SheetUser, so a first admin exists. Leave empty once admins are managed in the app.";
    seedAdminUsernames.Folder = null; // default value
    seedAdminUsernames.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    seedAdminUsernames.IsSecret = false; // default value
    
    /*** creating ISiteProperty 'SeedTestUsernames' ***/
    var seedTestUsernames = eSpace.CreateSiteProperty(false, "SeedTestUsernames");
    seedTestUsernames.DataType = eSpace.TextType;
    seedTestUsernames.SetDefaultValue("\"\"");
    seedTestUsernames.Description = "Comma-separated usernames that the Bootstrap timer grants SheetUser and assigns to the seed project. Leave empty outside test environments.";
    seedTestUsernames.Folder = null; // default value
    seedTestUsernames.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    seedTestUsernames.IsSecret = false; // default value
}
