using System;
using System.Collections.Generic;
using System.Linq;
using RazerPoliceLights.Pattern.Predefined.Keyboard;

namespace RazerPoliceLights.Pattern
{
    public class EffectPatternManager
    {
        #region Constructors

        static EffectPatternManager()
        {
            Instance = new EffectPatternManager();
        }

        private EffectPatternManager()
        {
            EffectPatterns = new List<EffectPattern>();
            Init();
        }

        #endregion

        #region Properties

        public static EffectPatternManager Instance { get; }

        public List<EffectPattern> EffectPatterns { get; }

        #endregion

        public EffectPattern GetByName(DeviceType deviceType, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");
            return EffectPatterns.FirstOrDefault(e => e.SupportedDevice == deviceType &&
                                                      e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public List<EffectPattern> GetByDevice(DeviceType deviceType)
        {
            return EffectPatterns.FindAll(e => e.SupportedDevice == deviceType);
        }

        public void AddEffect(EffectPattern effectPattern)
        {
            Assert.NotNull(effectPattern, "effectPattern cannot be null");
            EffectPatterns.Add(effectPattern);
        }

        /// <summary>
        /// Add the given range of effect patterns to this manager.
        /// </summary>
        /// <param name="effectPatterns">Set the list of effect to add.</param>
        public void AddAll(List<EffectPattern> effectPatterns)
        {
            Assert.NotNull(effectPatterns, "effectPatterns cannot be null");
            EffectPatterns.AddRange(effectPatterns);
        }

        /// <summary>
        /// Clear all effects stored in this effect manager.
        /// </summary>
        public void Clear()
        {
            EffectPatterns.Clear();
        }

        private void Init()
        {
            EffectPatterns.AddRange(
                new List<EffectPattern>
                {
                    Alternate.Get,
                    AlternateAndFullFlash.Get,
                    AlternateFlash.Get,
                    EvenOdd.Get,
                    EvenOddFlash.Get
                });
            EffectPatterns.AddRange(
                new List<EffectPattern>
                {
                    Predefined.Mouse.Alternate.Get,
                    Predefined.Mouse.AlternateFlash.Get,
                    Predefined.Mouse.EvenOdd.Get,
                    Predefined.Mouse.EvenOddFlash.Get
                });
        }
    }
}