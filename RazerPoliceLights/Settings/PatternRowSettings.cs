using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Settings
{
    public class PatternRowSettings
    {
        public ColorType[] ColorTypes { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PatternRowSettings) obj);
        }

        public override int GetHashCode()
        {
            return (ColorTypes != null ? ColorTypes.GetHashCode() : 0);
        }

        protected bool Equals(PatternRowSettings other)
        {
            return Equals(ColorTypes, other.ColorTypes);
        }
    }
}