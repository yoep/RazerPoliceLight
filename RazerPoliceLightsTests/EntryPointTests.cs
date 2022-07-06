using RazerPoliceLights;
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
            TestUtils.InitializeIoC();
            
            var result = EntryPoint.IsCueSdkAvailable();
            
            Assert.True(result);
        }
    }
}