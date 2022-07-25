using Colore;
using Colore.Data;
using RazerPoliceLightsBase.AbstractionLayer;

namespace RazerPoliceLightsBase.Devices.Razer
{
    public static class RazerUtils
    {
        private const int InstanceCreationTimeout = 5000;

        private static IChroma _instance;

        public static IChroma Instance()
        {
            var logger = IoC.Instance.GetInstance<ILogger>();
            if (_instance != null)
            {
                logger.Trace("Returning initialized Chroma SDK instance");
                return _instance;
            }

            logger.Trace("Creating new Chroma SDK instance");
            var createTask = ColoreProvider.CreateNativeAsync();

            if (createTask.Wait(InstanceCreationTimeout))
            {
                var instance = createTask.Result;
                _instance = instance;

                return instance;
            }

            logger.Error("Chroma SDK instance creation timed out");
            return null;
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