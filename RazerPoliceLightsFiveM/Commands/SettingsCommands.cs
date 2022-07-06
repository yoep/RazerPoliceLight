using System;
using System.Collections.Generic;
using CitizenFX.Core.Native;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Settings;

namespace RazerPoliceLightsFiveM.Commands
{
    public class SettingsCommands : ICommand
    {
        private readonly ILogger _logger;

        public SettingsCommands(ILogger logger)
        {
            _logger = logger;
        }

        public void Register()
        {
            _logger.Debug("Registering settings commands");
            API.RegisterCommand("PoliceLightsReloadSettings",
                new Action<int, List<object>, string>((source, args, raw) => { ReloadSettings(); }), false);
        }

        private static void ReloadSettings()
        {
            var logger = IoC.Instance.GetInstance<ILogger>();
            
            logger.Debug("Reload settings command invoked");
            IoC.Instance.GetInstance<ISettingsManager>().Load();
        }
    }
}