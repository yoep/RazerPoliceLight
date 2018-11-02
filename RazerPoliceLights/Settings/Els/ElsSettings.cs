using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings.Els
{
    [XmlRootName("vcfroot")]
    public class ElsSettings
    {
        [XmlElement(Name = "EOVERRIDE")]
        public LightingSettings LightingSettings { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ElsSettings) obj);
        }

        public override int GetHashCode()
        {
            return (LightingSettings != null ? LightingSettings.GetHashCode() : 0);
        }

        protected bool Equals(ElsSettings other)
        {
            return Equals(LightingSettings, other.LightingSettings);
        }
    }
}