using Colore;
using Colore.Data;

namespace RazerPoliceLightsBase.Devices.Razer
{
    public static class RazerUtils
    {
        private static IChroma _instance;

        public static IChroma Instance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            var instance = ColoreProvider.CreateNativeAsync().Result;
            _instance = instance;
            return instance;
        }
        
        /// <summary>
        /// Convert the given system drawing color to a Chroma color.
        /// </summary>
        /// <param name="color">The system color.</param>
        /// <returns>Returns the converted Chroma color.</returns>
        public static Color Convert(System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B);
        }
    }
}