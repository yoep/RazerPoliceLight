using System;

namespace RazerPoliceLights
{
    public class NoAvailableSDKException : Exception
    {
        public NoAvailableSDKException() : base("Unable to find any supported SDK")
        {
        }
    }
}