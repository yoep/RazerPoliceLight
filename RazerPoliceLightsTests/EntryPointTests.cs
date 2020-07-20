using RazerPoliceLightsBase;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Settings;
using Xunit;
using Assert = Xunit.Assert;

namespace RazerPoliceLightsTests
{
    public class EntryPointTests
    {
        [Fact]
        public void ShouldInitializeIoC()
        {
            var ioC = IoC.Instance;
            TestUtils.InitializeIoC();
            
            Assert.NotNull(ioC.GetInstance<ISettingsManager>());
            Assert.NotNull(ioC.GetInstance<IKeyboardEffect>());
            Assert.NotNull(ioC.GetInstance<IMouseEffect>());
            Assert.NotNull(ioC.GetInstance<IEffectsManager>());
        }
    }
}