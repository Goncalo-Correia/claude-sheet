(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IUserException 'AssignmentValidation' ***/
    var assignmentValidation = eSpace.CreateUserException("AssignmentValidation");
    
    /*** creating IUserException 'ClientValidation' ***/
    var clientValidation = eSpace.CreateUserException("ClientValidation");
    
    /*** creating IUserException 'DaySheetValidation' ***/
    var daySheetValidation = eSpace.CreateUserException("DaySheetValidation");
    
    /*** creating IUserException 'HolidayValidation' ***/
    var holidayValidation = eSpace.CreateUserException("HolidayValidation");
    
    /*** creating IUserException 'PeriodValidation' ***/
    var periodValidation = eSpace.CreateUserException("PeriodValidation");
    
    /*** creating IUserException 'ProjectValidation' ***/
    var projectValidation = eSpace.CreateUserException("ProjectValidation");
}
