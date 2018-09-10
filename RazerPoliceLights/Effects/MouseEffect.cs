using System.Collections.Generic;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Pattern.Predefined.Mouse;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public class MouseEffect : AbstractEffect
    {
        private readonly ColorSettings _colorSettings = SettingsManager.Instance.Settings.ColorSettings;
        private readonly IMouse _chromaMouse;

        public MouseEffect() : base(GetEffects())
        {
            _chromaMouse = Chroma.Instance.Mouse;
        }

        protected override void OnEffectTick()
        {
            var effectPattern = GetEffectPattern();
            var columnSize = Constants.MaxColumns / effectPattern.TotalColumns;
            var columnStartIndex = 0;
            var playPattern = effectPattern.PatternRows.ElementAt(EffectCursor);

            for (var patternColumn = 0; patternColumn < effectPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + columnSize;

                if (IsMismatchingLastColumnEndIndex(effectPattern, Constants.MaxColumns, patternColumn, columnEndIndex))
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

            if (EffectCursor < effectPattern.TotalPlaybackRows - 1)
            {
                EffectCursor++;
            }
            else
            {
                EffectCursor = 0;
            }
        }

        protected override void OnEffectStop()
        {
            _chromaMouse.SetStatic(new Static(Led.All, _colorSettings.StandbyColor));
        }

        private static List<EffectPattern> GetEffects()
        {
            return new List<EffectPattern>
            {
                LeftRightFlash.Get,
                LeftRight.Get,
                EvenOddFlash.Get,
                EvenOdd.Get
            };
        }
    }
}