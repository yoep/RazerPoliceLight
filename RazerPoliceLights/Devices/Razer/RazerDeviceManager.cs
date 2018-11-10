using Corale.Colore.Core;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Rage;

namespace RazerPoliceLights.Devices.Razer
{
    public class RazerDeviceManager : IDeviceManager
    {
        private readonly IRage _rage;
        
        public RazerDeviceManager(IRage rage)
        {
            _rage = rage;
            IoC.Instance
                .RegisterSingleton<IKeyboardEffect>(typeof(RazerKeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(RazerMouseEffect));

            Initialize();
        }

        public IKeyboardEffect KeyboardDevice => IoC.Instance.GetInstance<IKeyboardEffect>();

        public IMouseEffect MouseDevice => IoC.Instance.GetInstance<IMouseEffect>();

        private void Initialize()
        {
            _rage.LogTrivial("--- Chroma SDK info ---");
            _rage.LogTrivial("Version " + Chroma.Instance.SdkVersion);
            _rage.LogTrivial("Initialization state " + Chroma.Instance.Initialized);
            _rage.LogTrivial("---");
        }
    }
}