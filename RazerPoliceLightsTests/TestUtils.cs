using Moq;
using RazerPoliceLights;
using RazerPoliceLights.Effects;
using RazerPoliceLights.GameListeners;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;

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
                .RegisterSingleton<IKeyboardEffect>(typeof(KeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(MouseEffect))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterInstance<IVehicleListener>(Mock.Of<IVehicleListener>());
        }
    }
}