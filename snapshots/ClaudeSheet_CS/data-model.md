# Data model

Module-wide read in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IReference _System_) => {
    // IServerEntitySignature (System)/Activity
    var eSpace = _System_.GetESpace();
    var activity = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*hg460+gk0UGzMRTFlEBTOQ"));
    
    /*** creating IStaticEntity 'CalendarDayType' ***/
    var calendarDayType = eSpace.CreateStaticEntity("CalendarDayType");
    calendarDayType.Description = "How a calendar day is classified for the current user (first match wins: Holiday, DayOff, Confirmed, Weekend, Future, Pending).";
    calendarDayType.ExposeReadOnly = true;
    calendarDayType.Label = "Calendar Day Type";
    calendarDayType.LabelPlural = "Calendar Day Types";
    calendarDayType.Public = true;
    calendarDayType.Shareable = true;
    calendarDayType.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Optimized;
    calendarDayType.UseTranslations = true;
    
    var id = calendarDayType.CreateAttribute("Id");
    id.DataType = eSpace.IntegerType;
    id.Label = "Id";
    calendarDayType.IdentifierAttribute = id;
    
    var is_Active = calendarDayType.CreateAttribute("Is_Active");
    is_Active.DataType = eSpace.BooleanType;
    is_Active.IsMandatory = true;
    is_Active.Label = "Is Active";
    calendarDayType.IsActiveAttribute = is_Active;
    
    var label = calendarDayType.CreateAttribute("Label");
    label.DataType = eSpace.TextType;
    label.IsMandatory = true;
    label.Label = "Label";
    label.Length = 50;
    calendarDayType.LabelAttribute = label;
    
    var order = calendarDayType.CreateAttribute("Order");
    order.DataType = eSpace.IntegerType;
    order.IsMandatory = true;
    order.Label = "Order";
    calendarDayType.OrderByAttribute = order;
    
    var pluralLabel = calendarDayType.CreateAttribute("PluralLabel");
    pluralLabel.DataType = eSpace.TextType;
    pluralLabel.IsMandatory = true;
    pluralLabel.Label = "Plural Label";
    pluralLabel.Length = 50;
    
    var cssClass = calendarDayType.CreateAttribute("CssClass");
    cssClass.DataType = eSpace.TextType;
    cssClass.IsMandatory = true;
    cssClass.Label = "Css Class";
    cssClass.Length = 50;
    
    var showInKPI = calendarDayType.CreateAttribute("ShowInKPI");
    showInKPI.DataType = eSpace.BooleanType;
    showInKPI.IsMandatory = true;
    showInKPI.Label = "Show In KPI";
    
    var staticEntityRecord = calendarDayType.CreateRecord();
    staticEntityRecord.Identifier = "Future";
    staticEntityRecord.SetAttributeValue(is_Active, "True");
    staticEntityRecord.SetAttributeValue(id, "5");
    staticEntityRecord.SetAttributeValue(pluralLabel, "\"Future days\"");
    staticEntityRecord.SetAttributeValue(showInKPI, "False");
    staticEntityRecord.SetAttributeValue(cssClass, "\"day-future\"");
    staticEntityRecord.SetAttributeValue(label, "\"Future\"");
    staticEntityRecord.SetAttributeValue(order, "5");
    
    var staticEntityRecord2 = calendarDayType.CreateRecord();
    staticEntityRecord2.Identifier = "Holiday";
    staticEntityRecord2.SetAttributeValue(showInKPI, "True");
    staticEntityRecord2.SetAttributeValue(order, "3");
    staticEntityRecord2.SetAttributeValue(pluralLabel, "\"Holidays\"");
    staticEntityRecord2.SetAttributeValue(id, "3");
    staticEntityRecord2.SetAttributeValue(label, "\"Holiday\"");
    staticEntityRecord2.SetAttributeValue(cssClass, "\"day-holiday\"");
    staticEntityRecord2.SetAttributeValue(is_Active, "True");
    
    var staticEntityRecord3 = calendarDayType.CreateRecord();
    staticEntityRecord3.Identifier = "Pending";
    staticEntityRecord3.SetAttributeValue(is_Active, "True");
    staticEntityRecord3.SetAttributeValue(id, "4");
    staticEntityRecord3.SetAttributeValue(label, "\"Pending\"");
    staticEntityRecord3.SetAttributeValue(cssClass, "\"day-pending\"");
    staticEntityRecord3.SetAttributeValue(order, "4");
    staticEntityRecord3.SetAttributeValue(showInKPI, "True");
    staticEntityRecord3.SetAttributeValue(pluralLabel, "\"Pending\"");
    
    var staticEntityRecord4 = calendarDayType.CreateRecord();
    staticEntityRecord4.Identifier = "DayOff";
    staticEntityRecord4.SetAttributeValue(order, "2");
    staticEntityRecord4.SetAttributeValue(id, "2");
    staticEntityRecord4.SetAttributeValue(cssClass, "\"day-off\"");
    staticEntityRecord4.SetAttributeValue(showInKPI, "True");
    staticEntityRecord4.SetAttributeValue(label, "\"Day off\"");
    staticEntityRecord4.SetAttributeValue(pluralLabel, "\"Days off\"");
    staticEntityRecord4.SetAttributeValue(is_Active, "True");
    
    var staticEntityRecord5 = calendarDayType.CreateRecord();
    staticEntityRecord5.Identifier = "Confirmed";
    staticEntityRecord5.SetAttributeValue(is_Active, "True");
    staticEntityRecord5.SetAttributeValue(id, "1");
    staticEntityRecord5.SetAttributeValue(showInKPI, "True");
    staticEntityRecord5.SetAttributeValue(label, "\"Confirmed\"");
    staticEntityRecord5.SetAttributeValue(cssClass, "\"day-confirmed\"");
    staticEntityRecord5.SetAttributeValue(pluralLabel, "\"Confirmed\"");
    staticEntityRecord5.SetAttributeValue(order, "1");
    
    var staticEntityRecord6 = calendarDayType.CreateRecord();
    staticEntityRecord6.Identifier = "Weekend";
    staticEntityRecord6.SetAttributeValue(showInKPI, "False");
    staticEntityRecord6.SetAttributeValue(cssClass, "\"day-weekend\"");
    staticEntityRecord6.SetAttributeValue(order, "6");
    staticEntityRecord6.SetAttributeValue(pluralLabel, "\"Weekend days\"");
    staticEntityRecord6.SetAttributeValue(id, "6");
    staticEntityRecord6.SetAttributeValue(is_Active, "True");
    staticEntityRecord6.SetAttributeValue(label, "\"Weekend\"");
    
    /*** creating IServerEntity 'Client' ***/
    var client = eSpace.CreateServerEntity("Client");
    client.Description = "Customer that projects are billed to.";
    client.Label = "Client";
    client.LabelPlural = "Clients";
    client.Public = true;
    client.Shareable = true;
    
    var id2 = client.CreateAttribute("Id");
    id2.DataType = eSpace.LongIntegerType;
    id2.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id2.Label = "Id";
    client.IdentifierAttribute = id2;
    
    var name = client.CreateAttribute("Name");
    name.DataType = eSpace.TextType;
    name.IsMandatory = true;
    name.Label = "Name";
    name.Length = 100;
    client.LabelAttribute = name;
    
    var isActive = client.CreateAttribute("IsActive");
    isActive.DataType = eSpace.BooleanType;
    isActive.SetDefaultValue("True");
    isActive.IsMandatory = true;
    isActive.Label = "Is Active";
    
    /*** creating IServerEntity 'DaySheet' ***/
    var daySheet = eSpace.CreateServerEntity("DaySheet");
    daySheet.Description = "One user\'s time sheet for one day.";
    daySheet.Label = "Day Sheet";
    daySheet.LabelPlural = "Day Sheets";
    daySheet.Public = true;
    daySheet.Shareable = true;
    
    var id3 = daySheet.CreateAttribute("Id");
    id3.DataType = eSpace.LongIntegerType;
    id3.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id3.Label = "Id";
    daySheet.IdentifierAttribute = id3;
    
    var userId = daySheet.CreateAttribute("UserId");
    userId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    userId.IsMandatory = true;
    userId.Label = "User";
    // IServerEntitySignature (System)/User
    var user = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*LYIWpF+Ey0aicHD144NUTA"));
    userId.DataType = user.IdentifierType;
    
    var sheetDate = daySheet.CreateAttribute("SheetDate");
    sheetDate.DataType = eSpace.DateType;
    sheetDate.IsMandatory = true;
    sheetDate.Label = "Sheet Date";
    
    var dayStatusId = daySheet.CreateAttribute("DayStatusId");
    dayStatusId.IsMandatory = true;
    dayStatusId.Label = "Day Status";
    
    /*** creating IStaticEntity 'DayStatus' ***/
    var dayStatus = eSpace.CreateStaticEntity("DayStatus");
    dayStatus.Description = "Status of a user\'s day sheet.";
    dayStatus.ExposeReadOnly = true;
    dayStatus.Label = "Day Status";
    dayStatus.LabelPlural = "Day Statuses";
    dayStatus.Public = true;
    dayStatus.Shareable = true;
    dayStatus.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Optimized;
    dayStatus.UseTranslations = true;
    
    var id4 = dayStatus.CreateAttribute("Id");
    id4.DataType = eSpace.IntegerType;
    id4.Label = "Id";
    
    var label2 = dayStatus.CreateAttribute("Label");
    label2.DataType = eSpace.TextType;
    label2.IsMandatory = true;
    label2.Label = "Label";
    label2.Length = 50;
    
    var order2 = dayStatus.CreateAttribute("Order");
    order2.DataType = eSpace.IntegerType;
    order2.IsMandatory = true;
    order2.Label = "Order";
    
    var is_Active2 = dayStatus.CreateAttribute("Is_Active");
    is_Active2.DataType = eSpace.BooleanType;
    is_Active2.IsMandatory = true;
    is_Active2.Label = "Is Active";
    
    var staticEntityRecord7 = dayStatus.CreateRecord();
    staticEntityRecord7.Identifier = "Draft";
    staticEntityRecord7.SetAttributeValue(is_Active2, "True");
    staticEntityRecord7.SetAttributeValue(order2, "1");
    staticEntityRecord7.SetAttributeValue(label2, "\"Draft\"");
    staticEntityRecord7.SetAttributeValue(id4, "1");
    
    var staticEntityRecord8 = dayStatus.CreateRecord();
    staticEntityRecord8.Identifier = "DayOff";
    staticEntityRecord8.SetAttributeValue(order2, "3");
    staticEntityRecord8.SetAttributeValue(is_Active2, "True");
    staticEntityRecord8.SetAttributeValue(id4, "3");
    staticEntityRecord8.SetAttributeValue(label2, "\"Day off\"");
    
    var staticEntityRecord9 = dayStatus.CreateRecord();
    staticEntityRecord9.Identifier = "Confirmed";
    staticEntityRecord9.SetAttributeValue(order2, "2");
    staticEntityRecord9.SetAttributeValue(label2, "\"Confirmed\"");
    staticEntityRecord9.SetAttributeValue(id4, "2");
    staticEntityRecord9.SetAttributeValue(is_Active2, "True");
    dayStatus.IdentifierAttribute = id4;
    dayStatus.IsActiveAttribute = is_Active2;
    dayStatus.LabelAttribute = label2;
    dayStatus.OrderByAttribute = order2;
    
    var notes = daySheet.CreateAttribute("Notes");
    notes.DataType = eSpace.TextType;
    notes.Label = "Notes";
    notes.Length = 500;
    dayStatusId.DataType = dayStatus.IdentifierType;
    
    var createdOn = daySheet.CreateAttribute("CreatedOn");
    createdOn.DataType = eSpace.DateTimeType;
    createdOn.IsMandatory = true;
    createdOn.Label = "Created On";
    
    var updatedOn = daySheet.CreateAttribute("UpdatedOn");
    updatedOn.DataType = eSpace.DateTimeType;
    updatedOn.Label = "Updated On";
    // IServerEntitySignature (System)/Espace
    var espace = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*Peat6jeAoS7k0hgUWu_7Nw"));
    // IServerEntitySignature (System)/Group
    var group = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*77Uo0HdEN06fos3dfg5xRg"));
    
    /*** creating IServerEntity 'Holiday' ***/
    var holiday = eSpace.CreateServerEntity("Holiday");
    holiday.Description = "Public holiday. Holidays are not editable in the calendar.";
    holiday.Label = "Holiday";
    holiday.LabelPlural = "Holidays";
    holiday.Public = true;
    holiday.Shareable = true;
    
    var id5 = holiday.CreateAttribute("Id");
    id5.DataType = eSpace.LongIntegerType;
    id5.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id5.Label = "Id";
    holiday.IdentifierAttribute = id5;
    
    var name2 = holiday.CreateAttribute("Name");
    name2.DataType = eSpace.TextType;
    name2.IsMandatory = true;
    name2.Label = "Name";
    name2.Length = 100;
    holiday.LabelAttribute = name2;
    
    var holidayDate = holiday.CreateAttribute("HolidayDate");
    holidayDate.DataType = eSpace.DateType;
    holidayDate.IsMandatory = true;
    holidayDate.Label = "Holiday Date";
    
    var isActive2 = holiday.CreateAttribute("IsActive");
    isActive2.DataType = eSpace.BooleanType;
    isActive2.SetDefaultValue("True");
    isActive2.IsMandatory = true;
    isActive2.Label = "Is Active";
    
    var isFixedDate = holiday.CreateAttribute("IsFixedDate");
    isFixedDate.DataType = eSpace.BooleanType;
    isFixedDate.SetDefaultValue("True");
    isFixedDate.Description = "True when the holiday falls on the same day and month every year (Holiday_CopyYear copies only these). False for movable holidays such as Good Friday, Easter and Corpus Christi.";
    isFixedDate.IsMandatory = true;
    isFixedDate.Label = "Is Fixed Date";
    // IServerEntitySignature (System)/Process
    var process = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*rk1aJNDvckKT91HxgCaO6w"));
    
    /*** creating IServerEntity 'Project' ***/
    var project = eSpace.CreateServerEntity("Project");
    project.Description = "Project a user can record time against.";
    project.Label = "Project";
    project.LabelPlural = "Projects";
    project.Public = true;
    project.Shareable = true;
    
    var id6 = project.CreateAttribute("Id");
    id6.DataType = eSpace.LongIntegerType;
    id6.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id6.Label = "Id";
    project.IdentifierAttribute = id6;
    
    var name3 = project.CreateAttribute("Name");
    name3.DataType = eSpace.TextType;
    name3.IsMandatory = true;
    name3.Label = "Name";
    name3.Length = 100;
    project.LabelAttribute = name3;
    
    var clientId = project.CreateAttribute("ClientId");
    clientId.DataType = client.IdentifierType;
    clientId.IsMandatory = true;
    clientId.Label = "Client";
    
    var code = project.CreateAttribute("Code");
    code.DataType = eSpace.TextType;
    code.IsMandatory = true;
    code.Label = "Code";
    code.Length = 30;
    
    var isActive3 = project.CreateAttribute("IsActive");
    isActive3.DataType = eSpace.BooleanType;
    isActive3.SetDefaultValue("True");
    isActive3.IsMandatory = true;
    isActive3.Label = "Is Active";
    
    /*** creating IServerEntity 'ProjectAssignment' ***/
    var projectAssignment = eSpace.CreateServerEntity("ProjectAssignment");
    projectAssignment.Description = "A user\'s assignment to a project for a date range. A user can have several periods on the same project (one per StartDate); periods are not expected to overlap.";
    projectAssignment.Label = "Project Assignment";
    projectAssignment.LabelPlural = "Project Assignments";
    projectAssignment.Public = true;
    projectAssignment.Shareable = true;
    
    var id7 = projectAssignment.CreateAttribute("Id");
    id7.DataType = eSpace.LongIntegerType;
    id7.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id7.Label = "Id";
    projectAssignment.IdentifierAttribute = id7;
    
    var userId2 = projectAssignment.CreateAttribute("UserId");
    userId2.DataType = user.IdentifierType;
    userId2.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    userId2.IsMandatory = true;
    userId2.Label = "User";
    
    var projectId = projectAssignment.CreateAttribute("ProjectId");
    projectId.DataType = project.IdentifierType;
    projectId.IsMandatory = true;
    projectId.Label = "Project";
    
    var startDate = projectAssignment.CreateAttribute("StartDate");
    startDate.DataType = eSpace.DateType;
    startDate.IsMandatory = true;
    startDate.Label = "Start Date";
    
    var endDate = projectAssignment.CreateAttribute("EndDate");
    endDate.DataType = eSpace.DateType;
    endDate.Label = "End Date";
    
    var isActive4 = projectAssignment.CreateAttribute("IsActive");
    isActive4.DataType = eSpace.BooleanType;
    isActive4.SetDefaultValue("True");
    isActive4.IsMandatory = true;
    isActive4.Label = "Is Active";
    // IServerEntitySignature (System)/Role
    var role = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*NuqV5Rtc2D6TqrunUsf4kg"));
    // IStaticEntitySignature (System)/RuntimeKind
    var runtimeKind = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*64ECB3l5wUuxUhWl1Ubk4A"));
    // IServerEntitySignature (System)/Sent_Email
    var sent_Email = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*CWv+3dcKHk6ctXgJ81F2tg"));
    // IServerEntitySignature (System)/Tenant
    var tenant = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*WQMM07oXpk63b_WESl+a5Q"));
    
    /*** creating IServerEntity 'TimeEntry' ***/
    var timeEntry = eSpace.CreateServerEntity("TimeEntry");
    timeEntry.Description = "Minutes recorded on one project within a day sheet.";
    timeEntry.Label = "Time Entry";
    timeEntry.LabelPlural = "Time Entries";
    timeEntry.Public = true;
    timeEntry.Shareable = true;
    
    var id8 = timeEntry.CreateAttribute("Id");
    id8.DataType = eSpace.LongIntegerType;
    id8.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id8.Label = "Id";
    timeEntry.IdentifierAttribute = id8;
    
    var daySheetId = timeEntry.CreateAttribute("DaySheetId");
    daySheetId.DataType = daySheet.IdentifierType;
    daySheetId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    daySheetId.IsMandatory = true;
    daySheetId.Label = "Day Sheet";
    
    var projectId2 = timeEntry.CreateAttribute("ProjectId");
    projectId2.DataType = project.IdentifierType;
    projectId2.IsMandatory = true;
    projectId2.Label = "Project";
    
    var minutes = timeEntry.CreateAttribute("Minutes");
    minutes.DataType = eSpace.IntegerType;
    minutes.IsMandatory = true;
    minutes.Label = "Minutes";
    
    var description = timeEntry.CreateAttribute("Description");
    description.DataType = eSpace.TextType;
    description.Label = "Description";
    description.Length = 250;
    
    var createdOn2 = timeEntry.CreateAttribute("CreatedOn");
    createdOn2.DataType = eSpace.DateTimeType;
    createdOn2.IsMandatory = true;
    createdOn2.Label = "Created On";
    
    var updatedOn2 = timeEntry.CreateAttribute("UpdatedOn");
    updatedOn2.DataType = eSpace.DateTimeType;
    updatedOn2.Label = "Updated On";
}
```
