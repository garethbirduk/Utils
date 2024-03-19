@echo off
for /f %%i in ('git rev-parse --abbrev-ref HEAD') do set "branch=%%i"
echo Current branch: %branch%
dotnet script .github/scripts/update-version.csx %GITHUB_USERNAME% GradientSoftware.Utils %GITHUB_APIKEY% %branch%
