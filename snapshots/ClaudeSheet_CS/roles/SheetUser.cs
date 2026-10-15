(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IAppRole 'SheetUser' ***/
    var sheetUser = eSpace.CreateRole("SheetUser");
    sheetUser.Description = "Users who record their time in MySheet.";
    sheetUser.Folder = null; // default value
    sheetUser.Public = true;
    sheetUser.Shareable = true;
}
