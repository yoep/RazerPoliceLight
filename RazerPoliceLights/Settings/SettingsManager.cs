using System;
using System.IO;
using System.Linq;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings.Exceptions;
using RazerPoliceLights.Xml;

namespace RazerPoliceLights.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private const string FileName = @"./Plugins/RazerPoliceLights.xml";
        private readonly IRage _rage;
        private Settings _settings;
        private string _fileName;

        #region Constructors

        public SettingsManager(IRage rage)
        {
            _rage = rage;
            _fileName = FileName;
        }

        public SettingsManager(IRage rage, string fileName)
        {
            _rage = rage;
            _fileName = fileName;
        }

        #endregion

        #region Getters & Setters

        public Settings Settings => GetSettings();

        #endregion

        #region Methods

        public void Load()
        {
            var objectMapper = ObjectMapperFactory.CreateInstance();

            try
            {
                _settings = objectMapper.ReadValue<Settings>(_fileName);
                UpdateEffectManager();

                _rage.DisplayNotification("configuration loaded");
                _rage.LogTrivial(Settings.ToString());
            }
            catch (FileNotFoundException)
            {
                _rage.DisplayNotification("configuration file not found, using defaults instead");
                LoadDefaults();
            }
            catch (Exception e)
            {
                _rage.LogTrivial(e.Message + Environment.NewLine + e.StackTrace);
                _rage.DisplayNotification("configuration file is not valid, using defaults instead");
                LoadDefaults();
            }
        }

        #endregion

        private Settings GetSettings()
        {
            if (_settings == null)
                throw new SettingsNotInitializedException();

            return _settings;
        }

        private void LoadDefaults()
        {
            _settings = Settings.Defaults;
            UpdateEffectManager();
        }

        private void UpdateEffectManager()
        {
            var effectPatternManager = EffectPatternManager.Instance;
            effectPatternManager.Clear();

            //only add the effects which are enabled in the device settings to the effect manager
            effectPatternManager.AddAll(_settings.EffectPatterns[DeviceType.Keyboard]
                .Where(x => _settings.DeviceSettings.KeyboardSettings.Patterns.Contains(x.Name))
                .ToList());
            effectPatternManager.AddAll(_settings.EffectPatterns[DeviceType.Mouse]
                .Where(x => _settings.DeviceSettings.MouseSettings.Patterns.Contains(x.Name))
                .ToList());
        }
    }
}