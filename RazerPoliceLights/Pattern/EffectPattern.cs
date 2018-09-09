using System.Collections.Generic;
using System.Linq;

namespace RazerPoliceLights.Pattern
{
    public class EffectPattern
    {
        public EffectPattern(params PatternRow[] patternRows)
        {
            PatternRows = new List<PatternRow>(patternRows);
            if (!IsValid(PatternRows))
                throw new EffectPatternException("One or more pattern row(s) are invalid");
        }

        public List<PatternRow> PatternRows { get; private set; }

        public int TotalColumns
        {
            get { return PatternRows.ElementAt(0).TotalColums; }
        }
        
        public int TotalPlaybackRows
        {
            get { return PatternRows.Count; }
        }

        private bool IsValid(IReadOnlyCollection<PatternRow> patternRows)
        {
            return patternRows.Count != 0 && patternRows.All(e => IsValid(e, TotalColumns));
        }

        private static bool IsValid(PatternRow patternRow, int totalColumns)
        {
            return patternRow.TotalColums == totalColumns;
        }
    }
}