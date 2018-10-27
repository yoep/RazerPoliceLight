using System.Xml.XPath;

namespace RazerPoliceLights.Xml.Context
{
    public abstract class XmlContext
    {
        public XmlContext(XPathDocument document, XPathNavigator currentNode)
        {
            Document = document;
            CurrentNode = currentNode;
        }

        public XPathDocument Document { get; }

        public XPathNavigator CurrentNode { get; }
    }
}