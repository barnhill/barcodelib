@ECHO OFF

WHERE /Q msbuild
IF ERRORLEVEL 1 (
    ECHO ERROR >&2
    ECHO Please ensure that msbuild is in your path, perhaps by using >&2
    ECHO the Developer Command Prompt feature of your Visual Studio >&2
    ECHO installation. >&2
    EXIT /B 1
)

WHERE /Q nuget
IF ERRORLEVEL 1 (
    ECHO ERROR >&2
    ECHO Please install nuget.exe. You may obtain it from http://nuget.org/nuget.exe. >&2
    ECHO You may copy nuget.exe to this folder or open an Administrator >&2
    ECHO Command Prompt and COPY it to both %WINDIR%\system32 and >&2
    ECHO %WINDIR%\syswow64 (the latter only if your Windows is multilib^). >&2
    ECHO (Not using an elevated cmd session will result in the file ending >&2
    ECHO up somewhere other than system32 or syswow64^). >&2
    EXIT /B 1
)

SET CONFIGURATION_FLAG=Configuration=Release
msbuild /p:%CONFIGURATION_FLAG% && nuget pack -Properties %CONFIGURATION_FLAG%;DoNotInvokeNugetPackDirectly=
