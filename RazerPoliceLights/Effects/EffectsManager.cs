using System;
using System.Collections.Generic;
using System.Linq;
using Rage;
using RazerPoliceLights.Devices;
using RazerPoliceLights.Devices.Razer;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Effects
{
    public class EffectsManager : IEffectsManager
    {
        private readonly IDeviceManager _deviceManager;

        #region Constructors

        // ReSharper disable SuggestBaseTypeForParameter
        public EffectsManager(IDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }

        #endregion

        #region Properties

        private IEnumerable<IEffect> DeviceEffects => new List<IEffect> {_deviceManager.KeyboardDevice, _deviceManager.MouseDevice};

        public bool IsPlaying => DeviceEffects.Any(e => e.IsPlaying);

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
            Game.LogTrivialDebug("Stopping all device effect threads");
            foreach (var deviceEffect in DeviceEffects)
            {
                deviceEffect.OnUnload(isTerminating);
            }
        }

        public void Initialize()
        {
            Game.LogTrivialDebug("Initializing all device effects");
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

        #endregion
    }
}