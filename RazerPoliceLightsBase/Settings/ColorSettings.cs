using Corale.Colore.Core;
using RazerPoliceLightsRage.Xml.Attributes;

namespace RazerPoliceLightsBase.Settings
{
    public class ColorSettings
    {
        [Xml(DefaultValue = false, IsOptional = true)]
        public bool ElsEnabled { get; set; }

        [XmlElement(Name = "Primary")]
        public Color PrimaryColor { get; set; }

        [XmlElement(Name = "Secondary")]
        public Color SecondaryColor { get; set; }

        [XmlElement(Name = "Standby")]
        public Color StandbyColor { get; set; }

        public override string ToString()
        {
            return $"{nameof(ElsEnabled)}: {ElsEnabled}, " +
                   $"{nameof(PrimaryColor)}: {PrimaryColor.Value}, " +
                   $"{nameof(SecondaryColor)}: {SecondaryColor.Value}, " +
                   $"{nameof(StandbyColor)}: {StandbyColor.Value}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ColorSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PrimaryColor.GetHashCode();
                hashCode = (hashCode * 397) ^ SecondaryColor.GetHashCode();
                hashCode = (hashCode * 397) ^ StandbyColor.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(ColorSettings other)
        {
            return PrimaryColor.Equals(other.PrimaryColor) && SecondaryColor.Equals(other.SecondaryColor) && StandbyColor.Equals(other.StandbyColor);
        }
    }
}