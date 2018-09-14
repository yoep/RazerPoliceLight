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

                        if (patternTypes.Length > 0)
                        {
                            patternRows.Add(new PatternRow(speed, GetPatternColors(patternTypes)));
                        }
                        else
                        {
                            Game.LogTrivial("Effect pattern cannot have 0 columns, ignoring effect line");
                            DisplayNotification();
                        }
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

        private static ColorType[] GetPatternColors(IEnumerable<string> patternTypes)
        {
            var patternColors = new List<ColorType>();

            foreach (var patternType in patternTypes)
            {
                try
                {
                    patternColors.Add(GetColorType(patternType));
                }
                catch (ColorTypeException e)
                {
                    Game.LogTrivial(e.Message + Environment.NewLine + e);
                    DisplayNotification();
                }
            }

            return patternColors.ToArray();
        }

        private static ColorType GetColorType(string patternType)
        {
            switch (patternType)
            {
                case "0":
                    return ColorType.OFF;
                case "1":
                    return ColorType.PRIMARY;
                case "2":
                    return ColorType.SECONDARY;
                default:
                    throw new ColorTypeException("Pattern type '" + patternType +
                                                 "' is invalid, see manual for allowed values");
            }
        }

        private static void DisplayNotification()
        {
            Game.DisplayNotification("Razer Keyboard Police Lights has detected an issue with an effect pattern");
        }
    }
}