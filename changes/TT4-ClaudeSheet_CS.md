# TT4 · ClaudeSheet_CS changes

Every `applyModelApiCode` save for TT4 in ClaudeSheet_CS, oldest first. Logged when the module was published.

## Save 1 · 2026-10-15 17:50:23 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    var dailyExpectedMinutes = eSpace.CreateSiteProperty(false, "DailyExpectedMinutes");
    dailyExpectedMinutes.DataType = eSpace.IntegerType;
    dailyExpectedMinutes.SetDefaultValue("480");
    dailyExpectedMinutes.Description = "Minutes a user is expected to record per working day.";
    var seedTestUsernames = eSpace.CreateSiteProperty(false, "SeedTestUsernames");
    seedTestUsernames.DataType = eSpace.TextType;
    seedTestUsernames.SetDefaultValue("\"\"");
    seedTestUsernames.Description = "Comma-separated usernames that the Bootstrap timer grants SheetUser and assigns to the seed project. Leave empty outside test environments.";
    var sheetUser = eSpace.CreateRole("SheetUser");
    sheetUser.Description = "Users who record their time in MySheet.";
    sheetUser.Public = true;
    var daySheetValidation = eSpace.CreateUserException("DaySheetValidation");
}
```

## Save 2 · 2026-10-15 17:50:49 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    /* DayStatus */
    var E_DayStatus = eSpace.CreateStaticEntity("DayStatus");
    E_DayStatus.Description = "Status of a user's day sheet.";
    E_DayStatus.Label = "Day Status";
    E_DayStatus.LabelPlural = "Day Statuses";
    E_DayStatus.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.No;
    E_DayStatus.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Optimized;
    E_DayStatus.UseTranslations = true;
    E_DayStatus.Public = true;
    E_DayStatus.ExposeReadOnly = true;
    /* CalendarDayType */
    var E_CalendarDayType = eSpace.CreateStaticEntity("CalendarDayType");
    E_CalendarDayType.Description = "How a calendar day is classified for the current user (first match wins: Holiday, DayOff, Confirmed, Weekend, Future, Pending).";
    E_CalendarDayType.Label = "Calendar Day Type";
    E_CalendarDayType.LabelPlural = "Calendar Day Types";
    E_CalendarDayType.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.No;
    E_CalendarDayType.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Optimized;
    E_CalendarDayType.UseTranslations = true;
    E_CalendarDayType.Public = true;
    E_CalendarDayType.ExposeReadOnly = true;
    var A_DayStatus_Id = E_DayStatus.CreateAttribute("Id");
    A_DayStatus_Id.DataType = eSpace.IntegerType;
    A_DayStatus_Id.IsMandatory = true;
    A_DayStatus_Id.Label = "Id";
    var A_DayStatus_Label = E_DayStatus.CreateAttribute("Label");
    A_DayStatus_Label.DataType = eSpace.TextType;
    A_DayStatus_Label.Length = 50;
    A_DayStatus_Label.IsMandatory = true;
    A_DayStatus_Label.Label = "Label";
    var A_DayStatus_Order = E_DayStatus.CreateAttribute("Order");
    A_DayStatus_Order.DataType = eSpace.IntegerType;
    A_DayStatus_Order.IsMandatory = true;
    A_DayStatus_Order.Label = "Order";
    var A_DayStatus_Is_Active = E_DayStatus.CreateAttribute("Is_Active");
    A_DayStatus_Is_Active.DataType = eSpace.BooleanType;
    A_DayStatus_Is_Active.IsMandatory = true;
    A_DayStatus_Is_Active.Label = "Is Active";
    E_DayStatus.IdentifierAttribute = A_DayStatus_Id;
    E_DayStatus.LabelAttribute = A_DayStatus_Label;
    E_DayStatus.IsActiveAttribute = A_DayStatus_Is_Active;
    E_DayStatus.OrderByAttribute = A_DayStatus_Order;
    var R_DayStatus_Confirmed = E_DayStatus.CreateRecord();
    R_DayStatus_Confirmed.Identifier = "Confirmed";
    R_DayStatus_Confirmed.SetAttributeValue(A_DayStatus_Id, "2");
    R_DayStatus_Confirmed.SetAttributeValue(A_DayStatus_Is_Active, "True");
    R_DayStatus_Confirmed.SetAttributeValue(A_DayStatus_Label, "\"Confirmed\"");
    R_DayStatus_Confirmed.SetAttributeValue(A_DayStatus_Order, "2");
    var R_DayStatus_DayOff = E_DayStatus.CreateRecord();
    R_DayStatus_DayOff.Identifier = "DayOff";
    R_DayStatus_DayOff.SetAttributeValue(A_DayStatus_Id, "3");
    R_DayStatus_DayOff.SetAttributeValue(A_DayStatus_Is_Active, "True");
    R_DayStatus_DayOff.SetAttributeValue(A_DayStatus_Label, "\"Day off\"");
    R_DayStatus_DayOff.SetAttributeValue(A_DayStatus_Order, "3");
    var R_DayStatus_Draft = E_DayStatus.CreateRecord();
    R_DayStatus_Draft.Identifier = "Draft";
    R_DayStatus_Draft.SetAttributeValue(A_DayStatus_Id, "1");
    R_DayStatus_Draft.SetAttributeValue(A_DayStatus_Is_Active, "True");
    R_DayStatus_Draft.SetAttributeValue(A_DayStatus_Label, "\"Draft\"");
    R_DayStatus_Draft.SetAttributeValue(A_DayStatus_Order, "1");
    var A_CalendarDayType_Id = E_CalendarDayType.CreateAttribute("Id");
    A_CalendarDayType_Id.DataType = eSpace.IntegerType;
    A_CalendarDayType_Id.IsMandatory = true;
    A_CalendarDayType_Id.Label = "Id";
    var A_CalendarDayType_Label = E_CalendarDayType.CreateAttribute("Label");
    A_CalendarDayType_Label.DataType = eSpace.TextType;
    A_CalendarDayType_Label.Length = 50;
    A_CalendarDayType_Label.IsMandatory = true;
    A_CalendarDayType_Label.Label = "Label";
    var A_CalendarDayType_PluralLabel = E_CalendarDayType.CreateAttribute("PluralLabel");
    A_CalendarDayType_PluralLabel.DataType = eSpace.TextType;
    A_CalendarDayType_PluralLabel.Length = 50;
    A_CalendarDayType_PluralLabel.IsMandatory = true;
    A_CalendarDayType_PluralLabel.Label = "Plural Label";
    var A_CalendarDayType_CssClass = E_CalendarDayType.CreateAttribute("CssClass");
    A_CalendarDayType_CssClass.DataType = eSpace.TextType;
    A_CalendarDayType_CssClass.Length = 50;
    A_CalendarDayType_CssClass.IsMandatory = true;
    A_CalendarDayType_CssClass.Label = "Css Class";
    var A_CalendarDayType_ShowInKPI = E_CalendarDayType.CreateAttribute("ShowInKPI");
    A_CalendarDayType_ShowInKPI.DataType = eSpace.BooleanType;
    A_CalendarDayType_ShowInKPI.IsMandatory = true;
    A_CalendarDayType_ShowInKPI.Label = "Show In KPI";
    var A_CalendarDayType_Order = E_CalendarDayType.CreateAttribute("Order");
    A_CalendarDayType_Order.DataType = eSpace.IntegerType;
    A_CalendarDayType_Order.IsMandatory = true;
    A_CalendarDayType_Order.Label = "Order";
    var A_CalendarDayType_Is_Active = E_CalendarDayType.CreateAttribute("Is_Active");
    A_CalendarDayType_Is_Active.DataType = eSpace.BooleanType;
    A_CalendarDayType_Is_Active.IsMandatory = true;
    A_CalendarDayType_Is_Active.Label = "Is Active";
    E_CalendarDayType.IdentifierAttribute = A_CalendarDayType_Id;
    E_CalendarDayType.LabelAttribute = A_CalendarDayType_Label;
    E_CalendarDayType.IsActiveAttribute = A_CalendarDayType_Is_Active;
    E_CalendarDayType.OrderByAttribute = A_CalendarDayType_Order;
    var R_CalendarDayType_Holiday = E_CalendarDayType.CreateRecord();
    R_CalendarDayType_Holiday.Identifier = "Holiday";
    R_CalendarDayType_Holiday.SetAttributeValue(A_CalendarDayType_CssClass, "\"day-holiday\"");
    R_CalendarDayType_Holiday.SetAttributeValue(A_CalendarDayType_Id, "3");
    R_CalendarDayType_Holiday.SetAttributeValue(A_CalendarDayType_Is_Active, "True");
    R_CalendarDayType_Holiday.SetAttributeValue(A_CalendarDayType_Label, "\"Holiday\"");
    R_CalendarDayType_Holiday.SetAttributeValue(A_CalendarDayType_Order, "3");
    R_CalendarDayType_Holiday.SetAttributeValue(A_CalendarDayType_PluralLabel, "\"Holidays\"");
    R_CalendarDayType_Holiday.SetAttributeValue(A_CalendarDayType_ShowInKPI, "True");
    var R_CalendarDayType_Confirmed = E_CalendarDayType.CreateRecord();
    R_CalendarDayType_Confirmed.Identifier = "Confirmed";
    R_CalendarDayType_Confirmed.SetAttributeValue(A_CalendarDayType_CssClass, "\"day-confirmed\"");
    R_CalendarDayType_Confirmed.SetAttributeValue(A_CalendarDayType_Id, "1");
    R_CalendarDayType_Confirmed.SetAttributeValue(A_CalendarDayType_Is_Active, "True");
    R_CalendarDayType_Confirmed.SetAttributeValue(A_CalendarDayType_Label, "\"Confirmed\"");
    R_CalendarDayType_Confirmed.SetAttributeValue(A_CalendarDayType_Order, "1");
    R_CalendarDayType_Confirmed.SetAttributeValue(A_CalendarDayType_PluralLabel, "\"Confirmed\"");
    R_CalendarDayType_Confirmed.SetAttributeValue(A_CalendarDayType_ShowInKPI, "True");
    var R_CalendarDayType_Pending = E_CalendarDayType.CreateRecord();
    R_CalendarDayType_Pending.Identifier = "Pending";
    R_CalendarDayType_Pending.SetAttributeValue(A_CalendarDayType_CssClass, "\"day-pending\"");
    R_CalendarDayType_Pending.SetAttributeValue(A_CalendarDayType_Id, "4");
    R_CalendarDayType_Pending.SetAttributeValue(A_CalendarDayType_Is_Active, "True");
    R_CalendarDayType_Pending.SetAttributeValue(A_CalendarDayType_Label, "\"Pending\"");
    R_CalendarDayType_Pending.SetAttributeValue(A_CalendarDayType_Order, "4");
    R_CalendarDayType_Pending.SetAttributeValue(A_CalendarDayType_PluralLabel, "\"Pending\"");
    R_CalendarDayType_Pending.SetAttributeValue(A_CalendarDayType_ShowInKPI, "True");
    var R_CalendarDayType_DayOff = E_CalendarDayType.CreateRecord();
    R_CalendarDayType_DayOff.Identifier = "DayOff";
    R_CalendarDayType_DayOff.SetAttributeValue(A_CalendarDayType_CssClass, "\"day-off\"");
    R_CalendarDayType_DayOff.SetAttributeValue(A_CalendarDayType_Id, "2");
    R_CalendarDayType_DayOff.SetAttributeValue(A_CalendarDayType_Is_Active, "True");
    R_CalendarDayType_DayOff.SetAttributeValue(A_CalendarDayType_Label, "\"Day off\"");
    R_CalendarDayType_DayOff.SetAttributeValue(A_CalendarDayType_Order, "2");
    R_CalendarDayType_DayOff.SetAttributeValue(A_CalendarDayType_PluralLabel, "\"Days off\"");
    R_CalendarDayType_DayOff.SetAttributeValue(A_CalendarDayType_ShowInKPI, "True");
    var R_CalendarDayType_Weekend = E_CalendarDayType.CreateRecord();
    R_CalendarDayType_Weekend.Identifier = "Weekend";
    R_CalendarDayType_Weekend.SetAttributeValue(A_CalendarDayType_CssClass, "\"day-weekend\"");
    R_CalendarDayType_Weekend.SetAttributeValue(A_CalendarDayType_Id, "6");
    R_CalendarDayType_Weekend.SetAttributeValue(A_CalendarDayType_Is_Active, "True");
    R_CalendarDayType_Weekend.SetAttributeValue(A_CalendarDayType_Label, "\"Weekend\"");
    R_CalendarDayType_Weekend.SetAttributeValue(A_CalendarDayType_Order, "6");
    R_CalendarDayType_Weekend.SetAttributeValue(A_CalendarDayType_PluralLabel, "\"Weekend days\"");
    R_CalendarDayType_Weekend.SetAttributeValue(A_CalendarDayType_ShowInKPI, "False");
    var R_CalendarDayType_Future = E_CalendarDayType.CreateRecord();
    R_CalendarDayType_Future.Identifier = "Future";
    R_CalendarDayType_Future.SetAttributeValue(A_CalendarDayType_CssClass, "\"day-future\"");
    R_CalendarDayType_Future.SetAttributeValue(A_CalendarDayType_Id, "5");
    R_CalendarDayType_Future.SetAttributeValue(A_CalendarDayType_Is_Active, "True");
    R_CalendarDayType_Future.SetAttributeValue(A_CalendarDayType_Label, "\"Future\"");
    R_CalendarDayType_Future.SetAttributeValue(A_CalendarDayType_Order, "5");
    R_CalendarDayType_Future.SetAttributeValue(A_CalendarDayType_PluralLabel, "\"Future days\"");
    R_CalendarDayType_Future.SetAttributeValue(A_CalendarDayType_ShowInKPI, "False");
}
```

