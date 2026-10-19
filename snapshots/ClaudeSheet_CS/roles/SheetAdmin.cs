(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IAppRole 'SheetAdmin' ***/
    var sheetAdmin = eSpace.CreateRole("SheetAdmin");
    sheetAdmin.Description = "Manages clients, projects, assignments, holidays, users and settings.";
    sheetAdmin.Folder = null; // default value
    sheetAdmin.Public = true;
    sheetAdmin.Shareable = true;
}
