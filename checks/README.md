# Checks

Model API lambdas that check a module against `docs/conventions.md`, or fix what the conventions ask for. Each `.cs` file starts with `//` comment lines saying what it does and how to run it. Strip those lines and send the rest as the `code` of `applyModelApiCode`.

- A **report** ends in `throw new Exception("PROBE:…")`, so it saves nothing and returns its result in `exceptionMessage`.
- A **fix** saves like any other change, so merge and review it.

| Check | Kind | Checks |
|---|---|---|
| [flow-layout.cs](flow-layout.cs) | Report | Flow layout per flow: connectors, allowed diagonals (For Each loop-backs, merges into a non-End node), other diagonals, crossings, overlaps. A touched flow passes with 0 other diagonals, 0 crossings and 0 overlaps. |
| [layout-flow.cs](layout-flow.cs) | Fix | Lays out the flows in its `targets` (positions and new End nodes only). Run `flow-layout.cs` afterwards. |
| [node-names.cs](node-names.cs) | Report | Action folders and node names: actions outside a folder, unlabelled Assign / If / Message / For Each / Switch nodes, Execute names ending in a digit. A module passes with every count at 0. |

The reports are the conventions gate before every merge and review.

For a one-off question about the model, `runQuery` (ModelQL, e.g. `Root { ServerActions { Name } }`) is often enough. Add it here only if it's worth keeping.
