// Published: 2026-10-16T11:38:00+00:00 (manual 1-Click Publish; logged by Claude, applied by the main session)
// Module:   ClaudeSheet
// When:     2026-10-16T11:11:54+00:00
// Story:    US5
// Outcome:  saved
// Imports:  -
eSpace => { var role = eSpace.References.Named("ClaudeSheet_CS").Roles.Named("SheetUser2"); role.Name = "SheetUser"; var common = eSpace.MobileFlows.Named("Common"); foreach (var n in new[] { "Login", "InvalidPermissions" }) { var sc = common.Nodes.OfType<IMobileScreen>().Named(n); if (!sc.Roles.Contains(eSpace.AnonymousRole)) sc.Roles.Add(eSpace.AnonymousRole); } }
