<#
  ____    _    _  __   ____  _          _ _ 
 / __ \  / \  | |/ /  / ___|| |__   ___| | |
| |  | |/ _ \ | ' /   \___ \| '_ \ / _ \ | |
| |  | / ___ \| . \    ___) | | | |  __/ | |
 \____/_/   \_\_|\_\  |____/|_| |_|\___|_|_|

 Installs the defualt Visual Studio release build directory in the user PATH variable.
#>

$CurrentDirectory = (Get-Location).Path
$ReleaseDirectory = "$CurrentDirectory\bin\Release\net10.0"

$CurrentUserPath = [Environment]::GetEnvironmentVariable("Path", "User")
if ($CurrentUserPath -notlike "*$ReleaseDirectory*") {
    $NewUserPath = "$CurrentUserPath;$ReleaseDirectory"
    [Environment]::SetEnvironmentVariable("Path", $NewUserPath, "User")
    Write-Host "Successfully added $ReleaseDirectory to user PATH variable"
} else {
    Write-Host "Path $ReleaseDirectory already exists in the user PATH variable"
}