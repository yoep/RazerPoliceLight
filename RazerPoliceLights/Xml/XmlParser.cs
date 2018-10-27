using System;
using System.Reflection;
using System.Xml.XPath;
using RazerPoliceLights.Xml.Attributes;
using RazerPoliceLights.Xml.Context;

namespace RazerPoliceLights.Xml
{
    public class XmlParser
    {
        public XPathNavigator FetchNodeForMember(XmlContext context, MemberInfo member)
        {
            Assert.NotNull(context, "context cannot be null");
            Assert.NotNull(member, "member cannot be null");

            return context.CurrentNode.SelectSingleNode(GetXmlLookupName(member));
        }


        public string FetchAttributeValue(XmlContext context, MemberInfo member)
        {
            Assert.NotNull(context, "context cannot be null");
            Assert.NotNull(member, "member cannot be null");

            return GetAttributeValue(context, GetXmlAttributeLookupName(member));
        }

        public string GetAttributeValue(XmlContext context, string lookupName)
        {
            return context.CurrentNode.GetAttribute(lookupName, "");
        }

        public static string GetXmlLookupName(MemberInfo member)
        {
            return member.GetType().IsInstanceOfType(typeof(Type))
                ? LookupTypeName(member)
                : LookupPropertyName(member);
        }

        public static string GetXmlAttributeLookupName(MemberInfo member)
        {
            var xmlAttribute = member.GetCustomAttribute<XmlAttribute>();

            if (xmlAttribute != null && !string.IsNullOrEmpty(xmlAttribute.Name))
                return xmlAttribute.Name;

            return member.Name;
        }

        private static string LookupTypeName(MemberInfo member)
        {
            var rootName = member.GetCustomAttribute<XmlRootElement>();

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