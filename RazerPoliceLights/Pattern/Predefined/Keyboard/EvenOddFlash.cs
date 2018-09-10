namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class EvenOddFlash
    {
        public static EffectPattern Get
        {
            get
            {
                return new EffectPattern(
                    new PatternRow(ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
            }
        }
    }
}