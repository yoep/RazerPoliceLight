using System;

namespace RazerPoliceLights.Xml.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Defines serialization and deserialization information for the xml root element.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class XmlRootElement : Attribute
    {
        public XmlRootElement(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Get the name of the xml root element.
        /// </summary>
        public string Name { get; }
    }
}