using System;
using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public class MouseEffect : AbstractEffect
    {
        private readonly ColorSettings _colorSettings = ColorSettings.Instance;
        private readonly IMouse _chromaMouse;
        private readonly EffectPattern _effectPattern = CreatePatternEffect();

        private int _effectCursor;

        public MouseEffect()
        {
            _chromaMouse = Chroma.Instance.Mouse;
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
                        _chromaMouse[row, column] =
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
            _chromaMouse.SetStatic(new Static(Led.All, _colorSettings.DefaultColor));
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
            return new EffectPattern(new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
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