using Moq;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;
using Xunit;

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
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "RazerPoliceLights.xml");

                settingsManager.Load();

                Assert.NotNull(settingsManager.Settings);
                rage.Verify(x => x.DisplayNotification("configuration loaded"));
            }

            [Fact]
            public void ShouldUpdateEffectPatternManager()
            {
                var rage = new Mock<IRage>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "RazerPoliceLights.xml");

                settingsManager.Load();

                Assert.NotEmpty(EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard));
                Assert.Equal(7, EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard).Count);
                Assert.NotEmpty(EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse));
                Assert.Equal(6, EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse).Count);
            }
            
            [Fact]
            public void ShouldNotCallLoadOnElsSettingsManagerWhenElsIsDisabled()
            {
                var rage = new Mock<IRage>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "RazerPoliceLights.xml");
                
                settingsManager.Load();
                
                elsSettingsManager.Verify(x => x.Load(), Times.Never);
            }

            [Fact]
            public void ShouldCallLoadOnElsSettingsManagerWhenElsIsEnabled()
            {
                var rage = new Mock<IRage>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "ElsEnabled.xml");
                
                settingsManager.Load();
                
                elsSettingsManager.Verify(x => x.Load());
            }
        }
    }
}