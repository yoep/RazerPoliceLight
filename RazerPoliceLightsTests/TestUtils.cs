using Moq;
using RazerPoliceLights;
using RazerPoliceLights.Devices;
using RazerPoliceLights.Devices.Razer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.GameListeners;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;
using RazerPoliceLights.Utils;

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
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IDeviceManager>(typeof(RazerDeviceManager))
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
                .RegisterInstance<IVehicleListener>(Mock.Of<IVehicleListener>());
        }
    }
}