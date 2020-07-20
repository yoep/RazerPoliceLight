using Rage;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;

namespace RazerPoliceLightsRage.AbstractionLayer.Implementation
{
    public class RageNotification : INotification
    {
        /// <inheritdoc />
        public void DisplayPluginNotification(string message)
        {
            Game.DisplayNotification("~b~" + RazerPoliceLights.Name + " ~s~" + message.Trim());
        }

        /// <inheritdoc />
        public void DisplayNotification(string message)
        {
            Game.DisplayNotification(message.Trim());
        }
    }
}