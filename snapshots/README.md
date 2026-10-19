# Snapshots

The state of each module as of its last publish, one Markdown file per element. Each file has a short header and the read result in one block: Model API code (`csharp`) or a name list (`json`). Lines that only set a property to its default value (`// default value`) are left out, so the files are about half the size and diffs show only real changes. Refreshed after every publish by the `outsystems-mcp-publish` skill, which can also refresh them on request.

```
snapshots/<Module>/
  README.md              overview: type, version, counts, last refresh
  data-model.md          getDataModel (includes entities from references)
  structures.md          getStructures
  settings.md            getSettings (site properties)
  names/*.md             every get…Names list, sorted
  server-actions/*.md    getServerAction, one file per action
  client-actions/*.md    getClientAction
  screens/*.md           getScreen / getScreenTraditional
  web-blocks/*.md        getWebBlock / getWebBlockTraditional
  roles/*.md             getRole
  …
```

To see what changed in a module between publishes:

```bash
git log -p -- snapshots/<Module>/
```
