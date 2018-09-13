namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class Alternate
    {
        public static EffectPattern Get => new EffectPattern("Alternate", DeviceType.Keyboard,
            new PatternRow(2.0, ColorType.PRIMARY, ColorType.PRIMARY, ColorType.OFF, ColorType.OFF),
            new PatternRow(2.0, ColorType.OFF, ColorType.OFF, ColorType.SECONDARY, ColorType.SECONDARY));
    }
}