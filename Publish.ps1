<#
.SYNOPSIS
    Publishes the MicrosoftAzure.Api package to NuGet.org.

.DESCRIPTION
    This script performs the following steps:
    1. Checks for clean git working directory (porcelain)
    2. Determines the Nerdbank GitVersioning version
    3. Validates nuget-key.txt exists, has content, and is gitignored
    4. Runs unit tests (unless -SkipTests is specified)
    5. Publishes to NuGet.org

.PARAMETER SkipTests
    Skip running unit tests.

.EXAMPLE
    .\Publish.ps1
    .\Publish.ps1 -SkipTests
#>

param(
    [switch]$SkipTests
)

$ErrorActionPreference = 'Stop'

function Write-Step {
    param([string]$Message)
    Write-Host "`n=== $Message ===" -ForegroundColor Cyan
}

function Exit-WithError {
    param([string]$Message)
    Write-Host "ERROR: $Message" -ForegroundColor Red
    exit 1
}

# Get script directory (solution root)
$solutionRoot = $PSScriptRoot
Set-Location $solutionRoot

# Step 1: Check for git porcelain (clean working directory)
Write-Step "Checking git working directory status"
$gitStatus = git status --porcelain
if ($gitStatus) {
    Exit-WithError "Git working directory is not clean. Please commit or stash your changes.`n$gitStatus"
}
Write-Host "Git working directory is clean." -ForegroundColor Green

# Step 2: Determine the Nerdbank git version
Write-Step "Determining Nerdbank GitVersioning version"
$nbgvOutput = nbgv get-version --format json 2>&1
if ($LASTEXITCODE -ne 0) {
    Exit-WithError "Failed to get Nerdbank GitVersioning version. Ensure nbgv tool is installed.`n$nbgvOutput"
}
$versionInfo = $nbgvOutput | ConvertFrom-Json
$nugetVersion = $versionInfo.NuGetPackageVersion
if (-not $nugetVersion) {
    Exit-WithError "Failed to determine NuGet package version from Nerdbank GitVersioning."
}
Write-Host "Version: $nugetVersion" -ForegroundColor Green

# Step 3: Check that nuget-key.txt exists, has content, and is gitignored
Write-Step "Validating nuget-key.txt"
$nugetKeyPath = Join-Path $solutionRoot "nuget-key.txt"

if (-not (Test-Path $nugetKeyPath)) {
    Exit-WithError "nuget-key.txt not found at: $nugetKeyPath"
}

$nugetKey = (Get-Content $nugetKeyPath -Raw).Trim()
if ([string]::IsNullOrWhiteSpace($nugetKey)) {
    Exit-WithError "nuget-key.txt is empty."
}

# Check if nuget-key.txt is gitignored
$gitCheckIgnore = git check-ignore "nuget-key.txt" 2>&1
if ($LASTEXITCODE -ne 0) {
    Exit-WithError "nuget-key.txt is not in .gitignore. Add it to protect your API key."
}
Write-Host "nuget-key.txt is valid and gitignored." -ForegroundColor Green

# Step 4: Run unit tests (unless -SkipTests is specified)
if (-not $SkipTests) {
    Write-Step "Running unit tests"
    dotnet test --configuration Release --no-build 2>&1
    if ($LASTEXITCODE -ne 0) {
        # Try with build
        dotnet test --configuration Release 2>&1
        if ($LASTEXITCODE -ne 0) {
            Exit-WithError "Unit tests failed."
        }
    }
    Write-Host "All tests passed." -ForegroundColor Green
}
else {
    Write-Step "Skipping unit tests (-SkipTests specified)"
}

# Step 5: Build and pack the project
Write-Step "Building and packing the project"
$projectPath = Join-Path $solutionRoot "MicrosoftAzure.Api\MicrosoftAzure.Api.csproj"
dotnet pack $projectPath --configuration Release --output "$solutionRoot\artifacts" 2>&1
if ($LASTEXITCODE -ne 0) {
    Exit-WithError "Failed to pack the project."
}
Write-Host "Package created successfully." -ForegroundColor Green

# Step 6: Publish to NuGet.org
Write-Step "Publishing to NuGet.org"
$packagePath = Join-Path $solutionRoot "artifacts\MicrosoftAzure.Api.$nugetVersion.nupkg"
if (-not (Test-Path $packagePath)) {
    # Try to find the package
    $packagePath = Get-ChildItem -Path "$solutionRoot\artifacts" -Filter "MicrosoftAzure.Api.*.nupkg" | 
        Where-Object { $_.Name -notlike "*.symbols.*" } |
        Sort-Object LastWriteTime -Descending | 
        Select-Object -First 1 -ExpandProperty FullName
    if (-not $packagePath) {
        Exit-WithError "Could not find NuGet package in artifacts folder."
    }
}

Write-Host "Publishing package: $packagePath"
dotnet nuget push $packagePath --api-key $nugetKey --source "https://api.nuget.org/v3/index.json" --skip-duplicate 2>&1
if ($LASTEXITCODE -ne 0) {
    Exit-WithError "Failed to publish package to NuGet.org."
}

Write-Host "`n=== Successfully published MicrosoftAzure.Api v$nugetVersion to NuGet.org ===" -ForegroundColor Green
exit 0
