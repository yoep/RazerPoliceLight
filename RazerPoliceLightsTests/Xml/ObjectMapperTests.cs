using System;
using System.IO;
using System.Reflection;
using Corale.Colore.Core;
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
                var expectedResult = new Settings
                {
                    PlaybackSettings = new PlaybackSettings
                    {
                        SpeedModifier = 1.0,
                        LeaveLightsOn = false
                    },
                    ColorSettings = new ColorSettings
                    {
                        PrimaryColor = Color.Blue,
                        SecondaryColor = Color.Red,
                        StandbyColor = Color.Red
                    },
                    DeviceSettings = new DeviceSettings
                    {
                        KeyboardSettings = new KeyboardSettings
                        {
                            IsEnabled = true,
                            IsScanEnabled = true
                        },
                        MouseSettings = new MouseSettings
                        {
                            IsEnabled = true,
                            IsScanEnabled = true
                        }
                    }
                };
                
                var result =
                    _objectMapper.ReadValue<Settings>(GetResourceFile("RazerPoliceLights.xml"), typeof(Settings));

                Assert.NotNull(result);
                Assert.Equal(expectedResult, result);
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