# Closes a module's tab in Service Studio through Windows UI Automation.
#
#   powershell -File close-module.ps1 -Name <Module>[,<Module> ...]
#
# Each open module is a TabItem in Service Studio's window; its header holds a TextBlock with
# "<Module>" or "<Module> (<App>)", prefixed with "* " when there are unsaved changes, and a
# close Button. A tab with unsaved changes is never closed: that work is the user's.
# Checking that the module is gone is done with the MCP listApps tool.
param([Parameter(Mandatory)][string[]]$Name)

$ErrorActionPreference = 'Stop'
# With powershell -File, "-Name A,B" arrives as one string.
$Name = $Name | ForEach-Object { $_ -split ',' } | ForEach-Object { $_.Trim() } | Where-Object { $_ }
Add-Type -AssemblyName UIAutomationClient, UIAutomationTypes
$AE = [Windows.Automation.AutomationElement]
function Cond($prop, $value) { New-Object Windows.Automation.PropertyCondition($prop, $value) }

$procs = Get-Process -Name ServiceStudio -ErrorAction SilentlyContinue
if (-not $procs) { throw 'Service Studio is not running.' }

$tabs = foreach ($p in $procs) {
  foreach ($w in $AE::RootElement.FindAll([Windows.Automation.TreeScope]::Children, (Cond $AE::ProcessIdProperty $p.Id))) {
    $w.FindAll([Windows.Automation.TreeScope]::Descendants, (Cond $AE::ControlTypeProperty ([Windows.Automation.ControlType]::TabItem)))
  }
}

foreach ($n in $Name) {
  $found = $false
  foreach ($t in $tabs) {
    $txt = $t.FindFirst([Windows.Automation.TreeScope]::Descendants, (Cond $AE::ControlTypeProperty ([Windows.Automation.ControlType]::Text)))
    if (-not $txt) { continue }
    $header = $txt.Current.Name
    $unsaved = $header.StartsWith('* ')
    $label = $header -replace '^\* ', ''
    if ($label -ne $n -and -not $label.StartsWith("$n (")) { continue }
    $found = $true
    if ($unsaved) { "close-module: NOT closing $n - it has unsaved changes ('$header')"; break }
    $btn = $t.FindFirst([Windows.Automation.TreeScope]::Descendants, (Cond $AE::ControlTypeProperty ([Windows.Automation.ControlType]::Button)))
    if (-not $btn) { throw "No close button found on the $n tab." }
    $btn.GetCurrentPattern([Windows.Automation.InvokePattern]::Pattern).Invoke()
    "close-module: closed $n"
    break
  }
  if (-not $found) { "close-module: $n is not open" }
}
