using System;
using Corale.Colore.Core;

namespace RazerPoliceLights.Settings
{
    public class ColorSettings
    {
        public Color PrimaryColor { get; internal set; }

        public Color SecondaryColor { get; internal set; }

        public Color StandbyColor { get; internal set; }

        public override string ToString()
        {
            return
                $"{nameof(PrimaryColor)}: {PrimaryColor.Value}," +
                $" {nameof(SecondaryColor)}: {SecondaryColor.Value}," +
                $" {nameof(StandbyColor)}: {StandbyColor.Value}";
        }
    }
}