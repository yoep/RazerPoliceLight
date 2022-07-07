namespace RazerPoliceLightsBase.GameListeners
{
    public interface IVehicleListener
    {
        /// <summary>
        /// Start the vehicle listener.
        /// The listener will start listening on game events for player- and vehicle state changes.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop the vehicle listener.
        /// </summary>
        void Stop();
    }
}