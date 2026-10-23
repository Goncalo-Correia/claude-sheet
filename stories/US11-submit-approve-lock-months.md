# US11: [Screen] Submit, approve and lock monthly timesheets

- **Status:** Done
- **Published:** ClaudeSheet_CS 2026-10-23 14:04, ClaudeSheet 2026-10-23 14:22 (both through the MCP)
- **Modules:** ClaudeSheet_CS, ClaudeSheet (producer first)
- **Requested by:** goncalo.c@bool.pt
- **Created:** 2026-10-23
- **Depends on:** TT5 (SheetManager role, GetMenuAccess), US10 (HoursCalendar day editor)
- **Source:** gap analysis of 2026-10-16, findings F13, F17 (the analysis file is in commit 5bb59c3)
- **Trello:** https://trello.com/c/LrYU7ORJ (moved to Done 2026-10-23)

## Story

As a consultant I want to submit my month, and as a manager I want to approve or reject it, so that approved hours can't change afterwards. Today confirmed days can still be edited or cleared, and there is no submission or approval. Covers: F13, F17.

## Acceptance criteria

- [x] A01 · AC11.1 Given a month with no Pending days, When the user submits it, Then the badge shows Submitted and its days can't be edited or cleared.
- [x] A02 · AC11.2 Given a month with Pending days, Then Submit is refused with a message giving how many are pending.
- [x] A03 · AC11.3 Given a manager approves it, Then it stays locked; When they reject it with a comment, Then it reopens and the user sees the comment.
- [x] A04 · AC11.4 Given a user calls DaySheet_Save on an approved month directly, Then the server refuses it.
- [x] A05 · AC11.5 Given a SheetUser without SheetManager, Then Approvals is refused; 0 errors and 0 warnings.

## Implementation plan

### ClaudeSheet_CS

1. P01 · Static entity PeriodStatus (Open, Submitted, Approved, Rejected) and entity MonthSheet (UserId, Year, Month, PeriodStatusId, SubmittedOn, DecidedBy, DecidedOn, Comment; unique index UserId+Year+Month; public, read-only).
2. P02 · Public server actions: MonthSheet_GetForUser (Year, Month; current user), MonthSheet_Submit (SheetUser; only when the month has no Pending days), MonthSheet_GetSubmitted (SheetManager), MonthSheet_Approve, MonthSheet_Reject (Comment required), MonthSheet_Reopen (SheetManager). User exception PeriodValidation.
3. P03 · DaySheet_Save, DaySheet_Clear and DaySheet_FillPending raise DaySheetValidation ("This month is submitted or approved") when the month is Submitted or Approved.
4. P04 · Calendar_GetMonth: add IsLocked to the result.

### ClaudeSheet

Dependencies to add (ClaudeSheet_CS): PeriodStatus, MonthSheet, the 6 actions, PeriodValidation, SheetManager.

5. P05 · MySheet / HoursCalendar: month status badge and "Submit month" button; locked days open read-only.
6. P06 · Screen MainFlow › Approvals (SheetManager): submitted months (user, month, total vs expected) with Approve, Reject with comment, and Reopen.
7. P07 · Menu link "Approvals", visible to SheetManager.

## Implementation notes

