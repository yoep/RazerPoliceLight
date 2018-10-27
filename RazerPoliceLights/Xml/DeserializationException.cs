using System;

namespace RazerPoliceLights.Xml
{
    public class DeserializationException : Exception
    {
        public DeserializationException(string message) : base(message)
        {
        }

        public DeserializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}