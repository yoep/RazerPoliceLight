using System.Collections.Generic;

namespace RazerPoliceLightsBase.Pattern
{
    public class PatternRow
    {
        private readonly int _totalColumns;

        /// <summary>
        /// Initialize a new instance of PatternRow.
        /// </summary>
        /// <param name="speed">Set the speed factor of the pattern row.</param>
        /// <param name="colors">Set the colors within the pattern.</param>
        public PatternRow(double speed, params ColorType[] colors)
        {
            _totalColumns = colors.Length;
            Speed = speed;
            ColorColumns = new List<ColorType>(colors);
        }

        #region Properties

        public double Speed { get; private set; }

        public int TotalColumns => _totalColumns;

        public List<ColorType> ColorColumns { get; private set; }

        #endregion
    }
}