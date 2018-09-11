using System.Xml;
using Corale.Colore.Core;

namespace RazerPoliceLights.Settings.Loaders
{
    public class ColorSettingsLoader : AbstractLoader
    {
        private const string ColorsPath = "RazerPoliceLights/Colors";
        private const string StandbyPath = "Standby";
        private const string PrimaryPath = "Primary";
        private const string SecondaryPath = "Secondary";

        public static ColorSettings Load(XmlNode document)
        {
            var colorsNode = document.SelectSingleNode(ColorsPath);

            if (colorsNode == null)
                throw new SettingsException("Color settings configuration is missing in the configuration file");

            return new ColorSettings
            {
                StandbyColor = GetColorFromNode(colorsNode.SelectSingleNode(StandbyPath)),
                PrimaryColor = GetColorFromNode(colorsNode.SelectSingleNode(PrimaryPath)),
                SecondaryColor = GetColorFromNode(colorsNode.SelectSingleNode(SecondaryPath))
            };
        }

        private static Color GetColorFromNode(XmlNode node)
        {
            if (node == null)
                throw new SettingsException("Missing color setting in color settings section");

            var nodeInnerText = node.InnerText;

            if (IsNotEmpty(nodeInnerText))
            {
                return ConvertTextToColor(nodeInnerText);
            }

            var redValue = GetColorValueFromAttribute(node, "R");
            var greenValue = GetColorValueFromAttribute(node, "G");
            var blueValue = GetColorValueFromAttribute(node, "B");

            return new Color(redValue, greenValue, blueValue);
        }

        private static byte GetColorValueFromAttribute(XmlNode node, string attributeName)
        {
            var colorValue = byte.Parse(GetAttributeValue(node, attributeName));

            if (colorValue <= 255)
                return colorValue;

            throw new UnknownColorSettingException(colorValue + " color value for color setting attribute" +
                                                   attributeName + " is not between 0 and 255");
        }

        private static Color ConvertTextToColor(string nodeInnerText)
        {
            switch (nodeInnerText.ToLower())
            {
                case "black":
                    return Color.Black;
                case "blue":
                    return Color.Blue;
                case "red":
                    return Color.Red;
                case "orange":
                    return Color.Orange;
                case "yellow":
                    return Color.Yellow;
                case "white":
                    return Color.White;
                default:
                    throw new SettingsException(nodeInnerText + " is not a valid color value");
            }
        }
    }
}