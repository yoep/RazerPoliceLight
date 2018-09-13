using System.Xml;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Settings.Loaders
{
    public class DeviceSettingsLoader : AbstractLoader
    {
        private const string DeviceSettingsPath = "RazerPoliceLights/Devices";
        private const string KeyboardPath = "Keyboard";
        private const string MousePath = "Mouse";
        private const string EnableScanModeAttribute = "EnableScanMode";
        private const string EnabledAttribute = "Enabled";

        public static DeviceSettings Load(XmlNode document)
        {
            var deviceSettingsNode = document.SelectSingleNode(DeviceSettingsPath);

            if (deviceSettingsNode == null)
                throw new SettingsException("Device settings configuration is missing in the configuration file");

            var keyboardNode = deviceSettingsNode.SelectSingleNode(KeyboardPath);
            var mouseNode = deviceSettingsNode.SelectSingleNode(MousePath);

            if (keyboardNode == null)
                throw new SettingsException(
                    "Keyboard device settings configuration is missing in the configuration file");
            if (mouseNode == null)
                throw new SettingsException("Mouse device settings configuration is missing in the configuration file");
            
            return new DeviceSettings
            {
                KeyboardSettings = new KeyboardSettings
                {
                    IsScanEnabled = GetBoolAttributeValue(keyboardNode, EnableScanModeAttribute, true),
                    IsEnabled = GetBoolAttributeValue(keyboardNode, EnabledAttribute, true),
                    EffectPatterns = DevicePatternsSettingsLoader.Load(keyboardNode, DeviceType.Keyboard)
                },
                MouseSettings = new MouseSettings
                {
                    IsScanEnabled = GetBoolAttributeValue(mouseNode, EnableScanModeAttribute, true),
                    IsEnabled = GetBoolAttributeValue(mouseNode, EnabledAttribute, true),
                    EffectPatterns = DevicePatternsSettingsLoader.Load(mouseNode, DeviceType.Mouse)
                }
            };
        }
    }
}