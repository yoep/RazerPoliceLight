namespace RazerPoliceLights.Pattern.Predefined.Keyboard
{
    public static class EvenOdd
    {
        public static EffectPattern Get => new EffectPattern("EvenOdd", DeviceType.Keyboard,
            new PatternRow(2.0, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY, ColorType.OFF),
            new PatternRow(2.0, ColorType.OFF, ColorType.PRIMARY, ColorType.OFF, ColorType.SECONDARY));
    }
}