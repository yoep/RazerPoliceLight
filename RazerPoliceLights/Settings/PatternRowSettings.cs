using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    [XmlRootName("Effect")]
    public class PatternRowSettings
    {
        [Xml] public double Speed { get; set; }

        public string Value { get; set; }
    }
}