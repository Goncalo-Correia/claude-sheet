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
    login.AnonymousAccess = false; // default value
    login.CustomURL = false; // default value
    login.Description = "Screen where users login.";
    login.HorizontalPosition = 457;
    login.Icon = null; // default value
    login.IsDisabled = false; // default value
    login.PageName = null; // default value
    login.Public = false; // default value
    login.Shareable = false; // default value
    login.StyleSheet = null; // default value
    login.SetTitle(null); // default value
    login.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    login.VerticalPosition = 686;
    
    var username = login.CreateLocalVariable("Username");
    username.DataType = eSpace.TextType;
    username.SetDefaultValue(null); // default value
    username.Description = null; // default value
    
    var password = login.CreateLocalVariable("Password");
    password.DataType = eSpace.TextType;
    password.SetDefaultValue(null); // default value
    password.Description = null; // default value
    
    var isExecuting = login.CreateLocalVariable("IsExecuting");
    isExecuting.DataType = eSpace.BooleanType;
    isExecuting.SetDefaultValue("False");
    isExecuting.Description = null; // default value
    
    var remember = login.CreateLocalVariable("Remember");
    remember.DataType = eSpace.BooleanType;
    remember.SetDefaultValue(null); // default value
    remember.Description = null; // default value
    login.OnDestroy.Destination = null; // default value
    login.OnReady.Destination = null; // default value
    login.OnRender.Destination = null; // default value
    
    var forgotPassword = login.CreateScreenAction("ForgotPassword");
    forgotPassword.Description = null; // default value
    
    var startNode = forgotPassword.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var onInitialize = login.CreateScreenAction("OnInitialize");
    onInitialize.Description = null; // default value
    
    var startNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var assignNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 3192;
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Not executing";
    assignNode.VerticalPosition = 2964;
    
    var assignment = assignNode.CreateAssignment("IsExecuting", "False");
    
    var endNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 5120;
    login.OnInitialize.Destination = onInitialize;
    
    var endNode2 = forgotPassword.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(startNode, 4320);
    endNode2.IsDisabled = false; // default value
    startNode2.Target = assignNode;
    assignNode.Target = endNode;
    
    var login2 = login.CreateScreenAction("Login");
    login2.Description = null; // default value
    
    var startNode3 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 2057;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 686;
    
    var assignNode2 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode3, 1600);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Start executing";
    
    var assignment2 = assignNode2.CreateAssignment("IsExecuting", "True");
    
    var doLogin = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DoLogin").ConnectedBelow(assignNode2, 1600);
    var doLogin2 = eSpace.ServerActions.Named("DoLogin");
    doLogin.Action = doLogin2;
    doLogin.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    doLogin.IsDisabled = false; // default value
    doLogin.ServerRequestTimeout = null; // default value
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
    feedbackMessageClose.IsDisabled = false; // default value
    
    var destinationNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ConnectedBelow(feedbackMessageClose, 1600);
    var redirectToURL = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IExternalSite>().Named("RedirectToURL");
    destinationNode.Destination = redirectToURL;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    var uRL = redirectToURL.InputParameters.Named("URL");
    destinationNode.SetArgumentValue(uRL, "If(Client.LastURL = \"\", GetOwnerURLPath(), Client.LastURL)\n\n");
    
    var invalidLogin = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("InvalidLogin");
    invalidLogin.AbortTransaction = true; // default value
    invalidLogin.Exception = eSpace.InvalidLogin;
    invalidLogin.HorizontalPosition = 7771;
    invalidLogin.IsDisabled = false; // default value
    invalidLogin.LogError = true; // default value
    invalidLogin.VerticalPosition = 686;
    
    var assignNode3 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(invalidLogin, 1600);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Clear IsExecuting";
    
    var assignment3 = assignNode3.CreateAssignment("IsExecuting", "False");
    
    var messageNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(assignNode3, 1600);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = "Exception Message";
    messageNode.SetMessage("InvalidLogin.ExceptionMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode3 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1828);
    endNode3.IsDisabled = false; // default value
    
    var allExceptions = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = true; // default value
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 9829;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 686;
    
    var assignNode4 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1600);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Clear IsExecuting";
    
    var assignment4 = assignNode4.CreateAssignment("IsExecuting", "False");
    
    var messageNode2 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(assignNode4, 1600);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Exception Message";
    messageNode2.SetMessage("AllExceptions.ExceptionMessage");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode4 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1828);
    endNode4.IsDisabled = false; // default value
    
    var commentNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 4343;
    commentNode.IsDisabled = false; // default value
    commentNode.IsReminder = false; // default value
    commentNode.Text = "Capturing raised InvalidLogin \nexceptions to log login attempts.";
    commentNode.VerticalPosition = 1143;
    commentNode.CreateConnector(invalidLogin);
    
    var layoutBlankInstance = login.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutBlankInstance.CustomStyle = null; // default value
    layoutBlankInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutBlank = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutBlank");
    layoutBlankInstance.SourceBlock = layoutBlank;
    var enableAccessibilityFeatures = layoutBlank.InputParameters.Named("EnableAccessibilityFeatures");
    layoutBlankInstance.SetArgumentValue(enableAccessibilityFeatures, null); // default value
    var extendedClass = layoutBlank.InputParameters.Named("ExtendedClass");
    layoutBlankInstance.SetArgumentValue(extendedClass, null);
    
    var placeholderContentWidget = layoutBlankInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
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
    container.SetStyle("\"login-screen\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var loginForm = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("LoginForm");
    loginForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    loginForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    loginForm.ColumnGap = null; // default value
    loginForm.ColumnSpan = null; // default value
    loginForm.CustomStyle = null; // default value
    loginForm.FlexBasis = null; // default value
    loginForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    loginForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    loginForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    loginForm.Gap = null;
    loginForm.GridColumns = null; // default value
    loginForm.GridRows = null; // default value
    loginForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    loginForm.RowGap = null; // default value
    loginForm.RowSpan = null; // default value
    loginForm.SetStyle("\"login-form\"");
    loginForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    loginForm.Width = "";
    
    var container2 = loginForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container2.Animate = false; // default value
    container2.ColumnGap = null; // default value
    container2.ColumnSpan = null; // default value
    container2.CustomStyle = "text-align: center;";
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
    container2.SetStyle("\"login-logo\"");
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
    container3.CustomStyle = "text-align: center;";
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
    container3.SetStyle(null); // default value
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var image = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = "height: 100px;";
    image.DefaultImage = null; // default value
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var logo = eSpace.Images.Named("Logo");
    image.Image = logo;
    image.SetImageContent(null); // default value
    image.RowSpan = null; // default value
    image.SetStyle(null); // default value
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Static; // default value
    image.SetUrl(null); // default value
    image.Width = ""; // default value
    
    var extendedProperty = image.CreateExtendedProperty();
    extendedProperty.Property = "alt";
    extendedProperty.SetValue("\"\"");
    
    var advancedHtml = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IAdvancedHtml>();
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
    
    var expression = advancedHtml.Content.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = "Application Title";
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle("\"heading5 text-neutral-8\"");
    expression.SetValue("GetEntryEspaceName()");
    expression.Width = ""; // default value
    
    var extendedProperty2 = advancedHtml.CreateExtendedProperty();
    extendedProperty2.Property = "class";
    extendedProperty2.SetValue("\"margin-top-base\"");
    
    var container4 = loginForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"login-inputs margin-top-m\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var container5 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle(null); // default value
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var animatedLabelInstance = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    animatedLabelInstance.CustomStyle = null; // default value
    animatedLabelInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
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
    animatedLabelInstance.SetArgumentValue(extendedClass2, null); // default value
    var eventHandler = animatedLabelInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler.Handler = null; // default value
    
    var label = placeholderContentWidget2.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label.ColumnGap = null; // default value
    label.ColumnSpan = null; // default value
    label.CustomStyle = null; // default value
    label.FlexBasis = null; // default value
    label.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label.Gap = null;
    label.GridColumns = null; // default value
    label.GridRows = null; // default value
    label.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label.RowGap = null; // default value
    label.RowSpan = null; // default value
    label.SetStyle(null); // default value
    label.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label.Width = "(fill parent)"; // default value
    
    var textWidget = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label.Widgets.First();
    textWidget.CustomStyle = null; // default value
    textWidget.Name = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Username";
    
    var input_UsernameVal = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_UsernameVal");
    input_UsernameVal.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_UsernameVal.ColumnSpan = null; // default value
    input_UsernameVal.CustomStyle = null; // default value
    input_UsernameVal.SetEnabled("True");
    input_UsernameVal.FlexBasis = null; // default value
    input_UsernameVal.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_UsernameVal.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    input_UsernameVal.SetMandatory("True");
    input_UsernameVal.MaxLength = 250;
    input_UsernameVal.SetPrompt(null); // default value
    input_UsernameVal.RowSpan = null; // default value
    input_UsernameVal.SetStyle("\"form-control\""); // default value
    input_UsernameVal.SetVariable("Username");
    input_UsernameVal.Width = "(fill parent)"; // default value
    var builtinEvent = input_UsernameVal.OnChange;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = null; // default value
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label.TargetWidget = input_UsernameVal;
    
    var container6 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"margin-top-base\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var animatedLabelInstance2 = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    animatedLabelInstance2.CustomStyle = null; // default value
    animatedLabelInstance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    animatedLabelInstance2.SourceBlock = animatedLabel;
    // delete default content in Label placeholder
    var placeholderContentWidget4 = animatedLabelInstance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Label");
    placeholderContentWidget4.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Input placeholder
    var placeholderContentWidget5 = animatedLabelInstance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Input");
    placeholderContentWidget5.Widgets.ToList().ForEach(w => w.Delete());
    animatedLabelInstance2.SetArgumentValue(extendedClass2, null); // default value
    var eventHandler2 = animatedLabelInstance2.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler2.Handler = null; // default value
    
    var label2 = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label2.ColumnGap = null; // default value
    label2.ColumnSpan = null; // default value
    label2.CustomStyle = null; // default value
    label2.FlexBasis = null; // default value
    label2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label2.Gap = null;
    label2.GridColumns = null; // default value
    label2.GridRows = null; // default value
    label2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label2.RowGap = null; // default value
    label2.RowSpan = null; // default value
    label2.SetStyle(null); // default value
    label2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label2.Width = "(fill parent)"; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label2.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Password";
    
    var input_PasswordVal = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_PasswordVal");
    input_PasswordVal.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_PasswordVal.ColumnSpan = null; // default value
    input_PasswordVal.CustomStyle = null; // default value
    input_PasswordVal.SetEnabled("True");
    input_PasswordVal.FlexBasis = null; // default value
    input_PasswordVal.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_PasswordVal.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Password;
    input_PasswordVal.SetMandatory("True");
    input_PasswordVal.MaxLength = null;
    input_PasswordVal.SetPrompt(null); // default value
    input_PasswordVal.RowSpan = null; // default value
    input_PasswordVal.SetStyle("\"form-control login-password\"");
    input_PasswordVal.SetVariable("Password");
    input_PasswordVal.Width = "(fill parent)"; // default value
    var builtinEvent2 = input_PasswordVal.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = null; // default value
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label2.TargetWidget = input_PasswordVal;
    
    var container7 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container7.SetStyle("\"margin-top-l\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var columns2Instance = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance.CustomStyle = null; // default value
    columns2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
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
    columns2Instance.SetArgumentValue(extendedClass3, null); // default value
    
    var placeholderContentWidget6 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var alignCenterInstance = placeholderContentWidget6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    alignCenterInstance.CustomStyle = null; // default value
    alignCenterInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var utilities = outSystemsUI.MobileFlows.Named("Utilities");
    var alignCenter = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("AlignCenter");
    alignCenterInstance.SourceBlock = alignCenter;
    var isHorizontal = alignCenter.InputParameters.Named("IsHorizontal");
    alignCenterInstance.SetArgumentValue(isHorizontal, null);
    var extendedClass4 = alignCenter.InputParameters.Named("ExtendedClass");
    alignCenterInstance.SetArgumentValue(extendedClass4, null); // default value
    
    var placeholderContentWidget7 = alignCenterInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var checkbox1 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("Checkbox1");
    checkbox1.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    checkbox1.ColumnSpan = null; // default value
    checkbox1.CustomStyle = null; // default value
    checkbox1.SetEnabled("True");
    checkbox1.FlexBasis = null; // default value
    checkbox1.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    checkbox1.RowSpan = null; // default value
    checkbox1.SetStyle("\"checkbox\""); // default value
    checkbox1.SetVariable("Remember");
    var builtinEvent3 = checkbox1.OnChange;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = null; // default value
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty3 = checkbox1.CreateExtendedProperty();
    extendedProperty3.Property = "aria-label";
    extendedProperty3.SetValue("\"Remember me the password\"");
    
    var label3 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label3.ColumnGap = null; // default value
    label3.ColumnSpan = null; // default value
    label3.CustomStyle = null; // default value
    label3.FlexBasis = null; // default value
    label3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label3.Gap = null;
    label3.GridColumns = null; // default value
    label3.GridRows = null; // default value
    label3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label3.RowGap = null; // default value
    label3.RowSpan = null; // default value
    label3.SetStyle("\"font-size-s margin-left-s\"");
    label3.TargetWidget = checkbox1;
    label3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label3.Width = "(fill parent)"; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label3.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Remember me";
    
    var placeholderContentWidget8 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container8 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = false; // default value
    container8.ColumnGap = null; // default value
    container8.ColumnSpan = null; // default value
    container8.CustomStyle = "text-align: right;";
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
    container8.SetStyle(null); // default value
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var link = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    link.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link.ColumnGap = null; // default value
    link.ColumnSpan = null; // default value
    link.SetConfirmationMessage(null); // default value
    link.CustomStyle = null; // default value
    link.SetEnabled("True");
    link.FlexBasis = null; // default value
    link.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link.Gap = null;
    link.GridColumns = null; // default value
    link.GridRows = null; // default value
    link.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link.RowGap = null; // default value
    link.RowSpan = null; // default value
    link.SetStyle(null); // default value
    link.SetVisible("True"); // default value
    link.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link.Width = ""; // default value
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)link.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Forgot password?";
    var builtinEvent4 = link.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = forgotPassword;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty4 = link.CreateExtendedProperty();
    extendedProperty4.Property = "aria-label";
    extendedProperty4.SetValue("\"Forgot password? Click here to recover it\"");
    
    var container9 = loginForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container9.SetStyle("\"login-button margin-top-l\"");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var buttonLoadingInstance = container9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    buttonLoadingInstance.CustomStyle = null; // default value
    buttonLoadingInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
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
    eventHandler3.Handler = null; // default value
    
    var button = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
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
    button.SetStyle("\"btn btn-primary\"");
    button.SetVisible("True"); // default value
    button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button.Width = "(fill parent)";
    
    var container10 = button.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container10.SetStyle("\"osui-btn-loading__spinner-animation\"");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var textWidget5 = button.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Login";
    var builtinEvent5 = button.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent5.Destination = login2;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
}
