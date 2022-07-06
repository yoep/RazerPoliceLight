using System;
using System.Diagnostics.CodeAnalysis;
using Rage;
using Rage.Attributes;
using RazerPoliceLightsBase;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Pattern;

namespace RazerPoliceLights.Commands
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class PlaybackCommands
    {
        [ConsoleCommand(Name = "PlayEffects",
            Description = "Plays the Razer Police Lights Effects")]
        public static void Play(
            [ConsoleCommandParameter(Description = "Keyboard or Mouse", AutoCompleterType = typeof(DeviceType))]
            string device = null,
            [ConsoleCommandParameter(Description = "Name of the effect")]
            string effectName = null)
        {
            if (!string.IsNullOrEmpty(device) && !string.IsNullOrEmpty(effectName))
            {
                PlayEffect(device, effectName);
            }
            else
            {
                PlayAllEffects();
            }
        }

        [ConsoleCommand(Name = "StopEffects",
            Description = "Stops the Razer Police Lights Effects")]
        public static void Stop()
        {
            var effectsManager = IoC.Instance.GetInstance<IEffectsManager>();

            if (effectsManager.IsPlaying)
            {
                effectsManager.Stop();
            }
        }

        private static void PlayEffect(string device, string effectName)
        {
            DeviceType deviceType;

            if (Enum.TryParse(device, out deviceType))
            {
                var effectPattern = EffectPatternManager.Instance.GetByName(deviceType, effectName);

                if (effectPattern != null)
                {
                    IoC.Instance.GetInstance<IEffectsManager>().Play(null, effectPattern);
                }
                else
                {
                    Game.LogTrivial("Effect pattern '" + effectName + "' is not found for device " + device);
                }
            }
            else
            {
                Game.LogTrivial("Device " + device + " not found");
            }
        }

        private static void PlayAllEffects()
        {
            var effectsManager = IoC.Instance.GetInstance<IEffectsManager>();

            if (!effectsManager.IsPlaying)
            {
                effectsManager.Play(null);
            }
        }
    }
}