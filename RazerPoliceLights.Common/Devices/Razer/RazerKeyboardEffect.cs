using Colore;
using Colore.Effects.Keyboard;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;

namespace RazerPoliceLightsBase.Devices.Razer
{
    public class RazerKeyboardEffect : AbstractKeyboardEffect
    {
        private readonly IChroma _chroma;
        private IKeyboard _chromaKeyboard;

        #region Constructors

        public RazerKeyboardEffect(IChroma chroma, INotification notification, ILogger logger, ISettingsManager settingsManager, IColorManager colorManager)
            : base(notification, logger, settingsManager, colorManager)
        {
            _chroma = chroma;
        }

        #endregion

        #region Methods

        public override void Initialize()
        {
            if (IsDisabled)
                return;

            Logger.Debug("Initializing Chroma.Instance.Keyboard...");
            _chromaKeyboard = _chroma.Keyboard;

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

            var columnSize = KeyboardConstants.MaxColumns / playPattern.TotalColumns;
            var columnStartIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = columnStartIndex + columnSize;

                if (IsMismatchingLastEndIndex(playPattern, KeyboardConstants.MaxColumns, patternColumn, columnEndIndex))
                {
                    columnEndIndex = KeyboardConstants.MaxColumns;
                }

                for (var row = 0; row < KeyboardConstants.MaxRows; row++)
                {
                    for (var column = columnStartIndex; column < columnEndIndex; column++)
                    {
                        try
                        {
                            _chromaKeyboard[row, column] =
                                RazerUtils.Convert(GetPlaybackColumnColor(playPattern, patternColumn));
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
            var effect = new StaticKeyboardEffect(RazerUtils.Convert(SettingsManager.Settings.ColorSettings.StandbyColor));
            _chromaKeyboard?.SetStaticAsync(effect);
        }
    }
}