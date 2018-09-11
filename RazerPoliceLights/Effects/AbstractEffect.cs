using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Corale.Colore.Core;
using Rage;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public abstract class AbstractEffect : IEffect
    {
        protected int EffectCursor;

        private readonly List<EffectPattern> _effectPatterns;
        private readonly Settings.Settings _settings;
        private Thread _effectThread;
        private EffectPattern _currentPlayingEffect;
        private bool _isEffectRunning;

        protected AbstractEffect(List<EffectPattern> effectPatterns)
        {
            _effectPatterns = effectPatterns;
            _settings = SettingsManager.Instance.Settings;
        }

        public void Play()
        {
            _isEffectRunning = true;
            _effectThread = new Thread(() =>
            {
                try
                {
                    while (_isEffectRunning)
                    {
                        OnEffectTick();
                        Thread.Sleep((int) (100 * _settings.PlaybackSettings.SpeedModifier));
                    }
                }
                catch (Exception exception)
                {
                    Game.LogTrivial(exception.Message + Environment.NewLine + exception.StackTrace);
                    Game.DisplayNotification("Razer Police Lights Keyboard plugin thread stopped responding");
                }
            }) {IsBackground = true};
            _effectThread.Start();
        }

        public void Stop()
        {
            //End the thread by killing the infinite loop running in the thread
            _isEffectRunning = false;
            OnEffectStop();
        }

        public void OnUnload(bool isTerminating)
        {
            _isEffectRunning = false;
            if (isTerminating)
                _effectThread.Abort();
        }

        protected EffectPattern GetEffectPattern()
        {
            var random = new Random();

            if (_currentPlayingEffect == null)
            {
                _currentPlayingEffect = _effectPatterns.ElementAt(0);
                return _currentPlayingEffect;
            }

            if (EffectCursor == 0)
            {
                if (random.Next(0, 2) == 1)
                {
                    _currentPlayingEffect = _effectPatterns.ElementAt(random.Next(0, _effectPatterns.Count));
                }
            }

            return _currentPlayingEffect;
        }

        protected Color GetPlaybackColumnColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.OFF:
                    return Color.Black;
                case ColorType.PRIMARY:
                    return _settings.ColorSettings.PrimaryColor;
                case ColorType.SECONDARY:
                    return _settings.ColorSettings.SecondaryColor;
                default:
                    throw new ArgumentOutOfRangeException("colorType", colorType, null);
            }
        }

        protected bool IsMismatchingLastColumnEndIndex(
            EffectPattern effectPattern,
            int maxColumns,
            int patternColumn,
            int columnEndIndex)
        {
            return patternColumn == effectPattern.TotalColumns - 1 && columnEndIndex != maxColumns;
        }

        /// <summary>
        /// Is invoked on each effect tick.
        /// An effect tick is triggered each 100 miliseconds of the system time clock as the effect is running in a
        /// background thread and isn't aware of the game ticks.
        /// </summary>
        protected abstract void OnEffectTick();

        /// <summary>
        /// Is invoked when the effect playback is stopped.
        /// </summary>
        protected abstract void OnEffectStop();
    }
}