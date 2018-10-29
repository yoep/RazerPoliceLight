using System;

namespace RazerPoliceLights.Settings.Exceptions
{
    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message)
        {
        }
    }
}