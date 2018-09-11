namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class AlternateFlash
    {
        public static EffectPattern Get => new EffectPattern(
            new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
            new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
            new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.SECONDARY, ColorType.SECONDARY),
            new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.SECONDARY, ColorType.SECONDARY),
            new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
    }
}