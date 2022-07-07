using System;
using CUE.NET;
using CUE.NET.Devices.Generic.Enums;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;

namespace RazerPoliceLightsBase.Devices.Corsair
{
    public class CorsairDeviceManager : IDeviceManager
    {
        private readonly ILogger _logger;

        public CorsairDeviceManager(ILogger logger)
        {
            _logger = logger;
            Initialize();
        }

        public IKeyboardEffect KeyboardDevice => IoC.Instance.GetInstance<IKeyboardEffect>();

        public IMouseEffect MouseDevice => IoC.Instance.GetInstance<IMouseEffect>();

        private void Initialize()
        {
            try
            {
                _logger.Debug("Starting registration of CUE devices in IoC...");
                IoC.Instance
                    .RegisterSingleton<IKeyboardEffect>(typeof(CorsairKeyboardEffect))
                    .RegisterSingleton<IMouseEffect>(typeof(CorsairMouseEffect));
                _logger.Debug("Registration done");

                _logger.Debug("Initializing CueSDK...");
                CueSDK.Initialize(true);
                _logger.Debug("CueSDK initialization done");

                _logger.Info("--- CueSDK info ---");
                _logger.Info("Architecture " + CueSDK.LoadedArchitecture);
                _logger.Info("Version " + CueSDK.ProtocolDetails.SdkVersion);
                _logger.Info("Server version " + CueSDK.ProtocolDetails.ServerVersion);
                _logger.Info("Protocol version " + CueSDK.ProtocolDetails.SdkProtocolVersion);
                _logger.Info("Breaking changes " + CueSDK.ProtocolDetails.BreakingChanges);
                _logger.Info("---");

                _logger.Debug("Updating CueSDK mode to 'Continuous'...");
                CueSDK.UpdateMode = UpdateMode.Continuous;
                _logger.Debug("CueSDK mode update done");
            }
            catch (Exception ex)
            {
                _logger.Error(
                    "Failed to initialize CueSDK with exception type '" + ex.GetType() + " and error '" + ex.Message +
                    "'", ex);
                throw new DeviceInitializationException(ex.Message, ex);
            }
        }
    }
}