# SheetUser

Role in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IAppRole 'SheetUser' ***/
    var sheetUser = eSpace.CreateRole("SheetUser");
    sheetUser.Description = "Users who record their time in MySheet.";
    sheetUser.Public = true;
    sheetUser.Shareable = true;
}
```
