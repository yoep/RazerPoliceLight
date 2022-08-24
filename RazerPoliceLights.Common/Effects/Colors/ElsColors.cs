using System.Drawing;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;

namespace RazerPoliceLights.Effects.Colors
{
    /// <summary>
    /// Colors based on the ELS configuration.
    /// </summary>
    public class ElsColors : IColors
    {
        private readonly IElsSettingsManager _elsSettingsManager;
        private readonly ColorSettings _colorSettings;

        /// <summary>
        /// Initialize a new instance of ElsColor.
        /// </summary>
        /// <param name="elsSettingsManager">The ELS settings manager to use for loading ELS vehicle data.</param>
        /// <param name="colorSettings">The fallback color settings to use for when no vehicle data could be found.</param>
        public ElsColors(IElsSettingsManager elsSettingsManager, ColorSettings colorSettings)
        {
            _elsSettingsManager = elsSettingsManager;
            _colorSettings = colorSettings;
        }

        /// <inheritdoc />
        public Color this[int index, int max]
        {
            get
            {
                var vehicleSettings = _elsSettingsManager.GetByName(VehicleName);
                var average = max / 2;

                // verify if the vehicle settings could be found
                // otherwise, use the default configured colors
                if (vehicleSettings == null)
                    return index < average ? _colorSettings.PrimaryColor : _colorSettings.SecondaryColor;

                var lightingSettings = vehicleSettings.ElsSettings.LightingSettings;

                return index < average
                    ? lightingSettings.GetColorForIndex(index)
                    : lightingSettings.GetColorForIndex(index - average + 3);
            }
        }

        /// <inheritdoc />
        public string VehicleName { get; set; }
    }
}