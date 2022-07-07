using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLightsBase.Settings
{
    public class EffectSettings
    {
        [Xml] public double Speed { get; set; }

        public PatternRowSettings PatternRow { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EffectSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Speed.GetHashCode() * 397) ^ (PatternRow != null ? PatternRow.GetHashCode() : 0);
            }
        }

        protected bool Equals(EffectSettings other)
        {
            return Speed.Equals(other.Speed) && Equals(PatternRow, other.PatternRow);
        }
    }
}