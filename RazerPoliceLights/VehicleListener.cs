using System;
using Rage;

namespace RazerPoliceLights
{
    public class VehicleListener
    {
        private Ped _player;

        private VehicleListener()
        {
            this._player = Game.LocalPlayer.Character;
            KeepAlive();
        }

        public static void Start()
        {
            try
            {
                new VehicleListener();
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception);
            }
        }

        private void KeepAlive()
        {
            while (true)
            {
                if (IsPlayerDriving())
                {
                    
                }

                GameFiber.Yield();
            }
        }

        private bool IsPlayerDriving()
        {
            Vehicle playerLastVehicle = _player.LastVehicle;

            return playerLastVehicle.Driver == _player;
        }
    }
}