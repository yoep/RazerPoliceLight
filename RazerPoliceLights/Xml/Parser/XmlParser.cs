using System;
using System.Reflection;
using System.Xml.XPath;
using RazerPoliceLights.Xml.Attributes;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml.Parser
{
    public class XmlParser
    {
        public XPathNavigator FetchNodeForMember(XmlContext context, MemberInfo member)
        {
            Assert.NotNull(context, "context cannot be null");
            Assert.NotNull(member, "member cannot be null");

            return context.CurrentNode.SelectSingleNode(GetXmlLookupName(member));
        }

        public XPathNodeIterator FetchNodesForMember(XmlContext context, MemberInfo member)
        {
            Assert.NotNull(context, "context cannot be null");
            Assert.NotNull(member, "member cannot be null");

            var currentNode = context.CurrentNode;
            var lookupName = GetXmlLookupName(member);
            var filteredChildNodes = currentNode.Select(lookupName);

            return filteredChildNodes.Count > 1 ? filteredChildNodes : currentNode.SelectSingleNode(lookupName)?.SelectChildren(XPathNodeType.Element);
        }

        public string FetchAttributeValue(XmlContext context, MemberInfo member)
        {
            Assert.NotNull(context, "context cannot be null");
            Assert.NotNull(member, "member cannot be null");
            var xmlAttribute = member.GetCustomAttribute<XmlAttribute>();
            var attributeValue = GetAttributeValue(context, GetXmlAttributeLookupName(member));

            return string.IsNullOrEmpty(attributeValue) && xmlAttribute.DefaultValue != null
                ? xmlAttribute.DefaultValue.ToString()
                : attributeValue;
        }

        public string GetAttributeValue(XmlContext context, string lookupName)
        {
            return context.CurrentNode.GetAttribute(lookupName, "");
        }

        public bool HasNodeChildren(XPathNavigator node)
        {
            return node.SelectChildren(XPathNodeType.Element).Count > 0;
        }

        public string GetXmlLookupName(MemberInfo member)
        {
            return member.GetType().IsInstanceOfType(typeof(Type))
                ? LookupTypeName(member)
                : LookupPropertyName(member);
        }

        public string GetXmlAttributeLookupName(MemberInfo member)
        {
            var xmlAttribute = member.GetCustomAttribute<XmlAttribute>();

            if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Name))
                return xmlAttribute.Name;

            return member.Name;
        }

        private static string LookupTypeName(MemberInfo member)
        {
            var rootName = member.GetCustomAttribute<XmlRootName>();

            return rootName != null && !string.IsNullOrEmpty(rootName.Name) ? rootName.Name : member.Name;
        }

        private static string LookupPropertyName(MemberInfo member)
        {
            var xmlElement = member.GetCustomAttribute<XmlElement>();

            if (xmlElement != null && !string.IsNullOrEmpty(xmlElement.Name))
                return xmlElement.Name;

            return member.Name;
        }
    }
}