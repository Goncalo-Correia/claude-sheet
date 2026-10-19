# Bootstrap

Server action in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Bootstrap' ***/
    var bootstrap = eSpace.CreateServerAction("Bootstrap");
    bootstrap.Description = "Seeds reference data when missing: client ICNF, project RUBUS, Portuguese 2026 holidays, SheetUser + RUBUS assignment for the users in Site.SeedTestUsernames, and SheetAdmin + SheetUser for the users in Site.SeedAdminUsernames. Safe to run more than once. Run by Timer_Bootstrap.";
    var setup = eSpace.Folders.Named("Setup");
    bootstrap.Folder = setup;
    
    var clientId = bootstrap.CreateLocalVariable("ClientId");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client.IdentifierType;
    
    var projectId = bootstrap.CreateLocalVariable("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    
    var newClient = bootstrap.CreateLocalVariable("NewClient");
    newClient.DataType = client;
    
    var newProject = bootstrap.CreateLocalVariable("NewProject");
    newProject.DataType = project;
    
    var newAssignment = bootstrap.CreateLocalVariable("NewAssignment");
    var projectAssignment = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    newAssignment.DataType = projectAssignment;
    
    var seedList = bootstrap.CreateLocalVariable("SeedList");
    seedList.DataType = eSpace.TextType;
    
    var startNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var getSeedClient = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedClient").ConnectedBelow(startNode, 1371);
    var client2 = getSeedClient.AsDatabaseAggregate.CreateSource(client);
    getSeedClient.AsDatabaseAggregate.CreateFilter("Client.Name = \"ICNF\"");
    
    
    var ifNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedClient, 1371);
    ifNode.SetCondition("GetSeedClient.List.Empty");
    ifNode.Label = "Client missing?";
    
    var assignNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode.Label = "Use existing client";
    
    var assignment = assignNode.CreateAssignment("ClientId", "GetSeedClient.List.Current.Client.Id");
    ifNode.FalseTarget = assignNode;
    
    var assignNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode2.Label = "New ICNF client";
    
    var assignment2 = assignNode2.CreateAssignment("NewClient.Name", "\"ICNF\"");
    
    var assignment3 = assignNode2.CreateAssignment("NewClient.IsActive", "True");
    ifNode.TrueTarget = assignNode2;
    
    var getSeedProject = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedProject");
    getSeedProject.HorizontalPosition = 3200;
    getSeedProject.VerticalPosition = 9026;
    var project2 = getSeedProject.AsDatabaseAggregate.CreateSource(project);
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.ClientId = ClientId");
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.Code = \"RUBUS\"");
    
    assignNode.Target = getSeedProject;
    
    var createClient = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateClient").ConnectedBelow(assignNode2, 1371);
    createClient.Action = client.CreateAction;
    var createClient2 = client.CreateAction;
    var source = createClient2.InputParameters.Named("Source");
    createClient.SetArgumentValue(source, "NewClient");
    
    var ifNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedProject, 1371);
    ifNode2.SetCondition("GetSeedProject.List.Empty");
    ifNode2.Label = "Project missing?";
    
    var assignNode3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createClient, 1371);
    assignNode3.Label = "Keep new client Id";
    assignNode3.Target = getSeedProject;
    
    var assignment4 = assignNode3.CreateAssignment("ClientId", "CreateClient.Id");
    
    var assignNode4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode4.Label = "Use existing project";
    
    var assignment5 = assignNode4.CreateAssignment("ProjectId", "GetSeedProject.List.Current.Project.Id");
    ifNode2.FalseTarget = assignNode4;
    
    var assignNode5 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode5.Label = "New RUBUS project";
    
    var assignment6 = assignNode5.CreateAssignment("NewProject.ClientId", "ClientId");
    
    var assignment7 = assignNode5.CreateAssignment("NewProject.Name", "\"Resina e Pinha\"");
    
    var assignment8 = assignNode5.CreateAssignment("NewProject.Code", "\"RUBUS\"");
    
    var assignment9 = assignNode5.CreateAssignment("NewProject.IsActive", "True");
    ifNode2.TrueTarget = assignNode5;
    
    var createHoliday_NewYear = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_NewYear");
    var holiday_CreateIfMissing = eSpace.ServerActions.Named("Holiday_CreateIfMissing");
    createHoliday_NewYear.Action = holiday_CreateIfMissing;
    createHoliday_NewYear.HorizontalPosition = 3200;
    createHoliday_NewYear.VerticalPosition = 15881;
    var holidayDate = holiday_CreateIfMissing.InputParameters.Named("HolidayDate");
    createHoliday_NewYear.SetArgumentValue(holidayDate, "NewDate(2026, 1, 1)");
    var isFixedDate = holiday_CreateIfMissing.InputParameters.Named("IsFixedDate");
    createHoliday_NewYear.SetArgumentValue(isFixedDate, null);
    var name = holiday_CreateIfMissing.InputParameters.Named("Name");
    createHoliday_NewYear.SetArgumentValue(name, "\"Ano Novo\"");
    assignNode4.Target = createHoliday_NewYear;
    
    var createProject = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateProject").ConnectedBelow(assignNode5, 1371);
    createProject.Action = project.CreateAction;
    var createProject2 = project.CreateAction;
    var source2 = createProject2.InputParameters.Named("Source");
    createProject.SetArgumentValue(source2, "NewProject");
    
    var createHoliday_GoodFriday = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_GoodFriday").ConnectedBelow(createHoliday_NewYear, 1371);
    createHoliday_GoodFriday.Action = holiday_CreateIfMissing;
    createHoliday_GoodFriday.SetArgumentValue(isFixedDate, "False");
    createHoliday_GoodFriday.SetArgumentValue(holidayDate, "NewDate(2026, 4, 3)");
    createHoliday_GoodFriday.SetArgumentValue(name, "\"Sexta-feira Santa\"");
    
    var assignNode6 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createProject, 1371);
    assignNode6.Label = "Keep new project Id";
    assignNode6.Target = createHoliday_NewYear;
    
    var assignment10 = assignNode6.CreateAssignment("ProjectId", "CreateProject.Id");
    
    var createHoliday_Easter = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_Easter").ConnectedBelow(createHoliday_GoodFriday, 1371);
    createHoliday_Easter.Action = holiday_CreateIfMissing;
    createHoliday_Easter.SetArgumentValue(isFixedDate, "False");
    createHoliday_Easter.SetArgumentValue(holidayDate, "NewDate(2026, 4, 5)");
    createHoliday_Easter.SetArgumentValue(name, "\"Páscoa\"");
    
    var createHoliday_FreedomDay = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_FreedomDay").ConnectedBelow(createHoliday_Easter, 1371);
    createHoliday_FreedomDay.Action = holiday_CreateIfMissing;
    createHoliday_FreedomDay.SetArgumentValue(holidayDate, "NewDate(2026, 4, 25)");
    createHoliday_FreedomDay.SetArgumentValue(name, "\"Dia da Liberdade\"");
    createHoliday_FreedomDay.SetArgumentValue(isFixedDate, null);
    
    var createHoliday_LabourDay = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_LabourDay").ConnectedBelow(createHoliday_FreedomDay, 1371);
    createHoliday_LabourDay.Action = holiday_CreateIfMissing;
    createHoliday_LabourDay.SetArgumentValue(holidayDate, "NewDate(2026, 5, 1)");
    createHoliday_LabourDay.SetArgumentValue(isFixedDate, null);
    createHoliday_LabourDay.SetArgumentValue(name, "\"Dia do Trabalhador\"");
    
    var createHoliday_CorpusChristi = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_CorpusChristi").ConnectedBelow(createHoliday_LabourDay, 1371);
    createHoliday_CorpusChristi.Action = holiday_CreateIfMissing;
    createHoliday_CorpusChristi.SetArgumentValue(holidayDate, "NewDate(2026, 6, 4)");
    createHoliday_CorpusChristi.SetArgumentValue(isFixedDate, "False");
    createHoliday_CorpusChristi.SetArgumentValue(name, "\"Corpo de Deus\"");
    
    var createHoliday_PortugalDay = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_PortugalDay").ConnectedBelow(createHoliday_CorpusChristi, 1371);
    createHoliday_PortugalDay.Action = holiday_CreateIfMissing;
    createHoliday_PortugalDay.SetArgumentValue(isFixedDate, null);
    createHoliday_PortugalDay.SetArgumentValue(holidayDate, "NewDate(2026, 6, 10)");
    createHoliday_PortugalDay.SetArgumentValue(name, "\"Dia de Portugal\"");
    
    var createHoliday_Assumption = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_Assumption").ConnectedBelow(createHoliday_PortugalDay, 1371);
    createHoliday_Assumption.Action = holiday_CreateIfMissing;
    createHoliday_Assumption.SetArgumentValue(isFixedDate, null);
    createHoliday_Assumption.SetArgumentValue(holidayDate, "NewDate(2026, 8, 15)");
    createHoliday_Assumption.SetArgumentValue(name, "\"Assunção de Nossa Senhora\"");
    
    var createHoliday_RepublicDay = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_RepublicDay").ConnectedBelow(createHoliday_Assumption, 1371);
    createHoliday_RepublicDay.Action = holiday_CreateIfMissing;
    createHoliday_RepublicDay.SetArgumentValue(isFixedDate, null);
    createHoliday_RepublicDay.SetArgumentValue(holidayDate, "NewDate(2026, 10, 5)");
    createHoliday_RepublicDay.SetArgumentValue(name, "\"Implantação da República\"");
    
    var createHoliday_AllSaints = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_AllSaints").ConnectedBelow(createHoliday_RepublicDay, 1371);
    createHoliday_AllSaints.Action = holiday_CreateIfMissing;
    createHoliday_AllSaints.SetArgumentValue(name, "\"Dia de Todos-os-Santos\"");
    createHoliday_AllSaints.SetArgumentValue(holidayDate, "NewDate(2026, 11, 1)");
    createHoliday_AllSaints.SetArgumentValue(isFixedDate, null);
    
    var createHoliday_IndependenceDay = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_IndependenceDay").ConnectedBelow(createHoliday_AllSaints, 1371);
    createHoliday_IndependenceDay.Action = holiday_CreateIfMissing;
    createHoliday_IndependenceDay.SetArgumentValue(holidayDate, "NewDate(2026, 12, 1)");
    createHoliday_IndependenceDay.SetArgumentValue(name, "\"Restauração da Independência\"");
    createHoliday_IndependenceDay.SetArgumentValue(isFixedDate, null);
    
    var createHoliday_ImmaculateConception = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_ImmaculateConception").ConnectedBelow(createHoliday_IndependenceDay, 1371);
    createHoliday_ImmaculateConception.Action = holiday_CreateIfMissing;
    createHoliday_ImmaculateConception.SetArgumentValue(isFixedDate, null);
    createHoliday_ImmaculateConception.SetArgumentValue(name, "\"Imaculada Conceição\"");
    createHoliday_ImmaculateConception.SetArgumentValue(holidayDate, "NewDate(2026, 12, 8)");
    
    var createHoliday_Christmas = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday_Christmas").ConnectedBelow(createHoliday_ImmaculateConception, 1371);
    createHoliday_Christmas.Action = holiday_CreateIfMissing;
    createHoliday_Christmas.SetArgumentValue(name, "\"Natal\"");
    createHoliday_Christmas.SetArgumentValue(isFixedDate, null);
    createHoliday_Christmas.SetArgumentValue(holidayDate, "NewDate(2026, 12, 25)");
    
    var assignNode7 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createHoliday_Christmas, 1371);
    assignNode7.Label = "Test user list";
    
    var assignment11 = assignNode7.CreateAssignment("SeedList", "\",\" + Replace(Site.SeedTestUsernames, \" \", \"\") + \",\"");
    
    var getSeedUsers = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedUsers").ConnectedBelow(assignNode7, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getSeedUsers.AsDatabaseAggregate.CreateSource(user2);
    getSeedUsers.AsDatabaseAggregate.CreateFilter("User.Is_Active");
    getSeedUsers.AsDatabaseAggregate.CreateFilter("Index(SeedList, \",\" + User.Username + \",\") >= 0");
    
    
    var forEachNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getSeedUsers, 1371);
    forEachNode.Label = "Each test user";
    forEachNode.SetRecordList("GetSeedUsers.List");
    
    var ifNode3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode3.SetCondition("CheckSheetUserRole(GetSeedUsers.List.Current.User.Id)");
    ifNode3.Label = "Has SheetUser?";
    forEachNode.CycleTarget = ifNode3;
    
    var assignNode8 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(forEachNode, 6855);
    assignNode8.Label = "Admin seed list";
    
    var assignment12 = assignNode8.CreateAssignment("SeedList", "\",\" + Replace(Site.SeedAdminUsernames, \" \", \"\") + \",\"");
    forEachNode.Target = assignNode8;
    
    var grantSheetUserRole = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetUserRole").ToTheRightOf(ifNode3, 1828);
    var sheetUser = eSpace.Roles.Named("SheetUser");
    var grantSheetUserRole2 = sheetUser.RoleActions.Named("GrantSheetUserRole");
    grantSheetUserRole.Action = grantSheetUserRole2;
    var userId = grantSheetUserRole2.InputParameters.Named("UserId");
    grantSheetUserRole.SetArgumentValue(userId, "GetSeedUsers.List.Current.User.Id");
    ifNode3.FalseTarget = grantSheetUserRole;
    
    var getSeedAssignment = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedAssignment").Below(ifNode3, 1371);
    var projectAssignment2 = getSeedAssignment.AsDatabaseAggregate.CreateSource(projectAssignment);
    getSeedAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = GetSeedUsers.List.Current.User.Id");
    getSeedAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.ProjectId = ProjectId");
    
    ifNode3.TrueTarget = getSeedAssignment;
    
    var getSeedAdmins = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedAdmins").ConnectedBelow(assignNode8, 1371);
    var user3 = getSeedAdmins.AsDatabaseAggregate.CreateSource(user2);
    getSeedAdmins.AsDatabaseAggregate.CreateFilter("User.Is_Active");
    getSeedAdmins.AsDatabaseAggregate.CreateFilter("Index(SeedList, \",\" + User.Username + \",\") >= 0");
    
    grantSheetUserRole.Target = getSeedAssignment;
    
    var ifNode4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedAssignment, 1371);
    ifNode4.SetCondition("GetSeedAssignment.List.Empty");
    ifNode4.FalseTarget = forEachNode;
    ifNode4.Label = "Assignment missing?";
    
    var forEachNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getSeedAdmins, 1371);
    forEachNode2.Label = "Each admin user";
    forEachNode2.SetRecordList("GetSeedAdmins.List");
    
    var assignNode9 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode9.Label = "New RUBUS assignment";
    
    var assignment13 = assignNode9.CreateAssignment("NewAssignment.UserId", "GetSeedUsers.List.Current.User.Id");
    
    var assignment14 = assignNode9.CreateAssignment("NewAssignment.ProjectId", "ProjectId");
    
    var assignment15 = assignNode9.CreateAssignment("NewAssignment.StartDate", "NewDate(2026, 1, 1)");
    
    var assignment16 = assignNode9.CreateAssignment("NewAssignment.EndDate", "NullDate()");
    
    var assignment17 = assignNode9.CreateAssignment("NewAssignment.IsActive", "True");
    ifNode4.TrueTarget = assignNode9;
    
    var ifNode5 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode2, 1828);
    ifNode5.SetCondition("CheckSheetUserRole(GetSeedAdmins.List.Current.User.Id)");
    ifNode5.Label = "Has SheetUser?";
    forEachNode2.CycleTarget = ifNode5;
    
    var endNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode2, 4113);
    forEachNode2.Target = endNode;
    
    var createProjectAssignment = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateProjectAssignment").ConnectedBelow(assignNode9, 1371);
    createProjectAssignment.Action = projectAssignment.CreateAction;
    createProjectAssignment.Target = forEachNode;
    var createProjectAssignment2 = projectAssignment.CreateAction;
    var source3 = createProjectAssignment2.InputParameters.Named("Source");
    createProjectAssignment.SetArgumentValue(source3, "NewAssignment");
    
    var grantSheetUserRole_Admin = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetUserRole_Admin").ToTheRightOf(ifNode5, 1828);
    grantSheetUserRole_Admin.Action = grantSheetUserRole2;
    grantSheetUserRole_Admin.SetArgumentValue(userId, "GetSeedAdmins.List.Current.User.Id");
    ifNode5.FalseTarget = grantSheetUserRole_Admin;
    
    var ifNode6 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode5, 1371);
    ifNode6.SetCondition("CheckSheetAdminRole(GetSeedAdmins.List.Current.User.Id)");
    ifNode6.Label = "Has SheetAdmin?";
    ifNode6.TrueTarget = forEachNode2;
    ifNode5.TrueTarget = ifNode6;
    grantSheetUserRole_Admin.Target = ifNode6;
    
    var grantSheetAdminRole = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetAdminRole").Below(ifNode6, 1371);
    var sheetAdmin = eSpace.Roles.Named("SheetAdmin");
    var grantSheetAdminRole2 = sheetAdmin.RoleActions.Named("GrantSheetAdminRole");
    grantSheetAdminRole.Action = grantSheetAdminRole2;
    grantSheetAdminRole.Target = forEachNode2;
    var userId2 = grantSheetAdminRole2.InputParameters.Named("UserId");
    grantSheetAdminRole.SetArgumentValue(userId2, "GetSeedAdmins.List.Current.User.Id");
    ifNode6.FalseTarget = grantSheetAdminRole;
}
```
