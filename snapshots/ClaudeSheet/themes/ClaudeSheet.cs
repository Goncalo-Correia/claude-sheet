(OutSystems.Model.IESpace eSpace) => {
    
    /*** creating IMobileTheme 'ClaudeSheet' ***/
    var claudeSheet = eSpace.CreateMobileTheme("ClaudeSheet");
    claudeSheet.AddOrUpdateThemeValues(new Dictionary<ThemeProperty, string> {
        {ThemeProperty.AppPrimaryColor, "#dc3023"},
        {ThemeProperty.SecondaryColor, "#dc3023"},
        {ThemeProperty.PrimaryColor, "#dc3023"},
        {ThemeProperty.CreatedWithoutCustomCSS, "true"},
    });
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var outSystemsUI2 = outSystemsUI.MobileThemes.Named("OutSystemsUI");
    claudeSheet.BaseTheme = outSystemsUI2;
    claudeSheet.Columns = 12; // default value
    claudeSheet.ColumnWidth = 60; // default value
    claudeSheet.Description = "Application theme";
    claudeSheet.Folder = null; // default value
    claudeSheet.Footer = null; // default value
    claudeSheet.GridType = OutSystems.Model.Enumerations.GridType.Fluid;
    claudeSheet.GutterWidth = 20; // default value
    claudeSheet.GutterWidthPercentage = 30; // default value
    claudeSheet.Header = null; // default value
    claudeSheet.IconLibrary = null;
    var layouts = eSpace.MobileFlows.Named("Layouts");
    var layoutTopMenu = layouts.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopMenu");
    claudeSheet.Layout = layoutTopMenu;
    claudeSheet.MaxWidth = 1280;
    var common = eSpace.MobileFlows.Named("Common");
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    claudeSheet.Menu = menu;
    claudeSheet.MinWidth = null; // default value
    claudeSheet.Public = true;
    claudeSheet.Shareable = true;
    claudeSheet.StyleSheet = "/* ===================================================================== */\r\n/*  The following CSS was created using Theme Editor.                    */\r\n/*  To modify this CSS, click \"Open Theme Editor\".                       */\r\n/* ===================================================================== */\r\n\r\n/* ===================================================================== */\r\n/*  Root - CSS Variables                                                 */\r\n/* ===================================================================== */\r\n\r\n:root {\r\n\t\r\n    /* Color - Brand */\r\n    --color-primary: #dc3023;\r\n    --color-secondary: #dc3023;\r\n\r\n    --color-primary-hover: #a5241a;\r\n    --color-primary-selected: rgba(220, 48, 35, .12);\r\n\t\r\n    /* Dark Theme */\r\n    --background-dark-theme: rgba(255, 255, 255, .1);\r\n    --background-dark-theme-hover: rgba(255, 255, 255, .2);\r\n}\r\n\r\n\r\n.ms-theme-start { }\r\n\r\n/* MySheet - status colours and calendar day classes (US-000) */\r\n:root {\r\n    --sheet-confirmed: #2e9e5b;\r\n    --sheet-dayoff: #6b5bd2;\r\n    --sheet-holiday: #e0a100;\r\n    --sheet-pending: #e05a47;\r\n    --sheet-accent: var(--color-primary);\r\n    --sheet-card: #ffffff;\r\n    --sheet-bg: #f4f5f7;\r\n}\r\n\r\n.day-confirmed { background-color: var(--sheet-confirmed); border-color: var(--sheet-confirmed); color: #ffffff; }\r\n.day-off { background-color: var(--sheet-dayoff); border-color: var(--sheet-dayoff); color: #ffffff; }\r\n.day-holiday { background-color: var(--sheet-holiday); border-color: var(--sheet-holiday); color: #ffffff; }\r\n.day-pending { background-color: var(--sheet-card); border: 1px dashed var(--sheet-pending); color: var(--sheet-pending); }\r\n.day-future { background-color: var(--sheet-card); color: var(--color-neutral-6); }\r\n.day-weekend { background-color: var(--sheet-bg); color: var(--color-neutral-6); }\r\n";
}
