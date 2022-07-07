using RazerPoliceLights.Xml.Attributes;

namespace RazerPoliceLightsBase.Settings
{
    public class PlaybackSettings
    {
        [XmlElement(Name = "Speed")] public double SpeedModifier { get; set; }

        public bool LeaveLightsOn { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(SpeedModifier)}: {SpeedModifier}, " +
                $"{nameof(LeaveLightsOn)}: {LeaveLightsOn}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlaybackSettings) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (SpeedModifier.GetHashCode() * 397) ^ LeaveLightsOn.GetHashCode();
            }
        }

        protected bool Equals(PlaybackSettings other)
        {
            return SpeedModifier.Equals(other.SpeedModifier) && LeaveLightsOn == other.LeaveLightsOn;
        }
    }
}