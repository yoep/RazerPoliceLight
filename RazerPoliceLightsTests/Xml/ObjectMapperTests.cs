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
                    Assert.Throws<ArgumentException>(() => _objectMapper.ReadValue<string>(null, typeof(string)));

                Assert.Equal("uri cannot be empty", result.Message);
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenUriIsEmpty()
            {
                var result =
                    Assert.Throws<ArgumentException>(() => _objectMapper.ReadValue<string>("", typeof(string)));

                Assert.Equal("uri cannot be empty", result.Message);
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenClazzIsNull()
            {
                var result = Assert.Throws<ArgumentException>(() => _objectMapper.ReadValue<string>("aze", null));

                Assert.Equal("clazz cannot be null", result.Message);
            }

            [Fact]
            public void ShouldThrowFileNotFoundExceptionWhenUriDoesNotExist()
            {
                var result = Assert.Throws<FileNotFoundException>(() =>
                    _objectMapper.ReadValue<string>("unknown.xml", typeof(string)));

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

                var result =
                    _objectMapper.ReadValue<Settings>(GetResourceFile("RazerPoliceLights.xml"), typeof(Settings));

                Assert.NotNull(result);
                Assert.Equal(expectedPlaybackSettings, result.PlaybackSettings);
                Assert.Equal(expectedColorSettings, result.ColorSettings);
                Assert.True(result.DeviceSettings.KeyboardSettings.IsEnabled);
                Assert.True(result.DeviceSettings.KeyboardSettings.IsScanEnabled);
                Assert.True(result.DeviceSettings.MouseSettings.IsEnabled);
                Assert.True(result.DeviceSettings.MouseSettings.IsScanEnabled);
                Assert.Equal(7, result.EffectPatterns[DeviceType.Keyboard].Count);
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