using System.Collections.Generic;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsRage.Effects.Colors;

namespace RazerPoliceLightsBase.Effects
{
    public abstract class AbstractMouseEffect : AbstractEffect, IMouseEffect
    {
        protected AbstractMouseEffect(INotification notification, ILogger logger, ISettingsManager settingsManager,
            IColorManager colorManager)
            : base(notification, logger, settingsManager, colorManager)
        {
        }

        #region Properties

        /// <inheritdoc />
        public override bool IsDisabled => !SettingsManager.Settings.DeviceSettings.MouseSettings.IsEnabled;

        /// <inheritdoc />
        protected override List<EffectPattern> EffectPatterns =>
            EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse);

        /// <inheritdoc />
        protected override bool IsScanModeEnabled =>
            SettingsManager.Settings.DeviceSettings.MouseSettings.IsScanEnabled;

        protected bool IsAnimateVerticallyEnabled =>
            SettingsManager.Settings.DeviceSettings.MouseSettings.AnimateVertically;

        #endregion
    }
}