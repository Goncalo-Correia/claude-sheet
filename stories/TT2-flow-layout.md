# TT2: Re-lay out ClaudeSheet action flows (vertical first, no crossings)

- **Status:** Published
- **Published:** 2026-10-15 16:33 (manual 1-Click Publish from Service Studio)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-15
- **Depends on:** TT1
- **Trello:** https://trello.com/c/OWAFnzOA

## Story

### Goal

Bring every action flow of ClaudeSheet in line with the new **Flow layout** rules in `docs/conventions.md`, so flows read top to bottom and no connector crosses another. **No logic change**: only node positions change, plus extra End nodes (one per branch), which is a layout change.

### Rules applied (docs/conventions.md › Flow layout)

- Priority: **vertical**, then **horizontal**, then **diagonal**.
- Main path straight down one column, from Start to End.
- If False and Switch conditions leave **horizontally on the same row** into a new column, then run down it.
- **Each branch ends on its own End node.** No branch is drawn back to a shared End.
- For Each body to the right, and the flow after the loop continues below the body.
- Exception handlers in their own columns, and comments and unconnected nodes in a column of their own.
- Allowed diagonals: For Each loop-backs, and merges into a non-End node (noted in the implementation notes).
- **No crossings** (only in very exceptional, documented cases) and **no overlaps**.

### Current state (queries/flow-layout.cs, 2026-10-15)

64 flows: 292 vertical, 48 horizontal, 18 loop-back, 4 merge, **41 other diagonal**, **8 crossings**, 0 overlaps. **15 flows break the rules:**

- DaySheet_Save: 17 other diagonals, **8 crossings**, 2 merges
- Bootstrap: 3 other diagonals, 2 merges
- DaySheet_FillPending: 3 other diagonals
- DaySheet_Clear, Calendar_GetMonth, DoLogout, Holiday_CreateIfMissing, DaySheet_GetDetail: 2 other diagonals each
- Calendar_GetMonthSummary, Project_GetAssignedToUser: 1 other diagonal each
- HoursCalendar: OnInitialize (2 other diagonals), OnClick_ConfirmFillButton (1), OnClick_DayCell (1)
- DayStatusKPIs OnAfterFetch_GetKPIs (1), TotalHoursMonth OnAfterFetch_GetSummary (1)

### Expected result (dry run of queries/layout-flow.cs on all 64 flows)

- other diagonals 41 → 0, crossings 8 → 1, overlaps 0 → 0, merges 4 (allowed), loop-backs 18 (allowed)
- 7 new End nodes
- The one remaining crossing (DaySheet_Save) and the merge diagonals in DaySheet_Save and Bootstrap are fixed or justified by hand.

### Out of scope

- Any change to logic, names, widgets, data or CSS
- Changing the shared servicestudio-mcp-oml example (only a pointer to the project rules was added)

### Notes

- DaySheet_Save's crossings come from the TT1 re-layout, which removed the overlaps but put If False branches one row down (diagonal).
- Follows implement → review → publish; the review's Conventions section must show every flow passing.

## Acceptance criteria

- [ ] A01 · AC2.1 Given the module after TT2, When queries/flow-layout.cs runs, Then every flow shows 0 other diagonals, 0 crossings and 0 overlaps
- [ ] A02 · AC2.2 Given any flow, Then the main path runs straight down from Start to End and If False / Switch branches leave horizontally on the same row
- [ ] A03 · AC2.3 Given any If or Switch branch that ends the flow, Then it ends on its own End node (no connector into a shared End)
- [ ] A04 · AC2.4 Given any For Each, Then its body is to the right and the flow after the loop continues below the body; the loop-back is the only diagonal
- [ ] A05 · AC2.5 Given a merge diagonal that remains, Then it is listed with its reason in the TT2 implementation notes
- [ ] A06 · AC2.6 Given the snapshot diff before and after TT2, Then only node positions changed and End nodes were added: no other node, expression, name, widget or CSS change
- [ ] A07 · AC2.7 Given the published module and the mockup scenario (October 2026), When I use TotalHoursMonth, HoursCalendar and DayStatusKPIs, Then they behave exactly as before
- [ ] A08 · AC2.8 Given getValidationMessages, Then it returns 0 errors

## Implementation plan

1. P01 · Run queries/flow-layout.cs on the live module and confirm the list of flows that break the rules (15 expected)
2. P02 · Run queries/layout-flow.cs with the 15 flows as targets: DaySheet_Save, Bootstrap, DaySheet_FillPending, DaySheet_Clear, Calendar_GetMonth, DoLogout, Holiday_CreateIfMissing, DaySheet_GetDetail, Calendar_GetMonthSummary, Project_GetAssignedToUser, HoursCalendar OnInitialize, OnClick_ConfirmFillButton, OnClick_DayCell, DayStatusKPIs OnAfterFetch_GetKPIs, TotalHoursMonth OnAfterFetch_GetSummary
3. P03 · Verify each re-laid-out flow keeps the same connectors (same source → target pairs, apart from branches redirected to their new End nodes)
4. P04 · Fix by hand the remaining DaySheet_Save crossing (re-route the merge so it rejoins horizontally, or add an End node if the branch can finish there)
5. P05 · Review the merge diagonals in DaySheet_Save and Bootstrap: make the merge connector horizontal where possible, otherwise note why in the implementation notes
6. P06 · Run queries/flow-layout.cs again: every flow shows 0 other diagonals, 0 crossings and 0 overlaps; getValidationMessages returns 0 errors; merge
7. P07 · Review with outsystems-mcp-code-review (Conventions section: all flows pass), publish with outsystems-mcp-publish and refresh the snapshots

