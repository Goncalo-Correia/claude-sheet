# Review: ClaudeSheet · US6 (manage clients, screens)

## Implementation plan check — round 1 — 2026-10-19 13:36

ClaudeSheet_CS part published 2026-10-19 13:13 (manual). Dependencies ticked by the user; references verified (no suffix). Implemented in 1 `applyModelApiCode` save (after two dry runs). The `omlMerge` call timed out on Claude's side after 300 s; the merge completed in Service Studio (the mutated OML was consumed and `omlReset` showed no chain in flight). Live after merge: identical to the validated state; 0 validation messages.

| ID | Step | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| P03 | Screen Admin › Clients | Approved | GetClients (Client_GetList), search + show inactive (OnChange_Filters), New client, table with empty state; verify at runtime | Yes |
| P04 | Screen Admin › ClientDetail; Save catches ClientValidation | Rejected | Screen complete, but Save and Activate/Deactivate catch AllExceptions (the Model API can't reference the producer's ClientValidation); the same message is shown above the form | Yes |
| P05 | Menu link "Clients", visible to SheetAdmin | Rejected | ClientsLink exists; Visible = GetMenuAccess.IsSheetAdmin (new Menu server data action), because referenced roles have no client-side check | Yes |

No reasons (no row answered No).

Outcome: all approved; P04 and P05 accepted despite Rejected tag (platform limits, documented in implementation notes and docs/conventions.md).

Plan check: approved

## Review — round 1 — 2026-10-19 13:41

Plan check approved 2026-10-19 (P04, P05 accepted despite Rejected tag). Live: ClaudeSheet 0 validation messages; flow layout of the 8 new flows: 0 diagonals, 0 crossings, 0 overlaps.

### Acceptance criteria

| ID | Criterion | Tag | Reason / evidence | Answer |
|---|---|---|---|---|
| A01 | AC6.1 Active clients listed with project count | Approved | Clients › GetClients → Client_GetList; ClientList shows Name, ProjectCount, Active; verify at runtime | Yes |
| A02 | AC6.2 Duplicate name rejected with message | Approved | OnClick_SaveButton → Client_CreateOrUpdate; AllExceptions → FormError; verify at runtime | Yes |
| A03 | AC6.3 Deactivated client's projects hidden from day editors | Approved | OnClick_ToggleActiveButton → Client_SetActive; Project_GetAssignedToUser filters Client.IsActive; verify at runtime | Yes |
| A04 | AC6.4 Non-admins refused | Approved | Both screens SheetAdmin only; ClientsLink hidden; CS actions check the role; verify at runtime | Yes |
| A05 | AC6.5 0 errors, 0 warnings | Approved | ClaudeSheet and ClaudeSheet_CS clean | Yes |

### Changes

| ID | Type | Path | Status | Answer |
|---|---|---|---|---|
| C01 | Modified | Module properties › Dependencies › ClaudeSheet_CS (4 actions, ClientSummary, ClientValidation; added by the user) | New | Yes |
| C02 | Created | Interface › UI Flows › Admin › Clients | New | Yes |
| C03 | Created | Interface › UI Flows › Admin › ClientDetail | New | Yes |
| C04 | Modified | Interface › UI Flows › Common › Menu (GetMenuAccess, ClientsLink) | New | Yes |

### Conventions

| ID | Check | Tag | Answer |
|---|---|---|---|
| N01 | No convention violations (names, placement, screen roles, role-gated menu link, AllExceptions catch, screen CSS, flow layout) | Approved | Yes |

No reasons (no row answered No).

Outcome: all approved in round 1. Next: publish ClaudeSheet.
