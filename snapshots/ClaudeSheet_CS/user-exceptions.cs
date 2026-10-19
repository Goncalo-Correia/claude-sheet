(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IUserException 'AssignmentValidation' ***/
    var assignmentValidation = eSpace.CreateUserException("AssignmentValidation");
    
    /*** creating IUserException 'ClientValidation' ***/
    var clientValidation = eSpace.CreateUserException("ClientValidation");
    
    /*** creating IUserException 'DaySheetValidation' ***/
    var daySheetValidation = eSpace.CreateUserException("DaySheetValidation");
    
    /*** creating IUserException 'ProjectValidation' ***/
    var projectValidation = eSpace.CreateUserException("ProjectValidation");
}
