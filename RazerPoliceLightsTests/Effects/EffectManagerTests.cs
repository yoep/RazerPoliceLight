using System.Collections.Generic;
using Moq;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Devices;
using RazerPoliceLightsBase.Effects;
using Xunit;

namespace RazerPoliceLightsTests.Effects
{
    public class EffectManagerTests
    {
        private readonly Mock<IDeviceManager> _deviceManager = new Mock<IDeviceManager>();
        private readonly Mock<ILogger> _logger = new Mock<ILogger>();
        private readonly Mock<IKeyboardEffect> _keyboardEffect = new Mock<IKeyboardEffect>();
        private readonly Mock<IMouseEffect> _mouseEffect = new Mock<IMouseEffect>();
        private readonly IEffectsManager _effectsManager;

        public EffectManagerTests()
        {
            _effectsManager = new EffectsManager(new List<IDeviceManager> { _deviceManager.Object }, _logger.Object);

            _deviceManager.Setup(x => x.Devices).Returns(new List<IEffect> { _keyboardEffect.Object, _mouseEffect.Object });
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