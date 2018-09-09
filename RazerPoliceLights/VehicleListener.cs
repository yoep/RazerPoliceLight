using System;
using System.Collections.Generic;
using Rage;
using RazerPoliceLights.Effects;

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
            _player = Game.LocalPlayer.Character;
            _oldPlayerState = PlayerState;
        }

        public PlayerState PlayerState
        {
            get { return IsPlayerDriving() ? PlayerState.DRIVING : PlayerState.WALKING; }
        }

        public bool IsSirenOn
        {
            get { return GetPlayerVehicle().IsSirenOn; }
        }

        public static void Start()
        {
            try
            {
                var vehicleListener = new VehicleListener();
                vehicleListener.Listen();
            }
            catch (Exception exception)
            {
                Game.LogTrivial(exception.Message + Environment.NewLine + exception.StackTrace);
            }
        }

        public void Listen()
        {
            while (true)
            {
                UpdateStates();

                if (_playerStateChanged)
                {
                    Game.LogVerbose("Player state changed to " + PlayerState);
                }

                if (_sirenStateChanged)
                {
                    Game.LogTrivial("Siren state changed to " + IsSirenOn);
                }

                if (PlayerState == PlayerState.DRIVING)
                {
                    if (_sirenStateChanged && IsSirenOn)
                    {
                        foreach (var deviceEffect in _deviceEffects)
                        {
                            deviceEffect.Play();
                        }
                    }
                    else if(_sirenStateChanged)
                    {
                        foreach (var deviceEffect in _deviceEffects)
                        {
                            deviceEffect.Stop();
                        }
                    }
                }

                GameFiber.Yield();
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

            return playerLastVehicle.Exists() && playerLastVehicle.Driver == _player;
        }

        private Vehicle GetPlayerVehicle()
        {
            return _player.CurrentVehicle;
        }
    }
}