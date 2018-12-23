namespace RazerPoliceLights.Pattern.Predefined.Mouse
{
    public static class EvenOddFlash
    {
        public static EffectPattern Get => new EffectPattern("EvenOddFlash", DeviceType.Mouse,
            new PatternRow(1.0, ColorType.ON, ColorType.OFF, ColorType.ON, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.ON, ColorType.OFF, ColorType.ON, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.ON, ColorType.OFF, ColorType.ON),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.ON, ColorType.OFF, ColorType.ON),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
    }
}