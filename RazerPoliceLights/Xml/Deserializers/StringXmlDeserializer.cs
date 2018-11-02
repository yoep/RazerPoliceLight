using System;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLights.Xml.Parser;

namespace RazerPoliceLights.Xml.Deserializers
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