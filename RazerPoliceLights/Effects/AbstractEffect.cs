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
        protected readonly ISettingsManager _settingsManager;

        private Thread _effectThread;
        private EffectPattern _currentPlayingEffect;
        private bool _isEffectRunning;
        private int _effectCursor;
        private int _playbackCount;

        protected AbstractEffect(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #region Properties

        public bool IsPlaying => _isEffectRunning;

        #endregion

        #region Methods

        public void Play()
        {
            Play(null);
        }

        public void Play(EffectPattern effectPattern)
        {
            if (IsDisabled)
                return;

            _isEffectRunning = true;
            _effectThread = new Thread(() =>
            {
                try
                {
                    while (_isEffectRunning)
                    {
                        var pattern = effectPattern ?? GetEffectPattern();
                        var patternRow = GetPatternRow(pattern);
                        OnEffectTick(patternRow);
                        UpdateEffectCursor(pattern);
                        Thread.Sleep((int) (100 * _settingsManager.Settings.PlaybackSettings.SpeedModifier * patternRow.Speed));
                    }
                }
                catch (Exception exception)
                {
                    Game.LogTrivial(exception.Message + Environment.NewLine + exception.StackTrace);
                    Game.DisplayNotification(RazerPoliceLights.Name + " plugin thread stopped responding");
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
            Game.LogTrivialDebug("Device effect thread is being " + (isTerminating ? "forcefully aborted" : "stopped"));
            _isEffectRunning = false;
            if (isTerminating)
                _effectThread.Abort();
        }

        #endregion

        #region Functions

        protected Color GetPlaybackColumnColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.OFF:
                    return Color.Black;
                case ColorType.PRIMARY:
                    return _settingsManager.Settings.ColorSettings.PrimaryColor;
                case ColorType.SECONDARY:
                    return _settingsManager.Settings.ColorSettings.SecondaryColor;
                default:
                    throw new ArgumentOutOfRangeException("colorType", colorType, null);
            }
        }

        protected bool IsMismatchingLastEndIndex(
            PatternRow patternRow,
            int maxColumns,
            int patternColumn,
            int columnEndIndex)
        {
            return patternColumn == patternRow.TotalColumns - 1 && columnEndIndex != maxColumns;
        }

        private EffectPattern GetEffectPattern()
        {
            var random = new Random();

            if (_currentPlayingEffect == null)
            {
                _currentPlayingEffect = EffectPatterns.ElementAt(0);
                return _currentPlayingEffect;
            }

            if (_effectCursor == 0 && IsScanModeEnabled())
            {
                if (_playbackCount > 3 && random.Next(0, 2) == 1)
                {
                    _playbackCount = 0;
                    _currentPlayingEffect = EffectPatterns.ElementAt(random.Next(0, EffectPatterns.Count));
                }
                else
                {
                    _playbackCount++;
                }
            }

            return _currentPlayingEffect;
        }

        private PatternRow GetPatternRow(EffectPattern effectPattern)
        {
            return effectPattern.PatternRows.ElementAt(_effectCursor);
        }

        private void UpdateEffectCursor(EffectPattern pattern)
        {
            if (_effectCursor < pattern.TotalPlaybackRows - 1)
            {
                _effectCursor++;
            }
            else
            {
                _effectCursor = 0;
            }
        }

        #endregion

        /// <summary>
        /// Is invoked on each effect tick.
        /// An effect tick is triggered each 100 milliseconds of the system time clock as the effect is running in a
        /// background thread and isn't aware of the game ticks.
        /// </summary>
        /// <param name="playPattern">Set the pattern the effect tick needs to process.</param>
        protected abstract void OnEffectTick(PatternRow playPattern);

        /// <summary>
        /// Is invoked when the effect playback is stopped.
        /// </summary>
        protected abstract void OnEffectStop();

        /// <summary>
        /// Get if the scan mode for the device is enabled.
        /// </summary>
        /// <returns>Returns true if the scan mode is enabled.</returns>
        protected abstract bool IsScanModeEnabled();

        /// <summary>
        /// Get the activated effect patterns.
        /// </summary>
        protected abstract List<EffectPattern> EffectPatterns { get; }

        /// <summary>
        /// Get if this effect device is disabled.
        /// </summary>
        protected abstract bool IsDisabled { get; }
    }
}