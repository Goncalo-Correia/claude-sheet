# Checks

Model API lambdas that check a module against `docs/conventions.md`, or fix what the conventions ask for. Each `.cs` file starts with `//` comment lines saying what it does and how to run it. Strip those lines and send the rest as the `code` of `applyModelApiCode`.

- A **report** ends in `throw new Exception("PROBE:…")`, so it saves nothing and returns its result in `exceptionMessage`.
- A **fix** saves like any other change, so merge and review it.

For a one-off question about the model, `runQuery` (ModelQL, e.g. `Root { ServerActions { Name } }`) is often enough. Add a check here only if it is worth keeping.
