# Asks Service Studio to download a module from the server and open it.
#
#   powershell -File open-module.ps1 -Name ClaudeSheet_CS [-Name ClaudeSheet ...]
#   powershell -File open-module.ps1 -Key <eSpaceKey GUID>
#   powershell -File open-module.ps1 -Decode <compact key from eSpace.GlobalKey.ModuleKey>
#
# Names resolve through ../modules.json (host + eSpaceKeys). The script only launches the
# servicestudio:// link; checking that the module is open is done with the MCP listApps tool.
param(
  [string[]]$Name,
  [string]$Key,
  [string]$Decode,
  [int]$DelaySeconds = 8
)

$ErrorActionPreference = 'Stop'

if ($Decode) {
  $b64 = $Decode.Replace('_', '/')
  while ($b64.Length % 4) { $b64 += '=' }
  (New-Object guid (,[Convert]::FromBase64String($b64))).ToString()
  return
}

$config = Get-Content -Raw (Join-Path $PSScriptRoot '..\modules.json') | ConvertFrom-Json

$targets = @()
if ($Key) { $targets += [pscustomobject]@{ Name = $Key; Key = $Key } }
foreach ($n in $Name) {
  $k = $config.modules.$n
  if (-not $k) { throw "'$n' is not in modules.json. Get its key first (see SKILL.md, step 2)." }
  $targets += [pscustomobject]@{ Name = $n; Key = $k }
}
if (-not $targets) { throw 'Pass -Name <Module> (one or more), -Key <GUID> or -Decode <compact key>.' }

$guid = [guid]::Empty
for ($i = 0; $i -lt $targets.Count; $i++) {
  $t = $targets[$i]
  # The key goes into a URL handed to Windows: accept nothing but a GUID.
  if (-not [guid]::TryParse($t.Key, [ref]$guid)) { throw "Invalid eSpaceKey for $($t.Name): $($t.Key)" }
  $url = "servicestudio://-downloadAndOpen%20-eSpaceKey%20$guid%20-url%20$([uri]::EscapeDataString($config.host))"
  Start-Process $url
  "open-module: asked Service Studio to open $($t.Name) ($guid)"
  if ($i -lt $targets.Count - 1) { Start-Sleep -Seconds $DelaySeconds }
}
