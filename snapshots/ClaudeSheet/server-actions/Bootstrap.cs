(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IServerAction 'Bootstrap' ***/
    var bootstrap = eSpace.CreateServerAction("Bootstrap");
    bootstrap.CacheInMinutes = null; // default value
    bootstrap.Description = "Seeds reference data when missing: client ICNF, project RUBUS, Portuguese 2026 holidays, and SheetUser + RUBUS assignment for the users in Site.SeedTestUsernames. Safe to run more than once. Run by Timer_Bootstrap.";
    bootstrap.Folder = null; // default value
    bootstrap.Function = false; // default value
    bootstrap.Icon = null; // default value
    bootstrap.Public = false; // default value
    bootstrap.Shareable = false; // default value
    
    var clientId = bootstrap.CreateLocalVariable("ClientId");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client.IdentifierType;
    clientId.SetDefaultValue(null); // default value
    clientId.Description = null; // default value
    
    var projectId = bootstrap.CreateLocalVariable("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.SetDefaultValue(null); // default value
    projectId.Description = null; // default value
    
    var newClient = bootstrap.CreateLocalVariable("NewClient");
    newClient.DataType = client;
    newClient.Description = null; // default value
    
    var newProject = bootstrap.CreateLocalVariable("NewProject");
    newProject.DataType = project;
    newProject.Description = null; // default value
    
    var newAssignment = bootstrap.CreateLocalVariable("NewAssignment");
    var projectAssignment = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    newAssignment.DataType = projectAssignment;
    newAssignment.Description = null; // default value
    
    var seedList = bootstrap.CreateLocalVariable("SeedList");
    seedList.DataType = eSpace.TextType;
    seedList.SetDefaultValue(null); // default value
    seedList.Description = null; // default value
    
    var startNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var getSeedClient = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedClient").ConnectedBelow(startNode, 1371);
    getSeedClient.CacheInMinutes = null; // default value
    getSeedClient.Description = null; // default value
    getSeedClient.IsDisabled = false; // default value
    getSeedClient.SetMaxRecords(null); // default value
    getSeedClient.SetStartIndex(null); // default value
    getSeedClient.Timeout = null; // default value
    var client2 = getSeedClient.AsDatabaseAggregate.CreateSource(client);
    getSeedClient.AsDatabaseAggregate.CreateFilter("Client.Name = \"ICNF\"");
    
    
    var ifNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedClient, 1371);
    ifNode.SetCondition("GetSeedClient.List.Empty");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Client missing?";
    
    var assignNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 5028;
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    assignNode.VerticalPosition = 4913;
    
    var assignment = assignNode.CreateAssignment("ClientId", "GetSeedClient.List.Current.Client.Id");
    ifNode.FalseTarget = assignNode;
    
    var assignNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("NewClient.Name", "\"ICNF\"");
    
    var assignment3 = assignNode2.CreateAssignment("NewClient.IsActive", "True");
    ifNode.TrueTarget = assignNode2;
    
    var getSeedProject = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedProject").ConnectedBelow(assignNode, 1371);
    getSeedProject.CacheInMinutes = null; // default value
    getSeedProject.Description = null; // default value
    getSeedProject.IsDisabled = false; // default value
    getSeedProject.SetMaxRecords(null); // default value
    getSeedProject.SetStartIndex(null); // default value
    getSeedProject.Timeout = null; // default value
    var project2 = getSeedProject.AsDatabaseAggregate.CreateSource(project);
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.ClientId = ClientId");
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.Code = \"RUBUS\"");
    
    
    var createClient = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateClient").ConnectedBelow(assignNode2, 1371);
    createClient.Action = client.CreateAction;
    createClient.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createClient.IsDisabled = false; // default value
    createClient.ServerRequestTimeout = null; // default value
    var createClient2 = client.CreateAction;
    var source = createClient2.InputParameters.Named("Source");
    createClient.SetArgumentValue(source, "NewClient");
    
    var ifNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedProject, 1371);
    ifNode2.SetCondition("GetSeedProject.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Project missing?";
    
    var assignNode3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createClient, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.Target = getSeedProject;
    
    var assignment4 = assignNode3.CreateAssignment("ClientId", "CreateClient.Id");
    
    var assignNode4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode4.HorizontalPosition = 6856;
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.VerticalPosition = 9026;
    
    var assignment5 = assignNode4.CreateAssignment("ProjectId", "GetSeedProject.List.Current.Project.Id");
    ifNode2.FalseTarget = assignNode4;
    
    var assignNode5 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment6 = assignNode5.CreateAssignment("NewProject.ClientId", "ClientId");
    
    var assignment7 = assignNode5.CreateAssignment("NewProject.Name", "\"Resina e Pinha\"");
    
    var assignment8 = assignNode5.CreateAssignment("NewProject.Code", "\"RUBUS\"");
    
    var assignment9 = assignNode5.CreateAssignment("NewProject.IsActive", "True");
    ifNode2.TrueTarget = assignNode5;
    
    var holiday_CreateIfMissing = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing").ConnectedBelow(assignNode4, 1371);
    var holiday_CreateIfMissing2 = eSpace.ServerActions.Named("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing.IsDisabled = false; // default value
    holiday_CreateIfMissing.ServerRequestTimeout = null; // default value
    var holidayDate = holiday_CreateIfMissing2.InputParameters.Named("HolidayDate");
    holiday_CreateIfMissing.SetArgumentValue(holidayDate, "NewDate(2026, 1, 1)");
    var name = holiday_CreateIfMissing2.InputParameters.Named("Name");
    holiday_CreateIfMissing.SetArgumentValue(name, "\"Ano Novo\"");
    
    var createProject = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateProject").ConnectedBelow(assignNode5, 1371);
    createProject.Action = project.CreateAction;
    createProject.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createProject.IsDisabled = false; // default value
    createProject.ServerRequestTimeout = null; // default value
    var createProject2 = project.CreateAction;
    var source2 = createProject2.InputParameters.Named("Source");
    createProject.SetArgumentValue(source2, "NewProject");
    
    var holiday_CreateIfMissing22 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing2").ConnectedBelow(holiday_CreateIfMissing, 1371);
    holiday_CreateIfMissing22.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing22.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing22.IsDisabled = false; // default value
    holiday_CreateIfMissing22.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing22.SetArgumentValue(name, "\"Sexta-feira Santa\"");
    holiday_CreateIfMissing22.SetArgumentValue(holidayDate, "NewDate(2026, 4, 3)");
    
    var assignNode6 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createProject, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.Target = holiday_CreateIfMissing;
    
    var assignment10 = assignNode6.CreateAssignment("ProjectId", "CreateProject.Id");
    
    var holiday_CreateIfMissing3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing3").ConnectedBelow(holiday_CreateIfMissing22, 1371);
    holiday_CreateIfMissing3.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing3.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing3.IsDisabled = false; // default value
    holiday_CreateIfMissing3.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing3.SetArgumentValue(holidayDate, "NewDate(2026, 4, 5)");
    holiday_CreateIfMissing3.SetArgumentValue(name, "\"Páscoa\"");
    
    var holiday_CreateIfMissing4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing4").ConnectedBelow(holiday_CreateIfMissing3, 1371);
    holiday_CreateIfMissing4.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing4.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing4.IsDisabled = false; // default value
    holiday_CreateIfMissing4.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing4.SetArgumentValue(name, "\"Dia da Liberdade\"");
    holiday_CreateIfMissing4.SetArgumentValue(holidayDate, "NewDate(2026, 4, 25)");
    
    var holiday_CreateIfMissing5 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing5").ConnectedBelow(holiday_CreateIfMissing4, 1371);
    holiday_CreateIfMissing5.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing5.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing5.IsDisabled = false; // default value
    holiday_CreateIfMissing5.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing5.SetArgumentValue(name, "\"Dia do Trabalhador\"");
    holiday_CreateIfMissing5.SetArgumentValue(holidayDate, "NewDate(2026, 5, 1)");
    
    var holiday_CreateIfMissing6 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing6").ConnectedBelow(holiday_CreateIfMissing5, 1371);
    holiday_CreateIfMissing6.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing6.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing6.IsDisabled = false; // default value
    holiday_CreateIfMissing6.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing6.SetArgumentValue(name, "\"Corpo de Deus\"");
    holiday_CreateIfMissing6.SetArgumentValue(holidayDate, "NewDate(2026, 6, 4)");
    
    var holiday_CreateIfMissing7 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing7").ConnectedBelow(holiday_CreateIfMissing6, 1371);
    holiday_CreateIfMissing7.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing7.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing7.IsDisabled = false; // default value
    holiday_CreateIfMissing7.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing7.SetArgumentValue(name, "\"Dia de Portugal\"");
    holiday_CreateIfMissing7.SetArgumentValue(holidayDate, "NewDate(2026, 6, 10)");
    
    var holiday_CreateIfMissing8 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing8").ConnectedBelow(holiday_CreateIfMissing7, 1371);
    holiday_CreateIfMissing8.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing8.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing8.IsDisabled = false; // default value
    holiday_CreateIfMissing8.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing8.SetArgumentValue(holidayDate, "NewDate(2026, 8, 15)");
    holiday_CreateIfMissing8.SetArgumentValue(name, "\"Assunção de Nossa Senhora\"");
    
    var holiday_CreateIfMissing9 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing9").ConnectedBelow(holiday_CreateIfMissing8, 1371);
    holiday_CreateIfMissing9.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing9.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing9.IsDisabled = false; // default value
    holiday_CreateIfMissing9.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing9.SetArgumentValue(name, "\"Implantação da República\"");
    holiday_CreateIfMissing9.SetArgumentValue(holidayDate, "NewDate(2026, 10, 5)");
    
    var holiday_CreateIfMissing10 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing10").ConnectedBelow(holiday_CreateIfMissing9, 1371);
    holiday_CreateIfMissing10.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing10.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing10.IsDisabled = false; // default value
    holiday_CreateIfMissing10.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing10.SetArgumentValue(name, "\"Dia de Todos-os-Santos\"");
    holiday_CreateIfMissing10.SetArgumentValue(holidayDate, "NewDate(2026, 11, 1)");
    
    var holiday_CreateIfMissing11 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing11").ConnectedBelow(holiday_CreateIfMissing10, 1371);
    holiday_CreateIfMissing11.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing11.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing11.IsDisabled = false; // default value
    holiday_CreateIfMissing11.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing11.SetArgumentValue(name, "\"Restauração da Independência\"");
    holiday_CreateIfMissing11.SetArgumentValue(holidayDate, "NewDate(2026, 12, 1)");
    
    var holiday_CreateIfMissing12 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing12").ConnectedBelow(holiday_CreateIfMissing11, 1371);
    holiday_CreateIfMissing12.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing12.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing12.IsDisabled = false; // default value
    holiday_CreateIfMissing12.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing12.SetArgumentValue(name, "\"Imaculada Conceição\"");
    holiday_CreateIfMissing12.SetArgumentValue(holidayDate, "NewDate(2026, 12, 8)");
    
    var holiday_CreateIfMissing13 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing13").ConnectedBelow(holiday_CreateIfMissing12, 1371);
    holiday_CreateIfMissing13.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing13.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing13.IsDisabled = false; // default value
    holiday_CreateIfMissing13.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing13.SetArgumentValue(holidayDate, "NewDate(2026, 12, 25)");
    holiday_CreateIfMissing13.SetArgumentValue(name, "\"Natal\"");
    
    var assignNode7 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(holiday_CreateIfMissing13, 1371);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    
    var assignment11 = assignNode7.CreateAssignment("SeedList", "\",\" + Replace(Site.SeedTestUsernames, \" \", \"\") + \",\"");
    
    var getSeedUsers = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedUsers").ConnectedBelow(assignNode7, 1371);
    getSeedUsers.CacheInMinutes = null; // default value
    getSeedUsers.Description = null; // default value
    getSeedUsers.IsDisabled = false; // default value
    getSeedUsers.SetMaxRecords(null); // default value
    getSeedUsers.SetStartIndex(null); // default value
    getSeedUsers.Timeout = null; // default value
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getSeedUsers.AsDatabaseAggregate.CreateSource(user2);
    getSeedUsers.AsDatabaseAggregate.CreateFilter("User.Is_Active");
    getSeedUsers.AsDatabaseAggregate.CreateFilter("Index(SeedList, \",\" + User.Username + \",\") >= 0");
    
    
    var forEachNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getSeedUsers, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetSeedUsers.List");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 1828);
    ifNode3.SetCondition("CheckSheetUserRole(GetSeedUsers.List.Current.User.Id)");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Has SheetUser?";
    forEachNode.CycleTarget = ifNode3;
    
    var endNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 1371);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var grantSheetUserRole = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetUserRole");
    var sheetUser = eSpace.Roles.Named("SheetUser");
    var grantSheetUserRole2 = sheetUser.RoleActions.Named("GrantSheetUserRole");
    grantSheetUserRole.Action = grantSheetUserRole2;
    grantSheetUserRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    grantSheetUserRole.HorizontalPosition = 10512;
    grantSheetUserRole.IsDisabled = false; // default value
    grantSheetUserRole.ServerRequestTimeout = null; // default value
    grantSheetUserRole.VerticalPosition = 32333;
    var userId = grantSheetUserRole2.InputParameters.Named("UserId");
    grantSheetUserRole.SetArgumentValue(userId, "GetSeedUsers.List.Current.User.Id");
    ifNode3.FalseTarget = grantSheetUserRole;
    
    var getSeedAssignment = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedAssignment").ConnectedToTheLeftOf(grantSheetUserRole, 1828);
    getSeedAssignment.CacheInMinutes = null; // default value
    getSeedAssignment.Description = null; // default value
    getSeedAssignment.IsDisabled = false; // default value
    getSeedAssignment.SetMaxRecords(null); // default value
    getSeedAssignment.SetStartIndex(null); // default value
    getSeedAssignment.Timeout = null; // default value
    var projectAssignment2 = getSeedAssignment.AsDatabaseAggregate.CreateSource(projectAssignment);
    getSeedAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = GetSeedUsers.List.Current.User.Id");
    getSeedAssignment.AsDatabaseAggregate.CreateFilter("ProjectAssignment.ProjectId = ProjectId");
    
    ifNode3.TrueTarget = getSeedAssignment;
    
    var ifNode4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getSeedAssignment, 1371);
    ifNode4.SetCondition("GetSeedAssignment.List.Empty");
    ifNode4.FalseTarget = forEachNode;
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Assignment missing?";
    
    var assignNode8 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    
    var assignment12 = assignNode8.CreateAssignment("NewAssignment.UserId", "GetSeedUsers.List.Current.User.Id");
    
    var assignment13 = assignNode8.CreateAssignment("NewAssignment.ProjectId", "ProjectId");
    
    var assignment14 = assignNode8.CreateAssignment("NewAssignment.StartDate", "NewDate(2026, 1, 1)");
    
    var assignment15 = assignNode8.CreateAssignment("NewAssignment.EndDate", "NullDate()");
    
    var assignment16 = assignNode8.CreateAssignment("NewAssignment.IsActive", "True");
    ifNode4.TrueTarget = assignNode8;
    
    var createProjectAssignment = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateProjectAssignment").ConnectedBelow(assignNode8, 1371);
    createProjectAssignment.Action = projectAssignment.CreateAction;
    createProjectAssignment.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createProjectAssignment.IsDisabled = false; // default value
    createProjectAssignment.ServerRequestTimeout = null; // default value
    createProjectAssignment.Target = forEachNode;
    var createProjectAssignment2 = projectAssignment.CreateAction;
    var source3 = createProjectAssignment2.InputParameters.Named("Source");
    createProjectAssignment.SetArgumentValue(source3, "NewAssignment");
}
