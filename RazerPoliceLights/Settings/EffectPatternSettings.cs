using System.Collections.Generic;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    public class EffectPatternSettings
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public DeviceType Device { get; set; }

        public List<PatternRowSettings> Effects { get; set; }
    }
}