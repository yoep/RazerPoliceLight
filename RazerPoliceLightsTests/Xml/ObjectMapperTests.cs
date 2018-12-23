using System;
using System.IO;
using System.Reflection;
using Corale.Colore.Core;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Xml;
using Xunit;

namespace RazerPoliceLightsTests.Xml
{
    public class ObjectMapperTests
    {
        public class ReadValue
        {
            private readonly ObjectMapper _objectMapper;

            public ReadValue()
            {
                _objectMapper = ObjectMapperFactory.CreateInstance();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenUriIsNull()
            {
                var result =
                    Assert.Throws<ArgumentException>(() => _objectMapper.ReadValue<string>(null));

                Assert.Equal("uri cannot be empty", result.Message);
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenUriIsEmpty()
            {
                var result =
                    Assert.Throws<ArgumentException>(() => _objectMapper.ReadValue<string>(""));

                Assert.Equal("uri cannot be empty", result.Message);
            }

            [Fact]
            public void ShouldThrowFileNotFoundExceptionWhenUriDoesNotExist()
            {
                var result = Assert.Throws<FileNotFoundException>(() =>
                    _objectMapper.ReadValue<string>("unknown.xml"));

                Assert.Equal("unknown.xml does not exist", result.Message);
            }

            [Fact]
            public void ShouldMapXmlToTargetWhenFileExists()
            {
                var expectedPlaybackSettings = new PlaybackSettings
                {
                    SpeedModifier = 1.0,
                    LeaveLightsOn = false
                };
                var expectedColorSettings = new ColorSettings
                {
                    PrimaryColor = Color.Blue,
                    SecondaryColor = Color.Red,
                    StandbyColor = Color.Red
                };

                var result = _objectMapper.ReadValue<RazerPoliceLights.Settings.Settings>(GetResourceFile("RazerPoliceLights.xml"));

                Assert.NotNull(result);
                Assert.Equal(expectedPlaybackSettings, result.PlaybackSettings);
                Assert.Equal(expectedColorSettings, result.ColorSettings);
                Assert.True(result.DeviceSettings.KeyboardSettings.IsEnabled);
                Assert.True(result.DeviceSettings.KeyboardSettings.IsScanEnabled);
                Assert.Equal(11, result.DeviceSettings.KeyboardSettings.Patterns.Count);
                Assert.True(result.DeviceSettings.MouseSettings.IsEnabled);
                Assert.True(result.DeviceSettings.MouseSettings.IsScanEnabled);
                Assert.Equal(6, result.DeviceSettings.MouseSettings.Patterns.Count);
                Assert.Equal(11, result.EffectPatterns[DeviceType.Keyboard].Count);
                Assert.Equal(6, result.EffectPatterns[DeviceType.Mouse].Count);
            }

            private static string GetResourceFile(string path)
            {
                var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
                var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
                var dirPath = Path.GetDirectoryName(codeBasePath);
                return Path.Combine(dirPath, path);
            }
        }
    }
}