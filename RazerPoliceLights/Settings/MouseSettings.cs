using System.Collections.Generic;
using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Settings
{
    public class MouseSettings
    {
        public bool IsScanEnabled { get; set; }

        public bool IsEnabled { get; set; }

        public bool AnimateVertically { get; set; }

        public List<EffectPattern> EffectPatterns { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(IsScanEnabled)}: {IsScanEnabled}, " +
                $"{nameof(IsEnabled)}: {IsEnabled}, " +
                $"{nameof(AnimateVertically)}: {AnimateVertically}, " +
                $"{nameof(EffectPatterns)}: {EffectPatterns.Count} activated effects";
        }
    }
}