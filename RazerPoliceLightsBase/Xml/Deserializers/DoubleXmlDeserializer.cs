using System;
using System.Globalization;
using RazerPoliceLightsBase.Xml;
using RazerPoliceLightsBase.Xml.Parser;
using RazerPoliceLightsRage.Xml.Context;

namespace RazerPoliceLightsRage.Xml.Deserializers
{
    public class DoubleXmlDeserializer : IXmlDeserializer
    {
        public object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext)
        {
            return !string.IsNullOrEmpty(deserializationContext.Value)
                ? double.Parse(deserializationContext.Value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture)
                : deserializationContext.CurrentNode.ValueAsDouble;
        }

        public bool CanHandle(Type type)
        {
            return type == typeof(double);
        }
    }
}