using System;
using System.Threading;
using Rage;

namespace RazerPoliceLights.Effects
{
    public abstract class AbstractEffect : IEffect
    {
        private Thread _effectThread;
        private bool _isEffectRunning;

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
                        Thread.Sleep(100);
                    }
                }
                catch (Exception exception)
                {
                    Game.LogTrivial(exception.Message + Environment.NewLine + exception.StackTrace);
                    Game.DisplayHelp("Razer Police Lights Keyboard plugin thread stopped responding");
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