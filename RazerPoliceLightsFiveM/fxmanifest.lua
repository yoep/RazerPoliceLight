fx_version 'bodacious'
game 'gta5'

author 'yoep'
description 'Razer & Corsair Keyboard Police Lights'
version '2.0.0'

fxdk_watch_command 'dotnet' {'watch', '--project', 'Client/RazerPoliceLightsFiveM.Client.csproj', 'publish', '--configuration', 'Release'}

file 'Client/bin/Release/**/publish/*.dll'

client_script 'Client/bin/Release/**/publish/*.net.dll'
