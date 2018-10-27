using System;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class BooleanXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return deserializationContext.CurrentNode.ValueAsBoolean;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(bool);
        }
    }
}