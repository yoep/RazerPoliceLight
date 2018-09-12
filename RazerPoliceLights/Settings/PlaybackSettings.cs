namespace RazerPoliceLights.Settings
{
    public class PlaybackSettings
    {
        public double SpeedModifier { get; set; }

        public bool LeaveLightsOn { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(SpeedModifier)}: {SpeedModifier}, " +
                $"{nameof(LeaveLightsOn)}: {LeaveLightsOn}";
        }
    }
}