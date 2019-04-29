using System;
using System.Threading;
using Rage;
using RazerPoliceLights.AbstractionLayer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.GameListeners
{
    public class VehicleListener : IVehicleListener
    {
        private readonly IRage _rage;
        private readonly ILogger _log;
        private readonly ISettingsManager _settingsManager;
        private readonly IEffectsManager _effectsManager;

        private PlayerState _oldPlayerState;
        private bool _oldSirenStateOn;
        private bool _sirenStateChanged;
        private bool _playerStateChanged;
        private bool _keepAlive = true;

        #region Constructors

        public VehicleListener(IRage rage, ISettingsManager settingsManager, IEffectsManager effectsManager, ILogger log)
        {
            _rage = rage;
            _settingsManager = settingsManager;
            _effectsManager = effectsManager;
            _log = log;
            _oldPlayerState = PlayerState;
        }

        #endregion

        #region Getters & Setters

        private PlayerState PlayerState => IsPlayerDriving() ? PlayerState.DRIVING : PlayerState.WALKING;

        private bool IsSirenOn
        {
            get
            {
                var playerVehicle = GetPlayerVehicle();
                return playerVehicle != null && playerVehicle.IsSirenOn;
            }
        }

        #endregion

        public void Start()
        {
            try
            {
                _settingsManager.Load();
                Listen();
            }
            catch (Exception exception)
            {
                if (!(exception is ThreadAbortException))
                {
                    _log.Error("has encountered an issue, " + exception.Message, exception);
                    _rage.DisplayNotification("plugin has crashed");
                } //else, plugin is being unloaded
            }
        }

        public void Stop()
        {
            _log.Debug("Stopping vehicle listener");
            _keepAlive = false;
        }

        private void Listen()
        {
            while (_keepAlive)
            {
                UpdateStates();

                if (PlayerState == PlayerState.DRIVING)
                {
                    if (_sirenStateChanged && IsSirenOn && !_effectsManager.IsPlaying)
                    {
                        StartEffects();
                    }
                    else if (_sirenStateChanged && !IsSirenOn && _effectsManager.IsPlaying)
                    {
                        StopEffects();
                    }
                }
                else if (_playerStateChanged)
                {
                    if (!_settingsManager.Settings.PlaybackSettings.LeaveLightsOn)
                    {
                        if (!IsSirenOn)
                            StopEffects();
                    }
                }

                if (_keepAlive)
                    _rage.FiberYield();
            }
        }

        private void StartEffects()
        {
            var vehicleName = GetPlayerVehicle().Model.Name;

            _log.Debug("playing effects for vehicle " + vehicleName);
            _effectsManager.Play(vehicleName);
        }

        private void StopEffects()
        {
            _effectsManager.Stop();
        }

        private void UpdateStates()
        {
            _sirenStateChanged = IsSirenOn != _oldSirenStateOn;
            _playerStateChanged = PlayerState != _oldPlayerState;
            _oldSirenStateOn = IsSirenOn;
            _oldPlayerState = PlayerState;
        }

        private bool IsPlayerDriving()
        {
            var playerLastVehicle = GetPlayerVehicle();
            return playerLastVehicle != null && playerLastVehicle.Driver == GetPlayer();
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
    }
}