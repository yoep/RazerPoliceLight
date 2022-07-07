using System;
using System.Collections.Generic;
using System.IO;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Xml;

namespace RazerPoliceLightsBase.Settings.Els
{
    public class ElsSettingsManager : IElsSettingsManager
    {
        private const string ElsDirectory = "./ELS/";
        private const string DefaultDirectory = ElsDirectory + "pack_default/";

        private readonly ILogger _logger;
        private readonly INotification _notification;
        private readonly List<ElsVehicleSettings> _elsVehicleSettings = new List<ElsVehicleSettings>();
        private readonly Dictionary<string, ElsSettings> _elsSettings = new Dictionary<string, ElsSettings>();

        #region Constructors

        public ElsSettingsManager(ILogger logger, INotification notification)
        {
            _logger = logger;
            _notification = notification;
        }

        #endregion

        #region Methods

        public bool Load()
        {
            _logger.Debug("loading ELS configuration files...");
            var objectMapper = ObjectMapperFactory.CreateInstance();

            _elsVehicleSettings.Clear();
            _elsSettings.Clear();

            try
            {
                foreach (var file in Directory.GetFiles(ElsDirectory, "*.xml"))
                {
                    _logger.Debug("loading els configuration file " + file);
                    var settings = LoadElsFile(file, objectMapper);

                    _logger.Debug("storing els configuration for vehicle " + settings.Key);
                    _elsSettings.Add(settings.Key, settings.Value);
                }

                foreach (var file in Directory.GetFiles(DefaultDirectory, "*.xml"))
                {
                    _logger.Debug("loading els default configuration file " + file);
                    var settings = LoadElsFile(file, objectMapper);
                    var name = settings.Key;

                    _logger.Debug("storing els default configuration for vehicle " + name);
                    _elsVehicleSettings.Add(_elsSettings.ContainsKey(name)
                        ? new ElsVehicleSettings(name, _elsSettings[name], settings.Value)
                        : new ElsVehicleSettings(name, settings.Value));
                }

                _logger.Info("ELS configuration files loaded");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to load ELS configuration, error: " + ex.Message, ex);
                _notification.DisplayPluginNotification("an error occurred while loading the ELS configuration");
            }

            return false;
        }

        public ElsVehicleSettings GetByName(string name)
        {
            return _elsVehicleSettings.Find(x => x.Name.Equals(name.ToUpper()));
        }

        #endregion

        private static KeyValuePair<string, ElsSettings> LoadElsFile(string file, ObjectMapper objectMapper)
        {
            var name = Path.GetFileNameWithoutExtension(file)?.ToUpper();
            var elsSettings = objectMapper.ReadValue<ElsSettings>(file);

            return new KeyValuePair<string, ElsSettings>(name, elsSettings);
        }
    }
}