using System;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public class KeyboardEffect : AbstractEffect
    {
        private readonly ColorSettings _colorSettings = ColorSettings.Instance;
        private readonly IKeyboard _chromaKeyboard;
        private readonly EffectPattern _effectPattern = CreatePatternEffect();
        private int _effectCursor;

        public KeyboardEffect()
        {
            _chromaKeyboard = Chroma.Instance.Keyboard;
        }

        protected override void OnEffectTick()
        {
            var columnSize = Constants.MaxColumns / _effectPattern.TotalColumns;
            var columnStartIndex = 0;
            var playPattern = _effectPattern.PatternRows.ElementAt(_effectCursor);

            for (var patternColumn = 0; patternColumn < _effectPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + columnSize;

                if (IsMismatchingLastColumnEndIndex(patternColumn, columnEndIndex))
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

            if (_effectCursor < _effectPattern.TotalPlaybackRows - 1)
            {
                _effectCursor++;
            }
            else
            {
                _effectCursor = 0;
            }
        }

        protected override void OnEffectStop()
        {
            _chromaKeyboard.SetStatic(new Static(_colorSettings.DefaultColor));
        }

        private bool IsMismatchingLastColumnEndIndex(int patternColumn, int columnEndIndex)
        {
            return patternColumn == _effectPattern.TotalColumns - 1 && columnEndIndex != Constants.MaxColumns;
        }

        private Color GetPlaybackColumnColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.OFF:
                    return Color.Black;
                case ColorType.PRIMARY:
                    return _colorSettings.PrimaryColor;
                case ColorType.SECONDARY:
                    return _colorSettings.SecondaryColor;
                default:
                    throw new ArgumentOutOfRangeException("colorType", colorType, null);
            }
        }

        private static EffectPattern CreatePatternEffect()
        {
            return new EffectPattern(
                new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
                new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
                new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.PRIMARY, ColorType.PRIMARY),
                new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.PRIMARY, ColorType.PRIMARY),
                new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
        }
    }
}