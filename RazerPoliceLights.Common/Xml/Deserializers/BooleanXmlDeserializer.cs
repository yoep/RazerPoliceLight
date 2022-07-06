using System;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLightsBase.Xml;
using RazerPoliceLightsBase.Xml.Parser;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class BooleanXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return !string.IsNullOrEmpty(deserializationContext.Value)
                ? bool.Parse(deserializationContext.Value)
                : deserializationContext.CurrentNode.ValueAsBoolean;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(bool);
        }
    }
}