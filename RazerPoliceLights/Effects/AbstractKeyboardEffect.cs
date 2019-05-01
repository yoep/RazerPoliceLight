using System.Collections.Generic;
using RazerPoliceLights.AbstractionLayer;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public abstract class AbstractKeyboardEffect : AbstractEffect, IKeyboardEffect
    {
        protected AbstractKeyboardEffect(IRage rage, ILogger logger, ISettingsManager settingsManager, IColorManager colorManager) 
            : base(rage, logger, settingsManager, colorManager)
        {
        }

        #region Properties

        /// <inheritdoc />
        public override bool IsDisabled => !SettingsManager.Settings.DeviceSettings.KeyboardSettings.IsEnabled;

        /// <inheritdoc />
        protected override List<EffectPattern> EffectPatterns => EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard);

        /// <inheritdoc />
        protected override bool IsScanModeEnabled => SettingsManager.Settings.DeviceSettings.KeyboardSettings.IsScanEnabled;

        #endregion
    }
}