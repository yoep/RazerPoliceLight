using System;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLights.Xml.Parser;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class DoubleXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return deserializationContext.CurrentNode.ValueAsDouble;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(double);
        }
    }
}