Decisions before building (2026-10-23, user's choice):
- **Approvals list:** Submitted months by default; a "Show approved" checkbox adds Approved months. Approve and Reject only on Submitted rows, Reopen only on Approved rows (MonthSheet_GetSubmitted takes IncludeApproved).
- **Submit timing:** a month can be submitted only once it has ended, besides the no-Pending-days rule ("You can submit a month once it has ended.").
- **Roles:** Approvals and the manager actions are SheetManager only.
- **Locked** = Submitted or Approved. Open and Rejected months stay editable; a rejected month shows the manager's comment.

### ClaudeSheet_CS

- Implemented 2026-10-23 in **4 `applyModelApiCode` saves** (data and helpers; the 6 actions; a fix of 3 If conditions (`GetMonthSheetForUpdate.Record.MonthSheet.PeriodStatusId`); the locks), 0 validation messages at the end. Two attempts failed before saving anything: a wrong enum namespace, and ListFilter's input is `SourceList`.
- Flow-layout report on the 12 touched flows: 0 other diagonals, 0 crossings, 0 overlaps, 0 unlabelled nodes, every action in a folder. DaySheet_Save keeps its existing merge diagonal (from before this story).
- **P01 · PeriodStatus** (static, public, read-only): Open 1, Submitted 2, Approved 3, Rejected 4 (Id, Label, Order, Is_Active). **MonthSheet** (public, read-only, Update Behavior Optimized): Id, UserId (User, mandatory, Delete), Year*, Month*, PeriodStatusId* (Protect), SubmittedOn, DecidedBy (User, Protect by default), DecidedOn, Comment (Text 500). Unique index **UQ_MonthSheet_UserYearMonth** (UserId, Year, Month). No MonthSheet record = Open.
- **P02 · folder MonthSheet**, all public except the two helpers:
  - `MonthSheet_GetForUser(Year, Month)` → PeriodStatusId (Open when there's no record), Comment, DecidedOn, IsLocked. SheetUser.
  - `MonthSheet_Submit(Year, Month)`, SheetUser. In order: `Month ended?` (the first day of the next month <= today) → Calendar_GetMonth → `GetPendingDays` (System ListFilter, DayTypeId = Pending) → `Pending days?` raises "1 day is / N days are still pending. Confirm them or mark them as days off before submitting." → `Already submitted?` (Submitted or Approved) → CreateOrUpdate with Submitted, SubmittedOn = now, decision and comment cleared (a Rejected month can be resubmitted).
  - `MonthSheet_GetSubmitted(IncludeApproved)` → Months (List of the new public structure **MonthSheetSummary**: MonthSheetId, UserId, UserName, Year, Month, PeriodStatusId, SubmittedOn, DecidedOn, Comment, TotalMinutes, ExpectedMinutes). SheetManager. Newest month first, then user name.
  - `MonthSheet_Approve(MonthSheetId)` (Submitted → Approved, comment cleared), `MonthSheet_Reject(MonthSheetId, Comment)` (Comment required and trimmed; Submitted → Rejected), `MonthSheet_Reopen(MonthSheetId)` (Approved → Open, comment cleared). SheetManager; each sets DecidedBy and DecidedOn; the wrong status raises PeriodValidation.
  - User exception **PeriodValidation** for these business rules. Its Public flag can't be set through the Model API. ClaudeSheet doesn't need it: per the conventions it catches AllExceptions and shows the message.
  - Helpers (not in the plan, private): **MonthSheet_IsLocked(UserId, Year, Month)** is a *function* (Submitted or Approved), used in expressions by the lock checks and Calendar_GetMonth. **MonthSheet_GetTotals(UserId, Year, Month)** → TotalMinutes, ExpectedMinutes (weekdays − active weekday holidays − days off) × Site.DailyExpectedMinutes, as in Calendar_GetMonthSummary, but for any user.
- **P03:** DaySheet_Save, DaySheet_Clear and DaySheet_FillPending have a new If **Month locked?** right after the SheetUser check, which raises DaySheetValidation "This month is submitted or approved." The nodes below moved down one row (positions only). Descriptions updated.
- **P04:** Calendar_GetMonth has a new output **IsLocked**, set in `Month bounds` with MonthSheet_IsLocked(GetUserId(), Year, Month).

### ClaudeSheet

- **Dependencies** (2026-10-23, through the MCP with `outsystems-mcp-add-reference`, as in US10): MonthSheet_GetForUser, _Submit, _GetSubmitted, _Approve, _Reject, _Reopen, structure MonthSheetSummary and static entity PeriodStatus added to the ClaudeSheet_CS reference (1 save, merged), then `omlRefreshReferences` (Calendar_GetMonth.IsLocked). SheetManager was already referenced. MonthSheet and PeriodValidation aren't used by ClaudeSheet, so they weren't added.
- Implemented in **3 `applyModelApiCode` saves** (HoursCalendar; Approvals and Menu; theme CSS). One attempt failed to compile and saved nothing (IMobileWidget namespace). Validation: 0 messages apart from the theme's **InvalidIconLibrary** error. That error comes from the MCP sidecar's validator and appears on every theme stylesheet edit; the Model API skill documents it as expected, and it doesn't show in Service Studio. IconLibrary was left unset on purpose.
- Flow-layout report on the 10 touched flows: 0 other diagonals, 0 crossings, 0 overlaps, 0 unlabelled nodes.
- **P05 · HoursCalendar:**
  - **GetCalendar** calls MonthSheet_GetForUser (new node before `Set outputs`) and has new outputs PeriodStatusId, MonthComment and IsLocked (= Calendar_GetMonth.IsLocked).
  - Under the header there's a new container **MonthStatus** (visible once fetched) with the badge **MonthStatusBadge** "Month: Open / Submitted / Approved / Rejected" (`sheet-cal__status sheet-cal__status--<status>`) and the If **HasRejectComment**, which shows "Rejected by your manager: <comment>".
  - The footer has a new **SubmitMonthButton** "Submit month" (hidden when locked) → **OnClick_SubmitMonthButton**: MonthSheet_Submit, then refresh GetCalendar and show "Month submitted for approval."; any refusal (pending days, month not ended) is shown as an error message. FillPendingButton is hidden when the month is locked.
  - **Read-only when locked:** the If **IsLockedHint** shows "This month is submitted or approved, so its days are read-only."; DayKind, HoursDropdown, MinutesDropdown, DescriptionInput and NotesInput are disabled; Save as draft, Confirm day, Save day off, Set day off and delete hours, and Clear day are hidden. Days still open, so the user can read them.
- **P06 · Screen MainFlow › Approvals** (role SheetManager only; Menu ActiveItem 5):
  - Data action **GetApprovals** → MonthSheet_GetSubmitted(IncludeApproved = ShowApproved), with an AllExceptions handler → ErrorMessage (box HasLoadError).
  - Toolbar checkbox **ShowApprovedCheckbox** "Show approved" (OnChange_ShowApprovedCheckbox refreshes).
  - Table `admin-table admin-table--approvals` with columns User, Month, Hours (logged / expected, Minutes_Format), Status tag and Actions. The links **ApproveLink** and **RejectLink** show on Submitted rows, **ReopenLink** on Approved rows. Empty state: "No months waiting for approval."
  - **RejectPopup**: "Reject <user>, <month>", TextArea **RejectCommentInput** (500, mandatory), RejectErrorBox, buttons **ConfirmRejectButton** "Reject month" and **CancelRejectButton**.
  - Handlers **OnClick_ApproveLink / OnClick_ReopenLink(Row)** call the action, refresh and show "<user>: <month> approved / reopened."; errors are shown as an error message. **OnClick_ConfirmRejectButton**: errors stay in the popup.
- **P07 · Menu:** GetMenuAccess has a new output **IsSheetManager** = CheckSheetManagerRole() (Assign renamed `Check roles`), and **ApprovalsLink** "Approvals" is the last link in PageLinks, Visible = GetMenuAccess.IsSheetManager.
  - To check: the Menu highlights links by their position in PageLinks (ActiveItem 5). A manager who isn't an admin doesn't see the 4 admin links, so the highlight may land on the wrong link. This is how the conventions say the menu works, and it can only be checked in the browser.
- **Theme CSS** (ClaudeSheet theme, at the end): `.sheet-cal__status-row`, `.sheet-cal__status` and its `--submitted / --approved / --rejected` modifiers, `.sheet-cal__comment`, a gap on `.sheet-cal__footer`, and the column template `.admin-table--approvals` (as the conventions ask when a new layout needs one).

## Trail

- Changes: `changes/US11-ClaudeSheet_CS.md`, `changes/US11-ClaudeSheet.md`
- Review: `reviews/ClaudeSheet_CS/US11.md`, `reviews/ClaudeSheet/US11.md`
- Published: see `PUBLISHES.md` (ClaudeSheet_CS 2026-10-23 14:04, then ClaudeSheet 14:22; both through the MCP)
- Plan check and review: approved in one round per module (2026-10-23); accepted despite a Rejected tag: P02 (ClaudeSheet_CS plan check), A05 (review)
- Snapshots: refreshed 2026-10-23 (`snapshots/ClaudeSheet_CS/`, `snapshots/ClaudeSheet/`)
