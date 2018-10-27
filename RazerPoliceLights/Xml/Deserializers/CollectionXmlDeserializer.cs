using System;
using System.Collections;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class CollectionXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(ICollection);
        }
    }
}