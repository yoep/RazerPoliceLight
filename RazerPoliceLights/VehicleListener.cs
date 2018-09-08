using System;
using Rage;

namespace RazerPoliceLights
{
    public class VehicleListener
    {
        private readonly Ped _player;

        private PlayerState _oldPlayerState;
        private bool _oldSirenStateOn;
        private bool _sirenStateChanged;
        private bool _playerStateChanged;

//        private readonly IKeyboard _chromaKeyboard;
//        private readonly IMouse _chromaMouse;

        private VehicleListener()
        {
            this._player = Game.LocalPlayer.Character;
            this._oldPlayerState = PlayerState;
//            this._chromaKeyboard = Chroma.Instance.Keyboard;
//            this._chromaMouse = Chroma.Instance.Mouse;
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
                Game.LogVerbose(exception.Message + Environment.NewLine + exception.StackTrace);
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
                    if (IsSirenOn)
                    {
//                        _chromaMouse.SetBlinking(Color.Blue, Led.All);
                    }
                    else
                    {
//                        _chromaMouse.Clear();
                    }
                }

                GameFiber.Yield();
            }
        }

        private void UpdateStates()
        {
            this._sirenStateChanged = IsSirenOn != _oldSirenStateOn;
            this._playerStateChanged = PlayerState != _oldPlayerState;
            this._oldSirenStateOn = IsSirenOn;
            this._oldPlayerState = PlayerState;
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