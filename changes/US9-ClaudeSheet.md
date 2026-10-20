# US9 · ClaudeSheet changes

What each `applyModelApiCode` save for US9 changed in ClaudeSheet, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet/`.

## Save 1 · 2026-10-20 13:25:11 UTC

- **Published:** 2026-10-20 13:37 UTC, approximate (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Created the block `AssignmentEditor` in the Admin flow: US7's assignment list and popup, now working for a project (`ProjectId`) or for a user (`UserId`). `ProjectDetail` now uses this block instead of its own assignment section, popup, actions and variables. Created the admin screens `Users` (search, role tags, active assignments) and `UserDetail` (role switches saved through `User_SetRoles`, and the user's assignments through the block). Changed the `InvalidPermissions` text, added the `UsersLink` menu link for admins, and added theme CSS for the 4-column table, the role tags and the switch rows.
