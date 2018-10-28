using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLights.Xml.Parser;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class PatternsXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            var patterns = new Dictionary<DeviceType, List<EffectPattern>>
            {
                {DeviceType.Keyboard, new List<EffectPattern>()},
                {DeviceType.Mouse, new List<EffectPattern>()}
            };

            foreach (XPathNavigator node in deserializationContext.Nodes)
            {
                var effectSetting =
                    (EffectPatternSettings) deserializationContext.Deserialize(parser, node,
                        typeof(EffectPatternSettings));

                patterns[effectSetting.Device]
                    .Add(new EffectPattern(effectSetting.Name, effectSetting.Device, ConvertPatternRows(effectSetting.Effects)));
            }

            return patterns;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(Dictionary<DeviceType, List<EffectPattern>>);
        }

        private static PatternRow[] ConvertPatternRows(IEnumerable<EffectSettings> effects)
        {
            return effects.Select(e => new PatternRow(e.Speed, e.PatternRow.ColorTypes)).ToArray();
        }
    }
}