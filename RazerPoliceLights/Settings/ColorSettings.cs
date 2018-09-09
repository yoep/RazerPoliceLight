using Corale.Colore.Core;

namespace RazerPoliceLights.Settings
{
    public class ColorSettings
    {
        #region Constructors

        static ColorSettings()
        {
            Instance = new ColorSettings();
        }

        private ColorSettings()
        {
            PrimaryColor = Color.Blue;
            SecondaryColor = Color.Blue;
            DefaultColor = Color.Red;
        }

        #endregion

        public static ColorSettings Instance { get; private set; }

        public Color PrimaryColor { get; private set; }

        public Color SecondaryColor { get; private set; }
        
        public Color DefaultColor { get; private set; }
    }
}