using System;
using System.Threading;
using Rage;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights
{
    public class VehicleListener
    {
        private readonly Ped _player;
        private readonly EffectsManager _effectsManager = EffectsManager.Instance;

        private PlayerState _oldPlayerState;
        private bool _oldSirenStateOn;
        private bool _sirenStateChanged;
        private bool _playerStateChanged;

        private VehicleListener()
        {
            _player = GetPlayer();
            _oldPlayerState = PlayerState;
        }

        public PlayerState PlayerState => IsPlayerDriving() ? PlayerState.DRIVING : PlayerState.WALKING;

        public bool IsSirenOn
        {
            get
            {
                var playerVehicle = GetPlayerVehicle();
                return playerVehicle != null && playerVehicle.IsSirenOn;
            }
        }

        public static void Start()
        {
            try
            {
                SettingsManager.Instance.Load();
                var vehicleListener = new VehicleListener();
                vehicleListener.Listen();
            }
            catch (Exception exception)
            {
                if (!(exception is ThreadAbortException))
                {
                    LogException(exception);
                    Game.DisplayNotification("Razer Police Lights Keyboard plugin has crashed");
                } //else, plugin is being unloaded
            }
        }

        public void Listen()
        {
            while (true)
            {
                UpdateStates();

                if (PlayerState == PlayerState.DRIVING)
                {
                    if (_sirenStateChanged && IsSirenOn && !_effectsManager.IsPlaying())
                    {
                        StartEffects();
                    }
                    else if (_sirenStateChanged && _effectsManager.IsPlaying())
                    {
                        StopEffects();
                    }
                }
                else if (_playerStateChanged)
                {
                    if (!SettingsManager.Instance.Settings.PlaybackSettings.LeaveLightsOn)
                    {
                        StopEffects();
                    }
                }

                GameFiber.Yield();
            }
        }

        private void StartEffects()
        {
            foreach (var deviceEffect in _effectsManager.DevicEffects)
            {
                deviceEffect.Play();
            }
        }

        private void StopEffects()
        {
            foreach (var deviceEffect in _effectsManager.DevicEffects)
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

        private Vehicle GetPlayerVehicle()
        {
            try
            {
                return GetPlayer()?.CurrentVehicle;
            }
            catch (Exception e)
            {
                LogException(e);
                return null;
            }
        }

        private Ped GetPlayer()
        {
            try
            {
                return _player != null ? _player : Game.LocalPlayer.Character;
            }
            catch (Exception e)
            {
                LogException(e);
                return null;
            }
        }

        private static void LogException(Exception e)
        {
            Game.LogTrivial(e.Message + Environment.NewLine + e.StackTrace);
        }
    }
}