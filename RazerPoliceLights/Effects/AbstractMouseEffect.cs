using System.Collections.Generic;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public abstract class AbstractMouseEffect : AbstractEffect, IMouseEffect
    {
        protected AbstractMouseEffect(IRage rage, ISettingsManager settingsManager, IColorManager colorManager) 
            : base(rage, settingsManager, colorManager)
        {
        }

        #region Properties

        /// <inheritdoc />
        protected override List<EffectPattern> EffectPatterns => EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse);

        /// <inheritdoc />
        protected override bool IsDisabled => !SettingsManager.Settings.DeviceSettings.MouseSettings.IsEnabled;

        /// <inheritdoc />
        protected override bool IsScanModeEnabled => SettingsManager.Settings.DeviceSettings.MouseSettings.IsScanEnabled;

        protected bool IsAnimateVerticallyEnabled => SettingsManager.Settings.DeviceSettings.MouseSettings.AnimateVertically;

        #endregion
    }
}