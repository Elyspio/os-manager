

$origin = Get-Location

cd $PSScriptRoot/../../
npm run build


Invoke-Expression "$PSScriptRoot/aero-oled.client.ps1 -nobuild"
Invoke-Expression "$PSScriptRoot/elyspi.server.ps1 -nobuild"
Invoke-Expression "$PSScriptRoot/elyspi.client.ps1 -nobuild"

cd $origin
