using CUE.NET;
using CUE.NET.Devices.Generic.Enums;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Rage;

namespace RazerPoliceLights.Devices.Corsair
{
    public class CorsairDeviceManager : IDeviceManager
    {
        private readonly IRage _rage;
        
        public CorsairDeviceManager(IRage rage)
        {
            _rage = rage;
            Initialize();
        }

        public IKeyboardEffect KeyboardDevice => IoC.Instance.GetInstance<IKeyboardEffect>();

        public IMouseEffect MouseDevice => IoC.Instance.GetInstance<IMouseEffect>();

        private void Initialize()
        {
            _rage.LogTrivialDebug("Starting registration of CUE devices in IoC...");
            IoC.Instance
                .RegisterSingleton<IKeyboardEffect>(typeof(CorsairKeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(CorsairMouseEffect));
            _rage.LogTrivialDebug("Registration done");

            _rage.LogTrivialDebug("Initializing CueSDK...");
            CueSDK.Initialize(true);
            _rage.LogTrivialDebug("CueSDK initialization done");
            
            _rage.LogTrivial("--- CueSDK info ---");
            _rage.LogTrivial("Architecture " + CueSDK.LoadedArchitecture);
            _rage.LogTrivial("Version " + CueSDK.ProtocolDetails.SdkVersion);
            _rage.LogTrivial("Server version " + CueSDK.ProtocolDetails.ServerVersion);
            _rage.LogTrivial("Protocol version " + CueSDK.ProtocolDetails.SdkProtocolVersion);
            _rage.LogTrivial("Breaking changes " + CueSDK.ProtocolDetails.BreakingChanges);
            _rage.LogTrivial("---");
            
            _rage.LogTrivialDebug("Updating CueSDK mode to 'Continuous'...");
            CueSDK.UpdateMode = UpdateMode.Continuous;
            _rage.LogTrivialDebug("CueSDK mode update done");
        }
    }
}