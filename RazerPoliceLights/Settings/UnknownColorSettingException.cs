using System;

namespace RazerPoliceLights.Settings
{
    public class UnknownColorSettingException : Exception
    {
        public UnknownColorSettingException(string message) : base(message)
        {
        }
    }
}