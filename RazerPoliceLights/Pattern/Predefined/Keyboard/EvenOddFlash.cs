namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class EvenOddFlash
    {
        public static EffectPattern Get => new EffectPattern("EvenOddFlash", DeviceType.Keyboard,
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