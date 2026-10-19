# US8 · ClaudeSheet_CS changes

Every `applyModelApiCode` save for US8 in ClaudeSheet_CS, oldest first. Logged when the module was published.

## Save 1 · 2026-10-19 15:49:17 UTC

- **Published:** 2026-10-19 16:00:38 UTC (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  var hol = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
  var roleEx = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetAdmin");
  var hv = eSpace.CreateUserException("HolidayValidation");

  var fx = hol.CreateAttribute("IsFixedDate");
  fx.DataType = eSpace.BooleanType; fx.SetDefaultValue("True"); fx.IsMandatory = true; fx.Label = "Is Fixed Date";
  fx.Description = "True when the holiday falls on the same day and month every year (Holiday_CopyYear copies only these). False for movable holidays such as Good Friday, Easter and Corpus Christi.";

  Action<OutSystems.Model.Logic.Nodes.IActionNode, int, int> at = (n, c, r) => { n.HorizontalPosition = 3200 + c * 1828; n.VerticalPosition = 800 + r * 1371; };

  var cim = eSpace.ServerActions.Single(a => a.Name == "Holiday_CreateIfMissing");
  cim.Description = "Creates an active Holiday for HolidayDate unless one already exists for that date (active or not); Created tells whether it did. IsFixedDate (default True) is stored on create; when it is False and the existing holiday is still marked fixed, the existing one is marked movable. Used by Bootstrap and Holiday_CopyYear.";
  var pFx = cim.CreateInputParameter("IsFixedDate"); pFx.DataType = eSpace.BooleanType; pFx.IsMandatory = false; pFx.SetDefaultValue("True");
  var oCr = cim.CreateOutputParameter("Created"); oCr.DataType = eSpace.BooleanType;
  var cIf = cim.Nodes.OfType<OutSystems.Model.Logic.Nodes.IIfNode>().Single();
  var cAsg = (OutSystems.Model.Logic.Nodes.IAssignNode)cIf.TrueTarget;
  cAsg.CreateAssignment("NewHoliday.IsFixedDate", "IsFixedDate");
  cAsg.CreateAssignment("Created", "True");
  var oldEnd = cIf.FalseTarget;
  var x0 = cIf.HorizontalPosition; var y0 = cIf.VerticalPosition;
  var mIf = cim.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); mIf.HorizontalPosition = x0 + 1828; mIf.VerticalPosition = y0;
  mIf.SetCondition("not IsFixedDate and GetExistingHoliday.List.Current.Holiday.IsFixedDate"); mIf.Label = "Mark movable?";
  cIf.FalseTarget = mIf;
  oldEnd.HorizontalPosition = x0 + 2 * 1828; oldEnd.VerticalPosition = y0;
  mIf.FalseTarget = oldEnd;
  var mAsg = cim.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); mAsg.HorizontalPosition = x0 + 1828; mAsg.VerticalPosition = y0 + 1371;
  mAsg.CreateAssignment("NewHoliday", "GetExistingHoliday.List.Current.Holiday");
  mAsg.CreateAssignment("NewHoliday.IsFixedDate", "False");
  mIf.TrueTarget = mAsg;
  var mUpd = cim.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateHoliday"); mUpd.HorizontalPosition = x0 + 1828; mUpd.VerticalPosition = y0 + 2 * 1371;
  mUpd.Action = hol.UpdateAction; mUpd.SetArgumentValue(hol.UpdateAction.InputParameters.Single(p => p.Name == "Source"), "NewHoliday");
  mAsg.Target = mUpd;
  var mEnd = cim.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); mEnd.HorizontalPosition = x0 + 1828; mEnd.VerticalPosition = y0 + 3 * 1371;
  mUpd.Target = mEnd;

  var boot = eSpace.ServerActions.Single(a => a.Name == "Bootstrap");
  var pDate = cim.InputParameters.Single(p => p.Name == "HolidayDate");
  var movable = new[] { "NewDate(2026, 4, 3)", "NewDate(2026, 4, 5)", "NewDate(2026, 6, 4)" };
  var marked = 0;
  foreach (var n in boot.Nodes.OfType<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>().Where(e => e.Action == cim)) {
    var arg = n.Arguments.FirstOrDefault(a => a.Parameter == pDate);
    if (arg == null) continue;
    var v = arg.Value.ToString().Split(new[] { " [" }, StringSplitOptions.None)[0];
    if (movable.Contains(v)) { n.SetArgumentValue(pFx, "False"); marked++; }
  }
  if (marked != 3) throw new Exception("expected 3 movable Bootstrap nodes, found " + marked);

  Func<OutSystems.Model.Logic.IServerAction, OutSystems.Model.Logic.Nodes.IIfNode> gate = a => {
    var st = a.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>(); at(st, 0, 0);
    var ifn = a.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(ifn, 0, 1);
    ifn.SetCondition("CheckSheetAdminRole()"); ifn.Label = "Is SheetAdmin?";
    st.Target = ifn;
    var rx = a.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>(); at(rx, 1, 1);
    rx.Exception = roleEx; rx.SetExceptionMessage("\"Only administrators can manage holidays.\"");
    ifn.FalseTarget = rx;
    return ifn;
  };
  Func<OutSystems.Model.Logic.IServerAction, string, string, string, int, OutSystems.Model.Logic.Nodes.IIfNode> check = (a, label, cond, msg, row) => {
    var ifn = a.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>(); at(ifn, 0, row);
    ifn.SetCondition(cond); ifn.Label = label;
    var rx = a.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>(); at(rx, 1, row);
    rx.Exception = hv; rx.SetExceptionMessage(msg);
    ifn.TrueTarget = rx;
    return ifn;
  };
  Func<OutSystems.Model.Logic.IServerAction, int, OutSystems.Model.Logic.Nodes.IEndNode> end = (a, row) => { var e = a.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>(); at(e, 0, row); return e; };

  var gl = eSpace.CreateServerAction("Holiday_GetList"); gl.Public = true;
  gl.Description = "Holidays of Year sorted by date (SheetAdmin only). IncludeInactive also returns deactivated holidays.";
  { var p = gl.CreateInputParameter("Year"); p.DataType = eSpace.IntegerType; }
  { var p = gl.CreateInputParameter("IncludeInactive"); p.DataType = eSpace.BooleanType; p.IsMandatory = false; }
  { var o = gl.CreateOutputParameter("Holidays"); o.DataType = eSpace.GetOrCreateListType(hol); }
  var g1 = gate(gl);
  var agL = gl.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetYearHolidays"); at(agL, 0, 2);
  agL.AsDatabaseAggregate.CreateSource(hol);
  agL.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= NewDate(Year, 1, 1) and Holiday.HolidayDate <= NewDate(Year, 12, 31)");
  agL.AsDatabaseAggregate.CreateFilter("IncludeInactive or Holiday.IsActive");
  agL.AsDatabaseAggregate.CreateSort().SetAttribute("Holiday.HolidayDate");
  agL.Name = "GetYearHolidays";
  g1.TrueTarget = agL;
  var glA = gl.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(glA, 0, 3); glA.CreateAssignment("Holidays", "GetYearHolidays.List");
  agL.Target = glA; glA.Target = end(gl, 4);

  var cu = eSpace.CreateServerAction("Holiday_CreateOrUpdate"); cu.Public = true;
  cu.Description = "Creates or updates a holiday (SheetAdmin only). Name is trimmed and required, the date is required and unique (UQ_Holiday_HolidayDate, inactive holidays included). Raises HolidayValidation with a user-facing message otherwise. Returns the holiday's Id.";
  { var p = cu.CreateInputParameter("HolidayRecord"); p.DataType = hol; }
  { var o = cu.CreateOutputParameter("HolidayId"); o.DataType = hol.IdentifierType; }
  var g2 = gate(cu);
  var c1 = check(cu, "Name empty?", "Trim(HolidayRecord.Name) = \"\"", "\"Enter the holiday name.\"", 2); g2.TrueTarget = c1;
  var c2 = check(cu, "No date?", "HolidayRecord.HolidayDate = NullDate()", "\"Enter the holiday date.\"", 3); c1.FalseTarget = c2;
  var agS = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSameDate"); at(agS, 0, 4);
  agS.AsDatabaseAggregate.CreateSource(hol);
  agS.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = HolidayRecord.HolidayDate");
  agS.AsDatabaseAggregate.CreateFilter("Holiday.Id <> HolidayRecord.Id");
  c2.FalseTarget = agS;
  var c3 = check(cu, "Date taken?", "not GetSameDate.List.Empty", "\"There is already a holiday on this date (\" + GetSameDate.List.Current.Holiday.Name + \").\"", 5); agS.Target = c3;
  var cuA = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cuA, 0, 6); cuA.CreateAssignment("HolidayRecord.Name", "Trim(HolidayRecord.Name)"); c3.FalseTarget = cuA;
  var cuS = cu.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateHoliday"); at(cuS, 0, 7);
  cuS.Action = hol.CreateOrUpdateAction; cuS.SetArgumentValue(hol.CreateOrUpdateAction.InputParameters.Single(p => p.Name == "Source"), "HolidayRecord");
  cuA.Target = cuS;
  var cuA2 = cu.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cuA2, 0, 8); cuA2.CreateAssignment("HolidayId", "CreateOrUpdateHoliday.Id");
  cuS.Target = cuA2; cuA2.Target = end(cu, 9);

  var sa = eSpace.CreateServerAction("Holiday_SetActive"); sa.Public = true;
  sa.Description = "Activates or deactivates a holiday (SheetAdmin only); holidays are deactivated instead of deleted. Inactive holidays are ignored by the calendar (Calendar_GetMonth filters on IsActive).";
  { var p = sa.CreateInputParameter("HolidayId"); p.DataType = hol.IdentifierType; }
  { var p = sa.CreateInputParameter("IsActive"); p.DataType = eSpace.BooleanType; }
  { var l = sa.CreateLocalVariable("HolidayRecord"); l.DataType = hol; }
  var g3 = gate(sa);
  var agH = sa.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetHoliday"); at(agH, 0, 2);
  agH.AsDatabaseAggregate.CreateSource(hol); agH.AsDatabaseAggregate.CreateFilter("Holiday.Id = HolidayId");
  g3.TrueTarget = agH;
  var nf = check(sa, "Not found?", "GetHoliday.List.Empty", "\"Holiday not found.\"", 3); agH.Target = nf;
  var saA = sa.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(saA, 0, 4);
  saA.CreateAssignment("HolidayRecord", "GetHoliday.List.Current.Holiday"); saA.CreateAssignment("HolidayRecord.IsActive", "IsActive");
  nf.FalseTarget = saA;
  var saU = sa.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateHoliday"); at(saU, 0, 5);
  saU.Action = hol.UpdateAction; saU.SetArgumentValue(hol.UpdateAction.InputParameters.Single(p => p.Name == "Source"), "HolidayRecord");
  saA.Target = saU; saU.Target = end(sa, 6);

  var cy = eSpace.CreateServerAction("Holiday_CopyYear"); cy.Public = true;
  cy.Description = "Copies the active fixed-date holidays (IsFixedDate) of FromYear to the same day and month of ToYear (SheetAdmin only), skipping dates that already have a holiday and 29 February. Returns CopiedCount; running it again copies nothing.";
  { var p = cy.CreateInputParameter("FromYear"); p.DataType = eSpace.IntegerType; }
  { var p = cy.CreateInputParameter("ToYear"); p.DataType = eSpace.IntegerType; }
  { var o = cy.CreateOutputParameter("CopiedCount"); o.DataType = eSpace.IntegerType; }
  var g4 = gate(cy);
  var sy = check(cy, "Same year?", "FromYear = ToYear", "\"Choose a different year to copy to.\"", 2); g4.TrueTarget = sy;
  var agF = cy.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetFixedHolidays"); at(agF, 0, 3);
  agF.AsDatabaseAggregate.CreateSource(hol);
  agF.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= NewDate(FromYear, 1, 1) and Holiday.HolidayDate <= NewDate(FromYear, 12, 31)");
  agF.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
  agF.AsDatabaseAggregate.CreateFilter("Holiday.IsFixedDate");
  agF.AsDatabaseAggregate.CreateFilter("Month(Holiday.HolidayDate) <> 2 or Day(Holiday.HolidayDate) <> 29");
  agF.AsDatabaseAggregate.CreateSort().SetAttribute("Holiday.HolidayDate");
  agF.Name = "GetFixedHolidays";
  sy.FalseTarget = agF;
  var fe = cy.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>(); at(fe, 0, 4); fe.SetRecordList("GetFixedHolidays.List");
  agF.Target = fe;
  var cr = cy.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing"); at(cr, 1, 4);
  cr.Action = cim;
  cr.SetArgumentValue(pDate, "NewDate(ToYear, Month(GetFixedHolidays.List.Current.Holiday.HolidayDate), Day(GetFixedHolidays.List.Current.Holiday.HolidayDate))");
  cr.SetArgumentValue(cim.InputParameters.Single(p => p.Name == "Name"), "GetFixedHolidays.List.Current.Holiday.Name");
  cr.SetArgumentValue(pFx, "True");
  fe.CycleTarget = cr;
  var cnt = cy.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>(); at(cnt, 1, 5);
  cnt.CreateAssignment("CopiedCount", "CopiedCount + If(Holiday_CreateIfMissing.Created, 1, 0)");
  cr.Target = cnt; cnt.Target = fe;
  fe.Target = end(cy, 6);
}
```
