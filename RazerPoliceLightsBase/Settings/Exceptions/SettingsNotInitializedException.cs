using System;

namespace RazerPoliceLightsBase.Settings.Exceptions
{
    public class SettingsNotInitializedException : Exception
    {
        public SettingsNotInitializedException() : base("Settings have not yet been initialized. Call Load() before trying to retrieve the settings.")
        {
        }
    }
}