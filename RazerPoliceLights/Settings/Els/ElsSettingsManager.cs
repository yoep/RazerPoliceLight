using System;
using System.Collections.Generic;
using System.IO;
using RazerPoliceLights.AbstractionLayer;
using RazerPoliceLights.Xml;

namespace RazerPoliceLights.Settings.Els
{
    public class ElsSettingsManager : IElsSettingsManager
    {
        private const string ElsDirectory = "./ELS/";
        private const string DefaultDirectory = ElsDirectory + "pack_default/";

        private readonly IRage _rage;
        private readonly List<ElsVehicleSettings> _elsVehicleSettings = new List<ElsVehicleSettings>();
        private readonly Dictionary<string, ElsSettings> _elsSettings = new Dictionary<string, ElsSettings>();

        #region Constructors
        
        public ElsSettingsManager(IRage rage)
        {
            _rage = rage;
        }
        
        #endregion

        #region Methods
        
        public bool Load()
        {
            _rage.LogTrivialDebug("loading ELS configuration files...");
            var objectMapper = ObjectMapperFactory.CreateInstance();
            
            _elsVehicleSettings.Clear();
            _elsSettings.Clear();

            try
            {
                foreach (var file in Directory.GetFiles(ElsDirectory, "*.xml"))
                {
                    _rage.LogTrivialDebug("loading els configuration file " + file);
                    var settings = LoadElsFile(file, objectMapper);
                    
                    _rage.LogTrivialDebug("storing els configuration for vehicle " + settings.Key);
                    _elsSettings.Add(settings.Key, settings.Value);
                }

                foreach (var file in Directory.GetFiles(DefaultDirectory, "*.xml"))
                {
                    _rage.LogTrivialDebug("loading els default configuration file " + file);
                    var settings = LoadElsFile(file, objectMapper);
                    var name = settings.Key;

                    _rage.LogTrivialDebug("storing els default configuration for vehicle " + name);
                    _elsVehicleSettings.Add(_elsSettings.ContainsKey(name)
                        ? new ElsVehicleSettings(name, _elsSettings[name], settings.Value)
                        : new ElsVehicleSettings(name, settings.Value));
                }

                _rage.LogTrivial("ELS configuration files loaded");
                return true;
            }
            catch (Exception e)
            {
                _rage.LogTrivial(e.Message + Environment.NewLine + e.StackTrace);
                _rage.DisplayNotification("an error occurred while loading the ELS configuration");
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