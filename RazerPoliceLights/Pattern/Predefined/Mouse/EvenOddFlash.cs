namespace RazerPoliceLights.Pattern.Predefined.Mouse
{
    public static class EvenOddFlash
    {
        public static EffectPattern Get => new EffectPattern("EvenOddFlash", DeviceType.Mouse,
            new PatternRow(1.0, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
    }
}