# US9 · ClaudeSheet_CS changes

What each `applyModelApiCode` save for US9 changed in ClaudeSheet_CS, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet_CS/`.

## Save 1 · 2026-10-20 11:51:42 UTC

- **Published:** 2026-10-20 12:23 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Created the public structure `UserAccess` and two public server actions in folder `User`. `User_GetList` lists users with their three ClaudeSheet roles and active assignment count, filtered by a search text and optionally only users with access. `User_SetRoles` grants or revokes `SheetUser`, `SheetAdmin` and `SheetManager` for a user. Both are for admins only, and `User_SetRoles` refuses to remove the caller's own `SheetAdmin`.
