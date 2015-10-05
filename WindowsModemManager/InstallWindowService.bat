@ECHO off
ECHO the InstanceId is %InstanceId%  > "%~dp0InstanceId.txt"
SETLOCAL
CALL :GetInstanceIndex
ECHO the index is %ERRORLEVEL% > "%~dp0InstanceIndex.txt"

IF %ERRORLEVEL% GTR 0 ( 
GOTO SkipInstalation 
 )

IF %ERRORLEVEL% LEQ 0 (
GOTO InstallRecomended
)

:InstallRecomended
ECHO Stepped into service installation
::Service Install command here

::Stops the Service and logs the ouput
ECHO Stopping the Window Service
NET STOP ModemManagerService > "%~dp0ServiceStop.txt"
::Uninstals the service and Instals again
ECHO Uninstalling the Window Service
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil /u "%~dp0WindowsModemManager.exe" > "%~dp0ServiceUninstall.txt"
ECHO Installing the Window Service
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil /i "%~dp0WindowsModemManager.exe" > "%~dp0ServiceInstall.txt"
ECHO InstallationCompleted the Window Service
::Starts the service
ECHO Starting the Window Service
NET START ModemManagerService > "%~dp0ServiceStart.txt"
GOTO EOF

:SkipInstalation
ECHO Installation Skipped
::Stops the Service and logs the ouput
NET STOP SimplyReliableService > "%~dp0ServiceStop.txt"
::Uninstals the service and Instals again
ECHO Uninstalling the Window Service
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil /u "%~dp0WindowsModemManager.exe" > "%~dp0ServiceUninstall.txt"
GOTO EOF

 
:GetInstanceIndex
::Gets the instance index from Instance id
FOR /f "tokens=1,2,3 delims=_ " %%a IN ("%InstanceId%") DO SET rolename=%%a&set instancindex=%%c
ECHO.InstanceIndex  : %instancindex% 
exit /b %instancindex%


:EOF
::Exit with Proper Exit code
Exit /b 0




