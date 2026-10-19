// Published: 2026-10-19T14:06:42+00:00 (manual 1-Click Publish; logged by Claude from the session transcript)
// Module:   ClaudeSheet_CS
// When:     2026-10-19T13:58:22.661Z
// Story:    US7
// Outcome:  saved
// Imports:  -
eSpace => {
  var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
  var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
  var pa = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
  var te = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
  var ds = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
  var userIdType = pa.Attributes.Single(a => a.Name == "UserId").DataType;
  var roleEx = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");

  var pv = eSpace.CreateUserException("ProjectValidation");
  var av = eSpace.CreateUserException("AssignmentValidation");

  var ps = eSpace.CreateStructure("ProjectSummary");
  ps.Description = "One project in the admin list, with its client and the number of users assigned now or later (Project_GetList).";
  ps.Public = true;
  { var a = ps.CreateAttribute("Id"); a.DataType = project.IdentifierType; }
  { var a = ps.CreateAttribute("ClientId"); a.DataType = client.IdentifierType; }
  { var a = ps.CreateAttribute("ClientName"); a.DataType = eSpace.TextType; a.Length = 100; }
  { var a = ps.CreateAttribute("Code"); a.DataType = eSpace.TextType; a.Length = 30; }
  { var a = ps.CreateAttribute("Name"); a.DataType = eSpace.TextType; a.Length = 100; }
  { var a = ps.CreateAttribute("IsActive"); a.DataType = eSpace.BooleanType; }
  { var a = ps.CreateAttribute("AssignedUsers"); a.DataType = eSpace.IntegerType; }

  var pas = eSpace.CreateStructure("ProjectAssignmentSummary");
  pas.Description = "One assignment with project, client and user names and its Status (Active, Upcoming, Ended, Inactive) on today's date (ProjectAssignment_GetList).";
  pas.Public = true;
  { var a = pas.CreateAttribute("Id"); a.DataType = pa.IdentifierType; }
  { var a = pas.CreateAttribute("ProjectId"); a.DataType = project.IdentifierType; }
  { var a = pas.CreateAttribute("ProjectCode"); a.DataType = eSpace.TextType; a.Length = 30; }
  { var a = pas.CreateAttribute("ProjectName"); a.DataType = eSpace.TextType; a.Length = 100; }
  { var a = pas.CreateAttribute("ClientName"); a.DataType = eSpace.TextType; a.Length = 100; }
  { var a = pas.CreateAttribute("UserId"); a.DataType = userIdType; }
  { var a = pas.CreateAttribute("UserName"); a.DataType = eSpace.TextType; a.Length = 250; }
  { var a = pas.CreateAttribute("StartDate"); a.DataType = eSpace.DateType; }
  { var a = pas.CreateAttribute("EndDate"); a.DataType = eSpace.DateType; }
  { var a = pas.CreateAttribute("IsActive"); a.DataType = eSpace.BooleanType; }
  { var a = pas.CreateAttribute("Status"); a.DataType = eSpace.TextType; a.Length = 20; }

  var uo = eSpace.CreateStructure("UserOption");
  uo.Description = "A user for a dropdown: Id, name and username (User_GetActive).";
  uo.Public = true;
  { var a = uo.CreateAttribute("UserId"); a.DataType = userIdType; }
  { var a = uo.CreateAttribute("Name"); a.DataType = eSpace.TextType; a.Length = 250; }
  { var a = uo.CreateAttribute("Username"); a.DataType = eSpace.TextType; a.Length = 250; }

  Action<OutSystems.Model.Logic.Nodes.IActionNode, int, int> at = (n, c, r) => { n.HorizontalPosition = 3200 + c * 1828; n.VerticalPosition = 800 + r * 1371; };
  Func<OutSystems.Model.Logic.IServerAction, string, OutSystems.Model.Logic.Nodes.IIfNode> gate = (a, msg) => {
    var st = a.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0);
    var ifn = a.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(ifn, 0, 1);
    ifn.SetCondition("CheckSheetAdminRole()"); ifn.Label = "Is SheetAdmin?";
    st.Target = ifn;
    var rx = a.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>(); at(rx, 1, 1);
    rx.Exception = roleEx; rx.SetExceptionMessage(msg);
    ifn.FalseTarget = rx;
    return ifn;
  };
  Func<OutSystems.Model.Logic.IServerAction, OutSystems.Model.Logic.IUserException, string, string, string, int, OutSystems.Model.Logic.Nodes.IIfNode> check = (a, ex, label, cond, msg, row) => {
    var ifn = a.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(ifn, 0, row);
    ifn.SetCondition(cond); ifn.Label = label;
    var rx = a.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>(); at(rx, 1, row);
    rx.Exception = ex; rx.SetExceptionMessage(msg);
    ifn.TrueTarget = rx;
    return ifn;
  };
  Func<OutSystems.Model.Logic.IServerAction, int, OutSystems.Model.Logic.Nodes.IEndNode> end = (a, row) => { var e = a.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(e, 0, row); return e; };
  string projMsg = "\"Only administrators can manage projects.\"";
  string asgMsg = "\"Only administrators can manage project assignments.\"";

  // ---------- Project_GetList
  var gl = eSpace.CreateServerAction("Project_GetList");
  gl.Public = true;
  gl.Description = "Projects for the admin list (SheetAdmin only), with client name and the number of distinct users with an active assignment that hasn't ended, sorted by client and project name. ClientId filters by client (empty = all); IncludeInactive also returns deactivated projects.";
  { var p = gl.CreateInputParameter("ClientId"); p.DataType = client.IdentifierType; p.IsMandatory = false; }
  { var p = gl.CreateInputParameter("IncludeInactive"); p.DataType = eSpace.BooleanType; p.IsMandatory = false; }
  { var o = gl.CreateOutputParameter("Projects"); o.DataType = eSpace.GetOrCreateListType(ps); }
  var g1 = gate(gl, projMsg);
  var sql1 = gl.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetProjects"); at(sql1, 0, 2);
  { var p = sql1.CreateInputParameter("ClientId"); p.DataType = client.IdentifierType; sql1.SetArgumentValue(p, "ClientId"); }
  { var p = sql1.CreateInputParameter("IncludeInactive"); p.DataType = eSpace.BooleanType; sql1.SetArgumentValue(p, "IncludeInactive"); }
  { var p = sql1.CreateInputParameter("Today"); p.DataType = eSpace.DateType; sql1.SetArgumentValue(p, "CurrDate()"); }
  sql1.CreateOutput(ps);
  sql1.Statement = "SELECT {Project}.[Id], {Project}.[ClientId], {Client}.[Name], {Project}.[Code], {Project}.[Name], {Project}.[IsActive],\n  (SELECT COUNT(DISTINCT {ProjectAssignment}.[UserId]) FROM {ProjectAssignment}\n   WHERE {ProjectAssignment}.[ProjectId] = {Project}.[Id] AND {ProjectAssignment}.[IsActive] = 1\n     AND ({ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] <= '1900-01-01' OR {ProjectAssignment}.[EndDate] >= @Today))\nFROM {Project}\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nWHERE (@IncludeInactive = 1 OR {Project}.[IsActive] = 1)\n  AND (COALESCE(@ClientId, 0) = 0 OR {Project}.[ClientId] = @ClientId)\nORDER BY {Client}.[Name], {Project}.[Name]";
  g1.TrueTarget = sql1;
  var glA = gl.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(glA, 0, 3);
  glA.CreateAssignment("Projects", "GetProjects.List");
  sql1.Target = glA; glA.Target = end(gl, 4);

  // ---------- Project_GetById
  var gb = eSpace.CreateServerAction("Project_GetById");
  gb.Public = true;
  gb.Description = "One project (ProjectRecord) for the admin detail screen (SheetAdmin only). Raises ProjectValidation when the project doesn't exist.";
  { var p = gb.CreateInputParameter("ProjectId"); p.DataType = project.IdentifierType; }
  { var o = gb.CreateOutputParameter("ProjectRecord"); o.DataType = project; }
  var g2 = gate(gb, projMsg);
  var agP = gb.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProject"); at(agP, 0, 2);
  agP.AsDatabaseAggregate.CreateSource(project);
  agP.AsDatabaseAggregate.CreateFilter("Project.Id = ProjectId");
  g2.TrueTarget = agP;
  var nf1 = check(gb, pv, "Not found?", "GetProject.List.Empty", "\"Project not found.\"", 3);
  agP.Target = nf1;
  var gbA = gb.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(gbA, 0, 4);
  gbA.CreateAssignment("ProjectRecord", "GetProject.List.Current.Project");
  nf1.FalseTarget = gbA; gbA.Target = end(gb, 5);

  // ---------- Project_CreateOrUpdate
  var cu = eSpace.CreateServerAction("Project_CreateOrUpdate");
  cu.Public = true;
  cu.Description = "Creates or updates a project (SheetAdmin only). Name and Code are trimmed and required, the client must exist and be active, and Code is unique per client (case-insensitive by the database collation). Raises ProjectValidation with a user-facing message otherwise. Returns the project's Id.";
  { var p = cu.CreateInputParameter("ProjectRecord"); p.DataType = project; }
  { var o = cu.CreateOutputParameter("ProjectId"); o.DataType = project.IdentifierType; }
  var g3 = gate(cu, projMsg);
  var c1 = check(cu, pv, "Name empty?", "Trim(ProjectRecord.Name) = \"\"", "\"Enter the project name.\"", 2);
  g3.TrueTarget = c1;
  var c2 = check(cu, pv, "Code empty?", "Trim(ProjectRecord.Code) = \"\"", "\"Enter the project code.\"", 3);
  c1.FalseTarget = c2;
  var agC = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClient"); at(agC, 0, 4);
  agC.AsDatabaseAggregate.CreateSource(client);
  agC.AsDatabaseAggregate.CreateFilter("Client.Id = ProjectRecord.ClientId");
  c2.FalseTarget = agC;
  var c3 = check(cu, pv, "No client?", "GetClient.List.Empty", "\"Choose a client.\"", 5);
  agC.Target = c3;
  var c4 = check(cu, pv, "Client inactive?", "not GetClient.List.Current.Client.IsActive", "\"This client is inactive. Activate it first or choose another client.\"", 6);
  c3.FalseTarget = c4;
  var agS = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSameCode"); at(agS, 0, 7);
  agS.AsDatabaseAggregate.CreateSource(project);
  agS.AsDatabaseAggregate.CreateFilter("Project.ClientId = ProjectRecord.ClientId");
  agS.AsDatabaseAggregate.CreateFilter("Project.Code = Trim(ProjectRecord.Code)");
  agS.AsDatabaseAggregate.CreateFilter("Project.Id <> ProjectRecord.Id");
  c4.FalseTarget = agS;
  var c5 = check(cu, pv, "Code taken?", "not GetSameCode.List.Empty", "\"A project with this code already exists for this client.\"", 8);
  agS.Target = c5;
  var cuA = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cuA, 0, 9);
  cuA.CreateAssignment("ProjectRecord.Name", "Trim(ProjectRecord.Name)");
  cuA.CreateAssignment("ProjectRecord.Code", "Trim(ProjectRecord.Code)");
  c5.FalseTarget = cuA;
  var cuS = cu.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateProject"); at(cuS, 0, 10);
  cuS.Action = project.CreateOrUpdateAction;
  cuS.SetArgumentValue(project.CreateOrUpdateAction.InputParameters.Single(p => p.Name == "Source"), "ProjectRecord");
  cuA.Target = cuS;
  var cuA2 = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cuA2, 0, 11);
  cuA2.CreateAssignment("ProjectId", "CreateOrUpdateProject.Id");
  cuS.Target = cuA2; cuA2.Target = end(cu, 12);

  // ---------- Project_SetActive
  var sa = eSpace.CreateServerAction("Project_SetActive");
  sa.Public = true;
  sa.Description = "Activates or deactivates a project (SheetAdmin only); projects are deactivated instead of deleted (ProjectAssignment.ProjectId and TimeEntry.ProjectId are Protect). Inactive projects are no longer offered to users (Project_GetAssignedToUser filters on Project.IsActive).";
  { var p = sa.CreateInputParameter("ProjectId"); p.DataType = project.IdentifierType; }
  { var p = sa.CreateInputParameter("IsActive"); p.DataType = eSpace.BooleanType; }
  { var l = sa.CreateLocalVariable("ProjectRecord"); l.DataType = project; }
  var g4 = gate(sa, projMsg);
  var agP2 = sa.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProject"); at(agP2, 0, 2);
  agP2.AsDatabaseAggregate.CreateSource(project);
  agP2.AsDatabaseAggregate.CreateFilter("Project.Id = ProjectId");
  g4.TrueTarget = agP2;
  var nf2 = check(sa, pv, "Not found?", "GetProject.List.Empty", "\"Project not found.\"", 3);
  agP2.Target = nf2;
  var saA = sa.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(saA, 0, 4);
  saA.CreateAssignment("ProjectRecord", "GetProject.List.Current.Project");
  saA.CreateAssignment("ProjectRecord.IsActive", "IsActive");
  nf2.FalseTarget = saA;
  var saU = sa.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateProject"); at(saU, 0, 5);
  saU.Action = project.UpdateAction;
  saU.SetArgumentValue(project.UpdateAction.InputParameters.Single(p => p.Name == "Source"), "ProjectRecord");
  saA.Target = saU; saU.Target = end(sa, 6);

  // ---------- ProjectAssignment_GetList
  var al = eSpace.CreateServerAction("ProjectAssignment_GetList");
  al.Public = true;
  al.Description = "Assignments of a project (ProjectId) or of a user (UserId), or both, with project, client and user names and Status on today's date (Inactive, Upcoming, Ended, Active), sorted by client, project, user and start date (SheetAdmin only). Raises AssignmentValidation when both are empty.";
  { var p = al.CreateInputParameter("ProjectId"); p.DataType = project.IdentifierType; p.IsMandatory = false; }
  { var p = al.CreateInputParameter("UserId"); p.DataType = userIdType; p.IsMandatory = false; }
  { var o = al.CreateOutputParameter("Assignments"); o.DataType = eSpace.GetOrCreateListType(pas); }
  var g5 = gate(al, asgMsg);
  var c6 = check(al, av, "No filter?", "ProjectId = NullIdentifier() and UserId = NullIdentifier()", "\"Choose a project or a user.\"", 2);
  g5.TrueTarget = c6;
  var sql2 = al.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetAssignments"); at(sql2, 0, 3);
  { var p = sql2.CreateInputParameter("ProjectId"); p.DataType = project.IdentifierType; sql2.SetArgumentValue(p, "ProjectId"); }
  { var p = sql2.CreateInputParameter("UserId"); p.DataType = userIdType; sql2.SetArgumentValue(p, "UserId"); }
  { var p = sql2.CreateInputParameter("Today"); p.DataType = eSpace.DateType; sql2.SetArgumentValue(p, "CurrDate()"); }
  sql2.CreateOutput(pas);
  sql2.Statement = "SELECT {ProjectAssignment}.[Id], {ProjectAssignment}.[ProjectId], {Project}.[Code], {Project}.[Name], {Client}.[Name],\n  {ProjectAssignment}.[UserId], {User}.[Name], {ProjectAssignment}.[StartDate], {ProjectAssignment}.[EndDate], {ProjectAssignment}.[IsActive],\n  CASE WHEN {ProjectAssignment}.[IsActive] = 0 THEN 'Inactive'\n       WHEN {ProjectAssignment}.[StartDate] > @Today THEN 'Upcoming'\n       WHEN {ProjectAssignment}.[EndDate] > '1900-01-01' AND {ProjectAssignment}.[EndDate] < @Today THEN 'Ended'\n       ELSE 'Active' END\nFROM {ProjectAssignment}\nINNER JOIN {Project} ON {Project}.[Id] = {ProjectAssignment}.[ProjectId]\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nINNER JOIN {User} ON {User}.[Id] = {ProjectAssignment}.[UserId]\nWHERE (COALESCE(@ProjectId, 0) = 0 OR {ProjectAssignment}.[ProjectId] = @ProjectId)\n  AND (COALESCE(@UserId, 0) = 0 OR {ProjectAssignment}.[UserId] = @UserId)\nORDER BY {Client}.[Name], {Project}.[Name], {User}.[Name], {ProjectAssignment}.[StartDate]";
  c6.FalseTarget = sql2;
  var alA = al.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(alA, 0, 4);
  alA.CreateAssignment("Assignments", "GetAssignments.List");
  sql2.Target = alA; alA.Target = end(al, 5);

  // ---------- ProjectAssignment_CreateOrUpdate
  var ac = eSpace.CreateServerAction("ProjectAssignment_CreateOrUpdate");
  ac.Public = true;
  ac.Description = "Creates or updates an assignment (SheetAdmin only): user and start date required; EndDate empty (open end) or not before StartDate; project and its client active; no overlap with another active period of the same user and project, nor the same start date (ADR 0002). Raises AssignmentValidation with a user-facing message otherwise. Saves the assignment as active and returns its Id.";
  { var p = ac.CreateInputParameter("ProjectAssignmentRecord"); p.DataType = pa; }
  { var o = ac.CreateOutputParameter("ProjectAssignmentId"); o.DataType = pa.IdentifierType; }
  var g6 = gate(ac, asgMsg);
  var d1 = check(ac, av, "No user?", "ProjectAssignmentRecord.UserId = NullIdentifier()", "\"Choose a user.\"", 2);
  g6.TrueTarget = d1;
  var d2 = check(ac, av, "No start date?", "ProjectAssignmentRecord.StartDate = NullDate()", "\"Enter the start date.\"", 3);
  d1.FalseTarget = d2;
  var d3 = check(ac, av, "End before start?", "ProjectAssignmentRecord.EndDate <> NullDate() and ProjectAssignmentRecord.EndDate < ProjectAssignmentRecord.StartDate", "\"The end date can't be before the start date.\"", 4);
  d2.FalseTarget = d3;
  var agPr = ac.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProject"); at(agPr, 0, 5);
  {
    var sP = agPr.AsDatabaseAggregate.CreateSource(project);
    var sC = agPr.AsDatabaseAggregate.CreateSource(client);
    var j = agPr.AsDatabaseAggregate.CreateJoin();
    j.LeftSource = sP; j.RightSource = sC; j.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    j.SetCondition("Project.ClientId = Client.Id");
    agPr.AsDatabaseAggregate.CreateFilter("Project.Id = ProjectAssignmentRecord.ProjectId");
  }
  d3.FalseTarget = agPr;
  var d4 = check(ac, av, "No project?", "GetProject.List.Empty", "\"Project not found.\"", 6);
  agPr.Target = d4;
  var d5 = check(ac, av, "Project inactive?", "not GetProject.List.Current.Project.IsActive or not GetProject.List.Current.Client.IsActive", "\"This project or its client is inactive.\"", 7);
  d4.FalseTarget = d5;
  var agO = ac.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetOverlapping"); at(agO, 0, 8);
  agO.AsDatabaseAggregate.CreateSource(pa);
  agO.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = ProjectAssignmentRecord.UserId");
  agO.AsDatabaseAggregate.CreateFilter("ProjectAssignment.ProjectId = ProjectAssignmentRecord.ProjectId");
  agO.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id <> ProjectAssignmentRecord.Id");
  agO.AsDatabaseAggregate.CreateFilter("ProjectAssignment.IsActive or ProjectAssignment.StartDate = ProjectAssignmentRecord.StartDate");
  agO.AsDatabaseAggregate.CreateFilter("ProjectAssignmentRecord.EndDate = NullDate() or ProjectAssignment.StartDate <= ProjectAssignmentRecord.EndDate");
  agO.AsDatabaseAggregate.CreateFilter("ProjectAssignment.EndDate = NullDate() or ProjectAssignment.EndDate >= ProjectAssignmentRecord.StartDate");
  d5.FalseTarget = agO;
  var d6 = check(ac, av, "Overlaps?", "not GetOverlapping.List.Empty", "\"This user is already assigned to this project in an overlapping period.\"", 9);
  agO.Target = d6;
  var acA = ac.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(acA, 0, 10);
  acA.CreateAssignment("ProjectAssignmentRecord.IsActive", "True");
  d6.FalseTarget = acA;
  var acS = ac.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateProjectAssignment"); at(acS, 0, 11);
  acS.Action = pa.CreateOrUpdateAction;
  acS.SetArgumentValue(pa.CreateOrUpdateAction.InputParameters.Single(p => p.Name == "Source"), "ProjectAssignmentRecord");
  acA.Target = acS;
  var acA2 = ac.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(acA2, 0, 12);
  acA2.CreateAssignment("ProjectAssignmentId", "CreateOrUpdateProjectAssignment.Id");
  acS.Target = acA2; acA2.Target = end(ac, 13);

  // ---------- ProjectAssignment_End
  var ae = eSpace.CreateServerAction("ProjectAssignment_End");
  ae.Public = true;
  ae.Description = "Ends an assignment on EndDate (SheetAdmin only): EndDate required and not before the assignment's StartDate. Raises AssignmentValidation otherwise. Use it instead of ProjectAssignment_Delete once the user has time entries.";
  { var p = ae.CreateInputParameter("ProjectAssignmentId"); p.DataType = pa.IdentifierType; }
  { var p = ae.CreateInputParameter("EndDate"); p.DataType = eSpace.DateType; }
  { var l = ae.CreateLocalVariable("ProjectAssignmentRecord"); l.DataType = pa; }
  var g7 = gate(ae, asgMsg);
  var agE = ae.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignment"); at(agE, 0, 2);
  agE.AsDatabaseAggregate.CreateSource(pa);
  agE.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id = ProjectAssignmentId");
  g7.TrueTarget = agE;
  var e1 = check(ae, av, "Not found?", "GetAssignment.List.Empty", "\"Assignment not found.\"", 3);
  agE.Target = e1;
  var e2 = check(ae, av, "No end date?", "EndDate = NullDate()", "\"Enter the end date.\"", 4);
  e1.FalseTarget = e2;
  var e3 = check(ae, av, "End before start?", "EndDate < GetAssignment.List.Current.ProjectAssignment.StartDate", "\"The end date can't be before the start date.\"", 5);
  e2.FalseTarget = e3;
  var aeA = ae.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(aeA, 0, 6);
  aeA.CreateAssignment("ProjectAssignmentRecord", "GetAssignment.List.Current.ProjectAssignment");
  aeA.CreateAssignment("ProjectAssignmentRecord.EndDate", "EndDate");
  e3.FalseTarget = aeA;
  var aeU = ae.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateProjectAssignment"); at(aeU, 0, 7);
  aeU.Action = pa.UpdateAction;
  aeU.SetArgumentValue(pa.UpdateAction.InputParameters.Single(p => p.Name == "Source"), "ProjectAssignmentRecord");
  aeA.Target = aeU; aeU.Target = end(ae, 8);

  // ---------- ProjectAssignment_Delete
  var ad = eSpace.CreateServerAction("ProjectAssignment_Delete");
  ad.Public = true;
  ad.Description = "Deletes an assignment (SheetAdmin only), only when its user has no TimeEntry on its project between StartDate and EndDate (open end = no limit). Otherwise raises AssignmentValidation asking to end it instead (ProjectAssignment_End).";
  { var p = ad.CreateInputParameter("ProjectAssignmentId"); p.DataType = pa.IdentifierType; }
  var g8 = gate(ad, asgMsg);
  var agD = ad.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignment"); at(agD, 0, 2);
  agD.AsDatabaseAggregate.CreateSource(pa);
  agD.AsDatabaseAggregate.CreateFilter("ProjectAssignment.Id = ProjectAssignmentId");
  g8.TrueTarget = agD;
  var f1 = check(ad, av, "Not found?", "GetAssignment.List.Empty", "\"Assignment not found.\"", 3);
  agD.Target = f1;
  var agT = ad.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetEntriesInPeriod"); at(agT, 0, 4);
  {
    var sT = agT.AsDatabaseAggregate.CreateSource(te);
    var sD = agT.AsDatabaseAggregate.CreateSource(ds);
    var j = agT.AsDatabaseAggregate.CreateJoin();
    j.LeftSource = sT; j.RightSource = sD; j.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    j.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    agT.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetAssignment.List.Current.ProjectAssignment.UserId");
    agT.AsDatabaseAggregate.CreateFilter("TimeEntry.ProjectId = GetAssignment.List.Current.ProjectAssignment.ProjectId");
    agT.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= GetAssignment.List.Current.ProjectAssignment.StartDate");
    agT.AsDatabaseAggregate.CreateFilter("GetAssignment.List.Current.ProjectAssignment.EndDate = NullDate() or DaySheet.SheetDate <= GetAssignment.List.Current.ProjectAssignment.EndDate");
  }
  f1.FalseTarget = agT;
  var f2 = check(ad, av, "Has entries?", "not GetEntriesInPeriod.List.Empty", "\"This assignment has time entries and can't be deleted. Use End to close it instead.\"", 5);
  agT.Target = f2;
  var adD = ad.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteProjectAssignment"); at(adD, 0, 6);
  adD.Action = pa.DeleteAction;
  adD.SetArgumentValue(pa.DeleteAction.InputParameters.Single(), "ProjectAssignmentId");
  f2.FalseTarget = adD; adD.Target = end(ad, 7);

  // ---------- User_GetActive
  var ua = eSpace.CreateServerAction("User_GetActive");
  ua.Public = true;
  ua.Description = "Active users for a dropdown (SheetAdmin only): UserId, name and username, sorted by name. Used by the assignment popup in ClaudeSheet.";
  { var o = ua.CreateOutputParameter("Users"); o.DataType = eSpace.GetOrCreateListType(uo); }
  var g9 = gate(ua, "\"Only administrators can list users.\"");
  var sql3 = ua.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetUsers"); at(sql3, 0, 2);
  sql3.CreateOutput(uo);
  sql3.Statement = "SELECT {User}.[Id], {User}.[Name], {User}.[Username]\nFROM {User}\nWHERE {User}.[Is_Active] = 1\nORDER BY {User}.[Name]";
  g9.TrueTarget = sql3;
  var uaA = ua.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(uaA, 0, 3);
  uaA.CreateAssignment("Users", "GetUsers.List");
  sql3.Target = uaA; uaA.Target = end(ua, 4);

  // ---------- P04 delete rule
  pa.Attributes.Single(a => a.Name == "ProjectId").DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect;
}
