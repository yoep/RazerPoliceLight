using Moq;
using RazerPoliceLights;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using Xunit;
using Assert = Xunit.Assert;

namespace RazerPoliceLightsTests.Settings
{
    public class SettingsManagerTests
    {
        public class Load
        {
            [Fact]
            public void ShouldLoadExpectedSettings()
            {
                var rage = new Mock<IRage>();
                var settingsManager = new SettingsManager(rage.Object, "RazerPoliceLights.xml");

                settingsManager.Load();

                Assert.NotNull(settingsManager.Settings);
                rage.Verify(x => x.DisplayNotification("configuration loaded"));
            }

            [Fact]
            public void ShouldUpdateEffectPatternManager()
            {
                var rage = new Mock<IRage>();
                var settingsManager = new SettingsManager(rage.Object, "RazerPoliceLights.xml");

                settingsManager.Load();

                Assert.NotEmpty(EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard));
                Assert.Equal(7, EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard).Count);
                Assert.NotEmpty(EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse));
                Assert.Equal(6, EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse).Count);
            }
        }
    }
}