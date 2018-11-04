using RazerPoliceLights.Effects;

namespace RazerPoliceLights.Devices
{
    public interface IDeviceManager
    {
        IKeyboardEffect KeyboardDevice { get; }

        IMouseEffect MouseDevice { get; }
    }
}