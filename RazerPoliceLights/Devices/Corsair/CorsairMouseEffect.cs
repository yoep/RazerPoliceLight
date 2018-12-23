using System;
using System.Drawing;
using System.Linq;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Mouse;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Devices.Corsair
{
    public class CorsairMouseEffect : AbstractMouseEffect
    {
        private CorsairMouse _mouse;

        #region Constructors

        public CorsairMouseEffect(IRage rage, ISettingsManager settingsManager, IColorManager colorManager)
            : base(rage, settingsManager, colorManager)
        {
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Initialize()
        {
            if (IsDisabled)
                return;

            Rage.LogTrivialDebug("Initializing CueSDK.MouseSDK...");
            _mouse = CueSDK.MouseSDK;

            if (_mouse != null)
            {
                _mouse.Brush = (SolidColorBrush) Color.Transparent;
                Rage.LogTrivialDebug("Initialization of CueSDK.MouseSDK done");
            }
            else
            {
                Rage.LogTrivial("CueSDK.MouseSDK could not be registered, do you have a Cue supported mouse?");
                Rage.LogTrivialDebug("--- SDK info ---");
                Rage.LogTrivialDebug("Last SDK error: " + CueSDK.LastError);
                Rage.LogTrivialDebug("Devices: " + string.Join(",", CueSDK.InitializedDevices.Select(x => x.DeviceInfo.Type + "-" + x.DeviceInfo.Model)));
            }
        }

        #endregion

        protected override void OnEffectTick(PatternRow playPattern)
        {
            if (_mouse == null)
                return; //something probably went wrong during initialization, ignore this device effect playback

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

            _mouse.Update();
        }

        protected override void OnEffectStop()
        {
            if (_mouse == null)
                return; //something probably went wrong during initialization, ignore this device effect playback

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
            var columnColor = GetPlaybackColumnColor(playPattern, patternColumn);
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
            var columnColor = GetPlaybackColumnColor(playPattern, patternColumn);
            var corsairColor = new CorsairColor(columnColor.R, columnColor.G, columnColor.B);

            foreach (var led in _mouse[drawArea])
            {
                led.Color = corsairColor;
            }
        }
    }
}