using System;
using System.Drawing;
using System.Linq;
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
        private readonly CorsairMouse _mouse;

        public CorsairMouseEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager)
            : base(rage, settingsManager, elsSettingsManager)
        {
            _mouse = CueSDK.MouseSDK;
        }

        protected override void OnEffectTick(PatternRow playPattern)
        {
            var maxWidth = (int) Math.Round(_mouse.DeviceRectangle.Width);
            var maxHeight = (int) Math.Round(_mouse.DeviceRectangle.Height);
            var columnSize = maxWidth / playPattern.TotalColumns;
            var startIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = startIndex + columnSize;
                var rowEndIndex = startIndex + columnSize;

                if (IsMismatchingLastEndIndex(playPattern, maxWidth, patternColumn, columnEndIndex))
                    columnEndIndex = maxWidth;
                if (IsMismatchingLastEndIndex(playPattern, maxHeight, patternColumn, columnEndIndex))
                    rowEndIndex = maxHeight;

                if (IsAnimateVerticallyEnabled)
                {
                    AnimateVertical(playPattern, startIndex, rowEndIndex, patternColumn);
                    startIndex = rowEndIndex;
                }
                else
                {
                    AnimateHorizontal(playPattern, startIndex, columnEndIndex, patternColumn);
                    startIndex = columnEndIndex;
                }
            }
        }

        protected override void OnEffectStop()
        {
            var standbyColor = SettingsManager.Settings.ColorSettings.StandbyColor;
            var mouseLedColor = new CorsairColor(standbyColor.R, standbyColor.G, standbyColor.B);
            
            foreach (var mouseLed in _mouse.Leds)
            {
                mouseLed.Color = mouseLedColor;
            }
        }
        
        private void AnimateHorizontal(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            var maxHeight = (int) Math.Round(_mouse.DeviceRectangle.Height);
            var drawArea = new RectangleF(startIndex, 0, endIndex, maxHeight);
            var columnColor = GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn), patternColumn);
            var corsairColor = new CorsairColor(columnColor.R, columnColor.G, columnColor.B);
            
            foreach (var led in _mouse[drawArea])
            {
                led.Color = corsairColor;
            }
        }

        private void AnimateVertical(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            var maxWidth = (int) Math.Round(_mouse.DeviceRectangle.Width);
            var drawArea = new RectangleF(0, startIndex, maxWidth, endIndex);
            var columnColor = GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn), patternColumn);
            var corsairColor = new CorsairColor(columnColor.R, columnColor.G, columnColor.B);
            
            foreach (var led in _mouse[drawArea])
            {
                led.Color = corsairColor;
            }
        }
    }
}