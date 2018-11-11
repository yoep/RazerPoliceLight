# Razer GTA V Police Lighting Effects

Razer devices lighting effect support for GTA V.
This plugin uses RAGE plugin hook & Colore API for Razer Synapse SDK.
    
        Adds police lighting effects to Chroma enabled Razer devices. The effects are currently based around presets which can be configured for the keyboard and mouse. 
        The color of the effects can also be modified to predefined color values or RGB values.
        This plugin supports ELS.

## Dependencies

- [RAGE Plugin Hook](http://ragepluginhook.net/)
- [Colore Razer SDK API](https://github.com/chroma-sdk/Colore)
- [CUE.NET SDK API](https://github.com/DarthAffe/CUE.NET)

## Currently supports

- Chroma enabled Razer keyboard
- Chroma enabled Razer mouse
- Cue enabled Corsair keyboard
- Cue enabled Corsair mouse

## Features

- Customize effect speed
- Enable on foot
- Customize colors
- Effect scan mode
- Pattern selection per device
- Selection of active Chroma devices
- Color configuration based on ELS configuration

## Changelog features for 1.0.3

- Fixed CueSDK not being recognized (moved SDK to x64 folder in the installation directory)
- Fixed Cue keyboard LED drawing not including the last keyboard column and row
- Added SDK info logging
- Auto-disabled devices when they could not be registered (a message is logged in this case)
- Prevented plugin from crashing when a device didn't register correctly and the device playback thread is being stopped