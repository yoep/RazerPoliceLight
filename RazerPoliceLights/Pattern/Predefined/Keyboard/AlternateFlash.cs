namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class AlternateFlash
    {
        public static EffectPattern Get => new EffectPattern("AlternateFlash", DeviceType.Keyboard,
            new PatternRow(1.0, ColorType.ON, ColorType.ON, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.ON, ColorType.ON, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.ON, ColorType.ON),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.ON, ColorType.ON),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
    }
}