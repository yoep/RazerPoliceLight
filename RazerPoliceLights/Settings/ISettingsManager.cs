namespace RazerPoliceLights.Settings
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Get the loaded settings.
        /// </summary>
        Settings Settings { get; }
        
        /// <summary>
        /// Load the configuration file.
        /// </summary>
        void Load();
    }
}