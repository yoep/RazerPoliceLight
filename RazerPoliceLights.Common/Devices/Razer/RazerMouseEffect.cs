using Colore;
using Colore.Effects.Mouse;
using RazerPoliceLights.Effects.Colors;
using RazerPoliceLightsBase.AbstractionLayer;
using RazerPoliceLightsBase.Effects;
using RazerPoliceLightsBase.Pattern;
using RazerPoliceLightsBase.Settings;

namespace RazerPoliceLightsBase.Devices.Razer
{
    public class RazerMouseEffect : AbstractMouseEffect
    {
        private readonly IChroma _chroma;
        private IMouse _chromaMouse;

        #region Constructors

        public RazerMouseEffect(IChroma chroma, INotification notification, ILogger logger, ISettingsManager settingsManager, IColorManager colorManager)
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

            Logger.Debug("Initializing Chroma.Instance.Mouse...");
            _chromaMouse = _chroma.Mouse;

            if (_chromaMouse != null)
            {
                Logger.Debug("Initialization of Chroma.Instance.Mouse done");
            }
            else
            {
                Logger.Warn("Chroma.Instance.Mouse could not be registered, do you have a Chroma supported mouse?");
            }
        }

        #endregion

        protected override void OnEffectTick(PatternRow playPattern)
        {
            if (_chromaMouse == null)
                return; //something probably went wrong during initialization, ignore this device effect playback

            var columnSize = MouseConstants.MaxColumns / playPattern.TotalColumns;
            var rowSize = MouseConstants.MaxRows / playPattern.TotalColumns;
            var startIndex = 0;

            for (var patternColumn = 0; patternColumn < playPattern.TotalColumns; patternColumn++)
            {
                var columnEndIndex = startIndex + columnSize;
                var rowEndIndex = startIndex + rowSize;

                if (IsMismatchingLastEndIndex(playPattern, MouseConstants.MaxColumns, patternColumn, columnEndIndex))
                    columnEndIndex = MouseConstants.MaxColumns;
                if (IsMismatchingLastEndIndex(playPattern, MouseConstants.MaxRows, patternColumn, columnEndIndex))
                    rowEndIndex = MouseConstants.MaxRows;

                if (IsAnimateVerticallyEnabled)
                {
                    AnimateVertical(playPattern, startIndex, rowEndIndex, patternColumn);
                    startIndex = rowEndIndex;
                }
                else
                {
                    AnimateHorizontal(playPattern, startIndex, columnEndIndex, patternColumn);
                    startIndex = columnEndIndex;
                }
            }
        }

        protected override void OnEffectStop()
        {
            var effect = new StaticMouseEffect(Led.All, RazerUtils.Convert(SettingsManager.Settings.ColorSettings.StandbyColor));
            _chromaMouse?.SetStaticAsync(effect);
        }

        private void AnimateHorizontal(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            for (var row = 0; row < MouseConstants.MaxRows; row++)
            {
                for (var column = startIndex; column < endIndex; column++)
                {
                    try
                    {
                        _chromaMouse[row, column] =
                            RazerUtils.Convert(GetPlaybackColumnColor(playPattern, patternColumn));
                    }
                    catch (ColoreException ex)
                    {
                        Logger.Warn("Chroma SDK has raised an issue the mouse: " + ex.Message, ex);
                    }
                }
            }
        }

        private void AnimateVertical(PatternRow playPattern, int startIndex, int endIndex, int patternColumn)
        {
            for (var column = 0; column < MouseConstants.MaxColumns; column++)
            {
                for (var row = startIndex; row < endIndex; row++)
                {
                    try
                    {
                        // make sure the row and column are allowed 
                        _chromaMouse[row, column] =
                            RazerUtils.Convert(GetPlaybackColumnColor(playPattern, patternColumn));
                    }
                    catch (ColoreException ex)
                    {
                        Logger.Warn("Chroma SDK has raised an issue for mouse: " + ex.Message, ex);
                    }
                }
            }
        }
    }
}