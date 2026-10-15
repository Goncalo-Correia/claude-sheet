// Published: 2026-10-15T17:09:41.123041+00:00 (manual 1-Click Publish; logged by Claude, not the hook)
// Module:   ClaudeSheet
// When:     2026-10-15T16:52:52.209185+00:00
// Story:    US4
// Outcome:  saved with 1 validation error(s)
// Imports:  -
// Error:    /ProjectHours_GetWeekly/Build day header/DayHeader.DayName = If(DayOfWeek(DayHeader.Date) = 1, "Monday", If(DayOfWeek(DayHeader.Date) = 2, "Tuesday", If(DayOfWeek(DayHeader.Date) = 3, "Wednesday", If(DayOfWeek(DayHeader.Date) = 4, "Thursday", If(DayOfWeek(DayHeader.Date) = 5, "Friday", If(DayOfWeek(DayHeader.Date) = 6, "Saturday", "Sunday")))))) - Invalid Data Type (fixed by the next save)
eSpace => {
    // ---- US4: extend the TT0 weekly structures (unused until now) ----
    var wdh = eSpace.Structures.Named("WeekDayHeader");
    wdh.Attributes.Named("DayLabel").Name = "DayName";
    var dateLabel = wdh.CreateAttribute("DateLabel");
    dateLabel.DataType = eSpace.TextType; dateLabel.Length = 10;
    var wcell = eSpace.Structures.Named("WeeklyCell");
    var showDash = wcell.CreateAttribute("ShowDash");
    showDash.DataType = eSpace.BooleanType;
    showDash.Description = "True when the cell shows \"-\": no entry AND the day is in the future, a weekend, a holiday or a day off.";
    var wrow = eSpace.Structures.Named("WeeklyProjectRow");
    var wh = eSpace.Structures.Named("WeeklyHours");
    wh.Attributes.Named("TotalMinutes").Name = "GrandTotalMinutes";
    var dayTotals = wh.CreateAttribute("DayTotals"); dayTotals.DataType = eSpace.GetOrCreateListType(wcell);
    dayTotals.Description = "Total per day (7 cells); ShowDash when every row is \"-\" that day.";

    // ---- server action ProjectHours_GetWeekly ----
    var act = eSpace.CreateServerAction("ProjectHours_GetWeekly");
    act.Description = "Weekly grid for the current user: the Monday-Sunday week containing ReferenceDate, day headers classified like the calendar (Calendar_GetMonth), one row per project assigned during the week or with entries in it (sorted by client, project), 7 cells per row with the \"-\" rule, totals per row, per day and for the week.";
    var pRef = act.CreateInputParameter("ReferenceDate"); pRef.DataType = eSpace.DateType; pRef.IsMandatory = true;
    var outWeek = act.CreateOutputParameter("Week"); outWeek.DataType = wh;
    var vIdx = act.CreateLocalVariable("DayIndex"); vIdx.DataType = eSpace.IntegerType;
    var vDay = act.CreateLocalVariable("DayHeader"); vDay.DataType = wdh;
    var vRow = act.CreateLocalVariable("Row"); vRow.DataType = wrow;
    var vCell = act.CreateLocalVariable("Cell"); vCell.DataType = wcell;

    var start = act.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    start.HorizontalPosition = 3200; start.VerticalPosition = 800;

    var ifRole = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(start, 1371);
    ifRole.SetCondition("CheckSheetUserRole()"); ifRole.Label = "Is SheetUser?";
    var raise = act.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifRole, 1828);
    raise.Exception = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raise.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifRole.FalseTarget = raise;

    var aWeek = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifRole, 1371);
    aWeek.Label = "Week bounds";
    aWeek.CreateAssignment("Week.WeekStart", "Date_GetWeekStart(ReferenceDate)");
    aWeek.CreateAssignment("Week.WeekEnd", "DateTimeToDate(AddDays(Week.WeekStart, 6))");
    ifRole.TrueTarget = aWeek;

    var calGetMonth = eSpace.ServerActions.Named("Calendar_GetMonth");
    var m1 = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("StartMonth").ConnectedBelow(aWeek, 1371);
    m1.Action = calGetMonth;
    m1.SetArgumentValue(calGetMonth.InputParameters.Named("Year"), "Year(Week.WeekStart)");
    m1.SetArgumentValue(calGetMonth.InputParameters.Named("Month"), "Month(Week.WeekStart)");
    var m2 = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("EndMonth").ConnectedBelow(m1, 1371);
    m2.Action = calGetMonth;
    m2.SetArgumentValue(calGetMonth.InputParameters.Named("Year"), "Year(Week.WeekEnd)");
    m2.SetArgumentValue(calGetMonth.InputParameters.Named("Month"), "Month(Week.WeekEnd)");
    var sm = m1.Name; var em = m2.Name;

    var aIdx0 = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(m2, 1371);
    aIdx0.CreateAssignment("DayIndex", "0");
    var ifDays = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(aIdx0, 1371);
    ifDays.SetCondition("DayIndex < 7"); ifDays.Label = "More days?";

    Func<string, string> fromCal = attr => "If(Month(DayHeader.Date) = Month(Week.WeekStart), " + sm + ".Days[Day(DayHeader.Date) - 1]." + attr + ", " + em + ".Days[Day(DayHeader.Date) - 1]." + attr + ")";
    var aDay = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifDays, 1828);
    aDay.Label = "Build day header";
    aDay.CreateAssignment("DayHeader.Date", "DateTimeToDate(AddDays(Week.WeekStart, DayIndex))");
    aDay.CreateAssignment("DayHeader.DayName", "If(DayOfWeek(DayHeader.Date) = 1, \"Monday\", If(DayOfWeek(DayHeader.Date) = 2, \"Tuesday\", If(DayOfWeek(DayHeader.Date) = 3, \"Wednesday\", If(DayOfWeek(DayHeader.Date) = 4, \"Thursday\", If(DayOfWeek(DayHeader.Date) = 5, \"Friday\", If(DayOfWeek(DayHeader.Date) = 6, \"Saturday\", \"Sunday\"))))))");
    aDay.CreateAssignment("DayHeader.DateLabel", "FormatDateTime(DayHeader.Date, \"dd/MM\")");
    aDay.CreateAssignment("DayHeader.IsWeekend", "DayOfWeek(DayHeader.Date) = 0 or DayOfWeek(DayHeader.Date) = 6");
    aDay.CreateAssignment("DayHeader.IsToday", "DayHeader.Date = CurrDate()");
    aDay.CreateAssignment("DayHeader.DayTypeId", fromCal("DayTypeId"));
    aDay.CreateAssignment("DayHeader.CssClass", fromCal("CssClass"));
    aDay.CreateAssignment("DayHeader.IsEditable", fromCal("IsEditable"));
    aDay.CreateAssignment("Cell.Date", "DayHeader.Date");
    aDay.CreateAssignment("Cell.Minutes", "0");
    aDay.CreateAssignment("Cell.ShowDash", "True");
    ifDays.TrueTarget = aDay;

    var sys = eSpace.References.Named("(System)");
    var listAppend = sys.ServerActions.Named("ListAppend");
    var listClear = sys.ServerActions.Named("ListClear");
    Func<OutSystems.Model.Logic.Nodes.IActionNode, string, string, OutSystems.Model.Logic.Nodes.IExecuteServerActionNode> append = (prev, list, elem) => {
        var n = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(prev, 1371);
        n.Action = listAppend;
        n.SetArgumentValue(listAppend.InputParameters.Named("List"), list);
        n.SetArgumentValue(listAppend.InputParameters.Named("Element"), elem);
        return n;
    };
    var apDay = append(aDay, "Week.Days", "DayHeader");
    var apTot = append(apDay, "Week.DayTotals", "Cell");
    var aNextDay = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(apTot, 1371);
    aNextDay.CreateAssignment("DayIndex", "DayIndex + 1");
    aNextDay.Target = ifDays;

    // projects assigned during the week OR with entries in the week, sorted by client then project
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var sql = act.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetWeekProjects").Below(ifDays, 1371);
    ifDays.FalseTarget = sql;
    var userEnt = (OutSystems.Model.Data.IServerEntitySignature)sys.Entities.Named("User");
    var pUser = sql.CreateInputParameter("UserId"); pUser.DataType = userEnt.IdentifierType; sql.SetArgumentValue(pUser, "GetUserId()");
    var pWs = sql.CreateInputParameter("WeekStart"); pWs.DataType = eSpace.DateType; sql.SetArgumentValue(pWs, "Week.WeekStart");
    var pWe = sql.CreateInputParameter("WeekEnd"); pWe.DataType = eSpace.DateType; sql.SetArgumentValue(pWe, "Week.WeekEnd");
    var pNd = sql.CreateInputParameter("NullDate"); pNd.DataType = eSpace.DateType; sql.SetArgumentValue(pNd, "NullDate()");
    sql.CreateOutput(project);
    sql.CreateOutput(client);
    sql.Statement =
        "SELECT {Project}.*, {Client}.*\n" +
        "FROM {Project}\n" +
        "INNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\n" +
        "WHERE {Project}.[Id] IN (\n" +
        "    SELECT {ProjectAssignment}.[ProjectId] FROM {ProjectAssignment}\n" +
        "    WHERE {ProjectAssignment}.[UserId] = @UserId AND {ProjectAssignment}.[IsActive] = 1\n" +
        "      AND {ProjectAssignment}.[StartDate] <= @WeekEnd\n" +
        "      AND ({ProjectAssignment}.[EndDate] = @NullDate OR {ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] >= @WeekStart)\n" +
        "      AND {Project}.[IsActive] = 1 AND {Client}.[IsActive] = 1)\n" +
        "OR {Project}.[Id] IN (\n" +
        "    SELECT {TimeEntry}.[ProjectId] FROM {TimeEntry}\n" +
        "    INNER JOIN {DaySheet} ON {DaySheet}.[Id] = {TimeEntry}.[DaySheetId]\n" +
        "    WHERE {DaySheet}.[UserId] = @UserId AND {DaySheet}.[SheetDate] >= @WeekStart AND {DaySheet}.[SheetDate] <= @WeekEnd)\n" +
        "ORDER BY {Client}.[Name], {Project}.[Name]";
    var sqlName = sql.Name;

    // minutes per project per day (single grouped aggregate)
    var timeEntry = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var agg = act.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetWeekEntries").ConnectedBelow(sql, 1371);
    var dba = agg.AsDatabaseAggregate;
    var sTe = dba.CreateSource(timeEntry); var sDs = dba.CreateSource(daySheet);
    var j = dba.CreateJoin(); j.LeftSource = sTe; j.RightSource = sDs; j.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    j.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    dba.CreateFilter("DaySheet.UserId = GetUserId()");
    dba.CreateFilter("DaySheet.SheetDate >= Week.WeekStart and DaySheet.SheetDate <= Week.WeekEnd");
    dba.CreateGroupByAttribute("ProjectId").SetAttribute("TimeEntry.ProjectId");
    dba.CreateGroupByAttribute("SheetDate").SetAttribute("DaySheet.SheetDate");
    var sum = dba.CreateAggregatedAttribute("Minutes"); sum.SetAttribute("TimeEntry.Minutes"); sum.AggregationType = OutSystems.Model.Enumerations.AggregationType.Sum;
    var aggName = agg.Name;

    // one row per project, 7 cells each
    var feRows = act.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(agg, 1371);
    feRows.SetRecordList(sqlName + ".List");
    var cur = sqlName + ".List.Current";
    var aRow = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(feRows, 1828);
    aRow.Label = "Start row";
    aRow.CreateAssignment("Row.ProjectId", cur + ".Project.Id");
    aRow.CreateAssignment("Row.ClientName", cur + ".Client.Name");
    aRow.CreateAssignment("Row.ProjectName", cur + ".Project.Name");
    aRow.CreateAssignment("Row.ProjectCode", cur + ".Project.Code");
    aRow.CreateAssignment("Row.TotalMinutes", "0");
    aRow.CreateAssignment("DayIndex", "0");
    feRows.CycleTarget = aRow;
    var clr = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListClear").ConnectedBelow(aRow, 1371);
    clr.Action = listClear;
    clr.SetArgumentValue(listClear.InputParameters.Named("List"), "Row.Cells");
    var ifCells = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(clr, 1371);
    ifCells.SetCondition("DayIndex < 7"); ifCells.Label = "More cells?";
    var aCell = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifCells, 1828);
    aCell.Label = "Start cell";
    aCell.CreateAssignment("Cell.Date", "Week.Days[DayIndex].Date");
    aCell.CreateAssignment("Cell.Minutes", "0");
    ifCells.TrueTarget = aCell;
    var feEnt = act.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(aCell, 1371);
    feEnt.SetRecordList(aggName + ".List");
    var ifMatch = act.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(feEnt, 1828);
    ifMatch.SetCondition(aggName + ".List.Current.ProjectId = Row.ProjectId and " + aggName + ".List.Current.SheetDate = Cell.Date");
    ifMatch.Label = "Same project and day?";
    feEnt.CycleTarget = ifMatch;
    var aAdd = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifMatch, 1371);
    aAdd.CreateAssignment("Cell.Minutes", "Cell.Minutes + LongIntegerToInteger(" + aggName + ".List.Current.Minutes)");
    ifMatch.TrueTarget = aAdd;
    ifMatch.FalseTarget = feEnt;
    aAdd.Target = feEnt;
    var aTot = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(feEnt, 1371);
    aTot.Label = "Dash rule and totals";
    aTot.CreateAssignment("Cell.ShowDash", "Cell.Minutes = 0 and (Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Future or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Weekend or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Holiday or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.DayOff)");
    aTot.CreateAssignment("Row.TotalMinutes", "Row.TotalMinutes + Cell.Minutes");
    aTot.CreateAssignment("Week.DayTotals[DayIndex].Minutes", "Week.DayTotals[DayIndex].Minutes + Cell.Minutes");
    aTot.CreateAssignment("Week.DayTotals[DayIndex].ShowDash", "Week.DayTotals[DayIndex].ShowDash and Cell.ShowDash");
    aTot.CreateAssignment("Week.GrandTotalMinutes", "Week.GrandTotalMinutes + Cell.Minutes");
    feEnt.Target = aTot;
    var apCell = append(aTot, "Row.Cells", "Cell");
    var aNextCell = act.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(apCell, 1371);
    aNextCell.CreateAssignment("DayIndex", "DayIndex + 1");
    aNextCell.Target = ifCells;
    var apRow = act.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").Below(ifCells, 1371);
    apRow.Action = listAppend;
    apRow.SetArgumentValue(listAppend.InputParameters.Named("List"), "Week.Rows");
    apRow.SetArgumentValue(listAppend.InputParameters.Named("Element"), "Row");
    ifCells.FalseTarget = apRow;
    apRow.Target = feRows;

    var end = act.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(feRows, 1371);
    feRows.Target = end;
}
