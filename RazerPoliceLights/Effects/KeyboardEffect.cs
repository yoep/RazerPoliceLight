using System.Collections.Generic;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Pattern.Predefined.Keyboard;

namespace RazerPoliceLights.Effects
{
    internal class KeyboardEffect : AbstractEffect
    {
        private readonly IKeyboard _chromaKeyboard;

        internal KeyboardEffect() : base(GetEffects())
        {
            _chromaKeyboard = Chroma.Instance.Keyboard;
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
                        _chromaKeyboard[row, column] =
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
            _chromaKeyboard.SetStatic(new Static(Settings.ColorSettings.StandbyColor));
        }

        protected override bool IsScanModeEnabled()
        {
            return Settings.DeviceSettings.KeyboardSettings.IsScanEnabled;
        }

        private static List<EffectPattern> GetEffects()
        {
            return new List<EffectPattern>
            {
                AlternateFlash.Get,
                Alternate.Get,
                AlternateAndFullFlash.Get,
                EvenOddFlash.Get,
                EvenOdd.Get
            };
        }
    }
}