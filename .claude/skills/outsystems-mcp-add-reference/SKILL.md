---
name: outsystems-mcp-add-reference
description: Team-validated recipe to ADD a new dependency (reference to another module's public actions / entities / structures) to an OutSystems 11 module through the Service Studio MCP, which the official servicestudio-mcp-oml skill says is unsupported. Works by loading the producer's OML with LoadESpace and passing its signature to eSpace.AddDependency(SignatureInformation). Use whenever a task needs an element from a module the consumer does not reference yet, instead of asking the user to use Manage Dependencies; use together with servicestudio-mcp-oml.
---

# Add a module reference via the Service Studio MCP (team-validated)

`servicestudio-mcp-oml` § 8 says new dependencies can't be added via MCP:
`eSpace.AddDependency(globalKey)` / `RefreshDependency(globalKey)` fail with
`The event IModelServices.ResolveModuleSignature must be set first`, because the
sidecar has no resolver to fetch the producer's signature from the server.

The workaround: **give the Model API the producer's signature yourself**, by loading
the producer's OML from disk inside the consumer's lambda. No resolver is involved.

Verified 2026-10-23 on O11 (consumer `Andre_sSandbox` Reactive, producer
`Sonnet_FieldReporting_CS` Service): the reference was added with only the chosen
elements, which came back as `ReferenceAction` / `ReferenceEntity` (true references,
not copies). `omlMerge` was accepted and the module had 0 errors.

## Prerequisites

- Producer and consumer are both **open in Service Studio** (`listApps` shows them with
  `isOpen: true`).
- The elements you want are **Public** in the producer.
- You have a session token (`createSessionToken`) — see `servicestudio-mcp-oml`.

## Step 1 — get the producer's OML on disk

1. `startOmlEditSession` on the **producer**.
2. Make any read call on the producer (e.g. a throw-probe `applyModelApiCode`) so the
   host snapshots it. The working copy appears as
   `%TEMP%\ServiceStudio.MCPServer\<random-id>-in.oml` (the newest `-in.oml`).
3. **Copy it** to the scratchpad (or any stable folder): the original is deleted when
   the session ends.
4. `endOmlEditSession` on the producer (it has no changes).

Step 3 checks that the file really is the producer (`producer.Name`). Otherwise the
newest `-in.oml` could be the wrong module.

An OML saved from Service Studio or downloaded from Service Center should also work
with `LoadESpace`, but **this has not been tested**.

## Step 2 — probe in the consumer (nothing saved)

`startOmlEditSession` on the **consumer**, then `applyModelApiCode` with the template in
[`examples/AddReferenceFromProducerOml.cs`](examples/AddReferenceFromProducerOml.cs),
keeping the final `throw`. Fill in:

- the producer OML path and the producer's name,
- the element names to bring.

The probe reports what was added and what the reference contains afterwards. Expected:
`ReferenceAction:<name>`, `ReferenceEntity:<name>`, etc. Check `validationMessages` for
errors (pre-existing warnings are fine).

## Step 3 — apply and merge

1. Run the same lambda **without** the `throw` → you get a `mutatedOmlPath`.
2. `omlMerge` with that path.
3. `getValidationMessages` with `filter: ["error"]` → expect `[]`.
4. Use the new elements in the same session or a later one through
   `eSpace.References.Named("<Producer>")` (`.ServerActions`, `.Entities`, …), as with any
   resident reference.
5. `endOmlEditSession` on the consumer when done. `discardedChanges: true` after a
   successful merge is normal: only the working copy is discarded.
6. Ask the user to check **Manage Dependencies** in Service Studio and publish.

## Choosing what to reference

`SignatureInformation.TargetElementKeys`:

- **List of element keys** (`producer.ServerActions.Named("X").ObjectKey`,
  `producer.Entities.Named("Y").ObjectKey`, `producer.Structures.Named("Z").ObjectKey`, …)
  → only those elements are referenced. **Prefer this**: it matches what a developer
  would tick in Manage Dependencies.
- **Empty** (the default) → only the module-level reference is created, without
  elements.

## What does NOT work (don't retry)

| Attempt | Result |
|---|---|
| `eSpace.AddDependency(globalKey)` / `RefreshDependency(globalKey)` | `ResolveModuleSignature must be set first` |
| Feed raw signature bytes (e.g. from DB table `OSSYS_ESPACE_VERSION_SIGNATURE`) | `BinarySignatureInformation.Signature` is `internal` (CS0122) |
| `StreamSignatureInformation` + `MemoryStream` | `MemoryStream` not found; `System.IO` is blocked by the sandbox |
| `Services.ModelServices.LoadModuleSignature(bytes)` with no metadata | `AbstractExtension.Key is null … provide it through the SignatureMetadata parameter` |
| Writing reference rows into `OSSYS_*` tables | Doesn't touch the OML. The platform rewrites those rows from the OML on publish |

## Not tested yet — check before relying on it

- A producer whose public elements use types from a **third module** (e.g. an action
  whose parameter is a structure from another module). `AddDependency` has an optional
  `fetchAdditionalSignature` callback for this. If the probe fails, load the third
  module's OML too and return its `GetSignatureInformation()` from that callback.
- A producer that is **not open** in Service Studio (OML from file or Service Center).
- Publishing the consumer and confirming the reference isn't flagged as outdated.
- Refreshing an existing reference the same way (`eSpace.RefreshDependencies(si)`).

When you test one of these, update this section.
