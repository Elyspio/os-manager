$origin = Get-Location

cd $PSScriptRoot/../../
npm run build

Invoke-Expression  "./lib/register/windows/client.ps1 -remove"

rm -R -Force S:/apps/android-link/
Expand-Archive ./lib/lib.zip  -DestinationPath S:/apps/android-link/
Invoke-Expression "S:/apps/android-link/register/windows/client.ps1"

cd $origin

