using RazerPoliceLights.Effects;

namespace RazerPoliceLights.Devices.Razer
{
    public class RazerDeviceManager : IDeviceManager
    {
        public RazerDeviceManager()
        {
            IoC.Instance
                .RegisterSingleton<IKeyboardEffect>(typeof(RazerKeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(RazerMouseEffect));
        }

        public IKeyboardEffect KeyboardDevice => IoC.Instance.GetInstance<IKeyboardEffect>();

        public IMouseEffect MouseDevice => IoC.Instance.GetInstance<IMouseEffect>();
    }
}