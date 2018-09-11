using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Corale.Colore.Core;
using Rage;

namespace RazerPoliceLights.Settings
{
    public class SettingsManager
    {
        private const string FileName = @"./Plugins/RazerPoliceLights.xml";

        #region Constructors

        static SettingsManager()
        {
            Instance = new SettingsManager();
        }

        private SettingsManager()
        {
        }

        #endregion

        #region Getters & Setters

        public static SettingsManager Instance { get; private set; }

        public Settings Settings { get; private set; }

        #endregion

        #region Methods

        public void Load()
        {
            var document = new XmlDocument();

            try
            {
                document.Load(FileName);

                Settings = new Settings
                {
                    PlaybackSettings = ReadPlaybackSettings(document),
                    ColorSettings = ReadColorSettings(document)
                };
            }
            catch (FileNotFoundException)
            {
                Game.DisplayNotification("Razer Police Lights configuration file not found, using defaults instead");
                LoadDefauls();
            }
            catch (Exception e)
            {
                Game.LogTrivial(e.Message + Environment.NewLine + e.StackTrace);
                Game.DisplayNotification("Razer Police Lights configuration file is not valid, using defaults instead");
                LoadDefauls();
            }
        }

        #endregion

        private ColorSettings ReadColorSettings(XmlNode document)
        {
            var colorsNode = document.SelectSingleNode("RazerPoliceLights/Colors");

            if (colorsNode == null)
                throw new SettingsException("Color settings configuration is missing in the configuration file");

            return new ColorSettings
            {
                StandbyColor = GetColorFromNode(colorsNode.SelectSingleNode("Standby")),
                PrimaryColor = GetColorFromNode(colorsNode.SelectSingleNode("Primary")),
                SecondaryColor = GetColorFromNode(colorsNode.SelectSingleNode("Secondary"))
            };
        }

        private PlaybackSettings ReadPlaybackSettings(XmlNode document)
        {
            var playbackNode = document.SelectSingleNode("RazerPoliceLights/Playback");

            if (playbackNode == null)
                throw new SettingsException("Playback settings configuration is missing in the configuration file");

            var speedMultiplayerNode = playbackNode.SelectSingleNode("Speed");
            var enableOnFootNode = playbackNode.SelectSingleNode("EnableOnFoot");

            if (speedMultiplayerNode == null)
                throw new SettingsException(
                    "Speed multiplayer setting in the playback configuration is missing in the configuration file");
            if (speedMultiplayerNode == null)
                throw new SettingsException(
                    "Speed multiplayer setting in the playback configuration is missing in the configuration file");

            var speedModifierNode = double.Parse(speedMultiplayerNode.InnerText, CultureInfo.InvariantCulture);
            var enableOnFoot = bool.Parse(enableOnFootNode.InnerText);

            if (speedModifierNode < 0)
            {
                throw new SettingsException("Speed multiplayer cannot be smaller than 0");
            }

            return new PlaybackSettings
            {
                SpeedModifier = speedModifierNode,
                EnableOnFoot = enableOnFoot
            };
        }

        private Color GetColorFromNode(XmlNode node)
        {
            if (node != null)
            {
                var nodeInnerText = node.InnerText;

                if (!string.IsNullOrEmpty(nodeInnerText))
                {
                    return ConvertTextToColor(nodeInnerText);
                }

                var redValue = GetColorValueFromAttribute(node, "R");
                var greenValue = GetColorValueFromAttribute(node, "G");
                var blueValue = GetColorValueFromAttribute(node, "B");

                return new Color(redValue, greenValue, blueValue);
            }

            throw new SettingsException("Missing color setting in color settings section");
        }

        private byte GetColorValueFromAttribute(XmlNode node, string attributeName)
        {
            var colorValue = byte.Parse(GetAttributeValue(node, attributeName));

            if (colorValue <= 255)
                return colorValue;

            throw new UnknownColorSettingException(colorValue + " color value for color setting attribute" +
                                                   attributeName + " is not between 0 and 255");
        }

        private string GetAttributeValue(XmlNode node, string attributeName)
        {
            var xmlAttributeCollection = node.Attributes;

            if (xmlAttributeCollection != null)
            {
                for (var i = 0; i < xmlAttributeCollection.Count; i++)
                {
                    var attribute = xmlAttributeCollection.Item(i);

                    if (string.Equals(attribute.Name, attributeName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return attribute.Value;
                    }
                }

                throw new SettingsException("Color setting is missing attribute '" + attributeName + "'");
            }

            throw new SettingsException("Color setting doesn't have any attributes");
        }

        private Color ConvertTextToColor(string nodeInnerText)
        {
            switch (nodeInnerText.ToLower())
            {
                case "black":
                    return Color.Black;
                case "blue":
                    return Color.Blue;
                case "red":
                    return Color.Red;
                case "orange":
                    return Color.Orange;
                case "yellow":
                    return Color.Yellow;
                case "white":
                    return Color.White;
                default:
                    throw new SettingsException(nodeInnerText + " is not a valid color value");
            }
        }

        private void LoadDefauls()
        {
            Settings = Settings.Defaults;
        }
    }
}