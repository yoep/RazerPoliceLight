using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Corale.Colore.Core;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Effects
{
    public abstract class AbstractEffect : IEffect
    {
        private const double DelayMargin = 1.2;

        protected readonly IRage Rage;
        protected readonly ISettingsManager SettingsManager;
        private readonly IColorManager _colorManager;

        private Thread _effectThread;
        private EffectPattern _currentPlayingEffect;
        private DateTime _threadMonitor;
        private int _effectCursor;
        private int _playbackCount;
        private int _delay;

        protected AbstractEffect(IRage rage, ISettingsManager settingsManager, IColorManager colorManager)
        {
            Rage = rage;
            SettingsManager = settingsManager;
            _colorManager = colorManager;
        }

        #region Properties

        public bool IsPlaying { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Play(string vehicleName)
        {
            Play(vehicleName, null);
        }

        /// <inheritdoc />
        public void Play(string vehicleName, EffectPattern effectPattern)
        {
            if (IsDisabled)
                return;

            IsPlaying = true;
            _colorManager.VehicleName = vehicleName;
            _threadMonitor = DateTime.Now;
            _effectThread = new Thread(async () =>
            {
                try
                {
                    while (IsPlaying)
                    {
                        MonitorThreadLag();
                        var pattern = effectPattern ?? GetEffectPattern();
                        var patternRow = GetPatternRow(pattern);
                        _delay = (int) (100 * SettingsManager.Settings.PlaybackSettings.SpeedModifier * patternRow.Speed);
                        OnEffectTick(patternRow);
                        UpdateEffectCursor(pattern);
                        await Task.Delay(_delay);
                    }
                }
                catch (Exception exception)
                {
                    Rage.LogTrivial(exception.Message + Environment.NewLine + exception.StackTrace);
                    Rage.DisplayNotification("plugin thread stopped responding");
                }
            }) {IsBackground = true};
            _effectThread.Start();
        }

        private void MonitorThreadLag()
        {
            //ignore first tick
            if (_delay == 0)
                return;
            
            var actualDelay = DateTime.Now - _threadMonitor;
            _threadMonitor = DateTime.Now;

            if (actualDelay.TotalMilliseconds > _delay * DelayMargin)
            {
                Rage.LogTrivial("Detected thread lag with a delay of " + (int) actualDelay.TotalMilliseconds + " while it should be " + _delay);
            }
        }

        public void Stop()
        {
            //End the thread by killing the infinite loop running in the thread
            IsPlaying = false;
            OnEffectStop();
        }

        public void OnUnload(bool isTerminating)
        {
            Rage.LogTrivialDebug("Device effect thread is being " + (isTerminating ? "forcefully aborted" : "stopped"));
            IsPlaying = false;
            if (isTerminating)
                _effectThread.Abort();
        }

        /// <inheritdoc />
        public abstract void Initialize();

        #endregion

        #region Functions

        protected Color GetPlaybackColumnColor(PatternRow playPattern, int index)
        {
            var colorType = playPattern.ColorColumns.ElementAt(index);

            return colorType == ColorType.OFF ? Color.Black : _colorManager[index, playPattern.TotalColumns];
        }

        protected bool IsMismatchingLastEndIndex(
            PatternRow patternRow,
            int maxColumns,
            int patternColumn,
            int columnEndIndex)
        {
            return IsLastPatternColumn(patternRow, patternColumn) && columnEndIndex != maxColumns;
        }

        protected bool IsLastPatternColumn(PatternRow patternRow, int patternColumn)
        {
            return patternColumn == patternRow.TotalColumns - 1;
        }

        private EffectPattern GetEffectPattern()
        {
            var random = new Random();

            if (_currentPlayingEffect == null)
            {
                _currentPlayingEffect = EffectPatterns.ElementAt(0);
                return _currentPlayingEffect;
            }

            if (_effectCursor == 0 && IsScanModeEnabled)
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
        /// Get the activated effect patterns.
        /// </summary>
        protected abstract List<EffectPattern> EffectPatterns { get; }

        /// <summary>
        /// Get if this effect device is disabled.
        /// </summary>
        protected abstract bool IsDisabled { get; }

        /// <summary>
        /// Get if the scan mode for the device is enabled.
        /// </summary>
        /// <returns>Returns true if the scan mode is enabled.</returns>
        protected abstract bool IsScanModeEnabled { get; }
    }
}