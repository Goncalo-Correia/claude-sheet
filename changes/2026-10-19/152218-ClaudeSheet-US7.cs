// Published: 2026-10-19T15:32:29+00:00 (manual 1-Click Publish; logged by Claude from the session transcript)
// Module:   ClaudeSheet
// When:     2026-10-19T15:22:18.521Z
// Story:    US7
// Outcome:  saved
// Imports:  -
eSpace => {
  var dds = eSpace.GetAllDescendantsOfType<ServiceStudio.Plugin.NRWidgets.IDropdown>().ToList();
  var cf = dds.Single(d => d.Name == "ClientFilterDropdown");
  cf.SetValues("Id"); cf.SetLabels("Name");
  var cd = dds.Single(d => d.Name == "ClientDropdown");
  cd.SetValues("Id"); cd.SetLabels("If(IsActive, Name, Name + \" (inactive)\")");
  var ud = dds.Single(d => d.Name == "UserDropdown");
  ud.SetValues("UserId"); ud.SetLabels("Name + \" (\" + Username + \")\"");
}
