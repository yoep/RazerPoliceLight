using System;
using System.Diagnostics.CodeAnalysis;
using CUE.NET;
using Rage;
using Rage.Attributes;
using RazerPoliceLights.AbstractionLayer.Implementation;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.GameListeners;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Devices.Corsair;
using RazerPoliceLightsBase.Devices.Razer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Effects.Colors;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;

[assembly:
    Plugin(RazerPoliceLightsPlugin.Name,
        PrefersSingleInstance = true,
        Description = "Razer & Corsair Keyboard Police Lights",
        Author = "yoep",
        ExitPoint = "RazerPoliceLights.EntryPoint.OnUnload")]

// ReSharper disable once CheckNamespace
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

            if (IsCueSdkAvailable())
                CueSDK.Reset();
        }

        private static void InitializeIoContainer()
        {
            IoC.Instance
                .RegisterSingleton<INotification>(typeof(RageNotification))
                .RegisterSingleton<IGameFiber>(typeof(RageFiber))
                .RegisterSingleton<ILogger>(typeof(RageLogger))
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
                .RegisterSingleton<IVehicleListener>(typeof(VehicleListener));
        }

        public static void InitializeDeviceManager()
        {
            var logger = IoC.Instance.GetInstance<ILogger>();
            var sdkAvailable = false;

            if (IsChromaSdkAvailable())
            {
                IoC.Instance.RegisterSingleton<IRazerDeviceManager>(typeof(RazerDeviceManager));
                logger.Info("Found Chroma supported SDK");
                sdkAvailable = true;
            }

            if (IsCueSdkAvailable())
            {
                IoC.Instance.RegisterSingleton<ICorsairDeviceManager>(typeof(CorsairDeviceManager));
                logger.Info("Found CueSDK supported SDK");
                sdkAvailable = true;
            }

            if (!sdkAvailable)
            {
                throw new NoAvailableSdkException();
            }
        }

        public static bool IsChromaSdkAvailable()
        {
            var logger = IoC.Instance.GetInstance<ILogger>();
            var error = false;

            try
            {
                logger.Debug("Checking if the Chroma SDK is available on the device");
                var instance = RazerUtils.Instance();

                if (instance != null)
                    return instance.Initialized;
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to check the Chroma SDK availability, {ex.Message}", ex);
                error = true;
            }

            var reason = error ? "error occurred while initializing the Chroma SDK" : "Chroma SDK is not available on the system";
            logger.Debug($"Chroma SDK is unavailable, {reason}");
            return false;
        }

        public static bool IsCueSdkAvailable()
        {
            var logger = IoC.Instance.GetInstance<ILogger>();

            try
            {
                logger.Debug("Checking if the iCue SDK is available on the device");
                return CueSDK.IsSDKAvailable();
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to check the iCue SDK availability, {ex.Message}", ex);
                return false;
            }
        }
    }
}