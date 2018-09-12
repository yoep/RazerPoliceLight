using System;

namespace RazerPoliceLights.Settings
{
    public class DeviceSettings
    {
        public KeyboardSettings KeyboardSettings { get; set; }
        
        public MouseSettings MouseSettings { get; set; }

        public override string ToString()
        {
            return $"----{nameof(KeyboardSettings)}----{Environment.NewLine}{KeyboardSettings}," +
                   $"{Environment.NewLine}----{nameof(MouseSettings)}----{Environment.NewLine}{MouseSettings}";
        }
    }
}