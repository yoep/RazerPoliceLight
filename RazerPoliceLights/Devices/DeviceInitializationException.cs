using System;

namespace RazerPoliceLights.Devices
{
    public class DeviceInitializationException : Exception
    {
        public DeviceInitializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}