using System;
using System.Collections.Generic;
using System.Linq;
using Corale.Colore.Core;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Pattern.Predefined.Keyboard;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    [XmlRootName("RazerPoliceLights")]
    public class Settings
    {
        [XmlIgnore]
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
                    Patterns = EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard).Select(x => x.Name).ToList()
                },
                MouseSettings = new MouseSettings
                {
                    IsScanEnabled = true,
                    IsEnabled = true,
                    Patterns = EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse).Select(x => x.Name).ToList()
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

        [XmlElement(Name = "Playback")] public PlaybackSettings PlaybackSettings { get; set; }

        [XmlElement(Name = "Colors")] public ColorSettings ColorSettings { get; set; }

        [XmlElement(Name = "Devices")] public DeviceSettings DeviceSettings { get; set; }

        [XmlElement(Name = "Patterns/EffectPattern")]
        public Dictionary<DeviceType, List<EffectPattern>> EffectPatterns { get; set; }

        public override string ToString()
        {
            return $"{Environment.NewLine}---{nameof(PlaybackSettings)}---{Environment.NewLine}{PlaybackSettings}" +
                   $"{Environment.NewLine}---{nameof(ColorSettings)}---{Environment.NewLine}{ColorSettings}" +
                   $"{Environment.NewLine}---{nameof(DeviceSettings)}---{Environment.NewLine}{DeviceSettings}" +
                   $"{Environment.NewLine}---{nameof(EffectPatterns)}---" +
                   $"{Environment.NewLine}{DeviceType.Keyboard}: Total available patterns {EffectPatterns?[DeviceType.Keyboard].Count}" +
                   $"{Environment.NewLine}{DeviceType.Mouse}: Total available patterns {EffectPatterns?[DeviceType.Mouse].Count}";
        }

        protected bool Equals(Settings other)
        {
            return Equals(PlaybackSettings, other.PlaybackSettings) && Equals(ColorSettings, other.ColorSettings) &&
                   Equals(DeviceSettings, other.DeviceSettings) && Equals(EffectPatterns, other.EffectPatterns);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Settings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (PlaybackSettings != null ? PlaybackSettings.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ColorSettings != null ? ColorSettings.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DeviceSettings != null ? DeviceSettings.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EffectPatterns != null ? EffectPatterns.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}