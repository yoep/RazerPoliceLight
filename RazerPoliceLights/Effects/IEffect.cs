namespace RazerPoliceLights.Effects
{
    public interface IEffect
    {
        /// <summary>
        /// Check if the effect is already playing.
        /// </summary>
        /// <returns>Returns true if the effect is already playing, else false.</returns>
        bool IsPlaying();
        
        /// <summary>
        /// Start playing the device effect.
        /// </summary>
        void Play();

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