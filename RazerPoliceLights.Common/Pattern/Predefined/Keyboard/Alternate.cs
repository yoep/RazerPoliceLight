namespace RazerPoliceLightsBase.Pattern.Predefined.Keyboard
{
    public static class Alternate
    {
        public static EffectPattern Get => new EffectPattern("Alternate", DeviceType.Keyboard,
            new PatternRow(2.0, ColorType.ON, ColorType.ON, ColorType.OFF, ColorType.OFF),
            new PatternRow(2.0, ColorType.OFF, ColorType.OFF, ColorType.ON, ColorType.ON));
    }
}