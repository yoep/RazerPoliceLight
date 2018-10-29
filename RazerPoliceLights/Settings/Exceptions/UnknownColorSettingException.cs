using System;

namespace RazerPoliceLights.Settings.Exceptions
{
    public class UnknownColorSettingException : Exception
    {
        public UnknownColorSettingException(string message) : base(message)
        {
        }
    }
}