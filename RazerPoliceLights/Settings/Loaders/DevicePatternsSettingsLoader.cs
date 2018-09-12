using System.Collections.Generic;
using System.Xml;
using Rage;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Settings.Loaders
{
    public class DevicePatternsSettingsLoader : AbstractLoader
    {
        private const string PatternsNodePath = "Patterns";

        public static List<EffectPattern> Load(XmlNode deviceNode, DeviceType deviceType)
        {
            var effectPatterns = new List<EffectPattern>();

            var patternsNode = deviceNode.SelectSingleNode(PatternsNodePath);

            if (patternsNode == null)
                throw new SettingsException("Pattern settings are missing in the configuration for device " +
                                            deviceNode.Name);

            foreach (XmlNode patternNode in patternsNode.ChildNodes)
            {
                var nodeValue = GetNodeInnerValue(patternNode, patternNode.Name);
                var effectPattern = EffectPatternManager.Instance.GetByName(deviceType, nodeValue);

                if (effectPattern != null)
                {
                    effectPatterns.Add(effectPattern);
                }
                else
                {
                    Game.LogTrivial(GetMessageNotFound(deviceType, nodeValue));
                    Game.DisplayNotification(GetMessageNotFound(deviceType, nodeValue));
                }
            }

            return effectPatterns;
        }

        private static string GetMessageNotFound(DeviceType deviceType, string nodeValue)
        {
            return nodeValue + " pattern doesn't exist, ignoring pattern for device " + deviceType;
        }
    }
}