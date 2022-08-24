using System.Linq;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.Devices;
using RazerPoliceLightsBase.Devices.Razer;
using Xunit;
using Assert = Xunit.Assert;

namespace RazerPoliceLightsTests.Devices.Razer
{
    public class RazerDeviceManagerTests
    {
        [Fact]
        public void WhenSdkIsAvailableShouldReturnRazerDevices()
        {
            TestUtils.InitializeIoC();
            IoC.Instance.Register<IRazerDeviceManager>(typeof(RazerDeviceManager));
            var deviceManager = IoC.Instance.GetInstance<IDeviceManager>();

            var result = deviceManager.Devices;

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}