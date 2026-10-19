# Timers

Module-wide read in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating ITimer 'Timer_Bootstrap' ***/
    var timer_Bootstrap = eSpace.CreateTimer("Timer_Bootstrap");
    var bootstrap = eSpace.ServerActions.Named("Bootstrap");
    timer_Bootstrap.Action = bootstrap;
    timer_Bootstrap.CronSchedule = "WhenPublished";
    timer_Bootstrap.Description = "Seeds reference data (ICNF, RUBUS, PT 2026 holidays, test users) on every publish. Bootstrap only inserts what is missing.";
    timer_Bootstrap.Schedule = "WhenPublished";
}
```
