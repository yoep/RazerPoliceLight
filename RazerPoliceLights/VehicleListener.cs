using System;
using System.Threading;
using Rage;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights
{
    public class VehicleListener
    {
        private readonly EffectsManager _effectsManager = EffectsManager.Instance;

        private PlayerState _oldPlayerState;
        private bool _oldSirenStateOn;
        private bool _sirenStateChanged;
        private bool _playerStateChanged;
        private bool _keepAlive = true;

        #region Constructors

        static VehicleListener()
        {
            Instance = new VehicleListener();
        }

        private VehicleListener()
        {
            _oldPlayerState = PlayerState;
        }

        #endregion

        #region Getters & Setters

        public static VehicleListener Instance { get; private set; }

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
                SettingsManager.Instance.Load();
                Listen();
            }
            catch (Exception exception)
            {
                if (!(exception is ThreadAbortException))
                {
                    LogException(exception);
                    Game.DisplayNotification(RazerPoliceLights.Name + " plugin has crashed");
                } //else, plugin is being unloaded
            }
        }

        public void Stop()
        {
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
                    if (!SettingsManager.Instance.Settings.PlaybackSettings.LeaveLightsOn)
                    {
                        if (!IsSirenOn)
                            StopEffects();
                    }
                }

                if (_keepAlive)
                    GameFiber.Yield();
            }
        }

        private void StartEffects()
        {
            foreach (var deviceEffect in _effectsManager.DeviceEffects)
            {
                deviceEffect.Play();
            }
        }

        private void StopEffects()
        {
            foreach (var deviceEffect in _effectsManager.DeviceEffects)
            {
                deviceEffect.Stop();
            }
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

        private static Vehicle GetPlayerVehicle()
        {
            try
            {
                return GetPlayer()?.CurrentVehicle;
            }
            catch (Exception e)
            {
                Game.LogTrivialDebug("Vehicle retrieval failed with " + e.Message + Environment.NewLine + e);
                //catch exception when character model is changed but was found right before it's disposed in memory
                return null;
            }
        }

        private static Ped GetPlayer()
        {
            try
            {
                return Game.LocalPlayer.Character;
            }
            catch (Exception e)
            {
                Game.LogTrivialDebug("Player retrieval failed with " + e.Message + Environment.NewLine + e);
                //catch exception when character model is changed but was found right before it's disposed in memory
                return null;
            }
        }

        private static void LogException(Exception e)
        {
            Game.LogTrivial(RazerPoliceLights.Name + " has encountered an issue" + Environment.NewLine
                            + e.Message + Environment.NewLine + e.StackTrace);
        }
    }
}