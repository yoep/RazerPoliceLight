using Moq;
using RazerPoliceLights;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
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
                    .RegisterSingleton<ISettingsManager>(typeof(SettingsManager));
                var expectedResult = ioC.GetInstance<ISettingsManager>();

                var result = ioC.GetInstance<ISettingsManager>();

                Assert.Equal(expectedResult, result);
            }
        }
    }
}