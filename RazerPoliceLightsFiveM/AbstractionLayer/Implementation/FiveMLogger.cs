using System;
using RazerPoliceLightsBase.AbstractionLayer;

namespace RazerPoliceLightsFiveM.AbstractionLayer.Implementation
{
    public class FiveMLogger : ILogger
    {
        private const string LevelWarn = "WARN";
        private const string LevelError = "ERROR";
        
        public void Trace(string message)
        {
            CitizenFX.Core.Debug.WriteLine(BuildMessage("TRACE", message));
        }

        public void Debug(string message)
        {
            CitizenFX.Core.Debug.WriteLine(BuildMessage("DEBUG", message));
        }

        public void Info(string message)
        {
            CitizenFX.Core.Debug.WriteLine(BuildMessage("INFO", message));
        }

        public void Warn(string message)
        {
            CitizenFX.Core.Debug.WriteLine(BuildMessage(LevelWarn, message));
        }

        public void Warn(string message, Exception exception)
        {
            CitizenFX.Core.Debug.WriteLine(BuildMessage(LevelWarn, message, exception));
        }

        public void Error(string message)
        {
            CitizenFX.Core.Debug.WriteLine(BuildMessage(LevelError, message));
        }

        public void Error(string message, Exception exception)
        {
            CitizenFX.Core.Debug.WriteLine(BuildMessage(LevelError, message, exception));
        }

        private static string BuildMessage(string level, string message, Exception exception = null)
        {
            var stacktrace = exception?.StackTrace;

            return $"[{level}] {message}\n{stacktrace}";
        }
    }
}