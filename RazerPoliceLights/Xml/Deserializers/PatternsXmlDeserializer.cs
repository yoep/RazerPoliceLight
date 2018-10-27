using System;
using System.Collections.Generic;
using System.Xml.XPath;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class PatternsXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            var children = deserializationContext.CurrentNode.SelectChildren(XPathNodeType.Element);
            var patterns = new Dictionary<DeviceType, List<EffectPattern>>
            {
                {DeviceType.Keyboard, new List<EffectPattern>()},
                {DeviceType.Mouse, new List<EffectPattern>()}
            };

            foreach (XPathNavigator child in children)
            {
                var effectSetting =
                    (EffectPatternSettings) deserializationContext.Deserialize(parser, child,
                        typeof(EffectPatternSettings));

                patterns[effectSetting.Device]
                    .Add(new EffectPattern(effectSetting.Name, effectSetting.Device));
            }

            return null;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(Dictionary<DeviceType, List<EffectPattern>>);
        }
    }
}