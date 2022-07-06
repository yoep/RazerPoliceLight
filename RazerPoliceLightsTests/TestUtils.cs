using System;
using Moq;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Devices;
using RazerPoliceLightsBase.Devices.Razer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Effects.Colors;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;

namespace RazerPoliceLightsTests
{
    public static class TestUtils
    {
        public static void InitializeIoC()
        {
            IoC.Instance
                .UnregisterAll()
                .RegisterInstance<INotification>(Mock.Of<INotification>())
                .RegisterInstance<IGameFiber>(Mock.Of<IGameFiber>())
                .RegisterSingleton<ILogger>(typeof(ConsoleLogger))
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IDeviceManager>(typeof(RazerDeviceManager))
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
                .RegisterInstance<IVehicleListener>(Mock.Of<IVehicleListener>());
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Trace(string message)
        {
            Console.WriteLine("[TRACE]" + message);
        }

        public void Debug(string message)
        {
            Console.WriteLine("[DEBUG]" + message);
        }

        public void Info(string message)
        {
            Console.WriteLine("[INFO]" + message);
        }

        public void Warn(string message)
        {
            Console.WriteLine("[WARN]" + message);
        }

        public void Warn(string message, Exception exception)
        {
            Console.WriteLine("[WARN]" + message + Environment.NewLine + exception.StackTrace);
        }

        public void Error(string message)
        {
            Console.WriteLine("[ERROR]" + message);
        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine("[ERROR]" + message + Environment.NewLine + exception.StackTrace);
        }
    }
}