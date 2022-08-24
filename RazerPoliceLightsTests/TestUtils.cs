using System;
using Moq;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Effects.Colors;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;
using Xunit.Abstractions;

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
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
                .RegisterInstance<IVehicleListener>(Mock.Of<IVehicleListener>());
        }

        public static void SetLogger(ITestOutputHelper testOutputHelper)
        {
            var logger = (ConsoleLogger) IoC.Instance.GetInstance<ILogger>();
            logger.SetTestLogger(testOutputHelper);
        }
    }

    public class ConsoleLogger : ILogger
    {
        private ITestOutputHelper _testOutputHelper;

        public void SetTestLogger(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        public void Trace(string message)
        {
            _testOutputHelper?.WriteLine("[TRACE] " + message);
            Console.WriteLine("[TRACE] " + message);
        }

        public void Debug(string message)
        {
            _testOutputHelper?.WriteLine("[DEBUG] " + message);
            Console.WriteLine("[DEBUG] " + message);
        }

        public void Info(string message)
        {
            _testOutputHelper?.WriteLine("[INFO] " + message);
            Console.WriteLine("[INFO] " + message);
        }

        public void Warn(string message)
        {
            _testOutputHelper?.WriteLine("[WARN] " + message);
            Console.WriteLine("[WARN] " + message);
        }

        public void Warn(string message, Exception exception)
        {
            _testOutputHelper?.WriteLine("[WARN] " + message + Environment.NewLine + exception.StackTrace);
            Console.WriteLine("[WARN] " + message + Environment.NewLine + exception.StackTrace);
        }

        public void Error(string message)
        {
            _testOutputHelper?.WriteLine("[ERROR] " + message);
            Console.WriteLine("[ERROR] " + message);
        }

        public void Error(string message, Exception exception)
        {
            _testOutputHelper?.WriteLine("[ERROR] " + message + Environment.NewLine + exception.StackTrace);
            Console.WriteLine("[ERROR] " + message + Environment.NewLine + exception.StackTrace);
        }
    }
}