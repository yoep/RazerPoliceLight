using System;

namespace RazerPoliceLights.Settings
{
    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message)
        {
        }
    }
}