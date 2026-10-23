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

Verified on O11 (Reactive consumer, Service producers). The chosen elements come back as
`ReferenceAction` / `ReferenceEntity`, i.e. true references and not copies. `omlMerge` is
accepted and the module has 0 errors. Also verified for elements that use a type from a
**third module**, and for system modules that Service Studio only opens as a clone
(e.g. `Users`).

## Prerequisites

- Producer and consumer are both **open in Service Studio** (`listApps` shows them with
  `isOpen: true`). If the producer, or a third module whose types you need, is not open,
  **open it yourself** with the `outsystems-open-module` skill (see "Opening and closing
  modules" below). Don't ask the user to do it.
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

Step 2 checks that the file really is the producer (`producer.Name`). Otherwise the
newest `-in.oml` could be the wrong module.

An OML saved from Service Studio or downloaded from Service Center should also work
with `LoadESpace`, but **this has not been tested**.

## Opening and closing modules

The producer and any third module only have to be open long enough to copy their OML
(Step 1).

1. Note which modules `listApps` shows as open **before** you start.
2. Open each missing one with `outsystems-open-module`, **on the consumer's server**
   (`-SameServerAs <Consumer>`). Service Studio can have several environments
   registered, and opening the producer on another one gets the wrong module or none.
   Follow that skill's steps, including the one-line warning to the user about unsaved
   work. This opens the **last published** version, which is what the consumer should
   reference anyway. The producer's own types used by the requested elements (its
   structures and entities) come in automatically.
3. Do Step 1 for each of them (edit session → copy `-in.oml` → `endOmlEditSession`).
4. **Close the modules you opened** (only those; never one that was open before) with
   `outsystems-open-module`'s close script, then check with `listApps`. It refuses a tab
   with unsaved changes (`* ` in its header). The copied OMLs are all the next steps need.

Then continue with Step 2 in the consumer.

## Step 2 — probe in the consumer (nothing saved)

`startOmlEditSession` on the **consumer**, then `applyModelApiCode` with the template in
[`examples/AddReferenceFromProducerOml.cs`](examples/AddReferenceFromProducerOml.cs),
keeping the final `throw`. Fill in:

- the producer OML path and the producer's name,
- the element names to bring.

The probe reports what was added and what the reference contains afterwards. Expected:
`ReferenceAction:<name>`, `ReferenceEntity:<name>`, etc. Check `validationMessages` for
errors (pre-existing warnings are fine).

> **`added=[]` with no error is a silent failure.** It happens when a requested element
> uses a type from a third module whose signature you didn't supply (see
> "Third-module types" below). Always check that every requested element is in
> `added`. The template throws if one is missing.

## Step 3 — apply and merge

1. Run the same lambda **without** the `throw` → you get a `mutatedOmlPath`.
2. `omlMerge` with that path.
3. `getValidationMessages` with `filter: ["error"]` → expect no new errors.
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

## Third-module types (transitive dependencies)

Case: the producer's action has an output (or input) whose type is a structure defined in
a **third module**, and the consumer references neither.

Pass `fetchAdditionalSignature`. `AddDependency` calls it once for **every module the
producer references** that matters for the requested elements (typically `(System)` and
the third module).

- **Module that owns a type you need** → load its OML too (same Step 1: open it in
  Service Studio, copy its `-in.oml`) and return `third.GetSignatureInformation()` with
  `TargetElementKeys` = the needed element(s). The consumer gets a new reference to that
  module with just those elements, added automatically.
- **Module the consumer already references (e.g. `(System)`)** → returning a
  metadata-only `new BinarySignatureInformation { Key = k, Name = …, ModuleKind =
  ReferenceKind.eSpace, TargetElementKeys = new IKey[0] }` is enough.
- To find which module a requested key belongs to, match `k.ToString()` against
  `producer.References[i].ModuleKey.ToString()` or `third.ObjectKey.ToString()`. Log the
  keys in a probe first.

Gotchas:
- **Returning `null`** from the callback → `NullReferenceException` in
  `ReferenceInfo.FromSignatureInformation`.
- **No callback, or metadata-only for the owner of a needed type** → `added=[]`, no error.
- A lambda passed inline is **ambiguous** (CS0121: sync vs `Task<>` overload). Declare it
  as `Func<IKey, SignatureInformation> fetch = k => …;` first.
- `SignatureInformation` is abstract. Use `BinarySignatureInformation` (it has a public
  no-arg constructor).

Template: [`examples/AddReferenceWithThirdModuleType.cs`](examples/AddReferenceWithThirdModuleType.cs).

## Without the producer's OML: raw signature bytes

A hand-built `SignatureInformation` works the same way as `GetSignatureInformation()`:

