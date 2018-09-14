using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Rage;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Settings.Loaders
{
    public class EffectPatternLoad : AbstractLoader
    {
        private const string PatternsNodePath = "RazerPoliceLights/Patterns";
        private const string EffectNodePath = "Effect";
        private const string EffectSpeedAttribute = "Speed";
        private const string EffectNameAttribute = "Name";
        private const string EffectDeviceAttribute = "Device";

        public static Dictionary<DeviceType, List<EffectPattern>> Load(XmlNode document)
        {
            var patternsNode = document.SelectSingleNode(PatternsNodePath);

            if (patternsNode == null)
            {
                Game.DisplayNotification("No effect patterns found in configuration file, using defaults instead");
                return Settings.Defaults.EffectPatterns;
            }

            EffectPatternManager.Instance.Clear();

            foreach (XmlNode effectPatternNode in patternsNode.ChildNodes)
            {
                try
                {
                    var patternRows = new List<PatternRow>();
                    var effectName = GetAttributeValue(effectPatternNode, EffectNameAttribute);
                    var effectDeviceValue = GetAttributeValue(effectPatternNode, EffectDeviceAttribute);
                    DeviceType deviceType;

                    if (!Enum.TryParse(effectDeviceValue, out deviceType))
                    {
                        Game.LogTrivial("Effect '" + effectName + "' has an invalid device type " + effectDeviceValue);
                        DisplayNotification();
                        continue;
                    }

                    foreach (XmlNode effectNode in effectPatternNode.ChildNodes)
                    {
                        var patternColors = new List<ColorType>();
                        var effectPattern = GetNodeInnerValue(effectNode, EffectNodePath);
                        var speedValue = GetAttributeValue(effectNode, EffectSpeedAttribute);
                        double speed;

                        if (!double.TryParse(speedValue, NumberStyles.Float | NumberStyles.AllowThousands,
                            CultureInfo.InvariantCulture, out speed))
                        {
                            Game.LogTrivial("Effect '" + effectName + "' has an invalid pattern row speed");
                            DisplayNotification();
                        }

                        var patternTypes = effectPattern.Split(',');

                        foreach (var patternType in patternTypes)
                        {
                            GetColorType(patternType, patternColors, effectName);
                        }

                        patternRows.Add(new PatternRow(speed));
                    }

                    EffectPatternManager.Instance.AddEffect(
                        new EffectPattern(effectName, deviceType, patternRows.ToArray()));
                }
                catch (Exception e)
                {
                    Game.LogTrivial(e.Message + Environment.NewLine + e);
                    DisplayNotification();
                }
            }

            return new Dictionary<DeviceType, List<EffectPattern>>
            {
                {DeviceType.Keyboard, EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard)},
                {DeviceType.Mouse, EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse)}
            };
        }

        private static void GetColorType(string patternType, ICollection<ColorType> patternColors, string effectName)
        {
            switch (patternType)
            {
                case "0":
                    patternColors.Add(ColorType.OFF);
                    break;
                case "1":
                    patternColors.Add(ColorType.PRIMARY);
                    break;
                case "2":
                    patternColors.Add(ColorType.SECONDARY);
                    break;
                default:
                    Game.LogTrivial("Effect '" + effectName + "' has an invalid pattern row color type");
                    DisplayNotification();
                    break;
            }
        }

        private static void DisplayNotification()
        {
            Game.DisplayNotification("Razer Keyboard Police Lights has detected an issue with an effect pattern");
        }
    }
}