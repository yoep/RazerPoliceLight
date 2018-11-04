using Moq;
using RazerPoliceLights.Devices;
using RazerPoliceLights.Effects;
using Xunit;

namespace RazerPoliceLightsTests.Effects
{
    public class EffectManagerTests
    {
        private readonly Mock<IDeviceManager> _deviceManager = new Mock<IDeviceManager>();
        private readonly Mock<IKeyboardEffect> _keyboardEffect = new Mock<IKeyboardEffect>();
        private readonly Mock<IMouseEffect> _mouseEffect = new Mock<IMouseEffect>();
        private readonly IEffectsManager _effectsManager;

        public EffectManagerTests()
        {
            _effectsManager = new EffectsManager(_deviceManager.Object);

            _deviceManager.Setup(x => x.KeyboardDevice).Returns(_keyboardEffect.Object);
            _deviceManager.Setup(x => x.MouseDevice).Returns(_mouseEffect.Object);
        }

        [Fact]
        public void ShouldPlayEffectsOnDevices()
        {
            _effectsManager.Play(null);

            _keyboardEffect.Verify(x => x.Play(null));
            _mouseEffect.Verify(x => x.Play(null));
        }

        [Fact]
        public void ShouldStopEffectsOnDevices()
        {
            _effectsManager.Stop();

            _keyboardEffect.Verify(x => x.Stop());
            _mouseEffect.Verify(x => x.Stop());
        }
    }
}