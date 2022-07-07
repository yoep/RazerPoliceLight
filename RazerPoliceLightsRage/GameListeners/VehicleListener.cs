using System;
using Rage;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;

namespace RazerPoliceLights.GameListeners
{
    public class VehicleListener : AbstractVehicleListener
    {
        #region Constructors

        public VehicleListener(INotification notification, ILogger log, IGameFiber gameFiber, ISettingsManager settingsManager, IEffectsManager effectsManager)
            : base(notification, gameFiber, log, settingsManager, effectsManager)
        {
        }

        #endregion

        #region Functions

        protected override bool IsPlayerDriving()
        {
            var playerLastVehicle = GetPlayerVehicle();
            return playerLastVehicle != null && playerLastVehicle.Driver == GetPlayer();
        }

        protected override bool IsSirenOn()
        {
            var playerVehicle = GetPlayerVehicle();
            return playerVehicle != null && playerVehicle.IsSirenOn;
        }

        protected override void Listen()
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

                if (_keepAlive)
                    _gameFiber.FiberYield();
            }
        }

        private void StartEffects()
        {
            var vehicleName = GetPlayerVehicle().Model.Name;

            StartEffects(vehicleName);
        }

        private Vehicle GetPlayerVehicle()
        {
            try
            {
                return GetPlayer()?.CurrentVehicle;
            }
            catch (Exception e)
            {
                _log.Warn("Vehicle retrieval failed with " + e.Message, e);
                //catch exception when character model is changed but was found right before it's disposed in memory
                return null;
            }
        }

        private Ped GetPlayer()
        {
            try
            {
                return Game.LocalPlayer.Character;
            }
            catch (Exception e)
            {
                _log.Warn("Player retrieval failed with " + e.Message, e);
                //catch exception when character model is changed but was found right before it's disposed in memory
                return null;
            }
        }

        #endregion
    }
}