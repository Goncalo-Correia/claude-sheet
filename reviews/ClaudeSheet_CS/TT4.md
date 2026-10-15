# Review: ClaudeSheet_CS · TT4 (move the backend from ClaudeSheet to ClaudeSheet_CS)

## Implementation plan check — round 1 — 2026-10-15 18:04

Implemented by a ClaudeSheet_CS subagent in 6 `applyModelApiCode` saves (a first subagent was stopped and its work lost in a Service Studio restart). Verified by the main session against the mutated module, then merged with `omlMerge` (`merged: true`). Live after merge: same inventory as validated (only list order differs), 0 validation errors, flow-layout report shows only the accepted carry-overs.

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P01 | Confirm inventory; both modules open | Approved | listApps; live ClaudeSheet matches the story | Yes |
| P02 | Site properties, role SheetUser (public), DaySheetValidation (public) | Rejected | DaySheetValidation not Public: no Public property for user exceptions in the Model API; set by hand in Service Studio before publishing CS | Yes |
| P03 | Static entities DayStatus, CalendarDayType | Approved | Same records, identifiers and Ids; Public, Read Only | Yes |
| P04 | 6 entities, public and read-only | Approved | Public + ExposeReadOnly; attribute/index/delete-rule dump identical | Yes |
| P05 | 12 structures, public | Approved | All 12 Public, attributes identical | Yes |
| P06 | 12 server actions + Settings_GetDailyExpectedMinutes | Approved | 13 actions, 9 Public (those ClaudeSheet calls + Settings), logic diffed identical | Yes |
| P07 | Timer_Bootstrap | Approved | Runs Bootstrap, WhenPublished | Yes |
| P08 | Gate; plan check, merge, review, publish | Rejected | Gate passes; review and publish follow through their own skills | Yes |

No "No" answers.

Outcome: all steps approved. P02 and P08 accepted despite the Rejected tag. Before publishing CS: set DaySheetValidation Public = Yes in Service Studio.

Plan check: approved

## Review — round 1 — 2026-10-15 18:12 (producer part)

Plan check approved earlier the same day (P02, P08 accepted despite Rejected tag). Changes merged, not published. DaySheetValidation set Public by hand in Service Studio (the Model API can't read or set that flag); the user confirmed it (A04).

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC-TT4.1 CS contents | Approved | Live inventory: 2 static, 6 entities, 12 structures, 13 actions, timer, role, exception, 2 site properties | Yes |
| A02 | AC-TT4.2 ClaudeSheet without backend | Rejected | Not yet: consumer part (P10–P11) | Yes |
| A03 | AC-TT4.3 References direction | Approved | Producer part: CS references only (System) | Yes |
| A04 | AC-TT4.4 Public / Read Only | Approved | Entities Public + ExposeReadOnly; structures, used actions, role public; DaySheetValidation confirmed by the user | Yes |
| A05 | AC-TT4.5 0 errors, layout | Approved | Producer part: 0 errors, accepted carry-overs only | Yes |
| A06 | AC-TT4.6 Seeding after publish | Approved | Timer + Bootstrap present; verify at runtime | Yes |
| A07 | AC-TT4.7 Blocks behave as before | Rejected | Not yet: consumer part | Yes |
| A08 | AC-TT4.8 Role exception | Approved | Role gate in every CS action; verify at runtime | Yes |
| A09 | AC-TT4.9 DaySheetValidation message | Approved | Producer part: raised by DaySheet_Save; handler rebound in P10 | Yes |
| A10 | AC-TT4.10 Snapshots and docs | Rejected | Not yet: after publishes (P14) | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01–C02 | Created | Data › Database › DayStatus, CalendarDayType (static) | New | Yes |
| C03–C08 | Created | Data › Database › Client, Project, ProjectAssignment, Holiday, DaySheet, TimeEntry | New | Yes |
| C09–C20 | Created | Data › Structures › the 12 structures | New | Yes |
| C21–C22 | Created | Data › Site Properties › DailyExpectedMinutes, SeedTestUsernames | New | Yes |
| C23–C35 | Created | Logic › Server Actions › the 12 copied actions + Settings_GetDailyExpectedMinutes | New | Yes |
| C36 | Created | Logic › Roles › SheetUser | New | Yes |
| C37 | Created | Logic › Exceptions › DaySheetValidation | New | Yes |
| C38 | Created | Processes › Timers › Timer_Bootstrap | New | Yes |

### Conventions

| ID | Item | Tag | Answer |
|---|---|---|---|
| N01 | Names, placement, role checks | Approved | Yes |
| N02 | DaySheet_FillPending crossing (carried over, justified) | Rejected | Yes |
| N03 | DaySheet_Save merge (carried over, justified) | Rejected | Yes |
| N04 | Bootstrap merges (carried over, justified) | Rejected | Yes |

No "No" answers. A04 was missing in the first submission and answered separately. Accepted despite Rejected tag: A02, A07, A10, N02, N03, N04.

Outcome: all rows approved in round 1 (producer part). Next: publish ClaudeSheet_CS, then the dependency checklist for ClaudeSheet.
