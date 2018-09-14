using System;
using System.Collections.Generic;
using Corale.Colore.Core;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Pattern.Predefined.Keyboard;

namespace RazerPoliceLights.Settings
{
    public class Settings
    {
        public static Settings Defaults => new Settings
        {
            PlaybackSettings = new PlaybackSettings
            {
                SpeedModifier = 1,
                LeaveLightsOn = false
            },
            ColorSettings = new ColorSettings
            {
                StandbyColor = Color.Red,
                PrimaryColor = Color.Blue,
                SecondaryColor = Color.Red
            },
            DeviceSettings = new DeviceSettings
            {
                KeyboardSettings = new KeyboardSettings
                {
                    IsScanEnabled = true,
                    IsEnabled = true,
                    EffectPatterns = EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard)
                },
                MouseSettings = new MouseSettings
                {
                    IsScanEnabled = true,
                    IsEnabled = true,
                    EffectPatterns = EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse)
                }
            },
            EffectPatterns = new Dictionary<DeviceType, List<EffectPattern>>
            {
                {
                    DeviceType.Keyboard, new List<EffectPattern>
                    {
                        Alternate.Get,
                        AlternateAndFullFlash.Get,
                        AlternateFlash.Get,
                        EvenOdd.Get,
                        EvenOddFlash.Get
                    }
                },
                {
                    DeviceType.Mouse, new List<EffectPattern>
                    {
                        Pattern.Predefined.Mouse.Alternate.Get,
                        Pattern.Predefined.Mouse.AlternateFlash.Get,
                        Pattern.Predefined.Mouse.EvenOdd.Get,
                        Pattern.Predefined.Mouse.EvenOddFlash.Get
                    }
                }
            }
        };

        public PlaybackSettings PlaybackSettings { get; set; }

        public ColorSettings ColorSettings { get; set; }

        public DeviceSettings DeviceSettings { get; set; }

        public Dictionary<DeviceType, List<EffectPattern>> EffectPatterns { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}---{nameof(PlaybackSettings)}---{Environment.NewLine}{PlaybackSettings}" +
                   $"{Environment.NewLine}---{nameof(ColorSettings)}---{Environment.NewLine}{ColorSettings}" +
                   $"{Environment.NewLine}---{nameof(DeviceSettings)}---{Environment.NewLine}{DeviceSettings}" +
                   $"{Environment.NewLine}---{nameof(EffectPatterns)}---" +
                   $"{Environment.NewLine}{DeviceType.Keyboard}: Total available patterns {EffectPatterns[DeviceType.Keyboard].Count}" +
                   $"{Environment.NewLine}{DeviceType.Mouse}: Total available patterns {EffectPatterns[DeviceType.Mouse].Count}";
        }
    }
}