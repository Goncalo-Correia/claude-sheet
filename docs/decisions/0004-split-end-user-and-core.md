# 0004. Split ClaudeSheet into an End-user module and a Core module (ClaudeSheet_CS)

- **Date:** 2026-10-15
- **Status:** Accepted
- **Story:** TT4 (migration)

## Context

Until now ClaudeSheet held everything: the data model, the server logic, the Bootstrap timer and the UI. The team created a second module, **ClaudeSheet_CS**, to be the backend, following the OutSystems Architecture Canvas (End-user on top of Core). The goal is a clear boundary: the UI module only shows data and calls services, and the business rules and data live in one place that other apps can reuse later.

## Decision

- **ClaudeSheet_CS (Core)** holds the database (entities and static entities), timers, processes, server actions and structures, as the team specified. It also takes the elements those depend on:
  - the **SheetUser role**, because every CS server action starts with `CheckSheetUserRole()`;
  - the **user exceptions** (`DaySheetValidation`), because CS server actions raise them;
  - the **site properties** (`DailyExpectedMinutes`, `SeedTestUsernames`), because CS server actions read them. ClaudeSheet reads `DailyExpectedMinutes` through a public CS server action, as site properties can't be shared across modules.
- **ClaudeSheet (End-user)** keeps the screens, blocks, data actions, screen/block actions, client actions, theme, images and client variables. Two exceptions to "server actions go to CS":
  - the OutSystems **login template** actions `DoLogin` and `DoLogout` stay with the Common flow screens that use them;
  - the **client** versions of `Minutes_Format` and `Date_GetWeekStart` stay (client logic). Their server versions move.
- CS elements are **public** only when ClaudeSheet uses them. Entities are public with Read Only = Yes, and writes go through public CS server actions.
- ClaudeSheet_CS never references ClaudeSheet.
- Stories that touch both modules run **producer first** and use **one subagent per module**. The main session owns the plan check forms, merges, reviews, publishes and the change log (`CLAUDE.md` › Multi-module stories).

## Consequences

- **Migration (TT4).** The Model API can't move an element between modules, so every backend element is **recreated** in ClaudeSheet_CS, made public, and ClaudeSheet is rewired to it before the old element is deleted.
  - Recreated entities are **new tables**. ClaudeSheet only has seed data, and the Bootstrap timer recreates it in CS. Any other rows (hours logged while testing) are lost unless migrated by hand.
  - Static entity record identifiers and names must be kept, because expressions use them (`Entities.CalendarDayType.Pending`).
- **Dependencies.** ClaudeSheet must add each CS element it uses in Service Studio › Manage Dependencies. The Model API can't add a dependency (`AddDependency` fails). Refreshing existing ones can be automated with `omlRefreshReferences`, but only for what CS has **published**.
- **Two publishes per cross-module story,** in order: CS, then ClaudeSheet.
- **Repository.** `snapshots/`, `reviews/` and `changes/` are kept per module, and story files list their modules and group their plan per module (`stories/TEMPLATE.md`).
- **Open question.** The UI-only structures (`NumberOption`, `DayEditorLine`, `DayStatusKPI`) move to CS under the "all structures" rule. Revisit if that adds dependency churn.
