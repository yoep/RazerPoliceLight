namespace RazerPoliceLights.Pattern.Predefined.Mouse
{
    public class LeftRight
    {
        public static EffectPattern Get
        {
            get
            {
                return new EffectPattern(
                    new PatternRow(ColorType.PRIMARY, ColorType.OFF),
                    new PatternRow(ColorType.PRIMARY, ColorType.OFF),
                    new PatternRow(ColorType.OFF, ColorType.SECONDARY),
                    new PatternRow(ColorType.OFF, ColorType.SECONDARY));
            }
        }
    }
}