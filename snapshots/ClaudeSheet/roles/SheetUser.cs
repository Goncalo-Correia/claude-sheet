(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IAppRole 'SheetUser' ***/
    var sheetUser = eSpace.CreateRole("SheetUser");
    sheetUser.Description = "Users who record their time in MySheet.";
    sheetUser.Folder = null; // default value
    sheetUser.Public = false; // default value
    sheetUser.Shareable = false; // default value
}
