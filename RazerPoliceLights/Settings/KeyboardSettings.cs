using System.Collections.Generic;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Settings
{
    public class KeyboardSettings
    {
        public bool IsScanEnabled { get; set; }

        public bool IsEnabled { get; set; }

        public List<EffectPattern> EffectPatterns { get; set; }

        public override string ToString()
        {
            return $"{nameof(IsScanEnabled)}: {IsScanEnabled}," +
                   $" {nameof(IsEnabled)}: {IsEnabled}," +
                   $" {nameof(EffectPatterns)}: {EffectPatterns.Count}";
        }
    }
}