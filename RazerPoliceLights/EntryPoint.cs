using Rage;
using Rage.Attributes;

[assembly:
    Plugin("Razer Police Lights Keyboard",
        Description = "Razer Keyboard lighting effect",
        Author = "yoep")]

namespace RazerPoliceLights
{
    public static class EntryPoint
    {
        public static void Main()
        {
            Game.LogTrivial("Initializing RazerPoliceLights");
            while (Game.IsLoading)
                GameFiber.Yield();

            Game.LogTrivial("Creating new VehicleListener");
            GameFiber.StartNew(VehicleListener.Start);
        }
    }
}