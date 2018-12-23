﻿namespace RazerPoliceLights.Pattern.Predefined.Mouse
{
    public static class Alternate
    {
        public static EffectPattern Get => new EffectPattern("Alternate", DeviceType.Mouse,
            new PatternRow(2.0, ColorType.ON, ColorType.OFF),
            new PatternRow(2.0, ColorType.OFF, ColorType.ON));
    }
}