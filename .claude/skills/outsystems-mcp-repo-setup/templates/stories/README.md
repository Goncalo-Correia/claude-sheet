# Stories

One file per user story or technical task: `<ID>-<short-slug>.md`, for example `US-012-export-orders.md`. Start from [TEMPLATE.md](TEMPLATE.md).

When work on a story starts, Claude writes its ID to `.claude/state/current-story`. The publish hook then tags `PUBLISHES.md` rows and `changes/` files with it, and the `outsystems-mcp-publish` skill clears it when the story is closed. That file is local and not committed.

Status: `To do` → `In progress` (implementing) → `In review` (published to Development, being tested) → `Done` (review approved, repository updated).
