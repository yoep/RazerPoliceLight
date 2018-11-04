using CUE.NET;
using CUE.NET.Devices.Generic.Enums;
using RazerPoliceLights.Effects;

namespace RazerPoliceLights.Devices.Corsair
{
    public class CorsairDeviceManager : IDeviceManager
    {
        public CorsairDeviceManager()
        {
            IoC.Instance
                .RegisterSingleton<IKeyboardEffect>(typeof(CorsairKeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(CorsairMouseEffect));

            CueSDK.Initialize();
            CueSDK.UpdateMode = UpdateMode.Continuous;
        }

        public IKeyboardEffect KeyboardDevice => IoC.Instance.GetInstance<IKeyboardEffect>();

        public IMouseEffect MouseDevice => IoC.Instance.GetInstance<IMouseEffect>();
    }
}