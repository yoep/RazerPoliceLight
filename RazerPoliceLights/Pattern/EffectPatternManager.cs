using System.Collections.Generic;
using RazerPoliceLights.Pattern.Predefined.Keyboard;

namespace RazerPoliceLights.Pattern
{
    public class EffectPatternManager
    {
        private readonly Dictionary<string, EffectPattern> _keyboardEffectPatterns =
            new Dictionary<string, EffectPattern>();

        private readonly Dictionary<string, EffectPattern> _mouseEffectPatterns =
            new Dictionary<string, EffectPattern>();

        #region Constructors

        static EffectPatternManager()
        {
            Instance = new EffectPatternManager();
        }

        private EffectPatternManager()
        {
            Init();
        }

        #endregion

        #region Getters & Setters

        public static EffectPatternManager Instance { get; private set; }

        public Dictionary<string, EffectPattern> KeyboardEffectPatterns => _keyboardEffectPatterns;
        
        public Dictionary<string, EffectPattern> MouEffectPatterns => _mouseEffectPatterns;

        #endregion

        public EffectPattern GetByName(DeviceType deviceType, string name)
        {
            return GetByName(deviceType == DeviceType.KEYBOARD ? _keyboardEffectPatterns : _mouseEffectPatterns, name);
        }

        private EffectPattern GetByName(IReadOnlyDictionary<string, EffectPattern> patterns, string name)
        {
            return patterns.ContainsKey(name) ? patterns[name] : null;
        }

        private void Init()
        {
            _keyboardEffectPatterns.Add("Alternate", Alternate.Get);
            _keyboardEffectPatterns.Add("AlternateAndFullFlash", AlternateAndFullFlash.Get);
            _keyboardEffectPatterns.Add("AlternateFlash", AlternateFlash.Get);
            _keyboardEffectPatterns.Add("EvenOdd", EvenOdd.Get);
            _keyboardEffectPatterns.Add("EvenOddFlash", EvenOddFlash.Get);

            _mouseEffectPatterns.Add("Alternate", Predefined.Mouse.Alternate.Get);
            _mouseEffectPatterns.Add("AlternateFlash", Predefined.Mouse.AlternateFlash.Get);
            _mouseEffectPatterns.Add("EvenOdd", Predefined.Mouse.EvenOdd.Get);
            _mouseEffectPatterns.Add("EvenOddFlash", Predefined.Mouse.EvenOddFlash.Get);
        }
    }
}