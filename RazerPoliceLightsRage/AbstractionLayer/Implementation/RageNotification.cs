using Rage;
using RazerPoliceLightsBase.AbstractionLayer;

namespace RazerPoliceLights.AbstractionLayer.Implementation
{
    public class RageNotification : INotification
    {
        /// <inheritdoc />
        public void DisplayPluginNotification(string message)
        {
            Game.DisplayNotification("~b~" + RazerPoliceLightsBase.RazerPoliceLightsPlugin.Name + " ~s~" + message.Trim());
        }

        /// <inheritdoc />
        public void DisplayNotification(string message)
        {
            Game.DisplayNotification(message.Trim());
        }
    }
}