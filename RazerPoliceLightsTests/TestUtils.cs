using Moq;
using RazerPoliceLights;
using RazerPoliceLights.Devices.Razer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.GameListeners;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLightsTests
{
    public static class TestUtils
    {
        public static void InitializeIoC()
        {
            IoC.Instance
                .UnregisterAll()
                .RegisterInstance<IRage>(Mock.Of<IRage>())
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IKeyboardEffect>(typeof(RazerKeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(RazerMouseEffect))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterInstance<IVehicleListener>(Mock.Of<IVehicleListener>());
        }
    }
}