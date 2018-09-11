﻿using Corale.Colore.Core;

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
                    IsScanEnabled = true
                },
                MouseSettings = new MouseSettings
                {
                    IsScanEnabled = true
                }
            }
        };

        public PlaybackSettings PlaybackSettings { get; set; }

        public ColorSettings ColorSettings { get; set; }
        
        public DeviceSettings DeviceSettings { get; set; }
    }
}