using System.Collections.Generic;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Effects
{
    internal class KeyboardEffect : AbstractEffect
    {
        private readonly IKeyboard _chromaKeyboard;

        #region Constructors

        internal KeyboardEffect()
        {
            _chromaKeyboard = Chroma.Instance.Keyboard;
        }

        #endregion

        #region Properties

        protected override List<EffectPattern> EffectPatterns =>
            Settings.DeviceSettings.KeyboardSettings.EffectPatterns;

        protected override bool IsDisabled => !Settings.DeviceSettings.KeyboardSettings.IsEnabled;

        #endregion

        protected override void OnEffectTick(PatternRow playPattern)
        {
            var columnSize = Constants.MaxColumns / playPattern.TotalColumns;
            var columnStartIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + columnSize;

                if (IsMismatchingLastColumnEndIndex(playPattern, Constants.MaxColumns, patternColumn, columnEndIndex))
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
            _chromaKeyboard.SetStatic(new Static(Settings.ColorSettings.StandbyColor));
        }

        protected override bool IsScanModeEnabled()
        {
            return Settings.DeviceSettings.KeyboardSettings.IsScanEnabled;
        }
    }
}