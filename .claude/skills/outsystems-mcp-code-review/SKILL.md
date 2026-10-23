---
name: outsystems-mcp-code-review
description: Review gate for a user story implemented through the Service Studio MCP and already published to Development by outsystems-mcp-implementation, so the user can test it in the browser and check it in Service Studio. It requires the user story's implementation plan and an explicit acceptance criteria section, and asks for whichever is missing. It validates every acceptance criterion against the live module, tagging each Approved or Rejected (with the reason), and lists every changed element (entities, attributes, structures, actions, blocks, screens, roles, CSS, settings). When the repository has docs/conventions.md, it also checks the changed elements against it (naming rules, flow layout: vertical first, no crossings, no overlaps) and lists each violation. The implementation plan steps are added to the form only when the plan check wasn't already approved by outsystems-mcp-implementation. Everything goes into one Yes/No form shown in the conversation, with a reason box for each "No", "Yes to all" / "No to all" buttons and one Submit button; the user's answer is final. On any No it fixes only what the user rejected, merges, republishes to Development so the user can test again, and shows the full form again with the fixed rows highlighted in yellow. When everything is Yes and there are no validation errors it records the approval and hands over to outsystems-mcp-publish, which updates the repository. Use this skill whenever an OutSystems user story, technical task or set of Model API changes has been implemented and published through Service Studio MCP. Also use it when the user says "review the changes", "review US X", "code review", "show me what changed", "list the changes", "check the acceptance criteria", "review the acceptance criteria", "test the app", "validate the story", "can I close the story", or names outsystems-mcp-code-review, even if they don't explicitly ask for a form.
---

# OutSystems MCP code review

The purpose is to let a human developer verify that what Claude built for a user story meets its acceptance criteria and changes only what it should. In OutSystems an app can only be tested in the browser once it is published, so the module is **already published to Development** when this skill runs (`outsystems-mcp-implementation` publishes it after the plan check). The user tests the acceptance criteria **in the browser** and checks the changes **in Service Studio**, and keeps the final say on every item. The loop continues until they approve everything; each fix is republished so it can be tested again.

This skill usually runs after `outsystems-mcp-implementation`, which defines the story (description, implementation plan, acceptance criteria), implements it, has the user approve each plan step and publishes. This skill then reviews the **acceptance criteria** and the **changes**. When the plan check wasn't done there, this skill checks the plan too. When everything is approved, `outsystems-mcp-publish` updates the repository (snapshots, change log, story Done).

The loop:

```
implementation published to Development
  -> 0. require the implementation plan and the acceptance criteria (ask for what's missing, then stop)
  -> 1. make sure the approved state is merged and published (publish if it isn't)
  -> 2. build the lists:
          acceptance criteria (Claude tags each Approved / Rejected + reason from the model)
          change ledger       (one row per element)
          plan steps          (only if the plan check isn't already approved; tagged like the criteria)
  -> 3. show the review form in the conversation, with the app URL
        (the user tests each criterion in the browser and checks the changes in Service Studio)
  -> 4. read the answers (the user's Yes / No is final, whatever Claude's tag says)
        all Yes + 0 validation errors -> 5. record the approval, hand over to outsystems-mcp-publish
        any No, or validation errors  -> 6. fix only those -> merge -> publish -> back to 2 (full form, fixed rows in yellow)
```

Service Studio MCP tools are named `mcp__servicestudio__*`. If their schemas are not loaded, load them with ToolSearch first. Every call needs the session token from `createSessionToken`. Reuse the token already obtained in this conversation, and only create one if none exists. Make **read calls one at a time**: parallel reads have been seen to fail with "OML not found" temp-file errors.

## Forms live in the conversation

The review form is rendered **inline in the conversation** with the widget tool (`mcp__visualize__show_widget`), or as markdown tables in the chat when the widget tool isn't available. Never write the form to an `.html` file, never publish it as an Artifact and never open it in the browser pane.

## 0. Require the implementation plan and the acceptance criteria

The review checks the work against the story's acceptance criteria (and, when needed, its plan), so both have to exist before anything else happens.

### Find the story

1. If the repository has `.claude/state/current-story`, it holds the story ID. Open `stories/<ID>-*.md`.
2. Otherwise use the story the user named ("review US 3") or the one the conversation has been working on.
3. If there is no story file, use what the user gave in the conversation.

