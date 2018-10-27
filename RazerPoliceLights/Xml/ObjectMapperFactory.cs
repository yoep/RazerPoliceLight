using System.Collections.Generic;
using RazerPoliceLights.Xml.Deserializers;

namespace RazerPoliceLights.Xml
{
    public class ObjectMapperFactory
    {
        private readonly List<IXmlDeserializer> _deserializers = new List<IXmlDeserializer>();

        public ObjectMapperFactory()
        {
        }

        public static ObjectMapper CreateInstance()
        {
            var objectMapperFactory = new ObjectMapperFactory();

            return objectMapperFactory
                .RegisterDefaultSerializers()
                .GetInstance();
        }

        public ObjectMapperFactory RegisterDefaultSerializers()
        {
            _deserializers.AddRange(new List<IXmlDeserializer>
            {
                new StringXmlDeserializer(),
                new DoubleXmlDeserializer(),
                new BooleanXmlDeserializer(),
                new ColorXmlDeserializer(),
                new EffectPatternXmlDeserializer(),
                new ObjectXmlDeserializer()
            });
            return this;
        }

        public ObjectMapper GetInstance()
        {
            return new ObjectMapper(_deserializers);
        }
    }
}