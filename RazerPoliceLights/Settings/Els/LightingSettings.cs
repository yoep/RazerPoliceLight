namespace RazerPoliceLights.Settings.Els
{
    public class LightingSettings
    {
        public ExtraSettings Extra01 { get; set; }

        public ExtraSettings Extra02 { get; set; }

        public ExtraSettings Extra03 { get; set; }

        public ExtraSettings Extra04 { get; set; }

        public ExtraSettings Extra05 { get; set; }

        public ExtraSettings Extra06 { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LightingSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Extra01 != null ? Extra01.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Extra02 != null ? Extra02.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Extra03 != null ? Extra03.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Extra04 != null ? Extra04.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Extra05 != null ? Extra05.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Extra06 != null ? Extra06.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(LightingSettings other)
        {
            return Equals(Extra01, other.Extra01) && Equals(Extra02, other.Extra02) && Equals(Extra03, other.Extra03) && Equals(Extra04, other.Extra04) &&
                   Equals(Extra05, other.Extra05) && Equals(Extra06, other.Extra06);
        }
    }
}