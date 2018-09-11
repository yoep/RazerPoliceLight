using System.Diagnostics.CodeAnalysis;
using Rage;
using Rage.Attributes;
using RazerPoliceLights.Effects;

[assembly:
    Plugin("Razer Police Lights Keyboard",
        PrefersSingleInstance = true,
        Description = "Razer Keyboard lighting effect",
        Author = "yoep",
        ExitPoint = "RazerPoliceLights.EntryPoint.OnUnload")]

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

        public static void OnUnload(bool isTerminating)
        {
            EffectsManager.Instance.OnUnload(isTerminating);
        }
    }
}