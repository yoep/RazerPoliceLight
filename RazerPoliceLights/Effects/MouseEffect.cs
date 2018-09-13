using System.Collections.Generic;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Effects
{
    internal class MouseEffect : AbstractEffect
    {
        private readonly IMouse _chromaMouse;

        #region Constructors

        internal MouseEffect()
        {
            _chromaMouse = Chroma.Instance.Mouse;
        }

        #endregion

        #region Properties

        protected override List<EffectPattern> EffectPatterns =>
            Settings.DeviceSettings.MouseSettings.EffectPatterns;

        protected override bool IsDisabled => !Settings.DeviceSettings.MouseSettings.IsEnabled;

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
                        _chromaMouse[row, column] =
                            GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn));
                    }
                }

                columnStartIndex = columnEndIndex;
            }
        }

        protected override void OnEffectStop()
        {
            _chromaMouse.SetStatic(new Static(Led.All, Settings.ColorSettings.StandbyColor));
        }

        protected override bool IsScanModeEnabled()
        {
            return Settings.DeviceSettings.MouseSettings.IsScanEnabled;
        }
    }
}