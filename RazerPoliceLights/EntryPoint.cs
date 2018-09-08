using Rage;

[assembly: Rage.Attributes.Plugin("Razer Police Lights Keyboard", Description = "Razer Keyboard lighting effect")]

namespace RazerPoliceLights
{
    public class EntryPoint
    {
        public static void Main()
        {
            GameFiber.StartNew(VehicleListener.Start);
        }
    }
}