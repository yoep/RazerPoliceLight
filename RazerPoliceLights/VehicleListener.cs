using System;
using System.Collections.Generic;
using System.Threading;
using Rage;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights
{
    public class VehicleListener
    {
        private readonly Ped _player;
        private readonly List<IEffect> _deviceEffects;

        private PlayerState _oldPlayerState;
        private bool _oldSirenStateOn;
        private bool _sirenStateChanged;
        private bool _playerStateChanged;

        private VehicleListener()
        {
            _deviceEffects = new List<IEffect>(new IEffect[] {new KeyboardEffect(), new MouseEffect()});
            _player = GetPlayer();
            _oldPlayerState = PlayerState;
        }

        public PlayerState PlayerState
        {
            get { return IsPlayerDriving() ? PlayerState.DRIVING : PlayerState.WALKING; }
        }

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
                    Game.LogTrivial(exception.Message + Environment.NewLine + exception.StackTrace);
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
                    if (_sirenStateChanged && IsSirenOn)
                    {
                        StartEffects();
                    }
                    else if (_sirenStateChanged)
                    {
                        StopEffects();
                    }
                }
                else if (_playerStateChanged)
                {
                    StopEffects();
                }

                GameFiber.Yield();
            }
        }

        private void StartEffects()
        {
            foreach (var deviceEffect in _deviceEffects)
            {
                deviceEffect.Play();
            }
        }

        private void StopEffects()
        {
            foreach (var deviceEffect in _deviceEffects)
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
            return playerLastVehicle.Exists() && playerLastVehicle.Driver == GetPlayer();
        }

        private Vehicle GetPlayerVehicle()
        {
            var player = GetPlayer();

            return player != null ? player.CurrentVehicle : null;
        }

        private Ped GetPlayer()
        {
            return _player != null ? _player : Game.LocalPlayer.Character;
        }
    }
}