using RazerPoliceLightsBase.Effects;

namespace RazerPoliceLightsBase.Devices
{
    public interface IDeviceManager
    {
        IKeyboardEffect KeyboardDevice { get; }

        IMouseEffect MouseDevice { get; }
    }
}