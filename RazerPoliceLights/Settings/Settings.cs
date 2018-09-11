using Corale.Colore.Core;

namespace RazerPoliceLights.Settings
{
    public class Settings
    {
        public static Settings Defaults
        {
            get
            {
                return new Settings
                {
                    PlaybackSettings = new PlaybackSettings
                    {
                        SpeedModifier = 1,
                        EnableOnFoot = false
                    },
                    ColorSettings = new ColorSettings
                    {
                        StandbyColor = Color.Red,
                        PrimaryColor = Color.Blue,
                        SecondaryColor = Color.Red
                    }
                };
            }
        }

        public PlaybackSettings PlaybackSettings { get; set; }

        public ColorSettings ColorSettings { get; set; }
    }
}