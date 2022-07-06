using CitizenFX.Core;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.AbstractionLayer;

namespace RazerPoliceLightsFiveM.AbstractionLayer.Implementation
{
    public class FiveMNotification : INotification
    {
        /// <inheritdoc />
        public void DisplayPluginNotification(string message)
        {
            BaseScript.TriggerEvent("chat:addMessage", new
            {
                color = new[] {0, 0, 255},
                multiline = true,
                args = new[] {RazerPoliceLightsPlugin.Name, message.Trim()}
            });
        }

        /// <inheritdoc />
        public void DisplayNotification(string message)
        {
            BaseScript.TriggerEvent("chat:addMessage", new
            {
                color = new[] {255, 255, 255},
                multiline = true,
                args = new[] {message.Trim()}
            });
        }
    }
}