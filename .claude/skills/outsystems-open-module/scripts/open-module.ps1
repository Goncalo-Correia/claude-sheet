# Asks Service Studio to download a module from the server and open it.
#
#   powershell -File open-module.ps1 -Name <Module> [-Name <Module> ...]
#   powershell -File open-module.ps1 -Key <eSpaceKey GUID> [-ServerHost <host>]
#   powershell -File open-module.ps1 -Decode <compact key from eSpace.GlobalKey.ModuleKey>
#
# Names and the server host come from the project's .claude/outsystems-modules.json (run from the
# repository root, or pass -Config). -ServerHost overrides the host. The script only launches the
# servicestudio:// link; checking that the module is open is done with the MCP listApps tool.
param(
  [string[]]$Name,
  [string]$Key,
  [string]$Decode,
  [string]$ServerHost,
  [string]$Config = (Join-Path (Get-Location) '.claude\outsystems-modules.json'),
  [int]$DelaySeconds = 8
)

$ErrorActionPreference = 'Stop'

if ($Decode) {
  $b64 = $Decode.Replace('_', '/')
  while ($b64.Length % 4) { $b64 += '=' }
  (New-Object guid (,[Convert]::FromBase64String($b64))).ToString()
  return
}

$cfg = $null
if (Test-Path $Config) { $cfg = Get-Content -Raw $Config | ConvertFrom-Json }
if (-not $ServerHost -and $cfg) { $ServerHost = $cfg.host }
if (-not $ServerHost) { throw "No server host: pass -ServerHost <host> or create $Config (see SKILL.md, step 1)." }
# The host goes into a URL handed to Windows: accept a plain host name only.
if ($ServerHost -notmatch '^[A-Za-z0-9.-]+(:\d+)?$') { throw "Invalid server host: $ServerHost" }

$targets = @()
if ($Key) { $targets += [pscustomobject]@{ Name = $Key; Key = $Key } }
foreach ($n in $Name) {
  $k = if ($cfg) { $cfg.modules.$n } else { $null }
  if (-not $k) { throw "'$n' is not in $Config. Get its key first (see SKILL.md, step 2)." }
  $targets += [pscustomobject]@{ Name = $n; Key = $k }
}
if (-not $targets) { throw 'Pass -Name <Module> (one or more), -Key <GUID> or -Decode <compact key>.' }

$guid = [guid]::Empty
for ($i = 0; $i -lt $targets.Count; $i++) {
  $t = $targets[$i]
  # The key goes into a URL handed to Windows: accept nothing but a GUID.
  if (-not [guid]::TryParse($t.Key, [ref]$guid)) { throw "Invalid eSpaceKey for $($t.Name): $($t.Key)" }
  $url = "servicestudio://-downloadAndOpen%20-eSpaceKey%20$guid%20-url%20$([uri]::EscapeDataString($ServerHost))"
  Start-Process $url
  "open-module: asked Service Studio to open $($t.Name) ($guid) from $ServerHost"
  if ($i -lt $targets.Count - 1) { Start-Sleep -Seconds $DelaySeconds }
}
