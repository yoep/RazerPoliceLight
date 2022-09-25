using System;
using System.Collections.Generic;
using RazerPoliceLightsBase.Effects;

namespace RazerPoliceLightsBase.Devices
{
    public interface IDeviceManager : IDisposable
    {
        /// <summary>
        /// Get the effect devices from this manager.
        /// </summary>
        IEnumerable<IEffect> Devices { get; }
    }
}