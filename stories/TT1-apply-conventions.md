# TT1: Apply the naming and flow layout conventions to ClaudeSheet

- **Status:** Published
- **Published:** 2026-10-15 15:37 (manual 1-Click Publish from Service Studio)
- **Modules:** ClaudeSheet
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-15
- **Depends on:** TT0, US1, US2, US3
- **Trello:** https://trello.com/c/I6jHXsn1

## Story

### Goal

Bring the existing ClaudeSheet code in line with the new rules in `docs/conventions.md` (Action names, and No overlapping nodes), so every later story starts from a module that follows the standard. **No functional change**: only action names and node positions change.

### Rules applied

- **Reusable server and client actions**: `<Entity>_<Verb>[<Qualifier>]`, e.g. `Item_GetById`, `Item_CreateOrUpdate`, `Item_GetNewest`.
- **Screen and block actions bound to an event**: `<Event>_<Target>`, e.g. `OnClick_Item`, `OnChange_Item`, `OnAfterFetch_GetItem`. Lifecycle handlers take the event name (`OnInitialize`, `OnReady`, `OnParametersChanged`). A handler shared by several widgets is `<Event>_<Purpose>`.
- **Helpers** (screen/block actions not bound to an event): verb + noun. An action is never both an event handler and called from other actions.
- **No overlapping nodes** in any action flow (closer than 900 units on both axes). Checked with `queries/overlapping-nodes.cs`.

### Scope (proposed names, confirm during implementation)

**Server actions**
- ClearDaySheet → DaySheet_Clear
- FillPendingDays → DaySheet_FillPending
- GetDaySheetDetail → DaySheet_GetDetail
- SaveDaySheet → DaySheet_Save
- GetMonthCalendar → Calendar_GetMonth
- GetMonthSummary → Calendar_GetMonthSummary
- GetUserProjects → Project_GetAssignedToUser
- FormatMinutes → Minutes_Format
- GetWeekStart → Date_GetWeekStart
- Unchanged: Holiday_CreateIfMissing (already conforms), Bootstrap (timer action), DoLogin / DoLogout (template), CheckSheetUserRole (platform)

**Client actions**
- FormatMinutes → Minutes_Format
- GetWeekStart → Date_GetWeekStart

**Block actions**
- TotalHoursMonth: GetSummaryOnAfterFetch → OnAfterFetch_GetSummary
- DayStatusKPIs: GetKPIsOnAfterFetch → OnAfterFetch_GetKPIs
- HoursCalendar:
  - PreviousMonthOnClick → OnClick_PreviousMonth
  - NextMonthOnClick → OnClick_NextMonth
  - DayOnClick → OnClick_DayCell
  - OpenFillPopup → OnClick_FillPendingButton
  - SaveDay (Save as draft, Confirm day, Save day off, Set day off and delete hours) → OnClick_SaveDay
  - ClearDay → OnClick_ClearDayButton
  - CloseDayEditor → OnClick_CancelButton
  - ConfirmFill → OnClick_ConfirmFillButton
  - CloseFillPopup → OnClick_CancelFillButton
  - BuildTimeOptions (OnInitialize) → OnInitialize
  - BindEscapeKey (OnReady) → OnReady
  - RecalcEditorTotal (bound to HoursDropdown and MinutesDropdown OnChange, and called by OpenDayEditor) → split into handler OnChange_EditorTime + helper RecalcEditorTotal
  - DayEditorSaved (helper) → FinishDaySave
  - Unchanged: OpenDayEditor, RecalcEditorTotal (helpers), OnParametersChanged

**Overlapping nodes found on 2026-10-15 (63 flows checked)**
- SaveDaySheet: 7 overlapping pairs (If/End/RaiseError/ForEach/Assign/ExecuteAction stacked at x 5028 and 6856)
- HoursCalendar › BuildTimeOptions: 2 Assign nodes at the same position

### Out of scope

- Any change to behaviour, data model, widgets or CSS
- Renaming entities, structures, blocks, widgets or data actions (they already follow the conventions)

