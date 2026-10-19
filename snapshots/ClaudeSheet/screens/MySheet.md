# MySheet

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
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
    mySheet.Description = "The consultant\'s timesheet: monthly total, calendar, day status KPIs and weekly project hours, kept in sync. Default screen of the module.";
    mySheet.HorizontalPosition = 400;
    mySheet.StyleSheet = "/* MySheet (US5): dark page, 3-column month row, weekly table below. Rules are scoped with :has(.sheet-page) so they stop applying when another screen is shown. */\n:root:has(.sheet-page) { --sheet-bg: #000000; }\nbody:has(.sheet-page), body:has(.sheet-page) .layout, body:has(.sheet-page) .main, body:has(.sheet-page) .header, body:has(.sheet-page) .content { background-color: var(--sheet-bg); color: #FFFFFF; }\nbody:has(.sheet-page) .header { border-bottom: 1px solid var(--sheet-track); box-shadow: none; }\nbody:has(.sheet-page) .app-menu-links a { color: #FFFFFF; }\nbody:has(.sheet-page) .app-menu-links a.active { color: var(--sheet-accent); }\n.sheet-page { display: flex; flex-direction: column; gap: 32px; color: #FFFFFF; }\n.sheet-page__header { display: flex; flex-direction: column; gap: 4px; }\n.sheet-page__title { margin: 0; color: #FFFFFF; }\n.sheet-page__subtitle { margin: 0; color: #FFFFFF; font-weight: 700; }\n.sheet-page__top { display: grid; grid-template-columns: minmax(0, 1fr) minmax(0, 1.8fr) minmax(0, 1fr); align-items: center; gap: 32px; }\n.sheet-page__top > *, .sheet-page__week { min-width: 0; }\n/* Tablet: Total + Calendar on row 1, KPIs (2x2) on a full-width row below. */\n@media (max-width: 1024px) { .sheet-page__top { grid-template-columns: minmax(0, 1fr) minmax(0, 1.8fr); } .sheet-page__kpis { grid-column: 1 / -1; } }\n/* Phone: one column, Total, Calendar, KPIs, Weekly. */\n@media (max-width: 767px) { .sheet-page__top { grid-template-columns: minmax(0, 1fr); } }";
    mySheet.SetTitle("\"My Sheet\"");
    mySheet.VerticalPosition = 400;
    
    var selectedYear = mySheet.CreateLocalVariable("SelectedYear");
    selectedYear.DataType = eSpace.IntegerType;
    selectedYear.Description = "Year shown by the total, calendar and KPIs. Set in OnInitialize and by OnMonthChanged_HoursCalendar.";
    
    var selectedMonth = mySheet.CreateLocalVariable("SelectedMonth");
    selectedMonth.DataType = eSpace.IntegerType;
    selectedMonth.Description = "Month (1-12) shown by the total, calendar and KPIs. Set in OnInitialize and by OnMonthChanged_HoursCalendar.";
    
    var weekReferenceDate = mySheet.CreateLocalVariable("WeekReferenceDate");
    weekReferenceDate.DataType = eSpace.DateType;
    weekReferenceDate.Description = "A date in the week shown by WeeklyProjectHours. Today at start; follows the selected month, the selected day and the week arrows.";
    
    var refreshKey = mySheet.CreateLocalVariable("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Incremented after a day is saved so every block reloads.";
    
    var onDaySaved_HoursCalendar = mySheet.CreateScreenAction("OnDaySaved_HoursCalendar");
    onDaySaved_HoursCalendar.Description = "A day was saved in the calendar: increments RefreshKey so all four blocks reload.";
    
    var startNode = onDaySaved_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.VerticalPosition = 800;
    
    var onInitialize = mySheet.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Starts on the current month and the current week.";
    
    var startNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.VerticalPosition = 800;
    
    var assignNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode2, 1371);
    assignNode.Label = "Current month";
    
    var assignment = assignNode.CreateAssignment("SelectedYear", "Year(CurrDate())");
    
    var assignment2 = assignNode.CreateAssignment("SelectedMonth", "Month(CurrDate())");
    
    var assignment3 = assignNode.CreateAssignment("WeekReferenceDate", "CurrDate()");
    
    var endNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    mySheet.OnInitialize.Destination = onInitialize;
    
    var assignNode2 = onDaySaved_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode, 1371);
    assignNode2.Label = "Refresh blocks";
    
    var assignment4 = assignNode2.CreateAssignment("RefreshKey", "RefreshKey + 1");
    
    var endNode2 = onDaySaved_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    
    var onDaySelected_HoursCalendar = mySheet.CreateScreenAction("OnDaySelected_HoursCalendar");
    onDaySelected_HoursCalendar.Description = "A day was clicked in the calendar: the weekly table jumps to that day\'s week.";
    
    var date = onDaySelected_HoursCalendar.CreateInputParameter("Date");
    date.DataType = eSpace.DateType;
    
    var startNode3 = onDaySelected_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.VerticalPosition = 800;
    
    var assignNode3 = onDaySelected_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode3.Label = "Set week date";
    
    var assignment5 = assignNode3.CreateAssignment("WeekReferenceDate", "Date");
    
    var endNode3 = onDaySelected_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    
    var onMonthChanged_HoursCalendar = mySheet.CreateScreenAction("OnMonthChanged_HoursCalendar");
    onMonthChanged_HoursCalendar.Description = "The calendar moved to another month: the total, calendar and KPIs follow it, and the weekly table shows the current week (current month) or the week of the 1st (other months).";
    
    var year = onMonthChanged_HoursCalendar.CreateInputParameter("Year");
    year.DataType = eSpace.IntegerType;
    
    var month = onMonthChanged_HoursCalendar.CreateInputParameter("Month");
    month.DataType = eSpace.IntegerType;
    
    var startNode4 = onMonthChanged_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.VerticalPosition = 800;
    
    var assignNode4 = onMonthChanged_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode4.Label = "Set month";
    
    var assignment6 = assignNode4.CreateAssignment("SelectedYear", "Year");
    
    var assignment7 = assignNode4.CreateAssignment("SelectedMonth", "Month");
    
    var assignment8 = assignNode4.CreateAssignment("WeekReferenceDate", "If(Year = Year(CurrDate()) and Month = Month(CurrDate()), CurrDate(), NewDate(Year, Month, 1))");
    
    var endNode4 = onMonthChanged_HoursCalendar.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode4, 1371);
    
    var onWeekChanged_WeeklyProjectHours = mySheet.CreateScreenAction("OnWeekChanged_WeeklyProjectHours");
    onWeekChanged_WeeklyProjectHours.Description = "The weekly table moved to another week: keeps WeekReferenceDate in step (the month doesn\'t change), so selecting a day later always moves the table.";
    
    var weekStartDate = onWeekChanged_WeeklyProjectHours.CreateInputParameter("WeekStartDate");
    weekStartDate.DataType = eSpace.DateType;
    
    var startNode5 = onWeekChanged_WeeklyProjectHours.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.VerticalPosition = 800;
    
    var assignNode5 = onWeekChanged_WeeklyProjectHours.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode5.Label = "Set week date";
    
    var assignment9 = assignNode5.CreateAssignment("WeekReferenceDate", "WeekStartDate");
    
    var endNode5 = onWeekChanged_WeeklyProjectHours.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode5, 1371);
    
    var layoutTopMenuInstance = mySheet.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, "\"sheet-layout\"");
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeItem = menu.InputParameters.Named("ActiveItem");
    menuInstance.SetArgumentValue(activeItem, "0");
    var activeSubItem = menu.InputParameters.Named("ActiveSubItem");
    menuInstance.SetArgumentValue(activeSubItem, null);
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var sheetPage = placeholderContentWidget2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("SheetPage");
    sheetPage.Gap = null;
    sheetPage.SetStyle("\"sheet-page\"");
    
    var pageHeader = sheetPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PageHeader");
    pageHeader.Gap = null;
    pageHeader.SetStyle("\"sheet-page__header\"");
    
    var advancedHtml = pageHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.Gap = null;
    advancedHtml.Tag = "h1";
    
    var textWidget = advancedHtml.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.Text = "My Sheet";
    
    var extendedProperty = advancedHtml.CreateExtendedProperty();
    extendedProperty.Property = "class";
    extendedProperty.SetValue("\"sheet-page__title\"");
    
    var advancedHtml2 = pageHeader.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml2.Gap = null;
    advancedHtml2.Tag = "h4";
    
    var textWidget2 = advancedHtml2.Content.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.Text = "What did you work on today?";
    
    var extendedProperty2 = advancedHtml2.CreateExtendedProperty();
    extendedProperty2.Property = "class";
    extendedProperty2.SetValue("\"sheet-page__subtitle\"");
    
    var monthRow = sheetPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MonthRow");
    monthRow.Gap = null;
    monthRow.SetStyle("\"sheet-page__top\"");
    
    var totalColumn = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("TotalColumn");
    totalColumn.Gap = null;
    totalColumn.SetStyle("\"sheet-page__total\"");
    
    var totalHoursMonthInstance = totalColumn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    calendarColumn.Gap = null;
    calendarColumn.SetStyle("\"sheet-page__calendar\"");
    
    var hoursCalendarInstance = calendarColumn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    eventHandler3.SetArgumentValue(year, "Year");
    eventHandler3.SetArgumentValue(month, "Month");
    
    var kPIsColumn = monthRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("KPIsColumn");
    kPIsColumn.Gap = null;
    kPIsColumn.SetStyle("\"sheet-page__kpis\"");
    
    var dayStatusKPIsInstance = kPIsColumn.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var dayStatusKPIs = sheetBlocks.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("DayStatusKPIs");
    dayStatusKPIsInstance.SourceBlock = dayStatusKPIs;
    var year4 = dayStatusKPIs.InputParameters.Named("Year");
    dayStatusKPIsInstance.SetArgumentValue(year4, "SelectedYear");
    var refreshKey4 = dayStatusKPIs.InputParameters.Named("RefreshKey");
    dayStatusKPIsInstance.SetArgumentValue(refreshKey4, "RefreshKey");
    var month4 = dayStatusKPIs.InputParameters.Named("Month");
    dayStatusKPIsInstance.SetArgumentValue(month4, "SelectedMonth");
    
    var weekRow = sheetPage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("WeekRow");
    weekRow.Gap = null;
    weekRow.SetStyle("\"sheet-page__week\"");
    
    var weeklyProjectHoursInstance = weekRow.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
```
