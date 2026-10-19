# Structures

Module-wide read in ClaudeSheet_CS, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IStructure 'CalendarDay' ***/
    var calendarDay = eSpace.CreateStructure("CalendarDay");
    calendarDay.Description = "One day of the month calendar for the current user.";
    calendarDay.Public = true;
    calendarDay.Shareable = true;
    
    var date = calendarDay.CreateAttribute("Date");
    date.DataType = eSpace.DateType;
    date.Label = "Date";
    
    var dayNumber = calendarDay.CreateAttribute("DayNumber");
    dayNumber.DataType = eSpace.IntegerType;
    dayNumber.Label = "Day Number";
    
    var weekDay = calendarDay.CreateAttribute("WeekDay");
    weekDay.DataType = eSpace.IntegerType;
    weekDay.Label = "Week Day";
    
    var isWeekend = calendarDay.CreateAttribute("IsWeekend");
    isWeekend.DataType = eSpace.BooleanType;
    isWeekend.Label = "Is Weekend";
    
    var isToday = calendarDay.CreateAttribute("IsToday");
    isToday.DataType = eSpace.BooleanType;
    isToday.Label = "Is Today";
    
    var dayTypeId = calendarDay.CreateAttribute("DayTypeId");
    dayTypeId.Label = "Day Type";
    
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
    
    var label = calendarDayType.CreateAttribute("Label");
    label.DataType = eSpace.TextType;
    label.IsMandatory = true;
    label.Label = "Label";
    label.Length = 50;
    
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
    
    var order = calendarDayType.CreateAttribute("Order");
    order.DataType = eSpace.IntegerType;
    order.IsMandatory = true;
    order.Label = "Order";
    
    var is_Active = calendarDayType.CreateAttribute("Is_Active");
    is_Active.DataType = eSpace.BooleanType;
    is_Active.IsMandatory = true;
    is_Active.Label = "Is Active";
    
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
    calendarDayType.IdentifierAttribute = id;
    calendarDayType.IsActiveAttribute = is_Active;
    calendarDayType.LabelAttribute = label;
    calendarDayType.OrderByAttribute = order;
    
    var cssClass2 = calendarDay.CreateAttribute("CssClass");
    cssClass2.DataType = eSpace.TextType;
    cssClass2.Label = "Css Class";
    cssClass2.Length = 50;
    dayTypeId.DataType = calendarDayType.IdentifierType;
    
    var daySheetId = calendarDay.CreateAttribute("DaySheetId");
    daySheetId.Label = "Day Sheet";
    
    /*** creating IServerEntity 'DaySheet' ***/
    var daySheet = eSpace.CreateServerEntity("DaySheet");
    daySheet.Description = "One user\'s time sheet for one day.";
    daySheet.Label = "Day Sheet";
    daySheet.LabelPlural = "Day Sheets";
    daySheet.Public = true;
    daySheet.Shareable = true;
    
    var id2 = daySheet.CreateAttribute("Id");
    id2.DataType = eSpace.LongIntegerType;
    id2.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id2.Label = "Id";
    
    var userId = daySheet.CreateAttribute("UserId");
    userId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    userId.IsMandatory = true;
    userId.Label = "User";
    
    var sheetDate = daySheet.CreateAttribute("SheetDate");
    sheetDate.DataType = eSpace.DateType;
    sheetDate.IsMandatory = true;
    sheetDate.Label = "Sheet Date";
    
    var dayStatusId = daySheet.CreateAttribute("DayStatusId");
    dayStatusId.IsMandatory = true;
    dayStatusId.Label = "Day Status";
    
    var notes = daySheet.CreateAttribute("Notes");
    notes.DataType = eSpace.TextType;
    notes.Label = "Notes";
    notes.Length = 500;
    
    var createdOn = daySheet.CreateAttribute("CreatedOn");
    createdOn.DataType = eSpace.DateTimeType;
    createdOn.IsMandatory = true;
    createdOn.Label = "Created On";
    
    var updatedOn = daySheet.CreateAttribute("UpdatedOn");
    updatedOn.DataType = eSpace.DateTimeType;
    updatedOn.Label = "Updated On";
    daySheet.IdentifierAttribute = id2;
    // IServerEntitySignature (System)/User
    var user = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*LYIWpF+Ey0aicHD144NUTA"));
    userId.DataType = user.IdentifierType;
    
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
    
    var id3 = dayStatus.CreateAttribute("Id");
    id3.DataType = eSpace.IntegerType;
    id3.Label = "Id";
    
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
    staticEntityRecord7.SetAttributeValue(id3, "1");
    
    var staticEntityRecord8 = dayStatus.CreateRecord();
    staticEntityRecord8.Identifier = "DayOff";
    staticEntityRecord8.SetAttributeValue(order2, "3");
    staticEntityRecord8.SetAttributeValue(is_Active2, "True");
    staticEntityRecord8.SetAttributeValue(id3, "3");
    staticEntityRecord8.SetAttributeValue(label2, "\"Day off\"");
    
    var staticEntityRecord9 = dayStatus.CreateRecord();
    staticEntityRecord9.Identifier = "Confirmed";
    staticEntityRecord9.SetAttributeValue(order2, "2");
    staticEntityRecord9.SetAttributeValue(label2, "\"Confirmed\"");
    staticEntityRecord9.SetAttributeValue(id3, "2");
    staticEntityRecord9.SetAttributeValue(is_Active2, "True");
    dayStatus.IdentifierAttribute = id3;
    dayStatus.IsActiveAttribute = is_Active2;
    dayStatus.LabelAttribute = label2;
    dayStatus.OrderByAttribute = order2;
    
    var dayStatusId2 = calendarDay.CreateAttribute("DayStatusId");
    dayStatusId2.DataType = dayStatus.IdentifierType;
    dayStatusId2.Label = "Day Status";
    daySheetId.DataType = daySheet.IdentifierType;
    dayStatusId.DataType = dayStatus.IdentifierType;
    
    var holidayName = calendarDay.CreateAttribute("HolidayName");
    holidayName.DataType = eSpace.TextType;
    holidayName.Label = "Holiday Name";
    holidayName.Length = 100;
    
    var totalMinutes = calendarDay.CreateAttribute("TotalMinutes");
    totalMinutes.DataType = eSpace.IntegerType;
    totalMinutes.Label = "Total Minutes";
    
    var isEditable = calendarDay.CreateAttribute("IsEditable");
    isEditable.DataType = eSpace.BooleanType;
    isEditable.Label = "Is Editable";
    
    /*** creating IStructure 'ClientSummary' ***/
    var clientSummary = eSpace.CreateStructure("ClientSummary");
    clientSummary.Description = "One client in the admin list, with the number of its projects (Client_GetList).";
    clientSummary.Public = true;
    clientSummary.Shareable = true;
    
    var id4 = clientSummary.CreateAttribute("Id");
    id4.Label = "Id";
    
    /*** creating IServerEntity 'Client' ***/
    var client = eSpace.CreateServerEntity("Client");
    client.Description = "Customer that projects are billed to.";
    client.Label = "Client";
    client.LabelPlural = "Clients";
    client.Public = true;
    client.Shareable = true;
    
    var id5 = client.CreateAttribute("Id");
    id5.DataType = eSpace.LongIntegerType;
    id5.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id5.Label = "Id";
    
    var name = client.CreateAttribute("Name");
    name.DataType = eSpace.TextType;
    name.IsMandatory = true;
    name.Label = "Name";
    name.Length = 100;
    
    var isActive = client.CreateAttribute("IsActive");
    isActive.DataType = eSpace.BooleanType;
    isActive.SetDefaultValue("True");
    isActive.IsMandatory = true;
    isActive.Label = "Is Active";
    client.IdentifierAttribute = id5;
    client.LabelAttribute = name;
    
    var name2 = clientSummary.CreateAttribute("Name");
    name2.DataType = eSpace.TextType;
    name2.Label = "Name";
    name2.Length = 100;
    id4.DataType = client.IdentifierType;
    
    var isActive2 = clientSummary.CreateAttribute("IsActive");
    isActive2.DataType = eSpace.BooleanType;
    isActive2.Label = "Is Active";
    
    var projectCount = clientSummary.CreateAttribute("ProjectCount");
    projectCount.DataType = eSpace.IntegerType;
    projectCount.Label = "Project Count";
    
    /*** creating IStructure 'DayEditorLine' ***/
    var dayEditorLine = eSpace.CreateStructure("DayEditorLine");
    dayEditorLine.Description = "One project line of the HoursCalendar day editor, with the time split into Hours and Mins for the dropdowns.";
    dayEditorLine.Public = true;
    dayEditorLine.Shareable = true;
    
    var projectId = dayEditorLine.CreateAttribute("ProjectId");
    projectId.Label = "Project";
    
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
    
    var clientId = project.CreateAttribute("ClientId");
    clientId.DataType = client.IdentifierType;
    clientId.IsMandatory = true;
    clientId.Label = "Client";
    
    var name3 = project.CreateAttribute("Name");
    name3.DataType = eSpace.TextType;
    name3.IsMandatory = true;
    name3.Label = "Name";
    name3.Length = 100;
    
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
    project.IdentifierAttribute = id6;
    project.LabelAttribute = name3;
    
    var clientName = dayEditorLine.CreateAttribute("ClientName");
    clientName.DataType = eSpace.TextType;
    clientName.Label = "Client Name";
    projectId.DataType = project.IdentifierType;
    
    var projectName = dayEditorLine.CreateAttribute("ProjectName");
    projectName.DataType = eSpace.TextType;
    projectName.Label = "Project Name";
    
    var projectCode = dayEditorLine.CreateAttribute("ProjectCode");
    projectCode.DataType = eSpace.TextType;
    projectCode.Label = "Project Code";
    
    var hours = dayEditorLine.CreateAttribute("Hours");
    hours.DataType = eSpace.IntegerType;
    hours.Label = "Hours";
    
    var mins = dayEditorLine.CreateAttribute("Mins");
    mins.DataType = eSpace.IntegerType;
    mins.Label = "Mins";
    
    var description = dayEditorLine.CreateAttribute("Description");
    description.DataType = eSpace.TextType;
    description.Label = "Description";
    
    /*** creating IStructure 'DaySheetDetail' ***/
    var daySheetDetail = eSpace.CreateStructure("DaySheetDetail");
    daySheetDetail.Description = "A day sheet with its project lines, as shown in the day editor.";
    daySheetDetail.Public = true;
    daySheetDetail.Shareable = true;
    
    var daySheetId2 = daySheetDetail.CreateAttribute("DaySheetId");
    daySheetId2.DataType = daySheet.IdentifierType;
    daySheetId2.Label = "Day Sheet";
    
    var sheetDate2 = daySheetDetail.CreateAttribute("SheetDate");
    sheetDate2.DataType = eSpace.DateType;
    sheetDate2.Label = "Sheet Date";
    
    var dayStatusId3 = daySheetDetail.CreateAttribute("DayStatusId");
    dayStatusId3.DataType = dayStatus.IdentifierType;
    dayStatusId3.Label = "Day Status";
    
    var dayTypeId2 = daySheetDetail.CreateAttribute("DayTypeId");
    dayTypeId2.DataType = calendarDayType.IdentifierType;
    dayTypeId2.Label = "Day Type";
    
    var notes2 = daySheetDetail.CreateAttribute("Notes");
    notes2.DataType = eSpace.TextType;
    notes2.Label = "Notes";
    notes2.Length = 500;
    
    var holidayName2 = daySheetDetail.CreateAttribute("HolidayName");
    holidayName2.DataType = eSpace.TextType;
    holidayName2.Label = "Holiday Name";
    holidayName2.Length = 100;
    
    var isEditable2 = daySheetDetail.CreateAttribute("IsEditable");
    isEditable2.DataType = eSpace.BooleanType;
    isEditable2.Label = "Is Editable";
    
    var totalMinutes2 = daySheetDetail.CreateAttribute("TotalMinutes");
    totalMinutes2.DataType = eSpace.IntegerType;
    totalMinutes2.Label = "Total Minutes";
    
    var lines = daySheetDetail.CreateAttribute("Lines");
    lines.Label = "Lines";
    
    /*** creating IStructure 'DaySheetProjectLine' ***/
    var daySheetProjectLine = eSpace.CreateStructure("DaySheetProjectLine");
    daySheetProjectLine.Description = "One project row in the day sheet editor.";
    daySheetProjectLine.Public = true;
    daySheetProjectLine.Shareable = true;
    
    var projectId2 = daySheetProjectLine.CreateAttribute("ProjectId");
    projectId2.DataType = project.IdentifierType;
    projectId2.Label = "Project";
    lines.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    
    var clientName2 = daySheetProjectLine.CreateAttribute("ClientName");
    clientName2.DataType = eSpace.TextType;
    clientName2.Label = "Client Name";
    clientName2.Length = 100;
    
    var projectName2 = daySheetProjectLine.CreateAttribute("ProjectName");
    projectName2.DataType = eSpace.TextType;
    projectName2.Label = "Project Name";
    projectName2.Length = 100;
    
    var projectCode2 = daySheetProjectLine.CreateAttribute("ProjectCode");
    projectCode2.DataType = eSpace.TextType;
    projectCode2.Label = "Project Code";
    projectCode2.Length = 30;
    
    var timeEntryId = daySheetProjectLine.CreateAttribute("TimeEntryId");
    timeEntryId.Label = "Time Entry";
    
    /*** creating IServerEntity 'TimeEntry' ***/
    var timeEntry = eSpace.CreateServerEntity("TimeEntry");
    timeEntry.Description = "Minutes recorded on one project within a day sheet.";
    timeEntry.Label = "Time Entry";
    timeEntry.LabelPlural = "Time Entries";
    timeEntry.Public = true;
    timeEntry.Shareable = true;
    
    var id7 = timeEntry.CreateAttribute("Id");
    id7.DataType = eSpace.LongIntegerType;
    id7.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id7.Label = "Id";
    
    var daySheetId3 = timeEntry.CreateAttribute("DaySheetId");
    daySheetId3.DataType = daySheet.IdentifierType;
    daySheetId3.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    daySheetId3.IsMandatory = true;
    daySheetId3.Label = "Day Sheet";
    
    var projectId3 = timeEntry.CreateAttribute("ProjectId");
    projectId3.DataType = project.IdentifierType;
    projectId3.IsMandatory = true;
    projectId3.Label = "Project";
    
    var minutes = timeEntry.CreateAttribute("Minutes");
    minutes.DataType = eSpace.IntegerType;
    minutes.IsMandatory = true;
    minutes.Label = "Minutes";
    
    var description2 = timeEntry.CreateAttribute("Description");
    description2.DataType = eSpace.TextType;
    description2.Label = "Description";
    description2.Length = 250;
    
    var createdOn2 = timeEntry.CreateAttribute("CreatedOn");
    createdOn2.DataType = eSpace.DateTimeType;
    createdOn2.IsMandatory = true;
    createdOn2.Label = "Created On";
    
    var updatedOn2 = timeEntry.CreateAttribute("UpdatedOn");
    updatedOn2.DataType = eSpace.DateTimeType;
    updatedOn2.Label = "Updated On";
    timeEntry.IdentifierAttribute = id7;
    
    var minutes2 = daySheetProjectLine.CreateAttribute("Minutes");
    minutes2.DataType = eSpace.IntegerType;
    minutes2.Label = "Minutes";
    timeEntryId.DataType = timeEntry.IdentifierType;
    
    var description3 = daySheetProjectLine.CreateAttribute("Description");
    description3.DataType = eSpace.TextType;
    description3.Label = "Description";
    description3.Length = 250;
    
    /*** creating IStructure 'DayStatusKPI' ***/
    var dayStatusKPI = eSpace.CreateStructure("DayStatusKPI");
    dayStatusKPI.Description = "One KPI card of the DayStatusKPIs block: a CalendarDayType with its count in the selected month and the label already in singular/plural.";
    dayStatusKPI.Public = true;
    dayStatusKPI.Shareable = true;
    
    var calendarDayTypeId = dayStatusKPI.CreateAttribute("CalendarDayTypeId");
    calendarDayTypeId.DataType = calendarDayType.IdentifierType;
    calendarDayTypeId.Label = "Calendar Day Type";
    
    var label3 = dayStatusKPI.CreateAttribute("Label");
    label3.DataType = eSpace.TextType;
    label3.Label = "Label";
    label3.Length = 50;
    
    var cssClass3 = dayStatusKPI.CreateAttribute("CssClass");
    cssClass3.DataType = eSpace.TextType;
    cssClass3.Label = "Css Class";
    cssClass3.Length = 50;
    
    var count = dayStatusKPI.CreateAttribute("Count");
    count.DataType = eSpace.IntegerType;
    count.Label = "Count";
    
    /*** creating IStructure 'MonthSummary' ***/
    var monthSummary = eSpace.CreateStructure("MonthSummary");
    monthSummary.Description = "Totals and KPI counts for one month of the current user.";
    monthSummary.Public = true;
    monthSummary.Shareable = true;
    
    var year = monthSummary.CreateAttribute("Year");
    year.DataType = eSpace.IntegerType;
    year.Label = "Year";
    
    var month = monthSummary.CreateAttribute("Month");
    month.DataType = eSpace.IntegerType;
    month.Label = "Month";
    
    var totalMinutes3 = monthSummary.CreateAttribute("TotalMinutes");
    totalMinutes3.DataType = eSpace.IntegerType;
    totalMinutes3.Label = "Total Minutes";
    
    var elapsedDays = monthSummary.CreateAttribute("ElapsedDays");
    elapsedDays.DataType = eSpace.IntegerType;
    elapsedDays.Label = "Elapsed Days";
    
    var expectedMinutes = monthSummary.CreateAttribute("ExpectedMinutes");
    expectedMinutes.DataType = eSpace.IntegerType;
    expectedMinutes.Label = "Expected Minutes";
    
    var progressPercent = monthSummary.CreateAttribute("ProgressPercent");
    progressPercent.DataType = eSpace.DecimalType;
    progressPercent.Label = "Progress Percent";
    
    var confirmedCount = monthSummary.CreateAttribute("ConfirmedCount");
    confirmedCount.DataType = eSpace.IntegerType;
    confirmedCount.Label = "Confirmed Count";
    
    var dayOffCount = monthSummary.CreateAttribute("DayOffCount");
    dayOffCount.DataType = eSpace.IntegerType;
    dayOffCount.Label = "Day Off Count";
    
    var holidayCount = monthSummary.CreateAttribute("HolidayCount");
    holidayCount.DataType = eSpace.IntegerType;
    holidayCount.Label = "Holiday Count";
    
    var pendingCount = monthSummary.CreateAttribute("PendingCount");
    pendingCount.DataType = eSpace.IntegerType;
    pendingCount.Label = "Pending Count";
    
    /*** creating IStructure 'NumberOption' ***/
    var numberOption = eSpace.CreateStructure("NumberOption");
    numberOption.Description = "Value/label pair for number dropdowns (hours 0-12, minutes 00/15/30/45).";
    numberOption.Public = true;
    numberOption.Shareable = true;
    
    var value = numberOption.CreateAttribute("Value");
    value.DataType = eSpace.IntegerType;
    value.Label = "Value";
    
    var label4 = numberOption.CreateAttribute("Label");
    label4.DataType = eSpace.TextType;
    label4.Label = "Label";
    
    /*** creating IStructure 'ProjectAssignmentSummary' ***/
    var projectAssignmentSummary = eSpace.CreateStructure("ProjectAssignmentSummary");
    projectAssignmentSummary.Description = "One assignment with project, client and user names and its Status (Active, Upcoming, Ended, Inactive) on today\'s date (ProjectAssignment_GetList).";
    projectAssignmentSummary.Public = true;
    projectAssignmentSummary.Shareable = true;
    
    var id8 = projectAssignmentSummary.CreateAttribute("Id");
    id8.Label = "Id";
    
    /*** creating IServerEntity 'ProjectAssignment' ***/
    var projectAssignment = eSpace.CreateServerEntity("ProjectAssignment");
    projectAssignment.Description = "A user\'s assignment to a project for a date range. A user can have several periods on the same project (one per StartDate); periods are not expected to overlap.";
    projectAssignment.Label = "Project Assignment";
    projectAssignment.LabelPlural = "Project Assignments";
    projectAssignment.Public = true;
    projectAssignment.Shareable = true;
    
    var id9 = projectAssignment.CreateAttribute("Id");
    id9.DataType = eSpace.LongIntegerType;
    id9.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id9.Label = "Id";
    
    var userId2 = projectAssignment.CreateAttribute("UserId");
    userId2.DataType = user.IdentifierType;
    userId2.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    userId2.IsMandatory = true;
    userId2.Label = "User";
    
    var projectId4 = projectAssignment.CreateAttribute("ProjectId");
    projectId4.DataType = project.IdentifierType;
    projectId4.IsMandatory = true;
    projectId4.Label = "Project";
    
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
    projectAssignment.IdentifierAttribute = id9;
    
    var projectId5 = projectAssignmentSummary.CreateAttribute("ProjectId");
    projectId5.DataType = project.IdentifierType;
    projectId5.Label = "Project";
    id8.DataType = projectAssignment.IdentifierType;
    
    var projectCode3 = projectAssignmentSummary.CreateAttribute("ProjectCode");
    projectCode3.DataType = eSpace.TextType;
    projectCode3.Label = "Project Code";
    projectCode3.Length = 30;
    
    var projectName3 = projectAssignmentSummary.CreateAttribute("ProjectName");
    projectName3.DataType = eSpace.TextType;
    projectName3.Label = "Project Name";
    projectName3.Length = 100;
    
    var clientName3 = projectAssignmentSummary.CreateAttribute("ClientName");
    clientName3.DataType = eSpace.TextType;
    clientName3.Label = "Client Name";
    clientName3.Length = 100;
    
    var userId3 = projectAssignmentSummary.CreateAttribute("UserId");
    userId3.DataType = user.IdentifierType;
    userId3.Label = "User";
    
    var userName = projectAssignmentSummary.CreateAttribute("UserName");
    userName.DataType = eSpace.TextType;
    userName.Label = "User Name";
    userName.Length = 250;
    
    var startDate2 = projectAssignmentSummary.CreateAttribute("StartDate");
    startDate2.DataType = eSpace.DateType;
    startDate2.Label = "Start Date";
    
    var endDate2 = projectAssignmentSummary.CreateAttribute("EndDate");
    endDate2.DataType = eSpace.DateType;
    endDate2.Label = "End Date";
    
    var isActive5 = projectAssignmentSummary.CreateAttribute("IsActive");
    isActive5.DataType = eSpace.BooleanType;
    isActive5.Label = "Is Active";
    
    var status = projectAssignmentSummary.CreateAttribute("Status");
    status.DataType = eSpace.TextType;
    status.Label = "Status";
    status.Length = 20;
    
    /*** creating IStructure 'ProjectSummary' ***/
    var projectSummary = eSpace.CreateStructure("ProjectSummary");
    projectSummary.Description = "One project in the admin list, with its client and the number of users assigned now or later (Project_GetList).";
    projectSummary.Public = true;
    projectSummary.Shareable = true;
    
    var id10 = projectSummary.CreateAttribute("Id");
    id10.DataType = project.IdentifierType;
    id10.Label = "Id";
    
    var clientId2 = projectSummary.CreateAttribute("ClientId");
    clientId2.DataType = client.IdentifierType;
    clientId2.Label = "Client";
    
    var clientName4 = projectSummary.CreateAttribute("ClientName");
    clientName4.DataType = eSpace.TextType;
    clientName4.Label = "Client Name";
    clientName4.Length = 100;
    
    var code2 = projectSummary.CreateAttribute("Code");
    code2.DataType = eSpace.TextType;
    code2.Label = "Code";
    code2.Length = 30;
    
    var name4 = projectSummary.CreateAttribute("Name");
    name4.DataType = eSpace.TextType;
    name4.Label = "Name";
    name4.Length = 100;
    
    var isActive6 = projectSummary.CreateAttribute("IsActive");
    isActive6.DataType = eSpace.BooleanType;
    isActive6.Label = "Is Active";
    
    var assignedUsers = projectSummary.CreateAttribute("AssignedUsers");
    assignedUsers.DataType = eSpace.IntegerType;
    assignedUsers.Label = "Assigned Users";
    
    /*** creating IStructure 'TimeEntryInput' ***/
    var timeEntryInput = eSpace.CreateStructure("TimeEntryInput");
    timeEntryInput.Description = "Minutes and description entered for one project on one day.";
    timeEntryInput.Public = true;
    timeEntryInput.Shareable = true;
    
    var projectId6 = timeEntryInput.CreateAttribute("ProjectId");
    projectId6.DataType = project.IdentifierType;
    projectId6.Label = "Project";
    
    var minutes3 = timeEntryInput.CreateAttribute("Minutes");
    minutes3.DataType = eSpace.IntegerType;
    minutes3.Label = "Minutes";
    
    var description4 = timeEntryInput.CreateAttribute("Description");
    description4.DataType = eSpace.TextType;
    description4.Label = "Description";
    description4.Length = 250;
    
    /*** creating IStructure 'UserOption' ***/
    var userOption = eSpace.CreateStructure("UserOption");
    userOption.Description = "A user for a dropdown: Id, name and username (User_GetActive).";
    userOption.Public = true;
    userOption.Shareable = true;
    
    var userId4 = userOption.CreateAttribute("UserId");
    userId4.DataType = user.IdentifierType;
    userId4.Label = "User";
    
    var name5 = userOption.CreateAttribute("Name");
    name5.DataType = eSpace.TextType;
    name5.Label = "Name";
    name5.Length = 250;
    
    var username = userOption.CreateAttribute("Username");
    username.DataType = eSpace.TextType;
    username.Label = "Username";
    username.Length = 250;
    
    /*** creating IStructure 'WeekDayHeader' ***/
    var weekDayHeader = eSpace.CreateStructure("WeekDayHeader");
    weekDayHeader.Description = "Column header of the weekly grid.";
    weekDayHeader.Public = true;
    weekDayHeader.Shareable = true;
    
    var date2 = weekDayHeader.CreateAttribute("Date");
    date2.DataType = eSpace.DateType;
    date2.Label = "Date";
    
    var dayName = weekDayHeader.CreateAttribute("DayName");
    dayName.DataType = eSpace.TextType;
    dayName.Label = "Day Name";
    dayName.Length = 20;
    
    var isWeekend2 = weekDayHeader.CreateAttribute("IsWeekend");
    isWeekend2.DataType = eSpace.BooleanType;
    isWeekend2.Label = "Is Weekend";
    
    var isToday2 = weekDayHeader.CreateAttribute("IsToday");
    isToday2.DataType = eSpace.BooleanType;
    isToday2.Label = "Is Today";
    
    var dayTypeId3 = weekDayHeader.CreateAttribute("DayTypeId");
    dayTypeId3.DataType = calendarDayType.IdentifierType;
    dayTypeId3.Label = "Day Type";
    
    var cssClass4 = weekDayHeader.CreateAttribute("CssClass");
    cssClass4.DataType = eSpace.TextType;
    cssClass4.Label = "Css Class";
    cssClass4.Length = 50;
    
    var isEditable3 = weekDayHeader.CreateAttribute("IsEditable");
    isEditable3.DataType = eSpace.BooleanType;
    isEditable3.Label = "Is Editable";
    
    var totalMinutes4 = weekDayHeader.CreateAttribute("TotalMinutes");
    totalMinutes4.DataType = eSpace.IntegerType;
    totalMinutes4.Label = "Total Minutes";
    
    var dateLabel = weekDayHeader.CreateAttribute("DateLabel");
    dateLabel.DataType = eSpace.TextType;
    dateLabel.Label = "Date Label";
    dateLabel.Length = 10;
    
    /*** creating IStructure 'WeeklyCell' ***/
    var weeklyCell = eSpace.CreateStructure("WeeklyCell");
    weeklyCell.Description = "Minutes for one project on one day of the weekly grid.";
    weeklyCell.Public = true;
    weeklyCell.Shareable = true;
    
    var date3 = weeklyCell.CreateAttribute("Date");
    date3.DataType = eSpace.DateType;
    date3.Label = "Date";
    
    var timeEntryId2 = weeklyCell.CreateAttribute("TimeEntryId");
    timeEntryId2.DataType = timeEntry.IdentifierType;
    timeEntryId2.Label = "Time Entry";
    
    var minutes4 = weeklyCell.CreateAttribute("Minutes");
    minutes4.DataType = eSpace.IntegerType;
    minutes4.Label = "Minutes";
    
    var isEditable4 = weeklyCell.CreateAttribute("IsEditable");
    isEditable4.DataType = eSpace.BooleanType;
    isEditable4.Label = "Is Editable";
    
    var showDash = weeklyCell.CreateAttribute("ShowDash");
    showDash.DataType = eSpace.BooleanType;
    showDash.Description = "True when the cell shows \"-\": no entry AND the day is in the future, a weekend, a holiday or a day off.";
    showDash.Label = "Show Dash";
    
    /*** creating IStructure 'WeeklyHours' ***/
    var weeklyHours = eSpace.CreateStructure("WeeklyHours");
    weeklyHours.Description = "The weekly grid: day headers, project rows and totals.";
    weeklyHours.Public = true;
    weeklyHours.Shareable = true;
    
    var weekStart = weeklyHours.CreateAttribute("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.Label = "Week Start";
    
    var weekEnd = weeklyHours.CreateAttribute("WeekEnd");
    weekEnd.DataType = eSpace.DateType;
    weekEnd.Label = "Week End";
    
    var days = weeklyHours.CreateAttribute("Days");
    days.DataType = eSpace.GetOrCreateListType(weekDayHeader);
    days.Label = "Days";
    
    var rows = weeklyHours.CreateAttribute("Rows");
    rows.Label = "Rows";
    
    var grandTotalMinutes = weeklyHours.CreateAttribute("GrandTotalMinutes");
    grandTotalMinutes.DataType = eSpace.IntegerType;
    grandTotalMinutes.Label = "Grand Total Minutes";
    
    var dayTotals = weeklyHours.CreateAttribute("DayTotals");
    dayTotals.DataType = eSpace.GetOrCreateListType(weeklyCell);
    dayTotals.Description = "Total per day (7 cells); ShowDash when every row is \"-\" that day.";
    dayTotals.Label = "Day Totals";
    
    /*** creating IStructure 'WeeklyProjectRow' ***/
    var weeklyProjectRow = eSpace.CreateStructure("WeeklyProjectRow");
    weeklyProjectRow.Description = "One project row of the weekly grid.";
    weeklyProjectRow.Public = true;
    weeklyProjectRow.Shareable = true;
    
    var projectId7 = weeklyProjectRow.CreateAttribute("ProjectId");
    projectId7.DataType = project.IdentifierType;
    projectId7.Label = "Project";
    rows.DataType = eSpace.GetOrCreateListType(weeklyProjectRow);
    
    var clientName5 = weeklyProjectRow.CreateAttribute("ClientName");
    clientName5.DataType = eSpace.TextType;
    clientName5.Label = "Client Name";
    clientName5.Length = 100;
    
    var projectName4 = weeklyProjectRow.CreateAttribute("ProjectName");
    projectName4.DataType = eSpace.TextType;
    projectName4.Label = "Project Name";
    projectName4.Length = 100;
    
    var projectCode4 = weeklyProjectRow.CreateAttribute("ProjectCode");
    projectCode4.DataType = eSpace.TextType;
    projectCode4.Label = "Project Code";
    projectCode4.Length = 30;
    
    var cells = weeklyProjectRow.CreateAttribute("Cells");
    cells.DataType = eSpace.GetOrCreateListType(weeklyCell);
    cells.Label = "Cells";
    
    var totalMinutes5 = weeklyProjectRow.CreateAttribute("TotalMinutes");
    totalMinutes5.DataType = eSpace.IntegerType;
    totalMinutes5.Label = "Total Minutes";
}
```
