using System.Diagnostics.CodeAnalysis;
using Rage.Attributes;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Utils;

namespace RazerPoliceLightsRage.Commands
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class SettingsCommands
    {
        [ConsoleCommand(Name = "PoliceLightsReloadSettings",
            Description = "Reloads the settings from RazerPoliceLights.xml")]
        public static void ReloadSettings()
        {
            IoC.Instance.GetInstance<ISettingsManager>().Load();
        }
    }
}