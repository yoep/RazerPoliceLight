using System;
using CUE.NET;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Keyboard;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Devices.Corsair
{
    public class CorsairKeyboardEffect : AbstractKeyboardEffect
    {
        private CorsairKeyboard _keyboard;

        public CorsairKeyboardEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager)
            : base(rage, settingsManager, elsSettingsManager)
        {
            _keyboard = CueSDK.KeyboardSDK;
        }

        protected override void OnEffectTick(PatternRow playPattern)
        {
            throw new NotImplementedException();
        }

        protected override void OnEffectStop()
        {
            var standbyColor = _settingsManager.Settings.ColorSettings.StandbyColor;
            var keyboardLedColor = new CorsairColor(standbyColor.R, standbyColor.G, standbyColor.B);
            
            foreach (var keyboardLed in _keyboard.Leds)
            {
                keyboardLed.Color = keyboardLedColor;
            }
        }
    }
}