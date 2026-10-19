# SheetAdmin

Role in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IAppRole 'SheetAdmin' ***/
    var sheetAdmin = eSpace.CreateRole("SheetAdmin");
    sheetAdmin.Description = "Manages clients, projects, assignments, holidays, users and settings.";
    sheetAdmin.Public = true;
    sheetAdmin.Shareable = true;
}
```
