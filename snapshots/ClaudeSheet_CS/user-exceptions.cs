(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IUserException 'ClientValidation' ***/
    var clientValidation = eSpace.CreateUserException("ClientValidation");
    
    /*** creating IUserException 'DaySheetValidation' ***/
    var daySheetValidation = eSpace.CreateUserException("DaySheetValidation");
}
