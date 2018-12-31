using System;
using System.Diagnostics.CodeAnalysis;
using Corale.Colore.Core;
using CUE.NET;
using Rage;
using Rage.Attributes;
using RazerPoliceLights.Devices;
using RazerPoliceLights.Devices.Corsair;
using RazerPoliceLights.Devices.Razer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.GameListeners;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;
using RazerPoliceLights.Utils;

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

            //Start with initializing the IoC container before doing anything else
            InitializeIoContainer();
            
            try
            {
                InitializeDeviceManager();

                var vehicleListener = IoC.Instance.GetInstance<IVehicleListener>();
                var rage = IoC.Instance.GetInstance<IRage>();
                rage.LogTrivialDebug("Creating a new GameFiber for IVehicleListener");
                GameFiber.StartNew(vehicleListener.Start);
            }
            catch (NoAvailableSdkException)
            {
                var rage = IoC.Instance.GetInstance<IRage>();
                rage.DisplayNotification("no supported SDK available");
                rage.LogTrivial("No supported SDK available");
            }
            catch (Exception ex)
            {
                //an unknown error occurred, catch it and log it or otherwise the game will crash (probably because I was testing something odd)
                var rage = IoC.Instance.GetInstance<IRage>();
                rage.LogTrivial("*** An unknown error occurred and the plugin has stopped working ***");
                rage.LogTrivial(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        public static void OnUnload(bool isTerminating)
        {
            var ioC = IoC.Instance;
            ioC.GetInstance<IRage>().LogTrivialDebug("received unload command with termination " + isTerminating);

            //this prevents RAGE from throwing an exception if the plugin couldn't load correctly (aka no SDK)
            if (!ioC.InstanceExists<IVehicleListener>()) 
                return;
            
            ioC.GetInstance<IVehicleListener>().Stop();
            ioC.GetInstance<IEffectsManager>().OnUnload(isTerminating);
            
            if (CueSDK.IsSDKAvailable())
                CueSDK.Reset();
        }

        private static void InitializeIoContainer()
        {
            IoC.Instance
                .Register<IRage>(typeof(RageImpl))
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
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
                throw new NoAvailableSdkException();
            }
        }
    }
}