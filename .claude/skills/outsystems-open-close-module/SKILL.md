---
name: outsystems-open-close-module
description: Opens and closes OutSystems 11 modules in Service Studio. Opens a module from the server by name or eSpaceKey through a servicestudio:// link (no LifeTime API needed), on the same server as an already-open module when needed (-SameServerAs). Closes a module's tab through UI Automation (close-module.ps1), never one with unsaved changes. Confirms both with the Service Studio MCP listApps tool. Works on any environment and project; the server host and the modules' eSpaceKeys live in the project's .claude/outsystems-modules.json, created on first use. Use whenever a module a story needs is not open ("listApps" doesn't show it), before reading, changing, reviewing, publishing or snapshotting it; to close modules you opened once you are done with them; and when the user says "open <Module>", "open the module", "open the app", "open all modules", "close the module", "close <Module>", or names outsystems-open-close-module.
---

# Open or close a module in Service Studio

Service Studio registers the `servicestudio:` URL protocol on Windows. This link makes it download the **last published version** of a module from the server and open it:

```
servicestudio://-downloadAndOpen%20-eSpaceKey%20<GUID>%20-url%20<host>
```

The project's **`.claude/outsystems-modules.json`** holds the default server host and the eSpaceKey of each module:

```json
{
  "host": "<environment>.outsystemscloud.com",
  "modules": {
    "<Module>": "<eSpaceKey GUID>",
    "<ModuleOnAnotherServer>": { "key": "<eSpaceKey GUID>", "host": "<other host>" }
  }
}
```

An entry is either `"<GUID>"` (uses the file's default `host`) or `{ "key", "host" }`. The file is project data, not part of this skill, so the skill works on any environment. It holds no credentials and can be committed so the team shares it.

**Open on the right server.** Several environments can be registered in Service Studio
(personal, client, company…). When you open a module **for another module** (e.g. a producer
to reference from a consumer), open it on the **consumer's server**:
`-SameServerAs <Consumer>`. The script reads the consumer's host from Service Studio's
`Settings.xml` (recent modules, each with its host and eSpaceKey). `-HostOf <Module>` only
prints that host. Never assume the default `host` in the file is the user's current
environment.

An "app" is opened by opening each of its modules (the `CLAUDE.md` Modules table lists them, when there is one), **producer first** (Foundation, then Core, then End-user, e.g. `<App>_CS` before `<App>`).

## Steps

1. **Check the configuration.** If `.claude/outsystems-modules.json` doesn't exist, create it:
   - `host`: the server the modules are opened from, without `https://` or a path. Take the Development environment from `CLAUDE.md`'s Environments table when there is one; otherwise ask the user which environment to open from.
   - `modules`: start empty and fill it as in step 3.
   - When the user asks for another environment than the one in the file, pass `-ServerHost <host>` to the script instead of editing `host`.
2. **Check what is open.** Call `listApps` (reuse this session's token). Skip modules already listed with `isOpen: true`.
3. **Get the key** if the module is not in `.claude/outsystems-modules.json`:
   - From a module that is open and references it, run a throw-probe: `eSpace => { throw new Exception("PROBE:" + eSpace.References.Named("<Module>").ModuleKey); }`. For an open module's own key, use `eSpace.GlobalKey.ModuleKey`.
   - The probe returns a compact base64 key. Turn it into a GUID with `scripts/open-module.ps1 -Decode <key>`.
   - Add the module and its GUID to the file, with its `host` when it isn't on the default one.
   - If no open module gives the key, ask the user for it. The eSpaceKey is the same on every environment the module is deployed to, and Service Center shows it in the module's details.
   - The `key` that `listApps` shows is **not** the eSpaceKey (it changes every time the module is opened); never use it.
4. **Before opening, warn the user** in one line: opening a module can close or replace a Service Studio window that is already open, so unsaved work there should be saved first.
5. **Open**, from the repository root:
   ```powershell
   powershell -NoProfile -File .claude/skills/outsystems-open-close-module/scripts/open-module.ps1 -Name <Producer>,<Consumer>
   ```
   For a module needed by an open one, add `-SameServerAs <OpenModule>`. Several names open in the order given, 8 s apart. Use `-Key <GUID>` for a module not in the file, `-ServerHost <host>` for another environment, and `-Config <path>` when running from another folder. The script uses `Start-Process`, not `cmd.exe`, so the `%20`s in the link aren't read as variables.
6. **Confirm.** Call `listApps` until each module is listed with `isOpen: true`, one call at a time. It can be empty for a moment while Service Studio swaps windows. To check it is the right module, run the `eSpace.GlobalKey.ModuleKey` probe on it and compare the decoded GUID with the file.
7. **Stop and tell the user** if a module is still not open after about 2 minutes. Service Studio is usually waiting on them (unsaved changes, sign-in to the environment, a merge question). Their unsaved work is never yours to discard: don't call `omlReset` or reopen to get past it.

Done when `listApps` shows every requested module open.

## Close a module

There is no close command in the MCP or in `servicestudio://`. The script uses Windows UI
Automation instead. Each open module is a tab in Service Studio's window, and the script
clicks the close button in that tab's header:

```powershell
powershell -NoProfile -File .claude/skills/outsystems-open-close-module/scripts/close-module.ps1 -Name <Module>
```

- **Only close modules you opened yourself** in this task, never one that was already open.
- **A tab whose header starts with `* `** has unsaved changes. The script refuses to close
  it and says so. Don't work around this: the unsaved work belongs to the user.
- **Confirm with `listApps`** that the module is gone. If it is still listed, Service Studio
  is probably showing a dialog: stop and tell the user.
- End any `startOmlEditSession` on that module before closing it.

Verified 2026-10-23 on Service Studio 11 (XPlatform): it closed a saved module's tab with no
dialog, and it refused a tab with unsaved changes.

## Notes

- This opens what is **published** on the server. Unpublished changes exist only in the Service Studio window that has them.
- Service Studio must already be connected (signed in) to the environment; otherwise it asks the user to sign in.
- Each module has its own MCP session pointer. After opening, pass the right `eSpaceName` on every call.
- Don't write the session token or any credential to `.claude/outsystems-modules.json` or this folder.
