::@ECHO OFF


net stop ModemManager > %~dp0ModemManagerServiceStop.txt

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil /u "%~dp0WindowsModemManager.exe" > %~dp0Uninstallserice.txt

pause

