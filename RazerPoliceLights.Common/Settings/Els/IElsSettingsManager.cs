namespace RazerPoliceLightsBase.Settings.Els
{
    public interface IElsSettingsManager
    {
        /// <summary>
        /// Load the ELS configuration settings.
        /// </summary>
        /// <returns>Returns true when the configuration loaded successfully, else false.</returns>
        bool Load();

        /// <summary>
        /// Get the ELS configuration based on the given vehicle name.
        /// </summary>
        /// <param name="name">Set the vehicle name.</param>
        /// <returns>Returns the settings if found, else null.</returns>
        ElsVehicleSettings GetByName(string name);
    }
}