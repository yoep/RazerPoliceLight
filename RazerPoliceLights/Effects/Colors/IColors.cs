using Corale.Colore.Core;

namespace RazerPoliceLights.Effects.Colors
{
    public interface IColors
    {
        /// <summary>
        /// Get the color for the given index.
        /// </summary>
        /// <param name="index">Set the pattern row index.</param>
        /// <param name="max">Set the pattern max row index.</param>
        Color this[int index, int max] { get; }
        
        /// <summary>
        /// Get or set the current vehicle name.
        /// </summary>
        string VehicleName { get; set; }
    }
}