# Asks Service Studio to download a module from the server and open it.
#
#   powershell -File open-module.ps1 -Name <Producer>[,<Consumer> ...]
#   powershell -File open-module.ps1 -Name <Producer> -SameServerAs <OpenConsumer>
#   powershell -File open-module.ps1 -Key <eSpaceKey GUID> [-ServerHost <host>]
#   powershell -File open-module.ps1 -HostOf <Module>
#   powershell -File open-module.ps1 -Decode <compact key from eSpace.GlobalKey.ModuleKey>
#
# Names resolve through the project's .claude/outsystems-modules.json (run from the repository
# root, or pass -Config): eSpaceKeys, a default host and an optional host per module. The server
# is, in order: -ServerHost, the host of -SameServerAs (from Service Studio's Settings.xml), the
# module's own host in the file, then the file's default host. The script only launches the
# servicestudio:// link; checking that the module is open is done with the MCP listApps tool.
param(
  [string[]]$Name,
  [string]$Key,
  [Alias('Server')][string]$ServerHost,
  [string]$SameServerAs,
  [string]$HostOf,
  [string]$Decode,
  [string]$Config = (Join-Path (Get-Location) '.claude\outsystems-modules.json'),
  [int]$DelaySeconds = 8
)

$ErrorActionPreference = 'Stop'

# Service Studio records every recently opened module with its server and eSpaceKey.
function Get-RecentModuleHost([string]$module) {
  $files = Get-ChildItem "$env:LOCALAPPDATA\OutSystems" -Directory -Filter 'ServiceStudio 11*' -ErrorAction SilentlyContinue |
    ForEach-Object { Join-Path $_.FullName 'Settings.xml' } | Where-Object { Test-Path $_ }
  $hits = foreach ($f in $files) {
    ([xml](Get-Content -Raw $f)).SelectNodes('//RecentItemInfo') | Where-Object { $_.ESpace -eq $module } |
      ForEach-Object { [pscustomobject]@{ Host = $_.HostInfo.HostName; Date = [datetime]$_.LastOpenedDate } }
  }
  $best = $hits | Sort-Object Date -Descending | Select-Object -First 1
  if (-not $best) { throw "'$module' is not in Service Studio's recent modules; pass -ServerHost <host>." }
  $best.Host
}

if ($Decode) {
  $b64 = $Decode.Replace('_', '/')
  while ($b64.Length % 4) { $b64 += '=' }
  (New-Object guid (,[Convert]::FromBase64String($b64))).ToString()
  return
}

if ($HostOf) { Get-RecentModuleHost $HostOf; return }

# With powershell -File, "-Name A,B" arrives as one string.
$Name = $Name | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim() } | Where-Object { $_ }

$cfg = $null
if (Test-Path $Config) { $cfg = Get-Content -Raw $Config | ConvertFrom-Json }
if ($SameServerAs -and -not $ServerHost) { $ServerHost = Get-RecentModuleHost $SameServerAs }

$targets = @()
if ($Key) { $targets += [pscustomobject]@{ Name = $Key; Key = $Key; Host = $null } }
foreach ($n in $Name) {
  $entry = if ($cfg) { $cfg.modules.$n } else { $null }
  if (-not $entry) { throw "'$n' is not in $Config. Get its key first (see SKILL.md, step 3)." }
  # An entry is either "<GUID>" (default host) or { "key": "<GUID>", "host": "<host>" }.
  if ($entry -is [string]) { $targets += [pscustomobject]@{ Name = $n; Key = $entry; Host = $null } }
  else { $targets += [pscustomobject]@{ Name = $n; Key = $entry.key; Host = $entry.host } }
}
if (-not $targets) { throw 'Pass -Name <Module> (one or more), -Key <GUID>, -HostOf <Module> or -Decode <compact key>.' }

$guid = [guid]::Empty
for ($i = 0; $i -lt $targets.Count; $i++) {
  $t = $targets[$i]
  # The key goes into a URL handed to Windows: accept nothing but a GUID.
  if (-not [guid]::TryParse($t.Key, [ref]$guid)) { throw "Invalid eSpaceKey for $($t.Name): $($t.Key)" }
  $h = if ($ServerHost) { $ServerHost } elseif ($t.Host) { $t.Host } elseif ($cfg) { $cfg.host } else { $null }
  if (-not $h) { throw "No server host for $($t.Name): pass -ServerHost <host> or create $Config (see SKILL.md, step 1)." }
  # The host goes into the same URL: accept a plain host name only.
  if ($h -notmatch '^[A-Za-z0-9.-]+(:\d+)?$') { throw "Invalid host for $($t.Name): $h" }
  $url = "servicestudio://-downloadAndOpen%20-eSpaceKey%20$guid%20-url%20$([uri]::EscapeDataString($h))"
  Start-Process $url
  "open-module: asked Service Studio to open $($t.Name) ($guid) from $h"
  if ($i -lt $targets.Count - 1) { Start-Sleep -Seconds $DelaySeconds }
}
