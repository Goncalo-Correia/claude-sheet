# InvalidPermissions

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
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
    invalidPermissions.Description = "Screen where users will be redirected when they have invalid permissions.";
    invalidPermissions.HorizontalPosition = 2285;
    invalidPermissions.VerticalPosition = 685;
    
    var layoutTopMenuInstance = invalidPermissions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    
    var placeholderContentWidget2 = layoutTopMenuInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var blankSlateInstance = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var content = outSystemsUI.MobileFlows.Named("Content");
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    blankSlateInstance.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget3 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget3.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass2 = blankSlate.InputParameters.Named("ExtendedClass");
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    blankSlateInstance.SetArgumentValue(fullHeight, "True");
    
    var icon1 = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("Icon1");
    icon1.Icon = "exclamation-triangle";
    icon1.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon1.SetStyle("\"icon text-neutral-4\"");
    
    var placeholderContentWidget4 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var container = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    
    var textWidget = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.SetStyleClasses("\"heading6\"");
    textWidget.Text = "You don\'t have permissions to view this screen.";
    
    var container2 = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Gap = null;
    container2.SetStyle("\"margin-top-s\"");
    
    var textWidget2 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.Text = "Please contact your system administrator.";
    
    var placeholderContentWidget5 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
    var button = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    button.Widgets.ToList().ForEach(x => x.Delete());
    button.SetEnabled("True");
    button.Gap = null;
    
    var icon = button.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.Icon = "angle-left";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    
    var textWidget3 = button.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.SetStyleClasses("\"margin-left-s\"");
    textWidget3.Text = "Go Back";
    var builtinEvent = button.OnClick;
    builtinEvent.Destination = eSpace.PreviousScreen;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.UseHistory;
    
    var notRegistered = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("NotRegistered");
    notRegistered.SetCondition("GetUserId() = NullIdentifier()");
    notRegistered.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var button2 = notRegistered.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    button2.SetEnabled("True");
    button2.Gap = null;
    button2.SetStyle("\"btn btn-primary margin-left-m\"");
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button2.Widgets.First();
    textWidget4.Text = "Login";
    var builtinEvent2 = button2.OnClick;
    var login = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Login");
    builtinEvent2.Destination = login;
}
```
