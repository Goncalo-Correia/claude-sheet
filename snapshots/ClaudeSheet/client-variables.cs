(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IClientVariable 'LastURL' ***/
    var lastURL = eSpace.CreateClientVariable("LastURL");
    lastURL.DataType = eSpace.TextType;
    lastURL.SetDefaultValue(null); // default value
    lastURL.Description = null; // default value
    lastURL.Folder = null; // default value
    
    /*** creating IClientVariable 'Username' ***/
    var username = eSpace.CreateClientVariable("Username");
    username.DataType = eSpace.TextType;
    username.SetDefaultValue(null); // default value
    username.Description = null; // default value
    username.Folder = null; // default value
}
