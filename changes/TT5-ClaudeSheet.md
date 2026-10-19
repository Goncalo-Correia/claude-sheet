# TT5 · ClaudeSheet changes

Every `applyModelApiCode` save for TT5 in ClaudeSheet, oldest first. Logged when the module was published.

## Save 1 · 2026-10-19 11:36:30 UTC

- **Published:** 2026-10-19 11:45:45 UTC (manual 1-Click Publish; logged by Claude, applied by the main session)
- **Outcome:** saved
- **Imports:** -

```csharp
eSpace => {
  var admin = eSpace.CreateMobileFlow("Admin");
  admin.Description = "Back-office screens for SheetAdmin: clients, projects and assignments, holidays, users and settings (US6-US9, US14). Every screen in this flow is restricted to SheetAdmin; its Menu link is visible only with CheckSheetAdminRole().";
}
```
