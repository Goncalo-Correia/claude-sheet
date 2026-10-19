# Login

Screen in ClaudeSheet, as Model API code returned by Service Studio at the last publish. Lines that only set a property to its default value are left out.

```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileScreen 'Login' ***/
    var login = common.CreateScreen("Login");
    /*** Deleting Widgets that are created by default when instantiating an IMobileScreen ***/
    login.Widgets.ToList().ForEach(x => x.Delete());
    login.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = common.GetESpace();
    var anonymous = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Anonymous");
    login.Roles.Add(anonymous);
    var claudeSheet_CS = eSpace.References.Named("ClaudeSheet_CS");
    var sheetManager = claudeSheet_CS.Roles.Named("SheetManager");
    login.Roles.Add(sheetManager);
    var sheetUser = claudeSheet_CS.Roles.Named("SheetUser");
    login.Roles.Add(sheetUser);
    var sheetAdmin = claudeSheet_CS.Roles.Named("SheetAdmin");
    login.Roles.Add(sheetAdmin);
    var registered = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Registered");
    login.Roles.Add(registered);
    login.Description = "Screen where users login.";
    login.HorizontalPosition = 457;
    login.VerticalPosition = 686;
    
    var username = login.CreateLocalVariable("Username");
    username.DataType = eSpace.TextType;
    
    var password = login.CreateLocalVariable("Password");
    password.DataType = eSpace.TextType;
    
    var isExecuting = login.CreateLocalVariable("IsExecuting");
    isExecuting.DataType = eSpace.BooleanType;
    isExecuting.SetDefaultValue("False");
    
    var remember = login.CreateLocalVariable("Remember");
    remember.DataType = eSpace.BooleanType;
    
    var forgotPassword = login.CreateScreenAction("ForgotPassword");
    
    var startNode = forgotPassword.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var onInitialize = login.CreateScreenAction("OnInitialize");
    
    var startNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    
    var assignNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 3192;
    assignNode.Label = "Not executing";
    assignNode.VerticalPosition = 2964;
    
    var assignment = assignNode.CreateAssignment("IsExecuting", "False");
    
    var endNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.VerticalPosition = 5120;
    login.OnInitialize.Destination = onInitialize;
    
    var endNode2 = forgotPassword.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(startNode, 4320);
    startNode2.Target = assignNode;
    assignNode.Target = endNode;
    
    var login2 = login.CreateScreenAction("Login");
    
    var startNode3 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 2057;
    startNode3.VerticalPosition = 686;
    
    var assignNode2 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1600);
    assignNode2.Label = "Start executing";
    
    var assignment2 = assignNode2.CreateAssignment("IsExecuting", "True");
    
    var doLogin = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DoLogin").ConnectedBelow(assignNode2, 1600);
    var doLogin2 = eSpace.ServerActions.Named("DoLogin");
    doLogin.Action = doLogin2;
    var password2 = doLogin2.InputParameters.Named("Password");
    doLogin.SetArgumentValue(password2, "Password");
    var username2 = doLogin2.InputParameters.Named("Username");
    doLogin.SetArgumentValue(username2, "Username");
    var rememberLogin = doLogin2.InputParameters.Named("RememberLogin");
    doLogin.SetArgumentValue(rememberLogin, "Remember");
    
    var feedbackMessageClose = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("FeedbackMessageClose").ConnectedBelow(doLogin, 1828);
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var feedbackMessageClose2 = outSystemsUI.ClientActions.Named("FeedbackMessageClose");
    feedbackMessageClose.Action = feedbackMessageClose2;
    
    var destinationNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ConnectedBelow(feedbackMessageClose, 1600);
    var redirectToURL = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IExternalSite>().Named("RedirectToURL");
    destinationNode.Destination = redirectToURL;
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    var uRL = redirectToURL.InputParameters.Named("URL");
    destinationNode.SetArgumentValue(uRL, "If(Client.LastURL = \"\", GetOwnerURLPath(), Client.LastURL)\n\n");
    
    var invalidLogin = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("InvalidLogin");
    invalidLogin.Exception = eSpace.InvalidLogin;
    invalidLogin.HorizontalPosition = 7771;
    invalidLogin.VerticalPosition = 686;
    
    var assignNode3 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(invalidLogin, 1600);
    assignNode3.Label = "Clear IsExecuting";
    
    var assignment3 = assignNode3.CreateAssignment("IsExecuting", "False");
    
    var messageNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(assignNode3, 1600);
    messageNode.Label = "Exception Message";
    messageNode.SetMessage("InvalidLogin.ExceptionMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode3 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1828);
    
    var allExceptions = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 9829;
    allExceptions.VerticalPosition = 686;
    
    var assignNode4 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1600);
    assignNode4.Label = "Clear IsExecuting";
    
    var assignment4 = assignNode4.CreateAssignment("IsExecuting", "False");
    
    var messageNode2 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(assignNode4, 1600);
    messageNode2.Label = "Exception Message";
    messageNode2.SetMessage("AllExceptions.ExceptionMessage");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1828);
    
    var commentNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 4343;
    commentNode.Text = "Capturing raised InvalidLogin \nexceptions to log login attempts.";
    commentNode.VerticalPosition = 1143;
    commentNode.CreateConnector(invalidLogin);
    
    var layoutBlankInstance = login.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutBlank = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutBlank");
    layoutBlankInstance.SourceBlock = layoutBlank;
    var enableAccessibilityFeatures = layoutBlank.InputParameters.Named("EnableAccessibilityFeatures");
    var extendedClass = layoutBlank.InputParameters.Named("ExtendedClass");
    layoutBlankInstance.SetArgumentValue(extendedClass, null);
    
    var placeholderContentWidget = layoutBlankInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var container = placeholderContentWidget.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Gap = null;
    container.SetStyle("\"login-screen\"");
    
    var loginForm = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("LoginForm");
    loginForm.Gap = null;
    loginForm.SetStyle("\"login-form\"");
    loginForm.Width = "";
    
    var container2 = loginForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.CustomStyle = "text-align: center;";
    container2.Gap = null;
    container2.SetStyle("\"login-logo\"");
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.CustomStyle = "text-align: center;";
    container3.Gap = null;
    
    var image = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.CustomStyle = "height: 100px;";
    var logo = eSpace.Images.Named("Logo");
    image.Image = logo;
    
    var extendedProperty = image.CreateExtendedProperty();
    extendedProperty.Property = "alt";
    extendedProperty.SetValue("\"\"");
    
    var advancedHtml = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
    advancedHtml.Gap = null;
    advancedHtml.Tag = "h1";
    
    var expression = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.Example = "Application Title";
    expression.SetStyle("\"heading5 text-neutral-8\"");
    expression.SetValue("GetEntryEspaceName()");
    
    var extendedProperty2 = advancedHtml.CreateExtendedProperty();
    extendedProperty2.Property = "class";
    extendedProperty2.SetValue("\"margin-top-base\"");
    
    var container4 = loginForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Gap = null;
    container4.SetStyle("\"login-inputs margin-top-m\"");
    
    var container5 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Gap = null;
    
    var animatedLabelInstance = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var animatedLabel = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("AnimatedLabel");
    animatedLabelInstance.SourceBlock = animatedLabel;
    // delete default content in Label placeholder
    var placeholderContentWidget2 = animatedLabelInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Label");
    placeholderContentWidget2.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Input placeholder
    var placeholderContentWidget3 = animatedLabelInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Input");
    placeholderContentWidget3.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass2 = animatedLabel.InputParameters.Named("ExtendedClass");
    var eventHandler = animatedLabelInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    
    var label = placeholderContentWidget2.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label.Gap = null;
    
    var textWidget = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label.Widgets.First();
    textWidget.Text = "Username";
    
    var input_UsernameVal = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_UsernameVal");
    input_UsernameVal.SetEnabled("True");
    input_UsernameVal.SetMandatory("True");
    input_UsernameVal.MaxLength = 250;
    input_UsernameVal.SetVariable("Username");
    var builtinEvent = input_UsernameVal.OnChange;
    label.TargetWidget = input_UsernameVal;
    
    var container6 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Gap = null;
    container6.SetStyle("\"margin-top-base\"");
    
    var animatedLabelInstance2 = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    animatedLabelInstance2.SourceBlock = animatedLabel;
    // delete default content in Label placeholder
    var placeholderContentWidget4 = animatedLabelInstance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Label");
    placeholderContentWidget4.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Input placeholder
    var placeholderContentWidget5 = animatedLabelInstance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Input");
    placeholderContentWidget5.Widgets.ToList().ForEach(w => w.Delete());
    var eventHandler2 = animatedLabelInstance2.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    
    var label2 = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label2.Gap = null;
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label2.Widgets.First();
    textWidget2.Text = "Password";
    
    var input_PasswordVal = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_PasswordVal");
    input_PasswordVal.SetEnabled("True");
    input_PasswordVal.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Password;
    input_PasswordVal.SetMandatory("True");
    input_PasswordVal.MaxLength = null;
    input_PasswordVal.SetStyle("\"form-control login-password\"");
    input_PasswordVal.SetVariable("Password");
    var builtinEvent2 = input_PasswordVal.OnChange;
    label2.TargetWidget = input_PasswordVal;
    
    var container7 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Gap = null;
    container7.SetStyle("\"margin-top-l\"");
    
    var columns2Instance = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var columns2 = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Columns2");
    columns2Instance.SourceBlock = columns2;
    var phoneBehavior = columns2.InputParameters.Named("PhoneBehavior");
    columns2Instance.SetArgumentValue(phoneBehavior, null);
    var gutterSize = columns2.InputParameters.Named("GutterSize");
    columns2Instance.SetArgumentValue(gutterSize, null);
    var tabletBehavior = columns2.InputParameters.Named("TabletBehavior");
    columns2Instance.SetArgumentValue(tabletBehavior, null);
    var extendedClass3 = columns2.InputParameters.Named("ExtendedClass");
    
    var placeholderContentWidget6 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var alignCenterInstance = placeholderContentWidget6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var utilities = outSystemsUI.MobileFlows.Named("Utilities");
    var alignCenter = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("AlignCenter");
    alignCenterInstance.SourceBlock = alignCenter;
    var isHorizontal = alignCenter.InputParameters.Named("IsHorizontal");
    alignCenterInstance.SetArgumentValue(isHorizontal, null);
    var extendedClass4 = alignCenter.InputParameters.Named("ExtendedClass");
    
    var placeholderContentWidget7 = alignCenterInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var checkbox1 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("Checkbox1");
    checkbox1.SetEnabled("True");
    checkbox1.SetVariable("Remember");
    var builtinEvent3 = checkbox1.OnChange;
    
    var extendedProperty3 = checkbox1.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Remember me the password\"");
    
    var label3 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label3.Gap = null;
    label3.SetStyle("\"font-size-s margin-left-s\"");
    label3.TargetWidget = checkbox1;
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label3.Widgets.First();
    textWidget3.Text = "Remember me";
    
    var placeholderContentWidget8 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container8 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.CustomStyle = "text-align: right;";
    container8.Gap = null;
    
    var link = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    link.SetEnabled("True");
    link.Gap = null;
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)link.Widgets.First();
    textWidget4.Text = "Forgot password?";
    var builtinEvent4 = link.OnClick;
    builtinEvent4.Destination = forgotPassword;
    
    var extendedProperty4 = link.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Forgot password? Click here to recover it\"");
    
    var container9 = loginForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Gap = null;
    container9.SetStyle("\"login-button margin-top-l\"");
    
    var buttonLoadingInstance = container9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    var buttonLoading = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ButtonLoading");
    buttonLoadingInstance.SourceBlock = buttonLoading;
    // delete default content in Button placeholder
    var placeholderContentWidget9 = buttonLoadingInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Button");
    placeholderContentWidget9.Widgets.ToList().ForEach(w => w.Delete());
    var showLabelOnLoading = buttonLoading.InputParameters.Named("ShowLabelOnLoading");
    buttonLoadingInstance.SetArgumentValue(showLabelOnLoading, null);
    var extendedClass5 = buttonLoading.InputParameters.Named("ExtendedClass");
    buttonLoadingInstance.SetArgumentValue(extendedClass5, "\"full-width\"");
    var isLoading = buttonLoading.InputParameters.Named("IsLoading");
    buttonLoadingInstance.SetArgumentValue(isLoading, "IsExecuting");
    var eventHandler3 = buttonLoadingInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    
    var button = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    button.Widgets.ToList().ForEach(x => x.Delete());
    button.SetEnabled("True");
    button.Gap = null;
    button.SetStyle("\"btn btn-primary\"");
    button.Width = "(fill parent)";
    
    var container10 = button.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Gap = null;
    container10.SetStyle("\"osui-btn-loading__spinner-animation\"");
    
    var textWidget5 = button.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.Text = "Login";
    var builtinEvent5 = button.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent5.Destination = login2;
}
```
