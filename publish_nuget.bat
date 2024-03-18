echo %GITHUB_USERNAME%
echo %GITHUB_APIKEY%
echo %GITHUB_INDEX%

dotnet build --configuration Release
dotnet nuget push Utils/bin/Release/GradientSoftware.Utils.1.0.1.nupkg --api-key %GITHUB_APIKEY% --source "github" 