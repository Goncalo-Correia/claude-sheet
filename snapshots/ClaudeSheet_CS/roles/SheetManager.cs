(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IAppRole 'SheetManager' ***/
    var sheetManager = eSpace.CreateRole("SheetManager");
    sheetManager.Description = "Sees and approves the team\'s sheets and runs reports.";
    sheetManager.Folder = null; // default value
    sheetManager.Public = true;
    sheetManager.Shareable = true;
}
