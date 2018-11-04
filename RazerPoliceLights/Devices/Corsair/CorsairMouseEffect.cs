using System;
using CUE.NET;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Mouse;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Devices.Corsair
{
    public class CorsairMouseEffect : AbstractMouseEffect
    {
        private CorsairMouse _mouse;

        public CorsairMouseEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager)
            : base(rage, settingsManager, elsSettingsManager)
        {
            _mouse = CueSDK.MouseSDK;
        }

        protected override void OnEffectTick(PatternRow playPattern)
        {
            throw new NotImplementedException();
        }

        protected override void OnEffectStop()
        {
            var standbyColor = _settingsManager.Settings.ColorSettings.StandbyColor;
            var mouseLedColor = new CorsairColor(standbyColor.R, standbyColor.G, standbyColor.B);
            
            foreach (var mouseLed in _mouse.Leds)
            {
                mouseLed.Color = mouseLedColor;
            }
        }
    }
}