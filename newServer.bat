cd C:\Users\Alex\Desktop\ViCityBuildNet\instances
tar -xf vicity-server.zip
rename ViCityBuildServer %1
cd %1
ViCity.exe -port %1
exit