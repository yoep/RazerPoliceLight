using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Colore;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;

namespace RazerPoliceLightsBase.Devices.Razer
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class RazerDeviceManager : IRazerDeviceManager
    {
        private readonly ILogger _logger;

        public RazerDeviceManager(ILogger logger)
        {
            _logger = logger;
            Initialize();
        }

        /// <inheritdoc />
        public IEnumerable<IEffect> Devices => IoC.Instance.GetInstances<IEffect>()
            .Where(x => x.DeviceSdk == DeviceSdk.Chroma)
            .ToList();
        
        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            var instance = IoC.Instance.GetInstance<IChroma>();
            instance.Dispose();
        }

        #endregion

        private void Initialize()
        {
            if (IoC.Instance.TypeExists<IChroma>())
                return;
            
            try
            {
                var instance = RazerUtils.Instance();
                IoC.Instance
                    .RegisterInstance<IChroma>(instance);

                RegisterChromaDevices(instance);
                _logger.Info("--- Chroma SDK info ---");
                _logger.Info("Version " + instance.SdkVersion);
                _logger.Info("Initialization state " + instance.Initialized);
                _logger.Info("---");
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to initialize Chroma SDK with exception type '" + ex.GetType() + " and error '" + ex.Message + "'", ex);
                throw new DeviceInitializationException(ex.Message, ex);
            }
        }

        private void RegisterChromaDevices(IChroma instance)
        {
            var numberOfDevices = 0;
            
            _logger.Debug("Starting registration of Chroma devices in IoC...");
            if (instance.Keyboard != null)
            {
                _logger.Debug("Chroma keyboard device detected");
                IoC.Instance.RegisterSingleton<IKeyboardEffect>(typeof(RazerKeyboardEffect));
                numberOfDevices++;
            }

            if (instance.Mouse != null)
            {
                _logger.Debug("Chroma mouse device detected");
                IoC.Instance.RegisterSingleton<IMouseEffect>(typeof(RazerMouseEffect));
                numberOfDevices++;
            }
            
            _logger.Debug("Chroma device registration done");
            _logger.Info($"Found a total of {numberOfDevices} Chroma devices");
        }
    }
}