using System.Collections.Generic;
using System.Xml;
using Rage;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Settings.Loaders
{
    public class EffectPatternLoad : AbstractLoader
    {
        private const string PatternsNodePath = "RazerPoliceLights/Patterns";

        public static Dictionary<DeviceType, List<EffectPattern>> Load(XmlNode document)
        {
            var keyBoardEffects = new List<EffectPattern>();
            var mouseEffects = new List<EffectPattern>();

            var patternsNode = document.SelectSingleNode(PatternsNodePath);

            if (patternsNode == null)
            {
                Game.DisplayNotification("No effect patterns found in configuration file, using defaults instead");
                return Settings.Defaults.EffectPatterns;
            }
            
            foreach (XmlNode effectPatternNode in patternsNode.ChildNodes)
            {
                
            }

            return new Dictionary<DeviceType, List<EffectPattern>>
            {
                {DeviceType.Keyboard, keyBoardEffects},
                {DeviceType.Mouse, mouseEffects}
            };
        }
    }
}