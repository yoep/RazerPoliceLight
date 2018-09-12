using System;
using System.Xml;
using Rage;

namespace RazerPoliceLights.Settings.Loaders
{
    public abstract class AbstractLoader
    {
        protected static string GetSingleNodeInnerValue(XmlNode document, string path)
        {
            var node = document.SelectSingleNode(path);

            if (node == null)
                throw new SettingsException(
                    path + " setting in the playback configuration is missing in the configuration file");

            var nodeValue = GetNodeInnerValue(node, path);

            return nodeValue;
        }

        protected static string GetNodeInnerValue(XmlNode node, string path)
        {
            var nodeValue = node.InnerText;

            if (IsEmpty(nodeValue))
                throw new SettingsException(path + " setting cannot be empty");
            return nodeValue;
        }

        protected static bool GetBoolAttributeValue(XmlNode node, string attribute, bool defaultValue)
        {
            var attributeStringValue = GetAttributeValue(node, attribute);
            bool attributeValue;

            if (bool.TryParse(attributeStringValue, out attributeValue))
            {
                return attributeValue;
            }

            Game.LogTrivial(attribute + " in " + node.Name + " is not a valid boolean value, using default instead");
            Game.DisplayNotification(node.Name + "." + attribute + " is not a valid configuration value, using " +
                                     defaultValue + " instead");
            return defaultValue;
        }

        protected static string GetAttributeValue(XmlNode node, string attributeName)
        {
            var xmlAttributeCollection = node.Attributes;

            if (xmlAttributeCollection == null)
                throw new SettingsException(node.Name + " setting doesn't have any attributes");

            for (var i = 0; i < xmlAttributeCollection.Count; i++)
            {
                var attribute = xmlAttributeCollection.Item(i);

                if (string.Equals(attribute.Name, attributeName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return attribute.Value;
                }
            }

            throw new SettingsException(node.Name + " setting is missing attribute '" + attributeName + "'");
        }

        protected static bool IsNotEmpty(string nodeInnerText)
        {
            return !IsEmpty(nodeInnerText);
        }

        protected static bool IsEmpty(string nodeInnerText)
        {
            return string.IsNullOrEmpty(nodeInnerText);
        }
    }
}