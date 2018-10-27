using Corale.Colore.Core;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    public class ColorSettings
    {
        [XmlElement(Name = "Primary")] public Color PrimaryColor { get; internal set; }

        [XmlElement(Name = "Secondary")] public Color SecondaryColor { get; internal set; }

        [XmlElement(Name = "Standby")] public Color StandbyColor { get; internal set; }

        public override string ToString()
        {
            return
                $"{nameof(PrimaryColor)}: {PrimaryColor.Value}," +
                $" {nameof(SecondaryColor)}: {SecondaryColor.Value}," +
                $" {nameof(StandbyColor)}: {StandbyColor.Value}";
        }
    }
}