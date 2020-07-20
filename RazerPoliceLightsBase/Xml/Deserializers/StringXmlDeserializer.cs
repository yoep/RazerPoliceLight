using System;
using RazerPoliceLightsBase.Xml.Parser;
using RazerPoliceLightsRage.Xml.Context;

namespace RazerPoliceLightsBase.Xml.Deserializers
{
    public class StringXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return !string.IsNullOrEmpty(deserializationContext.Value)
                ? deserializationContext.Value
                : deserializationContext.CurrentNode.Value;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(string);
        }
    }
}