### Implementation plan

The plan is **provided** when either:

- the story file has an `## Implementation plan` section with real steps, or
- the user gave the plan in the conversation.

An empty section, or one that still holds the template's placeholder text (for example "Elements to create or change…"), counts as missing.

### Acceptance criteria

The criteria are **provided** only when there is an explicit acceptance criteria list:

- an `## Acceptance criteria` section in the story file with real items (not the template's `- [ ] …` placeholders), or
- when there is no story file, a list the user explicitly gave as acceptance criteria in the conversation.

Don't treat other sections as acceptance criteria. "Functional rules", "Story", "UI", "Block contract" or the implementation plan are not a substitute, even when they read like criteria. If they exist, you may mention that they could be turned into criteria, but the user has to write or confirm the list.

### If either is missing

Stop and ask for it **before** merging, building lists or showing a form. Say exactly what is missing, for example:

> US1 has no `## Acceptance criteria` section. Please add one to `stories/US1-order-total-month.md` (or paste the list here and I'll add it), then I'll run the review.

Ask for both in one message when both are missing. Don't invent a plan or criteria to fill the gap, and don't run a partial review without them. If the user pastes the list in chat and a story file exists, offer to write it into the story's section.

## 1. Make sure the app is published

The user tests the app in the browser, so everything to review must be merged **and published** to Development.

- **Unmerged chain**: if the implementation's last `applyModelApiCode` result has a `mutatedOmlPath` that hasn't been merged yet, call `omlMerge` with it. If `merged` is false, stop and tell the user what they need to do in Service Studio. Don't build the lists for changes that aren't in the module.
- **Merged but not published** (an `omlMerge` in this session after the module's last successful `omlPublish`, no publish recorded for the story in `PUBLISHES.md`, or the user says so): check `getValidationMessages` (errors become rows, see step 5), then publish as in step 6.5.
- Otherwise the module is already published: don't publish again.

## 2. Build the lists

All lists are checked against the **live module** using the read tools (`getDataModel`, `getStructures`, `getActionNames`, `getServerAction`, `getClientAction`, `getScreen`, `getWebBlock`, `getRoleNames`, `getSettings`, `getTheme`, `getTimers`, `runQuery`, …). Don't tag or list something based only on what you intended to do.

### Is the plan already approved?

Look in the story's review log (see "Review log") for an `## Implementation plan check` section ending with `Plan check: approved`, written by `outsystems-mcp-implementation`.

- **Found**: the plan isn't part of this form. Mention in the intro that the plan check was approved on <date>, with any steps accepted despite a Rejected tag.
- **Not found** (the story wasn't implemented through that skill, or its plan check never finished): add the plan list below to the form.

### Tags: Approved / Rejected

Acceptance criteria (and plan steps, when included) each get a tag from Claude:

- **Approved**: the live module does what the item says.
- **Rejected**: it doesn't, or only partly does. Always give the reason: what is missing or different, with the element name, so the reviewer can check it in seconds.

Tagging rules:

- **Documented deviation**: if the work deliberately differs from the item and the story's implementation notes explain why, tag it **Rejected** and point to the note (for example "Uses a Message node instead of FeedbackMessageShow, see implementation notes"). The user decides whether the deviation is acceptable.
- **Runtime behaviour**: visual sizes, responsiveness, messages and similar can't be verified with the read tools; the user tests them in the browser. If the model has what's needed to support it (for example, the CSS rule exists), tag it **Approved** and add the note "test in the browser" with what to look at (screen, user, steps). If the model clearly lacks it, tag it **Rejected**.
- The tag is Claude's opinion. It never replaces the user's answer, and Claude never fixes a Rejected item on its own (see step 4).

### Acceptance criteria (`A01`, `A02`, …)

One row per criterion, in the story's order. Reuse the IDs `outsystems-mcp-implementation` gave them (`A01 · …` in the story) when present. If the story numbers its criteria (for example `AC1.2`), show that number next to the ID.

Each row: ID, the criterion text, the tag, the reason (Rejected) or the evidence (Approved: which element and what in it satisfies the criterion), any "test in the browser" note (screen, user, steps), and the round status.

### Change ledger (`C01`, `C02`, …)

#### Where the changes come from

Use, in order of reliability:

1. The `applyModelApiCode` lambdas executed for this implementation in the conversation. They are the exact record of what was created, modified or deleted.
2. The read tools, to **confirm each element really exists in the module** in its final form. If a change is missing from the module, say so in the row and mark it as a problem in the summary.
3. If earlier rounds exist, the previous ledger in the review log.

If the conversation was summarised or the implementation happened in another session and the lambdas are gone, rebuild the ledger from the read tools (and `changes/` or `snapshots/` in the repository, when present), comparing against the story's implementation plan and implementation notes. Tell the user the list was reconstructed.

#### What counts as one row

One row per **model element** that was created, modified or deleted:

| Element kind | Path format (as it appears in Service Studio) |
|---|---|
| Entity / static entity | `Data › Database › Order` |
| Structure | `Data › Structures › OrderSummary` |
| Site property / client variable / session variable | `Data › Site Properties › MaxOrderAmount` |
| Server / client / service action | `Logic › Server Actions › SaveOrder` |
| Role | `Logic › Roles › OrderManager` |
| Timer | `Processes › Timers › Bootstrap` |
| Screen | `Interface › UI Flows › MainFlow › OrderList` |
| Block | `Interface › UI Flows › OrderBlocks › OrderCard` |
| Theme / CSS | `Interface › Themes › Sales` (one row per stylesheet touched) |
| Image / resource / script | `Interface › Images › logo` |
| Module properties | `Module properties` |

Sub-elements (attributes, indexes, records, input/output parameters, local variables, widgets, data actions, event handlers) go **inside their parent's row** as the detail list. A sub-element gets its own row only when it is the only thing that changed in an element that already existed (for example, one attribute added to an existing entity). This keeps the list per element without hiding anything.

Order the rows by section: Data → Logic → Processes → Interface → Theme → Other, and within a section in the order they were created.

#### What each row contains

- **Change type**: Created / Modified / Deleted.
- **Path**: where to click in Service Studio.
- **What changed**: concise, concrete facts the reviewer can check against the module, for example:
  - entity: every attribute with type, length, mandatory, default, delete rule; indexes (unique?)
  - action: inputs/outputs with types, the main flow steps, aggregates and their filters, the role check, exceptions raised
  - screen/block: inputs, events, local vars, data actions, main widgets and their bindings, event handlers
  - CSS: the classes/variables added
- **Covers**: the plan steps and acceptance criteria it serves (for example `P03, A02`).

Change rows have no Approved/Rejected tag: they describe what was done, and the criteria judge it.

### Conventions (`N01`, `N02`, …), when `docs/conventions.md` exists

Check every element in the change ledger against the repository's conventions, on the live module:

- **Names**: each created or renamed element (entities, structures, actions, screen/block actions, screens, blocks, roles, site properties, timers…) follows the naming rules. Read names back from the module, not from the lambdas: Service Studio may have suffixed them.
- **Flow layout**: every flow touched by the story follows the Flow layout rules. Run the repository's report (`checks/flow-layout.cs`, an `applyModelApiCode` throw-probe that saves nothing; strip its `//` header lines, set `onlyFlows` to the ledger's flows). One Rejected row per flow with other diagonals, crossings or overlaps, with the counts and the fix (usually: run `checks/layout-flow.cs` on it). Merge diagonals and crossings justified in the implementation notes are listed as Rejected with "justified in implementation notes", so the user decides.
- Any other rule in `docs/conventions.md` that the changed elements fall under (delete rules, role checks, CSS placement…).

One row per **violation**, tagged **Rejected**, with the element path, the rule and the conventional alternative (for example "`GetKPIsOnAfterFetch` should be `OnAfterFetch_GetKPIs` (Action names › On After Fetch)"). When there are none, one **Approved** row `N01 · No convention violations` listing what was checked. Violations in elements the story didn't touch are not rows: mention them in the intro as a possible follow-up.

A name the user explicitly approved during `outsystems-mcp-implementation` (recorded in the story's implementation notes) is still listed, tagged Rejected with "approved during implementation"; the user can answer Yes.

### Implementation plan (`P01`, `P02`, …), only when not already approved

One row per plan step, in the plan's order, tagged like the criteria. If a step has sub-bullets that are separate checks, keep them in the step's row as its detail and tag the step Rejected if any of them fails, naming which.

Each row: ID, the step text (shortened if long, keeping the element names), the tag, the reason (Rejected) or a one-line confirmation of where it was found (Approved), and the round status.

### Common to all rows

- **IDs are stable across rounds.** A row keeps its ID in later rounds. New rows created by a fix get the next free number in their list.
- **Round status**: `New`, `Unchanged`, or `Fixed in round N`. For fixed rows, also give the user's original "No" reason and what was done about it. If a tag changed since the last round (for example Rejected → Approved because of a fix), say so in the row.

### Stories that touch more than one module

- The review runs once **every** module of the story is published (`outsystems-mcp-implementation` publishes each one, producer first).
- **One form per module, producer first** (e.g. `Sales_CS`, then `Sales`), header `"<Module> · <story> · round N review"`, each with its own change ledger, conventions rows and log (`reviews/<Module>/<story-id>.md`).
- **Acceptance criteria go in the end-user module's form** (the app the user tests in the browser), checked end to end. The producer's form holds its changes, conventions and, when needed, plan steps.
- **Module placement** is a conventions check (when `docs/conventions.md` or `docs/architecture.md` defines it): one Rejected row per element created in the wrong module.
- A fix in the producer is published before the consumer is retested; run `omlRefreshReferences` on the consumer before its next `applyModelApiCode` chain.

Before showing the form, check yourself:

- every acceptance criterion (and every plan step, when included) has exactly one row;
- every element in the change ledger was checked against the conventions (when `docs/conventions.md` exists), and every violation has a row;
- every executed lambda maps to at least one change row, and every change row maps to something that exists in the module;
- every Rejected tag has a reason.

Missing rows defeat the purpose of the review.

## 3. Show the review form

Send a short text intro first: the app URL to test (the Development environment in `CLAUDE.md` plus the module's path, for example `https://<env>/<App>/`), when it was last published, module, story, round number, whether the plan check was already approved, number of rows per list, how many rows Claude tagged Rejected, and the number of fixed rows. Below the form, give a compact markdown summary table per list (ID, tag or type, text or path) so the lists stay readable in the transcript.

Render the form **in the conversation** with `mcp__visualize__show_widget` (call `mcp__visualize__read_me` with modules `["elicitation"]` once beforehand if it hasn't been read in this conversation). Use the markup in `references/review-form.md`. It relies on the elicitation shell: Yes/No pills per row, "No" reveals a reason box, and a footer with "Yes to all" and "No to all" next to the Submit button. The bulk buttons need the short `<script>` at the end of the markup; copy it as is.

- **One form, in this order**: Acceptance criteria, Changes, Conventions (when `docs/conventions.md` exists), then Implementation plan (only when included). Each section has a small heading with its row count.
- **Criteria, conventions and plan rows show Claude's tag** as a badge (Approved green, Rejected red) with the reason or evidence under it. The user still answers Yes or No on every row; the tag doesn't pre-select an answer.
- **The full form every time.** Every round shows all rows of every list in the form, including those approved before, because a fix can have side effects on rows that were already approved.
- **Rows fixed in this round are highlighted in yellow** (the markup's fixed-row style) and carry the "Fixed in round N" note with the original reason.
- Set the header title to `"<Module> · <story> · round N review"` so rounds are distinguishable.
- Keep the per-row text tight (a few lines). Put long details (for example a full attribute list) as a compact comma-separated line, not paragraphs.

If the widget tool isn't available, fall back to one markdown table per list with the same rows (criteria and plan tables include the tag and reason). Ask the user to reply with lines like `A03 No: <reason>` and "all others Yes". Don't use AskUserQuestion for this: it only fits 4 questions and has no single submit.

## 4. Read the answers

The submitted payload arrives as one line, for example `… — A01: Yes · A02: No · A02 (other): total must exclude Draft days · C03: Yes …`.

- **The user's answer is final.** Claude's tag is advice:
  - **Rejected + Yes**: the user accepts the item as it is. Don't fix it, and don't ask again. Record it in the log as "accepted despite Rejected tag".
  - **Approved + No**: the user found a problem Claude missed. Fix it like any other No.
  - **Rejected + No**: fix it, using the user's reason (and Claude's, where it helps).
- **Never fix anything the user didn't answer No to**, even rows Claude tagged Rejected.
- **Every row must have an answer.** If any row is unanswered, list the missing IDs and show the form again (same round, same highlighting). Don't treat silence as Yes.
- **A "No" without a reason**: ask for the reason in chat before fixing. Guessing what the reviewer disliked usually produces a second wrong fix.
- **Skipped form**: nothing is approved; ask what they want to do.
- Save the round to the review log (below) before acting.

## 5. All Yes: record the approval

1. Call `getValidationMessages` with filter `["error"]`.
2. **Errors > 0**: not approved. Turn each error into a new change row (type `Validation error`, path = `ownerPath`, detail = message + detail). Fix them as in step 6, then show the full form again.
3. **0 errors**: the story is approved.
   - End the round in the review log with the line `Review: approved`.
   - Report the number of rows approved per list, the rounds taken, and any rows accepted despite a Rejected tag. Mention any validation **warnings** for information only; warnings don't block.
   - Tell the user the next step is `outsystems-mcp-publish`, which updates the repository (change log summaries, snapshots, story Done). Don't run it on your own.

Only approve in response to a submitted form where every row is Yes. A "looks fine" in chat about a partial list is not approval of the full form.

## 6. Any No: fix, merge, regenerate

1. For each "No" row, fix exactly what the reason describes with `applyModelApiCode`. Keep the fix scoped: don't refactor rows that were approved unless the fix requires it. If it does, those rows also become `Fixed in round N` (yellow) with a note explaining why they changed.
2. If a reason conflicts with the story, its plan or its acceptance criteria, or would break another approved element, ask before changing it. If the user's answer means the **plan or criteria text** should change rather than the module, ask before editing the story file, then re-tag the item against the new text.
3. If the plan check was approved earlier and a fix changes what an approved plan step describes, say so in the fixed row (for example "changes plan step P03: …") so the user sees the plan is affected.
4. `omlMerge` the new `mutatedOmlPath`, then confirm the fix with the read tools.
5. **Publish the fix** so the user can test it: `getValidationMessages` (errors become rows, fix them in the same round), then `mcp__servicestudio__omlPublish` with `{ "eSpaceName": "<Module>" }`. It asks for confirmation. The project's publish hook logs it in `PUBLISHES.md` and `changes/`; replace each new `SUMMARY:` line with a plain-words summary. `blocked`: ask the user to grant the permission or 1-Click Publish in Service Studio (then add the record yourself, see `outsystems-mcp-publish` › Manual publish). Failure: report the messages verbatim and turn them into rows.
6. Go back to step 2 with round N+1: re-check and re-tag **every** criterion (and plan step, when included) against the live module, rebuild the ledger, and show the full form. Fixed rows are yellow, the rest are `Unchanged` (noting any tag that changed). The user retests the fixed rows in the browser.

If the same row is rejected 3 rounds in a row, stop and talk it through in chat instead of trying a fourth fix blindly.

## Review log

Keep a markdown log per story so the user has a history of what was rejected and why. It is the same file `outsystems-mcp-implementation` writes its plan check to:

- In a repository that has a `reviews/` folder at its root: `reviews/<eSpaceName>/<story-id>.md`.
- Otherwise: `~/.claude/outsystems-reviews/<eSpaceName>/<story-id or yyyy-mm-dd-short-name>.md`.

Append one section per round, headed `## Review — round N — <date time>`: one table per list (criteria and plan: ID, text, tag, reason, answer; changes: ID, type, path, status, answer), the No reasons, and the outcome (fixed rows, rows accepted despite a Rejected tag, validation errors, republished or not). End the approving round with `Review: approved`. Link the file to the user when the review ends.

## Things to keep in mind

- The reviewer is a developer reading the module in Service Studio. Use OutSystems vocabulary (aggregate, data action, OnParametersChanged, delete rule) and exact element names, so every row can be located and checked in seconds.
- Never present intended changes as done. If a lambda failed or a merge didn't apply, the row must say so, and the criteria or plan rows it serves are Rejected.
- Never review without both the implementation plan and an explicit acceptance criteria list.
- Publishing here only puts the reviewed code or a fix on Development so it can be tested (steps 1 and 6). Recording the story in the repository and closing it is `outsystems-mcp-publish`'s job.
