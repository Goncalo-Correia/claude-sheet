---
name: outsystems-mcp-implementation
description: Implements an OutSystems 11 user story through the Service Studio MCP from a complete definition. Triggered by "implement US X" (or "implement US-012", "implement story X"), it first asks, one at a time, for the story's description, its implementation plan and its acceptance criteria, writes them to the story file, then implements the plan with applyModelApiCode. Before merging it validates every implementation plan step against the mutated module, tags each Approved or Rejected with the reason, shows the list as a Yes/No form in the conversation and then calls omlMerge right away, so the user reviews the plan check while reviewing the merge in Service Studio; the user's answer is final, and it fixes only what the user answers No to. It never publishes; acceptance criteria are reviewed afterwards with outsystems-mcp-code-review. Use whenever the user says "implement US X", "implement story X", "build US X", "start US X", or names outsystems-mcp-implementation.
---

# OutSystems MCP implementation

The purpose is to turn a user story into a working change in the open OutSystems module, starting from a **complete definition** (description, implementation plan, acceptance criteria), and to let the developer confirm, step by step, that the implementation follows the plan.

This skill covers definition → implementation → plan check. It does **not** review the acceptance criteria and never publishes: that's the job of `outsystems-mcp-code-review`, which the user calls afterwards.

```
"implement US X"
  -> 1. ask for the description          (wait for the answer)
  -> 2. ask for the implementation plan  (wait for the answer)
  -> 3. ask for the acceptance criteria  (wait for the answer)
  -> 4. write the story file, set the current story
  -> 5. implement the plan (applyModelApiCode), no merge yet
  -> 6. validate each plan step against the mutated module: Approved / Rejected + reason
  -> 7. show the plan check form AND call omlMerge right after it, same turn
        (the user answers the form while reviewing the Compare-and-Merge window)
  -> 8. read the merge result, then the answers (the user's Yes / No is final)
        all Yes -> 9. close out, point to outsystems-mcp-code-review
        any No  -> fix only those -> back to 6 (full list, fixed rows in yellow, form + merge together)
```

## The form and the merge run together

The plan check form is shown **while the merge is happening**, never after it. The user validates the plan steps in the conversation at the same time as they review the diff in Service Studio's Compare-and-Merge window, so each check is made against the change they are about to accept.

That's possible because, until `omlMerge` resolves, the read tools follow the session pointer and see the last successful mutation (see `servicestudio-mcp-oml`). So the order is always: validate against the mutated module → render the form → call `omlMerge` immediately, without waiting for the answers. `omlMerge` blocks until the user finishes the merge; the form stays answerable in the meantime.

## Forms live in the conversation

Every form this skill shows is rendered **inline in the conversation** with the widget tool (`mcp__visualize__show_widget`), or as a markdown table in the chat when the widget tool isn't available. Never write the form to an `.html` file, never publish it as an Artifact and never open it in the browser pane.

## 1–3. Collect the story definition

Read the story ID from the user's message (`implement US 3` → `US3`). Match the ID format of the existing files in `stories/` (for example `US1-…` or `US-001-…`); if there are none, use the ID as the user wrote it, without spaces.

Ask for the three parts **one at a time**, in this order, each in its own message, and wait for the user's reply before asking the next:

1. **Description**: "What is the description of US X?" (the story itself: who, what, why, plus any rules, UI notes or contract the user wants recorded).
2. **Implementation plan**: "What is the implementation plan for US X?" (the elements to create or change, in order).
3. **Acceptance criteria**: "What are the acceptance criteria for US X?"

Rules:

