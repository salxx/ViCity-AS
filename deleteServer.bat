set /a monitoringPort=%1 - 1000
FOR /F "tokens=5 delims= " %%P IN ('netstat -a -n -o ^| findstr :%monitoringPort%') DO TaskKill.exe /PID %%P
FOR /F "tokens=5 delims= " %%P IN ('netstat -a -n -o ^| findstr :%1') DO TaskKill.exe /PID %%P
timeout /T 1 /nobreak
cd C:\Users\Alex\Desktop\ViCityBuildNet\instances
FOR /D /R ./ %%X IN (%1) DO RMDIR /S /Q "%%X"
exit