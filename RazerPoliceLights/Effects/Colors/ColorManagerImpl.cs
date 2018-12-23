using Corale.Colore.Core;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Effects.Colors
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
        public void Initialize(Settings.Settings settings)
        {
            if (settings.ColorSettings.ElsEnabled)
            {
                _colors = new ElsColors(_elsSettingsManager);
            }
            else
            {
                _colors = new PluginConfigColors(settings.ColorSettings);
            }
        }
    }
}