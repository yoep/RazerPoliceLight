using RazerPoliceLightsBase.Devices.Razer;
using Xunit;

namespace RazerPoliceLightsTests.Devices.Razer
{
    public class RazerUtilsTests
    {
        [Fact]
        public void WhenInstanceIsRequested_ShouldReturnChromaInstance()
        {
            var result = RazerUtils.Instance();
            
            Assert.NotNull(result);
        }
        
        [Fact]
        public void WhenColorIsGiven_ShouldReturnTheExpectedColor()
        {
            var color = System.Drawing.Color.FromArgb(255, 100, 10);

            var result = RazerUtils.Convert(color);
            
            Assert.Equal(255, result.R);
            Assert.Equal(100, result.G);
            Assert.Equal(10, result.B);
        }
    }
}