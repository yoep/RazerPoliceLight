using System;
using System.Collections.Generic;
using System.Linq;
using Rage;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Effects
{
    public class EffectsManager : IEffect
    {
        #region Constructors

        static EffectsManager()
        {
            Instance = new EffectsManager();
        }

        public EffectsManager()
        {
            DeviceEffects = new List<IEffect> {new KeyboardEffect(), new MouseEffect()};
        }

        #endregion

        #region Properties

        public static EffectsManager Instance { get; private set; }

        public List<IEffect> DeviceEffects { get; private set; }

        public bool IsPlaying => DeviceEffects.Any(e => e.IsPlaying);

        #endregion

        public void Play()
        {
            foreach (var deviceEffect in DeviceEffects)
            {
                deviceEffect.Play();
            }
        }

        public void Play(EffectPattern effectPattern)
        {
            var device = GetDevice(effectPattern.SupportedDevice);
            device.Play(effectPattern);
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

        private IEffect GetDevice(DeviceType deviceType)
        {
            return deviceType == DeviceType.Keyboard
                ? GetByType(typeof(KeyboardEffect))
                : GetByType(typeof(MouseEffect));
        }

        private IEffect GetByType(Type type)
        {
            return DeviceEffects.First(e => e.GetType() == type);
        }
    }
}