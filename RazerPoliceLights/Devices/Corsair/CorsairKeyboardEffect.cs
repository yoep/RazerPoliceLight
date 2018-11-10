using System;
using System.Drawing;
using System.Linq;
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
        private readonly CorsairKeyboard _keyboard;

        public CorsairKeyboardEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager)
            : base(rage, settingsManager, elsSettingsManager)
        {
            rage.LogTrivialDebug("Initializing CueSDK.KeyboardSDK...");
            _keyboard = CueSDK.KeyboardSDK;
            rage.LogTrivialDebug("Initialization of CueSDK.KeyboardSDK done");
        }

        protected override void OnEffectTick(PatternRow playPattern)
        {
            var columnSize = _keyboard.DeviceRectangle.Width / playPattern.TotalColumns;
            var columnStartIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + (int) Math.Round(columnSize);
                var maxWidth = (int) Math.Round(_keyboard.DeviceRectangle.Width);

                if (IsMismatchingLastEndIndex(playPattern, maxWidth, patternColumn, columnEndIndex))
                {
                    columnEndIndex = maxWidth;
                }

                var drawArea = new RectangleF(columnStartIndex, 0, columnSize, _keyboard.DeviceRectangle.Height);
                var columnColor = GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn), patternColumn);
                var corsairColor = new CorsairColor(columnColor.R, columnColor.G, columnColor.B);
                
                foreach (var led in _keyboard[drawArea])
                {
                    led.Color = corsairColor;
                }

                columnStartIndex = columnEndIndex;
            }
        }

        protected override void OnEffectStop()
        {
            var standbyColor = SettingsManager.Settings.ColorSettings.StandbyColor;
            var keyboardLedColor = new CorsairColor(standbyColor.R, standbyColor.G, standbyColor.B);

            foreach (var keyboardLed in _keyboard.Leds)
            {
                keyboardLed.Color = keyboardLedColor;
            }
        }
    }
}