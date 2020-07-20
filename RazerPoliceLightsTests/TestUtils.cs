using Moq;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Devices;
using RazerPoliceLightsBase.Devices.Razer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Effects.Colors;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;
using RazerPoliceLightsBase.Utils;
using RazerPoliceLightsRage.Effects.Colors;

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
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IDeviceManager>(typeof(RazerDeviceManager))
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
                .RegisterInstance<IVehicleListener>(Mock.Of<IVehicleListener>());
        }
    }
}