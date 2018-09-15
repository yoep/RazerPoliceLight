# Razer GTA V Police Lighting Effects

Razer devices lighting effect support for GTA V.
This plugin uses RAGE plugin hook & Colore API for Razer Synapse SDK.
    
        Adds police lighting effects to Chroma enabled Razer devices. The effects are currently based around presets which can be configured for the keyboard and mouse. 
        The color of the effects can also be modified to predefined color values or RGB values.
        This plugin supports ELS.

## Dependencies

- [RAGE Plugin Hook](http://ragepluginhook.net/)
- [Colore Razer SDK API](https://github.com/chroma-sdk/Colore)

## Currently supports

- Chroma enabled  Razer keyboard
- Chroma enabled Razer mouse

## Features

- Customize effect speed
- Enable on foot
- Customize colors
- Effect scan mode
- Pattern selection per device
- Selection of active Chroma devices

## Changelog features for 1.0.1

- Added option to play effect vertically on a mouse device
- Added effect pattern definitions to the configuration
- Added option to create custom effects
- Added PlayEffects command to test custom created effects (accepts device and effect name as arguments)
- Added StopEffects command to stop playing all effects
- Added PoliceLightsReloadSettings command to reload the RazerPoliceLights.xml configuration