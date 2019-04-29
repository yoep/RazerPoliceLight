using Moq;
using RazerPoliceLights.AbstractionLayer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
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
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "RazerPoliceLights.xml", effectManager.Object,
                    colorManager.Object);

                settingsManager.Load();

                Assert.NotNull(settingsManager.Settings);
                rage.Verify(x => x.DisplayNotification("configuration loaded"));
            }

            [Fact]
            public void ShouldUpdateEffectPatternManager()
            {
                var rage = new Mock<IRage>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "RazerPoliceLights.xml", effectManager.Object,
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
                var rage = new Mock<IRage>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "RazerPoliceLights.xml", effectManager.Object,
                    colorManager.Object);

                settingsManager.Load();

                elsSettingsManager.Verify(x => x.Load(), Times.Never);
            }

            [Fact]
            public void ShouldCallLoadOnElsSettingsManagerWhenElsIsEnabled()
            {
                var rage = new Mock<IRage>();
                var elsSettingsManager = new Mock<IElsSettingsManager>();
                var effectManager = new Mock<IEffectsManager>();
                var colorManager = new Mock<IColorManager>();
                var settingsManager = new SettingsManager(rage.Object, elsSettingsManager.Object, "ElsEnabled.xml", effectManager.Object, colorManager.Object);

                settingsManager.Load();

                elsSettingsManager.Verify(x => x.Load());
            }
        }
    }
}