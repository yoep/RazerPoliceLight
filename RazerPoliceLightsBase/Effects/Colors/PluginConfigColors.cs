using Corale.Colore.Core;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsRage.Effects.Colors;

namespace RazerPoliceLightsBase.Effects.Colors
{
    /// <summary>
    /// Colors based on the plugin configuration.
    /// </summary>
    public class PluginConfigColors : IColors
    {
        private readonly ColorSettings _colorSettings;

        public PluginConfigColors(ColorSettings colorSettings)
        {
            _colorSettings = colorSettings;
        }

        /// <inheritdoc />
        public string VehicleName { get; set; }

        /// <inheritdoc />
        public Color this[int index, int max] => index < max / 2 ? _colorSettings.PrimaryColor : _colorSettings.SecondaryColor;
    }
}