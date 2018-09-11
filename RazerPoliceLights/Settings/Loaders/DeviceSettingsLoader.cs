using System.Xml;

namespace RazerPoliceLights.Settings.Loaders
{
    public class DeviceSettingsLoader : AbstractLoader
    {
        private const string DeviceSettingsPath = "RazerPoliceLights/Devices";
        private const string KeyboardPath = "Keyboard";
        private const string MousePath = "Mouse";
        private const string EnableScanModeAttribute = "EnableScanMode";

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
                    IsScanEnabled = bool.Parse(GetAttributeValue(keyboardNode, EnableScanModeAttribute))
                },
                MouseSettings = new MouseSettings
                {
                    IsScanEnabled = bool.Parse(GetAttributeValue(mouseNode, EnableScanModeAttribute))
                }
            };
        }
    }
}