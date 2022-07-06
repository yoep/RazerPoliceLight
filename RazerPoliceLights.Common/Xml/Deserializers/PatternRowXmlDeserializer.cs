using System;
using System.Collections.Generic;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Exceptions;
using RazerPoliceLightsBase.Xml;
using RazerPoliceLightsBase.Xml.Parser;

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
                throw new SettingsException("Pattern row cannot be empty");
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
                    throw new SettingsException(e.Message + Environment.NewLine + e.StackTrace);
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
                    return ColorType.ON;
                case "2":
                    //TODO: remove deprecated value, but still support it currently for backwards compatibility 
                    return ColorType.ON;
                default:
                    throw new ColorTypeException("Pattern type '" + patternType +
                                                 "' is invalid, see manual for allowed values");
            }
        }
    }
}