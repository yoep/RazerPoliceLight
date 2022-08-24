using RazerPoliceLights;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.Devices;
using Xunit;
using Assert = Xunit.Assert;

namespace RazerPoliceLightsTests
{
    public class EntryPointTests
    {
        [Fact]
        public void WhenChromaIsAvailable_ShouldReturnTrue()
        {
            TestUtils.InitializeIoC();
            var result = EntryPoint.IsChromaSdkAvailable();

            Assert.True(result);
        }

        [Fact]
        public void WhenCueSdkIsAvailable_ShouldReturnTrue()
        {
            var result = EntryPoint.IsCueSdkAvailable();

            Assert.True(result);
        }

        [Fact]
        public void WhenInitializeDeviceManagerInvoked_ShouldRegisterAvailableDeviceManagers()
        {
            TestUtils.InitializeIoC();
            EntryPoint.InitializeDeviceManager();

            var deviceManagers = IoC.Instance.GetInstances<IDeviceManager>();

            Assert.Equal(2, deviceManagers.Count);
        }
    }
}