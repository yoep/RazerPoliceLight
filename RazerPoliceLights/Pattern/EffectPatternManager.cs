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

        public static EffectPatternManager Instance { get; private set; }

        public List<EffectPattern> EffectPatterns { get; private set; }

        #endregion

        public EffectPattern GetByName(DeviceType deviceType, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name cannot be empty");
            return EffectPatterns.First(e => e.SupportedDevice == deviceType && e.Name == name);
        }

        public List<EffectPattern> GetByDevice(DeviceType deviceType)
        {
            return EffectPatterns.FindAll(e => e.SupportedDevice == deviceType);
        }

        public void AddEffect(EffectPattern effectPattern)
        {
            if (effectPattern == null)
                throw new ArgumentException("effectPattern cannot be null");
            EffectPatterns.Add(effectPattern);
        }

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