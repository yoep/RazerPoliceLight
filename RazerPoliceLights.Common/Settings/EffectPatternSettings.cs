using System.Collections.Generic;
using RazerPoliceLights.Xml.Attributes;
using RazerPoliceLightsBase.Pattern;

namespace RazerPoliceLightsBase.Settings
{
    public class EffectPatternSettings
    {
        [Xml] public string Name { get; set; }

        [Xml] public DeviceType Device { get; set; }

        [XmlElement(Name = "Effect")] public List<EffectSettings> Effects { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EffectPatternSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Device;
                hashCode = (hashCode * 397) ^ (Effects != null ? Effects.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(EffectPatternSettings other)
        {
            return string.Equals(Name, other.Name) && Device == other.Device && Equals(Effects, other.Effects);
        }
    }
}