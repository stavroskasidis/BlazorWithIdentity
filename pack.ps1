param(
    [string] $VersionSuffix
)
Push-Location $PSScriptRoot

function Write-Message{
    param([string]$message)
    Write-Host
    Write-Host $message
    Write-Host
}
function Confirm-PreviousCommand {
    param([string]$errorMessage)
    if ( $LASTEXITCODE -ne 0) { 
        if( $errorMessage) {
            Write-Message $errorMessage
        }    
        exit $LASTEXITCODE 
    }
}

function Confirm-Process {
    param ([System.Diagnostics.Process]$process,[string]$errorMessage)
    $process.WaitForExit()
    if($process.ExitCode -ne 0){
        Write-Host $process.ExitCode
        if( $errorMessage) {
            Write-Message $errorMessage
        }    
        exit $process.ExitCode 
    }
}

Write-Host "Parameters"
Write-Host "=========="
Write-Host "Version suffix: $VersionSuffix"

# Check prerequisites
$proc = Start-Process "dotnet" -ArgumentList "--version" -PassThru
Confirm-Process $proc "Could not find dotnet sdk, please install and run again ..."

Write-Message "Cleaning artifacts folder"
Remove-Item -Path artifacts/*.nupkg -Recurse
Confirm-PreviousCommand

Write-Message "Packing ..."
dotnet pack templatepack.csproj -c Release -o artifacts /p:VersionSuffix="$VersionSuffix"
Confirm-PreviousCommand

Write-Message "Pack completed successfully"