### Notes

- Renames inside the module update their call sites automatically. ClaudeSheet has no consumer modules, so no reference refresh is needed elsewhere.
- US4 and US5 (Backlog) mention some of the old names (e.g. GetMonthSummary, GetWeekStart, FormatMinutes); update them as part of this task.
- Follows the implement → review → publish flow; the review's new Conventions section must come back clean.

## Acceptance criteria

- [ ] A01 · AC1.1 Given the module after TT1, When I open Logic › Server Actions, Then every own action is named <Entity>_<Verb>[<Qualifier>] except DoLogin, DoLogout and Bootstrap
- [ ] A02 · AC1.2 Given Logic › Client Actions, Then the client actions are Minutes_Format and Date_GetWeekStart
- [ ] A03 · AC1.3 Given any block, When I list its actions bound to an event, Then each is named <Event>_<Target>, <Event>_<Purpose> (shared handler) or the lifecycle event name
- [ ] A04 · AC1.4 Given any block, Then no action is both bound to an event and called from another action, and every helper is named verb + noun
- [ ] A05 · AC1.5 Given the overlap probe (queries/overlapping-nodes.cs), When it runs against the module, Then it returns "no overlaps" for every flow
- [ ] A06 · AC1.6 Given the renamed module, Then getValidationMessages returns 0 errors and every call site uses the new names
- [ ] A07 · AC1.7 Given the snapshot diff before and after TT1, Then only action names and node positions changed: no entity, attribute, structure, widget, expression or CSS change
- [ ] A08 · AC1.8 Given the published module and the mockup scenario (October 2026), When I use TotalHoursMonth, HoursCalendar and DayStatusKPIs, Then they behave exactly as before (totals, colours, KPI counts, day editor, fill pending days)
- [ ] A09 · AC1.9 Given a new story implemented after TT1, When it is reviewed with outsystems-mcp-code-review, Then the Conventions section checks names and flow overlaps of its changes
- [ ] A10 · AC1.10 Given the snapshots after the publish, Then the files of the old action names are gone and the new names are present
- [ ] A11 · AC1.11 Given the US4 and US5 cards and the repository docs, Then they reference the new action names

## Implementation plan

1. P01 · Re-run the overlap probe (queries/overlapping-nodes.cs) and the naming inventory on the live module; confirm the rename list with the team before changing anything
2. P02 · Rename the reusable server actions: ClearDaySheet → DaySheet_Clear, FillPendingDays → DaySheet_FillPending, GetDaySheetDetail → DaySheet_GetDetail, SaveDaySheet → DaySheet_Save, GetMonthCalendar → Calendar_GetMonth, GetMonthSummary → Calendar_GetMonthSummary, GetUserProjects → Project_GetAssignedToUser, FormatMinutes → Minutes_Format, GetWeekStart → Date_GetWeekStart
3. P03 · Rename the client actions: FormatMinutes → Minutes_Format, GetWeekStart → Date_GetWeekStart
4. P04 · Rename the On After Fetch handlers: TotalHoursMonth GetSummaryOnAfterFetch → OnAfterFetch_GetSummary, DayStatusKPIs GetKPIsOnAfterFetch → OnAfterFetch_GetKPIs
5. P05 · Rename the HoursCalendar click handlers: OnClick_PreviousMonth, OnClick_NextMonth, OnClick_DayCell, OnClick_FillPendingButton, OnClick_SaveDay, OnClick_ClearDayButton, OnClick_CancelButton, OnClick_ConfirmFillButton, OnClick_CancelFillButton
6. P06 · Rename the HoursCalendar lifecycle handlers BuildTimeOptions → OnInitialize and BindEscapeKey → OnReady, and the helper DayEditorSaved → FinishDaySave
7. P07 · Split RecalcEditorTotal: new handler OnChange_EditorTime bound to HoursDropdown and MinutesDropdown OnChange calls the helper RecalcEditorTotal; check no other event handler is also called from another action (split it the same way if so)
8. P08 · Re-lay out the SaveDaySheet (DaySheet_Save) and BuildTimeOptions (OnInitialize) flows with the standard spacing (1371 vertical, 1828 horizontal) without changing any connector
9. P09 · Run the overlap probe again (no overlaps in any flow) and getValidationMessages (0 errors); merge
10. P10 · Review with outsystems-mcp-code-review (Conventions section clean), publish with outsystems-mcp-publish and refresh the snapshots, deleting the old-name snapshot files
11. P11 · Update the old action names in the US4 and US5 cards, the stories and docs that mention them

