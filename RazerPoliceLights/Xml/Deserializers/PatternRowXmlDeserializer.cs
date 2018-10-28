using System;
using System.Collections.Generic;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLights.Xml.Parser;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class PatternRowXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            var patternRowSettings = new PatternRowSettings();
            var value = deserializationContext.CurrentNode.InnerXml;

            var patternTypes = value.Split(',');

            if (patternTypes.Length > 0)
            {
                patternRowSettings.ColorTypes = GetPatternColors(patternTypes);
            }
            else
            {
                //TODO: log to rage console
            }

            return patternRowSettings;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(PatternRowSettings);
        }

        private static ColorType[] GetPatternColors(IEnumerable<string> patternTypes)
        {
            var patternColors = new List<ColorType>();

            foreach (var patternType in patternTypes)
            {
                try
                {
                    patternColors.Add(GetColorType(patternType));
                }
                catch (ColorTypeException e)
                {
                    //TODO log to console
                }
            }

            return patternColors.ToArray();
        }

        private static ColorType GetColorType(string patternType)
        {
            switch (patternType)
            {
                case "0":
                    return ColorType.OFF;
                case "1":
                    return ColorType.PRIMARY;
                case "2":
                    return ColorType.SECONDARY;
                default:
                    throw new ColorTypeException("Pattern type '" + patternType +
                                                 "' is invalid, see manual for allowed values");
            }
        }
    }
}