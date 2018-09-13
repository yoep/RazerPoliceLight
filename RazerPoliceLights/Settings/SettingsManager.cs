using System;
using System.IO;
using System.Xml;
using Rage;
using RazerPoliceLights.Settings.Loaders;

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

        public static SettingsManager Instance { get; private set; }

        public Settings Settings { get; private set; }

        #endregion

        #region Methods

        public void Load()
        {
            var document = new XmlDocument();

            try
            {
                document.Load(FileName);

                Settings = new Settings
                {
                    PlaybackSettings = PlaybackSettingsLoader.Load(document),
                    ColorSettings = ColorSettingsLoader.Load(document),
                    DeviceSettings = DeviceSettingsLoader.Load(document),
                    EffectPatterns = EffectPatternLoad.Load(document)
                };
                
                Game.DisplayNotification("Razer Police Lights Keyboard configuration loaded");
                Game.LogTrivial(Settings.ToString());
            }
            catch (FileNotFoundException)
            {
                Game.DisplayNotification("Razer Police Lights configuration file not found, using defaults instead");
                LoadDefauls();
            }
            catch (Exception e)
            {
                Game.LogTrivial(e.Message + Environment.NewLine + e.StackTrace);
                Game.DisplayNotification("Razer Police Lights configuration file is not valid, using defaults instead");
                LoadDefauls();
            }
        }

        #endregion

        private void LoadDefauls()
        {
            Settings = Settings.Defaults;
        }
    }
}