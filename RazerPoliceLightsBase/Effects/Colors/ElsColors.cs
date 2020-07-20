using Corale.Colore.Core;
using RazerPoliceLightsBase.Settings.Els;

namespace RazerPoliceLightsRage.Effects.Colors
{
    /// <summary>
    /// Colors based on the ELS configuration.
    /// </summary>
    public class ElsColors : IColors
    {
        private readonly IElsSettingsManager _elsSettingsManager;

        public ElsColors(IElsSettingsManager elsSettingsManager)
        {
            _elsSettingsManager = elsSettingsManager;
        }

        /// <inheritdoc />
        public Color this[int index, int max]
        {
            get
            {
                var vehicleSettings = _elsSettingsManager.GetByName(VehicleName);
                var lightingSettings = vehicleSettings.ElsSettings.LightingSettings;
                var average = max / 2;

                return index < average
                    ? lightingSettings.GetColorForIndex(index)
                    : lightingSettings.GetColorForIndex(index - average + 3);
            }
        }

        /// <inheritdoc />
        public string VehicleName { get; set; }
    }
}