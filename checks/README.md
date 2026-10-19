# Checks

Model API lambdas that check a module against `docs/conventions.md`, or fix what the conventions ask for. Each file says what the check does, how to run it and what passing means, then gives the code in one `csharp` block. Send that block as the `code` of `applyModelApiCode`.

- A **report** ends in `throw new Exception("PROBE:…")`, so it saves nothing and returns its result in `exceptionMessage`.
- A **fix** saves like any other change, so merge and review it.

| Check | Kind | Checks |
|---|---|---|
| [flow-layout.md](flow-layout.md) | Report | Flow layout: connectors, allowed diagonals, other diagonals, crossings, overlaps per flow |
| [layout-flow.md](layout-flow.md) | Fix | Lays out the flows in its `targets` (positions and new End nodes only) |
| [node-names.md](node-names.md) | Report | Action folders and node names: actions outside a folder, unlabelled nodes, Execute names ending in a digit |

The reports are the conventions gate before every merge and review.

For a one-off question about the model, `runQuery` (ModelQL, e.g. `Root { ServerActions { Name } }`) is often enough. Add it here only if it's worth keeping.
