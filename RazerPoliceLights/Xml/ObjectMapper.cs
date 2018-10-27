using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml
{
    public class ObjectMapper
    {
        public ObjectMapper(List<IXmlDeserializer> deserializers)
        {
            Deserializers = deserializers;
        }

        public List<IXmlDeserializer> Deserializers { get; }

        public T ReadValue<T>(string uri, Type clazz) where T : class
        {
            Assert.NotNull(clazz, "clazz cannot be null");
            Assert.HasText(uri, "uri cannot be empty");

            if (!File.Exists(uri))
                throw new FileNotFoundException(uri + " does not exist");

            var document = new XPathDocument(uri);
            var xmlParser = new XmlParser();
            var rootNode =
                xmlParser.FetchNodeForMember(
                    new XmlDeserializationContext(document, document.CreateNavigator(), clazz, null), clazz);
            var deserializationContext = new XmlDeserializationContext(document, rootNode, clazz, Deserializers);
            var xmlDeserializer = Deserializers.Find(e => e.CanHandle(clazz));

            return (T) xmlDeserializer.deserialize(xmlParser, deserializationContext);
        }
    }
}