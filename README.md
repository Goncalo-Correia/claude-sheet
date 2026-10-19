# ClaudeSheet

TODO: describe the project

This is an OutSystems 11 project built with Claude Code through the Service Studio MCP. OutSystems keeps the modules in the platform as binary `.oml` files. This repository is the text record around them: what was asked for, what changed, what the model looked like after each publish, and why. Everything in it is Markdown.

The repository map, the modules and the workflow are in [CLAUDE.md](CLAUDE.md).

## Prerequisites

- Service Studio 11 with the MCP server enabled. The repo expects it at `http://127.0.0.1:41820/mcp`.
- Claude Code.
- Node.js 18 or later, for the scripts of the `outsystems-mcp-publish` skill.
- The user-level skills `servicestudio-mcp-oml`, `outsystems-mcp-implementation`, `outsystems-mcp-code-review` and `outsystems-mcp-publish` in `~/.claude/skills/`.
- The modules (`ClaudeSheet_CS`, `ClaudeSheet`) open in Service Studio when you work on them.

## Getting started

1. Clone the repository and open Claude Code in its folder.
2. Approve the `servicestudio` MCP server when Claude Code asks; it's defined in `.mcp.json`.
3. Open the modules in Service Studio.
4. Describe the story you want to build. Claude follows the workflow in [CLAUDE.md](CLAUDE.md).

## What changed?

- **In a story:** the story file in [stories/](stories/), then `changes/<story>-<Module>.md`.
- **Between two publishes:** `git log -p snapshots/<Module>/`.
- **Who approved it:** `reviews/<Module>/<story>.md`.
- **When it was published:** [PUBLISHES.md](PUBLISHES.md).

Set up on 2026-10-14 with the `outsystems-mcp-repo-setup` skill.
