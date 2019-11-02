# Grab the latest release from GitHub, and stuff its various .dlls and .sos and .dylibs into the right places.
# Also, update the version number in the .csproj.

$fileNameToFolderName = @{
    # Zipfile name = ("destination folder name", "library file name inside zip file")
    #Androids
    "aarch64-linux-android"    = @("android-arm64", "libscannit_core_ffi.so");
    "arm-linux-androideabi"    = @("android-arm", "libscannit_core_ffi.so");
    #Windowses
    "x86_64-pc-windows-msvc"   = @("win-x64", "scannit_core_ffi.dll");
    "i686-pc-windows-msvc"     = @("win-x86", "scannit_core_ffi.dll");
    #OSX
    "x86_64-apple-darwin"      = @("osx-x64", "libscannit_core_ffi.dylib");
    #Linuxes
    "x86_64-unknown-linux-gnu" = @("linux-x64", "libscannit_core_ffi.so");
    "i686-unknown-linux-gnu"   = @("linux-x86", "libscannit_core_ffi.so");
};

# Get latest release from GitHub:
$scratchFolder = New-Item -ItemType Directory "_latestDeps" -Force
$githubResponse = Invoke-RestMethod https://api.github.com/repos/pingzing/scannit-core/releases/latest;
if (-not($githubResponse)) {
    Write-Error "Failed to get a valid response from GitHub. Bailing.";
}

# Parse out the version number for later
$versionString = $githubResponse.tag_name.Substring(1);

foreach ($asset in $githubResponse.assets) {
    $outFile = "$($scratchFolder.FullName)/$($asset.name)";
    Invoke-WebRequest -Uri $asset.browser_download_url -OutFile $outFile; # This never returns anything to inspect. What the hell.
    # Get the parts filename that'll map to the hashtable up above. That's everything after
    # 'scannit-core_versionNumber_' and before '.zip'.
    [string]$zipFileName = $asset.name;
    [int]$firstUnderscoreIndex = $zipFileName.IndexOf('_');
    [int]$secondUnderscoreIndex = $zipFileName.Substring($firstUnderscoreIndex + 1).IndexOf('_');
    [int]$beginIndex = $firstUnderscoreIndex + $secondUnderscoreIndex + 2;
    [int]$lengthToSubstring = $zipFileName.Length - ($zipFileName.Length - $zipFileName.IndexOf('.')) - $beginIndex;
    [string]$sanitizedFileName = $zipFileName.Substring($beginIndex, $lengthToSubstring);
    [string]$nativeFolderName = $fileNameToFolderName[$sanitizedFileName][0];
    [string]$fileNameToExtract = $fileNameToFolderName[$sanitizedFileName][1];
    [string]$nativeFileExtension = $fileNameToExtract.Substring($fileNameToExtract.IndexOf('.'));
    
    # This is (supposed to be) idempotent.
    Add-Type -Assembly System.IO.Compression.FileSystem;
    [System.IO.Compression.ZipArchive]$zipFile;
    [string]$destPath = "$($PSScriptRoot)/runtimes/$($nativeFolderName)/native/scannit_core_ffi$($nativeFileExtension)";
    try {
        $zipFile = [IO.Compression.ZipFile]::OpenRead("$($scratchFolder.FullName)/$($zipFileName)");
        $zipFile.Entries | 
            Where-Object { $_.Name -eq $fileNameToExtract } | 
            ForEach-Object { 
                [System.IO.Compression.ZipFileExtensions]::ExtractToFile($_, $destPath, $true);
            };
    }
    catch {
        Write-Error("Failed to open the zip file: $($_.Exception.Message)");
    }
    finally {
        $zipFile.Dispose();
        Write-Host "Extracted file to $destPath";
    }
}

# TODO: Update csproj version number with whatever we pulled out of GitHub.
[string]$versionReplaceRegex = "<Version>.+<`/Version>";
[string]$csprojContent = Get-Content "$($PSScriptRoot)/ScannitSharp.csproj" -Raw;
$csprojContent = $csprojContent -replace $versionReplaceRegex, "<Version>$($versionString)</Version>";
Set-Content "$($PSScriptRoot)/ScannitSharp.csproj" $csprojContent;
Write-Host "Version of ScannitSharp.csproj set to $($versionString)";

Write-Host "Cleaning up _latestDeps folder..."
Remove-Item "_latestDeps" -Recurse -Force;
