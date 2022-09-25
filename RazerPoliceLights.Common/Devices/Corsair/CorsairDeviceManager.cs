using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CUE.NET;
using CUE.NET.Devices.Generic.Enums;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;

namespace RazerPoliceLightsBase.Devices.Corsair
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public class CorsairDeviceManager : ICorsairDeviceManager
    {
        private readonly ILogger _logger;

        public CorsairDeviceManager(ILogger logger)
        {
            _logger = logger;
            Initialize();
        }

        #region IDeviceManager

        /// <inheritdoc />
        public IEnumerable<IEffect> Devices => IoC.Instance.GetInstances<IEffect>()
            .Where(x => x.DeviceSdk == DeviceSdk.ICue)
            .ToList();

        #endregion

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            CueSDK.Reset();
        }

        #endregion

        private void Initialize()
        {
            try
            {
                _logger.Debug("Initializing CueSDK...");
                CueSDK.Initialize(true);
                _logger.Debug("CueSDK initialization done");
                
                RegisterCueDevices();

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

        private void RegisterCueDevices()
        {
            var numberOfDevices = 0;
            
            _logger.Debug("Starting registration of CUE devices in IoC...");
            foreach (var device in CueSDK.InitializedDevices)
            {
                switch (device.DeviceInfo.Type)
                {
                    case CorsairDeviceType.Keyboard:
                        _logger.Debug("CUE keyboard device detected");
                        IoC.Instance.RegisterSingleton<IKeyboardEffect>(typeof(CorsairKeyboardEffect));
                        numberOfDevices++;
                        break;
                    case CorsairDeviceType.Mouse:
                        _logger.Debug("CUE mouse device detected");
                        IoC.Instance.RegisterSingleton<IMouseEffect>(typeof(CorsairMouseEffect));
                        numberOfDevices++;
                        break;
                }
            }
            _logger.Debug("CUE device registration done");
            _logger.Info($"Found a total of {numberOfDevices} CUE devices");
        }
    }
}