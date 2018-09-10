namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class LeftRightFullFlash
    {
        public static EffectPattern Get
        {
            get
            {
                return new EffectPattern(
                    new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.SECONDARY, ColorType.SECONDARY),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.SECONDARY, ColorType.SECONDARY),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.SECONDARY, ColorType.SECONDARY),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.PRIMARY, ColorType.PRIMARY, ColorType.SECONDARY, ColorType.SECONDARY),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
            }
        }
    }
}