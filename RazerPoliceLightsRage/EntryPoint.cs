using System;
using System.Diagnostics.CodeAnalysis;
using Corale.Colore.Core;
using CUE.NET;
using Rage;
using Rage.Attributes;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Devices;
using RazerPoliceLightsBase.Devices.Corsair;
using RazerPoliceLightsBase.Devices.Razer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Effects.Colors;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;
using RazerPoliceLightsBase.Utils;
using RazerPoliceLightsRage.AbstractionLayer.Implementation;
using RazerPoliceLightsRage.Effects.Colors;
using RazerPoliceLightsRage.GameListeners;

[assembly:
    Plugin(RazerPoliceLights.Name,
        PrefersSingleInstance = true,
        Description = "Razer Keyboard & Mouse lighting effects",
        Author = "yoep",
        ExitPoint = "RazerPoliceLights.EntryPoint.OnUnload")]

namespace RazerPoliceLightsRage
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
                var logger = IoC.Instance.GetInstance<ILogger>();
                logger.Debug("Creating a new GameFiber for IVehicleListener");
                GameFiber.StartNew(vehicleListener.Start);
            }
            catch (NoAvailableSdkException)
            {
                var logger = IoC.Instance.GetInstance<ILogger>();
                var notification = IoC.Instance.GetInstance<INotification>();
                notification.DisplayPluginNotification("no supported SDK available");
                logger.Error("No supported SDK available");
            }
            catch (Exception ex)
            {
                //an unknown error occurred, catch it and log it or otherwise the game will crash (probably because I was testing something odd)
                var logger = IoC.Instance.GetInstance<ILogger>();
                logger.Error("*** An unknown error occurred and the plugin has stopped working ***");
                logger.Error(ex.Message, ex);
            }
        }

        public static void OnUnload(bool isTerminating)
        {
            var ioC = IoC.Instance;
            ioC.GetInstance<ILogger>().Debug("received unload command with termination " + isTerminating);

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
                .RegisterSingleton<INotification>(typeof(RageNotification))
                .RegisterSingleton<IGameFiber>(typeof(RageFiber))
                .RegisterSingleton<ILogger>(typeof(Logger))
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
                .RegisterSingleton<IVehicleListener>(typeof(VehicleListener));
        }

        private static void InitializeDeviceManager()
        {
            var logger = IoC.Instance.GetInstance<ILogger>();

            if (Chroma.SdkAvailable)
            {
                IoC.Instance.Register<IDeviceManager>(typeof(RazerDeviceManager));
                logger.Info("Found Chroma supported SDK");
            }
            else if (CueSDK.IsSDKAvailable())
            {
                IoC.Instance.Register<IDeviceManager>(typeof(CorsairDeviceManager));
                logger.Info("Found CueSDK supported SDK");
            }
            else
            {
                throw new NoAvailableSdkException();
            }
        }
    }
}