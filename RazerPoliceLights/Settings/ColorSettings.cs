using Corale.Colore.Core;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    public class ColorSettings
    {
        [XmlElement(Name = "Primary")] public Color PrimaryColor { get; set; }

        [XmlElement(Name = "Secondary")] public Color SecondaryColor { get; set; }

        [XmlElement(Name = "Standby")] public Color StandbyColor { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(PrimaryColor)}: {PrimaryColor.Value}," +
                $" {nameof(SecondaryColor)}: {SecondaryColor.Value}," +
                $" {nameof(StandbyColor)}: {StandbyColor.Value}";
        }
    }
}