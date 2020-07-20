using Moq;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;
using RazerPoliceLightsRage.Effects.Colors;
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
                var logger = new Mock<ILogger>();
                var notification = new Mock<INotification>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(logger.Object, notification.Object, elsSettingsManager.Object, "RazerPoliceLights.xml", effectManager.Object,
                    colorManager.Object);

                settingsManager.Load();

                Assert.NotNull(settingsManager.Settings);
                notification.Verify(x => x.DisplayPluginNotification("configuration loaded"));
            }

            [Fact]
            public void ShouldUpdateEffectPatternManager()
            {
                var logger = new Mock<ILogger>();
                var notification = new Mock<INotification>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(logger.Object, notification.Object, elsSettingsManager.Object, "RazerPoliceLights.xml", effectManager.Object,
                    colorManager.Object);

                settingsManager.Load();

                Assert.NotEmpty(EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard));
                Assert.Equal(11, EffectPatternManager.Instance.GetByDevice(DeviceType.Keyboard).Count);
                Assert.NotEmpty(EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse));
                Assert.Equal(6, EffectPatternManager.Instance.GetByDevice(DeviceType.Mouse).Count);
            }

            [Fact]
            public void ShouldNotCallLoadOnElsSettingsManagerWhenElsIsDisabled()
            {
                var logger = new Mock<ILogger>();
                var notification = new Mock<INotification>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(logger.Object, notification.Object, elsSettingsManager.Object, "RazerPoliceLights.xml", effectManager.Object,
                    colorManager.Object);

                settingsManager.Load();

                elsSettingsManager.Verify(x => x.Load(), Times.Never);
            }

            [Fact]
            public void ShouldCallLoadOnElsSettingsManagerWhenElsIsEnabled()
            {
                var logger = new Mock<ILogger>();
                var notification = new Mock<INotification>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(logger.Object, notification.Object, elsSettingsManager.Object, "ElsEnabled.xml", effectManager.Object, colorManager.Object);

                settingsManager.Load();

                elsSettingsManager.Verify(x => x.Load());
            }
        }
    }
}