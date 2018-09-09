using System.Diagnostics.CodeAnalysis;
using Rage;
using Rage.Attributes;

[assembly:
    Plugin("Razer Police Lights Keyboard",
        Description = "Razer Keyboard lighting effect",
        Author = "yoep")]

namespace RazerPoliceLights
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class EntryPoint
    {
        public static void Main()
        {
            while (Game.IsLoading)
                GameFiber.Yield();

            GameFiber.StartNew(VehicleListener.Start);
        }
    }
}