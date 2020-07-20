﻿using System.Collections.Generic;
using RazerPoliceLightsRage.Xml.Attributes;

namespace RazerPoliceLightsBase.Settings
{
    public class KeyboardSettings
    {
        [Xml(Name = "EnableScanMode")] public bool IsScanEnabled { get; set; }

        [Xml(Name = "Enabled")] public bool IsEnabled { get; set; }

        public List<string> Patterns { get; set; }

        public override string ToString()
        {
            return $"{nameof(IsScanEnabled)}: {IsScanEnabled}," +
                   $" {nameof(IsEnabled)}: {IsEnabled}," +
                   $" {nameof(Patterns)}: {Patterns?.Count} activated effects";
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
                hashCode = (hashCode * 397) ^ (Patterns != null ? Patterns.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(KeyboardSettings other)
        {
            return IsScanEnabled == other.IsScanEnabled && IsEnabled == other.IsEnabled && Equals(Patterns, other.Patterns);
        }
    }
}