using Corale.Colore;
using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;
using RazerPoliceLights.AbstractionLayer;
using RazerPoliceLights.Effects;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLights.Pattern;
using RazerPoliceLights.Settings;

namespace RazerPoliceLights.Devices.Razer
{
    public class RazerKeyboardEffect : AbstractKeyboardEffect
    {
        private IKeyboard _chromaKeyboard;

        #region Constructors

        public RazerKeyboardEffect(IRage rage, ILogger logger, ISettingsManager settingsManager, IColorManager colorManager)
            : base(rage, logger, settingsManager, colorManager)
        {
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            if (IsDisabled)
                return;

            Logger.Debug("Initializing Chroma.Instance.Keyboard...");
            _chromaKeyboard = Chroma.Instance.Keyboard;

            if (_chromaKeyboard != null)
            {
                Logger.Debug("Initialization Chroma.Instance.Keyboard done");
            }
            else
            {
                Logger.Warn("Chroma.Instance.Keyboard could not be registered, do you have a Chroma supported keyboard?");
            }
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
                        try
                        {
                            _chromaKeyboard[row, column] =
                                GetPlaybackColumnColor(playPattern, patternColumn);
                        }
                        catch (ColoreException ex)
                        {
                            Logger.Warn("Chroma SDK has raised an issue for the keyboard: " + ex.Message, ex);
                        }
                    }
                }

                columnStartIndex = columnEndIndex;
            }
        }

        protected override void OnEffectStop()
        {
            _chromaKeyboard?.SetStatic(new Static(SettingsManager.Settings.ColorSettings.StandbyColor));
        }
    }
}