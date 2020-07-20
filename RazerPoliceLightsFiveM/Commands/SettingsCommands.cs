using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Utils;

namespace RazerPoliceLightsFiveM.Commands
{
    public class SettingsCommands
    {
        public static void ReloadSettings()
        {
            IoC.Instance.GetInstance<ISettingsManager>().Load();
        }
    }
}