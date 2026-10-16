(OutSystems.Model.UI.Mobile.IMobileFlow mainFlow) => {
    
    /*** creating IMobileScreen 'MySheet' ***/
    var mySheet = mainFlow.CreateScreen("MySheet");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    mySheet.Widgets.ToList().ForEach(x => x.Delete());
    mySheet.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = mainFlow.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetUser = claudeSheet_CS.Roles.Named("SheetUser");
    mySheet.Roles.Add(sheetUser);
    mySheet.AnonymousAccess = false; // default value
    mySheet.CustomURL = false; // default value
    mySheet.Description = "The consultant\'s timesheet: monthly total, calendar, day status KPIs and weekly project hours, kept in sync. Default screen of the module.";
    mySheet.HorizontalPosition = 400;
    mySheet.Icon = null; // default value
    mySheet.IsDisabled = false; // default value
    mySheet.PageName = null; // default value
    mySheet.Public = false; // default value
    mySheet.Shareable = false; // default value
    mySheet.StyleSheet = "/* MySheet (US5): dark page, 3-column month row, weekly table below. Rules are scoped with :has(.sheet-page) so they stop applying when another screen is shown. */\n:root:has(.sheet-page) { --sheet-bg: #000000; }\nbody:has(.sheet-page), body:has(.sheet-page) .layout, body:has(.sheet-page) .main, body:has(.sheet-page) .header, body:has(.sheet-page) .content { background-color: var(--sheet-bg); color: #FFFFFF; }\nbody:has(.sheet-page) .header { border-bottom: 1px solid var(--sheet-track); box-shadow: none; }\nbody:has(.sheet-page) .app-menu-links a { color: #FFFFFF; }\nbody:has(.sheet-page) .app-menu-links a.active { color: var(--sheet-accent); }\n.sheet-page { display: flex; flex-direction: column; gap: 32px; color: #FFFFFF; }\n.sheet-page__header { display: flex; flex-direction: column; gap: 4px; }\n.sheet-page__title { margin: 0; color: #FFFFFF; }\n.sheet-page__subtitle { margin: 0; color: #FFFFFF; font-weight: 700; }\n.sheet-page__top { display: grid; grid-template-columns: minmax(0, 1fr) minmax(0, 1.8fr) minmax(0, 1fr); align-items: center; gap: 32px; }\n.sheet-page__top > *, .sheet-page__week { min-width: 0; }\n/* Tablet: Total + Calendar on row 1, KPIs (2x2) on a full-width row below. */\n@media (max-width: 1024px) { .sheet-page__top { grid-template-columns: minmax(0, 1fr) minmax(0, 1.8fr); } .sheet-page__kpis { grid-column: 1 / -1; } }\n/* Phone: one column, Total, Calendar, KPIs, Weekly. */\n@media (max-width: 767px) { .sheet-page__top { grid-template-columns: minmax(0, 1fr); } }";
    mySheet.SetTitle("\"My Sheet\"");
    mySheet.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    mySheet.VerticalPosition = 400;
    
    var selectedYear = mySheet.CreateLocalVariable("SelectedYear");
    selectedYear.DataType = eSpace.IntegerType;
    selectedYear.SetDefaultValue(null); // default value
    selectedYear.Description = "Year shown by the total, calendar and KPIs. Set in OnInitialize and by OnMonthChanged_HoursCalendar.";
    
    var selectedMonth = mySheet.CreateLocalVariable("SelectedMonth");
    selectedMonth.DataType = eSpace.IntegerType;
    selectedMonth.SetDefaultValue(null); // default value
    selectedMonth.Description = "Month (1-12) shown by the total, calendar and KPIs. Set in OnInitialize and by OnMonthChanged_HoursCalendar.";
    
    var weekReferenceDate = mySheet.CreateLocalVariable("WeekReferenceDate");
    weekReferenceDate.DataType = eSpace.DateType;
    weekReferenceDate.SetDefaultValue(null); // default value
    weekReferenceDate.Description = "A date in the week shown by WeeklyProjectHours. Today at start; follows the selected month, the selected day and the week arrows.";
    
    var refreshKey = mySheet.CreateLocalVariable("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Incremented after a day is saved so every block reloads.";
    mySheet.OnDestroy.Destination = null; // default value
    mySheet.OnReady.Destination = null; // default value
    mySheet.OnRender.Destination = null; // default value
    
    var onDaySaved_HoursCalendar = mySheet.CreateScreenAction("OnDaySaved_HoursCalendar");
    onDaySaved_HoursCalendar.Description = "A day was saved in the calendar: increments RefreshKey so all four blocks reload.";
    
    var startNode = onDaySaved_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var onInitialize = mySheet.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Starts on the current month and the current week.";
    
    var startNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var assignNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode2, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("SelectedYear", "Year(CurrDate())");
    
    var assignment2 = assignNode.CreateAssignment("SelectedMonth", "Month(CurrDate())");
    
    var assignment3 = assignNode.CreateAssignment("WeekReferenceDate", "CurrDate()");
    
    var endNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    mySheet.OnInitialize.Destination = onInitialize;
    
    var assignNode2 = onDaySaved_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment4 = assignNode2.CreateAssignment("RefreshKey", "RefreshKey + 1");
    
    var endNode2 = onDaySaved_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var onDaySelected_HoursCalendar = mySheet.CreateScreenAction("OnDaySelected_HoursCalendar");
    onDaySelected_HoursCalendar.Description = "A day was clicked in the calendar: the weekly table jumps to that day\'s week.";
    
    var date = onDaySelected_HoursCalendar.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    date.Description = null; // default value
    date.IsMandatory = true; // default value
    
    var startNode3 = onDaySelected_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var assignNode3 = onDaySelected_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment5 = assignNode3.CreateAssignment("WeekReferenceDate", "Date");
    
    var endNode3 = onDaySelected_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode3.IsDisabled = false; // default value
    
    var onMonthChanged_HoursCalendar = mySheet.CreateScreenAction("OnMonthChanged_HoursCalendar");
    onMonthChanged_HoursCalendar.Description = "The calendar moved to another month: the total, calendar and KPIs follow it, and the weekly table shows the current week (current month) or the week of the 1st (other months).";
    
    var year = onMonthChanged_HoursCalendar.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    year.Description = null; // default value
    year.IsMandatory = true; // default value
    
    var month = onMonthChanged_HoursCalendar.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    month.Description = null; // default value
    month.IsMandatory = true; // default value
    
    var startNode4 = onMonthChanged_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var assignNode4 = onMonthChanged_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment6 = assignNode4.CreateAssignment("SelectedYear", "Year");
    
    var assignment7 = assignNode4.CreateAssignment("SelectedMonth", "Month");
    
    var assignment8 = assignNode4.CreateAssignment("WeekReferenceDate", "If(Year = Year(CurrDate()) and Month = Month(CurrDate()), CurrDate(), NewDate(Year, Month, 1))");
    
    var endNode4 = onMonthChanged_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    endNode4.IsDisabled = false; // default value
    
    var onWeekChanged_WeeklyProjectHours = mySheet.CreateScreenAction("OnWeekChanged_WeeklyProjectHours");
    onWeekChanged_WeeklyProjectHours.Description = "The weekly table moved to another week: keeps WeekReferenceDate in step (the month doesn\'t change), so selecting a day later always moves the table.";
    
    var weekStartDate = onWeekChanged_WeeklyProjectHours.CreateInputParameter("WeekStartDate");
    weekStartDate.DataType = eSpace.DateType;
    weekStartDate.Description = null; // default value
    weekStartDate.IsMandatory = true; // default value
    
    var startNode5 = onWeekChanged_WeeklyProjectHours.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var assignNode5 = onWeekChanged_WeeklyProjectHours.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment9 = assignNode5.CreateAssignment("WeekReferenceDate", "WeekStartDate");
    
    var endNode5 = onWeekChanged_WeeklyProjectHours.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    endNode5.IsDisabled = false; // default value
    
    var layoutTopMenuInstance = mySheet.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopMenuInstance.CustomStyle = null; // default value
    layoutTopMenuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    layoutTopMenuInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, "\"sheet-layout\"");
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "0");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var sheetPage = placeholderContentWidget2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("SheetPage");
    sheetPage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    sheetPage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    sheetPage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sheetPage.Animate = false; // default value
    sheetPage.ColumnGap = null; // default value
    sheetPage.ColumnSpan = null; // default value
    sheetPage.CustomStyle = null; // default value
    sheetPage.FlexBasis = null; // default value
    sheetPage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    sheetPage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sheetPage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    sheetPage.Gap = null;
    sheetPage.GridColumns = null; // default value
    sheetPage.GridRows = null; // default value
    sheetPage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    sheetPage.RowGap = null; // default value
    sheetPage.RowSpan = null; // default value
    sheetPage.SetStyle("\"sheet-page\"");
    sheetPage.SetVisible("True"); // default value
    sheetPage.VisibleWasExpression = false; // default value
    sheetPage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    sheetPage.Width = "(fill parent)"; // default value
    
    var pageHeader = sheetPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageHeader");
    pageHeader.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    pageHeader.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    pageHeader.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    pageHeader.Animate = false; // default value
    pageHeader.ColumnGap = null; // default value
    pageHeader.ColumnSpan = null; // default value
    pageHeader.CustomStyle = null; // default value
    pageHeader.FlexBasis = null; // default value
    pageHeader.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    pageHeader.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    pageHeader.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    pageHeader.Gap = null;
    pageHeader.GridColumns = null; // default value
    pageHeader.GridRows = null; // default value
    pageHeader.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    pageHeader.RowGap = null; // default value
    pageHeader.RowSpan = null; // default value
    pageHeader.SetStyle("\"sheet-page__header\"");
    pageHeader.SetVisible("True"); // default value
    pageHeader.VisibleWasExpression = false; // default value
    pageHeader.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    pageHeader.Width = "(fill parent)"; // default value
    
    var advancedHtml = pageHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    advancedHtml.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    advancedHtml.ColumnGap = null; // default value
    advancedHtml.ColumnSpan = null; // default value
    advancedHtml.CustomStyle = null; // default value
    advancedHtml.FlexBasis = null; // default value
    advancedHtml.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    advancedHtml.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    advancedHtml.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    advancedHtml.Gap = null;
    advancedHtml.GridColumns = null; // default value
    advancedHtml.GridRows = null; // default value
    advancedHtml.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    advancedHtml.RowGap = null; // default value
    advancedHtml.RowSpan = null; // default value
    advancedHtml.Tag = "h1";
    advancedHtml.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget = advancedHtml.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "My Sheet";
    
    var extendedProperty = advancedHtml.CreateExtendedProperty();
    extendedProperty.Property = "class";
    extendedProperty.SetValue("\"sheet-page__title\"");
    
    var advancedHtml2 = pageHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    advancedHtml2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    advancedHtml2.ColumnGap = null; // default value
    advancedHtml2.ColumnSpan = null; // default value
    advancedHtml2.CustomStyle = null; // default value
    advancedHtml2.FlexBasis = null; // default value
    advancedHtml2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    advancedHtml2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    advancedHtml2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    advancedHtml2.Gap = null;
    advancedHtml2.GridColumns = null; // default value
    advancedHtml2.GridRows = null; // default value
    advancedHtml2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    advancedHtml2.RowGap = null; // default value
    advancedHtml2.RowSpan = null; // default value
    advancedHtml2.Tag = "h4";
    advancedHtml2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget2 = advancedHtml2.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "What did you work on today?";
    
    var extendedProperty2 = advancedHtml2.CreateExtendedProperty();
    extendedProperty2.Property = "class";
    extendedProperty2.SetValue("\"sheet-page__subtitle\"");
    
    var monthRow = sheetPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthRow");
    monthRow.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    monthRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    monthRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    monthRow.Animate = false; // default value
    monthRow.ColumnGap = null; // default value
    monthRow.ColumnSpan = null; // default value
    monthRow.CustomStyle = null; // default value
    monthRow.FlexBasis = null; // default value
    monthRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    monthRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    monthRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    monthRow.Gap = null;
    monthRow.GridColumns = null; // default value
    monthRow.GridRows = null; // default value
    monthRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    monthRow.RowGap = null; // default value
    monthRow.RowSpan = null; // default value
    monthRow.SetStyle("\"sheet-page__top\"");
    monthRow.SetVisible("True"); // default value
    monthRow.VisibleWasExpression = false; // default value
    monthRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    monthRow.Width = "(fill parent)"; // default value
    
    var totalColumn = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("TotalColumn");
    totalColumn.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    totalColumn.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    totalColumn.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    totalColumn.Animate = false; // default value
    totalColumn.ColumnGap = null; // default value
    totalColumn.ColumnSpan = null; // default value
    totalColumn.CustomStyle = null; // default value
    totalColumn.FlexBasis = null; // default value
    totalColumn.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    totalColumn.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    totalColumn.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    totalColumn.Gap = null;
    totalColumn.GridColumns = null; // default value
    totalColumn.GridRows = null; // default value
    totalColumn.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    totalColumn.RowGap = null; // default value
    totalColumn.RowSpan = null; // default value
    totalColumn.SetStyle("\"sheet-page__total\"");
    totalColumn.SetVisible("True"); // default value
    totalColumn.VisibleWasExpression = false; // default value
    totalColumn.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    totalColumn.Width = "(fill parent)"; // default value
    
    var totalHoursMonthInstance = totalColumn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    totalHoursMonthInstance.CustomStyle = null; // default value
    totalHoursMonthInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var sheetBlocks = eSpace.MobileFlows.Named("SheetBlocks");
    var totalHoursMonth = sheetBlocks.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("TotalHoursMonth");
    totalHoursMonthInstance.SourceBlock = totalHoursMonth;
    var refreshKey2 = totalHoursMonth.InputParameters.Named("RefreshKey");
    totalHoursMonthInstance.SetArgumentValue(refreshKey2, "RefreshKey");
    var year2 = totalHoursMonth.InputParameters.Named("Year");
    totalHoursMonthInstance.SetArgumentValue(year2, "SelectedYear");
    var month2 = totalHoursMonth.InputParameters.Named("Month");
    totalHoursMonthInstance.SetArgumentValue(month2, "SelectedMonth");
    
    var calendarColumn = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("CalendarColumn");
    calendarColumn.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    calendarColumn.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    calendarColumn.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    calendarColumn.Animate = false; // default value
    calendarColumn.ColumnGap = null; // default value
    calendarColumn.ColumnSpan = null; // default value
    calendarColumn.CustomStyle = null; // default value
    calendarColumn.FlexBasis = null; // default value
    calendarColumn.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    calendarColumn.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    calendarColumn.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    calendarColumn.Gap = null;
    calendarColumn.GridColumns = null; // default value
    calendarColumn.GridRows = null; // default value
    calendarColumn.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    calendarColumn.RowGap = null; // default value
    calendarColumn.RowSpan = null; // default value
    calendarColumn.SetStyle("\"sheet-page__calendar\"");
    calendarColumn.SetVisible("True"); // default value
    calendarColumn.VisibleWasExpression = false; // default value
    calendarColumn.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    calendarColumn.Width = "(fill parent)"; // default value
    
    var hoursCalendarInstance = calendarColumn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    hoursCalendarInstance.CustomStyle = null; // default value
    hoursCalendarInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var hoursCalendar = sheetBlocks.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("HoursCalendar");
    hoursCalendarInstance.SourceBlock = hoursCalendar;
    var refreshKey3 = hoursCalendar.InputParameters.Named("RefreshKey");
    hoursCalendarInstance.SetArgumentValue(refreshKey3, "RefreshKey");
    var year3 = hoursCalendar.InputParameters.Named("Year");
    hoursCalendarInstance.SetArgumentValue(year3, "SelectedYear");
    var month3 = hoursCalendar.InputParameters.Named("Month");
    hoursCalendarInstance.SetArgumentValue(month3, "SelectedMonth");
    var eventHandler = hoursCalendarInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnDaySaved");
    eventHandler.Handler = onDaySaved_HoursCalendar;
    var eventHandler2 = hoursCalendarInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnDaySelected");
    eventHandler2.Handler = onDaySelected_HoursCalendar;
    eventHandler2.SetArgumentValue(date, "Date");
    var eventHandler3 = hoursCalendarInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnMonthChanged");
    eventHandler3.Handler = onMonthChanged_HoursCalendar;
    eventHandler3.SetArgumentValue(month, "Month");
    eventHandler3.SetArgumentValue(year, "Year");
    
    var kPIsColumn = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPIsColumn");
    kPIsColumn.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    kPIsColumn.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    kPIsColumn.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    kPIsColumn.Animate = false; // default value
    kPIsColumn.ColumnGap = null; // default value
    kPIsColumn.ColumnSpan = null; // default value
    kPIsColumn.CustomStyle = null; // default value
    kPIsColumn.FlexBasis = null; // default value
    kPIsColumn.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    kPIsColumn.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    kPIsColumn.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    kPIsColumn.Gap = null;
    kPIsColumn.GridColumns = null; // default value
    kPIsColumn.GridRows = null; // default value
    kPIsColumn.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    kPIsColumn.RowGap = null; // default value
    kPIsColumn.RowSpan = null; // default value
    kPIsColumn.SetStyle("\"sheet-page__kpis\"");
    kPIsColumn.SetVisible("True"); // default value
    kPIsColumn.VisibleWasExpression = false; // default value
    kPIsColumn.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    kPIsColumn.Width = "(fill parent)"; // default value
    
    var dayStatusKPIsInstance = kPIsColumn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dayStatusKPIsInstance.CustomStyle = null; // default value
    dayStatusKPIsInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dayStatusKPIs = sheetBlocks.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("DayStatusKPIs");
    dayStatusKPIsInstance.SourceBlock = dayStatusKPIs;
    var year4 = dayStatusKPIs.InputParameters.Named("Year");
    dayStatusKPIsInstance.SetArgumentValue(year4, "SelectedYear");
    var refreshKey4 = dayStatusKPIs.InputParameters.Named("RefreshKey");
    dayStatusKPIsInstance.SetArgumentValue(refreshKey4, "RefreshKey");
    var month4 = dayStatusKPIs.InputParameters.Named("Month");
    dayStatusKPIsInstance.SetArgumentValue(month4, "SelectedMonth");
    
    var weekRow = sheetPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("WeekRow");
    weekRow.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    weekRow.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    weekRow.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    weekRow.Animate = false; // default value
    weekRow.ColumnGap = null; // default value
    weekRow.ColumnSpan = null; // default value
    weekRow.CustomStyle = null; // default value
    weekRow.FlexBasis = null; // default value
    weekRow.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    weekRow.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    weekRow.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    weekRow.Gap = null;
    weekRow.GridColumns = null; // default value
    weekRow.GridRows = null; // default value
    weekRow.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    weekRow.RowGap = null; // default value
    weekRow.RowSpan = null; // default value
    weekRow.SetStyle("\"sheet-page__week\"");
    weekRow.SetVisible("True"); // default value
    weekRow.VisibleWasExpression = false; // default value
    weekRow.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    weekRow.Width = "(fill parent)"; // default value
    
    var weeklyProjectHoursInstance = weekRow.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    weeklyProjectHoursInstance.CustomStyle = null; // default value
    weeklyProjectHoursInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var weeklyProjectHours = sheetBlocks.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("WeeklyProjectHours");
    weeklyProjectHoursInstance.SourceBlock = weeklyProjectHours;
    var referenceDate = weeklyProjectHours.InputParameters.Named("ReferenceDate");
    weeklyProjectHoursInstance.SetArgumentValue(referenceDate, "WeekReferenceDate");
    var refreshKey5 = weeklyProjectHours.InputParameters.Named("RefreshKey");
    weeklyProjectHoursInstance.SetArgumentValue(refreshKey5, "RefreshKey");
    var eventHandler4 = weeklyProjectHoursInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnWeekChanged");
    eventHandler4.Handler = onWeekChanged_WeeklyProjectHours;
    eventHandler4.SetArgumentValue(weekStartDate, "WeekStartDate");
}
