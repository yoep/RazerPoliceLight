using System.Drawing;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase.Settings.Els;

namespace RazerPoliceLightsBase.Effects.Colors
{
    public class ColorManagerImpl : IColorManager
    {
        private readonly IElsSettingsManager _elsSettingsManager;
        private IColors _colors;

        public ColorManagerImpl(IElsSettingsManager elsSettingsManager)
        {
            _elsSettingsManager = elsSettingsManager;
        }

        /// <inheritdoc />
        public Color this[int index, int max] => _colors[index, max];

        /// <inheritdoc />
        public string VehicleName
        {
            get { return _colors.VehicleName; }
            set { _colors.VehicleName = value; }
        }

        /// <inheritdoc />
        public void Initialize(Settings.Settings settings)
        {
            if (settings.ColorSettings.ElsEnabled)
            {
                _colors = new ElsColors(_elsSettingsManager, settings.ColorSettings);
            }
            else
            {
                _colors = new PluginConfigColors(settings.ColorSettings);
            }
        }
    }
}