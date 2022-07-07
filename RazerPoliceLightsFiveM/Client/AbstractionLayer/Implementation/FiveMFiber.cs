using System;
using System.Threading;
using RazerPoliceLightsBase.AbstractionLayer;

namespace RazerPoliceLightsFiveM.Client.AbstractionLayer.Implementation
{
    public class FiveMFiber : IGameFiber
    {
        private readonly INotification _notification;
        private readonly ILogger _logger;

        public FiveMFiber(INotification notification, ILogger logger)
        {
            _notification = notification;
            _logger = logger;
        }

        /// <inheritdoc />
        public void NewSafeFiber(Action action, string name)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    action.Invoke();
                }
                catch (ThreadInterruptedException)
                {
                    //ignore as this is probably on plugin termination and thread is in waiting state
                }
                catch (ThreadAbortException)
                {
                    //ignore as this is probably on plugin termination and thread was executing a method and couldn't exit correctly
                }
                catch (Exception ex)
                {
                    _logger.Error("An unexpected error occurred in '" + name + "' thread, error: " + ex.Message, ex);
                    _notification.DisplayPluginNotification("~r~" + name + " thread has stopped working, see logs for more info");
                }
            }) {Name = name};

            thread.Start();
        }

        /// <inheritdoc />
        public void FiberYield()
        {
            // no-op
        }
    }
}