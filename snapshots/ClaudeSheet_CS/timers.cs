(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating ITimer 'Timer_Bootstrap' ***/
    var timer_Bootstrap = eSpace.CreateTimer("Timer_Bootstrap");
    var bootstrap = eSpace.ServerActions.Named("Bootstrap");
    timer_Bootstrap.Action = bootstrap;
    timer_Bootstrap.CronSchedule = "WhenPublished";
    timer_Bootstrap.Description = "Seeds reference data (ICNF, RUBUS, PT 2026 holidays, test users) on every publish. Bootstrap only inserts what is missing.";
    timer_Bootstrap.Folder = null; // default value
    timer_Bootstrap.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    timer_Bootstrap.Priority = OutSystems.Model.Enumerations.Priority.Normal; // default value
    timer_Bootstrap.Schedule = "WhenPublished";
    timer_Bootstrap.TimeoutInMinutes = 20; // default value
}
