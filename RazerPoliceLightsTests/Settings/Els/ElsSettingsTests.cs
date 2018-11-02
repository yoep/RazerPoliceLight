using Corale.Colore.Core;
using RazerPoliceLights.Settings.Els;
using RazerPoliceLights.Xml;
using Xunit;

namespace RazerPoliceLightsTests.Settings.Els
{
    public class ElsSettingsTests
    {
        public static string FILE = @"AMBULANCE.xml";
        
        [Fact]
        public void ShouldLoadElsFile()
        {
            var elsSettings = new ElsSettings
            {
                LightingSettings = new LightingSettings
                {
                    Extra01 = new ExtraSettings
                    {
                        IsElsControlled = true,
                        AllowEnvLight = true,
                        Color = Color.Blue
                    },
                    Extra02 = new ExtraSettings
                    {
                        IsElsControlled = true,
                        AllowEnvLight = true,
                        Color = Color.Blue
                    },
                    Extra03 = new ExtraSettings
                    {
                        IsElsControlled = true,
                        AllowEnvLight = true,
                        Color = Color.Blue
                    },
                    Extra04 = new ExtraSettings
                    {
                        IsElsControlled = true,
                        AllowEnvLight = true,
                        Color = Color.Red
                    },
                    Extra05 = new ExtraSettings
                    {
                        IsElsControlled = true,
                        AllowEnvLight = true,
                        Color = Color.Red
                    },
                    Extra06 = new ExtraSettings
                    {
                        IsElsControlled = true,
                        AllowEnvLight = true,
                        Color = Color.Red
                    }
                }
            };
            var objectMapper = ObjectMapperFactory.CreateInstance();

            var result = objectMapper.ReadValue<ElsSettings>(FILE);
            
            Assert.NotNull(result);
            Assert.Equal(elsSettings, result);
        }
    }
}