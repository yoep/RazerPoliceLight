using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Moq;
using RazerPoliceLights.Xml;
using RazerPoliceLights.Xml.Context;
using RazerPoliceLights.Xml.Parser;
using Xunit;

namespace RazerPoliceLightsTests.Xml.Context
{
    public class XmlDeserializationContextTests
    {
        public class DeserializeNode
        {
            private readonly XmlParser _parser;
            private readonly XPathDocument _document;
            private readonly XPathNavigator _node;

            public DeserializeNode()
            {
                _parser = Mock.Of<XmlParser>();
                _document = new Mock<XPathDocument>("RazerPoliceLights.xml").Object;
                _node = Mock.Of<XPathNavigator>();
            }

            [Fact]
            public void ShouldThrowXmlExceptionWhenDeserializerIsNotFound()
            {
                var context = new XmlDeserializationContext(_document, _node, typeof(string), new List<IXmlDeserializer>());
                var type = typeof(string);

                var result = Assert.Throws<XmlException>(() => context.Deserialize(_parser, _node, type));

                Assert.Equal("Could not find deserializer for type " + type, result.Message);
            }

            [Fact]
            public void ShouldCallDeserializeOnDeserializerWhenFound()
            {
                var deserializerMock = new Mock<IXmlDeserializer>();
                var context = new XmlDeserializationContext(_document, _node, typeof(string), new List<IXmlDeserializer> {deserializerMock.Object});
                var type = typeof(string);
                deserializerMock.Setup(x => x.CanHandle(type)).Returns(true);

                context.Deserialize(_parser, _node, type);

                deserializerMock.Verify(x => x.Deserialize(_parser, It.IsAny<XmlDeserializationContext>()));
            }
        }

        public class DeserializeNodes
        {
            private readonly XmlParser _parser;
            private readonly XPathDocument _document;
            private readonly XPathNodeIterator _nodes;

            public DeserializeNodes()
            {
                _parser = Mock.Of<XmlParser>();
                _document = new Mock<XPathDocument>("RazerPoliceLights.xml").Object;
                _nodes = Mock.Of<XPathNodeIterator>();
            }

            [Fact]
            public void ShouldThrowXmlExceptionWhenDeserializerIsNotFound()
            {
                var context = new XmlDeserializationContext(_document, Mock.Of<XPathNavigator>(), typeof(string), new List<IXmlDeserializer>());
                var type = typeof(string);

                var result = Assert.Throws<XmlException>(() => context.Deserialize(_parser, _nodes, type));

                Assert.Equal("Could not find deserializer for type " + type, result.Message);
            }

            [Fact]
            public void ShouldCallDeserializeOnDeserializerWhenFound()
            {
                var deserializerMock = new Mock<IXmlDeserializer>();
                var context = new XmlDeserializationContext(_document, Mock.Of<XPathNavigator>(), typeof(string), new List<IXmlDeserializer> {deserializerMock.Object});
                var type = typeof(string);
                deserializerMock.Setup(x => x.CanHandle(type)).Returns(true);

                context.Deserialize(_parser, _nodes, type);

                deserializerMock.Verify(x => x.Deserialize(_parser, It.IsAny<XmlDeserializationContext>()));
            }
        }
    }
}