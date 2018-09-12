﻿using System;
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

        private Thread _effectThread;
        private EffectPattern _currentPlayingEffect;
        private bool _isEffectRunning;
        private int _playbackCount;

        #region Properties

        public bool IsPlaying()
        {
            return _isEffectRunning;
        }

        protected Settings.Settings Settings => SettingsManager.Instance.Settings;

        #endregion

        #region Methods

        public void Play()
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
                        OnEffectTick();
                        Thread.Sleep((int) (100 * Settings.PlaybackSettings.SpeedModifier));
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

        #endregion

        #region Functions

        protected EffectPattern GetEffectPattern()
        {
            var random = new Random();

            if (_currentPlayingEffect == null)
            {
                _currentPlayingEffect = EffectPatterns.ElementAt(0);
                return _currentPlayingEffect;
            }

            if (EffectCursor == 0 && IsScanModeEnabled())
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

        protected Color GetPlaybackColumnColor(ColorType colorType)
        {
            switch (colorType)
            {
                case ColorType.OFF:
                    return Color.Black;
                case ColorType.PRIMARY:
                    return Settings.ColorSettings.PrimaryColor;
                case ColorType.SECONDARY:
                    return Settings.ColorSettings.SecondaryColor;
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

        #endregion

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