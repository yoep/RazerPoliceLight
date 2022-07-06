using RazerPoliceLights.Xml.Context;
using RazerPoliceLightsBase.Xml.Parser;

namespace RazerPoliceLightsBase.Xml
{
    public interface IXmlDeserializer : IXmlSerialization
    {
        /// <summary>
        /// Deserialize the given context.
        /// </summary>
        /// <param name="parser">Set the parser which can be used during deserialization.</param>
        /// <param name="deserializationContext">Set the context which needs to be deserialized.</param>
        /// <returns>Returns deserialized context result.</returns>
        object Deserialize(XmlParser parser, XmlDeserializationContext deserializationContext);
    }
}