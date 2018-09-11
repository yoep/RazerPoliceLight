using System.Collections.Generic;

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
            DevicEffects = new List<IEffect> {new KeyboardEffect(), new MouseEffect()};
        }

        #endregion

        #region Getters & Setters

        public static EffectsManager Instance { get; private set; }

        public List<IEffect> DevicEffects { get; private set; }

        #endregion

        public void Play()
        {
            foreach (var devicEffect in DevicEffects)
            {
                devicEffect.Play();
            }
        }

        public void Stop()
        {
            foreach (var devicEffect in DevicEffects)
            {
                devicEffect.Stop();
            }
        }

        public void OnUnload(bool isTerminating)
        {
            foreach (var devicEffect in DevicEffects)
            {
                devicEffect.OnUnload(isTerminating);
            }
        }
    }
}