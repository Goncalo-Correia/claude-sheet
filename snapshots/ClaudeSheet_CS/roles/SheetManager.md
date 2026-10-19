# SheetManager

Role in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IAppRole 'SheetManager' ***/
    var sheetManager = eSpace.CreateRole("SheetManager");
    sheetManager.Description = "Sees and approves the team\'s sheets and runs reports.";
    sheetManager.Public = true;
    sheetManager.Shareable = true;
}
```
