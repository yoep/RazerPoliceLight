﻿using System.Collections.Generic;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLights.Settings
{
    public class KeyboardSettings
    {
        [XmlAttribute(Name = "EnableScanMode")] public bool IsScanEnabled { get; set; }

        [XmlAttribute(Name = "Enabled")] public bool IsEnabled { get; set; }

        [XmlIgnore]
        public List<EffectPattern> EffectPatterns { get; set; }

        public override string ToString()
        {
            return $"{nameof(IsScanEnabled)}: {IsScanEnabled}," +
                   $" {nameof(IsEnabled)}: {IsEnabled}," +
                   $" {nameof(EffectPatterns)}: {EffectPatterns?.Count} activated effects";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyboardSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsScanEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ IsEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ (EffectPatterns != null ? EffectPatterns.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(KeyboardSettings other)
        {
            return IsScanEnabled == other.IsScanEnabled && IsEnabled == other.IsEnabled && Equals(EffectPatterns, other.EffectPatterns);
        }
    }
}