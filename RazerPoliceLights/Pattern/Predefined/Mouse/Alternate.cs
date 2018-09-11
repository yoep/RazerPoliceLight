namespace RazerPoliceLights.Pattern.Predefined.Mouse
{
    public static class Alternate
    {
        public static EffectPattern Get => new EffectPattern(
            new PatternRow(ColorType.PRIMARY, ColorType.OFF),
            new PatternRow(ColorType.PRIMARY, ColorType.OFF),
            new PatternRow(ColorType.OFF, ColorType.SECONDARY),
            new PatternRow(ColorType.OFF, ColorType.SECONDARY));
    }
}