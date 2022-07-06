using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Effects.Colors;
using RazerPoliceLightsBase.GameListeners;
using RazerPoliceLightsBase.Settings;
using RazerPoliceLightsBase.Settings.Els;
using RazerPoliceLightsFiveM.AbstractionLayer.Implementation;
using RazerPoliceLightsFiveM.Client.AbstractionLayer.Implementation;
using RazerPoliceLightsFiveM.Client.Commands;
using RazerPoliceLightsFiveM.Client.GameListeners;
using static CitizenFX.Core.Native.API;

namespace RazerPoliceLightsFiveM.Client
{
    public class ClientMain : BaseScript
    {
        public EntryPoint()
        {
            Debug.WriteLine("Starting RazerPoliceLightsFiveM");
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
            var ioC = IoC.Instance;
            var logger = ioC.GetInstance<ILogger>();
            var commands = ioC.GetInstances<ICommand>();

            logger.Debug("Registering " + commands.Count + " command(s)");
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
