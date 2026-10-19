# Client variables

Module-wide read in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IClientVariable 'LastURL' ***/
    var lastURL = eSpace.CreateClientVariable("LastURL");
    lastURL.DataType = eSpace.TextType;
    
    /*** creating IClientVariable 'Username' ***/
    var username = eSpace.CreateClientVariable("Username");
    username.DataType = eSpace.TextType;
}
```
