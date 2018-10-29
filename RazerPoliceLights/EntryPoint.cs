using System.Diagnostics.CodeAnalysis;
using Rage;
using Rage.Attributes;
using RazerPoliceLights.Effects;
using RazerPoliceLights.GameListeners;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;

[assembly:
    Plugin(RazerPoliceLights.RazerPoliceLights.Name,
        PrefersSingleInstance = true,
        Description = "Razer Keyboard & Mouse lighting effects",
        Author = "yoep",
        ExitPoint = "RazerPoliceLights.EntryPoint.OnUnload")]

namespace RazerPoliceLights
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class EntryPoint
    {
        public static void Main()
        {
            while (Game.IsLoading)
                GameFiber.Yield();

            InitializeIoContainer();
            GameFiber.StartNew(IoC.Instance.GetInstance<IVehicleListener>().Start);
        }

        public static void OnUnload(bool isTerminating)
        {
            Game.LogTrivialDebug(RazerPoliceLights.Name + " received unload command with termination " + isTerminating);
            IoC.Instance.GetInstance<IVehicleListener>().Stop();
            IoC.Instance.GetInstance<IEffectsManager>().OnUnload(isTerminating);
        }

        private static void InitializeIoContainer()
        {
            IoC.Instance
                .Register<IRage>(typeof(RageImpl))
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IKeyboardEffect>(typeof(KeyboardEffect))
                .RegisterSingleton<IMouseEffect>(typeof(MouseEffect))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IVehicleListener>(typeof(VehicleListener));
        }
    }
}