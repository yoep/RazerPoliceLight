namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class AlternateFlash
    {
        public static EffectPattern Get => new EffectPattern("AlternateFlash", DeviceType.Keyboard,
            new PatternRow(1.0, ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.SECONDARY, ColorType.SECONDARY),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.SECONDARY, ColorType.SECONDARY),
            new PatternRow(1.0, ColorType.OFF, ColorType.OFF, ColorType.OFF, ColorType.OFF));
    }
}