using System;
using System.IO;
using System.Linq;
using Rage;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Xml;

namespace RazerPoliceLights.Settings
{
    public class SettingsManager
    {
        private const string FileName = @"./Plugins/RazerPoliceLights.xml";

        #region Constructors

        static SettingsManager()
        {
            Instance = new SettingsManager();
        }

        private SettingsManager()
        {
        }

        #endregion

        #region Getters & Setters

        public static SettingsManager Instance { get; }

        public Settings Settings { get; private set; }

        #endregion

        #region Methods

        public void Load()
        {
            var objectMapper = ObjectMapperFactory.CreateInstance();

            try
            {
                Settings = objectMapper.ReadValue<Settings>(FileName, typeof(Settings));
                UpdateEffectManager();

                Game.DisplayNotification(RazerPoliceLights.Name + " configuration loaded");
                Game.LogTrivial(Settings.ToString());
            }
            catch (FileNotFoundException)
            {
                Game.DisplayNotification(RazerPoliceLights.Name +
                                         " configuration file not found, using defaults instead");
                LoadDefaults();
            }
            catch (Exception e)
            {
                Game.LogTrivial(e.Message + Environment.NewLine + e.StackTrace);
                Game.DisplayNotification(RazerPoliceLights.Name +
                                         " configuration file is not valid, using defaults instead");
                LoadDefaults();
            }
        }

        #endregion

        private void LoadDefaults()
        {
            Settings = Settings.Defaults;
            UpdateEffectManager();
        }

        private void UpdateEffectManager()
        {
            var effectPatternManager = EffectPatternManager.Instance;
            effectPatternManager.Clear();

            //only add the effects which are enabled in the device settings to the effect manager
            effectPatternManager.AddAll(Settings.EffectPatterns[DeviceType.Keyboard]
                .Where(x => Settings.DeviceSettings.KeyboardSettings.Patterns.Contains(x.Name))
                .ToList());
            effectPatternManager.AddAll(Settings.EffectPatterns[DeviceType.Mouse]
                .Where(x => Settings.DeviceSettings.MouseSettings.Patterns.Contains(x.Name))
                .ToList());
        }
    }
}