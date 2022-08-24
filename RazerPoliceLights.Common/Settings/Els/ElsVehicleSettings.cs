namespace RazerPoliceLightsBase.Settings.Els
{
    public class ElsVehicleSettings
    {
        private readonly ElsSettings _elsSettings;
        private readonly ElsSettings _defaultElsSettings;

        public ElsVehicleSettings(string name, ElsSettings elsSettings, ElsSettings defaultElsSettings)
        {
            Assert.NotNull(defaultElsSettings, "defaultElsSettings cannot be null");
            _elsSettings = elsSettings;
            _defaultElsSettings = defaultElsSettings;
            Name = name;
        }

        public ElsVehicleSettings(string name, ElsSettings defaultElsSettings)
        {
            Assert.NotNull(defaultElsSettings, "defaultElsSettings cannot be null");
            _defaultElsSettings = defaultElsSettings;
            Name = name;
        }

        public string Name { get; }

        /// <summary>
        /// Get ELS settings which are available in the ELS configuration directory for the given vehicle.
        /// </summary>
        public ElsSettings ElsSettings => _elsSettings ?? _defaultElsSettings;
    }
}