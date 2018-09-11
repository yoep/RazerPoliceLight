﻿namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class EvenOdd
    {
        public static EffectPattern Get => new EffectPattern(
            new PatternRow(ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY, ColorType.OFF),
            new PatternRow(ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY, ColorType.OFF),
            new PatternRow(ColorType.OFF, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY),
            new PatternRow(ColorType.OFF, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY));
    }
}