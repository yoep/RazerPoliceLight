using System;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class DoubleXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return deserializationContext.CurrentNode.ValueAsDouble;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(double);
        }
    }
}