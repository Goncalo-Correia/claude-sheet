# 0006. Publish to Development before the review; the publish skill closes the story

- **Date:** 2026-10-20
- **Status:** Accepted
- **Story:** none (workflow change)

## Context

The review (`outsystems-mcp-code-review`) ran before anything was published and published only once every row was Yes. But an OutSystems app can only be tested in the browser once it is published, so the acceptance criteria could only be checked against the model. Runtime behaviour (sizes, responsiveness, messages, flows) was tagged "verify at runtime" and never actually tested before approval.

## Decision

- `outsystems-mcp-implementation` still collects the description, the implementation plan and the acceptance criteria, implements, and shows the plan check with the merge. When every plan step is Yes and there are no validation errors, it **publishes to Development**.
- `outsystems-mcp-code-review` runs on the published app: the user tests the acceptance criteria in the browser and checks the changes in Service Studio. Each fix is merged and republished so it can be retested. The approving round ends with `Review: approved`; the review no longer publishes on approval.
- `outsystems-mcp-publish` runs last and updates the repository: it publishes only if something approved is still unpublished, completes the change log, refreshes the snapshots, marks the story `Done` and offers to commit.
- The publish hook (`log-publish.mjs`) moves from the publish skill's frontmatter to the project's `.claude/settings.json`, so every MCP publish is logged whichever skill makes it.

## Consequences

- Acceptance criteria are tested on the running app before a story is closed.
- Development can run a build that the review later rejects. That's acceptable while Development is the only environment; once there are others, promoting to them should require `Review: approved`.
- A story can have several `PUBLISHES.md` rows (the implementation and each fix round). Whoever publishes writes the `SUMMARY:` lines right away; the publish skill checks none are left.
- Story status: `In review` means published to Development and being tested; `Done` is set by the publish skill. `Published` is no longer used.
- `outsystems-mcp-repo-setup` scaffolds the same flow: its templates describe it, and its scaffold script adds the publish hook to `.claude/settings.json` (using a vendored copy of the publish skill when the repo has one, otherwise `~/.claude/skills/outsystems-mcp-publish`).
