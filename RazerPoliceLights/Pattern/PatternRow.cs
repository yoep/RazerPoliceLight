using System.Collections.Generic;

namespace RazerPoliceLights.Pattern
{
    public class PatternRow
    {
        private readonly int _totalColums;

        public PatternRow(params ColorType[] colors)
        {
            _totalColums = colors.Length;
            ColorColumns = new List<ColorType>(colors);
        }

        public int TotalColums
        {
            get { return _totalColums; }
        }

        public List<ColorType> ColorColumns { get; private set; }
    }
}