using Moq;
using RazerPoliceLights.AbstractionLayer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;
using RazerPoliceLights.Utils;
using RazerPoliceLightsTests.Model;
using Xunit;

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

        public class PostConstruct
        {
            [Fact]
            public void ShouldInvokePostConstructMethod()
            {
                var ioC = IoC.Instance;
                ioC
                    .UnregisterAll()
                    .RegisterSingleton<IPostConstructModel>(typeof(PostConstructModel));

                var result = ioC.GetInstance<IPostConstructModel>();
                
                Assert.NotNull(result);
                Assert.True(result.IsInitialized);
            }
        }
    }
}