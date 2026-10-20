---
name: outsystems-open-module
description: Opens OutSystems 11 modules in Service Studio from the server, by name or eSpaceKey, through a servicestudio:// link (no LifeTime API needed), then confirms with the Service Studio MCP listApps tool. Use whenever a module a story needs is not open ("listApps" doesn't show it), before reading, changing, reviewing, publishing or snapshotting it, and when the user says "open ClaudeSheet_CS", "open the module", "open the ClaudeSheet app", "open all modules", or names outsystems-open-module.
---

# Open a module in Service Studio

Service Studio registers the `servicestudio:` URL protocol on Windows. This link makes it download the **last published version** of a module from the server and open it:

```
servicestudio://-downloadAndOpen%20-eSpaceKey%20<GUID>%20-url%20<host>
```

The host and the eSpaceKey of each known module are in [modules.json](modules.json). An "app" is opened by opening each of its modules (the `CLAUDE.md` Modules table lists them), **producer first** (ClaudeSheet_CS, then ClaudeSheet).

## Steps

1. **Check what is open.** Call `listApps` (reuse this session's token). Skip modules already listed with `isOpen: true`.
2. **Get the key** if the module is not in `modules.json`:
   - From a module that is open and references it, run a throw-probe: `eSpace => { throw new Exception("PROBE:" + eSpace.References.Named("<Module>").ModuleKey); }`. For an open module's own key, use `eSpace.GlobalKey.ModuleKey`.
   - The probe returns a compact base64 key. Turn it into a GUID with `scripts/open-module.ps1 -Decode <key>`.
   - Add the module and its GUID to `modules.json`.
   - The `key` that `listApps` shows is **not** the eSpaceKey (it changes every time the module is opened); never use it.
3. **Before opening, warn the user** in one line: opening a module can close or replace a Service Studio window that is already open, so unsaved work there should be saved first.
4. **Open:**
   ```powershell
   powershell -NoProfile -File .claude/skills/outsystems-open-module/scripts/open-module.ps1 -Name ClaudeSheet_CS -Name ClaudeSheet
   ```
   Several names open in the order given, 8 s apart. Use `-Key <GUID>` for a module not in `modules.json`. The script uses `Start-Process`, not `cmd.exe`, so the `%20`s in the link aren't read as variables.
5. **Confirm.** Call `listApps` until each module is listed with `isOpen: true`, one call at a time. It can be empty for a moment while Service Studio swaps windows. To check it is the right module, run the `eSpace.GlobalKey.ModuleKey` probe on it and compare the decoded GUID with `modules.json`.
6. **Stop and tell the user** if a module is still not open after about 2 minutes. Service Studio is usually waiting on them (unsaved changes, sign-in, a merge question). Their unsaved work is never yours to discard: don't call `omlReset` or reopen to get past it.

Done when `listApps` shows every requested module open.

## Notes

- This opens what is **published** on the server. Unpublished changes exist only in the Service Studio window that has them.
- Each module has its own MCP session pointer. After opening, pass the right `eSpaceName` on every call, as `CLAUDE.md` says.
- Don't write the session token or any credential to `modules.json` or this folder.
