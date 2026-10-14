# 0002. Seed test users from a site property; allow several assignment periods per project

- **Date:** 2026-10-14
- **Status:** Accepted
- **Story:** US-000

## Context

US-000 seeds reference data with a bootstrap timer and grants the SheetUser role plus a project assignment to "the test users", without naming them. There are no back-office screens yet to manage users or assignments.

The implementation guidelines also asked for a unique index on `ProjectAssignment (UserId, ProjectId)`. During review the reviewer rejected it: a user can be assigned to the same project in separate periods, for example January, then March to December.

## Decision

1. Test users are configured in the site property `SeedTestUsernames`, a comma-separated list of usernames that is empty by default. `Bootstrap` (run by `Timer_Bootstrap` when the module is published) grants SheetUser and creates the RUBUS assignment only for active users in that list, and only when they don't already have them.
2. `ProjectAssignment` is unique on `(UserId, ProjectId, StartDate)` (`UQ_PA_UserProjectStart`) instead of `(UserId, ProjectId)`.

## Consequences

- No usernames are hard-coded, and other environments stay clean while the site property is empty. In each test environment, someone has to set the property in Service Center and run the timer again.
- A user can hold several periods on the same project. The database doesn't prevent overlapping periods, so the future back-office action that saves assignments must reject overlaps. Otherwise `GetUserProjects` could return the same project twice for the overlapping dates.
- `Bootstrap` creates the RUBUS assignment only when the user has none for RUBUS, so it never adds periods on later runs.
