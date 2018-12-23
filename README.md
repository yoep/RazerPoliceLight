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

- General performance optimizations (should prevent lag)
- Reworked color configuration implementation
- Color is now based on effect pattern columns instead of OFF(0), PRIMARY(1) and SECONDARY(2)
    - Modified effect pattern color config to OFF(0) and ON(1) - this should make creating custom patterns a lot easier (see manual for example)
- Reworked the way ELS colors were being used