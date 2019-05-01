using System;
using System.IO;
using System.Linq;
using RazerPoliceLights.AbstractionLayer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings.Els;
using RazerPoliceLights.Settings.Exceptions;
using RazerPoliceLights.Xml;

namespace RazerPoliceLights.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private const string FileName = @"./Plugins/RazerPoliceLights.xml";

        private readonly IRage _rage;
        private readonly IElsSettingsManager _elsSettingsManager;
        private readonly IEffectsManager _effectsManager;
        private readonly IColorManager _colorManager;

        private Settings _settings;
        private string _fileName;

        #region Constructors

        public SettingsManager(IRage rage, IElsSettingsManager elsSettingsManager, IEffectsManager effectsManager, IColorManager colorManager)
        {
            _rage = rage;
            _elsSettingsManager = elsSettingsManager;
            _effectsManager = effectsManager;
            _colorManager = colorManager;
            _fileName = FileName;
        }

        public SettingsManager(IRage rage, IElsSettingsManager elsSettingsManager, string fileName, IEffectsManager effectsManager, IColorManager colorManager)
        {
            _rage = rage;
            _fileName = fileName;
            _effectsManager = effectsManager;
            _colorManager = colorManager;
            _elsSettingsManager = elsSettingsManager;
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
                UpdateEffectPatternManager();

                //try loading the ELS configuration files, if something goes wrong, disable the els option
                if (_settings.ColorSettings.ElsEnabled)
                    _settings.ColorSettings.ElsEnabled = _elsSettingsManager.Load();

                _rage.DisplayPluginNotification("configuration loaded");
                _rage.LogTrivial(Settings.ToString());
                
                //initialize/reinitialize the effect devices and color manager
                _effectsManager.Initialize();
                _colorManager.Initialize(_settings);
            }
            catch (FileNotFoundException)
            {
                _rage.DisplayPluginNotification("configuration file not found, using defaults instead");
                LoadDefaults();
            }
            catch (Exception e)
            {
                _rage.LogTrivial(e.Message + Environment.NewLine + e.StackTrace);
                _rage.DisplayPluginNotification("configuration file is not valid, using defaults instead");
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
            UpdateEffectPatternManager();
        }

        private void UpdateEffectPatternManager()
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