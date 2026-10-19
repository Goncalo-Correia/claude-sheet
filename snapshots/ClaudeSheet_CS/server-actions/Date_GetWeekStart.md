# Date_GetWeekStart

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Date_GetWeekStart' ***/
    var date_GetWeekStart = eSpace.CreateServerAction("Date_GetWeekStart");
    date_GetWeekStart.Description = "Returns the Monday of the week that contains Date.";
    var date = eSpace.Folders.Named("Date");
    date_GetWeekStart.Folder = date;
    date_GetWeekStart.Function = true;
    
    var date2 = date_GetWeekStart.CreateInputParameter("Date");
    date2.DataType = eSpace.DateType;
    
    var startNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var assignNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.Label = "Monday of week";
    
    var assignment = assignNode.CreateAssignment("WeekStart", "DateTimeToDate(AddDays(Date, -DecimalToInteger(Mod(DayOfWeek(Date) + 6, 7))))");
    
    var endNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    
    var weekStart = date_GetWeekStart.CreateOutputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
}
```
