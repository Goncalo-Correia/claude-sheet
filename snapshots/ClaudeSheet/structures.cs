(OutSystems.Model.IReference outSystemsCharts) => {
    // IStructureSignature OutSystemsCharts/AxisLabel
    var eSpace = outSystemsCharts.GetESpace();
    var axisLabel = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*PMnEbiR+8UWA9TPQv0lzIw"));
    // IStructureSignature OutSystemsUI/BulkActions
    var bulkActions = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*Nin0Z0JPKUedfb_eUCXCnA"));
    
    /*** creating IStructure 'CalendarDay' ***/
    var calendarDay = eSpace.CreateStructure("CalendarDay");
    calendarDay.Description = "One day of the month calendar for the current user.";
    calendarDay.Folder = null; // default value
    calendarDay.Public = false; // default value
    calendarDay.Shareable = false; // default value
    
    var date = calendarDay.CreateAttribute("Date");
    date.DataType = eSpace.DateType;
    date.SetDefaultValue(null); // default value
    date.Description = null; // default value
    date.IsMandatory = false; // default value
    date.Label = "Date";
    date.NameInJSON = null; // default value
    
    var dayNumber = calendarDay.CreateAttribute("DayNumber");
    dayNumber.DataType = eSpace.IntegerType;
    dayNumber.SetDefaultValue(null); // default value
    dayNumber.Description = null; // default value
    dayNumber.IsMandatory = false; // default value
    dayNumber.Label = "Day Number";
    dayNumber.NameInJSON = null; // default value
    
    var weekDay = calendarDay.CreateAttribute("WeekDay");
    weekDay.DataType = eSpace.IntegerType;
    weekDay.SetDefaultValue(null); // default value
    weekDay.Description = null; // default value
    weekDay.IsMandatory = false; // default value
    weekDay.Label = "Week Day";
    weekDay.NameInJSON = null; // default value
    
    var isWeekend = calendarDay.CreateAttribute("IsWeekend");
    isWeekend.DataType = eSpace.BooleanType;
    isWeekend.SetDefaultValue(null); // default value
    isWeekend.Description = null; // default value
    isWeekend.IsMandatory = false; // default value
    isWeekend.Label = "Is Weekend";
    isWeekend.NameInJSON = null; // default value
    
    var isToday = calendarDay.CreateAttribute("IsToday");
    isToday.DataType = eSpace.BooleanType;
    isToday.SetDefaultValue(null); // default value
    isToday.Description = null; // default value
    isToday.IsMandatory = false; // default value
    isToday.Label = "Is Today";
    isToday.NameInJSON = null; // default value
    
    var dayTypeId = calendarDay.CreateAttribute("DayTypeId");
    dayTypeId.SetDefaultValue(null); // default value
    dayTypeId.Description = null; // default value
    dayTypeId.IsMandatory = false; // default value
    dayTypeId.Label = "Day Type";
    dayTypeId.NameInJSON = null; // default value
    
    /*** creating IStaticEntity 'CalendarDayType' ***/
    var calendarDayType = eSpace.CreateStaticEntity("CalendarDayType");
    calendarDayType.Description = "How a calendar day is classified for the current user (first match wins: Holiday, DayOff, Confirmed, Weekend, Future, Pending).";
    calendarDayType.ExposeProcessEvents = false; // default value
    calendarDayType.Folder = null; // default value
    calendarDayType.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.No; // default value
    calendarDayType.IsRestrictedInQueries = false; // default value
    calendarDayType.Label = "Calendar Day Type";
    calendarDayType.LabelPlural = "Calendar Day Types";
    calendarDayType.Public = false; // default value
    calendarDayType.Shareable = false; // default value
    calendarDayType.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Optimized;
    calendarDayType.UseTranslations = true;
    
    var id = calendarDayType.CreateAttribute("Id");
    id.DataType = eSpace.IntegerType;
    id.SetDefaultValue(null); // default value
    id.Description = null; // default value
    id.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.No; // default value
    id.IsSearchable = false; // default value
    id.Label = "Id";
    id.OriginalName = null; // default value
    id.OriginalType = null; // default value
    
    var label = calendarDayType.CreateAttribute("Label");
    label.DataType = eSpace.TextType;
    label.SetDefaultValue(null); // default value
    label.Description = null; // default value
    label.IsMandatory = true;
    label.IsSearchable = false; // default value
    label.Label = "Label";
    label.Length = 50;
    label.OriginalName = null; // default value
    label.OriginalType = null; // default value
    
    var pluralLabel = calendarDayType.CreateAttribute("PluralLabel");
    pluralLabel.DataType = eSpace.TextType;
    pluralLabel.SetDefaultValue(null); // default value
    pluralLabel.Description = null; // default value
    pluralLabel.IsMandatory = true;
    pluralLabel.IsSearchable = false; // default value
    pluralLabel.Label = "Plural Label";
    pluralLabel.Length = 50;
    pluralLabel.OriginalName = null; // default value
    pluralLabel.OriginalType = null; // default value
    
    var cssClass = calendarDayType.CreateAttribute("CssClass");
    cssClass.DataType = eSpace.TextType;
    cssClass.SetDefaultValue(null); // default value
    cssClass.Description = null; // default value
    cssClass.IsMandatory = true;
    cssClass.IsSearchable = false; // default value
    cssClass.Label = "Css Class";
    cssClass.Length = 50;
    cssClass.OriginalName = null; // default value
    cssClass.OriginalType = null; // default value
    
    var showInKPI = calendarDayType.CreateAttribute("ShowInKPI");
    showInKPI.DataType = eSpace.BooleanType;
    showInKPI.SetDefaultValue(null); // default value
    showInKPI.Description = null; // default value
    showInKPI.IsMandatory = true;
    showInKPI.IsSearchable = false; // default value
    showInKPI.Label = "Show In KPI";
    showInKPI.OriginalName = null; // default value
    showInKPI.OriginalType = null; // default value
    
    var order = calendarDayType.CreateAttribute("Order");
    order.DataType = eSpace.IntegerType;
    order.SetDefaultValue(null); // default value
    order.Description = null; // default value
    order.IsMandatory = true;
    order.IsSearchable = false; // default value
    order.Label = "Order";
    order.OriginalName = null; // default value
    order.OriginalType = null; // default value
    
    var is_Active = calendarDayType.CreateAttribute("Is_Active");
    is_Active.DataType = eSpace.BooleanType;
    is_Active.SetDefaultValue(null); // default value
    is_Active.Description = null; // default value
    is_Active.IsMandatory = true;
    is_Active.IsSearchable = false; // default value
    is_Active.Label = "Is Active";
    is_Active.OriginalName = null; // default value
    is_Active.OriginalType = null; // default value
    
    var staticEntityRecord = calendarDayType.CreateRecord();
    staticEntityRecord.Icon = null; // default value
    staticEntityRecord.Identifier = "Pending";
    staticEntityRecord.SetAttributeValue(pluralLabel, "\"Pending\"");
    staticEntityRecord.SetAttributeValue(id, "4");
    staticEntityRecord.SetAttributeValue(label, "\"Pending\"");
    staticEntityRecord.SetAttributeValue(cssClass, "\"day-pending\"");
    staticEntityRecord.SetAttributeValue(is_Active, "True");
    staticEntityRecord.SetAttributeValue(showInKPI, "True");
    staticEntityRecord.SetAttributeValue(order, "4");
    
    var staticEntityRecord2 = calendarDayType.CreateRecord();
    staticEntityRecord2.Icon = null; // default value
    staticEntityRecord2.Identifier = "Weekend";
    staticEntityRecord2.SetAttributeValue(showInKPI, "False");
    staticEntityRecord2.SetAttributeValue(order, "6");
    staticEntityRecord2.SetAttributeValue(label, "\"Weekend\"");
    staticEntityRecord2.SetAttributeValue(cssClass, "\"day-weekend\"");
    staticEntityRecord2.SetAttributeValue(id, "6");
    staticEntityRecord2.SetAttributeValue(is_Active, "True");
    staticEntityRecord2.SetAttributeValue(pluralLabel, "\"Weekend days\"");
    
    var staticEntityRecord3 = calendarDayType.CreateRecord();
    staticEntityRecord3.Icon = null; // default value
    staticEntityRecord3.Identifier = "Future";
    staticEntityRecord3.SetAttributeValue(pluralLabel, "\"Future days\"");
    staticEntityRecord3.SetAttributeValue(is_Active, "True");
    staticEntityRecord3.SetAttributeValue(label, "\"Future\"");
    staticEntityRecord3.SetAttributeValue(cssClass, "\"day-future\"");
    staticEntityRecord3.SetAttributeValue(id, "5");
    staticEntityRecord3.SetAttributeValue(order, "5");
    staticEntityRecord3.SetAttributeValue(showInKPI, "False");
    
    var staticEntityRecord4 = calendarDayType.CreateRecord();
    staticEntityRecord4.Icon = null; // default value
    staticEntityRecord4.Identifier = "Holiday";
    staticEntityRecord4.SetAttributeValue(id, "3");
    staticEntityRecord4.SetAttributeValue(order, "3");
    staticEntityRecord4.SetAttributeValue(is_Active, "True");
    staticEntityRecord4.SetAttributeValue(cssClass, "\"day-holiday\"");
    staticEntityRecord4.SetAttributeValue(label, "\"Holiday\"");
    staticEntityRecord4.SetAttributeValue(showInKPI, "True");
    staticEntityRecord4.SetAttributeValue(pluralLabel, "\"Holidays\"");
    
    var staticEntityRecord5 = calendarDayType.CreateRecord();
    staticEntityRecord5.Icon = null; // default value
    staticEntityRecord5.Identifier = "Confirmed";
    staticEntityRecord5.SetAttributeValue(label, "\"Confirmed\"");
    staticEntityRecord5.SetAttributeValue(showInKPI, "True");
    staticEntityRecord5.SetAttributeValue(pluralLabel, "\"Confirmed\"");
    staticEntityRecord5.SetAttributeValue(cssClass, "\"day-confirmed\"");
    staticEntityRecord5.SetAttributeValue(id, "1");
    staticEntityRecord5.SetAttributeValue(order, "1");
    staticEntityRecord5.SetAttributeValue(is_Active, "True");
    
    var staticEntityRecord6 = calendarDayType.CreateRecord();
    staticEntityRecord6.Icon = null; // default value
    staticEntityRecord6.Identifier = "DayOff";
    staticEntityRecord6.SetAttributeValue(is_Active, "True");
    staticEntityRecord6.SetAttributeValue(label, "\"Day off\"");
    staticEntityRecord6.SetAttributeValue(pluralLabel, "\"Days off\"");
    staticEntityRecord6.SetAttributeValue(showInKPI, "True");
    staticEntityRecord6.SetAttributeValue(cssClass, "\"day-off\"");
    staticEntityRecord6.SetAttributeValue(id, "2");
    staticEntityRecord6.SetAttributeValue(order, "2");
    calendarDayType.IdentifierAttribute = id;
    calendarDayType.IsActiveAttribute = is_Active;
    calendarDayType.LabelAttribute = label;
    calendarDayType.OrderByAttribute = order;
    
    var cssClass2 = calendarDay.CreateAttribute("CssClass");
    cssClass2.DataType = eSpace.TextType;
    cssClass2.SetDefaultValue(null); // default value
    cssClass2.Description = null; // default value
    cssClass2.IsMandatory = false; // default value
    cssClass2.Label = "Css Class";
    cssClass2.Length = 50;
    cssClass2.NameInJSON = null; // default value
    dayTypeId.DataType = calendarDayType.IdentifierType;
    
    var daySheetId = calendarDay.CreateAttribute("DaySheetId");
    daySheetId.SetDefaultValue(null); // default value
    daySheetId.Description = null; // default value
    daySheetId.IsMandatory = false; // default value
    daySheetId.Label = "Day Sheet";
    daySheetId.NameInJSON = null; // default value
    
    /*** creating IServerEntity 'DaySheet' ***/
    var daySheet = eSpace.CreateServerEntity("DaySheet");
    daySheet.Description = "One user\'s time sheet for one day.";
    daySheet.ExposeProcessEvents = false; // default value
    daySheet.Folder = null; // default value
    daySheet.IsActiveAttribute = null; // default value
    daySheet.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    daySheet.IsRestrictedInQueries = false; // default value
    daySheet.Label = "Day Sheet";
    daySheet.LabelAttribute = null; // default value
    daySheet.LabelPlural = "Day Sheets";
    daySheet.OrderByAttribute = null; // default value
    daySheet.Public = false; // default value
    daySheet.Shareable = false; // default value
    daySheet.TableNameOverride = null; // default value
    daySheet.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full; // default value
    daySheet.UseTranslations = false; // default value
    daySheet.ViewNameOverride = null; // default value
    
    var id2 = daySheet.CreateAttribute("Id");
    id2.DataType = eSpace.LongIntegerType;
    id2.SetDefaultValue(null); // default value
    id2.Description = null; // default value
    id2.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id2.IsSearchable = false; // default value
    id2.Label = "Id";
    id2.OriginalName = null; // default value
    id2.OriginalType = null; // default value
    
    var userId = daySheet.CreateAttribute("UserId");
    userId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    userId.Description = null; // default value
    userId.IsMandatory = true;
    userId.IsSearchable = false; // default value
    userId.Label = "User";
    userId.OriginalName = null; // default value
    userId.OriginalType = null; // default value
    
    var sheetDate = daySheet.CreateAttribute("SheetDate");
    sheetDate.DataType = eSpace.DateType;
    sheetDate.SetDefaultValue(null); // default value
    sheetDate.Description = null; // default value
    sheetDate.IsMandatory = true;
    sheetDate.IsSearchable = false; // default value
    sheetDate.Label = "Sheet Date";
    sheetDate.OriginalName = null; // default value
    sheetDate.OriginalType = null; // default value
    
    var dayStatusId = daySheet.CreateAttribute("DayStatusId");
    dayStatusId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect; // default value
    dayStatusId.Description = null; // default value
    dayStatusId.IsMandatory = true;
    dayStatusId.IsSearchable = false; // default value
    dayStatusId.Label = "Day Status";
    dayStatusId.OriginalName = null; // default value
    dayStatusId.OriginalType = null; // default value
    
    var notes = daySheet.CreateAttribute("Notes");
    notes.DataType = eSpace.TextType;
    notes.SetDefaultValue(null); // default value
    notes.Description = null; // default value
    notes.IsMandatory = false; // default value
    notes.IsSearchable = false; // default value
    notes.Label = "Notes";
    notes.Length = 500;
    notes.OriginalName = null; // default value
    notes.OriginalType = null; // default value
    
    var createdOn = daySheet.CreateAttribute("CreatedOn");
    createdOn.DataType = eSpace.DateTimeType;
    createdOn.SetDefaultValue(null); // default value
    createdOn.Description = null; // default value
    createdOn.IsMandatory = true;
    createdOn.IsSearchable = false; // default value
    createdOn.Label = "Created On";
    createdOn.OriginalName = null; // default value
    createdOn.OriginalType = null; // default value
    
    var updatedOn = daySheet.CreateAttribute("UpdatedOn");
    updatedOn.DataType = eSpace.DateTimeType;
    updatedOn.SetDefaultValue(null); // default value
    updatedOn.Description = null; // default value
    updatedOn.IsMandatory = false; // default value
    updatedOn.IsSearchable = false; // default value
    updatedOn.Label = "Updated On";
    updatedOn.OriginalName = null; // default value
    updatedOn.OriginalType = null; // default value
    daySheet.IdentifierAttribute = id2;
    // IServerEntitySignature (System)/User
    var user = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*LYIWpF+Ey0aicHD144NUTA"));
    userId.DataType = user.IdentifierType;
    
    /*** creating IStaticEntity 'DayStatus' ***/
    var dayStatus = eSpace.CreateStaticEntity("DayStatus");
    dayStatus.Description = "Status of a user\'s day sheet.";
    dayStatus.ExposeProcessEvents = false; // default value
    dayStatus.Folder = null; // default value
    dayStatus.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.No; // default value
    dayStatus.IsRestrictedInQueries = false; // default value
    dayStatus.Label = "Day Status";
    dayStatus.LabelPlural = "Day Statuses";
    dayStatus.Public = false; // default value
    dayStatus.Shareable = false; // default value
    dayStatus.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Optimized;
    dayStatus.UseTranslations = true;
    
    var id3 = dayStatus.CreateAttribute("Id");
    id3.DataType = eSpace.IntegerType;
    id3.SetDefaultValue(null); // default value
    id3.Description = null; // default value
    id3.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.No; // default value
    id3.IsSearchable = false; // default value
    id3.Label = "Id";
    id3.OriginalName = null; // default value
    id3.OriginalType = null; // default value
    
    var label2 = dayStatus.CreateAttribute("Label");
    label2.DataType = eSpace.TextType;
    label2.SetDefaultValue(null); // default value
    label2.Description = null; // default value
    label2.IsMandatory = true;
    label2.IsSearchable = false; // default value
    label2.Label = "Label";
    label2.Length = 50;
    label2.OriginalName = null; // default value
    label2.OriginalType = null; // default value
    
    var order2 = dayStatus.CreateAttribute("Order");
    order2.DataType = eSpace.IntegerType;
    order2.SetDefaultValue(null); // default value
    order2.Description = null; // default value
    order2.IsMandatory = true;
    order2.IsSearchable = false; // default value
    order2.Label = "Order";
    order2.OriginalName = null; // default value
    order2.OriginalType = null; // default value
    
    var is_Active2 = dayStatus.CreateAttribute("Is_Active");
    is_Active2.DataType = eSpace.BooleanType;
    is_Active2.SetDefaultValue(null); // default value
    is_Active2.Description = null; // default value
    is_Active2.IsMandatory = true;
    is_Active2.IsSearchable = false; // default value
    is_Active2.Label = "Is Active";
    is_Active2.OriginalName = null; // default value
    is_Active2.OriginalType = null; // default value
    
    var staticEntityRecord7 = dayStatus.CreateRecord();
    staticEntityRecord7.Icon = null; // default value
    staticEntityRecord7.Identifier = "Draft";
    staticEntityRecord7.SetAttributeValue(is_Active2, "True");
    staticEntityRecord7.SetAttributeValue(id3, "1");
    staticEntityRecord7.SetAttributeValue(order2, "1");
    staticEntityRecord7.SetAttributeValue(label2, "\"Draft\"");
    
    var staticEntityRecord8 = dayStatus.CreateRecord();
    staticEntityRecord8.Icon = null; // default value
    staticEntityRecord8.Identifier = "DayOff";
    staticEntityRecord8.SetAttributeValue(order2, "3");
    staticEntityRecord8.SetAttributeValue(id3, "3");
    staticEntityRecord8.SetAttributeValue(label2, "\"Day off\"");
    staticEntityRecord8.SetAttributeValue(is_Active2, "True");
    
    var staticEntityRecord9 = dayStatus.CreateRecord();
    staticEntityRecord9.Icon = null; // default value
    staticEntityRecord9.Identifier = "Confirmed";
    staticEntityRecord9.SetAttributeValue(is_Active2, "True");
    staticEntityRecord9.SetAttributeValue(order2, "2");
    staticEntityRecord9.SetAttributeValue(id3, "2");
    staticEntityRecord9.SetAttributeValue(label2, "\"Confirmed\"");
    dayStatus.IdentifierAttribute = id3;
    dayStatus.IsActiveAttribute = is_Active2;
    dayStatus.LabelAttribute = label2;
    dayStatus.OrderByAttribute = order2;
    
    var dayStatusId2 = calendarDay.CreateAttribute("DayStatusId");
    dayStatusId2.DataType = dayStatus.IdentifierType;
    dayStatusId2.SetDefaultValue(null); // default value
    dayStatusId2.Description = null; // default value
    dayStatusId2.IsMandatory = false; // default value
    dayStatusId2.Label = "Day Status";
    dayStatusId2.NameInJSON = null; // default value
    daySheetId.DataType = daySheet.IdentifierType;
    dayStatusId.DataType = dayStatus.IdentifierType;
    
    var holidayName = calendarDay.CreateAttribute("HolidayName");
    holidayName.DataType = eSpace.TextType;
    holidayName.SetDefaultValue(null); // default value
    holidayName.Description = null; // default value
    holidayName.IsMandatory = false; // default value
    holidayName.Label = "Holiday Name";
    holidayName.Length = 100;
    holidayName.NameInJSON = null; // default value
    
    var totalMinutes = calendarDay.CreateAttribute("TotalMinutes");
    totalMinutes.DataType = eSpace.IntegerType;
    totalMinutes.SetDefaultValue(null); // default value
    totalMinutes.Description = null; // default value
    totalMinutes.IsMandatory = false; // default value
    totalMinutes.Label = "Total Minutes";
    totalMinutes.NameInJSON = null; // default value
    
    var isEditable = calendarDay.CreateAttribute("IsEditable");
    isEditable.DataType = eSpace.BooleanType;
    isEditable.SetDefaultValue(null); // default value
    isEditable.Description = null; // default value
    isEditable.IsMandatory = false; // default value
    isEditable.Label = "Is Editable";
    isEditable.NameInJSON = null; // default value
    // IStructureSignature OutSystemsUI/CarouselItems
    var carouselItems = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*3HUi56hX10erbaugSaKF9g"));
    // IStructureSignature OutSystemsUI/CarouselOptionalConfigs
    var carouselOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*yukJtDn+o0uipSQPZip3mQ"));
    // IStructureSignature OutSystemsCharts/ChartExportMenuOptions
    var chartExportMenuOptions = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*QMEZMhC+wkiBtdmavx+22A"));
    // IStructureSignature OutSystemsCharts/ChartExportOption
    var chartExportOption = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*10yZaFGrEEuzzj8OJyEmOQ"));
    // IStructureSignature OutSystemsCharts/ChartExportOptionalConfigs
    var chartExportOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*ZIvhqJjgtEa0RQTlapF+ww"));
    // IStructureSignature OutSystemsCharts/ChartLegendOptionalConfigs
    var chartLegendOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*paF_yF+UTUuPIOKkkmyixg"));
    // IStructureSignature OutSystemsCharts/ChartLegendStyling
    var chartLegendStyling = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*W+QAg3aYgUegoRHmRXQsig"));
    // IStructureSignature OutSystemsMaps/Coordinates
    var coordinates = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*WZx1aHJQ0UWsUfXtv2pX2w"));
    // IStructureSignature OutSystemsCharts/DataPoint
    var dataPoint = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*cxjG6+C620ONyhWlajy1gg"));
    // IStructureSignature OutSystemsUI/DatePickerOptionalConfigs
    var datePickerOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*JNLyX_5nIkWxxM8qnqUZow"));
    // IStructureSignature OutSystemsUI/DatePickerRangeOptionalConfigs
    var datePickerRangeOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*grM32LlBXU+cqCKpKb0jsw"));
    
    /*** creating IStructure 'DayEditorLine' ***/
    var dayEditorLine = eSpace.CreateStructure("DayEditorLine");
    dayEditorLine.Description = "One project line of the HoursCalendar day editor, with the time split into Hours and Mins for the dropdowns.";
    dayEditorLine.Folder = null; // default value
    dayEditorLine.Public = false; // default value
    dayEditorLine.Shareable = false; // default value
    
    var projectId = dayEditorLine.CreateAttribute("ProjectId");
    projectId.SetDefaultValue(null); // default value
    projectId.Description = null; // default value
    projectId.IsMandatory = false; // default value
    projectId.Label = "Project";
    projectId.NameInJSON = null; // default value
    
    /*** creating IServerEntity 'Project' ***/
    var project = eSpace.CreateServerEntity("Project");
    project.Description = "Project a user can record time against.";
    project.ExposeProcessEvents = false; // default value
    project.Folder = null; // default value
    project.IsActiveAttribute = null; // default value
    project.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    project.IsRestrictedInQueries = false; // default value
    project.Label = "Project";
    project.LabelPlural = "Projects";
    project.OrderByAttribute = null; // default value
    project.Public = false; // default value
    project.Shareable = false; // default value
    project.TableNameOverride = null; // default value
    project.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full; // default value
    project.UseTranslations = false; // default value
    project.ViewNameOverride = null; // default value
    
    var id4 = project.CreateAttribute("Id");
    id4.DataType = eSpace.LongIntegerType;
    id4.SetDefaultValue(null); // default value
    id4.Description = null; // default value
    id4.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id4.IsSearchable = false; // default value
    id4.Label = "Id";
    id4.OriginalName = null; // default value
    id4.OriginalType = null; // default value
    
    var clientId = project.CreateAttribute("ClientId");
    clientId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect; // default value
    clientId.Description = null; // default value
    clientId.IsMandatory = true;
    clientId.IsSearchable = false; // default value
    clientId.Label = "Client";
    clientId.OriginalName = null; // default value
    clientId.OriginalType = null; // default value
    
    var name = project.CreateAttribute("Name");
    name.DataType = eSpace.TextType;
    name.SetDefaultValue(null); // default value
    name.Description = null; // default value
    name.IsMandatory = true;
    name.IsSearchable = false; // default value
    name.Label = "Name";
    name.Length = 100;
    name.OriginalName = null; // default value
    name.OriginalType = null; // default value
    
    var code = project.CreateAttribute("Code");
    code.DataType = eSpace.TextType;
    code.SetDefaultValue(null); // default value
    code.Description = null; // default value
    code.IsMandatory = true;
    code.IsSearchable = false; // default value
    code.Label = "Code";
    code.Length = 30;
    code.OriginalName = null; // default value
    code.OriginalType = null; // default value
    
    var isActive = project.CreateAttribute("IsActive");
    isActive.DataType = eSpace.BooleanType;
    isActive.SetDefaultValue("True");
    isActive.Description = null; // default value
    isActive.IsMandatory = true;
    isActive.IsSearchable = false; // default value
    isActive.Label = "Is Active";
    isActive.OriginalName = null; // default value
    isActive.OriginalType = null; // default value
    project.IdentifierAttribute = id4;
    project.LabelAttribute = name;
    
    /*** creating IServerEntity 'Client' ***/
    var client = eSpace.CreateServerEntity("Client");
    client.Description = "Customer that projects are billed to.";
    client.ExposeProcessEvents = false; // default value
    client.Folder = null; // default value
    client.IsActiveAttribute = null; // default value
    client.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    client.IsRestrictedInQueries = false; // default value
    client.Label = "Client";
    client.LabelPlural = "Clients";
    client.OrderByAttribute = null; // default value
    client.Public = false; // default value
    client.Shareable = false; // default value
    client.TableNameOverride = null; // default value
    client.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full; // default value
    client.UseTranslations = false; // default value
    client.ViewNameOverride = null; // default value
    
    var id5 = client.CreateAttribute("Id");
    id5.DataType = eSpace.LongIntegerType;
    id5.SetDefaultValue(null); // default value
    id5.Description = null; // default value
    id5.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id5.IsSearchable = false; // default value
    id5.Label = "Id";
    id5.OriginalName = null; // default value
    id5.OriginalType = null; // default value
    
    var name2 = client.CreateAttribute("Name");
    name2.DataType = eSpace.TextType;
    name2.SetDefaultValue(null); // default value
    name2.Description = null; // default value
    name2.IsMandatory = true;
    name2.IsSearchable = false; // default value
    name2.Label = "Name";
    name2.Length = 100;
    name2.OriginalName = null; // default value
    name2.OriginalType = null; // default value
    
    var isActive2 = client.CreateAttribute("IsActive");
    isActive2.DataType = eSpace.BooleanType;
    isActive2.SetDefaultValue("True");
    isActive2.Description = null; // default value
    isActive2.IsMandatory = true;
    isActive2.IsSearchable = false; // default value
    isActive2.Label = "Is Active";
    isActive2.OriginalName = null; // default value
    isActive2.OriginalType = null; // default value
    client.IdentifierAttribute = id5;
    client.LabelAttribute = name2;
    
    var clientName = dayEditorLine.CreateAttribute("ClientName");
    clientName.DataType = eSpace.TextType;
    clientName.SetDefaultValue(null); // default value
    clientName.Description = null; // default value
    clientName.IsMandatory = false; // default value
    clientName.Label = "Client Name";
    clientName.Length = null; // default value
    clientName.NameInJSON = null; // default value
    projectId.DataType = project.IdentifierType;
    clientId.DataType = client.IdentifierType;
    
    var projectName = dayEditorLine.CreateAttribute("ProjectName");
    projectName.DataType = eSpace.TextType;
    projectName.SetDefaultValue(null); // default value
    projectName.Description = null; // default value
    projectName.IsMandatory = false; // default value
    projectName.Label = "Project Name";
    projectName.Length = null; // default value
    projectName.NameInJSON = null; // default value
    
    var projectCode = dayEditorLine.CreateAttribute("ProjectCode");
    projectCode.DataType = eSpace.TextType;
    projectCode.SetDefaultValue(null); // default value
    projectCode.Description = null; // default value
    projectCode.IsMandatory = false; // default value
    projectCode.Label = "Project Code";
    projectCode.Length = null; // default value
    projectCode.NameInJSON = null; // default value
    
    var hours = dayEditorLine.CreateAttribute("Hours");
    hours.DataType = eSpace.IntegerType;
    hours.SetDefaultValue(null); // default value
    hours.Description = null; // default value
    hours.IsMandatory = false; // default value
    hours.Label = "Hours";
    hours.NameInJSON = null; // default value
    
    var mins = dayEditorLine.CreateAttribute("Mins");
    mins.DataType = eSpace.IntegerType;
    mins.SetDefaultValue(null); // default value
    mins.Description = null; // default value
    mins.IsMandatory = false; // default value
    mins.Label = "Mins";
    mins.NameInJSON = null; // default value
    
    var description = dayEditorLine.CreateAttribute("Description");
    description.DataType = eSpace.TextType;
    description.SetDefaultValue(null); // default value
    description.Description = null; // default value
    description.IsMandatory = false; // default value
    description.Label = "Description";
    description.Length = null; // default value
    description.NameInJSON = null; // default value
    
    /*** creating IStructure 'DaySheetDetail' ***/
    var daySheetDetail = eSpace.CreateStructure("DaySheetDetail");
    daySheetDetail.Description = "A day sheet with its project lines, as shown in the day editor.";
    daySheetDetail.Folder = null; // default value
    daySheetDetail.Public = false; // default value
    daySheetDetail.Shareable = false; // default value
    
    var daySheetId2 = daySheetDetail.CreateAttribute("DaySheetId");
    daySheetId2.DataType = daySheet.IdentifierType;
    daySheetId2.SetDefaultValue(null); // default value
    daySheetId2.Description = null; // default value
    daySheetId2.IsMandatory = false; // default value
    daySheetId2.Label = "Day Sheet";
    daySheetId2.NameInJSON = null; // default value
    
    var sheetDate2 = daySheetDetail.CreateAttribute("SheetDate");
    sheetDate2.DataType = eSpace.DateType;
    sheetDate2.SetDefaultValue(null); // default value
    sheetDate2.Description = null; // default value
    sheetDate2.IsMandatory = false; // default value
    sheetDate2.Label = "Sheet Date";
    sheetDate2.NameInJSON = null; // default value
    
    var dayStatusId3 = daySheetDetail.CreateAttribute("DayStatusId");
    dayStatusId3.DataType = dayStatus.IdentifierType;
    dayStatusId3.SetDefaultValue(null); // default value
    dayStatusId3.Description = null; // default value
    dayStatusId3.IsMandatory = false; // default value
    dayStatusId3.Label = "Day Status";
    dayStatusId3.NameInJSON = null; // default value
    
    var dayTypeId2 = daySheetDetail.CreateAttribute("DayTypeId");
    dayTypeId2.DataType = calendarDayType.IdentifierType;
    dayTypeId2.SetDefaultValue(null); // default value
    dayTypeId2.Description = null; // default value
    dayTypeId2.IsMandatory = false; // default value
    dayTypeId2.Label = "Day Type";
    dayTypeId2.NameInJSON = null; // default value
    
    var notes2 = daySheetDetail.CreateAttribute("Notes");
    notes2.DataType = eSpace.TextType;
    notes2.SetDefaultValue(null); // default value
    notes2.Description = null; // default value
    notes2.IsMandatory = false; // default value
    notes2.Label = "Notes";
    notes2.Length = 500;
    notes2.NameInJSON = null; // default value
    
    var holidayName2 = daySheetDetail.CreateAttribute("HolidayName");
    holidayName2.DataType = eSpace.TextType;
    holidayName2.SetDefaultValue(null); // default value
    holidayName2.Description = null; // default value
    holidayName2.IsMandatory = false; // default value
    holidayName2.Label = "Holiday Name";
    holidayName2.Length = 100;
    holidayName2.NameInJSON = null; // default value
    
    var isEditable2 = daySheetDetail.CreateAttribute("IsEditable");
    isEditable2.DataType = eSpace.BooleanType;
    isEditable2.SetDefaultValue(null); // default value
    isEditable2.Description = null; // default value
    isEditable2.IsMandatory = false; // default value
    isEditable2.Label = "Is Editable";
    isEditable2.NameInJSON = null; // default value
    
    var totalMinutes2 = daySheetDetail.CreateAttribute("TotalMinutes");
    totalMinutes2.DataType = eSpace.IntegerType;
    totalMinutes2.SetDefaultValue(null); // default value
    totalMinutes2.Description = null; // default value
    totalMinutes2.IsMandatory = false; // default value
    totalMinutes2.Label = "Total Minutes";
    totalMinutes2.NameInJSON = null; // default value
    
    var lines = daySheetDetail.CreateAttribute("Lines");
    lines.Description = null; // default value
    lines.IsMandatory = false; // default value
    lines.Label = "Lines";
    lines.NameInJSON = null; // default value
    
    /*** creating IStructure 'DaySheetProjectLine' ***/
    var daySheetProjectLine = eSpace.CreateStructure("DaySheetProjectLine");
    daySheetProjectLine.Description = "One project row in the day sheet editor.";
    daySheetProjectLine.Folder = null; // default value
    daySheetProjectLine.Public = false; // default value
    daySheetProjectLine.Shareable = false; // default value
    
    var projectId2 = daySheetProjectLine.CreateAttribute("ProjectId");
    projectId2.DataType = project.IdentifierType;
    projectId2.SetDefaultValue(null); // default value
    projectId2.Description = null; // default value
    projectId2.IsMandatory = false; // default value
    projectId2.Label = "Project";
    projectId2.NameInJSON = null; // default value
    lines.DataType = eSpace.GetOrCreateListType(daySheetProjectLine);
    
    var clientName2 = daySheetProjectLine.CreateAttribute("ClientName");
    clientName2.DataType = eSpace.TextType;
    clientName2.SetDefaultValue(null); // default value
    clientName2.Description = null; // default value
    clientName2.IsMandatory = false; // default value
    clientName2.Label = "Client Name";
    clientName2.Length = 100;
    clientName2.NameInJSON = null; // default value
    
    var projectName2 = daySheetProjectLine.CreateAttribute("ProjectName");
    projectName2.DataType = eSpace.TextType;
    projectName2.SetDefaultValue(null); // default value
    projectName2.Description = null; // default value
    projectName2.IsMandatory = false; // default value
    projectName2.Label = "Project Name";
    projectName2.Length = 100;
    projectName2.NameInJSON = null; // default value
    
    var projectCode2 = daySheetProjectLine.CreateAttribute("ProjectCode");
    projectCode2.DataType = eSpace.TextType;
    projectCode2.SetDefaultValue(null); // default value
    projectCode2.Description = null; // default value
    projectCode2.IsMandatory = false; // default value
    projectCode2.Label = "Project Code";
    projectCode2.Length = 30;
    projectCode2.NameInJSON = null; // default value
    
    var timeEntryId = daySheetProjectLine.CreateAttribute("TimeEntryId");
    timeEntryId.SetDefaultValue(null); // default value
    timeEntryId.Description = null; // default value
    timeEntryId.IsMandatory = false; // default value
    timeEntryId.Label = "Time Entry";
    timeEntryId.NameInJSON = null; // default value
    
    /*** creating IServerEntity 'TimeEntry' ***/
    var timeEntry = eSpace.CreateServerEntity("TimeEntry");
    timeEntry.Description = "Minutes recorded on one project within a day sheet.";
    timeEntry.ExposeProcessEvents = false; // default value
    timeEntry.Folder = null; // default value
    timeEntry.IsActiveAttribute = null; // default value
    timeEntry.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    timeEntry.IsRestrictedInQueries = false; // default value
    timeEntry.Label = "Time Entry";
    timeEntry.LabelAttribute = null; // default value
    timeEntry.LabelPlural = "Time Entries";
    timeEntry.OrderByAttribute = null; // default value
    timeEntry.Public = false; // default value
    timeEntry.Shareable = false; // default value
    timeEntry.TableNameOverride = null; // default value
    timeEntry.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full; // default value
    timeEntry.UseTranslations = false; // default value
    timeEntry.ViewNameOverride = null; // default value
    
    var id6 = timeEntry.CreateAttribute("Id");
    id6.DataType = eSpace.LongIntegerType;
    id6.SetDefaultValue(null); // default value
    id6.Description = null; // default value
    id6.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id6.IsSearchable = false; // default value
    id6.Label = "Id";
    id6.OriginalName = null; // default value
    id6.OriginalType = null; // default value
    
    var daySheetId3 = timeEntry.CreateAttribute("DaySheetId");
    daySheetId3.DataType = daySheet.IdentifierType;
    daySheetId3.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    daySheetId3.Description = null; // default value
    daySheetId3.IsMandatory = true;
    daySheetId3.IsSearchable = false; // default value
    daySheetId3.Label = "Day Sheet";
    daySheetId3.OriginalName = null; // default value
    daySheetId3.OriginalType = null; // default value
    
    var projectId3 = timeEntry.CreateAttribute("ProjectId");
    projectId3.DataType = project.IdentifierType;
    projectId3.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect; // default value
    projectId3.Description = null; // default value
    projectId3.IsMandatory = true;
    projectId3.IsSearchable = false; // default value
    projectId3.Label = "Project";
    projectId3.OriginalName = null; // default value
    projectId3.OriginalType = null; // default value
    
    var minutes = timeEntry.CreateAttribute("Minutes");
    minutes.DataType = eSpace.IntegerType;
    minutes.SetDefaultValue(null); // default value
    minutes.Description = null; // default value
    minutes.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.No; // default value
    minutes.IsMandatory = true;
    minutes.IsSearchable = false; // default value
    minutes.Label = "Minutes";
    minutes.OriginalName = null; // default value
    minutes.OriginalType = null; // default value
    
    var description2 = timeEntry.CreateAttribute("Description");
    description2.DataType = eSpace.TextType;
    description2.SetDefaultValue(null); // default value
    description2.Description = null; // default value
    description2.IsMandatory = false; // default value
    description2.IsSearchable = false; // default value
    description2.Label = "Description";
    description2.Length = 250;
    description2.OriginalName = null; // default value
    description2.OriginalType = null; // default value
    
    var createdOn2 = timeEntry.CreateAttribute("CreatedOn");
    createdOn2.DataType = eSpace.DateTimeType;
    createdOn2.SetDefaultValue(null); // default value
    createdOn2.Description = null; // default value
    createdOn2.IsMandatory = true;
    createdOn2.IsSearchable = false; // default value
    createdOn2.Label = "Created On";
    createdOn2.OriginalName = null; // default value
    createdOn2.OriginalType = null; // default value
    
    var updatedOn2 = timeEntry.CreateAttribute("UpdatedOn");
    updatedOn2.DataType = eSpace.DateTimeType;
    updatedOn2.SetDefaultValue(null); // default value
    updatedOn2.Description = null; // default value
    updatedOn2.IsMandatory = false; // default value
    updatedOn2.IsSearchable = false; // default value
    updatedOn2.Label = "Updated On";
    updatedOn2.OriginalName = null; // default value
    updatedOn2.OriginalType = null; // default value
    timeEntry.IdentifierAttribute = id6;
    
    var minutes2 = daySheetProjectLine.CreateAttribute("Minutes");
    minutes2.DataType = eSpace.IntegerType;
    minutes2.SetDefaultValue(null); // default value
    minutes2.Description = null; // default value
    minutes2.IsMandatory = false; // default value
    minutes2.Label = "Minutes";
    minutes2.NameInJSON = null; // default value
    timeEntryId.DataType = timeEntry.IdentifierType;
    
    var description3 = daySheetProjectLine.CreateAttribute("Description");
    description3.DataType = eSpace.TextType;
    description3.SetDefaultValue(null); // default value
    description3.Description = null; // default value
    description3.IsMandatory = false; // default value
    description3.Label = "Description";
    description3.Length = 250;
    description3.NameInJSON = null; // default value
    
    /*** creating IStructure 'DayStatusKPI' ***/
    var dayStatusKPI = eSpace.CreateStructure("DayStatusKPI");
    dayStatusKPI.Description = "One KPI card of the DayStatusKPIs block: a CalendarDayType with its count in the selected month and the label already in singular/plural.";
    dayStatusKPI.Folder = null; // default value
    dayStatusKPI.Public = false; // default value
    dayStatusKPI.Shareable = false; // default value
    
    var calendarDayTypeId = dayStatusKPI.CreateAttribute("CalendarDayTypeId");
    calendarDayTypeId.DataType = calendarDayType.IdentifierType;
    calendarDayTypeId.SetDefaultValue(null); // default value
    calendarDayTypeId.Description = null; // default value
    calendarDayTypeId.IsMandatory = false; // default value
    calendarDayTypeId.Label = "Calendar Day Type";
    calendarDayTypeId.NameInJSON = null; // default value
    
    var label3 = dayStatusKPI.CreateAttribute("Label");
    label3.DataType = eSpace.TextType;
    label3.SetDefaultValue(null); // default value
    label3.Description = null; // default value
    label3.IsMandatory = false; // default value
    label3.Label = "Label";
    label3.Length = 50;
    label3.NameInJSON = null; // default value
    
    var cssClass3 = dayStatusKPI.CreateAttribute("CssClass");
    cssClass3.DataType = eSpace.TextType;
    cssClass3.SetDefaultValue(null); // default value
    cssClass3.Description = null; // default value
    cssClass3.IsMandatory = false; // default value
    cssClass3.Label = "Css Class";
    cssClass3.Length = 50;
    cssClass3.NameInJSON = null; // default value
    
    var count = dayStatusKPI.CreateAttribute("Count");
    count.DataType = eSpace.IntegerType;
    count.SetDefaultValue(null); // default value
    count.Description = null; // default value
    count.IsMandatory = false; // default value
    count.Label = "Count";
    count.NameInJSON = null; // default value
    // IStructureSignature OutSystemsUI/DeviceConfiguration
    var deviceConfiguration = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*uUnElVRZ7EeykU8Op0Iq9A"));
    // IStructureSignature OutSystemsUI/DropdownItem
    var dropdownItem = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*GsxolpT5U0Gckg0MkAMyPQ"));
    // IStructureSignature OutSystemsUI/DropdownOption
    var dropdownOption = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*S7xoxEqbCUG_BCxpreTOYw"));
    // IStructureSignature OutSystemsUI/DropdownOptionalConfigs
    var dropdownOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*X1pGkDp6nkSgQbNaytI6Pg"));
    // IStructureSignature OutSystemsUI/DropdownTagsOptionalConfigs
    var dropdownTagsOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*do9jkzG6BUq50KGz_s009w"));
    // IStructureSignature OutSystemsUI/ErrorMessage
    var errorMessage = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*5nlHOt6drUmqeh9sD2bQBA"));
    // IStructureSignature OutSystemsCharts/ErrorMessage2
    var errorMessage2 = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*GN8lvtIwZkSHL0Z3vcEAPQ"));
    // IStructureSignature OutSystemsUI/FlatpickrConfigs
    var flatpickrConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*O+U0HyabQ0yxBLMCogSRKg"));
    // IStructureSignature OutSystemsUI/GradientColor
    var gradientColor = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*IxDFM_PyIUSuvuT8UjkOpA"));
    // IStructureSignature OutSystemsMaps/IconSize
    var iconSize = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*0K72Qk3W8UKfwN50QYd4Xw"));
    // IStructureSignature OutSystemsUI/LightboxImageOptionalConfigs
    var lightboxImageOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*nH0s8+IJh0KOba4Bye9HBg"));
    // IStructureSignature OutSystemsMaps/Localization
    var localization = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*yihIBQrI706bmMXHr3P3DQ"));
    // IStructureSignature OutSystemsMaps/Marker
    var marker = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*NiCGoYbA+0qb4lPY4N633w"));
    // IStructureSignature OutSystemsMaps/MarkerClusterer
    var markerClusterer = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*8G2shutUB0Oe7ahWNAesaw"));
    // IStructureSignature OutSystemsUI/MonthOrderYear
    var monthOrderYear = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*IEKMqKLYpkqqO+CGuDduBw"));
    
    /*** creating IStructure 'MonthSummary' ***/
    var monthSummary = eSpace.CreateStructure("MonthSummary");
    monthSummary.Description = "Totals and KPI counts for one month of the current user.";
    monthSummary.Folder = null; // default value
    monthSummary.Public = false; // default value
    monthSummary.Shareable = false; // default value
    
    var year = monthSummary.CreateAttribute("Year");
    year.DataType = eSpace.IntegerType;
    year.SetDefaultValue(null); // default value
    year.Description = null; // default value
    year.IsMandatory = false; // default value
    year.Label = "Year";
    year.NameInJSON = null; // default value
    
    var month = monthSummary.CreateAttribute("Month");
    month.DataType = eSpace.IntegerType;
    month.SetDefaultValue(null); // default value
    month.Description = null; // default value
    month.IsMandatory = false; // default value
    month.Label = "Month";
    month.NameInJSON = null; // default value
    
    var totalMinutes3 = monthSummary.CreateAttribute("TotalMinutes");
    totalMinutes3.DataType = eSpace.IntegerType;
    totalMinutes3.SetDefaultValue(null); // default value
    totalMinutes3.Description = null; // default value
    totalMinutes3.IsMandatory = false; // default value
    totalMinutes3.Label = "Total Minutes";
    totalMinutes3.NameInJSON = null; // default value
    
    var elapsedDays = monthSummary.CreateAttribute("ElapsedDays");
    elapsedDays.DataType = eSpace.IntegerType;
    elapsedDays.SetDefaultValue(null); // default value
    elapsedDays.Description = null; // default value
    elapsedDays.IsMandatory = false; // default value
    elapsedDays.Label = "Elapsed Days";
    elapsedDays.NameInJSON = null; // default value
    
    var expectedMinutes = monthSummary.CreateAttribute("ExpectedMinutes");
    expectedMinutes.DataType = eSpace.IntegerType;
    expectedMinutes.SetDefaultValue(null); // default value
    expectedMinutes.Description = null; // default value
    expectedMinutes.IsMandatory = false; // default value
    expectedMinutes.Label = "Expected Minutes";
    expectedMinutes.NameInJSON = null; // default value
    
    var progressPercent = monthSummary.CreateAttribute("ProgressPercent");
    progressPercent.DataType = eSpace.DecimalType;
    progressPercent.Decimals = null; // default value
    progressPercent.SetDefaultValue(null); // default value
    progressPercent.Description = null; // default value
    progressPercent.IsMandatory = false; // default value
    progressPercent.Label = "Progress Percent";
    progressPercent.Length = null; // default value
    progressPercent.NameInJSON = null; // default value
    
    var confirmedCount = monthSummary.CreateAttribute("ConfirmedCount");
    confirmedCount.DataType = eSpace.IntegerType;
    confirmedCount.SetDefaultValue(null); // default value
    confirmedCount.Description = null; // default value
    confirmedCount.IsMandatory = false; // default value
    confirmedCount.Label = "Confirmed Count";
    confirmedCount.NameInJSON = null; // default value
    
    var dayOffCount = monthSummary.CreateAttribute("DayOffCount");
    dayOffCount.DataType = eSpace.IntegerType;
    dayOffCount.SetDefaultValue(null); // default value
    dayOffCount.Description = null; // default value
    dayOffCount.IsMandatory = false; // default value
    dayOffCount.Label = "Day Off Count";
    dayOffCount.NameInJSON = null; // default value
    
    var holidayCount = monthSummary.CreateAttribute("HolidayCount");
    holidayCount.DataType = eSpace.IntegerType;
    holidayCount.SetDefaultValue(null); // default value
    holidayCount.Description = null; // default value
    holidayCount.IsMandatory = false; // default value
    holidayCount.Label = "Holiday Count";
    holidayCount.NameInJSON = null; // default value
    
    var pendingCount = monthSummary.CreateAttribute("PendingCount");
    pendingCount.DataType = eSpace.IntegerType;
    pendingCount.SetDefaultValue(null); // default value
    pendingCount.Description = null; // default value
    pendingCount.IsMandatory = false; // default value
    pendingCount.Label = "Pending Count";
    pendingCount.NameInJSON = null; // default value
    // IStructureSignature OutSystemsUI/MonthYear
    var monthYear = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*0fcWF_jbQ0ilugRe0GBAnQ"));
    // IStructureSignature OutSystemsUI/NotificationOptionalConfigs
    var notificationOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*PmxFCOpovUiOxQ+CO48nVQ"));
    // IStructureSignature OutSystemsUI/NoUiSliderConfigs
    var noUiSliderConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*OHFQpHsvwky+_PPPj80ixA"));
    // IStructureSignature OutSystemsUI/NoUiSliderConfigs_Range
    var noUiSliderConfigs_Range = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*2cQY0DjpYU6CzF+YJYSuBw"));
    
    /*** creating IStructure 'NumberOption' ***/
    var numberOption = eSpace.CreateStructure("NumberOption");
    numberOption.Description = "Value/label pair for number dropdowns (hours 0-12, minutes 00/15/30/45).";
    numberOption.Folder = null; // default value
    numberOption.Public = false; // default value
    numberOption.Shareable = false; // default value
    
    var value = numberOption.CreateAttribute("Value");
    value.DataType = eSpace.IntegerType;
    value.SetDefaultValue(null); // default value
    value.Description = null; // default value
    value.IsMandatory = false; // default value
    value.Label = "Value";
    value.NameInJSON = null; // default value
    
    var label4 = numberOption.CreateAttribute("Label");
    label4.DataType = eSpace.TextType;
    label4.SetDefaultValue(null); // default value
    label4.Description = null; // default value
    label4.IsMandatory = false; // default value
    label4.Label = "Label";
    label4.Length = null; // default value
    label4.NameInJSON = null; // default value
    // IStructureSignature OutSystemsMaps/Offset
    var offset = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*Vvpmd9jHNUqpUNrgJ9MmAw"));
    // IStructureSignature OutSystemsCharts/OptionalConfigs
    var optionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*_CcUVq6LpE6z1g1xn392SQ"));
    // IStructureSignature OutSystemsMaps/OptionalMapConfigs
    var optionalMapConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*Z6bJHsxUF0GE4UXTJclJew"));
    // IStructureSignature OutSystemsMaps/OptionalMarkerConfigs
    var optionalMarkerConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*NXdd1rRCLkez78cYLP3_qg"));
    // IStructureSignature OutSystemsMaps/OptionalStaticMapConfigs
    var optionalStaticMapConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*JZvz4YYXi0KWFxIRoNqGBQ"));
    // IStructureSignature OutSystemsUI/ProgressBarOptionalConfigs
    var progressBarOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*yr_QLdz3gUSapQFUagrtsQ"));
    // IStructureSignature OutSystemsUI/ProgressCircleOptionalConfigs
    var progressCircleOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*IX4mK1jQ+kaQWhxlHnzoMA"));
    // IStructureSignature OutSystemsCharts/PropertyValue
    var propertyValue = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*HwYhgBgvAUKeq0C5fagyDg"));
    // IStructureSignature OutSystemsUI/RangeSliderOptionalConfigs
    var rangeSliderOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*YH6NTPqYJkq2DoxNHSzraQ"));
    // IStructureSignature OutSystemsCharts/SeriesStyling
    var seriesStyling = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*uHnvkJbeQkasY7byITpeMg"));
    // IStructureSignature OutSystemsCharts/SeriesStylingMarker
    var seriesStylingMarker = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*LqcKXUXMNkKFd+T0BuO0fA"));
    // IStructureSignature OutSystemsCharts/SeriesStylingOptionalConfigs
    var seriesStylingOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*AWHYK1eo4kCoYBcQhH0C1g"));
    // IStructureSignature OutSystemsUI/SplideConfigs
    var splideConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*F9dhjiLK3Ue591n_3JFJlQ"));
    // IStructureSignature OutSystemsUI/SplideProviderClasses
    var splideProviderClasses = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*dbAI3uYXZkaqDM_imgqiHQ"));
    // IStructureSignature OutSystemsUI/SplideProvideri18n
    var splideProvideri18n = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*XVQm8JHa9E24D2KOuMKTVA"));
    // IStructureSignature OutSystemsMaps/StaticMarker
    var staticMarker = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*HWKV7+6mrUKd5B5htFPGKw"));
    // IStructureSignature OutSystemsUI/TabsOptionalConfigs
    var tabsOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*OVD8IhsywU+PQi603Zw0AQ"));
    
    /*** creating IStructure 'TimeEntryInput' ***/
    var timeEntryInput = eSpace.CreateStructure("TimeEntryInput");
    timeEntryInput.Description = "Minutes and description entered for one project on one day.";
    timeEntryInput.Folder = null; // default value
    timeEntryInput.Public = false; // default value
    timeEntryInput.Shareable = false; // default value
    
    var projectId4 = timeEntryInput.CreateAttribute("ProjectId");
    projectId4.DataType = project.IdentifierType;
    projectId4.SetDefaultValue(null); // default value
    projectId4.Description = null; // default value
    projectId4.IsMandatory = false; // default value
    projectId4.Label = "Project";
    projectId4.NameInJSON = null; // default value
    
    var minutes3 = timeEntryInput.CreateAttribute("Minutes");
    minutes3.DataType = eSpace.IntegerType;
    minutes3.SetDefaultValue(null); // default value
    minutes3.Description = null; // default value
    minutes3.IsMandatory = false; // default value
    minutes3.Label = "Minutes";
    minutes3.NameInJSON = null; // default value
    
    var description4 = timeEntryInput.CreateAttribute("Description");
    description4.DataType = eSpace.TextType;
    description4.SetDefaultValue(null); // default value
    description4.Description = null; // default value
    description4.IsMandatory = false; // default value
    description4.Label = "Description";
    description4.Length = 250;
    description4.NameInJSON = null; // default value
    // IStructureSignature OutSystemsUI/TimePickerOptionalConfigs
    var timePickerOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*S86ifK4baUy+ZoPeWdAqqQ"));
    // IStructureSignature OutSystemsUI/VideoCaptions
    var videoCaptions = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*+3w9d276xkuitQeiG1EGag"));
    // IStructureSignature OutSystemsUI/VideoOptionalConfigs
    var videoOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*f9HbDthd0ECNETfjDFOmow"));
    // IStructureSignature OutSystemsUI/VirtualSelectConfigs
    var virtualSelectConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*x3IZFA2oh0WqcttA4dlGzQ"));
    
    /*** creating IStructure 'WeekDayHeader' ***/
    var weekDayHeader = eSpace.CreateStructure("WeekDayHeader");
    weekDayHeader.Description = "Column header of the weekly grid.";
    weekDayHeader.Folder = null; // default value
    weekDayHeader.Public = false; // default value
    weekDayHeader.Shareable = false; // default value
    
    var date2 = weekDayHeader.CreateAttribute("Date");
    date2.DataType = eSpace.DateType;
    date2.SetDefaultValue(null); // default value
    date2.Description = null; // default value
    date2.IsMandatory = false; // default value
    date2.Label = "Date";
    date2.NameInJSON = null; // default value
    
    var dayLabel = weekDayHeader.CreateAttribute("DayLabel");
    dayLabel.DataType = eSpace.TextType;
    dayLabel.SetDefaultValue(null); // default value
    dayLabel.Description = null; // default value
    dayLabel.IsMandatory = false; // default value
    dayLabel.Label = "Day Label";
    dayLabel.Length = 20;
    dayLabel.NameInJSON = null; // default value
    
    var isWeekend2 = weekDayHeader.CreateAttribute("IsWeekend");
    isWeekend2.DataType = eSpace.BooleanType;
    isWeekend2.SetDefaultValue(null); // default value
    isWeekend2.Description = null; // default value
    isWeekend2.IsMandatory = false; // default value
    isWeekend2.Label = "Is Weekend";
    isWeekend2.NameInJSON = null; // default value
    
    var isToday2 = weekDayHeader.CreateAttribute("IsToday");
    isToday2.DataType = eSpace.BooleanType;
    isToday2.SetDefaultValue(null); // default value
    isToday2.Description = null; // default value
    isToday2.IsMandatory = false; // default value
    isToday2.Label = "Is Today";
    isToday2.NameInJSON = null; // default value
    
    var dayTypeId3 = weekDayHeader.CreateAttribute("DayTypeId");
    dayTypeId3.DataType = calendarDayType.IdentifierType;
    dayTypeId3.SetDefaultValue(null); // default value
    dayTypeId3.Description = null; // default value
    dayTypeId3.IsMandatory = false; // default value
    dayTypeId3.Label = "Day Type";
    dayTypeId3.NameInJSON = null; // default value
    
    var cssClass4 = weekDayHeader.CreateAttribute("CssClass");
    cssClass4.DataType = eSpace.TextType;
    cssClass4.SetDefaultValue(null); // default value
    cssClass4.Description = null; // default value
    cssClass4.IsMandatory = false; // default value
    cssClass4.Label = "Css Class";
    cssClass4.Length = 50;
    cssClass4.NameInJSON = null; // default value
    
    var isEditable3 = weekDayHeader.CreateAttribute("IsEditable");
    isEditable3.DataType = eSpace.BooleanType;
    isEditable3.SetDefaultValue(null); // default value
    isEditable3.Description = null; // default value
    isEditable3.IsMandatory = false; // default value
    isEditable3.Label = "Is Editable";
    isEditable3.NameInJSON = null; // default value
    
    var totalMinutes4 = weekDayHeader.CreateAttribute("TotalMinutes");
    totalMinutes4.DataType = eSpace.IntegerType;
    totalMinutes4.SetDefaultValue(null); // default value
    totalMinutes4.Description = null; // default value
    totalMinutes4.IsMandatory = false; // default value
    totalMinutes4.Label = "Total Minutes";
    totalMinutes4.NameInJSON = null; // default value
    
    /*** creating IStructure 'WeeklyCell' ***/
    var weeklyCell = eSpace.CreateStructure("WeeklyCell");
    weeklyCell.Description = "Minutes for one project on one day of the weekly grid.";
    weeklyCell.Folder = null; // default value
    weeklyCell.Public = false; // default value
    weeklyCell.Shareable = false; // default value
    
    var date3 = weeklyCell.CreateAttribute("Date");
    date3.DataType = eSpace.DateType;
    date3.SetDefaultValue(null); // default value
    date3.Description = null; // default value
    date3.IsMandatory = false; // default value
    date3.Label = "Date";
    date3.NameInJSON = null; // default value
    
    var timeEntryId2 = weeklyCell.CreateAttribute("TimeEntryId");
    timeEntryId2.DataType = timeEntry.IdentifierType;
    timeEntryId2.SetDefaultValue(null); // default value
    timeEntryId2.Description = null; // default value
    timeEntryId2.IsMandatory = false; // default value
    timeEntryId2.Label = "Time Entry";
    timeEntryId2.NameInJSON = null; // default value
    
    var minutes4 = weeklyCell.CreateAttribute("Minutes");
    minutes4.DataType = eSpace.IntegerType;
    minutes4.SetDefaultValue(null); // default value
    minutes4.Description = null; // default value
    minutes4.IsMandatory = false; // default value
    minutes4.Label = "Minutes";
    minutes4.NameInJSON = null; // default value
    
    var isEditable4 = weeklyCell.CreateAttribute("IsEditable");
    isEditable4.DataType = eSpace.BooleanType;
    isEditable4.SetDefaultValue(null); // default value
    isEditable4.Description = null; // default value
    isEditable4.IsMandatory = false; // default value
    isEditable4.Label = "Is Editable";
    isEditable4.NameInJSON = null; // default value
    
    /*** creating IStructure 'WeeklyHours' ***/
    var weeklyHours = eSpace.CreateStructure("WeeklyHours");
    weeklyHours.Description = "The weekly grid: day headers, project rows and totals.";
    weeklyHours.Folder = null; // default value
    weeklyHours.Public = false; // default value
    weeklyHours.Shareable = false; // default value
    
    var weekStart = weeklyHours.CreateAttribute("WeekStart");
    weekStart.DataType = eSpace.DateType;
    weekStart.SetDefaultValue(null); // default value
    weekStart.Description = null; // default value
    weekStart.IsMandatory = false; // default value
    weekStart.Label = "Week Start";
    weekStart.NameInJSON = null; // default value
    
    var weekEnd = weeklyHours.CreateAttribute("WeekEnd");
    weekEnd.DataType = eSpace.DateType;
    weekEnd.SetDefaultValue(null); // default value
    weekEnd.Description = null; // default value
    weekEnd.IsMandatory = false; // default value
    weekEnd.Label = "Week End";
    weekEnd.NameInJSON = null; // default value
    
    var days = weeklyHours.CreateAttribute("Days");
    days.DataType = eSpace.GetOrCreateListType(weekDayHeader);
    days.Description = null; // default value
    days.IsMandatory = false; // default value
    days.Label = "Days";
    days.NameInJSON = null; // default value
    
    var rows = weeklyHours.CreateAttribute("Rows");
    rows.Description = null; // default value
    rows.IsMandatory = false; // default value
    rows.Label = "Rows";
    rows.NameInJSON = null; // default value
    
    var totalMinutes5 = weeklyHours.CreateAttribute("TotalMinutes");
    totalMinutes5.DataType = eSpace.IntegerType;
    totalMinutes5.SetDefaultValue(null); // default value
    totalMinutes5.Description = null; // default value
    totalMinutes5.IsMandatory = false; // default value
    totalMinutes5.Label = "Total Minutes";
    totalMinutes5.NameInJSON = null; // default value
    
    /*** creating IStructure 'WeeklyProjectRow' ***/
    var weeklyProjectRow = eSpace.CreateStructure("WeeklyProjectRow");
    weeklyProjectRow.Description = "One project row of the weekly grid.";
    weeklyProjectRow.Folder = null; // default value
    weeklyProjectRow.Public = false; // default value
    weeklyProjectRow.Shareable = false; // default value
    
    var projectId5 = weeklyProjectRow.CreateAttribute("ProjectId");
    projectId5.DataType = project.IdentifierType;
    projectId5.SetDefaultValue(null); // default value
    projectId5.Description = null; // default value
    projectId5.IsMandatory = false; // default value
    projectId5.Label = "Project";
    projectId5.NameInJSON = null; // default value
    rows.DataType = eSpace.GetOrCreateListType(weeklyProjectRow);
    
    var clientName3 = weeklyProjectRow.CreateAttribute("ClientName");
    clientName3.DataType = eSpace.TextType;
    clientName3.SetDefaultValue(null); // default value
    clientName3.Description = null; // default value
    clientName3.IsMandatory = false; // default value
    clientName3.Label = "Client Name";
    clientName3.Length = 100;
    clientName3.NameInJSON = null; // default value
    
    var projectName3 = weeklyProjectRow.CreateAttribute("ProjectName");
    projectName3.DataType = eSpace.TextType;
    projectName3.SetDefaultValue(null); // default value
    projectName3.Description = null; // default value
    projectName3.IsMandatory = false; // default value
    projectName3.Label = "Project Name";
    projectName3.Length = 100;
    projectName3.NameInJSON = null; // default value
    
    var projectCode3 = weeklyProjectRow.CreateAttribute("ProjectCode");
    projectCode3.DataType = eSpace.TextType;
    projectCode3.SetDefaultValue(null); // default value
    projectCode3.Description = null; // default value
    projectCode3.IsMandatory = false; // default value
    projectCode3.Label = "Project Code";
    projectCode3.Length = 30;
    projectCode3.NameInJSON = null; // default value
    
    var cells = weeklyProjectRow.CreateAttribute("Cells");
    cells.DataType = eSpace.GetOrCreateListType(weeklyCell);
    cells.Description = null; // default value
    cells.IsMandatory = false; // default value
    cells.Label = "Cells";
    cells.NameInJSON = null; // default value
    
    var totalMinutes6 = weeklyProjectRow.CreateAttribute("TotalMinutes");
    totalMinutes6.DataType = eSpace.IntegerType;
    totalMinutes6.SetDefaultValue(null); // default value
    totalMinutes6.Description = null; // default value
    totalMinutes6.IsMandatory = false; // default value
    totalMinutes6.Label = "Total Minutes";
    totalMinutes6.NameInJSON = null; // default value
    // IStructureSignature OutSystemsCharts/XAxisGridLinesStyling
    var xAxisGridLinesStyling = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*2ecbARfP0EuxBFu1lcvJYQ"));
    // IStructureSignature OutSystemsCharts/XAxisOptionalConfigs
    var xAxisOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*JVC_Qn0eAk24+pFY7LTEvg"));
    // IStructureSignature OutSystemsCharts/XAxisStyling
    var xAxisStyling = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*Q84RaFYnQkaw+8XJozSh6Q"));
    // IStructureSignature OutSystemsCharts/YAxisGridLinesStyling
    var yAxisGridLinesStyling = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*1iCDZ_Vny0WyN3XnHpCm6A"));
    // IStructureSignature OutSystemsCharts/YAxisOptionalConfigs
    var yAxisOptionalConfigs = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*GOJW1amidk2So2VaUKw45w"));
    // IStructureSignature OutSystemsCharts/YAxisStyling
    var yAxisStyling = (OutSystems.Model.Data.IStructureSignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*nI9fPDxZG06RzCgkWOrnUw"));
}
