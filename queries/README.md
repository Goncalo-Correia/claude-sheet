# ModelQL queries

Reusable queries for the Service Studio MCP `runQuery` tool. They query the **module model** at design time, not the database. A query starts with `Root { … }`, and every field needs a selection set.

Save useful ones here as `<purpose>.graphql` with a comment saying what each one answers.

## Examples

All server actions:

```graphql
Root { ServerActions { Name } }
```

## Model API lambdas

Some checks and fixes can't be written in ModelQL because they need node positions. These are `applyModelApiCode` lambdas. Strip the leading `//` comment lines before sending the code. A **report** ends in `throw new Exception("PROBE:…")`, so it saves nothing and returns its result in `exceptionMessage`. A **fix** saves like any other change, so merge and review it.

- [`flow-layout.cs`](flow-layout.cs): **report** (saves nothing). Per flow: vertical/horizontal connectors, allowed diagonals (For Each loop-backs, merges into a non-End node), other diagonals, crossings and overlapping nodes. Checks `docs/conventions.md` › Flow layout.
- [`layout-flow.cs`](layout-flow.cs): **lays out** the flows listed in its `targets` and **saves** (not a probe). Positions plus new End nodes only. Run `flow-layout.cs` afterwards.
- [`node-names.cs`](node-names.cs): **report** (saves nothing). Server/client actions outside a folder, unlabelled Assign / If / Message / For Each / Switch nodes per flow, and Execute nodes whose name ends in a digit. Checks `docs/conventions.md` › Action folders and Node names; a module passes with every count at 0.
