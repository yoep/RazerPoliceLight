namespace RazerPoliceLights.Pattern.Predefined.Mouse
{
    public static class EvenOdd
    {
        public static EffectPattern Get => new EffectPattern("EvenOdd", DeviceType.Mouse,
            new PatternRow(2.0, ColorType.ON, ColorType.OFF, ColorType.ON, ColorType.OFF),
            new PatternRow(2.0, ColorType.OFF, ColorType.ON, ColorType.OFF, ColorType.ON));
    }
}