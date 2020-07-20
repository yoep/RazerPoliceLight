using System;

namespace RazerPoliceLightsBase.Settings.Exceptions
{
    public class UnknownColorSettingException : Exception
    {
        public UnknownColorSettingException(string message) : base(message)
        {
        }
    }
}