using Moq;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Devices;
using RazerPoliceLightsBase.Devices.Razer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;
using RazerPoliceLightsTests.Model;
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
                    .RegisterInstance<INotification>(Mock.Of<INotification>())
                    .RegisterInstance<ILogger>(Mock.Of<ILogger>())
                    .RegisterInstance<IElsSettingsManager>(Mock.Of<IElsSettingsManager>())
                    .RegisterInstance<IEffectsManager>(Mock.Of<IEffectsManager>())
                    .RegisterInstance<IColorManager>(Mock.Of<IColorManager>())
                    .Register<ISettingsManager>(typeof(SettingsManager));
                var expectedResult = ioC.GetInstance<ISettingsManager>();

                var result = ioC.GetInstance<ISettingsManager>();

                Assert.NotEqual(expectedResult, result);
            }

            [Fact]
            public void WhenUnregisterAllIsInvokedShouldRemoveAllKnownTypes()
            {
                var ioC = IoC.Instance;

                ioC.RegisterInstance<INotification>(Mock.Of<INotification>());
                Assert.True(ioC.TypeExists<INotification>());

                ioC.UnregisterAll();
                Assert.False(ioC.TypeExists<INotification>());
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
                    .RegisterInstance<INotification>(Mock.Of<INotification>())
                    .RegisterInstance<ILogger>(Mock.Of<ILogger>())
                    .RegisterInstance<IElsSettingsManager>(Mock.Of<IElsSettingsManager>())
                    .RegisterInstance<IEffectsManager>(Mock.Of<IEffectsManager>())
                    .RegisterInstance<IColorManager>(Mock.Of<IColorManager>())
                    .RegisterSingleton<ISettingsManager>(typeof(SettingsManager));
                var expectedResult = ioC.GetInstance<ISettingsManager>();

                var result = ioC.GetInstance<ISettingsManager>();

                Assert.Equal(expectedResult, result);
            }

            [Fact]
            public void WhenListContainsSingleton_ShouldAlwaysReturnTheSameSingletonInstance()
            {
                var ioC = IoC.Instance;
                ioC
                    .UnregisterAll()
                    .RegisterInstance<ILogger>(Mock.Of<ILogger>())
                    .RegisterSingleton<IRazerDeviceManager>(typeof(RazerDeviceManager));
                var deviceManager = ioC.GetInstance<IRazerDeviceManager>();
                var deviceManagers = ioC.GetInstances<IDeviceManager>();
                
                Assert.Single(deviceManagers);
                Assert.Equal(deviceManager, deviceManagers[0]);
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