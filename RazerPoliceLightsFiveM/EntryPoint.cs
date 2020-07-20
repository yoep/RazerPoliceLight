using System;
using System.Collections.Generic;
using CitizenFX.Core;
using RazerPoliceLightsFiveM.Commands;
using static CitizenFX.Core.Native.API;

namespace RazerPoliceLightsFiveM
{
    public class EntryPoint : BaseScript
    {
        public EntryPoint()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            RegisterCommand("PoliceLightsReloadSettings", new Action<int, List<object>, string>((source, args, raw) =>
            {
                SettingsCommands.ReloadSettings();
            }), false);
        }
    }
}