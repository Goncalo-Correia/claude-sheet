# ModelQL queries

Reusable queries for the Service Studio MCP `runQuery` tool. They query the **module model** at design time, not the database. A query starts with `Root { … }`, and every field needs a selection set.

Save useful ones here as `<purpose>.graphql` with a comment saying what each one answers.

## Examples

All server actions:

```graphql
Root { ServerActions { Name } }
```

## Model API probes

Some checks can't be written in ModelQL because they need node positions. These are `applyModelApiCode` lambdas that end in `throw new Exception("PROBE:…")`, so they save nothing and return their result in `exceptionMessage`. Strip the leading `//` comment lines before sending the code.

- [`overlapping-nodes.cs`](overlapping-nodes.cs): overlapping nodes in every action flow (rule in `docs/conventions.md`).