## Implementation notes

- Implemented 2026-10-15 in **6 `applyModelApiCode` saves**, one per plan group: server renames (P02), client renames (P03), On After Fetch renames (P04), HoursCalendar renames (P05 + P06, same block), RecalcEditorTotal split (P07), flow layout (P08). Calls go over raw HTTP to the Service Studio MCP endpoint, as in US3.
- **P01:** the live inventory matched the card. A dry-run probe confirmed that renaming an action also renames its Execute Action nodes and rewrites the expressions that use their outputs (e.g. `Calendar_GetMonthSummary.Summary.ConfirmedCount`), with 0 errors. The rename list was confirmed by the user ("keep" on the description and plan).
- **Renames:** every rename was read back. Service Studio added no suffixes, and 0 validation errors remain. Bootstrap's 12 `Holiday_CreateIfMissing<N>` node names were already conventional and are untouched.
- **P07:** `OnChange_EditorTime` (Start → RecalcEditorTotal → End) is now the OnChange of HoursDropdown and MinutesDropdown, and `RecalcEditorTotal` is a helper only (called by OnChange_EditorTime and OpenDayEditor). No other event handler is called from another action. The only calls between block actions are to the helpers OpenDayEditor, RecalcEditorTotal and FinishDaySave.
- **P08:** DaySheet_Save and HoursCalendar.OnInitialize were laid out on the standard grid (1828 × 1371, origin 3200/800): a breadth-first walk from Start, If False branches and For Each bodies one column to the right, exception handlers in their own columns, and a node moves right when its cell is taken. Only positions changed: the lambda compares every node's connector targets before and after and would have aborted on any difference.
- **P09:** the overlap probe returns `no overlaps` for all 64 flows (63 + OnChange_EditorTime), and getValidationMessages returns 0 errors. All 17 HoursCalendar event bindings read back with the new handler names.
- **P10 (review and publish)** is not done here: this skill doesn't review or publish.
- **P11, deviation:** the US4 card's two checklist items now use `Date_GetWeekStart` and `Minutes_Format`, US5 has no old names, and ADR 0002 names `Project_GetAssignedToUser`. The finished stories (US-000, US1, US2, US3) were **not rewritten**. Each got a "Renamed in TT1" line with the old → new names, because their notes and `changes/` files record what was built under the old names. Review logs are history and were left as they are.
- **Spotted, out of scope:** US4 plans a new server action `GetWeeklyProjectHours`, which doesn't follow the new rule (it would be something like `ProjectHours_GetWeekly`). The implementation skill will flag it when US4 starts.

## Trail

- Changes: `changes/2026-10-15/150201-ClaudeSheet-TT1.cs` (server renames), `150225` (client renames), `150244` (On After Fetch renames), `150306` (HoursCalendar renames), `150341` (RecalcEditorTotal split), `150418` (flow layout). Written by Claude from the applied lambdas (manual publish, MCP over raw HTTP).
- Review: `reviews/ClaudeSheet/TT1.md` (plan check round 1, review round 1: all approved; A03, A07, A10 accepted despite Rejected tag)
- Published: see `PUBLISHES.md` (2026-10-15 15:37, success (manual))
- Snapshots: `snapshots/ClaudeSheet/` refreshed 2026-10-15 (targeted); old-name files removed
- Left as is (accepted): 8 Description texts still mention old action names; OutSystems template blocks in Layouts/Common keep their template action names
