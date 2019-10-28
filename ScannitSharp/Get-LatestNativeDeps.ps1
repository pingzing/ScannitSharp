$fileNameToFolderName = @{
    #Androids
    "aarch64-linux-android"    = "android-arm64";
    "arm-linux-androideabi"    = "android-arm";
    #Windowses
    "x86_64-pc-windows-msvc"   = "win-x64";
    "i686-pc-windows-msvc"     = "win-x86";
    #OSX
    "x86_64-apple-darwin"      = "osx-64";
    #Linuxes
    "x86_64-unknown-linux-gnu" = "linux-x64";
    "i686-unknown-linux-gnu"   = "linux-x86";
};

# Grab the latest release from GitHub, and stuff its various .dlls and .sos and dylibs into the right places.
# Also, update the version number in the .csproj.

# Get latest release from GitHub:
$scratchFolder = New-Item -ItemType Directory "_latestDeps"
$githubResponse = Invoke-RestMethod https://api.github.com/repos/pingzing/scannit-core/releases/latest;
if (-not($githubResponse)) {
    Write-Error "Failed to get a valid response from GitHub. Bailing.";
}

# Parse out the version number for later
$versionString = $githubResponse.tag_name.Substring(1);

foreach ($asset in $githubResponse.assets) {
    $result = Invoke-WebRequest $asset.browser_download_url -OutFile "$($scratchFolder.FullName)/$($asset.name)";
    if ( -not($result -AND $result.StatusCode -eq 200 )) {
        Write-Error "Failed to download $($asset.name)";
    }
}
