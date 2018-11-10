using System.Diagnostics.CodeAnalysis;
using Corale.Colore.Core;
using CUE.NET;
using Rage;
using Rage.Attributes;
using RazerPoliceLights.Devices;
using RazerPoliceLights.Devices.Corsair;
using RazerPoliceLights.Devices.Razer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.GameListeners;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

[assembly:
    Plugin(RazerPoliceLights.RazerPoliceLights.Name,
        PrefersSingleInstance = true,
        Description = "Razer Keyboard & Mouse lighting effects",
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

            InitializeIoContainer();
            InitializeDeviceManager();

            var vehicleListener = IoC.Instance.GetInstance<IVehicleListener>();
            var rage = IoC.Instance.GetInstance<IRage>();
            rage.LogTrivialDebug("Creating a new GameFiber for IVehicleListener");
            GameFiber.StartNew(vehicleListener.Start);
        }

        public static void OnUnload(bool isTerminating)
        {
            Game.LogTrivialDebug(RazerPoliceLights.Name + " received unload command with termination " + isTerminating);
            IoC.Instance.GetInstance<IVehicleListener>().Stop();
            IoC.Instance.GetInstance<IEffectsManager>().OnUnload(isTerminating);
        }

        private static void InitializeIoContainer()
        {
            IoC.Instance
                .Register<IRage>(typeof(RageImpl))
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IVehicleListener>(typeof(VehicleListener));
        }

        private static void InitializeDeviceManager()
        {
            var rage = IoC.Instance.GetInstance<IRage>();

            if (Chroma.SdkAvailable)
            {
                IoC.Instance.Register<IDeviceManager>(typeof(RazerDeviceManager));
                rage.LogTrivial("Found Chroma supported SDK");
            }
            else if (CueSDK.IsSDKAvailable())
            {
                IoC.Instance.Register<IDeviceManager>(typeof(CorsairDeviceManager));
                rage.LogTrivial("Found CueSDK supported SDK");
            }
            else
            {
                rage.DisplayNotification("no supported SDK available");
                rage.LogTrivial("No supported SDK available");
                throw new NoAvailableSDKException();
            }
        }
    }
}