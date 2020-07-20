using System.Collections.Generic;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsRage.Effects.Colors;

namespace RazerPoliceLightsBase.Effects
{
    public abstract class AbstractKeyboardEffect : AbstractEffect, IKeyboardEffect
    {
        protected AbstractKeyboardEffect(INotification notification, ILogger logger, ISettingsManager settingsManager,
            IColorManager colorManager)
            : base(notification, logger, settingsManager, colorManager)
        {
        }

        #region Properties

        /// <inheritdoc />
        public override bool IsDisabled => !SettingsManager.Settings.DeviceSettings.KeyboardSettings.IsEnabled;

        /// <inheritdoc />
        protected override List<EffectPattern> EffectPatterns =>
            EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard);

        /// <inheritdoc />
        protected override bool IsScanModeEnabled =>
            SettingsManager.Settings.DeviceSettings.KeyboardSettings.IsScanEnabled;

        #endregion
    }
}