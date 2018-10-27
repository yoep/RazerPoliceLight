using System;
using System.Xml.XPath;
using Corale.Colore.Core;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class ColorXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            var textValue = deserializationContext.CurrentNode.Value;

            if (!string.IsNullOrEmpty(textValue)) 
                return ConvertTextToColor(textValue);
            
            var redValue = GetColorValueFromAttribute(parser, deserializationContext, "R");
            var greenValue = GetColorValueFromAttribute(parser, deserializationContext, "G");
            var blueValue = GetColorValueFromAttribute(parser, deserializationContext, "B");

            return new Color(redValue, greenValue, blueValue);
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