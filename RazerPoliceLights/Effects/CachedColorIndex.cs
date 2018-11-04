using RazerPoliceLights.Pattern;

namespace RazerPoliceLights.Effects
{
    public class CachedColorIndex
    {
        private readonly ColorType _colorType;
        private readonly int _index;

        public CachedColorIndex(ColorType colorType, int index)
        {
            _colorType = colorType;
            _index = index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CachedColorIndex) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) _colorType * 397) ^ _index;
            }
        }

        protected bool Equals(CachedColorIndex other)
        {
            return _colorType == other._colorType && _index == other._index;
        }
    }
}