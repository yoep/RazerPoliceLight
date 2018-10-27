using System;
using System.Collections.Generic;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class EffectPatternXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return null;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(Dictionary<DeviceType, List<EffectPattern>>);
        }
    }
}