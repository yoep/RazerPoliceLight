using System.Collections.Generic;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public class KeyboardEffect : AbstractEffect, IKeyboardEffect
    {
        private readonly IKeyboard _chromaKeyboard;

        #region Constructors

        public KeyboardEffect(ISettingsManager settingsManager) : base(settingsManager)
        {
            _chromaKeyboard = Chroma.Instance.Keyboard;
        }

        #endregion

        #region Properties

        protected override List<EffectPattern> EffectPatterns => EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard);

        protected override bool IsDisabled => !_settingsManager.Settings.DeviceSettings.KeyboardSettings.IsEnabled;

        #endregion

        protected override void OnEffectTick(PatternRow playPattern)
        {
            var columnSize = Constants.MaxColumns / playPattern.TotalColumns;
            var columnStartIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + columnSize;

                if (IsMismatchingLastEndIndex(playPattern, Constants.MaxColumns, patternColumn, columnEndIndex))
                {
                    columnEndIndex = Constants.MaxColumns;
                }

                for (var row = 0; row < Constants.MaxRows; row++)
                {
                    for (var column = columnStartIndex; column < columnEndIndex; column++)
                    {
                        _chromaKeyboard[row, column] =
                            GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn));
                    }
                }

                columnStartIndex = columnEndIndex;
            }
        }

        protected override void OnEffectStop()
        {
            _chromaKeyboard.SetStatic(new Static(_settingsManager.Settings.ColorSettings.StandbyColor));
        }

        protected override bool IsScanModeEnabled()
        {
            return _settingsManager.Settings.DeviceSettings.KeyboardSettings.IsScanEnabled;
        }
    }
}