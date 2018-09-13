﻿using System.Diagnostics.CodeAnalysis;
using Rage.Attributes;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Commands
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class SettingsCommands
    {
        [ConsoleCommand(Name = "PoliceLightsReloadSettings",
            Description = "Reloads the settings from RazerPoliceLights.xml")]
        public static void ReloadSettings()
        {
            SettingsManager.Instance.Load();
        }
    }
}