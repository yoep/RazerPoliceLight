using System;
using System.Collections.Generic;
using System.Linq;

namespace RazerPoliceLightsBase.Pattern
{
    public class EffectPattern
    {
        /// <summary>
        /// Create a new instance of EffectPattern.
        /// </summary>
        /// <param name="name">Set the name of the effect pattern.</param>
        /// <param name="deviceType">Set the supported device for this pattern.</param>
        /// <param name="patternRows">Set the effect pattern rows.</param>
        /// <exception cref="EffectPatternException">Is thrown when one or more rows are invalid.</exception>
        public EffectPattern(string name, DeviceType deviceType, params PatternRow[] patternRows)
        {
            Name = name;
            SupportedDevice = deviceType;
            PatternRows = new List<PatternRow>(patternRows);
            if (PatternRows == null || PatternRows?.Count == 0)
                throw new EffectPatternException("The effect pattern should contain at least 1 row");
            if (!IsValid(PatternRows))
                throw new EffectPatternException("One or more pattern row(s) are invalid");
        }

        #region Properties

        public string Name { get; }

        public DeviceType SupportedDevice { get; }

        public List<PatternRow> PatternRows { get; }

        public int TotalColumns => PatternRows.ElementAt(0).TotalColumns;

        public int TotalPlaybackRows => PatternRows.Count;

        #endregion

        private bool IsValid(IReadOnlyCollection<PatternRow> patternRows)
        {
            return patternRows.Count != 0 && patternRows.All(e => IsValid(e, TotalColumns));
        }

        private static bool IsValid(PatternRow patternRow, int totalColumns)
        {
            return patternRow.TotalColumns == totalColumns;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}{Environment.NewLine}" +
                   $"{nameof(SupportedDevice)}: {SupportedDevice}{Environment.NewLine}" +
                   $"{nameof(TotalColumns)}: {TotalColumns}{Environment.NewLine}" +
                   $"{nameof(TotalPlaybackRows)}: {TotalPlaybackRows}{Environment.NewLine}";
        }
    }
}