## Save 3 · 2026-10-15 17:51:27 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    var user = (OutSystems.Model.Data.IServerEntitySignature)eSpace.References.Named("(System)").Entities.Named("User");
    var E_DayStatus = eSpace.Entities.OfType<OutSystems.Model.Data.IStaticEntity>().Named("DayStatus");
    var E_CalendarDayType = eSpace.Entities.OfType<OutSystems.Model.Data.IStaticEntity>().Named("CalendarDayType");
    /* Client */
    var E_Client = eSpace.CreateServerEntity("Client");
    E_Client.Description = "Customer that projects are billed to.";
    E_Client.Label = "Client";
    E_Client.LabelPlural = "Clients";
    E_Client.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited;
    E_Client.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full;
    E_Client.UseTranslations = false;
    E_Client.Public = true;
    E_Client.ExposeReadOnly = true;
    /* Project */
    var E_Project = eSpace.CreateServerEntity("Project");
    E_Project.Description = "Project a user can record time against.";
    E_Project.Label = "Project";
    E_Project.LabelPlural = "Projects";
    E_Project.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited;
    E_Project.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full;
    E_Project.UseTranslations = false;
    E_Project.Public = true;
    E_Project.ExposeReadOnly = true;
    /* ProjectAssignment */
    var E_ProjectAssignment = eSpace.CreateServerEntity("ProjectAssignment");
    E_ProjectAssignment.Description = "A user's assignment to a project for a date range. A user can have several periods on the same project (one per StartDate); periods are not expected to overlap.";
    E_ProjectAssignment.Label = "Project Assignment";
    E_ProjectAssignment.LabelPlural = "Project Assignments";
    E_ProjectAssignment.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited;
    E_ProjectAssignment.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full;
    E_ProjectAssignment.UseTranslations = false;
    E_ProjectAssignment.Public = true;
    E_ProjectAssignment.ExposeReadOnly = true;
    /* Holiday */
    var E_Holiday = eSpace.CreateServerEntity("Holiday");
    E_Holiday.Description = "Public holiday. Holidays are not editable in the calendar.";
    E_Holiday.Label = "Holiday";
    E_Holiday.LabelPlural = "Holidays";
    E_Holiday.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited;
    E_Holiday.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full;
    E_Holiday.UseTranslations = false;
    E_Holiday.Public = true;
    E_Holiday.ExposeReadOnly = true;
    /* DaySheet */
    var E_DaySheet = eSpace.CreateServerEntity("DaySheet");
    E_DaySheet.Description = "One user's time sheet for one day.";
    E_DaySheet.Label = "Day Sheet";
    E_DaySheet.LabelPlural = "Day Sheets";
    E_DaySheet.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited;
    E_DaySheet.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full;
    E_DaySheet.UseTranslations = false;
    E_DaySheet.Public = true;
    E_DaySheet.ExposeReadOnly = true;
    /* TimeEntry */
    var E_TimeEntry = eSpace.CreateServerEntity("TimeEntry");
    E_TimeEntry.Description = "Minutes recorded on one project within a day sheet.";
    E_TimeEntry.Label = "Time Entry";
    E_TimeEntry.LabelPlural = "Time Entries";
    E_TimeEntry.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited;
    E_TimeEntry.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full;
    E_TimeEntry.UseTranslations = false;
    E_TimeEntry.Public = true;
    E_TimeEntry.ExposeReadOnly = true;
    var A_Client_Id = E_Client.CreateAttribute("Id");
    A_Client_Id.DataType = eSpace.LongIntegerType;
    A_Client_Id.IsMandatory = true;
    A_Client_Id.Label = "Id";
    A_Client_Id.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    var A_Client_Name = E_Client.CreateAttribute("Name");
    A_Client_Name.DataType = eSpace.TextType;
    A_Client_Name.Length = 100;
    A_Client_Name.IsMandatory = true;
    A_Client_Name.Label = "Name";
    var A_Client_IsActive = E_Client.CreateAttribute("IsActive");
    A_Client_IsActive.DataType = eSpace.BooleanType;
    A_Client_IsActive.IsMandatory = true;
    A_Client_IsActive.SetDefaultValue("True");
    A_Client_IsActive.Label = "Is Active";
    E_Client.IdentifierAttribute = A_Client_Id;
    E_Client.LabelAttribute = A_Client_Name;
    var I_UQ_Client_Name = E_Client.CreateIndex("UQ_Client_Name");
    I_UQ_Client_Name.Unique = true;
    I_UQ_Client_Name.AddAttribute(A_Client_Name);
    var A_Project_Id = E_Project.CreateAttribute("Id");
    A_Project_Id.DataType = eSpace.LongIntegerType;
    A_Project_Id.IsMandatory = true;
    A_Project_Id.Label = "Id";
    A_Project_Id.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    var A_Project_ClientId = E_Project.CreateAttribute("ClientId");
    A_Project_ClientId.DataType = E_Client.IdentifierType;
    A_Project_ClientId.IsMandatory = true;
    A_Project_ClientId.Label = "Client";
    A_Project_ClientId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect;
    var A_Project_Name = E_Project.CreateAttribute("Name");
    A_Project_Name.DataType = eSpace.TextType;
    A_Project_Name.Length = 100;
    A_Project_Name.IsMandatory = true;
    A_Project_Name.Label = "Name";
    var A_Project_Code = E_Project.CreateAttribute("Code");
    A_Project_Code.DataType = eSpace.TextType;
    A_Project_Code.Length = 30;
    A_Project_Code.IsMandatory = true;
    A_Project_Code.Label = "Code";
    var A_Project_IsActive = E_Project.CreateAttribute("IsActive");
    A_Project_IsActive.DataType = eSpace.BooleanType;
    A_Project_IsActive.IsMandatory = true;
    A_Project_IsActive.SetDefaultValue("True");
    A_Project_IsActive.Label = "Is Active";
    E_Project.IdentifierAttribute = A_Project_Id;
    E_Project.LabelAttribute = A_Project_Name;
    var I_UQ_Project_ClientCode = E_Project.CreateIndex("UQ_Project_ClientCode");
    I_UQ_Project_ClientCode.Unique = true;
    I_UQ_Project_ClientCode.AddAttribute(A_Project_ClientId);
    I_UQ_Project_ClientCode.AddAttribute(A_Project_Code);
    var A_ProjectAssignment_Id = E_ProjectAssignment.CreateAttribute("Id");
    A_ProjectAssignment_Id.DataType = eSpace.LongIntegerType;
    A_ProjectAssignment_Id.IsMandatory = true;
    A_ProjectAssignment_Id.Label = "Id";
    A_ProjectAssignment_Id.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    var A_ProjectAssignment_UserId = E_ProjectAssignment.CreateAttribute("UserId");
    A_ProjectAssignment_UserId.DataType = user.IdentifierType;
    A_ProjectAssignment_UserId.IsMandatory = true;
    A_ProjectAssignment_UserId.Label = "User";
    A_ProjectAssignment_UserId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    var A_ProjectAssignment_ProjectId = E_ProjectAssignment.CreateAttribute("ProjectId");
    A_ProjectAssignment_ProjectId.DataType = E_Project.IdentifierType;
    A_ProjectAssignment_ProjectId.IsMandatory = true;
    A_ProjectAssignment_ProjectId.Label = "Project";
    A_ProjectAssignment_ProjectId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    var A_ProjectAssignment_StartDate = E_ProjectAssignment.CreateAttribute("StartDate");
    A_ProjectAssignment_StartDate.DataType = eSpace.DateType;
    A_ProjectAssignment_StartDate.IsMandatory = true;
    A_ProjectAssignment_StartDate.Label = "Start Date";
    var A_ProjectAssignment_EndDate = E_ProjectAssignment.CreateAttribute("EndDate");
    A_ProjectAssignment_EndDate.DataType = eSpace.DateType;
    A_ProjectAssignment_EndDate.IsMandatory = false;
    A_ProjectAssignment_EndDate.Label = "End Date";
    var A_ProjectAssignment_IsActive = E_ProjectAssignment.CreateAttribute("IsActive");
    A_ProjectAssignment_IsActive.DataType = eSpace.BooleanType;
    A_ProjectAssignment_IsActive.IsMandatory = true;
    A_ProjectAssignment_IsActive.SetDefaultValue("True");
    A_ProjectAssignment_IsActive.Label = "Is Active";
    E_ProjectAssignment.IdentifierAttribute = A_ProjectAssignment_Id;
    var I_UQ_PA_UserProjectStart = E_ProjectAssignment.CreateIndex("UQ_PA_UserProjectStart");
    I_UQ_PA_UserProjectStart.Unique = true;
    I_UQ_PA_UserProjectStart.AddAttribute(A_ProjectAssignment_UserId);
    I_UQ_PA_UserProjectStart.AddAttribute(A_ProjectAssignment_ProjectId);
    I_UQ_PA_UserProjectStart.AddAttribute(A_ProjectAssignment_StartDate);
    var A_Holiday_Id = E_Holiday.CreateAttribute("Id");
    A_Holiday_Id.DataType = eSpace.LongIntegerType;
    A_Holiday_Id.IsMandatory = true;
    A_Holiday_Id.Label = "Id";
    A_Holiday_Id.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    var A_Holiday_HolidayDate = E_Holiday.CreateAttribute("HolidayDate");
    A_Holiday_HolidayDate.DataType = eSpace.DateType;
    A_Holiday_HolidayDate.IsMandatory = true;
    A_Holiday_HolidayDate.Label = "Holiday Date";
    var A_Holiday_Name = E_Holiday.CreateAttribute("Name");
    A_Holiday_Name.DataType = eSpace.TextType;
    A_Holiday_Name.Length = 100;
    A_Holiday_Name.IsMandatory = true;
    A_Holiday_Name.Label = "Name";
    var A_Holiday_IsActive = E_Holiday.CreateAttribute("IsActive");
    A_Holiday_IsActive.DataType = eSpace.BooleanType;
    A_Holiday_IsActive.IsMandatory = true;
    A_Holiday_IsActive.SetDefaultValue("True");
    A_Holiday_IsActive.Label = "Is Active";
    E_Holiday.IdentifierAttribute = A_Holiday_Id;
    E_Holiday.LabelAttribute = A_Holiday_Name;
    var I_UQ_Holiday_HolidayDate = E_Holiday.CreateIndex("UQ_Holiday_HolidayDate");
    I_UQ_Holiday_HolidayDate.Unique = true;
    I_UQ_Holiday_HolidayDate.AddAttribute(A_Holiday_HolidayDate);
    var A_DaySheet_Id = E_DaySheet.CreateAttribute("Id");
    A_DaySheet_Id.DataType = eSpace.LongIntegerType;
    A_DaySheet_Id.IsMandatory = true;
    A_DaySheet_Id.Label = "Id";
    A_DaySheet_Id.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    var A_DaySheet_UserId = E_DaySheet.CreateAttribute("UserId");
    A_DaySheet_UserId.DataType = user.IdentifierType;
    A_DaySheet_UserId.IsMandatory = true;
    A_DaySheet_UserId.Label = "User";
    A_DaySheet_UserId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    var A_DaySheet_SheetDate = E_DaySheet.CreateAttribute("SheetDate");
    A_DaySheet_SheetDate.DataType = eSpace.DateType;
    A_DaySheet_SheetDate.IsMandatory = true;
    A_DaySheet_SheetDate.Label = "Sheet Date";
    var A_DaySheet_DayStatusId = E_DaySheet.CreateAttribute("DayStatusId");
    A_DaySheet_DayStatusId.DataType = E_DayStatus.IdentifierType;
    A_DaySheet_DayStatusId.IsMandatory = true;
    A_DaySheet_DayStatusId.Label = "Day Status";
    A_DaySheet_DayStatusId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect;
    var A_DaySheet_Notes = E_DaySheet.CreateAttribute("Notes");
    A_DaySheet_Notes.DataType = eSpace.TextType;
    A_DaySheet_Notes.Length = 500;
    A_DaySheet_Notes.IsMandatory = false;
    A_DaySheet_Notes.Label = "Notes";
    var A_DaySheet_CreatedOn = E_DaySheet.CreateAttribute("CreatedOn");
    A_DaySheet_CreatedOn.DataType = eSpace.DateTimeType;
    A_DaySheet_CreatedOn.IsMandatory = true;
    A_DaySheet_CreatedOn.Label = "Created On";
    var A_DaySheet_UpdatedOn = E_DaySheet.CreateAttribute("UpdatedOn");
    A_DaySheet_UpdatedOn.DataType = eSpace.DateTimeType;
    A_DaySheet_UpdatedOn.IsMandatory = false;
    A_DaySheet_UpdatedOn.Label = "Updated On";
    E_DaySheet.IdentifierAttribute = A_DaySheet_Id;
    var I_UQ_DaySheet_UserDate = E_DaySheet.CreateIndex("UQ_DaySheet_UserDate");
    I_UQ_DaySheet_UserDate.Unique = true;
    I_UQ_DaySheet_UserDate.AddAttribute(A_DaySheet_UserId);
    I_UQ_DaySheet_UserDate.AddAttribute(A_DaySheet_SheetDate);
    var A_TimeEntry_Id = E_TimeEntry.CreateAttribute("Id");
    A_TimeEntry_Id.DataType = eSpace.LongIntegerType;
    A_TimeEntry_Id.IsMandatory = true;
    A_TimeEntry_Id.Label = "Id";
    A_TimeEntry_Id.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    var A_TimeEntry_DaySheetId = E_TimeEntry.CreateAttribute("DaySheetId");
    A_TimeEntry_DaySheetId.DataType = E_DaySheet.IdentifierType;
    A_TimeEntry_DaySheetId.IsMandatory = true;
    A_TimeEntry_DaySheetId.Label = "Day Sheet";
    A_TimeEntry_DaySheetId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    var A_TimeEntry_ProjectId = E_TimeEntry.CreateAttribute("ProjectId");
    A_TimeEntry_ProjectId.DataType = E_Project.IdentifierType;
    A_TimeEntry_ProjectId.IsMandatory = true;
    A_TimeEntry_ProjectId.Label = "Project";
    A_TimeEntry_ProjectId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect;
    var A_TimeEntry_Minutes = E_TimeEntry.CreateAttribute("Minutes");
    A_TimeEntry_Minutes.DataType = eSpace.IntegerType;
    A_TimeEntry_Minutes.IsMandatory = true;
    A_TimeEntry_Minutes.Label = "Minutes";
    var A_TimeEntry_Description = E_TimeEntry.CreateAttribute("Description");
    A_TimeEntry_Description.DataType = eSpace.TextType;
    A_TimeEntry_Description.Length = 250;
    A_TimeEntry_Description.IsMandatory = false;
    A_TimeEntry_Description.Label = "Description";
    var A_TimeEntry_CreatedOn = E_TimeEntry.CreateAttribute("CreatedOn");
    A_TimeEntry_CreatedOn.DataType = eSpace.DateTimeType;
    A_TimeEntry_CreatedOn.IsMandatory = true;
    A_TimeEntry_CreatedOn.Label = "Created On";
    var A_TimeEntry_UpdatedOn = E_TimeEntry.CreateAttribute("UpdatedOn");
    A_TimeEntry_UpdatedOn.DataType = eSpace.DateTimeType;
    A_TimeEntry_UpdatedOn.IsMandatory = false;
    A_TimeEntry_UpdatedOn.Label = "Updated On";
    E_TimeEntry.IdentifierAttribute = A_TimeEntry_Id;
    var I_UQ_TimeEntry_SheetProject = E_TimeEntry.CreateIndex("UQ_TimeEntry_SheetProject");
    I_UQ_TimeEntry_SheetProject.Unique = true;
    I_UQ_TimeEntry_SheetProject.AddAttribute(A_TimeEntry_DaySheetId);
    I_UQ_TimeEntry_SheetProject.AddAttribute(A_TimeEntry_ProjectId);
}
```

## Save 4 · 2026-10-15 17:52:20 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
    var E_CalendarDayType = eSpace.Entities.Named("CalendarDayType");
    var E_DaySheet = eSpace.Entities.Named("DaySheet");
    var E_DayStatus = eSpace.Entities.Named("DayStatus");
    var E_Project = eSpace.Entities.Named("Project");
    var E_TimeEntry = eSpace.Entities.Named("TimeEntry");
    var S_CalendarDay = eSpace.CreateStructure("CalendarDay");
    S_CalendarDay.Description = "One day of the month calendar for the current user.";
    S_CalendarDay.Public = true;
    var S_MonthSummary = eSpace.CreateStructure("MonthSummary");
    S_MonthSummary.Description = "Totals and KPI counts for one month of the current user.";
    S_MonthSummary.Public = true;
    var S_TimeEntryInput = eSpace.CreateStructure("TimeEntryInput");
    S_TimeEntryInput.Description = "Minutes and description entered for one project on one day.";
    S_TimeEntryInput.Public = true;
    var S_DaySheetProjectLine = eSpace.CreateStructure("DaySheetProjectLine");
    S_DaySheetProjectLine.Description = "One project row in the day sheet editor.";
    S_DaySheetProjectLine.Public = true;
    var S_DaySheetDetail = eSpace.CreateStructure("DaySheetDetail");
    S_DaySheetDetail.Description = "A day sheet with its project lines, as shown in the day editor.";
    S_DaySheetDetail.Public = true;
    var S_WeekDayHeader = eSpace.CreateStructure("WeekDayHeader");
    S_WeekDayHeader.Description = "Column header of the weekly grid.";
    S_WeekDayHeader.Public = true;
    var S_WeeklyCell = eSpace.CreateStructure("WeeklyCell");
    S_WeeklyCell.Description = "Minutes for one project on one day of the weekly grid.";
    S_WeeklyCell.Public = true;
    var S_WeeklyProjectRow = eSpace.CreateStructure("WeeklyProjectRow");
    S_WeeklyProjectRow.Description = "One project row of the weekly grid.";
    S_WeeklyProjectRow.Public = true;
    var S_WeeklyHours = eSpace.CreateStructure("WeeklyHours");
    S_WeeklyHours.Description = "The weekly grid: day headers, project rows and totals.";
    S_WeeklyHours.Public = true;
    var S_NumberOption = eSpace.CreateStructure("NumberOption");
    S_NumberOption.Description = "Value/label pair for number dropdowns (hours 0-12, minutes 00/15/30/45).";
    S_NumberOption.Public = true;
    var S_DayEditorLine = eSpace.CreateStructure("DayEditorLine");
    S_DayEditorLine.Description = "One project line of the HoursCalendar day editor, with the time split into Hours and Mins for the dropdowns.";
    S_DayEditorLine.Public = true;
    var S_DayStatusKPI = eSpace.CreateStructure("DayStatusKPI");
    S_DayStatusKPI.Description = "One KPI card of the DayStatusKPIs block: a CalendarDayType with its count in the selected month and the label already in singular/plural.";
    S_DayStatusKPI.Public = true;
    var A_CalendarDay_Date = S_CalendarDay.CreateAttribute("Date");
    A_CalendarDay_Date.DataType = eSpace.DateType;
    A_CalendarDay_Date.Label = "Date";
    var A_CalendarDay_DayNumber = S_CalendarDay.CreateAttribute("DayNumber");
    A_CalendarDay_DayNumber.DataType = eSpace.IntegerType;
    A_CalendarDay_DayNumber.Label = "Day Number";
    var A_CalendarDay_WeekDay = S_CalendarDay.CreateAttribute("WeekDay");
    A_CalendarDay_WeekDay.DataType = eSpace.IntegerType;
    A_CalendarDay_WeekDay.Label = "Week Day";
    var A_CalendarDay_IsWeekend = S_CalendarDay.CreateAttribute("IsWeekend");
    A_CalendarDay_IsWeekend.DataType = eSpace.BooleanType;
    A_CalendarDay_IsWeekend.Label = "Is Weekend";
    var A_CalendarDay_IsToday = S_CalendarDay.CreateAttribute("IsToday");
    A_CalendarDay_IsToday.DataType = eSpace.BooleanType;
    A_CalendarDay_IsToday.Label = "Is Today";
    var A_CalendarDay_DayTypeId = S_CalendarDay.CreateAttribute("DayTypeId");
    A_CalendarDay_DayTypeId.DataType = E_CalendarDayType.IdentifierType;
    A_CalendarDay_DayTypeId.Label = "Day Type";
    var A_CalendarDay_CssClass = S_CalendarDay.CreateAttribute("CssClass");
    A_CalendarDay_CssClass.DataType = eSpace.TextType;
    A_CalendarDay_CssClass.Length = 50;
    A_CalendarDay_CssClass.Label = "Css Class";
    var A_CalendarDay_DaySheetId = S_CalendarDay.CreateAttribute("DaySheetId");
    A_CalendarDay_DaySheetId.DataType = E_DaySheet.IdentifierType;
    A_CalendarDay_DaySheetId.Label = "Day Sheet";
    var A_CalendarDay_DayStatusId = S_CalendarDay.CreateAttribute("DayStatusId");
    A_CalendarDay_DayStatusId.DataType = E_DayStatus.IdentifierType;
    A_CalendarDay_DayStatusId.Label = "Day Status";
    var A_CalendarDay_HolidayName = S_CalendarDay.CreateAttribute("HolidayName");
    A_CalendarDay_HolidayName.DataType = eSpace.TextType;
    A_CalendarDay_HolidayName.Length = 100;
    A_CalendarDay_HolidayName.Label = "Holiday Name";
    var A_CalendarDay_TotalMinutes = S_CalendarDay.CreateAttribute("TotalMinutes");
    A_CalendarDay_TotalMinutes.DataType = eSpace.IntegerType;
    A_CalendarDay_TotalMinutes.Label = "Total Minutes";
    var A_CalendarDay_IsEditable = S_CalendarDay.CreateAttribute("IsEditable");
    A_CalendarDay_IsEditable.DataType = eSpace.BooleanType;
    A_CalendarDay_IsEditable.Label = "Is Editable";
    var A_MonthSummary_Year = S_MonthSummary.CreateAttribute("Year");
    A_MonthSummary_Year.DataType = eSpace.IntegerType;
    A_MonthSummary_Year.Label = "Year";
    var A_MonthSummary_Month = S_MonthSummary.CreateAttribute("Month");
    A_MonthSummary_Month.DataType = eSpace.IntegerType;
    A_MonthSummary_Month.Label = "Month";
    var A_MonthSummary_TotalMinutes = S_MonthSummary.CreateAttribute("TotalMinutes");
    A_MonthSummary_TotalMinutes.DataType = eSpace.IntegerType;
    A_MonthSummary_TotalMinutes.Label = "Total Minutes";
    var A_MonthSummary_ElapsedDays = S_MonthSummary.CreateAttribute("ElapsedDays");
    A_MonthSummary_ElapsedDays.DataType = eSpace.IntegerType;
    A_MonthSummary_ElapsedDays.Label = "Elapsed Days";
    var A_MonthSummary_ExpectedMinutes = S_MonthSummary.CreateAttribute("ExpectedMinutes");
    A_MonthSummary_ExpectedMinutes.DataType = eSpace.IntegerType;
    A_MonthSummary_ExpectedMinutes.Label = "Expected Minutes";
    var A_MonthSummary_ProgressPercent = S_MonthSummary.CreateAttribute("ProgressPercent");
    A_MonthSummary_ProgressPercent.DataType = eSpace.DecimalType;
    A_MonthSummary_ProgressPercent.Label = "Progress Percent";
    var A_MonthSummary_ConfirmedCount = S_MonthSummary.CreateAttribute("ConfirmedCount");
    A_MonthSummary_ConfirmedCount.DataType = eSpace.IntegerType;
    A_MonthSummary_ConfirmedCount.Label = "Confirmed Count";
    var A_MonthSummary_DayOffCount = S_MonthSummary.CreateAttribute("DayOffCount");
    A_MonthSummary_DayOffCount.DataType = eSpace.IntegerType;
    A_MonthSummary_DayOffCount.Label = "Day Off Count";
    var A_MonthSummary_HolidayCount = S_MonthSummary.CreateAttribute("HolidayCount");
    A_MonthSummary_HolidayCount.DataType = eSpace.IntegerType;
    A_MonthSummary_HolidayCount.Label = "Holiday Count";
    var A_MonthSummary_PendingCount = S_MonthSummary.CreateAttribute("PendingCount");
    A_MonthSummary_PendingCount.DataType = eSpace.IntegerType;
    A_MonthSummary_PendingCount.Label = "Pending Count";
    var A_TimeEntryInput_ProjectId = S_TimeEntryInput.CreateAttribute("ProjectId");
    A_TimeEntryInput_ProjectId.DataType = E_Project.IdentifierType;
    A_TimeEntryInput_ProjectId.Label = "Project";
    var A_TimeEntryInput_Minutes = S_TimeEntryInput.CreateAttribute("Minutes");
    A_TimeEntryInput_Minutes.DataType = eSpace.IntegerType;
    A_TimeEntryInput_Minutes.Label = "Minutes";
    var A_TimeEntryInput_Description = S_TimeEntryInput.CreateAttribute("Description");
    A_TimeEntryInput_Description.DataType = eSpace.TextType;
    A_TimeEntryInput_Description.Length = 250;
    A_TimeEntryInput_Description.Label = "Description";
    var A_DaySheetProjectLine_ProjectId = S_DaySheetProjectLine.CreateAttribute("ProjectId");
    A_DaySheetProjectLine_ProjectId.DataType = E_Project.IdentifierType;
    A_DaySheetProjectLine_ProjectId.Label = "Project";
    var A_DaySheetProjectLine_ClientName = S_DaySheetProjectLine.CreateAttribute("ClientName");
    A_DaySheetProjectLine_ClientName.DataType = eSpace.TextType;
    A_DaySheetProjectLine_ClientName.Length = 100;
    A_DaySheetProjectLine_ClientName.Label = "Client Name";
    var A_DaySheetProjectLine_ProjectName = S_DaySheetProjectLine.CreateAttribute("ProjectName");
    A_DaySheetProjectLine_ProjectName.DataType = eSpace.TextType;
    A_DaySheetProjectLine_ProjectName.Length = 100;
    A_DaySheetProjectLine_ProjectName.Label = "Project Name";
    var A_DaySheetProjectLine_ProjectCode = S_DaySheetProjectLine.CreateAttribute("ProjectCode");
    A_DaySheetProjectLine_ProjectCode.DataType = eSpace.TextType;
    A_DaySheetProjectLine_ProjectCode.Length = 30;
    A_DaySheetProjectLine_ProjectCode.Label = "Project Code";
    var A_DaySheetProjectLine_TimeEntryId = S_DaySheetProjectLine.CreateAttribute("TimeEntryId");
    A_DaySheetProjectLine_TimeEntryId.DataType = E_TimeEntry.IdentifierType;
    A_DaySheetProjectLine_TimeEntryId.Label = "Time Entry";
    var A_DaySheetProjectLine_Minutes = S_DaySheetProjectLine.CreateAttribute("Minutes");
    A_DaySheetProjectLine_Minutes.DataType = eSpace.IntegerType;
    A_DaySheetProjectLine_Minutes.Label = "Minutes";
    var A_DaySheetProjectLine_Description = S_DaySheetProjectLine.CreateAttribute("Description");
    A_DaySheetProjectLine_Description.DataType = eSpace.TextType;
    A_DaySheetProjectLine_Description.Length = 250;
    A_DaySheetProjectLine_Description.Label = "Description";
    var A_DaySheetDetail_DaySheetId = S_DaySheetDetail.CreateAttribute("DaySheetId");
    A_DaySheetDetail_DaySheetId.DataType = E_DaySheet.IdentifierType;
    A_DaySheetDetail_DaySheetId.Label = "Day Sheet";
    var A_DaySheetDetail_SheetDate = S_DaySheetDetail.CreateAttribute("SheetDate");
    A_DaySheetDetail_SheetDate.DataType = eSpace.DateType;
    A_DaySheetDetail_SheetDate.Label = "Sheet Date";
    var A_DaySheetDetail_DayStatusId = S_DaySheetDetail.CreateAttribute("DayStatusId");
    A_DaySheetDetail_DayStatusId.DataType = E_DayStatus.IdentifierType;
    A_DaySheetDetail_DayStatusId.Label = "Day Status";
    var A_DaySheetDetail_DayTypeId = S_DaySheetDetail.CreateAttribute("DayTypeId");
    A_DaySheetDetail_DayTypeId.DataType = E_CalendarDayType.IdentifierType;
    A_DaySheetDetail_DayTypeId.Label = "Day Type";
    var A_DaySheetDetail_Notes = S_DaySheetDetail.CreateAttribute("Notes");
    A_DaySheetDetail_Notes.DataType = eSpace.TextType;
    A_DaySheetDetail_Notes.Length = 500;
    A_DaySheetDetail_Notes.Label = "Notes";
    var A_DaySheetDetail_HolidayName = S_DaySheetDetail.CreateAttribute("HolidayName");
    A_DaySheetDetail_HolidayName.DataType = eSpace.TextType;
    A_DaySheetDetail_HolidayName.Length = 100;
    A_DaySheetDetail_HolidayName.Label = "Holiday Name";
    var A_DaySheetDetail_IsEditable = S_DaySheetDetail.CreateAttribute("IsEditable");
    A_DaySheetDetail_IsEditable.DataType = eSpace.BooleanType;
    A_DaySheetDetail_IsEditable.Label = "Is Editable";
    var A_DaySheetDetail_TotalMinutes = S_DaySheetDetail.CreateAttribute("TotalMinutes");
    A_DaySheetDetail_TotalMinutes.DataType = eSpace.IntegerType;
    A_DaySheetDetail_TotalMinutes.Label = "Total Minutes";
    var A_DaySheetDetail_Lines = S_DaySheetDetail.CreateAttribute("Lines");
    A_DaySheetDetail_Lines.DataType = eSpace.GetOrCreateListType(S_DaySheetProjectLine);
    A_DaySheetDetail_Lines.Label = "Lines";
    var A_WeekDayHeader_Date = S_WeekDayHeader.CreateAttribute("Date");
    A_WeekDayHeader_Date.DataType = eSpace.DateType;
    A_WeekDayHeader_Date.Label = "Date";
    var A_WeekDayHeader_DayName = S_WeekDayHeader.CreateAttribute("DayName");
    A_WeekDayHeader_DayName.DataType = eSpace.TextType;
    A_WeekDayHeader_DayName.Length = 20;
    A_WeekDayHeader_DayName.Label = "Day Name";
    var A_WeekDayHeader_IsWeekend = S_WeekDayHeader.CreateAttribute("IsWeekend");
    A_WeekDayHeader_IsWeekend.DataType = eSpace.BooleanType;
    A_WeekDayHeader_IsWeekend.Label = "Is Weekend";
    var A_WeekDayHeader_IsToday = S_WeekDayHeader.CreateAttribute("IsToday");
    A_WeekDayHeader_IsToday.DataType = eSpace.BooleanType;
    A_WeekDayHeader_IsToday.Label = "Is Today";
    var A_WeekDayHeader_DayTypeId = S_WeekDayHeader.CreateAttribute("DayTypeId");
    A_WeekDayHeader_DayTypeId.DataType = E_CalendarDayType.IdentifierType;
    A_WeekDayHeader_DayTypeId.Label = "Day Type";
    var A_WeekDayHeader_CssClass = S_WeekDayHeader.CreateAttribute("CssClass");
    A_WeekDayHeader_CssClass.DataType = eSpace.TextType;
    A_WeekDayHeader_CssClass.Length = 50;
    A_WeekDayHeader_CssClass.Label = "Css Class";
    var A_WeekDayHeader_IsEditable = S_WeekDayHeader.CreateAttribute("IsEditable");
    A_WeekDayHeader_IsEditable.DataType = eSpace.BooleanType;
    A_WeekDayHeader_IsEditable.Label = "Is Editable";
    var A_WeekDayHeader_TotalMinutes = S_WeekDayHeader.CreateAttribute("TotalMinutes");
    A_WeekDayHeader_TotalMinutes.DataType = eSpace.IntegerType;
    A_WeekDayHeader_TotalMinutes.Label = "Total Minutes";
    var A_WeekDayHeader_DateLabel = S_WeekDayHeader.CreateAttribute("DateLabel");
    A_WeekDayHeader_DateLabel.DataType = eSpace.TextType;
    A_WeekDayHeader_DateLabel.Length = 10;
    A_WeekDayHeader_DateLabel.Label = "Date Label";
    var A_WeeklyCell_Date = S_WeeklyCell.CreateAttribute("Date");
    A_WeeklyCell_Date.DataType = eSpace.DateType;
    A_WeeklyCell_Date.Label = "Date";
    var A_WeeklyCell_TimeEntryId = S_WeeklyCell.CreateAttribute("TimeEntryId");
    A_WeeklyCell_TimeEntryId.DataType = E_TimeEntry.IdentifierType;
    A_WeeklyCell_TimeEntryId.Label = "Time Entry";
    var A_WeeklyCell_Minutes = S_WeeklyCell.CreateAttribute("Minutes");
    A_WeeklyCell_Minutes.DataType = eSpace.IntegerType;
    A_WeeklyCell_Minutes.Label = "Minutes";
    var A_WeeklyCell_IsEditable = S_WeeklyCell.CreateAttribute("IsEditable");
    A_WeeklyCell_IsEditable.DataType = eSpace.BooleanType;
    A_WeeklyCell_IsEditable.Label = "Is Editable";
    var A_WeeklyCell_ShowDash = S_WeeklyCell.CreateAttribute("ShowDash");
    A_WeeklyCell_ShowDash.DataType = eSpace.BooleanType;
    A_WeeklyCell_ShowDash.Label = "Show Dash";
    A_WeeklyCell_ShowDash.Description = "True when the cell shows \"-\": no entry AND the day is in the future, a weekend, a holiday or a day off.";
    var A_WeeklyProjectRow_ProjectId = S_WeeklyProjectRow.CreateAttribute("ProjectId");
    A_WeeklyProjectRow_ProjectId.DataType = E_Project.IdentifierType;
    A_WeeklyProjectRow_ProjectId.Label = "Project";
    var A_WeeklyProjectRow_ClientName = S_WeeklyProjectRow.CreateAttribute("ClientName");
    A_WeeklyProjectRow_ClientName.DataType = eSpace.TextType;
    A_WeeklyProjectRow_ClientName.Length = 100;
    A_WeeklyProjectRow_ClientName.Label = "Client Name";
    var A_WeeklyProjectRow_ProjectName = S_WeeklyProjectRow.CreateAttribute("ProjectName");
    A_WeeklyProjectRow_ProjectName.DataType = eSpace.TextType;
    A_WeeklyProjectRow_ProjectName.Length = 100;
    A_WeeklyProjectRow_ProjectName.Label = "Project Name";
    var A_WeeklyProjectRow_ProjectCode = S_WeeklyProjectRow.CreateAttribute("ProjectCode");
    A_WeeklyProjectRow_ProjectCode.DataType = eSpace.TextType;
    A_WeeklyProjectRow_ProjectCode.Length = 30;
    A_WeeklyProjectRow_ProjectCode.Label = "Project Code";
    var A_WeeklyProjectRow_Cells = S_WeeklyProjectRow.CreateAttribute("Cells");
    A_WeeklyProjectRow_Cells.DataType = eSpace.GetOrCreateListType(S_WeeklyCell);
    A_WeeklyProjectRow_Cells.Label = "Cells";
    var A_WeeklyProjectRow_TotalMinutes = S_WeeklyProjectRow.CreateAttribute("TotalMinutes");
    A_WeeklyProjectRow_TotalMinutes.DataType = eSpace.IntegerType;
    A_WeeklyProjectRow_TotalMinutes.Label = "Total Minutes";
    var A_WeeklyHours_WeekStart = S_WeeklyHours.CreateAttribute("WeekStart");
    A_WeeklyHours_WeekStart.DataType = eSpace.DateType;
    A_WeeklyHours_WeekStart.Label = "Week Start";
    var A_WeeklyHours_WeekEnd = S_WeeklyHours.CreateAttribute("WeekEnd");
    A_WeeklyHours_WeekEnd.DataType = eSpace.DateType;
    A_WeeklyHours_WeekEnd.Label = "Week End";
    var A_WeeklyHours_Days = S_WeeklyHours.CreateAttribute("Days");
    A_WeeklyHours_Days.DataType = eSpace.GetOrCreateListType(S_WeekDayHeader);
    A_WeeklyHours_Days.Label = "Days";
    var A_WeeklyHours_Rows = S_WeeklyHours.CreateAttribute("Rows");
    A_WeeklyHours_Rows.DataType = eSpace.GetOrCreateListType(S_WeeklyProjectRow);
    A_WeeklyHours_Rows.Label = "Rows";
    var A_WeeklyHours_GrandTotalMinutes = S_WeeklyHours.CreateAttribute("GrandTotalMinutes");
    A_WeeklyHours_GrandTotalMinutes.DataType = eSpace.IntegerType;
    A_WeeklyHours_GrandTotalMinutes.Label = "Grand Total Minutes";
    var A_WeeklyHours_DayTotals = S_WeeklyHours.CreateAttribute("DayTotals");
    A_WeeklyHours_DayTotals.DataType = eSpace.GetOrCreateListType(S_WeeklyCell);
    A_WeeklyHours_DayTotals.Label = "Day Totals";
    A_WeeklyHours_DayTotals.Description = "Total per day (7 cells); ShowDash when every row is \"-\" that day.";
    var A_NumberOption_Value = S_NumberOption.CreateAttribute("Value");
    A_NumberOption_Value.DataType = eSpace.IntegerType;
    A_NumberOption_Value.Label = "Value";
    var A_NumberOption_Label = S_NumberOption.CreateAttribute("Label");
    A_NumberOption_Label.DataType = eSpace.TextType;
    A_NumberOption_Label.Label = "Label";
    var A_DayEditorLine_ProjectId = S_DayEditorLine.CreateAttribute("ProjectId");
    A_DayEditorLine_ProjectId.DataType = E_Project.IdentifierType;
    A_DayEditorLine_ProjectId.Label = "Project";
    var A_DayEditorLine_ClientName = S_DayEditorLine.CreateAttribute("ClientName");
    A_DayEditorLine_ClientName.DataType = eSpace.TextType;
    A_DayEditorLine_ClientName.Label = "Client Name";
    var A_DayEditorLine_ProjectName = S_DayEditorLine.CreateAttribute("ProjectName");
    A_DayEditorLine_ProjectName.DataType = eSpace.TextType;
    A_DayEditorLine_ProjectName.Label = "Project Name";
    var A_DayEditorLine_ProjectCode = S_DayEditorLine.CreateAttribute("ProjectCode");
    A_DayEditorLine_ProjectCode.DataType = eSpace.TextType;
    A_DayEditorLine_ProjectCode.Label = "Project Code";
    var A_DayEditorLine_Hours = S_DayEditorLine.CreateAttribute("Hours");
    A_DayEditorLine_Hours.DataType = eSpace.IntegerType;
    A_DayEditorLine_Hours.Label = "Hours";
    var A_DayEditorLine_Mins = S_DayEditorLine.CreateAttribute("Mins");
    A_DayEditorLine_Mins.DataType = eSpace.IntegerType;
    A_DayEditorLine_Mins.Label = "Mins";
    var A_DayEditorLine_Description = S_DayEditorLine.CreateAttribute("Description");
    A_DayEditorLine_Description.DataType = eSpace.TextType;
    A_DayEditorLine_Description.Label = "Description";
    var A_DayStatusKPI_CalendarDayTypeId = S_DayStatusKPI.CreateAttribute("CalendarDayTypeId");
    A_DayStatusKPI_CalendarDayTypeId.DataType = E_CalendarDayType.IdentifierType;
    A_DayStatusKPI_CalendarDayTypeId.Label = "Calendar Day Type";
    var A_DayStatusKPI_Label = S_DayStatusKPI.CreateAttribute("Label");
    A_DayStatusKPI_Label.DataType = eSpace.TextType;
    A_DayStatusKPI_Label.Length = 50;
    A_DayStatusKPI_Label.Label = "Label";
    var A_DayStatusKPI_CssClass = S_DayStatusKPI.CreateAttribute("CssClass");
    A_DayStatusKPI_CssClass.DataType = eSpace.TextType;
    A_DayStatusKPI_CssClass.Length = 50;
    A_DayStatusKPI_CssClass.Label = "Css Class";
    var A_DayStatusKPI_Count = S_DayStatusKPI.CreateAttribute("Count");
    A_DayStatusKPI_Count.DataType = eSpace.IntegerType;
    A_DayStatusKPI_Count.Label = "Count";
}
```

