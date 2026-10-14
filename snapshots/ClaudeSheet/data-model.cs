(OutSystems.Model.IReference outSystemsUI) => {
    // IStaticEntitySignature OutSystemsUI/AccordionIconPosition
    var eSpace = outSystemsUI.GetESpace();
    var accordionIconPosition = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*LdtIqGz+8U2MEIvbPC6Uig"));
    // IStaticEntitySignature OutSystemsUI/AccordionIconType
    var accordionIconType = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*3sFcY972ZUu68cZhvFGCZg"));
    // IServerEntitySignature (System)/Activity
    var activity = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*hg460+gk0UGzMRTFlEBTOQ"));
    // IStaticEntitySignature OutSystemsUI/Alert
    var alert = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*wIZEkprd6katdCz5rAyE2Q"));
    // IStaticEntitySignature OutSystemsUI/AnimationType
    var animationType = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*SdRjBDRo1UCBez100acbsg"));
    // IStaticEntitySignature OutSystemsUI/Autoplay
    var autoplay = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*tFiqgruLGU2StilEr_rQLA"));
    // IStaticEntitySignature OutSystemsCharts/AxisValuesType
    var axisValuesType = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*eX10_X1B+k6PiS15u43oNQ"));
    // IStaticEntitySignature OutSystemsUI/BooleanTypes
    var booleanTypes = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*_MiCmNYNLkm2uXpoBZ7MBg"));
    // IStaticEntitySignature OutSystemsUI/BreakColumns
    var breakColumns = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*k0X3aMSlZU6Fi_RCEerxIg"));
    
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
    calendarDayType.IdentifierAttribute = id;
    
    var is_Active = calendarDayType.CreateAttribute("Is_Active");
    is_Active.DataType = eSpace.BooleanType;
    is_Active.SetDefaultValue(null); // default value
    is_Active.Description = null; // default value
    is_Active.IsMandatory = true;
    is_Active.IsSearchable = false; // default value
    is_Active.Label = "Is Active";
    is_Active.OriginalName = null; // default value
    is_Active.OriginalType = null; // default value
    calendarDayType.IsActiveAttribute = is_Active;
    
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
    calendarDayType.LabelAttribute = label;
    
    var order = calendarDayType.CreateAttribute("Order");
    order.DataType = eSpace.IntegerType;
    order.SetDefaultValue(null); // default value
    order.Description = null; // default value
    order.IsMandatory = true;
    order.IsSearchable = false; // default value
    order.Label = "Order";
    order.OriginalName = null; // default value
    order.OriginalType = null; // default value
    calendarDayType.OrderByAttribute = order;
    
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
    
    var staticEntityRecord = calendarDayType.CreateRecord();
    staticEntityRecord.Icon = null; // default value
    staticEntityRecord.Identifier = "Pending";
    staticEntityRecord.SetAttributeValue(pluralLabel, "\"Pending days\"");
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
    staticEntityRecord5.SetAttributeValue(pluralLabel, "\"Confirmed days\"");
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
    // IStaticEntitySignature OutSystemsUI/CarouselDirection
    var carouselDirection = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*ZuLLccX3+0K+rna8Ml2Z6g"));
    // IStaticEntitySignature OutSystemsUI/CarouselNavigation
    var carouselNavigation = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*ye867jfk+0mGogwtBlbtQw"));
    
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
    
    var id2 = client.CreateAttribute("Id");
    id2.DataType = eSpace.LongIntegerType;
    id2.SetDefaultValue(null); // default value
    id2.Description = null; // default value
    id2.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id2.IsSearchable = false; // default value
    id2.Label = "Id";
    id2.OriginalName = null; // default value
    id2.OriginalType = null; // default value
    client.IdentifierAttribute = id2;
    
    var name = client.CreateAttribute("Name");
    name.DataType = eSpace.TextType;
    name.SetDefaultValue(null); // default value
    name.Description = null; // default value
    name.IsMandatory = true;
    name.IsSearchable = false; // default value
    name.Label = "Name";
    name.Length = 100;
    name.OriginalName = null; // default value
    name.OriginalType = null; // default value
    client.LabelAttribute = name;
    
    var isActive = client.CreateAttribute("IsActive");
    isActive.DataType = eSpace.BooleanType;
    isActive.SetDefaultValue("True");
    isActive.Description = null; // default value
    isActive.IsMandatory = true;
    isActive.IsSearchable = false; // default value
    isActive.Label = "Is Active";
    isActive.OriginalName = null; // default value
    isActive.OriginalType = null; // default value
    // IStaticEntitySignature OutSystemsUI/Color
    var color = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*px1ty9Qm2UW8IppMSC5s4g"));
    // IStaticEntitySignature OutSystemsUI/ColumnBreak
    var columnBreak = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*IazmzCqUL0mLRmTF5uoFew"));
    // IStaticEntitySignature OutSystemsUI/DatePickerLanguage
    var datePickerLanguage = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*5C5ARb9zokK1+Dca6UC_Tw"));
    // IStaticEntitySignature OutSystemsUI/DatePickerTimeFormat
    var datePickerTimeFormat = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*GkvlVf4O70uuEMJ2kSljTQ"));
    // IStaticEntitySignature OutSystemsUI/DatePickerWeekDay
    var datePickerWeekDay = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*utBO_bkaPkqisRj0F5O+LQ"));
    
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
    
    var id3 = daySheet.CreateAttribute("Id");
    id3.DataType = eSpace.LongIntegerType;
    id3.SetDefaultValue(null); // default value
    id3.Description = null; // default value
    id3.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id3.IsSearchable = false; // default value
    id3.Label = "Id";
    id3.OriginalName = null; // default value
    id3.OriginalType = null; // default value
    daySheet.IdentifierAttribute = id3;
    
    var userId = daySheet.CreateAttribute("UserId");
    userId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    userId.Description = null; // default value
    userId.IsMandatory = true;
    userId.IsSearchable = false; // default value
    userId.Label = "User";
    userId.OriginalName = null; // default value
    userId.OriginalType = null; // default value
    // IServerEntitySignature (System)/User
    var user = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*LYIWpF+Ey0aicHD144NUTA"));
    userId.DataType = user.IdentifierType;
    
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
    
    var id4 = dayStatus.CreateAttribute("Id");
    id4.DataType = eSpace.IntegerType;
    id4.SetDefaultValue(null); // default value
    id4.Description = null; // default value
    id4.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.No; // default value
    id4.IsSearchable = false; // default value
    id4.Label = "Id";
    id4.OriginalName = null; // default value
    id4.OriginalType = null; // default value
    
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
    staticEntityRecord7.SetAttributeValue(id4, "1");
    staticEntityRecord7.SetAttributeValue(order2, "1");
    staticEntityRecord7.SetAttributeValue(label2, "\"Draft\"");
    
    var staticEntityRecord8 = dayStatus.CreateRecord();
    staticEntityRecord8.Icon = null; // default value
    staticEntityRecord8.Identifier = "DayOff";
    staticEntityRecord8.SetAttributeValue(order2, "3");
    staticEntityRecord8.SetAttributeValue(id4, "3");
    staticEntityRecord8.SetAttributeValue(label2, "\"Day off\"");
    staticEntityRecord8.SetAttributeValue(is_Active2, "True");
    
    var staticEntityRecord9 = dayStatus.CreateRecord();
    staticEntityRecord9.Icon = null; // default value
    staticEntityRecord9.Identifier = "Confirmed";
    staticEntityRecord9.SetAttributeValue(is_Active2, "True");
    staticEntityRecord9.SetAttributeValue(order2, "2");
    staticEntityRecord9.SetAttributeValue(id4, "2");
    staticEntityRecord9.SetAttributeValue(label2, "\"Confirmed\"");
    dayStatus.IdentifierAttribute = id4;
    dayStatus.IsActiveAttribute = is_Active2;
    dayStatus.LabelAttribute = label2;
    dayStatus.OrderByAttribute = order2;
    
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
    dayStatusId.DataType = dayStatus.IdentifierType;
    
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
    // IStaticEntitySignature OutSystemsUI/DeviceResponsive
    var deviceResponsive = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*HY5GKVqiw0K_U7IyrMwVDQ"));
    // IStaticEntitySignature OutSystemsUI/Direction
    var direction = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*hoBiZ4t_hE+peKnT9UeWnA"));
    // IServerEntitySignature (System)/Espace
    var espace = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*Peat6jeAoS7k0hgUWu_7Nw"));
    // IStaticEntitySignature OutSystemsUI/FloatingPosition
    var floatingPosition = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*KotrkgYJb06O9eGP3OS1rw"));
    // IStaticEntitySignature OutSystemsUI/Gradient
    var gradient = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*XrX0FejJDU+gQwCT4X+gTQ"));
    // IServerEntitySignature (System)/Group
    var group = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*77Uo0HdEN06fos3dfg5xRg"));
    // IStaticEntitySignature OutSystemsUI/GutterSize
    var gutterSize = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*6KGBLqRP0EupRWU1KZmwvg"));
    // IStaticEntitySignature OutSystemsCharts/HighchartModules
    var highchartModules = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*xdGASEDTGECq_FfOVwaSLg"));
    
    /*** creating IServerEntity 'Holiday' ***/
    var holiday = eSpace.CreateServerEntity("Holiday");
    holiday.Description = "Public holiday. Holidays are not editable in the calendar.";
    holiday.ExposeProcessEvents = false; // default value
    holiday.Folder = null; // default value
    holiday.IsActiveAttribute = null; // default value
    holiday.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    holiday.IsRestrictedInQueries = false; // default value
    holiday.Label = "Holiday";
    holiday.LabelPlural = "Holidays";
    holiday.OrderByAttribute = null; // default value
    holiday.Public = false; // default value
    holiday.Shareable = false; // default value
    holiday.TableNameOverride = null; // default value
    holiday.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full; // default value
    holiday.UseTranslations = false; // default value
    holiday.ViewNameOverride = null; // default value
    
    var id5 = holiday.CreateAttribute("Id");
    id5.DataType = eSpace.LongIntegerType;
    id5.SetDefaultValue(null); // default value
    id5.Description = null; // default value
    id5.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id5.IsSearchable = false; // default value
    id5.Label = "Id";
    id5.OriginalName = null; // default value
    id5.OriginalType = null; // default value
    holiday.IdentifierAttribute = id5;
    
    var name2 = holiday.CreateAttribute("Name");
    name2.DataType = eSpace.TextType;
    name2.SetDefaultValue(null); // default value
    name2.Description = null; // default value
    name2.IsMandatory = true;
    name2.IsSearchable = false; // default value
    name2.Label = "Name";
    name2.Length = 100;
    name2.OriginalName = null; // default value
    name2.OriginalType = null; // default value
    holiday.LabelAttribute = name2;
    
    var holidayDate = holiday.CreateAttribute("HolidayDate");
    holidayDate.DataType = eSpace.DateType;
    holidayDate.SetDefaultValue(null); // default value
    holidayDate.Description = null; // default value
    holidayDate.IsMandatory = true;
    holidayDate.IsSearchable = false; // default value
    holidayDate.Label = "Holiday Date";
    holidayDate.OriginalName = null; // default value
    holidayDate.OriginalType = null; // default value
    
    var isActive2 = holiday.CreateAttribute("IsActive");
    isActive2.DataType = eSpace.BooleanType;
    isActive2.SetDefaultValue("True");
    isActive2.Description = null; // default value
    isActive2.IsMandatory = true;
    isActive2.IsSearchable = false; // default value
    isActive2.Label = "Is Active";
    isActive2.OriginalName = null; // default value
    isActive2.OriginalType = null; // default value
    // IStaticEntitySignature OutSystemsCharts/LegendLayout
    var legendLayout = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*pW0A8z8iV0CJSolm4Bujnw"));
    // IStaticEntitySignature OutSystemsCharts/LegendPosition
    var legendPosition = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*l3rhqgo_y0GJN+Sdc3qXcQ"));
    // IStaticEntitySignature OutSystemsMaps/MapErrors
    var mapErrors = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*5BXgClPJM06uwJFnTQXL9Q"));
    // IStaticEntitySignature OutSystemsMaps/MapEventTriggered
    var mapEventTriggered = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*ZKnyfPheAUmr5uhMtmFqYg"));
    // IStaticEntitySignature OutSystemsMaps/MarkerEventTriggered
    var markerEventTriggered = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*shJYz8m9sk6CQAn2Nnxq1g"));
    // IStaticEntitySignature OutSystemsUI/MenuAction
    var menuAction = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*gyjBnKsG8EyZl+3nxsAtZw"));
    // IStaticEntitySignature OutSystemsUI/MessageStatus
    var messageStatus = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*wF8BwRzIzECgRr+ZzyGigA"));
    // IStaticEntitySignature OutSystemsUI/Month
    var month = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*JLfZM7GgCkuR+ychQ8RKhQ"));
    // IStaticEntitySignature OutSystemsUI/Orientation
    var orientation = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*JPhxTWOlY0+MdmJuTgvd2w"));
    // IStaticEntitySignature OutSystemsUI/Position
    var position = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*6HPAg8K6IkGXcqpuEipdNg"));
    // IServerEntitySignature (System)/Process
    var process = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*rk1aJNDvckKT91HxgCaO6w"));
    
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
    
    var id6 = project.CreateAttribute("Id");
    id6.DataType = eSpace.LongIntegerType;
    id6.SetDefaultValue(null); // default value
    id6.Description = null; // default value
    id6.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id6.IsSearchable = false; // default value
    id6.Label = "Id";
    id6.OriginalName = null; // default value
    id6.OriginalType = null; // default value
    project.IdentifierAttribute = id6;
    
    var name3 = project.CreateAttribute("Name");
    name3.DataType = eSpace.TextType;
    name3.SetDefaultValue(null); // default value
    name3.Description = null; // default value
    name3.IsMandatory = true;
    name3.IsSearchable = false; // default value
    name3.Label = "Name";
    name3.Length = 100;
    name3.OriginalName = null; // default value
    name3.OriginalType = null; // default value
    project.LabelAttribute = name3;
    
    var clientId = project.CreateAttribute("ClientId");
    clientId.DataType = client.IdentifierType;
    clientId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect; // default value
    clientId.Description = null; // default value
    clientId.IsMandatory = true;
    clientId.IsSearchable = false; // default value
    clientId.Label = "Client";
    clientId.OriginalName = null; // default value
    clientId.OriginalType = null; // default value
    
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
    
    var isActive3 = project.CreateAttribute("IsActive");
    isActive3.DataType = eSpace.BooleanType;
    isActive3.SetDefaultValue("True");
    isActive3.Description = null; // default value
    isActive3.IsMandatory = true;
    isActive3.IsSearchable = false; // default value
    isActive3.Label = "Is Active";
    isActive3.OriginalName = null; // default value
    isActive3.OriginalType = null; // default value
    
    /*** creating IServerEntity 'ProjectAssignment' ***/
    var projectAssignment = eSpace.CreateServerEntity("ProjectAssignment");
    projectAssignment.Description = "A user\'s assignment to a project for a date range. A user can have several periods on the same project (one per StartDate); periods are not expected to overlap.";
    projectAssignment.ExposeProcessEvents = false; // default value
    projectAssignment.Folder = null; // default value
    projectAssignment.IsActiveAttribute = null; // default value
    projectAssignment.IsMultiTenant = OutSystems.Model.Enumerations.BooleanWithInheritance.Inherited; // default value
    projectAssignment.IsRestrictedInQueries = false; // default value
    projectAssignment.Label = "Project Assignment";
    projectAssignment.LabelAttribute = null; // default value
    projectAssignment.LabelPlural = "Project Assignments";
    projectAssignment.OrderByAttribute = null; // default value
    projectAssignment.Public = false; // default value
    projectAssignment.Shareable = false; // default value
    projectAssignment.TableNameOverride = null; // default value
    projectAssignment.UpdateBehavior = OutSystems.Model.Enumerations.UpdateEntityBehaviorType.Full; // default value
    projectAssignment.UseTranslations = false; // default value
    projectAssignment.ViewNameOverride = null; // default value
    
    var id7 = projectAssignment.CreateAttribute("Id");
    id7.DataType = eSpace.LongIntegerType;
    id7.SetDefaultValue(null); // default value
    id7.Description = null; // default value
    id7.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id7.IsSearchable = false; // default value
    id7.Label = "Id";
    id7.OriginalName = null; // default value
    id7.OriginalType = null; // default value
    projectAssignment.IdentifierAttribute = id7;
    
    var userId2 = projectAssignment.CreateAttribute("UserId");
    userId2.DataType = user.IdentifierType;
    userId2.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    userId2.Description = null; // default value
    userId2.IsMandatory = true;
    userId2.IsSearchable = false; // default value
    userId2.Label = "User";
    userId2.OriginalName = null; // default value
    userId2.OriginalType = null; // default value
    
    var projectId = projectAssignment.CreateAttribute("ProjectId");
    projectId.DataType = project.IdentifierType;
    projectId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    projectId.Description = null; // default value
    projectId.IsMandatory = true;
    projectId.IsSearchable = false; // default value
    projectId.Label = "Project";
    projectId.OriginalName = null; // default value
    projectId.OriginalType = null; // default value
    
    var startDate = projectAssignment.CreateAttribute("StartDate");
    startDate.DataType = eSpace.DateType;
    startDate.SetDefaultValue(null); // default value
    startDate.Description = null; // default value
    startDate.IsMandatory = true;
    startDate.IsSearchable = false; // default value
    startDate.Label = "Start Date";
    startDate.OriginalName = null; // default value
    startDate.OriginalType = null; // default value
    
    var endDate = projectAssignment.CreateAttribute("EndDate");
    endDate.DataType = eSpace.DateType;
    endDate.SetDefaultValue(null); // default value
    endDate.Description = null; // default value
    endDate.IsMandatory = false; // default value
    endDate.IsSearchable = false; // default value
    endDate.Label = "End Date";
    endDate.OriginalName = null; // default value
    endDate.OriginalType = null; // default value
    
    var isActive4 = projectAssignment.CreateAttribute("IsActive");
    isActive4.DataType = eSpace.BooleanType;
    isActive4.SetDefaultValue("True");
    isActive4.Description = null; // default value
    isActive4.IsMandatory = true;
    isActive4.IsSearchable = false; // default value
    isActive4.Label = "Is Active";
    isActive4.OriginalName = null; // default value
    isActive4.OriginalType = null; // default value
    // IServerEntitySignature (System)/Role
    var role = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*NuqV5Rtc2D6TqrunUsf4kg"));
    // IStaticEntitySignature (System)/RuntimeKind
    var runtimeKind = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*64ECB3l5wUuxUhWl1Ubk4A"));
    // IStaticEntitySignature OutSystemsUI/ScrollbarStyle
    var scrollbarStyle = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*6t_Ww6JtikaA5FzmaNQ_cA"));
    // IServerEntitySignature (System)/Sent_Email
    var sent_Email = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*CWv+3dcKHk6ctXgJ81F2tg"));
    // IStaticEntitySignature OutSystemsCharts/SeriesType
    var seriesType = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*6GPGqVRjAk+THGNvxYt9UQ"));
    // IStaticEntitySignature OutSystemsUI/Shape
    var shape = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*qWjhb23B20ybb6nmxreTJg"));
    // IStaticEntitySignature OutSystemsUI/SideMenuBehavior
    var sideMenuBehavior = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*e5OlEU3pUEKZqJyDWNPZZQ"));
    // IStaticEntitySignature OutSystemsUI/Size
    var size = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*tBDBGmSJC0emsoGuSmtb3g"));
    // IStaticEntitySignature OutSystemsUI/Space
    var space = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*cdSzj6SCnEOcwQVV36kUUQ"));
    // IStaticEntitySignature OutSystemsUI/Speed
    var speed = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*rsRcCk+1o06azp9Jx3JGBg"));
    // IStaticEntitySignature OutSystemsUI/StackedCardsPosition
    var stackedCardsPosition = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*tF6Gcf5VpUC8f0WwBaOgtQ"));
    // IStaticEntitySignature OutSystemsCharts/StackingType
    var stackingType = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*MtWj9wu4+EyoihsTikMiZg"));
    // IStaticEntitySignature OutSystemsUI/Steps
    var steps = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*n47d5CCm9U22GZuKdxvlow"));
    // IStaticEntitySignature OutSystemsUI/StepsBehaviour
    var stepsBehaviour = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*nuMDocxDtUiF1wAzZJFSKQ"));
    // IStaticEntitySignature OutSystemsMaps/Style
    var style = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*O7itoAjiOUC8kpHQG44ggQ"));
    // IServerEntitySignature (System)/Tenant
    var tenant = (OutSystems.Model.Data.IServerEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("uXCIR2Atc0+es3zYuZSnNw*WQMM07oXpk63b_WESl+a5Q"));
    
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
    
    var id8 = timeEntry.CreateAttribute("Id");
    id8.DataType = eSpace.LongIntegerType;
    id8.SetDefaultValue(null); // default value
    id8.Description = null; // default value
    id8.IsAutoNumber = OutSystems.Model.Enumerations.AutoNumber.Yes;
    id8.IsSearchable = false; // default value
    id8.Label = "Id";
    id8.OriginalName = null; // default value
    id8.OriginalType = null; // default value
    timeEntry.IdentifierAttribute = id8;
    
    var daySheetId = timeEntry.CreateAttribute("DaySheetId");
    daySheetId.DataType = daySheet.IdentifierType;
    daySheetId.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Delete;
    daySheetId.Description = null; // default value
    daySheetId.IsMandatory = true;
    daySheetId.IsSearchable = false; // default value
    daySheetId.Label = "Day Sheet";
    daySheetId.OriginalName = null; // default value
    daySheetId.OriginalType = null; // default value
    
    var projectId2 = timeEntry.CreateAttribute("ProjectId");
    projectId2.DataType = project.IdentifierType;
    projectId2.DeleteRule = OutSystems.Model.Enumerations.DeleteRule.Protect; // default value
    projectId2.Description = null; // default value
    projectId2.IsMandatory = true;
    projectId2.IsSearchable = false; // default value
    projectId2.Label = "Project";
    projectId2.OriginalName = null; // default value
    projectId2.OriginalType = null; // default value
    
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
    
    var description = timeEntry.CreateAttribute("Description");
    description.DataType = eSpace.TextType;
    description.SetDefaultValue(null); // default value
    description.Description = null; // default value
    description.IsMandatory = false; // default value
    description.IsSearchable = false; // default value
    description.Label = "Description";
    description.Length = 250;
    description.OriginalName = null; // default value
    description.OriginalType = null; // default value
    
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
    // IStaticEntitySignature OutSystemsUI/Trigger
    var trigger = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*uhIJjybGlUeQplMR4rlwyw"));
    // IStaticEntitySignature OutSystemsMaps/Type
    var type = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*w2QUMibLL0G2ChxxQNyMGw"));
    // IStaticEntitySignature OutSystemsCharts/ValuesType
    var valuesType = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Iw63OPxQEEeAzzaCqdyZig*cCFW0FbPSkOF77jTPyAj8g"));
    // IStaticEntitySignature OutSystemsUI/VideoState
    var videoState = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("Kn_hixxDWEm4lMd7mIpycQ*N49Q8acBbUWd0GTf9i9tOQ"));
    // IStaticEntitySignature OutSystemsMaps/Zoom
    var zoom = (OutSystems.Model.Data.IStaticEntitySignature)eSpace.AddDependency(Services.ModelServices.ParseGlobalKey("0TG7lXnw1k+rLlyDJoVaqg*fPCs5IqzcUinwkAX6qvpXA"));
}
