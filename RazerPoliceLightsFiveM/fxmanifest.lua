fx_version 'bodacious'
game 'gta5'

author 'yoep'
description 'Razer & Corsair Keyboard Police Lights'
version '2.0.0'

fxdk_watch_command 'dotnet' {'watch', '--project', 'RazerPoliceLightsFiveM/RazerPoliceLightsFiveM.csproj', 'publish', '--configuration', 'Release'}

file 'RazerPoliceLightsFiveM/bin/Release/**/publish/*.dll'

client_script 'RazerPoliceLightsFiveM/bin/Release/**/publish/*.net.dll'
