using System;
using CitizenFX.Core;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Effects.Colors;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;
using RazerPoliceLightsFiveM.AbstractionLayer.Implementation;
using RazerPoliceLightsFiveM.Commands;
using RazerPoliceLightsFiveM.GameListeners;
using RazerPoliceLightsRage.Effects.Colors;
using static CitizenFX.Core.Native.API;

namespace RazerPoliceLightsFiveM
{
    public class EntryPoint : BaseScript
    {
        public EntryPoint()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
            EventHandlers["onClientResourceStop"] += new Action<string>(OnClientResourceStop);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            InitializeIoContainer();
            RegisterCommands();
            StartListener();
        }

        private void OnClientResourceStop(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            StopListener();
        }

        private static void InitializeIoContainer()
        {
            IoC.Instance
                .RegisterSingleton<INotification>(typeof(FiveMNotification))
                .RegisterSingleton<IGameFiber>(typeof(FiveMFiber))
                .RegisterSingleton<ILogger>(typeof(FiveMLogger))
                .RegisterSingleton<ICommand>(typeof(SettingsCommands))
                .RegisterSingleton<ISettingsManager>(typeof(SettingsManager))
                .RegisterSingleton<IElsSettingsManager>(typeof(ElsSettingsManager))
                .RegisterSingleton<IEffectsManager>(typeof(EffectsManager))
                .RegisterSingleton<IColorManager>(typeof(ColorManagerImpl))
                .RegisterSingleton<IVehicleListener>(typeof(VehicleListener));
        }

        private void RegisterCommands()
        {
            var commands = IoC.Instance.GetInstances<ICommand>();

            foreach (var command in commands)
            {
                command.Register();
            }
        }

        private void StartListener()
        {
            var vehicleListener = IoC.Instance.GetInstance<IVehicleListener>();
            
            vehicleListener.Start();
        }

        private void StopListener()
        {
            var vehicleListener = IoC.Instance.GetInstance<IVehicleListener>();
            
            vehicleListener.Stop();
        }
    }
}