$projects = "ChessOk.ModelFramework"
$nugetExe = Resolve-Path .nuget\nuget.exe
    
Remove-Item build\*.nupkg

foreach($project in $projects)
{
    Write-Host "Packing project «$project»."
    Set-Location -Path "$project"

    & $nugetExe pack $project.csproj -Build -Symbols -Properties Configuration=Release

    Write-Host "Moving nuget package to the build folder"
    Move-Item .\*.nupkg ..\build\ -Force

    Set-Location ..
}

$packages = Get-ChildItem build\*.nupkg
foreach($package in $packages)
{
    Write-Host "Publishing package «$package»"
    & $nugetExe push $package
}

Write-Host "Operation completed!"