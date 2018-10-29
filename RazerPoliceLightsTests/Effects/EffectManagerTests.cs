using Moq;
using RazerPoliceLights.Effects;
using Xunit;

namespace RazerPoliceLightsTests.Effects
{
    public class EffectManagerTests
    {
        private Mock<IKeyboardEffect> _keyboardEffect = new Mock<IKeyboardEffect>();
        private Mock<IMouseEffect> _mouseEffect = new Mock<IMouseEffect>();
        private IEffectsManager _effectsManager;

        public EffectManagerTests()
        {
            _effectsManager = new EffectsManager(_keyboardEffect.Object, _mouseEffect.Object);
        }

        [Fact]
        public void ShouldPlayEffectsOnDevices()
        {
            _effectsManager.Play();

            _keyboardEffect.Verify(x => x.Play());
            _mouseEffect.Verify(x => x.Play());
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