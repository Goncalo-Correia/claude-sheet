---
name: outsystems-mcp-investigate
description: Investigates OutSystems 11 code, modules or features through the Service Studio MCP, read-only. It starts one subagent per module to analyse it (data model, logic, screens and blocks, roles, CSS, validation messages, conventions, flow layout, performance, security, dead code), compiles every finding into one numbered checklist, then turns the checklist into proposed user stories and technical tasks with a description, an implementation plan and acceptance criteria. The user approves or rejects each story in a form in the conversation; once the form is submitted it generates a new prompt containing only the approved stories, ready to run with outsystems-mcp-implementation. Use whenever the user asks to investigate, analyse, analyze, audit, inspect, assess, look into, check the health of, or find problems or improvements in any OutSystems code, module, screen, block, action or feature, for example "investigate the calendar", "analyse ClaudeSheet_CS", "what can we improve in MySheet", "audit the modules", or names outsystems-mcp-investigate. It never changes a module.
---

# OutSystems MCP investigation

The purpose is to turn "look at this and tell me what's wrong or missing" into a short list of stories the team has agreed to build, without anyone reading the whole module by hand. The investigation is **read-only**: nothing is changed, merged or published.

```
"investigate X"
  -> 1. scope: which modules, which feature, what to look for
  -> 2. one subagent per module, read-only, each returns its findings
  -> 3. compile the findings into one checklist (F01, F02, …) and show it
  -> 4. group the checklist into proposed stories (US / TT) with plan and acceptance criteria
  -> 5. approval form: Approve / Reject per story, one Submit
  -> 6. on submit: generate the new prompt with the approved stories only
```

Service Studio MCP tools are named `mcp__servicestudio__*`. If their schemas are not loaded, load them with ToolSearch. Follow the `servicestudio-mcp-oml` skill for the read tools and for throw-probes.

## Hard rules

- **Read-only.** No `applyModelApiCode` that saves, no `omlMerge`, `omlReset`, `omlRefreshReferences` or `omlPublish`. `applyModelApiCode` is allowed only as a throw-probe (ends in `throw new Exception("PROBE:" + …)`, saves nothing). Subagents get the same rule.
- **Session token.** Call `createSessionToken` once in the main session (reuse it if the conversation already has one) and pass it to the subagents in their prompt. Never write it to a file.
- **One read at a time per module.** Parallel reads on the same module fail with "OML not found".
- Findings come from the live module, not from memory or intent. Snapshots (`snapshots/<Module>/`) can be used to plan reads and to spot drift, but every finding is confirmed with a live read.
- Don't write story files, don't touch Trello or other trackers, and don't commit. The only file this skill writes is the investigation report (step 3).

## 1. Scope

Work out from the request and the repository, and ask only for what can't be inferred:

- **Modules.** The ones the user named; otherwise every module `listApps` shows with `isOpen: true` and `isReference: false`. If the request names a feature ("the calendar"), find which modules hold it (`CLAUDE.md`, `docs/architecture.md`, `snapshots/*/names/*.json`) and include only those, plus the producer modules its logic lives in.
- **Focus.** A feature or element list ("HoursCalendar and the actions it calls"), or the whole module.
- **Lenses.** By default all of them (below). If the user asked for one angle ("performance", "security", "is it following our conventions"), use only that one.

Every module in scope must be open in Service Studio. If one isn't, ask the user to open it and stop.

State the scope in one line before starting ("Investigating HoursCalendar in ClaudeSheet and the CS actions it calls, all lenses, 2 subagents").

### Lenses

