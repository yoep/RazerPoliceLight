using System;

namespace RazerPoliceLightsBase.Devices
{
    public class DeviceInitializationException : Exception
    {
        public DeviceInitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}