// Published: 2026-10-19T13:13:29+00:00 (manual 1-Click Publish; logged by Claude, applied by the main session)
// Module:   ClaudeSheet_CS
// When:     2026-10-19T11:54:32+00:00
// Story:    US6
// Outcome:  saved
// Imports:  -
eSpace => {
  var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
  var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
  var roleEx = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");

  var cv = eSpace.CreateUserException("ClientValidation");

  var cs = eSpace.CreateStructure("ClientSummary");
  cs.Description = "One client in the admin list, with the number of its projects (Client_GetList).";
  cs.Public = true;
  var sId = cs.CreateAttribute("Id"); sId.DataType = client.IdentifierType;
  var sName = cs.CreateAttribute("Name"); sName.DataType = eSpace.TextType; sName.Length = 100;
  var sAct = cs.CreateAttribute("IsActive"); sAct.DataType = eSpace.BooleanType;
  var sCnt = cs.CreateAttribute("ProjectCount"); sCnt.DataType = eSpace.IntegerType;

  Action<OutSystems.Model.Logic.Nodes.IActionNode, int, int> at = (n, c, r) => { n.HorizontalPosition = 3200 + c * 1828; n.VerticalPosition = 800 + r * 1371; };
  Func<OutSystems.Model.Logic.IServerAction, OutSystems.Model.Logic.Nodes.IIfNode> gate = a => {
    var st = a.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0);
    var ifn = a.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(ifn, 0, 1);
    ifn.SetCondition("CheckSheetAdminRole()"); ifn.Label = "Is SheetAdmin?";
    st.Target = ifn;
    var rx = a.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>(); at(rx, 1, 1);
    rx.Exception = roleEx; rx.SetExceptionMessage("\"Only administrators can manage clients.\"");
    ifn.FalseTarget = rx;
    return ifn;
  };
  Func<OutSystems.Model.Logic.IServerAction, string, string, int, OutSystems.Model.Logic.Nodes.IIfNode> check = (a, cond, msg, row) => {
    var ifn = a.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(ifn, 0, row);
    ifn.SetCondition(cond);
    var rx = a.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>(); at(rx, 1, row);
    rx.Exception = cv; rx.SetExceptionMessage(msg);
    ifn.TrueTarget = rx;
    return ifn;
  };

  var gl = eSpace.CreateServerAction("Client_GetList");
  gl.Public = true;
  gl.Description = "Clients for the admin list (SheetAdmin only): name, active flag and number of projects, sorted by name. IncludeInactive also returns deactivated clients; Search filters by name (contains).";
  var glInc = gl.CreateInputParameter("IncludeInactive"); glInc.DataType = eSpace.BooleanType; glInc.IsMandatory = false;
  var glSearch = gl.CreateInputParameter("Search"); glSearch.DataType = eSpace.TextType; glSearch.IsMandatory = false;
  var glOut = gl.CreateOutputParameter("Clients"); glOut.DataType = eSpace.GetOrCreateListType(cs);
  var g1 = gate(gl);
  var sql = gl.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetClients"); at(sql, 0, 2);
  var pInc = sql.CreateInputParameter("IncludeInactive"); pInc.DataType = eSpace.BooleanType; sql.SetArgumentValue(pInc, "IncludeInactive");
  var pSearch = sql.CreateInputParameter("Search"); pSearch.DataType = eSpace.TextType; sql.SetArgumentValue(pSearch, "Trim(Search)");
  sql.CreateOutput(cs);
  sql.Statement = "SELECT {Client}.*,\n  (SELECT COUNT(*) FROM {Project} WHERE {Project}.[ClientId] = {Client}.[Id])\nFROM {Client}\nWHERE (@IncludeInactive = 1 OR {Client}.[IsActive] = 1)\n  AND (@Search = '' OR CHARINDEX(@Search, {Client}.[Name]) > 0)\nORDER BY {Client}.[Name]";
  g1.TrueTarget = sql;
  var glAsg = gl.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(glAsg, 0, 3);
  glAsg.CreateAssignment("Clients", "GetClients.List");
  sql.Target = glAsg;
  var glEnd = gl.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(glEnd, 0, 4); glAsg.Target = glEnd;

  var gb = eSpace.CreateServerAction("Client_GetById");
  gb.Public = true;
  gb.Description = "One client (ClientRecord) with its projects sorted by name (SheetAdmin only). Raises ClientValidation when the client doesn't exist.";
  var gbId = gb.CreateInputParameter("ClientId"); gbId.DataType = client.IdentifierType;
  var gbOut = gb.CreateOutputParameter("ClientRecord"); gbOut.DataType = client;
  var gbProj = gb.CreateOutputParameter("Projects"); gbProj.DataType = eSpace.GetOrCreateListType(project);
  var g2 = gate(gb);
  var aggC = gb.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClient"); at(aggC, 0, 2);
  aggC.AsDatabaseAggregate.CreateSource(client);
  aggC.AsDatabaseAggregate.CreateFilter("Client.Id = ClientId");
  g2.TrueTarget = aggC;
  var nf = check(gb, "GetClient.List.Empty", "\"Client not found.\"", 3); nf.Label = "Not found?";
  aggC.Target = nf;
  var aggP = gb.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClientProjects"); at(aggP, 0, 4);
  aggP.AsDatabaseAggregate.CreateSource(project);
  aggP.AsDatabaseAggregate.CreateFilter("Project.ClientId = ClientId");
  aggP.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Name");
  nf.FalseTarget = aggP;
  var gbAsg = gb.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(gbAsg, 0, 5);
  gbAsg.CreateAssignment("ClientRecord", "GetClient.List.Current.Client");
  gbAsg.CreateAssignment("Projects", "GetClientProjects.List");
  aggP.Target = gbAsg;
  var gbEnd = gb.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(gbEnd, 0, 6); gbAsg.Target = gbEnd;

  var cu = eSpace.CreateServerAction("Client_CreateOrUpdate");
  cu.Public = true;
  cu.Description = "Creates or updates a client (SheetAdmin only). Name is trimmed, required and unique (case-insensitive by the database collation); raises ClientValidation with a user-facing message otherwise. Returns the client's Id.";
  var cuIn = cu.CreateInputParameter("ClientRecord"); cuIn.DataType = client;
  var cuOut = cu.CreateOutputParameter("ClientId"); cuOut.DataType = client.IdentifierType;
  var g3 = gate(cu);
  var emp = check(cu, "Trim(ClientRecord.Name) = \"\"", "\"Enter the client name.\"", 2); emp.Label = "Name empty?";
  g3.TrueTarget = emp;
  var aggS = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSameName"); at(aggS, 0, 3);
  aggS.AsDatabaseAggregate.CreateSource(client);
  aggS.AsDatabaseAggregate.CreateFilter("Client.Name = Trim(ClientRecord.Name)");
  aggS.AsDatabaseAggregate.CreateFilter("Client.Id <> ClientRecord.Id");
  emp.FalseTarget = aggS;
  var dup = check(cu, "not GetSameName.List.Empty", "\"A client with this name already exists.\"", 4); dup.Label = "Name taken?";
  aggS.Target = dup;
  var cuAsg = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cuAsg, 0, 5);
  cuAsg.CreateAssignment("ClientRecord.Name", "Trim(ClientRecord.Name)");
  dup.FalseTarget = cuAsg;
  var save = cu.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateClient"); at(save, 0, 6);
  save.Action = client.CreateOrUpdateAction;
  save.SetArgumentValue(client.CreateOrUpdateAction.InputParameters.Single(p => p.Name == "Source"), "ClientRecord");
  cuAsg.Target = save;
  var cuAsg2 = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cuAsg2, 0, 7);
  cuAsg2.CreateAssignment("ClientId", "CreateOrUpdateClient.Id");
  save.Target = cuAsg2;
  var cuEnd = cu.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(cuEnd, 0, 8); cuAsg2.Target = cuEnd;

  var sa = eSpace.CreateServerAction("Client_SetActive");
  sa.Public = true;
  sa.Description = "Activates or deactivates a client (SheetAdmin only); clients are deactivated instead of deleted. Projects of an inactive client are no longer offered to users (Project_GetAssignedToUser filters on the client's IsActive).";
  var saId = sa.CreateInputParameter("ClientId"); saId.DataType = client.IdentifierType;
  var saAct = sa.CreateInputParameter("IsActive"); saAct.DataType = eSpace.BooleanType;
  var saRec = sa.CreateLocalVariable("ClientRecord"); saRec.DataType = client;
  var g4 = gate(sa);
  var aggA = sa.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetClient"); at(aggA, 0, 2);
  aggA.AsDatabaseAggregate.CreateSource(client);
  aggA.AsDatabaseAggregate.CreateFilter("Client.Id = ClientId");
  g4.TrueTarget = aggA;
  var nf2 = check(sa, "GetClient.List.Empty", "\"Client not found.\"", 3); nf2.Label = "Not found?";
  aggA.Target = nf2;
  var saAsg = sa.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(saAsg, 0, 4);
  saAsg.CreateAssignment("ClientRecord", "GetClient.List.Current.Client");
  saAsg.CreateAssignment("ClientRecord.IsActive", "IsActive");
  nf2.FalseTarget = saAsg;
  var upd = sa.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateClient"); at(upd, 0, 5);
  upd.Action = client.UpdateAction;
  upd.SetArgumentValue(client.UpdateAction.InputParameters.Single(p => p.Name == "Source"), "ClientRecord");
  saAsg.Target = upd;
  var saEnd = sa.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(saEnd, 0, 6); upd.Target = saEnd;
}
