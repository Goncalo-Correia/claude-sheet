(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IClientAction 'FormatMinutes' ***/
    var formatMinutes = eSpace.CreateClientAction("FormatMinutes");
    formatMinutes.Description = "Formats a number of minutes as hours and minutes, e.g. 2250 -> \"37h30\" (Separator \"h\") or \"37:30\" (Separator \":\"). Minutes always have 2 digits.";
    formatMinutes.Folder = null; // default value
    formatMinutes.Function = true;
    formatMinutes.Icon = null; // default value
    formatMinutes.Public = false; // default value
    formatMinutes.Shareable = false; // default value
    
    var minutes = formatMinutes.CreateInputParameter("Minutes");
    minutes.DataType = eSpace.IntegerType;
    minutes.Description = null; // default value
    minutes.IsMandatory = true; // default value
    
    var separator = formatMinutes.CreateInputParameter("Separator");
    separator.DataType = eSpace.TextType;
    separator.Description = "Text between hours and minutes, e.g. \"h\" or \":\".";
    separator.IsMandatory = true; // default value
    
    var startNode = formatMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = formatMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Result", "If(Minutes < 0, \"-\", \"\") + IntegerToText(DecimalToInteger(Trunc(Abs(Minutes) / 60))) + Separator + If(Mod(Abs(Minutes), 60) < 10, \"0\", \"\") + IntegerToText(DecimalToInteger(Mod(Abs(Minutes), 60)))");
    
    var endNode = formatMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var result = formatMinutes.CreateOutputParameter("Result");
    result.DataType = eSpace.TextType;
    result.SetDefaultValue(null); // default value
    result.Description = null; // default value
}
