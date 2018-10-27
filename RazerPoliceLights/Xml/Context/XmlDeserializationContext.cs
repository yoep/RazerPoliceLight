using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace RazerPoliceLights.Xml.Context
{
    public class XmlDeserializationContext : XmlContext
    {
        public XmlDeserializationContext(
            XPathDocument document,
            XPathNavigator currentNode,
            Type deserializationType,
            List<IXmlDeserializer> deserializers) : base(document, currentNode)
        {
            DeserializationType = deserializationType;
            Deserializers = deserializers;
        }

        public XmlDeserializationContext(XmlDeserializationContext parent, XPathNavigator currentNode,
            Type deserializationType)
            : base(parent.Document, currentNode)
        {
            Deserializers = parent.Deserializers;
            DeserializationType = deserializationType;
        }

        public List<IXmlDeserializer> Deserializers { get; }

        public Type DeserializationType { get; }

        public object Deserialize(XmlParser parser, XPathNavigator node, Type type)
        {
            var deserializer = Deserializers.Find(e => e.CanHandle(type));

            if (deserializer == null)
                throw new XmlException("Could not find deserializer for type " + type);

            return deserializer.deserialize(parser,
                new XmlDeserializationContext(this, node, type));
        }
    }
}