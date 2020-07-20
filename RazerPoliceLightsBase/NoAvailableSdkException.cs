using System;

namespace RazerPoliceLightsRage
{
    public class NoAvailableSdkException : Exception
    {
        public NoAvailableSdkException() : base("Unable to find any supported SDK")
        {
        }
    }
}