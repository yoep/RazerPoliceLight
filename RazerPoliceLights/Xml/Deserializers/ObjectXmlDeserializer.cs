using System;
using System.Reflection;
using System.Xml;
using RazerPoliceLights.Xml.Attributes;
using RazerPoliceLights.Xml.Context;
using XmlAttribute = RazerPoliceLights.Xml.Attributes.XmlAttribute;
using XmlElement = RazerPoliceLights.Xml.Attributes.XmlElement;

namespace RazerPoliceLights.Xml.Deserializers
{
    public class ObjectXmlDeserializer : IXmlDeserializer
    {
        public object deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            var instance = Activator.CreateInstance(deserializationContext.DeserializationType);

            foreach (var property in deserializationContext.DeserializationType.GetProperties())
            {
                if (!IsNotIgnored(property))
                    continue;

                if (GetElementAnnotation(property) != null && GetAttributeAnnotation(property) != null)
                    throw new XmlException("Property " + property.Name + " cannot be annotated with both " +
                                           typeof(XmlElement).FullName + " and " + typeof(XmlAttribute).FullName);

                var value = IsXmlElement(property)
                    ? ProcessElement(parser, deserializationContext, property)
                    : ProcessAttribute(parser, deserializationContext, property);

                property.SetValue(instance, value);
            }

            return instance;
        }

        private static object ProcessElement(
            XmlParser parser,
            XmlDeserializationContext deserializationContext,
            PropertyInfo property)
        {
            var node = parser.FetchNodeForMember(deserializationContext, property);

            if (node == null && IsRequiredMember(property))
                throw new XmlException("Missing xml node for " + XmlParser.GetXmlLookupName(property));

            return deserializationContext.Deserialize(parser, node, property.PropertyType);
        }

        private static object ProcessAttribute(XmlParser parser, XmlContext deserializationContext,
            PropertyInfo property)
        {
            var value = parser.FetchAttributeValue(deserializationContext, property);
            var type = property.PropertyType;

            if (type == typeof(bool))
                return bool.Parse(value);

            if (type == typeof(double))
                return double.Parse(value);

            if (type == typeof(float))
                return float.Parse(value);

            if (type == typeof(int))
                return int.Parse(value);

            if (type.IsEnum)
                return ProcessEnum(value, type);

            if (type == typeof(Array))
                throw new DeserializationException("Attribute cannot be of type Array");

            return value;
        }

        private static object ProcessEnum(string value, Type type)
        {
            foreach (var enumValue in type.GetEnumValues())
            {
                if (enumValue.ToString().Equals(value, StringComparison.InvariantCultureIgnoreCase))
                    return enumValue;
            }

            throw new XmlException("Enumeration value " + value + " could not be found for " + type.Name);
        }

        public bool CanHandle(Type type)
        {
            return !IsNativeType(type);
        }

        private static bool IsRequiredMember(MemberInfo member)
        {
            var xmlProperty = member.GetCustomAttribute<XmlElement>();

            return xmlProperty == null || !xmlProperty.IsOptional;
        }

        private static bool IsNotIgnored(MemberInfo member)
        {
            var xmlIgnore = member.GetCustomAttribute<XmlIgnore>();

            return xmlIgnore == null;
        }

        private static bool IsNativeType(Type type)
        {
            var ns = type.Namespace;
            return !string.IsNullOrEmpty(ns) && ns.Equals("System");
        }

        private static bool IsXmlElement(MemberInfo member)
        {
            return GetAttributeAnnotation(member) == null;
        }

        private static XmlAttribute GetAttributeAnnotation(MemberInfo member)
        {
            return member.GetCustomAttribute<XmlAttribute>();
        }

        private static XmlElement GetElementAnnotation(MemberInfo member)
        {
            return member.GetCustomAttribute<XmlElement>();
        }
    }
}