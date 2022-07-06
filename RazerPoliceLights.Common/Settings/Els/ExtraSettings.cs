using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLightsBase.Settings.Els
{
    public class ExtraSettings
    {
        [Xml] public bool IsElsControlled { get; set; }
        
        [Xml] public bool AllowEnvLight { get; set; }
        
        [Xml] public Color Color { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExtraSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsElsControlled.GetHashCode();
                hashCode = (hashCode * 397) ^ AllowEnvLight.GetHashCode();
                hashCode = (hashCode * 397) ^ Color.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(ExtraSettings other)
        {
            return IsElsControlled == other.IsElsControlled && AllowEnvLight == other.AllowEnvLight && Color.Equals(other.Color);
        }
    }
}