namespace RazerPoliceLights.Effects
{
    public interface IEffect
    {
        /// <summary>
        /// Start playing the device effect.
        /// </summary>
        void Play();

        /// <summary>
        /// Stop playing the device effect and restore it to it's initial state.
        /// </summary>
        void Stop();
    }
}