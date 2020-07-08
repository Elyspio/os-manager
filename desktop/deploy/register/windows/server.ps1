Param(
    [Parameter(Mandatory=$false)]
    [Switch]$remove
)

if($remove -eq $true) {
    Invoke-Expression "$PSScriptRoot/common.ps1 -mode server -remove"
}
else {
    Invoke-Expression "$PSScriptRoot/common.ps1 -mode server"

}
