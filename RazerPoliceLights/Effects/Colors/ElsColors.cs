using Corale.Colore.Core;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Effects.Colors
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
            _elsSettingsManager.Load();
        }

        public Color this[int index, int max]
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}