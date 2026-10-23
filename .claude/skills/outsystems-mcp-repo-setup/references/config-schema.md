# Scaffold config

`scripts/scaffold.mjs --config <file>` reads this JSON. Only `projectName` is required. Everything else has a default.

```jsonc
{
  "projectName": "Sales",                        // required
  "description": "Order management for the team", // one line; default "TODO: describe the project"
  "platform": "OutSystems 11",                   // default
  "environments": [                              // default: one Development row with url "TODO"
    { "name": "Development", "url": "https://dev.example.com" },
    { "name": "Production",  "url": "TODO" }
  ],
  "modules": [                                   // default: []
    { "name": "Sales",    "app": "Sales", "type": "Reactive", "layer": "End-user", "version": "" },
    { "name": "Sales_CS", "app": "Sales", "type": "Service",  "layer": "Core" }
  ],
  "references": ["OutSystemsUI", "Users"],       // platform/external modules the project depends on; default []
  "mcpUrl": "http://127.0.0.1:41820/mcp",        // default
  "vendorSkills": ["servicestudio-mcp-oml", "outsystems-mcp-implementation", "outsystems-mcp-code-review", "outsystems-mcp-publish"], // copied from ~/.claude/skills; default []
  "skillsSource": "C:/Users/me/.claude/skills"   // optional override of where vendorSkills come from
}
```

`type` uses the `moduleType` values from `listApps`: `Reactive`, `Mobile`, `Traditional`, `ReactiveLibrary`, `MobileLibrary`, `TraditionalLibrary`, `Service`.

`layer` is one of `End-user`, `Core`, `Foundation`, `Library` (the OutSystems Architecture Canvas).

## CLI flags

| Flag | Meaning |
|---|---|
| `--target <dir>` | Repo to set up (default: current directory) |
| `--config <file>` | Config JSON (required) |
| `--dry-run` | Print the report without writing anything |

## Report

The script prints JSON on stdout: `{ "dryRun", "target", "results": [{ "path", "status", "note" }] }`.

| Status | Meaning |
|---|---|
| `created` | File did not exist and was written |
| `kept` | File exists and matches the template; nothing to do |
| `kept-differs` | File exists and differs from the template. It was not touched; offer a manual merge |
| `merged` | A config file (`.gitignore`, `.gitattributes`, `.mcp.json`, `.claude/settings.json`) got missing entries added |
| `unchanged` | A config file already had everything |
| `skipped` | Not applicable (e.g. a vendored skill not found) |
