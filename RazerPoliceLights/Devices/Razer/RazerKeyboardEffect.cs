using System.Linq;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Rage;
using RazerPoliceLights.Settings;
using RazerPoliceLights.Settings.Els;

namespace RazerPoliceLights.Devices.Razer
{
    public class RazerKeyboardEffect : AbstractKeyboardEffect
    {
        private IKeyboard _chromaKeyboard;

        #region Constructors

        public RazerKeyboardEffect(IRage rage, ISettingsManager settingsManager, IElsSettingsManager elsSettingsManager)
            : base(rage, settingsManager, elsSettingsManager)
        {
            Initialize();
        }

        #endregion

        protected override void OnEffectTick(PatternRow playPattern)
        {
            if (_chromaKeyboard == null)
                return; //something probably went wrong during initialization, ignore this device effect playback
            
            var columnSize = Constants.MaxColumns / playPattern.TotalColumns;
            var columnStartIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + columnSize;

                if (IsMismatchingLastEndIndex(playPattern, Constants.MaxColumns, patternColumn, columnEndIndex))
                {
                    columnEndIndex = Constants.MaxColumns;
                }

                for (var row = 0; row < Constants.MaxRows; row++)
                {
                    for (var column = columnStartIndex; column < columnEndIndex; column++)
                    {
                        _chromaKeyboard[row, column] =
                            GetPlaybackColumnColor(playPattern.ColorColumns.ElementAt(patternColumn), patternColumn);
                    }
                }

                columnStartIndex = columnEndIndex;
            }
        }

        protected override void OnEffectStop()
        {
            _chromaKeyboard.SetStatic(new Static(SettingsManager.Settings.ColorSettings.StandbyColor));
        }

        private void Initialize()
        {
            if (IsDisabled)
                return;

            Rage.LogTrivialDebug("Initializing Chroma.Instance.Keyboard...");
            _chromaKeyboard = Chroma.Instance.Keyboard;

            if (_chromaKeyboard != null)
            {
                Rage.LogTrivialDebug("Initialization Chroma.Instance.Keyboard done");
            }
            else
            {
                Rage.LogTrivial("Chroma.Instance.Keyboard could not be registered, do you have a Chroma supported keyboard?");
            }
        }
    }
}