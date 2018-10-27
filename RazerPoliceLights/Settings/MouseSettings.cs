using System.Collections.Generic;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    public class MouseSettings
    {
        [XmlAttribute(Name = "EnableScanMode")]
        public bool IsScanEnabled { get; set; }

        [XmlAttribute(Name = "Enabled")]
        public bool IsEnabled { get; set; }

        [XmlAttribute]
        public bool AnimateVertically { get; set; }

        [XmlIgnore]
        public List<EffectPattern> EffectPatterns { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(IsScanEnabled)}: {IsScanEnabled}, " +
                $"{nameof(IsEnabled)}: {IsEnabled}, " +
                $"{nameof(AnimateVertically)}: {AnimateVertically}, " +
                $"{nameof(EffectPatterns)}: {EffectPatterns?.Count} activated effects";
        }
    }
}