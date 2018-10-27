using System;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class StringXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return deserializationContext.CurrentNode.Value;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(string);
        }
    }
}