## Implementation notes

- Implemented 2026-10-15 in **3 `applyModelApiCode` saves**, all on the same 15 flows (P02). Each save is a layout lambda that also checks the connectors (P03): every connector keeps its target, and the only allowed change is a connector moved from an End node to a newly created End. A failed check aborts the save. Calls went over raw HTTP, as in US3 and TT1.
- **P01:** the live report matched the card exactly: 64 flows, 41 other diagonals, 8 crossings, 15 flows breaking the rules.
- **Save 1** (`queries/layout-flow.cs` as committed, rule-for-rule): other diagonals 41 → 0, crossings 8 → 1, and 7 new End nodes. One End each in DaySheet_Clear, DoLogout, Holiday_CreateIfMissing, DaySheet_Save, HoursCalendar OnClick_DayCell, TotalHoursMonth OnAfterFetch_GetSummary and DayStatusKPIs OnAfterFetch_GetKPIs.
- **The layout tool was improved during the work** (saves 2 and 3), because a grid dump showed problems the counters didn't measure. `queries/layout-flow.cs` and `queries/flow-layout.cs` now hold the final version:
  - **Staircase main path:** DaySheet_Save's validation Ifs sent the Raise Exception down and the real continuation right, so the main path drifted 6 columns. Now the taller branch goes down and the error branch goes right.
  - **Far-right side branches:** a side branch was placed right of *everything* below it (the first error was at column 10). Now it takes the first column that is free in the rows it uses.
  - **Connectors through nodes:** the report now counts a connector passing within 400 units of a node it doesn't connect as an overlap. There were 8 after save 2. Fixed with two changes: an optional step that rejoins goes right (Bootstrap 'Has SheetUser?'); and loop bodies (For Each, or an If branch that leads back to its If, like HoursCalendar.OnInitialize) go far enough right that the loop-back clears them.
  - **Loop-back classification:** any back-edge now counts as a loop-back, including loops built with an If.
  - `docs/conventions.md` › Flow layout was updated with these four points.
- **P04:** DaySheet_Save now has **0 crossings**. Its main path runs straight down column 0, and every validation error leaves horizontally into column 1.
- **P05, merge diagonals kept (allowed, justified):**
  - **DaySheet_Save** 'New day sheet?': the True and False branches each set different fields (Assign) and must rejoin before the shared CreateOrUpdateDaySheet. An End can't replace the merge.
  - **Bootstrap** 'Client missing?' and 'Project missing?': lookup-or-create. Both branches set the Id and rejoin before the next seed step.
- **P06, exception (1 crossing, justified):** **DaySheet_FillPending**. Inside the For Each body, If 'Draft with hours?' has **two branches that both loop back** to the For Each (Assign at column 3 and Assign at column 2). The outer branch's loop-back has to pass the inner branch's column on the way to the For Each in column 0. Removing it would need a logic change (merging the branches into one "next" node), which is out of scope.
- **P06 result:** across 64 flows, 308 vertical, 70 horizontal, 22 loop-back, 3 merge, **0 other diagonal**, 1 crossing (above) and **0 overlaps**. getValidationMessages returns 0 errors. The other 49 flows weren't touched; they already pass the report.
- **P07 (review and publish)** is not done here.
- **Merge:** the `omlMerge` tool call showed as rejected in the conversation, but the merge reached Service Studio. The out files were deleted (a successful merge deletes them), and the live module shows exactly the validated state with 0 errors. A retry was refused with "mutatedOmlPath does not match the working copy", because no working copy was tracked any more. Confirm in Service Studio that DaySheet_Save runs straight down column 0.

## Trail

- Changes: `changes/2026-10-15/160952-ClaudeSheet-TT2.cs` (first layout of the 15 flows), `161209` (taller branch down, range-aware columns), `161841` (final layout: optional steps, loops clear). Written by Claude from the applied lambdas (manual publish, MCP over raw HTTP).
- Review: `reviews/ClaudeSheet/TT2.md` (plan check round 1, review round 1: all approved; A01, A02, A04, N01–N03 accepted despite Rejected tag)
- Published: see `PUBLISHES.md` (2026-10-15 16:33, success (manual))
- Snapshots: `snapshots/ClaudeSheet/` refreshed 2026-10-15 (targeted)
- Follow-up noted in the review: the For Each continuation in DayStatusKPIs GetKPIs, HoursCalendar OnClick_SaveDay and OpenDayEditor is on the last body row, not below the body (flows TT2 didn't touch)