```csharp
var si = new BinarySignatureInformation {
    Key = <producer module key>, Name = "<Producer>",
    ModuleKind = ReferenceKind.eSpace, ModuleType = <producer SegmentationKind>,
    SignatureBinary = bytes,                    // public setter
    TargetElementKeys = new[] { <element key> } };
eSpace.AddDependency(si, fetch);
```

Getting raw bytes from elsewhere (e.g. `OSSYS_ESPACE_VERSION_SIGNATURE`) is impractical:
the sandbox has no file I/O, so they would have to be a literal in the code. Use the
clone route below instead.

## System modules that open as a clone (e.g. `Users`)

Opening `Users` with `outsystems-open-module` (by its eSpaceKey, `-SameServerAs <Consumer>`)
makes Service Studio create an **unsaved clone, `CloneOfUsers`**, from the published
version. `listApps` then shows `CloneOfUsers` open, and `Users` with `isReference: true`.
The clone has a **new module key**, but the **same element keys** as the original. Its
signature is the original's, so:

1. Do Step 1 on `CloneOfUsers` (edit session → copy `-in.oml` → end session). Its read
   calls return dozens of validation errors (the clone doesn't compile); ignore them, and
   **never save or publish the clone**.
2. In the consumer, build the `SignatureInformation` from the clone's bytes, with the
   **original module's key and name**:
   ```csharp
   var clone = Services.ModelServices.LoadESpace(@"...\CloneOfUsers.oml");
   var csi = clone.GetSignatureInformation();
   var si = new BinarySignatureInformation {
       Key = eSpace.References.Named("Users").ModuleKey,   // or ParseKey of the original key
       Name = "Users", Description = csi.Description,
       ModuleKind = ReferenceKind.eSpace, ModuleType = csi.ModuleType,
       SignatureBinary = clone.GetSignatureBinary(),
       TargetElementKeys = new[] { clone.ServerActions.Named("User_GetIdByUsername").ObjectKey } };
   ```
   The element joins the existing `Users` reference. `fetchAdditionalSignature` is also
   called with the **clone's own key**: metadata-only is enough for it, as for
   `(System)`. Guard against a reference to the clone:
   `if (eSpace.References.Any(r => r.Name == clone.Name)) throw …`.
   Template: [`examples/AddReferenceFromSystemModuleClone.cs`](examples/AddReferenceFromSystemModuleClone.cs).
3. **The clone tab has `*`** (unsaved), so the close script refuses it. Ask the user to
   close it **without saving**.

If the consumer doesn't reference the module yet, get the original module key from
another module that references it (`otherModule.References.Named("Users").ModuleKey`).

**`(System)` cannot be opened at all**, not even as a clone. Adding new `(System)` elements
is **not possible through MCP**: copying the element from another module and building a
synthetic signature were both tested and failed (see the table below). Ask the user to
add it via *Manage Dependencies → (System)*. Elements the consumer already references
work as usual through `eSpace.References.Named("(System)")`.

## What does NOT work (don't retry)

| Attempt | Result |
|---|---|
| `eSpace.AddDependency(globalKey)` / `RefreshDependency(globalKey)` | `ResolveModuleSignature must be set first` |
| `BinarySignatureInformation.Signature` | `internal` (CS0122). Use **`SignatureBinary`** instead |
| `StreamSignatureInformation` + `MemoryStream` | `MemoryStream` not found; `System.IO` is blocked by the sandbox |
| `Services.ModelServices.LoadModuleSignature(bytes or path, metadata)` for an eSpace | It's the loader for extension/connection signatures: `NullReferenceException` in `ListType.GetUniqueId` on a module signature |
| Copying a `(System)` element from another module that already references it (`otherModule.References.Named("(System)").ServerActions.Named("X")` → `eSpace.AddDependency(...)`) | The element is a `ReferenceAction`, **not `IShareable`**, so `AddDependency` won't take it |
| Synthetic `(System)` signature: build a module with the `(System)` element's keys (from another module's `ReferenceAction`), pass its bytes as `(System)` | (1) `CreateESpace(..., key: <(System) key>)` → `The provided key is reserved and cannot be used`. (2) More fundamentally, **elements created through the Model API never reach the signature binary**, even after `Save` + `LoadESpace`: a new action on a real OML gives `added=[]` under that module's own key, while an existing one is added. The signature is only regenerated by Service Studio, so no synthetic signature is possible |
| Writing reference rows into `OSSYS_*` tables | Doesn't touch the OML. The platform rewrites those rows from the OML on publish |

## Not tested yet — check before relying on it

- A third module that is **not** a plain eSpace (e.g. an extension or a Forge module
  with its own dependencies), or a chain more than one level deep.
- A producer that is **not open** in Service Studio (OML from file or Service Center).
- Publishing the consumer and confirming the reference isn't flagged as outdated.
- Refreshing an existing reference the same way (`eSpace.RefreshDependencies(si)`).

When you test one of these, update this section.
