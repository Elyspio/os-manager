param (
    [Switch]$nobuild
)


$origin = Get-Location

cd $PSScriptRoot/../../

if(! $nobuild.IsPresent) {
    npm run build
}
scp lib/lib.zip pi@elyspi:/home/pi
ssh pi@elyspi "sudo rm -rf lib && unzip lib -d lib && chmod -R  755 lib/register && lib/register/linux/server.sh && rm -rf ~/lib ~/lib.zip"

cd $origin
