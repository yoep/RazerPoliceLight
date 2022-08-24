using RazerPoliceLightsBase;
using RazerPoliceLightsBase.Settings.Els;
using Xunit;
using Xunit.Abstractions;
using static System.IO.Directory;
using static System.IO.File;
using Assert = Xunit.Assert;

namespace RazerPoliceLightsTests.Settings.Els
{
    public class ElsSettingsManagerTests
    {
        private const string ExpectedPath = "ELS\\pack_default";
        
        public ElsSettingsManagerTests(ITestOutputHelper testOutputHelper)
        {
            TestUtils.InitializeIoC();
            TestUtils.SetLogger(testOutputHelper);
        }

        [Fact]
        public void TestLoad_WhenElsDirectoryIsPresent_ShouldLoadTheElsFiles()
        {
            var elsSettingsManager = IoC.Instance.GetInstance<IElsSettingsManager>();
            CreateDirectory(ExpectedPath);
            Copy("AMBULANCE.xml", ExpectedPath + "\\AMBULANCE.xml", true);
            
            var result = elsSettingsManager.Load();

            Assert.True(result);
            var vehicleSettings = elsSettingsManager.GetByName("AMBULANCE");
            Assert.NotNull(vehicleSettings);
        }

        [Fact]
        public void TestLoad_WhenVehicleFileDoesNotExist_ShouldReturnNull()
        {
            var elsSettingsManager = IoC.Instance.GetInstance<IElsSettingsManager>();
            CreateDirectory(ExpectedPath);
            
            var result = elsSettingsManager.Load();

            Assert.True(result);
            var vehicleSettings = elsSettingsManager.GetByName("POLICE");
            Assert.Null(vehicleSettings);
        }
    }
}