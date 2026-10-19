# US7 · ClaudeSheet_CS changes

What each `applyModelApiCode` save for US7 changed in ClaudeSheet_CS, oldest first. Logged when the module was published. The resulting module is in `snapshots/ClaudeSheet_CS/`; the exact code of these saves is in git history (commit 21f9b01, `changes/`).

## Save 1 · 2026-10-19 13:58:22 UTC

- **Published:** 2026-10-19 14:06:42 UTC (manual 1-Click Publish; logged by Claude from the session transcript)
- **Outcome:** saved

Created the user exceptions `ProjectValidation` and `AssignmentValidation`, the structures `ProjectSummary`, `ProjectAssignmentSummary` and `UserOption`, and the server actions `Project_GetList`, `Project_GetById`, `Project_CreateOrUpdate`, `Project_SetActive`, `ProjectAssignment_GetList`, `ProjectAssignment_CreateOrUpdate`, `ProjectAssignment_End`, `ProjectAssignment_Delete` and `User_GetActive`. Changed `ProjectAssignment.ProjectId` delete rule to Protect.
