using System;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;

namespace RazerPoliceLightsFiveM.Client.GameListeners
{
    public class VehicleListener : AbstractVehicleListener
    {
        #region Constructors

        public VehicleListener(INotification notification, IGameFiber gameFiber, ILogger log, ISettingsManager settingsManager, IEffectsManager effectsManager)
            : base(notification, gameFiber, log, settingsManager, effectsManager)
        {
        }

        #endregion

        #region Getters

        private static int PlayerPedId => API.PlayerPedId();

        #endregion

        #region Functions

        /// <inheritdoc />
        protected override bool IsPlayerDriving()
        {
            return API.IsPedInAnyVehicle(PlayerPedId, true);
        }

        /// <inheritdoc />
        protected override bool IsSirenOn()
        {
            var vehicle = GetPlayerVehicle();

            return vehicle != null && vehicle.IsSirenActive;
        }

        /// <inheritdoc />
        protected override void Listen()
        {
            _gameFiber.NewSafeFiber(() =>
            {
                while (_keepAlive)
                {
                    UpdateStates();

                    if (PlayerState == PlayerState.DRIVING)
                    {
                        if (_sirenStateChanged && IsSirenOn() && !_effectsManager.IsPlaying)
                        {
                            StartEffects();
                        }
                        else if (_sirenStateChanged && !IsSirenOn() && _effectsManager.IsPlaying)
                        {
                            StopEffects();
                        }
                    }
                    else if (_playerStateChanged)
                    {
                        if (!_settingsManager.Settings.PlaybackSettings.LeaveLightsOn)
                        {
                            if (!IsSirenOn())
                                StopEffects();
                        }
                    }
                }
            }, "VehicleListener");
        }

        private void StartEffects()
        {
            var vehicle = GetPlayerVehicle();

            StartEffects(vehicle.DisplayName);
        }

        private Vehicle GetPlayerVehicle()
        {
            var vehicleId = API.GetVehiclePedIsUsing(PlayerPedId);

            return vehicleId != 0 ? new Vehicle(vehicleId) : null;
        }

        #endregion
    }
}