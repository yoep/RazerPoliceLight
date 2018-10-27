using System;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    public class DeviceSettings
    {
        [XmlElement(Name = "Keyboard")]
        public KeyboardSettings KeyboardSettings { get; set; }
        
        [XmlElement(Name = "Mouse")]
        public MouseSettings MouseSettings { get; set; }
       
        public override string ToString()
        {
            return $"----{nameof(KeyboardSettings)}----{Environment.NewLine}{KeyboardSettings},{Environment.NewLine}" +
                   $"----{nameof(MouseSettings)}----{Environment.NewLine}{MouseSettings}";
        }
    }
}