// Published: 2026-10-19T11:32:00+00:00 (manual 1-Click Publish; logged by Claude, applied by the main session)
// Module:   ClaudeSheet_CS
// When:     2026-10-19T11:20:14+00:00
// Story:    TT5
// Outcome:  saved
// Imports:  -
eSpace => {
  var admin = eSpace.CreateRole("SheetAdmin");
  admin.Description = "Manages clients, projects, assignments, holidays, users and settings.";
  admin.Public = true;
  var mgr = eSpace.CreateRole("SheetManager");
  mgr.Description = "Sees and approves the team's sheets and runs reports.";
  mgr.Public = true;
  var sheetUser = eSpace.Roles.Named("SheetUser");

  var sp = eSpace.CreateSiteProperty(false, "SeedAdminUsernames");
  sp.DataType = eSpace.TextType;
  sp.SetDefaultValue("\"\"");
  sp.Description = "Comma-separated usernames that the Bootstrap timer grants SheetAdmin and SheetUser, so a first admin exists. Leave empty once admins are managed in the app.";

  var b = eSpace.ServerActions.Named("Bootstrap");
  b.Description = "Seeds reference data when missing: client ICNF, project RUBUS, Portuguese 2026 holidays, SheetUser + RUBUS assignment for the users in Site.SeedTestUsernames, and SheetAdmin + SheetUser for the users in Site.SeedAdminUsernames. Safe to run more than once. Run by Timer_Bootstrap.";
  var fe1 = b.Nodes.OfType<OutSystems.Model.Logic.Nodes.IForEachNode>().Single();
  var end = (OutSystems.Model.Logic.Nodes.IEndNode)fe1.Target;

  var asg = b.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
  asg.Label = "Admin seed list";
  asg.CreateAssignment("SeedList", "\",\" + Replace(Site.SeedAdminUsernames, \" \", \"\") + \",\"");
  fe1.Target = asg;

  var agg = b.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedAdmins");
  var userEnt = eSpace.References.Named("(System)").Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
  agg.AsDatabaseAggregate.CreateSource(userEnt);
  agg.AsDatabaseAggregate.CreateFilter("User.Is_Active");
  agg.AsDatabaseAggregate.CreateFilter("Index(SeedList, \",\" + User.Username + \",\") >= 0");
  asg.Target = agg;

  var fe2 = b.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>();
  fe2.SetRecordList("GetSeedAdmins.List");
  agg.Target = fe2;
  fe2.Target = end;

  var ifU = b.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
  ifU.SetCondition("CheckSheetUserRole(GetSeedAdmins.List.Current.User.Id)");
  ifU.Label = "Has SheetUser?";
  fe2.CycleTarget = ifU;

  var gU = b.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>();
  var grantU = sheetUser.GrantRoleAction;
  gU.Action = grantU;
  gU.SetArgumentValue(grantU.InputParameters.Single(p => p.Name == "UserId"), "GetSeedAdmins.List.Current.User.Id");
  ifU.FalseTarget = gU;

  var ifA = b.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
  ifA.SetCondition("CheckSheetAdminRole(GetSeedAdmins.List.Current.User.Id)");
  ifA.Label = "Has SheetAdmin?";
  ifU.TrueTarget = ifA;
  gU.Target = ifA;

  var gA = b.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>();
  var grantA = admin.GrantRoleAction;
  gA.Action = grantA;
  gA.SetArgumentValue(grantA.InputParameters.Single(p => p.Name == "UserId"), "GetSeedAdmins.List.Current.User.Id");
  ifA.FalseTarget = gA;
  ifA.TrueTarget = fe2;
  gA.Target = fe2;

  var mf = eSpace.ServerActions.Named("Minutes_Format");
  if (mf.Public) throw new Exception("STOP: Minutes_Format is public, not deleting");
  mf.Delete();
}
