using System;
using System.Collections.Generic;
using System.Linq;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Devices;
using RazerPoliceLightsBase.Devices.Razer;
using RazerPoliceLightsBase.Pattern;

namespace RazerPoliceLightsBase.Effects
{
    public class EffectsManager : IEffectsManager
    {
        private readonly IEnumerable<IDeviceManager> _deviceManagers;
        private readonly ILogger _logger;

        #region Constructors

        // ReSharper disable SuggestBaseTypeForParameter
        public EffectsManager(IEnumerable<IDeviceManager> deviceManagers, ILogger logger)
        {
            _deviceManagers = deviceManagers;
            _logger = logger;
        }

        #endregion

        #region Properties

        private IEnumerable<IEffect> DeviceEffects => RetrieveDeviceEffects();

        /// <inheritdoc />
        public bool IsPlaying => DeviceEffects.Any(e => e.IsPlaying);

        /// <inheritdoc />
        public DeviceSdk DeviceSdk => DeviceSdk.None;

        #endregion

        #region Methods

        public void Play(string vehicleName)
        {
            foreach (var deviceEffect in DeviceEffects)
            {
                deviceEffect.Play(vehicleName);
            }
        }

        public void Play(string vehicleName, EffectPattern effectPattern)
        {
            var device = GetDevice(effectPattern.SupportedDevice);
            device.Play(vehicleName, effectPattern);
        }

        public void Stop()
        {
            foreach (var deviceEffect in DeviceEffects)
            {
                deviceEffect.Stop();
            }
        }

        public void OnUnload(bool isTerminating)
        {
            _logger.Debug("Stopping all device effect threads");
            foreach (var deviceEffect in DeviceEffects)
            {
                deviceEffect.OnUnload(isTerminating);
            }
        }

        public void Initialize()
        {
            _logger.Debug("Initializing all device effects");
            foreach (var deviceEffect in DeviceEffects)
            {
                deviceEffect.Initialize();
            }
        }

        #endregion

        #region Functions

        private IEffect GetDevice(DeviceType deviceType)
        {
            return deviceType == DeviceType.Keyboard
                ? GetByType(typeof(RazerKeyboardEffect))
                : GetByType(typeof(RazerMouseEffect));
        }

        private IEffect GetByType(Type type)
        {
            return DeviceEffects.First(e => e.GetType() == type);
        }

        private IEnumerable<IEffect> RetrieveDeviceEffects()
        {
            var effects = new List<IEffect>();

            foreach (var deviceManager in _deviceManagers)
            {
                effects.AddRange(deviceManager.Devices);
            }

            return effects;
        }

        #endregion
    }
}