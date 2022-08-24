using System.Threading;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.Devices.Corsair;
using RazerPoliceLightsBase.Devices.Razer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Settings;
using Xunit;

namespace RazerPoliceLightsTests.Effects
{
    public class EffectTests
    {
        [Fact]
        public void WhenDevicesAreAvailable_ShouldPlayEffects()
        {
            TestUtils.InitializeIoC();
            var ioC = IoC.Instance;
            ioC.RegisterSingleton<IRazerDeviceManager>(typeof(RazerDeviceManager));
            ioC.RegisterSingleton<ICorsairDeviceManager>(typeof(CorsairDeviceManager));
            var settingsManager = ioC.GetInstance<ISettingsManager>();
            var colorManager = ioC.GetInstance<IColorManager>();
            var effectsManager = ioC.GetInstance<IEffectsManager>();

            settingsManager.Load();
            colorManager.Initialize(settingsManager.Settings);
            effectsManager.Initialize();
            effectsManager.Play("POLICE");
            Thread.Sleep(5000);
        }
    }
}