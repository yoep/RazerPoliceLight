using System;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLightsBase.Settings
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DeviceSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((KeyboardSettings != null ? KeyboardSettings.GetHashCode() : 0) * 397) ^ (MouseSettings != null ? MouseSettings.GetHashCode() : 0);
            }
        }

        protected bool Equals(DeviceSettings other)
        {
            return Equals(KeyboardSettings, other.KeyboardSettings) && Equals(MouseSettings, other.MouseSettings);
        }
    }
}