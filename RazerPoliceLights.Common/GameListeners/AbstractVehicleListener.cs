using System;
using System.Threading;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Settings;

namespace RazerPoliceLightsBase.GameListeners
{
    public abstract class AbstractVehicleListener : IVehicleListener
    {
        protected readonly INotification _notification;
        protected readonly IGameFiber _gameFiber;
        protected readonly ILogger _log;
        protected readonly ISettingsManager _settingsManager;
        protected readonly IEffectsManager _effectsManager;

        protected PlayerState _oldPlayerState;
        protected bool _oldSirenStateOn;
        protected bool _sirenStateChanged;
        protected bool _playerStateChanged;
        protected bool _keepAlive = true;

        #region Constructors

        protected AbstractVehicleListener(INotification notification, IGameFiber gameFiber, ILogger log, ISettingsManager settingsManager,
            IEffectsManager effectsManager)
        {
            _notification = notification;
            _gameFiber = gameFiber;
            _log = log;
            _settingsManager = settingsManager;
            _effectsManager = effectsManager;
            _oldPlayerState = PlayerState;
        }

        #endregion

        #region Getters & Setters

        protected PlayerState PlayerState => IsPlayerDriving() ? PlayerState.DRIVING : PlayerState.WALKING;

        #endregion

        #region Methods

        /// <inheritdoc />
        public void Start()
        {
            _log.Debug("Starting vehicle listener");
            try
            {
                _keepAlive = true;
                _settingsManager.Load();
                Listen();
            }
            catch (Exception exception)
            {
                if (!(exception is ThreadAbortException))
                {
                    _log.Error("Vehicle listener encountered an issue, error: " + exception.Message, exception);
                    _notification.DisplayPluginNotification("plugin has crashed");
                } //else, plugin is being unloaded
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            _log.Debug("Stopping vehicle listener");
            _keepAlive = false;
        }

        #endregion

        #region Functions

        /// <summary>
        /// Check if the player is currently driving a car.
        /// </summary>
        /// <returns>Returns true if the player is driving a car, else false.</returns>
        protected abstract bool IsPlayerDriving();

        /// <summary>
        /// Check if the player's vehicle has it's sirens on.
        /// </summary>
        /// <returns>Returns true if the player vehicle sirens are on, else false.</returns>
        protected abstract bool IsSirenOn();

        /// <summary>
        /// Start the internal vehicle listener.
        /// This methods listens on changes if the player is in a vehicle or not.
        /// </summary>
        protected abstract void Listen();

        /// <summary>
        /// Update the listener states.
        /// </summary>
        protected void UpdateStates()
        {
            _sirenStateChanged = IsSirenOn() != _oldSirenStateOn;
            _playerStateChanged = PlayerState != _oldPlayerState;
            _oldSirenStateOn = IsSirenOn();
            _oldPlayerState = PlayerState;
        }

        /// <summary>
        /// Start the effect playback for the given vehicle.
        /// </summary>
        /// <param name="vehicleName">The current vehicle model name.</param>
        protected void StartEffects(string vehicleName)
        {
            _log.Debug("playing effects for vehicle " + vehicleName);
            _effectsManager.Play(vehicleName);
        }

        /// <summary>
        /// Stop the effect playback.
        /// </summary>
        protected void StopEffects()
        {
            _effectsManager.Stop();
        }

        #endregion
    }
}