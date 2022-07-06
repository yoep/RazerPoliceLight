using System;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLightsBase.Settings.Exceptions;
using RazerPoliceLightsBase.Xml;
using RazerPoliceLightsBase.Xml.Parser;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class ColorXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            var textValue = !string.IsNullOrEmpty(deserializationContext.Value)
                ? deserializationContext.Value
                : deserializationContext.CurrentNode.Value;

            if (!string.IsNullOrEmpty(textValue))
                return ConvertTextToColor(textValue);

            var redValue = GetColorValueFromAttribute(parser, deserializationContext, "R");
            var greenValue = GetColorValueFromAttribute(parser, deserializationContext, "G");
            var blueValue = GetColorValueFromAttribute(parser, deserializationContext, "B");

            return Color.FromArgb(redValue, greenValue, blueValue);
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(Color);
        }

        private static byte GetColorValueFromAttribute(XmlParser parser, XmlContext node, string attributeName)
        {
            var colorValue = byte.Parse(parser.GetAttributeValue(node, attributeName));

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
                case "amber":
                    return Color.FromArgb(255, 191, 0);
                case "yellow":
                    return Color.Yellow;
                case "white":
                    return Color.White;
                case "green":
                    return Color.Green;
                case "purple":
                    return Color.Purple;
                default:
                    throw new SettingsException(nodeInnerText + " is not a valid color value");
            }
        }
    }
}