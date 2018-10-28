using System;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLights.Xml.Parser;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class BooleanXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return deserializationContext.CurrentNode.ValueAsBoolean;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(bool);
        }
    }
}