using System;
using System.Drawing;
using System.Linq;
using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic;
using CUE.NET.Devices.Keyboard;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;

namespace RazerPoliceLightsBase.Devices.Corsair
{
    public class CorsairKeyboardEffect : AbstractKeyboardEffect
    {
        private CorsairKeyboard _keyboard;

        #region Constructors

        public CorsairKeyboardEffect(INotification notification, ILogger logger, ISettingsManager settingsManager, IColorManager colorManager)
            : base(notification, logger, settingsManager, colorManager)
        {
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Initialize()
        {
            if (IsDisabled)
                return;

            Logger.Debug("Initializing CueSDK.KeyboardSDK...");
            _keyboard = CueSDK.KeyboardSDK;

            if (_keyboard != null)
            {
                Logger.Debug("Setting keyboard brush to Color.Transparent");
                _keyboard.Brush = (SolidColorBrush) Color.Transparent;
                Logger.Debug("Initialization of CueSDK.KeyboardSDK done");
            }
            else
            {
                Logger.Info("CueSDK.KeyboardSDK could not be registered, do you have a Cue supported keyboard?");
                Logger.Warn("--- SDK info ---");
                Logger.Warn("Last SDK error: " + CueSDK.LastError);
                Logger.Warn("Devices: " + string.Join(",", CueSDK.InitializedDevices.Select(x => x.DeviceInfo.Type + "-" + x.DeviceInfo.Model)));
            }
        }

        #endregion

        #region Functions

        protected override void OnEffectTick(PatternRow playPattern)
        {
            if (_keyboard == null)
                return; //something probably went wrong during initialization, ignore this device effect playback

            var columnSize = _keyboard.DeviceRectangle.Width / playPattern.TotalColumns;
            var columnStartIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + (int) Math.Round(columnSize);
                var maxWidth = (int) Math.Round(_keyboard.DeviceRectangle.Width);

                if (IsLastPatternColumn(playPattern, patternColumn))
                {
                    columnEndIndex = maxWidth + 100;
                }

                var drawArea = new RectangleF(columnStartIndex, 0, columnEndIndex, _keyboard.DeviceRectangle.Height + 100);
                var columnColor = GetPlaybackColumnColor(playPattern, patternColumn);
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
            if (_keyboard == null)
                return; //something probably went wrong during initialization, ignore this device effect playback

            var standbyColor = SettingsManager.Settings.ColorSettings.StandbyColor;
            var keyboardLedColor = new CorsairColor(standbyColor.R, standbyColor.G, standbyColor.B);

            foreach (var keyboardLed in _keyboard.Leds)
            {
                keyboardLed.Color = keyboardLedColor;
            }
        }

        #endregion
    }
}