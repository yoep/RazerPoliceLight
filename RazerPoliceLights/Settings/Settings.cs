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
                        SpeedMulitplayer = 1
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