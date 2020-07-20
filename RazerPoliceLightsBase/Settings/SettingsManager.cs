using System;
using System.IO;
using System.Linq;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings.Els;
using RazerPoliceLightsBase.Settings.Exceptions;
using RazerPoliceLightsBase.Xml;
using RazerPoliceLightsRage.Effects.Colors;

namespace RazerPoliceLightsBase.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private const string FileName = @"./Plugins/RazerPoliceLights.xml";

        private readonly ILogger _logger;
        private readonly INotification _notification;
        private readonly IElsSettingsManager _elsSettingsManager;
        private readonly IEffectsManager _effectsManager;
        private readonly IColorManager _colorManager;

        private Settings _settings;
        private string _fileName;

        #region Constructors

        public SettingsManager(ILogger logger, INotification notification, IElsSettingsManager elsSettingsManager, IEffectsManager effectsManager,
            IColorManager colorManager)
        {
            _logger = logger;
            _notification = notification;
            _elsSettingsManager = elsSettingsManager;
            _effectsManager = effectsManager;
            _colorManager = colorManager;
            _fileName = FileName;
        }

        public SettingsManager(ILogger logger, INotification notification, IElsSettingsManager elsSettingsManager, string fileName,
            IEffectsManager effectsManager, IColorManager colorManager)
        {
            _logger = logger;
            _notification = notification;
            _fileName = fileName;
            _effectsManager = effectsManager;
            _colorManager = colorManager;
            _logger = logger;
            _notification = notification;
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

                _notification.DisplayPluginNotification("configuration loaded");
                _logger.Info(Settings.ToString());

                //initialize/reinitialize the effect devices and color manager
                _effectsManager.Initialize();
                _colorManager.Initialize(_settings);
            }
            catch (FileNotFoundException)
            {
                _notification.DisplayPluginNotification("configuration file not found, using defaults instead");
                LoadDefaults();
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to load configuration file, error: " + ex.Message, ex);
                _notification.DisplayPluginNotification("configuration file is not valid, using defaults instead");
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