(OutSystems.Model.UI.Mobile.IMobileFlow sheetBlocks) => {
    
    /*** creating IMobileBlock 'WeeklyProjectHours' ***/
    var weeklyProjectHours = sheetBlocks.CreateBlock("WeeklyProjectHours");
    weeklyProjectHours.Description = "Weekly grid of the current user\'s hours per project and per day (Monday-Sunday week containing ReferenceDate), with totals per day, per week and a grand total. Previous/next arrows move one week and raise OnWeekChanged. Reloads when ReferenceDate or RefreshKey change.";
    weeklyProjectHours.HorizontalPosition = 800;
    weeklyProjectHours.Icon = null; // default value
    weeklyProjectHours.IsDisabled = false; // default value
    weeklyProjectHours.JavaScript = ""; // default value
    weeklyProjectHours.SetOnRenderJavascript(null); // default value
    weeklyProjectHours.Public = false; // default value
    weeklyProjectHours.Shareable = false; // default value
    weeklyProjectHours.StyleSheet = null; // default value
    weeklyProjectHours.VerticalPosition = 1900;
    
    var getWeek = weeklyProjectHours.CreateDataAction("GetWeek");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getWeek.OutputParameters.ToList().ForEach(x => x.Delete());
    getWeek.Description = "The weekly grid for CurrentReferenceDate (ProjectHours_GetWeekly).";
    getWeek.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getWeek.ServerRequestTimeout = null; // default value
    
    var startNode = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3200;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var projectHours_GetWeekly = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ProjectHours_GetWeekly").ConnectedBelow(startNode, 1371);
    var eSpace = sheetBlocks.GetESpace();
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var projectHours_GetWeekly2 = claudeSheet_CS.ServerActions.Named("ProjectHours_GetWeekly");
    projectHours_GetWeekly.Action = projectHours_GetWeekly2;
    projectHours_GetWeekly.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    projectHours_GetWeekly.IsDisabled = false; // default value
    projectHours_GetWeekly.ServerRequestTimeout = null; // default value
    var referenceDate = projectHours_GetWeekly2.InputParameters.Named("ReferenceDate");
    projectHours_GetWeekly.SetArgumentValue(referenceDate, "CurrentReferenceDate");
    
    var assignNode = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(projectHours_GetWeekly, 1371);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Week", "ProjectHours_GetWeekly.Week");
    
    var endNode = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 1371);
    endNode.IsDisabled = false; // default value
    
    var allExceptions = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = false;
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 5028;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 800;
    
    var assignNode2 = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("ErrorMessage", "AllExceptions.ExceptionMessage");
    
    var endNode2 = getWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode2, 1371);
    endNode2.IsDisabled = false; // default value
    
    var week = getWeek.CreateOutputParameter("Week");
    var weeklyHours = claudeSheet_CS.Structures.Named("WeeklyHours");
    week.DataType = weeklyHours;
    week.Description = null; // default value
    
    var onAfterFetch_GetWeek = weeklyProjectHours.CreateScreenAction("OnAfterFetch_GetWeek");
    onAfterFetch_GetWeek.Description = "Shows an error feedback message when GetWeek failed.";
    
    var startNode2 = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 3200;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 800;
    
    var ifNode = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1371);
    ifNode.SetCondition("GetWeek.ErrorMessage <> \"\"");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Failed?";
    
    var messageNode = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ToTheRightOf(ifNode, 1828);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("GetWeek.ErrorMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode3 = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode, 1371);
    endNode3.IsDisabled = false; // default value
    
    var endNode4 = onAfterFetch_GetWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1371);
    endNode4.IsDisabled = false; // default value
    getWeek.OnAfterFetch.Destination = onAfterFetch_GetWeek;
    ifNode.FalseTarget = endNode3;
    ifNode.TrueTarget = messageNode;
    
    var errorMessage = getWeek.CreateOutputParameter("ErrorMessage");
    errorMessage.DataType = eSpace.TextType;
    errorMessage.SetDefaultValue(null); // default value
    errorMessage.Description = null; // default value
    
    var onWeekChanged = weeklyProjectHours.CreateEvent("OnWeekChanged");
    onWeekChanged.Description = "Raised by the previous/next arrows with the Monday of the new week.";
    onWeekChanged.IsMandatory = false;
    
    var weekStartDate = onWeekChanged.CreateInputParameter("WeekStartDate");
    weekStartDate.DataType = eSpace.DateType;
    weekStartDate.Description = null; // default value
    weekStartDate.IsMandatory = true; // default value
    
    var referenceDate2 = weeklyProjectHours.CreateInputParameter("ReferenceDate");
    referenceDate2.DataType = eSpace.DateType;
    referenceDate2.SetDefaultValue("CurrDate()");
    referenceDate2.Description = "Any day of the week to show (default: today).";
    referenceDate2.IsMandatory = false;
    
    var refreshKey = weeklyProjectHours.CreateInputParameter("RefreshKey");
    refreshKey.DataType = eSpace.IntegerType;
    refreshKey.SetDefaultValue("0");
    refreshKey.Description = "Change this value to force the block to reload (e.g. after saving a day). Only used to trigger OnParametersChanged.";
    refreshKey.IsMandatory = false;
    
    var currentReferenceDate = weeklyProjectHours.CreateLocalVariable("CurrentReferenceDate");
    currentReferenceDate.DataType = eSpace.DateType;
    currentReferenceDate.SetDefaultValue(null); // default value
    currentReferenceDate.Description = "Day of the week being shown; starts from the ReferenceDate input and moves with the arrows.";
    weeklyProjectHours.OnDestroy.Destination = null; // default value
    weeklyProjectHours.OnReady.Destination = null; // default value
    weeklyProjectHours.OnRender.Destination = null; // default value
    
    var onClick_NextWeek = weeklyProjectHours.CreateScreenAction("OnClick_NextWeek");
    onClick_NextWeek.Description = "Moves the grid one week forward, reloads GetWeek and raises OnWeekChanged with the new Monday.";
    
    var startNode3 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 3200;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var onInitialize = weeklyProjectHours.CreateScreenAction("OnInitialize");
    onInitialize.Description = "Starts CurrentReferenceDate from the ReferenceDate input (today when empty).";
    
    var startNode4 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 3200;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 800;
    
    var assignNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode4, 1371);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment3 = assignNode3.CreateAssignment("CurrentReferenceDate", "If(ReferenceDate = NullDate(), CurrDate(), ReferenceDate)");
    
    var endNode5 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode3, 1371);
    endNode5.IsDisabled = false; // default value
    weeklyProjectHours.OnInitialize.Destination = onInitialize;
    
    var onParametersChanged = weeklyProjectHours.CreateScreenAction("OnParametersChanged");
    onParametersChanged.Description = "Syncs CurrentReferenceDate with the ReferenceDate input and reloads GetWeek (also when RefreshKey changes).";
    
    var startNode5 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3200;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 800;
    
    var assignNode4 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment4 = assignNode4.CreateAssignment("CurrentReferenceDate", "If(ReferenceDate = NullDate(), CurrDate(), ReferenceDate)");
    
    var refreshDataNode = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode4, 1371);
    refreshDataNode.DataSource = getWeek;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords(null); // default value
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode6 = onParametersChanged.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode6.IsDisabled = false; // default value
    weeklyProjectHours.OnParametersChanged.Destination = onParametersChanged;
    
    var assignNode5 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment5 = assignNode5.CreateAssignment("CurrentReferenceDate", "DateTimeToDate(AddDays(GetWeek.Week.WeekStart, 7))");
    
    var refreshDataNode2 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode5, 1371);
    refreshDataNode2.DataSource = getWeek;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords(null); // default value
    refreshDataNode2.SetStartIndex(null); // default value
    
    var onWeekChanged2 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnWeekChanged").ConnectedBelow(refreshDataNode2, 1371);
    onWeekChanged2.Description = null; // default value
    onWeekChanged2.Event = onWeekChanged;
    onWeekChanged2.IsDisabled = false; // default value
    onWeekChanged2.SetArgumentValue(weekStartDate, "CurrentReferenceDate");
    
    var endNode7 = onClick_NextWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onWeekChanged2, 1371);
    endNode7.IsDisabled = false; // default value
    
    var onClick_PreviousWeek = weeklyProjectHours.CreateScreenAction("OnClick_PreviousWeek");
    onClick_PreviousWeek.Description = "Moves the grid one week back, reloads GetWeek and raises OnWeekChanged with the new Monday.";
    
    var startNode6 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 3200;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 800;
    
    var assignNode6 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode6, 1371);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    
    var assignment6 = assignNode6.CreateAssignment("CurrentReferenceDate", "DateTimeToDate(AddDays(GetWeek.Week.WeekStart, -7))");
    
    var refreshDataNode3 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode6, 1371);
    refreshDataNode3.DataSource = getWeek;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    
    var onWeekChanged3 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.ITriggerNode>("OnWeekChanged").ConnectedBelow(refreshDataNode3, 1371);
    onWeekChanged3.Description = null; // default value
    onWeekChanged3.Event = onWeekChanged;
    onWeekChanged3.IsDisabled = false; // default value
    onWeekChanged3.SetArgumentValue(weekStartDate, "CurrentReferenceDate");
    
    var endNode8 = onClick_PreviousWeek.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(onWeekChanged3, 1371);
    endNode8.IsDisabled = false; // default value
    
    var weekGrid = weeklyProjectHours.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("WeekGrid");
    weekGrid.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    weekGrid.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    weekGrid.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    weekGrid.Animate = false; // default value
    weekGrid.ColumnGap = null; // default value
    weekGrid.ColumnSpan = null; // default value
    weekGrid.CustomStyle = null; // default value
    weekGrid.FlexBasis = null; // default value
    weekGrid.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    weekGrid.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    weekGrid.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    weekGrid.Gap = null;
    weekGrid.GridColumns = null; // default value
    weekGrid.GridRows = null; // default value
    weekGrid.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    weekGrid.RowGap = null; // default value
    weekGrid.RowSpan = null; // default value
    weekGrid.SetStyle("\"sheet-week\" + If(GetWeek.IsDataFetched, \"\", \" sheet-week--loading\")");
    weekGrid.SetVisible("True"); // default value
    weekGrid.VisibleWasExpression = false; // default value
    weekGrid.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    weekGrid.Width = "(fill parent)"; // default value
    
    var weekScroll = weekGrid.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("WeekScroll");
    weekScroll.CustomStyle = null; // default value
    weekScroll.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var scrollableArea = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ScrollableArea");
    weekScroll.SourceBlock = scrollableArea;
    var width = scrollableArea.InputParameters.Named("Width");
    weekScroll.SetArgumentValue(width, null); // default value
    var height = scrollableArea.InputParameters.Named("Height");
    weekScroll.SetArgumentValue(height, null); // default value
    var orientation = scrollableArea.InputParameters.Named("Orientation");
    weekScroll.SetArgumentValue(orientation, null);
    var scrollbarStyle = scrollableArea.InputParameters.Named("ScrollbarStyle");
    weekScroll.SetArgumentValue(scrollbarStyle, null);
    var extendedClass = scrollableArea.InputParameters.Named("ExtendedClass");
    weekScroll.SetArgumentValue(extendedClass, "\"sheet-week__scroll\"");
    
    var placeholderContentWidget = weekScroll.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var container = placeholderContentWidget.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container.Animate = false; // default value
    container.ColumnGap = null; // default value
    container.ColumnSpan = null; // default value
    container.CustomStyle = null; // default value
    container.FlexBasis = null; // default value
    container.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container.Gap = null;
    container.GridColumns = null; // default value
    container.GridRows = null; // default value
    container.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container.RowGap = null; // default value
    container.RowSpan = null; // default value
    container.SetStyle("\"sheet-week__table\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var container2 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container2.Animate = false; // default value
    container2.ColumnGap = null; // default value
    container2.ColumnSpan = null; // default value
    container2.CustomStyle = null; // default value
    container2.FlexBasis = null; // default value
    container2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container2.Gap = null;
    container2.GridColumns = null; // default value
    container2.GridRows = null; // default value
    container2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container2.RowGap = null; // default value
    container2.RowSpan = null; // default value
    container2.SetStyle("\"sheet-week__row sheet-week__head\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container3.Animate = false; // default value
    container3.ColumnGap = null; // default value
    container3.ColumnSpan = null; // default value
    container3.CustomStyle = null; // default value
    container3.FlexBasis = null; // default value
    container3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container3.Gap = null;
    container3.GridColumns = null; // default value
    container3.GridRows = null; // default value
    container3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container3.RowGap = null; // default value
    container3.RowSpan = null; // default value
    container3.SetStyle("\"sheet-week__label\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var container4 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container4.Animate = false; // default value
    container4.ColumnGap = null; // default value
    container4.ColumnSpan = null; // default value
    container4.CustomStyle = null; // default value
    container4.FlexBasis = null; // default value
    container4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container4.Gap = null;
    container4.GridColumns = null; // default value
    container4.GridRows = null; // default value
    container4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container4.RowGap = null; // default value
    container4.RowSpan = null; // default value
    container4.SetStyle("\"sheet-week__title\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var expression = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("\"Project daily hours\"");
    expression.Width = ""; // default value
    
    var previousWeek = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("PreviousWeek");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    previousWeek.Widgets.ToList().ForEach(x => x.Delete());
    previousWeek.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    previousWeek.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    previousWeek.ColumnGap = null; // default value
    previousWeek.ColumnSpan = null; // default value
    previousWeek.SetConfirmationMessage(null); // default value
    previousWeek.CustomStyle = null; // default value
    previousWeek.SetEnabled("True");
    previousWeek.FlexBasis = null; // default value
    previousWeek.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    previousWeek.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    previousWeek.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    previousWeek.Gap = null;
    previousWeek.GridColumns = null; // default value
    previousWeek.GridRows = null; // default value
    previousWeek.IsDefault = false; // default value
    previousWeek.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    previousWeek.RowGap = null; // default value
    previousWeek.RowSpan = null; // default value
    previousWeek.SetStyle("\"sheet-week__arrow\"");
    previousWeek.SetVisible("True"); // default value
    previousWeek.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    previousWeek.Width = ""; // default value
    
    var icon = previousWeek.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon.ColumnSpan = null; // default value
    icon.CustomStyle = null; // default value
    icon.FlexBasis = null; // default value
    icon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon.Icon = "chevron-left";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon.RowSpan = null; // default value
    icon.SetStyle("\"icon\""); // default value
    icon.SetVisible("True"); // default value
    icon.Weight = null; // default value
    var builtinEvent = previousWeek.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onClick_PreviousWeek;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty = previousWeek.CreateExtendedProperty();
    extendedProperty.Property = "aria-label";
    extendedProperty.SetValue("\"Previous week\"");
    
    var extendedProperty2 = container3.CreateExtendedProperty();
    extendedProperty2.Property = "role";
    extendedProperty2.SetValue("\"columnheader\"");
    
    var dayHeaders = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("DayHeaders");
    dayHeaders.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayHeaders.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayHeaders.AnimateItems = true; // default value
    dayHeaders.ColumnGap = null; // default value
    dayHeaders.ColumnSpan = null; // default value
    dayHeaders.CustomStyle = null; // default value
    dayHeaders.ExpandedInWebEditor = true; // default value
    dayHeaders.FlexBasis = null; // default value
    dayHeaders.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayHeaders.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayHeaders.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayHeaders.Gap = null;
    dayHeaders.GridColumns = null; // default value
    dayHeaders.GridRows = null; // default value
    dayHeaders.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayHeaders.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    dayHeaders.RowGap = null; // default value
    dayHeaders.RowSpan = null; // default value
    dayHeaders.SetSource("GetWeek.Week.Days");
    dayHeaders.SetStyle("\"sheet-week__contents\"");
    dayHeaders.Tag = "div"; // default value
    dayHeaders.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayHeaders.Width = "(fill parent)"; // default value
    
    var container5 = dayHeaders.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container5.Animate = false; // default value
    container5.ColumnGap = null; // default value
    container5.ColumnSpan = null; // default value
    container5.CustomStyle = null; // default value
    container5.FlexBasis = null; // default value
    container5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container5.Gap = null;
    container5.GridColumns = null; // default value
    container5.GridRows = null; // default value
    container5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container5.RowGap = null; // default value
    container5.RowSpan = null; // default value
    container5.SetStyle("\"sheet-week__day\" + If(GetWeek.Week.Days.Current.IsToday, \" is-today\", \"\") + If(GetWeek.Week.Days.Current.IsWeekend, \" is-weekend\", \"\")");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var container6 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container6.Animate = false; // default value
    container6.ColumnGap = null; // default value
    container6.ColumnSpan = null; // default value
    container6.CustomStyle = null; // default value
    container6.FlexBasis = null; // default value
    container6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container6.Gap = null;
    container6.GridColumns = null; // default value
    container6.GridRows = null; // default value
    container6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container6.RowGap = null; // default value
    container6.RowSpan = null; // default value
    container6.SetStyle("\"sheet-week__day-name\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var expression2 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetWeek.Week.Days.Current.DayName");
    expression2.Width = ""; // default value
    
    var container7 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container7.Animate = false; // default value
    container7.ColumnGap = null; // default value
    container7.ColumnSpan = null; // default value
    container7.CustomStyle = null; // default value
    container7.FlexBasis = null; // default value
    container7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container7.Gap = null;
    container7.GridColumns = null; // default value
    container7.GridRows = null; // default value
    container7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container7.RowGap = null; // default value
    container7.RowSpan = null; // default value
    container7.SetStyle("\"sheet-week__day-date\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var expression3 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("GetWeek.Week.Days.Current.DateLabel");
    expression3.Width = ""; // default value
    
    var extendedProperty3 = container5.CreateExtendedProperty();
    extendedProperty3.Property = "role";
    extendedProperty3.SetValue("\"columnheader\"");
    var builtinEvent2 = dayHeaders.OnScrollEnding;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = null; // default value
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container8 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = false; // default value
    container8.ColumnGap = null; // default value
    container8.ColumnSpan = null; // default value
    container8.CustomStyle = null; // default value
    container8.FlexBasis = null; // default value
    container8.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container8.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container8.Gap = null;
    container8.GridColumns = null; // default value
    container8.GridRows = null; // default value
    container8.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container8.RowGap = null; // default value
    container8.RowSpan = null; // default value
    container8.SetStyle("\"sheet-week__total-head\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var nextWeek = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("NextWeek");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    nextWeek.Widgets.ToList().ForEach(x => x.Delete());
    nextWeek.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    nextWeek.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    nextWeek.ColumnGap = null; // default value
    nextWeek.ColumnSpan = null; // default value
    nextWeek.SetConfirmationMessage(null); // default value
    nextWeek.CustomStyle = null; // default value
    nextWeek.SetEnabled("True");
    nextWeek.FlexBasis = null; // default value
    nextWeek.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    nextWeek.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    nextWeek.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    nextWeek.Gap = null;
    nextWeek.GridColumns = null; // default value
    nextWeek.GridRows = null; // default value
    nextWeek.IsDefault = false; // default value
    nextWeek.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    nextWeek.RowGap = null; // default value
    nextWeek.RowSpan = null; // default value
    nextWeek.SetStyle("\"sheet-week__arrow\"");
    nextWeek.SetVisible("True"); // default value
    nextWeek.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    nextWeek.Width = ""; // default value
    
    var icon2 = nextWeek.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "chevron-right";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon\""); // default value
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    var builtinEvent3 = nextWeek.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = onClick_NextWeek;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty4 = nextWeek.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Next week\"");
    
    var container9 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container9.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container9.Animate = false; // default value
    container9.ColumnGap = null; // default value
    container9.ColumnSpan = null; // default value
    container9.CustomStyle = null; // default value
    container9.FlexBasis = null; // default value
    container9.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container9.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container9.Gap = null;
    container9.GridColumns = null; // default value
    container9.GridRows = null; // default value
    container9.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container9.RowGap = null; // default value
    container9.RowSpan = null; // default value
    container9.SetStyle("\"sheet-week__total-label\"");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var expression4 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("\"Total per week\"");
    expression4.Width = ""; // default value
    
    var extendedProperty5 = container8.CreateExtendedProperty();
    extendedProperty5.Property = "role";
    extendedProperty5.SetValue("\"columnheader\"");
    
    var extendedProperty6 = container2.CreateExtendedProperty();
    extendedProperty6.Property = "role";
    extendedProperty6.SetValue("\"row\"");
    
    var hasRows = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasRows");
    hasRows.Animate = false; // default value
    hasRows.SetCondition("GetWeek.Week.Rows.Length > 0 or not GetWeek.IsDataFetched");
    hasRows.CustomStyle = null; // default value
    hasRows.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container10 = hasRows.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container10.Animate = false; // default value
    container10.ColumnGap = null; // default value
    container10.ColumnSpan = null; // default value
    container10.CustomStyle = null; // default value
    container10.FlexBasis = null; // default value
    container10.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container10.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container10.Gap = null;
    container10.GridColumns = null; // default value
    container10.GridRows = null; // default value
    container10.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container10.RowGap = null; // default value
    container10.RowSpan = null; // default value
    container10.SetStyle("\"sheet-week__body\"");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var projectRows = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProjectRows");
    projectRows.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    projectRows.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    projectRows.AnimateItems = true; // default value
    projectRows.ColumnGap = null; // default value
    projectRows.ColumnSpan = null; // default value
    projectRows.CustomStyle = null; // default value
    projectRows.ExpandedInWebEditor = true; // default value
    projectRows.FlexBasis = null; // default value
    projectRows.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    projectRows.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    projectRows.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    projectRows.Gap = null;
    projectRows.GridColumns = null; // default value
    projectRows.GridRows = null; // default value
    projectRows.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    projectRows.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    projectRows.RowGap = null; // default value
    projectRows.RowSpan = null; // default value
    projectRows.SetSource("GetWeek.Week.Rows");
    projectRows.SetStyle("\"sheet-week__rows\"");
    projectRows.Tag = "div"; // default value
    projectRows.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    projectRows.Width = "(fill parent)"; // default value
    
    var container11 = projectRows.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container11.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container11.Animate = false; // default value
    container11.ColumnGap = null; // default value
    container11.ColumnSpan = null; // default value
    container11.CustomStyle = null; // default value
    container11.FlexBasis = null; // default value
    container11.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container11.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container11.Gap = null;
    container11.GridColumns = null; // default value
    container11.GridRows = null; // default value
    container11.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container11.RowGap = null; // default value
    container11.RowSpan = null; // default value
    container11.SetStyle("\"sheet-week__row sheet-week__card\"");
    container11.SetVisible("True"); // default value
    container11.VisibleWasExpression = false; // default value
    container11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container11.Width = "(fill parent)"; // default value
    
    var container12 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container12.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container12.Animate = false; // default value
    container12.ColumnGap = null; // default value
    container12.ColumnSpan = null; // default value
    container12.CustomStyle = null; // default value
    container12.FlexBasis = null; // default value
    container12.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container12.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container12.Gap = null;
    container12.GridColumns = null; // default value
    container12.GridRows = null; // default value
    container12.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container12.RowGap = null; // default value
    container12.RowSpan = null; // default value
    container12.SetStyle("\"sheet-week__label sheet-week__label--project\"");
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var container13 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container13.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container13.Animate = false; // default value
    container13.ColumnGap = null; // default value
    container13.ColumnSpan = null; // default value
    container13.CustomStyle = null; // default value
    container13.FlexBasis = null; // default value
    container13.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container13.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container13.Gap = null;
    container13.GridColumns = null; // default value
    container13.GridRows = null; // default value
    container13.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container13.RowGap = null; // default value
    container13.RowSpan = null; // default value
    container13.SetStyle("\"sheet-week__client\"");
    container13.SetVisible("True"); // default value
    container13.VisibleWasExpression = false; // default value
    container13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container13.Width = "(fill parent)"; // default value
    
    var expression5 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("GetWeek.Week.Rows.Current.ClientName");
    expression5.Width = ""; // default value
    
    var container14 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container14.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container14.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container14.Animate = false; // default value
    container14.ColumnGap = null; // default value
    container14.ColumnSpan = null; // default value
    container14.CustomStyle = null; // default value
    container14.FlexBasis = null; // default value
    container14.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container14.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container14.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container14.Gap = null;
    container14.GridColumns = null; // default value
    container14.GridRows = null; // default value
    container14.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container14.RowGap = null; // default value
    container14.RowSpan = null; // default value
    container14.SetStyle("\"sheet-week__project\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "(fill parent)"; // default value
    
    var expression6 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("GetWeek.Week.Rows.Current.ProjectName + \" | \" + GetWeek.Week.Rows.Current.ProjectCode");
    expression6.Width = ""; // default value
    
    var extendedProperty7 = container12.CreateExtendedProperty();
    extendedProperty7.Property = "role";
    extendedProperty7.SetValue("\"rowheader\"");
    
    var rowCells = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("RowCells");
    rowCells.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCells.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCells.AnimateItems = true; // default value
    rowCells.ColumnGap = null; // default value
    rowCells.ColumnSpan = null; // default value
    rowCells.CustomStyle = null; // default value
    rowCells.ExpandedInWebEditor = true; // default value
    rowCells.FlexBasis = null; // default value
    rowCells.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCells.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCells.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCells.Gap = null;
    rowCells.GridColumns = null; // default value
    rowCells.GridRows = null; // default value
    rowCells.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCells.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    rowCells.RowGap = null; // default value
    rowCells.RowSpan = null; // default value
    rowCells.SetSource("GetWeek.Week.Rows.Current.Cells");
    rowCells.SetStyle("\"sheet-week__contents\"");
    rowCells.Tag = "div"; // default value
    rowCells.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    rowCells.Width = "(fill parent)"; // default value
    
    var container15 = rowCells.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container15.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container15.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container15.Animate = false; // default value
    container15.ColumnGap = null; // default value
    container15.ColumnSpan = null; // default value
    container15.CustomStyle = null; // default value
    container15.FlexBasis = null; // default value
    container15.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container15.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container15.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container15.Gap = null;
    container15.GridColumns = null; // default value
    container15.GridRows = null; // default value
    container15.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container15.RowGap = null; // default value
    container15.RowSpan = null; // default value
    container15.SetStyle("\"sheet-week__cell\" + If(DayOfWeek(GetWeek.Week.Rows.Current.Cells.Current.Date) = 0 or DayOfWeek(GetWeek.Week.Rows.Current.Cells.Current.Date) = 6, \" is-weekend\", \"\")");
    container15.SetVisible("True"); // default value
    container15.VisibleWasExpression = false; // default value
    container15.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container15.Width = "(fill parent)"; // default value
    
    var expression7 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("If(GetWeek.Week.Rows.Current.Cells.Current.ShowDash, \"-\", Minutes_Format(GetWeek.Week.Rows.Current.Cells.Current.Minutes, \":\"))");
    expression7.Width = ""; // default value
    
    var extendedProperty8 = container15.CreateExtendedProperty();
    extendedProperty8.Property = "role";
    extendedProperty8.SetValue("\"cell\"");
    var builtinEvent4 = rowCells.OnScrollEnding;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = null; // default value
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container16 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container16.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container16.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container16.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container16.Animate = false; // default value
    container16.ColumnGap = null; // default value
    container16.ColumnSpan = null; // default value
    container16.CustomStyle = null; // default value
    container16.FlexBasis = null; // default value
    container16.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container16.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container16.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container16.Gap = null;
    container16.GridColumns = null; // default value
    container16.GridRows = null; // default value
    container16.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container16.RowGap = null; // default value
    container16.RowSpan = null; // default value
    container16.SetStyle("\"sheet-week__cell sheet-week__total\"");
    container16.SetVisible("True"); // default value
    container16.VisibleWasExpression = false; // default value
    container16.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container16.Width = "(fill parent)"; // default value
    
    var expression8 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("Minutes_Format(GetWeek.Week.Rows.Current.TotalMinutes, \":\")");
    expression8.Width = ""; // default value
    
    var extendedProperty9 = container16.CreateExtendedProperty();
    extendedProperty9.Property = "role";
    extendedProperty9.SetValue("\"cell\"");
    
    var extendedProperty10 = container11.CreateExtendedProperty();
    extendedProperty10.Property = "role";
    extendedProperty10.SetValue("\"row\"");
    var builtinEvent5 = projectRows.OnScrollEnding;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container17 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container17.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container17.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container17.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container17.Animate = false; // default value
    container17.ColumnGap = null; // default value
    container17.ColumnSpan = null; // default value
    container17.CustomStyle = null; // default value
    container17.FlexBasis = null; // default value
    container17.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container17.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container17.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container17.Gap = null;
    container17.GridColumns = null; // default value
    container17.GridRows = null; // default value
    container17.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container17.RowGap = null; // default value
    container17.RowSpan = null; // default value
    container17.SetStyle("\"sheet-week__row sheet-week__card sheet-week__totals\"");
    container17.SetVisible("True"); // default value
    container17.VisibleWasExpression = false; // default value
    container17.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container17.Width = "(fill parent)"; // default value
    
    var container18 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container18.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container18.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container18.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container18.Animate = false; // default value
    container18.ColumnGap = null; // default value
    container18.ColumnSpan = null; // default value
    container18.CustomStyle = null; // default value
    container18.FlexBasis = null; // default value
    container18.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container18.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container18.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container18.Gap = null;
    container18.GridColumns = null; // default value
    container18.GridRows = null; // default value
    container18.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container18.RowGap = null; // default value
    container18.RowSpan = null; // default value
    container18.SetStyle("\"sheet-week__label sheet-week__label--project\"");
    container18.SetVisible("True"); // default value
    container18.VisibleWasExpression = false; // default value
    container18.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container18.Width = "(fill parent)"; // default value
    
    var expression9 = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("\"Total per day\"");
    expression9.Width = ""; // default value
    
    var extendedProperty11 = container18.CreateExtendedProperty();
    extendedProperty11.Property = "role";
    extendedProperty11.SetValue("\"rowheader\"");
    
    var dayTotals = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("DayTotals");
    dayTotals.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    dayTotals.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    dayTotals.AnimateItems = true; // default value
    dayTotals.ColumnGap = null; // default value
    dayTotals.ColumnSpan = null; // default value
    dayTotals.CustomStyle = null; // default value
    dayTotals.ExpandedInWebEditor = true; // default value
    dayTotals.FlexBasis = null; // default value
    dayTotals.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    dayTotals.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    dayTotals.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    dayTotals.Gap = null;
    dayTotals.GridColumns = null; // default value
    dayTotals.GridRows = null; // default value
    dayTotals.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    dayTotals.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    dayTotals.RowGap = null; // default value
    dayTotals.RowSpan = null; // default value
    dayTotals.SetSource("GetWeek.Week.DayTotals");
    dayTotals.SetStyle("\"sheet-week__contents\"");
    dayTotals.Tag = "div"; // default value
    dayTotals.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    dayTotals.Width = "(fill parent)"; // default value
    
    var container19 = dayTotals.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container19.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container19.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container19.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container19.Animate = false; // default value
    container19.ColumnGap = null; // default value
    container19.ColumnSpan = null; // default value
    container19.CustomStyle = null; // default value
    container19.FlexBasis = null; // default value
    container19.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container19.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container19.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container19.Gap = null;
    container19.GridColumns = null; // default value
    container19.GridRows = null; // default value
    container19.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container19.RowGap = null; // default value
    container19.RowSpan = null; // default value
    container19.SetStyle("\"sheet-week__cell\" + If(DayOfWeek(GetWeek.Week.DayTotals.Current.Date) = 0 or DayOfWeek(GetWeek.Week.DayTotals.Current.Date) = 6, \" is-weekend\", \"\")");
    container19.SetVisible("True"); // default value
    container19.VisibleWasExpression = false; // default value
    container19.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container19.Width = "(fill parent)"; // default value
    
    var expression10 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("If(GetWeek.Week.DayTotals.Current.ShowDash, \"-\", Minutes_Format(GetWeek.Week.DayTotals.Current.Minutes, \":\"))");
    expression10.Width = ""; // default value
    
    var extendedProperty12 = container19.CreateExtendedProperty();
    extendedProperty12.Property = "role";
    extendedProperty12.SetValue("\"cell\"");
    var builtinEvent6 = dayTotals.OnScrollEnding;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = null; // default value
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container20 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container20.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container20.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container20.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container20.Animate = false; // default value
    container20.ColumnGap = null; // default value
    container20.ColumnSpan = null; // default value
    container20.CustomStyle = null; // default value
    container20.FlexBasis = null; // default value
    container20.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container20.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container20.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container20.Gap = null;
    container20.GridColumns = null; // default value
    container20.GridRows = null; // default value
    container20.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container20.RowGap = null; // default value
    container20.RowSpan = null; // default value
    container20.SetStyle("\"sheet-week__cell sheet-week__total\"");
    container20.SetVisible("True"); // default value
    container20.VisibleWasExpression = false; // default value
    container20.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container20.Width = "(fill parent)"; // default value
    
    var expression11 = container20.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle(null); // default value
    expression11.SetValue("Minutes_Format(GetWeek.Week.GrandTotalMinutes, \":\")");
    expression11.Width = ""; // default value
    
    var extendedProperty13 = container20.CreateExtendedProperty();
    extendedProperty13.Property = "role";
    extendedProperty13.SetValue("\"cell\"");
    
    var extendedProperty14 = container17.CreateExtendedProperty();
    extendedProperty14.Property = "role";
    extendedProperty14.SetValue("\"row\"");
    
    var container21 = hasRows.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container21.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container21.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container21.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container21.Animate = false; // default value
    container21.ColumnGap = null; // default value
    container21.ColumnSpan = null; // default value
    container21.CustomStyle = null; // default value
    container21.FlexBasis = null; // default value
    container21.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container21.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container21.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container21.Gap = null;
    container21.GridColumns = null; // default value
    container21.GridRows = null; // default value
    container21.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container21.RowGap = null; // default value
    container21.RowSpan = null; // default value
    container21.SetStyle("\"sheet-week__empty\"");
    container21.SetVisible("True"); // default value
    container21.VisibleWasExpression = false; // default value
    container21.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container21.Width = "(fill parent)"; // default value
    
    var expression12 = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression12.ColumnSpan = null; // default value
    expression12.CustomStyle = null; // default value
    expression12.Example = null; // default value
    expression12.FlexBasis = null; // default value
    expression12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression12.RowSpan = null; // default value
    expression12.SetStyle(null); // default value
    expression12.SetValue("\"No projects assigned for this week\"");
    expression12.Width = ""; // default value
    
    var extendedProperty15 = container21.CreateExtendedProperty();
    extendedProperty15.Property = "role";
    extendedProperty15.SetValue("\"status\"");
    
    var extendedProperty16 = container.CreateExtendedProperty();
    extendedProperty16.Property = "role";
    extendedProperty16.SetValue("\"table\"");
    
    var extendedProperty17 = container.CreateExtendedProperty();
    extendedProperty17.Property = "aria-label";
    extendedProperty17.SetValue("\"Project daily hours, week of \" + FormatDateTime(GetWeek.Week.WeekStart, \"dd/MM/yyyy\")");
    
    var extendedProperty18 = weekGrid.CreateExtendedProperty();
    extendedProperty18.Property = "aria-busy";
    extendedProperty18.SetValue("If(GetWeek.IsDataFetched, \"false\", \"true\")");
}
