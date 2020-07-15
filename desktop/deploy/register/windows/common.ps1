Param(
    [Parameter(Mandatory = $true)]
    $mode,

    [Parameter(Mandatory = $false)]
    [Switch]$remove
)

$scriptPath = $PSScriptRoot

$const = (Get-Content "$([System.IO.Path]::Combine($scriptPath, "..", "const.json") )" | ConvertFrom-Json)
$appPath = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($scriptPath, "..", ".."))

$service = $const.$mode
Write-Output "mode $mode "
Write-Output "app path $appPath $service"

if ($remove -eq $false)
{

    Invoke-Expression "$( $scriptPath )/nssm.exe install $service $([System.IO.Path]::Combine($appPath, "node.exe") ) $([System.IO.Path]::Combine($appPath, $mode, "index.js") )"
    Invoke-Expression "$( $scriptPath )/nssm.exe set $service Start SERVICE_AUTO_START"
    Invoke-Expression "$( $scriptPath )/nssm.exe set $service AppDirectory $([System.IO.Path]::Combine($appPath, $mode) )"
    Invoke-Expression "$( $scriptPath )/nssm.exe set $service DisplayName Android Windows Link - $mode"
    Invoke-Expression "$( $scriptPath )/nssm.exe set $service AppEnvironmentExtra NODE_ENV=production"
    Invoke-Expression "$( $scriptPath )/nssm.exe start $service"

}
else
{
    Invoke-Expression "$( $scriptPath )/nssm.exe stop $service confirm"
    Invoke-Expression "$( $scriptPath )/nssm.exe remove $service confirm"
}

