using System.Globalization;
using System.Xml;

namespace RazerPoliceLights.Settings.Loaders
{
    public class PlaybackSettingsLoader : AbstractLoader
    {
        private const string PlaybackNodePath = "RazerPoliceLights/Playback";
        private const string SpeedNodePath = "Speed";
        private const string LeaveLightsOnNodePath = "LeaveLightsOn";

        public static PlaybackSettings Load(XmlNode document)
        {
            var playbackNode = document.SelectSingleNode(PlaybackNodePath);

            if (playbackNode == null)
                throw new SettingsException("Playback settings configuration is missing in the configuration file");

            var speedModifierValue = GetSingleNodeInnerValue(playbackNode, SpeedNodePath);
            var leaveLightsOnValue = GetSingleNodeInnerValue(playbackNode, LeaveLightsOnNodePath);

            var speedModifier = double.Parse(speedModifierValue, CultureInfo.InvariantCulture);
            var leaveLightsOn = bool.Parse(leaveLightsOnValue);

            if (speedModifier < 0)
            {
                throw new SettingsException("Speed multiplayer cannot be smaller than 0");
            }

            return new PlaybackSettings
            {
                SpeedModifier = speedModifier,
                LeaveLightsOn = leaveLightsOn
            };
        }
    }
}