- Ask in plain chat text. Don't use AskUserQuestion: these are free-text answers.
- Don't start reading the module, planning or implementing before all three answers are in.
- Take each answer as given. Don't rewrite, reorder or "improve" it. Only number the items: plan steps become `P01`, `P02`, … and criteria become `A01`, `A02`, … in the order given (keep the user's own numbering, such as `AC1.2`, next to the ID).
- An empty or "skip" answer for the plan or the criteria isn't accepted: say both are needed for the story to be complete, and ask again.
- If an answer is ambiguous (a plan step that names no element, a criterion that can't be checked), ask about that specific point before moving on.
- **The story file already exists**: for each part that already has real content in it, show it and ask whether to keep it or replace it, instead of asking from scratch. Placeholder text from `stories/TEMPLATE.md` counts as empty.

## 4. Write the story file

In a repository with a `stories/` folder:

1. Create or update `stories/<ID>-<slug>.md` from `stories/TEMPLATE.md`. The slug is a few words from the description (`US4-weekly-summary.md`).
   - Title: `# <ID>: <short title from the description>`
   - Status `In progress`, Modules = the open module, Requested by = the user, Created = today.
   - `## Story` = the description, exactly as given.
   - `## Acceptance criteria` = the criteria as a checklist (`- [ ] A01 · …`).
   - `## Implementation plan` = the plan as a numbered list.
2. Write the ID to `.claude/state/current-story`.

Outside such a repository, keep the definition in the conversation and show it back to the user once, in full, before implementing.

Then check the plan against `docs/conventions.md` (when it exists). If a step contradicts the conventions, point it out and ask before implementing it. This includes **names**: if the plan names an action, structure or other element in a way the conventions' naming rules don't allow (for example a block's On After Fetch handler not called `OnAfterFetch_<DataSource>`, or a reusable server action not called `<Entity>_<Verb>`), propose the conventional name and ask which one to use. Elements the plan doesn't name are named by the conventions. This also includes **module placement**: when the conventions or `docs/architecture.md` say which module an element belongs in (for example backend elements in a `_CS` module), check every plan step's module, point out a step that puts an element in the wrong module and ask before implementing it. Ask the user to open every module the plan touches in Service Studio (`listApps` must show each one `isOpen`).

## 5. Implement

Follow the `servicestudio-mcp-oml` skill for the Model API contract (load it if it isn't loaded yet). In addition:

- Service Studio MCP tools are named `mcp__servicestudio__*`. If their schemas are not loaded, load them with ToolSearch. Reuse the session token from `createSessionToken` if this conversation has one; create it only once.
- Make **read calls one at a time**: parallel reads fail with "OML not found".
- Before changing an element, read `snapshots/<Module>/` (when it exists) and confirm the element's current state with the live read tools; snapshots can be out of date.
- Implement the plan **in its order**. Group steps into one `applyModelApiCode` call only when they touch the same element.
- After each save, check `getValidationMessages` (errors) and fix errors before moving on.
- **Conventions gate before the merge** (when `docs/conventions.md` exists):
  - every element created or renamed in this story follows its naming rules (read the names back: Service Studio may have suffixed them);
  - **every flow touched in this story follows the Flow layout rules** (vertical main path, branches leaving horizontally, one End per branch, no crossings, no overlaps, diagonals only for For Each loop-backs and justified merges). Lay the flow out as you build it, or run the repository's layout lambda (`checks/layout-flow.cs`, with the touched flows in `targets`; it saves) as the last `applyModelApiCode`. Then run the report (`checks/flow-layout.cs`, a throw-probe that saves nothing; set `onlyFlows`, remove the `//` header lines). A touched flow passes with 0 other diagonals, 0 crossings and 0 overlaps. Fix leftovers with another `applyModelApiCode` and run the report again. Record every allowed merge diagonal, and any crossing that truly can't be avoided, in the implementation notes with the reason. Layout problems in flows this story didn't touch are reported in the implementation notes, not fixed.
  - adding End nodes so each branch ends on its own is a layout change, not a logic change.
- If a step can't be done as written (the platform doesn't allow it, an element it relies on doesn't exist), choose the closest correct implementation, and record the deviation and its reason. Don't silently skip a step. If no reasonable alternative exists, stop and ask.
- When the last mutation is clean, keep its `mutatedOmlPath`, but **don't merge yet**: the merge is called in step 7, right after the form is shown.
- **Never call `omlPublish`.**

Write what was actually done to the story's `## Implementation notes`: the number of saves, deviations from the plan and why, assumptions, and anything the reviewer should know.

## 5b. Stories that touch more than one module

When the plan has steps in more than one module (the story's `## Implementation plan` is grouped per module, or its steps name different modules):

1. **Order the modules producer first** (Foundation, then Core, then End-user; e.g. `ClaudeSheet_CS` before `ClaudeSheet`). A consumer can only use what its producer has **published**.
2. **One module at a time, in this session.** Don't start subagents. For each module, in order: step 5 for that module's plan steps only (pass its `eSpaceName` on every call, run the conventions gate for it), then steps 6 and 7 (plan check form for that module's steps + `omlMerge` of its `mutatedOmlPath`) and step 8. Finish one module's chain before starting the next.
3. **Hand-off to the consumer module.** The producer is reviewed (`outsystems-mcp-code-review`) and published (`outsystems-mcp-publish`) first. The Model API can't add a dependency, so the user ticks them, guided by a checklist:
   1. **Build the checklist** from the consumer's plan steps: every producer element they use, grouped by type (Entities, Static Entities, Structures, Server Actions, Service Actions, Roles, Exceptions, Blocks). Write it into the story's plan under the consumer (`Dependencies to add`).
   2. **Ask** with AskUserQuestion, as the only call in the message: *"In **<Consumer>** → Manage Dependencies → **<Producer>**, tick: Server Actions: …; Structures: …; …"*, with the options **Done** and **Not yet** (Done first). Keep the list in the question text so the user can work from it.
   3. **On Done**, run `omlRefreshReferences` on the consumer (before its first `applyModelApiCode`: it drops the consumer's session pointer). Then verify with a throw-probe on the consumer: for each listed element, look it up in `eSpace.References.Named("<Producer>")` (`ServerActions`, `Structures`, `Entities`, `Roles`, …) and report the ones not found. If any are missing, ask again with only the missing ones. If all are found, start the consumer's chain (step 2).
   4. **On Not yet** (or any other answer), stop and wait. Never start the consumer before the check passes.
   5. The first time you do this in a repository, also confirm that the reference lists **only** the ticked elements; if it lists every public element of the producer, say so: the probe then only proves the reference was refreshed, and you rely on the user's Done for the rest. (Observed 2026-10-15: the reference lists the ticked elements plus the elements they depend on, e.g. entities behind a structure's identifier attributes. A ticked element whose name clashes with an element still in the consumer appears with a numeric suffix, e.g. `DaySheet_Save2`; look elements up by base name with an optional numeric suffix, and tell the user about the clash.)
4. Each module has its own session pointer: never interleave `applyModelApiCode` calls on two modules in one chain.

Write the implementation notes per module (`### <Module>` subsections).

## 6. Validate the plan against the mutated module

Before merging, for every plan step, check the **mutated module** with the read tools (they follow the session pointer, so they read what is about to be merged; not the lambdas, not what you intended) and tag it:

- **Approved**: the module does what the step says.
- **Rejected**: it doesn't, or only partly does. Always give the reason: what is missing or different, with element names, so the reviewer can check it in Service Studio in seconds.

Tagging rules:

- **Documented deviation** (step 5): tag it **Rejected**, give the reason and point to the implementation notes. The user decides whether it's acceptable.
- **Can't be verified from the model** (runtime behaviour, visual sizes): if the model has what's needed to support it, tag it **Approved** with the note "verify at runtime"; if it clearly lacks it, **Rejected**.
- A step with several sub-points is Rejected if any of them fails; name which.
- Give **Approved** rows a one-line evidence: where in the module it was found (for example `Logic › Server Actions › GetMonthSummary`, local ExpectedDays).
- A step whose elements break the conventions gate (step 5) is **Rejected** with the name or flow at fault, unless the user approved that name explicitly in step 4 (then say so in the evidence).

Before showing the form, check yourself: every plan step has exactly one row, and every Rejected tag has a reason.

## 7. Show the plan check form, then merge

Send a short text intro: module, story, round number, number of steps, how many are Approved and Rejected, the number of fixed rows, and that the Compare-and-Merge window is opening now so they can check both side by side.

Render the form **in the conversation** with `mcp__visualize__show_widget` (call `mcp__visualize__read_me` with modules `["elicitation"]` once beforehand if it hasn't been read in this conversation). Use the markup in `references/plan-check-form.md`. One row per plan step: the ID, the step text, Claude's tag badge, the reason or evidence, and Yes/No pills with a reason box on No. One Submit button.

- Header title: `"<Module> · <story> · implementation plan check · round N"`.
- The tag never pre-selects an answer: the user answers every row.
- **The full list every round.** Rows fixed in this round are highlighted in yellow with "Fixed in round N", the user's original reason and what was done.
- Below the form, add a compact markdown table (ID, tag, step, reason) so the list stays readable in the transcript.

If the widget tool isn't available, show that markdown table as the form and ask the user to reply with lines like `P03 No: <reason>` and "all others Yes". Don't use AskUserQuestion for this.

**Then, in the same turn, call `omlMerge`** with the kept `mutatedOmlPath`, with nothing in between: no question, no waiting for the answers. The call blocks while the user reviews the merge; the form stays open in the conversation during that time.

- The form must be on screen **before** `omlMerge` is called. Never merge first and show the form afterwards, and never hold the merge until the form is answered.
- When the merge is accepted with no dialog (the user's Service Studio setting), the call returns at once; that's fine, the form is still answered as usual.

## 8. Read the merge result and the answers

First, the merge result:

- **`merged: true`**: re-read the elements of every plan step with the read tools (the pointer is now cleared, so they read the live module). If anything differs from what was validated (the user left a change out in the Compare-and-Merge window), say which steps are affected, re-tag them and show the form again (same round) before acting on any answer.
- **`merged: false`**: nothing reached the module, so the form's answers don't count. Tell the user, follow the `merged: false` procedure in `servicestudio-mcp-oml`, and once the change is re-applied go back to step 6 (same round). Keep the `mutatedOmlPath`.

Then the answers. The payload arrives as one line, for example `… — P01: Yes · P02: No · P02 (other): use a Message node · P03: Yes …`.

- **The user's answer is final.** Claude's tag is advice:
  - **Rejected + Yes**: accepted as it is. Don't fix it and don't ask again. Record it as "accepted despite Rejected tag".
  - **Approved + No**: the user found a problem Claude missed. Fix it.
  - **Rejected + No**: fix it.
- **Never fix anything the user didn't answer No to**, even rows tagged Rejected.
- **Every row must have an answer.** If any are missing, list them and show the form again (same round). Silence isn't Yes.
- **A No without a reason**: ask for the reason before fixing.
- **Skipped form**: nothing is approved; ask what they want to do.
- Save the round to the log (below) before acting.

### Fixing

1. Fix exactly what each No reason describes with `applyModelApiCode`, scoped to that step. If the fix has to touch elements of an approved step, that step also becomes `Fixed in round N` with a note explaining why.
2. If the reason means the **plan text** should change rather than the module, ask before editing the story's plan, then re-tag the step against the new text.
3. Update the implementation notes. **Don't merge yet.**
4. Back to step 6 with round N+1: re-check and re-tag **every** step against the mutated module, show the full list with the fixed rows in yellow, and call `omlMerge` right after the form (step 7).

A fix round starts a new chain off the live module (the previous merge cleared the pointer), so the round's merge carries only the fixes.

If the same step is answered No 3 rounds in a row, stop and talk it through in chat instead of trying a fourth fix.

## 9. Close out

When every step has a Yes:

1. Record the final round in the log, with any steps accepted despite a Rejected tag.
2. Update the story: Status `In review`, implementation notes complete.
3. Tell the user the story is fully defined (description, plan, acceptance criteria) and implemented, the plan check is approved, and the next step is to review the acceptance criteria with `outsystems-mcp-code-review` (the changes are merged in Service Studio, not published).

Don't start the review or publish on your own.

## Log

Append the plan check to the story's review log, so the review skill finds it:

- In a repository with a `reviews/` folder at its root: `reviews/<eSpaceName>/<story-id>.md`.
- Otherwise: `~/.claude/outsystems-reviews/<eSpaceName>/<story-id>.md`.

One section per round, headed `## Implementation plan check — round N — <date time>`: a table (ID, step, tag, reason, answer), the No reasons, and the outcome (fixed steps, accepted despite Rejected, all approved or not). End the last round with the line `Plan check: approved`.

## Things to keep in mind

- The user reads the module in Service Studio. Use OutSystems vocabulary (aggregate, data action, OnParametersChanged, delete rule) and exact element names.
- Never present intended changes as done. If a lambda failed or a merge didn't apply, the step is Rejected and says so.
- Never write session tokens or credentials to any file.
