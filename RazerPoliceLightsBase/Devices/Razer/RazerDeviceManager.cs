using System;
using Corale.Colore.Core;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Utils;

namespace RazerPoliceLightsBase.Devices.Razer
{
    public class RazerDeviceManager : IDeviceManager
    {
        private readonly ILogger _logger;

        public RazerDeviceManager(ILogger logger)
        {
            _logger = logger;
            IoC.Instance
                .RegisterSingleton<IKeyboardEffect>(typeof(RazerKeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(RazerMouseEffect));

            Initialize();
        }

        public IKeyboardEffect KeyboardDevice => IoC.Instance.GetInstance<IKeyboardEffect>();

        public IMouseEffect MouseDevice => IoC.Instance.GetInstance<IMouseEffect>();

        private void Initialize()
        {
            try
            {
                _logger.Info("--- Chroma SDK info ---");
                _logger.Info("Version " + Chroma.Instance.SdkVersion);
                _logger.Info("Initialization state " + Chroma.Instance.Initialized);
                _logger.Info("---");
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to initialize Chroma SDK with exception type '" + ex.GetType() + " and error '" + ex.Message + "'", ex);
                throw new DeviceInitializationException(ex.Message, ex);
            }
        }
    }
}