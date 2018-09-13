﻿using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Effects
{
    public interface IEffect
    {
        /// <summary>
        /// Get if the effect is already playing.
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Start the playback of effects on the device.
        /// </summary>
        void Play();
        
        /// <summary>
        /// Start the playback of the given effect pattern on the device.
        /// </summary>
        /// <param name="effectPattern">Set the effect pattern to play.</param>
        void Play(EffectPattern effectPattern);

        /// <summary>
        /// Stop playing the device effect and restore it to it's initial state.
        /// </summary>
        void Stop();

        /// <summary>
        /// Is invoked when the plugin is being requested to terminate.
        /// </summary>
        /// <param name="isTerminating">Identifies if the unload is a termination.</param>
        void OnUnload(bool isTerminating);
    }
}