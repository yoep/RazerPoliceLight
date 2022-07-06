using System;

namespace RazerPoliceLightsBase.Settings.Exceptions
{
    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message)
        {
        }
    }
}