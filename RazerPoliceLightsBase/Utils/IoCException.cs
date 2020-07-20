using System;

namespace RazerPoliceLightsBase.Utils
{
    public class IoCException : Exception
    {
        public IoCException(string message) : base(message)
        {
        }

        public IoCException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}