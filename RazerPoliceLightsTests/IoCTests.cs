using Moq;
using RazerPoliceLights;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;
using Xunit;
using Assert = Xunit.Assert;

namespace RazerPoliceLightsTests
{
    public class IoCTests
    {
        public class Register
        {
            [Fact]
            public void ShouldReturnDifferentInstances()
            {
                var ioC = IoC.Instance;
                ioC
                    .UnregisterAll()
                    .RegisterInstance<IRage>(Mock.Of<IRage>())
                    .RegisterInstance<IElsSettingsManager>(Mock.Of<IElsSettingsManager>())
                    .RegisterInstance<IEffectsManager>(Mock.Of<IEffectsManager>())
                    .RegisterInstance<IColorManager>(Mock.Of<IColorManager>())
                    .Register<ISettingsManager>(typeof(SettingsManager));
                var expectedResult = ioC.GetInstance<ISettingsManager>();

                var result = ioC.GetInstance<ISettingsManager>();

                Assert.NotEqual(expectedResult, result);
            }
        }

        public class RegisterSingleton
        {
            [Fact]
            public void ShouldReturnSingletonInstance()
            {
                var ioC = IoC.Instance;
                ioC
                    .UnregisterAll()
                    .RegisterInstance<IRage>(Mock.Of<IRage>())
                    .RegisterInstance<IElsSettingsManager>(Mock.Of<IElsSettingsManager>())
                    .RegisterInstance<IEffectsManager>(Mock.Of<IEffectsManager>())
                    .RegisterInstance<IColorManager>(Mock.Of<IColorManager>())
                    .RegisterSingleton<ISettingsManager>(typeof(SettingsManager));
                var expectedResult = ioC.GetInstance<ISettingsManager>();

                var result = ioC.GetInstance<ISettingsManager>();

                Assert.Equal(expectedResult, result);
            }
        }
    }
}