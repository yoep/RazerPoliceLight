using System.Collections.Generic;
using System.Xml;
using Rage;
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
        private const string AnimateVerticallyAttribute = "AnimateVertically";

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

            var effectPatternsKeyboard = DevicePatternsSettingsLoader.Load(keyboardNode, DeviceType.Keyboard);
            var effectPatternsMouse = DevicePatternsSettingsLoader.Load(mouseNode, DeviceType.Mouse);

            if (effectPatternsKeyboard.Count == 0)
                DisplayDeviceDisabledNotification(DeviceType.Keyboard);
            if (effectPatternsMouse.Count == 0)
                DisplayDeviceDisabledNotification(DeviceType.Mouse);

            return new DeviceSettings
            {
                KeyboardSettings = new KeyboardSettings
                {
                    IsScanEnabled = GetBoolAttributeValue(keyboardNode, EnableScanModeAttribute, true),
                    EffectPatterns = effectPatternsKeyboard,
                    IsEnabled = GetIfDeviceIsEnabled(keyboardNode, effectPatternsKeyboard)
                },
                MouseSettings = new MouseSettings
                {
                    IsScanEnabled = GetBoolAttributeValue(mouseNode, EnableScanModeAttribute, true),
                    EffectPatterns = effectPatternsMouse,
                    IsEnabled = GetIfDeviceIsEnabled(mouseNode, effectPatternsMouse),
                    AnimateVertically = GetBoolAttributeValue(mouseNode, AnimateVerticallyAttribute, false)
                }
            };
        }

        private static bool GetIfDeviceIsEnabled(
            XmlNode keyboardNode,
            IReadOnlyCollection<EffectPattern> effectPatterns)
        {
            return effectPatterns.Count > 0 && GetBoolAttributeValue(keyboardNode, EnabledAttribute, true);
        }

        private static void DisplayDeviceDisabledNotification(DeviceType deviceType)
        {
            var message = "Disabling " + deviceType + " device because it has no effects it can run or configured";
            Game.LogTrivial(message);
            Game.DisplayNotification(message);
        }
    }
}