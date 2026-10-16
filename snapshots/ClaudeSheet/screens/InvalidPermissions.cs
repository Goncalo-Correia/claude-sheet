(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileScreen 'InvalidPermissions' ***/
    var invalidPermissions = common.CreateScreen("InvalidPermissions");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    invalidPermissions.Widgets.ToList().ForEach(x => x.Delete());
    invalidPermissions.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = common.GetESpace();
    var anonymous = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Anonymous");
    invalidPermissions.Roles.Add(anonymous);
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetUser = claudeSheet_CS.Roles.Named("SheetUser");
    invalidPermissions.Roles.Add(sheetUser);
    var registered = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Registered");
    invalidPermissions.Roles.Add(registered);
    invalidPermissions.AnonymousAccess = false; // default value
    invalidPermissions.CustomURL = false; // default value
    invalidPermissions.Description = "Screen where users will be redirected when they have invalid permissions.";
    invalidPermissions.HorizontalPosition = 2285;
    invalidPermissions.Icon = null; // default value
    invalidPermissions.IsDisabled = false; // default value
    invalidPermissions.PageName = null; // default value
    invalidPermissions.Public = false; // default value
    invalidPermissions.Shareable = false; // default value
    invalidPermissions.StyleSheet = null; // default value
    invalidPermissions.SetTitle(null); // default value
    invalidPermissions.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    invalidPermissions.VerticalPosition = 685;
    invalidPermissions.OnDestroy.Destination = null; // default value
    invalidPermissions.OnInitialize.Destination = null; // default value
    invalidPermissions.OnReady.Destination = null; // default value
    invalidPermissions.OnRender.Destination = null; // default value
    
    var layoutTopMenuInstance = invalidPermissions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopMenuInstance.CustomStyle = null; // default value
    layoutTopMenuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    layoutTopMenuInstance.SourceBlock = layoutTopMenu;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass = layoutTopMenu.InputParameters.Named("ExtendedClass");
    layoutTopMenuInstance.SetArgumentValue(extendedClass, null);
    var hasFixedHeader = layoutTopMenu.InputParameters.Named("HasFixedHeader");
    layoutTopMenuInstance.SetArgumentValue(hasFixedHeader, null);
    var enableAccessibilityFeatures = layoutTopMenu.InputParameters.Named("EnableAccessibilityFeatures");
    layoutTopMenuInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var blankSlateInstance = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    blankSlateInstance.CustomStyle = null; // default value
    blankSlateInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var content = outSystemsUI.MobileFlows.Named("Content");
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    blankSlateInstance.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget3 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget3.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass2 = blankSlate.InputParameters.Named("ExtendedClass");
    blankSlateInstance.SetArgumentValue(extendedClass2, null); // default value
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    blankSlateInstance.SetArgumentValue(fullHeight, "True");
    
    var icon1 = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("Icon1");
    icon1.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon1.ColumnSpan = null; // default value
    icon1.CustomStyle = null; // default value
    icon1.FlexBasis = null; // default value
    icon1.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon1.Icon = "exclamation-triangle";
    icon1.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon1.RowSpan = null; // default value
    icon1.SetStyle("\"icon text-neutral-4\"");
    icon1.SetVisible("True"); // default value
    icon1.Weight = null; // default value
    
    var placeholderContentWidget4 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var container = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle(null); // default value
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var textWidget = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses("\"heading6\"");
    textWidget.Text = "You don\'t have permissions to view this screen.";
    
    var container2 = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"margin-top-s\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var textWidget2 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Please contact your system administrator.";
    
    var placeholderContentWidget5 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
    var button = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    button.Widgets.ToList().ForEach(x => x.Delete());
    button.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button.ColumnGap = null; // default value
    button.ColumnSpan = null; // default value
    button.SetConfirmationMessage(null); // default value
    button.CustomStyle = null; // default value
    button.SetEnabled("True");
    button.FlexBasis = null; // default value
    button.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button.Gap = null;
    button.GridColumns = null; // default value
    button.GridRows = null; // default value
    button.IsDefault = false; // default value
    button.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button.RowGap = null; // default value
    button.RowSpan = null; // default value
    button.SetStyle("\"btn\""); // default value
    button.SetVisible("True"); // default value
    button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button.Width = ""; // default value
    
    var icon = button.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon.ColumnSpan = null; // default value
    icon.CustomStyle = null; // default value
    icon.FlexBasis = null; // default value
    icon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon.Icon = "angle-left";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon.RowSpan = null; // default value
    icon.SetStyle("\"icon\""); // default value
    icon.SetVisible("True"); // default value
    icon.Weight = null; // default value
    
    var textWidget3 = button.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses("\"margin-left-s\"");
    textWidget3.Text = "Go Back";
    var builtinEvent = button.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = eSpace.PreviousScreen;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.UseHistory;
    
    var notRegistered = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("NotRegistered");
    notRegistered.Animate = false; // default value
    notRegistered.SetCondition("GetUserId() = NullIdentifier()");
    notRegistered.CustomStyle = null; // default value
    notRegistered.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var button2 = notRegistered.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    button2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button2.ColumnGap = null; // default value
    button2.ColumnSpan = null; // default value
    button2.SetConfirmationMessage(null); // default value
    button2.CustomStyle = null; // default value
    button2.SetEnabled("True");
    button2.FlexBasis = null; // default value
    button2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button2.Gap = null;
    button2.GridColumns = null; // default value
    button2.GridRows = null; // default value
    button2.IsDefault = false; // default value
    button2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button2.RowGap = null; // default value
    button2.RowSpan = null; // default value
    button2.SetStyle("\"btn btn-primary margin-left-m\"");
    button2.SetVisible("True"); // default value
    button2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button2.Width = ""; // default value
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button2.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Login";
    var builtinEvent2 = button2.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var login = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Login");
    builtinEvent2.Destination = login;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
}
