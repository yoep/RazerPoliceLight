using System;
using System.Linq;
using Corale.Colore.Core;
using RazerPoliceLights.Pattern;

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
                    EffectPatterns = EffectPatternManager.Instance.KeyboardEffectPatterns.Values.ToList()
                },
                MouseSettings = new MouseSettings
                {
                    IsScanEnabled = true,
                    IsEnabled = true,
                    EffectPatterns = EffectPatternManager.Instance.MouEffectPatterns.Values.ToList()
                }
            }
        };

        public PlaybackSettings PlaybackSettings { get; set; }

        public ColorSettings ColorSettings { get; set; }

        public DeviceSettings DeviceSettings { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}---{nameof(PlaybackSettings)}---{Environment.NewLine}{PlaybackSettings}" +
                   $"{Environment.NewLine}---{nameof(ColorSettings)}---{Environment.NewLine}{ColorSettings}" +
                   $"{Environment.NewLine}---{nameof(DeviceSettings)}---{Environment.NewLine}{DeviceSettings}";
        }
    }
}