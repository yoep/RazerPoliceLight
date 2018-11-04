using System.Collections.Generic;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Effects
{
    public abstract class AbstractKeyboardEffect : AbstractEffect, IKeyboardEffect
    {
        protected AbstractKeyboardEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager)
            : base(rage, settingsManager, elsSettingsManager)
        {
        }

        #region Properties

        /// <inheritdoc />
        protected override List<EffectPattern> EffectPatterns => EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard);

        /// <inheritdoc />
        protected override bool IsDisabled => !_settingsManager.Settings.DeviceSettings.KeyboardSettings.IsEnabled;

        /// <inheritdoc />
        protected override bool IsScanModeEnabled => _settingsManager.Settings.DeviceSettings.KeyboardSettings.IsScanEnabled;

        #endregion
    }
}