## Save 5 · 2026-10-15 17:55:12 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  { // Date_GetWeekStart
    
    /*** creating IServerAction 'Date_GetWeekStart' ***/
    var date_GetWeekStart = eSpace.CreateServerAction("Date_GetWeekStart");
    date_GetWeekStart.CacheInMinutes = null; // default value
    date_GetWeekStart.Description = "Returns the Monday of the week that contains Date.";
    date_GetWeekStart.Folder = null; // default value
    date_GetWeekStart.Function = true;
    date_GetWeekStart.Icon = null; // default value
    date_GetWeekStart.Public = false; // default value
    
    var date = date_GetWeekStart.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("WeekStart", "DateTimeToDate(AddDays(Date, -DecimalToInteger(Mod(DayOfWeek(Date) + 6, 7))))");
    
    var endNode = date_GetWeekStart.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var weekStart = date_GetWeekStart.CreateOutputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.SetDefaultValue(null); // default value
    weekStart.Description = null; // default value
  }
  { // Minutes_Format
    
    /*** creating IServerAction 'Minutes_Format' ***/
    var minutes_Format = eSpace.CreateServerAction("Minutes_Format");
    minutes_Format.CacheInMinutes = null; // default value
    minutes_Format.Description = "Formats a number of minutes as hours and minutes, e.g. 2250 -> \"37h30\" (Separator \"h\") or \"37:30\" (Separator \":\"). Minutes always have 2 digits.";
    minutes_Format.Folder = null; // default value
    minutes_Format.Function = true;
    minutes_Format.Icon = null; // default value
    minutes_Format.Public = false; // default value
    
    var minutes = minutes_Format.CreateInputParameter("Minutes");
    minutes.DataType = eSpace.IntegerType;
    minutes.Description = null; // default value
    minutes.IsMandatory = true; // default value
    
    var separator = minutes_Format.CreateInputParameter("Separator");
    separator.DataType = eSpace.TextType;
    separator.Description = "Text between hours and minutes, e.g. \"h\" or \":\".";
    separator.IsMandatory = true; // default value
    
    var startNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Result", "If(Minutes < 0, \"-\", \"\") + IntegerToText(DecimalToInteger(Trunc(Abs(Minutes) / 60))) + Separator + If(Mod(Abs(Minutes), 60) < 10, \"0\", \"\") + IntegerToText(DecimalToInteger(Mod(Abs(Minutes), 60)))");
    
    var endNode = minutes_Format.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode);
    endNode.IsDisabled = false; // default value
    
    var result = minutes_Format.CreateOutputParameter("Result");
    result.DataType = eSpace.TextType;
    result.SetDefaultValue(null); // default value
    result.Description = null; // default value
  }
  { // Holiday_CreateIfMissing
    
    /*** creating IServerAction 'Holiday_CreateIfMissing' ***/
    var holiday_CreateIfMissing = eSpace.CreateServerAction("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.CacheInMinutes = null; // default value
    holiday_CreateIfMissing.Description = "Creates an active Holiday for HolidayDate unless one already exists for that date (active or not). Used by Bootstrap.";
    holiday_CreateIfMissing.Folder = null; // default value
    holiday_CreateIfMissing.Function = false; // default value
    holiday_CreateIfMissing.Icon = null; // default value
    holiday_CreateIfMissing.Public = false; // default value
    
    var holidayDate = holiday_CreateIfMissing.CreateInputParameter("HolidayDate");
    holidayDate.DataType = eSpace.DateType;
    holidayDate.Description = null; // default value
    holidayDate.IsMandatory = true; // default value
    
    var name = holiday_CreateIfMissing.CreateInputParameter("Name");
    name.DataType = eSpace.TextType;
    name.Description = null; // default value
    name.IsMandatory = true; // default value
    
    var newHoliday = holiday_CreateIfMissing.CreateLocalVariable("NewHoliday");
    var holiday = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    newHoliday.DataType = holiday;
    newHoliday.Description = null; // default value
    
    var startNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var getExistingHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetExistingHoliday").ConnectedBelow(startNode, 1371);
    getExistingHoliday.CacheInMinutes = null; // default value
    getExistingHoliday.Description = null; // default value
    getExistingHoliday.IsDisabled = false; // default value
    getExistingHoliday.SetMaxRecords(null); // default value
    getExistingHoliday.SetStartIndex(null); // default value
    getExistingHoliday.Timeout = null; // default value
    var holiday2 = getExistingHoliday.AsDatabaseAggregate.CreateSource(holiday);
    getExistingHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = HolidayDate");
    
    
    var ifNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getExistingHoliday, 1371);
    ifNode.SetCondition("GetExistingHoliday.List.Empty");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Missing?";
    
    var endNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1828);
    endNode.IsDisabled = false; // default value
    ifNode.FalseTarget = endNode;
    
    var assignNode = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("NewHoliday.HolidayDate", "HolidayDate");
    
    var assignment2 = assignNode.CreateAssignment("NewHoliday.Name", "Name");
    
    var assignment3 = assignNode.CreateAssignment("NewHoliday.IsActive", "True");
    ifNode.TrueTarget = assignNode;
    
    var createHoliday = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateHoliday").ConnectedBelow(assignNode, 1371);
    createHoliday.Action = holiday.CreateAction;
    createHoliday.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createHoliday.IsDisabled = false; // default value
    createHoliday.ServerRequestTimeout = null; // default value
    var createHoliday2 = holiday.CreateAction;
    var source = createHoliday2.InputParameters.Named("Source");
    createHoliday.SetArgumentValue(source, "NewHoliday");
    
    var endNode2 = holiday_CreateIfMissing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(createHoliday, 1371);
    endNode2.IsDisabled = false; // default value
  }
  { // Project_GetAssignedToUser
    
    /*** creating IServerAction 'Project_GetAssignedToUser' ***/
    var project_GetAssignedToUser = eSpace.CreateServerAction("Project_GetAssignedToUser");
    project_GetAssignedToUser.CacheInMinutes = null; // default value
    project_GetAssignedToUser.Description = "Active projects (with active client) the current user is assigned to on ForDate, sorted by client and project name.";
    project_GetAssignedToUser.Folder = null; // default value
    project_GetAssignedToUser.Function = false; // default value
    project_GetAssignedToUser.Icon = null; // default value
    project_GetAssignedToUser.Public = false; // default value (set to true at the end)
    
    var forDate = project_GetAssignedToUser.CreateInputParameter("ForDate");
    forDate.DataType = eSpace.DateType;
    forDate.Description = null; // default value
    forDate.IsMandatory = true; // default value
    
    var line = project_GetAssignedToUser.CreateLocalVariable("Line");
    var daySheetProjectLine = eSpace.Structures.Named("DaySheetProjectLine");
    line.DataType = daySheetProjectLine;
    line.Description = null; // default value
    
    var startNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getAssignedProjects = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetAssignedProjects").Below(ifNode, 1371);
    getAssignedProjects.CacheInMinutes = null; // default value
    getAssignedProjects.Description = null; // default value
    getAssignedProjects.IsDisabled = false; // default value
    getAssignedProjects.SetMaxRecords(null); // default value
    getAssignedProjects.SetStartIndex(null); // default value
    getAssignedProjects.Timeout = null; // default value
    var projectAssignment2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("ProjectAssignment");
    var projectAssignment = getAssignedProjects.AsDatabaseAggregate.CreateSource(projectAssignment2);
    var client2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client = getAssignedProjects.AsDatabaseAggregate.CreateSource(client2);
    var project2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project = getAssignedProjects.AsDatabaseAggregate.CreateSource(project2);
    var join = getAssignedProjects.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = projectAssignment;
    join.RightSource = project;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("ProjectAssignment.ProjectId = Project.Id");
    var join2 = getAssignedProjects.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = project;
    join2.RightSource = client;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Project.ClientId = Client.Id");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.UserId = GetUserId()");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.StartDate <= ForDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("ProjectAssignment.EndDate = NullDate() or ProjectAssignment.EndDate >= ForDate");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Project.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateFilter("Client.IsActive");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Client.Name");
    getAssignedProjects.AsDatabaseAggregate.CreateSort().SetAttribute("Project.Name");
    
    ifNode.TrueTarget = getAssignedProjects;
    
    var forEachNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getAssignedProjects, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetAssignedProjects.List");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Line.ProjectId", "GetAssignedProjects.List.Current.Project.Id");
    
    var assignment2 = assignNode.CreateAssignment("Line.ClientName", "GetAssignedProjects.List.Current.Client.Name");
    
    var assignment3 = assignNode.CreateAssignment("Line.ProjectName", "GetAssignedProjects.List.Current.Project.Name");
    
    var assignment4 = assignNode.CreateAssignment("Line.ProjectCode", "GetAssignedProjects.List.Current.Project.Code");
    
    var assignment5 = assignNode.CreateAssignment("Line.TimeEntryId", "NullIdentifier()");
    
    var assignment6 = assignNode.CreateAssignment("Line.Minutes", "0");
    
    var assignment7 = assignNode.CreateAssignment("Line.Description", "\"\"");
    forEachNode.CycleTarget = assignNode;
    
    var endNode = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 2742);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var listAppend = project_GetAssignedToUser.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    listAppend.Target = forEachNode;
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Line");
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Projects");
    
    var projects = project_GetAssignedToUser.CreateOutputParameter("Projects");
    projects.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    projects.Description = null; // default value
    project_GetAssignedToUser.Public = true;
  }
  { // Calendar_GetMonth
    
    /*** creating IServerAction 'Calendar_GetMonth' ***/
    var calendar_GetMonth = eSpace.CreateServerAction("Calendar_GetMonth");
    calendar_GetMonth.CacheInMinutes = null; // default value
    calendar_GetMonth.Description = "One CalendarDay per day of the month for the current user, classified (first match wins) as Holiday, DayOff, Confirmed, Weekend, Future (after today, server date) or Pending. IsEditable = weekday that is not a holiday.";
    calendar_GetMonth.Folder = null; // default value
    calendar_GetMonth.Function = false; // default value
    calendar_GetMonth.Icon = null; // default value
    calendar_GetMonth.Public = false; // default value (set to true at the end)
    
    var year = calendar_GetMonth.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = calendar_GetMonth.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var firstDay = calendar_GetMonth.CreateLocalVariable("FirstDay");
    firstDay.DataType = eSpace.DateType;
    firstDay.SetDefaultValue(null); // default value
    firstDay.Description = null; // default value
    
    var lastDay = calendar_GetMonth.CreateLocalVariable("LastDay");
    lastDay.DataType = eSpace.DateType;
    lastDay.SetDefaultValue(null); // default value
    lastDay.Description = null; // default value
    
    var today = calendar_GetMonth.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    today.SetDefaultValue(null); // default value
    today.Description = null; // default value
    
    var dayIndex = calendar_GetMonth.CreateLocalVariable("DayIndex");
    dayIndex.DataType = eSpace.IntegerType;
    dayIndex.SetDefaultValue(null); // default value
    dayIndex.Description = null; // default value
    
    var calDay = calendar_GetMonth.CreateLocalVariable("CalDay");
    var calendarDay = eSpace.Structures.Named("CalendarDay");
    calDay.DataType = calendarDay;
    calDay.Description = null; // default value
    
    var startNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("FirstDay", "NewDate(Year, Month, 1)");
    
    var assignment2 = assignNode.CreateAssignment("LastDay", "DateTimeToDate(AddDays(AddMonths(FirstDay, 1), -1))");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var assignment4 = assignNode.CreateAssignment("DayIndex", "1");
    ifNode.TrueTarget = assignNode;
    
    var ifNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode2.SetCondition("DayIndex <= Day(LastDay)");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "More days?";
    
    var getMonthHolidays = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthHolidays").Below(ifNode2, 4113);
    getMonthHolidays.CacheInMinutes = null; // default value
    getMonthHolidays.Description = null; // default value
    getMonthHolidays.IsDisabled = false; // default value
    getMonthHolidays.SetMaxRecords(null); // default value
    getMonthHolidays.SetStartIndex(null); // default value
    getMonthHolidays.Timeout = null; // default value
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getMonthHolidays.AsDatabaseAggregate.CreateSource(holiday2);
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getMonthHolidays.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate >= FirstDay and Holiday.HolidayDate <= LastDay");
    
    ifNode2.FalseTarget = getMonthHolidays;
    
    var assignNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment5 = assignNode2.CreateAssignment("CalDay.Date", "DateTimeToDate(AddDays(FirstDay, DayIndex - 1))");
    
    var assignment6 = assignNode2.CreateAssignment("CalDay.DayNumber", "DayIndex");
    
    var assignment7 = assignNode2.CreateAssignment("CalDay.WeekDay", "DecimalToInteger(Mod(DayOfWeek(CalDay.Date) + 6, 7)) + 1");
    
    var assignment8 = assignNode2.CreateAssignment("CalDay.IsWeekend", "CalDay.WeekDay >= 6");
    
    var assignment9 = assignNode2.CreateAssignment("CalDay.IsToday", "CalDay.Date = Today");
    ifNode2.TrueTarget = assignNode2;
    
    var forEachNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthHolidays, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetMonthHolidays.List");
    forEachNode.SetStartIndex(null); // default value
    
    var listAppend = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode2, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Days");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "CalDay");
    
    var assignNode3 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.Target = forEachNode;
    
    var assignment10 = assignNode3.CreateAssignment("Days[Day(GetMonthHolidays.List.Current.Holiday.HolidayDate) - 1].HolidayName", "GetMonthHolidays.List.Current.Holiday.Name");
    forEachNode.CycleTarget = assignNode3;
    
    var getMonthDaySheets = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthDaySheets").Below(forEachNode, 1371);
    getMonthDaySheets.CacheInMinutes = null; // default value
    getMonthDaySheets.Description = null; // default value
    getMonthDaySheets.IsDisabled = false; // default value
    getMonthDaySheets.SetMaxRecords(null); // default value
    getMonthDaySheets.SetStartIndex(null); // default value
    getMonthDaySheets.Timeout = null; // default value
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getMonthDaySheets.AsDatabaseAggregate.CreateSource(daySheet2);
    getMonthDaySheets.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getMonthDaySheets.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= FirstDay and DaySheet.SheetDate <= LastDay");
    
    forEachNode.Target = getMonthDaySheets;
    
    var assignNode4 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.Target = ifNode2;
    
    var assignment11 = assignNode4.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var forEachNode2 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthDaySheets, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("GetMonthDaySheets.List");
    forEachNode2.SetStartIndex(null); // default value
    
    var assignNode5 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode2, 1828);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    assignNode5.Target = forEachNode2;
    
    var assignment12 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DaySheetId", "GetMonthDaySheets.List.Current.DaySheet.Id");
    
    var assignment13 = assignNode5.CreateAssignment("Days[Day(GetMonthDaySheets.List.Current.DaySheet.SheetDate) - 1].DayStatusId", "GetMonthDaySheets.List.Current.DaySheet.DayStatusId");
    forEachNode2.CycleTarget = assignNode5;
    
    var getMonthMinutes = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetMonthMinutes").Below(forEachNode2, 1371);
    getMonthMinutes.CacheInMinutes = null; // default value
    getMonthMinutes.Description = null; // default value
    getMonthMinutes.IsDisabled = false; // default value
    getMonthMinutes.SetMaxRecords(null); // default value
    getMonthMinutes.SetStartIndex(null); // default value
    getMonthMinutes.Timeout = null; // default value
    var timeEntry2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var timeEntry = getMonthMinutes.AsDatabaseAggregate.CreateSource(timeEntry2);
    var daySheet3 = getMonthMinutes.AsDatabaseAggregate.CreateSource(daySheet2);
    var join = getMonthMinutes.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet3;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    getMonthMinutes.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getMonthMinutes.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= FirstDay and DaySheet.SheetDate <= LastDay");
    getMonthMinutes.AsDatabaseAggregate.CreateGroupByAttribute("SheetDate").SetAttribute("DaySheet.SheetDate");
    var totalMinutes = getMonthMinutes.AsDatabaseAggregate.CreateAggregatedAttribute("TotalMinutes");
    totalMinutes.SetAttribute("TimeEntry.Minutes");
    totalMinutes.AggregationType = AggregationType.Sum;
    
    forEachNode2.Target = getMonthMinutes;
    
    var forEachNode3 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getMonthMinutes, 1371);
    forEachNode3.IsDisabled = false; // default value
    forEachNode3.Label = null; // default value
    forEachNode3.SetMaximumIterations(null); // default value
    forEachNode3.SetRecordList("GetMonthMinutes.List");
    forEachNode3.SetStartIndex(null); // default value
    
    var assignNode6 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 1828);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.Target = forEachNode3;
    
    var assignment14 = assignNode6.CreateAssignment("Days[Day(GetMonthMinutes.List.Current.SheetDate) - 1].TotalMinutes", "LongIntegerToInteger(GetMonthMinutes.List.Current.TotalMinutes)");
    forEachNode3.CycleTarget = assignNode6;
    
    var forEachNode4 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(forEachNode3, 1371);
    forEachNode4.IsDisabled = false; // default value
    forEachNode4.Label = null; // default value
    forEachNode4.SetMaximumIterations(null); // default value
    forEachNode4.SetRecordList("Days");
    forEachNode4.SetStartIndex(null); // default value
    forEachNode3.Target = forEachNode4;
    
    var assignNode7 = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode4, 1828);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    assignNode7.Target = forEachNode4;
    
    var assignment15 = assignNode7.CreateAssignment("Days.Current.DayTypeId", "If(Days.Current.HolidayName <> \"\", Entities.CalendarDayType.Holiday, If(Days.Current.DayStatusId = Entities.DayStatus.DayOff, Entities.CalendarDayType.DayOff, If(Days.Current.DayStatusId = Entities.DayStatus.Confirmed, Entities.CalendarDayType.Confirmed, If(Days.Current.IsWeekend, Entities.CalendarDayType.Weekend, If(Days.Current.Date > Today, Entities.CalendarDayType.Future, Entities.CalendarDayType.Pending)))))");
    
    var assignment16 = assignNode7.CreateAssignment("Days.Current.CssClass", "If(Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, \"day-holiday\", If(Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, \"day-off\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, \"day-confirmed\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Weekend, \"day-weekend\", If(Days.Current.DayTypeId = Entities.CalendarDayType.Future, \"day-future\", \"day-pending\")))))");
    
    var assignment17 = assignNode7.CreateAssignment("Days.Current.IsEditable", "not Days.Current.IsWeekend and Days.Current.HolidayName = \"\"");
    forEachNode4.CycleTarget = assignNode7;
    
    var endNode = calendar_GetMonth.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode4, 1371);
    endNode.IsDisabled = false; // default value
    forEachNode4.Target = endNode;
    
    var days = calendar_GetMonth.CreateOutputParameter("Days");
    days.DataType = eSpace.GetOrCreateListType(calendarDay);
    days.Description = null; // default value
    calendar_GetMonth.Public = true;
  }
  { // Settings
    /*** creating IServerAction 'Settings_GetDailyExpectedMinutes' (new in TT4) ***/
    var settings_GetDailyExpectedMinutes = eSpace.CreateServerAction("Settings_GetDailyExpectedMinutes");
    settings_GetDailyExpectedMinutes.Description = "Returns the DailyExpectedMinutes site property (minutes a user is expected to record per working day), for modules that can't read the site property directly.";
    settings_GetDailyExpectedMinutes.Public = true;
    var minutes = settings_GetDailyExpectedMinutes.CreateOutputParameter("Minutes");
    minutes.DataType = eSpace.IntegerType;
    var startNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    var ifNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.Label = "!Is SheetUser?";
    var raiseExceptionNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    ifNode.FalseTarget = raiseExceptionNode;
    var assignNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.CreateAssignment("Minutes", "Site.DailyExpectedMinutes");
    ifNode.TrueTarget = assignNode;
    var endNode = settings_GetDailyExpectedMinutes.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
  }
}
```

## Save 6 · 2026-10-15 17:56:35 UTC

- **Published:** 2026-10-15 18:13:37 UTC (manual 1-Click Publish; logged by Claude from a subagent's returned lambdas)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  { // Calendar_GetMonthSummary
    
    /*** creating IServerAction 'Calendar_GetMonthSummary' ***/
    var calendar_GetMonthSummary = eSpace.CreateServerAction("Calendar_GetMonthSummary");
    calendar_GetMonthSummary.CacheInMinutes = null; // default value
    calendar_GetMonthSummary.Description = "Month totals and KPI counts for the current user, derived from GetMonthCalendar. TotalMinutes = minutes on Draft/Confirmed day sheets. ExpectedMinutes = (weekdays - holidays - days off of the whole month) * Site.DailyExpectedMinutes. ProgressPercent = Total/Expected*100 capped at 100 (0 when Expected = 0). ElapsedDays = calendar days elapsed in the month up to today (past month = days in month, future month = 0).";
    calendar_GetMonthSummary.Folder = null; // default value
    calendar_GetMonthSummary.Function = false; // default value
    calendar_GetMonthSummary.Icon = null; // default value
    calendar_GetMonthSummary.Public = false; // default value (set to true at the end)
    
    var year = calendar_GetMonthSummary.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = calendar_GetMonthSummary.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var today = calendar_GetMonthSummary.CreateLocalVariable("Today");
    today.DataType = eSpace.DateType;
    today.Description = null; // default value
    
    var expectedDays = calendar_GetMonthSummary.CreateLocalVariable("ExpectedDays");
    expectedDays.DataType = eSpace.IntegerType;
    expectedDays.Description = null; // default value
    
    var startNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var calendar_GetMonth = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").Below(ifNode, 1371);
    var calendar_GetMonth2 = eSpace.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    calendar_GetMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonth.IsDisabled = false; // default value
    calendar_GetMonth.ServerRequestTimeout = null; // default value
    var month2 = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month2, "Month");
    var year2 = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year2, "Year");
    ifNode.TrueTarget = calendar_GetMonth;
    
    var assignNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonth, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Summary.Year", "Year");
    
    var assignment2 = assignNode.CreateAssignment("Summary.Month", "Month");
    
    var assignment3 = assignNode.CreateAssignment("Today", "CurrDate()");
    
    var forEachNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Calendar_GetMonth.Days");
    forEachNode.SetStartIndex(null); // default value
    
    var assignNode2 = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 1828);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode;
    
    var assignment4 = assignNode2.CreateAssignment("Summary.TotalMinutes", "Summary.TotalMinutes + If(Calendar_GetMonth.Days.Current.DayStatusId = Entities.DayStatus.DayOff, 0, Calendar_GetMonth.Days.Current.TotalMinutes)");
    
    var assignment5 = assignNode2.CreateAssignment("Summary.ConfirmedCount", "Summary.ConfirmedCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Confirmed, 1, 0)");
    
    var assignment6 = assignNode2.CreateAssignment("Summary.DayOffCount", "Summary.DayOffCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.DayOff, 1, 0)");
    
    var assignment7 = assignNode2.CreateAssignment("Summary.HolidayCount", "Summary.HolidayCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Holiday, 1, 0)");
    
    var assignment8 = assignNode2.CreateAssignment("Summary.PendingCount", "Summary.PendingCount + If(Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Pending, 1, 0)");
    
    var assignment9 = assignNode2.CreateAssignment("ExpectedDays", "ExpectedDays + If(not Calendar_GetMonth.Days.Current.IsWeekend and Calendar_GetMonth.Days.Current.DayTypeId <> Entities.CalendarDayType.Holiday and Calendar_GetMonth.Days.Current.DayTypeId <> Entities.CalendarDayType.DayOff, 1, 0)");
    forEachNode.CycleTarget = assignNode2;
    
    var assignNode3 = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(forEachNode, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment10 = assignNode3.CreateAssignment("Summary.ElapsedDays", "If(Year * 100 + Month < Year(Today) * 100 + Month(Today), Calendar_GetMonth.Days.Length, If(Year = Year(Today) and Month = Month(Today), Day(Today), 0))");
    
    var assignment11 = assignNode3.CreateAssignment("Summary.ExpectedMinutes", "ExpectedDays * Site.DailyExpectedMinutes");
    
    var assignment12 = assignNode3.CreateAssignment("Summary.ProgressPercent", "If(Summary.ExpectedMinutes > 0, Min(100, Summary.TotalMinutes * 100 / Summary.ExpectedMinutes), 0)");
    forEachNode.Target = assignNode3;
    
    var endNode = calendar_GetMonthSummary.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode.IsDisabled = false; // default value
    
    var summary = calendar_GetMonthSummary.CreateOutputParameter("Summary");
    var monthSummary = eSpace.Structures.Named("MonthSummary");
    summary.DataType = monthSummary;
    summary.Description = null; // default value
    calendar_GetMonthSummary.Public = true;
  }
  { // DaySheet_GetDetail
    
    /*** creating IServerAction 'DaySheet_GetDetail' ***/
    var daySheet_GetDetail = eSpace.CreateServerAction("DaySheet_GetDetail");
    daySheet_GetDetail.CacheInMinutes = null; // default value
    daySheet_GetDetail.Description = "Day editor data for the current user: DaySheet status and notes, holiday, and one line per project assigned on Date (GetUserProjects) merged with the existing TimeEntry minutes and description.";
    daySheet_GetDetail.Folder = null; // default value
    daySheet_GetDetail.Function = false; // default value
    daySheet_GetDetail.Icon = null; // default value
    daySheet_GetDetail.Public = false; // default value (set to true at the end)
    
    var date = daySheet_GetDetail.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "!Is SheetUser?";
    
    var raiseExceptionNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getDayRecord = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode, 1371);
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet2);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode.TrueTarget = getDayRecord;
    
    var getDayEntries = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayEntries").ConnectedBelow(getDayRecord, 1371);
    getDayEntries.CacheInMinutes = null; // default value
    getDayEntries.Description = null; // default value
    getDayEntries.IsDisabled = false; // default value
    getDayEntries.SetMaxRecords(null); // default value
    getDayEntries.SetStartIndex(null); // default value
    getDayEntries.Timeout = null; // default value
    var timeEntry2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var timeEntry = getDayEntries.AsDatabaseAggregate.CreateSource(timeEntry2);
    var daySheet3 = getDayEntries.AsDatabaseAggregate.CreateSource(daySheet2);
    var join = getDayEntries.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet3;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    getDayEntries.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayEntries.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    
    var getDayHoliday = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayHoliday").ConnectedBelow(getDayEntries, 1371);
    getDayHoliday.CacheInMinutes = null; // default value
    getDayHoliday.Description = null; // default value
    getDayHoliday.IsDisabled = false; // default value
    getDayHoliday.SetMaxRecords(null); // default value
    getDayHoliday.SetStartIndex(null); // default value
    getDayHoliday.Timeout = null; // default value
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getDayHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = Date");
    
    
    var project_GetAssignedToUser = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetAssignedToUser").ConnectedBelow(getDayHoliday, 1371);
    var project_GetAssignedToUser2 = eSpace.ServerActions.Named("Project_GetAssignedToUser");
    project_GetAssignedToUser.Action = project_GetAssignedToUser2;
    project_GetAssignedToUser.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetAssignedToUser.IsDisabled = false; // default value
    project_GetAssignedToUser.ServerRequestTimeout = null; // default value
    var forDate = project_GetAssignedToUser2.InputParameters.Named("ForDate");
    project_GetAssignedToUser.SetArgumentValue(forDate, "Date");
    
    var assignNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(project_GetAssignedToUser, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Detail.SheetDate", "Date");
    
    var assignment2 = assignNode.CreateAssignment("Detail.DaySheetId", "GetDayRecord.List.Current.DaySheet.Id");
    
    var assignment3 = assignNode.CreateAssignment("Detail.DayStatusId", "GetDayRecord.List.Current.DaySheet.DayStatusId");
    
    var assignment4 = assignNode.CreateAssignment("Detail.Notes", "GetDayRecord.List.Current.DaySheet.Notes");
    
    var assignment5 = assignNode.CreateAssignment("Detail.HolidayName", "GetDayHoliday.List.Current.Holiday.Name");
    
    var assignment6 = assignNode.CreateAssignment("Detail.IsEditable", "Mod(DayOfWeek(Date) + 6, 7) < 5 and GetDayHoliday.List.Empty");
    
    var assignment7 = assignNode.CreateAssignment("Detail.DayTypeId", "If(not GetDayHoliday.List.Empty, Entities.CalendarDayType.Holiday, If(GetDayRecord.List.Current.DaySheet.DayStatusId = Entities.DayStatus.DayOff, Entities.CalendarDayType.DayOff, If(GetDayRecord.List.Current.DaySheet.DayStatusId = Entities.DayStatus.Confirmed, Entities.CalendarDayType.Confirmed, If(Mod(DayOfWeek(Date) + 6, 7) >= 5, Entities.CalendarDayType.Weekend, If(Date > CurrDate(), Entities.CalendarDayType.Future, Entities.CalendarDayType.Pending)))))");
    
    var assignment8 = assignNode.CreateAssignment("Detail.Lines", "Project_GetAssignedToUser.Projects");
    
    var forEachNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Detail.Lines");
    forEachNode.SetStartIndex(null); // default value
    
    var forEachNode2 = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ToTheRightOf(forEachNode, 1828);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("GetDayEntries.List");
    forEachNode2.SetStartIndex(null); // default value
    forEachNode2.Target = forEachNode;
    forEachNode.CycleTarget = forEachNode2;
    
    var endNode = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 2742);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var ifNode2 = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode2, 1828);
    ifNode2.SetCondition("GetDayEntries.List.Current.TimeEntry.ProjectId = Detail.Lines.Current.ProjectId");
    ifNode2.FalseTarget = forEachNode2;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Same project?";
    forEachNode2.CycleTarget = ifNode2;
    
    var assignNode2 = daySheet_GetDetail.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode2, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode2;
    
    var assignment9 = assignNode2.CreateAssignment("Detail.Lines.Current.TimeEntryId", "GetDayEntries.List.Current.TimeEntry.Id");
    
    var assignment10 = assignNode2.CreateAssignment("Detail.Lines.Current.Minutes", "GetDayEntries.List.Current.TimeEntry.Minutes");
    
    var assignment11 = assignNode2.CreateAssignment("Detail.Lines.Current.Description", "GetDayEntries.List.Current.TimeEntry.Description");
    
    var assignment12 = assignNode2.CreateAssignment("Detail.TotalMinutes", "Detail.TotalMinutes + GetDayEntries.List.Current.TimeEntry.Minutes");
    ifNode2.TrueTarget = assignNode2;
    
    var detail = daySheet_GetDetail.CreateOutputParameter("Detail");
    var daySheetDetail = eSpace.Structures.Named("DaySheetDetail");
    detail.DataType = daySheetDetail;
    detail.Description = null; // default value
    daySheet_GetDetail.Public = true;
  }
  { // DaySheet_Save
    
    /*** creating IServerAction 'DaySheet_Save' ***/
    var daySheet_Save = eSpace.CreateServerAction("DaySheet_Save");
    daySheet_Save.CacheInMinutes = null; // default value
    daySheet_Save.Description = "Validates and saves the current user\'s day: weekday, not a holiday, no hours in the future, projects assigned on Date, minutes multiple of 15, total <= 24:00, Confirm needs a total > 0. Upserts the DaySheet (DayOff / Confirmed / Draft) and its TimeEntries; IsDayOff deletes the day\'s entries. Raises DaySheetValidation with a user-facing message.";
    daySheet_Save.Folder = null; // default value
    daySheet_Save.Function = false; // default value
    daySheet_Save.Icon = null; // default value
    daySheet_Save.Public = false; // default value (set to true at the end)
    
    var date = daySheet_Save.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var isDayOff = daySheet_Save.CreateInputParameter("IsDayOff");
    isDayOff.DataType = eSpace.BooleanType;
    isDayOff.Description = null; // default value
    isDayOff.IsMandatory = true; // default value
    
    var confirm = daySheet_Save.CreateInputParameter("Confirm");
    confirm.DataType = eSpace.BooleanType;
    confirm.Description = null; // default value
    confirm.IsMandatory = true; // default value
    
    var entries = daySheet_Save.CreateInputParameter("Entries");
    var timeEntryInput = eSpace.Structures.Named("TimeEntryInput");
    entries.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    entries.Description = null; // default value
    entries.IsMandatory = false;
    
    var notes = daySheet_Save.CreateInputParameter("Notes");
    notes.DataType = eSpace.TextType;
    notes.Description = null; // default value
    notes.IsMandatory = false;
    
    var total = daySheet_Save.CreateLocalVariable("Total");
    total.DataType = eSpace.IntegerType;
    total.Description = null; // default value
    
    var found = daySheet_Save.CreateLocalVariable("Found");
    found.DataType = eSpace.BooleanType;
    found.Description = null; // default value
    
    var sheetId = daySheet_Save.CreateLocalVariable("SheetId");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    sheetId.DataType = daySheet.IdentifierType;
    sheetId.Description = null; // default value
    
    var sheetRec = daySheet_Save.CreateLocalVariable("SheetRec");
    sheetRec.DataType = daySheet;
    sheetRec.Description = null; // default value
    
    var entryRec = daySheet_Save.CreateLocalVariable("EntryRec");
    var timeEntry = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    entryRec.DataType = timeEntry;
    entryRec.Description = null; // default value
    
    var startNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var ifNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1371);
    ifNode2.SetCondition("Mod(DayOfWeek(Date) + 6, 7) >= 5");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Weekend?";
    ifNode.TrueTarget = ifNode2;
    
    var getDayHoliday = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayHoliday").Below(ifNode2, 1371);
    getDayHoliday.CacheInMinutes = null; // default value
    getDayHoliday.Description = null; // default value
    getDayHoliday.IsDisabled = false; // default value
    getDayHoliday.SetMaxRecords(null); // default value
    getDayHoliday.SetStartIndex(null); // default value
    getDayHoliday.Timeout = null; // default value
    var holiday2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Holiday");
    var holiday = getDayHoliday.AsDatabaseAggregate.CreateSource(holiday2);
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.IsActive");
    getDayHoliday.AsDatabaseAggregate.CreateFilter("Holiday.HolidayDate = Date");
    
    ifNode2.FalseTarget = getDayHoliday;
    
    var raiseExceptionNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode2, 1828);
    var daySheetValidation = eSpace.UserExceptions.Named("DaySheetValidation");
    raiseExceptionNode2.Exception = daySheetValidation;
    raiseExceptionNode2.SetExceptionMessage("\"Nothing can be saved on a weekend.\"");
    raiseExceptionNode2.IsDisabled = false; // default value
    ifNode2.TrueTarget = raiseExceptionNode2;
    
    var ifNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayHoliday, 1371);
    ifNode3.SetCondition("not GetDayHoliday.List.Empty");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "!Holiday?";
    
    var project_GetAssignedToUser = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Project_GetAssignedToUser").Below(ifNode3, 1371);
    var project_GetAssignedToUser2 = eSpace.ServerActions.Named("Project_GetAssignedToUser");
    project_GetAssignedToUser.Action = project_GetAssignedToUser2;
    project_GetAssignedToUser.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    project_GetAssignedToUser.IsDisabled = false; // default value
    project_GetAssignedToUser.ServerRequestTimeout = null; // default value
    var forDate = project_GetAssignedToUser2.InputParameters.Named("ForDate");
    project_GetAssignedToUser.SetArgumentValue(forDate, "Date");
    ifNode3.FalseTarget = project_GetAssignedToUser;
    
    var raiseExceptionNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode3, 1828);
    raiseExceptionNode3.Exception = daySheetValidation;
    raiseExceptionNode3.SetExceptionMessage("GetDayHoliday.List.Current.Holiday.Name + \" is a holiday: nothing can be saved on this day.\"");
    raiseExceptionNode3.IsDisabled = false; // default value
    ifNode3.TrueTarget = raiseExceptionNode3;
    
    var forEachNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(project_GetAssignedToUser, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Entries");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode4.SetCondition("Entries.Current.Minutes < 0 or Mod(Entries.Current.Minutes, 15) <> 0");
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "!Invalid minutes?";
    forEachNode.CycleTarget = ifNode4;
    
    var ifNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode, 9597);
    ifNode5.SetCondition("not IsDayOff and Total > 0 and Date > CurrDate()");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "!Hours in the future?";
    forEachNode.Target = ifNode5;
    
    var assignNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Total", "Total + Entries.Current.Minutes");
    
    var assignment2 = assignNode.CreateAssignment("Found", "False");
    ifNode4.FalseTarget = assignNode;
    
    var raiseExceptionNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode4, 1828);
    raiseExceptionNode4.Exception = daySheetValidation;
    raiseExceptionNode4.SetExceptionMessage("\"Minutes must be a multiple of 15.\"");
    raiseExceptionNode4.IsDisabled = false; // default value
    ifNode4.TrueTarget = raiseExceptionNode4;
    
    var ifNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode5, 1371);
    ifNode6.SetCondition("not IsDayOff and Total > 1440");
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = "!Over 24:00?";
    ifNode5.FalseTarget = ifNode6;
    
    var raiseExceptionNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode5, 1828);
    raiseExceptionNode5.Exception = daySheetValidation;
    raiseExceptionNode5.SetExceptionMessage("\"Hours cannot be logged in the future.\"");
    raiseExceptionNode5.IsDisabled = false; // default value
    ifNode5.TrueTarget = raiseExceptionNode5;
    
    var ifNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode, 1371);
    ifNode7.SetCondition("Entries.Current.Minutes > 0");
    ifNode7.FalseTarget = forEachNode;
    ifNode7.IsDisabled = false; // default value
    ifNode7.Label = "Has minutes?";
    
    var ifNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode6, 1371);
    ifNode8.SetCondition("Confirm and not IsDayOff and Total = 0");
    ifNode8.IsDisabled = false; // default value
    ifNode8.Label = "!Confirm without hours?";
    ifNode6.FalseTarget = ifNode8;
    
    var raiseExceptionNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode6, 1828);
    raiseExceptionNode6.Exception = daySheetValidation;
    raiseExceptionNode6.SetExceptionMessage("\"A day cannot exceed 24:00\"");
    raiseExceptionNode6.IsDisabled = false; // default value
    ifNode6.TrueTarget = raiseExceptionNode6;
    
    var forEachNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(ifNode7, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("Project_GetAssignedToUser.Projects");
    forEachNode2.SetStartIndex(null); // default value
    ifNode7.TrueTarget = forEachNode2;
    
    var getDayRecord = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode8, 1371);
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    var daySheet2 = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode8.FalseTarget = getDayRecord;
    
    var raiseExceptionNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode8, 1828);
    raiseExceptionNode7.Exception = daySheetValidation;
    raiseExceptionNode7.SetExceptionMessage("\"Log at least 15 minutes to confirm the day\"");
    raiseExceptionNode7.IsDisabled = false; // default value
    ifNode8.TrueTarget = raiseExceptionNode7;
    
    var ifNode9 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode2, 1828);
    ifNode9.SetCondition("Project_GetAssignedToUser.Projects.Current.ProjectId = Entries.Current.ProjectId");
    ifNode9.FalseTarget = forEachNode2;
    ifNode9.IsDisabled = false; // default value
    ifNode9.Label = "!Same project?";
    forEachNode2.CycleTarget = ifNode9;
    
    var ifNode10 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode2, 2742);
    ifNode10.SetCondition("not Found");
    ifNode10.FalseTarget = forEachNode;
    ifNode10.IsDisabled = false; // default value
    ifNode10.Label = "!Not assigned?";
    forEachNode2.Target = ifNode10;
    
    var ifNode11 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode11.SetCondition("GetDayRecord.List.Empty");
    ifNode11.IsDisabled = false; // default value
    ifNode11.Label = "!New day sheet?";
    
    var assignNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode9, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode2;
    
    var assignment3 = assignNode2.CreateAssignment("Found", "True");
    ifNode9.TrueTarget = assignNode2;
    
    var raiseExceptionNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().Below(ifNode10, 1371);
    raiseExceptionNode8.Exception = daySheetValidation;
    raiseExceptionNode8.SetExceptionMessage("\"You are not assigned to one of these projects on this day.\"");
    raiseExceptionNode8.IsDisabled = false; // default value
    ifNode10.TrueTarget = raiseExceptionNode8;
    
    var assignNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode11, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment4 = assignNode3.CreateAssignment("SheetRec", "GetDayRecord.List.Current.DaySheet");
    ifNode11.FalseTarget = assignNode3;
    
    var assignNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode11, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment5 = assignNode4.CreateAssignment("SheetRec.UserId", "GetUserId()");
    
    var assignment6 = assignNode4.CreateAssignment("SheetRec.SheetDate", "Date");
    
    var assignment7 = assignNode4.CreateAssignment("SheetRec.CreatedOn", "CurrDateTime()");
    ifNode11.TrueTarget = assignNode4;
    
    var assignNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode4, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment8 = assignNode5.CreateAssignment("SheetRec.DayStatusId", "If(IsDayOff, Entities.DayStatus.DayOff, If(Confirm, Entities.DayStatus.Confirmed, Entities.DayStatus.Draft))");
    
    var assignment9 = assignNode5.CreateAssignment("SheetRec.Notes", "Notes");
    
    var assignment10 = assignNode5.CreateAssignment("SheetRec.UpdatedOn", "CurrDateTime()");
    assignNode3.Target = assignNode5;
    
    var createOrUpdateDaySheet = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateOrUpdateDaySheet").ConnectedBelow(assignNode5, 1371);
    createOrUpdateDaySheet.Action = daySheet.CreateOrUpdateAction;
    createOrUpdateDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createOrUpdateDaySheet.IsDisabled = false; // default value
    createOrUpdateDaySheet.ServerRequestTimeout = null; // default value
    var createOrUpdateDaySheet2 = daySheet.CreateOrUpdateAction;
    var source = createOrUpdateDaySheet2.InputParameters.Named("Source");
    createOrUpdateDaySheet.SetArgumentValue(source, "SheetRec");
    
    var assignNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(createOrUpdateDaySheet, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    
    var assignment11 = assignNode6.CreateAssignment("SheetId", "CreateOrUpdateDaySheet.Id");
    
    var getSheetEntries = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSheetEntries").ConnectedBelow(assignNode6, 1371);
    getSheetEntries.CacheInMinutes = null; // default value
    getSheetEntries.Description = null; // default value
    getSheetEntries.IsDisabled = false; // default value
    getSheetEntries.SetMaxRecords(null); // default value
    getSheetEntries.SetStartIndex(null); // default value
    getSheetEntries.Timeout = null; // default value
    var timeEntry2 = getSheetEntries.AsDatabaseAggregate.CreateSource(timeEntry);
    getSheetEntries.AsDatabaseAggregate.CreateFilter("TimeEntry.DaySheetId = SheetId");
    
    
    var forEachNode3 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getSheetEntries, 1371);
    forEachNode3.IsDisabled = false; // default value
    forEachNode3.Label = null; // default value
    forEachNode3.SetMaximumIterations(null); // default value
    forEachNode3.SetRecordList("GetSheetEntries.List");
    forEachNode3.SetStartIndex(null); // default value
    
    var assignNode7 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode3, 3656);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    
    var assignment12 = assignNode7.CreateAssignment("Found", "False");
    forEachNode3.CycleTarget = assignNode7;
    
    var ifNode12 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode3, 8226);
    ifNode12.SetCondition("not IsDayOff");
    ifNode12.IsDisabled = false; // default value
    ifNode12.Label = "Work day?";
    forEachNode3.Target = ifNode12;
    
    var forEachNode4 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode7, 1371);
    forEachNode4.IsDisabled = false; // default value
    forEachNode4.Label = null; // default value
    forEachNode4.SetMaximumIterations(null); // default value
    forEachNode4.SetRecordList("Entries");
    forEachNode4.SetStartIndex(null); // default value
    
    var endNode = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode12, 1828);
    endNode.IsDisabled = false; // default value
    ifNode12.FalseTarget = endNode;
    
    var forEachNode5 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().Below(ifNode12, 1371);
    forEachNode5.IsDisabled = false; // default value
    forEachNode5.Label = null; // default value
    forEachNode5.SetMaximumIterations(null); // default value
    forEachNode5.SetRecordList("Entries");
    forEachNode5.SetStartIndex(null); // default value
    ifNode12.TrueTarget = forEachNode5;
    
    var ifNode13 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode4, 1828);
    ifNode13.SetCondition("not IsDayOff and Entries.Current.ProjectId = GetSheetEntries.List.Current.TimeEntry.ProjectId and Entries.Current.Minutes > 0");
    ifNode13.FalseTarget = forEachNode4;
    ifNode13.IsDisabled = false; // default value
    ifNode13.Label = "!Still sent?";
    forEachNode4.CycleTarget = ifNode13;
    
    var ifNode14 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode4, 4113);
    ifNode14.SetCondition("not Found");
    ifNode14.FalseTarget = forEachNode3;
    ifNode14.IsDisabled = false; // default value
    ifNode14.Label = "!Not sent anymore?";
    forEachNode4.Target = ifNode14;
    
    var ifNode15 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode5, 3656);
    ifNode15.SetCondition("Entries.Current.Minutes > 0");
    ifNode15.FalseTarget = forEachNode5;
    ifNode15.IsDisabled = false; // default value
    ifNode15.Label = "!Has minutes?";
    forEachNode5.CycleTarget = ifNode15;
    
    var endNode2 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode5, 9597);
    endNode2.IsDisabled = false; // default value
    forEachNode5.Target = endNode2;
    
    var assignNode8 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode13, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    
    var assignment13 = assignNode8.CreateAssignment("EntryRec", "GetSheetEntries.List.Current.TimeEntry");
    
    var assignment14 = assignNode8.CreateAssignment("EntryRec.Minutes", "Entries.Current.Minutes");
    
    var assignment15 = assignNode8.CreateAssignment("EntryRec.Description", "Entries.Current.Description");
    
    var assignment16 = assignNode8.CreateAssignment("EntryRec.UpdatedOn", "CurrDateTime()");
    
    var assignment17 = assignNode8.CreateAssignment("Found", "True");
    ifNode13.TrueTarget = assignNode8;
    
    var deleteTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteTimeEntry").Below(ifNode14, 1371);
    deleteTimeEntry.Action = timeEntry.DeleteAction;
    deleteTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteTimeEntry.IsDisabled = false; // default value
    deleteTimeEntry.ServerRequestTimeout = null; // default value
    deleteTimeEntry.Target = forEachNode3;
    var deleteTimeEntry2 = timeEntry.DeleteAction;
    var id = deleteTimeEntry2.InputParameters.Named("Id");
    deleteTimeEntry.SetArgumentValue(id, "GetSheetEntries.List.Current.TimeEntry.Id");
    ifNode14.TrueTarget = deleteTimeEntry;
    
    var assignNode9 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode15, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    
    var assignment18 = assignNode9.CreateAssignment("Found", "False");
    ifNode15.TrueTarget = assignNode9;
    
    var updateTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateTimeEntry").ConnectedBelow(assignNode8, 1371);
    updateTimeEntry.Action = timeEntry.UpdateAction;
    updateTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateTimeEntry.IsDisabled = false; // default value
    updateTimeEntry.ServerRequestTimeout = null; // default value
    updateTimeEntry.Target = forEachNode4;
    var updateTimeEntry2 = timeEntry.UpdateAction;
    var source2 = updateTimeEntry2.InputParameters.Named("Source");
    updateTimeEntry.SetArgumentValue(source2, "EntryRec");
    
    var forEachNode6 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode9, 1371);
    forEachNode6.IsDisabled = false; // default value
    forEachNode6.Label = null; // default value
    forEachNode6.SetMaximumIterations(null); // default value
    forEachNode6.SetRecordList("GetSheetEntries.List");
    forEachNode6.SetStartIndex(null); // default value
    
    var ifNode16 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode6, 1828);
    ifNode16.SetCondition("GetSheetEntries.List.Current.TimeEntry.ProjectId = Entries.Current.ProjectId");
    ifNode16.FalseTarget = forEachNode6;
    ifNode16.IsDisabled = false; // default value
    ifNode16.Label = "!Already saved?";
    forEachNode6.CycleTarget = ifNode16;
    
    var ifNode17 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(forEachNode6, 2742);
    ifNode17.SetCondition("not Found");
    ifNode17.FalseTarget = forEachNode5;
    ifNode17.IsDisabled = false; // default value
    ifNode17.Label = "!New entry?";
    forEachNode6.Target = ifNode17;
    
    var assignNode10 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode16, 1371);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = null; // default value
    assignNode10.Target = forEachNode6;
    
    var assignment19 = assignNode10.CreateAssignment("Found", "True");
    ifNode16.TrueTarget = assignNode10;
    
    var assignNode11 = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode17, 1371);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = null; // default value
    
    var assignment20 = assignNode11.CreateAssignment("EntryRec.Id", "NullIdentifier()");
    
    var assignment21 = assignNode11.CreateAssignment("EntryRec.DaySheetId", "SheetId");
    
    var assignment22 = assignNode11.CreateAssignment("EntryRec.ProjectId", "Entries.Current.ProjectId");
    
    var assignment23 = assignNode11.CreateAssignment("EntryRec.Minutes", "Entries.Current.Minutes");
    
    var assignment24 = assignNode11.CreateAssignment("EntryRec.Description", "Entries.Current.Description");
    
    var assignment25 = assignNode11.CreateAssignment("EntryRec.CreatedOn", "CurrDateTime()");
    
    var assignment26 = assignNode11.CreateAssignment("EntryRec.UpdatedOn", "CurrDateTime()");
    ifNode17.TrueTarget = assignNode11;
    
    var createTimeEntry = daySheet_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("CreateTimeEntry").ConnectedBelow(assignNode11, 1371);
    createTimeEntry.Action = timeEntry.CreateAction;
    createTimeEntry.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    createTimeEntry.IsDisabled = false; // default value
    createTimeEntry.ServerRequestTimeout = null; // default value
    createTimeEntry.Target = forEachNode5;
    var createTimeEntry2 = timeEntry.CreateAction;
    var source3 = createTimeEntry2.InputParameters.Named("Source");
    createTimeEntry.SetArgumentValue(source3, "EntryRec");
    daySheet_Save.Public = true;
  }
  { // DaySheet_Clear
    
    /*** creating IServerAction 'DaySheet_Clear' ***/
    var daySheet_Clear = eSpace.CreateServerAction("DaySheet_Clear");
    daySheet_Clear.CacheInMinutes = null; // default value
    daySheet_Clear.Description = "Deletes the current user\'s DaySheet for Date (its TimeEntries are deleted by the Delete rule). The day goes back to Pending or Future.";
    daySheet_Clear.Folder = null; // default value
    daySheet_Clear.Function = false; // default value
    daySheet_Clear.Icon = null; // default value
    daySheet_Clear.Public = false; // default value (set to true at the end)
    
    var date = daySheet_Clear.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "!Is SheetUser?";
    
    var raiseExceptionNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var getDayRecord = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetDayRecord").Below(ifNode, 1371);
    getDayRecord.CacheInMinutes = null; // default value
    getDayRecord.Description = null; // default value
    getDayRecord.IsDisabled = false; // default value
    getDayRecord.SetMaxRecords(null); // default value
    getDayRecord.SetStartIndex(null); // default value
    getDayRecord.Timeout = null; // default value
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getDayRecord.AsDatabaseAggregate.CreateSource(daySheet2);
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getDayRecord.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate = Date");
    
    ifNode.TrueTarget = getDayRecord;
    
    var ifNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(getDayRecord, 1371);
    ifNode2.SetCondition("not GetDayRecord.List.Empty");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Has day sheet?";
    
    var endNode = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1828);
    endNode.IsDisabled = false; // default value
    ifNode2.FalseTarget = endNode;
    
    var deleteDaySheet = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DeleteDaySheet").Below(ifNode2, 1371);
    deleteDaySheet.Action = daySheet2.DeleteAction;
    deleteDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    deleteDaySheet.IsDisabled = false; // default value
    deleteDaySheet.ServerRequestTimeout = null; // default value
    var deleteDaySheet2 = daySheet2.DeleteAction;
    var id = deleteDaySheet2.InputParameters.Named("Id");
    deleteDaySheet.SetArgumentValue(id, "GetDayRecord.List.Current.DaySheet.Id");
    ifNode2.TrueTarget = deleteDaySheet;
    
    var endNode2 = daySheet_Clear.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(deleteDaySheet, 1371);
    endNode2.IsDisabled = false; // default value
    daySheet_Clear.Public = true;
  }
  { // DaySheet_FillPending
    
    /*** creating IServerAction 'DaySheet_FillPending' ***/
    var daySheet_FillPending = eSpace.CreateServerAction("DaySheet_FillPending");
    daySheet_FillPending.CacheInMinutes = null; // default value
    daySheet_FillPending.Description = "Fills and confirms every Pending day of the month for the current user: a Draft with a total > 0 is confirmed as is; any other Pending day is saved through SaveDaySheet with one TimeEntry (ProjectId, MinutesPerDay) and confirmed. Runs in one request, so any error rolls back every day. Returns the number of days filled.";
    daySheet_FillPending.Folder = null; // default value
    daySheet_FillPending.Function = false; // default value
    daySheet_FillPending.Icon = null; // default value
    daySheet_FillPending.Public = false; // default value (set to true at the end)
    
    var year = daySheet_FillPending.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = daySheet_FillPending.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var projectId = daySheet_FillPending.CreateInputParameter("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
    projectId.Description = null; // default value
    projectId.IsMandatory = true; // default value
    
    var minutesPerDay = daySheet_FillPending.CreateInputParameter("MinutesPerDay");
    minutesPerDay.DataType = eSpace.IntegerType;
    minutesPerDay.Description = null; // default value
    minutesPerDay.IsMandatory = true; // default value
    
    var entry = daySheet_FillPending.CreateLocalVariable("Entry");
    var timeEntryInput = eSpace.Structures.Named("TimeEntryInput");
    entry.DataType = timeEntryInput;
    entry.Description = null; // default value
    
    var oneEntry = daySheet_FillPending.CreateLocalVariable("OneEntry");
    oneEntry.DataType = eSpace.GetOrCreateListType(timeEntryInput);
    oneEntry.Description = null; // default value
    
    var sheetRec = daySheet_FillPending.CreateLocalVariable("SheetRec");
    var daySheet = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    sheetRec.DataType = daySheet;
    sheetRec.Description = null; // default value
    
    var startNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var calendar_GetMonth = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Calendar_GetMonth").Below(ifNode, 1371);
    var calendar_GetMonth2 = eSpace.ServerActions.Named("Calendar_GetMonth");
    calendar_GetMonth.Action = calendar_GetMonth2;
    calendar_GetMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    calendar_GetMonth.IsDisabled = false; // default value
    calendar_GetMonth.ServerRequestTimeout = null; // default value
    var month2 = calendar_GetMonth2.InputParameters.Named("Month");
    calendar_GetMonth.SetArgumentValue(month2, "Month");
    var year2 = calendar_GetMonth2.InputParameters.Named("Year");
    calendar_GetMonth.SetArgumentValue(year2, "Year");
    ifNode.TrueTarget = calendar_GetMonth;
    
    var assignNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(calendar_GetMonth, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Entry.ProjectId", "ProjectId");
    
    var assignment2 = assignNode.CreateAssignment("Entry.Minutes", "MinutesPerDay");
    
    var assignment3 = assignNode.CreateAssignment("FilledCount", "0");
    
    var listAppend = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode, 1371);
    var _System_ = eSpace.References.Named("(System)");
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "OneEntry");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "Entry");
    
    var forEachNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(listAppend, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("Calendar_GetMonth.Days");
    forEachNode.SetStartIndex(null); // default value
    
    var ifNode2 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode2.SetCondition("Calendar_GetMonth.Days.Current.DayTypeId = Entities.CalendarDayType.Pending");
    ifNode2.FalseTarget = forEachNode;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "!Pending?";
    forEachNode.CycleTarget = ifNode2;
    
    var endNode = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 8226);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var ifNode3 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode2, 1371);
    ifNode3.SetCondition("Calendar_GetMonth.Days.Current.DaySheetId <> NullIdentifier() and Calendar_GetMonth.Days.Current.TotalMinutes > 0");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Draft with hours?";
    ifNode2.TrueTarget = ifNode3;
    
    var daySheet_Save = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DaySheet_Save").ToTheRightOf(ifNode3, 1828);
    var daySheet_Save2 = eSpace.ServerActions.Named("DaySheet_Save");
    daySheet_Save.Action = daySheet_Save2;
    daySheet_Save.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    daySheet_Save.IsDisabled = false; // default value
    daySheet_Save.ServerRequestTimeout = null; // default value
    var entries = daySheet_Save2.InputParameters.Named("Entries");
    daySheet_Save.SetArgumentValue(entries, "OneEntry");
    var isDayOff = daySheet_Save2.InputParameters.Named("IsDayOff");
    daySheet_Save.SetArgumentValue(isDayOff, "False");
    var notes = daySheet_Save2.InputParameters.Named("Notes");
    daySheet_Save.SetArgumentValue(notes, "\"\"");
    var confirm = daySheet_Save2.InputParameters.Named("Confirm");
    daySheet_Save.SetArgumentValue(confirm, "True");
    var date = daySheet_Save2.InputParameters.Named("Date");
    daySheet_Save.SetArgumentValue(date, "Calendar_GetMonth.Days.Current.Date");
    ifNode3.FalseTarget = daySheet_Save;
    
    var getDaySheetForUpdate = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetDaySheetForUpdate").Below(ifNode3, 1371);
    getDaySheetForUpdate.Action = daySheet.GetForUpdateAction;
    getDaySheetForUpdate.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getDaySheetForUpdate.IsDisabled = false; // default value
    getDaySheetForUpdate.ServerRequestTimeout = null; // default value
    var getDaySheetForUpdate2 = daySheet.GetForUpdateAction;
    var id = getDaySheetForUpdate2.InputParameters.Named("Id");
    getDaySheetForUpdate.SetArgumentValue(id, "Calendar_GetMonth.Days.Current.DaySheetId");
    ifNode3.TrueTarget = getDaySheetForUpdate;
    
    var assignNode2 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(daySheet_Save, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.Target = forEachNode;
    
    var assignment4 = assignNode2.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var assignNode3 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getDaySheetForUpdate, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment5 = assignNode3.CreateAssignment("SheetRec", "GetDaySheetForUpdate.Record");
    
    var assignment6 = assignNode3.CreateAssignment("SheetRec.DayStatusId", "Entities.DayStatus.Confirmed");
    
    var assignment7 = assignNode3.CreateAssignment("SheetRec.UpdatedOn", "CurrDateTime()");
    
    var updateDaySheet = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("UpdateDaySheet").ConnectedBelow(assignNode3, 1371);
    updateDaySheet.Action = daySheet.UpdateAction;
    updateDaySheet.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    updateDaySheet.IsDisabled = false; // default value
    updateDaySheet.ServerRequestTimeout = null; // default value
    var updateDaySheet2 = daySheet.UpdateAction;
    var source = updateDaySheet2.InputParameters.Named("Source");
    updateDaySheet.SetArgumentValue(source, "SheetRec");
    
    var assignNode4 = daySheet_FillPending.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(updateDaySheet, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.Target = forEachNode;
    
    var assignment8 = assignNode4.CreateAssignment("FilledCount", "FilledCount + 1");
    
    var filledCount = daySheet_FillPending.CreateOutputParameter("FilledCount");
    filledCount.DataType = eSpace.IntegerType;
    filledCount.Description = null; // default value
    daySheet_FillPending.Public = true;
  }
  { // ProjectHours_GetWeekly
    
    /*** creating IServerAction 'ProjectHours_GetWeekly' ***/
    var projectHours_GetWeekly = eSpace.CreateServerAction("ProjectHours_GetWeekly");
    projectHours_GetWeekly.CacheInMinutes = null; // default value
    projectHours_GetWeekly.Description = "Weekly grid for the current user: the Monday-Sunday week containing ReferenceDate, day headers classified like the calendar (Calendar_GetMonth), one row per project assigned during the week or with entries in it (sorted by client, project), 7 cells per row with the \"-\" rule, totals per row, per day and for the week.";
    projectHours_GetWeekly.Folder = null; // default value
    projectHours_GetWeekly.Function = false; // default value
    projectHours_GetWeekly.Icon = null; // default value
    projectHours_GetWeekly.Public = false; // default value (set to true at the end)
    
    var referenceDate = projectHours_GetWeekly.CreateInputParameter("ReferenceDate");
    referenceDate.DataType = eSpace.DateType;
    referenceDate.Description = null; // default value
    referenceDate.IsMandatory = true; // default value
    
    var dayIndex = projectHours_GetWeekly.CreateLocalVariable("DayIndex");
    dayIndex.DataType = eSpace.IntegerType;
    dayIndex.Description = null; // default value
    
    var dayHeader = projectHours_GetWeekly.CreateLocalVariable("DayHeader");
    var weekDayHeader = eSpace.Structures.Named("WeekDayHeader");
    dayHeader.DataType = weekDayHeader;
    dayHeader.Description = null; // default value
    
    var row = projectHours_GetWeekly.CreateLocalVariable("Row");
    var weeklyProjectRow = eSpace.Structures.Named("WeeklyProjectRow");
    row.DataType = weeklyProjectRow;
    row.Description = null; // default value
    
    var cell = projectHours_GetWeekly.CreateLocalVariable("Cell");
    var weeklyCell = eSpace.Structures.Named("WeeklyCell");
    cell.DataType = weeklyCell;
    cell.Description = null; // default value
    
    var startNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var ifNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode, 1371);
    ifNode.SetCondition("CheckSheetUserRole()");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Is SheetUser?";
    
    var raiseExceptionNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IRaiseExceptionNode>().ToTheRightOf(ifNode, 1828);
    var roleException = eSpace.RoleExceptions.Single(e => e.Role.Name == "SheetUser");
    raiseExceptionNode.Exception = roleException;
    raiseExceptionNode.SetExceptionMessage("\"You are not allowed to use MySheet.\"");
    raiseExceptionNode.IsDisabled = false; // default value
    ifNode.FalseTarget = raiseExceptionNode;
    
    var assignNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Week bounds";
    
    var assignment = assignNode.CreateAssignment("Week.WeekStart", "Date_GetWeekStart(ReferenceDate)");
    
    var assignment2 = assignNode.CreateAssignment("Week.WeekEnd", "DateTimeToDate(AddDays(Week.WeekStart, 6))");
    ifNode.TrueTarget = assignNode;
    
    var startMonth = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("StartMonth").ConnectedBelow(assignNode, 1371);
    var calendar_GetMonth = eSpace.ServerActions.Named("Calendar_GetMonth");
    startMonth.Action = calendar_GetMonth;
    startMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    startMonth.IsDisabled = false; // default value
    startMonth.ServerRequestTimeout = null; // default value
    var year = calendar_GetMonth.InputParameters.Named("Year");
    startMonth.SetArgumentValue(year, "Year(Week.WeekStart)");
    var month = calendar_GetMonth.InputParameters.Named("Month");
    startMonth.SetArgumentValue(month, "Month(Week.WeekStart)");
    
    var endMonth = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("EndMonth").ConnectedBelow(startMonth, 1371);
    endMonth.Action = calendar_GetMonth;
    endMonth.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    endMonth.IsDisabled = false; // default value
    endMonth.ServerRequestTimeout = null; // default value
    endMonth.SetArgumentValue(month, "Month(Week.WeekEnd)");
    endMonth.SetArgumentValue(year, "Year(Week.WeekEnd)");
    
    var assignNode2 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(endMonth, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment3 = assignNode2.CreateAssignment("DayIndex", "0");
    
    var ifNode2 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode2, 1371);
    ifNode2.SetCondition("DayIndex < 7");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "More days?";
    
    var getWeekProjects = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetWeekProjects").Below(ifNode2, 5484);
    getWeekProjects.CacheInMinutes = null; // default value
    getWeekProjects.Description = null; // default value
    getWeekProjects.IsDisabled = false; // default value
    getWeekProjects.SetMaxRecords(null); // default value
    getWeekProjects.Statement = "SELECT {Project}.*, {Client}.*\nFROM {Project}\nINNER JOIN {Client} ON {Client}.[Id] = {Project}.[ClientId]\nWHERE {Project}.[Id] IN (\n    SELECT {ProjectAssignment}.[ProjectId] FROM {ProjectAssignment}\n    WHERE {ProjectAssignment}.[UserId] = @UserId AND {ProjectAssignment}.[IsActive] = 1\n      AND {ProjectAssignment}.[StartDate] <= @WeekEnd\n      AND ({ProjectAssignment}.[EndDate] = @NullDate OR {ProjectAssignment}.[EndDate] IS NULL OR {ProjectAssignment}.[EndDate] >= @WeekStart)\n      AND {Project}.[IsActive] = 1 AND {Client}.[IsActive] = 1)\nOR {Project}.[Id] IN (\n    SELECT {TimeEntry}.[ProjectId] FROM {TimeEntry}\n    INNER JOIN {DaySheet} ON {DaySheet}.[Id] = {TimeEntry}.[DaySheetId]\n    WHERE {DaySheet}.[UserId] = @UserId AND {DaySheet}.[SheetDate] >= @WeekStart AND {DaySheet}.[SheetDate] <= @WeekEnd)\nORDER BY {Client}.[Name], {Project}.[Name]";
    getWeekProjects.Timeout = null; // default value
    
    var userId = getWeekProjects.CreateInputParameter("UserId");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userId.DataType = user.IdentifierType;
    userId.Description = null; // default value
    userId.ExpandInline = false; // default value
    
    var weekStart = getWeekProjects.CreateInputParameter("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.Description = null; // default value
    weekStart.ExpandInline = false; // default value
    
    var weekEnd = getWeekProjects.CreateInputParameter("WeekEnd");
    weekEnd.DataType = eSpace.DateType;
    weekEnd.Description = null; // default value
    weekEnd.ExpandInline = false; // default value
    
    var nullDate = getWeekProjects.CreateInputParameter("NullDate");
    nullDate.DataType = eSpace.DateType;
    nullDate.Description = null; // default value
    nullDate.ExpandInline = false; // default value
    
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    var project2 = getWeekProjects.CreateOutput(project, "Project");
    
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    var client2 = getWeekProjects.CreateOutput(client, "Client");
    ifNode2.FalseTarget = getWeekProjects;
    
    var assignNode3 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Build day header";
    
    var assignment4 = assignNode3.CreateAssignment("DayHeader.Date", "DateTimeToDate(AddDays(Week.WeekStart, DayIndex))");
    
    var assignment5 = assignNode3.CreateAssignment("DayHeader.DayName", "If(DayOfWeek(DayHeader.Date) = 1, \"Monday\", If(DayOfWeek(DayHeader.Date) = 2, \"Tuesday\", If(DayOfWeek(DayHeader.Date) = 3, \"Wednesday\", If(DayOfWeek(DayHeader.Date) = 4, \"Thursday\", If(DayOfWeek(DayHeader.Date) = 5, \"Friday\", If(DayOfWeek(DayHeader.Date) = 6, \"Saturday\", \"Sunday\"))))))");
    
    var assignment6 = assignNode3.CreateAssignment("DayHeader.DateLabel", "FormatDateTime(DayHeader.Date, \"dd/MM\")");
    
    var assignment7 = assignNode3.CreateAssignment("DayHeader.IsWeekend", "DayOfWeek(DayHeader.Date) = 0 or DayOfWeek(DayHeader.Date) = 6");
    
    var assignment8 = assignNode3.CreateAssignment("DayHeader.IsToday", "DayHeader.Date = CurrDate()");
    
    var assignment9 = assignNode3.CreateAssignment("DayHeader.DayTypeId", "If(Month(DayHeader.Date) = Month(Week.WeekStart), StartMonth.Days[Day(DayHeader.Date) - 1].DayTypeId, EndMonth.Days[Day(DayHeader.Date) - 1].DayTypeId)");
    
    var assignment10 = assignNode3.CreateAssignment("DayHeader.CssClass", "If(Month(DayHeader.Date) = Month(Week.WeekStart), StartMonth.Days[Day(DayHeader.Date) - 1].CssClass, EndMonth.Days[Day(DayHeader.Date) - 1].CssClass)");
    
    var assignment11 = assignNode3.CreateAssignment("DayHeader.IsEditable", "If(Month(DayHeader.Date) = Month(Week.WeekStart), StartMonth.Days[Day(DayHeader.Date) - 1].IsEditable, EndMonth.Days[Day(DayHeader.Date) - 1].IsEditable)");
    
    var assignment12 = assignNode3.CreateAssignment("Cell.Date", "DayHeader.Date");
    
    var assignment13 = assignNode3.CreateAssignment("Cell.Minutes", "0");
    
    var assignment14 = assignNode3.CreateAssignment("Cell.ShowDash", "True");
    ifNode2.TrueTarget = assignNode3;
    
    var getWeekEntries = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetWeekEntries").ConnectedBelow(getWeekProjects, 1371);
    getWeekEntries.CacheInMinutes = null; // default value
    getWeekEntries.Description = null; // default value
    getWeekEntries.IsDisabled = false; // default value
    getWeekEntries.SetMaxRecords(null); // default value
    getWeekEntries.SetStartIndex(null); // default value
    getWeekEntries.Timeout = null; // default value
    var timeEntry2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("TimeEntry");
    var timeEntry = getWeekEntries.AsDatabaseAggregate.CreateSource(timeEntry2);
    var daySheet2 = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("DaySheet");
    var daySheet = getWeekEntries.AsDatabaseAggregate.CreateSource(daySheet2);
    var join = getWeekEntries.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = timeEntry;
    join.RightSource = daySheet;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("TimeEntry.DaySheetId = DaySheet.Id");
    getWeekEntries.AsDatabaseAggregate.CreateFilter("DaySheet.UserId = GetUserId()");
    getWeekEntries.AsDatabaseAggregate.CreateFilter("DaySheet.SheetDate >= Week.WeekStart and DaySheet.SheetDate <= Week.WeekEnd");
    getWeekEntries.AsDatabaseAggregate.CreateGroupByAttribute("ProjectId").SetAttribute("TimeEntry.ProjectId");
    getWeekEntries.AsDatabaseAggregate.CreateGroupByAttribute("SheetDate").SetAttribute("DaySheet.SheetDate");
    var minutes = getWeekEntries.AsDatabaseAggregate.CreateAggregatedAttribute("Minutes");
    minutes.SetAttribute("TimeEntry.Minutes");
    minutes.AggregationType = AggregationType.Sum;
    
    getWeekProjects.SetArgumentValue(weekStart, "Week.WeekStart");
    getWeekProjects.SetArgumentValue(nullDate, "NullDate()");
    getWeekProjects.SetArgumentValue(weekEnd, "Week.WeekEnd");
    getWeekProjects.SetArgumentValue(userId, "GetUserId()");
    
    var listAppend = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend").ConnectedBelow(assignNode3, 1371);
    var listAppend2 = _System_.ServerActions.Named("ListAppend");
    listAppend.Action = listAppend2;
    listAppend.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend.IsDisabled = false; // default value
    listAppend.ServerRequestTimeout = null; // default value
    var list = listAppend2.InputParameters.Named("List");
    listAppend.SetArgumentValue(list, "Week.Days");
    var element = listAppend2.InputParameters.Named("Element");
    listAppend.SetArgumentValue(element, "DayHeader");
    
    var forEachNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(getWeekEntries, 1371);
    forEachNode.IsDisabled = false; // default value
    forEachNode.Label = null; // default value
    forEachNode.SetMaximumIterations(null); // default value
    forEachNode.SetRecordList("GetWeekProjects.List");
    forEachNode.SetStartIndex(null); // default value
    
    var listAppend22 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend2").ConnectedBelow(listAppend, 1371);
    listAppend22.Action = listAppend2;
    listAppend22.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend22.IsDisabled = false; // default value
    listAppend22.ServerRequestTimeout = null; // default value
    listAppend22.SetArgumentValue(list, "Week.DayTotals");
    listAppend22.SetArgumentValue(element, "Cell");
    
    var assignNode4 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(forEachNode, 5484);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Start row";
    
    var assignment15 = assignNode4.CreateAssignment("Row.ProjectId", "GetWeekProjects.List.Current.Project.Id");
    
    var assignment16 = assignNode4.CreateAssignment("Row.ClientName", "GetWeekProjects.List.Current.Client.Name");
    
    var assignment17 = assignNode4.CreateAssignment("Row.ProjectName", "GetWeekProjects.List.Current.Project.Name");
    
    var assignment18 = assignNode4.CreateAssignment("Row.ProjectCode", "GetWeekProjects.List.Current.Project.Code");
    
    var assignment19 = assignNode4.CreateAssignment("Row.TotalMinutes", "0");
    
    var assignment20 = assignNode4.CreateAssignment("DayIndex", "0");
    forEachNode.CycleTarget = assignNode4;
    
    var endNode = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 12339);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var assignNode5 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend22, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    assignNode5.Target = ifNode2;
    
    var assignment21 = assignNode5.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var listClear = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListClear").ConnectedBelow(assignNode4, 1371);
    var listClear2 = _System_.ServerActions.Named("ListClear");
    listClear.Action = listClear2;
    listClear.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listClear.IsDisabled = false; // default value
    listClear.ServerRequestTimeout = null; // default value
    var list2 = listClear2.InputParameters.Named("List");
    listClear.SetArgumentValue(list2, "Row.Cells");
    
    var ifNode3 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(listClear, 1371);
    ifNode3.SetCondition("DayIndex < 7");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "More cells?";
    
    var listAppend4 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend4").Below(ifNode3, 8226);
    listAppend4.Action = listAppend2;
    listAppend4.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend4.IsDisabled = false; // default value
    listAppend4.ServerRequestTimeout = null; // default value
    listAppend4.Target = forEachNode;
    listAppend4.SetArgumentValue(list, "Week.Rows");
    listAppend4.SetArgumentValue(element, "Row");
    ifNode3.FalseTarget = listAppend4;
    
    var assignNode6 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 3656);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Start cell";
    
    var assignment22 = assignNode6.CreateAssignment("Cell.Date", "Week.Days[DayIndex].Date");
    
    var assignment23 = assignNode6.CreateAssignment("Cell.Minutes", "0");
    ifNode3.TrueTarget = assignNode6;
    
    var forEachNode2 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IForEachNode>().ConnectedBelow(assignNode6, 1371);
    forEachNode2.IsDisabled = false; // default value
    forEachNode2.Label = null; // default value
    forEachNode2.SetMaximumIterations(null); // default value
    forEachNode2.SetRecordList("GetWeekEntries.List");
    forEachNode2.SetStartIndex(null); // default value
    
    var ifNode4 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode2, 1828);
    ifNode4.SetCondition("GetWeekEntries.List.Current.ProjectId = Row.ProjectId and GetWeekEntries.List.Current.SheetDate = Cell.Date");
    ifNode4.FalseTarget = forEachNode2;
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Same project and day?";
    forEachNode2.CycleTarget = ifNode4;
    
    var assignNode7 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(forEachNode2, 2742);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "Dash rule and totals";
    
    var assignment24 = assignNode7.CreateAssignment("Cell.ShowDash", "Cell.Minutes = 0 and (Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Future or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Weekend or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.Holiday or Week.Days[DayIndex].DayTypeId = Entities.CalendarDayType.DayOff)");
    
    var assignment25 = assignNode7.CreateAssignment("Row.TotalMinutes", "Row.TotalMinutes + Cell.Minutes");
    
    var assignment26 = assignNode7.CreateAssignment("Week.DayTotals[DayIndex].Minutes", "Week.DayTotals[DayIndex].Minutes + Cell.Minutes");
    
    var assignment27 = assignNode7.CreateAssignment("Week.DayTotals[DayIndex].ShowDash", "Week.DayTotals[DayIndex].ShowDash and Cell.ShowDash");
    
    var assignment28 = assignNode7.CreateAssignment("Week.GrandTotalMinutes", "Week.GrandTotalMinutes + Cell.Minutes");
    forEachNode2.Target = assignNode7;
    
    var assignNode8 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode4, 1371);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = null; // default value
    assignNode8.Target = forEachNode2;
    
    var assignment29 = assignNode8.CreateAssignment("Cell.Minutes", "Cell.Minutes + LongIntegerToInteger(GetWeekEntries.List.Current.Minutes)");
    ifNode4.TrueTarget = assignNode8;
    
    var listAppend3 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppend3").ConnectedBelow(assignNode7, 1371);
    listAppend3.Action = listAppend2;
    listAppend3.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppend3.IsDisabled = false; // default value
    listAppend3.ServerRequestTimeout = null; // default value
    listAppend3.SetArgumentValue(list, "Row.Cells");
    listAppend3.SetArgumentValue(element, "Cell");
    
    var assignNode9 = projectHours_GetWeekly.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppend3, 1371);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    assignNode9.Target = ifNode3;
    
    var assignment30 = assignNode9.CreateAssignment("DayIndex", "DayIndex + 1");
    
    var week = projectHours_GetWeekly.CreateOutputParameter("Week");
    var weeklyHours = eSpace.Structures.Named("WeeklyHours");
    week.DataType = weeklyHours;
    week.Description = null; // default value
    projectHours_GetWeekly.Public = true;
  }
  { // Bootstrap
    
    /*** creating IServerAction 'Bootstrap' ***/
    var bootstrap = eSpace.CreateServerAction("Bootstrap");
    bootstrap.CacheInMinutes = null; // default value
    bootstrap.Description = "Seeds reference data when missing: client ICNF, project RUBUS, Portuguese 2026 holidays, and SheetUser + RUBUS assignment for the users in Site.SeedTestUsernames. Safe to run more than once. Run by Timer_Bootstrap.";
    bootstrap.Folder = null; // default value
    bootstrap.Function = false; // default value
    bootstrap.Icon = null; // default value
    bootstrap.Public = false; // default value
    
    var clientId = bootstrap.CreateLocalVariable("ClientId");
    var client = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Client");
    clientId.DataType = client.IdentifierType;
    clientId.Description = null; // default value
    
    var projectId = bootstrap.CreateLocalVariable("ProjectId");
    var project = eSpace.Entities.OfType<OutSystems.Model.Data.IServerEntity>().Named("Project");
    projectId.DataType = project.IdentifierType;
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
    
    var assignNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1828);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("ClientId", "GetSeedClient.List.Current.Client.Id");
    ifNode.FalseTarget = assignNode;
    
    var assignNode2 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("NewClient.Name", "\"ICNF\"");
    
    var assignment3 = assignNode2.CreateAssignment("NewClient.IsActive", "True");
    ifNode.TrueTarget = assignNode2;
    
    var getSeedProject = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedProject");
    getSeedProject.CacheInMinutes = null; // default value
    getSeedProject.Description = null; // default value
    getSeedProject.HorizontalPosition = 3200;
    getSeedProject.IsDisabled = false; // default value
    getSeedProject.SetMaxRecords(null); // default value
    getSeedProject.SetStartIndex(null); // default value
    getSeedProject.Timeout = null; // default value
    getSeedProject.VerticalPosition = 9026;
    var project2 = getSeedProject.AsDatabaseAggregate.CreateSource(project);
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.ClientId = ClientId");
    getSeedProject.AsDatabaseAggregate.CreateFilter("Project.Code = \"RUBUS\"");
    
    assignNode.Target = getSeedProject;
    
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
    
    var assignNode4 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode2, 1828);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
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
    
    var holiday_CreateIfMissing = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Holiday_CreateIfMissing");
    var holiday_CreateIfMissing2 = eSpace.ServerActions.Named("Holiday_CreateIfMissing");
    holiday_CreateIfMissing.Action = holiday_CreateIfMissing2;
    holiday_CreateIfMissing.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    holiday_CreateIfMissing.HorizontalPosition = 3200;
    holiday_CreateIfMissing.IsDisabled = false; // default value
    holiday_CreateIfMissing.ServerRequestTimeout = null; // default value
    holiday_CreateIfMissing.VerticalPosition = 15881;
    var holidayDate = holiday_CreateIfMissing2.InputParameters.Named("HolidayDate");
    holiday_CreateIfMissing.SetArgumentValue(holidayDate, "NewDate(2026, 1, 1)");
    var name = holiday_CreateIfMissing2.InputParameters.Named("Name");
    holiday_CreateIfMissing.SetArgumentValue(name, "\"Ano Novo\"");
    assignNode4.Target = holiday_CreateIfMissing;
    
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
    
    var ifNode3 = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(forEachNode, 3656);
    ifNode3.SetCondition("CheckSheetUserRole(GetSeedUsers.List.Current.User.Id)");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Has SheetUser?";
    forEachNode.CycleTarget = ifNode3;
    
    var endNode = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(forEachNode, 6855);
    endNode.IsDisabled = false; // default value
    forEachNode.Target = endNode;
    
    var grantSheetUserRole = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GrantSheetUserRole").ToTheRightOf(ifNode3, 1828);
    var sheetUser = eSpace.Roles.Named("SheetUser");
    var grantSheetUserRole2 = sheetUser.GrantRoleAction;
    grantSheetUserRole.Action = grantSheetUserRole2;
    grantSheetUserRole.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    grantSheetUserRole.IsDisabled = false; // default value
    grantSheetUserRole.ServerRequestTimeout = null; // default value
    var userId = grantSheetUserRole2.InputParameters.Named("UserId");
    grantSheetUserRole.SetArgumentValue(userId, "GetSeedUsers.List.Current.User.Id");
    ifNode3.FalseTarget = grantSheetUserRole;
    
    var getSeedAssignment = bootstrap.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetSeedAssignment").Below(ifNode3, 1371);
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
    grantSheetUserRole.Target = getSeedAssignment;
    
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
  { // Timer
    
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
}
```
