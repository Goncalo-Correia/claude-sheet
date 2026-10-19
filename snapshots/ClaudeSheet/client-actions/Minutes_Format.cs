(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IClientAction 'Minutes_Format' ***/
    var minutes_Format = eSpace.CreateClientAction("Minutes_Format");
    minutes_Format.Description = "Formats a number of minutes as hours and minutes, e.g. 2250 -> \"37h30\" (Separator \"h\") or \"37:30\" (Separator \":\"). Minutes always have 2 digits.";
    var minutes = eSpace.Folders.Named("Minutes");
    minutes_Format.Folder = minutes;
    minutes_Format.Function = true;
    minutes_Format.Icon = null; // default value
    minutes_Format.Public = false; // default value
    minutes_Format.Shareable = false; // default value
    
    var minutes2 = minutes_Format.CreateInputParameter("Minutes");
    minutes2.DataType = eSpace.IntegerType;
    minutes2.Description = null; // default value
    minutes2.IsMandatory = true; // default value
    
    var separator = minutes_Format.CreateInputParameter("Separator");
    separator.DataType = eSpace.TextType;
    separator.Description = "Text between hours and minutes, e.g. \"h\" or \":\".";
    separator.IsMandatory = true; // default value
    
    var startNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Format h:mm";
    
    var assignment = assignNode.CreateAssignment("Result", "If(Minutes < 0, \"-\", \"\") + IntegerToText(DecimalToInteger(Trunc(Abs(Minutes) / 60))) + Separator + If(Mod(Abs(Minutes), 60) < 10, \"0\", \"\") + IntegerToText(DecimalToInteger(Mod(Abs(Minutes), 60)))");
    
    var endNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var result = minutes_Format.CreateOutputParameter("Result");
    result.DataType = eSpace.TextType;
    result.SetDefaultValue(null); // default value
    result.Description = null; // default value
}
