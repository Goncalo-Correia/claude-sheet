# Minutes_Format

Client action in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IClientAction 'Minutes_Format' ***/
    var minutes_Format = eSpace.CreateClientAction("Minutes_Format");
    minutes_Format.Description = "Formats a number of minutes as hours and minutes, e.g. 2250 -> \"37h30\" (Separator \"h\") or \"37:30\" (Separator \":\"). Minutes always have 2 digits.";
    var minutes = eSpace.Folders.Named("Minutes");
    minutes_Format.Folder = minutes;
    minutes_Format.Function = true;
    
    var minutes2 = minutes_Format.CreateInputParameter("Minutes");
    minutes2.DataType = eSpace.IntegerType;
    
    var separator = minutes_Format.CreateInputParameter("Separator");
    separator.DataType = eSpace.TextType;
    separator.Description = "Text between hours and minutes, e.g. \"h\" or \":\".";
    
    var startNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var assignNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.Label = "Format h:mm";
    
    var assignment = assignNode.CreateAssignment("Result", "If(Minutes < 0, \"-\", \"\") + IntegerToText(DecimalToInteger(Trunc(Abs(Minutes) / 60))) + Separator + If(Mod(Abs(Minutes), 60) < 10, \"0\", \"\") + IntegerToText(DecimalToInteger(Mod(Abs(Minutes), 60)))");
    
    var endNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    
    var result = minutes_Format.CreateOutputParameter("Result");
    result.DataType = eSpace.TextType;
}
```