| Lens | What to look for |
|---|---|
| Correctness | Logic that can't do what the description or the story says, unhandled branches, wrong filters, missing null/empty handling, events never triggered or never handled, data actions never refreshed |
| Validation | Every `getValidationMessages` error and warning, with its `ownerPath` |
| Conventions | Everything in `docs/conventions.md` (names, module placement, delete rules, role checks, CSS placement) and the Flow layout rules (`checks/flow-layout.cs` report, when the repo has it) |
| Security | Screens without an explicit role, server actions without a role check, public elements that don't need to be public, writes not going through Core actions, sensitive data sent to the client |
| Performance | Aggregates in loops, server calls per row, missing indexes on filtered attributes, large data fetched to the client, data actions fetched At Start that aren't needed at start |
| Maintainability | Duplicated logic, unused elements, very long flows, hard-coded values that should be site properties or static entities, missing descriptions |
| UX and accessibility | Missing loading / empty / error states, missing aria attributes, non-semantic markup, responsive gaps |
| Gaps | What a feature obviously lacks compared with its story, its acceptance criteria or the rest of the app |

## 2. One subagent per module

Start one subagent (Agent tool, `general-purpose`) per module in scope. Different modules can run at the same time; if a subagent reports "OML not found", run that module again on its own once the others have finished. Never start two subagents on the same module.

Give each subagent, in its prompt:

- the module name (`eSpaceName`), its layer and role (from `CLAUDE.md` / `docs/architecture.md`), and the focus and lenses;
- the session token, and the rules: read-only, throw-probes only, one read call at a time, `includeJson: "Never"` on the read tools, follow `servicestudio-mcp-oml`;
- the repository paths to read first: `docs/conventions.md`, `docs/architecture.md`, `snapshots/<Module>/`, and the `stories/` files of the elements in focus (their acceptance criteria are the expected behaviour);
- for the conventions lens, `checks/flow-layout.cs` (strip the `//` header lines, set `onlyFlows` when the focus is narrow);
- what to return, exactly in this shape, and nothing written to the repository:

```text
MODULE: <eSpaceName>
READS: <number of read calls and probes made>
FINDING
  title: <one line, the problem or opportunity, not the fix>
  lens: <Correctness | Validation | Conventions | Security | Performance | Maintainability | UX | Gaps>
  severity: <High | Medium | Low>
  element: <path as in Service Studio, e.g. Interface › UI Flows › SheetBlocks › HoursCalendar › OnClick_SaveDay>
  evidence: <what the live module shows: node names, expressions, message text>
  suggestion: <the change that would fix it, with element names>
  depends_on_module: <other module that must change too, or ->
FINDING
  …
NOT CHECKED: <anything in scope it couldn't verify, and why>
```

Severity: **High** = wrong data, a security hole, an error, or a feature that doesn't work; **Medium** = a convention or warning, a performance risk, a missing state; **Low** = polish, naming of untouched elements, descriptions.

Tell the subagent to report only what it confirmed in the live module, to quote the evidence, and to keep findings in different elements separate even when they share a cause.

## 3. Compile the checklist

When every subagent has returned:

1. **Merge.** One list across modules. Drop exact duplicates; when two subagents found two sides of one problem (a CS action and the block that calls it), keep both rows and link them (`see F07`).
2. **Number** the findings `F01`, `F02`, … ordered by severity, then module (producer first), then element.
3. **Write the report** to `~/.claude/outsystems-investigations/<repository folder name>/<yyyy-mm-dd>-<slug>.md`, outside the repository: it is a working file, and the approved stories become story files. Create the folder if needed. It holds the scope, the date, the modules and read counts, the checklist as a table (ID, severity, lens, module, element, finding, evidence, suggestion) and each subagent's NOT CHECKED notes.
4. **Show the checklist** in the conversation as a markdown table (ID, severity, lens, module, element, finding), most severe first, with a one-line count per severity. Don't ask for approval of findings: the approval happens at story level.

## 4. Compile the checklist into stories

Group the findings into proposed stories. Each finding goes into exactly one story, or into the **Not proposed** list with a reason (too small on its own and nothing to group it with, needs a product decision, out of scope).

Grouping rules:

- One story per user-visible outcome (a feature works, a screen gets an empty state); one technical task (`TT`) per technical outcome (conventions clean-up in a module, performance of one action, security hardening).
- Don't mix unrelated changes, and don't split one change across stories. Findings in the same element with the same fix go together.
- A story that touches several modules keeps them in one story, with the plan grouped per module, producer first (the repository's multi-module rules apply when it's implemented).
- High severity findings never go to Not proposed.

IDs: the next free numbers after the highest `US<n>` / `TT<n>` in `stories/` (match the existing format, e.g. `US6`, `TT5`). Mark them as proposed: they're only reserved if approved.

Each proposed story has:

- **ID and title**: `US6 · [Screen] MySheet: empty state when no projects are assigned` (type in brackets: Screen, Block, Logic, Data, Tech, …).
- **Description**: for a US, *As a … I want … so that …* plus context; for a TT, the problem and why it matters. End with the findings it covers (`Covers: F03, F08`).
- **Implementation plan**: numbered steps grouped per module (producer first), each naming the element to create or change and following `docs/conventions.md` (names, placement).
- **Acceptance criteria**: numbered `AC<n>.<m>`, each checkable (Given / When / Then, or a validation or naming check).
- **Size**: S / M / L, and **Priority**: the highest severity among its findings.

Append the stories and the Not proposed list to the report, then show them in the conversation as a compact table (ID, title, priority, size, modules, findings covered) before the form.

## 5. Approval form

Render the form **inline in the conversation** with `mcp__visualize__show_widget` (call `mcp__visualize__read_me` with modules `["elicitation"]` once beforehand if it hasn't been read in this conversation). Use the markup in [references/approval-form.md](references/approval-form.md). One row per proposed story: ID, title, priority, size, modules, the description in two lines, the findings covered, and Approve / Reject pills; Reject reveals an optional reason box. One Submit button. If the widget tool isn't available, show the table and ask the user to reply with `US6 Approve`, `TT5 Reject: <reason>` lines.

Never write the form to an `.html` file or publish it as an Artifact.

## 6. On submit: generate the new prompt

The answers arrive as one line, e.g. `Investigation stories — US6: Approve · TT5: Reject · TT5 (other): not now · US7: Approve`.

1. **Every story needs an answer.** If any is missing, list it and show the form again. A skipped form approves nothing: ask what to do.
2. **Update the report**: mark each story Approved or Rejected (with the reason). Rejected stories are removed from the proposals list and kept only as one line under `## Rejected` so they aren't proposed again by a later investigation.
3. **Generate the new prompt** with the approved stories only, in one fenced `text` block the user can copy or send as is. It contains, per approved story in priority order: the ID and title, the description, the implementation plan and the acceptance criteria, exactly as approved, and it opens with the instruction to implement them one at a time with `outsystems-mcp-implementation`, using this definition instead of asking for it. Template:

````text
Implement the following approved stories one at a time with outsystems-mcp-implementation, in this order.
Each story's description, implementation plan and acceptance criteria are below: use them as the story definition instead of asking for them.
Source: investigation <yyyy-mm-dd>-<slug> (cite the finding IDs in each story's Source line)

## US6 · [Screen] MySheet: empty state when no projects are assigned
### Description
…
### Implementation plan
#### ClaudeSheet
1. …
### Acceptance criteria
- AC6.1 …

## TT5 · …
````

4. Save the same prompt at the end of the report (`## Approved prompt`).
5. Close with one line: how many stories were approved and rejected, and where the report is. If nothing was approved, say so and don't generate a prompt.

Don't start implementing: the user decides when to send the prompt.

## Things to keep in mind

- The reader is a developer who will open the module in Service Studio. Use OutSystems vocabulary (aggregate, data action, OnParametersChanged, delete rule) and exact element paths.
- Prefer fewer, well-grouped stories over one story per finding.
- A finding without live evidence isn't a finding. If a subagent couldn't verify something, it goes to NOT CHECKED, not to the checklist.
- Suggestions must follow `docs/conventions.md`; if the best fix contradicts it, say so in the story instead of silently breaking the convention.
